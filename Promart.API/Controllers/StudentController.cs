using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Promart.API.Context;
using Promart.API.Models;

namespace Promart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        readonly AppDbContext context;

        public StudentController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<Student> Get()
        {
            return Ok(new Student());
        }

        [HttpGet("{id:int}", Name = "GetStudent")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            if (context.Students != null)
            {
                Student? student = await context.Students.FindAsync(id);
                return student != null ? Ok(student) : NotFound();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            if (context.Students != null)
            {
                context.Students.Add(student);
                await context.SaveChangesAsync();

                return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Student student)
        {
            if (context.Students != null || id != student.Id)
            {
                context.Entry(student).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(student);
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (context.Students != null)
            {
                var student = context.Students.FirstOrDefault(s => s.Id == id);

                if (student != null)
                {
                    context.Students.Remove(student);
                    await context.SaveChangesAsync();
                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}
