using System;

namespace System.Xml.Schema
{
	// Token: 0x020001DF RID: 479
	internal class Datatype_byte : Datatype_short
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00063EC0 File Offset: 0x00062EC0
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_byte.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00063EC7 File Offset: 0x00062EC7
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Byte;
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00063ECC File Offset: 0x00062ECC
		internal override int Compare(object value1, object value2)
		{
			return ((sbyte)value1).CompareTo(value2);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00063EE8 File Offset: 0x00062EE8
		public override Type ValueType
		{
			get
			{
				return Datatype_byte.atomicValueType;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00063EEF File Offset: 0x00062EEF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_byte.listValueType;
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00063EF8 File Offset: 0x00062EF8
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_byte.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				sbyte b;
				ex = XmlConvert.TryToSByte(s, out b);
				if (ex == null)
				{
					ex = Datatype_byte.numeric10FacetsChecker.CheckValueFacets((short)b, this);
					if (ex == null)
					{
						typedValue = b;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000D9D RID: 3485
		private static readonly Type atomicValueType = typeof(sbyte);

		// Token: 0x04000D9E RID: 3486
		private static readonly Type listValueType = typeof(sbyte[]);

		// Token: 0x04000D9F RID: 3487
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-128m, 127m);
	}
}
