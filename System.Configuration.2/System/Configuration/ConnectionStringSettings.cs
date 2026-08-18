using System;

namespace System.Configuration
{
	// Token: 0x0200004A RID: 74
	public sealed class ConnectionStringSettings : ConfigurationElement
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0001271C File Offset: 0x0001091C
		static ConnectionStringSettings()
		{
			ConnectionStringSettings._properties = new ConfigurationPropertyCollection();
			ConnectionStringSettings._properties.Add(ConnectionStringSettings._propName);
			ConnectionStringSettings._properties.Add(ConnectionStringSettings._propConnectionString);
			ConnectionStringSettings._properties.Add(ConnectionStringSettings._propProviderName);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000127BF File Offset: 0x000109BF
		public ConnectionStringSettings()
		{
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000127C7 File Offset: 0x000109C7
		public ConnectionStringSettings(string name, string connectionString) : this()
		{
			this.Name = name;
			this.ConnectionString = connectionString;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000127DD File Offset: 0x000109DD
		public ConnectionStringSettings(string name, string connectionString, string providerName) : this()
		{
			this.Name = name;
			this.ConnectionString = connectionString;
			this.ProviderName = providerName;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000127FA File Offset: 0x000109FA
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00012802 File Offset: 0x00010A02
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionStringSettings._properties;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00012809 File Offset: 0x00010A09
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0001281B File Offset: 0x00010A1B
		[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey), DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[ConnectionStringSettings._propName];
			}
			set
			{
				base[ConnectionStringSettings._propName] = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00012829 File Offset: 0x00010A29
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0001283B File Offset: 0x00010A3B
		[ConfigurationProperty("connectionString", Options = ConfigurationPropertyOptions.IsRequired, DefaultValue = "")]
		public string ConnectionString
		{
			get
			{
				return (string)base[ConnectionStringSettings._propConnectionString];
			}
			set
			{
				base[ConnectionStringSettings._propConnectionString] = value;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00012849 File Offset: 0x00010A49
		public override string ToString()
		{
			return this.ConnectionString;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00012851 File Offset: 0x00010A51
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00012863 File Offset: 0x00010A63
		[ConfigurationProperty("providerName", DefaultValue = "System.Data.SqlClient")]
		public string ProviderName
		{
			get
			{
				return (string)base[ConnectionStringSettings._propProviderName];
			}
			set
			{
				base[ConnectionStringSettings._propProviderName] = value;
			}
		}

		// Token: 0x0400023C RID: 572
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x0400023D RID: 573
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, ConfigurationProperty.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400023E RID: 574
		private static readonly ConfigurationProperty _propConnectionString = new ConfigurationProperty("connectionString", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400023F RID: 575
		private static readonly ConfigurationProperty _propProviderName = new ConfigurationProperty("providerName", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
