import { Component, EventEmitter, Input, OnInit, OnChanges, SimpleChanges, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { TarefaRequest } from '../../models/TarefaRequest';
import { TarefaResponse } from '../../models/TarefaResponse';
import { TarefaService } from '../../services/tarefa.service';

@Component({
  standalone: true,
  selector: 'app-tarefa-form',
  templateUrl: './tarefa-form.component.html',
  styleUrls: ['./tarefa-form.component.css'],
  imports: [CommonModule, FormsModule ]
})
export class TarefaFormComponent implements OnInit, OnChanges {
  @Input() tarefaEdicao?: TarefaResponse;  
  @Output() fechar = new EventEmitter<void>();
  @Output() successMessage = new EventEmitter<string>();
  @Output() errorMessage = new EventEmitter<string>();

  model: TarefaRequest = {
    name: '',
    description: '',
    status: 0,
    priority: 0,
    dueDate: ''
  };

  constructor(private tarefaService: TarefaService) {}

  ngOnInit(): void {
    this.preencherFormulario();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tarefaEdicao'] && changes['tarefaEdicao'].currentValue) {
      this.preencherFormulario();
    }
  }

  private preencherFormulario(): void {
    if (this['tarefaEdicao']) {
      this.model = {
        name: this['tarefaEdicao'].name,
        description: this['tarefaEdicao'].description,
        status: this['tarefaEdicao'].status,
        priority: this['tarefaEdicao'].priority,
        dueDate: this['tarefaEdicao'].dueDate ? this['tarefaEdicao'].dueDate.split('T')[0] : ''
      };
    } else {
      this.model = {
        name: '',
        description: '',
        status: 0,
        priority: 0,
        dueDate: ''
      };
    }
  }

  salvar(form: NgForm) {
    if (form.invalid) return;
    
    if (this['tarefaEdicao']) {
      this.tarefaService.update(this['tarefaEdicao'].id, this.model).subscribe({
        next: () => {
          this.successMessage.emit('Tarefa atualizada com sucesso!');
          this.fecharModal();
        },
        error: err => {
          console.error(err);
          this.errorMessage.emit('Erro ao atualizar tarefa.');
        }
      });
    } else {
      this.tarefaService.create(this.model).subscribe({
        next: () => {
          this.successMessage.emit('Tarefa criada com sucesso!');
          form.resetForm();
          this.fecharModal();
        },
        error: err => {
          console.error(err);
          form.resetForm();
          this.errorMessage.emit('Erro ao criar tarefa.');
        }
      });
    }
  }

  fecharModal() {
    this.fechar.emit();
  }
}
