using TaskService.Repositories;
namespace TaskService;

public class TaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo)
    {
        _repo = repo;
    }

    public TaskItem AddTask(string title, string userId)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty");
        }

        if (title.Length > 100)
        {
            throw new ArgumentException("Title cannot be longer than 100 characetrs.");
        }
        
        return _repo.Add(title, userId);
    }

    public List<TaskItem> GetTasks(string userId)
    {
        return _repo.GetAll(userId);
    }

    public void CompleteTask(int id)
    {
        _repo.Complete(id);
    }

    public void DeleteTask(int id)
    {
        _repo.Delete(id);
    }
}
