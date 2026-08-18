using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000272 RID: 626
	public class XmlSchemaAttributeGroup : XmlSchemaAnnotated
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x000CD196 File Offset: 0x000CB396
		// (set) Token: 0x060025BA RID: 9658 RVA: 0x000CD19E File Offset: 0x000CB39E
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

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000CD1A7 File Offset: 0x000CB3A7
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000CD1AF File Offset: 0x000CB3AF
		// (set) Token: 0x060025BD RID: 9661 RVA: 0x000CD1B7 File Offset: 0x000CB3B7
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

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x000CD1C0 File Offset: 0x000CB3C0
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x000CD1C8 File Offset: 0x000CB3C8
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

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000CD1E3 File Offset: 0x000CB3E3
		// (set) Token: 0x060025C1 RID: 9665 RVA: 0x000CD1EB File Offset: 0x000CB3EB
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

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000CD1F4 File Offset: 0x000CB3F4
		[XmlIgnore]
		public XmlSchemaAttributeGroup RedefinedAttributeGroup
		{
			get
			{
				return this.redefined;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000CD1FC File Offset: 0x000CB3FC
		// (set) Token: 0x060025C4 RID: 9668 RVA: 0x000CD204 File Offset: 0x000CB404
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

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000CD20D File Offset: 0x000CB40D
		// (set) Token: 0x060025C6 RID: 9670 RVA: 0x000CD215 File Offset: 0x000CB415
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

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000CD21E File Offset: 0x000CB41E
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x000CD226 File Offset: 0x000CB426
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

		// Token: 0x060025C9 RID: 9673 RVA: 0x000CD22F File Offset: 0x000CB42F
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000CD238 File Offset: 0x000CB438
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

		// Token: 0x0400107F RID: 4223
		private string name;

		// Token: 0x04001080 RID: 4224
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001081 RID: 4225
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001082 RID: 4226
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001083 RID: 4227
		private XmlSchemaAttributeGroup redefined;

		// Token: 0x04001084 RID: 4228
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x04001085 RID: 4229
		private XmlSchemaAnyAttribute attributeWildcard;

		// Token: 0x04001086 RID: 4230
		private int selfReferenceCount;
	}
}
