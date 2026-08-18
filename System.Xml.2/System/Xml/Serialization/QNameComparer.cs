using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200016A RID: 362
	internal class QNameComparer : IComparer
	{
		// Token: 0x06001847 RID: 6215 RVA: 0x00069988 File Offset: 0x00067B88
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
