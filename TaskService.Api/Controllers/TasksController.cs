using Microsoft.AspNetCore.Mvc;
using TaskService;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService taskManager;

    public TaskController(TaskService taskService)
    {
        taskManager = taskService;
    }

    [HttpGet]
    public IEnumerable<TaskItem> GetTask()
    {
        return taskManager.GetTasks();
    }

    [HttpPost]
    public TaskItem AddTask(string title)
    {
        return taskManager.AddTask(title);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        taskManager.DeleteTask(id);
        return NoContent();
    }
}