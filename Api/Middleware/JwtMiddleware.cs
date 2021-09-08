using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middleware
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IConfiguration _configuration;
		public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
		{
			_next = next;
			_configuration = configuration;
		}

		public async Task Invoke(HttpContext context)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split().Last();
			if (token != null)
			{
				try
				{
					var tokenHandler = new JwtSecurityTokenHandler();
					var key = Encoding.ASCII.GetBytes(_configuration["Client:Secret"]);
					tokenHandler.ValidateToken(token, new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false,
						ClockSkew = TimeSpan.Zero
					}, out SecurityToken validatedToken);
				
					var jwtToken = (JwtSecurityToken)validatedToken;
					context.Items["authenticated"] = "true";
				}
				catch (Exception e)
				{
				}
			}

			await _next(context);
		}
	}
}
