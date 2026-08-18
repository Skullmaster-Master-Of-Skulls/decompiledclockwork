using System;

namespace System.Security.Claims
{
	// Token: 0x0200001A RID: 26
	internal static class AuthenticationTypeMaps
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004060 File Offset: 0x00002260
		public static string Denormalize(string normalizedAuthenticationMethod, AuthenticationTypeMaps.Mapping[] mappingTable)
		{
			foreach (AuthenticationTypeMaps.Mapping mapping in mappingTable)
			{
				if (StringComparer.Ordinal.Equals(normalizedAuthenticationMethod, mapping.Normalized))
				{
					return mapping.Unnormalized;
				}
			}
			return normalizedAuthenticationMethod;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000040A0 File Offset: 0x000022A0
		public static string Normalize(string unnormalizedAuthenticationMethod, AuthenticationTypeMaps.Mapping[] mappingTable)
		{
			foreach (AuthenticationTypeMaps.Mapping mapping in mappingTable)
			{
				if (StringComparer.Ordinal.Equals(unnormalizedAuthenticationMethod, mapping.Unnormalized))
				{
					return mapping.Normalized;
				}
			}
			return unnormalizedAuthenticationMethod;
		}

		// Token: 0x040000BA RID: 186
		public static AuthenticationTypeMaps.Mapping[] Saml = new AuthenticationTypeMaps.Mapping[]
		{
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/hardwaretoken", "URI:urn:oasis:names:tc:SAML:1.0:am:HardwareToken"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/kerberos", "urn:ietf:rfc:1510"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/password", "urn:oasis:names:tc:SAML:1.0:am:password"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/pgp", "urn:oasis:names:tc:SAML:1.0:am:PGP"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/secureremotepassword", "urn:ietf:rfc:2945"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/signature", "urn:ietf:rfc:3075"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/spki", "urn:oasis:names:tc:SAML:1.0:am:SPKI"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/tlsclient", "urn:ietf:rfc:2246"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/unspecified", "urn:oasis:names:tc:SAML:1.0:am:unspecified"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/windows", "urn:federation:authentication:windows"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/x509", "urn:oasis:names:tc:SAML:1.0:am:X509-PKI"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/xkms", "urn:oasis:names:tc:SAML:1.0:am:XKMS")
		};

		// Token: 0x040000BB RID: 187
		public static AuthenticationTypeMaps.Mapping[] Saml2 = new AuthenticationTypeMaps.Mapping[]
		{
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/kerberos", "urn:oasis:names:tc:SAML:2.0:ac:classes:Kerberos"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/password", "urn:oasis:names:tc:SAML:2.0:ac:classes:Password"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/pgp", "urn:oasis:names:tc:SAML:2.0:ac:classes:PGP"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/secureremotepassword", "urn:oasis:names:tc:SAML:2.0:ac:classes:SecureRemotePassword"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/signature", "urn:oasis:names:tc:SAML:2.0:ac:classes:XMLDSig"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/spki", "urn:oasis:names:tc:SAML:2.0:ac:classes:SPKI"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/smartcard", "urn:oasis:names:tc:SAML:2.0:ac:classes:Smartcard"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/smartcardpki", "urn:oasis:names:tc:SAML:2.0:ac:classes:SmartcardPKI"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/tlsclient", "urn:oasis:names:tc:SAML:2.0:ac:classes:TLSClient"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/unspecified", "urn:oasis:names:tc:SAML:2.0:ac:classes:Unspecified"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/x509", "urn:oasis:names:tc:SAML:2.0:ac:classes:X509"),
			new AuthenticationTypeMaps.Mapping("http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/windows", "urn:federation:authentication:windows")
		};

		// Token: 0x02000227 RID: 551
		public struct Mapping
		{
			// Token: 0x060011E4 RID: 4580 RVA: 0x0004E53F File Offset: 0x0004C73F
			public Mapping(string normalized, string unnormalized)
			{
				this.Normalized = normalized;
				this.Unnormalized = unnormalized;
			}

			// Token: 0x04000F03 RID: 3843
			public string Normalized;

			// Token: 0x04000F04 RID: 3844
			public string Unnormalized;
		}
	}
}
