using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.Repositories.Entities.Authentication;
using ecommerce.Test.Integration.Persistence.DbContexts;
using ecommerce.Test.Utility;
using ecommerce.Test.Utility.Entities.Authentication;
using ecommerce.Test.Utility.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Test.Integration.Persistence.Repositories.Entities.Authentication
{
    [Collection(nameof(AppDbContextCollection))]
    public class UserRepositoryTest
    {
        private readonly UserRepository userRepository;

        public UserRepositoryTest(AppDbContextFixture appDbContextFixture)
        {
            userRepository = new UserRepository(appDbContextFixture.AppDbContext);
        }

        [Fact]
        public async Task SaveChangesAsync_WhenUserExistsAndIsUpdated_UpdatesUser()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            string newEmail = StringGenerator.Generate() + "@example.com";
            string newPhoneNumber = StringGenerator.Generate();

            user.Email = newEmail;
            user.PhoneNumber = newPhoneNumber;

            int result = await userRepository.SaveChangesAsync();

            // Assert
            var updatedUser = await userRepository.GetUserById(user.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.NotNull(updatedUser);
            Assert.Equal(newEmail, updatedUser.Email);
            Assert.Equal(newPhoneNumber, updatedUser.PhoneNumber);
        }

        [Fact]
        public async Task AddAsync_WhenUserValid_AddsNewOne()
        {
            // Arrange
            User user = UserGenerator.Generate();

            // Act
            await userRepository.AddAsync(user);
            int result = await userRepository.SaveChangesAsync();

            // Assert
            var addedUser = await userRepository.GetUserById(user.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Equal(user, addedUser);
        }

        [Fact]
        public async Task UpdateById_WhenIdExists_UpdatesUser()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            string newEmail = StringGenerator.Generate() + "@example.com";
            string newPhoneNumber = StringGenerator.Generate();

            int result = await userRepository.UpdateById(user.Id, _ => _
                .SetProperty(u => u.Email, newEmail)
                .SetProperty(u => u.PhoneNumber, newPhoneNumber));

            // Assert
            var updatedUser = await userRepository.GetUserById(user.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.NotNull(updatedUser);
            Assert.Equal(newEmail.ToLower(), updatedUser.Email);
            Assert.Equal(newPhoneNumber, updatedUser.PhoneNumber);
        }

        [Fact]
        public async Task UpdateById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = new Guid();

            // Act
            string newEmail = StringGenerator.Generate() + "@example.com";

            int result = await userRepository.UpdateById(id, _ => _
                .SetProperty(u => u.Email, newEmail));

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task SoftDelete_WhenUserExists_SoftDeletesUser()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            userRepository.SoftDelete(user);
            int result = await userRepository.SaveChangesAsync();

            // Assert
            var softDeletedUser = await userRepository.Table.IgnoreQueryFilters()
                .Where(u => u.Id.Equals(user.Id))
                .FirstOrDefaultAsync();

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Equal(user, softDeletedUser);
            Assert.True(softDeletedUser!.IsDeleted);
            Assert.NotNull(softDeletedUser.SoftDeletedDate);
            Assert.Null(softDeletedUser.Email);
            Assert.Null(softDeletedUser.PasswordHash);
            Assert.Null(softDeletedUser.PhoneNumber);
            Assert.False(softDeletedUser.IsEmailConfirmed);
            Assert.False(softDeletedUser.IsPhoneNumberConfirmed);
            Assert.False(softDeletedUser.IsTwoFactorEnabled);
            Assert.Equal(0, softDeletedUser.AccessFailCount);
            Assert.Null(softDeletedUser.LockoutEndDate);
            Assert.False(softDeletedUser.IsLockoutEnabled);
        }

        [Fact]
        public async Task SoftDeleteById_WhenIdExists_SoftDeletesUser()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            await userRepository.SoftDeleteById(user.Id);
            int result = await userRepository.SaveChangesAsync();

            // Assert
            var softDeletedUser = await userRepository.Table.IgnoreQueryFilters()
                .Where(u => u.Id.Equals(user.Id))
                .FirstOrDefaultAsync();

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Equal(user, softDeletedUser);
            Assert.True(softDeletedUser!.IsDeleted);
            Assert.NotNull(softDeletedUser.SoftDeletedDate);
            Assert.Null(softDeletedUser.Email);
            Assert.Null(softDeletedUser.PasswordHash);
            Assert.Null(softDeletedUser.PhoneNumber);
            Assert.False(softDeletedUser.IsEmailConfirmed);
            Assert.False(softDeletedUser.IsPhoneNumberConfirmed);
            Assert.False(softDeletedUser.IsTwoFactorEnabled);
            Assert.Equal(0, softDeletedUser.AccessFailCount);
            Assert.Null(softDeletedUser.LockoutEndDate);
            Assert.False(softDeletedUser.IsLockoutEnabled);
        }

        [Fact]
        public async Task SoftDeleteById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            await userRepository.SoftDeleteById(id);
            int result = await userRepository.SaveChangesAsync();

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task GetUserById_WhenIdExists_ReturnsUser()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userRepository.GetUserById(user.Id);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserById_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userRepository.GetUserById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByIdSelect_WhenIdExists_ReturnsUser()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userRepository.GetUserById(user.Id, u => new
            {
                u.Id,
                u.Email
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByIdSelect_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userRepository.GetUserById(id, u => new
            {
                u.Id,
                u.Email
            });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByEmail_WhenEmailExists_ReturnsUser()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userRepository.GetUserByEmail(user.Email!);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserByEmail_WhenEmailDoesNotExist_ReturnsNull()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await userRepository.GetUserByEmail(email);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByEmailSelect_WhenEmailExists_ReturnsUser()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userRepository.GetUserByEmail(user.Email!, u => new
            {
                u.Id,
                u.Email
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByEmailSelect_WhenEmailDoesNotExist_ReturnsNull()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await userRepository.GetUserByEmail(email, u => new
            {
                u.Id,
                u.Email
            });

            // Assert
            Assert.Null(result);
        }
    }
}
