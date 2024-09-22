using ecommerce.Domain.Entities.Account;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.Repositories.Entities.Account;
using ecommerce.Persistence.Repositories.Entities.Authentication;
using ecommerce.Test.Integration.Persistence.DbContexts;
using ecommerce.Test.Utility;
using ecommerce.Test.Utility.Entities.Account;
using ecommerce.Test.Utility.Entities.Authentication;
using ecommerce.Test.Utility.Fixtures;

namespace ecommerce.Test.Integration.Persistence.Repositories.Entities.Account
{
    [Collection(nameof(AppDbContextCollection))]
    public class AddressRepositoryTest
    {
        private readonly AddressRepository addressRepository;
        private readonly BuyerRepository buyerRepository;

        public AddressRepositoryTest(AppDbContextFixture appDbContextFixture)
        {
            addressRepository = new AddressRepository(appDbContextFixture.AppDbContext);
            buyerRepository = new BuyerRepository(appDbContextFixture.AppDbContext);
        }

        [Fact]
        public async Task SaveChangesAsync_WhenAddressExistsAndIsUpdated_UpdatesAddress()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            Address address = AddressGenerator.Generate();
            buyer.Addresses.Add(address);

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            string newTitle = address.Title + StringGenerator.Generate();
            string newStreetLine1 = address.StreetLine1 + StringGenerator.Generate();
            string newStreetLine2 = address.StreetLine2 + StringGenerator.Generate();
            string newStateOrProvince = address.StateOrProvince + StringGenerator.Generate();
            string newZipCode = address.ZipCode + StringGenerator.Generate();
            string newCity = address.City + StringGenerator.Generate();
            string newCountry = address.Country + StringGenerator.Generate();

            address.Title = newTitle;
            address.StreetLine1 = newStreetLine1;
            address.StreetLine2 = newStreetLine2;
            address.StateOrProvince = newStateOrProvince;
            address.ZipCode = newZipCode;
            address.City = newCity;
            address.Country = newCountry;

            int result = await addressRepository.SaveChangesAsync();

            // Assert
            var updatedAddresses = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.True(updatedAddresses.Count() == 1, "There is none or more than one address related to the user. It should have been only one address");
            Assert.Equal(newTitle, updatedAddresses.ElementAt(0).Title);
            Assert.Equal(newStreetLine1, updatedAddresses.ElementAt(0).StreetLine1);
            Assert.Equal(newStreetLine2, updatedAddresses.ElementAt(0).StreetLine2);
            Assert.Equal(newStateOrProvince, updatedAddresses.ElementAt(0).StateOrProvince);
            Assert.Equal(newZipCode, updatedAddresses.ElementAt(0).ZipCode);
            Assert.Equal(newCity, updatedAddresses.ElementAt(0).City);
            Assert.Equal(newCountry, updatedAddresses.ElementAt(0).Country);
        }

        [Fact]
        public async Task UpdateById_WhenIdIsFound_UpdatesAddress()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            Address address = AddressGenerator.Generate();
            buyer.Addresses.Add(address);

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            string newTitle = address.Title + StringGenerator.Generate();
            string newStreetLine1 = address.StreetLine1 + StringGenerator.Generate();
            string newStreetLine2 = address.StreetLine2 + StringGenerator.Generate();
            string newStateOrProvince = address.StateOrProvince + StringGenerator.Generate();
            string newZipCode = address.ZipCode + StringGenerator.Generate();
            string newCity = address.City + StringGenerator.Generate();
            string newCountry = address.Country + StringGenerator.Generate();

            int result = await addressRepository.UpdateById(address.Id, _ => _
                .SetProperty(a => a.Title, newTitle)
                .SetProperty(a => a.StreetLine1, newStreetLine1)
                .SetProperty(a => a.StreetLine2, newStreetLine2)
                .SetProperty(a => a.StateOrProvince, newStateOrProvince)
                .SetProperty(a => a.ZipCode, newZipCode)
                .SetProperty(a => a.City, newCity)
                .SetProperty(a => a.Country, newCountry));

