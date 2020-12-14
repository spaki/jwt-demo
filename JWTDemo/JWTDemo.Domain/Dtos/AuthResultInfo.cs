namespace JWTDemo.Domain.Dtos
{
    public class AuthResultInfo
    {
        public AuthResultInfo(string email, string name, string refreshToken)
        {
            Email = email;
            Name = name;
            RefreshToken = refreshToken;
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
