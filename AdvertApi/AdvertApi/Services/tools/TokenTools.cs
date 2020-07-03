using AdvertApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.Services.tools
{
    public class TokenTools
    {
        private readonly IConfiguration _configuration;

        public TokenTools(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetRefreshToken(int id, string login)
        { 
            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, "client")
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: "s17651",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

            var refreshToken = Guid.NewGuid();

            return refreshToken.ToString();
        }
    }
}
