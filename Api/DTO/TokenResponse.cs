﻿namespace Api.DTO
{
	public class TokenResponse
	{
		public string Token { get; set; }

		public string ExpiresIn { get; set; }

		public string TokenRefresh { get; set; }
	}
}
