using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LMT.Common.Jwt
{
    public static class Extensions
    {
        public static void AddJwt(this IServiceCollection service, IConfiguration configuration)
        {
            var options = new JwtOptions();
            var section = configuration.GetSection("JwtOptions");
            section.Bind(options);
            service.Configure<JwtOptions>(section);
            service.AddSingleton<IJwtHandler, JwtHandler>();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.SecretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!  
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                // Validate the JWT Issuer (iss) claim 
                ValidateIssuer = true,
                ValidIssuer = options.Issuer,
                // Validate the JWT Audience (aud) claim  
                ValidateAudience = true,
                ValidAudience = options.Audience,
                // Validate the token expiry 
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };
            service.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "Bearer";
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}
