using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x02000339 RID: 825
	public class IssuedTokenServiceCredential
	{
		// Token: 0x06001DE1 RID: 7649 RVA: 0x0006EA8D File Offset: 0x0006CC8D
		internal IssuedTokenServiceCredential()
		{
			this.allowedAudienceUris = new List<string>();
			this.knownCertificates = new List<X509Certificate2>();
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0006EAC8 File Offset: 0x0006CCC8
		internal IssuedTokenServiceCredential(IssuedTokenServiceCredential other)
		{
			this.audienceUriMode = other.audienceUriMode;
			this.allowedAudienceUris = new List<string>(other.allowedAudienceUris);
			this.samlSerializer = other.samlSerializer;
			this.knownCertificates = new List<X509Certificate2>(other.knownCertificates);
			this.certificateValidationMode = other.certificateValidationMode;
			this.customCertificateValidator = other.customCertificateValidator;
			this.trustedStoreLocation = other.trustedStoreLocation;
			this.revocationMode = other.revocationMode;
			this.allowUntrustedRsaIssuers = other.allowUntrustedRsaIssuers;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0006EB79 File Offset: 0x0006CD79
		public IList<string> AllowedAudienceUris
		{
			get
			{
				if (this.isReadOnly)
				{
					return this.allowedAudienceUris.AsReadOnly();
				}
				return this.allowedAudienceUris;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x0006EB95 File Offset: 0x0006CD95
		// (set) Token: 0x06001DE5 RID: 7653 RVA: 0x0006EB9D File Offset: 0x0006CD9D
		public AudienceUriMode AudienceUriMode
		{
			get
			{
				return this.audienceUriMode;
			}
			set
			{
				this.ThrowIfImmutable();
				AudienceUriModeValidationHelper.Validate(this.audienceUriMode);
				this.audienceUriMode = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0006EBB7 File Offset: 0x0006CDB7
		public IList<X509Certificate2> KnownCertificates
		{
			get
			{
				if (this.isReadOnly)
				{
					return this.knownCertificates.AsReadOnly();
				}
				return this.knownCertificates;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0006EBD3 File Offset: 0x0006CDD3
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0006EBDB File Offset: 0x0006CDDB
		public SamlSerializer SamlSerializer
		{
			get
			{
				return this.samlSerializer;
			}
			set
			{
				this.ThrowIfImmutable();
				this.samlSerializer = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0006EBEA File Offset: 0x0006CDEA
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0006EBF2 File Offset: 0x0006CDF2
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

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0006EC07 File Offset: 0x0006CE07
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x0006EC0F File Offset: 0x0006CE0F
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

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0006EC1E File Offset: 0x0006CE1E
		// (set) Token: 0x06001DEE RID: 7662 RVA: 0x0006EC26 File Offset: 0x0006CE26
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

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0006EC35 File Offset: 0x0006CE35
		// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0006EC3D File Offset: 0x0006CE3D
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

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0006EC4C File Offset: 0x0006CE4C
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x0006EC54 File Offset: 0x0006CE54
		public bool AllowUntrustedRsaIssuers
		{
			get
			{
				return this.allowUntrustedRsaIssuers;
			}
			set
			{
				this.ThrowIfImmutable();
				this.allowUntrustedRsaIssuers = value;
			}
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0006EC64 File Offset: 0x0006CE64
		internal X509CertificateValidator GetCertificateValidator()
		{
			if (this.certificateValidationMode == X509CertificateValidationMode.None)
			{
				return X509CertificateValidator.None;
			}
			if (this.certificateValidationMode == X509CertificateValidationMode.PeerTrust)
			{
				return X509CertificateValidator.PeerTrust;
			}
			if (this.certificateValidationMode == X509CertificateValidationMode.Custom)
			{
				if (this.customCertificateValidator == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MissingCustomCertificateValidator")));
				}
				return this.customCertificateValidator;
			}
			else
			{
				bool useMachineContext = this.trustedStoreLocation == StoreLocation.LocalMachine;
				X509ChainPolicy x509ChainPolicy = new X509ChainPolicy();
				x509ChainPolicy.RevocationMode = this.revocationMode;
				if (this.certificateValidationMode == X509CertificateValidationMode.ChainTrust)
				{
					return X509CertificateValidator.CreateChainTrustValidator(useMachineContext, x509ChainPolicy);
				}
				return X509CertificateValidator.CreatePeerOrChainTrustValidator(useMachineContext, x509ChainPolicy);
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x0006ECF4 File Offset: 0x0006CEF4
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0006ECFD File Offset: 0x0006CEFD
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E45 RID: 7749
		internal const bool DefaultAllowUntrustedRsaIssuers = false;

		// Token: 0x04001E46 RID: 7750
		internal const AudienceUriMode DefaultAudienceUriMode = AudienceUriMode.Always;

		// Token: 0x04001E47 RID: 7751
		internal const X509CertificateValidationMode DefaultCertificateValidationMode = X509CertificateValidationMode.ChainTrust;

		// Token: 0x04001E48 RID: 7752
		internal const X509RevocationMode DefaultRevocationMode = X509RevocationMode.Online;

		// Token: 0x04001E49 RID: 7753
		internal const StoreLocation DefaultTrustedStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04001E4A RID: 7754
		private List<string> allowedAudienceUris;

		// Token: 0x04001E4B RID: 7755
		private AudienceUriMode audienceUriMode = AudienceUriMode.Always;

		// Token: 0x04001E4C RID: 7756
		private List<X509Certificate2> knownCertificates;

		// Token: 0x04001E4D RID: 7757
		private SamlSerializer samlSerializer;

		// Token: 0x04001E4E RID: 7758
		private X509CertificateValidationMode certificateValidationMode = X509CertificateValidationMode.ChainTrust;

		// Token: 0x04001E4F RID: 7759
		private X509RevocationMode revocationMode = X509RevocationMode.Online;

		// Token: 0x04001E50 RID: 7760
		private StoreLocation trustedStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04001E51 RID: 7761
		private X509CertificateValidator customCertificateValidator;

		// Token: 0x04001E52 RID: 7762
		private bool allowUntrustedRsaIssuers;

		// Token: 0x04001E53 RID: 7763
		private bool isReadOnly;
	}
}
