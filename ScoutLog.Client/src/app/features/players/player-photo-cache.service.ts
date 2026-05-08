import { Injectable } from '@angular/core';
import { Player } from './player.models';

const PHOTO_KEY_PREFIX = 'scoutlog.player.photo.';

@Injectable({ providedIn: 'root' })
export class PlayerPhotoCacheService {
  private pendingPhotoDataUrl = '';

  setPendingPhoto(dataUrl: string): void {
    this.pendingPhotoDataUrl = dataUrl;
  }

  clearPendingPhoto(): void {
    this.pendingPhotoDataUrl = '';
  }

  savePendingPhoto(playerId: number): void {
    if (!this.pendingPhotoDataUrl) {
      return;
    }

    localStorage.setItem(this.key(playerId), this.pendingPhotoDataUrl);
    this.clearPendingPhoto();
  }

  getPhotoSource(player: Player): string | null {
    const cachedPhoto = localStorage.getItem(this.key(player.id));

    if (cachedPhoto) {
      return cachedPhoto;
    }

    if (!player.photoUrl || player.photoUrl.startsWith('blob:')) {
      return null;
    }

    return player.photoUrl;
  }

  private key(playerId: number): string {
    return `${PHOTO_KEY_PREFIX}${playerId}`;
  }
}
