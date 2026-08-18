using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000698 RID: 1688
	public sealed class UserPrincipalNameElement : ConfigurationElement
	{
		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x000F7F00 File Offset: 0x000F6100
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

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x000F7F5D File Offset: 0x000F615D
		// (set) Token: 0x0600415A RID: 16730 RVA: 0x000F7F6F File Offset: 0x000F616F
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

		// Token: 0x04002CE6 RID: 11494
		private ConfigurationPropertyCollection properties;
	}
}
