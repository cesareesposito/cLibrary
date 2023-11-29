namespace cLibrary.Jwt
{
    public class JwtSettings
    {
        public static string Key { get; set; } //your-256-bit-secret
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static int ExpiryInDays { get; set; } = 1;
    }
}
