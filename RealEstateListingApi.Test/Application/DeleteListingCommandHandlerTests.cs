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
        var transactionScope = new Mock<ITransactionScope>();
        var repository = new Mock<IRepository<Listing>>();
        
        var handler = new DeleteListingCommandHandler(unitOfWorkMock.Object, repository.Object);
        unitOfWorkMock.Setup(x => x.CreateTransaction())
            .Returns(transactionScope.Object);
        transactionScope.Setup(x => x.WithActions(It.IsAny<Action>()))
            .Callback<Action>(action => action())
            .Returns(transactionScope.Object);
        transactionScope.Setup(t => t.Commit())
            .Returns(Task.CompletedTask);
        repository.Setup(x => x.Delete(It.Is<Listing>(l=>l.Id.Equals(dummyId))));

        await handler.Handle(command, CancellationToken.None);

        repository.Verify(r=>r.Delete(It.Is<Listing>(l=>l.Id.Equals(dummyId))), Times.Once);
        transactionScope.Verify(t => t.Commit(), Times.Once);
    }
}