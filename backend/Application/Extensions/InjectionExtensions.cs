using Application.Interfaces;
using Application.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class InjectionExtensions
    {
        [Obsolete]
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration) {
            services.AddSingleton(configuration);

            services.AddFluentValidation(option =>
            {
                option.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IWishListApplication, WishListApplication>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();

            return services;
        }
    }
}
