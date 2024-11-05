using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Extensions
{
    public static class DbContextServiceExtensions  
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
