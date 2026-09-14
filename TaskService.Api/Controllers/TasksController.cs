using Microsoft.AspNetCore.Mvc;
using TaskService;
using TaskService.Api.DTO;

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
    public TaskItem AddTask([FromBody] CreateTaskRequest request)
    {
        return taskManager.AddTask(request.Title);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        taskManager.DeleteTask(id);
        return NoContent();
    }
    [HttpPatch("{id}/complete")]
    public IActionResult CompleteTask(int id)
    {
        taskManager.CompleteTask(id);
        return NoContent();
    }
}