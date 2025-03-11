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

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName),
            new Claim(ClaimTypes.Role, user.UserType.ToString()),
            
        };
        // var roles = new[] { "Admin", "Editor", "User" };
        //
        // // Add multiple roles as claims
        // foreach (var role in roles)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, role));  // Add each role as a separate claim
        // }

        var token = new JwtSecurityToken(
            issuer: "https://localhost:5067",
            audience: "https://localhost:5067",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}