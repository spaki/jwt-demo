using JWTDemo.Cache.Repositories.Common;
using JWTDemo.Domain.Dtos;
using JWTDemo.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace JWTDemo.Cache.Repositories
{
    public class RefreshTokenRepositoryCache : RepositoryCacheBase, IRefreshTokenRepositoryCache
    {
        public RefreshTokenRepositoryCache(
            IDistributedCache cache
        ) : base(cache)
        {
        }

        public RefreshTokenLoginRequest GetRefreshToken(string refreshToken)
        {
            var json = cache.GetString(refreshToken);

            if (string.IsNullOrWhiteSpace(json))
                return null;

            var result = JsonConvert.DeserializeObject<RefreshTokenLoginRequest>(json);

            return result;
        }

        public void SaveRefreshToken(int timeoutInSeconds, RefreshTokenLoginRequest request)
        {
            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(timeoutInSeconds));

            cache.SetString
            (
                request.RefreshToken,
                JsonConvert.SerializeObject(request),
                cacheOptions
            );
        }
    }
}
