using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models.Entities.Concretes;

namespace TodoApp.Contexts;

public class AppDbContext:IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TodoItem>()
            .HasOne(p=>p.User)
            .WithMany()
            .HasForeignKey(p=>p.UserId).OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(builder);
    }


    // Tables
    public DbSet<TodoItem> TodoItems { get; set; }
}
