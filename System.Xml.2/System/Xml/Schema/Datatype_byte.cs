using System;

namespace System.Xml.Schema
{
	// Token: 0x02000235 RID: 565
	internal class Datatype_byte : Datatype_short
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x000B7818 File Offset: 0x000B5A18
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_byte.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000B781F File Offset: 0x000B5A1F
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Byte;
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000B7824 File Offset: 0x000B5A24
		internal override int Compare(object value1, object value2)
		{
			return ((sbyte)value1).CompareTo(value2);
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000B7840 File Offset: 0x000B5A40
		public override Type ValueType
		{
			get
			{
				return Datatype_byte.atomicValueType;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000B7847 File Offset: 0x000B5A47
		internal override Type ListValueType
		{
			get
			{
				return Datatype_byte.listValueType;
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000B7850 File Offset: 0x000B5A50
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

		// Token: 0x04000E8F RID: 3727
		private static readonly Type atomicValueType = typeof(sbyte);

		// Token: 0x04000E90 RID: 3728
		private static readonly Type listValueType = typeof(sbyte[]);

		// Token: 0x04000E91 RID: 3729
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-128m, 127m);
	}
}
