using JWTDemo.Domain.Dtos;
using JWTDemo.Domain.Dtos.Common;
using JWTDemo.Domain.Interfaces.Services.Common;
using System.Threading.Tasks;

namespace JWTDemo.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase
    {
        Task<AuthResult> AuthenticateAsync(EmailLoginRequest request);
        Task<AuthResult> AuthenticateAsync(RefreshTokenLoginRequest request);
        Task<PagedResult<UserInfoSimple>> SearchSimpleAsync(string value = null, int page = 1);
        Task<PagedResult<UserInfoComplete>> SearchCompleteAsync(string value = null, int page = 1);
        Task<UserInfoComplete> GetUserInfoByEmailAsync(string email);
        Task<OperationResult> CreateAsync(UserCreateRequest request);
    }
}
