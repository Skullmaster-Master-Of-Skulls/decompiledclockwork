using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000204 RID: 516
	internal abstract class DatatypeImplementation : XmlSchemaDatatype
	{
		// Token: 0x06002116 RID: 8470 RVA: 0x000B4AA0 File Offset: 0x000B2CA0
		static DatatypeImplementation()
		{
			DatatypeImplementation[] array = new DatatypeImplementation[13];
			array[0] = DatatypeImplementation.c_string;
			array[1] = DatatypeImplementation.c_ID;
			array[2] = DatatypeImplementation.c_IDREF;
			array[3] = DatatypeImplementation.c_IDREFS;
			array[4] = DatatypeImplementation.c_ENTITY;
			array[5] = DatatypeImplementation.c_ENTITIES;
			array[6] = DatatypeImplementation.c_NMTOKEN;
			array[7] = DatatypeImplementation.c_NMTOKENS;
			array[8] = DatatypeImplementation.c_NOTATION;
			array[9] = DatatypeImplementation.c_ENUMERATION;
			array[10] = DatatypeImplementation.c_QNameXdr;
			array[11] = DatatypeImplementation.c_NCName;
			DatatypeImplementation.c_tokenizedTypes = array;
			DatatypeImplementation[] array2 = new DatatypeImplementation[13];
			array2[0] = DatatypeImplementation.c_string;
			array2[1] = DatatypeImplementation.c_ID;
			array2[2] = DatatypeImplementation.c_IDREF;
			array2[3] = DatatypeImplementation.c_IDREFS;
			array2[4] = DatatypeImplementation.c_ENTITY;
			array2[5] = DatatypeImplementation.c_ENTITIES;
			array2[6] = DatatypeImplementation.c_NMTOKEN;
			array2[7] = DatatypeImplementation.c_NMTOKENS;
			array2[8] = DatatypeImplementation.c_NOTATION;
			array2[9] = DatatypeImplementation.c_ENUMERATION;
			array2[10] = DatatypeImplementation.c_QName;
			array2[11] = DatatypeImplementation.c_NCName;
			DatatypeImplementation.c_tokenizedTypesXsd = array2;
			DatatypeImplementation.c_XdrTypes = new DatatypeImplementation.SchemaDatatypeMap[]
			{
				new DatatypeImplementation.SchemaDatatypeMap("bin.base64", DatatypeImplementation.c_base64Binary),
				new DatatypeImplementation.SchemaDatatypeMap("bin.hex", DatatypeImplementation.c_hexBinary),
				new DatatypeImplementation.SchemaDatatypeMap("boolean", DatatypeImplementation.c_boolean),
				new DatatypeImplementation.SchemaDatatypeMap("char", DatatypeImplementation.c_char),
				new DatatypeImplementation.SchemaDatatypeMap("date", DatatypeImplementation.c_date),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime", DatatypeImplementation.c_dateTimeNoTz),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime.tz", DatatypeImplementation.c_dateTimeTz),
				new DatatypeImplementation.SchemaDatatypeMap("decimal", DatatypeImplementation.c_decimal),
				new DatatypeImplementation.SchemaDatatypeMap("entities", DatatypeImplementation.c_ENTITIES),
				new DatatypeImplementation.SchemaDatatypeMap("entity", DatatypeImplementation.c_ENTITY),
				new DatatypeImplementation.SchemaDatatypeMap("enumeration", DatatypeImplementation.c_ENUMERATION),
				new DatatypeImplementation.SchemaDatatypeMap("fixed.14.4", DatatypeImplementation.c_fixed),
				new DatatypeImplementation.SchemaDatatypeMap("float", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("float.ieee.754.32", DatatypeImplementation.c_floatXdr),
				new DatatypeImplementation.SchemaDatatypeMap("float.ieee.754.64", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("i1", DatatypeImplementation.c_byte),
				new DatatypeImplementation.SchemaDatatypeMap("i2", DatatypeImplementation.c_short),
				new DatatypeImplementation.SchemaDatatypeMap("i4", DatatypeImplementation.c_int),
				new DatatypeImplementation.SchemaDatatypeMap("i8", DatatypeImplementation.c_long),
				new DatatypeImplementation.SchemaDatatypeMap("id", DatatypeImplementation.c_ID),
				new DatatypeImplementation.SchemaDatatypeMap("idref", DatatypeImplementation.c_IDREF),
				new DatatypeImplementation.SchemaDatatypeMap("idrefs", DatatypeImplementation.c_IDREFS),
				new DatatypeImplementation.SchemaDatatypeMap("int", DatatypeImplementation.c_int),
				new DatatypeImplementation.SchemaDatatypeMap("nmtoken", DatatypeImplementation.c_NMTOKEN),
				new DatatypeImplementation.SchemaDatatypeMap("nmtokens", DatatypeImplementation.c_NMTOKENS),
				new DatatypeImplementation.SchemaDatatypeMap("notation", DatatypeImplementation.c_NOTATION),
				new DatatypeImplementation.SchemaDatatypeMap("number", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("r4", DatatypeImplementation.c_floatXdr),
				new DatatypeImplementation.SchemaDatatypeMap("r8", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("string", DatatypeImplementation.c_string),
				new DatatypeImplementation.SchemaDatatypeMap("time", DatatypeImplementation.c_timeNoTz),
				new DatatypeImplementation.SchemaDatatypeMap("time.tz", DatatypeImplementation.c_timeTz),
				new DatatypeImplementation.SchemaDatatypeMap("ui1", DatatypeImplementation.c_unsignedByte),
				new DatatypeImplementation.SchemaDatatypeMap("ui2", DatatypeImplementation.c_unsignedShort),
				new DatatypeImplementation.SchemaDatatypeMap("ui4", DatatypeImplementation.c_unsignedInt),
				new DatatypeImplementation.SchemaDatatypeMap("ui8", DatatypeImplementation.c_unsignedLong),
				new DatatypeImplementation.SchemaDatatypeMap("uri", DatatypeImplementation.c_anyURI),
				new DatatypeImplementation.SchemaDatatypeMap("uuid", DatatypeImplementation.c_uuid)
			};
			DatatypeImplementation.c_XsdTypes = new DatatypeImplementation.SchemaDatatypeMap[]
			{
				new DatatypeImplementation.SchemaDatatypeMap("ENTITIES", DatatypeImplementation.c_ENTITIES, 11),
				new DatatypeImplementation.SchemaDatatypeMap("ENTITY", DatatypeImplementation.c_ENTITY, 11),
				new DatatypeImplementation.SchemaDatatypeMap("ID", DatatypeImplementation.c_ID, 5),
				new DatatypeImplementation.SchemaDatatypeMap("IDREF", DatatypeImplementation.c_IDREF, 5),
				new DatatypeImplementation.SchemaDatatypeMap("IDREFS", DatatypeImplementation.c_IDREFS, 11),
				new DatatypeImplementation.SchemaDatatypeMap("NCName", DatatypeImplementation.c_NCName, 9),
				new DatatypeImplementation.SchemaDatatypeMap("NMTOKEN", DatatypeImplementation.c_NMTOKEN, 40),
				new DatatypeImplementation.SchemaDatatypeMap("NMTOKENS", DatatypeImplementation.c_NMTOKENS, 11),
				new DatatypeImplementation.SchemaDatatypeMap("NOTATION", DatatypeImplementation.c_NOTATION, 11),
				new DatatypeImplementation.SchemaDatatypeMap("Name", DatatypeImplementation.c_Name, 40),
				new DatatypeImplementation.SchemaDatatypeMap("QName", DatatypeImplementation.c_QName, 11),
				new DatatypeImplementation.SchemaDatatypeMap("anySimpleType", DatatypeImplementation.c_anySimpleType, -1),
				new DatatypeImplementation.SchemaDatatypeMap("anyURI", DatatypeImplementation.c_anyURI, 11),
				new DatatypeImplementation.SchemaDatatypeMap("base64Binary", DatatypeImplementation.c_base64Binary, 11),
				new DatatypeImplementation.SchemaDatatypeMap("boolean", DatatypeImplementation.c_boolean, 11),
				new DatatypeImplementation.SchemaDatatypeMap("byte", DatatypeImplementation.c_byte, 37),
				new DatatypeImplementation.SchemaDatatypeMap("date", DatatypeImplementation.c_date, 11),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime", DatatypeImplementation.c_dateTime, 11),
				new DatatypeImplementation.SchemaDatatypeMap("decimal", DatatypeImplementation.c_decimal, 11),
				new DatatypeImplementation.SchemaDatatypeMap("double", DatatypeImplementation.c_double, 11),
				new DatatypeImplementation.SchemaDatatypeMap("duration", DatatypeImplementation.c_duration, 11),
				new DatatypeImplementation.SchemaDatatypeMap("float", DatatypeImplementation.c_float, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gDay", DatatypeImplementation.c_day, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gMonth", DatatypeImplementation.c_month, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gMonthDay", DatatypeImplementation.c_monthDay, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gYear", DatatypeImplementation.c_year, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gYearMonth", DatatypeImplementation.c_yearMonth, 11),
				new DatatypeImplementation.SchemaDatatypeMap("hexBinary", DatatypeImplementation.c_hexBinary, 11),
				new DatatypeImplementation.SchemaDatatypeMap("int", DatatypeImplementation.c_int, 31),
				new DatatypeImplementation.SchemaDatatypeMap("integer", DatatypeImplementation.c_integer, 18),
				new DatatypeImplementation.SchemaDatatypeMap("language", DatatypeImplementation.c_language, 40),
				new DatatypeImplementation.SchemaDatatypeMap("long", DatatypeImplementation.c_long, 29),
				new DatatypeImplementation.SchemaDatatypeMap("negativeInteger", DatatypeImplementation.c_negativeInteger, 34),
				new DatatypeImplementation.SchemaDatatypeMap("nonNegativeInteger", DatatypeImplementation.c_nonNegativeInteger, 29),
				new DatatypeImplementation.SchemaDatatypeMap("nonPositiveInteger", DatatypeImplementation.c_nonPositiveInteger, 29),
				new DatatypeImplementation.SchemaDatatypeMap("normalizedString", DatatypeImplementation.c_normalizedString, 38),
				new DatatypeImplementation.SchemaDatatypeMap("positiveInteger", DatatypeImplementation.c_positiveInteger, 33),
				new DatatypeImplementation.SchemaDatatypeMap("short", DatatypeImplementation.c_short, 28),
				new DatatypeImplementation.SchemaDatatypeMap("string", DatatypeImplementation.c_string, 11),
				new DatatypeImplementation.SchemaDatatypeMap("time", DatatypeImplementation.c_time, 11),
				new DatatypeImplementation.SchemaDatatypeMap("token", DatatypeImplementation.c_token, 35),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedByte", DatatypeImplementation.c_unsignedByte, 44),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedInt", DatatypeImplementation.c_unsignedInt, 43),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedLong", DatatypeImplementation.c_unsignedLong, 33),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedShort", DatatypeImplementation.c_unsignedShort, 42)
			};
			DatatypeImplementation.CreateBuiltinTypes();
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000B553E File Offset: 0x000B373E
		internal static XmlSchemaSimpleType AnySimpleType
		{
			get
			{
				return DatatypeImplementation.anySimpleType;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x000B5545 File Offset: 0x000B3745
		internal static XmlSchemaSimpleType AnyAtomicType
		{
			get
			{
				return DatatypeImplementation.anyAtomicType;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000B554C File Offset: 0x000B374C
		internal static XmlSchemaSimpleType UntypedAtomicType
		{
			get
			{
				return DatatypeImplementation.untypedAtomicType;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000B5553 File Offset: 0x000B3753
		internal static XmlSchemaSimpleType YearMonthDurationType
		{
			get
			{
				return DatatypeImplementation.yearMonthDurationType;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x000B555A File Offset: 0x000B375A
		internal static XmlSchemaSimpleType DayTimeDurationType
		{
			get
			{
				return DatatypeImplementation.dayTimeDurationType;
			}
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000B5561 File Offset: 0x000B3761
		internal new static DatatypeImplementation FromXmlTokenizedType(XmlTokenizedType token)
		{
			return DatatypeImplementation.c_tokenizedTypes[(int)token];
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000B556A File Offset: 0x000B376A
		internal new static DatatypeImplementation FromXmlTokenizedTypeXsd(XmlTokenizedType token)
		{
			return DatatypeImplementation.c_tokenizedTypesXsd[(int)token];
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000B5574 File Offset: 0x000B3774
		internal new static DatatypeImplementation FromXdrName(string name)
		{
			int num = Array.BinarySearch(DatatypeImplementation.c_XdrTypes, name, null);
			if (num >= 0)
			{
				return (DatatypeImplementation)DatatypeImplementation.c_XdrTypes[num];
			}
			return null;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000B55A0 File Offset: 0x000B37A0
		private static DatatypeImplementation FromTypeName(string name)
		{
			int num = Array.BinarySearch(DatatypeImplementation.c_XsdTypes, name, null);
			if (num >= 0)
			{
				return (DatatypeImplementation)DatatypeImplementation.c_XsdTypes[num];
			}
			return null;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000B55CC File Offset: 0x000B37CC
		internal static XmlSchemaSimpleType StartBuiltinType(XmlQualifiedName qname, XmlSchemaDatatype dataType)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.SetQualifiedName(qname);
			xmlSchemaSimpleType.SetDatatype(dataType);
			xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(dataType);
			xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
			return xmlSchemaSimpleType;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000B5608 File Offset: 0x000B3808
		internal static void FinishBuiltinType(XmlSchemaSimpleType derivedType, XmlSchemaSimpleType baseType)
		{
			derivedType.SetBaseSchemaType(baseType);
			derivedType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
			if (derivedType.Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
			{
				derivedType.Content = new XmlSchemaSimpleTypeRestriction
				{
					BaseTypeName = baseType.QualifiedName
				};
			}
			if (derivedType.Datatype.Variety == XmlSchemaDatatypeVariety.List)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
				derivedType.SetDerivedBy(XmlSchemaDerivationMethod.List);
				XmlTypeCode typeCode = derivedType.Datatype.TypeCode;
				if (typeCode != XmlTypeCode.NmToken)
				{
					if (typeCode != XmlTypeCode.Idref)
					{
						if (typeCode == XmlTypeCode.Entity)
						{
							xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[39]);
						}
					}
					else
					{
						xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[38]);
					}
				}
				else
				{
					xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[34]);
				}
				derivedType.Content = xmlSchemaSimpleTypeList;
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000B56D4 File Offset: 0x000B38D4
		internal static void CreateBuiltinTypes()
		{
			DatatypeImplementation.SchemaDatatypeMap schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[11];
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema");
			DatatypeImplementation datatypeImplementation = DatatypeImplementation.FromTypeName(xmlQualifiedName.Name);
			DatatypeImplementation.anySimpleType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, datatypeImplementation);
			datatypeImplementation.parentSchemaType = DatatypeImplementation.anySimpleType;
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.anySimpleType);
			for (int i = 0; i < DatatypeImplementation.c_XsdTypes.Length; i++)
			{
				if (i != 11)
				{
					schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[i];
					xmlQualifiedName = new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema");
					datatypeImplementation = DatatypeImplementation.FromTypeName(xmlQualifiedName.Name);
					XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, datatypeImplementation);
					datatypeImplementation.parentSchemaType = xmlSchemaSimpleType;
					DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, xmlSchemaSimpleType);
					if (datatypeImplementation.variety == XmlSchemaDatatypeVariety.Atomic)
					{
						DatatypeImplementation.enumToTypeCode[(int)datatypeImplementation.TypeCode] = xmlSchemaSimpleType;
					}
				}
			}
			for (int j = 0; j < DatatypeImplementation.c_XsdTypes.Length; j++)
			{
				if (j != 11)
				{
					schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[j];
					XmlSchemaSimpleType derivedType = (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema")];
					if (schemaDatatypeMap.ParentIndex == 11)
					{
						DatatypeImplementation.FinishBuiltinType(derivedType, DatatypeImplementation.anySimpleType);
					}
					else
					{
						XmlSchemaSimpleType xmlSchemaSimpleType2 = (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[new XmlQualifiedName(DatatypeImplementation.c_XsdTypes[schemaDatatypeMap.ParentIndex].Name, "http://www.w3.org/2001/XMLSchema")];
						DatatypeImplementation.FinishBuiltinType(derivedType, xmlSchemaSimpleType2);
					}
				}
			}
			xmlQualifiedName = new XmlQualifiedName("anyAtomicType", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.anyAtomicType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_anyAtomicType);
			DatatypeImplementation.c_anyAtomicType.parentSchemaType = DatatypeImplementation.anyAtomicType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.anyAtomicType, DatatypeImplementation.anySimpleType);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.anyAtomicType);
			DatatypeImplementation.enumToTypeCode[10] = DatatypeImplementation.anyAtomicType;
			xmlQualifiedName = new XmlQualifiedName("untypedAtomic", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.untypedAtomicType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_untypedAtomicType);
			DatatypeImplementation.c_untypedAtomicType.parentSchemaType = DatatypeImplementation.untypedAtomicType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.untypedAtomicType, DatatypeImplementation.anyAtomicType);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.untypedAtomicType);
			DatatypeImplementation.enumToTypeCode[11] = DatatypeImplementation.untypedAtomicType;
			xmlQualifiedName = new XmlQualifiedName("yearMonthDuration", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.yearMonthDurationType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_yearMonthDuration);
			DatatypeImplementation.c_yearMonthDuration.parentSchemaType = DatatypeImplementation.yearMonthDurationType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.yearMonthDurationType, DatatypeImplementation.enumToTypeCode[17]);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.yearMonthDurationType);
			DatatypeImplementation.enumToTypeCode[53] = DatatypeImplementation.yearMonthDurationType;
			xmlQualifiedName = new XmlQualifiedName("dayTimeDuration", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.dayTimeDurationType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_dayTimeDuration);
			DatatypeImplementation.c_dayTimeDuration.parentSchemaType = DatatypeImplementation.dayTimeDurationType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.dayTimeDurationType, DatatypeImplementation.enumToTypeCode[17]);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.dayTimeDurationType);
			DatatypeImplementation.enumToTypeCode[54] = DatatypeImplementation.dayTimeDurationType;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000B59AB File Offset: 0x000B3BAB
		internal static XmlSchemaSimpleType GetSimpleTypeFromTypeCode(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.enumToTypeCode[(int)typeCode];
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000B59B4 File Offset: 0x000B3BB4
		internal static XmlSchemaSimpleType GetSimpleTypeFromXsdType(XmlQualifiedName qname)
		{
			return (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[qname];
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000B59C8 File Offset: 0x000B3BC8
		internal static XmlSchemaSimpleType GetNormalizedStringTypeV1Compat()
		{
			if (DatatypeImplementation.normalizedStringTypeV1Compat == null)
			{
				XmlSchemaSimpleType simpleTypeFromTypeCode = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.NormalizedString);
				XmlSchemaSimpleType xmlSchemaSimpleType = simpleTypeFromTypeCode.Clone() as XmlSchemaSimpleType;
				xmlSchemaSimpleType.SetDatatype(DatatypeImplementation.c_normalizedStringV1Compat);
				xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(DatatypeImplementation.c_normalizedStringV1Compat);
				xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
				DatatypeImplementation.normalizedStringTypeV1Compat = xmlSchemaSimpleType;
			}
			return DatatypeImplementation.normalizedStringTypeV1Compat;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000B5A28 File Offset: 0x000B3C28
		internal static XmlSchemaSimpleType GetTokenTypeV1Compat()
		{
			if (DatatypeImplementation.tokenTypeV1Compat == null)
			{
				XmlSchemaSimpleType simpleTypeFromTypeCode = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.Token);
				XmlSchemaSimpleType xmlSchemaSimpleType = simpleTypeFromTypeCode.Clone() as XmlSchemaSimpleType;
				xmlSchemaSimpleType.SetDatatype(DatatypeImplementation.c_tokenV1Compat);
				xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(DatatypeImplementation.c_tokenV1Compat);
				xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
				DatatypeImplementation.tokenTypeV1Compat = xmlSchemaSimpleType;
			}
			return DatatypeImplementation.tokenTypeV1Compat;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000B5A88 File Offset: 0x000B3C88
		internal static XmlSchemaSimpleType[] GetBuiltInTypes()
		{
			return DatatypeImplementation.enumToTypeCode;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000B5A90 File Offset: 0x000B3C90
		internal static XmlTypeCode GetPrimitiveTypeCode(XmlTypeCode typeCode)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.enumToTypeCode[(int)typeCode];
			while (xmlSchemaSimpleType.BaseXmlSchemaType != DatatypeImplementation.AnySimpleType)
			{
				xmlSchemaSimpleType = (xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType);
			}
			return xmlSchemaSimpleType.TypeCode;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000B5AC8 File Offset: 0x000B3CC8
		internal override XmlSchemaDatatype DeriveByRestriction(XmlSchemaObjectCollection facets, XmlNameTable nameTable, XmlSchemaType schemaType)
		{
			DatatypeImplementation datatypeImplementation = (DatatypeImplementation)base.MemberwiseClone();
			datatypeImplementation.restriction = this.FacetsChecker.ConstructRestriction(this, facets, nameTable);
			datatypeImplementation.baseType = this;
			datatypeImplementation.parentSchemaType = schemaType;
			datatypeImplementation.valueConverter = null;
			return datatypeImplementation;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000B5B0B File Offset: 0x000B3D0B
		internal override XmlSchemaDatatype DeriveByList(XmlSchemaType schemaType)
		{
			return this.DeriveByList(0, schemaType);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000B5B18 File Offset: 0x000B3D18
		internal XmlSchemaDatatype DeriveByList(int minSize, XmlSchemaType schemaType)
		{
			if (this.variety == XmlSchemaDatatypeVariety.List)
			{
				throw new XmlSchemaException("Sch_ListFromNonatomic", string.Empty);
			}
			if (this.variety == XmlSchemaDatatypeVariety.Union && !((Datatype_union)this).HasAtomicMembers())
			{
				throw new XmlSchemaException("Sch_ListFromNonatomic", string.Empty);
			}
			return new Datatype_List(this, minSize)
			{
				variety = XmlSchemaDatatypeVariety.List,
				restriction = null,
				baseType = DatatypeImplementation.c_anySimpleType,
				parentSchemaType = schemaType
			};
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000B5B90 File Offset: 0x000B3D90
		internal new static DatatypeImplementation DeriveByUnion(XmlSchemaSimpleType[] types, XmlSchemaType schemaType)
		{
			return new Datatype_union(types)
			{
				baseType = DatatypeImplementation.c_anySimpleType,
				variety = XmlSchemaDatatypeVariety.Union,
				parentSchemaType = schemaType
			};
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000B5BBE File Offset: 0x000B3DBE
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000B5BC0 File Offset: 0x000B3DC0
		public override bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			if (datatype == null)
			{
				return false;
			}
			for (DatatypeImplementation datatypeImplementation = this; datatypeImplementation != null; datatypeImplementation = datatypeImplementation.baseType)
			{
				if (datatypeImplementation == datatype)
				{
					return true;
				}
			}
			if (((DatatypeImplementation)datatype).baseType == null)
			{
				Type type = base.GetType();
				Type type2 = datatype.GetType();
				return type2 == type || type.IsSubclassOf(type2);
			}
			if (datatype.Variety == XmlSchemaDatatypeVariety.Union && !datatype.HasLexicalFacets && !datatype.HasValueFacets && this.variety != XmlSchemaDatatypeVariety.Union)
			{
				return ((Datatype_union)datatype).IsUnionBaseOf(this);
			}
			return (this.variety == XmlSchemaDatatypeVariety.Union || this.variety == XmlSchemaDatatypeVariety.List) && this.restriction == null && datatype == DatatypeImplementation.anySimpleType.Datatype;
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000B5C6C File Offset: 0x000B3E6C
		internal override bool IsEqual(object o1, object o2)
		{
			return this.Compare(o1, o2) == 0;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000B5C7C File Offset: 0x000B3E7C
		internal override bool IsComparable(XmlSchemaDatatype dtype)
		{
			XmlTypeCode typeCode = this.TypeCode;
			XmlTypeCode typeCode2 = dtype.TypeCode;
			return typeCode == typeCode2 || DatatypeImplementation.GetPrimitiveTypeCode(typeCode) == DatatypeImplementation.GetPrimitiveTypeCode(typeCode2) || (this.IsDerivedFrom(dtype) || dtype.IsDerivedFrom(this));
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000B5CC2 File Offset: 0x000B3EC2
		internal virtual XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return null;
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x000B5CC5 File Offset: 0x000B3EC5
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x000B5CCC File Offset: 0x000B3ECC
		internal override XmlValueConverter ValueConverter
		{
			get
			{
				if (this.valueConverter == null)
				{
					this.valueConverter = this.CreateValueConverter(this.parentSchemaType);
				}
				return this.valueConverter;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x000B5CEE File Offset: 0x000B3EEE
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x000B5CF2 File Offset: 0x000B3EF2
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x000B5CFE File Offset: 0x000B3EFE
		public override XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return this.variety;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x000B5D06 File Offset: 0x000B3F06
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x000B5D09 File Offset: 0x000B3F09
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x000B5D11 File Offset: 0x000B3F11
		internal override RestrictionFacets Restriction
		{
			get
			{
				return this.restriction;
			}
			set
			{
				this.restriction = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x000B5D1C File Offset: 0x000B3F1C
		internal override bool HasLexicalFacets
		{
			get
			{
				RestrictionFlags restrictionFlags = (this.restriction != null) ? this.restriction.Flags : ((RestrictionFlags)0);
				return restrictionFlags != (RestrictionFlags)0 && (restrictionFlags & (RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) != (RestrictionFlags)0;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x000B5D50 File Offset: 0x000B3F50
		internal override bool HasValueFacets
		{
			get
			{
				RestrictionFlags restrictionFlags = (this.restriction != null) ? this.restriction.Flags : ((RestrictionFlags)0);
				return restrictionFlags != (RestrictionFlags)0 && (restrictionFlags & (RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Enumeration | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) != (RestrictionFlags)0;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x000B5D83 File Offset: 0x000B3F83
		protected DatatypeImplementation Base
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600213D RID: 8509
		internal abstract Type ListValueType { get; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600213E RID: 8510
		internal abstract RestrictionFlags ValidRestrictionFlags { get; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x000B5D8B File Offset: 0x000B3F8B
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000B5D8E File Offset: 0x000B3F8E
		internal override object ParseValue(string s, Type typDest, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ValueConverter.ChangeType(this.ParseValue(s, nameTable, nsmgr), typDest, nsmgr);
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000B5DA8 File Offset: 0x000B3FA8
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object obj;
			Exception ex = this.TryParseValue(s, nameTable, nsmgr, out obj);
			if (ex != null)
			{
				throw new XmlSchemaException("Sch_InvalidValueDetailed", new string[]
				{
					s,
					this.GetTypeName(),
					ex.Message
				}, ex, null, 0, 0, null);
			}
			if (this.Variety == XmlSchemaDatatypeVariety.Union)
			{
				XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
				return xsdSimpleValue.TypedValue;
			}
			return obj;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000B5E08 File Offset: 0x000B4008
		internal override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, bool createAtomicValue)
		{
			if (!createAtomicValue)
			{
				return this.ParseValue(s, nameTable, nsmgr);
			}
			object result;
			Exception ex = this.TryParseValue(s, nameTable, nsmgr, out result);
			if (ex != null)
			{
				throw new XmlSchemaException("Sch_InvalidValueDetailed", new string[]
				{
					s,
					this.GetTypeName(),
					ex.Message
				}, ex, null, 0, 0, null);
			}
			return result;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000B5E60 File Offset: 0x000B4060
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue)
		{
			Exception ex = null;
			typedValue = null;
			if (value == null)
			{
				return new ArgumentNullException("value");
			}
			string text = value as string;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, namespaceResolver, out typedValue);
			}
			try
			{
				object obj = value;
				if (value.GetType() != this.ValueType)
				{
					obj = this.ValueConverter.ChangeType(value, this.ValueType, namespaceResolver);
				}
				if (this.HasLexicalFacets)
				{
					string text2 = (string)this.ValueConverter.ChangeType(value, typeof(string), namespaceResolver);
					ex = this.FacetsChecker.CheckLexicalFacets(ref text2, this);
					if (ex != null)
					{
						return ex;
					}
				}
				if (this.HasValueFacets)
				{
					ex = this.FacetsChecker.CheckValueFacets(obj, this);
					if (ex != null)
					{
						return ex;
					}
				}
				typedValue = obj;
				return null;
			}
			catch (FormatException ex2)
			{
				ex = ex2;
			}
			catch (InvalidCastException ex3)
			{
				ex = ex3;
			}
			catch (OverflowException ex4)
			{
				ex = ex4;
			}
			catch (ArgumentException ex5)
			{
				ex = ex5;
			}
			return ex;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000B5F74 File Offset: 0x000B4174
		internal string GetTypeName()
		{
			XmlSchemaType xmlSchemaType = this.parentSchemaType;
			string result;
			if (xmlSchemaType == null || xmlSchemaType.QualifiedName.IsEmpty)
			{
				result = base.TypeCodeString;
			}
			else
			{
				result = xmlSchemaType.QualifiedName.ToString();
			}
			return result;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000B5FB0 File Offset: 0x000B41B0
		protected int Compare(byte[] value1, byte[] value2)
		{
			int num = value1.Length;
			if (num != value2.Length)
			{
				return -1;
			}
			for (int i = 0; i < num; i++)
			{
				if (value1[i] != value2[i])
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x04000E09 RID: 3593
		private XmlSchemaDatatypeVariety variety;

		// Token: 0x04000E0A RID: 3594
		private RestrictionFacets restriction;

		// Token: 0x04000E0B RID: 3595
		private DatatypeImplementation baseType;

		// Token: 0x04000E0C RID: 3596
		private XmlValueConverter valueConverter;

		// Token: 0x04000E0D RID: 3597
		private XmlSchemaType parentSchemaType;

		// Token: 0x04000E0E RID: 3598
		private static Hashtable builtinTypes = new Hashtable();

		// Token: 0x04000E0F RID: 3599
		private static XmlSchemaSimpleType[] enumToTypeCode = new XmlSchemaSimpleType[55];

		// Token: 0x04000E10 RID: 3600
		private static XmlSchemaSimpleType anySimpleType;

		// Token: 0x04000E11 RID: 3601
		private static XmlSchemaSimpleType anyAtomicType;

		// Token: 0x04000E12 RID: 3602
		private static XmlSchemaSimpleType untypedAtomicType;

		// Token: 0x04000E13 RID: 3603
		private static XmlSchemaSimpleType yearMonthDurationType;

		// Token: 0x04000E14 RID: 3604
		private static XmlSchemaSimpleType dayTimeDurationType;

		// Token: 0x04000E15 RID: 3605
		private static volatile XmlSchemaSimpleType normalizedStringTypeV1Compat;

		// Token: 0x04000E16 RID: 3606
		private static volatile XmlSchemaSimpleType tokenTypeV1Compat;

		// Token: 0x04000E17 RID: 3607
		private const int anySimpleTypeIndex = 11;

		// Token: 0x04000E18 RID: 3608
		internal static XmlQualifiedName QnAnySimpleType = new XmlQualifiedName("anySimpleType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x04000E19 RID: 3609
		internal static XmlQualifiedName QnAnyType = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x04000E1A RID: 3610
		internal static FacetsChecker stringFacetsChecker = new StringFacetsChecker();

		// Token: 0x04000E1B RID: 3611
		internal static FacetsChecker miscFacetsChecker = new MiscFacetsChecker();

		// Token: 0x04000E1C RID: 3612
		internal static FacetsChecker numeric2FacetsChecker = new Numeric2FacetsChecker();

		// Token: 0x04000E1D RID: 3613
		internal static FacetsChecker binaryFacetsChecker = new BinaryFacetsChecker();

		// Token: 0x04000E1E RID: 3614
		internal static FacetsChecker dateTimeFacetsChecker = new DateTimeFacetsChecker();

		// Token: 0x04000E1F RID: 3615
		internal static FacetsChecker durationFacetsChecker = new DurationFacetsChecker();

		// Token: 0x04000E20 RID: 3616
		internal static FacetsChecker listFacetsChecker = new ListFacetsChecker();

		// Token: 0x04000E21 RID: 3617
		internal static FacetsChecker qnameFacetsChecker = new QNameFacetsChecker();

		// Token: 0x04000E22 RID: 3618
		internal static FacetsChecker unionFacetsChecker = new UnionFacetsChecker();

		// Token: 0x04000E23 RID: 3619
		private static readonly DatatypeImplementation c_anySimpleType = new Datatype_anySimpleType();

		// Token: 0x04000E24 RID: 3620
		private static readonly DatatypeImplementation c_anyURI = new Datatype_anyURI();

		// Token: 0x04000E25 RID: 3621
		private static readonly DatatypeImplementation c_base64Binary = new Datatype_base64Binary();

		// Token: 0x04000E26 RID: 3622
		private static readonly DatatypeImplementation c_boolean = new Datatype_boolean();

		// Token: 0x04000E27 RID: 3623
		private static readonly DatatypeImplementation c_byte = new Datatype_byte();

		// Token: 0x04000E28 RID: 3624
		private static readonly DatatypeImplementation c_char = new Datatype_char();

		// Token: 0x04000E29 RID: 3625
		private static readonly DatatypeImplementation c_date = new Datatype_date();

		// Token: 0x04000E2A RID: 3626
		private static readonly DatatypeImplementation c_dateTime = new Datatype_dateTime();

		// Token: 0x04000E2B RID: 3627
		private static readonly DatatypeImplementation c_dateTimeNoTz = new Datatype_dateTimeNoTimeZone();

		// Token: 0x04000E2C RID: 3628
		private static readonly DatatypeImplementation c_dateTimeTz = new Datatype_dateTimeTimeZone();

		// Token: 0x04000E2D RID: 3629
		private static readonly DatatypeImplementation c_day = new Datatype_day();

		// Token: 0x04000E2E RID: 3630
		private static readonly DatatypeImplementation c_decimal = new Datatype_decimal();

		// Token: 0x04000E2F RID: 3631
		private static readonly DatatypeImplementation c_double = new Datatype_double();

		// Token: 0x04000E30 RID: 3632
		private static readonly DatatypeImplementation c_doubleXdr = new Datatype_doubleXdr();

		// Token: 0x04000E31 RID: 3633
		private static readonly DatatypeImplementation c_duration = new Datatype_duration();

		// Token: 0x04000E32 RID: 3634
		private static readonly DatatypeImplementation c_ENTITY = new Datatype_ENTITY();

		// Token: 0x04000E33 RID: 3635
		private static readonly DatatypeImplementation c_ENTITIES = (DatatypeImplementation)DatatypeImplementation.c_ENTITY.DeriveByList(1, null);

		// Token: 0x04000E34 RID: 3636
		private static readonly DatatypeImplementation c_ENUMERATION = new Datatype_ENUMERATION();

		// Token: 0x04000E35 RID: 3637
		private static readonly DatatypeImplementation c_fixed = new Datatype_fixed();

		// Token: 0x04000E36 RID: 3638
		private static readonly DatatypeImplementation c_float = new Datatype_float();

		// Token: 0x04000E37 RID: 3639
		private static readonly DatatypeImplementation c_floatXdr = new Datatype_floatXdr();

		// Token: 0x04000E38 RID: 3640
		private static readonly DatatypeImplementation c_hexBinary = new Datatype_hexBinary();

		// Token: 0x04000E39 RID: 3641
		private static readonly DatatypeImplementation c_ID = new Datatype_ID();

		// Token: 0x04000E3A RID: 3642
		private static readonly DatatypeImplementation c_IDREF = new Datatype_IDREF();

		// Token: 0x04000E3B RID: 3643
		private static readonly DatatypeImplementation c_IDREFS = (DatatypeImplementation)DatatypeImplementation.c_IDREF.DeriveByList(1, null);

		// Token: 0x04000E3C RID: 3644
		private static readonly DatatypeImplementation c_int = new Datatype_int();

		// Token: 0x04000E3D RID: 3645
		private static readonly DatatypeImplementation c_integer = new Datatype_integer();

		// Token: 0x04000E3E RID: 3646
		private static readonly DatatypeImplementation c_language = new Datatype_language();

		// Token: 0x04000E3F RID: 3647
		private static readonly DatatypeImplementation c_long = new Datatype_long();

		// Token: 0x04000E40 RID: 3648
		private static readonly DatatypeImplementation c_month = new Datatype_month();

		// Token: 0x04000E41 RID: 3649
		private static readonly DatatypeImplementation c_monthDay = new Datatype_monthDay();

		// Token: 0x04000E42 RID: 3650
		private static readonly DatatypeImplementation c_Name = new Datatype_Name();

		// Token: 0x04000E43 RID: 3651
		private static readonly DatatypeImplementation c_NCName = new Datatype_NCName();

		// Token: 0x04000E44 RID: 3652
		private static readonly DatatypeImplementation c_negativeInteger = new Datatype_negativeInteger();

		// Token: 0x04000E45 RID: 3653
		private static readonly DatatypeImplementation c_NMTOKEN = new Datatype_NMTOKEN();

		// Token: 0x04000E46 RID: 3654
		private static readonly DatatypeImplementation c_NMTOKENS = (DatatypeImplementation)DatatypeImplementation.c_NMTOKEN.DeriveByList(1, null);

		// Token: 0x04000E47 RID: 3655
		private static readonly DatatypeImplementation c_nonNegativeInteger = new Datatype_nonNegativeInteger();

		// Token: 0x04000E48 RID: 3656
		private static readonly DatatypeImplementation c_nonPositiveInteger = new Datatype_nonPositiveInteger();

		// Token: 0x04000E49 RID: 3657
		private static readonly DatatypeImplementation c_normalizedString = new Datatype_normalizedString();

		// Token: 0x04000E4A RID: 3658
		private static readonly DatatypeImplementation c_NOTATION = new Datatype_NOTATION();

		// Token: 0x04000E4B RID: 3659
		private static readonly DatatypeImplementation c_positiveInteger = new Datatype_positiveInteger();

		// Token: 0x04000E4C RID: 3660
		private static readonly DatatypeImplementation c_QName = new Datatype_QName();

		// Token: 0x04000E4D RID: 3661
		private static readonly DatatypeImplementation c_QNameXdr = new Datatype_QNameXdr();

		// Token: 0x04000E4E RID: 3662
		private static readonly DatatypeImplementation c_short = new Datatype_short();

		// Token: 0x04000E4F RID: 3663
		private static readonly DatatypeImplementation c_string = new Datatype_string();

		// Token: 0x04000E50 RID: 3664
		private static readonly DatatypeImplementation c_time = new Datatype_time();

		// Token: 0x04000E51 RID: 3665
		private static readonly DatatypeImplementation c_timeNoTz = new Datatype_timeNoTimeZone();

		// Token: 0x04000E52 RID: 3666
		private static readonly DatatypeImplementation c_timeTz = new Datatype_timeTimeZone();

		// Token: 0x04000E53 RID: 3667
		private static readonly DatatypeImplementation c_token = new Datatype_token();

		// Token: 0x04000E54 RID: 3668
		private static readonly DatatypeImplementation c_unsignedByte = new Datatype_unsignedByte();

		// Token: 0x04000E55 RID: 3669
		private static readonly DatatypeImplementation c_unsignedInt = new Datatype_unsignedInt();

		// Token: 0x04000E56 RID: 3670
		private static readonly DatatypeImplementation c_unsignedLong = new Datatype_unsignedLong();

		// Token: 0x04000E57 RID: 3671
		private static readonly DatatypeImplementation c_unsignedShort = new Datatype_unsignedShort();

		// Token: 0x04000E58 RID: 3672
		private static readonly DatatypeImplementation c_uuid = new Datatype_uuid();

		// Token: 0x04000E59 RID: 3673
		private static readonly DatatypeImplementation c_year = new Datatype_year();

		// Token: 0x04000E5A RID: 3674
		private static readonly DatatypeImplementation c_yearMonth = new Datatype_yearMonth();

		// Token: 0x04000E5B RID: 3675
		internal static readonly DatatypeImplementation c_normalizedStringV1Compat = new Datatype_normalizedStringV1Compat();

		// Token: 0x04000E5C RID: 3676
		internal static readonly DatatypeImplementation c_tokenV1Compat = new Datatype_tokenV1Compat();

		// Token: 0x04000E5D RID: 3677
		private static readonly DatatypeImplementation c_anyAtomicType = new Datatype_anyAtomicType();

		// Token: 0x04000E5E RID: 3678
		private static readonly DatatypeImplementation c_dayTimeDuration = new Datatype_dayTimeDuration();

		// Token: 0x04000E5F RID: 3679
		private static readonly DatatypeImplementation c_untypedAtomicType = new Datatype_untypedAtomicType();

		// Token: 0x04000E60 RID: 3680
		private static readonly DatatypeImplementation c_yearMonthDuration = new Datatype_yearMonthDuration();

		// Token: 0x04000E61 RID: 3681
		private static readonly DatatypeImplementation[] c_tokenizedTypes;

		// Token: 0x04000E62 RID: 3682
		private static readonly DatatypeImplementation[] c_tokenizedTypesXsd;

		// Token: 0x04000E63 RID: 3683
		private static readonly DatatypeImplementation.SchemaDatatypeMap[] c_XdrTypes;

		// Token: 0x04000E64 RID: 3684
		private static readonly DatatypeImplementation.SchemaDatatypeMap[] c_XsdTypes;

		// Token: 0x0200048E RID: 1166
		private class SchemaDatatypeMap : IComparable
		{
			// Token: 0x06003122 RID: 12578 RVA: 0x0011E0BB File Offset: 0x0011C2BB
			internal SchemaDatatypeMap(string name, DatatypeImplementation type)
			{
				this.name = name;
				this.type = type;
			}

			// Token: 0x06003123 RID: 12579 RVA: 0x0011E0D1 File Offset: 0x0011C2D1
			internal SchemaDatatypeMap(string name, DatatypeImplementation type, int parentIndex)
			{
				this.name = name;
				this.type = type;
				this.parentIndex = parentIndex;
			}

			// Token: 0x06003124 RID: 12580 RVA: 0x0011E0EE File Offset: 0x0011C2EE
			public static explicit operator DatatypeImplementation(DatatypeImplementation.SchemaDatatypeMap sdm)
			{
				return sdm.type;
			}

			// Token: 0x17000A69 RID: 2665
			// (get) Token: 0x06003125 RID: 12581 RVA: 0x0011E0F6 File Offset: 0x0011C2F6
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000A6A RID: 2666
			// (get) Token: 0x06003126 RID: 12582 RVA: 0x0011E0FE File Offset: 0x0011C2FE
			public int ParentIndex
			{
				get
				{
					return this.parentIndex;
				}
			}

			// Token: 0x06003127 RID: 12583 RVA: 0x0011E106 File Offset: 0x0011C306
			public int CompareTo(object obj)
			{
				return string.Compare(this.name, (string)obj, StringComparison.Ordinal);
			}

			// Token: 0x04001E18 RID: 7704
			private string name;

			// Token: 0x04001E19 RID: 7705
			private DatatypeImplementation type;

			// Token: 0x04001E1A RID: 7706
			private int parentIndex;
		}
	}
}
