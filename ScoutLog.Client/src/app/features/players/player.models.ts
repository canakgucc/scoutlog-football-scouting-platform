export interface Player {
  id: number;
  clubId: number;
  teamId: number | null;
  firstName: string;
  lastName: string;
  birthDate: string;
  position: string;
  preferredFoot: string;
  height: number;
  weight: number;
  nationality: string;
  photoUrl: string | null;
  status: string;
  pipelineStatus: PipelineStatus;
  isWatchlisted: boolean;
  watchlistPriority: WatchlistPriority | null;
  watchlistReason: string | null;
  technicalScore?: number | null;
  physicalScore?: number | null;
  tacticalScore?: number | null;
  mentalScore?: number | null;
  potentialScore?: number | null;
  averageTechnicalScore?: number | null;
  averagePhysicalScore?: number | null;
  averageTacticalScore?: number | null;
  averageMentalScore?: number | null;
  averagePotentialScore?: number | null;
}

export interface CreatePlayerRequest {
  teamId: number | null;
  firstName: string;
  lastName: string;
  birthDate: string;
  position: string;
  preferredFoot: string;
  height: number;
  weight: number;
  nationality: string;
  photoUrl: string | null;
  status: string;
}

export type PipelineStatus =
  | 'New'
  | 'Under Observation'
  | 'Follow-up Needed'
  | 'Shortlisted'
  | 'Recommended'
  | 'Rejected';

export type WatchlistPriority = 'Low' | 'Medium' | 'High';

export interface UpdatePipelineStatusRequest {
  pipelineStatus: PipelineStatus;
}

export interface UpsertWatchlistRequest {
  priority: WatchlistPriority;
  reason: string;
}

export const PIPELINE_STATUSES: PipelineStatus[] = [
  'New',
  'Under Observation',
  'Follow-up Needed',
  'Shortlisted',
  'Recommended',
  'Rejected'
];

export const WATCHLIST_PRIORITIES: WatchlistPriority[] = ['Low', 'Medium', 'High'];
