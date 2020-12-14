namespace JWTDemo.Domain.Dtos
{
    public class OperationResult
    {
        public OperationResult()
        {
            Success = true;
        }

        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
