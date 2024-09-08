using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Persistence.Repositories.Entities.Authentication;
using ecommerce.Test.Integration.Persistence.DbContexts;
using ecommerce.Test.Utility.Entities.Authentication;
using ecommerce.Test.Utility.Fixtures;

namespace ecommerce.Test.Integration.Persistence.Repositories.Entities.Authentication
{
    [Collection(nameof(AppDbContextCollection))]
    public class UserTokenRepositoryTest
    {
        private readonly UserTokenRepository userTokenRepository;
        private readonly UserRepository userRepository;

        public UserTokenRepositoryTest(AppDbContextFixture appDbContextFixture)
        {
            userTokenRepository = new UserTokenRepository(appDbContextFixture.AppDbContext);
            userRepository = new UserRepository(appDbContextFixture.AppDbContext);
        }

        [Fact]
        public async Task AddAsync_WhenTokenIsValid_AddsNewOne()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            UserToken token = UserTokenGenerator.Generate();
            token.User = user;

            await userTokenRepository.AddAsync(token);
            int result = await userTokenRepository.SaveChangesAsync();

            // Assert
            var addedToken = await userTokenRepository.GetTokenById(token.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Equal(token, addedToken);
        }

        [Fact]
        public async Task Delete_WhenTokenExists_DeletesToken()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserToken token = UserTokenGenerator.Generate();
            user.UserTokens.Add(token);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            userTokenRepository.Delete(token);
            int result = await userTokenRepository.SaveChangesAsync();

            // Assert
            var deletedToken = await userTokenRepository.GetTokenById(token.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Null(deletedToken);
        }

        [Fact]
        public async Task DeleteById_WhenIdExists_DeletesToken()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserToken token = UserTokenGenerator.Generate();
            user.UserTokens.Add(token);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            int result = await userTokenRepository.DeleteById(token.Id);

            // Assert
            var deletedToken = await userTokenRepository.GetTokenById(token.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Null(deletedToken);
        }

