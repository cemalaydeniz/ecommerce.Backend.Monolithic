using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.Repositories.Entities.Authentication;
using ecommerce.Test.Integration.Persistence.DbContexts;
using ecommerce.Test.Utility.Entities.Authentication;
using ecommerce.Test.Utility;
using ecommerce.Test.Utility.Fixtures;
using Microsoft.EntityFrameworkCore;
using ecommerce.Test.Utility.Entities.Account;

namespace ecommerce.Test.Integration.Persistence.Repositories.Entities.Authentication
{
    [Collection(nameof(AppDbContextCollection))]
    public class BuyerRepositoryTest
    {
        private readonly BuyerRepository buyerRepository;

        public BuyerRepositoryTest(AppDbContextFixture appDbContextFixture)
        {
            buyerRepository = new BuyerRepository(appDbContextFixture.AppDbContext);
        }

        [Fact]
        public async Task SaveChangesAsync_WhenBuyerExistsAndIsUpdated_UpdatesBuyer()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            string newEmail = StringGenerator.Generate() + "@example.com";
            string newPhoneNumber = StringGenerator.Generate();
            string newFullName = StringGenerator.Generate();

            buyer.Email = newEmail;
            buyer.PhoneNumber = newPhoneNumber;
            buyer.FullName = newFullName;

            int result = await buyerRepository.SaveChangesAsync();

            // Assert
            var updatedBuyer = await buyerRepository.GetUserById(buyer.Id);

            Assert.True(result == 2, $"{result} row(s) are affected. It should have been 2");
            Assert.NotNull(updatedBuyer);
            Assert.Equal(newEmail, updatedBuyer.Email);
            Assert.Equal(newPhoneNumber, updatedBuyer.PhoneNumber);
            Assert.Equal(newFullName, updatedBuyer.FullName);
        }

        [Fact]
        public async Task AddAsync_WhenBuyerValid_AddsNewOne()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            // Act
            await buyerRepository.AddAsync(buyer);
            int result = await buyerRepository.SaveChangesAsync();

            // Assert
            var addedBuyer = await buyerRepository.GetUserById(buyer.Id);

