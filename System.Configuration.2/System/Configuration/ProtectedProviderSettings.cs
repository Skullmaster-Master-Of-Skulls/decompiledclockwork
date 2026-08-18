using System;

namespace System.Configuration
{
	// Token: 0x0200007F RID: 127
	public class ProtectedProviderSettings : ConfigurationElement
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0001989B File Offset: 0x00017A9B
		public ProtectedProviderSettings()
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(this._propProviders);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000198D7 File Offset: 0x00017AD7
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000198DF File Offset: 0x00017ADF
		[ConfigurationProperty("", IsDefaultCollection = true, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[this._propProviders];
			}
		}

		// Token: 0x040002D5 RID: 725
		private ConfigurationPropertyCollection _properties;

		// Token: 0x040002D6 RID: 726
		private readonly ConfigurationProperty _propProviders = new ConfigurationProperty(null, typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
