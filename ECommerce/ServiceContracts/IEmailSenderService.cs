namespace ECommerce.ServiceContracts
{
	public interface IEmailSenderService
	{
		 Task SendEmailAsync(string toEmail, string subject, string body);
	}

}
