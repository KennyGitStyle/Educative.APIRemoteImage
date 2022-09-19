namespace Educative.API.Extension
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services){
            return services.AddCors();
        }
    }
}