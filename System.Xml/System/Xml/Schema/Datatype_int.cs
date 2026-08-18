using System;

namespace System.Xml.Schema
{
	// Token: 0x020001DD RID: 477
	internal class Datatype_int : Datatype_long
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x00063D31 File Offset: 0x00062D31
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_int.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x00063D38 File Offset: 0x00062D38
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Int;
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00063D3C File Offset: 0x00062D3C
		internal override int Compare(object value1, object value2)
		{
			return ((int)value1).CompareTo(value2);
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x00063D58 File Offset: 0x00062D58
		public override Type ValueType
		{
			get
			{
				return Datatype_int.atomicValueType;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00063D5F File Offset: 0x00062D5F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_int.listValueType;
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00063D68 File Offset: 0x00062D68
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_int.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				int num;
				ex = XmlConvert.TryToInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_int.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000D97 RID: 3479
		private static readonly Type atomicValueType = typeof(int);

		// Token: 0x04000D98 RID: 3480
		private static readonly Type listValueType = typeof(int[]);

		// Token: 0x04000D99 RID: 3481
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-2147483648m, 2147483647m);
	}
}
