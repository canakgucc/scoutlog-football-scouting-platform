export interface ScoutReport {
  id: number;
  playerId: number;
  scoutId: number;
  reportType: ReportType;
  eventDate: string;
  opponent: string | null;
  competition: string | null;
  minutesPlayed: number | null;
  observedPosition: string | null;
  title: string;
  observationText: string;
  technicalScore: number;
  physicalScore: number;
  tacticalScore: number;
  mentalScore: number;
  potentialScore: number;
  overallScore: number;
  recommendation: string;
  analysisSummary: string | null;
  strengths: string | null;
  weaknesses: string | null;
  suggestedActions: string | null;
  suggestedScore: number | null;
  tags: string | null;
  developmentAdvice: string | null;
  createdAt: string;
}

export interface CreateScoutReportRequest {
  playerId: number;
  reportType: ReportType;
  eventDate: string;
  opponent: string | null;
  competition: string | null;
  minutesPlayed: number | null;
  observedPosition: string | null;
  title: string;
  observationText: string;
  technicalScore: number;
  physicalScore: number;
  tacticalScore: number;
  mentalScore: number;
  potentialScore: number;
  recommendation: string;
}

export type ReportType = 'Match' | 'Training';
