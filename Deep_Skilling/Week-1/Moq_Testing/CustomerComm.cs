namespace Moq_Testing
{
    public class CustomerComm
    {
        private readonly IMailSender _mailSender;

        public CustomerComm(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public bool DispatchMail(string emailAddress, string announcement)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }
            return _mailSender.SendMail(emailAddress, announcement);
        }
    }
}
