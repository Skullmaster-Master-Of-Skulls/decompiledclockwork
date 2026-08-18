using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace System.Data
{
	// Token: 0x0200019C RID: 412
	internal sealed class AllowedTypesSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x06001831 RID: 6193 RVA: 0x00250868 File Offset: 0x0024FC68
		public object Create(object parent, object configContext, XmlNode section)
		{
			XmlAttribute xmlAttribute = section.Attributes["auditOnly"];
			bool auditMode = false;
			if (xmlAttribute != null)
			{
				bool.TryParse(xmlAttribute.Value, out auditMode);
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode != null && xmlNode.Attributes != null)
				{
					string name = xmlNode.Name;
					XmlAttribute xmlAttribute2 = xmlNode.Attributes["type"];
					string key = (xmlAttribute2 == null) ? null : xmlAttribute2.Value;
					if (name == "add")
					{
						dictionary[key] = null;
					}
					else if (name == "remove")
					{
						dictionary.Remove(key);
					}
					else
					{
						if (!(name == "clear"))
						{
							throw ExceptionBuilder.ConfigElementNotAllowed(xmlNode);
						}
						dictionary.Clear();
					}
				}
			}
			return new AllowedTypesSectionHandler.Data
			{
				AuditMode = auditMode,
				AllowedTypes = dictionary.Keys
			};
		}

		// Token: 0x0200019D RID: 413
		internal sealed class Data
		{
			// Token: 0x04000D1E RID: 3358
			internal bool AuditMode;

			// Token: 0x04000D1F RID: 3359
			internal IEnumerable<string> AllowedTypes = new List<string>();
		}
	}
}
