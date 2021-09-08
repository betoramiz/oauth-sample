using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api
{
	public class Token
	{
		public static string Generate(string secret, string userName, string issuer, string audience, int expirationTimeInMinutes)
		{
			var key = Encoding.ASCII.GetBytes(secret);
			var claims = new[]
			{
				new Claim("Username", userName),
			};

			var tokenSecurity = new JwtSecurityToken(
				claims: claims,
				// issuer: issuer,
				// audience: audience,
				expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
				signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			);
			
			// var tokenSecurity = new JwtSecurityToken(
			// 	claims: claims,
			// 	issuer: string.Empty,
			// 	expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
			// 	signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			// );

			var token = new JwtSecurityTokenHandler().WriteToken(tokenSecurity);
			return token;
		}
	}
}
