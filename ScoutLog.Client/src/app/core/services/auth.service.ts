import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { API_BASE_URL } from '../api.config';
import { AuthResponse, CurrentUser, LoginRequest } from '../models/auth.models';
import { TokenService } from './token.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly tokenService = inject(TokenService);

  login(request: LoginRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${API_BASE_URL}/api/Auth/login`, request)
      .pipe(tap((response) => this.tokenService.setSession(response)));
  }

  logout(): void {
    this.tokenService.clearSession();
  }

  isAuthenticated(): boolean {
    return this.tokenService.isAuthenticated();
  }

  getCurrentUser(): CurrentUser | null {
    return this.tokenService.getCurrentUser();
  }
}
