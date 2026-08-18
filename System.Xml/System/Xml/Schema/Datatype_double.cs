using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B7 RID: 439
	internal class Datatype_double : Datatype_anySimpleType
	{
		// Token: 0x0600166A RID: 5738 RVA: 0x00063086 File Offset: 0x00062086
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0006308E File Offset: 0x0006208E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x00063095 File Offset: 0x00062095
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Double;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00063099 File Offset: 0x00062099
		public override Type ValueType
		{
			get
			{
				return Datatype_double.atomicValueType;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x000630A0 File Offset: 0x000620A0
		internal override Type ListValueType
		{
			get
			{
				return Datatype_double.listValueType;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x000630A7 File Offset: 0x000620A7
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x000630AA File Offset: 0x000620AA
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000630B4 File Offset: 0x000620B4
		internal override int Compare(object value1, object value2)
		{
			return ((double)value1).CompareTo(value2);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000630D0 File Offset: 0x000620D0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				double num;
				ex = XmlConvert.TryToDouble(s, out num);
				if (ex == null)
				{
					ex = DatatypeImplementation.numeric2FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000D7E RID: 3454
		private static readonly Type atomicValueType = typeof(double);

		// Token: 0x04000D7F RID: 3455
		private static readonly Type listValueType = typeof(double[]);
	}
}
