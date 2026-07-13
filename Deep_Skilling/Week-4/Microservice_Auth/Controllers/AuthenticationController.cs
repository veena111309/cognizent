using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microservice_Auth.Models;

namespace Microservice_Auth.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredential credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify Mock Credentials
            if (IsValidUser(credentials))
            {
                var token = GenerateSecureToken(credentials.Username);
                return Ok(new { Token = token, Schema = "Bearer" });
            }

            return Unauthorized(new { Error = "Authentication Failed", Message = "Incorrect username or password." });
        }

        private bool IsValidUser(UserCredential credentials)
        {
            return credentials.Username.Equals("developer", StringComparison.OrdinalIgnoreCase) 
                && credentials.Password == "secureservice2026";
        }

        private string GenerateSecureToken(string username)
        {
            var keyString = _config["Jwt:Key"] ?? "this_is_our_highly_secure_secret_key_for_microservices_encryption_2026_key";
            var issuer = _config["Jwt:Issuer"] ?? "MicroserviceAuthServer";
            var audience = _config["Jwt:Audience"] ?? "MicroserviceClients";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Developer"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
