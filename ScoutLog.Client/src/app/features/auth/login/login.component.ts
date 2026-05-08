import { Component, HostListener, OnInit, inject } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from '../../../core/services/auth.service';

type DemoClubId = 'fenerbahce' | 'galatasaray' | 'besiktas' | 'trabzonspor';
type AuthTab = 'signIn' | 'register';

interface DemoClub {
  id: DemoClubId;
  name: string;
  email: string;
  password: string;
}

const passwordsMatchValidator = (control: AbstractControl): ValidationErrors | null => {
  const password = control.get('password')?.value;
  const confirmPassword = control.get('confirmPassword')?.value;

  return password && confirmPassword && password !== confirmPassword
    ? { passwordsMismatch: true }
    : null;
};

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly formBuilder = inject(FormBuilder);
  private readonly router = inject(Router);

  readonly signInForm = this.formBuilder.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  });

  readonly registerForm = this.formBuilder.nonNullable.group(
    {
      clubName: ['', [Validators.required]],
      contactPerson: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required]]
    },
    { validators: passwordsMatchValidator }
  );

  activeAuthTab: AuthTab = 'signIn';
  isLoading = false;
  errorMessage = '';
  registrationMessage = '';
  selectedDemoClub: DemoClubId | null = null;

  readonly demoClubs: DemoClub[] = [
    {
      id: 'fenerbahce',
      name: 'Fenerbahçe Demo',
      email: 'scout@fenerbahce.local',
      password: 'Demo123!'
    },
    {
      id: 'galatasaray',
      name: 'Galatasaray Demo',
      email: 'scout@galatasaray.local',
      password: 'Demo123!'
    },
    {
      id: 'besiktas',
      name: 'Beşiktaş Demo',
      email: 'scout@besiktas.local',
      password: 'Demo123!'
    },
    {
      id: 'trabzonspor',
      name: 'Trabzonspor Demo',
      email: 'scout@trabzonspor.local',
      password: 'Demo123!'
    }
  ];

  constructor() {
    this.signInForm.reset({
      email: '',
      password: ''
    });

    this.signInForm.valueChanges.subscribe(() => {
      if (!this.selectedDemoClub) {
        return;
      }

      const selectedClub = this.demoClubs.find((club) => club.id === this.selectedDemoClub);

      if (
        !selectedClub ||
        this.signInForm.controls.email.value !== selectedClub.email ||
        this.signInForm.controls.password.value !== selectedClub.password
      ) {
        this.selectedDemoClub = null;
      }
    });
  }

  ngOnInit(): void {
    this.redirectAuthenticatedUser();
  }

  @HostListener('window:pageshow')
  onPageShow(): void {
    this.redirectAuthenticatedUser();
  }

  private redirectAuthenticatedUser(): void {
    if (this.authService.isAuthenticated()) {
      this.router.navigateByUrl('/dashboard', { replaceUrl: true });
    }
  }

  selectAuthTab(tab: AuthTab): void {
    this.activeAuthTab = tab;
    this.errorMessage = '';
    this.registrationMessage = '';
  }

  showDemoAccess(): void {
    this.selectAuthTab('signIn');
  }

  goToAuthSection(tab: AuthTab): void {
    this.selectAuthTab(tab);
    setTimeout(() => {
      document.getElementById('auth-section')?.scrollIntoView({
        behavior: 'smooth',
        block: 'start'
      });
    });
  }

  login(): void {
    if (this.signInForm.invalid || this.isLoading) {
      this.signInForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService
      .login(this.signInForm.getRawValue())
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe({
        next: () => this.router.navigateByUrl('/dashboard', { replaceUrl: true }),
        error: () => {
          this.errorMessage = 'Login failed. Check the API and credentials.';
        }
      });
  }

  fillDemoClub(club: DemoClub): void {
    this.selectedDemoClub = club.id;
    this.errorMessage = '';
    this.signInForm.setValue({
      email: club.email,
      password: club.password
    });
  }

  registerClub(): void {
    this.registrationMessage = '';

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    this.registrationMessage =
      'Club registration will be connected in the next phase. For demo access, please use one of the club demo accounts.';
  }
}
