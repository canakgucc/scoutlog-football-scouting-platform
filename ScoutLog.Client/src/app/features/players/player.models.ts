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
