using JWTDemoClient.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JWTDemoClient.Services
{
    public class AuthorizationService
    {
        private const int cacheTimeoutInMinutes = 1;

        private readonly AppSettings settings;
        private readonly ILogger<AuthorizationService> log;
        private readonly HttpClient client;
        protected readonly IDistributedCache cache;

        public AuthorizationService(
            IHttpClientFactory clientFactory,
            AppSettings settings,
            IDistributedCache cache,
            ILogger<AuthorizationService> log
        )
        {
            this.settings = settings;
            this.log = log;
            this.cache = cache;
            this.client = clientFactory.CreateClient();
        }

        public async Task<bool> IsTokenValid(string token) => await GetFromCacheAndSetAsync("auth", "token", async () => await ValidateTokenInJwtAuthServiceApi(token));

        private async Task<bool> ValidateTokenInJwtAuthServiceApi(string token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", token);

            try
            {
                var response = await client.GetAsync($"{settings.JwtAuthServiceApiUrl}api/v1/User/me");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Invalid Token");
                return false;
            }
        }

        private async Task<T> GetFromCacheAndSetAsync<T>(string area, string key, Func<Task<T>> getAsync)
        {
            var fullKey = $"{area}-{key}";
            var json = await cache.GetStringAsync(fullKey);

            if (!string.IsNullOrWhiteSpace(json))
            {
                log.LogInformation($"{fullKey} retrieved from cache");
                return JsonConvert.DeserializeObject<T>(json);
            }

            var result = await getAsync();
            json = JsonConvert.SerializeObject(result);

            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheTimeoutInMinutes));

            await cache.SetStringAsync(fullKey, json, cacheOptions);

            log.LogInformation($"{fullKey} retrieved from API");

            return result;
        }
    }
}
