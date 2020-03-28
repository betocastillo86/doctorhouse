using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorHouse.Data.Mapping
{
    public class RequestMapping : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.Property(c => c.Description)
                .IsRequired();

            builder.HasOne(c => c.Place)
               .WithMany()
               .HasForeignKey(c => c.PlaceId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.UserRequester)
               .WithMany()
               .HasForeignKey(c => c.UserRequesterId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}