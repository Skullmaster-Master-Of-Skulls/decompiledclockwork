using System;

namespace System.Xml.Schema
{
	// Token: 0x02000212 RID: 530
	internal class Datatype_dateTimeBase : Datatype_anySimpleType
	{
		// Token: 0x060021BB RID: 8635 RVA: 0x000B6D74 File Offset: 0x000B4F74
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlDateTimeConverter.Create(schemaType);
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x000B6D7C File Offset: 0x000B4F7C
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.dateTimeFacetsChecker;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000B6D83 File Offset: 0x000B4F83
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DateTime;
			}
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000B6D87 File Offset: 0x000B4F87
		internal Datatype_dateTimeBase()
		{
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000B6D8F File Offset: 0x000B4F8F
		internal Datatype_dateTimeBase(XsdDateTimeFlags dateTimeFlags)
		{
			this.dateTimeFlags = dateTimeFlags;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x000B6D9E File Offset: 0x000B4F9E
		public override Type ValueType
		{
			get
			{
				return Datatype_dateTimeBase.atomicValueType;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x000B6DA5 File Offset: 0x000B4FA5
		internal override Type ListValueType
		{
			get
			{
				return Datatype_dateTimeBase.listValueType;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x000B6DAC File Offset: 0x000B4FAC
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x000B6DAF File Offset: 0x000B4FAF
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000B6DB8 File Offset: 0x000B4FB8
		internal override int Compare(object value1, object value2)
		{
			DateTime dateTime = (DateTime)value1;
			DateTime value3 = (DateTime)value2;
			if (dateTime.Kind == DateTimeKind.Unspecified || value3.Kind == DateTimeKind.Unspecified)
			{
				return dateTime.CompareTo(value3);
			}
			return dateTime.ToUniversalTime().CompareTo(value3.ToUniversalTime());
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000B6E04 File Offset: 0x000B5004
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.dateTimeFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XsdDateTime xdt;
				if (!XsdDateTime.TryParse(s, this.dateTimeFlags, out xdt))
				{
					ex = new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
					{
						s,
						this.dateTimeFlags.ToString()
					}));
				}
				else
				{
					DateTime dateTime = DateTime.MinValue;
					try
					{
						dateTime = xdt;
					}
					catch (ArgumentException result)
					{
						return result;
					}
					ex = DatatypeImplementation.dateTimeFacetsChecker.CheckValueFacets(dateTime, this);
					if (ex == null)
					{
						typedValue = dateTime;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000E77 RID: 3703
		private static readonly Type atomicValueType = typeof(DateTime);

		// Token: 0x04000E78 RID: 3704
		private static readonly Type listValueType = typeof(DateTime[]);

		// Token: 0x04000E79 RID: 3705
		private XsdDateTimeFlags dateTimeFlags;
	}
}
