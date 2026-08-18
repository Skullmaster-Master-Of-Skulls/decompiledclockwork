using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200002D RID: 45
	internal class AttributeSortOrder : IComparer
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000044A9 File Offset: 0x000026A9
		internal AttributeSortOrder()
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000627C File Offset: 0x0000447C
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
