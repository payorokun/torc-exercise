namespace RealEstateListingApi.Application.Repositories;

public interface IRepositoryWrite<in TEntity>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
public interface IRepository<TEntity> : IRepositoryWrite<TEntity>
{
    Task<TEntity> GetByIdAsync(string id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}
