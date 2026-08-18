using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200068C RID: 1676
	public sealed class ServiceMetadataPublishingElement : BehaviorExtensionElement
	{
		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x000F5B00 File Offset: 0x000F3D00
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("externalMetadataLocation", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpGetEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpGetUrl", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsGetEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsGetUrl", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpGetBinding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpGetBindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsGetBinding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsGetBindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("policyVersion", typeof(PolicyVersion), "Default", new PolicyVersionConverter(), null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x000F5CAD File Offset: 0x000F3EAD
		// (set) Token: 0x060040C4 RID: 16580 RVA: 0x000F5CBF File Offset: 0x000F3EBF
		[ConfigurationProperty("externalMetadataLocation")]
		public Uri ExternalMetadataLocation
		{
			get
			{
				return (Uri)base["externalMetadataLocation"];
			}
			set
			{
				base["externalMetadataLocation"] = value;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x060040C5 RID: 16581 RVA: 0x000F5CCD File Offset: 0x000F3ECD
		// (set) Token: 0x060040C6 RID: 16582 RVA: 0x000F5CDF File Offset: 0x000F3EDF
		[ConfigurationProperty("httpGetEnabled", DefaultValue = false)]
		public bool HttpGetEnabled
		{
			get
			{
				return (bool)base["httpGetEnabled"];
			}
			set
			{
				base["httpGetEnabled"] = value;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x000F5CF2 File Offset: 0x000F3EF2
		// (set) Token: 0x060040C8 RID: 16584 RVA: 0x000F5D04 File Offset: 0x000F3F04
		[ConfigurationProperty("httpGetUrl")]
		public Uri HttpGetUrl
		{
			get
			{
				return (Uri)base["httpGetUrl"];
			}
			set
			{
				base["httpGetUrl"] = value;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x000F5D12 File Offset: 0x000F3F12
		// (set) Token: 0x060040CA RID: 16586 RVA: 0x000F5D24 File Offset: 0x000F3F24
		[ConfigurationProperty("httpsGetEnabled", DefaultValue = false)]
		public bool HttpsGetEnabled
		{
			get
			{
				return (bool)base["httpsGetEnabled"];
			}
			set
			{
				base["httpsGetEnabled"] = value;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x000F5D37 File Offset: 0x000F3F37
		// (set) Token: 0x060040CC RID: 16588 RVA: 0x000F5D49 File Offset: 0x000F3F49
		[ConfigurationProperty("httpsGetUrl")]
		public Uri HttpsGetUrl
		{
			get
			{
				return (Uri)base["httpsGetUrl"];
			}
			set
			{
				base["httpsGetUrl"] = value;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x000F5D57 File Offset: 0x000F3F57
		// (set) Token: 0x060040CE RID: 16590 RVA: 0x000F5D69 File Offset: 0x000F3F69
		[ConfigurationProperty("httpGetBinding", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string HttpGetBinding
		{
			get
			{
				return (string)base["httpGetBinding"];
			}
			set
			{
				base["httpGetBinding"] = value;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x060040CF RID: 16591 RVA: 0x000F5D77 File Offset: 0x000F3F77
		// (set) Token: 0x060040D0 RID: 16592 RVA: 0x000F5D89 File Offset: 0x000F3F89
		[ConfigurationProperty("httpGetBindingConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string HttpGetBindingConfiguration
		{
			get
			{
				return (string)base["httpGetBindingConfiguration"];
			}
			set
			{
				base["httpGetBindingConfiguration"] = value;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x060040D1 RID: 16593 RVA: 0x000F5D97 File Offset: 0x000F3F97
		// (set) Token: 0x060040D2 RID: 16594 RVA: 0x000F5DA9 File Offset: 0x000F3FA9
		[ConfigurationProperty("httpsGetBinding", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string HttpsGetBinding
		{
			get
			{
				return (string)base["httpsGetBinding"];
			}
			set
			{
				base["httpsGetBinding"] = value;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x060040D3 RID: 16595 RVA: 0x000F5DB7 File Offset: 0x000F3FB7
		// (set) Token: 0x060040D4 RID: 16596 RVA: 0x000F5DC9 File Offset: 0x000F3FC9
		[ConfigurationProperty("httpsGetBindingConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string HttpsGetBindingConfiguration
		{
			get
			{
				return (string)base["httpsGetBindingConfiguration"];
			}
			set
			{
				base["httpsGetBindingConfiguration"] = value;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x000F5DD7 File Offset: 0x000F3FD7
		// (set) Token: 0x060040D6 RID: 16598 RVA: 0x000F5DE9 File Offset: 0x000F3FE9
		[ConfigurationProperty("policyVersion", DefaultValue = "Default")]
		[TypeConverter(typeof(PolicyVersionConverter))]
		public PolicyVersion PolicyVersion
		{
			get
			{
				return (PolicyVersion)base["policyVersion"];
			}
			set
			{
				base["policyVersion"] = value;
			}
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x000F5DF8 File Offset: 0x000F3FF8
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ServiceMetadataPublishingElement serviceMetadataPublishingElement = (ServiceMetadataPublishingElement)from;
			this.HttpGetEnabled = serviceMetadataPublishingElement.HttpGetEnabled;
			this.HttpGetUrl = serviceMetadataPublishingElement.HttpGetUrl;
			this.HttpsGetEnabled = serviceMetadataPublishingElement.HttpsGetEnabled;
			this.HttpsGetUrl = serviceMetadataPublishingElement.HttpsGetUrl;
			this.ExternalMetadataLocation = serviceMetadataPublishingElement.ExternalMetadataLocation;
			this.PolicyVersion = serviceMetadataPublishingElement.PolicyVersion;
			this.HttpGetBinding = serviceMetadataPublishingElement.HttpGetBinding;
			this.HttpGetBindingConfiguration = serviceMetadataPublishingElement.HttpGetBindingConfiguration;
			this.HttpsGetBinding = serviceMetadataPublishingElement.HttpsGetBinding;
			this.HttpsGetBindingConfiguration = serviceMetadataPublishingElement.HttpsGetBindingConfiguration;
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x000F5E8C File Offset: 0x000F408C
		protected internal override object CreateBehavior()
		{
			ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();
			serviceMetadataBehavior.HttpGetEnabled = this.HttpGetEnabled;
			serviceMetadataBehavior.HttpGetUrl = this.HttpGetUrl;
			serviceMetadataBehavior.HttpsGetEnabled = this.HttpsGetEnabled;
			serviceMetadataBehavior.HttpsGetUrl = this.HttpsGetUrl;
			serviceMetadataBehavior.ExternalMetadataLocation = this.ExternalMetadataLocation;
			serviceMetadataBehavior.MetadataExporter.PolicyVersion = this.PolicyVersion;
			if (!string.IsNullOrEmpty(this.HttpGetBinding))
			{
				serviceMetadataBehavior.HttpGetBinding = ConfigLoader.LookupBinding(this.HttpGetBinding, this.HttpGetBindingConfiguration);
			}
			if (!string.IsNullOrEmpty(this.HttpsGetBinding))
			{
				serviceMetadataBehavior.HttpsGetBinding = ConfigLoader.LookupBinding(this.HttpsGetBinding, this.HttpsGetBindingConfiguration);
			}
			return serviceMetadataBehavior;
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x000F5F35 File Offset: 0x000F4135
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceMetadataBehavior);
			}
		}

		// Token: 0x04002CD9 RID: 11481
		private ConfigurationPropertyCollection properties;
	}
}
