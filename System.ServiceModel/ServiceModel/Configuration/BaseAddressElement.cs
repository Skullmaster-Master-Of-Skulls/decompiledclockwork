using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E8 RID: 1512
	public sealed class BaseAddressElement : ConfigurationElement
	{
		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000E0C8F File Offset: 0x000DEE8F
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x000E0CA1 File Offset: 0x000DEEA1
		[ConfigurationProperty("baseAddress", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string BaseAddress
		{
			get
			{
				return (string)base["baseAddress"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["baseAddress"] = value;
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x000E0CC0 File Offset: 0x000DEEC0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("baseAddress", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A6E RID: 10862
		private ConfigurationPropertyCollection properties;
	}
}
