using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002E3 RID: 739
	internal class XmlFacetComparer : IComparer
	{
		// Token: 0x0600228E RID: 8846 RVA: 0x000A1564 File Offset: 0x000A0564
		public int Compare(object o1, object o2)
		{
			XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)o1;
			XmlSchemaFacet xmlSchemaFacet2 = (XmlSchemaFacet)o2;
			return string.Compare(xmlSchemaFacet.GetType().Name + ":" + xmlSchemaFacet.Value, xmlSchemaFacet2.GetType().Name + ":" + xmlSchemaFacet2.Value, StringComparison.Ordinal);
		}
	}
}
