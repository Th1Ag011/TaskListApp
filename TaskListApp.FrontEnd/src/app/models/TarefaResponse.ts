import { Status } from "./Enums/Status";
import { TaskPriority } from "./Enums/TaskPriority";

export interface TarefaResponse {
    id: string;                
    name: string;
    description: string;
    status: Status;
    priority: TaskPriority;
    createdAt: string;         
    dueDate?: string | null;   
  }