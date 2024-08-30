#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Entities.Common;

namespace ecommerce.Domain.Entities.Authentication
{
    public class BusinessUploadedFile : UploadedFile
    {
        public string Description { get; set; }
    }
}
