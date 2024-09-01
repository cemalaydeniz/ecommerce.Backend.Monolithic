using ecommerce.Application.Validations.Constants;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Authentication
{
    public class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable(nameof(AppDbContext.Buyers))
                .HasBaseType<User>();

            builder.Property(b => b.FullName)
                .HasMaxLength(ValidationConstants.Name.MaxLength);

            // Relations
            builder.HasMany(b => b.Addresses)
                .WithOne();
        }
    }
}
