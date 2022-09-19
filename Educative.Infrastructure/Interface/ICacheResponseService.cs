using System;
using System.Threading.Tasks;

namespace Educative.Infrastructure.Interface;

public interface ICacheResponseService
{
    Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
    Task<string> GetCachedResponseAsync(string cacheKey);
}