using System;
using System.IdentityModel.Configuration;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200017C RID: 380
	public class SecurityTokenHandlerConfiguration
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00037F46 File Offset: 0x00036146
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x00037F4E File Offset: 0x0003614E
		public AudienceRestriction AudienceRestriction
		{
			get
			{
				return this.audienceRestriction;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.audienceRestriction = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00037F6A File Offset: 0x0003616A
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x00037F72 File Offset: 0x00036172
		public X509CertificateValidator CertificateValidator
		{
			get
			{
				return this.certificateValidator;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.certificateValidator = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00037F8E File Offset: 0x0003618E
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00037F96 File Offset: 0x00036196
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.revocationMode;
			}
			set
			{
				this.revocationMode = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00037F9F File Offset: 0x0003619F
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00037FA7 File Offset: 0x000361A7
		public StoreLocation TrustedStoreLocation
		{
			get
			{
				return this.trustedStoreLocation;
			}
			set
			{
				this.trustedStoreLocation = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00037FB0 File Offset: 0x000361B0
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x00037FB8 File Offset: 0x000361B8
		public X509CertificateValidationMode CertificateValidationMode
		{
			get
			{
				return this.certificateValidationMode;
			}
			set
			{
				this.certificateValidationMode = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00037FC1 File Offset: 0x000361C1
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x00037FC9 File Offset: 0x000361C9
		public bool DetectReplayedTokens
		{
			get
			{
				return this.detectReplayedTokens;
			}
			set
			{
				this.detectReplayedTokens = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00037FD2 File Offset: 0x000361D2
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00037FDA File Offset: 0x000361DA
		public IssuerNameRegistry IssuerNameRegistry
		{
			get
			{
				return this.issuerNameRegistry;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.issuerNameRegistry = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00037FF6 File Offset: 0x000361F6
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00037FFE File Offset: 0x000361FE
		public SecurityTokenResolver IssuerTokenResolver
		{
			get
			{
				return this.issuerTokenResolver;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.issuerTokenResolver = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0003801A File Offset: 0x0003621A
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00038022 File Offset: 0x00036222
		public TimeSpan MaxClockSkew
		{
			get
			{
				return this.maxClockSkew;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", value, SR.GetString("ID2070"));
				}
				this.maxClockSkew = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00038053 File Offset: 0x00036253
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0003805B File Offset: 0x0003625B
		public bool SaveBootstrapContext
		{
			get
			{
				return this.saveBootstrapContext;
			}
			set
			{
				this.saveBootstrapContext = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00038064 File Offset: 0x00036264
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x0003806C File Offset: 0x0003626C
		public SecurityTokenResolver ServiceTokenResolver
		{
			get
			{
				return this.serviceTokenResolver;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.serviceTokenResolver = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00038088 File Offset: 0x00036288
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00038090 File Offset: 0x00036290
		public IdentityModelCaches Caches
		{
			get
			{
				return this.caches;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.caches = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000380AC File Offset: 0x000362AC
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x000380B4 File Offset: 0x000362B4
		public TimeSpan TokenReplayCacheExpirationPeriod
		{
			get
			{
				return this.tokenReplayCacheExpirationPeriod;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", value, SR.GetString("ID0016"));
				}
				this.tokenReplayCacheExpirationPeriod = value;
			}
		}

		// Token: 0x04000C56 RID: 3158
		public static readonly bool DefaultDetectReplayedTokens;

		// Token: 0x04000C57 RID: 3159
		public static readonly IssuerNameRegistry DefaultIssuerNameRegistry = new ConfigurationBasedIssuerNameRegistry();

		// Token: 0x04000C58 RID: 3160
		public static readonly SecurityTokenResolver DefaultIssuerTokenResolver = System.IdentityModel.Tokens.IssuerTokenResolver.DefaultInstance;

		// Token: 0x04000C59 RID: 3161
		public static readonly TimeSpan DefaultMaxClockSkew = new TimeSpan(0, 5, 0);

		// Token: 0x04000C5A RID: 3162
		public static readonly bool DefaultSaveBootstrapContext;

		// Token: 0x04000C5B RID: 3163
		public static readonly TimeSpan DefaultTokenReplayCacheExpirationPeriod = TimeSpan.MaxValue;

		// Token: 0x04000C5C RID: 3164
		public static readonly X509CertificateValidationMode DefaultCertificateValidationMode = IdentityConfiguration.DefaultCertificateValidationMode;

		// Token: 0x04000C5D RID: 3165
		public static readonly X509RevocationMode DefaultRevocationMode = IdentityConfiguration.DefaultRevocationMode;

		// Token: 0x04000C5E RID: 3166
		public static readonly StoreLocation DefaultTrustedStoreLocation = IdentityConfiguration.DefaultTrustedStoreLocation;

		// Token: 0x04000C5F RID: 3167
		private StoreLocation trustedStoreLocation = SecurityTokenHandlerConfiguration.DefaultTrustedStoreLocation;

		// Token: 0x04000C60 RID: 3168
		private X509RevocationMode revocationMode = SecurityTokenHandlerConfiguration.DefaultRevocationMode;

		// Token: 0x04000C61 RID: 3169
		private X509CertificateValidationMode certificateValidationMode = SecurityTokenHandlerConfiguration.DefaultCertificateValidationMode;

		// Token: 0x04000C62 RID: 3170
		public static readonly X509CertificateValidator DefaultCertificateValidator = X509Util.CreateCertificateValidator(SecurityTokenHandlerConfiguration.DefaultCertificateValidationMode, SecurityTokenHandlerConfiguration.DefaultRevocationMode, SecurityTokenHandlerConfiguration.DefaultTrustedStoreLocation);

		// Token: 0x04000C63 RID: 3171
		private AudienceRestriction audienceRestriction = new AudienceRestriction();

		// Token: 0x04000C64 RID: 3172
		private X509CertificateValidator certificateValidator = SecurityTokenHandlerConfiguration.DefaultCertificateValidator;

		// Token: 0x04000C65 RID: 3173
		private bool detectReplayedTokens = SecurityTokenHandlerConfiguration.DefaultDetectReplayedTokens;

		// Token: 0x04000C66 RID: 3174
		private IssuerNameRegistry issuerNameRegistry = SecurityTokenHandlerConfiguration.DefaultIssuerNameRegistry;

		// Token: 0x04000C67 RID: 3175
		private SecurityTokenResolver issuerTokenResolver = SecurityTokenHandlerConfiguration.DefaultIssuerTokenResolver;

		// Token: 0x04000C68 RID: 3176
		private TimeSpan maxClockSkew = SecurityTokenHandlerConfiguration.DefaultMaxClockSkew;

		// Token: 0x04000C69 RID: 3177
		private bool saveBootstrapContext = SecurityTokenHandlerConfiguration.DefaultSaveBootstrapContext;

		// Token: 0x04000C6A RID: 3178
		private SecurityTokenResolver serviceTokenResolver = EmptySecurityTokenResolver.Instance;

		// Token: 0x04000C6B RID: 3179
		private TimeSpan tokenReplayCacheExpirationPeriod = SecurityTokenHandlerConfiguration.DefaultTokenReplayCacheExpirationPeriod;

		// Token: 0x04000C6C RID: 3180
		private IdentityModelCaches caches = new IdentityModelCaches();
	}
}
