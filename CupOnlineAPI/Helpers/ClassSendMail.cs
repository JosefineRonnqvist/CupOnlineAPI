using System.Net.Mail;

namespace CupOnlineAPI.Helpers
{
    public class ClassSendMail : IClassSendMail
    {
        private readonly IConfiguration config;

        public ClassSendMail(IConfiguration config)
        {
            this.config = config;
        }
        public async void SendMail(string mailTo, string subject, string body)
        {
            if (!string.IsNullOrEmpty(config.GetValue<string>("Email:SmtpServer")) && !string.IsNullOrEmpty(mailTo) && !string.IsNullOrEmpty(config.GetValue<string>("Email:MailFrom")))
            {
                SmtpClient client = new SmtpClient(config.GetValue<string>("Email:SmtpServer"));
                MailAddress from = new MailAddress(config.GetValue<string>("Email:MailFrom"));
                MailAddress to = new MailAddress(mailTo);
                MailMessage message = new MailMessage(from, to);
                message.Body = body;
                message.Subject = subject;
                message.IsBodyHtml = true;
                client.SendAsync(message, "Message");
            }
        }
    }
}
