export interface ScoutReport {
  id: number;
  playerId: number;
  scoutId: number;
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
  title: string;
  observationText: string;
  technicalScore: number;
  physicalScore: number;
  tacticalScore: number;
  mentalScore: number;
  potentialScore: number;
  recommendation: string;
}
