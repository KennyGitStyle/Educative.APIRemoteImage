using Educative.Infrastructure.Context;
using Educative.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Educative.API.Extension
{
    public static class DbInitializer
    {
        public static async Task UseDbInitializer(this WebApplication builder)
        {
            using var provider = builder.Services.CreateScope();
            var services = provider.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<EducativeContext>();
                await context.Database.MigrateAsync();
                await EducativeContextSeed.SeedDatabaseAsync(context, loggerFactory);
            }
            catch (Exception ex){
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }

        }
    }
}