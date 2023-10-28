
namespace Microservice3.CacheService;
public interface ICacheService
{
    Task<T?> GetAsync<T> (string cacheKey);
    Task SetAsync<T>(string cacheKey, T cacheData);
}
