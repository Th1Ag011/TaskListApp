using Microsoft.EntityFrameworkCore;
using TaskListApp.Domain.Entities;

namespace TaskListApp.Infrastructure.Data
{
    public class TaskListDbContext : DbContext
    {
        public TaskListDbContext(DbContextOptions<TaskListDbContext> options) : base(options) 
        { 
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveColumnType("timestamp without time zone");
        }
    }
}
