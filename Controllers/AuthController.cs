namespace NeComPlus.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using NeComPlus.Data;
    using NeComPlus.Models;
    using NeComPlus.Helpers;
    using NeComPlus.Models.DTOs.User;

    public class AuthController : ApiController
    {
        private readonly AppSettings _appSettings;

        private readonly SignInManager<AppUser> signInManager;

        public AuthController(
            AppDbContext context,
            IHostingEnvironment hostingEnvironment,
            IOptions<AppSettings> appSettings,
            SignInManager<AppUser> signInManager)
            : base(context, hostingEnvironment)
        {
            this._appSettings = appSettings.Value;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel login)
        {
            var result = await this.signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = this.Context.Users
                    .Include(u => u.Role)
                    .Single(r => r.Email.ToLower() == login.Email.ToLower());
                var token = this.GenerateJwtToken(appUser);
                return this.Ok(new { token, user = new UserDto(appUser)});
            }

            return this.Unauthorized();
        }

        [HttpGet]
        public OkResult TestToken()
        {
            return this.Ok();
        }

        private string GenerateJwtToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Ticks.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public class LoginModel
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }
    }
}
