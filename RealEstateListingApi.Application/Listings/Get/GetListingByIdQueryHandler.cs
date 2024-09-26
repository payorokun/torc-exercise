using MediatR;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.Get;
internal class GetListingByIdQueryHandler(IRepository<Listing> repository) : IRequestHandler<GetListingByIdQuery, Listing>
{
    public async Task<Listing> Handle(GetListingByIdQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetByIdAsync(request.Id);
    }
}
