using System;

namespace System.Xml.Schema
{
	// Token: 0x02000294 RID: 660
	internal class XmlUntypedConverter : XmlListConverter
	{
		// Token: 0x06001F66 RID: 8038 RVA: 0x0008E065 File Offset: 0x0008D065
		protected XmlUntypedConverter() : base(DatatypeImplementation.UntypedAtomicType)
		{
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0008E072 File Offset: 0x0008D072
		protected XmlUntypedConverter(XmlUntypedConverter atomicConverter, bool allowListToList) : base(atomicConverter, allowListToList ? XmlBaseConverter.StringArrayType : XmlBaseConverter.StringType)
		{
			this.allowListToList = allowListToList;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0008E091 File Offset: 0x0008D091
		public override bool ToBoolean(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0008E0A8 File Offset: 0x0008D0A8
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

		// Token: 0x06001F6A RID: 8042 RVA: 0x0008E0F0 File Offset: 0x0008D0F0
		public override DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlBaseConverter.UntypedAtomicToDateTime(value);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0008E108 File Offset: 0x0008D108
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

		// Token: 0x06001F6C RID: 8044 RVA: 0x0008E150 File Offset: 0x0008D150
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlBaseConverter.UntypedAtomicToDateTimeOffset(value);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0008E168 File Offset: 0x0008D168
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

		// Token: 0x06001F6E RID: 8046 RVA: 0x0008E1B0 File Offset: 0x0008D1B0
		public override decimal ToDecimal(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToDecimal(value);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0008E1C8 File Offset: 0x0008D1C8
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

		// Token: 0x06001F70 RID: 8048 RVA: 0x0008E210 File Offset: 0x0008D210
		public override double ToDouble(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToDouble(value);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0008E228 File Offset: 0x0008D228
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

		// Token: 0x06001F72 RID: 8050 RVA: 0x0008E270 File Offset: 0x0008D270
		public override int ToInt32(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToInt32(value);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0008E288 File Offset: 0x0008D288
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

		// Token: 0x06001F74 RID: 8052 RVA: 0x0008E2D0 File Offset: 0x0008D2D0
		public override long ToInt64(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToInt64(value);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0008E2E8 File Offset: 0x0008D2E8
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

		// Token: 0x06001F76 RID: 8054 RVA: 0x0008E330 File Offset: 0x0008D330
		public override float ToSingle(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToSingle(value);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0008E348 File Offset: 0x0008D348
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

		// Token: 0x06001F78 RID: 8056 RVA: 0x0008E390 File Offset: 0x0008D390
		public override string ToString(bool value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0008E398 File Offset: 0x0008D398
		public override string ToString(DateTime value)
		{
			return XmlBaseConverter.DateTimeToString(value);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0008E3A0 File Offset: 0x0008D3A0
		public override string ToString(DateTimeOffset value)
		{
			return XmlBaseConverter.DateTimeOffsetToString(value);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0008E3A8 File Offset: 0x0008D3A8
		public override string ToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0008E3B0 File Offset: 0x0008D3B0
		public override string ToString(double value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0008E3B9 File Offset: 0x0008D3B9
		public override string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0008E3C1 File Offset: 0x0008D3C1
		public override string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0008E3C9 File Offset: 0x0008D3C9
		public override string ToString(float value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0008E3D2 File Offset: 0x0008D3D2
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0008E3E4 File Offset: 0x0008D3E4
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

		// Token: 0x06001F82 RID: 8066 RVA: 0x0008E5B9 File Offset: 0x0008D5B9
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

		// Token: 0x06001F83 RID: 8067 RVA: 0x0008E5F6 File Offset: 0x0008D5F6
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

		// Token: 0x06001F84 RID: 8068 RVA: 0x0008E633 File Offset: 0x0008D633
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

		// Token: 0x06001F85 RID: 8069 RVA: 0x0008E670 File Offset: 0x0008D670
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

		// Token: 0x06001F86 RID: 8070 RVA: 0x0008E6AD File Offset: 0x0008D6AD
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

		// Token: 0x06001F87 RID: 8071 RVA: 0x0008E6EB File Offset: 0x0008D6EB
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

		// Token: 0x06001F88 RID: 8072 RVA: 0x0008E728 File Offset: 0x0008D728
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

		// Token: 0x06001F89 RID: 8073 RVA: 0x0008E765 File Offset: 0x0008D765
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

		// Token: 0x06001F8A RID: 8074 RVA: 0x0008E7A4 File Offset: 0x0008D7A4
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

		// Token: 0x06001F8B RID: 8075 RVA: 0x0008E994 File Offset: 0x0008D994
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

		// Token: 0x06001F8C RID: 8076 RVA: 0x0008ED00 File Offset: 0x0008DD00
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0008ED34 File Offset: 0x0008DD34
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

		// Token: 0x06001F8E RID: 8078 RVA: 0x0008ED84 File Offset: 0x0008DD84
		protected override object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (this.atomicConverter != null && (this.allowListToList || type == XmlBaseConverter.StringType || destinationType == XmlBaseConverter.StringType))
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

		// Token: 0x06001F8F RID: 8079 RVA: 0x0008EE30 File Offset: 0x0008DE30
		private bool SupportsType(Type clrType)
		{
			return clrType == XmlBaseConverter.BooleanType || clrType == XmlBaseConverter.ByteType || clrType == XmlBaseConverter.ByteArrayType || clrType == XmlBaseConverter.DateTimeType || clrType == XmlBaseConverter.DateTimeOffsetType || clrType == XmlBaseConverter.DecimalType || clrType == XmlBaseConverter.DoubleType || clrType == XmlBaseConverter.Int16Type || clrType == XmlBaseConverter.Int32Type || clrType == XmlBaseConverter.Int64Type || clrType == XmlBaseConverter.SByteType || clrType == XmlBaseConverter.SingleType || clrType == XmlBaseConverter.TimeSpanType || clrType == XmlBaseConverter.UInt16Type || clrType == XmlBaseConverter.UInt32Type || clrType == XmlBaseConverter.UInt64Type || clrType == XmlBaseConverter.UriType || clrType == XmlBaseConverter.XmlQualifiedNameType;
		}

		// Token: 0x040012A6 RID: 4774
		private bool allowListToList;

		// Token: 0x040012A7 RID: 4775
		public static readonly XmlValueConverter Untyped = new XmlUntypedConverter(new XmlUntypedConverter(), false);

		// Token: 0x040012A8 RID: 4776
		public static readonly XmlValueConverter UntypedList = new XmlUntypedConverter(new XmlUntypedConverter(), true);
	}
}
