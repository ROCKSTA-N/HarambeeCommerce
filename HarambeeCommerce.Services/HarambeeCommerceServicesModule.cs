using HarambeeCommerce.Persistence;
using HarambeeCommerce.Services.BasketServices;
using HarambeeCommerce.Services.CustomerServices;
using HarambeeCommerce.Services.ProductServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarambeeCommerce.Services;

public static class HarambeeCommerceServicesModule
{
    public static IServiceCollection AddServicesModule(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddPersistenceModule(configuration);

        services.AddTransient<IBasketService, BasketService>()
                .AddTransient<ICustomerService, CustomerService>()
                .AddTransient<IProductService, ProductService>();

        return services;
    }
}