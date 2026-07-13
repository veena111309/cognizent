using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Web_API_Advanced.Controllers
{
    [ApiController]
    [Route("api/Tokens")]
    [AllowAnonymous]
    public class TokensController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TokensController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/Tokens
        [HttpGet]
        public IActionResult CreateToken()
        {
            // Generating administrative mock token
            string token = GenerateAuthToken("1099", "Manager");
            return Ok(new { Token = token, Schema = "Bearer" });
        }

        private string GenerateAuthToken(string staffId, string staffRole)
        {
            var keyString = _config["Jwt:Key"] ?? "a_very_long_secure_secret_key_used_for_signing_tokens_123456";
            var issuer = _config["Jwt:Issuer"] ?? "EnterpriseAuthSystem";
            var audience = _config["Jwt:Audience"] ?? "EnterpriseServices";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, staffRole),
                new Claim("StaffId", staffId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
