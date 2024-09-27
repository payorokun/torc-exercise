using FluentAssertions;
using Moq;
using RealEstateListingApi.Application.Listings.Create;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Application.UnitOfWork;
using RealEstateListingApi.Domain.Models;
using RealEstateListingApi.Infrastructure.Data;
using RealEstateListingApi.Infrastructure.UnitOfWork;

namespace RealEstateListingApi.Test.Application;

public class CreateListingCommandHandlerTests
{
    [Test]
    public async Task Handler_CreatesItem()
    {
        var dummyId = "dummyId";

        var title = "title";
        var newListing = new Listing { Title = title, Id = dummyId };
        var command = new CreateListingCommand(newListing);
        var newId = command.NewListing.Id;

        var dbContext = new Mock<IApplicationDbContext>();
        var unitOfWorkMock = new UnitOfWork(dbContext.Object);
        var repository = new Mock<IRepository<Listing>>();

        var handler = new CreateListingCommandHandler(unitOfWorkMock, repository.Object);
        repository.Setup(x => x.Add(It.IsAny<Listing>()));

        var result = await handler.Handle(command, CancellationToken.None);
        result.Title.Should().Be(title);
        result.Id.Should().Be(newId);
        repository.Verify(r => r.Add(It.IsAny<Listing>()), Times.Once);
    }

    [Test]
    public async Task TransactionScope_AddsItem()
    {
        var dummyId = "dummyId";
        
        var title = "title";
        var newListing = new Listing {Title = title, Id = dummyId};
        var command = new CreateListingCommand(newListing);
        var newId = command.NewListing.Id;

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var transactionScope = new Mock<ITransactionScope>();
        var transactionScopeWithRepo = new Mock<ITransactionScopeWithRepo<Listing>>();
        var transactionScopeReady = new Mock<ITransactionScopeReady>();
        var repository = new Mock<IRepository<Listing>>();
        
        var handler = new CreateListingCommandHandler(unitOfWorkMock.Object, repository.Object);
        unitOfWorkMock.Setup(x => x.CreateTransaction())
            .Returns(transactionScope.Object);
        transactionScope.Setup(x => x.WithRepo(It.IsAny<IRepositoryWrite<Listing>>()))
            .Returns(transactionScopeWithRepo.Object);
        transactionScopeWithRepo.Setup(x => x.Add(It.IsAny<Listing>())).Returns(transactionScopeWithRepo.Object);
        transactionScopeWithRepo.Setup(x => x.Ready())
            .Returns(transactionScopeReady.Object);
        transactionScopeReady.Setup(t => t.CommitAsync())
            .Returns(Task.CompletedTask);
        repository.Setup(x => x.Add(It.IsAny<Listing>()));

        var result = await handler.Handle(command, CancellationToken.None);
        result.Title.Should().Be(title);
        result.Id.Should().Be(newId);
        transactionScopeWithRepo.Verify(r => r.Add(It.IsAny<Listing>()), Times.Once);
    }
}