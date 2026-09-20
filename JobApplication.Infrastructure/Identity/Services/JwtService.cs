using JobApplication.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobApplication.Infrastructure.Identity.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _securityKey;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            _securityKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( _configuration["Jwt:Key"]!));
        }

        public string GenerateToken(string userId, string email, IList<string> roles)
        {
            var claims = new List<Claim>{
        new Claim(ClaimTypes.NameIdentifier, userId),
         new Claim(JwtRegisteredClaimNames.Email, email)  };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var credentials = new SigningCredentials( _securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes( double.Parse( _configuration["Jwt:DurationInMinutes"]!)),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}