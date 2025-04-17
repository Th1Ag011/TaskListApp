import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TarefaDetalhesComponent } from './tarefa-detalhes.component';

describe('TarefaDetalhesComponent', () => {
  let component: TarefaDetalhesComponent;
  let fixture: ComponentFixture<TarefaDetalhesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TarefaDetalhesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TarefaDetalhesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
