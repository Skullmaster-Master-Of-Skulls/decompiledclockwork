using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001B5 RID: 437
	public class X509SecurityTokenProvider : SecurityTokenProvider, IDisposable
	{
		// Token: 0x06000E3D RID: 3645 RVA: 0x00041568 File Offset: 0x0003F768
		public X509SecurityTokenProvider(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			this.certificate = new X509Certificate2(certificate);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00041590 File Offset: 0x0003F790
		public X509SecurityTokenProvider(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue)
		{
			if (findValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("findValue");
			}
			X509CertificateStore x509CertificateStore = new X509CertificateStore(storeName, storeLocation);
			X509Certificate2Collection x509Certificate2Collection = null;
			try
			{
				x509CertificateStore.Open(OpenFlags.ReadOnly);
				x509Certificate2Collection = x509CertificateStore.Find(findType, findValue, false);
				if (x509Certificate2Collection.Count < 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("CannotFindCert", new object[]
					{
						storeName,
						storeLocation,
						findType,
						findValue
					})));
				}
				if (x509Certificate2Collection.Count > 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("FoundMultipleCerts", new object[]
					{
						storeName,
						storeLocation,
						findType,
						findValue
					})));
				}
				this.certificate = new X509Certificate2(x509Certificate2Collection[0]);
			}
			finally
			{
				SecurityUtils.ResetAllCertificates(x509Certificate2Collection);
				x509CertificateStore.Close();
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x00041698 File Offset: 0x0003F898
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000416A0 File Offset: 0x0003F8A0
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			return new X509SecurityToken(this.certificate);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000416AD File Offset: 0x0003F8AD
		public void Dispose()
		{
			SecurityUtils.ResetCertificate(this.certificate);
		}

		// Token: 0x04000CFC RID: 3324
		private X509Certificate2 certificate;
	}
}
