using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002AD RID: 685
	public class XmlSchemaSimpleContentRestriction : XmlSchemaContent
	{
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x000D1E03 File Offset: 0x000D0003
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x000D1E0B File Offset: 0x000D000B
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

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000D1E24 File Offset: 0x000D0024
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x000D1E2C File Offset: 0x000D002C
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

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x000D1E35 File Offset: 0x000D0035
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x000D1E3D File Offset: 0x000D003D
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000D1E45 File Offset: 0x000D0045
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x000D1E4D File Offset: 0x000D004D
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

		// Token: 0x060027DE RID: 10206 RVA: 0x000D1E56 File Offset: 0x000D0056
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x0400114A RID: 4426
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x0400114B RID: 4427
		private XmlSchemaSimpleType baseType;

		// Token: 0x0400114C RID: 4428
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();

		// Token: 0x0400114D RID: 4429
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x0400114E RID: 4430
		private XmlSchemaAnyAttribute anyAttribute;
	}
}
