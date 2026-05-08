import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { forkJoin, finalize } from 'rxjs';
import { ScoutReport } from '../../scout-reports/scout-report.models';
import { Player } from '../player.models';
import { PlayerPhotoCacheService } from '../player-photo-cache.service';
import { PlayerService } from '../player.service';

@Component({
  selector: 'app-player-detail',
  imports: [DatePipe, DecimalPipe, RouterLink],
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
  readonly errorMessage = signal('');

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
          this.player.set(player);
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
}
