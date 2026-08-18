using System;

namespace System.Xml.Schema
{
	// Token: 0x0200022A RID: 554
	internal class Datatype_NCName : Datatype_Name
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x000B7307 File Offset: 0x000B5507
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NCName;
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000B730C File Offset: 0x000B550C
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
