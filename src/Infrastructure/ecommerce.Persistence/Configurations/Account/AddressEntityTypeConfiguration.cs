using ecommerce.Application.Validations.Constants;
using ecommerce.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Account
{
    public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Title)
                .HasMaxLength(ValidationConstants.Address.TitleMaxLength);

            builder.Property(a => a.StreetLine1)
                .HasMaxLength(ValidationConstants.Address.StreetMaxLength);

            builder.Property(a => a.StreetLine2)
                .HasMaxLength(ValidationConstants.Address.StreetMaxLength);

            builder.Property(a => a.StateOrProvince)
                .HasMaxLength(ValidationConstants.Address.StateOrProvinceMaxLength);

            builder.Property(a => a.ZipCode)
                .HasMaxLength(ValidationConstants.Address.ZipCodeMaxLength);

            builder.Property(a => a.City)
                .HasMaxLength(ValidationConstants.Address.CityMaxLength);

            builder.Property(a => a.Country)
                .HasMaxLength(ValidationConstants.Address.CountryMaxLength);
        }
    }
}
