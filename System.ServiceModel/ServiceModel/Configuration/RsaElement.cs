using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200067E RID: 1662
	public sealed class RsaElement : ConfigurationElement
	{
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x000F1E4C File Offset: 0x000F004C
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

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x000F1EA9 File Offset: 0x000F00A9
		// (set) Token: 0x06003FDB RID: 16347 RVA: 0x000F1EBB File Offset: 0x000F00BB
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

		// Token: 0x04002CC2 RID: 11458
		private ConfigurationPropertyCollection properties;
	}
}
