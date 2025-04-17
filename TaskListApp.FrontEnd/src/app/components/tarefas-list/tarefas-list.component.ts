import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TarefaService } from '../../services/tarefa.service';
import { TarefaResponse } from '../../models/TarefaResponse';
import { TaskPriority } from '../../models/Enums/TaskPriority';
import { Status } from '../../models/Enums/Status';
import { TarefaFormComponent } from '../tarefa-form/tarefa-form.component';
import { TarefaDetalhesComponent } from '../tarefa-detalhes/tarefa-detalhes.component';

declare var bootstrap: any;

@Component({
  standalone: true,
  selector: 'app-tarefas-list',
  templateUrl: './tarefas-list.component.html',
  styleUrls: ['./tarefas-list.component.css'],
  imports: [
    TarefaFormComponent,
    TarefaDetalhesComponent,
    CommonModule,
    FormsModule,
  ]
})
export class TarefasListComponent implements OnInit {
  tarefas: TarefaResponse[] = [];
  tarefaSelecionada!: TarefaResponse;

  tarefaIdParaExcluir: string | null = null;

  statusFiltro: string = '';
  listaStatus = [
    { label: 'Pendente', value: Status.Pending },
    { label: 'Em Progresso', value: Status.InProgress },
    { label: 'Concluída', value: Status.Completed },
    { label: 'Cancelada', value: Status.Cancelled }
  ];

  successMessage: string = "";
  errorMessage: string = "";

  constructor(private tarefaService: TarefaService) { }

  ngOnInit(): void {
    this.carregarTarefas();
  }

  carregarTarefas(): void {
    this.tarefaService.getAll().subscribe({
      next: (data) => {
        this.tarefas = data;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  filtrarPorStatus(): void {
    if (!this.statusFiltro) {
      this.carregarTarefas();
      return;
    }
    const statusNumber = Number(this.statusFiltro);
    this.tarefaService.getByStatus(statusNumber).subscribe({
      next: (data) => {
        this.tarefas = data;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  public abrirModalCriar(): void {
    const modal = new bootstrap.Modal(document.getElementById('modalCriar'));
    modal.show();
  }

  public fecharModalCriar(): void {
    const modalEl = document.getElementById('modalCriar');
    const modal = bootstrap.Modal.getInstance(modalEl);
    modal.hide();
  }

  public abrirModalEditar(tarefa: TarefaResponse): void {
    this.tarefaSelecionada = tarefa;
    const modal = new bootstrap.Modal(document.getElementById('modalEditar'));
    modal.show();
  }

  public fecharModalEditar(): void {
    const modalEl = document.getElementById('modalEditar');
    const modal = bootstrap.Modal.getInstance(modalEl);
    modal.hide();
  }

  public abrirModalDetalhes(tarefa: TarefaResponse): void {
    this.tarefaSelecionada = tarefa;
    const modal = new bootstrap.Modal(document.getElementById('modalDetalhes'));
    modal.show();
  }


  public confirmarExclusao(id: string): void {
    this.tarefaIdParaExcluir = id;
    const modal = new bootstrap.Modal(document.getElementById('modalConfirmarExclusao'));
    modal.show();
  }

  public excluirTarefa(): void {
    if (!this.tarefaIdParaExcluir) {
      return;
    }
    this.tarefaService.delete(this.tarefaIdParaExcluir).subscribe({
      next: () => {
        const modalEl = document.getElementById('modalConfirmarExclusao');
        const modal = bootstrap.Modal.getInstance(modalEl);
        modal.hide();
        this.carregarTarefas();
        this.tarefaIdParaExcluir = null;
      },
      error: (err) => {
        console.error(err);
        alert('Ocorreu um erro ao excluir a tarefa.');
      }
    });
  }

  public atualizarLista(): void {
    this.carregarTarefas();
  }

  public converterStatus(status: Status): string {
    switch (status) {
      case Status.Pending: return 'Pendente';
      case Status.InProgress: return 'Em Progresso';
      case Status.Completed: return 'Concluída';
      case Status.Cancelled: return 'Cancelada';
      default: return '';
    }
  }

  public converterPriority(priority: TaskPriority): string {
    switch (priority) {
      case TaskPriority.Low: return 'Baixa';
      case TaskPriority.Medium: return 'Média';
      case TaskPriority.High: return 'Alta';
      case TaskPriority.Urgente: return 'Urgente';
      default: return '';
    }
  }

  public onSuccessMessage(message: string): void {
    this.successMessage = message;
    this.errorMessage = '';
    setTimeout(() => this.successMessage = '', 6000);
    this.atualizarLista();
  }

  public onErrorMessage(message: string): void {
    this.errorMessage = message;
    this.successMessage = ''; 
    setTimeout(() => this.errorMessage = '', 6000);
  }
}
