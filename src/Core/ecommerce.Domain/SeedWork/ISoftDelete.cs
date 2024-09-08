namespace ecommerce.Domain.SeedWork
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; }
        public DateTime? SoftDeletedDate { get; }
    }
}
