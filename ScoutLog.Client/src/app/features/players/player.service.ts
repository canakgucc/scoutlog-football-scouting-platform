import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../../core/api.config';
import { ScoutReport } from '../scout-reports/scout-report.models';
import { CreatePlayerRequest, Player } from './player.models';

@Injectable({ providedIn: 'root' })
export class PlayerService {
  private readonly http = inject(HttpClient);

  getPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(`${API_BASE_URL}/api/Players`, {
      headers: {
        'Cache-Control': 'no-cache',
        Pragma: 'no-cache'
      },
      params: {
        _: Date.now().toString()
      }
    });
  }

  getPlayerById(id: number): Observable<Player> {
    return this.http.get<Player>(`${API_BASE_URL}/api/Players/${id}`);
  }

  createPlayer(request: CreatePlayerRequest): Observable<Player> {
    return this.http.post<Player>(`${API_BASE_URL}/api/Players`, request);
  }

  deletePlayer(id: number): Observable<void> {
    return this.http.delete<void>(`${API_BASE_URL}/api/Players/${id}`);
  }

  getReportsByPlayerId(playerId: number): Observable<ScoutReport[]> {
    return this.http.get<ScoutReport[]>(`${API_BASE_URL}/api/scout-reports/player/${playerId}`);
  }
}
