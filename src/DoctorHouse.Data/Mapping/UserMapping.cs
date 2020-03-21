using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorHouse.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Salt)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(c => c.PasswordRecoveryToken)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasOne(c => c.Location)
               .WithMany()
               .HasForeignKey(c => c.LocationId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}