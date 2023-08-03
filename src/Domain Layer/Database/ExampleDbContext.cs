using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}