using JWTDemo.Domain.Models;

namespace JWTDemo.Domain.Dtos
{
    public class UserInfoSimple
    {
        public UserInfoSimple(User entity)
        {
            Name = entity.Name;
        }

        public string Name { get; set; }
    }
}
