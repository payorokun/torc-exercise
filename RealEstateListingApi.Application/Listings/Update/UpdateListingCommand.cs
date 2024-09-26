using MediatR;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.Update;

public record UpdateListingCommand : IRequest<Listing>
{
    public UpdateListingCommand(string id, Listing updatedListing)
    {
        UpdatedListing = updatedListing;
        UpdatedListing.Id = id;
    }

    public Listing UpdatedListing { get; init; }
}
