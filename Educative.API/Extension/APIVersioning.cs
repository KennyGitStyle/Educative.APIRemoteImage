using Microsoft.AspNetCore.Mvc.Versioning;

namespace Educative.API.Extension
{
    public static class APIVersioning
    {
        public static IServiceCollection AddApiVersioningService(this IServiceCollection services) 
        {
            return services.AddApiVersioning(opts => 
            {
                opts.ReportApiVersions = true;
                opts.DefaultApiVersion = new(1, 0);
                opts.AssumeDefaultVersionWhenUnspecified = true;
                

                opts.ApiVersionReader = new HeaderApiVersionReader("V-API-Version");
            });
        }
    }
}