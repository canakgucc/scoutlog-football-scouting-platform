import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { Player } from '../player.models';
import { PlayerPhotoCacheService } from '../player-photo-cache.service';
import { PlayerService } from '../player.service';

@Component({
  selector: 'app-player-list',
  imports: [FormsModule, RouterLink],
  templateUrl: './player-list.component.html',
  styleUrl: './player-list.component.css'
})
export class PlayerListComponent implements OnInit {
  private readonly playerService = inject(PlayerService);
  private readonly photoCache = inject(PlayerPhotoCacheService);

  readonly players = signal<Player[]>([]);
  readonly brokenPhotoIds = signal<Set<number>>(new Set());
  readonly isLoading = signal(true);
  readonly deletingPlayerId = signal<number | null>(null);
  readonly playerPendingDelete = signal<Player | null>(null);
  readonly errorMessage = signal('');
  readonly deleteErrorMessage = signal('');
  readonly searchTerm = signal('');
  readonly selectedPosition = signal('All');
  readonly selectedStatus = signal('All');

  readonly positions = computed(() => [
    'All',
    ...Array.from(new Set(this.players().map((player) => player.position))).sort()
  ]);

  readonly filteredPlayers = computed(() => {
    const term = this.searchTerm().trim().toLowerCase();
    const position = this.selectedPosition();
    const status = this.selectedStatus();

    return this.players().filter((player) => {
      const fullName = `${player.firstName} ${player.lastName}`.toLowerCase();
      const matchesSearch =
        !term ||
        fullName.includes(term) ||
        player.position.toLowerCase().includes(term) ||
        player.nationality.toLowerCase().includes(term);
      const matchesPosition = position === 'All' || player.position === position;
      const matchesStatus = status === 'All' || player.status === status;

      return matchesSearch && matchesPosition && matchesStatus;
    });
  });

  ngOnInit(): void {
    console.log('players list route state', history.state);
    this.loadPlayers();
  }

  loadPlayers(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');
    this.deleteErrorMessage.set('');
    this.players.set([]);

    this.playerService
      .getPlayers()
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (players) => {
          console.log('players list response', players);
          this.players.set([...players]);
        },
        error: (error) => {
          console.error('players list error', error);
          this.errorMessage.set('Players could not be loaded. Check the API connection.');
        }
      });
  }

  setSearchTerm(value: string): void {
    this.searchTerm.set(value);
  }

  setPosition(value: string): void {
    this.selectedPosition.set(value);
  }

  setStatus(value: string): void {
    this.selectedStatus.set(value);
  }

  photoSource(player: Player): string | null {
    if (this.brokenPhotoIds().has(player.id)) {
      return null;
    }

    return this.photoCache.getPhotoSource(player);
  }

  onPhotoError(player: Player): void {
    this.brokenPhotoIds.update((ids) => new Set(ids).add(player.id));
  }

  openDeleteDialog(player: Player): void {
    this.deleteErrorMessage.set('');
    this.playerPendingDelete.set(player);
  }

  closeDeleteDialog(): void {
    if (this.deletingPlayerId()) {
      return;
    }

    this.playerPendingDelete.set(null);
  }

  deletePlayer(): void {
    const player = this.playerPendingDelete();

    if (!player || this.deletingPlayerId()) {
      return;
    }

    this.deletingPlayerId.set(player.id);
    this.deleteErrorMessage.set('');

    this.playerService
      .deletePlayer(player.id)
      .pipe(finalize(() => this.deletingPlayerId.set(null)))
      .subscribe({
        next: () => {
          this.players.update((players) => players.filter((item) => item.id !== player.id));
          this.playerPendingDelete.set(null);
          this.loadPlayers();
        },
        error: (error) => {
          console.error('player delete error', error);
          this.deleteErrorMessage.set(
            'Player could not be deleted. If the player has related records, the API may block the operation.'
          );
        }
      });
  }

  calculateAge(birthDate: string): number {
    const birth = new Date(birthDate);
    const today = new Date();
    let age = today.getFullYear() - birth.getFullYear();
    const monthDiff = today.getMonth() - birth.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) {
      age--;
    }

    return age;
  }
}
