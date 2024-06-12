using TodoApp.Models.Entities.Commons;

namespace TodoApp.Models.Entities.Concretes;

public class TodoItem : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsSent { get; set; } = false;
    public bool IsCompleted { get; set; } = false;
    public DateTime? ExpireDateTime { get; set; }

    // Foreign Key
    public string? UserId { get; set; }
    // Navigation Properies
    public virtual User User { get; set; }
}
