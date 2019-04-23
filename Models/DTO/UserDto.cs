namespace NeComPlus.Models.DTOs.User
{
    public class UserDto
    {
        public UserDto()
        {
        }

        public UserDto(AppUser user)
        {
            this.Name = user.UserName;
            this.Email = user.Email;
            this.Role = user.Role.Name;
            this.RoleId = user.Role.Id;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string RoleId { get; set; }
    }
}