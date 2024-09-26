using FluentAssertions;
using Moq;
using RealEstateListingApi.Application.Listings.Create;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Application.UnitOfWork;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Test.Application;

public class CreateListingCommandHandlerTests
{
    [Test]
    public async Task Handler_CreatesItem()
    {
        var dummyId = "dummyId";
        
        var title = "title";
        var newListing = new Listing {Title = title, Id = dummyId};
        var command = new CreateListingCommand(newListing);
        var newId = command.NewListing.Id;

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var transactionScope = new Mock<ITransactionScope>();
        var repository = new Mock<IRepository<Listing>>();
        
        var handler = new CreateListingCommandHandler(unitOfWorkMock.Object, repository.Object);
        unitOfWorkMock.Setup(x => x.CreateTransaction())
            .Returns(transactionScope.Object);
        transactionScope.Setup(x => x.WithActions(It.IsAny<Action>()))
            .Callback<Action>(action => action())
            .Returns(transactionScope.Object);
        transactionScope.Setup(t => t.Commit())
            //.Callback(() => command.NewListing.Id = newId)
            .Returns(Task.CompletedTask);
        repository.Setup(x => x.Add(It.IsAny<Listing>()));

        var result = await handler.Handle(command, CancellationToken.None);
        result.Title.Should().Be(title);
        result.Id.Should().Be(newId);
        repository.Verify(r => r.Add(It.IsAny<Listing>()), Times.Once);
        transactionScope.Verify(t => t.Commit(), Times.Once);
    }
}