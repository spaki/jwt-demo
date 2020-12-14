using JWTDemo.API.Controllers.Common;
using JWTDemo.API.Services;
using JWTDemo.Domain.Dtos;
using JWTDemo.Domain.Dtos.Common;
using JWTDemo.Domain.Interfaces.Repositories;
using JWTDemo.Domain.Interfaces.Services;
using JWTDemo.Infra.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWTDemo.API.Controllers
{
    [ApiVersion("1")]
    public class UserController : RootController
    {
        private readonly IUserService userService;
        private readonly TokenService tokenService;
        private readonly IRefreshTokenRepositoryCache refreshTokenRepositoryCache;
        private readonly AppSettings settings;

        public UserController(
            IUserService userService,
            TokenService tokenService,
            IRefreshTokenRepositoryCache refreshTokenRepositoryCache,
            AppSettings settings
        )
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.refreshTokenRepositoryCache = refreshTokenRepositoryCache;
            this.settings = settings;
        }

        [HttpGet("simple")]
        public async Task<PagedResult<UserInfoSimple>> GetSimpleAsync(string value, int page = 1) => await userService.SearchSimpleAsync(value, page).ConfigureAwait(false);

        [Authorize]
        [HttpGet("complete")]
        public async Task<PagedResult<UserInfoComplete>> GetCompleteAsync(string value, int page = 1) => await userService.SearchCompleteAsync(value, page).ConfigureAwait(false);

        [Authorize]
        [HttpGet("me")]
        public async Task<UserInfoComplete> GetMeAsync()
        {
            var email = GetClaim(ClaimTypes.Email);
            var result = await userService.GetUserInfoByEmailAsync(email).ConfigureAwait(false);
            return result;
        }

        [HttpPost]
        public async Task<OperationResult> CreateAsync(UserCreateRequest request) => await userService.CreateAsync(request).ConfigureAwait(false);

        [HttpPost("token")]
        public async Task<ActionResult<AuthResult>> LoginAsync(EmailLoginRequest request)
        {
            var authResult = await userService.AuthenticateAsync(request).ConfigureAwait(false);
            var result = Authenticate(authResult);
            return result;
        }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult<AuthResult>> LoginAsync(RefreshTokenLoginRequest request)
        {
            var authResult = await userService.AuthenticateAsync(request).ConfigureAwait(false);
            var result = Authenticate(authResult);
            return result;
        }

        private ActionResult<AuthResult> Authenticate(AuthResult authResult)
        {
            if (!authResult.Success)
                return StatusCode(StatusCodes.Status401Unauthorized, authResult);

            var token = tokenService.GenerateToken(authResult);
            var refreshToken = tokenService.GenerateRefreshToken();

            refreshTokenRepositoryCache.SaveRefreshToken(settings.JWT.RefreshTokenTimeoutInSeconds, new RefreshTokenLoginRequest(authResult.AuthResultInfo.Email, refreshToken));
            authResult.SetTokens(token, refreshToken);

            return authResult;
        }
    }
}
