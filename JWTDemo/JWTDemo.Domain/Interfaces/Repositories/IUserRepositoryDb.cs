using JWTDemo.Domain.Interfaces.Repositories.Common;
using JWTDemo.Domain.Models;

namespace JWTDemo.Domain.Interfaces.Repositories
{
    public interface IUserRepositoryDb : ICrudRepositoryBase<User>
    {
    }
}
