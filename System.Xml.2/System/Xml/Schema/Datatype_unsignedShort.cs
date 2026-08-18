using System;

namespace System.Xml.Schema
{
	// Token: 0x02000239 RID: 569
	internal class Datatype_unsignedShort : Datatype_unsignedInt
	{
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x000B7A90 File Offset: 0x000B5C90
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedShort.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x000B7A97 File Offset: 0x000B5C97
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedShort;
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000B7A9C File Offset: 0x000B5C9C
		internal override int Compare(object value1, object value2)
		{
			return ((ushort)value1).CompareTo(value2);
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x000B7AB8 File Offset: 0x000B5CB8
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedShort.atomicValueType;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x000B7ABF File Offset: 0x000B5CBF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedShort.listValueType;
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000B7AC8 File Offset: 0x000B5CC8
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

		// Token: 0x04000E99 RID: 3737
		private static readonly Type atomicValueType = typeof(ushort);

		// Token: 0x04000E9A RID: 3738
		private static readonly Type listValueType = typeof(ushort[]);

		// Token: 0x04000E9B RID: 3739
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 65535m);
	}
}
