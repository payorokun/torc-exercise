using RealEstateListingApi.Application.UnitOfWork;
using RealEstateListingApi.Infrastructure.Data;

namespace RealEstateListingApi.Infrastructure.UnitOfWork;
public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private class TransactionScope(ApplicationDbContext dbContext) : ITransactionScope
    {
        private readonly List<Action> _actions = new();
        private readonly bool _isInMemory = dbContext.Database.ProviderName!.Equals("Microsoft.EntityFrameworkCore.InMemory");
        public ITransactionScope WithActions(Action action)
        {
            _actions.Add(action);
            return this;
        }

        public async Task Commit()
        {
            if (!_isInMemory)
            {
                await dbContext.Database.BeginTransactionAsync();
                try
                {
                    await Execute();
                    await dbContext.Database.CommitTransactionAsync();
                    return;
                }
                catch (Exception e)
                {
                    await dbContext.Database.RollbackTransactionAsync();
                    throw;
                }
            }
            //no transactions with memory databases
            try
            {
                await Execute();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task Execute()
        {
            foreach (var action in _actions)
            {
                action();
            }
            await dbContext.SaveChangesAsync();
        }
    }


    public ITransactionScope CreateTransaction()
    {
        return new TransactionScope(dbContext);
    }
}
