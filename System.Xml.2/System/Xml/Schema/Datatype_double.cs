using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020D RID: 525
	internal class Datatype_double : Datatype_anySimpleType
	{
		// Token: 0x06002194 RID: 8596 RVA: 0x000B6A06 File Offset: 0x000B4C06
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000B6A0E File Offset: 0x000B4C0E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x000B6A15 File Offset: 0x000B4C15
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Double;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000B6A19 File Offset: 0x000B4C19
		public override Type ValueType
		{
			get
			{
				return Datatype_double.atomicValueType;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000B6A20 File Offset: 0x000B4C20
		internal override Type ListValueType
		{
			get
			{
				return Datatype_double.listValueType;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x000B6A27 File Offset: 0x000B4C27
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x000B6A2A File Offset: 0x000B4C2A
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000B6A34 File Offset: 0x000B4C34
		internal override int Compare(object value1, object value2)
		{
			return ((double)value1).CompareTo(value2);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000B6A50 File Offset: 0x000B4C50
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

		// Token: 0x04000E70 RID: 3696
		private static readonly Type atomicValueType = typeof(double);

		// Token: 0x04000E71 RID: 3697
		private static readonly Type listValueType = typeof(double[]);
	}
}
