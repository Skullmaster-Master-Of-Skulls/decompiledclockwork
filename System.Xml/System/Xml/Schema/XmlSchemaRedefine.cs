using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000272 RID: 626
	public class XmlSchemaRedefine : XmlSchemaExternal
	{
		// Token: 0x06001CEC RID: 7404 RVA: 0x00083D46 File Offset: 0x00082D46
		public XmlSchemaRedefine()
		{
			base.Compositor = Compositor.Redefine;
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x00083D81 File Offset: 0x00082D81
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00083D89 File Offset: 0x00082D89
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				return this.attributeGroups;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x00083D91 File Offset: 0x00082D91
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00083D99 File Offset: 0x00082D99
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00083DA1 File Offset: 0x00082DA1
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x040011B6 RID: 4534
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x040011B7 RID: 4535
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x040011B8 RID: 4536
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x040011B9 RID: 4537
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();
	}
}
