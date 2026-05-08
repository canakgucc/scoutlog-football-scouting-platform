export interface DashboardSummary {
  totalPlayers: number;
  totalScoutReports: number;
  averagePotentialScore: number;
  playersToWatchCount: number;
  latestScoutReports: LatestScoutReport[];
  topPotentialPlayers: TopPotentialPlayer[];
  positionDistribution: PositionDistribution[];
  recentPerformanceWarnings: string[];
  averageTechnicalScore: number;
  averagePhysicalScore: number;
  averageTacticalScore: number;
  averageMentalScore: number;
}

export interface LatestScoutReport {
  id: number;
  playerId: number;
  playerName: string;
  title: string;
  potentialScore: number;
  overallScore: number;
  recommendation: string;
  createdAt: string;
}

export interface TopPotentialPlayer {
  playerId: number;
  fullName: string;
  position: string;
  potentialScore: number;
  suggestedScore: number;
  recommendation: string;
  tags: string;
}

export interface PositionDistribution {
  position: string;
  count: number;
}
