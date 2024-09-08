using ecommerce.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.SeedData
{
    public static class UserRoleSeedData
    {
        public static List<UserRole> SeedUserRoles(this ModelBuilder modelBuilder)
        {
            List<UserRole> roles = new List<UserRole>()
            {
                new UserRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "super admin"
                },
                new UserRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "admin"
                },
                new UserRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "seller"
                }
            };

            modelBuilder.Entity<UserRole>()
                .HasData(roles);

            return roles;
        }
    }
}
