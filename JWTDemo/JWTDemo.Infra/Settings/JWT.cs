namespace JWTDemo.Infra.Settings
{
    public class JWT
    {
        public string Secret { get; set; }
        public int TimeoutInSeconds { get; set; }
    }
}
