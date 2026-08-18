using System;

namespace System.Xml.Schema
{
	// Token: 0x0200028D RID: 653
	internal class XmlNumeric10Converter : XmlBaseConverter
	{
		// Token: 0x06001F00 RID: 7936 RVA: 0x0008B910 File Offset: 0x0008A910
		protected XmlNumeric10Converter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0008B919 File Offset: 0x0008A919
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlNumeric10Converter(schemaType);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0008B921 File Offset: 0x0008A921
		public override decimal ToDecimal(decimal value)
		{
			return value;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0008B924 File Offset: 0x0008A924
		public override decimal ToDecimal(int value)
		{
			return value;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0008B92C File Offset: 0x0008A92C
		public override decimal ToDecimal(long value)
		{
			return value;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0008B934 File Offset: 0x0008A934
		public override decimal ToDecimal(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlConvert.ToDecimal(value);
			}
			return XmlConvert.ToInteger(value);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0008B95C File Offset: 0x0008A95C
		public override decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DecimalType)
			{
				return (decimal)value;
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return (int)value;
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return (long)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDecimal((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (decimal)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DecimalType);
			}
			return (decimal)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0008B9FA File Offset: 0x0008A9FA
		public override int ToInt32(decimal value)
		{
			return XmlBaseConverter.DecimalToInt32(value);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0008BA02 File Offset: 0x0008AA02
		public override int ToInt32(int value)
		{
			return value;
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0008BA05 File Offset: 0x0008AA05
		public override int ToInt32(long value)
		{
			return XmlBaseConverter.Int64ToInt32(value);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0008BA0D File Offset: 0x0008AA0D
		public override int ToInt32(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlBaseConverter.DecimalToInt32(XmlConvert.ToDecimal(value));
			}
			return XmlConvert.ToInt32(value);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0008BA3C File Offset: 0x0008AA3C
		public override int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DecimalType)
			{
				return XmlBaseConverter.DecimalToInt32((decimal)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return (int)value;
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return XmlBaseConverter.Int64ToInt32((long)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToInt32((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsInt;
			}
			return (int)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0008BAD0 File Offset: 0x0008AAD0
		public override long ToInt64(decimal value)
		{
			return XmlBaseConverter.DecimalToInt64(value);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0008BAD8 File Offset: 0x0008AAD8
		public override long ToInt64(int value)
		{
			return (long)value;
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0008BADC File Offset: 0x0008AADC
		public override long ToInt64(long value)
		{
			return value;
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0008BADF File Offset: 0x0008AADF
		public override long ToInt64(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlBaseConverter.DecimalToInt64(XmlConvert.ToDecimal(value));
			}
			return XmlConvert.ToInt64(value);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0008BB0C File Offset: 0x0008AB0C
		public override long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DecimalType)
			{
				return XmlBaseConverter.DecimalToInt64((decimal)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return (long)((int)value);
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return (long)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToInt64((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsLong;
			}
			return (long)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0008BB9C File Offset: 0x0008AB9C
		public override string ToString(decimal value)
		{
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlConvert.ToString(value);
			}
			return XmlConvert.ToString(decimal.Truncate(value));
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0008BBBA File Offset: 0x0008ABBA
		public override string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0008BBC2 File Offset: 0x0008ABC2
		public override string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0008BBCA File Offset: 0x0008ABCA
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0008BBDC File Offset: 0x0008ABDC
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DecimalType)
			{
				return this.ToString((decimal)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return XmlConvert.ToString((int)value);
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return XmlConvert.ToString((long)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).Value;
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0008BC70 File Offset: 0x0008AC70
		public override object ChangeType(decimal value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return XmlBaseConverter.DecimalToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return XmlBaseConverter.DecimalToInt64(value);
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
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0008BD24 File Offset: 0x0008AD24
		public override object ChangeType(int value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return (long)value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0008BDCC File Offset: 0x0008ADCC
		public override object ChangeType(long value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return XmlBaseConverter.Int64ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0008BE78 File Offset: 0x0008AE78
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
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return this.ToDecimal(value);
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return this.ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return this.ToInt64(value);
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
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0008BF30 File Offset: 0x0008AF30
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
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return this.ToDecimal(value);
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return this.ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return this.ToInt64(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.DecimalType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
				}
				if (type == XmlBaseConverter.Int32Type)
				{
					return new XmlAtomicValue(base.SchemaType, (int)value);
				}
				if (type == XmlBaseConverter.Int64Type)
				{
					return new XmlAtomicValue(base.SchemaType, (long)value);
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
				if (type == XmlBaseConverter.DecimalType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
				}
				if (type == XmlBaseConverter.Int32Type)
				{
					return new XmlAtomicValue(base.SchemaType, (int)value);
				}
				if (type == XmlBaseConverter.Int64Type)
				{
					return new XmlAtomicValue(base.SchemaType, (long)value);
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
			if (destinationType == XmlBaseConverter.ByteType)
			{
				return XmlBaseConverter.Int32ToByte(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.Int16Type)
			{
				return XmlBaseConverter.Int32ToInt16(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.SByteType)
			{
				return XmlBaseConverter.Int32ToSByte(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt16Type)
			{
				return XmlBaseConverter.Int32ToUInt16(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type)
			{
				return XmlBaseConverter.Int64ToUInt32(this.ToInt64(value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type)
			{
				return XmlBaseConverter.DecimalToUInt64(this.ToDecimal(value));
			}
			if (type == XmlBaseConverter.ByteType)
			{
				return this.ChangeType((int)((byte)value), destinationType);
			}
			if (type == XmlBaseConverter.Int16Type)
			{
				return this.ChangeType((int)((short)value), destinationType);
			}
			if (type == XmlBaseConverter.SByteType)
			{
				return this.ChangeType((int)((sbyte)value), destinationType);
			}
			if (type == XmlBaseConverter.UInt16Type)
			{
				return this.ChangeType((int)((ushort)value), destinationType);
			}
			if (type == XmlBaseConverter.UInt32Type)
			{
				return this.ChangeType((long)((ulong)((uint)value)), destinationType);
			}
			if (type == XmlBaseConverter.UInt64Type)
			{
				return this.ChangeType((ulong)value, destinationType);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0008C1E4 File Offset: 0x0008B1E4
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (type == XmlBaseConverter.ByteType)
			{
				return this.ChangeType((int)((byte)value), destinationType);
			}
			if (type == XmlBaseConverter.Int16Type)
			{
				return this.ChangeType((int)((short)value), destinationType);
			}
			if (type == XmlBaseConverter.SByteType)
			{
				return this.ChangeType((int)((sbyte)value), destinationType);
			}
			if (type == XmlBaseConverter.UInt16Type)
			{
				return this.ChangeType((int)((ushort)value), destinationType);
			}
			if (type == XmlBaseConverter.UInt32Type)
			{
				return this.ChangeType((long)((ulong)((uint)value)), destinationType);
			}
			if (type == XmlBaseConverter.UInt64Type)
			{
				return this.ChangeType((ulong)value, destinationType);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0008C28C File Offset: 0x0008B28C
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.ByteType)
			{
				return XmlBaseConverter.Int32ToByte(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.Int16Type)
			{
				return XmlBaseConverter.Int32ToInt16(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.SByteType)
			{
				return XmlBaseConverter.Int32ToSByte(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt16Type)
			{
				return XmlBaseConverter.Int32ToUInt16(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type)
			{
				return XmlBaseConverter.Int64ToUInt32(this.ToInt64(value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type)
			{
				return XmlBaseConverter.DecimalToUInt64(this.ToDecimal(value));
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
