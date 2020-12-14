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

        public UserService(
            IUserRepositoryDb userRepositoryDb
        )
        {
            this.userRepositoryDb = userRepositoryDb;
        }

        public async Task<AuthResult> AuthenticateAsync(EmailLoginRequest request)
        {
            var encryptedPassword = request.Password.ToMD5();
            var user = await userRepositoryDb.FirstOrDefaultAsync(e => e.Email == request.Email && e.Password == encryptedPassword);

            if (user == null)
                return new AuthResult("Invalid user or password");

            var refreshToken = Guid.NewGuid().ToString();
            return new AuthResult(user, refreshToken);
        }

        public async Task<PagedResult<UserInfo>> SearchAsync(string value = null, int page = 1)
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
                ).To(e => new UserInfo(e));

            return result;
        }

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
    }
}
