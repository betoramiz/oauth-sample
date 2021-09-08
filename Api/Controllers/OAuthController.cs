using System;
using System.Linq;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Api.Database;
using Api.DTO;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [Route("api/oauth")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OAuthController(IConfiguration configuration) => _configuration = configuration;

        [HttpPost("secure")]
        public IActionResult Secure([FromBody]Credentials request)
        {
            var clientId = Request.Headers["client_id"].ToString();
            var clientSecret = Request.Headers["client_secret"].ToString();
            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("Client id is no valid");
            if (string.IsNullOrEmpty(clientSecret))
                return Unauthorized("Client secret is no valid");
            
            var client_id = _configuration["Client:ClientId"]; 
            var secret = _configuration["Client:Secret"]; 
            var issuer = _configuration["Client:Issuer"]; 
            var expirationTime = int.Parse(_configuration["Client:ExpirationInMinutes"]);
            int expirationTimeRefreshToken = 3600;
            var audience = _configuration["Client:Audience"];

            
            if (clientId != client_id)
                return Unauthorized("invalid client Id");
            
            if (clientSecret != secret)
                return Unauthorized("invalid client secret");
            
            var users = Database.Database
                .GetUsers()
                .FirstOrDefault(u => u.UserName.Trim().ToUpper() == request.Username.Trim().ToUpper() && u.Password == request.Password);

            if (users is null)
                return Unauthorized("Invalid credentials");

            var token = Token.Generate(clientSecret, users.UserName, string.Empty, string.Empty, expirationTime);
            var refreshToken = string.Empty; //Token.Generate(clientSecret, users.UserName, string.Empty, string.Empty, expirationTimeRefreshToken);

            var response = new TokenResponse
            {
                Token = token,
                TokenRefresh = refreshToken
            };
            return Ok(response);
        }
    }
}