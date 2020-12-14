namespace JWTDemo.Domain.Dtos
{
    public class AuthResultInfo
    {
        public AuthResultInfo(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
