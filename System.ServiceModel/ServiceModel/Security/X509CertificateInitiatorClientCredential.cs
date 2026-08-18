using System;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x02000342 RID: 834
	public sealed class X509CertificateInitiatorClientCredential
	{
		// Token: 0x06001E40 RID: 7744 RVA: 0x000702D0 File Offset: 0x0006E4D0
		internal X509CertificateInitiatorClientCredential()
		{
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000702D8 File Offset: 0x0006E4D8
		internal X509CertificateInitiatorClientCredential(X509CertificateInitiatorClientCredential other)
		{
			this.certificate = other.certificate;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x000702F8 File Offset: 0x0006E4F8
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x00070300 File Offset: 0x0006E500
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
			set
			{
				this.ThrowIfImmutable();
				this.certificate = value;
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0007030F File Offset: 0x0006E50F
		public void SetCertificate(string subjectName, StoreLocation storeLocation, StoreName storeName)
		{
			if (subjectName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectName");
			}
			this.SetCertificate(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName, subjectName);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0007032E File Offset: 0x0006E52E
		public void SetCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue)
		{
			if (findValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("findValue");
			}
			this.ThrowIfImmutable();
			this.certificate = SecurityUtils.GetCertificateFromStore(storeName, storeLocation, findType, findValue, null);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0007035B File Offset: 0x0006E55B
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x00070364 File Offset: 0x0006E564
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E75 RID: 7797
		internal const StoreLocation DefaultStoreLocation = StoreLocation.CurrentUser;

		// Token: 0x04001E76 RID: 7798
		internal const StoreName DefaultStoreName = StoreName.My;

		// Token: 0x04001E77 RID: 7799
		internal const X509FindType DefaultFindType = X509FindType.FindBySubjectDistinguishedName;

		// Token: 0x04001E78 RID: 7800
		private X509Certificate2 certificate;

		// Token: 0x04001E79 RID: 7801
		private bool isReadOnly;
	}
}
