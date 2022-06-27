namespace CupOnlineAPI.Helpers
{
    public interface IClassSendMail
    {
        void SendMail(string mailTo, string subject, string body);
    }
}