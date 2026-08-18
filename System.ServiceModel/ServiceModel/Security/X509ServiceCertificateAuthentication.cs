using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x02000349 RID: 841
	public sealed class X509ServiceCertificateAuthentication
	{
		// Token: 0x06001E8B RID: 7819 RVA: 0x00070C03 File Offset: 0x0006EE03
		public X509ServiceCertificateAuthentication()
		{
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00070C20 File Offset: 0x0006EE20
		internal X509ServiceCertificateAuthentication(X509ServiceCertificateAuthentication other)
		{
			this.certificateValidationMode = other.certificateValidationMode;
			this.customCertificateValidator = other.customCertificateValidator;
			this.revocationMode = other.revocationMode;
			this.trustedStoreLocation = other.trustedStoreLocation;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00070C84 File Offset: 0x0006EE84
		internal static X509CertificateValidator DefaultCertificateValidator
		{
			get
			{
				if (X509ServiceCertificateAuthentication.defaultCertificateValidator == null)
				{
					bool useMachineContext = false;
					X509ChainPolicy x509ChainPolicy = new X509ChainPolicy();
					if (!ServiceModelAppSettings.UseLegacyCertificateUsagePolicy)
					{
						x509ChainPolicy.ApplicationPolicy.Add(X509ServiceCertificateAuthentication.serverAuthOid);
					}
					x509ChainPolicy.RevocationMode = X509RevocationMode.Online;
					X509ServiceCertificateAuthentication.defaultCertificateValidator = X509CertificateValidator.CreateChainTrustValidator(useMachineContext, x509ChainPolicy);
				}
				return X509ServiceCertificateAuthentication.defaultCertificateValidator;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x00070CD0 File Offset: 0x0006EED0
		// (set) Token: 0x06001E8F RID: 7823 RVA: 0x00070CD8 File Offset: 0x0006EED8
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

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x00070CED File Offset: 0x0006EEED
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x00070CF5 File Offset: 0x0006EEF5
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

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x00070D04 File Offset: 0x0006EF04
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x00070D0C File Offset: 0x0006EF0C
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

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x00070D1B File Offset: 0x0006EF1B
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x00070D23 File Offset: 0x0006EF23
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

		// Token: 0x06001E96 RID: 7830 RVA: 0x00070D34 File Offset: 0x0006EF34
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
				if (!ServiceModelAppSettings.UseLegacyCertificateUsagePolicy)
				{
					x509ChainPolicy.ApplicationPolicy.Add(X509ServiceCertificateAuthentication.serverAuthOid);
				}
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

		// Token: 0x06001E97 RID: 7831 RVA: 0x00070DD0 File Offset: 0x0006EFD0
		internal X509CertificateValidator GetCertificateValidator()
		{
			X509CertificateValidator result;
			if (!this.TryGetCertificateValidator(out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MissingCustomCertificateValidator")));
			}
			return result;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x00070E02 File Offset: 0x0006F002
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00070E0B File Offset: 0x0006F00B
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001EA2 RID: 7842
		internal const X509CertificateValidationMode DefaultCertificateValidationMode = X509CertificateValidationMode.ChainTrust;

		// Token: 0x04001EA3 RID: 7843
		internal const X509RevocationMode DefaultRevocationMode = X509RevocationMode.Online;

		// Token: 0x04001EA4 RID: 7844
		internal const StoreLocation DefaultTrustedStoreLocation = StoreLocation.CurrentUser;

		// Token: 0x04001EA5 RID: 7845
		private static X509CertificateValidator defaultCertificateValidator;

		// Token: 0x04001EA6 RID: 7846
		private static readonly Oid serverAuthOid = new Oid("1.3.6.1.5.5.7.3.1", "1.3.6.1.5.5.7.3.1");

		// Token: 0x04001EA7 RID: 7847
		private X509CertificateValidationMode certificateValidationMode = X509CertificateValidationMode.ChainTrust;

		// Token: 0x04001EA8 RID: 7848
		private X509RevocationMode revocationMode = X509RevocationMode.Online;

		// Token: 0x04001EA9 RID: 7849
		private StoreLocation trustedStoreLocation = StoreLocation.CurrentUser;

		// Token: 0x04001EAA RID: 7850
		private X509CertificateValidator customCertificateValidator;

		// Token: 0x04001EAB RID: 7851
		private bool isReadOnly;
	}
}
