using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FeedApp.Data
{
    public class AuthenticationTokenProvider
    {
        private readonly string _authenticationTokenSymmetricSecurityKey;

        public AuthenticationTokenProvider(IOptions<Config> config)
        {
            _authenticationTokenSymmetricSecurityKey = config.Value.AuthenticationTokenSymmetricSecurityKey;
        }

        public string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_authenticationTokenSymmetricSecurityKey));

            var tokenHeader = new JwtHeader(new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityToken(tokenHeader, new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
