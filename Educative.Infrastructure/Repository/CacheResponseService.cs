using System;
using System.Text.Json;
using System.Threading.Tasks;
using Educative.Infrastructure.Interface;
using StackExchange.Redis;

namespace Educative.Infrastructure.Repository;

public class CacheResponseService : ICacheResponseService
{
    private readonly IDatabase _database;
    public CacheResponseService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
    {
        if(response is null) return;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var serializedResponse = JsonSerializer.Serialize(response, options);

        await _database.StringSetAsync(cacheKey, serializedResponse, timeToLive);
    }

    public async Task<string> GetCachedResponseAsync(string cacheKey)
    {
        var cachedResponse = await _database.StringGetAsync(cacheKey);

        if(cachedResponse.IsNullOrEmpty) return null;

        return cachedResponse;
        
    }
}