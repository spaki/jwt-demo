using JWTDemo.Domain.Interfaces.Repositories;
using JWTDemo.Domain.Models;
using JWTDemo.RepositoryDb.Context;
using JWTDemo.RepositoryDb.Repositories.Common;

namespace JWTDemo.RepositoryDb.Repositories
{
    public class UserRepositoryDb : CrudRepositoryBase<User>, IUserRepositoryDb
    {
        public UserRepositoryDb(MainDbContext context) : base(context)
        {
        }
    }
}
