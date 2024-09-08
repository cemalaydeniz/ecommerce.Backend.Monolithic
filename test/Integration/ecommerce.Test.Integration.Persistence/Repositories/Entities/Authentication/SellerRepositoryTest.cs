using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.Repositories.Entities.Authentication;
using ecommerce.Test.Integration.Persistence.DbContexts;
using ecommerce.Test.Utility.Entities.Authentication;
using ecommerce.Test.Utility;
using ecommerce.Test.Utility.Fixtures;
using ecommerce.Domain.Entities.Authentication.ValueObjects;
using ecommerce.Test.Utility.Entities.Authentication.ValueObjects;
using System;
using Microsoft.EntityFrameworkCore;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Test.Utility.Entities.Account;

namespace ecommerce.Test.Integration.Persistence.Repositories.Entities.Authentication
{
    [Collection(nameof(AppDbContextCollection))]
    public class SellerRepositoryTest
    {
        private readonly SellerRepository sellerRepository;

        public SellerRepositoryTest(AppDbContextFixture appDbContextFixture)
        {
            sellerRepository = new SellerRepository(appDbContextFixture.AppDbContext);
        }

        [Fact]
        public async Task SaveChangesAsync_WhenSellerExistsAndIsUpdated_UpdatesSeller()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            string newEmail = StringGenerator.Generate() + "@example.com";
            string newPhoneNumber = StringGenerator.Generate();
            string newBusinessName = StringGenerator.Generate();
            string newContactName = StringGenerator.Generate();
            string newContactEmail = StringGenerator.Generate();
            string newContantPhoneNumber = StringGenerator.Generate();
            CreditCardInformation newCreditCardInfo = CreditCardInformationGenerator.Generate();

            seller.Email = newEmail;
            seller.PhoneNumber = newPhoneNumber;
            seller.BusinessName = newBusinessName;
            seller.ContactName = newContactName;
            seller.ContactEmail = newContactEmail;
            seller.ContactPhoneNumber = newContantPhoneNumber;
            seller.CreditCardInformation = newCreditCardInfo;
            seller.AccountStatus = EAccountStatus.Active;

            int result = await sellerRepository.SaveChangesAsync();

            // Assert
            var updatedSeller = await sellerRepository.GetUserById(seller.Id);

            Assert.True(result == 2, $"{result} row(s) are affected. It should have been 2");
            Assert.NotNull(updatedSeller);
            Assert.Equal(newEmail, updatedSeller.Email);
            Assert.Equal(newPhoneNumber, updatedSeller.PhoneNumber);
            Assert.Equal(newBusinessName, updatedSeller.BusinessName);
            Assert.Equal(newContactName, updatedSeller.ContactName);
            Assert.Equal(newContactEmail, updatedSeller.ContactEmail);
            Assert.Equal(newContantPhoneNumber, updatedSeller.ContactPhoneNumber);
            Assert.Equal(newCreditCardInfo, updatedSeller.CreditCardInformation);
            Assert.Equal(EAccountStatus.Active, updatedSeller.AccountStatus);
        }

        [Fact]
        public async Task AddAsync_WhenSellerValid_AddsNewOne()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            // Act
            await sellerRepository.AddAsync(seller);
            int result = await sellerRepository.SaveChangesAsync();

            // Assert
            var addedSeller = await sellerRepository.GetUserById(seller.Id);

