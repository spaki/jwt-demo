using JWTDemo.Domain.Dtos;
using JWTDemo.Infra.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTDemo.API.Services
{
    public class TokenService
    {
        private readonly AppSettings settings;

        public TokenService(
            AppSettings settings
        )
        {
            this.settings = settings;
        }

        public string GenerateToken(AuthResult auth)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.JWT.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, auth.AuthResultInfo.Name),
                    new Claim(ClaimTypes.Email, auth.AuthResultInfo.Email)
                }),
                Expires = DateTime.UtcNow.AddSeconds(settings.JWT.TimeoutInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
