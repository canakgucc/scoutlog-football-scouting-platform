import { Injectable } from '@angular/core';
import { AuthResponse, CurrentUser } from '../models/auth.models';

const TOKEN_KEY = 'scoutlog.accessToken';
const USER_KEY = 'scoutlog.currentUser';
const EXPIRES_AT_KEY = 'scoutlog.expiresAt';
const SESSION_KEYS = [
  TOKEN_KEY,
  USER_KEY,
  EXPIRES_AT_KEY,
  'scoutlog.clubId',
  'scoutlog.clubName',
  'scoutlog.user',
  'scoutlog.auth',
  'clubId',
  'clubName',
  'accessToken',
  'currentUser',
  'expiresAt'
];

@Injectable({ providedIn: 'root' })
export class TokenService {
  setSession(response: AuthResponse): void {
    localStorage.setItem(TOKEN_KEY, response.accessToken);
    localStorage.setItem(EXPIRES_AT_KEY, response.expiresAt);
    localStorage.setItem(
      USER_KEY,
      JSON.stringify({
        userId: response.userId,
        fullName: response.fullName,
        email: response.email,
        role: response.role,
        clubId: response.clubId,
        clubName: response.clubName
      } satisfies CurrentUser)
    );
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  getCurrentUser(): CurrentUser | null {
    const rawUser = localStorage.getItem(USER_KEY);

    if (!rawUser) {
      return null;
    }

    try {
      return JSON.parse(rawUser) as CurrentUser;
    } catch {
      this.clearSession();
      return null;
    }
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    const expiresAt = localStorage.getItem(EXPIRES_AT_KEY);

    if (!token || !expiresAt) {
      return false;
    }

    return new Date(expiresAt).getTime() > Date.now();
  }

  clearSession(): void {
    SESSION_KEYS.forEach((key) => localStorage.removeItem(key));
  }
}
