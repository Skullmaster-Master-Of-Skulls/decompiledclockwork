using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000169 RID: 361
	internal class XmlFacetComparer : IComparer
	{
		// Token: 0x06001845 RID: 6213 RVA: 0x00069928 File Offset: 0x00067B28
		public int Compare(object o1, object o2)
		{
			XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)o1;
			XmlSchemaFacet xmlSchemaFacet2 = (XmlSchemaFacet)o2;
			return string.Compare(xmlSchemaFacet.GetType().Name + ":" + xmlSchemaFacet.Value, xmlSchemaFacet2.GetType().Name + ":" + xmlSchemaFacet2.Value, StringComparison.Ordinal);
		}
	}
}
