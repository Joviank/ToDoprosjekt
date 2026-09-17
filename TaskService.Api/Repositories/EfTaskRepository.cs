using TaskService.Api.Data;
using TaskService.Repositories;

namespace TaskService.Api.Repositories;

public class EfTaskRepository : ITaskRepository
{
    private readonly TaskDbContext _db;

    public EfTaskRepository(TaskDbContext db)
    {
        _db = db;
    }

    public List<TaskItem> GetAll(string userId)
    {
        return _db.Tasks
            .Where(t => t.UserId == userId)
            .ToList();
    }

    public TaskItem Add(string title, string userId)
    {
        var task = new TaskItem(0, title, userId);
        _db.Tasks.Add(task);
        _db.SaveChanges();
        return task;
    }
    public void Delete(int id)
    {
        var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            throw new KeyNotFoundException();
        }

        _db.Tasks.Remove(task);
        _db.SaveChanges();
    }
    public void Complete(int id)
    {
        var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            throw new KeyNotFoundException();
        }

        task.IsCompleted = !task.IsCompleted;
        _db.SaveChanges();
    }
}