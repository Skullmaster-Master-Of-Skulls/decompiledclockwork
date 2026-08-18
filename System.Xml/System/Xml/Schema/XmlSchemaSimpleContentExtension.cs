using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000277 RID: 631
	public class XmlSchemaSimpleContentExtension : XmlSchemaContent
	{
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x00085C96 File Offset: 0x00084C96
		// (set) Token: 0x06001D37 RID: 7479 RVA: 0x00085C9E File Offset: 0x00084C9E
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

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x00085CB7 File Offset: 0x00084CB7
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x00085CBF File Offset: 0x00084CBF
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x00085CC7 File Offset: 0x00084CC7
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

		// Token: 0x06001D3B RID: 7483 RVA: 0x00085CD0 File Offset: 0x00084CD0
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x040011D1 RID: 4561
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x040011D2 RID: 4562
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x040011D3 RID: 4563
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
