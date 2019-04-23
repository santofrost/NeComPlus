using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace NeComPlus.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string User { get; set; }
        
        public string Token { get; set; }
        
        public IdentityRole Role { get; set; }
    }
}
