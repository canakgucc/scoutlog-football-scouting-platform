import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { LoginComponent } from './features/auth/login/login.component';
import { PlayerComparisonComponent } from './features/comparison/player-comparison.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { PlayerCreateComponent } from './features/players/player-create/player-create.component';
import { PlayerDetailComponent } from './features/players/player-detail/player-detail.component';
import { PlayerListComponent } from './features/players/player-list/player-list.component';
import { ScoutReportCreateComponent } from './features/scout-reports/scout-report-create/scout-report-create.component';
import { ScoutReportListComponent } from './features/scout-reports/scout-report-list/scout-report-list.component';
import { ShellComponent } from './shared/components/shell/shell.component';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '',
    component: ShellComponent,
    canActivate: [authGuard],
    canActivateChild: [authGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent
      },
      {
        path: 'players',
        component: PlayerListComponent
      },
      {
        path: 'players/create',
        component: PlayerCreateComponent
      },
      {
        path: 'players/:id',
        component: PlayerDetailComponent
      },
      {
        path: 'scout-reports',
        component: ScoutReportListComponent
      },
      {
        path: 'scout-reports/create',
        component: ScoutReportCreateComponent
      },
      {
        path: 'scout-reports/create/:playerId',
        component: ScoutReportCreateComponent
      },
      {
        path: 'comparison',
        component: PlayerComparisonComponent
      },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard'
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];
