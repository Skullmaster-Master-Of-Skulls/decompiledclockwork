using System;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000012 RID: 18
	public static class Saml2Standard
	{
		// Token: 0x0400003F RID: 63
		public const string Assertion = "Assertion";

		// Token: 0x04000040 RID: 64
		public const string EncryptedAssertion = "EncryptedAssertion";

		// Token: 0x04000041 RID: 65
		public const string AssertionIdPrefix = "SamlSecurityToken-";

		// Token: 0x04000042 RID: 66
		public const string SamlAssertionTargetNamespaceUri = "urn:oasis:names:tc:SAML:2.0:assertion";

		// Token: 0x04000043 RID: 67
		public const string TokenType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0";

		// Token: 0x04000044 RID: 68
		public const string Conditions = "Conditions";

		// Token: 0x04000045 RID: 69
		public const string Issuer = "Issuer";

		// Token: 0x04000046 RID: 70
		public const string SamlIssuerTargetNamespaceUri = "urn:oasis:names:tc:SAML:2.0:assertion";

		// Token: 0x02000027 RID: 39
		public static class ConditionsAttributes
		{
			// Token: 0x04000068 RID: 104
			public const string NotBefore = "NotBefore";

			// Token: 0x04000069 RID: 105
			public const string NotOnOrAfter = "NotOnOrAfter";
		}

		// Token: 0x02000028 RID: 40
		public class SubjectConfirmationMethods
		{
			// Token: 0x0400006A RID: 106
			public const string HolderOfKey = "urn:oasis:names:tc:SAML:2.0:cm:holder-of-key";

			// Token: 0x0400006B RID: 107
			public const string Bearer = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
		}

		// Token: 0x02000029 RID: 41
		public class AuthenticationMethods
		{
			// Token: 0x0400006C RID: 108
			public const string UserNamePassword = "urn:oasis:names:tc:SAML:2.0:ac:classes:Password";

			// Token: 0x0400006D RID: 109
			public const string PasswordProtectedTransport = "urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport";

			// Token: 0x0400006E RID: 110
			public const string TransportLayerSecurity = "urn:oasis:names:tc:SAML:2.0:ac:classes:TLSClient";

			// Token: 0x0400006F RID: 111
			public const string X509Certificate = "urn:oasis:names:tc:SAML:2.0:ac:classes:X509";

			// Token: 0x04000070 RID: 112
			public const string WindowsAuthentication = "urn:federation:authentication:windows";

			// Token: 0x04000071 RID: 113
			public const string Kerberos = "urn:oasis:names:tc:SAML:2.0:ac:classes:Kerberos";
		}

		// Token: 0x0200002A RID: 42
		public class AttributeNameFormat
		{
			// Token: 0x04000072 RID: 114
			public const string URI = "urn:oasis:names:tc:SAML:2.0:attrname-format:uri";

			// Token: 0x04000073 RID: 115
			public const string Basic = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic";

			// Token: 0x04000074 RID: 116
			public const string Unspecified = "urn:oasis:names:tc:SAML:2.0:attrname-format:unspecified";
		}

		// Token: 0x0200002B RID: 43
		public static class BrowserPostProfile
		{
			// Token: 0x04000075 RID: 117
			public const string Response = "Response";

			// Token: 0x04000076 RID: 118
			public const string SamlResponseTargetNamespaceUri = "urn:oasis:names:tc:SAML:2.0:protocol";

			// Token: 0x04000077 RID: 119
			public const string Status = "Status";

			// Token: 0x04000078 RID: 120
			public const string StatusCode = "StatusCode";

			// Token: 0x0200003B RID: 59
			public static class ResponseAttributes
			{
				// Token: 0x040000B8 RID: 184
				public const string Version = "Version";

				// Token: 0x040000B9 RID: 185
				public const string Destination = "Destination";

				// Token: 0x040000BA RID: 186
				public const string ID = "ID";

				// Token: 0x040000BB RID: 187
				public const string IssueInstant = "IssueInstant";

				// Token: 0x040000BC RID: 188
				public const string Value = "Value";

				// Token: 0x040000BD RID: 189
				public const string InResponseTo = "InResponseTo";
			}
		}
	}
}
