using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Configuration.Internal
{
	// Token: 0x020000B8 RID: 184
	[ComVisible(false)]
	public interface IInternalConfigurationBuilderHost
	{
		// Token: 0x06000744 RID: 1860
		XmlNode ProcessRawXml(XmlNode rawXml, ConfigurationBuilder builder);

		// Token: 0x06000745 RID: 1861
		ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder builder);
	}
}
