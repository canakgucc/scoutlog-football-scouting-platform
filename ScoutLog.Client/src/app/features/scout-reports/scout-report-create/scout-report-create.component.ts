import { Component, OnInit, inject, signal } from '@angular/core';
import { ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { Player } from '../../players/player.models';
import { PlayerService } from '../../players/player.service';
import { ReportType, ScoutReport } from '../scout-report.models';
import { ScoutReportService } from '../scout-report.service';

type VoiceNoteMode = 'replace' | 'append';

interface VoiceNoteScenario {
  id: string;
  label: string;
  description: string;
  note: string;
}

@Component({
  selector: 'app-scout-report-create',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './scout-report-create.component.html',
  styleUrl: './scout-report-create.component.css'
})
export class ScoutReportCreateComponent implements OnInit {
  private readonly formBuilder = inject(FormBuilder);
  private readonly playerService = inject(PlayerService);
  private readonly scoutReportService = inject(ScoutReportService);
  private readonly route = inject(ActivatedRoute);

  readonly players = signal<Player[]>([]);
  readonly isLoadingPlayers = signal(true);
  readonly isSubmitting = signal(false);
  readonly errorMessage = signal('');
  readonly successMessage = signal('');
  readonly createdReport = signal<ScoutReport | null>(null);
  readonly reportTypes: ReportType[] = ['Match', 'Training'];
  readonly selectedVoiceNoteScenario = signal('fast-winger');
  readonly voiceNoteMode = signal<VoiceNoteMode>('replace');
  readonly voiceNoteMessage = signal('');
  readonly voiceNoteScenarios: VoiceNoteScenario[] = [
    {
      id: 'fast-winger',
      label: 'Fast winger',
      description: 'Pace, sprint quality, attacking output',
      note: 'Oyuncu sağ kanatta çok hızlı ve çabuk göründü. Sprint tekrarlarında rakibini geçti, pas kalitesi fena değildi ve bir asist öncesi doğru koşu yaptı. Ancak savunma dönüşlerinde geç kaldı, karar verme anlarında bazen riskli tercih yaptı.'
    },
    {
      id: 'technical-midfielder',
      label: 'Technical midfielder',
      description: 'Passing, vision, tempo control',
      note: 'Oyuncu merkezde pas kalitesi ve oyun görüşüyle öne çıktı. Dar alanda sakin kaldı, iki kez asist tehdidi yarattı ve takımın temposunu iyi yönetti. Fiziksel temaslarda biraz daha güçlü kalmalı.'
    },
    {
      id: 'defensive-issue',
      label: 'Defensive issue',
      description: 'Marking, recovery, awareness',
      note: 'Oyuncu savunma dönüşlerinde birkaç kez geç kaldı. Markaj paylaşımında pozisyon hatası yaptı ve rakibin koşusunu takip etmekte zorlandı. Topla çıkışta sade oynadı ama savunma farkındalığı gelişmeli.'
    },
    {
      id: 'physical-fatigue',
      label: 'Physical fatigue',
      description: 'Conditioning drop and late-game intensity',
      note: 'Oyuncu ilk bölümde tempolu ve istekliydi fakat ikinci yarıda kondisyon düşüşü yaşadı. Sprint mesafesi azaldı, baskı sonrası toparlanması gecikti ve bazı pozisyonlarda yorulduğu için top kaybı yaptı.'
    },
    {
      id: 'high-potential-striker',
      label: 'High potential striker',
      description: 'Finishing, box movement, upside',
      note: 'Oyuncu santrfor pozisyonunda gol tehdidi ve bitiricilik kalitesiyle dikkat çekti. Ceza sahası koşuları doğruydu, şut tercihleri etkiliydi ve fiziksel olarak güçlü kaldı. Karar verme biraz daha hızlanırsa yüksek potansiyel gösterebilir.'
    }
  ];

  readonly form = this.formBuilder.nonNullable.group({
    playerId: [0, [Validators.required, Validators.min(1)]],
    reportType: ['Match' as ReportType, [Validators.required]],
    eventDate: [this.todayInputValue(), [Validators.required]],
    opponent: [''],
    competition: [''],
    minutesPlayed: [90 as number | null, [Validators.min(0), Validators.max(120)]],
    observedPosition: [''],
    title: ['Demo scout report', [Validators.required, Validators.maxLength(160)]],
    observationText: [
      'Oyuncu bugün çok hızlı ve çabuktu. Sağ kanatta etkili sprintler attı, pas kalitesi iyiydi ve bir asist yaptı. Ancak savunma dönüşlerinde geç kaldı ve bazı pozisyonlarda karar verme konusunda riskli tercihler yaptı.',
      [Validators.required, Validators.minLength(20)]
    ],
    technicalScore: [78, [Validators.required, Validators.min(0), Validators.max(100)]],
    physicalScore: [82, [Validators.required, Validators.min(0), Validators.max(100)]],
    tacticalScore: [70, [Validators.required, Validators.min(0), Validators.max(100)]],
    mentalScore: [72, [Validators.required, Validators.min(0), Validators.max(100)]],
    potentialScore: [84, [Validators.required, Validators.min(0), Validators.max(100)]],
    recommendation: ['Izlenmeli', [Validators.required, Validators.maxLength(100)]]
  });

  ngOnInit(): void {
    this.form.controls.playerId.valueChanges.subscribe((playerId) =>
      this.fillObservedPositionFromPlayer(playerId)
    );
    this.loadPlayers();
  }

  loadPlayers(): void {
    this.isLoadingPlayers.set(true);
    this.errorMessage.set('');

    this.playerService
      .getPlayers()
      .pipe(finalize(() => this.isLoadingPlayers.set(false)))
      .subscribe({
        next: (players) => {
          this.players.set(players);
          const routePlayerId = Number(this.route.snapshot.paramMap.get('playerId'));
          const selectedPlayerId =
            routePlayerId > 0 && players.some((player) => player.id === routePlayerId)
              ? routePlayerId
              : players[0]?.id;

          if (selectedPlayerId && this.form.controls.playerId.value === 0) {
            this.form.controls.playerId.setValue(selectedPlayerId);
            this.fillObservedPositionFromPlayer(selectedPlayerId);
          }
        },
        error: () => this.errorMessage.set('Players could not be loaded.')
      });
  }

  submit(): void {
    if (this.form.invalid || this.isSubmitting()) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    this.errorMessage.set('');
    this.successMessage.set('');
    this.createdReport.set(null);

    this.scoutReportService
      .createReport(this.buildRequest())
      .pipe(finalize(() => this.isSubmitting.set(false)))
      .subscribe({
        next: (report) => {
          this.createdReport.set(report);
          this.successMessage.set('Scout report created and analyzed successfully.');
        },
        error: () => this.errorMessage.set('Scout report could not be created.')
      });
  }

  splitValues(value: string | null): string[] {
    return (value || '')
      .split(';')
      .map((item) => item.trim())
      .filter(Boolean);
  }

  scorePercent(score: number | null): number {
    return Math.max(0, Math.min(100, (score ?? 0) * 10));
  }

  isMatchReport(): boolean {
    return this.form.controls.reportType.value === 'Match';
  }

  setVoiceNoteScenario(id: string): void {
    this.selectedVoiceNoteScenario.set(id);
    this.voiceNoteMessage.set('');
  }

  setVoiceNoteMode(mode: VoiceNoteMode): void {
    this.voiceNoteMode.set(mode);
    this.voiceNoteMessage.set('');
  }

  selectedVoiceNoteDescription(): string {
    return this.selectedVoiceNote()?.description ?? '';
  }

  hasObservationText(): boolean {
    return this.form.controls.observationText.value.trim().length > 0;
  }

  simulateVoiceNote(): void {
    const scenario = this.selectedVoiceNote();

    if (!scenario) {
      return;
    }

    const observationControl = this.form.controls.observationText;
    const currentText = observationControl.value.trim();
    const nextText =
      this.voiceNoteMode() === 'append' && currentText
        ? `${currentText}\n\n${scenario.note}`
        : scenario.note;

    observationControl.setValue(nextText);
    observationControl.markAsDirty();
    observationControl.updateValueAndValidity();
    this.voiceNoteMessage.set('Voice note inserted. You can edit it before submitting.');
  }

  private buildRequest() {
    const value = this.form.getRawValue();

    return {
      ...value,
      opponent: this.optionalText(value.opponent),
      competition: this.optionalText(value.competition),
      observedPosition: this.optionalText(value.observedPosition),
      minutesPlayed: value.minutesPlayed === null ? null : Number(value.minutesPlayed)
    };
  }

  private fillObservedPositionFromPlayer(playerId: number): void {
    const player = this.players().find((item) => item.id === Number(playerId));

    if (!player || this.form.controls.observedPosition.value.trim()) {
      return;
    }

    this.form.controls.observedPosition.setValue(player.position);
  }

  private todayInputValue(): string {
    return new Date().toISOString().slice(0, 10);
  }

  private optionalText(value: string): string | null {
    return value.trim() ? value.trim() : null;
  }

  private selectedVoiceNote(): VoiceNoteScenario | undefined {
    return this.voiceNoteScenarios.find((scenario) => scenario.id === this.selectedVoiceNoteScenario());
  }
}
