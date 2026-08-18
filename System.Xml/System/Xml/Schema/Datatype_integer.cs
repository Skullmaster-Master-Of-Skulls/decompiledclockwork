using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D9 RID: 473
	internal class Datatype_integer : Datatype_decimal
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00063B90 File Offset: 0x00062B90
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Integer;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00063B94 File Offset: 0x00062B94
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
