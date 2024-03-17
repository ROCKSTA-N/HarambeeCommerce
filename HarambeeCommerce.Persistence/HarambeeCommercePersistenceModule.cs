using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarambeeCommerce.Persistence;

public static class HarambeeCommercePersistenceModule
{
    public static IServiceCollection AddPersistenceModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HarambeeCommerceContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("HarambeeCommerce"));
        });

        services.AddScoped(typeof(IRepository<Product>), typeof(Repository<Product>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
