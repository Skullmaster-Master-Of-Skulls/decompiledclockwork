using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027C RID: 636
	public class XmlSchemaSimpleTypeRestriction : XmlSchemaSimpleTypeContent
	{
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00085E8A File Offset: 0x00084E8A
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x00085E92 File Offset: 0x00084E92
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

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00085EAB File Offset: 0x00084EAB
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x00085EB3 File Offset: 0x00084EB3
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

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00085EBC File Offset: 0x00084EBC
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00085EC4 File Offset: 0x00084EC4
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)base.MemberwiseClone();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = this.baseTypeName.Clone();
			return xmlSchemaSimpleTypeRestriction;
		}

		// Token: 0x040011DD RID: 4573
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x040011DE RID: 4574
		private XmlSchemaSimpleType baseType;

		// Token: 0x040011DF RID: 4575
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();
	}
}
