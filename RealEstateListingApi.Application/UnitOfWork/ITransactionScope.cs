namespace RealEstateListingApi.Application.UnitOfWork;

public interface ITransactionScope
{
    ITransactionScopeWithActions WithActions(Action action);
}

public interface ITransactionScopeWithActions : ITransactionScope
{
    Task Commit();
}