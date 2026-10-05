using ECommerce.ServiceContracts;
using System.Net;
using System.Net.Mail;

namespace ECommerce.Services
{
	public class EmailSenderService : IEmailSenderService
	{
		private readonly IConfiguration _configuration;
		public EmailSenderService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string toEmail, string subject, string body)
		{
			var smtp = new SmtpClient(_configuration["EmailSettings:Host"])
			{
				Port = int.Parse(_configuration["EmailSettings:Port"]),
				Credentials = new NetworkCredential(
					_configuration["EmailSettings:FromEmail"],
					_configuration["EmailSettings:AppPassword"]),
				// SSL(Secure Sockets Layer) and TLS(Transport Layer Security) encrypt data sent over the network.
				// Setting it to true means the connection between your app and the SMTP server(e.g., Gmail, Outlook) is encrypted.
				// Setting it to false means data is sent in plain text, which is unsafe.
				EnableSsl = true,
			};

			var email = new MailMessage()
			{
				From = new MailAddress(_configuration["EmailSettings:FromEmail"]),
				Subject = subject,
				Body = body,
				IsBodyHtml = true, // Set to true if the body contains HTML content
			};

			email.To.Add(toEmail);

			await smtp.SendMailAsync(email);
		}
	}
}
