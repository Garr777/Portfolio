namespace Portfolio.Misc.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(Message message);
    }
}