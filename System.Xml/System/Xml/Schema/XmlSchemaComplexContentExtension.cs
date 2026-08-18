using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000243 RID: 579
	public class XmlSchemaComplexContentExtension : XmlSchemaContent
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x00081AE8 File Offset: 0x00080AE8
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x00081AF0 File Offset: 0x00080AF0
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

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x00081B09 File Offset: 0x00080B09
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x00081B11 File Offset: 0x00080B11
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		public XmlSchemaParticle Particle
		{
			get
			{
				return this.particle;
			}
			set
			{
				this.particle = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00081B1A File Offset: 0x00080B1A
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00081B22 File Offset: 0x00080B22
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x00081B2A File Offset: 0x00080B2A
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

		// Token: 0x06001B89 RID: 7049 RVA: 0x00081B33 File Offset: 0x00080B33
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04001111 RID: 4369
		private XmlSchemaParticle particle;

		// Token: 0x04001112 RID: 4370
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001113 RID: 4371
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001114 RID: 4372
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
