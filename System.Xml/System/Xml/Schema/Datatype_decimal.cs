using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B8 RID: 440
	internal class Datatype_decimal : Datatype_anySimpleType
	{
		// Token: 0x06001675 RID: 5749 RVA: 0x00063142 File Offset: 0x00062142
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric10Converter.Create(schemaType);
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x0006314A File Offset: 0x0006214A
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_decimal.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00063151 File Offset: 0x00062151
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Decimal;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x00063155 File Offset: 0x00062155
		public override Type ValueType
		{
			get
			{
				return Datatype_decimal.atomicValueType;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0006315C File Offset: 0x0006215C
		internal override Type ListValueType
		{
			get
			{
				return Datatype_decimal.listValueType;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00063163 File Offset: 0x00062163
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00063166 File Offset: 0x00062166
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits;
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00063170 File Offset: 0x00062170
		internal override int Compare(object value1, object value2)
		{
			return ((decimal)value1).CompareTo(value2);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0006318C File Offset: 0x0006218C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_decimal.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				decimal num;
				ex = XmlConvert.TryToDecimal(s, out num);
				if (ex == null)
				{
					ex = Datatype_decimal.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000D80 RID: 3456
		private static readonly Type atomicValueType = typeof(decimal);

		// Token: 0x04000D81 RID: 3457
		private static readonly Type listValueType = typeof(decimal[]);

		// Token: 0x04000D82 RID: 3458
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, decimal.MaxValue);
	}
}
