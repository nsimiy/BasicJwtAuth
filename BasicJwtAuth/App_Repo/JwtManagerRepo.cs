using BasicJwtAuth.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BasicJwtAuth.App_Repo
{
    public class JwtManagerRepo : IJwtManagerRepo
    {

        Dictionary<string, string> UserRecords = new Dictionary<string, string> {
            {"user1", "password1"},
            {"user2", "password2"},
            { "user3", "password3"}
        };

        public JwtManagerRepo(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Tokens Authenticate(Users user, object JwtTokenDescriptor)
        {
            if (!UserRecords.Any(x => x.Key == user.Name && x.Value == user.Password))
            {
                return null;
            }

            //else generate JWT token
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(Configuration["Jwt:key"]);

            var tokendescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature),

            };

            var token = tokenhandler.CreateToken(tokendescriptior);
            return new Tokens {Token = tokenhandler.WriteToken(token)};

        }

        public object Authenticate(Users userdata)
        {
            throw new NotImplementedException();
        }
    }
}


