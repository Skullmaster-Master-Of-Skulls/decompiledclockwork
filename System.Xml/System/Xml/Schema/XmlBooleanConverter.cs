using System;

namespace System.Xml.Schema
{
	// Token: 0x02000290 RID: 656
	internal class XmlBooleanConverter : XmlBaseConverter
	{
		// Token: 0x06001F41 RID: 8001 RVA: 0x0008CF7C File Offset: 0x0008BF7C
		protected XmlBooleanConverter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0008CF85 File Offset: 0x0008BF85
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlBooleanConverter(schemaType);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0008CF8D File Offset: 0x0008BF8D
		public override bool ToBoolean(bool value)
		{
			return value;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0008CF90 File Offset: 0x0008BF90
		public override bool ToBoolean(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0008CFA8 File Offset: 0x0008BFA8
		public override bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.BooleanType)
			{
				return (bool)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToBoolean((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsBoolean;
			}
			return (bool)this.ChangeListType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0008D013 File Offset: 0x0008C013
		public override string ToString(bool value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0008D01B File Offset: 0x0008C01B
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0008D02C File Offset: 0x0008C02C
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

		// Token: 0x06001F49 RID: 8009 RVA: 0x0008D098 File Offset: 0x0008C098
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
			if (destinationType == XmlBaseConverter.BooleanType)
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
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0008D11C File Offset: 0x0008C11C
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

		// Token: 0x06001F4B RID: 8011 RVA: 0x0008D1A8 File Offset: 0x0008C1A8
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
			if (destinationType == XmlBaseConverter.BooleanType)
			{
				return this.ToBoolean(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.BooleanType)
				{
					return new XmlAtomicValue(base.SchemaType, (bool)value);
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
				if (type == XmlBaseConverter.BooleanType)
				{
					return new XmlAtomicValue(base.SchemaType, (bool)value);
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
