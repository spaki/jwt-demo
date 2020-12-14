namespace JWTDemo.Domain.Dtos
{
    public class RefreshTokenLoginRequest
    {
        public RefreshTokenLoginRequest()
        {

        }

        public RefreshTokenLoginRequest(string email, string refreshToken)
        {
            Email = email;
            RefreshToken = refreshToken;
        }

        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}
