using ecommerce.Domain.Entities.Common;
using ecommerce.Persistence.Repositories.Entities.Common;
using ecommerce.Test.Integration.Persistence.DbContexts;
using ecommerce.Test.Utility.Entities.Common;
using ecommerce.Test.Utility.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Test.Integration.Persistence.Repositories.Entities.Common
{
    [Collection(nameof(AppDbContextCollection))]
    public class UploadedFileRepositoryTest
    {
        private readonly UploadedFileRepository uploadedFileRepository;

        public UploadedFileRepositoryTest(AppDbContextFixture appDbContextFixture)
        {
            uploadedFileRepository = new UploadedFileRepository(appDbContextFixture.AppDbContext);
        }

        [Fact]
        public async Task Delete_WhenFileExists_DeletesFile()
        {
            // Arrange
            UploadedFile file = UploadedFileGenerator.Generate();

            await uploadedFileRepository.Table.AddAsync(file);
            await uploadedFileRepository.SaveChangesAsync();

            // Act
            uploadedFileRepository.Delete(file);
            int result = await uploadedFileRepository.SaveChangesAsync();

            // Assert
            var deletedFile = await uploadedFileRepository.Table.FindAsync(file.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Null(deletedFile);
        }

        [Fact]
        public async Task DeleteById_WhenIdExists_DeletesFile()
        {
            // Arrange
            UploadedFile file = UploadedFileGenerator.Generate();

            await uploadedFileRepository.Table.AddAsync(file);
            await uploadedFileRepository.SaveChangesAsync();

            // Act
            int result = await uploadedFileRepository.DeleteById(file.Id);

            // Assert
            var deletedFile = await uploadedFileRepository.Table.FindAsync(file.Id);

            Assert.True(result == 1, $"{result} row(s) are affected. It should have been 1");
            Assert.Null(deletedFile);
        }

        [Fact]
        public async Task DeleteById_WhenIdDoesNotExist_DoesNothing()
        {
            // Arrange
            Guid id = new Guid();

            // Act
            int result = await uploadedFileRepository.DeleteById(id);

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }

        [Fact]
        public async Task DeleteMultiple_WhenFilesExist_DeletesAll()
        {
            // Arrange
            List<UploadedFile> files = new List<UploadedFile>()
            {
                UploadedFileGenerator.Generate(),
                UploadedFileGenerator.Generate(),
                UploadedFileGenerator.Generate()
            };

            await uploadedFileRepository.Table.AddRangeAsync(files);
            await uploadedFileRepository.SaveChangesAsync();

            // Act
            uploadedFileRepository.DeleteMultiple(files);
            int result = await uploadedFileRepository.SaveChangesAsync();

            // Assert
            var ids = files.Select(f => f.Id);
            var deletedFiles = await uploadedFileRepository.Table.Where(f => ids.Contains(f.Id)).ToListAsync();

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(deletedFiles);
        }

        [Fact]
        public async Task DeleteAllById_WhenIdsExist_DeletesAll()
        {
            // Arrange
            List<UploadedFile> files = new List<UploadedFile>()
            {
                UploadedFileGenerator.Generate(),
                UploadedFileGenerator.Generate(),
                UploadedFileGenerator.Generate()
            };
            var ids = files.Select(f => f.Id);

            await uploadedFileRepository.Table.AddRangeAsync(files);
            await uploadedFileRepository.SaveChangesAsync();

            // Act
            int result = await uploadedFileRepository.DeleteAllById(ids);

            // Assert
            var deletedFiles = await uploadedFileRepository.Table.Where(f => ids.Contains(f.Id)).ToListAsync();

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(deletedFiles);
        }

        [Fact]
        public async Task DeleteAllById_WhenSomeIdsExist_DeletesExistedOnes()
        {
            // Arrange
            List<UploadedFile> files = new List<UploadedFile>()
            {
                UploadedFileGenerator.Generate(),
                UploadedFileGenerator.Generate(),
                UploadedFileGenerator.Generate()
            };
            
            await uploadedFileRepository.Table.AddRangeAsync(files);
            await uploadedFileRepository.SaveChangesAsync();

            files.Add(UploadedFileGenerator.Generate());
            files.Add(UploadedFileGenerator.Generate());
            var ids = files.Select(f => f.Id);

            // Act
            int result = await uploadedFileRepository.DeleteAllById(ids);

            // Assert
            var deletedFiles = await uploadedFileRepository.Table.Where(f => ids.Contains(f.Id)).ToListAsync();

            Assert.True(result == 3, $"{result} row(s) are affected. It should have been 3");
            Assert.Empty(deletedFiles);
        }

        [Fact]
        public async Task DeleteAllById_WhenIdsDoNotExist_DoesNothing()
        {
            // Arrange
            List<UploadedFile> files = new List<UploadedFile>()
            {
                UploadedFileGenerator.Generate(),
                UploadedFileGenerator.Generate(),
                UploadedFileGenerator.Generate()
            };

            // Act
            int result = await uploadedFileRepository.DeleteAllById(files.Select(f => f.Id));

            // Assert
            Assert.True(result == 0, $"{result} row(s) are affected. It should have been 0");
        }
    }
}
