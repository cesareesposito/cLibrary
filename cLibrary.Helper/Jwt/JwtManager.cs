using cLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cLibrary.Jwt
{
    public class JwtManager
    {
        public JwtManager()
        {
        }

        public async Task<OperationResult> GenerateToken(IEnumerable<Claim> claims)
        {
            try
            {
                // Data di rilascio del certificato
                DateTime issuedAt = DateTime.Now;
                // Data scadenza
                //var days = _configuration.GetSection("JwtSettings:ExpiryInDays").Value.ToIntN() ?? 1;
                //var days = 1;
                var days = JwtSettings.ExpiryInDays;
                DateTime expires = DateTime.Now.AddDays(days);

                //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();               

                var tokenHandler = new JwtSecurityTokenHandler();

                // Creiamo l'identità e aggiungiamo le informazioni all'utente che vogliamo loggare
                var claimsIdentity = new ClaimsIdentity(claims);

                //var key = new SymmetricSecurityKey(Encoding.GetEncoding("UTF-8").GetBytes(_configuration.GetSection("JwtSettings:Key").Value ?? ""));
                //var key = new SymmetricSecurityKey(Encoding.GetEncoding("UTF-8").GetBytes("RANDOM_KEY_MUST_NOT_BE_SHARED"));
                var key = new SymmetricSecurityKey(Encoding.GetEncoding("UTF-8").GetBytes(JwtSettings.Key));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                //string issuer = _configuration.GetSection("JwtSettings:Issuer").Value;
                //string audience = _configuration.GetSection("JwtSettings:Audience").Value;
                //string issuer = "https://localhost";
                //string audience = "https://localhost";
                string issuer = JwtSettings.Issuer;
                string audience = JwtSettings.Audience;

                // Creiamo il token JWT
                var token = tokenHandler.CreateJwtSecurityToken(
                        issuer: issuer,
                        audience: audience,
                        subject: claimsIdentity,
                        notBefore: issuedAt,
                        expires: expires,
                        signingCredentials: creds);

                var _token = tokenHandler.WriteToken(token);
                return new OperationResult(() => _token?.Length ?? 0, data: _token);
            }
            catch (Exception ex)
            {
                return new OperationResult(ex);
            }
        }

        public async Task<OperationResult> ValidateToken(string jwtToken)
        {
            try
            {
                //string issuer = _configuration.GetSection("JwtSettings:Issuer").Value;
                //string audience = _configuration.GetSection("JwtSettings:Audience").Value;
                //string issuer = "https://localhost";
                //string audience = "https://localhost";
                string issuer = JwtSettings.Issuer;
                string audience = JwtSettings.Audience;

                //var key = new SymmetricSecurityKey(Encoding.GetEncoding("UTF-8").GetBytes(_configuration.GetSection("JwtSettings:Key").Value ?? ""));
                //var key = new SymmetricSecurityKey(Encoding.GetEncoding("UTF-8").GetBytes("RANDOM_KEY_MUST_NOT_BE_SHARED"));
                var key = new SymmetricSecurityKey(Encoding.GetEncoding("UTF-8").GetBytes(JwtSettings.Key));

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,

                    //ValidateIssuer = false,
                    ValidateIssuer = true,
                    ValidIssuer = issuer, // Specifica l'emittente valido

                    //ValidateAudience = false, 
                    ValidateAudience = true, // Puoi impostare questa opzione a true se vuoi validare anche l'audience
                    ValidAudience = audience, // "YOUR_VALID_AUDIENCE",

                    ValidateLifetime = true,
                    //ClockSkew = TimeSpan.Zero // Opzionale: imposta un clock skew personalizzato
                };

                //jwtToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJEYXJlcyJdLCJuYmYiOjE2OTkzNjMyMTAsImV4cCI6MTY5OTQ0OTYxMCwiaWF0IjoxNjk5MzYzMjEyLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdCIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0In0.65y9yxyT3grEa-VB0i908q_KaLw-EszoSPBJDEhm70yYuv7Zifay47ASOqpjPK0irfyORZ3bpbS6FCQdBhk5qg";

                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);

                return new OperationResult(true, data: JsonConvert.SerializeObject(claimsPrincipal));
            }
            catch (Exception ex)
            {
                return new OperationResult(ex);
            }
        }
    }
}
