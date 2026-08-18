using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000279 RID: 633
	public class XmlSchemaComplexContentExtension : XmlSchemaContent
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000CD9AC File Offset: 0x000CBBAC
		// (set) Token: 0x06002607 RID: 9735 RVA: 0x000CD9B4 File Offset: 0x000CBBB4
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

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x000CD9CD File Offset: 0x000CBBCD
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x000CD9D5 File Offset: 0x000CBBD5
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

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000CD9DE File Offset: 0x000CBBDE
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000CD9E6 File Offset: 0x000CBBE6
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x000CD9EE File Offset: 0x000CBBEE
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

		// Token: 0x0600260D RID: 9741 RVA: 0x000CD9F7 File Offset: 0x000CBBF7
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04001098 RID: 4248
		private XmlSchemaParticle particle;

		// Token: 0x04001099 RID: 4249
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x0400109A RID: 4250
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x0400109B RID: 4251
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
