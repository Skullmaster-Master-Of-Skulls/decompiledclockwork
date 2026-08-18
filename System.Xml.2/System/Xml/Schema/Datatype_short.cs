using System;

namespace System.Xml.Schema
{
	// Token: 0x02000234 RID: 564
	internal class Datatype_short : Datatype_int
	{
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x000B7750 File Offset: 0x000B5950
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_short.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x000B7757 File Offset: 0x000B5957
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Short;
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000B775C File Offset: 0x000B595C
		internal override int Compare(object value1, object value2)
		{
			return ((short)value1).CompareTo(value2);
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x000B7778 File Offset: 0x000B5978
		public override Type ValueType
		{
			get
			{
				return Datatype_short.atomicValueType;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000B777F File Offset: 0x000B597F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_short.listValueType;
			}
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000B7788 File Offset: 0x000B5988
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_short.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				short num;
				ex = XmlConvert.TryToInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_short.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000E8C RID: 3724
		private static readonly Type atomicValueType = typeof(short);

		// Token: 0x04000E8D RID: 3725
		private static readonly Type listValueType = typeof(short[]);

		// Token: 0x04000E8E RID: 3726
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-32768m, 32767m);
	}
}
