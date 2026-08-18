using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x02000348 RID: 840
	public class X509PeerCertificateAuthentication
	{
		// Token: 0x06001E7C RID: 7804 RVA: 0x00070A05 File Offset: 0x0006EC05
		internal X509PeerCertificateAuthentication()
		{
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00070A24 File Offset: 0x0006EC24
		internal X509PeerCertificateAuthentication(X509PeerCertificateAuthentication other)
		{
			this.certificateValidationMode = other.certificateValidationMode;
			this.customCertificateValidator = other.customCertificateValidator;
			this.revocationMode = other.revocationMode;
			this.trustedStoreLocation = other.trustedStoreLocation;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00070A88 File Offset: 0x0006EC88
		internal static X509CertificateValidator DefaultCertificateValidator
		{
			get
			{
				if (X509PeerCertificateAuthentication.defaultCertificateValidator == null)
				{
					bool useMachineContext = false;
					X509PeerCertificateAuthentication.defaultCertificateValidator = X509CertificateValidator.CreatePeerOrChainTrustValidator(useMachineContext, new X509ChainPolicy
					{
						RevocationMode = X509RevocationMode.Online
					});
				}
				return X509PeerCertificateAuthentication.defaultCertificateValidator;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00070ABC File Offset: 0x0006ECBC
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x00070AC4 File Offset: 0x0006ECC4
		public X509CertificateValidationMode CertificateValidationMode
		{
			get
			{
				return this.certificateValidationMode;
			}
			set
			{
				X509CertificateValidationModeHelper.Validate(value);
				this.ThrowIfImmutable();
				this.certificateValidationMode = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00070AD9 File Offset: 0x0006ECD9
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x00070AE1 File Offset: 0x0006ECE1
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.revocationMode;
			}
			set
			{
				this.ThrowIfImmutable();
				this.revocationMode = value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x00070AF0 File Offset: 0x0006ECF0
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x00070AF8 File Offset: 0x0006ECF8
		public StoreLocation TrustedStoreLocation
		{
			get
			{
				return this.trustedStoreLocation;
			}
			set
			{
				this.ThrowIfImmutable();
				this.trustedStoreLocation = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x00070B07 File Offset: 0x0006ED07
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x00070B0F File Offset: 0x0006ED0F
		public X509CertificateValidator CustomCertificateValidator
		{
			get
			{
				return this.customCertificateValidator;
			}
			set
			{
				this.ThrowIfImmutable();
				this.customCertificateValidator = value;
			}
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00070B20 File Offset: 0x0006ED20
		internal bool TryGetCertificateValidator(out X509CertificateValidator validator)
		{
			validator = null;
			if (this.certificateValidationMode == X509CertificateValidationMode.None)
			{
				validator = X509CertificateValidator.None;
			}
			else if (this.certificateValidationMode == X509CertificateValidationMode.PeerTrust)
			{
				validator = X509CertificateValidator.PeerTrust;
			}
			else if (this.certificateValidationMode == X509CertificateValidationMode.Custom)
			{
				validator = this.customCertificateValidator;
			}
			else
			{
				bool useMachineContext = this.trustedStoreLocation == StoreLocation.LocalMachine;
				X509ChainPolicy x509ChainPolicy = new X509ChainPolicy();
				x509ChainPolicy.RevocationMode = this.revocationMode;
				if (this.certificateValidationMode == X509CertificateValidationMode.ChainTrust)
				{
					validator = X509CertificateValidator.CreateChainTrustValidator(useMachineContext, x509ChainPolicy);
				}
				else
				{
					validator = X509CertificateValidator.CreatePeerOrChainTrustValidator(useMachineContext, x509ChainPolicy);
				}
			}
			return validator != null;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x00070BA4 File Offset: 0x0006EDA4
		internal X509CertificateValidator GetCertificateValidator()
		{
			X509CertificateValidator result;
			if (!this.TryGetCertificateValidator(out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MissingCustomCertificateValidator")));
			}
			return result;
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00070BD6 File Offset: 0x0006EDD6
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00070BDF File Offset: 0x0006EDDF
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E99 RID: 7833
		internal const X509CertificateValidationMode DefaultCertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

		// Token: 0x04001E9A RID: 7834
		internal const X509RevocationMode DefaultRevocationMode = X509RevocationMode.Online;

		// Token: 0x04001E9B RID: 7835
		internal const StoreLocation DefaultTrustedStoreLocation = StoreLocation.CurrentUser;

		// Token: 0x04001E9C RID: 7836
		private static X509CertificateValidator defaultCertificateValidator;

		// Token: 0x04001E9D RID: 7837
		private X509CertificateValidationMode certificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

		// Token: 0x04001E9E RID: 7838
		private X509RevocationMode revocationMode = X509RevocationMode.Online;

		// Token: 0x04001E9F RID: 7839
		private StoreLocation trustedStoreLocation = StoreLocation.CurrentUser;

		// Token: 0x04001EA0 RID: 7840
		private X509CertificateValidator customCertificateValidator;

		// Token: 0x04001EA1 RID: 7841
		private bool isReadOnly;
	}
}
