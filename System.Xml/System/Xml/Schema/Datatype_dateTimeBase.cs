using System;

namespace System.Xml.Schema
{
	// Token: 0x020001BC RID: 444
	internal class Datatype_dateTimeBase : Datatype_anySimpleType
	{
		// Token: 0x06001691 RID: 5777 RVA: 0x00063404 File Offset: 0x00062404
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlDateTimeConverter.Create(schemaType);
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0006340C File Offset: 0x0006240C
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.dateTimeFacetsChecker;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x00063413 File Offset: 0x00062413
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DateTime;
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00063417 File Offset: 0x00062417
		internal Datatype_dateTimeBase()
		{
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0006341F File Offset: 0x0006241F
		internal Datatype_dateTimeBase(XsdDateTimeFlags dateTimeFlags)
		{
			this.dateTimeFlags = dateTimeFlags;
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0006342E File Offset: 0x0006242E
		public override Type ValueType
		{
			get
			{
				return Datatype_dateTimeBase.atomicValueType;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x00063435 File Offset: 0x00062435
		internal override Type ListValueType
		{
			get
			{
				return Datatype_dateTimeBase.listValueType;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0006343C File Offset: 0x0006243C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0006343F File Offset: 0x0006243F
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00063448 File Offset: 0x00062448
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

		// Token: 0x0600169B RID: 5787 RVA: 0x00063494 File Offset: 0x00062494
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
						"XsdDateTime"
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

		// Token: 0x04000D85 RID: 3461
		private static readonly Type atomicValueType = typeof(DateTime);

		// Token: 0x04000D86 RID: 3462
		private static readonly Type listValueType = typeof(DateTime[]);

		// Token: 0x04000D87 RID: 3463
		private XsdDateTimeFlags dateTimeFlags;
	}
}
