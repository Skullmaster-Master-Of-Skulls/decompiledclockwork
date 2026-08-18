using System;

namespace System.Xml.Schema
{
	// Token: 0x02000298 RID: 664
	internal class XmlUnionConverter : XmlBaseConverter
	{
		// Token: 0x06001FAE RID: 8110 RVA: 0x0008F9F4 File Offset: 0x0008E9F4
		protected XmlUnionConverter(XmlSchemaType schemaType) : base(schemaType)
		{
			while (schemaType.DerivedBy == XmlSchemaDerivationMethod.Restriction)
			{
				schemaType = schemaType.BaseXmlSchemaType;
			}
			XmlSchemaSimpleType[] baseMemberTypes = ((XmlSchemaSimpleTypeUnion)((XmlSchemaSimpleType)schemaType).Content).BaseMemberTypes;
			this.converters = new XmlValueConverter[baseMemberTypes.Length];
			for (int i = 0; i < baseMemberTypes.Length; i++)
			{
				this.converters[i] = baseMemberTypes[i].ValueConverter;
				if (baseMemberTypes[i].Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					this.hasListMember = true;
				}
				else if (baseMemberTypes[i].Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
				{
					this.hasAtomicMember = true;
				}
			}
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0008FA8C File Offset: 0x0008EA8C
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlUnionConverter(schemaType);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0008FA94 File Offset: 0x0008EA94
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
			if (type == XmlBaseConverter.XmlAtomicValueType && this.hasAtomicMember)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			if (type == XmlBaseConverter.XmlAtomicValueArrayType && this.hasListMember)
			{
				return XmlAnyListConverter.ItemList.ChangeType(value, destinationType, nsResolver);
			}
			if (type != XmlBaseConverter.StringType)
			{
				throw base.CreateInvalidClrMappingException(type, destinationType);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			XsdSimpleValue xsdSimpleValue = (XsdSimpleValue)base.SchemaType.Datatype.ParseValue((string)value, new NameTable(), nsResolver, true);
			return xsdSimpleValue.XmlType.ValueConverter.ChangeType((string)value, destinationType, nsResolver);
		}

		// Token: 0x040012AE RID: 4782
		private XmlValueConverter[] converters;

		// Token: 0x040012AF RID: 4783
		private bool hasAtomicMember;

		// Token: 0x040012B0 RID: 4784
		private bool hasListMember;
	}
}
