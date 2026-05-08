import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../../core/api.config';
import { DashboardSummary } from './dashboard.models';

@Injectable({ providedIn: 'root' })
export class DashboardService {
  private readonly http = inject(HttpClient);

  getSummary(): Observable<DashboardSummary> {
    return this.http.get<DashboardSummary>(`${API_BASE_URL}/api/dashboard/summary`, {
      headers: {
        'Cache-Control': 'no-cache',
        Pragma: 'no-cache'
      },
      params: {
        _: Date.now().toString()
      }
    });
  }
}
