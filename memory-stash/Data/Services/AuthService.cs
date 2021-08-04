using memory_stash.Data.Models;
using memory_stash.Data.Services.Interfaces;
using memory_stash.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace memory_stash.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly MemoryStashDbContext _context;


        public AuthService()
        {
        }

        public AuthService(IOptions<JWTSettings> jwtSettings, MemoryStashDbContext context)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context; 
        }

        public string Authenticate(UserAuth userAuth)
        {
            if(!_context.Users.Any(m => m.Name == userAuth.UserName && m.Password == userAuth.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userAuth.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
