using Microsoft.EntityFrameworkCore;
using Promart.API.Models;

namespace Promart.API.Context
{
    public class AppDbContext : DbContext
    {
        DbSet<Student>? Students { get; set; }
        DbSet<Voluntary>? Volunteers { get; set; }
        DbSet<Workshop>? Workshops { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}