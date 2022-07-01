using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Promart.API.Context;
using Promart.API.Models;

namespace Promart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteerController : ControllerBase
    {
        readonly AppDbContext context;

        public VolunteerController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<Volunteer> Get()
        {
            return Ok(new Volunteer());
        }

        [HttpGet("{id:int}", Name = "GetVolunteer")]
        public async Task<ActionResult<Volunteer>> Get(int id)
        {
            if (context.Volunteers != null)
            {
                Volunteer? volunteer = await context.Volunteers.FindAsync(id);
                return volunteer != null ? Ok(volunteer) : NotFound();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Volunteer volunteer)
        {
            if (context.Volunteers != null)
            {
                context.Volunteers.Add(volunteer);
                await context.SaveChangesAsync();

                return CreatedAtRoute("GetVolunteer", new { id = volunteer.Id }, volunteer);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Volunteer volunteer)
        {
            if (context.Volunteers != null || id != volunteer.Id)
            {
                context.Entry(volunteer).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(volunteer);
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (context.Volunteers != null)
            {
                Volunteer? volunteer = context.Volunteers.FirstOrDefault(v => v.Id == id);

                if (volunteer != null)
                {
                    context.Volunteers.Remove(volunteer);
                    await context.SaveChangesAsync();
                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}
