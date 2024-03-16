using HarambeeCommerce.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarambeeCommerce.Services;

public static class HarambeeCommerceServicesModule
{
    public static IServiceCollection AddServicesModule(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddPersistenceModule(configuration);

        return services;
    }
}