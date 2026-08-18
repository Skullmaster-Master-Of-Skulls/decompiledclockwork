using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000618 RID: 1560
	public sealed class DnsElement : ConfigurationElement
	{
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003C02 RID: 15362 RVA: 0x000E564D File Offset: 0x000E384D
		// (set) Token: 0x06003C03 RID: 15363 RVA: 0x000E565F File Offset: 0x000E385F
		[ConfigurationProperty("value", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string Value
		{
			get
			{
				return (string)base["value"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["value"] = value;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003C04 RID: 15364 RVA: 0x000E567C File Offset: 0x000E387C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("value", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C79 RID: 11385
		private ConfigurationPropertyCollection properties;
	}
}
