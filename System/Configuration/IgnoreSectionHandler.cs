using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006FB RID: 1787
	public class IgnoreSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003729 RID: 14121 RVA: 0x000EA7CD File Offset: 0x000E97CD
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return null;
		}
	}
}
