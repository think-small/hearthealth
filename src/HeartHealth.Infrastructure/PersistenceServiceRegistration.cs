using HeartHealth.Application.Contracts.Persistence;
using HeartHealth.Domain.Entities;
using HeartHealth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeartHealth.Infrastructure
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HeartHealthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("HeartHealthConnectionString")));
            services.AddScoped<IBaseRepository<Measurement>, BaseRepository<Measurement>>();
            return services;
        }
    }
}
