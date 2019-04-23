using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeComPlus.Models
{
    public class BankingEntity
    {
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

        public EntityGroup Group { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
    }
}
