
namespace TodoApp.Models.Dtos;

public class AddTodoItemDTO {
    public string? Title { get; set; }
    public string UserId { get; set; }
    public string? Description { get; set; }
    public DateTime? ExpireDateTime { get; set; }
    public bool IsCompleted { get; set; } = false;
}
