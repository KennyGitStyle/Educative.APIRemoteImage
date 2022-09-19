using Educative.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Educative.API.Extension
{
    public static class ApiConfigurationService
    {
        public static IServiceCollection AddConfigureService(this IServiceCollection services) 
        {
            return services.Configure<ApiBehaviorOptions>(opts =>
            {
                opts.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                     .Where(e => e.Value.Errors.Count > 0)
                     .SelectMany(x => x.Value.Errors)
                     .Select(em => em.ErrorMessage).ToArray();


                    var errorResponse = new HttpValidationErrorResponse
                    {
                        Errors = errors

                    };

                    return new BadRequestObjectResult(errorResponse);

                };


            });
        }
    }
}