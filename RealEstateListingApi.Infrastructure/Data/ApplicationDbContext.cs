using Microsoft.EntityFrameworkCore;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Listing> Listings { get; set; }
    }
}
