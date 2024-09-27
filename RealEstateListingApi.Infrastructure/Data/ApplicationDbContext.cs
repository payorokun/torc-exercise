using Microsoft.EntityFrameworkCore;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : DbContext(options), IApplicationDbContext
    {
        public DbSet<Listing> Listings { get; set; }
        private bool? _isInMemory;

        private bool IsInMemory
        {
            get
            {
                _isInMemory ??= Database.ProviderName!.Equals("Microsoft.EntityFrameworkCore.InMemory");

                return _isInMemory.Value;
            }
        }

        public Task BeginTransactionAsync()
        {
            if (IsInMemory) return Task.CompletedTask;
            return Database.BeginTransactionAsync();
        }

        public Task CommitTransactionAsync()
        {
            if (IsInMemory) return Task.CompletedTask;
            return Database.CommitTransactionAsync();
        }

        public Task RollbackTransactionAsync()
        {
            if (IsInMemory) return Task.CompletedTask;
            return Database.RollbackTransactionAsync();
        }
    }

    public interface IApplicationDbContext
    {
        DbSet<Listing> Listings { get; set; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
