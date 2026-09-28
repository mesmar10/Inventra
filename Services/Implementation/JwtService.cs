using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Inventra.Models.DomainModels;
using Inventra.Services.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace Inventra.Services.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
    {
            _configuration = configuration;
    }
        public string GenerateToken(Employee employee)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier,employee.Id.ToString()),
            new Claim(ClaimTypes.Email,employee.Email),
            new Claim(ClaimTypes.Role,employee.Role.ToString())
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
             key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
   issuer: _configuration["Jwt:Issuer"],
   audience: _configuration["Jwt:Audience"],
   claims: claims,
   expires: DateTime.UtcNow.AddMinutes(
       Convert.ToDouble(
           _configuration["Jwt:DurationInMinutes"])),
   signingCredentials: credentials
);
            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
