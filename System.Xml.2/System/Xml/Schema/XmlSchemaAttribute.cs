using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000271 RID: 625
	public class XmlSchemaAttribute : XmlSchemaAnnotated
	{
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000CCF9D File Offset: 0x000CB19D
		// (set) Token: 0x0600259C RID: 9628 RVA: 0x000CCFA5 File Offset: 0x000CB1A5
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

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000CCFAE File Offset: 0x000CB1AE
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x000CCFB6 File Offset: 0x000CB1B6
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

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x000CCFBF File Offset: 0x000CB1BF
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x000CCFC7 File Offset: 0x000CB1C7
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

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x000CCFD0 File Offset: 0x000CB1D0
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x000CCFD8 File Offset: 0x000CB1D8
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

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000CCFE1 File Offset: 0x000CB1E1
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x000CCFE9 File Offset: 0x000CB1E9
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

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000CD002 File Offset: 0x000CB202
		// (set) Token: 0x060025A6 RID: 9638 RVA: 0x000CD00A File Offset: 0x000CB20A
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

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000CD023 File Offset: 0x000CB223
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x000CD02B File Offset: 0x000CB22B
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

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000CD034 File Offset: 0x000CB234
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x000CD03C File Offset: 0x000CB23C
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

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x000CD045 File Offset: 0x000CB245
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x000CD04D File Offset: 0x000CB24D
		[XmlIgnore]
		[Obsolete("This property has been deprecated. Please use AttributeSchemaType property that returns a strongly typed attribute type. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object AttributeType
		{
			get
			{
				if (this.attributeType == null)
				{
					return null;
				}
				if (this.attributeType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.attributeType.Datatype;
				}
				return this.attributeType;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x000CD087 File Offset: 0x000CB287
		[XmlIgnore]
		public XmlSchemaSimpleType AttributeSchemaType
		{
			get
			{
				return this.attributeType;
			}
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000CD090 File Offset: 0x000CB290
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

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x000CD0C7 File Offset: 0x000CB2C7
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

		// Token: 0x060025B0 RID: 9648 RVA: 0x000CD0DE File Offset: 0x000CB2DE
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000CD0E7 File Offset: 0x000CB2E7
		internal void SetAttributeType(XmlSchemaSimpleType value)
		{
			this.attributeType = value;
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x000CD0F0 File Offset: 0x000CB2F0
		// (set) Token: 0x060025B3 RID: 9651 RVA: 0x000CD0F8 File Offset: 0x000CB2F8
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

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x000CD101 File Offset: 0x000CB301
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x000CD10C File Offset: 0x000CB30C
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x000CD114 File Offset: 0x000CB314
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

		// Token: 0x060025B7 RID: 9655 RVA: 0x000CD120 File Offset: 0x000CB320
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)base.MemberwiseClone();
			xmlSchemaAttribute.refName = this.refName.Clone();
			xmlSchemaAttribute.typeName = this.typeName.Clone();
			xmlSchemaAttribute.qualifiedName = this.qualifiedName.Clone();
			return xmlSchemaAttribute;
		}

		// Token: 0x04001074 RID: 4212
		private string defaultValue;

		// Token: 0x04001075 RID: 4213
		private string fixedValue;

		// Token: 0x04001076 RID: 4214
		private string name;

		// Token: 0x04001077 RID: 4215
		private XmlSchemaForm form;

		// Token: 0x04001078 RID: 4216
		private XmlSchemaUse use;

		// Token: 0x04001079 RID: 4217
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x0400107A RID: 4218
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x0400107B RID: 4219
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x0400107C RID: 4220
		private XmlSchemaSimpleType type;

		// Token: 0x0400107D RID: 4221
		private XmlSchemaSimpleType attributeType;

		// Token: 0x0400107E RID: 4222
		private SchemaAttDef attDef;
	}
}
