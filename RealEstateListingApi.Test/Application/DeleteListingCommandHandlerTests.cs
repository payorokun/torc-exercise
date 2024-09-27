using FluentAssertions;
using Moq;
using RealEstateListingApi.Application.Listings.Create;
using RealEstateListingApi.Application.Listings.Delete;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Application.UnitOfWork;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Test.Application;

public class DeleteListingCommandHandlerTests
{
    [Test]
    public async Task Handler_DeletesItem()
    {
        var dummyId = "dummyId";
        var command = new DeleteListingCommand(dummyId);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var transactionScope = new Mock<IDummyTransactionScope>();
        var transactionScopeWithRepo = new Mock<ITransactionScopeWithRepo<Listing>>();
        var transactionScopeReady = new Mock<ITransactionScopeReady>();
        var transactionBuilder = new Mock<ITransactionBuilder>();
        var repository = new Mock<IRepository<Listing>>();

        var handler = new DeleteListingCommandHandler(unitOfWorkMock.Object, repository.Object);
        unitOfWorkMock.Setup(x => x.CreateTransaction())
            .Returns(transactionScope.Object);
        transactionScope.Setup(x => x.WithRepo(It.IsAny<IRepositoryWrite<Listing>>()))
            .Returns(transactionScopeWithRepo.Object);
        transactionScopeWithRepo.Setup(x => x.Delete(It.IsAny<Listing>())).Returns(transactionScopeWithRepo.Object);
        transactionScopeWithRepo.Setup(x => x.Ready())
            .Returns(transactionScopeReady.Object);
        transactionScopeReady.Setup(t => t.CommitAsync())
            .Returns(Task.CompletedTask);
        transactionBuilder.Setup(t => t.AppendAction(It.IsAny<Action>()));
        repository.Setup(x => x.Add(It.IsAny<Listing>()));

        await handler.Handle(command, CancellationToken.None);

        transactionScopeWithRepo.Verify(r=>r.Delete(It.Is<Listing>(l=>l.Id.Equals(dummyId))), Times.Once);
    }

    public interface IDummyTransactionScope : ITransactionScope, ITransactionBuilder;
}