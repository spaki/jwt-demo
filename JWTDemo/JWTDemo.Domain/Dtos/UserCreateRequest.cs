﻿namespace JWTDemo.Domain.Dtos
{
    public class UserCreateRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
