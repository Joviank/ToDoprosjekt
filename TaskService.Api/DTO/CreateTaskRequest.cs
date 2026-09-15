namespace TaskService.Api.DTO;

public class CreateTaskRequest
{
    public required string Title { get; set;}

}
// Frontend (HTTP request) -> API / Controller (mapper JSON -> C# Objektet) -> CreateTaskRequest -> TaskService -> Repository