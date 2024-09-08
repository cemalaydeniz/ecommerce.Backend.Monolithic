using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Common.ValueObjects;
using ecommerce.Persistence.Repositories.Entities.Authentication;
using ecommerce.Test.Integration.Persistence.DbContexts;
using ecommerce.Test.Utility;
using ecommerce.Test.Utility.Entities.Authentication;
using ecommerce.Test.Utility.Entities.Common.ValueObjects;
using ecommerce.Test.Utility.Fixtures;
using System.Net;

namespace ecommerce.Test.Integration.Persistence.Repositories.Entities.Authentication
{
    [Collection(nameof(AppDbContextCollection))]
    public class UserLoginRepositoryTest
    {
        private readonly UserLoginRepository userLoginRepository;
        private readonly UserRepository userRepository;

        public UserLoginRepositoryTest(AppDbContextFixture appDbContextFixture)
        {
            userLoginRepository = new UserLoginRepository(appDbContextFixture.AppDbContext);
            userRepository = new UserRepository(appDbContextFixture.AppDbContext);
        }

        [Fact]
        public async Task SaveChangesAsync_WhenLoginExistsAndIsUpdated_UpdatesLogin()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserLogin login = UserLoginGenerator.Generate();
            user.UserLogins.Add(login);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            Token newToken = TokenGenerator.Generate();
            IPAddress newIp = IpAddressGenerator.Generate();
            string newDeviceInfo = StringGenerator.Generate();

            login.RefreshToken = newToken;
            login.IpAddress = newIp;
            login.DeviceInformation = newDeviceInfo;

            int result = await userLoginRepository.SaveChangesAsync();

            // Assert
            var updatedLogin = await userLoginRepository.GetLoginById(login.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.NotNull(updatedLogin);
            Assert.Equal(newToken, updatedLogin.RefreshToken);
            Assert.Equal(newIp, updatedLogin.IpAddress);
            Assert.Equal(newDeviceInfo, updatedLogin.DeviceInformation);
        }

