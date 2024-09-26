namespace RealEstateListingApi.Application.UnitOfWork;

public interface ITransactionScope
{
    ITransactionScope WithActions(Action action);
    Task Commit();
}