            // Assert
            var updatedAddresses = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.NotNull(updatedAddresses);
            Assert.True(updatedAddresses.Count() == 1, "There is none or more than one address related to the user. It should have been only one address");
            Assert.Equal(newTitle, updatedAddresses.ElementAt(0).Title);
            Assert.Equal(newStreetLine1, updatedAddresses.ElementAt(0).StreetLine1);
            Assert.Equal(newStreetLine2, updatedAddresses.ElementAt(0).StreetLine2);
            Assert.Equal(newStateOrProvince, updatedAddresses.ElementAt(0).StateOrProvince);
            Assert.Equal(newZipCode, updatedAddresses.ElementAt(0).ZipCode);
            Assert.Equal(newCity, updatedAddresses.ElementAt(0).City);
            Assert.Equal(newCountry, updatedAddresses.ElementAt(0).Country);
        }

        [Fact]
        public async Task UpdateById_WhenIdIsNotFound_DoesNothing()
        {
            // Arrange
            Guid id = new Guid();
            string title = StringGenerator.Generate();

            // Act
            int result = await addressRepository.UpdateById(id, _ => _
                .SetProperty(a => a.Title, title));

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task UpdateById_WhenPropertyAndValuesAreNull_DoesNothing()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            Address address = AddressGenerator.Generate();
            buyer.Addresses.Add(address);

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            int result = await addressRepository.UpdateById(address.Id, null!);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task Delete_WhenAddressExists_DeleteAddress()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            Address address = AddressGenerator.Generate();
            buyer.Addresses.Add(address);

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            addressRepository.Delete(address);
            int result = await addressRepository.SaveChangesAsync();

            // Assert
            var userAddresses = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Empty(userAddresses);
        }
        
        [Fact]
        public async Task DeleteById_WhenIdIsFound_DeleteAddress()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            Address address = AddressGenerator.Generate();
            buyer.Addresses.Add(address);

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            int result = await addressRepository.DeleteById(address.Id);

            // Assert
            var userAddresses = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Empty(userAddresses);
        }

        [Fact]
        public async Task DeleteById_WhenIdIsNotFound_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            int result = await addressRepository.DeleteById(id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task DeleteMultiple_WhenAddressesExist_DeletesAll()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            List<Address> addresses = new List<Address>()
            {
                AddressGenerator.Generate(),
                AddressGenerator.Generate(),
                AddressGenerator.Generate()
            };
            addresses.ForEach(a => buyer.Addresses.Add(a));

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            addressRepository.DeleteMultiple(addresses);
            int result = await addressRepository.SaveChangesAsync();

            // Assert
            var userAddresses = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(userAddresses);
        }

        [Fact]
        public async Task DeleteAllById_WhenIdsExist_DeletesAll()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            List<Address> addresses = new List<Address>()
            {
                AddressGenerator.Generate(),
                AddressGenerator.Generate(),
                AddressGenerator.Generate()
            };
            addresses.ForEach(a => buyer.Addresses.Add(a));

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            int result = await addressRepository.DeleteAllById(addresses.Select(a => a.Id));

            // Assert
            var userAddresses = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(userAddresses);
        }

        [Fact]
        public async Task DeleteAllById_WhenSomeIdsExist_DeletesExistedOnes()
        {
            // Arrange
            Buyer buyer = BuyerGenerator.Generate();
            List<Address> addresses = new List<Address>()
            {
                AddressGenerator.Generate(),
                AddressGenerator.Generate(),
                AddressGenerator.Generate()
            };
            addresses.ForEach(a => buyer.Addresses.Add(a));
            addresses.Add(AddressGenerator.Generate());
            addresses.Add(AddressGenerator.Generate());

            await buyerRepository.AddAsync(buyer);
            await buyerRepository.SaveChangesAsync();

            // Act
            int result = await addressRepository.DeleteAllById(addresses.Select(a => a.Id));

            // Assert
            var userAddresses = await buyerRepository.GetAllAddressesById(buyer.Id, 1, 10);

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(userAddresses);
        }

        [Fact]
        public async Task DeleteAllById_WhenIdsDoNotExist_DoesNothing()
        {
            // Arrange
            List<Address> addresses = new List<Address>()
            {
                AddressGenerator.Generate(),
                AddressGenerator.Generate(),
                AddressGenerator.Generate()
            };

            // Act
            int result = await addressRepository.DeleteAllById(addresses.Select(a => a.Id));

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }
    }
}