        [Fact]
        public async Task AddAsync_WhenLoginIsValid_AddsNewOne()
        {
            // Arrange
            User user = UserGenerator.Generate();
            
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            UserLogin login = UserLoginGenerator.Generate();
            login.User = user;

            await userLoginRepository.AddAsync(login);
            int result = await userLoginRepository.SaveChangesAsync();

            // Assert
            var addedLogin = await userLoginRepository.GetLoginById(login.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Equal(login, addedLogin);
        }

        [Fact]
        public async Task UpdateById_WhenIdExists_UpdatesLogin()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserLogin login = UserLoginGenerator.Generate();
            user.UserLogins.Add(login);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            Token newToken = TokenGenerator.Generate();
            IPAddress newIp = IpAddressGenerator.Generate();
            string newDeviceInfo = StringGenerator.Generate();

            int result = await userLoginRepository.UpdateById(login.Id, _ => _
                .SetProperty(l => l.RefreshToken.ValueEncypted, newToken.ValueEncypted)
                .SetProperty(l => l.RefreshToken.ExpirationDate, newToken.ExpirationDate)
                .SetProperty(l => l.IpAddress, newIp)
                .SetProperty(l => l.DeviceInformation, newDeviceInfo));

            // Assert
            var updatedLogin = await userLoginRepository.GetLoginById(login.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.NotNull(updatedLogin);
            Assert.Equal(newToken, updatedLogin.RefreshToken);
            Assert.Equal(newIp, updatedLogin.IpAddress);
            Assert.Equal(newDeviceInfo, updatedLogin.DeviceInformation);
        }

        [Fact]
        public async Task UpdateById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string deviceInfo = StringGenerator.Generate();

            // Act
            int result = await userLoginRepository.UpdateById(id, _ => _
                .SetProperty(l => l.DeviceInformation, deviceInfo));

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task Delete_WhenLoginExists_DeletesLogin()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserLogin login = UserLoginGenerator.Generate();
            user.UserLogins.Add(login);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            userLoginRepository.Delete(login);
            int result = await userLoginRepository.SaveChangesAsync();

            // Assert
            var deletedLogin = await userLoginRepository.GetLoginById(login.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Null(deletedLogin);
        }

        [Fact]
        public async Task DeleteById_WhenIdExists_DeletesLogin()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserLogin login = UserLoginGenerator.Generate();
            user.UserLogins.Add(login);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            int result = await userLoginRepository.DeleteById(login.Id);

            // Assert
            var deletedLogin = await userLoginRepository.GetLoginById(login.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Null(deletedLogin);
        }

        [Fact]
        public async Task DeleteById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            int result = await userLoginRepository.DeleteById(id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task DeleteAllByUserId_WhenUserIdExistAndThereAreLogins_DeletesLogins()
        {
            // Arrange
            User user = UserGenerator.Generate();
            user.UserLogins.Add(UserLoginGenerator.Generate());
            user.UserLogins.Add(UserLoginGenerator.Generate());
            user.UserLogins.Add(UserLoginGenerator.Generate());

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            int result = await userLoginRepository.DeleteAllByUserId(user.Id);

            // Assert
            var userLogins = await userLoginRepository.GetLoginsByUserId(user.Id);

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(userLogins);
        }

        [Fact]
        public async Task DeleteAllByUserId_WhenUserIdExistButThereAreNoLogins_DoesNothing()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            int result = await userLoginRepository.DeleteAllByUserId(user.Id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task DeleteAllByUserId_WhenUserIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            int result = await userLoginRepository.DeleteAllByUserId(id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task GetLoginById_WhenIdExists_ReturnsLogin()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserLogin login = UserLoginGenerator.Generate();
            user.UserLogins.Add(login);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userLoginRepository.GetLoginById(login.Id);

            // Assert
            Assert.Equal(login, result);
        }

        [Fact]
        public async Task GetLoginById_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userLoginRepository.GetLoginById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetLoginByIdSelect_WhenIdExists_ReturnsLogin()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserLogin login = UserLoginGenerator.Generate();
            user.UserLogins.Add(login);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userLoginRepository.GetLoginById(login.Id, l => new
            {
                l.Id,
                l.IpAddress
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(login.Id, result.Id);
        }

        [Fact]
        public async Task GetLoginByIdSelect_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userLoginRepository.GetLoginById(id, l => new
            {
                l.Id,
                l.IpAddress
            });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetLoginsByUserId_WhenUserIdExistsAndThereAreLogins_ReturnsLogins()
        {
            // Arrange
            User user = UserGenerator.Generate();
            user.UserLogins.Add(UserLoginGenerator.Generate());
            user.UserLogins.Add(UserLoginGenerator.Generate());
            user.UserLogins.Add(UserLoginGenerator.Generate());

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userLoginRepository.GetLoginsByUserId(user.Id);

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(user.UserLogins.OrderBy(ul => ul.Id)
                .SequenceEqual(result.OrderBy(ul => ul.Id)));
        }

        [Fact]
        public async Task GetLoginsByUserId_WhenUserIdExistsButThereAreNoLogins_ReturnsEmptyList()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userLoginRepository.GetLoginsByUserId(user.Id);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetLoginsByUserId_WhenUserIdDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userLoginRepository.GetLoginsByUserId(id);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetLoginsByUserIdSelect_WhenUserIdExistsAndThereAreLogins_ReturnsLogins()
        {
            // Arrange
            User user = UserGenerator.Generate();
            user.UserLogins.Add(UserLoginGenerator.Generate());
            user.UserLogins.Add(UserLoginGenerator.Generate());
            user.UserLogins.Add(UserLoginGenerator.Generate());

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userLoginRepository.GetLoginsByUserId(user.Id, l => new
            {
                l.Id,
                l.IpAddress
            });

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(user.UserLogins.Select(ul => ul.Id).OrderBy(id => id)
                .SequenceEqual(result.Select(r => r.Id).OrderBy(id => id)));
        }

        [Fact]
        public async Task GetLoginsByUserIdSelect_WhenUserIdExistsButThereAreNoLogins_ReturnsEmptyList()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userLoginRepository.GetLoginsByUserId(user.Id, l => new
            {
                l.Id,
                l.IpAddress
            });

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetLoginsByUserIdSelect_WhenUserIdDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userLoginRepository.GetLoginsByUserId(id, l => new
            {
                l.Id,
                l.IpAddress
            });

            // Assert
            Assert.Empty(result);
        }
    }
}
