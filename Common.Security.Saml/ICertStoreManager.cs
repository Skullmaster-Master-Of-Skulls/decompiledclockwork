using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000008 RID: 8
	public interface ICertStoreManager
	{
		// Token: 0x0600002B RID: 43
		IList<X509Store> OpenSupportedStores();

		// Token: 0x0600002C RID: 44
		IList<X509Store> OpenSupportedStores(OpenFlags mode);

		// Token: 0x0600002D RID: 45
		void CloseSupportedStores(IList<X509Store> certificateStores);

		// Token: 0x0600002E RID: 46
		X509Certificate2 LookupCertFromSupportedStores(IList<X509Store> supportedStores, X509FindType findType, object findValue);

		// Token: 0x0600002F RID: 47
		X509Certificate2Collection LookupCertsFromSupportedStores(IList<X509Store> supportedStores, X509FindType findType, object findValue);
	}
}
