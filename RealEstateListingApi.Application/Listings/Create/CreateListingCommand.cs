using MediatR;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.Create;

public record CreateListingCommand : IRequest<Listing>
{
    public readonly Listing NewListing;
    public CreateListingCommand(Listing listing)
    {
        listing.Id = Guid.NewGuid().ToString()[..5];
        NewListing = listing;
    }
}
