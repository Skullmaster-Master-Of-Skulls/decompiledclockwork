using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000278 RID: 632
	public class XmlSchemaSimpleContentRestriction : XmlSchemaContent
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x00085CF7 File Offset: 0x00084CF7
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x00085CFF File Offset: 0x00084CFF
		[XmlAttribute("base")]
		public XmlQualifiedName BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
			set
			{
				this.baseTypeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00085D18 File Offset: 0x00084D18
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x00085D20 File Offset: 0x00084D20
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType BaseType
		{
			get
			{
				return this.baseType;
			}
			set
			{
				this.baseType = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00085D29 File Offset: 0x00084D29
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x00085D31 File Offset: 0x00084D31
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x00085D39 File Offset: 0x00084D39
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x00085D41 File Offset: 0x00084D41
		[XmlElement("anyAttribute")]
		public XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00085D4A File Offset: 0x00084D4A
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x040011D4 RID: 4564
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x040011D5 RID: 4565
		private XmlSchemaSimpleType baseType;

		// Token: 0x040011D6 RID: 4566
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();

		// Token: 0x040011D7 RID: 4567
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x040011D8 RID: 4568
		private XmlSchemaAnyAttribute anyAttribute;
	}
}
