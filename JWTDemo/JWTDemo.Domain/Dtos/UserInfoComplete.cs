using JWTDemo.Domain.Models;
using System;

namespace JWTDemo.Domain.Dtos
{
    public class UserInfoComplete
    {
        public UserInfoComplete(User entity)
        {
            Name = entity.Name;
            Email = entity.Email;
            CreatedAtUtc = entity.CreatedAtUtc;
            EditedAtUtc = entity.EditedAtUtc;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime EditedAtUtc { get; set; }
    }
}
