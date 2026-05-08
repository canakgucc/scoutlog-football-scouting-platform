import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize, forkJoin } from 'rxjs';
import { Player } from '../players/player.models';
import { PlayerPhotoCacheService } from '../players/player-photo-cache.service';
import { PlayerService } from '../players/player.service';
import { ScoutReport } from '../scout-reports/scout-report.models';
import { ScoutReportService } from '../scout-reports/scout-report.service';

interface ComparisonRow {
  label: string;
  leftValue: string;
  rightValue: string;
  leftMetric: number | null;
  rightMetric: number | null;
  note: string;
}

interface ScoreAverages {
  technical: number | null;
  physical: number | null;
  tactical: number | null;
  mental: number | null;
  potential: number | null;
  reportCount: number;
}

@Component({
  selector: 'app-player-comparison',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './player-comparison.component.html',
  styleUrl: './player-comparison.component.css'
})
export class PlayerComparisonComponent implements OnInit {
  private readonly playerService = inject(PlayerService);
  private readonly photoCache = inject(PlayerPhotoCacheService);
  private readonly scoutReportService = inject(ScoutReportService);
  private scoreRequestId = 0;

  readonly players = signal<Player[]>([]);
  readonly brokenPhotoIds = signal<Set<number>>(new Set());
  readonly isLoading = signal(true);
  readonly isLoadingScores = signal(false);
  readonly errorMessage = signal('');
  readonly scoreErrorMessage = signal('');
  readonly selectedLeftId = signal<number | null>(null);
  readonly selectedRightId = signal<number | null>(null);
  readonly leftAverages = signal<ScoreAverages | null>(null);
  readonly rightAverages = signal<ScoreAverages | null>(null);

  readonly leftPlayer = computed(() => this.findPlayer(this.selectedLeftId()));
  readonly rightPlayer = computed(() => this.findPlayer(this.selectedRightId()));

  readonly comparisonRows = computed<ComparisonRow[]>(() => {
    const left = this.leftPlayer();
    const right = this.rightPlayer();

    if (!left || !right) {
      return [];
    }

    return [
      this.textRow('Position', left.position, right.position),
      this.textRow('Preferred foot', left.preferredFoot, right.preferredFoot),
      this.metricRow('Age', this.calculateAge(left.birthDate), this.calculateAge(right.birthDate), 'yrs'),
      this.metricRow('Height', left.height, right.height, 'cm'),
      this.metricRow('Weight', left.weight, right.weight, 'kg'),
      this.textRow('Status', left.status, right.status),
      this.scoreRow('Technical avg', this.leftAverages()?.technical ?? null, this.rightAverages()?.technical ?? null),
      this.scoreRow('Physical avg', this.leftAverages()?.physical ?? null, this.rightAverages()?.physical ?? null),
      this.scoreRow('Tactical avg', this.leftAverages()?.tactical ?? null, this.rightAverages()?.tactical ?? null),
      this.scoreRow('Mental avg', this.leftAverages()?.mental ?? null, this.rightAverages()?.mental ?? null),
      this.scoreRow('Potential', this.leftAverages()?.potential ?? null, this.rightAverages()?.potential ?? null)
    ];
  });

  ngOnInit(): void {
    this.loadPlayers();
  }

