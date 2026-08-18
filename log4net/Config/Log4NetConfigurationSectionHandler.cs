using System;
using System.Configuration;
using System.Xml;

namespace log4net.Config
{
	// Token: 0x02000051 RID: 81
	public class Log4NetConfigurationSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x000094B1 File Offset: 0x000076B1
		public object Create(object parent, object configContext, XmlNode section)
		{
			return section;
		}
	}
}
