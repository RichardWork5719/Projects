using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MessingWithCode
{
    class EmailSender // DOESNT WORK
    {
        public EmailSender(string toEmail, string subject, string body) // hostname = smtp.gmail.com | port = 587
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587) // Use the appropriate SMTP server and port
            {
                Credentials = new NetworkCredential("sharptile@protonmail.com", "-"), // Your email and password

                EnableSsl = true // Enable SSL for secure connection
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("sharptile@protonmail.com"), // Your email address

                Subject = subject, //"Testing",

                Body = body, // "<h1>Hello, World!</h1><p>This is a test email.</p>",

                IsBodyHtml = true // Set to true if the body contains HTML
            };

            mailMessage.To.Add(toEmail);

            try
            {
                smtpClient.Send(mailMessage);

                Console.WriteLine("Email sent successfully.");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}
