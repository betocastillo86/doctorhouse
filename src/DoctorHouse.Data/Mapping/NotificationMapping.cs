using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorHouse.Data.Mapping
{
    public class NotificationMapping : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.EmailSubject).HasMaxLength(500);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(e => e.SystemText).HasMaxLength(2000);

            builder.Property(e => e.MobileText).HasMaxLength(500);

            builder.Property(e => e.Tags).HasMaxLength(3000);
        }
    }
}