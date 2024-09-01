using ecommerce.Application.Validations.Constants;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Authentication
{
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasIndex(ur => ur.Name)
                .IsUnique();
            builder.Property(ur => ur.Name)
                .IsUnicode(false)
                .HasMaxLength(ValidationConstants.Name.MaxLength)
                .HasConversion<LowerCaseConversion>();

            builder.Property(ur => ur.RowVersion)
                .IsRowVersion();

            // Relations
            builder.HasMany(ur => ur.Users)
                .WithMany(u => u.UserRoles);
        }
    }
}
