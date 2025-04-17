using Microsoft.EntityFrameworkCore;
using TaskListApp.Domain.Entities;
using TaskListApp.Domain.Interfaces;
using TaskListApp.Infrastructure.Data;

namespace TaskListApp.Infrastructure.Repository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly TaskListDbContext _context;

        public TaskItemRepository(TaskListDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task AddAsync(TaskItem taskItem)
        {
            await _context.Tasks.AddAsync(taskItem);
        }

        public void Update (TaskItem taskItem)
        {
            _context.Tasks.Update(taskItem);
        }

        public void Delete (TaskItem taskItem)
        {
           _context.Tasks.Remove(taskItem);   
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
