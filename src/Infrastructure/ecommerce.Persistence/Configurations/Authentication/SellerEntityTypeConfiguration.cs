using ecommerce.Application.Validations.Constants;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Authentication
{
    public class SellerEntityTypeConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable(nameof(AppDbContext.Sellers))
                .HasBaseType<User>();

            builder.Property(s => s.BusinessName)
                .HasMaxLength(ValidationConstants.Name.MaxLength);

            builder.Property(s => s.ContactName)
                .HasMaxLength(ValidationConstants.Name.MaxLength);

            builder.Property(s => s.ContactEmail)
                .HasMaxLength(ValidationConstants.Email.MaxLength);

            builder.Property(s => s.ContactPhoneNumber)
                .HasMaxLength(ValidationConstants.PhoneNumber.MaxLength);

            builder.Property(s => s.TinNumber)
                .HasMaxLength(ValidationConstants.TinNumber.MaxLength);

            builder.Property(s => s.VatNumber)
                .HasMaxLength(ValidationConstants.VatNumber.MaxLength);

            builder.ComplexProperty(s => s.CreditCardInformation);

            // Relations
            builder.HasOne(s => s.BusinessAddress)
                .WithOne()
                .HasForeignKey<Seller>(s => s.BusinessAddressId);

            builder.HasOne(s => s.BillingAddress)
                .WithOne()
                .HasForeignKey<Seller>(s => s.BillingAddressId);

            builder.HasMany(s => s.UploadedFiles)
                .WithOne();
        }
    }
}
