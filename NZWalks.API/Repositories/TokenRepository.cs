using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                // Adding the email claim
                new Claim(ClaimTypes.Email, user.Email),
                // Adding the user ID claim
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Adding roles to claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Generate the signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // Removed the space
                audience: _configuration["Jwt:Audience"], // Removed the space
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(59), // Using UTC for token expiry
                signingCredentials: credentials);

            // Return the serialized JWT
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
