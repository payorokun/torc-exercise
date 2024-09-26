using MediatR;

namespace RealEstateListingApi.Application.Listings.Delete;

public record DeleteListingCommand(string Id) : IRequest;
