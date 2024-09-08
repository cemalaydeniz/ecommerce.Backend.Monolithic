using ecommerce.Domain.Entities.Common;
using ecommerce.Domain.Entities.Common.Enums;

namespace ecommerce.Test.Utility.Entities.Common
{
    public static class UploadedFileGenerator
    {
        public static UploadedFile Generate()
        {
            return new UploadedFile()
            {
                Id = Guid.NewGuid(),
                StorageType = EStorageType.Local,
                Link = StringGenerator.Generate()
            };
        }
    }
}
