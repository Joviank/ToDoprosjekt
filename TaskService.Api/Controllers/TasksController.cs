using Microsoft.AspNetCore.Mvc;
using TaskService;
using TaskService.Api.DTO;
using TaskService.Api.Helpers;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService taskManager;
    private readonly UserContext userContext;

    public TaskController(TaskService taskService, UserContext userContext)
    {
        taskManager = taskService;
        this.userContext = userContext;
    }

    [HttpGet]
    public ActionResult <IEnumerable<TaskItem>> GetTask()
    {
        var userId = userContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID could not be found");
        }

        var tasks = taskManager.GetTasks(userId);
        return Ok(tasks);
    }

    [HttpPost]
    public ActionResult<TaskItem> AddTask([FromBody] CreateTaskRequest request)
    {
        var userId = userContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID could not be found");
        }
        var newTask = taskManager.AddTask(request.Title, userId);
        return Ok(newTask);
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