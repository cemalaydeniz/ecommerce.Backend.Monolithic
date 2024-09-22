using ecommerce.Domain.Repositories.Entities.Common;
using ecommerce.Domain.Entities.Common;
using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.Repositories.Entities.Common
{
    public class UploadedFileRepository : BaseRepository<UploadedFile, Guid>, IUploadedFileRepository<UploadedFile>
    {
        public UploadedFileRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public void Delete(UploadedFile file)
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

            UploadedFile? deletedFile = Table.GetLoadedEntityByPrimaryKey(fileId);
            if (deletedFile != null)
            {
                Table.Detach(deletedFile);
            }

            return affectedRows;
        }

        public void DeleteMultiple(IEnumerable<UploadedFile> files)
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
                UploadedFile? deletedFile = Table.GetLoadedEntityByPrimaryKey(id);
                if (deletedFile != null)
                {
                    Table.Detach(deletedFile);
                }
            }

            return affectedRows;
        }
    }
}
