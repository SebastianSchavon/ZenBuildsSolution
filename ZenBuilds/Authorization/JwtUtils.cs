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
        _appSettings = appSettings.Value;
    }

    /// <summary>
    ///     Generate token with create token method from JwtSecurityTokenHandler class
    ///     
    ///     Configure token claims
    ///         Authenticated users id 
    ///         Expire date 
    ///     
    ///     Sign and enconde token with the secret key
    /// </summary>
    /// <param name="user"> user object from authenticate method in user service </param>
    /// <returns> HmacSha2559 algoritm encoded token </returns>
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(3),

            // define signingcredentials: key and algorithm
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(EncodeKey()), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    ///     Validate token with validatetoken method from the JwtSecurityTokenHandler class
    ///     Retrieve and return user id from token claim if the issuersigningkey (secret key) matches
    ///     
    ///     Validate the IssuerSigningKey but not the Issuer or Audience
    /// </summary>
    /// <param name="token"> Token from JwtMiddleware </param>
    /// <returns> Users id from claim </returns>
    public int? ValidateToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var jwtToken = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(EncodeKey()),
                ValidateIssuer = false,
                ValidateAudience = false,

                // token expire exactly at expiration time, instead of 5 minutes later
                ClockSkew = TimeSpan.Zero

            }, out SecurityToken validateToken);

            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            return userId;

        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    ///     Returns the secret key (stored in appsettings) as an byte array
    /// </summary>
    /// <returns></returns>
    private byte[] EncodeKey()
        => Encoding.ASCII.GetBytes(_appSettings.Secret);
}
