using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200032B RID: 811
	public sealed class ConnectionManagementElement : ConfigurationElement
	{
		// Token: 0x06001D1B RID: 7451 RVA: 0x0008AE38 File Offset: 0x00089038
		public ConnectionManagementElement()
		{
			this.properties.Add(this.address);
			this.properties.Add(this.maxconnection);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0008AEB5 File Offset: 0x000890B5
		public ConnectionManagementElement(string address, int maxConnection) : this()
		{
			this.Address = address;
			this.MaxConnection = maxConnection;
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0008AECB File Offset: 0x000890CB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0008AED3 File Offset: 0x000890D3
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x0008AEE6 File Offset: 0x000890E6
		[ConfigurationProperty("address", IsRequired = true, IsKey = true)]
		public string Address
		{
			get
			{
				return (string)base[this.address];
			}
			set
			{
				base[this.address] = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0008AEF5 File Offset: 0x000890F5
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x0008AF08 File Offset: 0x00089108
		[ConfigurationProperty("maxconnection", IsRequired = true, DefaultValue = 1)]
		public int MaxConnection
		{
			get
			{
				return (int)base[this.maxconnection];
			}
			set
			{
				base[this.maxconnection] = value;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0008AF1C File Offset: 0x0008911C
		internal string Key
		{
			get
			{
				return this.Address;
			}
		}

		// Token: 0x04001C27 RID: 7207
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C28 RID: 7208
		private readonly ConfigurationProperty address = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001C29 RID: 7209
		private readonly ConfigurationProperty maxconnection = new ConfigurationProperty("maxconnection", typeof(int), 1, ConfigurationPropertyOptions.None);
	}
}
