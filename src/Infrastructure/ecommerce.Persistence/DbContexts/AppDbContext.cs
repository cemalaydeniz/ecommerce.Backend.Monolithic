using ecommerce.Domain.Entities.Account;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Common;
using ecommerce.Persistence.Configurations.Account;
using ecommerce.Persistence.Configurations.Authentication;
using ecommerce.Persistence.Configurations.Common;
using ecommerce.Persistence.Filters;
using ecommerce.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        // Authentication
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerUploadedFile> SellerUploadedFiles { get; set; }

        // Account
        public DbSet<Address> Addresses { get; set; }

        // Common
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddGlobalSoftDeleteQueryFilter();

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SellerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SellerUploadedFileEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new AddressEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new UploadedFileEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

            optionsBuilder.AddInterceptors(new CreatedDateInterceptor());
            optionsBuilder.AddInterceptors(new UpdateDateAuditInterceptor());
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }
    }
}
