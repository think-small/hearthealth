using HeartHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace HeartHealth.Infrastructure
{
    public class HeartHealthDbContext : DbContext
    {
        public HeartHealthDbContext(DbContextOptions<HeartHealthDbContext> options) : base(options) { }

        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeartHealthDbContext).Assembly);

            var id = Guid.Parse("11111111-1111-1111-1111-111111111111");

            modelBuilder.Entity<Measurement>(m =>
            {
                m.HasData(new
                {
                    Id = id,
                    Timestamp = DateTime.UtcNow
                });
                m.OwnsOne(m => m.BloodPressure)
                .HasData(new
                {
                    MeasurementId = id, Systolic = 120, Diastolic = 80
                });
            });
        }
    }
}
