namespace NeComPlus.Models.DTO
{
    using EntityGroup = NeComPlus.Models.EntityGroup;
    public class GroupDto
    {
        public GroupDto()
        {
        }

        public GroupDto(EntityGroup group)
        {
          this.Id = group.Id;
          this.Name = group.Name;
          this.Color = group.Color;
        }
        
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Color { get; set; }
    }
}