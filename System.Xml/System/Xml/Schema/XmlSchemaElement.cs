using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200024B RID: 587
	public class XmlSchemaElement : XmlSchemaParticle
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x000828B8 File Offset: 0x000818B8
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x000828C0 File Offset: 0x000818C0
		[DefaultValue(false)]
		[XmlAttribute("abstract")]
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
			set
			{
				this.isAbstract = value;
				this.hasAbstractAttribute = true;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x000828D0 File Offset: 0x000818D0
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x000828D8 File Offset: 0x000818D8
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		[XmlAttribute("block")]
		public XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x000828E1 File Offset: 0x000818E1
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x000828E9 File Offset: 0x000818E9
		[XmlAttribute("default")]
		[DefaultValue(null)]
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x000828F2 File Offset: 0x000818F2
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x000828FA File Offset: 0x000818FA
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		[XmlAttribute("final")]
		public XmlSchemaDerivationMethod Final
		{
			get
			{
				return this.final;
			}
			set
			{
				this.final = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00082903 File Offset: 0x00081903
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x0008290B File Offset: 0x0008190B
		[DefaultValue(null)]
		[XmlAttribute("fixed")]
		public string FixedValue
		{
			get
			{
				return this.fixedValue;
			}
			set
			{
				this.fixedValue = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00082914 File Offset: 0x00081914
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x0008291C File Offset: 0x0008191C
		[XmlAttribute("form")]
		[DefaultValue(XmlSchemaForm.None)]
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00082925 File Offset: 0x00081925
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x0008292D File Offset: 0x0008192D
		[DefaultValue("")]
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

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00082936 File Offset: 0x00081936
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x0008293E File Offset: 0x0008193E
		[DefaultValue(false)]
		[XmlAttribute("nillable")]
		public bool IsNillable
		{
			get
			{
				return this.isNillable;
			}
			set
			{
				this.isNillable = value;
				this.hasNillableAttribute = true;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0008294E File Offset: 0x0008194E
		[XmlIgnore]
		internal bool HasNillableAttribute
		{
			get
			{
				return this.hasNillableAttribute;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00082956 File Offset: 0x00081956
		[XmlIgnore]
		internal bool HasAbstractAttribute
		{
			get
			{
				return this.hasAbstractAttribute;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x0008295E File Offset: 0x0008195E
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x00082966 File Offset: 0x00081966
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0008297F File Offset: 0x0008197F
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x00082987 File Offset: 0x00081987
		[XmlAttribute("substitutionGroup")]
		public XmlQualifiedName SubstitutionGroup
		{
			get
			{
				return this.substitutionGroup;
			}
			set
			{
				this.substitutionGroup = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x000829A0 File Offset: 0x000819A0
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x000829A8 File Offset: 0x000819A8
		[XmlAttribute("type")]
		public XmlQualifiedName SchemaTypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x000829C1 File Offset: 0x000819C1
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x000829C9 File Offset: 0x000819C9
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		public XmlSchemaType SchemaType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000829D2 File Offset: 0x000819D2
		[XmlElement("keyref", typeof(XmlSchemaKeyref))]
		[XmlElement("unique", typeof(XmlSchemaUnique))]
		[XmlElement("key", typeof(XmlSchemaKey))]
		public XmlSchemaObjectCollection Constraints
		{
			get
			{
				if (this.constraints == null)
				{
					this.constraints = new XmlSchemaObjectCollection();
				}
				return this.constraints;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x000829ED File Offset: 0x000819ED
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x000829F5 File Offset: 0x000819F5
		[Obsolete("This property has been deprecated. Please use ElementSchemaType property that returns a strongly typed element type. http://go.microsoft.com/fwlink/?linkid=14202")]
		[XmlIgnore]
		public object ElementType
		{
			get
			{
				if (this.elementType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.elementType.Datatype;
				}
				return this.elementType;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00082A25 File Offset: 0x00081A25
		[XmlIgnore]
		public XmlSchemaType ElementSchemaType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x00082A2D File Offset: 0x00081A2D
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00082A35 File Offset: 0x00081A35
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00082A40 File Offset: 0x00081A40
		internal XmlReader Validate(XmlReader reader, XmlResolver resolver, XmlSchemaSet schemaSet, ValidationEventHandler valEventHandler)
		{
			if (schemaSet != null)
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.Schemas = schemaSet;
				xmlReaderSettings.ValidationEventHandler += valEventHandler;
				return new XsdValidatingReader(reader, resolver, xmlReaderSettings, this);
			}
			return null;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00082A77 File Offset: 0x00081A77
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00082A80 File Offset: 0x00081A80
		internal void SetElementType(XmlSchemaType value)
		{
			this.elementType = value;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00082A89 File Offset: 0x00081A89
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00082A92 File Offset: 0x00081A92
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00082A9B File Offset: 0x00081A9B
		[XmlIgnore]
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue.Length > 0;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00082AB5 File Offset: 0x00081AB5
		internal bool HasConstraints
		{
			get
			{
				return this.constraints != null && this.constraints.Count > 0;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x00082ACF File Offset: 0x00081ACF
		// (set) Token: 0x06001C13 RID: 7187 RVA: 0x00082AD7 File Offset: 0x00081AD7
		internal bool IsLocalTypeDerivationChecked
		{
			get
			{
				return this.isLocalTypeDerivationChecked;
			}
			set
			{
				this.isLocalTypeDerivationChecked = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x00082AE0 File Offset: 0x00081AE0
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x00082AE8 File Offset: 0x00081AE8
		internal SchemaElementDecl ElementDecl
		{
			get
			{
				return this.elementDecl;
			}
			set
			{
				this.elementDecl = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x00082AF1 File Offset: 0x00081AF1
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x00082AF9 File Offset: 0x00081AF9
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

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x00082B02 File Offset: 0x00081B02
		[XmlIgnore]
		internal override string NameString
		{
			get
			{
				return this.qualifiedName.ToString();
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00082B10 File Offset: 0x00081B10
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)base.MemberwiseClone();
			xmlSchemaElement.refName = this.refName.Clone();
			xmlSchemaElement.substitutionGroup = this.substitutionGroup.Clone();
			xmlSchemaElement.typeName = this.typeName.Clone();
			xmlSchemaElement.constraints = null;
			return xmlSchemaElement;
		}

		// Token: 0x0400114C RID: 4428
		private bool isAbstract;

		// Token: 0x0400114D RID: 4429
		private bool hasAbstractAttribute;

		// Token: 0x0400114E RID: 4430
		private bool isNillable;

		// Token: 0x0400114F RID: 4431
		private bool hasNillableAttribute;

		// Token: 0x04001150 RID: 4432
		private bool isLocalTypeDerivationChecked;

		// Token: 0x04001151 RID: 4433
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x04001152 RID: 4434
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x04001153 RID: 4435
		private XmlSchemaForm form;

		// Token: 0x04001154 RID: 4436
		private string defaultValue;

		// Token: 0x04001155 RID: 4437
		private string fixedValue;

		// Token: 0x04001156 RID: 4438
		private string name;

		// Token: 0x04001157 RID: 4439
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04001158 RID: 4440
		private XmlQualifiedName substitutionGroup = XmlQualifiedName.Empty;

		// Token: 0x04001159 RID: 4441
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x0400115A RID: 4442
		private XmlSchemaType type;

		// Token: 0x0400115B RID: 4443
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x0400115C RID: 4444
		private XmlSchemaType elementType;

		// Token: 0x0400115D RID: 4445
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x0400115E RID: 4446
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x0400115F RID: 4447
		private XmlSchemaObjectCollection constraints;

		// Token: 0x04001160 RID: 4448
		private SchemaElementDecl elementDecl;
	}
}
