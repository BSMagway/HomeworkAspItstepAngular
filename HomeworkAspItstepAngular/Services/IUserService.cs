using HomeworkAspItstepAngular.Models;

namespace HomeworkAspItstepAngular.Services
{
    public interface IUserService
    {
        LoginResult? Login(LoginRequest request);

        bool Register(RegisterRequest request);
    }
}
