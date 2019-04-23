using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeComPlus.Models
{
    public class EntityGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Color { get; set; }

        public IEnumerable<BankingEntity> Banks { get; set; }
    }
}
