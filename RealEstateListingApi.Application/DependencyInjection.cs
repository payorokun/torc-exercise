using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace RealEstateListingApi.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services) =>
            services
                .AddApplicationMediator()
                .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

    private static IServiceCollection AddApplicationMediator(
        this IServiceCollection services) =>
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

}
