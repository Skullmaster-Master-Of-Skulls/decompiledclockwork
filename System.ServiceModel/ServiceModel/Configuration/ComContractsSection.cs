using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B0 RID: 1712
	public sealed class ComContractsSection : ConfigurationSection
	{
		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x000FB688 File Offset: 0x000F9888
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(ComContractElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x000FB6CE File Offset: 0x000F98CE
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ComContractElementCollection ComContracts
		{
			get
			{
				return (ComContractElementCollection)base[""];
			}
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x000FB6E0 File Offset: 0x000F98E0
		internal static ComContractsSection GetSection()
		{
			return (ComContractsSection)ConfigurationHelpers.GetSection(ConfigurationStrings.ComContractsSectionPath);
		}

		// Token: 0x04002CFF RID: 11519
		private ConfigurationPropertyCollection properties;
	}
}
