using System;

namespace System.Xml.Schema
{
	// Token: 0x020001DC RID: 476
	internal class Datatype_long : Datatype_integer
	{
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00063C51 File Offset: 0x00062C51
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_long.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00063C58 File Offset: 0x00062C58
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x00063C5B File Offset: 0x00062C5B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Long;
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00063C60 File Offset: 0x00062C60
		internal override int Compare(object value1, object value2)
		{
			return ((long)value1).CompareTo(value2);
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x00063C7C File Offset: 0x00062C7C
		public override Type ValueType
		{
			get
			{
				return Datatype_long.atomicValueType;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00063C83 File Offset: 0x00062C83
		internal override Type ListValueType
		{
			get
			{
				return Datatype_long.listValueType;
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00063C8C File Offset: 0x00062C8C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_long.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				long num;
				ex = XmlConvert.TryToInt64(s, out num);
				if (ex == null)
				{
					ex = Datatype_long.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000D94 RID: 3476
		private static readonly Type atomicValueType = typeof(long);

		// Token: 0x04000D95 RID: 3477
		private static readonly Type listValueType = typeof(long[]);

		// Token: 0x04000D96 RID: 3478
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-9223372036854775808m, 9223372036854775807m);
	}
}
