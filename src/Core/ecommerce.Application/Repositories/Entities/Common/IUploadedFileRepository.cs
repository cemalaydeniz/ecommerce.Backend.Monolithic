using ecommerce.Domain.Entities.Common;

namespace ecommerce.Application.Repositories.Entities.Common
{
    public interface IUploadedFileRepository<TEntity> : IBaseRepository<TEntity, Guid>
        where TEntity : UploadedFile
    {
        /// <summary>
        /// Deletes the file from the table
        /// </summary>
        /// <param name="file">File to delete</param>
        void Delete(TEntity file);
        /// <summary>
        /// Deletes the file from the table by its ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="fileId">ID of the file to delete</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> DeleteById(Guid fileId);
        /// <summary>
        /// Deletes the files from the table
        /// </summary>
        /// <param name="files">Files to delete</param>
        void DeleteMultiple(IEnumerable<TEntity> files);
        /// <summary>
        /// Delete all files by their IDs immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="ids">IDs of the files to delete</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> DeleteAllById(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    }
}
