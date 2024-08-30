#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Domain.Entities.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Entities.Authentication
{
    public class UserToken : BaseEntity<Guid>
    {
        public Token Token { get; set; }
        public ETokenPurpose Purpose { get; set; }

        // Relations
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
