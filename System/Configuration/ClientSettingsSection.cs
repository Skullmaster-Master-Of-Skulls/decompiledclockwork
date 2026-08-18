using System;

namespace System.Configuration
{
	// Token: 0x02000722 RID: 1826
	public sealed class ClientSettingsSection : ConfigurationSection
	{
		// Token: 0x060037CC RID: 14284 RVA: 0x000EC3E1 File Offset: 0x000EB3E1
		static ClientSettingsSection()
		{
			ClientSettingsSection._properties = new ConfigurationPropertyCollection();
			ClientSettingsSection._properties.Add(ClientSettingsSection._propSettings);
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x000EC41B File Offset: 0x000EB41B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientSettingsSection._properties;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060037CF RID: 14287 RVA: 0x000EC422 File Offset: 0x000EB422
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public SettingElementCollection Settings
		{
			get
			{
				return (SettingElementCollection)base[ClientSettingsSection._propSettings];
			}
		}

		// Token: 0x040031F1 RID: 12785
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x040031F2 RID: 12786
		private static readonly ConfigurationProperty _propSettings = new ConfigurationProperty(null, typeof(SettingElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
