using MediatR;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.List;
internal class GetListingsQueryHandler(IRepository<Listing> repository) : IRequestHandler<GetListingsQuery, List<Listing>>
{
    public async Task<List<Listing>> Handle(GetListingsQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync()).ToList();
    }
}
