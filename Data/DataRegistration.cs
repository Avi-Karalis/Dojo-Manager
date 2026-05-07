using Microsoft.Extensions.DependencyInjection;
using DojoManager.Data;
using DojoManager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace DojoManager.Data {
    public static class DataRegistration {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("PostgreSQL")));
            return services;
        }
    }
}