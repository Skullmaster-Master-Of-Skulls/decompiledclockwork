using System;
using System.Security.Cryptography.X509Certificates;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000003 RID: 3
	public class CertificateStore
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000023F8 File Offset: 0x000005F8
		public X509Certificate2Collection GetInstalledCertificates(StoreLocation storeLocation, StoreName storeName)
		{
			X509Store x509Store = null;
			X509Certificate2Collection certificates;
			try
			{
				x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.ReadOnly);
				certificates = x509Store.Certificates;
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return certificates;
		}
	}
}
