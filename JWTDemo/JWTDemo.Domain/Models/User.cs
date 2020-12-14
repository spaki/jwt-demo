using JWTDemo.Domain.Models.Common;
using JWTDemo.Infra;
using System;

namespace JWTDemo.Domain.Models
{
    public class User : EntityBase
    {
        public User()
        {

        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password.ToMD5();
            CreatedAtUtc = EditedAtUtc = DateTime.UtcNow;
        }

        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
        public virtual DateTime EditedAtUtc { get; set; }
    }
}
