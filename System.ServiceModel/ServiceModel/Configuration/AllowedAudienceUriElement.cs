using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E0 RID: 1504
	public sealed class AllowedAudienceUriElement : ConfigurationElement
	{
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000E07A3 File Offset: 0x000DE9A3
		// (set) Token: 0x06003A48 RID: 14920 RVA: 0x000E07B5 File Offset: 0x000DE9B5
		[ConfigurationProperty("allowedAudienceUri", Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string AllowedAudienceUri
		{
			get
			{
				return (string)base["allowedAudienceUri"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["allowedAudienceUri"] = value;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x000E07D4 File Offset: 0x000DE9D4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("allowedAudienceUri", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A57 RID: 10839
		private ConfigurationPropertyCollection properties;
	}
}
