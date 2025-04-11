using Microsoft.EntityFrameworkCore;
using RiragRPCTestProject.Models;

namespace RiragRPCTestProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonEntity> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
