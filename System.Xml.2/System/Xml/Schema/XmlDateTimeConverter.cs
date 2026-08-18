using System;

namespace System.Xml.Schema
{
	// Token: 0x020002C4 RID: 708
	internal class XmlDateTimeConverter : XmlBaseConverter
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x000D8DE4 File Offset: 0x000D6FE4
		protected XmlDateTimeConverter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000D8DED File Offset: 0x000D6FED
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlDateTimeConverter(schemaType);
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000D8DF5 File Offset: 0x000D6FF5
		public override DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000D8DF8 File Offset: 0x000D6FF8
		public override DateTime ToDateTime(DateTimeOffset value)
		{
			return XmlBaseConverter.DateTimeOffsetToDateTime(value);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000D8E00 File Offset: 0x000D7000
		public override DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.StringToTime(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.StringToDate(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.StringToGYearMonth(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.StringToGYear(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.StringToGMonthDay(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.StringToGDay(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.StringToGMonth(value);
			default:
				return XmlBaseConverter.StringToDateTime(value);
			}
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000D8E80 File Offset: 0x000D7080
		public override DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return (DateTime)value;
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTime((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDateTime((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			return (DateTime)this.ChangeListType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000D8F15 File Offset: 0x000D7115
		public override DateTimeOffset ToDateTimeOffset(DateTime value)
		{
			return new DateTimeOffset(value);
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000D8F1D File Offset: 0x000D711D
		public override DateTimeOffset ToDateTimeOffset(DateTimeOffset value)
		{
			return value;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000D8F20 File Offset: 0x000D7120
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.StringToTimeOffset(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.StringToDateOffset(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.StringToGYearMonthOffset(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.StringToGYearOffset(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.StringToGMonthDayOffset(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.StringToGDayOffset(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.StringToGMonthOffset(value);
			default:
				return XmlBaseConverter.StringToDateTimeOffset(value);
			}
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000D8FA0 File Offset: 0x000D71A0
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTimeOffset((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return (DateTimeOffset)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDateTimeOffset((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			return (DateTimeOffset)this.ChangeListType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000D903C File Offset: 0x000D723C
		public override string ToString(DateTime value)
		{
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.TimeToString(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.DateToString(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.GYearMonthToString(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.GYearToString(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.GMonthDayToString(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.GDayToString(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.GMonthToString(value);
			default:
				return XmlBaseConverter.DateTimeToString(value);
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000D90B0 File Offset: 0x000D72B0
		public override string ToString(DateTimeOffset value)
		{
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.TimeOffsetToString(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.DateOffsetToString(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.GYearMonthOffsetToString(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.GYearOffsetToString(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.GMonthDayOffsetToString(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.GDayOffsetToString(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.GMonthOffsetToString(value);
			default:
				return XmlBaseConverter.DateTimeOffsetToString(value);
			}
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000D9122 File Offset: 0x000D7322
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000D9134 File Offset: 0x000D7334
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return this.ToString((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToString((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).Value;
			}
			return (string)this.ChangeListType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000D91CC File Offset: 0x000D73CC
		public override object ChangeType(DateTime value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000D9288 File Offset: 0x000D7488
		public override object ChangeType(DateTimeOffset value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000D9350 File Offset: 0x000D7550
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000D9414 File Offset: 0x000D7614
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.DateTimeType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTime)value);
				}
				if (type == XmlBaseConverter.DateTimeOffsetType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTimeOffset)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.DateTimeType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTime)value);
				}
				if (type == XmlBaseConverter.DateTimeOffsetType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTimeOffset)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
