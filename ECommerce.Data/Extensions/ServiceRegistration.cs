using ECommerce.Data.Context;
using ECommerce.Data.Repositories.Abstracts;
using ECommerce.Data.Repositories.Concretes;
using ECommerce.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Extensions
{
    public static class ServiceRegistration
    {

        public static void AddDataLayer(this IServiceCollection services , IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // Unit of Work (Repository'leri içeriden yönetir)
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
