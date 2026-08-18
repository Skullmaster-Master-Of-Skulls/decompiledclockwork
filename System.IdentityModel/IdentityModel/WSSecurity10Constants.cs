using System;

namespace System.IdentityModel
{
	// Token: 0x020000BD RID: 189
	internal static class WSSecurity10Constants
	{
		// Token: 0x040004EC RID: 1260
		public const string FragmentBaseAddress = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0";

		// Token: 0x040004ED RID: 1261
		public const string Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		// Token: 0x040004EE RID: 1262
		public const string Prefix = "wsse";

		// Token: 0x040004EF RID: 1263
		public const string Base64EncodingType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";

		// Token: 0x040004F0 RID: 1264
		public const string HexBinaryEncodingType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";

		// Token: 0x040004F1 RID: 1265
		public const string KerberosTokenType1510 = "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510";

		// Token: 0x040004F2 RID: 1266
		public const string KerberosTokenTypeGSS = "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ";

		// Token: 0x040004F3 RID: 1267
		public const string TextEncodingType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text";

		// Token: 0x040004F4 RID: 1268
		public const string X509TokenType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3";

		// Token: 0x040004F5 RID: 1269
		public const string UPTokenPasswordTextValue = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText";

		// Token: 0x02000246 RID: 582
		public static class Attributes
		{
			// Token: 0x04000F8A RID: 3978
			public const string ValueType = "ValueType";

			// Token: 0x04000F8B RID: 3979
			public const string EncodingType = "EncodingType";

			// Token: 0x04000F8C RID: 3980
			public const string URI = "URI";

			// Token: 0x04000F8D RID: 3981
			public const string Type = "Type";
		}

		// Token: 0x02000247 RID: 583
		public static class Elements
		{
			// Token: 0x04000F8E RID: 3982
			public const string BinarySecurityToken = "BinarySecurityToken";

			// Token: 0x04000F8F RID: 3983
			public const string Reference = "Reference";

			// Token: 0x04000F90 RID: 3984
			public const string KeyIdentifier = "KeyIdentifier";

			// Token: 0x04000F91 RID: 3985
			public const string SecurityTokenReference = "SecurityTokenReference";

			// Token: 0x04000F92 RID: 3986
			public const string UsernameToken = "UsernameToken";

			// Token: 0x04000F93 RID: 3987
			public const string Username = "Username";

			// Token: 0x04000F94 RID: 3988
			public const string Password = "Password";

			// Token: 0x04000F95 RID: 3989
			public const string Nonce = "Nonce";

			// Token: 0x04000F96 RID: 3990
			public const string Created = "Created";
		}

		// Token: 0x02000248 RID: 584
		public static class EncodingTypes
		{
			// Token: 0x04000F97 RID: 3991
			public const string Base64 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";

			// Token: 0x04000F98 RID: 3992
			public const string HexBinary = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";

			// Token: 0x04000F99 RID: 3993
			public const string Text = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text";
		}

		// Token: 0x02000249 RID: 585
		public static class FaultCodes
		{
			// Token: 0x04000F9A RID: 3994
			public const string FailedAuthentication = "FailedAuthentication";

			// Token: 0x04000F9B RID: 3995
			public const string FailedCheck = "FailedCheck";

			// Token: 0x04000F9C RID: 3996
			public const string InvalidSecurity = "InvalidSecurity";

			// Token: 0x04000F9D RID: 3997
			public const string InvalidSecurityToken = "InvalidSecurityToken";

			// Token: 0x04000F9E RID: 3998
			public const string MessageExpired = "MessageExpired";

			// Token: 0x04000F9F RID: 3999
			public const string SecurityTokenUnavailable = "SecurityTokenUnavailable";

			// Token: 0x04000FA0 RID: 4000
			public const string UnsupportedAlgorithm = "UnsupportedAlgorithm";

			// Token: 0x04000FA1 RID: 4001
			public const string UnsupportedSecurityToken = "UnsupportedSecurityToken";
		}
	}
}
