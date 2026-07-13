namespace Moq_Testing
{
    public interface IMailSender
    {
        bool SendMail(string recipientEmail, string bodyContent);
    }
}
