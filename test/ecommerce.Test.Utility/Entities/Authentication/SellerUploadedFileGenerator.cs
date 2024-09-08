using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Common.Enums;

namespace ecommerce.Test.Utility.Entities.Authentication
{
    public static class SellerUploadedFileGenerator
    {
        public static SellerUploadedFile Generate()
        {
            return new SellerUploadedFile()
            {
                Id = Guid.NewGuid(),
                StorageType = EStorageType.Local,
                Link = StringGenerator.Generate(),
                Description = StringGenerator.Generate()
            };
        }
    }
}
