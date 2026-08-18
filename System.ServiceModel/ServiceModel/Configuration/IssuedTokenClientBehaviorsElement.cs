using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000634 RID: 1588
	public sealed class IssuedTokenClientBehaviorsElement : ConfigurationElement
	{
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06003CF7 RID: 15607 RVA: 0x000E8A88 File Offset: 0x000E6C88
		// (set) Token: 0x06003CF8 RID: 15608 RVA: 0x000E8A9A File Offset: 0x000E6C9A
		[ConfigurationProperty("issuerAddress", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 0)]
		public string IssuerAddress
		{
			get
			{
				return (string)base["issuerAddress"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["issuerAddress"] = value;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x000E8AB7 File Offset: 0x000E6CB7
		// (set) Token: 0x06003CFA RID: 15610 RVA: 0x000E8AC9 File Offset: 0x000E6CC9
		[ConfigurationProperty("behaviorConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string BehaviorConfiguration
		{
			get
			{
				return (string)base["behaviorConfiguration"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["behaviorConfiguration"] = value;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x000E8AE8 File Offset: 0x000E6CE8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("issuerAddress", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("behaviorConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C89 RID: 11401
		private ConfigurationPropertyCollection properties;
	}
}
