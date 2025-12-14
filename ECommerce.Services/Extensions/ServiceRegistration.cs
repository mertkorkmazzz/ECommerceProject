using ECommerce.Services.Abstract;
using ECommerce.Services.Concreate;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            // AutoMapper (Service katmanındaki profilleri tarar)
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
