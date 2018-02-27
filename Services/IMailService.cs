namespace ExamAce.Services
{
    public interface IMailService
    {
        void SendEmail(string to, string subject, string body);
    }
}