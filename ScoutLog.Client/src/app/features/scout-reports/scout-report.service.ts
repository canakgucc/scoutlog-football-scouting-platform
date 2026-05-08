import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../../core/api.config';
import { CreateScoutReportRequest, ScoutReport } from './scout-report.models';

@Injectable({ providedIn: 'root' })
export class ScoutReportService {
  private readonly http = inject(HttpClient);

  getReports(): Observable<ScoutReport[]> {
    return this.http.get<ScoutReport[]>(`${API_BASE_URL}/api/scout-reports`, this.noCacheOptions());
  }

  getReportById(id: number): Observable<ScoutReport> {
    return this.http.get<ScoutReport>(`${API_BASE_URL}/api/scout-reports/${id}`, this.noCacheOptions());
  }

  getReportsByPlayerId(playerId: number): Observable<ScoutReport[]> {
    return this.http.get<ScoutReport[]>(
      `${API_BASE_URL}/api/scout-reports/player/${playerId}`,
      this.noCacheOptions()
    );
  }

  createReport(request: CreateScoutReportRequest): Observable<ScoutReport> {
    return this.http.post<ScoutReport>(`${API_BASE_URL}/api/scout-reports`, request);
  }

  analyzeReport(id: number): Observable<unknown> {
    return this.http.post(`${API_BASE_URL}/api/scout-reports/${id}/analyze`, {});
  }

  private noCacheOptions() {
    return {
      headers: {
        'Cache-Control': 'no-cache',
        Pragma: 'no-cache'
      },
      params: {
        _: Date.now().toString()
      }
    };
  }
}
