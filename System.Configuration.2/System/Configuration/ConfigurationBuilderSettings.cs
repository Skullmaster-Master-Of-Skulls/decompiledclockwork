using System;

namespace System.Configuration
{
	// Token: 0x02000021 RID: 33
	public class ConfigurationBuilderSettings : ConfigurationElement
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00009845 File Offset: 0x00007A45
		public ConfigurationBuilderSettings()
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(this._propBuilders);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00009881 File Offset: 0x00007A81
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00009889 File Offset: 0x00007A89
		[ConfigurationProperty("", IsDefaultCollection = true, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ProviderSettingsCollection Builders
		{
			get
			{
				return (ProviderSettingsCollection)base[this._propBuilders];
			}
		}

		// Token: 0x0400018A RID: 394
		private ConfigurationPropertyCollection _properties;

		// Token: 0x0400018B RID: 395
		private readonly ConfigurationProperty _propBuilders = new ConfigurationProperty(null, typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
