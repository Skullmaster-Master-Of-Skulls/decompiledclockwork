using System;
using System.Configuration.Provider;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200001D RID: 29
	public abstract class ConfigurationBuilder : ProviderBase
	{
		// Token: 0x0600012B RID: 299 RVA: 0x0000935E File Offset: 0x0000755E
		public virtual XmlNode ProcessRawXml(XmlNode rawXml)
		{
			return rawXml;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000935E File Offset: 0x0000755E
		public virtual ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection)
		{
			return configSection;
		}
	}
}
