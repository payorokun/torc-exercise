using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstateListingApi.Application.Repositories;
using RealEstateListingApi.Application.UnitOfWork;
using RealEstateListingApi.Domain.Models;
using RealEstateListingApi.Infrastructure.Data;
using RealEstateListingApi.Infrastructure.Repositories;

namespace RealEstateListingApi.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services) =>
            services
                .AddLocalDbContext()
                .AddServices();

    private static IServiceCollection AddLocalDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("RealEstateListings"));
        return services;
    }
        

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(GeneralRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        return services;
    }
        
    
}
