using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodePulse.API.Repositories.Implmentation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //create clains for the roles
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //use the claims to find the HWT Security Parameters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            //use this key to define credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //define token
            var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"], audience: configuration["Jwt:Audience"], claims: claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);


            //return token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
