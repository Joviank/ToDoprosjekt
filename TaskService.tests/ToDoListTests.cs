namespace TaskService.tests;

public class TaskServiceTests
{
    [Fact]
    public void AddTask_ShouldAddTaskToList()
    {
        // Arrange
        var service = new TaskService();

        // Act
        service.AddTask("Avskaffe monarkiet");

        // Assert
        Assert.Single(service.GetTasks());
    }

    [Fact]
    public void AddTask_ShouldStoreCorrectTitle()
    {
        // Given
        var service = new TaskService();
        // When
        service.AddTask("Avskaffe monarkiet part 2");
    
        // Then
        var task = service.GetTasks().First();
        Assert.Equal("Avskaffe monarkiet part 2", task.Title);
    }

    [Fact]
    public void CompleteTask_ShouldMarkAsCompleted()
    {
        // Arrange
        var service = new TaskService();
        var task = service.AddTask("Avskaffe monarkiet part 3");

        // Act 
        service.CompleteTask(task.Id);

        // Assert
        Assert.True(service.GetTasks().First().IsCompleted);
    }
    [Fact]
    public void CompleteTask_ShouldThrow_WhenTaskDoesntExist()
    {
        // Arrange
        var service = new TaskService();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => service.CompleteTask(1846374));
    }

    [Fact]
    public async Task HTTPHealth_ReturnsOk()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5288/health");
        Assert.True(response.IsSuccessStatusCode);
    }
}
