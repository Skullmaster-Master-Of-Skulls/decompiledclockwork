using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x020001AA RID: 426
	public abstract class XmlSerializationReader : XmlSerializationGeneratedCode
	{
		// Token: 0x06001C65 RID: 7269 RVA: 0x00085E0C File Offset: 0x0008400C
		static XmlSerializationReader()
		{
			XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
			XmlSerializationReader.checkDeserializeAdvances = (xmlSerializerSection != null && xmlSerializerSection.CheckDeserializeAdvances);
		}

		// Token: 0x06001C66 RID: 7270
		protected abstract void InitIDs();

		// Token: 0x06001C67 RID: 7271 RVA: 0x00085E3C File Offset: 0x0008403C
		internal void Init(XmlReader r, XmlDeserializationEvents events, string encodingStyle, TempAssembly tempAssembly)
		{
			this.events = events;
			if (XmlSerializationReader.checkDeserializeAdvances)
			{
				this.countingReader = new XmlCountingReader(r);
				this.r = this.countingReader;
			}
			else
			{
				this.r = r;
			}
			this.d = null;
			this.soap12 = (encodingStyle == "http://www.w3.org/2003/05/soap-encoding");
			base.Init(tempAssembly);
			this.schemaNsID = r.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.schemaNs2000ID = r.NameTable.Add("http://www.w3.org/2000/10/XMLSchema");
			this.schemaNs1999ID = r.NameTable.Add("http://www.w3.org/1999/XMLSchema");
			this.schemaNonXsdTypesNsID = r.NameTable.Add("http://microsoft.com/wsdl/types/");
			this.instanceNsID = r.NameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.instanceNs2000ID = r.NameTable.Add("http://www.w3.org/2000/10/XMLSchema-instance");
			this.instanceNs1999ID = r.NameTable.Add("http://www.w3.org/1999/XMLSchema-instance");
			this.soapNsID = r.NameTable.Add("http://schemas.xmlsoap.org/soap/encoding/");
			this.soap12NsID = r.NameTable.Add("http://www.w3.org/2003/05/soap-encoding");
			this.schemaID = r.NameTable.Add("schema");
			this.wsdlNsID = r.NameTable.Add("http://schemas.xmlsoap.org/wsdl/");
			this.wsdlArrayTypeID = r.NameTable.Add("arrayType");
			this.nullID = r.NameTable.Add("null");
			this.nilID = r.NameTable.Add("nil");
			this.typeID = r.NameTable.Add("type");
			this.arrayTypeID = r.NameTable.Add("arrayType");
			this.itemTypeID = r.NameTable.Add("itemType");
			this.arraySizeID = r.NameTable.Add("arraySize");
			this.arrayID = r.NameTable.Add("Array");
			this.urTypeID = r.NameTable.Add("anyType");
			this.InitIDs();
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00086056 File Offset: 0x00084256
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x0008605E File Offset: 0x0008425E
		protected bool DecodeName
		{
			get
			{
				return this.decodeName;
			}
			set
			{
				this.decodeName = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00086067 File Offset: 0x00084267
		protected XmlReader Reader
		{
			get
			{
				return this.r;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0008606F File Offset: 0x0008426F
		protected int ReaderCount
		{
			get
			{
				if (!XmlSerializationReader.checkDeserializeAdvances)
				{
					return 0;
				}
				return this.countingReader.AdvanceCount;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00086085 File Offset: 0x00084285
		protected XmlDocument Document
		{
			get
			{
				if (this.d == null)
				{
					this.d = new XmlDocument(this.r.NameTable);
					this.d.SetBaseURI(this.r.BaseURI);
				}
				return this.d;
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000860C1 File Offset: 0x000842C1
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			return DynamicAssemblies.Get(assemblyFullName);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000860CC File Offset: 0x000842CC
		private void InitPrimitiveIDs()
		{
			if (this.tokenID != null)
			{
				return;
			}
			object obj = this.r.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			object obj2 = this.r.NameTable.Add("http://microsoft.com/wsdl/types/");
			this.stringID = this.r.NameTable.Add("string");
			this.intID = this.r.NameTable.Add("int");
			this.booleanID = this.r.NameTable.Add("boolean");
			this.shortID = this.r.NameTable.Add("short");
			this.longID = this.r.NameTable.Add("long");
			this.floatID = this.r.NameTable.Add("float");
			this.doubleID = this.r.NameTable.Add("double");
			this.decimalID = this.r.NameTable.Add("decimal");
			this.dateTimeID = this.r.NameTable.Add("dateTime");
			this.qnameID = this.r.NameTable.Add("QName");
			this.dateID = this.r.NameTable.Add("date");
			this.timeID = this.r.NameTable.Add("time");
			this.hexBinaryID = this.r.NameTable.Add("hexBinary");
			this.base64BinaryID = this.r.NameTable.Add("base64Binary");
			this.unsignedByteID = this.r.NameTable.Add("unsignedByte");
			this.byteID = this.r.NameTable.Add("byte");
			this.unsignedShortID = this.r.NameTable.Add("unsignedShort");
			this.unsignedIntID = this.r.NameTable.Add("unsignedInt");
			this.unsignedLongID = this.r.NameTable.Add("unsignedLong");
			this.oldDecimalID = this.r.NameTable.Add("decimal");
			this.oldTimeInstantID = this.r.NameTable.Add("timeInstant");
			this.charID = this.r.NameTable.Add("char");
			this.guidID = this.r.NameTable.Add("guid");
			if (LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				this.timeSpanID = this.r.NameTable.Add("TimeSpan");
			}
			this.base64ID = this.r.NameTable.Add("base64");
			this.anyURIID = this.r.NameTable.Add("anyURI");
			this.durationID = this.r.NameTable.Add("duration");
			this.ENTITYID = this.r.NameTable.Add("ENTITY");
			this.ENTITIESID = this.r.NameTable.Add("ENTITIES");
			this.gDayID = this.r.NameTable.Add("gDay");
			this.gMonthID = this.r.NameTable.Add("gMonth");
			this.gMonthDayID = this.r.NameTable.Add("gMonthDay");
			this.gYearID = this.r.NameTable.Add("gYear");
			this.gYearMonthID = this.r.NameTable.Add("gYearMonth");
			this.IDID = this.r.NameTable.Add("ID");
			this.IDREFID = this.r.NameTable.Add("IDREF");
			this.IDREFSID = this.r.NameTable.Add("IDREFS");
			this.integerID = this.r.NameTable.Add("integer");
			this.languageID = this.r.NameTable.Add("language");
			this.NameID = this.r.NameTable.Add("Name");
			this.NCNameID = this.r.NameTable.Add("NCName");
			this.NMTOKENID = this.r.NameTable.Add("NMTOKEN");
			this.NMTOKENSID = this.r.NameTable.Add("NMTOKENS");
			this.negativeIntegerID = this.r.NameTable.Add("negativeInteger");
			this.nonNegativeIntegerID = this.r.NameTable.Add("nonNegativeInteger");
			this.nonPositiveIntegerID = this.r.NameTable.Add("nonPositiveInteger");
			this.normalizedStringID = this.r.NameTable.Add("normalizedString");
			this.NOTATIONID = this.r.NameTable.Add("NOTATION");
			this.positiveIntegerID = this.r.NameTable.Add("positiveInteger");
			this.tokenID = this.r.NameTable.Add("token");
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0008665C File Offset: 0x0008485C
		protected XmlQualifiedName GetXsiType()
		{
			string attribute = this.r.GetAttribute(this.typeID, this.instanceNsID);
			if (attribute == null)
			{
				attribute = this.r.GetAttribute(this.typeID, this.instanceNs2000ID);
				if (attribute == null)
				{
					attribute = this.r.GetAttribute(this.typeID, this.instanceNs1999ID);
					if (attribute == null)
					{
						return null;
					}
				}
			}
			return this.ToXmlQualifiedName(attribute, false);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000866C4 File Offset: 0x000848C4
		private Type GetPrimitiveType(XmlQualifiedName typeName, bool throwOnUnknown)
		{
			this.InitPrimitiveIDs();
			if (typeName.Namespace == this.schemaNsID || typeName.Namespace == this.soapNsID || typeName.Namespace == this.soap12NsID)
			{
				if (typeName.Name == this.stringID || typeName.Name == this.anyURIID || typeName.Name == this.durationID || typeName.Name == this.ENTITYID || typeName.Name == this.ENTITIESID || typeName.Name == this.gDayID || typeName.Name == this.gMonthID || typeName.Name == this.gMonthDayID || typeName.Name == this.gYearID || typeName.Name == this.gYearMonthID || typeName.Name == this.IDID || typeName.Name == this.IDREFID || typeName.Name == this.IDREFSID || typeName.Name == this.integerID || typeName.Name == this.languageID || typeName.Name == this.NameID || typeName.Name == this.NCNameID || typeName.Name == this.NMTOKENID || typeName.Name == this.NMTOKENSID || typeName.Name == this.negativeIntegerID || typeName.Name == this.nonPositiveIntegerID || typeName.Name == this.nonNegativeIntegerID || typeName.Name == this.normalizedStringID || typeName.Name == this.NOTATIONID || typeName.Name == this.positiveIntegerID || typeName.Name == this.tokenID)
				{
					return typeof(string);
				}
				if (typeName.Name == this.intID)
				{
					return typeof(int);
				}
				if (typeName.Name == this.booleanID)
				{
					return typeof(bool);
				}
				if (typeName.Name == this.shortID)
				{
					return typeof(short);
				}
				if (typeName.Name == this.longID)
				{
					return typeof(long);
				}
				if (typeName.Name == this.floatID)
				{
					return typeof(float);
				}
				if (typeName.Name == this.doubleID)
				{
					return typeof(double);
				}
				if (typeName.Name == this.decimalID)
				{
					return typeof(decimal);
				}
				if (typeName.Name == this.dateTimeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.qnameID)
				{
					return typeof(XmlQualifiedName);
				}
				if (typeName.Name == this.dateID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.timeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.hexBinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.base64BinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.unsignedByteID)
				{
					return typeof(byte);
				}
				if (typeName.Name == this.byteID)
				{
					return typeof(sbyte);
				}
				if (typeName.Name == this.unsignedShortID)
				{
					return typeof(ushort);
				}
				if (typeName.Name == this.unsignedIntID)
				{
					return typeof(uint);
				}
				if (typeName.Name == this.unsignedLongID)
				{
					return typeof(ulong);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else if (typeName.Namespace == this.schemaNs2000ID || typeName.Namespace == this.schemaNs1999ID)
			{
				if (typeName.Name == this.stringID || typeName.Name == this.anyURIID || typeName.Name == this.durationID || typeName.Name == this.ENTITYID || typeName.Name == this.ENTITIESID || typeName.Name == this.gDayID || typeName.Name == this.gMonthID || typeName.Name == this.gMonthDayID || typeName.Name == this.gYearID || typeName.Name == this.gYearMonthID || typeName.Name == this.IDID || typeName.Name == this.IDREFID || typeName.Name == this.IDREFSID || typeName.Name == this.integerID || typeName.Name == this.languageID || typeName.Name == this.NameID || typeName.Name == this.NCNameID || typeName.Name == this.NMTOKENID || typeName.Name == this.NMTOKENSID || typeName.Name == this.negativeIntegerID || typeName.Name == this.nonPositiveIntegerID || typeName.Name == this.nonNegativeIntegerID || typeName.Name == this.normalizedStringID || typeName.Name == this.NOTATIONID || typeName.Name == this.positiveIntegerID || typeName.Name == this.tokenID)
				{
					return typeof(string);
				}
				if (typeName.Name == this.intID)
				{
					return typeof(int);
				}
				if (typeName.Name == this.booleanID)
				{
					return typeof(bool);
				}
				if (typeName.Name == this.shortID)
				{
					return typeof(short);
				}
				if (typeName.Name == this.longID)
				{
					return typeof(long);
				}
				if (typeName.Name == this.floatID)
				{
					return typeof(float);
				}
				if (typeName.Name == this.doubleID)
				{
					return typeof(double);
				}
				if (typeName.Name == this.oldDecimalID)
				{
					return typeof(decimal);
				}
				if (typeName.Name == this.oldTimeInstantID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.qnameID)
				{
					return typeof(XmlQualifiedName);
				}
				if (typeName.Name == this.dateID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.timeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.hexBinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.byteID)
				{
					return typeof(sbyte);
				}
				if (typeName.Name == this.unsignedShortID)
				{
					return typeof(ushort);
				}
				if (typeName.Name == this.unsignedIntID)
				{
					return typeof(uint);
				}
				if (typeName.Name == this.unsignedLongID)
				{
					return typeof(ulong);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else if (typeName.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (typeName.Name == this.charID)
				{
					return typeof(char);
				}
				if (typeName.Name == this.guidID)
				{
					return typeof(Guid);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else
			{
				if (throwOnUnknown)
				{
					throw this.CreateUnknownTypeException(typeName);
				}
				return null;
			}
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x00086E27 File Offset: 0x00085027
		private bool IsPrimitiveNamespace(string ns)
		{
			return ns == this.schemaNsID || ns == this.schemaNonXsdTypesNsID || ns == this.soapNsID || ns == this.soap12NsID || ns == this.schemaNs2000ID || ns == this.schemaNs1999ID;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x00086E64 File Offset: 0x00085064
		private string ReadStringValue()
		{
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return string.Empty;
			}
			this.r.ReadStartElement();
			string result = this.r.ReadString();
			this.ReadEndElement();
			return result;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00086EB0 File Offset: 0x000850B0
		private XmlQualifiedName ReadXmlQualifiedName()
		{
			bool flag = false;
			string value;
			if (this.r.IsEmptyElement)
			{
				value = string.Empty;
				flag = true;
			}
			else
			{
				this.r.ReadStartElement();
				value = this.r.ReadString();
			}
			XmlQualifiedName result = this.ToXmlQualifiedName(value);
			if (flag)
			{
				this.r.Skip();
			}
			else
			{
				this.ReadEndElement();
			}
			return result;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00086F0C File Offset: 0x0008510C
		private byte[] ReadByteArray(bool isBase64)
		{
			ArrayList arrayList = new ArrayList();
			int num = 1024;
			int num2 = -1;
			int num3 = 0;
			int num4 = 0;
			byte[] array = new byte[num];
			arrayList.Add(array);
			while (num2 != 0)
			{
				if (num3 == array.Length)
				{
					num = Math.Min(num * 2, 65536);
					array = new byte[num];
					num3 = 0;
					arrayList.Add(array);
				}
				if (isBase64)
				{
					num2 = this.r.ReadElementContentAsBase64(array, num3, array.Length - num3);
				}
				else
				{
					num2 = this.r.ReadElementContentAsBinHex(array, num3, array.Length - num3);
				}
				num3 += num2;
				num4 += num2;
			}
			byte[] array2 = new byte[num4];
			num3 = 0;
			foreach (object obj in arrayList)
			{
				byte[] array3 = (byte[])obj;
				num = Math.Min(array3.Length, num4);
				if (num > 0)
				{
					Buffer.BlockCopy(array3, 0, array2, num3, num);
					num3 += num;
					num4 -= num;
				}
			}
			arrayList.Clear();
			return array2;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00087028 File Offset: 0x00085228
		protected object ReadTypedPrimitive(XmlQualifiedName type)
		{
			return this.ReadTypedPrimitive(type, false);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00087034 File Offset: 0x00085234
		private object ReadTypedPrimitive(XmlQualifiedName type, bool elementCanBeType)
		{
			this.InitPrimitiveIDs();
			if (!this.IsPrimitiveNamespace(type.Namespace) || type.Name == this.urTypeID)
			{
				return this.ReadXmlNodes(elementCanBeType);
			}
			object result;
			if (type.Namespace == this.schemaNsID || type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID)
			{
				if (type.Name == this.stringID || type.Name == this.normalizedStringID)
				{
					result = this.ReadStringValue();
				}
				else if (type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					result = this.CollapseWhitespace(this.ReadStringValue());
				}
				else if (type.Name == this.intID)
				{
					result = XmlConvert.ToInt32(this.ReadStringValue());
				}
				else if (type.Name == this.booleanID)
				{
					result = XmlConvert.ToBoolean(this.ReadStringValue());
				}
				else if (type.Name == this.shortID)
				{
					result = XmlConvert.ToInt16(this.ReadStringValue());
				}
				else if (type.Name == this.longID)
				{
					result = XmlConvert.ToInt64(this.ReadStringValue());
				}
				else if (type.Name == this.floatID)
				{
					result = XmlConvert.ToSingle(this.ReadStringValue());
				}
				else if (type.Name == this.doubleID)
				{
					result = XmlConvert.ToDouble(this.ReadStringValue());
				}
				else if (type.Name == this.decimalID)
				{
					result = XmlConvert.ToDecimal(this.ReadStringValue());
				}
				else if (type.Name == this.dateTimeID)
				{
					result = XmlSerializationReader.ToDateTime(this.ReadStringValue());
				}
				else if (type.Name == this.qnameID)
				{
					result = this.ReadXmlQualifiedName();
				}
				else if (type.Name == this.dateID)
				{
					result = XmlSerializationReader.ToDate(this.ReadStringValue());
				}
				else if (type.Name == this.timeID)
				{
					result = XmlSerializationReader.ToTime(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedByteID)
				{
					result = XmlConvert.ToByte(this.ReadStringValue());
				}
				else if (type.Name == this.byteID)
				{
					result = XmlConvert.ToSByte(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedShortID)
				{
					result = XmlConvert.ToUInt16(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedIntID)
				{
					result = XmlConvert.ToUInt32(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedLongID)
				{
					result = XmlConvert.ToUInt64(this.ReadStringValue());
				}
				else if (type.Name == this.hexBinaryID)
				{
					result = this.ToByteArrayHex(false);
				}
				else if (type.Name == this.base64BinaryID)
				{
					result = this.ToByteArrayBase64(false);
				}
				else if (type.Name == this.base64ID && (type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID))
				{
					result = this.ToByteArrayBase64(false);
				}
				else
				{
					result = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else if (type.Namespace == this.schemaNs2000ID || type.Namespace == this.schemaNs1999ID)
			{
				if (type.Name == this.stringID || type.Name == this.normalizedStringID)
				{
					result = this.ReadStringValue();
				}
				else if (type.Name == this.anyURIID || type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					result = this.CollapseWhitespace(this.ReadStringValue());
				}
				else if (type.Name == this.intID)
				{
					result = XmlConvert.ToInt32(this.ReadStringValue());
				}
				else if (type.Name == this.booleanID)
				{
					result = XmlConvert.ToBoolean(this.ReadStringValue());
				}
				else if (type.Name == this.shortID)
				{
					result = XmlConvert.ToInt16(this.ReadStringValue());
				}
				else if (type.Name == this.longID)
				{
					result = XmlConvert.ToInt64(this.ReadStringValue());
				}
				else if (type.Name == this.floatID)
				{
					result = XmlConvert.ToSingle(this.ReadStringValue());
				}
				else if (type.Name == this.doubleID)
				{
					result = XmlConvert.ToDouble(this.ReadStringValue());
				}
				else if (type.Name == this.oldDecimalID)
				{
					result = XmlConvert.ToDecimal(this.ReadStringValue());
				}
				else if (type.Name == this.oldTimeInstantID)
				{
					result = XmlSerializationReader.ToDateTime(this.ReadStringValue());
				}
				else if (type.Name == this.qnameID)
				{
					result = this.ReadXmlQualifiedName();
				}
				else if (type.Name == this.dateID)
				{
					result = XmlSerializationReader.ToDate(this.ReadStringValue());
				}
				else if (type.Name == this.timeID)
				{
					result = XmlSerializationReader.ToTime(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedByteID)
				{
					result = XmlConvert.ToByte(this.ReadStringValue());
				}
				else if (type.Name == this.byteID)
				{
					result = XmlConvert.ToSByte(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedShortID)
				{
					result = XmlConvert.ToUInt16(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedIntID)
				{
					result = XmlConvert.ToUInt32(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedLongID)
				{
					result = XmlConvert.ToUInt64(this.ReadStringValue());
				}
				else
				{
					result = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else if (type.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (type.Name == this.charID)
				{
					result = XmlSerializationReader.ToChar(this.ReadStringValue());
				}
				else if (type.Name == this.guidID)
				{
					result = new Guid(this.CollapseWhitespace(this.ReadStringValue()));
				}
				else if (type.Name == this.timeSpanID && LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					result = XmlConvert.ToTimeSpan(this.ReadStringValue());
				}
				else
				{
					result = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else
			{
				result = this.ReadXmlNodes(elementCanBeType);
			}
			return result;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000879B8 File Offset: 0x00085BB8
		protected object ReadTypedNull(XmlQualifiedName type)
		{
			this.InitPrimitiveIDs();
			if (!this.IsPrimitiveNamespace(type.Namespace) || type.Name == this.urTypeID)
			{
				return null;
			}
			object result;
			if (type.Namespace == this.schemaNsID || type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID)
			{
				if (type.Name == this.stringID || type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.normalizedStringID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					result = null;
				}
				else if (type.Name == this.intID)
				{
					result = null;
				}
				else if (type.Name == this.booleanID)
				{
					result = null;
				}
				else if (type.Name == this.shortID)
				{
					result = null;
				}
				else if (type.Name == this.longID)
				{
					result = null;
				}
				else if (type.Name == this.floatID)
				{
					result = null;
				}
				else if (type.Name == this.doubleID)
				{
					result = null;
				}
				else if (type.Name == this.decimalID)
				{
					result = null;
				}
				else if (type.Name == this.dateTimeID)
				{
					result = null;
				}
				else if (type.Name == this.qnameID)
				{
					result = null;
				}
				else if (type.Name == this.dateID)
				{
					result = null;
				}
				else if (type.Name == this.timeID)
				{
					result = null;
				}
				else if (type.Name == this.unsignedByteID)
				{
					result = null;
				}
				else if (type.Name == this.byteID)
				{
					result = null;
				}
				else if (type.Name == this.unsignedShortID)
				{
					result = null;
				}
				else if (type.Name == this.unsignedIntID)
				{
					result = null;
				}
				else if (type.Name == this.unsignedLongID)
				{
					result = null;
				}
				else if (type.Name == this.hexBinaryID)
				{
					result = null;
				}
				else if (type.Name == this.base64BinaryID)
				{
					result = null;
				}
				else if (type.Name == this.base64ID && (type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID))
				{
					result = null;
				}
				else
				{
					result = null;
				}
			}
			else if (type.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (type.Name == this.charID)
				{
					result = null;
				}
				else if (type.Name == this.guidID)
				{
					result = null;
				}
				else if (type.Name == this.timeSpanID && LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					result = null;
				}
				else
				{
					result = null;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00087DB9 File Offset: 0x00085FB9
		protected bool IsXmlnsAttribute(string name)
		{
			return name.StartsWith("xmlns", StringComparison.Ordinal) && (name.Length == 5 || name[5] == ':');
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00087DE4 File Offset: 0x00085FE4
		protected void ParseWsdlArrayType(XmlAttribute attr)
		{
			if (attr.LocalName == this.wsdlArrayTypeID && attr.NamespaceURI == this.wsdlNsID)
			{
				int num = attr.Value.LastIndexOf(':');
				if (num < 0)
				{
					attr.Value = this.r.LookupNamespace("") + ":" + attr.Value;
					return;
				}
				attr.Value = this.r.LookupNamespace(attr.Value.Substring(0, num)) + ":" + attr.Value.Substring(num + 1);
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00087E7C File Offset: 0x0008607C
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x00087E91 File Offset: 0x00086091
		protected bool IsReturnValue
		{
			get
			{
				return this.isReturnValue && !this.soap12;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00087E9C File Offset: 0x0008609C
		protected bool ReadNull()
		{
			if (!this.GetNullAttr())
			{
				return false;
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return true;
			}
			this.r.ReadStartElement();
			int num = 0;
			int readerCount = this.ReaderCount;
			while (this.r.NodeType != XmlNodeType.EndElement)
			{
				this.UnknownNode(null);
				this.CheckReaderCount(ref num, ref readerCount);
			}
			this.ReadEndElement();
			return true;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00087F0C File Offset: 0x0008610C
		protected bool GetNullAttr()
		{
			string attribute = this.r.GetAttribute(this.nilID, this.instanceNsID);
			if (attribute == null)
			{
				attribute = this.r.GetAttribute(this.nullID, this.instanceNsID);
			}
			if (attribute == null)
			{
				attribute = this.r.GetAttribute(this.nullID, this.instanceNs2000ID);
				if (attribute == null)
				{
					attribute = this.r.GetAttribute(this.nullID, this.instanceNs1999ID);
				}
			}
			return attribute != null && XmlConvert.ToBoolean(attribute);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00087F90 File Offset: 0x00086190
		protected string ReadNullableString()
		{
			if (this.ReadNull())
			{
				return null;
			}
			return this.r.ReadElementString();
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00087FA7 File Offset: 0x000861A7
		protected XmlQualifiedName ReadNullableQualifiedName()
		{
			if (this.ReadNull())
			{
				return null;
			}
			return this.ReadElementQualifiedName();
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00087FBC File Offset: 0x000861BC
		protected XmlQualifiedName ReadElementQualifiedName()
		{
			if (this.r.IsEmptyElement)
			{
				XmlQualifiedName result = new XmlQualifiedName(string.Empty, this.r.LookupNamespace(""));
				this.r.Skip();
				return result;
			}
			XmlQualifiedName result2 = this.ToXmlQualifiedName(this.CollapseWhitespace(this.r.ReadString()));
			this.r.ReadEndElement();
			return result2;
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00088024 File Offset: 0x00086224
		protected XmlDocument ReadXmlDocument(bool wrapped)
		{
			XmlNode xmlNode = this.ReadXmlNode(wrapped);
			if (xmlNode == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.ImportNode(xmlNode, true));
			return xmlDocument;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00088054 File Offset: 0x00086254
		protected string CollapseWhitespace(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Trim();
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00088064 File Offset: 0x00086264
		protected XmlNode ReadXmlNode(bool wrapped)
		{
			XmlNode result = null;
			if (wrapped)
			{
				if (this.ReadNull())
				{
					return null;
				}
				this.r.ReadStartElement();
				this.r.MoveToContent();
				if (this.r.NodeType != XmlNodeType.EndElement)
				{
					result = this.Document.ReadNode(this.r);
				}
				int num = 0;
				int readerCount = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					this.UnknownNode(null);
					this.CheckReaderCount(ref num, ref readerCount);
				}
				this.r.ReadEndElement();
			}
			else
			{
				result = this.Document.ReadNode(this.r);
			}
			return result;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00088103 File Offset: 0x00086303
		protected static byte[] ToByteArrayBase64(string value)
		{
			return XmlCustomFormatter.ToByteArrayBase64(value);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0008810B File Offset: 0x0008630B
		protected byte[] ToByteArrayBase64(bool isNull)
		{
			if (isNull)
			{
				return null;
			}
			return this.ReadByteArray(true);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00088119 File Offset: 0x00086319
		protected static byte[] ToByteArrayHex(string value)
		{
			return XmlCustomFormatter.ToByteArrayHex(value);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00088121 File Offset: 0x00086321
		protected byte[] ToByteArrayHex(bool isNull)
		{
			if (isNull)
			{
				return null;
			}
			return this.ReadByteArray(false);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x00088130 File Offset: 0x00086330
		protected int GetArrayLength(string name, string ns)
		{
			if (this.GetNullAttr())
			{
				return 0;
			}
			string attribute = this.r.GetAttribute(this.arrayTypeID, this.soapNsID);
			XmlSerializationReader.SoapArrayInfo soapArrayInfo = this.ParseArrayType(attribute);
			if (soapArrayInfo.dimensions != 1)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidArrayDimentions", new object[]
				{
					this.CurrentTag()
				}));
			}
			XmlQualifiedName xmlQualifiedName = this.ToXmlQualifiedName(soapArrayInfo.qname, false);
			if (xmlQualifiedName.Name != name)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidArrayTypeName", new object[]
				{
					xmlQualifiedName.Name,
					name,
					this.CurrentTag()
				}));
			}
			if (xmlQualifiedName.Namespace != ns)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidArrayTypeNamespace", new object[]
				{
					xmlQualifiedName.Namespace,
					ns,
					this.CurrentTag()
				}));
			}
			return soapArrayInfo.length;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00088218 File Offset: 0x00086418
		private XmlSerializationReader.SoapArrayInfo ParseArrayType(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(Res.GetString("XmlMissingArrayType", new object[]
				{
					this.CurrentTag()
				}));
			}
			if (value.Length == 0)
			{
				throw new ArgumentException(Res.GetString("XmlEmptyArrayType", new object[]
				{
					this.CurrentTag()
				}), "value");
			}
			char[] array = value.ToCharArray();
			int num = array.Length;
			XmlSerializationReader.SoapArrayInfo result = default(XmlSerializationReader.SoapArrayInfo);
			int num2 = num - 1;
			if (array[num2] != ']')
			{
				throw new ArgumentException(Res.GetString("XmlInvalidArraySyntax"), "value");
			}
			num2--;
			while (num2 != -1 && array[num2] != '[')
			{
				if (array[num2] == ',')
				{
					throw new ArgumentException(Res.GetString("XmlInvalidArrayDimentions", new object[]
					{
						this.CurrentTag()
					}), "value");
				}
				num2--;
			}
			if (num2 == -1)
			{
				throw new ArgumentException(Res.GetString("XmlMismatchedArrayBrackets"), "value");
			}
			int num3 = num - num2 - 2;
			if (num3 > 0)
			{
				string text = new string(array, num2 + 1, num3);
				try
				{
					result.length = int.Parse(text, CultureInfo.InvariantCulture);
					goto IL_14F;
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					throw new ArgumentException(Res.GetString("XmlInvalidArrayLength", new object[]
					{
						text
					}), "value");
				}
			}
			result.length = -1;
			IL_14F:
			num2--;
			result.jaggedDimensions = 0;
			while (num2 != -1 && array[num2] == ']')
			{
				num2--;
				if (num2 < 0)
				{
					throw new ArgumentException(Res.GetString("XmlMismatchedArrayBrackets"), "value");
				}
				if (array[num2] == ',')
				{
					throw new ArgumentException(Res.GetString("XmlInvalidArrayDimentions", new object[]
					{
						this.CurrentTag()
					}), "value");
				}
				if (array[num2] != '[')
				{
					throw new ArgumentException(Res.GetString("XmlInvalidArraySyntax"), "value");
				}
				num2--;
				result.jaggedDimensions++;
			}
			result.dimensions = 1;
			result.qname = new string(array, 0, num2 + 1);
			return result;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0008842C File Offset: 0x0008662C
		private XmlSerializationReader.SoapArrayInfo ParseSoap12ArrayType(string itemType, string arraySize)
		{
			XmlSerializationReader.SoapArrayInfo soapArrayInfo = default(XmlSerializationReader.SoapArrayInfo);
			if (itemType != null && itemType.Length > 0)
			{
				soapArrayInfo.qname = itemType;
			}
			else
			{
				soapArrayInfo.qname = "";
			}
			string[] array;
			if (arraySize != null && arraySize.Length > 0)
			{
				array = arraySize.Split(null);
			}
			else
			{
				array = new string[0];
			}
			soapArrayInfo.dimensions = 0;
			soapArrayInfo.length = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length > 0)
				{
					if (array[i] == "*")
					{
						soapArrayInfo.dimensions++;
					}
					else
					{
						try
						{
							soapArrayInfo.length = int.Parse(array[i], CultureInfo.InvariantCulture);
							soapArrayInfo.dimensions++;
						}
						catch (Exception ex)
						{
							if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
							{
								throw;
							}
							throw new ArgumentException(Res.GetString("XmlInvalidArrayLength", new object[]
							{
								array[i]
							}), "value");
						}
					}
				}
			}
			if (soapArrayInfo.dimensions == 0)
			{
				soapArrayInfo.dimensions = 1;
			}
			return soapArrayInfo;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0008854C File Offset: 0x0008674C
		protected static DateTime ToDateTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00088554 File Offset: 0x00086754
		protected static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDate(value);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0008855C File Offset: 0x0008675C
		protected static DateTime ToTime(string value)
		{
			return XmlCustomFormatter.ToTime(value);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00088564 File Offset: 0x00086764
		protected static char ToChar(string value)
		{
			return XmlCustomFormatter.ToChar(value);
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0008856C File Offset: 0x0008676C
		protected static long ToEnum(string value, Hashtable h, string typeName)
		{
			return XmlCustomFormatter.ToEnum(value, h, typeName, true);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00088577 File Offset: 0x00086777
		protected static string ToXmlName(string value)
		{
			return XmlCustomFormatter.ToXmlName(value);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0008857F File Offset: 0x0008677F
		protected static string ToXmlNCName(string value)
		{
			return XmlCustomFormatter.ToXmlNCName(value);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00088587 File Offset: 0x00086787
		protected static string ToXmlNmToken(string value)
		{
			return XmlCustomFormatter.ToXmlNmToken(value);
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0008858F File Offset: 0x0008678F
		protected static string ToXmlNmTokens(string value)
		{
			return XmlCustomFormatter.ToXmlNmTokens(value);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00088597 File Offset: 0x00086797
		protected XmlQualifiedName ToXmlQualifiedName(string value)
		{
			return this.ToXmlQualifiedName(value, this.DecodeName);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000885A8 File Offset: 0x000867A8
		internal XmlQualifiedName ToXmlQualifiedName(string value, bool decodeName)
		{
			int num = (value == null) ? -1 : value.LastIndexOf(':');
			string text = (num < 0) ? null : value.Substring(0, num);
			string text2 = value.Substring(num + 1);
			if (decodeName)
			{
				text = XmlConvert.DecodeName(text);
				text2 = XmlConvert.DecodeName(text2);
			}
			if (text == null || text.Length == 0)
			{
				return new XmlQualifiedName(this.r.NameTable.Add(value), this.r.LookupNamespace(string.Empty));
			}
			string text3 = this.r.LookupNamespace(text);
			if (text3 == null)
			{
				throw new InvalidOperationException(Res.GetString("XmlUndefinedAlias", new object[]
				{
					text
				}));
			}
			return new XmlQualifiedName(this.r.NameTable.Add(text2), text3);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00088662 File Offset: 0x00086862
		protected void UnknownAttribute(object o, XmlAttribute attr)
		{
			this.UnknownAttribute(o, attr, null);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00088670 File Offset: 0x00086870
		protected void UnknownAttribute(object o, XmlAttribute attr, string qnames)
		{
			if (this.events.OnUnknownAttribute != null)
			{
				int lineNumber;
				int linePosition;
				this.GetCurrentPosition(out lineNumber, out linePosition);
				XmlAttributeEventArgs e = new XmlAttributeEventArgs(attr, lineNumber, linePosition, o, qnames);
				this.events.OnUnknownAttribute(this.events.sender, e);
			}
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000886BB File Offset: 0x000868BB
		protected void UnknownElement(object o, XmlElement elem)
		{
			this.UnknownElement(o, elem, null);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000886C8 File Offset: 0x000868C8
		protected void UnknownElement(object o, XmlElement elem, string qnames)
		{
			if (this.events.OnUnknownElement != null)
			{
				int lineNumber;
				int linePosition;
				this.GetCurrentPosition(out lineNumber, out linePosition);
				XmlElementEventArgs e = new XmlElementEventArgs(elem, lineNumber, linePosition, o, qnames);
				this.events.OnUnknownElement(this.events.sender, e);
			}
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00088713 File Offset: 0x00086913
		protected void UnknownNode(object o)
		{
			this.UnknownNode(o, null);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00088720 File Offset: 0x00086920
		protected void UnknownNode(object o, string qnames)
		{
			if (this.r.NodeType == XmlNodeType.None || this.r.NodeType == XmlNodeType.Whitespace)
			{
				this.r.Read();
				return;
			}
			if (this.r.NodeType == XmlNodeType.EndElement)
			{
				return;
			}
			if (this.events.OnUnknownNode != null)
			{
				this.UnknownNode(this.Document.ReadNode(this.r), o, qnames);
				return;
			}
			if (this.r.NodeType == XmlNodeType.Attribute && this.events.OnUnknownAttribute == null)
			{
				return;
			}
			if (this.r.NodeType == XmlNodeType.Element && this.events.OnUnknownElement == null)
			{
				this.r.Skip();
				return;
			}
			this.UnknownNode(this.Document.ReadNode(this.r), o, qnames);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000887EC File Offset: 0x000869EC
		private void UnknownNode(XmlNode unknownNode, object o, string qnames)
		{
			if (unknownNode == null)
			{
				return;
			}
			if (unknownNode.NodeType != XmlNodeType.None && unknownNode.NodeType != XmlNodeType.Whitespace && this.events.OnUnknownNode != null)
			{
				int lineNumber;
				int linePosition;
				this.GetCurrentPosition(out lineNumber, out linePosition);
				XmlNodeEventArgs e = new XmlNodeEventArgs(unknownNode, lineNumber, linePosition, o);
				this.events.OnUnknownNode(this.events.sender, e);
			}
			if (unknownNode.NodeType == XmlNodeType.Attribute)
			{
				this.UnknownAttribute(o, (XmlAttribute)unknownNode, qnames);
				return;
			}
			if (unknownNode.NodeType == XmlNodeType.Element)
			{
				this.UnknownElement(o, (XmlElement)unknownNode, qnames);
			}
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0008887C File Offset: 0x00086A7C
		private void GetCurrentPosition(out int lineNumber, out int linePosition)
		{
			if (this.Reader is IXmlLineInfo)
			{
				IXmlLineInfo xmlLineInfo = (IXmlLineInfo)this.Reader;
				lineNumber = xmlLineInfo.LineNumber;
				linePosition = xmlLineInfo.LinePosition;
				return;
			}
			lineNumber = (linePosition = -1);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000888BC File Offset: 0x00086ABC
		protected void UnreferencedObject(string id, object o)
		{
			if (this.events.OnUnreferencedObject != null)
			{
				UnreferencedObjectEventArgs e = new UnreferencedObjectEventArgs(o, id);
				this.events.OnUnreferencedObject(this.events.sender, e);
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000888FC File Offset: 0x00086AFC
		private string CurrentTag()
		{
			XmlNodeType nodeType = this.r.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				return string.Concat(new string[]
				{
					"<",
					this.r.LocalName,
					" xmlns='",
					this.r.NamespaceURI,
					"'>"
				});
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
				break;
			case XmlNodeType.Text:
				return this.r.Value;
			case XmlNodeType.CDATA:
				return "CDATA";
			case XmlNodeType.ProcessingInstruction:
				return "<?";
			case XmlNodeType.Comment:
				return "<--";
			default:
				if (nodeType == XmlNodeType.EndElement)
				{
					return ">";
				}
				break;
			}
			return "(unknown)";
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000889AD File Offset: 0x00086BAD
		protected Exception CreateUnknownTypeException(XmlQualifiedName type)
		{
			return new InvalidOperationException(Res.GetString("XmlUnknownType", new object[]
			{
				type.Name,
				type.Namespace,
				this.CurrentTag()
			}));
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000889DF File Offset: 0x00086BDF
		protected Exception CreateReadOnlyCollectionException(string name)
		{
			return new InvalidOperationException(Res.GetString("XmlReadOnlyCollection", new object[]
			{
				name
			}));
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000889FA File Offset: 0x00086BFA
		protected Exception CreateAbstractTypeException(string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("XmlAbstractType", new object[]
			{
				name,
				ns,
				this.CurrentTag()
			}));
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00088A22 File Offset: 0x00086C22
		protected Exception CreateInaccessibleConstructorException(string typeName)
		{
			return new InvalidOperationException(Res.GetString("XmlConstructorInaccessible", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00088A3D File Offset: 0x00086C3D
		protected Exception CreateCtorHasSecurityException(string typeName)
		{
			return new InvalidOperationException(Res.GetString("XmlConstructorHasSecurityAttributes", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00088A58 File Offset: 0x00086C58
		protected Exception CreateUnknownNodeException()
		{
			return new InvalidOperationException(Res.GetString("XmlUnknownNode", new object[]
			{
				this.CurrentTag()
			}));
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00088A78 File Offset: 0x00086C78
		protected Exception CreateUnknownConstantException(string value, Type enumType)
		{
			return new InvalidOperationException(Res.GetString("XmlUnknownConstant", new object[]
			{
				value,
				enumType.Name
			}));
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00088A9C File Offset: 0x00086C9C
		protected Exception CreateInvalidCastException(Type type, object value)
		{
			return this.CreateInvalidCastException(type, value, null);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00088AA8 File Offset: 0x00086CA8
		protected Exception CreateInvalidCastException(Type type, object value, string id)
		{
			if (value == null)
			{
				return new InvalidCastException(Res.GetString("XmlInvalidNullCast", new object[]
				{
					type.FullName
				}));
			}
			if (id == null)
			{
				return new InvalidCastException(Res.GetString("XmlInvalidCast", new object[]
				{
					value.GetType().FullName,
					type.FullName
				}));
			}
			return new InvalidCastException(Res.GetString("XmlInvalidCastWithId", new object[]
			{
				value.GetType().FullName,
				type.FullName,
				id
			}));
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00088B37 File Offset: 0x00086D37
		protected Exception CreateBadDerivationException(string xsdDerived, string nsDerived, string xsdBase, string nsBase, string clrDerived, string clrBase)
		{
			return new InvalidOperationException(Res.GetString("XmlSerializableBadDerivation", new object[]
			{
				xsdDerived,
				nsDerived,
				xsdBase,
				nsBase,
				clrDerived,
				clrBase
			}));
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00088B69 File Offset: 0x00086D69
		protected Exception CreateMissingIXmlSerializableType(string name, string ns, string clrType)
		{
			return new InvalidOperationException(Res.GetString("XmlSerializableMissingClrType", new object[]
			{
				name,
				ns,
				typeof(XmlIncludeAttribute).Name,
				clrType
			}));
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00088BA0 File Offset: 0x00086DA0
		protected Array EnsureArrayIndex(Array a, int index, Type elementType)
		{
			if (a == null)
			{
				return Array.CreateInstance(elementType, 32);
			}
			if (index < a.Length)
			{
				return a;
			}
			Array array = Array.CreateInstance(elementType, a.Length * 2);
			Array.Copy(a, array, index);
			return array;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00088BDC File Offset: 0x00086DDC
		protected Array ShrinkArray(Array a, int length, Type elementType, bool isNullable)
		{
			if (a == null)
			{
				if (isNullable)
				{
					return null;
				}
				return Array.CreateInstance(elementType, 0);
			}
			else
			{
				if (a.Length == length)
				{
					return a;
				}
				Array array = Array.CreateInstance(elementType, length);
				Array.Copy(a, array, length);
				return array;
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00088C16 File Offset: 0x00086E16
		protected string ReadString(string value)
		{
			return this.ReadString(value, false);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00088C20 File Offset: 0x00086E20
		protected string ReadString(string value, bool trim)
		{
			string text = this.r.ReadString();
			if (text != null && trim)
			{
				text = text.Trim();
			}
			if (value == null || value.Length == 0)
			{
				return text;
			}
			return value + text;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00088C5C File Offset: 0x00086E5C
		protected IXmlSerializable ReadSerializable(IXmlSerializable serializable)
		{
			return this.ReadSerializable(serializable, false);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00088C68 File Offset: 0x00086E68
		protected IXmlSerializable ReadSerializable(IXmlSerializable serializable, bool wrappedAny)
		{
			string b = null;
			string b2 = null;
			if (wrappedAny)
			{
				b = this.r.LocalName;
				b2 = this.r.NamespaceURI;
				this.r.Read();
				this.r.MoveToContent();
			}
			serializable.ReadXml(this.r);
			if (wrappedAny)
			{
				while (this.r.NodeType == XmlNodeType.Whitespace)
				{
					this.r.Skip();
				}
				if (this.r.NodeType == XmlNodeType.None)
				{
					this.r.Skip();
				}
				if (this.r.NodeType == XmlNodeType.EndElement && this.r.LocalName == b && this.r.NamespaceURI == b2)
				{
					this.Reader.Read();
				}
			}
			return serializable;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00088D34 File Offset: 0x00086F34
		protected bool ReadReference(out string fixupReference)
		{
			string text = this.soap12 ? this.r.GetAttribute("ref", "http://www.w3.org/2003/05/soap-encoding") : this.r.GetAttribute("href");
			if (text == null)
			{
				fixupReference = null;
				return false;
			}
			if (!this.soap12)
			{
				if (!text.StartsWith("#", StringComparison.Ordinal))
				{
					throw new InvalidOperationException(Res.GetString("XmlMissingHref", new object[]
					{
						text
					}));
				}
				fixupReference = text.Substring(1);
			}
			else
			{
				fixupReference = text;
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
			}
			else
			{
				this.r.ReadStartElement();
				this.ReadEndElement();
			}
			return true;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00088DE4 File Offset: 0x00086FE4
		protected void AddTarget(string id, object o)
		{
			if (id == null)
			{
				if (this.targetsWithoutIds == null)
				{
					this.targetsWithoutIds = new ArrayList();
				}
				if (o != null)
				{
					this.targetsWithoutIds.Add(o);
					return;
				}
			}
			else
			{
				if (this.targets == null)
				{
					this.targets = new Hashtable();
				}
				if (!this.targets.Contains(id))
				{
					this.targets.Add(id, o);
				}
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00088E46 File Offset: 0x00087046
		protected void AddFixup(XmlSerializationReader.Fixup fixup)
		{
			if (this.fixups == null)
			{
				this.fixups = new ArrayList();
			}
			this.fixups.Add(fixup);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00088E68 File Offset: 0x00087068
		protected void AddFixup(XmlSerializationReader.CollectionFixup fixup)
		{
			if (this.collectionFixups == null)
			{
				this.collectionFixups = new ArrayList();
			}
			this.collectionFixups.Add(fixup);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00088E8C File Offset: 0x0008708C
		protected object GetTarget(string id)
		{
			object obj = (this.targets != null) ? this.targets[id] : null;
			if (obj == null)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidHref", new object[]
				{
					id
				}));
			}
			this.Referenced(obj);
			return obj;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00088ED6 File Offset: 0x000870D6
		protected void Referenced(object o)
		{
			if (o == null)
			{
				return;
			}
			if (this.referencedTargets == null)
			{
				this.referencedTargets = new Hashtable();
			}
			this.referencedTargets[o] = o;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00088EFC File Offset: 0x000870FC
		private void HandleUnreferencedObjects()
		{
			if (this.targets != null)
			{
				foreach (object obj in this.targets)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (this.referencedTargets == null || !this.referencedTargets.Contains(dictionaryEntry.Value))
					{
						this.UnreferencedObject((string)dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
			}
			if (this.targetsWithoutIds != null)
			{
				foreach (object obj2 in this.targetsWithoutIds)
				{
					if (this.referencedTargets == null || !this.referencedTargets.Contains(obj2))
					{
						this.UnreferencedObject(null, obj2);
					}
				}
			}
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00088FF0 File Offset: 0x000871F0
		private void DoFixups()
		{
			if (this.fixups == null)
			{
				return;
			}
			for (int i = 0; i < this.fixups.Count; i++)
			{
				XmlSerializationReader.Fixup fixup = (XmlSerializationReader.Fixup)this.fixups[i];
				fixup.Callback(fixup);
			}
			if (this.collectionFixups == null)
			{
				return;
			}
			for (int j = 0; j < this.collectionFixups.Count; j++)
			{
				XmlSerializationReader.CollectionFixup collectionFixup = (XmlSerializationReader.CollectionFixup)this.collectionFixups[j];
				collectionFixup.Callback(collectionFixup.Collection, collectionFixup.CollectionItems);
			}
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00089084 File Offset: 0x00087284
		protected void FixupArrayRefs(object fixup)
		{
			XmlSerializationReader.Fixup fixup2 = (XmlSerializationReader.Fixup)fixup;
			Array array = (Array)fixup2.Source;
			for (int i = 0; i < array.Length; i++)
			{
				string text = fixup2.Ids[i];
				if (text != null)
				{
					object target = this.GetTarget(text);
					try
					{
						array.SetValue(target, i);
					}
					catch (InvalidCastException)
					{
						throw new InvalidOperationException(Res.GetString("XmlInvalidArrayRef", new object[]
						{
							text,
							target.GetType().FullName,
							i.ToString(CultureInfo.InvariantCulture)
						}));
					}
				}
			}
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00089120 File Offset: 0x00087320
		private object ReadArray(string typeName, string typeNs)
		{
			Type type = null;
			XmlSerializationReader.SoapArrayInfo soapArrayInfo;
			if (this.soap12)
			{
				string attribute = this.r.GetAttribute(this.itemTypeID, this.soap12NsID);
				string attribute2 = this.r.GetAttribute(this.arraySizeID, this.soap12NsID);
				Type type2 = (Type)this.types[new XmlQualifiedName(typeName, typeNs)];
				if (attribute == null && attribute2 == null && (type2 == null || !type2.IsArray))
				{
					return null;
				}
				soapArrayInfo = this.ParseSoap12ArrayType(attribute, attribute2);
				if (type2 != null)
				{
					type = TypeScope.GetArrayElementType(type2, null);
				}
			}
			else
			{
				string attribute3 = this.r.GetAttribute(this.arrayTypeID, this.soapNsID);
				if (attribute3 == null)
				{
					return null;
				}
				soapArrayInfo = this.ParseArrayType(attribute3);
			}
			if (soapArrayInfo.dimensions != 1)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidArrayDimentions", new object[]
				{
					this.CurrentTag()
				}));
			}
			Type type3 = null;
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.urTypeID, this.schemaNsID);
			XmlQualifiedName xmlQualifiedName2;
			if (soapArrayInfo.qname.Length > 0)
			{
				xmlQualifiedName2 = this.ToXmlQualifiedName(soapArrayInfo.qname, false);
				type3 = (Type)this.types[xmlQualifiedName2];
			}
			else
			{
				xmlQualifiedName2 = xmlQualifiedName;
			}
			if (this.soap12 && type3 == typeof(object))
			{
				type3 = null;
			}
			bool flag;
			if (type3 == null)
			{
				if (!this.soap12)
				{
					type3 = this.GetPrimitiveType(xmlQualifiedName2, true);
					flag = true;
				}
				else
				{
					if (xmlQualifiedName2 != xmlQualifiedName)
					{
						type3 = this.GetPrimitiveType(xmlQualifiedName2, false);
					}
					if (type3 != null)
					{
						flag = true;
					}
					else if (type == null)
					{
						type3 = typeof(object);
						flag = false;
					}
					else
					{
						type3 = type;
						XmlQualifiedName xmlQualifiedName3 = (XmlQualifiedName)this.typesReverse[type3];
						if (xmlQualifiedName3 == null)
						{
							xmlQualifiedName3 = XmlSerializationWriter.GetPrimitiveTypeNameInternal(type3);
							flag = true;
						}
						else
						{
							flag = type3.IsPrimitive;
						}
						if (xmlQualifiedName3 != null)
						{
							xmlQualifiedName2 = xmlQualifiedName3;
						}
					}
				}
			}
			else
			{
				flag = type3.IsPrimitive;
			}
			if (!this.soap12 && soapArrayInfo.jaggedDimensions > 0)
			{
				for (int i = 0; i < soapArrayInfo.jaggedDimensions; i++)
				{
					type3 = type3.MakeArrayType();
				}
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return Array.CreateInstance(type3, 0);
			}
			this.r.ReadStartElement();
			this.r.MoveToContent();
			int num = 0;
			Array array = null;
			if (type3.IsValueType)
			{
				if (!flag && !type3.IsEnum)
				{
					throw new NotSupportedException(Res.GetString("XmlRpcArrayOfValueTypes", new object[]
					{
						type3.FullName
					}));
				}
				int num2 = 0;
				int readerCount = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					array = this.EnsureArrayIndex(array, num, type3);
					array.SetValue(this.ReadReferencedElement(xmlQualifiedName2.Name, xmlQualifiedName2.Namespace), num);
					num++;
					this.r.MoveToContent();
					this.CheckReaderCount(ref num2, ref readerCount);
				}
				array = this.ShrinkArray(array, num, type3, false);
			}
			else
			{
				string[] array2 = null;
				int num3 = 0;
				int num4 = 0;
				int readerCount2 = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					array = this.EnsureArrayIndex(array, num, type3);
					array2 = (string[])this.EnsureArrayIndex(array2, num3, typeof(string));
					string name;
					string ns;
					if (this.r.NamespaceURI.Length != 0)
					{
						name = this.r.LocalName;
						if (this.r.NamespaceURI == this.soapNsID)
						{
							ns = "http://www.w3.org/2001/XMLSchema";
						}
						else
						{
							ns = this.r.NamespaceURI;
						}
					}
					else
					{
						name = xmlQualifiedName2.Name;
						ns = xmlQualifiedName2.Namespace;
					}
					array.SetValue(this.ReadReferencingElement(name, ns, out array2[num3]), num);
					num++;
					num3++;
					this.r.MoveToContent();
					this.CheckReaderCount(ref num4, ref readerCount2);
				}
				if (this.soap12 && type3 == typeof(object))
				{
					Type type4 = null;
					for (int j = 0; j < num; j++)
					{
						object value = array.GetValue(j);
						if (value != null)
						{
							Type type5 = value.GetType();
							if (type5.IsValueType)
							{
								type4 = null;
								break;
							}
							if (type4 == null || type5.IsAssignableFrom(type4))
							{
								type4 = type5;
							}
							else if (!type4.IsAssignableFrom(type5))
							{
								type4 = null;
								break;
							}
						}
					}
					if (type4 != null)
					{
						type3 = type4;
					}
				}
				array2 = (string[])this.ShrinkArray(array2, num3, typeof(string), false);
				array = this.ShrinkArray(array, num, type3, false);
				XmlSerializationReader.Fixup fixup = new XmlSerializationReader.Fixup(array, new XmlSerializationFixupCallback(this.FixupArrayRefs), array2);
				this.AddFixup(fixup);
			}
			this.ReadEndElement();
			return array;
		}

		// Token: 0x06001CBB RID: 7355
		protected abstract void InitCallbacks();

		// Token: 0x06001CBC RID: 7356 RVA: 0x00089610 File Offset: 0x00087810
		protected void ReadReferencedElements()
		{
			this.r.MoveToContent();
			int num = 0;
			int readerCount = this.ReaderCount;
			while (this.r.NodeType != XmlNodeType.EndElement && this.r.NodeType != XmlNodeType.None)
			{
				string text;
				this.ReadReferencingElement(null, null, true, out text);
				this.r.MoveToContent();
				this.CheckReaderCount(ref num, ref readerCount);
			}
			this.DoFixups();
			this.HandleUnreferencedObjects();
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0008967E File Offset: 0x0008787E
		protected object ReadReferencedElement()
		{
			return this.ReadReferencedElement(null, null);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00089688 File Offset: 0x00087888
		protected object ReadReferencedElement(string name, string ns)
		{
			string text;
			return this.ReadReferencingElement(name, ns, out text);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0008969F File Offset: 0x0008789F
		protected object ReadReferencingElement(out string fixupReference)
		{
			return this.ReadReferencingElement(null, null, out fixupReference);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x000896AA File Offset: 0x000878AA
		protected object ReadReferencingElement(string name, string ns, out string fixupReference)
		{
			return this.ReadReferencingElement(name, ns, false, out fixupReference);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000896B8 File Offset: 0x000878B8
		protected object ReadReferencingElement(string name, string ns, bool elementCanBeType, out string fixupReference)
		{
			if (this.callbacks == null)
			{
				this.callbacks = new Hashtable();
				this.types = new Hashtable();
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.urTypeID, this.r.NameTable.Add("http://www.w3.org/2001/XMLSchema"));
				this.types.Add(xmlQualifiedName, typeof(object));
				this.typesReverse = new Hashtable();
				this.typesReverse.Add(typeof(object), xmlQualifiedName);
				this.InitCallbacks();
			}
			this.r.MoveToContent();
			if (this.ReadReference(out fixupReference))
			{
				return null;
			}
			if (this.ReadNull())
			{
				return null;
			}
			string id = this.soap12 ? this.r.GetAttribute("id", "http://www.w3.org/2003/05/soap-encoding") : this.r.GetAttribute("id", null);
			object obj;
			if ((obj = this.ReadArray(name, ns)) == null)
			{
				XmlQualifiedName xmlQualifiedName2 = this.GetXsiType();
				if (xmlQualifiedName2 == null)
				{
					if (name == null)
					{
						xmlQualifiedName2 = new XmlQualifiedName(this.r.NameTable.Add(this.r.LocalName), this.r.NameTable.Add(this.r.NamespaceURI));
					}
					else
					{
						xmlQualifiedName2 = new XmlQualifiedName(this.r.NameTable.Add(name), this.r.NameTable.Add(ns));
					}
				}
				XmlSerializationReadCallback xmlSerializationReadCallback = (XmlSerializationReadCallback)this.callbacks[xmlQualifiedName2];
				if (xmlSerializationReadCallback != null)
				{
					obj = xmlSerializationReadCallback();
				}
				else
				{
					obj = this.ReadTypedPrimitive(xmlQualifiedName2, elementCanBeType);
				}
			}
			this.AddTarget(id, obj);
			return obj;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00089854 File Offset: 0x00087A54
		protected void AddReadCallback(string name, string ns, Type type, XmlSerializationReadCallback read)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.r.NameTable.Add(name), this.r.NameTable.Add(ns));
			this.callbacks[xmlQualifiedName] = read;
			this.types[xmlQualifiedName] = type;
			this.typesReverse[type] = xmlQualifiedName;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000898B4 File Offset: 0x00087AB4
		protected void ReadEndElement()
		{
			while (this.r.NodeType == XmlNodeType.Whitespace)
			{
				this.r.Skip();
			}
			if (this.r.NodeType == XmlNodeType.None)
			{
				this.r.Skip();
				return;
			}
			this.r.ReadEndElement();
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00089904 File Offset: 0x00087B04
		private object ReadXmlNodes(bool elementCanBeType)
		{
			ArrayList arrayList = new ArrayList();
			string localName = this.Reader.LocalName;
			string namespaceURI = this.Reader.NamespaceURI;
			string name = this.Reader.Name;
			string text = null;
			string text2 = null;
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			XmlNode xmlNode;
			if (this.Reader.NodeType == XmlNodeType.Attribute)
			{
				XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, namespaceURI);
				xmlAttribute.Value = this.Reader.Value;
				xmlNode = xmlAttribute;
			}
			else
			{
				xmlNode = this.Document.CreateElement(name, namespaceURI);
			}
			this.GetCurrentPosition(out num2, out num3);
			XmlElement xmlElement = xmlNode as XmlElement;
			while (this.Reader.MoveToNextAttribute())
			{
				if (this.IsXmlnsAttribute(this.Reader.Name) || (this.Reader.Name == "id" && (!this.soap12 || this.Reader.NamespaceURI == "http://www.w3.org/2003/05/soap-encoding")))
				{
					num++;
				}
				if (this.Reader.LocalName == this.typeID && (this.Reader.NamespaceURI == this.instanceNsID || this.Reader.NamespaceURI == this.instanceNs2000ID || this.Reader.NamespaceURI == this.instanceNs1999ID))
				{
					string value = this.Reader.Value;
					int num4 = value.LastIndexOf(':');
					text = ((num4 >= 0) ? value.Substring(num4 + 1) : value);
					text2 = this.Reader.LookupNamespace((num4 >= 0) ? value.Substring(0, num4) : "");
				}
				XmlAttribute xmlAttribute2 = (XmlAttribute)this.Document.ReadNode(this.r);
				arrayList.Add(xmlAttribute2);
				if (xmlElement != null)
				{
					xmlElement.SetAttributeNode(xmlAttribute2);
				}
			}
			if (elementCanBeType && text == null)
			{
				text = localName;
				text2 = namespaceURI;
				XmlAttribute xmlAttribute3 = this.Document.CreateAttribute(this.typeID, this.instanceNsID);
				xmlAttribute3.Value = name;
				arrayList.Add(xmlAttribute3);
			}
			if (text == "anyType" && (text2 == this.schemaNsID || text2 == this.schemaNs1999ID || text2 == this.schemaNs2000ID))
			{
				num++;
			}
			this.Reader.MoveToElement();
			if (this.Reader.IsEmptyElement)
			{
				this.Reader.Skip();
			}
			else
			{
				this.Reader.ReadStartElement();
				this.Reader.MoveToContent();
				int num5 = 0;
				int readerCount = this.ReaderCount;
				while (this.Reader.NodeType != XmlNodeType.EndElement)
				{
					XmlNode xmlNode2 = this.Document.ReadNode(this.r);
					arrayList.Add(xmlNode2);
					if (xmlElement != null)
					{
						xmlElement.AppendChild(xmlNode2);
					}
					this.Reader.MoveToContent();
					this.CheckReaderCount(ref num5, ref readerCount);
				}
				this.ReadEndElement();
			}
			if (arrayList.Count <= num)
			{
				return new object();
			}
			XmlNode[] result = (XmlNode[])arrayList.ToArray(typeof(XmlNode));
			this.UnknownNode(xmlNode, null, null);
			return result;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00089C12 File Offset: 0x00087E12
		protected void CheckReaderCount(ref int whileIterations, ref int readerCount)
		{
			if (XmlSerializationReader.checkDeserializeAdvances)
			{
				whileIterations++;
				if ((whileIterations & 128) == 128)
				{
					if (readerCount == this.ReaderCount)
					{
						throw new InvalidOperationException(Res.GetString("XmlInternalErrorReaderAdvance"));
					}
					readerCount = this.ReaderCount;
				}
			}
		}

		// Token: 0x04000C5E RID: 3166
		private XmlReader r;

		// Token: 0x04000C5F RID: 3167
		private XmlCountingReader countingReader;

		// Token: 0x04000C60 RID: 3168
		private XmlDocument d;

		// Token: 0x04000C61 RID: 3169
		private Hashtable callbacks;

		// Token: 0x04000C62 RID: 3170
		private Hashtable types;

		// Token: 0x04000C63 RID: 3171
		private Hashtable typesReverse;

		// Token: 0x04000C64 RID: 3172
		private XmlDeserializationEvents events;

		// Token: 0x04000C65 RID: 3173
		private Hashtable targets;

		// Token: 0x04000C66 RID: 3174
		private Hashtable referencedTargets;

		// Token: 0x04000C67 RID: 3175
		private ArrayList targetsWithoutIds;

		// Token: 0x04000C68 RID: 3176
		private ArrayList fixups;

		// Token: 0x04000C69 RID: 3177
		private ArrayList collectionFixups;

		// Token: 0x04000C6A RID: 3178
		private bool soap12;

		// Token: 0x04000C6B RID: 3179
		private bool isReturnValue;

		// Token: 0x04000C6C RID: 3180
		private bool decodeName = true;

		// Token: 0x04000C6D RID: 3181
		private string schemaNsID;

		// Token: 0x04000C6E RID: 3182
		private string schemaNs1999ID;

		// Token: 0x04000C6F RID: 3183
		private string schemaNs2000ID;

		// Token: 0x04000C70 RID: 3184
		private string schemaNonXsdTypesNsID;

		// Token: 0x04000C71 RID: 3185
		private string instanceNsID;

		// Token: 0x04000C72 RID: 3186
		private string instanceNs2000ID;

		// Token: 0x04000C73 RID: 3187
		private string instanceNs1999ID;

		// Token: 0x04000C74 RID: 3188
		private string soapNsID;

		// Token: 0x04000C75 RID: 3189
		private string soap12NsID;

		// Token: 0x04000C76 RID: 3190
		private string schemaID;

		// Token: 0x04000C77 RID: 3191
		private string wsdlNsID;

		// Token: 0x04000C78 RID: 3192
		private string wsdlArrayTypeID;

		// Token: 0x04000C79 RID: 3193
		private string nullID;

		// Token: 0x04000C7A RID: 3194
		private string nilID;

		// Token: 0x04000C7B RID: 3195
		private string typeID;

		// Token: 0x04000C7C RID: 3196
		private string arrayTypeID;

		// Token: 0x04000C7D RID: 3197
		private string itemTypeID;

		// Token: 0x04000C7E RID: 3198
		private string arraySizeID;

		// Token: 0x04000C7F RID: 3199
		private string arrayID;

		// Token: 0x04000C80 RID: 3200
		private string urTypeID;

		// Token: 0x04000C81 RID: 3201
		private string stringID;

		// Token: 0x04000C82 RID: 3202
		private string intID;

		// Token: 0x04000C83 RID: 3203
		private string booleanID;

		// Token: 0x04000C84 RID: 3204
		private string shortID;

		// Token: 0x04000C85 RID: 3205
		private string longID;

		// Token: 0x04000C86 RID: 3206
		private string floatID;

		// Token: 0x04000C87 RID: 3207
		private string doubleID;

		// Token: 0x04000C88 RID: 3208
		private string decimalID;

		// Token: 0x04000C89 RID: 3209
		private string dateTimeID;

		// Token: 0x04000C8A RID: 3210
		private string qnameID;

		// Token: 0x04000C8B RID: 3211
		private string dateID;

		// Token: 0x04000C8C RID: 3212
		private string timeID;

		// Token: 0x04000C8D RID: 3213
		private string hexBinaryID;

		// Token: 0x04000C8E RID: 3214
		private string base64BinaryID;

		// Token: 0x04000C8F RID: 3215
		private string base64ID;

		// Token: 0x04000C90 RID: 3216
		private string unsignedByteID;

		// Token: 0x04000C91 RID: 3217
		private string byteID;

		// Token: 0x04000C92 RID: 3218
		private string unsignedShortID;

		// Token: 0x04000C93 RID: 3219
		private string unsignedIntID;

		// Token: 0x04000C94 RID: 3220
		private string unsignedLongID;

		// Token: 0x04000C95 RID: 3221
		private string oldDecimalID;

		// Token: 0x04000C96 RID: 3222
		private string oldTimeInstantID;

		// Token: 0x04000C97 RID: 3223
		private string anyURIID;

		// Token: 0x04000C98 RID: 3224
		private string durationID;

		// Token: 0x04000C99 RID: 3225
		private string ENTITYID;

		// Token: 0x04000C9A RID: 3226
		private string ENTITIESID;

		// Token: 0x04000C9B RID: 3227
		private string gDayID;

		// Token: 0x04000C9C RID: 3228
		private string gMonthID;

		// Token: 0x04000C9D RID: 3229
		private string gMonthDayID;

		// Token: 0x04000C9E RID: 3230
		private string gYearID;

		// Token: 0x04000C9F RID: 3231
		private string gYearMonthID;

		// Token: 0x04000CA0 RID: 3232
		private string IDID;

		// Token: 0x04000CA1 RID: 3233
		private string IDREFID;

		// Token: 0x04000CA2 RID: 3234
		private string IDREFSID;

		// Token: 0x04000CA3 RID: 3235
		private string integerID;

		// Token: 0x04000CA4 RID: 3236
		private string languageID;

		// Token: 0x04000CA5 RID: 3237
		private string NameID;

		// Token: 0x04000CA6 RID: 3238
		private string NCNameID;

		// Token: 0x04000CA7 RID: 3239
		private string NMTOKENID;

		// Token: 0x04000CA8 RID: 3240
		private string NMTOKENSID;

		// Token: 0x04000CA9 RID: 3241
		private string negativeIntegerID;

		// Token: 0x04000CAA RID: 3242
		private string nonPositiveIntegerID;

		// Token: 0x04000CAB RID: 3243
		private string nonNegativeIntegerID;

		// Token: 0x04000CAC RID: 3244
		private string normalizedStringID;

		// Token: 0x04000CAD RID: 3245
		private string NOTATIONID;

		// Token: 0x04000CAE RID: 3246
		private string positiveIntegerID;

		// Token: 0x04000CAF RID: 3247
		private string tokenID;

		// Token: 0x04000CB0 RID: 3248
		private string charID;

		// Token: 0x04000CB1 RID: 3249
		private string guidID;

		// Token: 0x04000CB2 RID: 3250
		private string timeSpanID;

		// Token: 0x04000CB3 RID: 3251
		private static bool checkDeserializeAdvances;

		// Token: 0x0200047F RID: 1151
		private struct SoapArrayInfo
		{
			// Token: 0x04001DD8 RID: 7640
			public string qname;

			// Token: 0x04001DD9 RID: 7641
			public int dimensions;

			// Token: 0x04001DDA RID: 7642
			public int length;

			// Token: 0x04001DDB RID: 7643
			public int jaggedDimensions;
		}

		// Token: 0x02000480 RID: 1152
		protected class Fixup
		{
			// Token: 0x060030CE RID: 12494 RVA: 0x0011D70F File Offset: 0x0011B90F
			public Fixup(object o, XmlSerializationFixupCallback callback, int count) : this(o, callback, new string[count])
			{
			}

			// Token: 0x060030CF RID: 12495 RVA: 0x0011D71F File Offset: 0x0011B91F
			public Fixup(object o, XmlSerializationFixupCallback callback, string[] ids)
			{
				this.callback = callback;
				this.Source = o;
				this.ids = ids;
			}

			// Token: 0x17000A41 RID: 2625
			// (get) Token: 0x060030D0 RID: 12496 RVA: 0x0011D73C File Offset: 0x0011B93C
			public XmlSerializationFixupCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			// Token: 0x17000A42 RID: 2626
			// (get) Token: 0x060030D1 RID: 12497 RVA: 0x0011D744 File Offset: 0x0011B944
			// (set) Token: 0x060030D2 RID: 12498 RVA: 0x0011D74C File Offset: 0x0011B94C
			public object Source
			{
				get
				{
					return this.source;
				}
				set
				{
					this.source = value;
				}
			}

			// Token: 0x17000A43 RID: 2627
			// (get) Token: 0x060030D3 RID: 12499 RVA: 0x0011D755 File Offset: 0x0011B955
			public string[] Ids
			{
				get
				{
					return this.ids;
				}
			}

			// Token: 0x04001DDC RID: 7644
			private XmlSerializationFixupCallback callback;

			// Token: 0x04001DDD RID: 7645
			private object source;

			// Token: 0x04001DDE RID: 7646
			private string[] ids;
		}

		// Token: 0x02000481 RID: 1153
		protected class CollectionFixup
		{
			// Token: 0x060030D4 RID: 12500 RVA: 0x0011D75D File Offset: 0x0011B95D
			public CollectionFixup(object collection, XmlSerializationCollectionFixupCallback callback, object collectionItems)
			{
				this.callback = callback;
				this.collection = collection;
				this.collectionItems = collectionItems;
			}

			// Token: 0x17000A44 RID: 2628
			// (get) Token: 0x060030D5 RID: 12501 RVA: 0x0011D77A File Offset: 0x0011B97A
			public XmlSerializationCollectionFixupCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			// Token: 0x17000A45 RID: 2629
			// (get) Token: 0x060030D6 RID: 12502 RVA: 0x0011D782 File Offset: 0x0011B982
			public object Collection
			{
				get
				{
					return this.collection;
				}
			}

			// Token: 0x17000A46 RID: 2630
			// (get) Token: 0x060030D7 RID: 12503 RVA: 0x0011D78A File Offset: 0x0011B98A
			public object CollectionItems
			{
				get
				{
					return this.collectionItems;
				}
			}

			// Token: 0x04001DDF RID: 7647
			private XmlSerializationCollectionFixupCallback callback;

			// Token: 0x04001DE0 RID: 7648
			private object collection;

			// Token: 0x04001DE1 RID: 7649
			private object collectionItems;
		}
	}
}
