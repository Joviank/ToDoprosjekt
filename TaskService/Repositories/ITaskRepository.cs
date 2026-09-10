namespace TaskService.Repositories;

public interface ITaskRepository
{
    List<TaskItem> GetAll();
    TaskItem Add(string title);
    void Delete(int id);
    void Complete(int id);
}