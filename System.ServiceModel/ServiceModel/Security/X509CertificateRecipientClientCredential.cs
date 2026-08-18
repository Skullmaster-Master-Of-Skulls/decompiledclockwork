using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x02000343 RID: 835
	public sealed class X509CertificateRecipientClientCredential
	{
		// Token: 0x06001E48 RID: 7752 RVA: 0x00070388 File Offset: 0x0006E588
		internal X509CertificateRecipientClientCredential()
		{
			this.authentication = new X509ServiceCertificateAuthentication();
			this.scopedCertificates = new Dictionary<Uri, X509Certificate2>();
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000703A8 File Offset: 0x0006E5A8
		internal X509CertificateRecipientClientCredential(X509CertificateRecipientClientCredential other)
		{
			this.authentication = new X509ServiceCertificateAuthentication(other.authentication);
			if (other.sslCertificateAuthentication != null)
			{
				this.sslCertificateAuthentication = new X509ServiceCertificateAuthentication(other.sslCertificateAuthentication);
			}
			this.defaultCertificate = other.defaultCertificate;
			this.scopedCertificates = new Dictionary<Uri, X509Certificate2>();
			foreach (Uri key in other.ScopedCertificates.Keys)
			{
				this.scopedCertificates.Add(key, other.ScopedCertificates[key]);
			}
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x00070464 File Offset: 0x0006E664
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x0007046C File Offset: 0x0006E66C
		public X509Certificate2 DefaultCertificate
		{
			get
			{
				return this.defaultCertificate;
			}
			set
			{
				this.ThrowIfImmutable();
				this.defaultCertificate = value;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0007047B File Offset: 0x0006E67B
		public Dictionary<Uri, X509Certificate2> ScopedCertificates
		{
			get
			{
				return this.scopedCertificates;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x00070483 File Offset: 0x0006E683
		public X509ServiceCertificateAuthentication Authentication
		{
			get
			{
				return this.authentication;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x0007048B File Offset: 0x0006E68B
		// (set) Token: 0x06001E4F RID: 7759 RVA: 0x00070493 File Offset: 0x0006E693
		public X509ServiceCertificateAuthentication SslCertificateAuthentication
		{
			get
			{
				return this.sslCertificateAuthentication;
			}
			set
			{
				this.ThrowIfImmutable();
				this.sslCertificateAuthentication = value;
			}
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x000704A2 File Offset: 0x0006E6A2
		public void SetDefaultCertificate(string subjectName, StoreLocation storeLocation, StoreName storeName)
		{
			if (subjectName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectName");
			}
			this.SetDefaultCertificate(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName, subjectName);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000704C1 File Offset: 0x0006E6C1
		public void SetDefaultCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue)
		{
			if (findValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("findValue");
			}
			this.ThrowIfImmutable();
			this.defaultCertificate = SecurityUtils.GetCertificateFromStore(storeName, storeLocation, findType, findValue, null);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000704EE File Offset: 0x0006E6EE
		public void SetScopedCertificate(string subjectName, StoreLocation storeLocation, StoreName storeName, Uri targetService)
		{
			if (subjectName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectName");
			}
			this.SetScopedCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectDistinguishedName, subjectName, targetService);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00070510 File Offset: 0x0006E710
		public void SetScopedCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue, Uri targetService)
		{
			if (findValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("findValue");
			}
			if (targetService == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("targetService");
			}
			this.ThrowIfImmutable();
			X509Certificate2 certificateFromStore = SecurityUtils.GetCertificateFromStore(storeName, storeLocation, findType, findValue, null);
			this.ScopedCertificates[targetService] = certificateFromStore;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0007056B File Offset: 0x0006E76B
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
			this.Authentication.MakeReadOnly();
			if (this.sslCertificateAuthentication != null)
			{
				this.sslCertificateAuthentication.MakeReadOnly();
			}
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00070592 File Offset: 0x0006E792
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E7A RID: 7802
		private X509ServiceCertificateAuthentication authentication;

		// Token: 0x04001E7B RID: 7803
		private X509ServiceCertificateAuthentication sslCertificateAuthentication;

		// Token: 0x04001E7C RID: 7804
		internal const StoreLocation DefaultStoreLocation = StoreLocation.CurrentUser;

		// Token: 0x04001E7D RID: 7805
		internal const StoreName DefaultStoreName = StoreName.My;

		// Token: 0x04001E7E RID: 7806
		internal const X509FindType DefaultFindType = X509FindType.FindBySubjectDistinguishedName;

		// Token: 0x04001E7F RID: 7807
		private X509Certificate2 defaultCertificate;

		// Token: 0x04001E80 RID: 7808
		private Dictionary<Uri, X509Certificate2> scopedCertificates;

		// Token: 0x04001E81 RID: 7809
		private bool isReadOnly;
	}
}
