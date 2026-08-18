using System;

namespace System.Xml.Schema
{
	// Token: 0x02000232 RID: 562
	internal class Datatype_long : Datatype_integer
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x000B75A7 File Offset: 0x000B57A7
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_long.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x000B75AE File Offset: 0x000B57AE
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x000B75B1 File Offset: 0x000B57B1
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Long;
			}
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000B75B8 File Offset: 0x000B57B8
		internal override int Compare(object value1, object value2)
		{
			return ((long)value1).CompareTo(value2);
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x000B75D4 File Offset: 0x000B57D4
		public override Type ValueType
		{
			get
			{
				return Datatype_long.atomicValueType;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x000B75DB File Offset: 0x000B57DB
		internal override Type ListValueType
		{
			get
			{
				return Datatype_long.listValueType;
			}
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000B75E4 File Offset: 0x000B57E4
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

		// Token: 0x04000E86 RID: 3718
		private static readonly Type atomicValueType = typeof(long);

		// Token: 0x04000E87 RID: 3719
		private static readonly Type listValueType = typeof(long[]);

		// Token: 0x04000E88 RID: 3720
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-9223372036854775808m, 9223372036854775807m);
	}
}
