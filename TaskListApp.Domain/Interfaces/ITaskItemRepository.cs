using TaskListApp.Domain.Entities;

namespace TaskListApp.Domain.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task AddAsync(TaskItem taskitem);
        void Update(TaskItem taskitem);
        void Delete(TaskItem taskitem);
        Task SaveAsync();

    }
}
