using System;

namespace System.Xml.Schema
{
	// Token: 0x0200022F RID: 559
	internal class Datatype_integer : Datatype_decimal
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x000B74EF File Offset: 0x000B56EF
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Integer;
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000B74F4 File Offset: 0x000B56F4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = this.FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				decimal num;
				ex = XmlConvert.TryToInteger(s, out num);
				if (ex == null)
				{
					ex = this.FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}
	}
}
