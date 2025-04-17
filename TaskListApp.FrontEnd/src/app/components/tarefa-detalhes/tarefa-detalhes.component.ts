import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TarefaResponse } from '../../models/TarefaResponse';

@Component({
  selector: 'app-tarefa-detalhes',
  templateUrl: './tarefa-detalhes.component.html',
  styleUrls: ['./tarefa-detalhes.component.css'],
  imports: [CommonModule]
})
export class TarefaDetalhesComponent {
  @Input() tarefa?: TarefaResponse;

  getStatusLabel(status: number): string {
    switch (status) {
      case 0: return 'Pendente';
      case 1: return 'Em Progresso';
      case 2: return 'Concluída';
      case 3: return 'Cancelada';
      default: return status.toString();
    }
  }

  getPriorityLabel(priority: number): string {
    switch (priority) {
      case 0: return 'Baixa';
      case 1: return 'Média';
      case 2: return 'Alta';
      case 3: return 'Urgente';
      default: return priority.toString();
    }
  }
}
