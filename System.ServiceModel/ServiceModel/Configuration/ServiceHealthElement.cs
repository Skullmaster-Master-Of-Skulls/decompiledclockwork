using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006BA RID: 1722
	public sealed class ServiceHealthElement : BehaviorExtensionElement
	{
		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x060042CA RID: 17098 RVA: 0x000FC804 File Offset: 0x000FAA04
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("healthDetailsEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpGetEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpGetUrl", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsGetEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsGetUrl", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpGetBinding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsGetBinding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpGetBindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsGetBindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x060042CC RID: 17100 RVA: 0x000FC9A0 File Offset: 0x000FABA0
		// (set) Token: 0x060042CD RID: 17101 RVA: 0x000FC9B2 File Offset: 0x000FABB2
		[ConfigurationProperty("healthDetailsEnabled", DefaultValue = true)]
		public bool HealthDetailsEnabled
		{
			get
			{
				return (bool)base["healthDetailsEnabled"];
			}
			set
			{
				base["healthDetailsEnabled"] = value;
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x000FC9C5 File Offset: 0x000FABC5
		// (set) Token: 0x060042CF RID: 17103 RVA: 0x000FC9D7 File Offset: 0x000FABD7
		[ConfigurationProperty("httpGetEnabled", DefaultValue = true)]
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

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060042D0 RID: 17104 RVA: 0x000FC9EA File Offset: 0x000FABEA
		// (set) Token: 0x060042D1 RID: 17105 RVA: 0x000FC9FC File Offset: 0x000FABFC
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

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x060042D2 RID: 17106 RVA: 0x000FCA0A File Offset: 0x000FAC0A
		// (set) Token: 0x060042D3 RID: 17107 RVA: 0x000FCA1C File Offset: 0x000FAC1C
		[ConfigurationProperty("httpsGetEnabled", DefaultValue = true)]
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

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060042D4 RID: 17108 RVA: 0x000FCA2F File Offset: 0x000FAC2F
		// (set) Token: 0x060042D5 RID: 17109 RVA: 0x000FCA41 File Offset: 0x000FAC41
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

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060042D6 RID: 17110 RVA: 0x000FCA4F File Offset: 0x000FAC4F
		// (set) Token: 0x060042D7 RID: 17111 RVA: 0x000FCA61 File Offset: 0x000FAC61
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

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060042D8 RID: 17112 RVA: 0x000FCA6F File Offset: 0x000FAC6F
		// (set) Token: 0x060042D9 RID: 17113 RVA: 0x000FCA81 File Offset: 0x000FAC81
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

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060042DA RID: 17114 RVA: 0x000FCA8F File Offset: 0x000FAC8F
		// (set) Token: 0x060042DB RID: 17115 RVA: 0x000FCAA1 File Offset: 0x000FACA1
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

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060042DC RID: 17116 RVA: 0x000FCAAF File Offset: 0x000FACAF
		// (set) Token: 0x060042DD RID: 17117 RVA: 0x000FCAC1 File Offset: 0x000FACC1
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

		// Token: 0x060042DE RID: 17118 RVA: 0x000FCAD0 File Offset: 0x000FACD0
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ServiceHealthElement serviceHealthElement = (ServiceHealthElement)from;
			this.HealthDetailsEnabled = serviceHealthElement.HealthDetailsEnabled;
			this.HttpGetEnabled = serviceHealthElement.HttpGetEnabled;
			this.HttpGetUrl = serviceHealthElement.HttpGetUrl;
			this.HttpsGetEnabled = serviceHealthElement.HttpsGetEnabled;
			this.HttpsGetUrl = serviceHealthElement.HttpsGetUrl;
			this.HttpGetBinding = serviceHealthElement.HttpGetBinding;
			this.HttpsGetBinding = serviceHealthElement.HttpsGetBinding;
			this.HttpGetBindingConfiguration = serviceHealthElement.HttpGetBindingConfiguration;
			this.HttpsGetBindingConfiguration = serviceHealthElement.HttpsGetBindingConfiguration;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x000FCB58 File Offset: 0x000FAD58
		protected internal override object CreateBehavior()
		{
			ServiceHealthBehavior serviceHealthBehavior = new ServiceHealthBehavior();
			serviceHealthBehavior.HealthDetailsEnabled = this.HealthDetailsEnabled;
			serviceHealthBehavior.HttpGetEnabled = this.HttpGetEnabled;
			serviceHealthBehavior.HttpGetUrl = this.HttpGetUrl;
			serviceHealthBehavior.HttpsGetEnabled = this.HttpsGetEnabled;
			serviceHealthBehavior.HttpsGetUrl = this.HttpsGetUrl;
			if (!string.IsNullOrEmpty(this.HttpGetBinding))
			{
				serviceHealthBehavior.HttpGetBinding = ConfigLoader.LookupBinding(this.HttpGetBinding, this.HttpGetBindingConfiguration);
			}
			if (!string.IsNullOrEmpty(this.HttpsGetBinding))
			{
				serviceHealthBehavior.HttpsGetBinding = ConfigLoader.LookupBinding(this.HttpsGetBinding, this.HttpsGetBindingConfiguration);
			}
			return serviceHealthBehavior;
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060042E0 RID: 17120 RVA: 0x000FCBF0 File Offset: 0x000FADF0
		public override Type BehaviorType { get; } = typeof(ServiceHealthBehavior);

		// Token: 0x04002D09 RID: 11529
		private ConfigurationPropertyCollection properties;
	}
}
