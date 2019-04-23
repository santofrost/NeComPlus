namespace NeComPlus.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using NeComPlus.Data;
    using NeComPlus.Exceptions;
    using NeComPlus.Models;
    using NeComPlus.Services;
    using NeComPlus.Models.DTOs.User;
    
    [Authorize]
    public class UsersController : ApiController
    {
        private IUserService UserService;

        private readonly SignInManager<AppUser> signInManager;

        public UsersController(IUserService userService,
            SignInManager<AppUser> signInManager)
        {
            this.UserService = userService;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserCreateDto userParam)
        {
            var result = await this.signInManager.PasswordSignInAsync(userParam.Email, userParam.Password, false, false);

            if (result.Succeeded) {
                var user = UserService.Authenticate(userParam.Email, userParam.Password);

                return this.Ok(user);
            }

            return this.Unauthorized();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  UserService.GetAll();
            return Ok(users);
        }
    }
}