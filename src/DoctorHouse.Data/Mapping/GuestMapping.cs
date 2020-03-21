using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorHouse.Data.Mapping
{
    public class GuestMapping : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.JobPlace)
                .IsRequired()
                .HasMaxLength(140);

            builder.Property(c => c.JobAddress)
                .IsRequired()
                .HasMaxLength(140);

            builder.HasOne(c => c.Request)
               .WithMany()
               .HasForeignKey(c => c.RequestId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
