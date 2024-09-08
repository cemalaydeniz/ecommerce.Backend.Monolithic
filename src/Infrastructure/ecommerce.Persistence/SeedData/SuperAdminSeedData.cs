using ecommerce.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.SeedData
{
    public static class SuperAdminSeedData
    {
        public static void SeedSuperAdmin(this ModelBuilder modelBuilder, UserRole superAdminRole)
        {
            Guid superAdminId = Guid.NewGuid();

            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = superAdminId,
                    Email = "placeholder@example.com",          // Change the email address in the migration file
                    PasswordHash = "placeholder",               // Request a new password after seeding the data
                    IsEmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid()
                });

            modelBuilder.Entity("UserUserRole")
                .HasData(new
                {
                    UsersId = superAdminId,
                    UserRolesId = superAdminRole.Id
                });
        }
    }
}
