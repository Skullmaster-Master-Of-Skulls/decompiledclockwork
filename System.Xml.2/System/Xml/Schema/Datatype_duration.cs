using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020F RID: 527
	internal class Datatype_duration : Datatype_anySimpleType
	{
		// Token: 0x060021AA RID: 8618 RVA: 0x000B6B9C File Offset: 0x000B4D9C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x000B6BA4 File Offset: 0x000B4DA4
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.durationFacetsChecker;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x000B6BAB File Offset: 0x000B4DAB
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Duration;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x000B6BAF File Offset: 0x000B4DAF
		public override Type ValueType
		{
			get
			{
				return Datatype_duration.atomicValueType;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000B6BB6 File Offset: 0x000B4DB6
		internal override Type ListValueType
		{
			get
			{
				return Datatype_duration.listValueType;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000B6BBD File Offset: 0x000B4DBD
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000B6BC0 File Offset: 0x000B4DC0
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000B6BC8 File Offset: 0x000B4DC8
		internal override int Compare(object value1, object value2)
		{
			return ((TimeSpan)value1).CompareTo(value2);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000B6BE4 File Offset: 0x000B4DE4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("Sch_EmptyAttributeValue", string.Empty);
			}
			Exception ex = DatatypeImplementation.durationFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				TimeSpan timeSpan;
				ex = XmlConvert.TryToTimeSpan(s, out timeSpan);
				if (ex == null)
				{
					ex = DatatypeImplementation.durationFacetsChecker.CheckValueFacets(timeSpan, this);
					if (ex == null)
					{
						typedValue = timeSpan;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000E75 RID: 3701
		private static readonly Type atomicValueType = typeof(TimeSpan);

		// Token: 0x04000E76 RID: 3702
		private static readonly Type listValueType = typeof(TimeSpan[]);
	}
}
