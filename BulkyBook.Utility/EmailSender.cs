using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("hello@dotnetmastery.com"));
                emailToSend.To.Add(MailboxAddress.Parse(email));
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

                // Send email
                using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    emailClient.Authenticate("bach0981957216@gmail.com", "jsmszvvpsgxwckpg");
                    emailClient.Send(emailToSend);
                    emailClient.Disconnect(true);
                }

                return Task.CompletedTask;
            }
			catch (SmtpException ex)
			{
				// Xử lý lỗi
				Console.WriteLine("Lỗi gửi email: " + ex.Message);
				return Task.CompletedTask;
			}
		}
    }
}
