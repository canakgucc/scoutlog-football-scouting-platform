import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { finalize } from 'rxjs';
import { DashboardService } from './dashboard.service';
import { DashboardSummary, PositionDistribution } from './dashboard.models';

@Component({
  selector: 'app-dashboard',
  imports: [DatePipe, DecimalPipe],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  private readonly dashboardService = inject(DashboardService);

  readonly summary = signal<DashboardSummary | null>(null);
  readonly isLoading = signal(true);
  readonly errorMessage = signal('');

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    console.log('Dashboard loading started');
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.dashboardService
      .getSummary()
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (summary) => {
          console.log('Dashboard response', summary);
          this.summary.set(summary);
        },
        error: (error) => {
          console.error('Dashboard loading failed', error);
          this.errorMessage.set('Dashboard data could not be loaded. Check the API connection.');
        }
      });
  }

  scorePercent(score: number): number {
    return Math.max(0, Math.min(100, score));
  }

  suggestedScorePercent(score: number): number {
    return Math.max(0, Math.min(100, score * 10));
  }

  maxPositionCount(items: PositionDistribution[]): number {
    return Math.max(1, ...items.map((item) => item.count));
  }

  positionPercent(item: PositionDistribution, items: PositionDistribution[]): number {
    return (item.count / this.maxPositionCount(items)) * 100;
  }
}
