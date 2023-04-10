using HomeworkAspItstepAngular.Data;
using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using HomeworkAspItstepAngular.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeworkAspItstepAngular.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dBcontext;

        public UserService(AppDbContext dbContext)
        {
            _dBcontext = dbContext;
        }

        public LoginResult? Login(LoginRequest request)
        {
            var user = _dBcontext.Users
                .Where(x => x.Username == request.Username)
                .Where(x => x.Password == request.Password)
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim("ApplicationUserId", user.ApplicationUserId.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7183",
                audience: "https://localhost:7183",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")), SecurityAlgorithms.HmacSha256)
            );

            return new LoginResult
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }

        public bool Register(RegisterRequest request)
        {
            using (var trans = _dBcontext.Database.BeginTransaction())
            {
                var hasUserExists = _dBcontext.Users.Any(x => x.Username == request.Username);

                if (hasUserExists)
                {
                    return false;
                }

                var user = new ApplicationUser
                {
                    Username = request.Username,
                    Password = request.Password,
                    ApplicationUserId = Guid.NewGuid(),
                };

                _dBcontext.Users.Add(user);
                _dBcontext.SaveChanges();
                trans.Commit();
            }

            return true;
        }
    }
}
