using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020002C6 RID: 710
	internal class XmlMiscConverter : XmlBaseConverter
	{
		// Token: 0x06002A09 RID: 10761 RVA: 0x000D9982 File Offset: 0x000D7B82
		protected XmlMiscConverter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000D998B File Offset: 0x000D7B8B
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlMiscConverter(schemaType);
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000D9993 File Offset: 0x000D7B93
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000D99A4 File Offset: 0x000D7BA4
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.ByteArrayType)
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.HexBinary)
				{
					return XmlConvert.ToBinHexString((byte[])value);
				}
				if (typeCode == XmlTypeCode.Base64Binary)
				{
					return XmlBaseConverter.Base64BinaryToString((byte[])value);
				}
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
			{
				return XmlBaseConverter.AnyUriToString((Uri)value);
			}
			if (type == XmlBaseConverter.TimeSpanType)
			{
				XmlTypeCode typeCode2 = base.TypeCode;
				if (typeCode2 == XmlTypeCode.Duration)
				{
					return XmlBaseConverter.DurationToString((TimeSpan)value);
				}
				if (typeCode2 == XmlTypeCode.YearMonthDuration)
				{
					return XmlBaseConverter.YearMonthDurationToString((TimeSpan)value);
				}
				if (typeCode2 == XmlTypeCode.DayTimeDuration)
				{
					return XmlBaseConverter.DayTimeDurationToString((TimeSpan)value);
				}
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
			{
				XmlTypeCode typeCode3 = base.TypeCode;
				if (typeCode3 == XmlTypeCode.QName)
				{
					return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
				}
				if (typeCode3 == XmlTypeCode.Notation)
				{
					return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
				}
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000D9AC4 File Offset: 0x000D7CC4
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
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.HexBinary)
				{
					return XmlBaseConverter.StringToHexBinary(value);
				}
				if (typeCode == XmlTypeCode.Base64Binary)
				{
					return XmlBaseConverter.StringToBase64Binary(value);
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				XmlTypeCode typeCode2 = base.TypeCode;
				if (typeCode2 == XmlTypeCode.QName)
				{
					return XmlBaseConverter.StringToQName(value, nsResolver);
				}
				if (typeCode2 == XmlTypeCode.Notation)
				{
					return XmlBaseConverter.StringToQName(value, nsResolver);
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				XmlTypeCode typeCode3 = base.TypeCode;
				if (typeCode3 == XmlTypeCode.Duration)
				{
					return XmlBaseConverter.StringToDuration(value);
				}
				if (typeCode3 == XmlTypeCode.YearMonthDuration)
				{
					return XmlBaseConverter.StringToYearMonthDuration(value);
				}
				if (typeCode3 == XmlTypeCode.DayTimeDuration)
				{
					return XmlBaseConverter.StringToDayTimeDuration(value);
				}
			}
			if (destinationType == XmlBaseConverter.UriType && base.TypeCode == XmlTypeCode.AnyUri)
			{
				return XmlConvert.ToUri(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value, nsResolver);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000D9BFC File Offset: 0x000D7DFC
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
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				if (type == XmlBaseConverter.ByteArrayType)
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.HexBinary)
					{
						return (byte[])value;
					}
					if (typeCode == XmlTypeCode.Base64Binary)
					{
						return (byte[])value;
					}
				}
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode typeCode2 = base.TypeCode;
					if (typeCode2 == XmlTypeCode.HexBinary)
					{
						return XmlBaseConverter.StringToHexBinary((string)value);
					}
					if (typeCode2 == XmlTypeCode.Base64Binary)
					{
						return XmlBaseConverter.StringToBase64Binary((string)value);
					}
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode typeCode3 = base.TypeCode;
					if (typeCode3 == XmlTypeCode.QName)
					{
						return XmlBaseConverter.StringToQName((string)value, nsResolver);
					}
					if (typeCode3 == XmlTypeCode.Notation)
					{
						return XmlBaseConverter.StringToQName((string)value, nsResolver);
					}
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					XmlTypeCode typeCode4 = base.TypeCode;
					if (typeCode4 == XmlTypeCode.QName)
					{
						return (XmlQualifiedName)value;
					}
					if (typeCode4 == XmlTypeCode.Notation)
					{
						return (XmlQualifiedName)value;
					}
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode typeCode5 = base.TypeCode;
					if (typeCode5 == XmlTypeCode.Duration)
					{
						return XmlBaseConverter.StringToDuration((string)value);
					}
					if (typeCode5 == XmlTypeCode.YearMonthDuration)
					{
						return XmlBaseConverter.StringToYearMonthDuration((string)value);
					}
					if (typeCode5 == XmlTypeCode.DayTimeDuration)
					{
						return XmlBaseConverter.StringToDayTimeDuration((string)value);
					}
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					XmlTypeCode typeCode6 = base.TypeCode;
					if (typeCode6 == XmlTypeCode.Duration)
					{
						return (TimeSpan)value;
					}
					if (typeCode6 == XmlTypeCode.YearMonthDuration)
					{
						return (TimeSpan)value;
					}
					if (typeCode6 == XmlTypeCode.DayTimeDuration)
					{
						return (TimeSpan)value;
					}
				}
			}
			if (destinationType == XmlBaseConverter.UriType)
			{
				if (type == XmlBaseConverter.StringType && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return XmlConvert.ToUri((string)value);
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return (Uri)value;
				}
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.ByteArrayType)
				{
					XmlTypeCode typeCode7 = base.TypeCode;
					if (typeCode7 == XmlTypeCode.HexBinary)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (typeCode7 == XmlTypeCode.Base64Binary)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value, nsResolver);
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					XmlTypeCode typeCode8 = base.TypeCode;
					if (typeCode8 == XmlTypeCode.Duration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (typeCode8 == XmlTypeCode.YearMonthDuration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (typeCode8 == XmlTypeCode.DayTimeDuration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return new XmlAtomicValue(base.SchemaType, value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					XmlTypeCode typeCode9 = base.TypeCode;
					if (typeCode9 == XmlTypeCode.QName)
					{
						return new XmlAtomicValue(base.SchemaType, value, nsResolver);
					}
					if (typeCode9 == XmlTypeCode.Notation)
					{
						return new XmlAtomicValue(base.SchemaType, value, nsResolver);
					}
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (XmlAtomicValue)value;
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000D9FD8 File Offset: 0x000D81D8
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000DA010 File Offset: 0x000D8210
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
