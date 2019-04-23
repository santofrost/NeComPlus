namespace NeComPlus.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NeComPlus.Models;

    public class SeedDataProduction
    {
        public static async Task CreateData(IServiceProvider serviceProvider, UserManager<AppUser> userManager)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            
            // Add data here
        }
    }
}