using ecommerce.Domain.Entities.Common;
using ecommerce.Domain.Entities.Common.Enums;
using ecommerce.Persistence.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations.Common
{
    public class UploadedFileEntityTypeConfiguration : IEntityTypeConfiguration<UploadedFile>
    {
        public void Configure(EntityTypeBuilder<UploadedFile> builder)
        {
            builder.Property(f => f.StorageType)
                .HasConversion<EnumConversion<EStorageType>>();
        }
    }
}
