using JWTDemo.Domain.Dtos;
using JWTDemo.Domain.Dtos.Common;
using JWTDemo.Domain.Interfaces.Services.Common;
using System.Threading.Tasks;

namespace JWTDemo.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase
    {
        Task<AuthResult> AuthenticateAsync(EmailLoginRequest request);
        Task<PagedResult<UserInfo>> SearchAsync(string value = null, int page = 1);
        Task<OperationResult> CreateAsync(UserCreateRequest request);
    }
}
