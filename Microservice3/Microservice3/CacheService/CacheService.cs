using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Microservice3.CacheService;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string cacheKey)
    {
        var retorno = await _cache.GetStringAsync(cacheKey);
        if (string.IsNullOrWhiteSpace(retorno))
        {
            Console.WriteLine("No Cache");
            return default;
        }

        return JsonConvert.DeserializeObject<T>(retorno);   
    }

    public async Task SetAsync<T>(string cacheKey, T cacheData)
    {
        var memoryCacheEntryOptions = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromSeconds(10),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
            AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(60)
        };

        var objectString = JsonConvert.SerializeObject(cacheData);
        await _cache.SetStringAsync(cacheKey, objectString, memoryCacheEntryOptions);
    }
}
