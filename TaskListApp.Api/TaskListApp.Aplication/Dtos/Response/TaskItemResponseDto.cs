using TaskListApp.Domain.Enums;

namespace TaskListApp.Aplication.Dtos.Response
{
    public class TaskItemResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Status Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
