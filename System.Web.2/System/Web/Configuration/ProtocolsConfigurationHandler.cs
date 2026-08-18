using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x0200073B RID: 1851
	public sealed class ProtocolsConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x0600593F RID: 22847 RVA: 0x00137678 File Offset: 0x00135878
		public object Create(object parent, object configContextObj, XmlNode section)
		{
			return new ProtocolsConfiguration(section);
		}
	}
}
