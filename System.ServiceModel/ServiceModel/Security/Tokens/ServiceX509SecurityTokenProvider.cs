using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200038F RID: 911
	internal class ServiceX509SecurityTokenProvider : X509SecurityTokenProvider
	{
		// Token: 0x060021CE RID: 8654 RVA: 0x0007C0D1 File Offset: 0x0007A2D1
		public ServiceX509SecurityTokenProvider(X509Certificate2 certificate) : base(certificate)
		{
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0007C0DA File Offset: 0x0007A2DA
		public ServiceX509SecurityTokenProvider(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue) : base(storeLocation, storeName, findType, findValue)
		{
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0007C0E7 File Offset: 0x0007A2E7
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			return new X509SecurityToken(base.Certificate, false, false);
		}
	}
}
