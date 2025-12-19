using ECommerce.Data.Context;
using ECommerce.Data.Extensions;
using ECommerce.Data.Seed;
using ECommerce.Services.Extensions;

namespace EcommerceApı
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDataLayer(builder.Configuration);
            builder.Services.AddBusinessServices();

            // Auto Mapper Configurations
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 🔽 app SADECE BURADA OLUŞUR
            var app = builder.Build();

            // 🔽 SEED KODU BURAYA
            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                FakeDataSeeder.SeedAsync(context).Wait();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
