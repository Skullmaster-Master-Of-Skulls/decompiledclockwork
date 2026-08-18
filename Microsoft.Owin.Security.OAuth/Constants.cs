using System;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000012 RID: 18
	internal static class Constants
	{
		// Token: 0x02000013 RID: 19
		public static class Parameters
		{
			// Token: 0x04000038 RID: 56
			public const string ResponseType = "response_type";

			// Token: 0x04000039 RID: 57
			public const string GrantType = "grant_type";

			// Token: 0x0400003A RID: 58
			public const string ClientId = "client_id";

			// Token: 0x0400003B RID: 59
			public const string ClientSecret = "client_secret";

			// Token: 0x0400003C RID: 60
			public const string RedirectUri = "redirect_uri";

			// Token: 0x0400003D RID: 61
			public const string Scope = "scope";

			// Token: 0x0400003E RID: 62
			public const string State = "state";

			// Token: 0x0400003F RID: 63
			public const string Code = "code";

			// Token: 0x04000040 RID: 64
			public const string RefreshToken = "refresh_token";

			// Token: 0x04000041 RID: 65
			public const string Username = "username";

			// Token: 0x04000042 RID: 66
			public const string Password = "password";

			// Token: 0x04000043 RID: 67
			public const string Error = "error";

			// Token: 0x04000044 RID: 68
			public const string ErrorDescription = "error_description";

			// Token: 0x04000045 RID: 69
			public const string ErrorUri = "error_uri";

			// Token: 0x04000046 RID: 70
			public const string ExpiresIn = "expires_in";

			// Token: 0x04000047 RID: 71
			public const string AccessToken = "access_token";

			// Token: 0x04000048 RID: 72
			public const string TokenType = "token_type";

			// Token: 0x04000049 RID: 73
			public const string ResponseMode = "response_mode";
		}

		// Token: 0x02000014 RID: 20
		public static class ResponseTypes
		{
			// Token: 0x0400004A RID: 74
			public const string Code = "code";

			// Token: 0x0400004B RID: 75
			public const string Token = "token";
		}

		// Token: 0x02000015 RID: 21
		public static class GrantTypes
		{
			// Token: 0x0400004C RID: 76
			public const string AuthorizationCode = "authorization_code";

			// Token: 0x0400004D RID: 77
			public const string ClientCredentials = "client_credentials";

			// Token: 0x0400004E RID: 78
			public const string RefreshToken = "refresh_token";

			// Token: 0x0400004F RID: 79
			public const string Password = "password";
		}

		// Token: 0x02000016 RID: 22
		public static class TokenTypes
		{
			// Token: 0x04000050 RID: 80
			public const string Bearer = "bearer";
		}

		// Token: 0x02000017 RID: 23
		public static class Errors
		{
			// Token: 0x04000051 RID: 81
			public const string InvalidRequest = "invalid_request";

			// Token: 0x04000052 RID: 82
			public const string InvalidClient = "invalid_client";

			// Token: 0x04000053 RID: 83
			public const string InvalidGrant = "invalid_grant";

			// Token: 0x04000054 RID: 84
			public const string UnsupportedResponseType = "unsupported_response_type";

			// Token: 0x04000055 RID: 85
			public const string UnsupportedGrantType = "unsupported_grant_type";

			// Token: 0x04000056 RID: 86
			public const string UnauthorizedClient = "unauthorized_client";
		}

		// Token: 0x02000018 RID: 24
		public static class Extra
		{
			// Token: 0x04000057 RID: 87
			public const string ClientId = "client_id";

			// Token: 0x04000058 RID: 88
			public const string RedirectUri = "redirect_uri";
		}

		// Token: 0x02000019 RID: 25
		public static class ResponseModes
		{
			// Token: 0x04000059 RID: 89
			public const string FormPost = "form_post";
		}
	}
}
