namespace NeComPlus.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NeComPlus.Models;

    public class SeedData
    {
        public static async void Initialize(IApplicationBuilder app, UserManager<AppUser> userManager, IHostingEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                await CreateDataAsync(services, userManager);

                if (env.IsDevelopment())
                {
                    await SeedDataDevelopment.CreateData(services, userManager);
                }

                if (env.IsProduction())
                {
                    await SeedDataProduction.CreateData(services, userManager);
                }
            }
        }

        public static async Task CreateDataAsync(IServiceProvider serviceProvider, UserManager<AppUser> userManager)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            // Roles
            string[] roles = { "Admin" };

            var userMag = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            
            var roleMag = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                
            foreach (var roleName in roles)
            {
                var rol = context.Roles.SingleOrDefault(r => r.Name == roleName);
                if (rol == null)
                {
                    rol = new IdentityRole(roleName);
                    await roleMag.CreateAsync(rol);
                }

                context.SaveChanges();
            }

            // Admin user
            var email = "admin@admin.com";
            var adminRoleName = "Admin";
            var adminRole = context.Roles.Single(r => r.Name == adminRoleName);

            if (!context.Users.Any(u => u.Email == email))
            {
                await SeedDataUsers.CreateUser(userMag, email, "P4$$w0rD", adminRole);
            }

            context.SaveChanges();

            // Groups
            if (!context.Groups.Any())
            {
                context.Groups.Add(new EntityGroup { Name = "Group 1", Color = "green" });
                context.Groups.Add(new EntityGroup { Name = "Group 2", Color = "blue" });
                context.Groups.Add(new EntityGroup { Name = "Group 3", Color = "red" });
                context.Groups.Add(new EntityGroup { Name = "Group 4", Color = "orange" });
                context.SaveChanges();
            }

            // Banks
            if (!context.Banks.Any())
            {
                var group1 = context.Groups.Single(g => g.Name == "Group 1");
                context.Banks.Add(new BankingEntity {
                    Name = "Bank 1",
                    Address = "Address 1",
                    Town = "Town 1",
                    City = "City 1",
                    CP = "Cp 1",
                    Phonenumber = "+11111111111",
                    Email = "1@1.1",
                    Logo = "Logo 1",
                    Country = "Spain 1",
                    ActiveStatus = true,
                    Group = group1
                    });
                context.SaveChanges();
            }
        }
    }
}