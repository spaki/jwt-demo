using System;

namespace JWTDemo.Domain.Models.Common
{
    public abstract class EntityBase
    {
        public virtual Guid Id { get; set; }
    }
}
