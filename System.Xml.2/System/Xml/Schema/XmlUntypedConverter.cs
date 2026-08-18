using System;

namespace System.Xml.Schema
{
	// Token: 0x020002C8 RID: 712
	internal class XmlUntypedConverter : XmlListConverter
	{
		// Token: 0x06002A17 RID: 10775 RVA: 0x000DA23E File Offset: 0x000D843E
		protected XmlUntypedConverter() : base(DatatypeImplementation.UntypedAtomicType)
		{
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000DA24B File Offset: 0x000D844B
		protected XmlUntypedConverter(XmlUntypedConverter atomicConverter, bool allowListToList) : base(atomicConverter, allowListToList ? XmlBaseConverter.StringArrayType : XmlBaseConverter.StringType)
		{
			this.allowListToList = allowListToList;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000DA26A File Offset: 0x000D846A
		public override bool ToBoolean(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000DA280 File Offset: 0x000D8480
		public override bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToBoolean((string)value);
			}
			return (bool)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000DA2CD File Offset: 0x000D84CD
		public override DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlBaseConverter.UntypedAtomicToDateTime(value);
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000DA2E4 File Offset: 0x000D84E4
		public override DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime((string)value);
			}
			return (DateTime)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000DA331 File Offset: 0x000D8531
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlBaseConverter.UntypedAtomicToDateTimeOffset(value);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000DA348 File Offset: 0x000D8548
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset((string)value);
			}
			return (DateTimeOffset)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000DA395 File Offset: 0x000D8595
		public override decimal ToDecimal(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToDecimal(value);
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000DA3AC File Offset: 0x000D85AC
		public override decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDecimal((string)value);
			}
			return (decimal)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000DA3F9 File Offset: 0x000D85F9
		public override double ToDouble(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToDouble(value);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000DA410 File Offset: 0x000D8610
		public override double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDouble((string)value);
			}
			return (double)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000DA45D File Offset: 0x000D865D
		public override int ToInt32(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToInt32(value);
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000DA474 File Offset: 0x000D8674
		public override int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt32((string)value);
			}
			return (int)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000DA4C1 File Offset: 0x000D86C1
		public override long ToInt64(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToInt64(value);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000DA4D8 File Offset: 0x000D86D8
		public override long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt64((string)value);
			}
			return (long)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000DA525 File Offset: 0x000D8725
		public override float ToSingle(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToSingle(value);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000DA53C File Offset: 0x000D873C
		public override float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToSingle((string)value);
			}
			return (float)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x000DA589 File Offset: 0x000D8789
		public override string ToString(bool value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000DA591 File Offset: 0x000D8791
		public override string ToString(DateTime value)
		{
			return XmlBaseConverter.DateTimeToString(value);
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000DA599 File Offset: 0x000D8799
		public override string ToString(DateTimeOffset value)
		{
			return XmlBaseConverter.DateTimeOffsetToString(value);
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x000DA5A1 File Offset: 0x000D87A1
		public override string ToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000DA5A9 File Offset: 0x000D87A9
		public override string ToString(double value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000DA5B2 File Offset: 0x000D87B2
		public override string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000DA5BA File Offset: 0x000D87BA
		public override string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000DA5C2 File Offset: 0x000D87C2
		public override string ToString(float value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000DA5CB File Offset: 0x000D87CB
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000DA5DC File Offset: 0x000D87DC
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.BooleanType)
			{
				return XmlConvert.ToString((bool)value);
			}
			if (type == XmlBaseConverter.ByteType)
			{
				return XmlConvert.ToString((byte)value);
			}
			if (type == XmlBaseConverter.ByteArrayType)
			{
				return XmlBaseConverter.Base64BinaryToString((byte[])value);
			}
			if (type == XmlBaseConverter.DateTimeType)
			{
				return XmlBaseConverter.DateTimeToString((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return XmlBaseConverter.DateTimeOffsetToString((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.DecimalType)
			{
				return XmlConvert.ToString((decimal)value);
			}
			if (type == XmlBaseConverter.DoubleType)
			{
				return XmlConvert.ToString((double)value);
			}
			if (type == XmlBaseConverter.Int16Type)
			{
				return XmlConvert.ToString((short)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return XmlConvert.ToString((int)value);
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return XmlConvert.ToString((long)value);
			}
			if (type == XmlBaseConverter.SByteType)
			{
				return XmlConvert.ToString((sbyte)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return XmlConvert.ToString((float)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.TimeSpanType)
			{
				return XmlBaseConverter.DurationToString((TimeSpan)value);
			}
			if (type == XmlBaseConverter.UInt16Type)
			{
				return XmlConvert.ToString((ushort)value);
			}
			if (type == XmlBaseConverter.UInt32Type)
			{
				return XmlConvert.ToString((uint)value);
			}
			if (type == XmlBaseConverter.UInt64Type)
			{
				return XmlConvert.ToString((ulong)value);
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType))
			{
				return XmlBaseConverter.AnyUriToString((Uri)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (string)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.StringType, nsResolver);
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
			{
				return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000DA80C File Offset: 0x000D8A0C
		public override object ChangeType(bool value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000DA864 File Offset: 0x000D8A64
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
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DateTimeToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000DA8BC File Offset: 0x000D8ABC
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
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DateTimeOffsetToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000DA914 File Offset: 0x000D8B14
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
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000DA96C File Offset: 0x000D8B6C
		public override object ChangeType(double value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000DA9C8 File Offset: 0x000D8BC8
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
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000DAA20 File Offset: 0x000D8C20
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
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000DAA78 File Offset: 0x000D8C78
		public override object ChangeType(float value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000DAAD4 File Offset: 0x000D8CD4
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
			if (destinationType == XmlBaseConverter.BooleanType)
			{
				return XmlConvert.ToBoolean(value);
			}
			if (destinationType == XmlBaseConverter.ByteType)
			{
				return XmlBaseConverter.Int32ToByte(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				return XmlBaseConverter.StringToBase64Binary(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return XmlConvert.ToDecimal(value);
			}
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return XmlConvert.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.Int16Type)
			{
				return XmlBaseConverter.Int32ToInt16(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return XmlConvert.ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return XmlConvert.ToInt64(value);
			}
			if (destinationType == XmlBaseConverter.SByteType)
			{
				return XmlBaseConverter.Int32ToSByte(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return XmlConvert.ToSingle(value);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				return XmlBaseConverter.StringToDuration(value);
			}
			if (destinationType == XmlBaseConverter.UInt16Type)
			{
				return XmlBaseConverter.Int32ToUInt16(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type)
			{
				return XmlBaseConverter.Int64ToUInt32(XmlConvert.ToInt64(value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type)
			{
				return XmlBaseConverter.DecimalToUInt64(XmlConvert.ToDecimal(value));
			}
			if (destinationType == XmlBaseConverter.UriType)
			{
				return XmlConvert.ToUri(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				return XmlBaseConverter.StringToQName(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000DAD38 File Offset: 0x000D8F38
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
			if (destinationType == XmlBaseConverter.BooleanType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToBoolean((string)value);
			}
			if (destinationType == XmlBaseConverter.ByteType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToByte(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.ByteArrayType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToBase64Binary((string)value);
			}
			if (destinationType == XmlBaseConverter.DateTimeType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime((string)value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset((string)value);
			}
			if (destinationType == XmlBaseConverter.DecimalType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDecimal((string)value);
			}
			if (destinationType == XmlBaseConverter.DoubleType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDouble((string)value);
			}
			if (destinationType == XmlBaseConverter.Int16Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToInt16(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.Int32Type && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt32((string)value);
			}
			if (destinationType == XmlBaseConverter.Int64Type && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt64((string)value);
			}
			if (destinationType == XmlBaseConverter.SByteType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToSByte(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.SingleType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToSingle((string)value);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToDuration((string)value);
			}
			if (destinationType == XmlBaseConverter.UInt16Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToUInt16(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int64ToUInt32(XmlConvert.ToInt64((string)value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DecimalToUInt64(XmlConvert.ToDecimal((string)value));
			}
			if (destinationType == XmlBaseConverter.UriType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToUri((string)value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToQName((string)value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000DB198 File Offset: 0x000D9398
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000DB1D0 File Offset: 0x000D93D0
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000DB228 File Offset: 0x000D9428
		protected override object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (this.atomicConverter != null && (this.allowListToList || !(type != XmlBaseConverter.StringType) || !(destinationType != XmlBaseConverter.StringType)))
			{
				return base.ChangeListType(value, destinationType, nsResolver);
			}
			if (this.SupportsType(type))
			{
				throw new InvalidCastException(Res.GetString("XmlConvert_TypeToString", new object[]
				{
					base.XmlTypeName,
					type.Name
				}));
			}
			if (this.SupportsType(destinationType))
			{
				throw new InvalidCastException(Res.GetString("XmlConvert_TypeFromString", new object[]
				{
					base.XmlTypeName,
					destinationType.Name
				}));
			}
			throw base.CreateInvalidClrMappingException(type, destinationType);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000DB2E0 File Offset: 0x000D94E0
		private bool SupportsType(Type clrType)
		{
			return clrType == XmlBaseConverter.BooleanType || clrType == XmlBaseConverter.ByteType || clrType == XmlBaseConverter.ByteArrayType || clrType == XmlBaseConverter.DateTimeType || clrType == XmlBaseConverter.DateTimeOffsetType || clrType == XmlBaseConverter.DecimalType || clrType == XmlBaseConverter.DoubleType || clrType == XmlBaseConverter.Int16Type || clrType == XmlBaseConverter.Int32Type || clrType == XmlBaseConverter.Int64Type || clrType == XmlBaseConverter.SByteType || clrType == XmlBaseConverter.SingleType || clrType == XmlBaseConverter.TimeSpanType || clrType == XmlBaseConverter.UInt16Type || clrType == XmlBaseConverter.UInt32Type || clrType == XmlBaseConverter.UInt64Type || clrType == XmlBaseConverter.UriType || clrType == XmlBaseConverter.XmlQualifiedNameType;
		}

		// Token: 0x04001217 RID: 4631
		private bool allowListToList;

		// Token: 0x04001218 RID: 4632
		public static readonly XmlValueConverter Untyped = new XmlUntypedConverter(new XmlUntypedConverter(), false);

		// Token: 0x04001219 RID: 4633
		public static readonly XmlValueConverter UntypedList = new XmlUntypedConverter(new XmlUntypedConverter(), true);
	}
}
