using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B9 RID: 1721
	public sealed class ServiceDebugElement : BehaviorExtensionElement
	{
		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060042B3 RID: 17075 RVA: 0x000FC41C File Offset: 0x000FA61C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("httpHelpPageEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpHelpPageUrl", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsHelpPageEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsHelpPageUrl", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpHelpPageBinding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpHelpPageBindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsHelpPageBinding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpsHelpPageBindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("includeExceptionDetailInFaults", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000FC5A8 File Offset: 0x000FA7A8
		// (set) Token: 0x060042B6 RID: 17078 RVA: 0x000FC5BA File Offset: 0x000FA7BA
		[ConfigurationProperty("httpHelpPageEnabled", DefaultValue = true)]
		public bool HttpHelpPageEnabled
		{
			get
			{
				return (bool)base["httpHelpPageEnabled"];
			}
			set
			{
				base["httpHelpPageEnabled"] = value;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x000FC5CD File Offset: 0x000FA7CD
		// (set) Token: 0x060042B8 RID: 17080 RVA: 0x000FC5DF File Offset: 0x000FA7DF
		[ConfigurationProperty("httpHelpPageUrl")]
		public Uri HttpHelpPageUrl
		{
			get
			{
				return (Uri)base["httpHelpPageUrl"];
			}
			set
			{
				base["httpHelpPageUrl"] = value;
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x060042B9 RID: 17081 RVA: 0x000FC5ED File Offset: 0x000FA7ED
		// (set) Token: 0x060042BA RID: 17082 RVA: 0x000FC5FF File Offset: 0x000FA7FF
		[ConfigurationProperty("httpsHelpPageEnabled", DefaultValue = true)]
		public bool HttpsHelpPageEnabled
		{
			get
			{
				return (bool)base["httpsHelpPageEnabled"];
			}
			set
			{
				base["httpsHelpPageEnabled"] = value;
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x000FC612 File Offset: 0x000FA812
		// (set) Token: 0x060042BC RID: 17084 RVA: 0x000FC624 File Offset: 0x000FA824
		[ConfigurationProperty("httpsHelpPageUrl")]
		public Uri HttpsHelpPageUrl
		{
			get
			{
				return (Uri)base["httpsHelpPageUrl"];
			}
			set
			{
				base["httpsHelpPageUrl"] = value;
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x000FC632 File Offset: 0x000FA832
		// (set) Token: 0x060042BE RID: 17086 RVA: 0x000FC644 File Offset: 0x000FA844
		[ConfigurationProperty("httpHelpPageBinding", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string HttpHelpPageBinding
		{
			get
			{
				return (string)base["httpHelpPageBinding"];
			}
			set
			{
				base["httpHelpPageBinding"] = value;
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x000FC652 File Offset: 0x000FA852
		// (set) Token: 0x060042C0 RID: 17088 RVA: 0x000FC664 File Offset: 0x000FA864
		[ConfigurationProperty("httpHelpPageBindingConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string HttpHelpPageBindingConfiguration
		{
			get
			{
				return (string)base["httpHelpPageBindingConfiguration"];
			}
			set
			{
				base["httpHelpPageBindingConfiguration"] = value;
			}
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x000FC672 File Offset: 0x000FA872
		// (set) Token: 0x060042C2 RID: 17090 RVA: 0x000FC684 File Offset: 0x000FA884
		[ConfigurationProperty("httpsHelpPageBinding", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string HttpsHelpPageBinding
		{
			get
			{
				return (string)base["httpsHelpPageBinding"];
			}
			set
			{
				base["httpsHelpPageBinding"] = value;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x000FC692 File Offset: 0x000FA892
		// (set) Token: 0x060042C4 RID: 17092 RVA: 0x000FC6A4 File Offset: 0x000FA8A4
		[ConfigurationProperty("httpsHelpPageBindingConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string HttpsHelpPageBindingConfiguration
		{
			get
			{
				return (string)base["httpsHelpPageBindingConfiguration"];
			}
			set
			{
				base["httpsHelpPageBindingConfiguration"] = value;
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x000FC6B2 File Offset: 0x000FA8B2
		// (set) Token: 0x060042C6 RID: 17094 RVA: 0x000FC6C4 File Offset: 0x000FA8C4
		[ConfigurationProperty("includeExceptionDetailInFaults", DefaultValue = false)]
		public bool IncludeExceptionDetailInFaults
		{
			get
			{
				return (bool)base["includeExceptionDetailInFaults"];
			}
			set
			{
				base["includeExceptionDetailInFaults"] = value;
			}
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x000FC6D8 File Offset: 0x000FA8D8
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ServiceDebugElement serviceDebugElement = (ServiceDebugElement)from;
			this.HttpHelpPageEnabled = serviceDebugElement.HttpHelpPageEnabled;
			this.HttpHelpPageUrl = serviceDebugElement.HttpHelpPageUrl;
			this.HttpsHelpPageEnabled = serviceDebugElement.HttpsHelpPageEnabled;
			this.HttpsHelpPageUrl = serviceDebugElement.HttpsHelpPageUrl;
			this.IncludeExceptionDetailInFaults = serviceDebugElement.IncludeExceptionDetailInFaults;
			this.HttpHelpPageBinding = serviceDebugElement.HttpHelpPageBinding;
			this.HttpHelpPageBindingConfiguration = serviceDebugElement.HttpHelpPageBindingConfiguration;
			this.HttpsHelpPageBinding = serviceDebugElement.HttpsHelpPageBinding;
			this.HttpsHelpPageBindingConfiguration = serviceDebugElement.HttpsHelpPageBindingConfiguration;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x000FC760 File Offset: 0x000FA960
		protected internal override object CreateBehavior()
		{
			ServiceDebugBehavior serviceDebugBehavior = new ServiceDebugBehavior();
			serviceDebugBehavior.HttpHelpPageEnabled = this.HttpHelpPageEnabled;
			serviceDebugBehavior.HttpHelpPageUrl = this.HttpHelpPageUrl;
			serviceDebugBehavior.HttpsHelpPageEnabled = this.HttpsHelpPageEnabled;
			serviceDebugBehavior.HttpsHelpPageUrl = this.HttpsHelpPageUrl;
			serviceDebugBehavior.IncludeExceptionDetailInFaults = this.IncludeExceptionDetailInFaults;
			if (!string.IsNullOrEmpty(this.HttpHelpPageBinding))
			{
				serviceDebugBehavior.HttpHelpPageBinding = ConfigLoader.LookupBinding(this.HttpHelpPageBinding, this.HttpHelpPageBindingConfiguration);
			}
			if (!string.IsNullOrEmpty(this.HttpsHelpPageBinding))
			{
				serviceDebugBehavior.HttpsHelpPageBinding = ConfigLoader.LookupBinding(this.HttpsHelpPageBinding, this.HttpsHelpPageBindingConfiguration);
			}
			return serviceDebugBehavior;
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x000FC7F8 File Offset: 0x000FA9F8
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceDebugBehavior);
			}
		}

		// Token: 0x04002D08 RID: 11528
		private ConfigurationPropertyCollection properties;
	}
}
