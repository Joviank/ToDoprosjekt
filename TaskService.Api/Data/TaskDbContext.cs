using Microsoft.EntityFrameworkCore;

namespace TaskService.Api.Data;

public class TaskDbContext : DbContext
{
    public DbSet <TaskItem> Tasks { get; set; }
    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base (options)
    {
        
    }
}