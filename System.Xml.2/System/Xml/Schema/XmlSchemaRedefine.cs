using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002A7 RID: 679
	public class XmlSchemaRedefine : XmlSchemaExternal
	{
		// Token: 0x06002785 RID: 10117 RVA: 0x000CFDC7 File Offset: 0x000CDFC7
		public XmlSchemaRedefine()
		{
			base.Compositor = Compositor.Redefine;
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x000CFE02 File Offset: 0x000CE002
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000CFE0A File Offset: 0x000CE00A
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				return this.attributeGroups;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x000CFE12 File Offset: 0x000CE012
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x000CFE1A File Offset: 0x000CE01A
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000CFE22 File Offset: 0x000CE022
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x0400112C RID: 4396
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x0400112D RID: 4397
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x0400112E RID: 4398
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x0400112F RID: 4399
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();
	}
}
