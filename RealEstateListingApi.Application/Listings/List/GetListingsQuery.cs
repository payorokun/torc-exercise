using MediatR;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.List;

public record GetListingsQuery : IRequest<List<Listing>>;
