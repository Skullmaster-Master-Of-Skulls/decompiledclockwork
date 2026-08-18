using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002B5 RID: 693
	public class XmlSchemaType : XmlSchemaAnnotated
	{
		// Token: 0x06002802 RID: 10242 RVA: 0x000D210E File Offset: 0x000D030E
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			return DatatypeImplementation.GetSimpleTypeFromXsdType(qualifiedName);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000D212A File Offset: 0x000D032A
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.GetSimpleTypeFromTypeCode(typeCode);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000D2132 File Offset: 0x000D0332
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlTypeCode typeCode)
		{
			if (typeCode == XmlTypeCode.Item)
			{
				return XmlSchemaComplexType.AnyType;
			}
			return null;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000D2140 File Offset: 0x000D0340
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

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x000D2192 File Offset: 0x000D0392
		// (set) Token: 0x06002807 RID: 10247 RVA: 0x000D219A File Offset: 0x000D039A
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

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x000D21A3 File Offset: 0x000D03A3
		// (set) Token: 0x06002809 RID: 10249 RVA: 0x000D21AB File Offset: 0x000D03AB
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

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x000D21B4 File Offset: 0x000D03B4
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x000D21BE File Offset: 0x000D03BE
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x000D21C6 File Offset: 0x000D03C6
		[XmlIgnore]
		[Obsolete("This property has been deprecated. Please use BaseXmlSchemaType property that returns a strongly typed base schema type. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object BaseSchemaType
		{
			get
			{
				if (this.baseSchemaType == null)
				{
					return null;
				}
				if (this.baseSchemaType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.baseSchemaType.Datatype;
				}
				return this.baseSchemaType;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x000D2200 File Offset: 0x000D0400
		[XmlIgnore]
		public XmlSchemaType BaseXmlSchemaType
		{
			get
			{
				return this.baseSchemaType;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x000D2208 File Offset: 0x000D0408
		[XmlIgnore]
		public XmlSchemaDerivationMethod DerivedBy
		{
			get
			{
				return this.derivedBy;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x000D2210 File Offset: 0x000D0410
		[XmlIgnore]
		public XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x000D2218 File Offset: 0x000D0418
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x000D221B File Offset: 0x000D041B
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

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x000D221D File Offset: 0x000D041D
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

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x000D223E File Offset: 0x000D043E
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

		// Token: 0x06002814 RID: 10260 RVA: 0x000D225C File Offset: 0x000D045C
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

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x000D2293 File Offset: 0x000D0493
		internal XmlSchemaContentType SchemaContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000D229B File Offset: 0x000D049B
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000D22A6 File Offset: 0x000D04A6
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000D22AF File Offset: 0x000D04AF
		internal void SetBaseSchemaType(XmlSchemaType value)
		{
			this.baseSchemaType = value;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000D22B8 File Offset: 0x000D04B8
		internal void SetDerivedBy(XmlSchemaDerivationMethod value)
		{
			this.derivedBy = value;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000D22C1 File Offset: 0x000D04C1
		internal void SetDatatype(XmlSchemaDatatype value)
		{
			this.datatype = value;
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x000D22CA File Offset: 0x000D04CA
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x000D22D4 File Offset: 0x000D04D4
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

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x000D22DF File Offset: 0x000D04DF
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x000D22E7 File Offset: 0x000D04E7
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

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x000D22F0 File Offset: 0x000D04F0
		internal virtual XmlQualifiedName DerivedFrom
		{
			get
			{
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000D22F7 File Offset: 0x000D04F7
		internal void SetContentType(XmlSchemaContentType value)
		{
			this.contentType = value;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000D2300 File Offset: 0x000D0500
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

		// Token: 0x06002822 RID: 10274 RVA: 0x000D2382 File Offset: 0x000D0582
		internal static bool IsDerivedFromDatatype(XmlSchemaDatatype derivedDataType, XmlSchemaDatatype baseDataType, XmlSchemaDerivationMethod except)
		{
			return DatatypeImplementation.AnySimpleType.Datatype == baseDataType || derivedDataType.IsDerivedFrom(baseDataType);
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x000D239A File Offset: 0x000D059A
		// (set) Token: 0x06002824 RID: 10276 RVA: 0x000D23A2 File Offset: 0x000D05A2
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

		// Token: 0x0400115C RID: 4444
		private string name;

		// Token: 0x0400115D RID: 4445
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x0400115E RID: 4446
		private XmlSchemaDerivationMethod derivedBy;

		// Token: 0x0400115F RID: 4447
		private XmlSchemaType baseSchemaType;

		// Token: 0x04001160 RID: 4448
		private XmlSchemaDatatype datatype;

		// Token: 0x04001161 RID: 4449
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x04001162 RID: 4450
		private volatile SchemaElementDecl elementDecl;

		// Token: 0x04001163 RID: 4451
		private volatile XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001164 RID: 4452
		private XmlSchemaType redefined;

		// Token: 0x04001165 RID: 4453
		private XmlSchemaContentType contentType;
	}
}
