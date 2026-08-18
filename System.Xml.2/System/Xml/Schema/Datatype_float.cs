using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020C RID: 524
	internal class Datatype_float : Datatype_anySimpleType
	{
		// Token: 0x06002189 RID: 8585 RVA: 0x000B694A File Offset: 0x000B4B4A
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x000B6952 File Offset: 0x000B4B52
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x000B6959 File Offset: 0x000B4B59
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Float;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x000B695D File Offset: 0x000B4B5D
		public override Type ValueType
		{
			get
			{
				return Datatype_float.atomicValueType;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x000B6964 File Offset: 0x000B4B64
		internal override Type ListValueType
		{
			get
			{
				return Datatype_float.listValueType;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x000B696B File Offset: 0x000B4B6B
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x000B696E File Offset: 0x000B4B6E
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000B6978 File Offset: 0x000B4B78
		internal override int Compare(object value1, object value2)
		{
			return ((float)value1).CompareTo(value2);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000B6994 File Offset: 0x000B4B94
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				float num;
				ex = XmlConvert.TryToSingle(s, out num);
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

		// Token: 0x04000E6E RID: 3694
		private static readonly Type atomicValueType = typeof(float);

		// Token: 0x04000E6F RID: 3695
		private static readonly Type listValueType = typeof(float[]);
	}
}
