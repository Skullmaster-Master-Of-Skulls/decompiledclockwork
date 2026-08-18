using System;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x02000344 RID: 836
	public sealed class X509CertificateInitiatorServiceCredential
	{
		// Token: 0x06001E56 RID: 7766 RVA: 0x000705B6 File Offset: 0x0006E7B6
		internal X509CertificateInitiatorServiceCredential()
		{
			this.authentication = new X509ClientCertificateAuthentication();
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000705C9 File Offset: 0x0006E7C9
		internal X509CertificateInitiatorServiceCredential(X509CertificateInitiatorServiceCredential other)
		{
			this.certificate = other.certificate;
			this.authentication = new X509ClientCertificateAuthentication(other.authentication);
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000705FA File Offset: 0x0006E7FA
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x00070602 File Offset: 0x0006E802
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x00070611 File Offset: 0x0006E811
		public X509ClientCertificateAuthentication Authentication
		{
			get
			{
				return this.authentication;
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00070619 File Offset: 0x0006E819
		public void SetCertificate(string subjectName, StoreLocation storeLocation, StoreName storeName)
		{
			if (subjectName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectName");
			}
			this.SetCertificate(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName, subjectName);
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00070638 File Offset: 0x0006E838
		public void SetCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue)
		{
			if (findValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("findValue");
			}
			this.ThrowIfImmutable();
			this.certificate = SecurityUtils.GetCertificateFromStore(storeName, storeLocation, findType, findValue, null);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00070665 File Offset: 0x0006E865
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
			this.Authentication.MakeReadOnly();
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00070679 File Offset: 0x0006E879
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E82 RID: 7810
		internal const StoreLocation DefaultStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04001E83 RID: 7811
		internal const StoreName DefaultStoreName = StoreName.My;

		// Token: 0x04001E84 RID: 7812
		internal const X509FindType DefaultFindType = X509FindType.FindBySubjectDistinguishedName;

		// Token: 0x04001E85 RID: 7813
		private X509Certificate2 certificate;

		// Token: 0x04001E86 RID: 7814
		private X509ClientCertificateAuthentication authentication;

		// Token: 0x04001E87 RID: 7815
		private bool isReadOnly;
	}
}
