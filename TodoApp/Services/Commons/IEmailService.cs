using System.Net.Mail;

namespace TodoApp.Services.Commons;
public interface IEmailService {

    // Fields

    public bool isHtml { get; set; }
    public MailAddress to { get; set; }
    public MailMessage email { get; set; }
    public MailAddress from { get; set; }

    // Methods

    public Task<bool> sendMailAsync(string mail, string title, string text, bool ishtml);
}