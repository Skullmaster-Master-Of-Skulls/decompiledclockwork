using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D4 RID: 468
	internal class Datatype_NCName : Datatype_Name
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0006398F File Offset: 0x0006298F
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NCName;
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00063994 File Offset: 0x00062994
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ex = DatatypeImplementation.stringFacetsChecker.CheckValueFacets(s, this);
				if (ex == null)
				{
					nameTable.Add(s);
					typedValue = s;
					return null;
				}
			}
			return ex;
		}
	}
}
