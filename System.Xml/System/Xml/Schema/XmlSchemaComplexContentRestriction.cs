using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000244 RID: 580
	public class XmlSchemaComplexContentRestriction : XmlSchemaContent
	{
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00081B5A File Offset: 0x00080B5A
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x00081B62 File Offset: 0x00080B62
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

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00081B7B File Offset: 0x00080B7B
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00081B83 File Offset: 0x00080B83
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

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x00081B8C File Offset: 0x00080B8C
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x00081B94 File Offset: 0x00080B94
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x00081B9C File Offset: 0x00080B9C
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

		// Token: 0x06001B92 RID: 7058 RVA: 0x00081BA5 File Offset: 0x00080BA5
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04001115 RID: 4373
		private XmlSchemaParticle particle;

		// Token: 0x04001116 RID: 4374
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001117 RID: 4375
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001118 RID: 4376
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
