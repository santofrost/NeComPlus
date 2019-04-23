using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using NeComPlus.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NeComPlus.Models;
using NeComPlus.Helpers;

namespace NeComPlus.Services
{
    public interface IUserService
    {
        AppUser Authenticate(string username, string password);
        IEnumerable<AppUser> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext Context;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, AppDbContext context = null)
        {
            this.Context = context;
            _appSettings = appSettings.Value;
        }

        public AppUser Authenticate(string email, string password)
        {
            var user = this.Context.Users.SingleOrDefault(x => x.UserName.ToLower() == email.ToLower());

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<AppUser> GetAll()
        {
            // return users without passwords
            return this.Context.Users;
        }
    }
}