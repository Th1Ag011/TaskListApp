using TaskListApp.Domain.Enums;

namespace TaskListApp.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public Status Status { get; private set; }
        public TaskPriority Priority { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DueDate { get; private set; }

        public TaskItem() { }

        public TaskItem(string name, string description, Status status, TaskPriority priority, DateTime? dueDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Status = status;
            Priority = priority;
            CreatedAt = DateTime.Today;
            DueDate = dueDate;
        }

        public void Update(string name, string description, Status status, TaskPriority priority, DateTime? dueDate)
        {
            Name = name;
            Description = description;
            Status = status;
            Priority = priority;
            DueDate = dueDate;
        }

    }
}
