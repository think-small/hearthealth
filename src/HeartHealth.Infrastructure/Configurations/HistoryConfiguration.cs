using HeartHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeartHealth.Infrastructure.Configurations
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("Histories");
            builder.HasKey("Id");

            builder.Property(h => h.Id)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(36);

            builder.OwnsOne(h => h.DateRange, d =>
            {
                d.Property(d => d.Start)
                    .IsRequired()
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Start");
                d.Property(d => d.End)
                    .IsRequired()
                    .HasColumnType("smalldatetime")
                    .HasColumnName("End");
            });

            builder.OwnsOne(h => h.AverageBloodPressure, b =>
            {
                b.Property(b => b.Systolic).HasColumnName("AverageSystolic");
                b.Property(b => b.Diastolic).HasColumnName("AverageDiastolic");
                b.Ignore(b => b.Units);
                b.Ignore(b => b.Stage);
            });

            builder.HasMany(h => h.Measurements);
        }
    }
}
