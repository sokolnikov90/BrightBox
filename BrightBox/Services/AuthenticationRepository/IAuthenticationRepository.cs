namespace BrightBox.Services
{
    using BrightBox.Models;

    interface IAuthenticationRepository
    {
        bool Authentication(User user);

        bool CheckToken(string token);

        string CreateToken();
    }
}
