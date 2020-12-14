using JWTDemo.Domain.Dtos;
using JWTDemo.Domain.Dtos.Common;
using JWTDemo.Domain.Interfaces.Repositories;
using JWTDemo.Domain.Interfaces.Services;
using JWTDemo.Domain.Models;
using JWTDemo.Domain.Services.Common;
using JWTDemo.Infra;
using System;
using System.Threading.Tasks;

namespace JWTDemo.Domain.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepositoryDb userRepositoryDb;
        private readonly IRefreshTokenRepositoryCache refreshTokenRepositoryCache;

        public UserService(
            IUserRepositoryDb userRepositoryDb,
            IRefreshTokenRepositoryCache refreshTokenRepositoryCache
        )
        {
            this.userRepositoryDb = userRepositoryDb;
            this.refreshTokenRepositoryCache = refreshTokenRepositoryCache;
        }

        public async Task<AuthResult> AuthenticateAsync(EmailLoginRequest request)
        {
            var encryptedPassword = request.Password.ToMD5();
            var user = await GetByEmailAsync(request.Email);

            if (user == null || user.Password != encryptedPassword)
                return new AuthResult("Invalid user or password");

            return new AuthResult(user);
        }

        public async Task<AuthResult> AuthenticateAsync(RefreshTokenLoginRequest request)
        {
            var invalidAuth = new AuthResult("Invalid refresh token");
            var refreshTokenInfo = refreshTokenRepositoryCache.GetRefreshToken(request?.RefreshToken);

            if (refreshTokenInfo == null || refreshTokenInfo.Email != refreshTokenInfo.Email)
                return invalidAuth;

            var user = await GetByEmailAsync(request.Email);

            if (user == null)
                return invalidAuth;

            return new AuthResult(user);
        }

        public async Task<PagedResult<UserInfoComplete>> SearchCompleteAsync(string value = null, int page = 1) => (await SearchAsync(value, page)).To(e => new UserInfoComplete(e));

        public async Task<PagedResult<UserInfoSimple>> SearchSimpleAsync(string value = null, int page = 1) => (await SearchAsync(value, page)).To(e => new UserInfoSimple(e));

        public async Task<UserInfoComplete> GetUserInfoByEmailAsync(string email) => new UserInfoComplete(await GetByEmailAsync(email));

        public async Task<OperationResult> CreateAsync(UserCreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return new OperationResult(false, $"Invalid mail");

            if (string.IsNullOrWhiteSpace(request.Name))
                return new OperationResult(false, $"Invalid name");

            if (string.IsNullOrWhiteSpace(request.Password))
                return new OperationResult(false, $"Invalid password");

            if (request.Password != request.PasswordConfirmation)
                return new OperationResult(false, $"Invalid password confirmation");

            if (await userRepositoryDb.AnyAsync(e => e.Email == request.Email))
                return new OperationResult(false, $"The email alread exists");

            var entity = new User(request.Name, request.Email, request.Password);
            await userRepositoryDb.SaveAsync(entity);

            return new OperationResult();
        }

        private async Task<PagedResult<User>> SearchAsync(string value = null, int page = 1)
        {
            var result = userRepositoryDb
                .Page(
                    e =>
                        (
                            value == null
                            || e.Name.Contains(value)
                            || e.Email.Contains(value)
                        ),
                    page
                );

            return result;
        }

        private async Task<User> GetByEmailAsync(string email) => await userRepositoryDb.FirstOrDefaultAsync(e => e.Email == email);
    }
}
