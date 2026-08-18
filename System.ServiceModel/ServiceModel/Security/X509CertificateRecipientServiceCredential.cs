using System;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x02000345 RID: 837
	public sealed class X509CertificateRecipientServiceCredential
	{
		// Token: 0x06001E5F RID: 7775 RVA: 0x0007069D File Offset: 0x0006E89D
		internal X509CertificateRecipientServiceCredential()
		{
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000706A5 File Offset: 0x0006E8A5
		internal X509CertificateRecipientServiceCredential(X509CertificateRecipientServiceCredential other)
		{
			this.certificate = other.certificate;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x000706C5 File Offset: 0x0006E8C5
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x000706CD File Offset: 0x0006E8CD
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

		// Token: 0x06001E63 RID: 7779 RVA: 0x000706DC File Offset: 0x0006E8DC
		public void SetCertificate(string subjectName)
		{
			this.SetCertificate(subjectName, StoreLocation.LocalMachine, StoreName.My);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000706E7 File Offset: 0x0006E8E7
		public void SetCertificate(string subjectName, StoreLocation storeLocation, StoreName storeName)
		{
			if (subjectName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectName");
			}
			this.SetCertificate(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName, subjectName);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00070706 File Offset: 0x0006E906
		public void SetCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue)
		{
			if (findValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("findValue");
			}
			this.ThrowIfImmutable();
			this.certificate = SecurityUtils.GetCertificateFromStore(storeName, storeLocation, findType, findValue, null);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x00070733 File Offset: 0x0006E933
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0007073C File Offset: 0x0006E93C
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E88 RID: 7816
		private X509Certificate2 certificate;

		// Token: 0x04001E89 RID: 7817
		internal const StoreLocation DefaultStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04001E8A RID: 7818
		internal const StoreName DefaultStoreName = StoreName.My;

		// Token: 0x04001E8B RID: 7819
		internal const X509FindType DefaultFindType = X509FindType.FindBySubjectDistinguishedName;

		// Token: 0x04001E8C RID: 7820
		private bool isReadOnly;
	}
}
