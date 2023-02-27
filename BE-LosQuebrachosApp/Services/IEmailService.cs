using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email); 
    }
}
