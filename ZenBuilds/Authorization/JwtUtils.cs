namespace ZenBuilds.Authorization;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;

public interface IJwtUtils
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);

}

public class JwtUtils : IJwtUtils
{
    private readonly AppSettings _appSettings;

    public JwtUtils(IOptions<AppSettings> appSettings)
    {
        // get key from appsettings.json
        _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();


        // configure token properties
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(3),

            // define signingcredentials: key and algorithm
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(EncodeKey()), SecurityAlgorithms.HmacSha256Signature)
        };

        // create token with defined configurations
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // serializes token into a JWT in Compact Serialization Format.
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                // https://stackoverflow.com/questions/70597009/what-is-the-meaning-of-validateissuer-and-validateaudience-in-jwt

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(EncodeKey()),
                ValidateIssuer = false,
                ValidateAudience = false,

                // token expire exactly at expiration time, instead of 5 minutes later
                ClockSkew = TimeSpan.Zero

            }, out SecurityToken validateToken);

            // när programmet är up and running => debugga för att se över vad validaToken ovan, och jwtToken nedan innehåller
            var jwtToken = (JwtSecurityToken)validateToken;

            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            return userId;

        }
        catch
        {
            return null;
        }
    }

    private byte[] EncodeKey()
        => Encoding.ASCII.GetBytes(_appSettings.Secret);
}
