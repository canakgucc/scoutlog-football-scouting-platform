export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  accessToken: string;
  expiresAt: string;
  userId: number;
  fullName: string;
  email: string;
  role: string;
  clubId: number;
  clubName: string;
}

export interface CurrentUser {
  userId: number;
  fullName: string;
  email: string;
  role: string;
  clubId: number;
  clubName: string;
}
