using JWTDemo.Domain.Models;

namespace JWTDemo.Domain.Dtos
{
    public class AuthResult
    {
        public AuthResult(string message)
        {
            Message = message;
        }

        public AuthResult(User entity, string refreshToken)
        {
            Success = true;
            AuthResultInfo = new AuthResultInfo(entity.Email, entity.Name, refreshToken);
        }

        public AuthResultInfo AuthResultInfo { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public void SetJwtToken(string token) => AuthResultInfo.Token = token;
    }
}
