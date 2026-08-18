using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020002CA RID: 714
	internal class XmlAnyConverter : XmlBaseConverter
	{
		// Token: 0x06002A45 RID: 10821 RVA: 0x000DB4CA File Offset: 0x000D96CA
		protected XmlAnyConverter(XmlTypeCode typeCode) : base(typeCode)
		{
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000DB4D4 File Offset: 0x000D96D4
		public override bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsBoolean;
			}
			return (bool)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000DB524 File Offset: 0x000D9724
		public override DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			return (DateTime)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000DB574 File Offset: 0x000D9774
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (DateTimeOffset)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DateTimeOffsetType);
			}
			return (DateTimeOffset)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000DB5CC File Offset: 0x000D97CC
		public override decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (decimal)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DecimalType);
			}
			return (decimal)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000DB624 File Offset: 0x000D9824
		public override double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDouble;
			}
			return (double)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000DB674 File Offset: 0x000D9874
		public override int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsInt;
			}
			return (int)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000DB6C4 File Offset: 0x000D98C4
		public override long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsLong;
			}
			return (long)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000DB714 File Offset: 0x000D9914
		public override float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (float)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.SingleType);
			}
			return (float)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000DB76C File Offset: 0x000D996C
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000DB7CC File Offset: 0x000D99CC
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.DateTime), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000DB82C File Offset: 0x000D9A2C
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.DateTime), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000DB890 File Offset: 0x000D9A90
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Decimal), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000DB8F4 File Offset: 0x000D9AF4
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Double), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000DB954 File Offset: 0x000D9B54
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Int), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000DB9B4 File Offset: 0x000D9BB4
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Long), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000DBA14 File Offset: 0x000D9C14
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Float), (double)value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000DBA74 File Offset: 0x000D9C74
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
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000DBADC File Offset: 0x000D9CDC
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
			if (destinationType == XmlBaseConverter.BooleanType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsBoolean;
			}
			if (destinationType == XmlBaseConverter.DateTimeType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DateTimeOffsetType);
			}
			if (destinationType == XmlBaseConverter.DecimalType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (decimal)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DecimalType);
			}
			if (destinationType == XmlBaseConverter.DoubleType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDouble;
			}
			if (destinationType == XmlBaseConverter.Int32Type && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsInt;
			}
			if (destinationType == XmlBaseConverter.Int64Type && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsLong;
			}
			if (destinationType == XmlBaseConverter.SingleType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (float)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.SingleType);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
				if (type == XmlBaseConverter.BooleanType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean), (bool)value);
				}
				if (type == XmlBaseConverter.ByteType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.UnsignedByte), value);
				}
				if (type == XmlBaseConverter.ByteArrayType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Base64Binary), value);
				}
				if (type == XmlBaseConverter.DateTimeType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.DateTime), (DateTime)value);
				}
				if (type == XmlBaseConverter.DateTimeOffsetType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.DateTime), (DateTimeOffset)value);
				}
				if (type == XmlBaseConverter.DecimalType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Decimal), value);
				}
				if (type == XmlBaseConverter.DoubleType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Double), (double)value);
				}
				if (type == XmlBaseConverter.Int16Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Short), value);
				}
				if (type == XmlBaseConverter.Int32Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Int), (int)value);
				}
				if (type == XmlBaseConverter.Int64Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Long), (long)value);
				}
				if (type == XmlBaseConverter.SByteType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Byte), value);
				}
				if (type == XmlBaseConverter.SingleType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Float), value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), (string)value);
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Duration), value);
				}
				if (type == XmlBaseConverter.UInt16Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.UnsignedShort), value);
				}
				if (type == XmlBaseConverter.UInt32Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.UnsignedInt), value);
				}
				if (type == XmlBaseConverter.UInt64Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.UnsignedLong), value);
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType))
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.AnyUri), value);
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.QName), value, nsResolver);
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
				{
					return (XPathNavigator)value;
				}
			}
			if (destinationType == XmlBaseConverter.XPathNavigatorType && XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
			{
				return this.ToNavigator((XPathNavigator)value);
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

		// Token: 0x06002A58 RID: 10840 RVA: 0x000DBF84 File Offset: 0x000DA184
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000DBFBC File Offset: 0x000DA1BC
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000DBFE7 File Offset: 0x000DA1E7
		private XPathNavigator ToNavigator(XPathNavigator nav)
		{
			if (base.TypeCode != XmlTypeCode.Item)
			{
				throw base.CreateInvalidClrMappingException(XmlBaseConverter.XPathNavigatorType, XmlBaseConverter.XPathNavigatorType);
			}
			return nav;
		}

		// Token: 0x0400121B RID: 4635
		public static readonly XmlValueConverter Item = new XmlAnyConverter(XmlTypeCode.Item);

		// Token: 0x0400121C RID: 4636
		public static readonly XmlValueConverter AnyAtomic = new XmlAnyConverter(XmlTypeCode.AnyAtomicType);
	}
}
