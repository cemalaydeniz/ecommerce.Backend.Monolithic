#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Entities.Authentication
{
    public class User : BaseEntity<Guid>, IUpdateDateAudit, ISoftDelete
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public int AccessFailCount { get; set; }
        public DateTime? LockoutEndDate { get; set; }
        public bool IsLockoutEnabled { get; set; }
        public Guid SecurityStamp { get; set; }
        public DateTime? UpdatedDate { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? SoftDeletedDate { get; private set; }

        // Relations
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserLogin> UserLogins { get; set; }
        public ICollection<UserToken> UserTokens { get; set; }
    }
}
