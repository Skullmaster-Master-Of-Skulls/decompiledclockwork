using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000177 RID: 375
	public static class SecurityTokenTypes
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00037125 File Offset: 0x00035325
		public static string UserName
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/UserName";
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0003712C File Offset: 0x0003532C
		public static string X509Certificate
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/X509Certificate";
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00037133 File Offset: 0x00035333
		public static string Kerberos
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/Kerberos";
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0003713A File Offset: 0x0003533A
		public static string Saml
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/Saml";
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00037141 File Offset: 0x00035341
		public static string Rsa
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/Rsa";
			}
		}

		// Token: 0x04000C3E RID: 3134
		private const string Namespace = "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens";

		// Token: 0x04000C3F RID: 3135
		private const string userName = "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/UserName";

		// Token: 0x04000C40 RID: 3136
		private const string x509Certificate = "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/X509Certificate";

		// Token: 0x04000C41 RID: 3137
		private const string kerberos = "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/Kerberos";

		// Token: 0x04000C42 RID: 3138
		private const string saml = "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/Saml";

		// Token: 0x04000C43 RID: 3139
		private const string rsa = "http://schemas.microsoft.com/ws/2006/05/identitymodel/tokens/Rsa";

		// Token: 0x04000C44 RID: 3140
		internal const string SamlTokenProfile11 = "urn:oasis:names:tc:SAML:1.0:assertion";

		// Token: 0x04000C45 RID: 3141
		internal const string Saml2TokenProfile11 = "urn:oasis:names:tc:SAML:2.0:assertion";

		// Token: 0x04000C46 RID: 3142
		internal const string OasisWssSamlTokenProfile11 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";

		// Token: 0x04000C47 RID: 3143
		internal const string OasisWssSaml2TokenProfile11 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0";
	}
}
