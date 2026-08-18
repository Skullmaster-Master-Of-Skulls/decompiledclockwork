using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003EB RID: 1003
	internal class XmlSerializationReaderMetadataSet : XmlSerializationReader
	{
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x00087B33 File Offset: 0x00085D33
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x00087B3B File Offset: 0x00085D3B
		public bool ProcessOuterElement
		{
			get
			{
				return this.processOuterElement;
			}
			set
			{
				this.processOuterElement = value;
			}
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x00087B44 File Offset: 0x00085D44
		public object Read68_Metadata()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (this.processOuterElement && (base.Reader.LocalName != this.id1_Metadata || base.Reader.NamespaceURI != this.id2_Item))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateUnknownNodeException());
				}
				result = this.Read67_MetadataSet(true, true);
			}
			else
			{
				base.UnknownNode(null, "http://schemas.xmlsoap.org/ws/2004/09/mex:Metadata");
			}
			return result;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x00087BC4 File Offset: 0x00085DC4
		private MetadataSet Read67_MetadataSet(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && this.processOuterElement && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_MetadataSet || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateUnknownTypeException(xmlQualifiedName));
			}
			if (flag)
			{
				return null;
			}
			MetadataSet metadataSet = new MetadataSet();
			Collection<MetadataSection> metadataSections = metadataSet.MetadataSections;
			Collection<XmlAttribute> attributes = metadataSet.Attributes;
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
					base.ParseWsdlArrayType(xmlAttribute);
					attributes.Add(xmlAttribute);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return metadataSet;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id4_MetadataSection && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (metadataSections == null)
						{
							base.Reader.Skip();
						}
						else
						{
							metadataSections.Add(this.Read66_MetadataSection(false, true));
						}
					}
					else
					{
						base.UnknownNode(metadataSet, "http://schemas.xmlsoap.org/ws/2004/09/mex:MetadataSection");
					}
				}
				else
				{
					base.UnknownNode(metadataSet, "http://schemas.xmlsoap.org/ws/2004/09/mex:MetadataSection");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return metadataSet;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x00087D90 File Offset: 0x00085F90
		private MetadataSection Read66_MetadataSection(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id4_MetadataSection || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateUnknownTypeException(xmlQualifiedName));
			}
			if (flag)
			{
				return null;
			}
			MetadataSection metadataSection = new MetadataSection();
			Collection<XmlAttribute> attributes = metadataSection.Attributes;
			bool[] array = new bool[4];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[1] && base.Reader.LocalName == this.id5_Dialect && base.Reader.NamespaceURI == this.id6_Item)
				{
					metadataSection.Dialect = base.Reader.Value;
					array[1] = true;
				}
				else if (!array[2] && base.Reader.LocalName == this.id7_Identifier && base.Reader.NamespaceURI == this.id6_Item)
				{
					metadataSection.Identifier = base.Reader.Value;
					array[2] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
					base.ParseWsdlArrayType(xmlAttribute);
					attributes.Add(xmlAttribute);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return metadataSection;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[3] && base.Reader.LocalName == this.id1_Metadata && base.Reader.NamespaceURI == this.id2_Item)
					{
						metadataSection.Metadata = this.Read67_MetadataSet(false, true);
						array[3] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id8_schema && base.Reader.NamespaceURI == this.id9_Item)
					{
						metadataSection.Metadata = XmlSchema.Read(base.Reader, null);
						if (base.Reader.NodeType == XmlNodeType.EndElement)
						{
							base.ReadEndElement();
						}
						array[3] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id10_definitions && base.Reader.NamespaceURI == this.id11_Item)
					{
						metadataSection.Metadata = ServiceDescription.Read(base.Reader);
						array[3] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id12_MetadataReference && base.Reader.NamespaceURI == this.id2_Item)
					{
						metadataSection.Metadata = (MetadataReference)base.ReadSerializable((IXmlSerializable)Activator.CreateInstance(typeof(MetadataReference), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], null));
						array[3] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id13_Location && base.Reader.NamespaceURI == this.id2_Item)
					{
						metadataSection.Metadata = this.Read65_MetadataLocation(false, true);
						array[3] = true;
					}
					else
					{
						metadataSection.Metadata = (XmlElement)base.ReadXmlNode(false);
					}
				}
				else
				{
					base.UnknownNode(metadataSection, "http://schemas.xmlsoap.org/ws/2004/09/mex:Metadata, http://www.w3.org/2001/XMLSchema:schema, http://schemas.xmlsoap.org/wsdl/:definitions, http://schemas.xmlsoap.org/ws/2004/09/mex:MetadataReference, http://schemas.xmlsoap.org/ws/2004/09/mex:Location");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return metadataSection;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0008812C File Offset: 0x0008632C
		private MetadataLocation Read65_MetadataLocation(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id14_MetadataLocation || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateUnknownTypeException(xmlQualifiedName));
			}
			if (flag)
			{
				return null;
			}
			MetadataLocation metadataLocation = new MetadataLocation();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(metadataLocation);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return metadataLocation;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				string text = null;
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(metadataLocation, "");
				}
				else if (base.Reader.NodeType == XmlNodeType.Text || base.Reader.NodeType == XmlNodeType.CDATA || base.Reader.NodeType == XmlNodeType.Whitespace || base.Reader.NodeType == XmlNodeType.SignificantWhitespace)
				{
					text = base.ReadString(text, false);
					metadataLocation.Location = text;
				}
				else
				{
					base.UnknownNode(metadataLocation, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return metadataLocation;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000882CA File Offset: 0x000864CA
		protected override void InitCallbacks()
		{
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000882CC File Offset: 0x000864CC
		protected override void InitIDs()
		{
			this.id60_documentation = base.Reader.NameTable.Add("documentation");
			this.id22_targetNamespace = base.Reader.NameTable.Add("targetNamespace");
			this.id10_definitions = base.Reader.NameTable.Add("definitions");
			this.id65_lang = base.Reader.NameTable.Add("lang");
			this.id31_attribute = base.Reader.NameTable.Add("attribute");
			this.id47_ref = base.Reader.NameTable.Add("ref");
			this.id4_MetadataSection = base.Reader.NameTable.Add("MetadataSection");
			this.id54_refer = base.Reader.NameTable.Add("refer");
			this.id83_union = base.Reader.NameTable.Add("union");
			this.id127_Item = base.Reader.NameTable.Add("XmlSchemaComplexContentRestriction");
			this.id53_XmlSchemaKeyref = base.Reader.NameTable.Add("XmlSchemaKeyref");
			this.id27_import = base.Reader.NameTable.Add("import");
			this.id75_all = base.Reader.NameTable.Add("all");
			this.id128_XmlSchemaSimpleContent = base.Reader.NameTable.Add("XmlSchemaSimpleContent");
			this.id139_XmlSchemaInclude = base.Reader.NameTable.Add("XmlSchemaInclude");
			this.id78_namespace = base.Reader.NameTable.Add("namespace");
			this.id18_attributeFormDefault = base.Reader.NameTable.Add("attributeFormDefault");
			this.id100_XmlSchemaFractionDigitsFacet = base.Reader.NameTable.Add("XmlSchemaFractionDigitsFacet");
			this.id32_attributeGroup = base.Reader.NameTable.Add("attributeGroup");
			this.id64_XmlSchemaDocumentation = base.Reader.NameTable.Add("XmlSchemaDocumentation");
			this.id93_maxLength = base.Reader.NameTable.Add("maxLength");
			this.id49_type = base.Reader.NameTable.Add("type");
			this.id86_XmlSchemaSimpleTypeRestriction = base.Reader.NameTable.Add("XmlSchemaSimpleTypeRestriction");
			this.id96_length = base.Reader.NameTable.Add("length");
			this.id104_XmlSchemaLengthFacet = base.Reader.NameTable.Add("XmlSchemaLengthFacet");
			this.id17_XmlSchema = base.Reader.NameTable.Add("XmlSchema");
			this.id134_public = base.Reader.NameTable.Add("public");
			this.id77_XmlSchemaAnyAttribute = base.Reader.NameTable.Add("XmlSchemaAnyAttribute");
			this.id24_id = base.Reader.NameTable.Add("id");
			this.id71_simpleContent = base.Reader.NameTable.Add("simpleContent");
			this.id51_key = base.Reader.NameTable.Add("key");
			this.id67_XmlSchemaKey = base.Reader.NameTable.Add("XmlSchemaKey");
			this.id80_XmlSchemaAttribute = base.Reader.NameTable.Add("XmlSchemaAttribute");
			this.id126_Item = base.Reader.NameTable.Add("XmlSchemaComplexContentExtension");
			this.id23_version = base.Reader.NameTable.Add("version");
			this.id121_XmlSchemaGroupRef = base.Reader.NameTable.Add("XmlSchemaGroupRef");
			this.id90_maxInclusive = base.Reader.NameTable.Add("maxInclusive");
			this.id116_memberTypes = base.Reader.NameTable.Add("memberTypes");
			this.id20_finalDefault = base.Reader.NameTable.Add("finalDefault");
			this.id120_any = base.Reader.NameTable.Add("any");
			this.id112_XmlSchemaMaxExclusiveFacet = base.Reader.NameTable.Add("XmlSchemaMaxExclusiveFacet");
			this.id15_EndpointReference = base.Reader.NameTable.Add("EndpointReference");
			this.id45_name = base.Reader.NameTable.Add("name");
			this.id122_XmlSchemaSequence = base.Reader.NameTable.Add("XmlSchemaSequence");
			this.id73_sequence = base.Reader.NameTable.Add("sequence");
			this.id82_XmlSchemaSimpleType = base.Reader.NameTable.Add("XmlSchemaSimpleType");
			this.id48_substitutionGroup = base.Reader.NameTable.Add("substitutionGroup");
			this.id111_XmlSchemaMinInclusiveFacet = base.Reader.NameTable.Add("XmlSchemaMinInclusiveFacet");
			this.id7_Identifier = base.Reader.NameTable.Add("Identifier");
			this.id113_XmlSchemaSimpleTypeList = base.Reader.NameTable.Add("XmlSchemaSimpleTypeList");
			this.id41_default = base.Reader.NameTable.Add("default");
			this.id125_extension = base.Reader.NameTable.Add("extension");
			this.id16_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/ws/2004/08/addressing");
			this.id1000_Item = base.Reader.NameTable.Add("http://www.w3.org/2005/08/addressing");
			this.id124_XmlSchemaComplexContent = base.Reader.NameTable.Add("XmlSchemaComplexContent");
			this.id72_complexContent = base.Reader.NameTable.Add("complexContent");
			this.id11_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/wsdl/");
			this.id25_include = base.Reader.NameTable.Add("include");
			this.id34_simpleType = base.Reader.NameTable.Add("simpleType");
			this.id91_minExclusive = base.Reader.NameTable.Add("minExclusive");
			this.id94_pattern = base.Reader.NameTable.Add("pattern");
			this.id2_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/ws/2004/09/mex");
			this.id95_enumeration = base.Reader.NameTable.Add("enumeration");
			this.id114_itemType = base.Reader.NameTable.Add("itemType");
			this.id115_XmlSchemaSimpleTypeUnion = base.Reader.NameTable.Add("XmlSchemaSimpleTypeUnion");
			this.id59_XmlSchemaAnnotation = base.Reader.NameTable.Add("XmlSchemaAnnotation");
			this.id28_notation = base.Reader.NameTable.Add("notation");
			this.id84_list = base.Reader.NameTable.Add("list");
			this.id39_abstract = base.Reader.NameTable.Add("abstract");
			this.id103_XmlSchemaWhiteSpaceFacet = base.Reader.NameTable.Add("XmlSchemaWhiteSpaceFacet");
			this.id110_XmlSchemaMaxInclusiveFacet = base.Reader.NameTable.Add("XmlSchemaMaxInclusiveFacet");
			this.id55_selector = base.Reader.NameTable.Add("selector");
			this.id43_fixed = base.Reader.NameTable.Add("fixed");
			this.id57_XmlSchemaXPath = base.Reader.NameTable.Add("XmlSchemaXPath");
			this.id118_XmlSchemaAll = base.Reader.NameTable.Add("XmlSchemaAll");
			this.id56_field = base.Reader.NameTable.Add("field");
			this.id119_XmlSchemaChoice = base.Reader.NameTable.Add("XmlSchemaChoice");
			this.id123_XmlSchemaAny = base.Reader.NameTable.Add("XmlSchemaAny");
			this.id132_XmlSchemaGroup = base.Reader.NameTable.Add("XmlSchemaGroup");
			this.id35_element = base.Reader.NameTable.Add("element");
			this.id129_Item = base.Reader.NameTable.Add("XmlSchemaSimpleContentExtension");
			this.id30_annotation = base.Reader.NameTable.Add("annotation");
			this.id44_form = base.Reader.NameTable.Add("form");
			this.id21_elementFormDefault = base.Reader.NameTable.Add("elementFormDefault");
			this.id98_totalDigits = base.Reader.NameTable.Add("totalDigits");
			this.id88_maxExclusive = base.Reader.NameTable.Add("maxExclusive");
			this.id42_final = base.Reader.NameTable.Add("final");
			this.id46_nillable = base.Reader.NameTable.Add("nillable");
			this.id9_Item = base.Reader.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.id61_appinfo = base.Reader.NameTable.Add("appinfo");
			this.id38_maxOccurs = base.Reader.NameTable.Add("maxOccurs");
			this.id70_mixed = base.Reader.NameTable.Add("mixed");
			this.id87_base = base.Reader.NameTable.Add("base");
			this.id13_Location = base.Reader.NameTable.Add("Location");
			this.id12_MetadataReference = base.Reader.NameTable.Add("MetadataReference");
			this.id97_whiteSpace = base.Reader.NameTable.Add("whiteSpace");
			this.id29_group = base.Reader.NameTable.Add("group");
			this.id92_minLength = base.Reader.NameTable.Add("minLength");
			this.id99_fractionDigits = base.Reader.NameTable.Add("fractionDigits");
			this.id137_schemaLocation = base.Reader.NameTable.Add("schemaLocation");
			this.id26_redefine = base.Reader.NameTable.Add("redefine");
			this.id101_value = base.Reader.NameTable.Add("value");
			this.id63_source = base.Reader.NameTable.Add("source");
			this.id89_minInclusive = base.Reader.NameTable.Add("minInclusive");
			this.id133_XmlSchemaNotation = base.Reader.NameTable.Add("XmlSchemaNotation");
			this.id52_keyref = base.Reader.NameTable.Add("keyref");
			this.id33_complexType = base.Reader.NameTable.Add("complexType");
			this.id135_system = base.Reader.NameTable.Add("system");
			this.id50_unique = base.Reader.NameTable.Add("unique");
			this.id74_choice = base.Reader.NameTable.Add("choice");
			this.id66_Item = base.Reader.NameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.id105_XmlSchemaEnumerationFacet = base.Reader.NameTable.Add("XmlSchemaEnumerationFacet");
			this.id107_XmlSchemaMaxLengthFacet = base.Reader.NameTable.Add("XmlSchemaMaxLengthFacet");
			this.id36_XmlSchemaElement = base.Reader.NameTable.Add("XmlSchemaElement");
			this.id106_XmlSchemaPatternFacet = base.Reader.NameTable.Add("XmlSchemaPatternFacet");
			this.id37_minOccurs = base.Reader.NameTable.Add("minOccurs");
			this.id130_Item = base.Reader.NameTable.Add("XmlSchemaSimpleContentRestriction");
			this.id68_XmlSchemaUnique = base.Reader.NameTable.Add("XmlSchemaUnique");
			this.id131_XmlSchemaAttributeGroup = base.Reader.NameTable.Add("XmlSchemaAttributeGroup");
			this.id40_block = base.Reader.NameTable.Add("block");
			this.id81_use = base.Reader.NameTable.Add("use");
			this.id85_restriction = base.Reader.NameTable.Add("restriction");
			this.id1_Metadata = base.Reader.NameTable.Add("Metadata");
			this.id69_XmlSchemaComplexType = base.Reader.NameTable.Add("XmlSchemaComplexType");
			this.id117_XmlSchemaAttributeGroupRef = base.Reader.NameTable.Add("XmlSchemaAttributeGroupRef");
			this.id138_XmlSchemaRedefine = base.Reader.NameTable.Add("XmlSchemaRedefine");
			this.id6_Item = base.Reader.NameTable.Add("");
			this.id102_XmlSchemaTotalDigitsFacet = base.Reader.NameTable.Add("XmlSchemaTotalDigitsFacet");
			this.id58_xpath = base.Reader.NameTable.Add("xpath");
			this.id5_Dialect = base.Reader.NameTable.Add("Dialect");
			this.id14_MetadataLocation = base.Reader.NameTable.Add("MetadataLocation");
			this.id3_MetadataSet = base.Reader.NameTable.Add("MetadataSet");
			this.id79_processContents = base.Reader.NameTable.Add("processContents");
			this.id76_anyAttribute = base.Reader.NameTable.Add("anyAttribute");
			this.id19_blockDefault = base.Reader.NameTable.Add("blockDefault");
			this.id136_XmlSchemaImport = base.Reader.NameTable.Add("XmlSchemaImport");
			this.id109_XmlSchemaMinExclusiveFacet = base.Reader.NameTable.Add("XmlSchemaMinExclusiveFacet");
			this.id108_XmlSchemaMinLengthFacet = base.Reader.NameTable.Add("XmlSchemaMinLengthFacet");
			this.id8_schema = base.Reader.NameTable.Add("schema");
			this.id62_XmlSchemaAppInfo = base.Reader.NameTable.Add("XmlSchemaAppInfo");
		}

		// Token: 0x040020D9 RID: 8409
		private bool processOuterElement = true;

		// Token: 0x040020DA RID: 8410
		private string id60_documentation;

		// Token: 0x040020DB RID: 8411
		private string id22_targetNamespace;

		// Token: 0x040020DC RID: 8412
		private string id10_definitions;

		// Token: 0x040020DD RID: 8413
		private string id65_lang;

		// Token: 0x040020DE RID: 8414
		private string id31_attribute;

		// Token: 0x040020DF RID: 8415
		private string id47_ref;

		// Token: 0x040020E0 RID: 8416
		private string id4_MetadataSection;

		// Token: 0x040020E1 RID: 8417
		private string id54_refer;

		// Token: 0x040020E2 RID: 8418
		private string id83_union;

		// Token: 0x040020E3 RID: 8419
		private string id127_Item;

		// Token: 0x040020E4 RID: 8420
		private string id53_XmlSchemaKeyref;

		// Token: 0x040020E5 RID: 8421
		private string id27_import;

		// Token: 0x040020E6 RID: 8422
		private string id75_all;

		// Token: 0x040020E7 RID: 8423
		private string id128_XmlSchemaSimpleContent;

		// Token: 0x040020E8 RID: 8424
		private string id139_XmlSchemaInclude;

		// Token: 0x040020E9 RID: 8425
		private string id78_namespace;

		// Token: 0x040020EA RID: 8426
		private string id18_attributeFormDefault;

		// Token: 0x040020EB RID: 8427
		private string id100_XmlSchemaFractionDigitsFacet;

		// Token: 0x040020EC RID: 8428
		private string id32_attributeGroup;

		// Token: 0x040020ED RID: 8429
		private string id64_XmlSchemaDocumentation;

		// Token: 0x040020EE RID: 8430
		private string id93_maxLength;

		// Token: 0x040020EF RID: 8431
		private string id49_type;

		// Token: 0x040020F0 RID: 8432
		private string id86_XmlSchemaSimpleTypeRestriction;

		// Token: 0x040020F1 RID: 8433
		private string id96_length;

		// Token: 0x040020F2 RID: 8434
		private string id104_XmlSchemaLengthFacet;

		// Token: 0x040020F3 RID: 8435
		private string id17_XmlSchema;

		// Token: 0x040020F4 RID: 8436
		private string id134_public;

		// Token: 0x040020F5 RID: 8437
		private string id77_XmlSchemaAnyAttribute;

		// Token: 0x040020F6 RID: 8438
		private string id24_id;

		// Token: 0x040020F7 RID: 8439
		private string id71_simpleContent;

		// Token: 0x040020F8 RID: 8440
		private string id51_key;

		// Token: 0x040020F9 RID: 8441
		private string id67_XmlSchemaKey;

		// Token: 0x040020FA RID: 8442
		private string id80_XmlSchemaAttribute;

		// Token: 0x040020FB RID: 8443
		private string id126_Item;

		// Token: 0x040020FC RID: 8444
		private string id23_version;

		// Token: 0x040020FD RID: 8445
		private string id121_XmlSchemaGroupRef;

		// Token: 0x040020FE RID: 8446
		private string id90_maxInclusive;

		// Token: 0x040020FF RID: 8447
		private string id116_memberTypes;

		// Token: 0x04002100 RID: 8448
		private string id20_finalDefault;

		// Token: 0x04002101 RID: 8449
		private string id120_any;

		// Token: 0x04002102 RID: 8450
		private string id112_XmlSchemaMaxExclusiveFacet;

		// Token: 0x04002103 RID: 8451
		private string id15_EndpointReference;

		// Token: 0x04002104 RID: 8452
		private string id45_name;

		// Token: 0x04002105 RID: 8453
		private string id122_XmlSchemaSequence;

		// Token: 0x04002106 RID: 8454
		private string id73_sequence;

		// Token: 0x04002107 RID: 8455
		private string id82_XmlSchemaSimpleType;

		// Token: 0x04002108 RID: 8456
		private string id48_substitutionGroup;

		// Token: 0x04002109 RID: 8457
		private string id111_XmlSchemaMinInclusiveFacet;

		// Token: 0x0400210A RID: 8458
		private string id7_Identifier;

		// Token: 0x0400210B RID: 8459
		private string id113_XmlSchemaSimpleTypeList;

		// Token: 0x0400210C RID: 8460
		private string id41_default;

		// Token: 0x0400210D RID: 8461
		private string id125_extension;

		// Token: 0x0400210E RID: 8462
		private string id16_Item;

		// Token: 0x0400210F RID: 8463
		private string id1000_Item;

		// Token: 0x04002110 RID: 8464
		private string id124_XmlSchemaComplexContent;

		// Token: 0x04002111 RID: 8465
		private string id72_complexContent;

		// Token: 0x04002112 RID: 8466
		private string id11_Item;

		// Token: 0x04002113 RID: 8467
		private string id25_include;

		// Token: 0x04002114 RID: 8468
		private string id34_simpleType;

		// Token: 0x04002115 RID: 8469
		private string id91_minExclusive;

		// Token: 0x04002116 RID: 8470
		private string id94_pattern;

		// Token: 0x04002117 RID: 8471
		private string id2_Item;

		// Token: 0x04002118 RID: 8472
		private string id95_enumeration;

		// Token: 0x04002119 RID: 8473
		private string id114_itemType;

		// Token: 0x0400211A RID: 8474
		private string id115_XmlSchemaSimpleTypeUnion;

		// Token: 0x0400211B RID: 8475
		private string id59_XmlSchemaAnnotation;

		// Token: 0x0400211C RID: 8476
		private string id28_notation;

		// Token: 0x0400211D RID: 8477
		private string id84_list;

		// Token: 0x0400211E RID: 8478
		private string id39_abstract;

		// Token: 0x0400211F RID: 8479
		private string id103_XmlSchemaWhiteSpaceFacet;

		// Token: 0x04002120 RID: 8480
		private string id110_XmlSchemaMaxInclusiveFacet;

		// Token: 0x04002121 RID: 8481
		private string id55_selector;

		// Token: 0x04002122 RID: 8482
		private string id43_fixed;

		// Token: 0x04002123 RID: 8483
		private string id57_XmlSchemaXPath;

		// Token: 0x04002124 RID: 8484
		private string id118_XmlSchemaAll;

		// Token: 0x04002125 RID: 8485
		private string id56_field;

		// Token: 0x04002126 RID: 8486
		private string id119_XmlSchemaChoice;

		// Token: 0x04002127 RID: 8487
		private string id123_XmlSchemaAny;

		// Token: 0x04002128 RID: 8488
		private string id132_XmlSchemaGroup;

		// Token: 0x04002129 RID: 8489
		private string id35_element;

		// Token: 0x0400212A RID: 8490
		private string id129_Item;

		// Token: 0x0400212B RID: 8491
		private string id30_annotation;

		// Token: 0x0400212C RID: 8492
		private string id44_form;

		// Token: 0x0400212D RID: 8493
		private string id21_elementFormDefault;

		// Token: 0x0400212E RID: 8494
		private string id98_totalDigits;

		// Token: 0x0400212F RID: 8495
		private string id88_maxExclusive;

		// Token: 0x04002130 RID: 8496
		private string id42_final;

		// Token: 0x04002131 RID: 8497
		private string id46_nillable;

		// Token: 0x04002132 RID: 8498
		private string id9_Item;

		// Token: 0x04002133 RID: 8499
		private string id61_appinfo;

		// Token: 0x04002134 RID: 8500
		private string id38_maxOccurs;

		// Token: 0x04002135 RID: 8501
		private string id70_mixed;

		// Token: 0x04002136 RID: 8502
		private string id87_base;

		// Token: 0x04002137 RID: 8503
		private string id13_Location;

		// Token: 0x04002138 RID: 8504
		private string id12_MetadataReference;

		// Token: 0x04002139 RID: 8505
		private string id97_whiteSpace;

		// Token: 0x0400213A RID: 8506
		private string id29_group;

		// Token: 0x0400213B RID: 8507
		private string id92_minLength;

		// Token: 0x0400213C RID: 8508
		private string id99_fractionDigits;

		// Token: 0x0400213D RID: 8509
		private string id137_schemaLocation;

		// Token: 0x0400213E RID: 8510
		private string id26_redefine;

		// Token: 0x0400213F RID: 8511
		private string id101_value;

		// Token: 0x04002140 RID: 8512
		private string id63_source;

		// Token: 0x04002141 RID: 8513
		private string id89_minInclusive;

		// Token: 0x04002142 RID: 8514
		private string id133_XmlSchemaNotation;

		// Token: 0x04002143 RID: 8515
		private string id52_keyref;

		// Token: 0x04002144 RID: 8516
		private string id33_complexType;

		// Token: 0x04002145 RID: 8517
		private string id135_system;

		// Token: 0x04002146 RID: 8518
		private string id50_unique;

		// Token: 0x04002147 RID: 8519
		private string id74_choice;

		// Token: 0x04002148 RID: 8520
		private string id66_Item;

		// Token: 0x04002149 RID: 8521
		private string id105_XmlSchemaEnumerationFacet;

		// Token: 0x0400214A RID: 8522
		private string id107_XmlSchemaMaxLengthFacet;

		// Token: 0x0400214B RID: 8523
		private string id36_XmlSchemaElement;

		// Token: 0x0400214C RID: 8524
		private string id106_XmlSchemaPatternFacet;

		// Token: 0x0400214D RID: 8525
		private string id37_minOccurs;

		// Token: 0x0400214E RID: 8526
		private string id130_Item;

		// Token: 0x0400214F RID: 8527
		private string id68_XmlSchemaUnique;

		// Token: 0x04002150 RID: 8528
		private string id131_XmlSchemaAttributeGroup;

		// Token: 0x04002151 RID: 8529
		private string id40_block;

		// Token: 0x04002152 RID: 8530
		private string id81_use;

		// Token: 0x04002153 RID: 8531
		private string id85_restriction;

		// Token: 0x04002154 RID: 8532
		private string id1_Metadata;

		// Token: 0x04002155 RID: 8533
		private string id69_XmlSchemaComplexType;

		// Token: 0x04002156 RID: 8534
		private string id117_XmlSchemaAttributeGroupRef;

		// Token: 0x04002157 RID: 8535
		private string id138_XmlSchemaRedefine;

		// Token: 0x04002158 RID: 8536
		private string id6_Item;

		// Token: 0x04002159 RID: 8537
		private string id102_XmlSchemaTotalDigitsFacet;

		// Token: 0x0400215A RID: 8538
		private string id58_xpath;

		// Token: 0x0400215B RID: 8539
		private string id5_Dialect;

		// Token: 0x0400215C RID: 8540
		private string id14_MetadataLocation;

		// Token: 0x0400215D RID: 8541
		private string id3_MetadataSet;

		// Token: 0x0400215E RID: 8542
		private string id79_processContents;

		// Token: 0x0400215F RID: 8543
		private string id76_anyAttribute;

		// Token: 0x04002160 RID: 8544
		private string id19_blockDefault;

		// Token: 0x04002161 RID: 8545
		private string id136_XmlSchemaImport;

		// Token: 0x04002162 RID: 8546
		private string id109_XmlSchemaMinExclusiveFacet;

		// Token: 0x04002163 RID: 8547
		private string id108_XmlSchemaMinLengthFacet;

		// Token: 0x04002164 RID: 8548
		private string id8_schema;

		// Token: 0x04002165 RID: 8549
		private string id62_XmlSchemaAppInfo;
	}
}
