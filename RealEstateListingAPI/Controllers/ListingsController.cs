using Microsoft.AspNetCore.Mvc;
using MediatR;
using RealEstateListingApi.Application.Listings.Create;
using RealEstateListingApi.Application.Listings.Delete;
using RealEstateListingApi.Application.Listings.Get;
using RealEstateListingApi.Application.Listings.List;
using RealEstateListingApi.Application.Listings.Update;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingsController(ISender sender) : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public ListingsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        // Tag this operation as "Listings Retrieval"
        [HttpGet]
        [Tags("Listings Retrieval")]
        public async Task<ActionResult<IEnumerable<Listing>>> GetAllListings()
        {
            return await sender.Send(new GetListingsQuery());
        }

        // Tag this operation as "Listings Management"
        [HttpPost]
        [Tags("Listings Management")]
        public async Task<ActionResult<Listing>> AddListing([FromBody] Listing listing)
        {
            var command = new CreateListingCommand(listing);
            var createdItem = await sender.Send(command);
            return CreatedAtAction(nameof(GetListingById), new { id = createdItem.Id }, listing);
        }

        // Tag this operation as "Listings Retrieval"
        [HttpGet("{id}")]
        [Tags("Listings Retrieval")]
        public async Task<ActionResult<Listing>> GetListingById(string id)
        {
            var listing = await sender.Send(new GetListingByIdQuery(id));
            if (listing is null)
            {
                return NotFound();
            }
            return listing;
        }

        [HttpPut("{id}")]
        [Tags("Listings Management")]
        public async Task<ActionResult<Listing>> UpdateListing(string id, [FromBody] Listing listing)
        {
            var command = new UpdateListingCommand(id, listing);
            var updatedItem = await sender.Send(command);
            if (updatedItem is null)
            {
                return NotFound();
            }
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        [Tags("Listings Management")]
        public async Task<ActionResult> DeleteListing(string id)
        {
            var command = new DeleteListingCommand(id);
            await sender.Send(command);
            return NoContent();
        }

    }
}
