using JWTDemo.Domain.Models;

namespace JWTDemo.Domain.Dtos
{
    public class AuthResult
    {
        public AuthResult(string message)
        {
            Message = message;
        }

        public AuthResult(User entity)
        {
            Success = true;
            AuthResultInfo = new AuthResultInfo(entity.Email, entity.Name);
        }

        public AuthResultInfo AuthResultInfo { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public void SetTokens(string token, string refreshToken)
        {
            AuthResultInfo.Token = token;
            AuthResultInfo.RefreshToken = refreshToken;
        }
    }
}
