using ecommerce.Application.Validations.Constants;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Persistence.Conversions;
using ecommerce.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Authentication
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(AppDbContext.Users));

            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.Email)
                .IsUnicode(false)
                .HasMaxLength(ValidationConstants.Email.MaxLength)
                .HasConversion<EmailConversion>();

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(ValidationConstants.PhoneNumber.MaxLength);

            builder.Property(u => u.AccountStatus)
                .HasConversion<EnumConversion<EAccountStatus>>();

            // Relations
            builder.HasMany(u => u.UserRoles)
                .WithMany(ur => ur.Users);

            builder.HasMany(u => u.UserLogins)
                .WithOne(ul => ul.User)
                .HasForeignKey(ul => ul.UserId);

            builder.HasMany(u => u.UserTokens)
                .WithOne(ut => ut.User)
                .HasForeignKey(ut => ut.UserId);
        }
    }
}
