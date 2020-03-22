using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorHouse.Data.Mapping
{
    public class PlaceMapping : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.Property(c => c.Latitude)
                .HasColumnType("decimal");

            builder.Property(c => c.Longitude)
                .HasColumnType("decimal");

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.Description)
                .IsRequired();

            builder.HasOne(c => c.Location)
               .WithMany()
               .HasForeignKey(c => c.LocationId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.User)
               .WithMany()
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}