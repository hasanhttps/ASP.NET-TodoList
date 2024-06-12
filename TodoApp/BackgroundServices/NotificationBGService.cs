using Microsoft.EntityFrameworkCore;
using TodoApp.Contexts;
using TodoApp.Services.Commons;
using TodoApp.Services.Concretes;

namespace TodoApp.BackgroundServices;

public class NotificationBGService : BackgroundService {

    // Fields

    private readonly IEmailService _emailService;
    private readonly AppDbContext _context;

    // Constructor

    public NotificationBGService(IDbContextFactory<AppDbContext> context) { 
        _context = context.CreateDbContext();
        _emailService = new EmailService();
    }

    // Methods

    private Timer _timer;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _timer = new Timer(SendNotification, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        Console.WriteLine("Checking...");
    }

    public async void SendNotification(object?obj) {
        var items = await _context.TodoItems.ToListAsync();
        foreach (var item in items) {
            if (item.ExpireDateTime - DateTime.Now <= TimeSpan.FromDays(1) && item.IsSent == false && item.IsCompleted == false) {
                item.IsSent = true;
                await _emailService.sendMailAsync(item.User.Email, "TodoList Notification", $"You have {item.Title} - {item.Description} task to do in less than one day", false);
            }
        }
    }
}
