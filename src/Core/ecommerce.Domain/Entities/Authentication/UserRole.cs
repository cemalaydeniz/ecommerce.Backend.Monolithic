#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Domain.Entities.Authentication
{
    public class UserRole : BaseEntity<Guid>, ISoftDelete
    {
        public string Name { get; set; }
        public bool IsDeleted { get; private set; }
        public DateTime? SoftDeletedDate { get; private set; }
        public uint RowVersion { get; set; }

        // Relations
        public ICollection<User> Users { get; set; }
    }
}
