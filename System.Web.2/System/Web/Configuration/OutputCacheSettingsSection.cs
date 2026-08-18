using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000722 RID: 1826
	public sealed class OutputCacheSettingsSection : ConfigurationSection
	{
		// Token: 0x060057F0 RID: 22512 RVA: 0x00133BCD File Offset: 0x00131DCD
		static OutputCacheSettingsSection()
		{
			OutputCacheSettingsSection._properties.Add(OutputCacheSettingsSection._propOutputCacheProfiles);
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x060057F2 RID: 22514 RVA: 0x00133C03 File Offset: 0x00131E03
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheSettingsSection._properties;
			}
		}

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x00133C0A File Offset: 0x00131E0A
		[ConfigurationProperty("outputCacheProfiles")]
		public OutputCacheProfileCollection OutputCacheProfiles
		{
			get
			{
				return (OutputCacheProfileCollection)base[OutputCacheSettingsSection._propOutputCacheProfiles];
			}
		}

		// Token: 0x04002EB7 RID: 11959
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002EB8 RID: 11960
		private static readonly ConfigurationProperty _propOutputCacheProfiles = new ConfigurationProperty("outputCacheProfiles", typeof(OutputCacheProfileCollection), null, ConfigurationPropertyOptions.None);
	}
}
