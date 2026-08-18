using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200032D RID: 813
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		// Token: 0x06001D30 RID: 7472 RVA: 0x0008AFE5 File Offset: 0x000891E5
		public ConnectionManagementSection()
		{
			this.properties.Add(this.connectionManagement);
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0008B021 File Offset: 0x00089221
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[this.connectionManagement];
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0008B034 File Offset: 0x00089234
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C2A RID: 7210
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C2B RID: 7211
		private readonly ConfigurationProperty connectionManagement = new ConfigurationProperty(null, typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
