using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}