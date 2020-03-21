using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorHouse.Data.Mapping
{
    public class LocationMapping : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.HasOne(c => c.ParentLocation)
               .WithMany()
               .HasForeignKey(c => c.ParentLocationId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}