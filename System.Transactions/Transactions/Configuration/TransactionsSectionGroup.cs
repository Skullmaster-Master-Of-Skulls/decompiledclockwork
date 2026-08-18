using System;
using System.Configuration;

namespace System.Transactions.Configuration
{
	// Token: 0x0200009D RID: 157
	public sealed class TransactionsSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0003E9B4 File Offset: 0x0003DDB4
		[ConfigurationProperty("defaultSettings")]
		public DefaultSettingsSection DefaultSettings
		{
			get
			{
				return (DefaultSettingsSection)base.Sections["defaultSettings"];
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0003E9E4 File Offset: 0x0003DDE4
		public static TransactionsSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return (TransactionsSectionGroup)config.GetSectionGroup("system.transactions");
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0003EA14 File Offset: 0x0003DE14
		[ConfigurationProperty("machineSettings")]
		public MachineSettingsSection MachineSettings
		{
			get
			{
				return (MachineSettingsSection)base.Sections["machineSettings"];
			}
		}
	}
}
