using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NeComPlus.Models;

namespace NeComPlus.Data
{
    public class SeedDataUsers
    {
        public static async Task CreateUser(UserManager<AppUser> userManager, string email, string password, IdentityRole role)
        {
            var user = new AppUser { Role = role, UserName = email, Email = email };
            await userManager.CreateAsync(user, password);
        }
    }
}