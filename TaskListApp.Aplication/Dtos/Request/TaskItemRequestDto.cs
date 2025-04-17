using TaskListApp.Domain.Enums;

namespace TaskListApp.Aplication.Dtos.Request
{
    public class TaskItemRequestDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Status Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
