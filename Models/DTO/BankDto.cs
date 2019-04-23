namespace NeComPlus.Models.DTO
{
    using NeComPlus.Models.DTO;
    using BankingEntity = NeComPlus.Models.BankingEntity;
    public class BankDto
    {
        public BankDto()
        {
        }

        public BankDto(BankingEntity bank)
        {
          this.Id = bank.Id;
          this.Name = bank.Name;
          this.Address = bank.Address;
          this.Town = bank.Town;
          this.City = bank.City;
          this.CP = bank.CP;
          this.Phonenumber = bank.Phonenumber;
          this.Email = bank.Email;
          this.Logo = bank.Logo;
          this.Country = bank.Country;
          this.ActiveStatus = bank.ActiveStatus;
          this.Group = new GroupDto(bank.Group);
        }
        
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public string Town { get; set; }
        
        public string City { get; set; }

        public string CP { get; set; }
        
        public string Phonenumber { get; set; }
        
        public string Email { get; set; }
        
        public string Logo { get; set; }

        public string Country { get; set; }
        
        public bool ActiveStatus { get; set; }

        public GroupDto Group { get; set; }
    }
}