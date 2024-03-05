namespace Application.Interfaces;

public interface IEmailService
{
    Task SendNewBookingAsync(string emailAdmin, string emailUser, string fullName, string dateTime, string serviceName, string price);
    Task SendMailRemind(string email, string fullName, string dateTime);
    Task SendNewPayAsync(string emailAdmin, string emailUser, string fullName, string dateTime, string price);
}