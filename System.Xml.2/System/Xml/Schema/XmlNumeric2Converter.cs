using System;

namespace System.Xml.Schema
{
	// Token: 0x020002C3 RID: 707
	internal class XmlNumeric2Converter : XmlBaseConverter
	{
		// Token: 0x060029DA RID: 10714 RVA: 0x000D8790 File Offset: 0x000D6990
		protected XmlNumeric2Converter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000D8799 File Offset: 0x000D6999
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlNumeric2Converter(schemaType);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000D87A1 File Offset: 0x000D69A1
		public override double ToDouble(double value)
		{
			return value;
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000D87A5 File Offset: 0x000D69A5
		public override double ToDouble(float value)
		{
			return (double)value;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000D87AA File Offset: 0x000D69AA
		public override double ToDouble(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return (double)XmlConvert.ToSingle(value);
			}
			return XmlConvert.ToDouble(value);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000D87D4 File Offset: 0x000D69D4
		public override double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return (double)value;
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return (double)((float)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDouble((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDouble;
			}
			return (double)this.ChangeListType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000D8864 File Offset: 0x000D6A64
		public override float ToSingle(double value)
		{
			return (float)value;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000D8869 File Offset: 0x000D6A69
		public override float ToSingle(float value)
		{
			return value;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000D886D File Offset: 0x000D6A6D
		public override float ToSingle(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToSingle(value);
			}
			return (float)XmlConvert.ToDouble(value);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000D8898 File Offset: 0x000D6A98
		public override float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return (float)((double)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return (float)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToSingle((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (float)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.SingleType);
			}
			return (float)this.ChangeListType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000D8932 File Offset: 0x000D6B32
		public override string ToString(double value)
		{
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToString(this.ToSingle(value));
			}
			return XmlConvert.ToString(value);
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000D8953 File Offset: 0x000D6B53
		public override string ToString(float value)
		{
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToString(value);
			}
			return XmlConvert.ToString((double)value);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000D896F File Offset: 0x000D6B6F
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000D8980 File Offset: 0x000D6B80
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return this.ToString((double)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return this.ToString((float)value);
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

		// Token: 0x060029E8 RID: 10728 RVA: 0x000D8A18 File Offset: 0x000D6C18
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return (float)value;
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

		// Token: 0x060029E9 RID: 10729 RVA: 0x000D8AD4 File Offset: 0x000D6CD4
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return (double)value;
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, (double)value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, (double)value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000D8B90 File Offset: 0x000D6D90
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return this.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return this.ToSingle(value);
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

		// Token: 0x060029EB RID: 10731 RVA: 0x000D8C54 File Offset: 0x000D6E54
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return this.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return this.ToSingle(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.DoubleType)
				{
					return new XmlAtomicValue(base.SchemaType, (double)value);
				}
				if (type == XmlBaseConverter.SingleType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
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
				if (type == XmlBaseConverter.DoubleType)
				{
					return new XmlAtomicValue(base.SchemaType, (double)value);
				}
				if (type == XmlBaseConverter.SingleType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
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
