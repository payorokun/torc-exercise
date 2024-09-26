using MediatR;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Application.UnitOfWork;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.Create;

public class CreateListingCommandHandler(IUnitOfWork unitOfWork, IRepository<Listing> repository) : IRequestHandler<CreateListingCommand, Listing>
{
    public async Task<Listing> Handle(CreateListingCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.CreateTransaction()
            .WithActions(() =>
            {
                repository.Add(request.NewListing);
            })
        .Commit();
        return request.NewListing;
    }
}
