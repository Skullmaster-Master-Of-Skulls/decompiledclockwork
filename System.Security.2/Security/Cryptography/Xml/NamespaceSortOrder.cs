using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200002E RID: 46
	internal class NamespaceSortOrder : IComparer
	{
		// Token: 0x0600012E RID: 302 RVA: 0x000044A9 File Offset: 0x000026A9
		internal NamespaceSortOrder()
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000062CC File Offset: 0x000044CC
		public int Compare(object a, object b)
		{
			XmlNode xmlNode = a as XmlNode;
			XmlNode xmlNode2 = b as XmlNode;
			if (a == null || b == null)
			{
				throw new ArgumentException();
			}
			bool flag = Utils.IsDefaultNamespaceNode(xmlNode);
			bool flag2 = Utils.IsDefaultNamespaceNode(xmlNode2);
			if (flag && flag2)
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			if (flag2)
			{
				return 1;
			}
			return string.CompareOrdinal(xmlNode.LocalName, xmlNode2.LocalName);
		}
	}
}
