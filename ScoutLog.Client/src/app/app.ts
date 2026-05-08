import { Component, HostListener, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  @HostListener('window:pageshow')
  onPageShow(): void {
    const currentPath = this.router.url.split('?')[0].split('#')[0];

    if (this.authService.isAuthenticated() && (currentPath === '/login' || currentPath === '/')) {
      this.router.navigateByUrl('/dashboard', { replaceUrl: true });
    }
  }
}
