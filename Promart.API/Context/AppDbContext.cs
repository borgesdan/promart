using Microsoft.EntityFrameworkCore;
using Promart.API.Models;

namespace Promart.API.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student>? Students { get; set; }
        public DbSet<Volunteer>? Volunteers { get; set; }
        public DbSet<Workshop>? Workshops { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}