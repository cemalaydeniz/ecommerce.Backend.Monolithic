#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Entities.Common.Enums;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Entities.Common
{
    public class UploadedFile : BaseEntity<Guid>
    {
        public EStorageType StorageType { get; set; }
        public string Link { get; set; }
    }
}
