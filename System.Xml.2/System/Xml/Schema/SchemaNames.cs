using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025F RID: 607
	internal sealed class SchemaNames
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000C6DC5 File Offset: 0x000C4FC5
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000C6DD0 File Offset: 0x000C4FD0
		public SchemaNames(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
			this.NsDataType = nameTable.Add("urn:schemas-microsoft-com:datatypes");
			this.NsDataTypeAlias = nameTable.Add("uuid:C2F41010-65B3-11D1-A29F-00AA00C14882");
			this.NsDataTypeOld = nameTable.Add("urn:uuid:C2F41010-65B3-11D1-A29F-00AA00C14882/");
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.NsXmlNs = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXdr = nameTable.Add("urn:schemas-microsoft-com:xml-data");
			this.NsXdrAlias = nameTable.Add("uuid:BDC6E3F0-6DA3-11D1-A2A3-00AA00C14882");
			this.NsXs = nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = nameTable.Add("type");
			this.XsiNil = nameTable.Add("nil");
			this.XsiSchemaLocation = nameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = nameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = nameTable.Add("schema");
			this.XdrSchema = nameTable.Add("Schema");
			this.QnPCData = new XmlQualifiedName(nameTable.Add("#PCDATA"));
			this.QnXml = new XmlQualifiedName(nameTable.Add("xml"));
			this.QnXmlNs = new XmlQualifiedName(nameTable.Add("xmlns"), this.NsXmlNs);
			this.QnDtDt = new XmlQualifiedName(nameTable.Add("dt"), this.NsDataType);
			this.QnXmlLang = new XmlQualifiedName(nameTable.Add("lang"), this.NsXml);
			this.QnName = new XmlQualifiedName(nameTable.Add("name"));
			this.QnType = new XmlQualifiedName(nameTable.Add("type"));
			this.QnMaxOccurs = new XmlQualifiedName(nameTable.Add("maxOccurs"));
			this.QnMinOccurs = new XmlQualifiedName(nameTable.Add("minOccurs"));
			this.QnInfinite = new XmlQualifiedName(nameTable.Add("*"));
			this.QnModel = new XmlQualifiedName(nameTable.Add("model"));
			this.QnOpen = new XmlQualifiedName(nameTable.Add("open"));
			this.QnClosed = new XmlQualifiedName(nameTable.Add("closed"));
			this.QnContent = new XmlQualifiedName(nameTable.Add("content"));
			this.QnMixed = new XmlQualifiedName(nameTable.Add("mixed"));
			this.QnEmpty = new XmlQualifiedName(nameTable.Add("empty"));
			this.QnEltOnly = new XmlQualifiedName(nameTable.Add("eltOnly"));
			this.QnTextOnly = new XmlQualifiedName(nameTable.Add("textOnly"));
			this.QnOrder = new XmlQualifiedName(nameTable.Add("order"));
			this.QnSeq = new XmlQualifiedName(nameTable.Add("seq"));
			this.QnOne = new XmlQualifiedName(nameTable.Add("one"));
			this.QnMany = new XmlQualifiedName(nameTable.Add("many"));
			this.QnRequired = new XmlQualifiedName(nameTable.Add("required"));
			this.QnYes = new XmlQualifiedName(nameTable.Add("yes"));
			this.QnNo = new XmlQualifiedName(nameTable.Add("no"));
			this.QnString = new XmlQualifiedName(nameTable.Add("string"));
			this.QnID = new XmlQualifiedName(nameTable.Add("id"));
			this.QnIDRef = new XmlQualifiedName(nameTable.Add("idref"));
			this.QnIDRefs = new XmlQualifiedName(nameTable.Add("idrefs"));
			this.QnEntity = new XmlQualifiedName(nameTable.Add("entity"));
			this.QnEntities = new XmlQualifiedName(nameTable.Add("entities"));
			this.QnNmToken = new XmlQualifiedName(nameTable.Add("nmtoken"));
			this.QnNmTokens = new XmlQualifiedName(nameTable.Add("nmtokens"));
			this.QnEnumeration = new XmlQualifiedName(nameTable.Add("enumeration"));
			this.QnDefault = new XmlQualifiedName(nameTable.Add("default"));
			this.QnTargetNamespace = new XmlQualifiedName(nameTable.Add("targetNamespace"));
			this.QnVersion = new XmlQualifiedName(nameTable.Add("version"));
			this.QnFinalDefault = new XmlQualifiedName(nameTable.Add("finalDefault"));
			this.QnBlockDefault = new XmlQualifiedName(nameTable.Add("blockDefault"));
			this.QnFixed = new XmlQualifiedName(nameTable.Add("fixed"));
			this.QnAbstract = new XmlQualifiedName(nameTable.Add("abstract"));
			this.QnBlock = new XmlQualifiedName(nameTable.Add("block"));
			this.QnSubstitutionGroup = new XmlQualifiedName(nameTable.Add("substitutionGroup"));
			this.QnFinal = new XmlQualifiedName(nameTable.Add("final"));
			this.QnNillable = new XmlQualifiedName(nameTable.Add("nillable"));
			this.QnRef = new XmlQualifiedName(nameTable.Add("ref"));
			this.QnBase = new XmlQualifiedName(nameTable.Add("base"));
			this.QnDerivedBy = new XmlQualifiedName(nameTable.Add("derivedBy"));
			this.QnNamespace = new XmlQualifiedName(nameTable.Add("namespace"));
			this.QnProcessContents = new XmlQualifiedName(nameTable.Add("processContents"));
			this.QnRefer = new XmlQualifiedName(nameTable.Add("refer"));
			this.QnPublic = new XmlQualifiedName(nameTable.Add("public"));
			this.QnSystem = new XmlQualifiedName(nameTable.Add("system"));
			this.QnSchemaLocation = new XmlQualifiedName(nameTable.Add("schemaLocation"));
			this.QnValue = new XmlQualifiedName(nameTable.Add("value"));
			this.QnUse = new XmlQualifiedName(nameTable.Add("use"));
			this.QnForm = new XmlQualifiedName(nameTable.Add("form"));
			this.QnAttributeFormDefault = new XmlQualifiedName(nameTable.Add("attributeFormDefault"));
			this.QnElementFormDefault = new XmlQualifiedName(nameTable.Add("elementFormDefault"));
			this.QnSource = new XmlQualifiedName(nameTable.Add("source"));
			this.QnMemberTypes = new XmlQualifiedName(nameTable.Add("memberTypes"));
			this.QnItemType = new XmlQualifiedName(nameTable.Add("itemType"));
			this.QnXPath = new XmlQualifiedName(nameTable.Add("xpath"));
			this.QnXdrSchema = new XmlQualifiedName(this.XdrSchema, this.NsXdr);
			this.QnXdrElementType = new XmlQualifiedName(nameTable.Add("ElementType"), this.NsXdr);
			this.QnXdrElement = new XmlQualifiedName(nameTable.Add("element"), this.NsXdr);
			this.QnXdrGroup = new XmlQualifiedName(nameTable.Add("group"), this.NsXdr);
			this.QnXdrAttributeType = new XmlQualifiedName(nameTable.Add("AttributeType"), this.NsXdr);
			this.QnXdrAttribute = new XmlQualifiedName(nameTable.Add("attribute"), this.NsXdr);
			this.QnXdrDataType = new XmlQualifiedName(nameTable.Add("datatype"), this.NsXdr);
			this.QnXdrDescription = new XmlQualifiedName(nameTable.Add("description"), this.NsXdr);
			this.QnXdrExtends = new XmlQualifiedName(nameTable.Add("extends"), this.NsXdr);
			this.QnXdrAliasSchema = new XmlQualifiedName(nameTable.Add("Schema"), this.NsDataTypeAlias);
			this.QnDtType = new XmlQualifiedName(nameTable.Add("type"), this.NsDataType);
			this.QnDtValues = new XmlQualifiedName(nameTable.Add("values"), this.NsDataType);
			this.QnDtMaxLength = new XmlQualifiedName(nameTable.Add("maxLength"), this.NsDataType);
			this.QnDtMinLength = new XmlQualifiedName(nameTable.Add("minLength"), this.NsDataType);
			this.QnDtMax = new XmlQualifiedName(nameTable.Add("max"), this.NsDataType);
			this.QnDtMin = new XmlQualifiedName(nameTable.Add("min"), this.NsDataType);
			this.QnDtMinExclusive = new XmlQualifiedName(nameTable.Add("minExclusive"), this.NsDataType);
			this.QnDtMaxExclusive = new XmlQualifiedName(nameTable.Add("maxExclusive"), this.NsDataType);
			this.QnXsdSchema = new XmlQualifiedName(this.XsdSchema, this.NsXs);
			this.QnXsdAnnotation = new XmlQualifiedName(nameTable.Add("annotation"), this.NsXs);
			this.QnXsdInclude = new XmlQualifiedName(nameTable.Add("include"), this.NsXs);
			this.QnXsdImport = new XmlQualifiedName(nameTable.Add("import"), this.NsXs);
			this.QnXsdElement = new XmlQualifiedName(nameTable.Add("element"), this.NsXs);
			this.QnXsdAttribute = new XmlQualifiedName(nameTable.Add("attribute"), this.NsXs);
			this.QnXsdAttributeGroup = new XmlQualifiedName(nameTable.Add("attributeGroup"), this.NsXs);
			this.QnXsdAnyAttribute = new XmlQualifiedName(nameTable.Add("anyAttribute"), this.NsXs);
			this.QnXsdGroup = new XmlQualifiedName(nameTable.Add("group"), this.NsXs);
			this.QnXsdAll = new XmlQualifiedName(nameTable.Add("all"), this.NsXs);
			this.QnXsdChoice = new XmlQualifiedName(nameTable.Add("choice"), this.NsXs);
			this.QnXsdSequence = new XmlQualifiedName(nameTable.Add("sequence"), this.NsXs);
			this.QnXsdAny = new XmlQualifiedName(nameTable.Add("any"), this.NsXs);
			this.QnXsdNotation = new XmlQualifiedName(nameTable.Add("notation"), this.NsXs);
			this.QnXsdSimpleType = new XmlQualifiedName(nameTable.Add("simpleType"), this.NsXs);
			this.QnXsdComplexType = new XmlQualifiedName(nameTable.Add("complexType"), this.NsXs);
			this.QnXsdUnique = new XmlQualifiedName(nameTable.Add("unique"), this.NsXs);
			this.QnXsdKey = new XmlQualifiedName(nameTable.Add("key"), this.NsXs);
			this.QnXsdKeyRef = new XmlQualifiedName(nameTable.Add("keyref"), this.NsXs);
			this.QnXsdSelector = new XmlQualifiedName(nameTable.Add("selector"), this.NsXs);
			this.QnXsdField = new XmlQualifiedName(nameTable.Add("field"), this.NsXs);
			this.QnXsdMinExclusive = new XmlQualifiedName(nameTable.Add("minExclusive"), this.NsXs);
			this.QnXsdMinInclusive = new XmlQualifiedName(nameTable.Add("minInclusive"), this.NsXs);
			this.QnXsdMaxInclusive = new XmlQualifiedName(nameTable.Add("maxInclusive"), this.NsXs);
			this.QnXsdMaxExclusive = new XmlQualifiedName(nameTable.Add("maxExclusive"), this.NsXs);
			this.QnXsdTotalDigits = new XmlQualifiedName(nameTable.Add("totalDigits"), this.NsXs);
			this.QnXsdFractionDigits = new XmlQualifiedName(nameTable.Add("fractionDigits"), this.NsXs);
			this.QnXsdLength = new XmlQualifiedName(nameTable.Add("length"), this.NsXs);
			this.QnXsdMinLength = new XmlQualifiedName(nameTable.Add("minLength"), this.NsXs);
			this.QnXsdMaxLength = new XmlQualifiedName(nameTable.Add("maxLength"), this.NsXs);
			this.QnXsdEnumeration = new XmlQualifiedName(nameTable.Add("enumeration"), this.NsXs);
			this.QnXsdPattern = new XmlQualifiedName(nameTable.Add("pattern"), this.NsXs);
			this.QnXsdDocumentation = new XmlQualifiedName(nameTable.Add("documentation"), this.NsXs);
			this.QnXsdAppinfo = new XmlQualifiedName(nameTable.Add("appinfo"), this.NsXs);
			this.QnXsdComplexContent = new XmlQualifiedName(nameTable.Add("complexContent"), this.NsXs);
			this.QnXsdSimpleContent = new XmlQualifiedName(nameTable.Add("simpleContent"), this.NsXs);
			this.QnXsdRestriction = new XmlQualifiedName(nameTable.Add("restriction"), this.NsXs);
			this.QnXsdExtension = new XmlQualifiedName(nameTable.Add("extension"), this.NsXs);
			this.QnXsdUnion = new XmlQualifiedName(nameTable.Add("union"), this.NsXs);
			this.QnXsdList = new XmlQualifiedName(nameTable.Add("list"), this.NsXs);
			this.QnXsdWhiteSpace = new XmlQualifiedName(nameTable.Add("whiteSpace"), this.NsXs);
			this.QnXsdRedefine = new XmlQualifiedName(nameTable.Add("redefine"), this.NsXs);
			this.QnXsdAnyType = new XmlQualifiedName(nameTable.Add("anyType"), this.NsXs);
			this.CreateTokenToQNameTable();
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000C7B1C File Offset: 0x000C5D1C
		public void CreateTokenToQNameTable()
		{
			this.TokenToQName[1] = this.QnName;
			this.TokenToQName[2] = this.QnType;
			this.TokenToQName[3] = this.QnMaxOccurs;
			this.TokenToQName[4] = this.QnMinOccurs;
			this.TokenToQName[5] = this.QnInfinite;
			this.TokenToQName[6] = this.QnModel;
			this.TokenToQName[7] = this.QnOpen;
			this.TokenToQName[8] = this.QnClosed;
			this.TokenToQName[9] = this.QnContent;
			this.TokenToQName[10] = this.QnMixed;
			this.TokenToQName[11] = this.QnEmpty;
			this.TokenToQName[12] = this.QnEltOnly;
			this.TokenToQName[13] = this.QnTextOnly;
			this.TokenToQName[14] = this.QnOrder;
			this.TokenToQName[15] = this.QnSeq;
			this.TokenToQName[16] = this.QnOne;
			this.TokenToQName[17] = this.QnMany;
			this.TokenToQName[18] = this.QnRequired;
			this.TokenToQName[19] = this.QnYes;
			this.TokenToQName[20] = this.QnNo;
			this.TokenToQName[21] = this.QnString;
			this.TokenToQName[22] = this.QnID;
			this.TokenToQName[23] = this.QnIDRef;
			this.TokenToQName[24] = this.QnIDRefs;
			this.TokenToQName[25] = this.QnEntity;
			this.TokenToQName[26] = this.QnEntities;
			this.TokenToQName[27] = this.QnNmToken;
			this.TokenToQName[28] = this.QnNmTokens;
			this.TokenToQName[29] = this.QnEnumeration;
			this.TokenToQName[30] = this.QnDefault;
			this.TokenToQName[31] = this.QnXdrSchema;
			this.TokenToQName[32] = this.QnXdrElementType;
			this.TokenToQName[33] = this.QnXdrElement;
			this.TokenToQName[34] = this.QnXdrGroup;
			this.TokenToQName[35] = this.QnXdrAttributeType;
			this.TokenToQName[36] = this.QnXdrAttribute;
			this.TokenToQName[37] = this.QnXdrDataType;
			this.TokenToQName[38] = this.QnXdrDescription;
			this.TokenToQName[39] = this.QnXdrExtends;
			this.TokenToQName[40] = this.QnXdrAliasSchema;
			this.TokenToQName[41] = this.QnDtType;
			this.TokenToQName[42] = this.QnDtValues;
			this.TokenToQName[43] = this.QnDtMaxLength;
			this.TokenToQName[44] = this.QnDtMinLength;
			this.TokenToQName[45] = this.QnDtMax;
			this.TokenToQName[46] = this.QnDtMin;
			this.TokenToQName[47] = this.QnDtMinExclusive;
			this.TokenToQName[48] = this.QnDtMaxExclusive;
			this.TokenToQName[49] = this.QnTargetNamespace;
			this.TokenToQName[50] = this.QnVersion;
			this.TokenToQName[51] = this.QnFinalDefault;
			this.TokenToQName[52] = this.QnBlockDefault;
			this.TokenToQName[53] = this.QnFixed;
			this.TokenToQName[54] = this.QnAbstract;
			this.TokenToQName[55] = this.QnBlock;
			this.TokenToQName[56] = this.QnSubstitutionGroup;
			this.TokenToQName[57] = this.QnFinal;
			this.TokenToQName[58] = this.QnNillable;
			this.TokenToQName[59] = this.QnRef;
			this.TokenToQName[60] = this.QnBase;
			this.TokenToQName[61] = this.QnDerivedBy;
			this.TokenToQName[62] = this.QnNamespace;
			this.TokenToQName[63] = this.QnProcessContents;
			this.TokenToQName[64] = this.QnRefer;
			this.TokenToQName[65] = this.QnPublic;
			this.TokenToQName[66] = this.QnSystem;
			this.TokenToQName[67] = this.QnSchemaLocation;
			this.TokenToQName[68] = this.QnValue;
			this.TokenToQName[119] = this.QnItemType;
			this.TokenToQName[120] = this.QnMemberTypes;
			this.TokenToQName[121] = this.QnXPath;
			this.TokenToQName[74] = this.QnXsdSchema;
			this.TokenToQName[75] = this.QnXsdAnnotation;
			this.TokenToQName[76] = this.QnXsdInclude;
			this.TokenToQName[77] = this.QnXsdImport;
			this.TokenToQName[78] = this.QnXsdElement;
			this.TokenToQName[79] = this.QnXsdAttribute;
			this.TokenToQName[80] = this.QnXsdAttributeGroup;
			this.TokenToQName[81] = this.QnXsdAnyAttribute;
			this.TokenToQName[82] = this.QnXsdGroup;
			this.TokenToQName[83] = this.QnXsdAll;
			this.TokenToQName[84] = this.QnXsdChoice;
			this.TokenToQName[85] = this.QnXsdSequence;
			this.TokenToQName[86] = this.QnXsdAny;
			this.TokenToQName[87] = this.QnXsdNotation;
			this.TokenToQName[88] = this.QnXsdSimpleType;
			this.TokenToQName[89] = this.QnXsdComplexType;
			this.TokenToQName[90] = this.QnXsdUnique;
			this.TokenToQName[91] = this.QnXsdKey;
			this.TokenToQName[92] = this.QnXsdKeyRef;
			this.TokenToQName[93] = this.QnXsdSelector;
			this.TokenToQName[94] = this.QnXsdField;
			this.TokenToQName[95] = this.QnXsdMinExclusive;
			this.TokenToQName[96] = this.QnXsdMinInclusive;
			this.TokenToQName[97] = this.QnXsdMaxExclusive;
			this.TokenToQName[98] = this.QnXsdMaxInclusive;
			this.TokenToQName[99] = this.QnXsdTotalDigits;
			this.TokenToQName[100] = this.QnXsdFractionDigits;
			this.TokenToQName[101] = this.QnXsdLength;
			this.TokenToQName[102] = this.QnXsdMinLength;
			this.TokenToQName[103] = this.QnXsdMaxLength;
			this.TokenToQName[104] = this.QnXsdEnumeration;
			this.TokenToQName[105] = this.QnXsdPattern;
			this.TokenToQName[117] = this.QnXsdWhiteSpace;
			this.TokenToQName[106] = this.QnXsdDocumentation;
			this.TokenToQName[107] = this.QnXsdAppinfo;
			this.TokenToQName[108] = this.QnXsdComplexContent;
			this.TokenToQName[110] = this.QnXsdRestriction;
			this.TokenToQName[113] = this.QnXsdRestriction;
			this.TokenToQName[115] = this.QnXsdRestriction;
			this.TokenToQName[109] = this.QnXsdExtension;
			this.TokenToQName[112] = this.QnXsdExtension;
			this.TokenToQName[111] = this.QnXsdSimpleContent;
			this.TokenToQName[116] = this.QnXsdUnion;
			this.TokenToQName[114] = this.QnXsdList;
			this.TokenToQName[118] = this.QnXsdRedefine;
			this.TokenToQName[69] = this.QnSource;
			this.TokenToQName[72] = this.QnUse;
			this.TokenToQName[73] = this.QnForm;
			this.TokenToQName[71] = this.QnElementFormDefault;
			this.TokenToQName[70] = this.QnAttributeFormDefault;
			this.TokenToQName[122] = this.QnXmlLang;
			this.TokenToQName[0] = XmlQualifiedName.Empty;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000C8254 File Offset: 0x000C6454
		public SchemaType SchemaTypeFromRoot(string localName, string ns)
		{
			if (this.IsXSDRoot(localName, ns))
			{
				return SchemaType.XSD;
			}
			if (this.IsXDRRoot(localName, XmlSchemaDatatype.XdrCanonizeUri(ns, this.nameTable, this)))
			{
				return SchemaType.XDR;
			}
			return SchemaType.None;
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000C827B File Offset: 0x000C647B
		public bool IsXSDRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXs) && Ref.Equal(localName, this.XsdSchema);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000C8299 File Offset: 0x000C6499
		public bool IsXDRRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXdr) && Ref.Equal(localName, this.XdrSchema);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000C82B7 File Offset: 0x000C64B7
		public XmlQualifiedName GetName(SchemaNames.Token token)
		{
			return this.TokenToQName[(int)token];
		}

		// Token: 0x04000F4A RID: 3914
		private XmlNameTable nameTable;

		// Token: 0x04000F4B RID: 3915
		public string NsDataType;

		// Token: 0x04000F4C RID: 3916
		public string NsDataTypeAlias;

		// Token: 0x04000F4D RID: 3917
		public string NsDataTypeOld;

		// Token: 0x04000F4E RID: 3918
		public string NsXml;

		// Token: 0x04000F4F RID: 3919
		public string NsXmlNs;

		// Token: 0x04000F50 RID: 3920
		public string NsXdr;

		// Token: 0x04000F51 RID: 3921
		public string NsXdrAlias;

		// Token: 0x04000F52 RID: 3922
		public string NsXs;

		// Token: 0x04000F53 RID: 3923
		public string NsXsi;

		// Token: 0x04000F54 RID: 3924
		public string XsiType;

		// Token: 0x04000F55 RID: 3925
		public string XsiNil;

		// Token: 0x04000F56 RID: 3926
		public string XsiSchemaLocation;

		// Token: 0x04000F57 RID: 3927
		public string XsiNoNamespaceSchemaLocation;

		// Token: 0x04000F58 RID: 3928
		public string XsdSchema;

		// Token: 0x04000F59 RID: 3929
		public string XdrSchema;

		// Token: 0x04000F5A RID: 3930
		public XmlQualifiedName QnPCData;

		// Token: 0x04000F5B RID: 3931
		public XmlQualifiedName QnXml;

		// Token: 0x04000F5C RID: 3932
		public XmlQualifiedName QnXmlNs;

		// Token: 0x04000F5D RID: 3933
		public XmlQualifiedName QnDtDt;

		// Token: 0x04000F5E RID: 3934
		public XmlQualifiedName QnXmlLang;

		// Token: 0x04000F5F RID: 3935
		public XmlQualifiedName QnName;

		// Token: 0x04000F60 RID: 3936
		public XmlQualifiedName QnType;

		// Token: 0x04000F61 RID: 3937
		public XmlQualifiedName QnMaxOccurs;

		// Token: 0x04000F62 RID: 3938
		public XmlQualifiedName QnMinOccurs;

		// Token: 0x04000F63 RID: 3939
		public XmlQualifiedName QnInfinite;

		// Token: 0x04000F64 RID: 3940
		public XmlQualifiedName QnModel;

		// Token: 0x04000F65 RID: 3941
		public XmlQualifiedName QnOpen;

		// Token: 0x04000F66 RID: 3942
		public XmlQualifiedName QnClosed;

		// Token: 0x04000F67 RID: 3943
		public XmlQualifiedName QnContent;

		// Token: 0x04000F68 RID: 3944
		public XmlQualifiedName QnMixed;

		// Token: 0x04000F69 RID: 3945
		public XmlQualifiedName QnEmpty;

		// Token: 0x04000F6A RID: 3946
		public XmlQualifiedName QnEltOnly;

		// Token: 0x04000F6B RID: 3947
		public XmlQualifiedName QnTextOnly;

		// Token: 0x04000F6C RID: 3948
		public XmlQualifiedName QnOrder;

		// Token: 0x04000F6D RID: 3949
		public XmlQualifiedName QnSeq;

		// Token: 0x04000F6E RID: 3950
		public XmlQualifiedName QnOne;

		// Token: 0x04000F6F RID: 3951
		public XmlQualifiedName QnMany;

		// Token: 0x04000F70 RID: 3952
		public XmlQualifiedName QnRequired;

		// Token: 0x04000F71 RID: 3953
		public XmlQualifiedName QnYes;

		// Token: 0x04000F72 RID: 3954
		public XmlQualifiedName QnNo;

		// Token: 0x04000F73 RID: 3955
		public XmlQualifiedName QnString;

		// Token: 0x04000F74 RID: 3956
		public XmlQualifiedName QnID;

		// Token: 0x04000F75 RID: 3957
		public XmlQualifiedName QnIDRef;

		// Token: 0x04000F76 RID: 3958
		public XmlQualifiedName QnIDRefs;

		// Token: 0x04000F77 RID: 3959
		public XmlQualifiedName QnEntity;

		// Token: 0x04000F78 RID: 3960
		public XmlQualifiedName QnEntities;

		// Token: 0x04000F79 RID: 3961
		public XmlQualifiedName QnNmToken;

		// Token: 0x04000F7A RID: 3962
		public XmlQualifiedName QnNmTokens;

		// Token: 0x04000F7B RID: 3963
		public XmlQualifiedName QnEnumeration;

		// Token: 0x04000F7C RID: 3964
		public XmlQualifiedName QnDefault;

		// Token: 0x04000F7D RID: 3965
		public XmlQualifiedName QnXdrSchema;

		// Token: 0x04000F7E RID: 3966
		public XmlQualifiedName QnXdrElementType;

		// Token: 0x04000F7F RID: 3967
		public XmlQualifiedName QnXdrElement;

		// Token: 0x04000F80 RID: 3968
		public XmlQualifiedName QnXdrGroup;

		// Token: 0x04000F81 RID: 3969
		public XmlQualifiedName QnXdrAttributeType;

		// Token: 0x04000F82 RID: 3970
		public XmlQualifiedName QnXdrAttribute;

		// Token: 0x04000F83 RID: 3971
		public XmlQualifiedName QnXdrDataType;

		// Token: 0x04000F84 RID: 3972
		public XmlQualifiedName QnXdrDescription;

		// Token: 0x04000F85 RID: 3973
		public XmlQualifiedName QnXdrExtends;

		// Token: 0x04000F86 RID: 3974
		public XmlQualifiedName QnXdrAliasSchema;

		// Token: 0x04000F87 RID: 3975
		public XmlQualifiedName QnDtType;

		// Token: 0x04000F88 RID: 3976
		public XmlQualifiedName QnDtValues;

		// Token: 0x04000F89 RID: 3977
		public XmlQualifiedName QnDtMaxLength;

		// Token: 0x04000F8A RID: 3978
		public XmlQualifiedName QnDtMinLength;

		// Token: 0x04000F8B RID: 3979
		public XmlQualifiedName QnDtMax;

		// Token: 0x04000F8C RID: 3980
		public XmlQualifiedName QnDtMin;

		// Token: 0x04000F8D RID: 3981
		public XmlQualifiedName QnDtMinExclusive;

		// Token: 0x04000F8E RID: 3982
		public XmlQualifiedName QnDtMaxExclusive;

		// Token: 0x04000F8F RID: 3983
		public XmlQualifiedName QnTargetNamespace;

		// Token: 0x04000F90 RID: 3984
		public XmlQualifiedName QnVersion;

		// Token: 0x04000F91 RID: 3985
		public XmlQualifiedName QnFinalDefault;

		// Token: 0x04000F92 RID: 3986
		public XmlQualifiedName QnBlockDefault;

		// Token: 0x04000F93 RID: 3987
		public XmlQualifiedName QnFixed;

		// Token: 0x04000F94 RID: 3988
		public XmlQualifiedName QnAbstract;

		// Token: 0x04000F95 RID: 3989
		public XmlQualifiedName QnBlock;

		// Token: 0x04000F96 RID: 3990
		public XmlQualifiedName QnSubstitutionGroup;

		// Token: 0x04000F97 RID: 3991
		public XmlQualifiedName QnFinal;

		// Token: 0x04000F98 RID: 3992
		public XmlQualifiedName QnNillable;

		// Token: 0x04000F99 RID: 3993
		public XmlQualifiedName QnRef;

		// Token: 0x04000F9A RID: 3994
		public XmlQualifiedName QnBase;

		// Token: 0x04000F9B RID: 3995
		public XmlQualifiedName QnDerivedBy;

		// Token: 0x04000F9C RID: 3996
		public XmlQualifiedName QnNamespace;

		// Token: 0x04000F9D RID: 3997
		public XmlQualifiedName QnProcessContents;

		// Token: 0x04000F9E RID: 3998
		public XmlQualifiedName QnRefer;

		// Token: 0x04000F9F RID: 3999
		public XmlQualifiedName QnPublic;

		// Token: 0x04000FA0 RID: 4000
		public XmlQualifiedName QnSystem;

		// Token: 0x04000FA1 RID: 4001
		public XmlQualifiedName QnSchemaLocation;

		// Token: 0x04000FA2 RID: 4002
		public XmlQualifiedName QnValue;

		// Token: 0x04000FA3 RID: 4003
		public XmlQualifiedName QnUse;

		// Token: 0x04000FA4 RID: 4004
		public XmlQualifiedName QnForm;

		// Token: 0x04000FA5 RID: 4005
		public XmlQualifiedName QnElementFormDefault;

		// Token: 0x04000FA6 RID: 4006
		public XmlQualifiedName QnAttributeFormDefault;

		// Token: 0x04000FA7 RID: 4007
		public XmlQualifiedName QnItemType;

		// Token: 0x04000FA8 RID: 4008
		public XmlQualifiedName QnMemberTypes;

		// Token: 0x04000FA9 RID: 4009
		public XmlQualifiedName QnXPath;

		// Token: 0x04000FAA RID: 4010
		public XmlQualifiedName QnXsdSchema;

		// Token: 0x04000FAB RID: 4011
		public XmlQualifiedName QnXsdAnnotation;

		// Token: 0x04000FAC RID: 4012
		public XmlQualifiedName QnXsdInclude;

		// Token: 0x04000FAD RID: 4013
		public XmlQualifiedName QnXsdImport;

		// Token: 0x04000FAE RID: 4014
		public XmlQualifiedName QnXsdElement;

		// Token: 0x04000FAF RID: 4015
		public XmlQualifiedName QnXsdAttribute;

		// Token: 0x04000FB0 RID: 4016
		public XmlQualifiedName QnXsdAttributeGroup;

		// Token: 0x04000FB1 RID: 4017
		public XmlQualifiedName QnXsdAnyAttribute;

		// Token: 0x04000FB2 RID: 4018
		public XmlQualifiedName QnXsdGroup;

		// Token: 0x04000FB3 RID: 4019
		public XmlQualifiedName QnXsdAll;

		// Token: 0x04000FB4 RID: 4020
		public XmlQualifiedName QnXsdChoice;

		// Token: 0x04000FB5 RID: 4021
		public XmlQualifiedName QnXsdSequence;

		// Token: 0x04000FB6 RID: 4022
		public XmlQualifiedName QnXsdAny;

		// Token: 0x04000FB7 RID: 4023
		public XmlQualifiedName QnXsdNotation;

		// Token: 0x04000FB8 RID: 4024
		public XmlQualifiedName QnXsdSimpleType;

		// Token: 0x04000FB9 RID: 4025
		public XmlQualifiedName QnXsdComplexType;

		// Token: 0x04000FBA RID: 4026
		public XmlQualifiedName QnXsdUnique;

		// Token: 0x04000FBB RID: 4027
		public XmlQualifiedName QnXsdKey;

		// Token: 0x04000FBC RID: 4028
		public XmlQualifiedName QnXsdKeyRef;

		// Token: 0x04000FBD RID: 4029
		public XmlQualifiedName QnXsdSelector;

		// Token: 0x04000FBE RID: 4030
		public XmlQualifiedName QnXsdField;

		// Token: 0x04000FBF RID: 4031
		public XmlQualifiedName QnXsdMinExclusive;

		// Token: 0x04000FC0 RID: 4032
		public XmlQualifiedName QnXsdMinInclusive;

		// Token: 0x04000FC1 RID: 4033
		public XmlQualifiedName QnXsdMaxInclusive;

		// Token: 0x04000FC2 RID: 4034
		public XmlQualifiedName QnXsdMaxExclusive;

		// Token: 0x04000FC3 RID: 4035
		public XmlQualifiedName QnXsdTotalDigits;

		// Token: 0x04000FC4 RID: 4036
		public XmlQualifiedName QnXsdFractionDigits;

		// Token: 0x04000FC5 RID: 4037
		public XmlQualifiedName QnXsdLength;

		// Token: 0x04000FC6 RID: 4038
		public XmlQualifiedName QnXsdMinLength;

		// Token: 0x04000FC7 RID: 4039
		public XmlQualifiedName QnXsdMaxLength;

		// Token: 0x04000FC8 RID: 4040
		public XmlQualifiedName QnXsdEnumeration;

		// Token: 0x04000FC9 RID: 4041
		public XmlQualifiedName QnXsdPattern;

		// Token: 0x04000FCA RID: 4042
		public XmlQualifiedName QnXsdDocumentation;

		// Token: 0x04000FCB RID: 4043
		public XmlQualifiedName QnXsdAppinfo;

		// Token: 0x04000FCC RID: 4044
		public XmlQualifiedName QnSource;

		// Token: 0x04000FCD RID: 4045
		public XmlQualifiedName QnXsdComplexContent;

		// Token: 0x04000FCE RID: 4046
		public XmlQualifiedName QnXsdSimpleContent;

		// Token: 0x04000FCF RID: 4047
		public XmlQualifiedName QnXsdRestriction;

		// Token: 0x04000FD0 RID: 4048
		public XmlQualifiedName QnXsdExtension;

		// Token: 0x04000FD1 RID: 4049
		public XmlQualifiedName QnXsdUnion;

		// Token: 0x04000FD2 RID: 4050
		public XmlQualifiedName QnXsdList;

		// Token: 0x04000FD3 RID: 4051
		public XmlQualifiedName QnXsdWhiteSpace;

		// Token: 0x04000FD4 RID: 4052
		public XmlQualifiedName QnXsdRedefine;

		// Token: 0x04000FD5 RID: 4053
		public XmlQualifiedName QnXsdAnyType;

		// Token: 0x04000FD6 RID: 4054
		internal XmlQualifiedName[] TokenToQName = new XmlQualifiedName[123];

		// Token: 0x02000498 RID: 1176
		public enum Token
		{
			// Token: 0x04001E4C RID: 7756
			Empty,
			// Token: 0x04001E4D RID: 7757
			SchemaName,
			// Token: 0x04001E4E RID: 7758
			SchemaType,
			// Token: 0x04001E4F RID: 7759
			SchemaMaxOccurs,
			// Token: 0x04001E50 RID: 7760
			SchemaMinOccurs,
			// Token: 0x04001E51 RID: 7761
			SchemaInfinite,
			// Token: 0x04001E52 RID: 7762
			SchemaModel,
			// Token: 0x04001E53 RID: 7763
			SchemaOpen,
			// Token: 0x04001E54 RID: 7764
			SchemaClosed,
			// Token: 0x04001E55 RID: 7765
			SchemaContent,
			// Token: 0x04001E56 RID: 7766
			SchemaMixed,
			// Token: 0x04001E57 RID: 7767
			SchemaEmpty,
			// Token: 0x04001E58 RID: 7768
			SchemaElementOnly,
			// Token: 0x04001E59 RID: 7769
			SchemaTextOnly,
			// Token: 0x04001E5A RID: 7770
			SchemaOrder,
			// Token: 0x04001E5B RID: 7771
			SchemaSeq,
			// Token: 0x04001E5C RID: 7772
			SchemaOne,
			// Token: 0x04001E5D RID: 7773
			SchemaMany,
			// Token: 0x04001E5E RID: 7774
			SchemaRequired,
			// Token: 0x04001E5F RID: 7775
			SchemaYes,
			// Token: 0x04001E60 RID: 7776
			SchemaNo,
			// Token: 0x04001E61 RID: 7777
			SchemaString,
			// Token: 0x04001E62 RID: 7778
			SchemaId,
			// Token: 0x04001E63 RID: 7779
			SchemaIdref,
			// Token: 0x04001E64 RID: 7780
			SchemaIdrefs,
			// Token: 0x04001E65 RID: 7781
			SchemaEntity,
			// Token: 0x04001E66 RID: 7782
			SchemaEntities,
			// Token: 0x04001E67 RID: 7783
			SchemaNmtoken,
			// Token: 0x04001E68 RID: 7784
			SchemaNmtokens,
			// Token: 0x04001E69 RID: 7785
			SchemaEnumeration,
			// Token: 0x04001E6A RID: 7786
			SchemaDefault,
			// Token: 0x04001E6B RID: 7787
			XdrRoot,
			// Token: 0x04001E6C RID: 7788
			XdrElementType,
			// Token: 0x04001E6D RID: 7789
			XdrElement,
			// Token: 0x04001E6E RID: 7790
			XdrGroup,
			// Token: 0x04001E6F RID: 7791
			XdrAttributeType,
			// Token: 0x04001E70 RID: 7792
			XdrAttribute,
			// Token: 0x04001E71 RID: 7793
			XdrDatatype,
			// Token: 0x04001E72 RID: 7794
			XdrDescription,
			// Token: 0x04001E73 RID: 7795
			XdrExtends,
			// Token: 0x04001E74 RID: 7796
			SchemaXdrRootAlias,
			// Token: 0x04001E75 RID: 7797
			SchemaDtType,
			// Token: 0x04001E76 RID: 7798
			SchemaDtValues,
			// Token: 0x04001E77 RID: 7799
			SchemaDtMaxLength,
			// Token: 0x04001E78 RID: 7800
			SchemaDtMinLength,
			// Token: 0x04001E79 RID: 7801
			SchemaDtMax,
			// Token: 0x04001E7A RID: 7802
			SchemaDtMin,
			// Token: 0x04001E7B RID: 7803
			SchemaDtMinExclusive,
			// Token: 0x04001E7C RID: 7804
			SchemaDtMaxExclusive,
			// Token: 0x04001E7D RID: 7805
			SchemaTargetNamespace,
			// Token: 0x04001E7E RID: 7806
			SchemaVersion,
			// Token: 0x04001E7F RID: 7807
			SchemaFinalDefault,
			// Token: 0x04001E80 RID: 7808
			SchemaBlockDefault,
			// Token: 0x04001E81 RID: 7809
			SchemaFixed,
			// Token: 0x04001E82 RID: 7810
			SchemaAbstract,
			// Token: 0x04001E83 RID: 7811
			SchemaBlock,
			// Token: 0x04001E84 RID: 7812
			SchemaSubstitutionGroup,
			// Token: 0x04001E85 RID: 7813
			SchemaFinal,
			// Token: 0x04001E86 RID: 7814
			SchemaNillable,
			// Token: 0x04001E87 RID: 7815
			SchemaRef,
			// Token: 0x04001E88 RID: 7816
			SchemaBase,
			// Token: 0x04001E89 RID: 7817
			SchemaDerivedBy,
			// Token: 0x04001E8A RID: 7818
			SchemaNamespace,
			// Token: 0x04001E8B RID: 7819
			SchemaProcessContents,
			// Token: 0x04001E8C RID: 7820
			SchemaRefer,
			// Token: 0x04001E8D RID: 7821
			SchemaPublic,
			// Token: 0x04001E8E RID: 7822
			SchemaSystem,
			// Token: 0x04001E8F RID: 7823
			SchemaSchemaLocation,
			// Token: 0x04001E90 RID: 7824
			SchemaValue,
			// Token: 0x04001E91 RID: 7825
			SchemaSource,
			// Token: 0x04001E92 RID: 7826
			SchemaAttributeFormDefault,
			// Token: 0x04001E93 RID: 7827
			SchemaElementFormDefault,
			// Token: 0x04001E94 RID: 7828
			SchemaUse,
			// Token: 0x04001E95 RID: 7829
			SchemaForm,
			// Token: 0x04001E96 RID: 7830
			XsdSchema,
			// Token: 0x04001E97 RID: 7831
			XsdAnnotation,
			// Token: 0x04001E98 RID: 7832
			XsdInclude,
			// Token: 0x04001E99 RID: 7833
			XsdImport,
			// Token: 0x04001E9A RID: 7834
			XsdElement,
			// Token: 0x04001E9B RID: 7835
			XsdAttribute,
			// Token: 0x04001E9C RID: 7836
			xsdAttributeGroup,
			// Token: 0x04001E9D RID: 7837
			XsdAnyAttribute,
			// Token: 0x04001E9E RID: 7838
			XsdGroup,
			// Token: 0x04001E9F RID: 7839
			XsdAll,
			// Token: 0x04001EA0 RID: 7840
			XsdChoice,
			// Token: 0x04001EA1 RID: 7841
			XsdSequence,
			// Token: 0x04001EA2 RID: 7842
			XsdAny,
			// Token: 0x04001EA3 RID: 7843
			XsdNotation,
			// Token: 0x04001EA4 RID: 7844
			XsdSimpleType,
			// Token: 0x04001EA5 RID: 7845
			XsdComplexType,
			// Token: 0x04001EA6 RID: 7846
			XsdUnique,
			// Token: 0x04001EA7 RID: 7847
			XsdKey,
			// Token: 0x04001EA8 RID: 7848
			XsdKeyref,
			// Token: 0x04001EA9 RID: 7849
			XsdSelector,
			// Token: 0x04001EAA RID: 7850
			XsdField,
			// Token: 0x04001EAB RID: 7851
			XsdMinExclusive,
			// Token: 0x04001EAC RID: 7852
			XsdMinInclusive,
			// Token: 0x04001EAD RID: 7853
			XsdMaxExclusive,
			// Token: 0x04001EAE RID: 7854
			XsdMaxInclusive,
			// Token: 0x04001EAF RID: 7855
			XsdTotalDigits,
			// Token: 0x04001EB0 RID: 7856
			XsdFractionDigits,
			// Token: 0x04001EB1 RID: 7857
			XsdLength,
			// Token: 0x04001EB2 RID: 7858
			XsdMinLength,
			// Token: 0x04001EB3 RID: 7859
			XsdMaxLength,
			// Token: 0x04001EB4 RID: 7860
			XsdEnumeration,
			// Token: 0x04001EB5 RID: 7861
			XsdPattern,
			// Token: 0x04001EB6 RID: 7862
			XsdDocumentation,
			// Token: 0x04001EB7 RID: 7863
			XsdAppInfo,
			// Token: 0x04001EB8 RID: 7864
			XsdComplexContent,
			// Token: 0x04001EB9 RID: 7865
			XsdComplexContentExtension,
			// Token: 0x04001EBA RID: 7866
			XsdComplexContentRestriction,
			// Token: 0x04001EBB RID: 7867
			XsdSimpleContent,
			// Token: 0x04001EBC RID: 7868
			XsdSimpleContentExtension,
			// Token: 0x04001EBD RID: 7869
			XsdSimpleContentRestriction,
			// Token: 0x04001EBE RID: 7870
			XsdSimpleTypeList,
			// Token: 0x04001EBF RID: 7871
			XsdSimpleTypeRestriction,
			// Token: 0x04001EC0 RID: 7872
			XsdSimpleTypeUnion,
			// Token: 0x04001EC1 RID: 7873
			XsdWhitespace,
			// Token: 0x04001EC2 RID: 7874
			XsdRedefine,
			// Token: 0x04001EC3 RID: 7875
			SchemaItemType,
			// Token: 0x04001EC4 RID: 7876
			SchemaMemberTypes,
			// Token: 0x04001EC5 RID: 7877
			SchemaXPath,
			// Token: 0x04001EC6 RID: 7878
			XmlLang
		}
	}
}
