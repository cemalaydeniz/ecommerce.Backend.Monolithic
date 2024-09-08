using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.Repositories.Entities.Authentication;
using ecommerce.Test.Integration.Persistence.DbContexts;
using ecommerce.Test.Utility.Entities.Authentication;
using ecommerce.Test.Utility.Fixtures;

namespace ecommerce.Test.Integration.Persistence.Repositories.Entities.Authentication
{
    [Collection(nameof(AppDbContextCollection))]
    public class SellerUploadedFileRepositoryTest
    {
        private readonly SellerUploadedFileRepository sellerUploadedFileRepository;
        private readonly SellerRepository sellerRepository;

        public SellerUploadedFileRepositoryTest(AppDbContextFixture appDbContextFixture)
        {
            sellerUploadedFileRepository = new SellerUploadedFileRepository(appDbContextFixture.AppDbContext);
            sellerRepository = new SellerRepository(appDbContextFixture.AppDbContext);
        }

        [Fact]
        public async Task Delete_WhenFileExists_DeletesFile()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            SellerUploadedFile file = SellerUploadedFileGenerator.Generate();
            seller.UploadedFiles.Add(file);

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            sellerUploadedFileRepository.Delete(file);
            int result = await sellerUploadedFileRepository.SaveChangesAsync();

            // Assert
            var sellerFiles = await sellerRepository.GetUploadedFilesById(seller.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Empty(sellerFiles);
        }

        [Fact]
        public async Task DeleteById_WhenIdExists_DeletesFile()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            SellerUploadedFile file = SellerUploadedFileGenerator.Generate();
            seller.UploadedFiles.Add(file);

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            int result = await sellerUploadedFileRepository.DeleteById(file.Id);

            // Assert
            var sellerFiles = await sellerRepository.GetUploadedFilesById(seller.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Empty(sellerFiles);
        }

        [Fact]
        public async Task DeleteById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = new Guid();

            // Act
            int result = await sellerUploadedFileRepository.DeleteById(id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task DeleteMultiple_WhenFilesExist_DeletesAll()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            List<SellerUploadedFile> files = new List<SellerUploadedFile>()
            {
                SellerUploadedFileGenerator.Generate(),
                SellerUploadedFileGenerator.Generate(),
                SellerUploadedFileGenerator.Generate()
            };
            files.ForEach(f => seller.UploadedFiles.Add(f));

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            sellerUploadedFileRepository.DeleteMultiple(files);
            int result = await sellerUploadedFileRepository.SaveChangesAsync();

            // Assert
            var sellerFiles = await sellerRepository.GetUploadedFilesById(seller.Id);

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(sellerFiles);
        }

        [Fact]
        public async Task DeleteAllById_WhenIdsExist_DeletesAll()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            List<SellerUploadedFile> files = new List<SellerUploadedFile>()
            {
                SellerUploadedFileGenerator.Generate(),
                SellerUploadedFileGenerator.Generate(),
                SellerUploadedFileGenerator.Generate()
            };
            files.ForEach(f => seller.UploadedFiles.Add(f));

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            int result = await sellerUploadedFileRepository.DeleteAllById(files.Select(f => f.Id));

            // Assert
            var sellerFiles = await sellerRepository.GetUploadedFilesById(seller.Id);

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(sellerFiles);
        }

        [Fact]
        public async Task DeleteAllById_WhenSomeIdsExist_DeletesExistedOnes()
        {
            // Arrange
            Seller seller = SellerGenerator.Generate();
            List<SellerUploadedFile> files = new List<SellerUploadedFile>()
            {
                SellerUploadedFileGenerator.Generate(),
                SellerUploadedFileGenerator.Generate(),
                SellerUploadedFileGenerator.Generate()
            };
            files.ForEach(f => seller.UploadedFiles.Add(f));
            files.Add(SellerUploadedFileGenerator.Generate());
            files.Add(SellerUploadedFileGenerator.Generate());

            await sellerRepository.AddAsync(seller);
            await sellerRepository.SaveChangesAsync();

            // Act
            int result = await sellerUploadedFileRepository.DeleteAllById(files.Select(f => f.Id));

            // Assert
            var sellerFiles = await sellerRepository.GetUploadedFilesById(seller.Id);

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(sellerFiles);
        }

        [Fact]
        public async Task DeleteAllById_WhenIdsDoNotExist_DoesNothing()
        {
            // Arrange
            List<SellerUploadedFile> files = new List<SellerUploadedFile>()
            {
                SellerUploadedFileGenerator.Generate(),
                SellerUploadedFileGenerator.Generate(),
                SellerUploadedFileGenerator.Generate()
            };

            // Act
            int result = await sellerUploadedFileRepository.DeleteAllById(files.Select(f => f.Id));

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }
    }
}
