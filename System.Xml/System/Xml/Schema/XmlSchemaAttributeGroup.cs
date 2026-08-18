using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200023A RID: 570
	public class XmlSchemaAttributeGroup : XmlSchemaAnnotated
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000812C1 File Offset: 0x000802C1
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x000812C9 File Offset: 0x000802C9
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x000812D2 File Offset: 0x000802D2
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000812DA File Offset: 0x000802DA
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x000812E2 File Offset: 0x000802E2
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

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x000812EB File Offset: 0x000802EB
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000812F3 File Offset: 0x000802F3
		[XmlIgnore]
		internal XmlSchemaObjectTable AttributeUses
		{
			get
			{
				if (this.attributeUses == null)
				{
					this.attributeUses = new XmlSchemaObjectTable();
				}
				return this.attributeUses;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0008130E File Offset: 0x0008030E
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x00081316 File Offset: 0x00080316
		[XmlIgnore]
		internal XmlSchemaAnyAttribute AttributeWildcard
		{
			get
			{
				return this.attributeWildcard;
			}
			set
			{
				this.attributeWildcard = value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0008131F File Offset: 0x0008031F
		[XmlIgnore]
		public XmlSchemaAttributeGroup RedefinedAttributeGroup
		{
			get
			{
				return this.redefined;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00081327 File Offset: 0x00080327
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x0008132F File Offset: 0x0008032F
		[XmlIgnore]
		internal XmlSchemaAttributeGroup Redefined
		{
			get
			{
				return this.redefined;
			}
			set
			{
				this.redefined = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00081338 File Offset: 0x00080338
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00081340 File Offset: 0x00080340
		[XmlIgnore]
		internal int SelfReferenceCount
		{
			get
			{
				return this.selfReferenceCount;
			}
			set
			{
				this.selfReferenceCount = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00081349 File Offset: 0x00080349
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x00081351 File Offset: 0x00080351
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0008135A File Offset: 0x0008035A
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00081364 File Offset: 0x00080364
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)base.MemberwiseClone();
			if (XmlSchemaComplexType.HasAttributeQNameRef(this.attributes))
			{
				xmlSchemaAttributeGroup.attributes = XmlSchemaComplexType.CloneAttributes(this.attributes);
				xmlSchemaAttributeGroup.attributeUses = null;
			}
			return xmlSchemaAttributeGroup;
		}

		// Token: 0x040010F8 RID: 4344
		private string name;

		// Token: 0x040010F9 RID: 4345
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x040010FA RID: 4346
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x040010FB RID: 4347
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x040010FC RID: 4348
		private XmlSchemaAttributeGroup redefined;

		// Token: 0x040010FD RID: 4349
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x040010FE RID: 4350
		private XmlSchemaAnyAttribute attributeWildcard;

		// Token: 0x040010FF RID: 4351
		private int selfReferenceCount;
	}
}
