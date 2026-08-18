using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E4 RID: 484
	internal class Datatype_unsignedByte : Datatype_unsignedShort
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00064204 File Offset: 0x00063204
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedByte.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x0006420B File Offset: 0x0006320B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedByte;
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00064210 File Offset: 0x00063210
		internal override int Compare(object value1, object value2)
		{
			return ((byte)value1).CompareTo(value2);
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0006422C File Offset: 0x0006322C
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedByte.atomicValueType;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00064233 File Offset: 0x00063233
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedByte.listValueType;
			}
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0006423C File Offset: 0x0006323C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedByte.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte b;
				ex = XmlConvert.TryToByte(s, out b);
				if (ex == null)
				{
					ex = Datatype_unsignedByte.numeric10FacetsChecker.CheckValueFacets((short)b, this);
					if (ex == null)
					{
						typedValue = b;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000DAA RID: 3498
		private static readonly Type atomicValueType = typeof(byte);

		// Token: 0x04000DAB RID: 3499
		private static readonly Type listValueType = typeof(byte[]);

		// Token: 0x04000DAC RID: 3500
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 255m);
	}
}
