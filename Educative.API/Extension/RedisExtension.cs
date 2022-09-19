using StackExchange.Redis;

namespace Educative.API.Extension;

public static class RedisExtension
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IConnectionMultiplexer>(options =>
        {
            var configOptions = ConfigurationOptions.Parse(config.GetConnectionString("RedisDevelopment"), true);
            return ConnectionMultiplexer.Connect(configOptions);
        });

        return services;
    }
}