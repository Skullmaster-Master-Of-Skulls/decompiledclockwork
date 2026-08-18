using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B8 RID: 1720
	public sealed class X509PeerCertificateAuthenticationElement : ConfigurationElement
	{
		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x000FC158 File Offset: 0x000FA358
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("customCertificateValidatorType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("certificateValidationMode", typeof(X509CertificateValidationMode), X509CertificateValidationMode.PeerOrChainTrust, null, new ServiceModelEnumValidator(typeof(X509CertificateValidationModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("revocationMode", typeof(X509RevocationMode), X509RevocationMode.Online, null, new StandardRuntimeEnumValidator(typeof(X509RevocationMode)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("trustedStoreLocation", typeof(StoreLocation), StoreLocation.CurrentUser, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x000FC24B File Offset: 0x000FA44B
		// (set) Token: 0x060042AA RID: 17066 RVA: 0x000FC25D File Offset: 0x000FA45D
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

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x000FC27A File Offset: 0x000FA47A
		// (set) Token: 0x060042AC RID: 17068 RVA: 0x000FC28C File Offset: 0x000FA48C
		[ConfigurationProperty("certificateValidationMode", DefaultValue = X509CertificateValidationMode.PeerOrChainTrust)]
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

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x000FC29F File Offset: 0x000FA49F
		// (set) Token: 0x060042AE RID: 17070 RVA: 0x000FC2B1 File Offset: 0x000FA4B1
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

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x000FC2C4 File Offset: 0x000FA4C4
		// (set) Token: 0x060042B0 RID: 17072 RVA: 0x000FC2D6 File Offset: 0x000FA4D6
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

		// Token: 0x060042B1 RID: 17073 RVA: 0x000FC2EC File Offset: 0x000FA4EC
		public void Copy(X509PeerCertificateAuthenticationElement from)
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

		// Token: 0x060042B2 RID: 17074 RVA: 0x000FC360 File Offset: 0x000FA560
		internal void ApplyConfiguration(X509PeerCertificateAuthentication cert)
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

		// Token: 0x04002D07 RID: 11527
		private ConfigurationPropertyCollection properties;
	}
}