            Assert.True(result == 2, $"{result} row(s) are affected. It should have been 2");
            Assert.Equal(buyer, addedBuyer);
        }

        [Fact]
        public async Task UpdateById_WhenIdExists_UpdatesBuyer()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            string newFullName = StringGenerator.Generate();

            int result = await buyerRepository.UpdateById(buyer.Id, _ => _
                .SetProperty(b => b.FullName, newFullName));

            // Assert
            var updatedBuyer = await buyerRepository.GetUserById(buyer.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.NotNull(updatedBuyer);
            Assert.Equal(newFullName, updatedBuyer.FullName);
        }

        [Fact]
        public async Task UpdateById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            string newFullName = StringGenerator.Generate();

            int result = await buyerRepository.UpdateById(id, _ => _
                .SetProperty(b => b.FullName, newFullName));

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task SoftDelete_WhenBuyerExists_SoftDeletesBuyer()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            buyerRepository.SoftDelete(buyer);
            int result = await buyerRepository.SaveChangesAsync();

            // Assert
            var softDeletedBuyer = await buyerRepository.Table.IgnoreQueryFilters()
                .Where(b => b.Id.Equals(buyer.Id))
                .FirstOrDefaultAsync();

            Assert.True(result == 2, $"{result} row(s) are affected. It should have been 2");
            Assert.Equal(buyer, softDeletedBuyer);
            Assert.True(softDeletedBuyer!.IsDeleted);
            Assert.NotNull(softDeletedBuyer.SoftDeletedDate);
            Assert.Null(softDeletedBuyer.Email);
            Assert.Null(softDeletedBuyer.PasswordHash);
            Assert.Null(softDeletedBuyer.PhoneNumber);
            Assert.False(softDeletedBuyer.IsEmailConfirmed);
            Assert.False(softDeletedBuyer.IsPhoneNumberConfirmed);
            Assert.False(softDeletedBuyer.IsTwoFactorEnabled);
            Assert.Equal(0, softDeletedBuyer.AccessFailCount);
            Assert.Null(softDeletedBuyer.LockoutEndDate);
            Assert.False(softDeletedBuyer.IsLockoutEnabled);
            Assert.Null(softDeletedBuyer.FullName);
        }

        [Fact]
        public async Task SoftDeleteById_WhenIdExists_SoftDeletesBuyer()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            await buyerRepository.SoftDeleteById(buyer.Id);
            int result = await buyerRepository.SaveChangesAsync();

            // Assert
            var softDeletedBuyer = await buyerRepository.Table.IgnoreQueryFilters()
                .Where(b => b.Id.Equals(buyer.Id))
                .FirstOrDefaultAsync();

            Assert.True(result == 2, $"{result} row(s) are affected. It should have been 2");
            Assert.Equal(buyer, softDeletedBuyer);
            Assert.True(softDeletedBuyer!.IsDeleted);
            Assert.NotNull(softDeletedBuyer.SoftDeletedDate);
            Assert.Null(softDeletedBuyer.Email);
            Assert.Null(softDeletedBuyer.PasswordHash);
            Assert.Null(softDeletedBuyer.PhoneNumber);
            Assert.False(softDeletedBuyer.IsEmailConfirmed);
            Assert.False(softDeletedBuyer.IsPhoneNumberConfirmed);
            Assert.False(softDeletedBuyer.IsTwoFactorEnabled);
            Assert.Equal(0, softDeletedBuyer.AccessFailCount);
            Assert.Null(softDeletedBuyer.LockoutEndDate);
            Assert.False(softDeletedBuyer.IsLockoutEnabled);
            Assert.Null(softDeletedBuyer.FullName);
        }

        [Fact]
        public async Task SoftDeleteById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            await buyerRepository.SoftDeleteById(id);
            int result = await buyerRepository.SaveChangesAsync();

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task GetUserById_WhenIdExists_ReturnsBuyer()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            var result = await buyerRepository.GetUserById(buyer.Id);

            // Assert
            Assert.Equal(buyer, result);
        }

        [Fact]
        public async Task GetUserById_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await buyerRepository.GetUserById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByIdSelect_WhenIdExists_ReturnsBuyer()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            var result = await buyerRepository.GetUserById(buyer.Id, b => new
            {
                b.Id,
                b.FullName
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(buyer.Id, result.Id);
            Assert.Equal(buyer.FullName, result.FullName);
        }

        [Fact]
        public async Task GetUserByIdSelect_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await buyerRepository.GetUserById(id, b => new
            {
                b.Id,
                b.FullName
            });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByEmail_WhenEmailExists_ReturnsBuyer()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            var result = await buyerRepository.GetUserByEmail(buyer.Email!);

            // Assert
            Assert.Equal(buyer, result);
        }

        [Fact]
        public async Task GetUserByEmail_WhenEmailDoesNotExist_ReturnsNull()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await buyerRepository.GetUserByEmail(email);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByEmailSelect_WhenEmailExists_ReturnsBuyer()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            var result = await buyerRepository.GetUserByEmail(buyer.Email!, b => new
            {
                b.Id,
                b.FullName
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(buyer.Id, result.Id);
            Assert.Equal(buyer.FullName, result.FullName);
        }

        [Fact]
        public async Task GetUserByEmailSelect_WhenEmailDoesNotExist_ReturnsNull()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await buyerRepository.GetUserByEmail(email, b => new
            {
                b.Id,
                b.FullName
            });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAddressesById_WhenIdExistsAndThereAreAddresses_ReturnsAddresses()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            buyer.Addresses.Add(AddressGenerator.Generate());
            buyer.Addresses.Add(AddressGenerator.Generate());
            buyer.Addresses.Add(AddressGenerator.Generate());

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            var result = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(buyer.Addresses.OrderBy(a => a.Id)
                .SequenceEqual(result.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetAllAddressesById_WhenIdExistsButThereAreNoAddresses_ReturnsEmptyList()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            var result = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAddressesById_WhenIdDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await buyerRepository.GetAllAddressesById(id, 1, 10);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAddressesByEmail_WhenEmailExistsAndThereAreAddresses_ReturnsAddresses()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            buyer.Addresses.Add(AddressGenerator.Generate());
            buyer.Addresses.Add(AddressGenerator.Generate());
            buyer.Addresses.Add(AddressGenerator.Generate());

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            var result = await buyerRepository.GetAllAddressesByEmail(buyer.Email!, 1, 10);

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(buyer.Addresses.OrderBy(a => a.Id)
                .SequenceEqual(result.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetAllAddressesByEmail_WhenEmailExistsButThereAreNoAddresses_ReturnsEmptyList()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            var result = await buyerRepository.GetAllAddressesByEmail(buyer.Email!, 1, 10);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAddressesByEmail_WhenEmailDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await buyerRepository.GetAllAddressesByEmail(email, 1, 10);

            // Assert
            Assert.Empty(result);
        }
    }
}
