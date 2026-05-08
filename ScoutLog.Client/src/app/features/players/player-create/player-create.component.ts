import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { CreatePlayerRequest } from '../player.models';
import { PlayerPhotoCacheService } from '../player-photo-cache.service';
import { PlayerService } from '../player.service';

@Component({
  selector: 'app-player-create',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './player-create.component.html',
  styleUrl: './player-create.component.css'
})
export class PlayerCreateComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly playerService = inject(PlayerService);
  private readonly photoCache = inject(PlayerPhotoCacheService);
  private readonly router = inject(Router);
  private submitLocked = false;

  readonly isSubmitting = signal(false);
  readonly errorMessage = signal('');
  readonly photoPreview = signal('');

  readonly form = this.formBuilder.nonNullable.group({
    firstName: ['', [Validators.required, Validators.maxLength(80)]],
    lastName: ['', [Validators.required, Validators.maxLength(80)]],
    birthDate: ['2008-04-12', [Validators.required]],
    position: ['Right Winger', [Validators.required, Validators.maxLength(40)]],
    preferredFoot: ['Right', [Validators.required, Validators.maxLength(20)]],
    height: [178, [Validators.required, Validators.min(120), Validators.max(230)]],
    weight: [70, [Validators.required, Validators.min(35), Validators.max(130)]],
    nationality: ['Turkish', [Validators.required, Validators.maxLength(80)]],
    photoUrl: ['', [Validators.maxLength(500)]],
    status: ['Active', [Validators.required, Validators.maxLength(40)]],
    teamId: [1, [Validators.required, Validators.min(1)]]
  });

  submit(): void {
    if (this.form.invalid || this.isSubmitting() || this.submitLocked) {
      this.form.markAllAsTouched();
      return;
    }

    const value = this.form.getRawValue();
    const request: CreatePlayerRequest = {
      ...value,
      photoUrl: null
    };

    console.log('create payload', request);

    this.submitLocked = true;
    this.isSubmitting.set(true);
    this.errorMessage.set('');

    this.playerService
      .createPlayer(request)
      .pipe(finalize(() => {
        this.isSubmitting.set(false);
        this.submitLocked = false;
      }))
      .subscribe({
        next: (createdPlayer) => {
          console.log('create response', createdPlayer);

          if (!createdPlayer?.id) {
            this.errorMessage.set('Player create response was not valid. The list was not refreshed.');
            return;
          }

          this.photoCache.savePendingPhoto(createdPlayer.id);
          this.router.navigate(['/players'], {
            state: {
              refreshPlayers: true,
              createdPlayerId: createdPlayer.id
            }
          });
        },
        error: (error) => {
          console.error('create error', error);
          this.errorMessage.set('Player could not be created. Please check the form values.');
        }
      });
  }

  onPhotoSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];

    if (!file) {
      this.photoPreview.set('');
      this.form.controls.photoUrl.setValue('');
      this.photoCache.clearPendingPhoto();
      return;
    }

    if (!file.type.startsWith('image/')) {
      this.errorMessage.set('Please select an image file for the player photo.');
      input.value = '';
      return;
    }

    const reader = new FileReader();

    reader.onload = () => {
      const dataUrl = String(reader.result || '');
      this.photoPreview.set(dataUrl);
      this.photoCache.setPendingPhoto(dataUrl);
      this.form.controls.photoUrl.setValue('');
      this.errorMessage.set('');
    };

    reader.onerror = () => {
      this.errorMessage.set('Player photo could not be read. Please try another image.');
      input.value = '';
    };

    reader.readAsDataURL(file);
  }

  hasError(controlName: keyof typeof this.form.controls): boolean {
    const control = this.form.controls[controlName];
    return control.invalid && (control.dirty || control.touched);
  }
}
