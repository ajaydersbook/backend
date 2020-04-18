using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LMT.Common.Jwt
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _options;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public JsonWebToken Create(/*int userId,*/ string Email, string UserName)
        {
            var now = DateTime.UtcNow;
            var Role = "Role";
            var claims = new Claim[]
            {
               //new Claim(Role, groupId),
                new Claim(ClaimTypes.Email, Email),
                 // new Claim(JwtRegisteredClaimNames.Role, groupId),
                //new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_options.ExpiryMinutes)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                Token = encodedJwt,
                Expires = (int)TimeSpan.FromMinutes(_options.ExpiryMinutes).TotalSeconds,
                RefreshToken = GetRefreshToken(61, true),
                RefreshTokenExpires = (int)TimeSpan.FromMinutes(_options.RefreshTokenExpiryMinutes).TotalSeconds
            };
        }


        private string GetRefreshToken(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}
