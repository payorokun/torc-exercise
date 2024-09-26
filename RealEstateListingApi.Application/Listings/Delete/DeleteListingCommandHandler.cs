using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Application.UnitOfWork;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.Delete;
internal class DeleteListingCommandHandler(IUnitOfWork unitOfWork, IRepository<Listing> repository) : IRequestHandler<DeleteListingCommand>
{
    public async Task Handle(DeleteListingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.CreateTransaction()
                .WithActions(() => { repository.Delete(new Listing {Id = request.Id}); })
                .Commit();
        }
        catch (DbUpdateConcurrencyException e)
        {
            //do nothing if not found
        }
    }
}
