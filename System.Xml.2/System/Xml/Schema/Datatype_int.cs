using System;

namespace System.Xml.Schema
{
	// Token: 0x02000233 RID: 563
	internal class Datatype_int : Datatype_long
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000B7689 File Offset: 0x000B5889
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_int.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x000B7690 File Offset: 0x000B5890
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Int;
			}
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000B7694 File Offset: 0x000B5894
		internal override int Compare(object value1, object value2)
		{
			return ((int)value1).CompareTo(value2);
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x000B76B0 File Offset: 0x000B58B0
		public override Type ValueType
		{
			get
			{
				return Datatype_int.atomicValueType;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x000B76B7 File Offset: 0x000B58B7
		internal override Type ListValueType
		{
			get
			{
				return Datatype_int.listValueType;
			}
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000B76C0 File Offset: 0x000B58C0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_int.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				int num;
				ex = XmlConvert.TryToInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_int.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000E89 RID: 3721
		private static readonly Type atomicValueType = typeof(int);

		// Token: 0x04000E8A RID: 3722
		private static readonly Type listValueType = typeof(int[]);

		// Token: 0x04000E8B RID: 3723
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-2147483648m, 2147483647m);
	}
}
