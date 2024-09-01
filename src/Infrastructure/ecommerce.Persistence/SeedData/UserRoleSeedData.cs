using ecommerce.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.SeedData
{
    public static class UserRoleSeedData
    {
        [Obsolete("This is used to generate migration code. The adjustments are in the migration file called SeedData")]
        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasData(new List<UserRole>()
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
                });
        }
    }
}
