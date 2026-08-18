using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000649 RID: 1609
	public sealed class ConnectionManagementElement : ConfigurationElement
	{
		// Token: 0x060031D5 RID: 12757 RVA: 0x000D4DD0 File Offset: 0x000D3DD0
		public ConnectionManagementElement()
		{
			this.properties.Add(this.address);
			this.properties.Add(this.maxconnection);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000D4E4D File Offset: 0x000D3E4D
		public ConnectionManagementElement(string address, int maxConnection) : this()
		{
			this.Address = address;
			this.MaxConnection = maxConnection;
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000D4E63 File Offset: 0x000D3E63
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000D4E6B File Offset: 0x000D3E6B
		// (set) Token: 0x060031D9 RID: 12761 RVA: 0x000D4E7E File Offset: 0x000D3E7E
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

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x000D4E8D File Offset: 0x000D3E8D
		// (set) Token: 0x060031DB RID: 12763 RVA: 0x000D4EA0 File Offset: 0x000D3EA0
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

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000D4EB4 File Offset: 0x000D3EB4
		internal string Key
		{
			get
			{
				return this.Address;
			}
		}

		// Token: 0x04002EF1 RID: 12017
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002EF2 RID: 12018
		private readonly ConfigurationProperty address = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002EF3 RID: 12019
		private readonly ConfigurationProperty maxconnection = new ConfigurationProperty("maxconnection", typeof(int), 1, ConfigurationPropertyOptions.None);
	}
}
