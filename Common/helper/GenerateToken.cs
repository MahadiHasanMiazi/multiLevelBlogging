using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Multi_Level_Blogging_System.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Multi_Level_Blogging_System.Common.helper;

public class GenerateToken
{
    public static string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyThatIsAtLeast32CharsLong!"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName),
            new Claim("userType", user.UserType.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: "yourdomain.com",
            audience: "yourdomain.com",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}