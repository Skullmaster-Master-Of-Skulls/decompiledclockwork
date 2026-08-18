using System;

namespace System.Configuration
{
	// Token: 0x0200004C RID: 76
	public sealed class ConnectionStringsSection : ConfigurationSection
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00012936 File Offset: 0x00010B36
		static ConnectionStringsSection()
		{
			ConnectionStringsSection._properties = new ConfigurationPropertyCollection();
			ConnectionStringsSection._properties.Add(ConnectionStringsSection._propConnectionStrings);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00012968 File Offset: 0x00010B68
		protected internal override object GetRuntimeObject()
		{
			this.SetReadOnly();
			return this;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00012971 File Offset: 0x00010B71
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionStringsSection._properties;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00012978 File Offset: 0x00010B78
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return (ConnectionStringSettingsCollection)base[ConnectionStringsSection._propConnectionStrings];
			}
		}

		// Token: 0x04000241 RID: 577
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04000242 RID: 578
		private static readonly ConfigurationProperty _propConnectionStrings = new ConfigurationProperty(null, typeof(ConnectionStringSettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
