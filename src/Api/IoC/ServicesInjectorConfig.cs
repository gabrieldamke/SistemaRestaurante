using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.IoC;

public static class ServicesInjectorConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestauranteDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RestauranteDb"))
                .EnableSensitiveDataLogging());

        services.AddScoped(typeof(IEntityRepository<>), typeof(BaseEntityRepository<>));git 
    }
}