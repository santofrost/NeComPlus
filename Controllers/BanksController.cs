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
    public class BanksController : ApiController
    {
        public BanksController(AppDbContext context)
            : base(context)
        {
        }

        [HttpGet]
        public IActionResult Get(int? id)
        {
            IQueryable<BankingEntity> banksQuery = this.Context.Banks.Include(b => b.Group).Where(b => b.ActiveStatus == true);

            if (id.HasValue)
            {
                banksQuery = banksQuery.Where(b => b.Id == id.Value);
            }

            return this.Ok(banksQuery.Select(b => new BankDto(b)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] BankingEntity bank)
        {
            this.Context.Banks.Add(bank);
            this.Context.SaveChanges();

            return this.Ok(bank);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] BankingEntity dto)
        {
            dto.Id = id;

            try
            {
                this.Context.Update(dto);
                this.Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.Context.Banks.Any(m => m.Id == id))
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
            var bank = this.Context.Banks.Single(b => b.Id == id);

            this.Context.Remove(bank);

            this.Context.SaveChanges();

            return this.Ok();
        }
    }
}