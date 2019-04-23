namespace NeComPlus.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    using NeComPlus.Data;
    using NeComPlus.Models;
    using NeComPlus.Models.DTO;

    [Authorize]
    public class GroupsController : ApiController
    {
        public GroupsController(AppDbContext context)
            : base(context)
        {
        }

        [HttpGet]
        public IActionResult Get(int? id)
        {
            IQueryable<EntityGroup> groupsQuery = this.Context.Groups;

            if (id.HasValue)
            {
                groupsQuery = groupsQuery.Where(b => b.Id == id.Value);
            }

            return this.Ok(groupsQuery.Select(g => new GroupDto(g)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] EntityGroup group)
        {
            this.Context.Groups.Add(group);
            this.Context.SaveChanges();

            return this.Ok(group);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] EntityGroup dto)
        {
            try
            {
                this.Context.Update(dto);
                this.Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.Context.Groups.Any(m => m.Id == id))
                {
                    return this.NotFound();
                }

                throw;
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public OkResult Delete(int id)
        {
            var group = this.Context.Groups.Include(g => g.Banks).Single(b => b.Id == id);

            foreach (var bank in group.Banks)
            {
                this.Context.Banks.Remove(bank);
            }

            this.Context.Remove(group);

            this.Context.SaveChanges();

            return this.Ok();
        }
    }
}