using System;

namespace System.Xml.Schema
{
	// Token: 0x02000237 RID: 567
	internal class Datatype_unsignedLong : Datatype_nonNegativeInteger
	{
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000B790B File Offset: 0x000B5B0B
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedLong.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x000B7912 File Offset: 0x000B5B12
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedLong;
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000B7918 File Offset: 0x000B5B18
		internal override int Compare(object value1, object value2)
		{
			return ((ulong)value1).CompareTo(value2);
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x000B7934 File Offset: 0x000B5B34
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedLong.atomicValueType;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x000B793B File Offset: 0x000B5B3B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedLong.listValueType;
			}
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000B7944 File Offset: 0x000B5B44
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

		// Token: 0x04000E93 RID: 3731
		private static readonly Type atomicValueType = typeof(ulong);

		// Token: 0x04000E94 RID: 3732
		private static readonly Type listValueType = typeof(ulong[]);

		// Token: 0x04000E95 RID: 3733
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 18446744073709551615m);
	}
}
