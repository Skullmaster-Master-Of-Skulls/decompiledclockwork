using System;

namespace System.Xml.Schema
{
	// Token: 0x020002CD RID: 717
	internal class XmlUnionConverter : XmlBaseConverter
	{
		// Token: 0x06002A6B RID: 10859 RVA: 0x000DC6FC File Offset: 0x000DA8FC
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

		// Token: 0x06002A6C RID: 10860 RVA: 0x000DC794 File Offset: 0x000DA994
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlUnionConverter(schemaType);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000DC79C File Offset: 0x000DA99C
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
			if (!(type == XmlBaseConverter.StringType))
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

		// Token: 0x04001220 RID: 4640
		private XmlValueConverter[] converters;

		// Token: 0x04001221 RID: 4641
		private bool hasAtomicMember;

		// Token: 0x04001222 RID: 4642
		private bool hasListMember;
	}
}