            Assert.True(result == 2, $"{result} row(s) are affected. It should have been 2");
            Assert.Equal(seller, addedSeller);
        }

        [Fact]
        public async Task UpdateById_WhenIdExists_UpdatesSeller()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            string newBusinessName = StringGenerator.Generate();
            string newContactName = StringGenerator.Generate();
            string newContactEmail = StringGenerator.Generate();
            string newContantPhoneNumber = StringGenerator.Generate();
            CreditCardInformation newCreditCardInfo = CreditCardInformationGenerator.Generate();

            int result = await sellerRepository.UpdateById(seller.Id, _ => _
                .SetProperty(s => s.BusinessName, newBusinessName)
                .SetProperty(s => s.ContactName, newContactName)
                .SetProperty(s => s.ContactEmail, newContactEmail)
                .SetProperty(s => s.ContactPhoneNumber, newContantPhoneNumber)
                .SetProperty(s => s.CreditCardInformation.CardHolderNameEncrypted, newCreditCardInfo.CardHolderNameEncrypted)
                .SetProperty(s => s.CreditCardInformation.CardNumberEncrypted, newCreditCardInfo.CardNumberEncrypted)
                .SetProperty(s => s.CreditCardInformation.ExpirationDateEncrypted, newCreditCardInfo.ExpirationDateEncrypted)
                .SetProperty(s => s.CreditCardInformation.CvvCodeEncrypted, newCreditCardInfo.CvvCodeEncrypted)
                .SetProperty(s => s.AccountStatus, EAccountStatus.Active));

            // Assert
            var updatedSeller = await sellerRepository.GetUserById(seller.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.NotNull(updatedSeller);
            Assert.Equal(newBusinessName, updatedSeller.BusinessName);
            Assert.Equal(newContactName, updatedSeller.ContactName);
            Assert.Equal(newContactEmail, updatedSeller.ContactEmail);
            Assert.Equal(newContantPhoneNumber, updatedSeller.ContactPhoneNumber);
            Assert.Equal(newCreditCardInfo, updatedSeller.CreditCardInformation);
            Assert.Equal(EAccountStatus.Active, updatedSeller.AccountStatus);
        }

        [Fact]
        public async Task UpdateById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            string newBusinessName = StringGenerator.Generate();

            int result = await sellerRepository.UpdateById(id, _ => _
                .SetProperty(b => b.BusinessName, newBusinessName));

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task SoftDelete_WhenSellerExists_SoftDeletesSeller()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            sellerRepository.SoftDelete(seller);
            int result = await sellerRepository.SaveChangesAsync();

            // Assert
            var softDeletedSeller = await sellerRepository.Table.IgnoreQueryFilters()
                .Where(b => b.Id.Equals(seller.Id))
                .FirstOrDefaultAsync();

            Assert.True(result == 2, $"{result} row(s) are affected. It should have been 2");
            Assert.Equal(seller, softDeletedSeller);
            Assert.True(softDeletedSeller!.IsDeleted);
            Assert.NotNull(softDeletedSeller.SoftDeletedDate);
            Assert.Null(softDeletedSeller.Email);
            Assert.Null(softDeletedSeller.PasswordHash);
            Assert.Null(softDeletedSeller.PhoneNumber);
            Assert.False(softDeletedSeller.IsEmailConfirmed);
            Assert.False(softDeletedSeller.IsPhoneNumberConfirmed);
            Assert.False(softDeletedSeller.IsTwoFactorEnabled);
            Assert.Equal(0, softDeletedSeller.AccessFailCount);
            Assert.Null(softDeletedSeller.LockoutEndDate);
            Assert.False(softDeletedSeller.IsLockoutEnabled);
            Assert.Null(softDeletedSeller.BusinessName);
            Assert.Null(softDeletedSeller.ContactName);
            Assert.Null(softDeletedSeller.ContactEmail);
            Assert.Null(softDeletedSeller.ContactPhoneNumber);
            Assert.Null(softDeletedSeller.TinNumber);
            Assert.Null(softDeletedSeller.VatNumber);
            Assert.Null(softDeletedSeller.CreditCardInformation.CardHolderNameEncrypted);
            Assert.Null(softDeletedSeller.CreditCardInformation.CardNumberEncrypted);
            Assert.Null(softDeletedSeller.CreditCardInformation.ExpirationDateEncrypted);
            Assert.Null(softDeletedSeller.CreditCardInformation.CvvCodeEncrypted);
            Assert.Equal(EAccountStatus.Suspended, softDeletedSeller.AccountStatus);
        }

        [Fact]
        public async Task SoftDeleteById_WhenIdExists_SoftDeletesSeller()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            await sellerRepository.SoftDeleteById(seller.Id);
            int result = await sellerRepository.SaveChangesAsync();

            // Assert
            var softDeletedSeller = await sellerRepository.Table.IgnoreQueryFilters()
                .Where(b => b.Id.Equals(seller.Id))
                .FirstOrDefaultAsync();

            Assert.True(result == 2, $"{result} row(s) are affected. It should have been 2");
            Assert.Equal(seller, softDeletedSeller);
            Assert.True(softDeletedSeller!.IsDeleted);
            Assert.NotNull(softDeletedSeller.SoftDeletedDate);
            Assert.Null(softDeletedSeller.Email);
            Assert.Null(softDeletedSeller.PasswordHash);
            Assert.Null(softDeletedSeller.PhoneNumber);
            Assert.False(softDeletedSeller.IsEmailConfirmed);
            Assert.False(softDeletedSeller.IsPhoneNumberConfirmed);
            Assert.False(softDeletedSeller.IsTwoFactorEnabled);
            Assert.Equal(0, softDeletedSeller.AccessFailCount);
            Assert.Null(softDeletedSeller.LockoutEndDate);
            Assert.False(softDeletedSeller.IsLockoutEnabled);
            Assert.Null(softDeletedSeller.BusinessName);
            Assert.Null(softDeletedSeller.ContactName);
            Assert.Null(softDeletedSeller.ContactEmail);
            Assert.Null(softDeletedSeller.ContactPhoneNumber);
            Assert.Null(softDeletedSeller.TinNumber);
            Assert.Null(softDeletedSeller.VatNumber);
            Assert.Null(softDeletedSeller.CreditCardInformation.CardHolderNameEncrypted);
            Assert.Null(softDeletedSeller.CreditCardInformation.CardNumberEncrypted);
            Assert.Null(softDeletedSeller.CreditCardInformation.ExpirationDateEncrypted);
            Assert.Null(softDeletedSeller.CreditCardInformation.CvvCodeEncrypted);
            Assert.Equal(EAccountStatus.Suspended, softDeletedSeller.AccountStatus);
        }

        [Fact]
        public async Task SoftDeleteById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            await sellerRepository.SoftDeleteById(id);
            int result = await sellerRepository.SaveChangesAsync();

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task GetUserById_WhenIdExists_ReturnsSeller()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetUserById(seller.Id);

            // Assert
            Assert.Equal(seller, result);
        }

        [Fact]
        public async Task GetUserById_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await sellerRepository.GetUserById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByIdSelect_WhenIdExists_ReturnsSeller()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetUserById(seller.Id, s => new
            {
                s.Id,
                s.BusinessName
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(seller.Id, result.Id);
            Assert.Equal(seller.BusinessName, result.BusinessName);
        }

        [Fact]
        public async Task GetUserByIdSelect_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await sellerRepository.GetUserById(id, s => new
            {
                s.Id,
                s.BusinessName
            });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByEmail_WhenEmailExists_ReturnsSeller()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetUserByEmail(seller.Email!);

            // Assert
            Assert.Equal(seller, result);
        }

        [Fact]
        public async Task GetUserByEmail_WhenEmailDoesNotExist_ReturnsNull()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await sellerRepository.GetUserByEmail(email);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByEmailSelect_WhenEmailExists_ReturnsSeller()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetUserByEmail(seller.Email!, s => new
            {
                s.Id,
                s.BusinessName
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(seller.Id, result.Id);
            Assert.Equal(seller.BusinessName, result.BusinessName);
        }

        [Fact]
        public async Task GetUserByEmailSelect_WhenEmailDoesNotExist_ReturnsNull()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await sellerRepository.GetUserByEmail(email, s => new
            {
                s.Id,
                s.BusinessName
            });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBusinessAddressById_WhenIdExistsAndThereIsBusinessAddress_ReturnsAddress()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            seller.BusinessAddress = AddressGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetBusinessAddressById(seller.Id);

            // Assert
            Assert.Equal(seller.BusinessAddress, result);
        }

        [Fact]
        public async Task GetBusinessAddressById_WhenIdExistsButThereIsNoBusinessAddress_ReturnsNull()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetBusinessAddressById(seller.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBusinessAddressById_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await sellerRepository.GetBusinessAddressById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBusinessAddressByEmail_WhenEmailExistsAndThereIsBusinessAddress_ReturnsAddress()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            seller.BusinessAddress = AddressGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetBusinessAddressByEmail(seller.Email!);

            // Assert
            Assert.Equal(seller.BusinessAddress, result);
        }

        [Fact]
        public async Task GetBusinessAddressByEmail_WhenEmailExistsButThereIsNoBusinessAddress_ReturnsNull()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetBusinessAddressByEmail(seller.Email!);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBusinessAddressByEmail_WhenEmailDoesNotExist_ReturnsNull()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await sellerRepository.GetBusinessAddressByEmail(email);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBillingAddressById_WhenIdExistsAndThereIsBillingAddress_ReturnsAddress()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            seller.BillingAddress = AddressGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetBillingAddressById(seller.Id);

            // Assert
            Assert.Equal(seller.BillingAddress, result);
        }

        [Fact]
        public async Task GetBillingAddressById_WhenIdExistsButThereIsNoBillingAddress_ReturnsNull()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetBillingAddressById(seller.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBillingAddressById_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await sellerRepository.GetBillingAddressById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBillingAddressByEmail_WhenEmailExistsAndThereIsBillingAddress_ReturnsAddress()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            seller.BillingAddress = AddressGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetBillingAddressByEmail(seller.Email!);

            // Assert
            Assert.Equal(seller.BillingAddress, result);
        }

        [Fact]
        public async Task GetBillingAddressByEmail_WhenEmailExistsButThereIsNoBillingAddress_ReturnsNull()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetBillingAddressByEmail(seller.Email!);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBillingAddressByEmail_WhenEmailDoesNotExist_ReturnsNull()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await sellerRepository.GetBillingAddressByEmail(email);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUploadedFilesById_WhenIdExistsAndThereAreFiles_ReturnsFiles()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            seller.UploadedFiles.Add(SellerUploadedFileGenerator.Generate());
            seller.UploadedFiles.Add(SellerUploadedFileGenerator.Generate());
            seller.UploadedFiles.Add(SellerUploadedFileGenerator.Generate());

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetUploadedFilesById(seller.Id);

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(seller.UploadedFiles.OrderBy(f => f.Id)
                .SequenceEqual(result.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetUploadedFilesById_WhenIdExistsButThereAreNoFiles_ReturnsEmptyList()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetUploadedFilesById(seller.Id);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUploadedFilesById_WhenIdDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await sellerRepository.GetUploadedFilesById(id);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUploadedFilesByEmail_WhenEmailExistsAndThereAreFiles_ReturnsFiles()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            seller.UploadedFiles.Add(SellerUploadedFileGenerator.Generate());
            seller.UploadedFiles.Add(SellerUploadedFileGenerator.Generate());
            seller.UploadedFiles.Add(SellerUploadedFileGenerator.Generate());

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetUploadedFilesByEmail(seller.Email!);

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(seller.UploadedFiles.OrderBy(f => f.Id)
                .SequenceEqual(result.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetUploadedFilesByEmail_WhenEmailExistsButThereAreNoFiles_ReturnsEmptyList()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            var result = await sellerRepository.GetUploadedFilesByEmail(seller.Email!);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUploadedFilesByEmail_WhenEmailDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            string email = StringGenerator.Generate();

            // Act
            var result = await sellerRepository.GetUploadedFilesByEmail(email);

            // Assert
            Assert.Empty(result);
        }
    }
}
