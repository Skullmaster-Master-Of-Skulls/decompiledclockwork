using System;
using System.Security.Cryptography.X509Certificates;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000016 RID: 22
	public static class X509StoreAdapter
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00004846 File Offset: 0x00002A46
		public static bool Contains(this X509Store certStore, X509Certificate2 certificate)
		{
			return certStore.Certificates.Contains(certificate);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004854 File Offset: 0x00002A54
		public static X509Certificate2 GetCertificateByThumbprint(this X509Store store, string thumbprint, bool validOnly = false)
		{
			X509Certificate2Collection x509Certificate2Collection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly);
			if (x509Certificate2Collection == null || x509Certificate2Collection.Count <= 0)
			{
				return null;
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004888 File Offset: 0x00002A88
		public static X509Certificate2 GetCertificateBySubjectName(this X509Store store, string subjectName, bool validOnly = false)
		{
			X509Certificate2Collection x509Certificate2Collection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, validOnly);
			if (x509Certificate2Collection == null || x509Certificate2Collection.Count <= 0)
			{
				return null;
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000048B9 File Offset: 0x00002AB9
		public static X509Certificate2 GetCertificate(this X509Store store, string findType, object findValue, bool validOnly = false)
		{
			if (findType.Equals("findbythumbprint", StringComparison.OrdinalIgnoreCase))
			{
				return store.GetCertificateByThumbprint(findValue.ToString(), validOnly);
			}
			if (findType.Equals("findbysubjectname", StringComparison.OrdinalIgnoreCase))
			{
				return store.GetCertificateBySubjectName(findValue.ToString(), validOnly);
			}
			return null;
		}
	}
}
