using MediatR;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.Get;
public record GetListingByIdQuery(string Id) : IRequest<Listing>;