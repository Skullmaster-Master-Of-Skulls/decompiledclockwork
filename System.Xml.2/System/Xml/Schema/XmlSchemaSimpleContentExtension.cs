using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002AC RID: 684
	public class XmlSchemaSimpleContentExtension : XmlSchemaContent
	{
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x000D1DA2 File Offset: 0x000CFFA2
		// (set) Token: 0x060027D0 RID: 10192 RVA: 0x000D1DAA File Offset: 0x000CFFAA
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

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000D1DC3 File Offset: 0x000CFFC3
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x000D1DCB File Offset: 0x000CFFCB
		// (set) Token: 0x060027D3 RID: 10195 RVA: 0x000D1DD3 File Offset: 0x000CFFD3
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

		// Token: 0x060027D4 RID: 10196 RVA: 0x000D1DDC File Offset: 0x000CFFDC
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04001147 RID: 4423
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001148 RID: 4424
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001149 RID: 4425
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
