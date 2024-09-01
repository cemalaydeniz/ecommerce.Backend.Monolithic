using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.SeedData
{
    public static class SuperAdminSeedData
    {
        [Obsolete("This is used to generate migration code. The adjustments are in the migration file called SeedData")]
        public static void SeedSuperAdmin(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "placeholder@example.com",          // Change the email address in the migration file
                    PasswordHash = "placeholder",               // Request a new password after seeding the data
                    IsEmailConfirmed = true,
                    AccountStatus = EAccountStatus.Active,
                    SecurityStamp = Guid.NewGuid()
                });
        }
    }
}
