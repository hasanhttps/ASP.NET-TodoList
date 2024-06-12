using Microsoft.AspNetCore.Identity;
using TodoApp.Models.Entities.Commons;

namespace TodoApp.Models.Entities.Concretes;

public class User : IdentityUser, IBaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }

    // Navigation Properties
    public virtual ICollection<TodoItem> TodoItems { get; set; }
}
