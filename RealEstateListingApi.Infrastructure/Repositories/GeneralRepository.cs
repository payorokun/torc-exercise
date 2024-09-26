using Microsoft.EntityFrameworkCore;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Infrastructure.Data;

namespace RealEstateListingApi.Infrastructure.Repositories;

public class GeneralRepository<TEntity>(ApplicationDbContext context) : IRepository<TEntity>
    where TEntity : class
{
    public async Task<TEntity> GetByIdAsync(string id) => await context.Set<TEntity>().FindAsync(id);
    
    public async Task<IEnumerable<TEntity>> GetAllAsync() => await context.Set<TEntity>().ToListAsync();

    public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);

    public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);
    public void Delete(TEntity entity)=> context.Set<TEntity>().Remove(entity);
}
