using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options) { }

        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<LoginTrafficLog> TrafficLogs{ get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionLog>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<LoginTrafficLog>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()"); 

            modelBuilder.Entity<RequestLog>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}