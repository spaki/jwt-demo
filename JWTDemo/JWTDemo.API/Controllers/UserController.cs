using JWTDemo.API.Controllers.Common;
using JWTDemo.API.Services;
using JWTDemo.Domain.Dtos;
using JWTDemo.Domain.Dtos.Common;
using JWTDemo.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JWTDemo.API.Controllers
{
    [ApiVersion("1")]
    public class UserController : RootController
    {
        private readonly IUserService userService;
        private readonly TokenService tokenService;

        public UserController(
            IUserService userService,
            TokenService tokenService
        )
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpGet]
        public async Task<PagedResult<UserInfo>> GetAsync(string value, int page = 1) => await userService.SearchAsync(value, page).ConfigureAwait(false);

        [HttpPost]
        public async Task<OperationResult> CreateAsync(UserCreateRequest request) => await userService.CreateAsync(request).ConfigureAwait(false);

        [HttpPost("token")]
        public async Task<AuthResult> LoginAsync(EmailLoginRequest request)
        {
            var authResult = await userService.AuthenticateAsync(request).ConfigureAwait(false);

            if(!authResult.Success)
                return authResult;

            authResult.SetJwtToken(tokenService.GenerateToken(authResult));
            return authResult;
        }
    }
}
