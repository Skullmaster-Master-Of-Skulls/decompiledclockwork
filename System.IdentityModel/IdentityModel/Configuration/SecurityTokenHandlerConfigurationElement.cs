using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001CC RID: 460
	public sealed class SecurityTokenHandlerConfigurationElement : ConfigurationElement
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0004348C File Offset: 0x0004168C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("audienceUris", typeof(AudienceUriElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("caches", typeof(IdentityModelCachesElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("certificateValidation", typeof(X509CertificateValidationElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuerNameRegistry", typeof(IssuerNameRegistryElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuerTokenResolver", typeof(CustomTypeElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("saveBootstrapContext", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maximumClockSkew", typeof(TimeSpan), TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("serviceTokenResolver", typeof(CustomTypeElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("tokenReplayDetection", typeof(TokenReplayDetectionElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00043630 File Offset: 0x00041830
		protected override void Init()
		{
			this.Name = "";
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00042D51 File Offset: 0x00040F51
		[ConfigurationProperty("audienceUris", IsRequired = false)]
		public AudienceUriElementCollection AudienceUris
		{
			get
			{
				return (AudienceUriElementCollection)base["audienceUris"];
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00042D63 File Offset: 0x00040F63
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00042D75 File Offset: 0x00040F75
		[ConfigurationProperty("caches", IsRequired = false)]
		public IdentityModelCachesElement Caches
		{
			get
			{
				return (IdentityModelCachesElement)base["caches"];
			}
			set
			{
				base["caches"] = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00042D83 File Offset: 0x00040F83
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00042D95 File Offset: 0x00040F95
		[ConfigurationProperty("certificateValidation", IsRequired = false)]
		public X509CertificateValidationElement CertificateValidation
		{
			get
			{
				return (X509CertificateValidationElement)base["certificateValidation"];
			}
			set
			{
				base["certificateValidation"] = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00042DE3 File Offset: 0x00040FE3
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00042DF5 File Offset: 0x00040FF5
		[ConfigurationProperty("issuerNameRegistry", IsRequired = false)]
		public IssuerNameRegistryElement IssuerNameRegistry
		{
			get
			{
				return (IssuerNameRegistryElement)base["issuerNameRegistry"];
			}
			set
			{
				base["issuerNameRegistry"] = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00042E03 File Offset: 0x00041003
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00042E15 File Offset: 0x00041015
		[ConfigurationProperty("issuerTokenResolver", IsRequired = false)]
		public CustomTypeElement IssuerTokenResolver
		{
			get
			{
				return (CustomTypeElement)base["issuerTokenResolver"];
			}
			set
			{
				base["issuerTokenResolver"] = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00042D31 File Offset: 0x00040F31
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00042D43 File Offset: 0x00040F43
		[ConfigurationProperty("name", IsRequired = false, Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 0)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00042E48 File Offset: 0x00041048
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x00042E5A File Offset: 0x0004105A
		[ConfigurationProperty("saveBootstrapContext", IsRequired = false, DefaultValue = false)]
		public bool SaveBootstrapContext
		{
			get
			{
				return (bool)base["saveBootstrapContext"];
			}
			set
			{
				base["saveBootstrapContext"] = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00042E23 File Offset: 0x00041023
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00042E35 File Offset: 0x00041035
		[ConfigurationProperty("maximumClockSkew", IsRequired = false, DefaultValue = "00:05:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[IdentityModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan MaximumClockSkew
		{
			get
			{
				return (TimeSpan)base["maximumClockSkew"];
			}
			set
			{
				base["maximumClockSkew"] = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00042E6D File Offset: 0x0004106D
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00042E7F File Offset: 0x0004107F
		[ConfigurationProperty("serviceTokenResolver", IsRequired = false)]
		public CustomTypeElement ServiceTokenResolver
		{
			get
			{
				return (CustomTypeElement)base["serviceTokenResolver"];
			}
			set
			{
				base["serviceTokenResolver"] = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00042E8D File Offset: 0x0004108D
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00042E9F File Offset: 0x0004109F
		[ConfigurationProperty("tokenReplayDetection", IsRequired = false)]
		public TokenReplayDetectionElement TokenReplayDetection
		{
			get
			{
				return (TokenReplayDetectionElement)base["tokenReplayDetection"];
			}
			set
			{
				base["tokenReplayDetection"] = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00043640 File Offset: 0x00041840
		internal bool IsConfigured
		{
			get
			{
				return this.AudienceUris.IsConfigured || this.Caches.IsConfigured || this.CertificateValidation.IsConfigured || this.IssuerNameRegistry.IsConfigured || this.IssuerTokenResolver.IsConfigured || base.ElementInformation.Properties["name"].ValueOrigin != PropertyValueOrigin.Default || base.ElementInformation.Properties["saveBootstrapContext"].ValueOrigin != PropertyValueOrigin.Default || base.ElementInformation.Properties["maximumClockSkew"].ValueOrigin != PropertyValueOrigin.Default || this.ServiceTokenResolver.IsConfigured || this.TokenReplayDetection.IsConfigured;
			}
		}

		// Token: 0x04000D80 RID: 3456
		private ConfigurationPropertyCollection properties;
	}
}
