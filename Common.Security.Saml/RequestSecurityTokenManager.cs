using System;
using System.IdentityModel.Protocols.WSTrust;
using System.Security.Cryptography;
using System.ServiceModel;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200000B RID: 11
	public class RequestSecurityTokenManager
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000028E8 File Offset: 0x00000AE8
		public RequestSecurityToken Issue()
		{
			byte[] randomKey = RequestSecurityTokenManager.GetRandomKey();
			return new RequestSecurityToken("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue", randomKey.Length * 8, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer", new NonceToken(randomKey), new EndpointAddress("https://clockworks.ca"), new Lifetime(DateTime.Now, DateTime.Now.AddHours(2.0)));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000294C File Offset: 0x00000B4C
		private static byte[] GetRandomKey()
		{
			byte[] array = new byte[32];
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			rngcryptoServiceProvider.GetBytes(array);
			return array;
		}

		// Token: 0x04000015 RID: 21
		public const string TokenType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0";
	}
}
