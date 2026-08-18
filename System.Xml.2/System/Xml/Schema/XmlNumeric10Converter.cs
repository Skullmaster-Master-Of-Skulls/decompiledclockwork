using System;

namespace System.Xml.Schema
{
	// Token: 0x020002C2 RID: 706
	internal class XmlNumeric10Converter : XmlBaseConverter
	{
		// Token: 0x060029BD RID: 10685 RVA: 0x000D7B80 File Offset: 0x000D5D80
		protected XmlNumeric10Converter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000D7B89 File Offset: 0x000D5D89
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlNumeric10Converter(schemaType);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000D7B91 File Offset: 0x000D5D91
		public override decimal ToDecimal(decimal value)
		{
			return value;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000D7B94 File Offset: 0x000D5D94
		public override decimal ToDecimal(int value)
		{
			return value;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000D7B9C File Offset: 0x000D5D9C
		public override decimal ToDecimal(long value)
		{
			return value;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000D7BA4 File Offset: 0x000D5DA4
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

		// Token: 0x060029C3 RID: 10691 RVA: 0x000D7BCC File Offset: 0x000D5DCC
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

		// Token: 0x060029C4 RID: 10692 RVA: 0x000D7C83 File Offset: 0x000D5E83
		public override int ToInt32(decimal value)
		{
			return XmlBaseConverter.DecimalToInt32(value);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000D7C8B File Offset: 0x000D5E8B
		public override int ToInt32(int value)
		{
			return value;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000D7C8E File Offset: 0x000D5E8E
		public override int ToInt32(long value)
		{
			return XmlBaseConverter.Int64ToInt32(value);
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000D7C96 File Offset: 0x000D5E96
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

		// Token: 0x060029C8 RID: 10696 RVA: 0x000D7CC4 File Offset: 0x000D5EC4
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

		// Token: 0x060029C9 RID: 10697 RVA: 0x000D7D71 File Offset: 0x000D5F71
		public override long ToInt64(decimal value)
		{
			return XmlBaseConverter.DecimalToInt64(value);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000D7D79 File Offset: 0x000D5F79
		public override long ToInt64(int value)
		{
			return (long)value;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000D7D7D File Offset: 0x000D5F7D
		public override long ToInt64(long value)
		{
			return value;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000D7D80 File Offset: 0x000D5F80
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

		// Token: 0x060029CD RID: 10701 RVA: 0x000D7DAC File Offset: 0x000D5FAC
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

		// Token: 0x060029CE RID: 10702 RVA: 0x000D7E55 File Offset: 0x000D6055
		public override string ToString(decimal value)
		{
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlConvert.ToString(value);
			}
			return XmlConvert.ToString(decimal.Truncate(value));
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000D7E73 File Offset: 0x000D6073
		public override string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000D7E7B File Offset: 0x000D607B
		public override string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000D7E83 File Offset: 0x000D6083
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000D7E94 File Offset: 0x000D6094
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

		// Token: 0x060029D3 RID: 10707 RVA: 0x000D7F44 File Offset: 0x000D6144
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

		// Token: 0x060029D4 RID: 10708 RVA: 0x000D8024 File Offset: 0x000D6224
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

		// Token: 0x060029D5 RID: 10709 RVA: 0x000D80F4 File Offset: 0x000D62F4
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

		// Token: 0x060029D6 RID: 10710 RVA: 0x000D81C8 File Offset: 0x000D63C8
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

		// Token: 0x060029D7 RID: 10711 RVA: 0x000D82A8 File Offset: 0x000D64A8
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

		// Token: 0x060029D8 RID: 10712 RVA: 0x000D85F8 File Offset: 0x000D67F8
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

		// Token: 0x060029D9 RID: 10713 RVA: 0x000D86C0 File Offset: 0x000D68C0
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
