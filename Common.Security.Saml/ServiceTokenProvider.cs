using System;
using System.IdentityModel.Tokens;
using System.ServiceModel;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000018 RID: 24
	public class ServiceTokenProvider
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000045BC File Offset: 0x000027BC
		public X509SecurityToken GetIssuerServiceToken(EndpointAddress endpoint, SecurityTokenElement tokenIssuer)
		{
			return tokenIssuer.GetServiceToken(endpoint);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000045D8 File Offset: 0x000027D8
		public X509SecurityToken GetIssuerServiceToken(SecurityTokenElement tokenIssuer)
		{
			return this.GetIssuerServiceToken(new EndpointAddress(tokenIssuer.Name), tokenIssuer);
		}
	}
}
