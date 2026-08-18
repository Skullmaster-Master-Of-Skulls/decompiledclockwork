using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B9 RID: 441
	internal class Datatype_duration : Datatype_anySimpleType
	{
		// Token: 0x06001680 RID: 5760 RVA: 0x0006322D File Offset: 0x0006222D
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x00063235 File Offset: 0x00062235
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.durationFacetsChecker;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0006323C File Offset: 0x0006223C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Duration;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00063240 File Offset: 0x00062240
		public override Type ValueType
		{
			get
			{
				return Datatype_duration.atomicValueType;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00063247 File Offset: 0x00062247
		internal override Type ListValueType
		{
			get
			{
				return Datatype_duration.listValueType;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0006324E File Offset: 0x0006224E
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00063251 File Offset: 0x00062251
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00063258 File Offset: 0x00062258
		internal override int Compare(object value1, object value2)
		{
			return ((TimeSpan)value1).CompareTo(value2);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00063274 File Offset: 0x00062274
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

		// Token: 0x04000D83 RID: 3459
		private static readonly Type atomicValueType = typeof(TimeSpan);

		// Token: 0x04000D84 RID: 3460
		private static readonly Type listValueType = typeof(TimeSpan[]);
	}
}
