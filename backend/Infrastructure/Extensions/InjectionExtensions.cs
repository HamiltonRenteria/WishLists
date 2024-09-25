using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DvpDbContext).Assembly.FullName;

            // PersistenceConnection
            services.AddDbContext<DvpDbContext>(
                option => option
                    .UseSqlServer(
                        configuration.GetConnectionString("PersitenceConnection"), 
                        b => b.MigrationsAssembly(assembly)), 
                ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
