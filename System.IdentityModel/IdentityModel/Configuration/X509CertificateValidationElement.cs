using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001CB RID: 459
	public sealed class X509CertificateValidationElement : ConfigurationElement
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x000432B0 File Offset: 0x000414B0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("certificateValidationMode", typeof(X509CertificateValidationMode), X509CertificateValidationMode.PeerOrChainTrust, null, new StandardRuntimeEnumValidator(typeof(X509CertificateValidationMode)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("revocationMode", typeof(X509RevocationMode), X509RevocationMode.Online, null, new StandardRuntimeEnumValidator(typeof(X509RevocationMode)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("trustedStoreLocation", typeof(StoreLocation), StoreLocation.LocalMachine, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("certificateValidator", typeof(CustomTypeElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0004338C File Offset: 0x0004158C
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0004339E File Offset: 0x0004159E
		[ConfigurationProperty("certificateValidationMode", IsRequired = false, DefaultValue = X509CertificateValidationMode.PeerOrChainTrust)]
		[StandardRuntimeEnumValidator(typeof(X509CertificateValidationMode))]
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

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000433B1 File Offset: 0x000415B1
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x000433C3 File Offset: 0x000415C3
		[ConfigurationProperty("revocationMode", IsRequired = false, DefaultValue = X509RevocationMode.Online)]
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

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000433D6 File Offset: 0x000415D6
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000433E8 File Offset: 0x000415E8
		[ConfigurationProperty("trustedStoreLocation", IsRequired = false, DefaultValue = StoreLocation.LocalMachine)]
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

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000433FB File Offset: 0x000415FB
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0004340D File Offset: 0x0004160D
		[ConfigurationProperty("certificateValidator", IsRequired = false)]
		public CustomTypeElement CertificateValidator
		{
			get
			{
				return (CustomTypeElement)base["certificateValidator"];
			}
			set
			{
				base["certificateValidator"] = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0004341C File Offset: 0x0004161C
		internal bool IsConfigured
		{
			get
			{
				return base.ElementInformation.Properties["certificateValidationMode"].ValueOrigin != PropertyValueOrigin.Default || base.ElementInformation.Properties["revocationMode"].ValueOrigin != PropertyValueOrigin.Default || base.ElementInformation.Properties["trustedStoreLocation"].ValueOrigin != PropertyValueOrigin.Default || this.CertificateValidator.IsConfigured;
			}
		}

		// Token: 0x04000D7C RID: 3452
		private ConfigurationPropertyCollection properties;

		// Token: 0x04000D7D RID: 3453
		private const X509CertificateValidationMode DefaultX509CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

		// Token: 0x04000D7E RID: 3454
		private const X509RevocationMode DefaultX509RevocationMode = X509RevocationMode.Online;

		// Token: 0x04000D7F RID: 3455
		private const StoreLocation DefaultStoreLocation = StoreLocation.LocalMachine;
	}
}
