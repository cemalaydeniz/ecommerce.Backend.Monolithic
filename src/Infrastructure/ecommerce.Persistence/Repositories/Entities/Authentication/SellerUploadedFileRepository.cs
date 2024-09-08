using ecommerce.Application.Repositories.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.Repositories.Entities.Authentication
{
    public class SellerUploadedFileRepository : BaseRepository<SellerUploadedFile, Guid>, ISellerUploadedFileRepository
    {
        public SellerUploadedFileRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public void Delete(SellerUploadedFile file)
        {
            Table.Remove(file);
        }

        public async Task<int> DeleteById(Guid fileId)
        {
            int affectedRows = await Table
                .Where(f => f.Id.Equals(fileId))
                .ExecuteDeleteAsync();

            if (affectedRows == 0)
                return 0;

            SellerUploadedFile? deletedFile = Table.GetLoadedEntityByPrimaryKey(fileId);
            if (deletedFile != null)
            {
                Table.Detach(deletedFile);
            }

            return affectedRows;
        }

        public void DeleteMultiple(IEnumerable<SellerUploadedFile> files)
        {
            Table.RemoveRange(files);
        }

        public async Task<int> DeleteAllById(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            int affectedRows = await Table
                .Where(f => ids.Contains(f.Id))
                .ExecuteDeleteAsync(cancellationToken);

            if (affectedRows == 0)
                return 0;

            foreach (var id in ids)
            {
                SellerUploadedFile? deletedFile = Table.GetLoadedEntityByPrimaryKey(id);
                if (deletedFile != null)
                {
                    Table.Detach(deletedFile);
                }
            }

            return affectedRows;
        }
    }
}
