using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000239 RID: 569
	public class XmlSchemaAttribute : XmlSchemaAnnotated
	{
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000810D9 File Offset: 0x000800D9
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x000810E1 File Offset: 0x000800E1
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

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x000810EA File Offset: 0x000800EA
		// (set) Token: 0x06001B15 RID: 6933 RVA: 0x000810F2 File Offset: 0x000800F2
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

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x000810FB File Offset: 0x000800FB
		// (set) Token: 0x06001B17 RID: 6935 RVA: 0x00081103 File Offset: 0x00080103
		[DefaultValue(XmlSchemaForm.None)]
		[XmlAttribute("form")]
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

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0008110C File Offset: 0x0008010C
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x00081114 File Offset: 0x00080114
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

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0008111D File Offset: 0x0008011D
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x00081125 File Offset: 0x00080125
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

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0008113E File Offset: 0x0008013E
		// (set) Token: 0x06001B1D RID: 6941 RVA: 0x00081146 File Offset: 0x00080146
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

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0008115F File Offset: 0x0008015F
		// (set) Token: 0x06001B1F RID: 6943 RVA: 0x00081167 File Offset: 0x00080167
		[XmlElement("simpleType")]
		public XmlSchemaSimpleType SchemaType
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

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x00081170 File Offset: 0x00080170
		// (set) Token: 0x06001B21 RID: 6945 RVA: 0x00081178 File Offset: 0x00080178
		[XmlAttribute("use")]
		[DefaultValue(XmlSchemaUse.None)]
		public XmlSchemaUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				this.use = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x00081181 File Offset: 0x00080181
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x00081189 File Offset: 0x00080189
		[XmlIgnore]
		[Obsolete("This property has been deprecated. Please use AttributeSchemaType property that returns a strongly typed attribute type. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object AttributeType
		{
			get
			{
				if (this.attributeType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.attributeType.Datatype;
				}
				return this.attributeType;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x000811B9 File Offset: 0x000801B9
		[XmlIgnore]
		public XmlSchemaSimpleType AttributeSchemaType
		{
			get
			{
				return this.attributeType;
			}
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000811C4 File Offset: 0x000801C4
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

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x000811FB File Offset: 0x000801FB
		[XmlIgnore]
		internal XmlSchemaDatatype Datatype
		{
			get
			{
				if (this.attributeType != null)
				{
					return this.attributeType.Datatype;
				}
				return null;
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00081212 File Offset: 0x00080212
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0008121B File Offset: 0x0008021B
		internal void SetAttributeType(XmlSchemaSimpleType value)
		{
			this.attributeType = value;
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x00081224 File Offset: 0x00080224
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0008122C File Offset: 0x0008022C
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x00081234 File Offset: 0x00080234
		internal SchemaAttDef AttDef
		{
			get
			{
				return this.attDef;
			}
			set
			{
				this.attDef = value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0008123D File Offset: 0x0008023D
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0008124B File Offset: 0x0008024B
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x00081253 File Offset: 0x00080253
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

		// Token: 0x06001B2F RID: 6959 RVA: 0x0008125C File Offset: 0x0008025C
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)base.MemberwiseClone();
			xmlSchemaAttribute.refName = this.refName.Clone();
			xmlSchemaAttribute.typeName = this.typeName.Clone();
			return xmlSchemaAttribute;
		}

		// Token: 0x040010EC RID: 4332
		private string defaultValue;

		// Token: 0x040010ED RID: 4333
		private string fixedValue;

		// Token: 0x040010EE RID: 4334
		private string name;

		// Token: 0x040010EF RID: 4335
		private string prefix;

		// Token: 0x040010F0 RID: 4336
		private XmlSchemaForm form;

		// Token: 0x040010F1 RID: 4337
		private XmlSchemaUse use;

		// Token: 0x040010F2 RID: 4338
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x040010F3 RID: 4339
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x040010F4 RID: 4340
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x040010F5 RID: 4341
		private XmlSchemaSimpleType type;

		// Token: 0x040010F6 RID: 4342
		private XmlSchemaSimpleType attributeType;

		// Token: 0x040010F7 RID: 4343
		private SchemaAttDef attDef;
	}
}
