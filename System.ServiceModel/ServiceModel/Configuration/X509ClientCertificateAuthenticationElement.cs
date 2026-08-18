using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A6 RID: 1702
	public sealed class X509ClientCertificateAuthenticationElement : ConfigurationElement
	{
		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x000F9CF8 File Offset: 0x000F7EF8
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
						new ConfigurationProperty("trustedStoreLocation", typeof(StoreLocation), StoreLocation.LocalMachine, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("includeWindowsGroups", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("mapClientCertificateToWindowsAccount", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x000F9E31 File Offset: 0x000F8031
		// (set) Token: 0x060041F0 RID: 16880 RVA: 0x000F9E43 File Offset: 0x000F8043
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

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060041F1 RID: 16881 RVA: 0x000F9E60 File Offset: 0x000F8060
		// (set) Token: 0x060041F2 RID: 16882 RVA: 0x000F9E72 File Offset: 0x000F8072
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

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x000F9E85 File Offset: 0x000F8085
		// (set) Token: 0x060041F4 RID: 16884 RVA: 0x000F9E97 File Offset: 0x000F8097
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

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x000F9EAA File Offset: 0x000F80AA
		// (set) Token: 0x060041F6 RID: 16886 RVA: 0x000F9EBC File Offset: 0x000F80BC
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

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x000F9ECF File Offset: 0x000F80CF
		// (set) Token: 0x060041F8 RID: 16888 RVA: 0x000F9EE1 File Offset: 0x000F80E1
		[ConfigurationProperty("includeWindowsGroups", DefaultValue = true)]
		public bool IncludeWindowsGroups
		{
			get
			{
				return (bool)base["includeWindowsGroups"];
			}
			set
			{
				base["includeWindowsGroups"] = value;
			}
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x000F9EF4 File Offset: 0x000F80F4
		// (set) Token: 0x060041FA RID: 16890 RVA: 0x000F9F06 File Offset: 0x000F8106
		[ConfigurationProperty("mapClientCertificateToWindowsAccount", DefaultValue = false)]
		public bool MapClientCertificateToWindowsAccount
		{
			get
			{
				return (bool)base["mapClientCertificateToWindowsAccount"];
			}
			set
			{
				base["mapClientCertificateToWindowsAccount"] = value;
			}
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000F9F1C File Offset: 0x000F811C
		public void Copy(X509ClientCertificateAuthenticationElement from)
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
			this.IncludeWindowsGroups = from.IncludeWindowsGroups;
			this.MapClientCertificateToWindowsAccount = from.MapClientCertificateToWindowsAccount;
			this.CustomCertificateValidatorType = from.CustomCertificateValidatorType;
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000F9FA8 File Offset: 0x000F81A8
		internal void ApplyConfiguration(X509ClientCertificateAuthentication cert)
		{
			if (cert == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("cert");
			}
			cert.CertificateValidationMode = this.CertificateValidationMode;
			cert.RevocationMode = this.RevocationMode;
			cert.TrustedStoreLocation = this.TrustedStoreLocation;
			cert.IncludeWindowsGroups = this.IncludeWindowsGroups;
			cert.MapClientCertificateToWindowsAccount = this.MapClientCertificateToWindowsAccount;
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

		// Token: 0x04002CF5 RID: 11509
		private ConfigurationPropertyCollection properties;
	}
}
