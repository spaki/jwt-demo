using JWTDemo.Domain.Interfaces.Repositories.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace JWTDemo.Cache.Repositories.Common
{
    public abstract class RepositoryCacheBase : IRepositoryBase
    {
        protected readonly IDistributedCache cache;

        protected RepositoryCacheBase(
            IDistributedCache cache
        )
        {
            this.cache = cache;
        }
    }
}
