using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000691 RID: 1681
	public sealed class ServicePrincipalNameElement : ConfigurationElement
	{
		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x000F728C File Offset: 0x000F548C
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

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x000F72E9 File Offset: 0x000F54E9
		// (set) Token: 0x0600410F RID: 16655 RVA: 0x000F72FB File Offset: 0x000F54FB
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

		// Token: 0x04002CDF RID: 11487
		private ConfigurationPropertyCollection properties;
	}
}
