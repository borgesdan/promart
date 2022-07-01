using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Promart.API.Context;
using Promart.API.Models;


namespace Promart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopController : ControllerBase
    {
        readonly AppDbContext context;

        public WorkshopController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<Workshop> Get()
        {
            return Ok(new Workshop());
        }

        [HttpGet("{id:int}", Name = "GetWorkshop")]
        public async Task<ActionResult<Workshop>> Get(int id)
        {
            if (context.Workshops != null)
            {
                Workshop? workshop = await context.Workshops.FindAsync(id);
                return workshop != null ? Ok(workshop) : NotFound();
            }

            return BadRequest();
        }        

        [HttpPost]
        public async Task<IActionResult> Post(Workshop workshop)
        {
            if (context.Workshops != null)
            {
                context.Workshops.Add(workshop);
                await context.SaveChangesAsync();

                return CreatedAtRoute("GetWorkshop", new { id = workshop.Id }, workshop);
            }

            return BadRequest();
        }        

        [HttpPost("{workshopId:int}/Student/{studentId:int}")]
        public async Task<IActionResult> AddStudent(int workshopId, int studentId)
        {
            if (context.Workshops != null && context.Students != null)
            {
                var workshop = context.Workshops.Include(w => w.Students).FirstOrDefault(w => w.Id == workshopId);

                if (workshop == null)
                {
                    return NotFound();
                }

                Student? student = context.Students.FirstOrDefault(s => s.Id == studentId);

                if (student != null && !workshop.Students.Contains(student))
                {                    
                    workshop.Students.Add(student);
                    context.Entry(workshop).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    return CreatedAtRoute("GetWorkshop", new { id = workshop.Id }, workshop);
                }

                return NoContent();                
            }

            return BadRequest();
        }

        [HttpPost("{workshopId:int}/Volunteer/{volunteerId:int}")]
        public async Task<IActionResult> AddVolunteer(int workshopId, int volunteerId)
        {
            if (context.Workshops != null && context.Volunteers != null)
            {
                var workshop = context.Workshops.Include(w => w.Volunteers).FirstOrDefault(w => w.Id == workshopId);

                if (workshop == null)
                {
                    return NotFound();
                }

                Volunteer? volunteer = context.Volunteers.FirstOrDefault(v => v.Id == volunteerId);

                if (volunteer != null && !workshop.Volunteers.Contains(volunteer))
                {
                    workshop.Volunteers.Add(volunteer);
                    context.Entry(workshop).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    return CreatedAtRoute("GetWorkshop", new { id = workshop.Id }, workshop);
                }

                return NoContent();
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Workshop workshop)
        {
            if (context.Workshops != null || id != workshop.Id)
            {
                context.Entry(workshop).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(workshop);
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (context.Workshops != null)
            {
                Workshop? workshop = await context.Workshops.FirstOrDefaultAsync(w => w.Id == id);

                if (workshop != null)
                {
                    context.Workshops.Remove(workshop);
                    await context.SaveChangesAsync();
                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }

        [HttpDelete("{workshopId:int}/Student/{studentId:int}")]
        public async Task<IActionResult> RemoveStudent(int workshopId, int studentId)
        {
            if(context.Workshops != null && context.Students != null)
            {
                var workshop = context.Workshops.Include(w => w.Students).FirstOrDefault(w => w.Id == workshopId);

                if (workshop == null)
                {
                    return NotFound();
                }

                Student? student = workshop.Students.FirstOrDefault(s => s.Id == studentId);

                if (student != null)
                {
                    workshop.Students.Remove(student);
                    context.Entry(workshop).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok();
                }

                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{workshopId:int}/Volunteer/{volunteerId:int}")]
        public async Task<IActionResult> RemoveVoluteer(int workshopId, int volunteerId)
        {
            if (context.Workshops != null && context.Volunteers != null)
            {
                var workshop = context.Workshops.Include(w => w.Volunteers).FirstOrDefault(w => w.Id == workshopId);

                if (workshop == null)
                {
                    return NotFound();
                }

                Volunteer? volunteer = workshop.Volunteers.FirstOrDefault(v => v.Id == volunteerId);

                if (volunteer != null)
                {
                    workshop.Volunteers.Remove(volunteer);
                    context.Entry(workshop).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok();
                }

                return NoContent();
            }

            return BadRequest();
        }
    }
}
