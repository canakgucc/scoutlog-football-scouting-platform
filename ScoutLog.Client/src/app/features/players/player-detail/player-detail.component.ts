import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { forkJoin, finalize } from 'rxjs';
import { ScoutReport } from '../../scout-reports/scout-report.models';
import { PIPELINE_STATUSES, PipelineStatus, Player, WATCHLIST_PRIORITIES, WatchlistPriority } from '../player.models';
import { PlayerPhotoCacheService } from '../player-photo-cache.service';
import { PlayerService } from '../player.service';

type DevelopmentTrend = 'Improving' | 'Stable' | 'Declining';

interface TimelinePoint {
  report: ScoutReport;
  compositeScore: number;
}

interface ScoreDelta {
  label: string;
  latest: number | null;
  previous: number | null;
  delta: number | null;
}

interface FrequencyItem {
  label: string;
  count: number;
}

@Component({
  selector: 'app-player-detail',
  standalone: true,
  imports: [DatePipe, DecimalPipe, FormsModule, RouterLink],
  templateUrl: './player-detail.component.html',
  styleUrl: './player-detail.component.css'
})
export class PlayerDetailComponent implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly playerService = inject(PlayerService);
  private readonly photoCache = inject(PlayerPhotoCacheService);

  readonly player = signal<Player | null>(null);
  readonly reports = signal<ScoutReport[]>([]);
  readonly hasBrokenPhoto = signal(false);
  readonly isLoading = signal(true);
  readonly isPipelineSaving = signal(false);
  readonly isWatchlistSaving = signal(false);
  readonly errorMessage = signal('');
  readonly pipelineMessage = signal('');
  readonly watchlistMessage = signal('');
  readonly selectedPipelineStatus = signal<PipelineStatus>('New');
  readonly watchlistPriority = signal<WatchlistPriority>('Medium');
  readonly watchlistReason = signal('');
  readonly pipelineStatuses = PIPELINE_STATUSES;
  readonly watchlistPriorities = WATCHLIST_PRIORITIES;

  readonly latestReport = computed(() => this.reports()[0] ?? null);
  readonly timelinePoints = computed<TimelinePoint[]>(() =>
    [...this.reports()]
      .sort((a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime())
      .map((report) => ({
        report,
        compositeScore: this.compositeScore(report)
      }))
  );
  readonly recentTrend = computed<DevelopmentTrend>(() => {
    const recentPoints = this.timelinePoints().slice(-5);

    if (recentPoints.length < 2) {
      return 'Stable';
    }

    const delta =
      recentPoints[recentPoints.length - 1].compositeScore - recentPoints[0].compositeScore;

    if (delta >= 3) {
      return 'Improving';
    }

    if (delta <= -3) {
      return 'Declining';
    }

    return 'Stable';
  });
  readonly recentTrendDelta = computed(() => {
    const recentPoints = this.timelinePoints().slice(-5);

    if (recentPoints.length < 2) {
      return 0;
    }

    return this.roundScore(
      recentPoints[recentPoints.length - 1].compositeScore - recentPoints[0].compositeScore
    );
  });
  readonly scoreDeltas = computed<ScoreDelta[]>(() => {
    const [latest, previous] = this.reports();

    return [
      this.buildScoreDelta('Technical', latest?.technicalScore, previous?.technicalScore),
      this.buildScoreDelta('Physical', latest?.physicalScore, previous?.physicalScore),
      this.buildScoreDelta('Tactical', latest?.tacticalScore, previous?.tacticalScore),
      this.buildScoreDelta('Mental', latest?.mentalScore, previous?.mentalScore),
      this.buildScoreDelta('Potential', latest?.potentialScore, previous?.potentialScore)
    ];
  });
  readonly repeatedWeaknesses = computed(() => this.topFrequencyItems('weaknesses'));
  readonly repeatedTags = computed(() => this.topFrequencyItems('tags'));
  readonly developmentSummary = computed(() => {
    const reportCount = this.reports().length;

    if (reportCount === 0) {
      return 'No development data yet.';
    }

    if (reportCount === 1) {
      return 'Initial scouting baseline created.';
    }

    const trend = this.recentTrend();

    if (trend === 'Improving') {
      return 'Recent reports show positive development across the player profile.';
    }

    if (trend === 'Declining') {
      return 'Recent scores are trending down, so the player should be reviewed closely.';
    }

    return 'Recent reports show a stable profile with targeted development areas.';
  });
  readonly averageScores = computed(() => {
    const reports = this.reports();

    if (reports.length === 0) {
      return {
        technical: 0,
        physical: 0,
        tactical: 0,
        mental: 0,
        potential: 0
      };
    }

    return {
      technical: this.average(reports.map((report) => report.technicalScore)),
      physical: this.average(reports.map((report) => report.physicalScore)),
      tactical: this.average(reports.map((report) => report.tacticalScore)),
      mental: this.average(reports.map((report) => report.mentalScore)),
      potential: this.average(reports.map((report) => report.potentialScore))
    };
  });

  ngOnInit(): void {
    this.loadPlayer();
  }

  loadPlayer(): void {
    const playerId = Number(this.route.snapshot.paramMap.get('id'));

    if (!playerId) {
      this.errorMessage.set('Invalid player id.');
      this.isLoading.set(false);
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set('');

    forkJoin({
      player: this.playerService.getPlayerById(playerId),
      reports: this.playerService.getReportsByPlayerId(playerId)
    })
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: ({ player, reports }) => {
          this.setPlayerState(player);
          this.hasBrokenPhoto.set(false);
          this.reports.set(
            [...reports].sort(
              (a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
            )
          );
        },
        error: () => this.errorMessage.set('Player detail could not be loaded.')
      });
  }

  savePipelineStatus(): void {
    const player = this.player();

    if (!player || this.isPipelineSaving()) {
      return;
    }

    this.isPipelineSaving.set(true);
    this.pipelineMessage.set('');

    this.playerService
      .updatePipelineStatus(player.id, { pipelineStatus: this.selectedPipelineStatus() })
      .pipe(finalize(() => this.isPipelineSaving.set(false)))
      .subscribe({
        next: (updatedPlayer) => {
          this.setPlayerState(updatedPlayer);
          this.pipelineMessage.set('Pipeline status updated.');
        },
        error: () => this.pipelineMessage.set('Pipeline status could not be updated.')
      });
  }

  saveWatchlist(): void {
    const player = this.player();

    if (!player || this.isWatchlistSaving()) {
      return;
    }

    if (!this.watchlistReason().trim()) {
      this.watchlistMessage.set('Watchlist reason is required.');
      return;
    }

    this.isWatchlistSaving.set(true);
    this.watchlistMessage.set('');

    this.playerService
      .addOrUpdateWatchlist(player.id, {
        priority: this.watchlistPriority(),
        reason: this.watchlistReason().trim()
      })
      .pipe(finalize(() => this.isWatchlistSaving.set(false)))
      .subscribe({
        next: (updatedPlayer) => {
          this.setPlayerState(updatedPlayer);
          this.watchlistMessage.set('Watchlist updated.');
        },
        error: () => this.watchlistMessage.set('Watchlist could not be updated.')
      });
  }

  removeWatchlist(): void {
    const player = this.player();

    if (!player || this.isWatchlistSaving()) {
      return;
    }

    this.isWatchlistSaving.set(true);
    this.watchlistMessage.set('');

    this.playerService
      .removeFromWatchlist(player.id)
      .pipe(finalize(() => this.isWatchlistSaving.set(false)))
      .subscribe({
        next: () => {
          this.setPlayerState({
            ...player,
            isWatchlisted: false,
            watchlistPriority: null,
            watchlistReason: null
          });
          this.watchlistMessage.set('Player removed from watchlist.');
        },
        error: () => this.watchlistMessage.set('Watchlist item could not be removed.')
      });
  }

  setPipelineStatus(value: PipelineStatus): void {
    this.selectedPipelineStatus.set(value);
    this.pipelineMessage.set('');
  }

  setWatchlistPriority(value: WatchlistPriority): void {
    this.watchlistPriority.set(value);
    this.watchlistMessage.set('');
  }

  setWatchlistReason(value: string): void {
    this.watchlistReason.set(value);
    this.watchlistMessage.set('');
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

  scorePercent(score: number): number {
    return Math.max(0, Math.min(100, score));
  }

  suggestedScorePercent(score: number | null): number {
    return Math.max(0, Math.min(100, (score ?? 0) * 10));
  }

  compositeScore(report: ScoutReport): number {
    return this.roundScore(
      (report.technicalScore +
        report.physicalScore +
        report.tacticalScore +
        report.mentalScore +
        report.potentialScore) /
        5
    );
  }

  trendClass(trend: DevelopmentTrend): string {
    return trend.toLowerCase();
  }

  deltaClass(delta: number | null): string {
    if (delta === null || delta === 0) {
      return 'neutral';
    }

    return delta > 0 ? 'positive' : 'negative';
  }

  formatDelta(delta: number | null): string {
    if (delta === null) {
      return 'No previous report';
    }

    if (delta === 0) {
      return '0';
    }

    return delta > 0 ? `+${delta}` : `${delta}`;
  }

  reportContext(report: ScoutReport): string {
    const opponent =
      report.reportType === 'Training'
        ? report.opponent || 'Training session'
        : report.opponent || 'Opponent not set';
    const position = report.observedPosition ? ` · ${report.observedPosition}` : '';
    const minutes = report.minutesPlayed !== null ? ` · ${report.minutesPlayed} min` : '';

    return `${report.reportType} · ${opponent}${position}${minutes}`;
  }

  photoSource(player: Player): string | null {
    if (this.hasBrokenPhoto()) {
      return null;
    }

    return this.photoCache.getPhotoSource(player);
  }

  onPhotoError(): void {
    this.hasBrokenPhoto.set(true);
  }

  private average(values: number[]): number {
    return this.roundScore(values.reduce((sum, value) => sum + value, 0) / values.length);
  }

  private setPlayerState(player: Player): void {
    this.player.set(player);
    this.selectedPipelineStatus.set(player.pipelineStatus);
    this.watchlistPriority.set(player.watchlistPriority ?? 'Medium');
    this.watchlistReason.set(player.watchlistReason ?? '');
  }

  private buildScoreDelta(
    label: string,
    latest: number | undefined,
    previous: number | undefined
  ): ScoreDelta {
    return {
      label,
      latest: latest ?? null,
      previous: previous ?? null,
      delta: latest === undefined || previous === undefined ? null : latest - previous
    };
  }

  private topFrequencyItems(field: 'weaknesses' | 'tags'): FrequencyItem[] {
    const counts = new Map<string, FrequencyItem>();

    for (const report of this.reports()) {
      const source = report[field];

      for (const item of this.splitReportList(source)) {
        const key = item.toLocaleLowerCase('tr-TR');
        const existing = counts.get(key);

        if (existing) {
          existing.count += 1;
        } else {
          counts.set(key, { label: item, count: 1 });
        }
      }
    }

    return Array.from(counts.values())
      .sort((a, b) => b.count - a.count || a.label.localeCompare(b.label))
      .slice(0, 5);
  }

  private splitReportList(value: string | null): string[] {
    if (!value) {
      return [];
    }

    return value
      .split(/[;,]/)
      .map((item) => item.trim())
      .filter(Boolean);
  }

  private roundScore(value: number): number {
    return Math.round(value * 10) / 10;
  }
}
