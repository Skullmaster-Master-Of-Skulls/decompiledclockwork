using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000168 RID: 360
	internal class XmlAttributeComparer : IComparer
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x000698D8 File Offset: 0x00067AD8
		public int Compare(object o1, object o2)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)o1;
			XmlAttribute xmlAttribute2 = (XmlAttribute)o2;
			int num = string.Compare(xmlAttribute.NamespaceURI, xmlAttribute2.NamespaceURI, StringComparison.Ordinal);
			if (num == 0)
			{
				return string.Compare(xmlAttribute.Name, xmlAttribute2.Name, StringComparison.Ordinal);
			}
			return num;
		}
	}
}
