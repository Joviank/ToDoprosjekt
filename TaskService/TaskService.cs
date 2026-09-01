namespace TaskService;

public class TaskService
{
    private readonly List<TaskItem> tasks = new();
    private int nextId = 0;

    public TaskItem AddTask(string title)
    {
        var task = new TaskItem(nextId++, title);
        tasks.Add (task);
        return task;
    }

    public List<TaskItem> GetTasks()
    {
        return tasks.ToList();
    }

    public void CompleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if(task == null)
        {
            throw new KeyNotFoundException("Du vil ha monarkiet. (Tasken finnes ikke)");
        }
        task.IsCompleted = true;
    }
}
