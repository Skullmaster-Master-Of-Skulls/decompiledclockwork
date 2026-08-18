using System;
using System.Configuration;

namespace TechnoPro.Common.Configuration.Sections
{
	// Token: 0x02000004 RID: 4
	public sealed class CustomSettingsSection : ConfigurationSection
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002674 File Offset: 0x00000874
		private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
		{
			bool flag = CustomSettingsSection.s_properties == null;
			if (flag)
			{
				CustomSettingsSection.s_propAppSettings = new ConfigurationProperty(null, typeof(KeyValueConfigurationCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
				ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection
				{
					CustomSettingsSection.s_propAppSettings
				};
				CustomSettingsSection.s_properties = configurationPropertyCollection;
			}
			return CustomSettingsSection.s_properties;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026C8 File Offset: 0x000008C8
		public CustomSettingsSection()
		{
			CustomSettingsSection.EnsureStaticPropertyBag();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000026D8 File Offset: 0x000008D8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomSettingsSection.EnsureStaticPropertyBag();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000026DF File Offset: 0x000008DF
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public KeyValueConfigurationCollection Settings
		{
			get
			{
				return (KeyValueConfigurationCollection)base[CustomSettingsSection.s_propAppSettings];
			}
		}

		// Token: 0x04000004 RID: 4
		private static ConfigurationPropertyCollection s_properties;

		// Token: 0x04000005 RID: 5
		private static ConfigurationProperty s_propAppSettings;
	}
}
