using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000162 RID: 354
	public abstract class SamlStatement
	{
		// Token: 0x06000B22 RID: 2850
		public abstract IAuthorizationPolicy CreatePolicy(ClaimSet issuer, SamlSecurityTokenAuthenticator samlAuthenticator);

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B23 RID: 2851
		public abstract bool IsReadOnly { get; }

		// Token: 0x06000B24 RID: 2852
		public abstract void MakeReadOnly();

		// Token: 0x06000B25 RID: 2853
		public abstract void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver);

		// Token: 0x06000B26 RID: 2854
		public abstract void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer);
	}
}
