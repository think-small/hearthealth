using HeartHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeartHealth.Infrastructure.Configurations
{
    public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.Property(m => m.Timestamp)
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder.Property(m => m.RequiresVerification)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(m => m.Id)
                .HasColumnType("varchar")
                .HasMaxLength(36);

            builder.HasKey(m => m.Id);

            builder.OwnsOne(m => m.BloodPressure, b =>
            {
                b.Ignore(b => b.Units);
                b.Property(b => b.Systolic)
                    .HasColumnName("Systolic");
                b.Property(b => b.Diastolic)
                    .HasColumnName("Diastolic");
            });
            //  EFCore requries navigation be set to required to enforce owned
            //  object properties to be non-nullable.
            //  EFCore issue: https://github.com/dotnet/efcore/issues/12100#issuecomment-772831657
            builder.Navigation(b => b.BloodPressure)
                .IsRequired();
        }
    }
}
