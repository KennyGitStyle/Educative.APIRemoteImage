
using Educative.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Educative.API.Extension
{
    public static class DbExtension
    {
        public static IServiceCollection AddDbContextExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<EducativeContext>(options =>
            {
                options
                
                .UseSqlite(config.GetConnectionString("EducativeDefaultConnection"));


            });

            return services;
        }
    }
}