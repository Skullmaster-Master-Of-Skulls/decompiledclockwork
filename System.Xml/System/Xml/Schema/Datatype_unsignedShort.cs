using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E3 RID: 483
	internal class Datatype_unsignedShort : Datatype_unsignedInt
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0006413E File Offset: 0x0006313E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedShort.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00064145 File Offset: 0x00063145
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedShort;
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0006414C File Offset: 0x0006314C
		internal override int Compare(object value1, object value2)
		{
			return ((ushort)value1).CompareTo(value2);
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00064168 File Offset: 0x00063168
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedShort.atomicValueType;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0006416F File Offset: 0x0006316F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedShort.listValueType;
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00064178 File Offset: 0x00063178
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedShort.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ushort num;
				ex = XmlConvert.TryToUInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedShort.numeric10FacetsChecker.CheckValueFacets((int)num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000DA7 RID: 3495
		private static readonly Type atomicValueType = typeof(ushort);

		// Token: 0x04000DA8 RID: 3496
		private static readonly Type listValueType = typeof(ushort[]);

		// Token: 0x04000DA9 RID: 3497
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 65535m);
	}
}