  loadPlayers(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.playerService
      .getPlayers()
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (players) => {
          const leftId = players[0]?.id ?? null;
          const rightId = players[1]?.id ?? players[0]?.id ?? null;

          this.players.set(players);
          this.selectedLeftId.set(leftId);
          this.selectedRightId.set(rightId);
          this.loadScoreAverages(leftId, rightId);
        },
        error: () => this.errorMessage.set('Players could not be loaded for comparison.')
      });
  }

  setLeftPlayer(value: string): void {
    const playerId = Number(value);
    this.selectedLeftId.set(playerId);
    this.loadScoreAverages(playerId, this.selectedRightId());
  }

  setRightPlayer(value: string): void {
    const playerId = Number(value);
    this.selectedRightId.set(playerId);
    this.loadScoreAverages(this.selectedLeftId(), playerId);
  }

  loadScoreAverages(leftId: number | null, rightId: number | null): void {
    if (!leftId || !rightId) {
      this.leftAverages.set(null);
      this.rightAverages.set(null);
      return;
    }

    const requestId = ++this.scoreRequestId;
    this.isLoadingScores.set(true);
    this.scoreErrorMessage.set('');
    this.leftAverages.set(null);
    this.rightAverages.set(null);

    forkJoin({
      leftReports: this.scoutReportService.getReportsByPlayerId(leftId),
      rightReports: this.scoutReportService.getReportsByPlayerId(rightId)
    })
      .pipe(finalize(() => {
        if (requestId === this.scoreRequestId) {
          this.isLoadingScores.set(false);
        }
      }))
      .subscribe({
        next: ({ leftReports, rightReports }) => {
          if (requestId !== this.scoreRequestId) {
            return;
          }

          this.leftAverages.set(this.calculateAverages(leftReports));
          this.rightAverages.set(this.calculateAverages(rightReports));
        },
        error: () => {
          if (requestId !== this.scoreRequestId) {
            return;
          }

          this.scoreErrorMessage.set('Scout report averages could not be loaded.');
        }
      });
  }

  playerName(player: Player | null): string {
    return player ? `${player.firstName} ${player.lastName}` : 'Select player';
  }

  initials(player: Player | null): string {
    if (!player) {
      return 'SL';
    }

    return `${player.firstName.charAt(0)}${player.lastName.charAt(0)}`.toUpperCase();
  }

  photoSource(player: Player | null): string | null {
    if (!player || this.brokenPhotoIds().has(player.id)) {
      return null;
    }

    return this.photoCache.getPhotoSource(player);
  }

  onPhotoError(player: Player | null): void {
    if (!player) {
      return;
    }

    this.brokenPhotoIds.update((ids) => new Set(ids).add(player.id));
  }

  calculateAge(birthDate: string): number {
    const birth = new Date(birthDate);
    const today = new Date();
    let age = today.getFullYear() - birth.getFullYear();
    const monthDiff = today.getMonth() - birth.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) {
      age--;
    }

    return age;
  }

  winner(row: ComparisonRow): 'left' | 'right' | 'tie' | 'none' {
    if (row.leftMetric === null || row.rightMetric === null) {
      return 'none';
    }

    if (row.leftMetric === row.rightMetric) {
      return 'tie';
    }

    return row.leftMetric > row.rightMetric ? 'left' : 'right';
  }

  barWidth(value: number | null, row: ComparisonRow): number {
    if (value === null || row.leftMetric === null || row.rightMetric === null) {
      return 0;
    }

    const max = Math.max(row.leftMetric, row.rightMetric, 1);
    return Math.max(8, Math.round((value / max) * 100));
  }

  isCategoricalRow(row: ComparisonRow): boolean {
    return ['Position', 'Preferred foot', 'Status'].includes(row.label);
  }

  deltaLabel(row: ComparisonRow): string {
    if (row.leftMetric === null || row.rightMetric === null || row.leftMetric === row.rightMetric) {
      return 'Equal';
    }

    const delta = Math.abs(row.leftMetric - row.rightMetric);
    const unit = row.label === 'Height' ? ' cm' : row.label === 'Weight' ? ' kg' : row.label === 'Age' ? ' yrs' : '';
    const value = Number.isInteger(delta) ? `${delta}` : delta.toFixed(1);

    return `+${value}${unit}`;
  }

  private findPlayer(id: number | null): Player | null {
    return this.players().find((player) => player.id === id) ?? null;
  }

  private textRow(label: string, leftValue: string, rightValue: string): ComparisonRow {
    const same = leftValue.trim().toLowerCase() === rightValue.trim().toLowerCase();

    return {
      label,
      leftValue,
      rightValue,
      leftMetric: same ? 1 : null,
      rightMetric: same ? 1 : null,
      note: same ? 'Same profile marker' : 'Profile comparison'
    };
  }

  private metricRow(label: string, left: number, right: number, unit: string): ComparisonRow {
    return {
      label,
      leftValue: `${left} ${unit}`,
      rightValue: `${right} ${unit}`,
      leftMetric: left,
      rightMetric: right,
      note: 'Higher value highlighted'
    };
  }

  private scoreRow(label: string, leftScore: number | null, rightScore: number | null): ComparisonRow {
    return {
      label,
      leftValue: this.scoreValue(leftScore),
      rightValue: this.scoreValue(rightScore),
      leftMetric: leftScore,
      rightMetric: rightScore,
      note: this.scoreNote()
    };
  }

  private scoreValue(score: number | null): string {
    if (this.isLoadingScores()) {
      return 'Loading...';
    }

    return score === null ? 'No reports yet' : `${score}`;
  }

  private scoreNote(): string {
    if (this.isLoadingScores()) {
      return 'Loading report averages';
    }

    return 'Calculated from player scout reports when reports exist';
  }

  private calculateAverages(reports: ScoutReport[]): ScoreAverages {
    return {
      technical: this.average(reports.map((report) => report.technicalScore)),
      physical: this.average(reports.map((report) => report.physicalScore)),
      tactical: this.average(reports.map((report) => report.tacticalScore)),
      mental: this.average(reports.map((report) => report.mentalScore)),
      potential: this.average(reports.map((report) => report.potentialScore)),
      reportCount: reports.length
    };
  }

  private average(values: number[]): number | null {
    if (values.length === 0) {
      return null;
    }

    return Math.round((values.reduce((sum, value) => sum + value, 0) / values.length) * 10) / 10;
  }
}
