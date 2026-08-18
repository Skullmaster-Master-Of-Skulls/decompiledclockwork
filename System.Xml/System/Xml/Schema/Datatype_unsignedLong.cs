using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E1 RID: 481
	internal class Datatype_unsignedLong : Datatype_nonNegativeInteger
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00063FB4 File Offset: 0x00062FB4
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedLong.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00063FBB File Offset: 0x00062FBB
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedLong;
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00063FC0 File Offset: 0x00062FC0
		internal override int Compare(object value1, object value2)
		{
			return ((ulong)value1).CompareTo(value2);
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00063FDC File Offset: 0x00062FDC
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedLong.atomicValueType;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00063FE3 File Offset: 0x00062FE3
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedLong.listValueType;
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00063FEC File Offset: 0x00062FEC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedLong.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ulong num;
				ex = XmlConvert.TryToUInt64(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedLong.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000DA1 RID: 3489
		private static readonly Type atomicValueType = typeof(ulong);

		// Token: 0x04000DA2 RID: 3490
		private static readonly Type listValueType = typeof(ulong[]);

		// Token: 0x04000DA3 RID: 3491
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 18446744073709551615m);
	}
}
