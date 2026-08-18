using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002E4 RID: 740
	internal class QNameComparer : IComparer
	{
		// Token: 0x06002290 RID: 8848 RVA: 0x000A15C4 File Offset: 0x000A05C4
		public int Compare(object o1, object o2)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)o1;
			XmlQualifiedName xmlQualifiedName2 = (XmlQualifiedName)o2;
			int num = string.Compare(xmlQualifiedName.Namespace, xmlQualifiedName2.Namespace, StringComparison.Ordinal);
			if (num == 0)
			{
				return string.Compare(xmlQualifiedName.Name, xmlQualifiedName2.Name, StringComparison.Ordinal);
			}
			return num;
		}
	}
}
