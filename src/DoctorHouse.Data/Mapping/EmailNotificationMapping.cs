using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorHouse.Data.Mapping
{
    public class EmailNotificationMapping : IEntityTypeConfiguration<EmailNotification>
    {
        public void Configure(EntityTypeBuilder<EmailNotification> builder)
        {
            builder.Property(e => e.Body).IsRequired();

            builder.HasIndex(e => new { e.SentTries, e.SentDate });

            builder.Property(e => e.Cc)
                .HasColumnName("CC")
                .HasMaxLength(500);

            builder.HasIndex(c => c.IsSent);

            builder.Property(e => e.Subject)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(e => e.To)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ToName).HasMaxLength(200);
        }
    }
}