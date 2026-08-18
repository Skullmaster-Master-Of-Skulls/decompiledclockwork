using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000245 RID: 581
	public class XmlSchemaType : XmlSchemaAnnotated
	{
		// Token: 0x06001B94 RID: 7060 RVA: 0x00081BCC File Offset: 0x00080BCC
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			return DatatypeImplementation.GetSimpleTypeFromXsdType(qualifiedName);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00081BE8 File Offset: 0x00080BE8
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.GetSimpleTypeFromTypeCode(typeCode);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00081BF0 File Offset: 0x00080BF0
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlTypeCode typeCode)
		{
			if (typeCode == XmlTypeCode.Item)
			{
				return XmlSchemaComplexType.AnyType;
			}
			return null;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00081C00 File Offset: 0x00080C00
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			if (qualifiedName.Equals(XmlSchemaComplexType.AnyType.QualifiedName))
			{
				return XmlSchemaComplexType.AnyType;
			}
			if (qualifiedName.Equals(XmlSchemaComplexType.UntypedAnyType.QualifiedName))
			{
				return XmlSchemaComplexType.UntypedAnyType;
			}
			return null;
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x00081C52 File Offset: 0x00080C52
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x00081C5A File Offset: 0x00080C5A
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

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x00081C63 File Offset: 0x00080C63
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x00081C6B File Offset: 0x00080C6B
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

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00081C74 File Offset: 0x00080C74
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00081C7C File Offset: 0x00080C7C
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x00081C84 File Offset: 0x00080C84
		[XmlIgnore]
		[Obsolete("This property has been deprecated. Please use BaseXmlSchemaType property that returns a strongly typed base schema type. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object BaseSchemaType
		{
			get
			{
				if (this.baseSchemaType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.baseSchemaType.Datatype;
				}
				return this.baseSchemaType;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00081CB4 File Offset: 0x00080CB4
		[XmlIgnore]
		public XmlSchemaType BaseXmlSchemaType
		{
			get
			{
				return this.baseSchemaType;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00081CBC File Offset: 0x00080CBC
		[XmlIgnore]
		public XmlSchemaDerivationMethod DerivedBy
		{
			get
			{
				return this.derivedBy;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00081CC4 File Offset: 0x00080CC4
		[XmlIgnore]
		public XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00081CCC File Offset: 0x00080CCC
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x00081CCF File Offset: 0x00080CCF
		[XmlIgnore]
		public virtual bool IsMixed
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00081CD1 File Offset: 0x00080CD1
		[XmlIgnore]
		public XmlTypeCode TypeCode
		{
			get
			{
				if (this == XmlSchemaComplexType.AnyType)
				{
					return XmlTypeCode.Item;
				}
				if (this.datatype == null)
				{
					return XmlTypeCode.None;
				}
				return this.datatype.TypeCode;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00081CF2 File Offset: 0x00080CF2
		[XmlIgnore]
		internal XmlValueConverter ValueConverter
		{
			get
			{
				if (this.datatype == null)
				{
					return XmlUntypedConverter.Untyped;
				}
				return this.datatype.ValueConverter;
			}
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00081D10 File Offset: 0x00080D10
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

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00081D47 File Offset: 0x00080D47
		internal XmlSchemaContentType SchemaContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00081D4F File Offset: 0x00080D4F
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00081D58 File Offset: 0x00080D58
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00081D61 File Offset: 0x00080D61
		internal void SetBaseSchemaType(XmlSchemaType value)
		{
			this.baseSchemaType = value;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00081D6A File Offset: 0x00080D6A
		internal void SetDerivedBy(XmlSchemaDerivationMethod value)
		{
			this.derivedBy = value;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00081D73 File Offset: 0x00080D73
		internal void SetDatatype(XmlSchemaDatatype value)
		{
			this.datatype = value;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00081D7C File Offset: 0x00080D7C
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x00081D84 File Offset: 0x00080D84
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

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x00081D8D File Offset: 0x00080D8D
		// (set) Token: 0x06001BB0 RID: 7088 RVA: 0x00081D95 File Offset: 0x00080D95
		[XmlIgnore]
		internal XmlSchemaType Redefined
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

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x00081D9E File Offset: 0x00080D9E
		internal virtual XmlQualifiedName DerivedFrom
		{
			get
			{
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00081DA5 File Offset: 0x00080DA5
		internal void SetContentType(XmlSchemaContentType value)
		{
			this.contentType = value;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00081DB0 File Offset: 0x00080DB0
		public static bool IsDerivedFrom(XmlSchemaType derivedType, XmlSchemaType baseType, XmlSchemaDerivationMethod except)
		{
			if (derivedType == null || baseType == null)
			{
				return false;
			}
			if (derivedType == baseType)
			{
				return true;
			}
			if (baseType == XmlSchemaComplexType.AnyType)
			{
				return true;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType;
			XmlSchemaSimpleType xmlSchemaSimpleType2;
			for (;;)
			{
				xmlSchemaSimpleType = (derivedType as XmlSchemaSimpleType);
				xmlSchemaSimpleType2 = (baseType as XmlSchemaSimpleType);
				if (xmlSchemaSimpleType2 != null && xmlSchemaSimpleType != null)
				{
					break;
				}
				if ((except & derivedType.DerivedBy) != XmlSchemaDerivationMethod.Empty)
				{
					return false;
				}
				derivedType = derivedType.BaseXmlSchemaType;
				if (derivedType == baseType)
				{
					return true;
				}
				if (derivedType == null)
				{
					return false;
				}
			}
			return xmlSchemaSimpleType2 == DatatypeImplementation.AnySimpleType || ((except & derivedType.DerivedBy) == XmlSchemaDerivationMethod.Empty && xmlSchemaSimpleType.Datatype.IsDerivedFrom(xmlSchemaSimpleType2.Datatype));
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00081E32 File Offset: 0x00080E32
		internal static bool IsDerivedFromDatatype(XmlSchemaDatatype derivedDataType, XmlSchemaDatatype baseDataType, XmlSchemaDerivationMethod except)
		{
			return DatatypeImplementation.AnySimpleType.Datatype == baseDataType || derivedDataType.IsDerivedFrom(baseDataType);
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00081E4A File Offset: 0x00080E4A
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x00081E52 File Offset: 0x00080E52
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

		// Token: 0x04001119 RID: 4377
		private string name;

		// Token: 0x0400111A RID: 4378
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x0400111B RID: 4379
		private XmlSchemaDerivationMethod derivedBy;

		// Token: 0x0400111C RID: 4380
		private XmlSchemaType baseSchemaType;

		// Token: 0x0400111D RID: 4381
		private XmlSchemaDatatype datatype;

		// Token: 0x0400111E RID: 4382
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x0400111F RID: 4383
		private SchemaElementDecl elementDecl;

		// Token: 0x04001120 RID: 4384
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001121 RID: 4385
		private XmlSchemaType redefined;

		// Token: 0x04001122 RID: 4386
		private XmlSchemaContentType contentType;
	}
}
