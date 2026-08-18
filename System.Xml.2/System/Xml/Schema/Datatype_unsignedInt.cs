using System;

namespace System.Xml.Schema
{
	// Token: 0x02000238 RID: 568
	internal class Datatype_unsignedInt : Datatype_unsignedLong
	{
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000B79D1 File Offset: 0x000B5BD1
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedInt.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x000B79D8 File Offset: 0x000B5BD8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedInt;
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000B79DC File Offset: 0x000B5BDC
		internal override int Compare(object value1, object value2)
		{
			return ((uint)value1).CompareTo(value2);
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000B79F8 File Offset: 0x000B5BF8
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedInt.atomicValueType;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000B79FF File Offset: 0x000B5BFF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedInt.listValueType;
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000B7A08 File Offset: 0x000B5C08
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

		// Token: 0x04000E96 RID: 3734
		private static readonly Type atomicValueType = typeof(uint);

		// Token: 0x04000E97 RID: 3735
		private static readonly Type listValueType = typeof(uint[]);

		// Token: 0x04000E98 RID: 3736
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 4294967295m);
	}
}
