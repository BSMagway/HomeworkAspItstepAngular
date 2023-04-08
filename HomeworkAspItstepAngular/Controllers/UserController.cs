using HomeworkAspItstepAngular.Data;
using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeworkAspItstepAngular.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dBcontext;

        public UserController(AppDbContext dbContext) 
        {
            _dBcontext = dbContext;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            using (var trans = _dBcontext.Database.BeginTransaction())
            {
                var hasUserExists = _dBcontext.Users.Any(x => x.Username == request.Username);

                if (hasUserExists)
                {
                    return BadRequest();
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

            return Ok();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _dBcontext.Users
                .Where(x => x.Username == request.Username)
                .Where(x => x.Password == request.Password)
                .FirstOrDefault();

            if (user == null)
            {
                return Unauthorized();
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

            return Ok(new LoginResult
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }
    }
}
