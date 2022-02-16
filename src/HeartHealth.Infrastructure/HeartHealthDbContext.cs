using HeartHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace HeartHealth.Infrastructure
{
    public class HeartHealthDbContext : DbContext
    {
        public HeartHealthDbContext(DbContextOptions<HeartHealthDbContext> options) : base(options) { }

        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeartHealthDbContext).Assembly);

            var firstId = Guid.NewGuid();
            var secondId = Guid.NewGuid();
            var thirdId = Guid.NewGuid();
            var today = DateTime.UtcNow;
            var yesterday = DateTime.UtcNow.AddDays(-1);
            var twoDaysAgo = DateTime.UtcNow.AddDays(-2);

            modelBuilder.Entity<Measurement>(m =>
            {
                m.HasData(new
                {
                    Id = firstId,
                    Timestamp = today,
                    RequiresVerification = false
                });
                m.OwnsOne(m => m.BloodPressure)
                .HasData(new
                {
                    MeasurementId = firstId, 
                    Systolic = 120, 
                    Diastolic = 80
                });
            });
            modelBuilder.Entity<Measurement>(m =>
            {
                m.HasData(new
                {
                    Id = secondId,
                    Timestamp = yesterday,
                    RequiresVerification = false
                });
                m.OwnsOne(m => m.BloodPressure)
                .HasData(new
                {
                    MeasurementId = secondId, 
                    Systolic = 112, 
                    Diastolic = 77
                });
            });
            modelBuilder.Entity<Measurement>(m => {
                m.HasData(new
                {
                    Id = thirdId,
                    Timestamp = twoDaysAgo,
                    RequiresVerification = false
                });
                m.OwnsOne(m => m.BloodPressure)
                .HasData(new
                {
                    MeasurementId = thirdId, 
                    Systolic = 118, 
                    Diastolic = 81
                });
            });
        }
    }
}
