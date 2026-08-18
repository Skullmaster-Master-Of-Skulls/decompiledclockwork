using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023A RID: 570
	internal class Datatype_unsignedByte : Datatype_unsignedShort
	{
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x000B7B53 File Offset: 0x000B5D53
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedByte.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x000B7B5A File Offset: 0x000B5D5A
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedByte;
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000B7B60 File Offset: 0x000B5D60
		internal override int Compare(object value1, object value2)
		{
			return ((byte)value1).CompareTo(value2);
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000B7B7C File Offset: 0x000B5D7C
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedByte.atomicValueType;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000B7B83 File Offset: 0x000B5D83
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedByte.listValueType;
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000B7B8C File Offset: 0x000B5D8C
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

		// Token: 0x04000E9C RID: 3740
		private static readonly Type atomicValueType = typeof(byte);

		// Token: 0x04000E9D RID: 3741
		private static readonly Type listValueType = typeof(byte[]);

		// Token: 0x04000E9E RID: 3742
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 255m);
	}
}