        [Fact]
        public async Task DeleteById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            int result = await userTokenRepository.DeleteById(id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task DeleteAllByUserId_WhenUserIdExistsAndThereAreTokens_DeletesTokens()
        {
            // Arrange
            User user = UserGenerator.Generate();
            user.UserTokens.Add(UserTokenGenerator.Generate());
            user.UserTokens.Add(UserTokenGenerator.Generate());
            user.UserTokens.Add(UserTokenGenerator.Generate());

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            int result = await userTokenRepository.DeleteAllByUserId(user.Id);

            // Assert
            var userTokens = await userTokenRepository.GetTokensByUserId(user.Id);
            
            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(userTokens);
        }

        [Fact]
        public async Task DeleteAllByUserId_WhenUserIdExistsButThereAreNoTokens_DoesNothing()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            int result = await userTokenRepository.DeleteAllByUserId(user.Id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task DeleteAllByUserId_WhenUserIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            int result = await userTokenRepository.DeleteAllByUserId(id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task GetTokenById_WhenIdExists_ReturnsToken()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserToken token = UserTokenGenerator.Generate();
            user.UserTokens.Add(token);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userTokenRepository.GetTokenById(token.Id);

            // Assert
            Assert.Equal(token, result);
        }

        [Fact]
        public async Task GetTokenById_WhenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userTokenRepository.GetTokenById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTokensByUserId_WhenUserIdExistsAndThereAreTokens_ReturnsTokens()
        {
            // Arrange
            User user = UserGenerator.Generate();
            user.UserTokens.Add(UserTokenGenerator.Generate());
            user.UserTokens.Add(UserTokenGenerator.Generate());
            user.UserTokens.Add(UserTokenGenerator.Generate());

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userTokenRepository.GetTokensByUserId(user.Id);

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(user.UserTokens.OrderBy(ut => ut.Id)
                .SequenceEqual(result.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetTokensByUserId_WhenUserIdExistsButThereAreNoTokens_ReturnsEmptyList()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userTokenRepository.GetTokensByUserId(user.Id);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTokensByUserId_WhenUserIdDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userTokenRepository.GetTokensByUserId(id);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTokensByUserIdAndTokenPurpose_WhenUserIdExistsAndThereAreTokens_ReturnsTokens()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserToken token = UserTokenGenerator.Generate();
            token.Purpose = ETokenPurpose.ResetPassword;
            user.UserTokens.Add(token);
            user.UserTokens.Add(UserTokenGenerator.Generate());
            user.UserTokens.Add(UserTokenGenerator.Generate());

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userTokenRepository.GetTokensByUserIdAndTokenPurpose(user.Id, ETokenPurpose.ConfirmEmail);
        
            // Assert
            Assert.Equal(2, result.Count());
            Assert.True(user.UserTokens.Where(ut => ut.Purpose == ETokenPurpose.ConfirmEmail).OrderBy(ut => ut.Id)
                .SequenceEqual(result.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetTokensByUserIdAndTokenPurpose_WhenUserIdExistsButThereAreNoTokens_ReturnsEmptyList()
        {
            // Arrange
            User user = UserGenerator.Generate();

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userTokenRepository.GetTokensByUserIdAndTokenPurpose(user.Id, ETokenPurpose.ConfirmEmail);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTokensByUserIdAndTokenPurpose_WhenUserIdDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userTokenRepository.GetTokensByUserIdAndTokenPurpose(id, ETokenPurpose.ConfirmEmail);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllExpiredTokensByUserId_WhenUserIdExistsAndThereAreExpiredTokens_ReturnsTokens()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserToken token = UserTokenGenerator.Generate();
            token.Token.ExpirationDate = DateTime.UtcNow.AddDays(-1);
            user.UserTokens.Add(token);
            token = UserTokenGenerator.Generate();
            token.Token.ExpirationDate = DateTime.UtcNow.AddDays(-1);
            user.UserTokens.Add(token);
            token = UserTokenGenerator.Generate();
            token.Token.ExpirationDate = DateTime.UtcNow.AddDays(-1);
            user.UserTokens.Add(token);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var result = await userTokenRepository.GetAllExpiredTokensByUserId(user.Id);

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(user.UserTokens.OrderBy(ut => ut.Id)
                .SequenceEqual(result.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetAllExpiredTokensByUserId_WhenUserIdExistsAndThereAreSomeExpiredTokens_ReturnsTokens()
        {
            // Arrange
            User user = UserGenerator.Generate();
            UserToken token = UserTokenGenerator.Generate();
            token.Token.ExpirationDate = DateTime.UtcNow.AddDays(-1);
            user.UserTokens.Add(token);
            token = UserTokenGenerator.Generate();
            token.Token.ExpirationDate = DateTime.UtcNow.AddDays(-1);
            user.UserTokens.Add(token);
            user.UserTokens.Add(UserTokenGenerator.Generate());
        
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
        
            // Act
            var result = await userTokenRepository.GetAllExpiredTokensByUserId(user.Id);
        
            // Assert
            Assert.Equal(2, result.Count());
            Assert.True(user.UserTokens.Where(ut => ut.Token.ExpirationDate < DateTime.UtcNow).OrderBy(ut => ut.Id)
                .SequenceEqual(result.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetAllExpiredTokensByUserId_WhenUserIdExistsButThereAreNoExpiredTokens_ReturnsEmptyList()
        {
            // Arrange
            User user = UserGenerator.Generate();
            user.UserTokens.Add(UserTokenGenerator.Generate());
            user.UserTokens.Add(UserTokenGenerator.Generate());
            user.UserTokens.Add(UserTokenGenerator.Generate());
        
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
        
            // Act
            var result = await userTokenRepository.GetAllExpiredTokensByUserId(user.Id);
        
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllExpiredTokensByUserId_WhenUserIdExistsButThereAreNoTokens_ReturnsEmptyList()
        {
            // Arrange
            User user = UserGenerator.Generate();
        
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
        
            // Act
            var result = await userTokenRepository.GetAllExpiredTokensByUserId(user.Id);
        
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllExpiredTokensByUserId_WhenUserIdDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var result = await userTokenRepository.GetAllExpiredTokensByUserId(id);

            // Assert
            Assert.Empty(result);
        }
    }
}
