public class TaskItem
{
    public int Id {get; set; }
    public string Title {get; set; }
    public bool IsCompleted {get; set; }
    public string UserId {get; set; } = string.Empty;

    public TaskItem()
    {    
    }

    public TaskItem(int id, string title, string userId)
    {
        Id = id;
        Title = title;
        IsCompleted = false;
        UserId = userId;
    }
}