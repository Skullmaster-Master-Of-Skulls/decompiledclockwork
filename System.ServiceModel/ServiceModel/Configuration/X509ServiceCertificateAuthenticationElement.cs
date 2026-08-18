using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006AE RID: 1710
	public sealed class X509ServiceCertificateAuthenticationElement : ConfigurationElement
	{
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x000FB198 File Offset: 0x000F9398
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("customCertificateValidatorType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("certificateValidationMode", typeof(X509CertificateValidationMode), X509CertificateValidationMode.ChainTrust, null, new ServiceModelEnumValidator(typeof(X509CertificateValidationModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("revocationMode", typeof(X509RevocationMode), X509RevocationMode.Online, null, new StandardRuntimeEnumValidator(typeof(X509RevocationMode)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("trustedStoreLocation", typeof(StoreLocation), StoreLocation.CurrentUser, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x000FB28B File Offset: 0x000F948B
		// (set) Token: 0x0600424C RID: 16972 RVA: 0x000FB29D File Offset: 0x000F949D
		[ConfigurationProperty("customCertificateValidatorType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string CustomCertificateValidatorType
		{
			get
			{
				return (string)base["customCertificateValidatorType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["customCertificateValidatorType"] = value;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x000FB2BA File Offset: 0x000F94BA
		// (set) Token: 0x0600424E RID: 16974 RVA: 0x000FB2CC File Offset: 0x000F94CC
		[ConfigurationProperty("certificateValidationMode", DefaultValue = X509CertificateValidationMode.ChainTrust)]
		[ServiceModelEnumValidator(typeof(X509CertificateValidationModeHelper))]
		public X509CertificateValidationMode CertificateValidationMode
		{
			get
			{
				return (X509CertificateValidationMode)base["certificateValidationMode"];
			}
			set
			{
				base["certificateValidationMode"] = value;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x000FB2DF File Offset: 0x000F94DF
		// (set) Token: 0x06004250 RID: 16976 RVA: 0x000FB2F1 File Offset: 0x000F94F1
		[ConfigurationProperty("revocationMode", DefaultValue = X509RevocationMode.Online)]
		[StandardRuntimeEnumValidator(typeof(X509RevocationMode))]
		public X509RevocationMode RevocationMode
		{
			get
			{
				return (X509RevocationMode)base["revocationMode"];
			}
			set
			{
				base["revocationMode"] = value;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x000FB304 File Offset: 0x000F9504
		// (set) Token: 0x06004252 RID: 16978 RVA: 0x000FB316 File Offset: 0x000F9516
		[ConfigurationProperty("trustedStoreLocation", DefaultValue = StoreLocation.CurrentUser)]
		[StandardRuntimeEnumValidator(typeof(StoreLocation))]
		public StoreLocation TrustedStoreLocation
		{
			get
			{
				return (StoreLocation)base["trustedStoreLocation"];
			}
			set
			{
				base["trustedStoreLocation"] = value;
			}
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x000FB32C File Offset: 0x000F952C
		public void Copy(X509ServiceCertificateAuthenticationElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.CertificateValidationMode = from.CertificateValidationMode;
			this.RevocationMode = from.RevocationMode;
			this.TrustedStoreLocation = from.TrustedStoreLocation;
			this.CustomCertificateValidatorType = from.CustomCertificateValidatorType;
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x000FB3A0 File Offset: 0x000F95A0
		internal void ApplyConfiguration(X509ServiceCertificateAuthentication cert)
		{
			if (cert == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("cert");
			}
			cert.CertificateValidationMode = this.CertificateValidationMode;
			cert.RevocationMode = this.RevocationMode;
			cert.TrustedStoreLocation = this.TrustedStoreLocation;
			if (!string.IsNullOrEmpty(this.CustomCertificateValidatorType))
			{
				Type type = Type.GetType(this.CustomCertificateValidatorType, true);
				if (!typeof(X509CertificateValidator).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidCertificateValidatorType", new object[]
					{
						this.CustomCertificateValidatorType,
						typeof(X509CertificateValidator).ToString()
					})));
				}
				cert.CustomCertificateValidator = (X509CertificateValidator)Activator.CreateInstance(type);
			}
		}

		// Token: 0x04002CFD RID: 11517
		private ConfigurationPropertyCollection properties;
	}
}
