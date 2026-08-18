using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200071D RID: 1821
	public class SingleTagSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x060037C5 RID: 14277 RVA: 0x000EC318 File Offset: 0x000EB318
		public virtual object Create(object parent, object context, XmlNode section)
		{
			Hashtable hashtable;
			if (parent == null)
			{
				hashtable = new Hashtable();
			}
			else
			{
				hashtable = new Hashtable((IDictionary)parent);
			}
			HandlerBase.CheckForChildNodes(section);
			foreach (object obj in section.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				hashtable[xmlAttribute.Name] = xmlAttribute.Value;
			}
			return hashtable;
		}
	}
}
