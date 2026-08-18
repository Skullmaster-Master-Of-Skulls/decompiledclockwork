using System;

namespace System.Xml.Schema
{
	// Token: 0x020001DE RID: 478
	internal class Datatype_short : Datatype_int
	{
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00063DF8 File Offset: 0x00062DF8
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_short.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x00063DFF File Offset: 0x00062DFF
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Short;
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00063E04 File Offset: 0x00062E04
		internal override int Compare(object value1, object value2)
		{
			return ((short)value1).CompareTo(value2);
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x00063E20 File Offset: 0x00062E20
		public override Type ValueType
		{
			get
			{
				return Datatype_short.atomicValueType;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00063E27 File Offset: 0x00062E27
		internal override Type ListValueType
		{
			get
			{
				return Datatype_short.listValueType;
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00063E30 File Offset: 0x00062E30
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_short.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				short num;
				ex = XmlConvert.TryToInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_short.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000D9A RID: 3482
		private static readonly Type atomicValueType = typeof(short);

		// Token: 0x04000D9B RID: 3483
		private static readonly Type listValueType = typeof(short[]);

		// Token: 0x04000D9C RID: 3484
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-32768m, 32767m);
	}
}
