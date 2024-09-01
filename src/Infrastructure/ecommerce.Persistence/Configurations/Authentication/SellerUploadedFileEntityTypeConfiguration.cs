using ecommerce.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Authentication
{
    public class SellerUploadedFileEntityTypeConfiguration : IEntityTypeConfiguration<SellerUploadedFile>
    {
        public void Configure(EntityTypeBuilder<SellerUploadedFile> builder)
        {
            builder.Property(f => f.Description)
                .HasMaxLength(255);
        }
    }
}
