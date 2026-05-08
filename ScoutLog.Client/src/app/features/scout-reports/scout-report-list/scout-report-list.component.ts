import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { forkJoin, finalize } from 'rxjs';
import { Player } from '../../players/player.models';
import { PlayerService } from '../../players/player.service';
import { ScoutReport } from '../scout-report.models';
import { ScoutReportService } from '../scout-report.service';

@Component({
  selector: 'app-scout-report-list',
  imports: [DatePipe, FormsModule, RouterLink],
  templateUrl: './scout-report-list.component.html',
  styleUrl: './scout-report-list.component.css'
})
export class ScoutReportListComponent implements OnInit {
  private readonly scoutReportService = inject(ScoutReportService);
  private readonly playerService = inject(PlayerService);

  readonly reports = signal<ScoutReport[]>([]);
  readonly players = signal<Player[]>([]);
  readonly isLoading = signal(true);
  readonly errorMessage = signal('');
  readonly searchTerm = signal('');
  readonly selectedPlayerId = signal('All');
  readonly selectedRecommendation = signal('All');

  readonly recommendations = computed(() => [
    'All',
    ...Array.from(new Set(this.reports().map((report) => report.recommendation))).sort()
  ]);

  readonly filteredReports = computed(() => {
    const term = this.searchTerm().trim().toLowerCase();
    const playerId = this.selectedPlayerId();
    const recommendation = this.selectedRecommendation();

    return this.reports().filter((report) => {
      const playerName = this.playerName(report.playerId).toLowerCase();
      const text = [
        report.title,
        report.observationText,
        report.analysisSummary ?? '',
        report.strengths ?? '',
        report.weaknesses ?? '',
        report.tags ?? '',
        playerName
      ]
        .join(' ')
        .toLowerCase();

      const matchesText = !term || text.includes(term);
      const matchesPlayer = playerId === 'All' || report.playerId === Number(playerId);
      const matchesRecommendation =
        recommendation === 'All' || report.recommendation === recommendation;

      return matchesText && matchesPlayer && matchesRecommendation;
    });
  });

  ngOnInit(): void {
    this.loadReports();
  }

  loadReports(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    forkJoin({
      reports: this.scoutReportService.getReports(),
      players: this.playerService.getPlayers()
    })
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: ({ reports, players }) => {
          this.reports.set(
            [...reports].sort(
              (a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
            )
          );
          this.players.set(players);
        },
        error: () => this.errorMessage.set('Scout reports could not be loaded.')
      });
  }

  setSearchTerm(value: string): void {
    this.searchTerm.set(value);
  }

  setPlayer(value: string): void {
    this.selectedPlayerId.set(value);
  }

  setRecommendation(value: string): void {
    this.selectedRecommendation.set(value);
  }

  playerName(playerId: number): string {
    const player = this.players().find((item) => item.id === playerId);
    return player ? `${player.firstName} ${player.lastName}` : `Player #${playerId}`;
  }

  splitValues(value: string | null): string[] {
    return (value || '')
      .split(';')
      .map((item) => item.trim())
      .filter(Boolean);
  }
}
