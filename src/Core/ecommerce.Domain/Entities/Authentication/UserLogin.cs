#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Entities.Common.ValueObjects;
using ecommerce.Domain.SeedWork;
using System.Net;

namespace ecommerce.Domain.Entities.Authentication
{
    public class UserLogin : BaseEntity<Guid>, IUpdateDateAudit
    {
        public Token RefreshToken { get; set; }
        public IPAddress IpAddress { get; set; }
        public string? DeviceInformation { get; set; }
        public DateTime? UpdatedDate { get; private set; }

        // Relations
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
