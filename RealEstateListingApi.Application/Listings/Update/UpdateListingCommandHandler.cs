using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Application.UnitOfWork;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Listings.Update;
internal class UpdateListingCommandHandler(IUnitOfWork unitOfWork, IRepository<Listing> repository) : IRequestHandler<UpdateListingCommand, Listing>
{
    public async Task<Listing> Handle(UpdateListingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.CreateTransaction()
                .WithActions(() =>
                {
                    repository.Update(request.UpdatedListing);
                })
                .Commit();
            return request.UpdatedListing;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return null;
        }
    }
}
