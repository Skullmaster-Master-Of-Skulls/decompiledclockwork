using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020E RID: 526
	internal class Datatype_decimal : Datatype_anySimpleType
	{
		// Token: 0x0600219F RID: 8607 RVA: 0x000B6AC2 File Offset: 0x000B4CC2
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric10Converter.Create(schemaType);
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x000B6ACA File Offset: 0x000B4CCA
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_decimal.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x000B6AD1 File Offset: 0x000B4CD1
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Decimal;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x000B6AD5 File Offset: 0x000B4CD5
		public override Type ValueType
		{
			get
			{
				return Datatype_decimal.atomicValueType;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x000B6ADC File Offset: 0x000B4CDC
		internal override Type ListValueType
		{
			get
			{
				return Datatype_decimal.listValueType;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x000B6AE3 File Offset: 0x000B4CE3
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000B6AE6 File Offset: 0x000B4CE6
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits;
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000B6AF0 File Offset: 0x000B4CF0
		internal override int Compare(object value1, object value2)
		{
			return ((decimal)value1).CompareTo(value2);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000B6B0C File Offset: 0x000B4D0C
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

		// Token: 0x04000E72 RID: 3698
		private static readonly Type atomicValueType = typeof(decimal);

		// Token: 0x04000E73 RID: 3699
		private static readonly Type listValueType = typeof(decimal[]);

		// Token: 0x04000E74 RID: 3700
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, decimal.MaxValue);
	}
}
