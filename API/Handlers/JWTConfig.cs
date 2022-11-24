using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Handlers
{
    public class JWTConfig
    {
        private readonly IConfiguration configuration;

        public JWTConfig(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Token(int id, string fullname, string email, string role)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", id.ToString()),
                new Claim("fullName", fullname),
                new Claim("email", email),
                new Claim("role", role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: signIn
                );

            var JWT = new JwtSecurityTokenHandler().WriteToken(token);

            return JWT;
        }

    }
}
