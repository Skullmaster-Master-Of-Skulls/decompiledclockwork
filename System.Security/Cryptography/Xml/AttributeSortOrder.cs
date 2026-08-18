using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000092 RID: 146
	internal class AttributeSortOrder : IComparer
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0000EBE9 File Offset: 0x0000DBE9
		internal AttributeSortOrder()
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000EBF4 File Offset: 0x0000DBF4
		public int Compare(object a, object b)
		{
			XmlNode xmlNode = a as XmlNode;
			XmlNode xmlNode2 = b as XmlNode;
			if (a == null || b == null)
			{
				throw new ArgumentException();
			}
			int num = string.CompareOrdinal(xmlNode.NamespaceURI, xmlNode2.NamespaceURI);
			if (num != 0)
			{
				return num;
			}
			return string.CompareOrdinal(xmlNode.LocalName, xmlNode2.LocalName);
		}
	}
}
