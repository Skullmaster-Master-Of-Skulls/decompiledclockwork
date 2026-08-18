using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000099 RID: 153
	internal sealed class AllowedTypesSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x000567D4 File Offset: 0x00055BD4
		public object Create(object parent, object configContext, XmlNode section)
		{
			XmlAttribute xmlAttribute = section.Attributes["auditOnly"];
			bool auditMode;
			bool.TryParse((xmlAttribute != null) ? xmlAttribute.Value : null, out auditMode);
			HashSet<string> hashSet = new HashSet<string>();
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode != null && xmlNode.Attributes != null)
				{
					string name = xmlNode.Name;
					XmlAttribute xmlAttribute2 = xmlNode.Attributes["type"];
					string item = (xmlAttribute2 == null) ? null : xmlAttribute2.Value;
					if (name == "add")
					{
						hashSet.Add(item);
					}
					else if (name == "remove")
					{
						hashSet.Remove(item);
					}
					else
					{
						if (!(name == "clear"))
						{
							throw ExceptionBuilder.ConfigElementNotAllowed(xmlNode);
						}
						hashSet.Clear();
					}
				}
			}
			return new AllowedTypesSectionHandler.Data
			{
				AuditMode = auditMode,
				AllowedTypes = hashSet
			};
		}

		// Token: 0x02000345 RID: 837
		internal sealed class Data
		{
			// Token: 0x04001EAC RID: 7852
			internal bool AuditMode;

			// Token: 0x04001EAD RID: 7853
			internal IEnumerable<string> AllowedTypes = new List<string>();
		}
	}
}
