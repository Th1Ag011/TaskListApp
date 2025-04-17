import { Status } from "./Enums/Status";
import { TaskPriority } from "./Enums/TaskPriority";

export interface TarefaRequest {
    name: string;
    description: string;
    status: Status;
    priority: TaskPriority;
    dueDate?: string | null;
  }