using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Web_API_Advanced.Filters;

namespace Web_API_Advanced
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add Filters globally (Exception filter only, Auth is applied as attribute or custom filter)
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<FileLoggerExceptionFilter>();
            });

            // 2. Configure JWT Authentication
            var keyString = builder.Configuration["Jwt:Key"] ?? "a_very_long_secure_secret_key_used_for_signing_tokens_123456";
            var issuer = builder.Configuration["Jwt:Issuer"] ?? "EnterpriseAuthSystem";
            var audience = builder.Configuration["Jwt:Audience"] ?? "EnterpriseServices";

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString))
                };
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // HTTP pipeline
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
