using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Persistence.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Authentication
{
    public class UserTokenEntityTypeConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ComplexProperty(ut => ut.Token);

            builder.Property(ut => ut.Purpose)
                .HasConversion<EnumConversion<ETokenPurpose>>();

            // Relations
            builder.HasOne(ut => ut.User)
                .WithMany(u => u.UserTokens)
                .HasForeignKey(ut => ut.UserId);
        }
    }
}
