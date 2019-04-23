using System.Collections.Generic;
using System.Linq;
using NeComPlus.Data;
using NeComPlus.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NeComPlus.Controllers
{
    [Route("api/[controller]")]
    public abstract class ApiController : Controller
    {
        protected readonly AppDbContext Context;

        protected readonly IHostingEnvironment HostingEnvironment;

        protected ApiController(
            AppDbContext context = null,
            IHostingEnvironment hostingEnvironment = null)
        {
            this.Context = context;
            this.HostingEnvironment = hostingEnvironment;
        }

        protected List<string> ModelStateErrors
        {
            get
            {
                var errors = new List<string>();

                foreach (KeyValuePair<string, ModelStateEntry> pair in this.ModelState)
                {
                    foreach (var val in pair.Value.Errors)
                    {
                        errors.Add(val.ErrorMessage);
                    }
                }

                return errors;
            }
        }

        protected void CheckModelState()
        {
            if (!this.ModelState.IsValid)
            {
                throw new BadRequestException(this.ModelStateErrors.First());
            }
        }
    }
}