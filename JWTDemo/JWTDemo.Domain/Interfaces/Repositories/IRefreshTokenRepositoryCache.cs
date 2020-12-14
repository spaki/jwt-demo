using JWTDemo.Domain.Dtos;
using JWTDemo.Domain.Interfaces.Repositories.Common;

namespace JWTDemo.Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepositoryCache : IRepositoryBase
    {
        RefreshTokenLoginRequest GetRefreshToken(string refreshToken);
        void SaveRefreshToken(int timeoutInSeconds, RefreshTokenLoginRequest request);
    }
}
