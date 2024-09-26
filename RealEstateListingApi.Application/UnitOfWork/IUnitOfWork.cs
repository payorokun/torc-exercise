namespace RealEstateListingApi.Application.UnitOfWork;
public interface IUnitOfWork
{
    ITransactionScope CreateTransaction();
}