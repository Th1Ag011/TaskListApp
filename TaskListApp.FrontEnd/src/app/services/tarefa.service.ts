import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TarefaResponse} from '../models/TarefaResponse';
import { TarefaRequest} from '../models/TarefaRequest';
import { Status} from '../models/Enums/Status';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class TarefaService {
  private apiUrl = 'http://localhost:7054/api/taskList';

  constructor(private http: HttpClient) {}

  getAll(): Observable<TarefaResponse[]> {
    return this.http.get<TarefaResponse[]>(this.apiUrl);
  }

  getById(id: string): Observable<TarefaResponse> {
    return this.http.get<TarefaResponse>(`${this.apiUrl}/${id}`);
  }

  getByStatus(status: Status): Observable<TarefaResponse[]> {
    return this.http.get<TarefaResponse[]>(`${this.apiUrl}/status/${status}`);
  }

  create(tarefa: TarefaRequest): Observable<TarefaResponse> {
    return this.http.post<TarefaResponse>(this.apiUrl, tarefa);
  }

  update(id: string, tarefa: TarefaRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, tarefa);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
