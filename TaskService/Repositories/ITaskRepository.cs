namespace TaskService.Repositories;

public interface ITaskRepository
{
    List<TaskItem> GetAll(string userId);
    TaskItem Add(string title, string userId);
    void Delete(int id);
    void Complete(int id);
}