using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027A RID: 634
	public class XmlSchemaComplexContentRestriction : XmlSchemaContent
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000CDA1E File Offset: 0x000CBC1E
		// (set) Token: 0x06002610 RID: 9744 RVA: 0x000CDA26 File Offset: 0x000CBC26
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

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000CDA3F File Offset: 0x000CBC3F
		// (set) Token: 0x06002612 RID: 9746 RVA: 0x000CDA47 File Offset: 0x000CBC47
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
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

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000CDA50 File Offset: 0x000CBC50
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000CDA58 File Offset: 0x000CBC58
		// (set) Token: 0x06002615 RID: 9749 RVA: 0x000CDA60 File Offset: 0x000CBC60
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

		// Token: 0x06002616 RID: 9750 RVA: 0x000CDA69 File Offset: 0x000CBC69
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x0400109C RID: 4252
		private XmlSchemaParticle particle;

		// Token: 0x0400109D RID: 4253
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x0400109E RID: 4254
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x0400109F RID: 4255
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
