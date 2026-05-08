import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { forkJoin, finalize } from 'rxjs';
import { ScoutReport } from '../../scout-reports/scout-report.models';
import { PIPELINE_STATUSES, PipelineStatus, Player, WATCHLIST_PRIORITIES, WatchlistPriority } from '../player.models';
import { PlayerPhotoCacheService } from '../player-photo-cache.service';
import { PlayerService } from '../player.service';

@Component({
  selector: 'app-player-detail',
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
    return Math.round((values.reduce((sum, value) => sum + value, 0) / values.length) * 10) / 10;
  }

  private setPlayerState(player: Player): void {
    this.player.set(player);
    this.selectedPipelineStatus.set(player.pipelineStatus);
    this.watchlistPriority.set(player.watchlistPriority ?? 'Medium');
    this.watchlistReason.set(player.watchlistReason ?? '');
  }
}
