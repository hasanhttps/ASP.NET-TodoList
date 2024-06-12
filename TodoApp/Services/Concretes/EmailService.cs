using System.Net;
using System.Net.Mail;
using TodoApp.Services.Commons;

namespace TodoApp.Services.Concretes;

public class EmailService : IEmailService {

    // Fields

    public MailAddress to { get; set; }
    public MailMessage email { get; set; }
    public bool isHtml { get; set; } = true;
    public MailAddress from { get; set; } = new MailAddress("reeljett@gmail.com");

    // Methods

    public async Task<bool> sendMailAsync(string mail, string title, string text, bool ishtml)
    {
        try
        {
            to = new MailAddress(mail);
            email = new MailMessage(from, to);
        }
        catch (Exception ex)
        {
            return false;
        }

        isHtml = ishtml;
        email.IsBodyHtml = isHtml;
        email.Subject = title;
        email.Body = text;

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 25;
        smtp.Credentials = new NetworkCredential("reeljett@gmail.com", "kyzrgjrzqyhxknua");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;

        try
        {
            await smtp.SendMailAsync(email);
        }
        catch (SmtpException ex)
        {
            return false;
        }
        return true;
    }
}