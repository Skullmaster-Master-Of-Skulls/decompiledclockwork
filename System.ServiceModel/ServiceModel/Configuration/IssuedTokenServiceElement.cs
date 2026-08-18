using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000633 RID: 1587
	public sealed class IssuedTokenServiceElement : ConfigurationElement
	{
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x000E83A3 File Offset: 0x000E65A3
		[ConfigurationProperty("allowedAudienceUris")]
		public AllowedAudienceUriElementCollection AllowedAudienceUris
		{
			get
			{
				return (AllowedAudienceUriElementCollection)base["allowedAudienceUris"];
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000E83B5 File Offset: 0x000E65B5
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x000E83C7 File Offset: 0x000E65C7
		[ConfigurationProperty("audienceUriMode", DefaultValue = AudienceUriMode.Always)]
		[ServiceModelEnumValidator(typeof(AudienceUriModeValidationHelper))]
		public AudienceUriMode AudienceUriMode
		{
			get
			{
				return (AudienceUriMode)base["audienceUriMode"];
			}
			set
			{
				base["audienceUriMode"] = value;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000E83DA File Offset: 0x000E65DA
		// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x000E83EC File Offset: 0x000E65EC
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

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x000E8409 File Offset: 0x000E6609
		// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x000E841B File Offset: 0x000E661B
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

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x000E842E File Offset: 0x000E662E
		// (set) Token: 0x06003CEB RID: 15595 RVA: 0x000E8440 File Offset: 0x000E6640
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

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x000E8453 File Offset: 0x000E6653
		// (set) Token: 0x06003CED RID: 15597 RVA: 0x000E8465 File Offset: 0x000E6665
		[ConfigurationProperty("trustedStoreLocation", DefaultValue = StoreLocation.LocalMachine)]
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

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000E8478 File Offset: 0x000E6678
		// (set) Token: 0x06003CEF RID: 15599 RVA: 0x000E848A File Offset: 0x000E668A
		[ConfigurationProperty("samlSerializerType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string SamlSerializerType
		{
			get
			{
				return (string)base["samlSerializerType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["samlSerializerType"] = value;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000E84A7 File Offset: 0x000E66A7
		[ConfigurationProperty("knownCertificates")]
		public X509CertificateTrustedIssuerElementCollection KnownCertificates
		{
			get
			{
				return (X509CertificateTrustedIssuerElementCollection)base["knownCertificates"];
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x000E84B9 File Offset: 0x000E66B9
		// (set) Token: 0x06003CF2 RID: 15602 RVA: 0x000E84CB File Offset: 0x000E66CB
		[ConfigurationProperty("allowUntrustedRsaIssuers", DefaultValue = false)]
		public bool AllowUntrustedRsaIssuers
		{
			get
			{
				return (bool)base["allowUntrustedRsaIssuers"];
			}
			set
			{
				base["allowUntrustedRsaIssuers"] = value;
			}
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000E84E0 File Offset: 0x000E66E0
		public void Copy(IssuedTokenServiceElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.SamlSerializerType = from.SamlSerializerType;
			PropertyInformationCollection propertyInformationCollection = from.ElementInformation.Properties;
			if (propertyInformationCollection["knownCertificates"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.KnownCertificates.Clear();
				foreach (object obj in from.KnownCertificates)
				{
					X509CertificateTrustedIssuerElement from2 = (X509CertificateTrustedIssuerElement)obj;
					X509CertificateTrustedIssuerElement x509CertificateTrustedIssuerElement = new X509CertificateTrustedIssuerElement();
					x509CertificateTrustedIssuerElement.Copy(from2);
					this.KnownCertificates.Add(x509CertificateTrustedIssuerElement);
				}
			}
			if (propertyInformationCollection["allowedAudienceUris"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.AllowedAudienceUris.Clear();
				foreach (object obj2 in from.AllowedAudienceUris)
				{
					AllowedAudienceUriElement allowedAudienceUriElement = (AllowedAudienceUriElement)obj2;
					AllowedAudienceUriElement allowedAudienceUriElement2 = new AllowedAudienceUriElement();
					allowedAudienceUriElement2.AllowedAudienceUri = allowedAudienceUriElement.AllowedAudienceUri;
					this.AllowedAudienceUris.Add(allowedAudienceUriElement2);
				}
			}
			this.AllowUntrustedRsaIssuers = from.AllowUntrustedRsaIssuers;
			this.CertificateValidationMode = from.CertificateValidationMode;
			this.AudienceUriMode = from.AudienceUriMode;
			this.CustomCertificateValidatorType = from.CustomCertificateValidatorType;
			this.RevocationMode = from.RevocationMode;
			this.TrustedStoreLocation = from.TrustedStoreLocation;
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000E8688 File Offset: 0x000E6888
		internal void ApplyConfiguration(IssuedTokenServiceCredential issuedToken)
		{
			if (issuedToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuedToken");
			}
			issuedToken.CertificateValidationMode = this.CertificateValidationMode;
			issuedToken.RevocationMode = this.RevocationMode;
			issuedToken.TrustedStoreLocation = this.TrustedStoreLocation;
			issuedToken.AudienceUriMode = this.AudienceUriMode;
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
				issuedToken.CustomCertificateValidator = (X509CertificateValidator)Activator.CreateInstance(type);
			}
			if (!string.IsNullOrEmpty(this.SamlSerializerType))
			{
				Type type2 = Type.GetType(this.SamlSerializerType, true);
				if (!typeof(SamlSerializer).IsAssignableFrom(type2))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidSamlSerializerType", new object[]
					{
						this.SamlSerializerType,
						typeof(SamlSerializer).ToString()
					})));
				}
				issuedToken.SamlSerializer = (SamlSerializer)Activator.CreateInstance(type2);
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["knownCertificates"].ValueOrigin != PropertyValueOrigin.Default)
			{
				foreach (object obj in this.KnownCertificates)
				{
					X509CertificateTrustedIssuerElement x509CertificateTrustedIssuerElement = (X509CertificateTrustedIssuerElement)obj;
					issuedToken.KnownCertificates.Add(SecurityUtils.GetCertificateFromStore(x509CertificateTrustedIssuerElement.StoreName, x509CertificateTrustedIssuerElement.StoreLocation, x509CertificateTrustedIssuerElement.X509FindType, x509CertificateTrustedIssuerElement.FindValue, null));
				}
			}
			if (propertyInformationCollection["allowedAudienceUris"].ValueOrigin != PropertyValueOrigin.Default)
			{
				foreach (object obj2 in this.AllowedAudienceUris)
				{
					AllowedAudienceUriElement allowedAudienceUriElement = (AllowedAudienceUriElement)obj2;
					issuedToken.AllowedAudienceUris.Add(allowedAudienceUriElement.AllowedAudienceUri);
				}
			}
			issuedToken.AllowUntrustedRsaIssuers = this.AllowUntrustedRsaIssuers;
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x000E88D8 File Offset: 0x000E6AD8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("allowedAudienceUris", typeof(AllowedAudienceUriElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("audienceUriMode", typeof(AudienceUriMode), AudienceUriMode.Always, null, new ServiceModelEnumValidator(typeof(AudienceUriModeValidationHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("customCertificateValidatorType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("certificateValidationMode", typeof(X509CertificateValidationMode), X509CertificateValidationMode.ChainTrust, null, new ServiceModelEnumValidator(typeof(X509CertificateValidationModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("revocationMode", typeof(X509RevocationMode), X509RevocationMode.Online, null, new StandardRuntimeEnumValidator(typeof(X509RevocationMode)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("trustedStoreLocation", typeof(StoreLocation), StoreLocation.LocalMachine, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("samlSerializerType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("knownCertificates", typeof(X509CertificateTrustedIssuerElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("allowUntrustedRsaIssuers", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C88 RID: 11400
		private ConfigurationPropertyCollection properties;
	}
}
