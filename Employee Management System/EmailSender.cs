


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Employee_Management_System
{
    public class EmailSender: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Email to:  {email}, subject:  {subject}");
            return Task.CompletedTask;
        }
    }
}
