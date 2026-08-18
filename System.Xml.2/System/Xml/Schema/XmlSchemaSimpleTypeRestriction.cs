using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002B1 RID: 689
	public class XmlSchemaSimpleTypeRestriction : XmlSchemaSimpleTypeContent
	{
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000D1F96 File Offset: 0x000D0196
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x000D1F9E File Offset: 0x000D019E
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

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x000D1FB7 File Offset: 0x000D01B7
		// (set) Token: 0x060027F1 RID: 10225 RVA: 0x000D1FBF File Offset: 0x000D01BF
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

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x000D1FC8 File Offset: 0x000D01C8
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

		// Token: 0x060027F3 RID: 10227 RVA: 0x000D1FD0 File Offset: 0x000D01D0
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)base.MemberwiseClone();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = this.baseTypeName.Clone();
			return xmlSchemaSimpleTypeRestriction;
		}

		// Token: 0x04001153 RID: 4435
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x04001154 RID: 4436
		private XmlSchemaSimpleType baseType;

		// Token: 0x04001155 RID: 4437
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();
	}
}
