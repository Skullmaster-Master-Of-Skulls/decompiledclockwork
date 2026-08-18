using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E2 RID: 482
	internal class Datatype_unsignedInt : Datatype_unsignedLong
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0006407D File Offset: 0x0006307D
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedInt.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x00064084 File Offset: 0x00063084
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedInt;
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00064088 File Offset: 0x00063088
		internal override int Compare(object value1, object value2)
		{
			return ((uint)value1).CompareTo(value2);
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x000640A4 File Offset: 0x000630A4
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedInt.atomicValueType;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x000640AB File Offset: 0x000630AB
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedInt.listValueType;
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000640B4 File Offset: 0x000630B4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedInt.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				uint num;
				ex = XmlConvert.TryToUInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedInt.numeric10FacetsChecker.CheckValueFacets((long)((ulong)num), this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000DA4 RID: 3492
		private static readonly Type atomicValueType = typeof(uint);

		// Token: 0x04000DA5 RID: 3493
		private static readonly Type listValueType = typeof(uint[]);

		// Token: 0x04000DA6 RID: 3494
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 4294967295m);
	}
}
