namespace JWTDemo.Infra.Settings
{
    public class JWT
    {
        public string Secret { get; set; }
        public int TokenTimeoutInSeconds { get; set; }
        public int RefreshTokenTimeoutInSeconds { get; set; }
    }
}
