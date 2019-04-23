namespace NeComPlus.Models.DTOs.User
{
    public class UserCreateDto : UserDto
    {
        public UserCreateDto()
        {
        }

        public UserCreateDto(AppUser user) : base(user)
        {
        }
        
        public string Password { get; set; }

    }
}