using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000283 RID: 643
	public class XmlSchemaElement : XmlSchemaParticle
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x000CE9C4 File Offset: 0x000CCBC4
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x000CE9CC File Offset: 0x000CCBCC
		[XmlAttribute("abstract")]
		[DefaultValue(false)]
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

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x000CE9DC File Offset: 0x000CCBDC
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x000CE9E4 File Offset: 0x000CCBE4
		[XmlAttribute("block")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
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

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000CE9ED File Offset: 0x000CCBED
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x000CE9F5 File Offset: 0x000CCBF5
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

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000CE9FE File Offset: 0x000CCBFE
		// (set) Token: 0x06002678 RID: 9848 RVA: 0x000CEA06 File Offset: 0x000CCC06
		[XmlAttribute("final")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
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

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000CEA0F File Offset: 0x000CCC0F
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x000CEA17 File Offset: 0x000CCC17
		[XmlAttribute("fixed")]
		[DefaultValue(null)]
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

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000CEA20 File Offset: 0x000CCC20
		// (set) Token: 0x0600267C RID: 9852 RVA: 0x000CEA28 File Offset: 0x000CCC28
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

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000CEA31 File Offset: 0x000CCC31
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x000CEA39 File Offset: 0x000CCC39
		[XmlAttribute("name")]
		[DefaultValue("")]
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

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000CEA42 File Offset: 0x000CCC42
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x000CEA4A File Offset: 0x000CCC4A
		[XmlAttribute("nillable")]
		[DefaultValue(false)]
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

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000CEA5A File Offset: 0x000CCC5A
		[XmlIgnore]
		internal bool HasNillableAttribute
		{
			get
			{
				return this.hasNillableAttribute;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x000CEA62 File Offset: 0x000CCC62
		[XmlIgnore]
		internal bool HasAbstractAttribute
		{
			get
			{
				return this.hasAbstractAttribute;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000CEA6A File Offset: 0x000CCC6A
		// (set) Token: 0x06002684 RID: 9860 RVA: 0x000CEA72 File Offset: 0x000CCC72
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

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x000CEA8B File Offset: 0x000CCC8B
		// (set) Token: 0x06002686 RID: 9862 RVA: 0x000CEA93 File Offset: 0x000CCC93
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

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x000CEAAC File Offset: 0x000CCCAC
		// (set) Token: 0x06002688 RID: 9864 RVA: 0x000CEAB4 File Offset: 0x000CCCB4
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

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000CEACD File Offset: 0x000CCCCD
		// (set) Token: 0x0600268A RID: 9866 RVA: 0x000CEAD5 File Offset: 0x000CCCD5
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
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

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000CEADE File Offset: 0x000CCCDE
		[XmlElement("key", typeof(XmlSchemaKey))]
		[XmlElement("keyref", typeof(XmlSchemaKeyref))]
		[XmlElement("unique", typeof(XmlSchemaUnique))]
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

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x000CEAF9 File Offset: 0x000CCCF9
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x000CEB01 File Offset: 0x000CCD01
		[XmlIgnore]
		[Obsolete("This property has been deprecated. Please use ElementSchemaType property that returns a strongly typed element type. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object ElementType
		{
			get
			{
				if (this.elementType == null)
				{
					return null;
				}
				if (this.elementType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.elementType.Datatype;
				}
				return this.elementType;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000CEB3B File Offset: 0x000CCD3B
		[XmlIgnore]
		public XmlSchemaType ElementSchemaType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000CEB43 File Offset: 0x000CCD43
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x000CEB4B File Offset: 0x000CCD4B
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000CEB54 File Offset: 0x000CCD54
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

		// Token: 0x06002692 RID: 9874 RVA: 0x000CEB8B File Offset: 0x000CCD8B
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000CEB94 File Offset: 0x000CCD94
		internal void SetElementType(XmlSchemaType value)
		{
			this.elementType = value;
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000CEB9D File Offset: 0x000CCD9D
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000CEBA6 File Offset: 0x000CCDA6
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x000CEBAF File Offset: 0x000CCDAF
		[XmlIgnore]
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue.Length > 0;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x000CEBC9 File Offset: 0x000CCDC9
		internal bool HasConstraints
		{
			get
			{
				return this.constraints != null && this.constraints.Count > 0;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x000CEBE3 File Offset: 0x000CCDE3
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x000CEBEB File Offset: 0x000CCDEB
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

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x000CEBF4 File Offset: 0x000CCDF4
		// (set) Token: 0x0600269B RID: 9883 RVA: 0x000CEBFC File Offset: 0x000CCDFC
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

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000CEC05 File Offset: 0x000CCE05
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x000CEC0D File Offset: 0x000CCE0D
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

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x000CEC16 File Offset: 0x000CCE16
		[XmlIgnore]
		internal override string NameString
		{
			get
			{
				return this.qualifiedName.ToString();
			}
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000CEC23 File Offset: 0x000CCE23
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000CEC2C File Offset: 0x000CCE2C
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)base.MemberwiseClone();
			xmlSchemaElement.refName = this.refName.Clone();
			xmlSchemaElement.substitutionGroup = this.substitutionGroup.Clone();
			xmlSchemaElement.typeName = this.typeName.Clone();
			xmlSchemaElement.qualifiedName = this.qualifiedName.Clone();
			XmlSchemaComplexType xmlSchemaComplexType = this.type as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.QualifiedName.IsEmpty)
			{
				xmlSchemaElement.type = (XmlSchemaType)xmlSchemaComplexType.Clone(parentSchema);
			}
			xmlSchemaElement.constraints = null;
			return xmlSchemaElement;
		}

		// Token: 0x040010C8 RID: 4296
		private bool isAbstract;

		// Token: 0x040010C9 RID: 4297
		private bool hasAbstractAttribute;

		// Token: 0x040010CA RID: 4298
		private bool isNillable;

		// Token: 0x040010CB RID: 4299
		private bool hasNillableAttribute;

		// Token: 0x040010CC RID: 4300
		private bool isLocalTypeDerivationChecked;

		// Token: 0x040010CD RID: 4301
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x040010CE RID: 4302
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x040010CF RID: 4303
		private XmlSchemaForm form;

		// Token: 0x040010D0 RID: 4304
		private string defaultValue;

		// Token: 0x040010D1 RID: 4305
		private string fixedValue;

		// Token: 0x040010D2 RID: 4306
		private string name;

		// Token: 0x040010D3 RID: 4307
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x040010D4 RID: 4308
		private XmlQualifiedName substitutionGroup = XmlQualifiedName.Empty;

		// Token: 0x040010D5 RID: 4309
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x040010D6 RID: 4310
		private XmlSchemaType type;

		// Token: 0x040010D7 RID: 4311
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x040010D8 RID: 4312
		private XmlSchemaType elementType;

		// Token: 0x040010D9 RID: 4313
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x040010DA RID: 4314
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x040010DB RID: 4315
		private XmlSchemaObjectCollection constraints;

		// Token: 0x040010DC RID: 4316
		private SchemaElementDecl elementDecl;
	}
}
