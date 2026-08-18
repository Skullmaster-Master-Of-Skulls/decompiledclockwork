using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200064B RID: 1611
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		// Token: 0x060031EA RID: 12778 RVA: 0x000D4F7D File Offset: 0x000D3F7D
		public ConnectionManagementSection()
		{
			this.properties.Add(this.connectionManagement);
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000D4FB9 File Offset: 0x000D3FB9
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[this.connectionManagement];
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000D4FCC File Offset: 0x000D3FCC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002EF4 RID: 12020
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002EF5 RID: 12021
		private readonly ConfigurationProperty connectionManagement = new ConfigurationProperty(null, typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
