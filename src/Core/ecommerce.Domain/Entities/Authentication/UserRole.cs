#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Domain.Entities.Authentication
{
    public class UserRole : BaseEntity<Guid>
    {
        public string Name { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        // Relations
        public ICollection<User> Users { get; set; }
    }
}
