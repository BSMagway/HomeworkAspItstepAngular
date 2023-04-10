using HomeworkAspItstepAngular.Models;

namespace HomeworkAspItstepAngular.Services.Interface
{
    public interface IUserService
    {
        LoginResult? Login(LoginRequest request);

        bool Register(RegisterRequest request);
    }
}
