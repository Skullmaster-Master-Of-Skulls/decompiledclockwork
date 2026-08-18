using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A29 RID: 2601
	internal class PeerX509TokenProvider : X509SecurityTokenProvider
	{
		// Token: 0x06006746 RID: 26438 RVA: 0x00181B19 File Offset: 0x0017FD19
		public PeerX509TokenProvider(X509CertificateValidator validator, X509Certificate2 credential) : base(credential)
		{
			this.validator = validator;
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x00181B2C File Offset: 0x0017FD2C
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			X509SecurityToken x509SecurityToken = (X509SecurityToken)base.GetTokenCore(timeout);
			if (this.validator != null)
			{
				this.validator.Validate(x509SecurityToken.Certificate);
			}
			return x509SecurityToken;
		}

		// Token: 0x04003B46 RID: 15174
		private X509CertificateValidator validator;
	}
}
