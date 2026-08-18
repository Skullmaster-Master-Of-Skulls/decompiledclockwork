using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000291 RID: 657
	internal class XmlMiscConverter : XmlBaseConverter
	{
		// Token: 0x06001F4C RID: 8012 RVA: 0x0008D2AD File Offset: 0x0008C2AD
		protected XmlMiscConverter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0008D2B6 File Offset: 0x0008C2B6
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlMiscConverter(schemaType);
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0008D2BE File Offset: 0x0008C2BE
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0008D2D0 File Offset: 0x0008C2D0
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.ByteArrayType)
			{
				switch (base.TypeCode)
				{
				case XmlTypeCode.HexBinary:
					return XmlConvert.ToBinHexString((byte[])value);
				case XmlTypeCode.Base64Binary:
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
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.Duration)
				{
					return XmlBaseConverter.DurationToString((TimeSpan)value);
				}
				switch (typeCode)
				{
				case XmlTypeCode.YearMonthDuration:
					return XmlBaseConverter.YearMonthDurationToString((TimeSpan)value);
				case XmlTypeCode.DayTimeDuration:
					return XmlBaseConverter.DayTimeDurationToString((TimeSpan)value);
				}
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
			{
				switch (base.TypeCode)
				{
				case XmlTypeCode.QName:
					return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
				case XmlTypeCode.Notation:
					return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
				}
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0008D3FC File Offset: 0x0008C3FC
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
				switch (base.TypeCode)
				{
				case XmlTypeCode.HexBinary:
					return XmlBaseConverter.StringToHexBinary(value);
				case XmlTypeCode.Base64Binary:
					return XmlBaseConverter.StringToBase64Binary(value);
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				switch (base.TypeCode)
				{
				case XmlTypeCode.QName:
					return XmlBaseConverter.StringToQName(value, nsResolver);
				case XmlTypeCode.Notation:
					return XmlBaseConverter.StringToQName(value, nsResolver);
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.Duration)
				{
					return XmlBaseConverter.StringToDuration(value);
				}
				switch (typeCode)
				{
				case XmlTypeCode.YearMonthDuration:
					return XmlBaseConverter.StringToYearMonthDuration(value);
				case XmlTypeCode.DayTimeDuration:
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

		// Token: 0x06001F51 RID: 8017 RVA: 0x0008D524 File Offset: 0x0008C524
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
					switch (base.TypeCode)
					{
					case XmlTypeCode.HexBinary:
						return (byte[])value;
					case XmlTypeCode.Base64Binary:
						return (byte[])value;
					}
				}
				if (type == XmlBaseConverter.StringType)
				{
					switch (base.TypeCode)
					{
					case XmlTypeCode.HexBinary:
						return XmlBaseConverter.StringToHexBinary((string)value);
					case XmlTypeCode.Base64Binary:
						return XmlBaseConverter.StringToBase64Binary((string)value);
					}
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					switch (base.TypeCode)
					{
					case XmlTypeCode.QName:
						return XmlBaseConverter.StringToQName((string)value, nsResolver);
					case XmlTypeCode.Notation:
						return XmlBaseConverter.StringToQName((string)value, nsResolver);
					}
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					switch (base.TypeCode)
					{
					case XmlTypeCode.QName:
						return (XmlQualifiedName)value;
					case XmlTypeCode.Notation:
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
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.Duration)
					{
						return XmlBaseConverter.StringToDuration((string)value);
					}
					switch (typeCode)
					{
					case XmlTypeCode.YearMonthDuration:
						return XmlBaseConverter.StringToYearMonthDuration((string)value);
					case XmlTypeCode.DayTimeDuration:
						return XmlBaseConverter.StringToDayTimeDuration((string)value);
					}
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					XmlTypeCode typeCode2 = base.TypeCode;
					if (typeCode2 == XmlTypeCode.Duration)
					{
						return (TimeSpan)value;
					}
					switch (typeCode2)
					{
					case XmlTypeCode.YearMonthDuration:
						return (TimeSpan)value;
					case XmlTypeCode.DayTimeDuration:
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
					switch (base.TypeCode)
					{
					case XmlTypeCode.HexBinary:
						return new XmlAtomicValue(base.SchemaType, value);
					case XmlTypeCode.Base64Binary:
						return new XmlAtomicValue(base.SchemaType, value);
					}
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value, nsResolver);
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					XmlTypeCode typeCode3 = base.TypeCode;
					if (typeCode3 == XmlTypeCode.Duration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					switch (typeCode3)
					{
					case XmlTypeCode.YearMonthDuration:
						return new XmlAtomicValue(base.SchemaType, value);
					case XmlTypeCode.DayTimeDuration:
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
					switch (base.TypeCode)
					{
					case XmlTypeCode.QName:
						return new XmlAtomicValue(base.SchemaType, value, nsResolver);
					case XmlTypeCode.Notation:
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

		// Token: 0x06001F52 RID: 8018 RVA: 0x0008D8DC File Offset: 0x0008C8DC
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0008D90F File Offset: 0x0008C90F
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
