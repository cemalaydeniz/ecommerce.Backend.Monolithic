using ecommerce.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Authentication
{
    public class UserLoginEntityTypeConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ComplexProperty(ul => ul.RefreshToken);

            builder.Property(ul => ul.IpAddress)
                .HasColumnType("inet");

            builder.Property(ul => ul.DeviceInformation)
                .HasMaxLength(255);

            // Relations
            builder.HasOne(ul => ul.User)
                .WithMany(u => u.UserLogins)
                .HasForeignKey(ul => ul.UserId);
        }
    }
}
