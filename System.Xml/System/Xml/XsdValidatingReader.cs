using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000B0 RID: 176
	internal class XsdValidatingReader : XmlReader, IXmlSchemaInfo, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0002C5E4 File Offset: 0x0002B5E4
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings, XmlSchemaObject partialValidationType)
		{
			this.coreReader = reader;
			this.coreReaderNSResolver = (reader as IXmlNamespaceResolver);
			this.lineInfo = (reader as IXmlLineInfo);
			this.coreReaderNameTable = this.coreReader.NameTable;
			if (this.coreReaderNSResolver == null)
			{
				this.nsManager = new XmlNamespaceManager(this.coreReaderNameTable);
				this.manageNamespaces = true;
			}
			this.thisNSResolver = this;
			this.xmlResolver = xmlResolver;
			this.processInlineSchema = ((readerSettings.ValidationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) != XmlSchemaValidationFlags.None);
			this.Init();
			this.SetupValidator(readerSettings, reader, partialValidationType);
			this.validationEvent = readerSettings.GetEventHandler();
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002C690 File Offset: 0x0002B690
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings) : this(reader, xmlResolver, readerSettings, null)
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002C69C File Offset: 0x0002B69C
		private void Init()
		{
			this.validationState = XsdValidatingReader.ValidatingReaderState.Init;
			this.defaultAttributes = new ArrayList();
			this.currentAttrIndex = -1;
			this.attributePSVINodes = new AttributePSVIInfo[8];
			this.valueGetter = new XmlValueGetter(this.GetStringValue);
			XsdValidatingReader.TypeOfString = typeof(string);
			this.xmlSchemaInfo = new XmlSchemaInfo();
			this.NsXmlNs = this.coreReaderNameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXs = this.coreReaderNameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = this.coreReaderNameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = this.coreReaderNameTable.Add("type");
			this.XsiNil = this.coreReaderNameTable.Add("nil");
			this.XsiSchemaLocation = this.coreReaderNameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = this.coreReaderNameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = this.coreReaderNameTable.Add("schema");
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002C7AC File Offset: 0x0002B7AC
		private void SetupValidator(XmlReaderSettings readerSettings, XmlReader reader, XmlSchemaObject partialValidationType)
		{
			this.validator = new XmlSchemaValidator(this.coreReaderNameTable, readerSettings.Schemas, this.thisNSResolver, readerSettings.ValidationFlags);
			this.validator.XmlResolver = this.xmlResolver;
			this.validator.SourceUri = XmlConvert.ToUri(reader.BaseURI);
			this.validator.ValidationEventSender = this;
			this.validator.ValidationEventHandler += readerSettings.GetEventHandler();
			this.validator.LineInfoProvider = this.lineInfo;
			if (this.validator.ProcessSchemaHints)
			{
				this.validator.SchemaSet.ReaderSettings.ProhibitDtd = readerSettings.ProhibitDtd;
			}
			this.validator.SetDtdSchemaInfo(XmlReader.GetDtdSchemaInfo(reader));
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
				return;
			}
			this.validator.Initialize();
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0002C888 File Offset: 0x0002B888
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.coreReader.Settings;
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				if (xmlReaderSettings == null)
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.Schemas = this.validator.SchemaSet;
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ValidationFlags = this.validator.ValidationFlags;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002C8E5 File Offset: 0x0002B8E5
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.NodeType;
				}
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0002C908 File Offset: 0x0002B908
		public override string Name
		{
			get
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
				{
					return this.coreReader.Name;
				}
				string defaultAttributePrefix = this.validator.GetDefaultAttributePrefix(this.cachedNode.Namespace);
				if (defaultAttributePrefix != null && defaultAttributePrefix.Length != 0)
				{
					return string.Concat(new string[]
					{
						defaultAttributePrefix + ":" + this.cachedNode.LocalName
					});
				}
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0002C97E File Offset: 0x0002B97E
		public override string LocalName
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.LocalName;
				}
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0002C9A0 File Offset: 0x0002B9A0
		public override string NamespaceURI
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Namespace;
				}
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0002C9C2 File Offset: 0x0002B9C2
		public override string Prefix
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Prefix;
				}
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0002C9E4 File Offset: 0x0002B9E4
		public override bool HasValue
		{
			get
			{
				return this.validationState < XsdValidatingReader.ValidatingReaderState.None || this.coreReader.HasValue;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0002C9FC File Offset: 0x0002B9FC
		public override string Value
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.RawValue;
				}
				return this.coreReader.Value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0002CA1E File Offset: 0x0002BA1E
		public override int Depth
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Depth;
				}
				return this.coreReader.Depth;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0002CA40 File Offset: 0x0002BA40
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0002CA4D File Offset: 0x0002BA4D
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0002CA5A File Offset: 0x0002BA5A
		public override bool IsDefault
		{
			get
			{
				return this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute || this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0002CA72 File Offset: 0x0002BA72
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002CA7F File Offset: 0x0002BA7F
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002CA8C File Offset: 0x0002BA8C
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0002CA99 File Offset: 0x0002BA99
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0002CA9C File Offset: 0x0002BA9C
		public override Type ValueType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					break;
				case XmlNodeType.Attribute:
					if (this.attributePSVI != null && this.AttributeSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
					{
						return this.AttributeSchemaInfo.SchemaType.Datatype.ValueType;
					}
					goto IL_6A;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						goto IL_6A;
					}
					break;
				}
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					return this.xmlSchemaInfo.SchemaType.Datatype.ValueType;
				}
				IL_6A:
				return XsdValidatingReader.TypeOfString;
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002CB18 File Offset: 0x0002BB18
		public override object ReadContentAsObject()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsObject(true);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002CB3C File Offset: 0x0002BB3C
		public override bool ReadContentAsBoolean()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsBoolean");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			bool result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToBoolean(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToBoolean(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002CC08 File Offset: 0x0002BC08
		public override DateTime ReadContentAsDateTime()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDateTime");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			DateTime result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDateTime(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDateTime(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002CCD4 File Offset: 0x0002BCD4
		public override double ReadContentAsDouble()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDouble");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			double result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDouble(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDouble(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002CDA0 File Offset: 0x0002BDA0
		public override float ReadContentAsFloat()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsFloat");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			float result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToSingle(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToSingle(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002CE6C File Offset: 0x0002BE6C
		public override decimal ReadContentAsDecimal()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDecimal");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			decimal result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDecimal(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDecimal(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002CF38 File Offset: 0x0002BF38
		public override int ReadContentAsInt()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsInt");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			int result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToInt32(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToInt32(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Int", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Int", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Int", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002D004 File Offset: 0x0002C004
		public override long ReadContentAsLong()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsLong");
			}
			object value = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			long result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToInt64(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToInt64(value);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Long", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Long", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Long", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002D0D0 File Offset: 0x0002C0D0
		public override string ReadContentAsString()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsString");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			string result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					result = (obj as string);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "String", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "String", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "String", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002D198 File Offset: 0x0002C198
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAs");
			}
			string text;
			object value = this.InternalReadContentAsObject(false, out text);
			XmlSchemaType xmlSchemaType = (this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType;
			object result;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						value = text;
					}
					result = xmlSchemaType.ValueConverter.ChangeType(value, returnType);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ChangeType(value, returnType, namespaceResolver);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException3, this);
			}
			return result;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002D28C File Offset: 0x0002C28C
		public override object ReadElementContentAsObject()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsObject");
			}
			XmlSchemaType xmlSchemaType;
			return this.InternalReadElementContentAsObject(out xmlSchemaType, true);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002D2B8 File Offset: 0x0002C2B8
		public override bool ReadElementContentAsBoolean()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsBoolean");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			bool result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToBoolean(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToBoolean(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002D36C File Offset: 0x0002C36C
		public override DateTime ReadElementContentAsDateTime()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDateTime");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			DateTime result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDateTime(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDateTime(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002D420 File Offset: 0x0002C420
		public override double ReadElementContentAsDouble()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDouble");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			double result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDouble(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDouble(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002D4D4 File Offset: 0x0002C4D4
		public override float ReadElementContentAsFloat()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsFloat");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			float result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToSingle(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToSingle(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002D588 File Offset: 0x0002C588
		public override decimal ReadElementContentAsDecimal()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDecimal");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			decimal result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToDecimal(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToDecimal(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002D63C File Offset: 0x0002C63C
		public override int ReadElementContentAsInt()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsInt");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			int result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToInt32(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToInt32(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Int", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Int", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Int", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002D6F0 File Offset: 0x0002C6F0
		public override long ReadElementContentAsLong()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsLong");
			}
			XmlSchemaType xmlSchemaType;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType);
			long result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToInt64(value);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ToInt64(value);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Long", innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Long", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Long", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002D7A4 File Offset: 0x0002C7A4
		public override string ReadElementContentAsString()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsString");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			string result;
			try
			{
				if (xmlSchemaType != null)
				{
					result = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					result = (obj as string);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "String", innerException, this);
			}
			catch (FormatException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "String", innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "String", innerException3, this);
			}
			return result;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002D854 File Offset: 0x0002C854
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAs");
			}
			XmlSchemaType xmlSchemaType;
			string text;
			object value = this.InternalReadElementContentAsObject(out xmlSchemaType, false, out text);
			object result;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						value = text;
					}
					result = xmlSchemaType.ValueConverter.ChangeType(value, returnType, namespaceResolver);
				}
				else
				{
					result = XmlUntypedConverter.Untyped.ChangeType(value, returnType, namespaceResolver);
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException, this);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException2, this);
			}
			catch (OverflowException innerException3)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException3, this);
			}
			return result;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002D930 File Offset: 0x0002C930
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002D938 File Offset: 0x0002C938
		public override string GetAttribute(string name)
		{
			string text = this.coreReader.GetAttribute(name);
			if (text == null && this.attributeCount > 0)
			{
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, false);
				if (defaultAttribute != null)
				{
					text = defaultAttribute.RawValue;
				}
			}
			return text;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0002D974 File Offset: 0x0002C974
		public override string GetAttribute(string name, string namespaceURI)
		{
			string attribute = this.coreReader.GetAttribute(name, namespaceURI);
			if (attribute == null && this.attributeCount > 0)
			{
				namespaceURI = ((namespaceURI == null) ? string.Empty : this.coreReaderNameTable.Get(namespaceURI));
				name = this.coreReaderNameTable.Get(name);
				if (name == null || namespaceURI == null)
				{
					return null;
				}
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, namespaceURI, false);
				if (defaultAttribute != null)
				{
					return defaultAttribute.RawValue;
				}
			}
			return attribute;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002D9E0 File Offset: 0x0002C9E0
		public override string GetAttribute(int i)
		{
			if (this.attributeCount == 0)
			{
				return null;
			}
			if (i < this.coreReaderAttributeCount)
			{
				return this.coreReader.GetAttribute(i);
			}
			int index = i - this.coreReaderAttributeCount;
			ValidatingReaderNodeData validatingReaderNodeData = (ValidatingReaderNodeData)this.defaultAttributes[index];
			return validatingReaderNodeData.RawValue;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002DA30 File Offset: 0x0002CA30
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				if (this.attributeCount > 0)
				{
					ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, true);
					if (defaultAttribute != null)
					{
						this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
						this.attributePSVI = defaultAttribute.AttInfo;
						this.cachedNode = defaultAttribute;
						goto IL_57;
					}
				}
				return false;
			}
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			this.attributePSVI = this.GetAttributePSVI(name);
			IL_57:
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002DAB8 File Offset: 0x0002CAB8
		public override bool MoveToAttribute(string name, string ns)
		{
			name = this.coreReaderNameTable.Get(name);
			ns = ((ns != null) ? this.coreReaderNameTable.Get(ns) : string.Empty);
			if (name == null || ns == null)
			{
				return false;
			}
			if (this.coreReader.MoveToAttribute(name, ns))
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.GetAttributePSVI(name, ns);
				}
				else
				{
					this.attributePSVI = null;
				}
			}
			else
			{
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, ns, true);
				if (defaultAttribute == null)
				{
					return false;
				}
				this.attributePSVI = defaultAttribute.AttInfo;
				this.cachedNode = defaultAttribute;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002DB78 File Offset: 0x0002CB78
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (i < this.coreReaderAttributeCount)
			{
				this.coreReader.MoveToAttribute(i);
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[i];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				int index = i - this.coreReaderAttributeCount;
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[index];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002DC34 File Offset: 0x0002CC34
		public override bool MoveToFirstAttribute()
		{
			if (this.coreReader.MoveToFirstAttribute())
			{
				this.currentAttrIndex = 0;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[0];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				if (this.defaultAttributes.Count <= 0)
				{
					return false;
				}
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[0];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.currentAttrIndex = 0;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002DCE8 File Offset: 0x0002CCE8
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.coreReaderAttributeCount)
			{
				this.coreReader.MoveToNextAttribute();
				this.currentAttrIndex++;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[this.currentAttrIndex];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				if (this.currentAttrIndex + 1 >= this.attributeCount)
				{
					return false;
				}
				int index = ++this.currentAttrIndex - this.coreReaderAttributeCount;
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[index];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002DDC9 File Offset: 0x0002CDC9
		public override bool MoveToElement()
		{
			if (this.coreReader.MoveToElement() || this.validationState < XsdValidatingReader.ValidatingReaderState.None)
			{
				this.currentAttrIndex = -1;
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				return true;
			}
			return false;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002DDF4 File Offset: 0x0002CDF4
		public override bool Read()
		{
			switch (this.validationState)
			{
			case XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue:
			case XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute:
			case XsdValidatingReader.ValidatingReaderState.OnAttribute:
			case XsdValidatingReader.ValidatingReaderState.ClearAttributes:
				this.ClearAttributesInfo();
				if (this.inlineSchemaParser != null)
				{
					this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
					goto IL_7C;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				break;
			case XsdValidatingReader.ValidatingReaderState.None:
				return false;
			case XsdValidatingReader.ValidatingReaderState.Init:
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessReaderEvent();
					return true;
				}
				break;
			case XsdValidatingReader.ValidatingReaderState.Read:
				break;
			case XsdValidatingReader.ValidatingReaderState.ParseInlineSchema:
				goto IL_7C;
			case XsdValidatingReader.ValidatingReaderState.ReadAhead:
				this.ClearAttributesInfo();
				this.ProcessReaderEvent();
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				return true;
			case XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent:
				this.validationState = this.savedState;
				this.readBinaryHelper.Finish();
				return this.Read();
			case XsdValidatingReader.ValidatingReaderState.ReaderClosed:
			case XsdValidatingReader.ValidatingReaderState.EOF:
				return false;
			default:
				return false;
			}
			if (this.coreReader.Read())
			{
				this.ProcessReaderEvent();
				return true;
			}
			this.validator.EndValidation();
			if (this.coreReader.EOF)
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
			}
			return false;
			IL_7C:
			this.ProcessInlineSchema();
			return true;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002DEFB File Offset: 0x0002CEFB
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002DF08 File Offset: 0x0002CF08
		public override void Close()
		{
			this.coreReader.Close();
			this.validationState = XsdValidatingReader.ValidatingReaderState.ReaderClosed;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002DF1C File Offset: 0x0002CF1C
		public override ReadState ReadState
		{
			get
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.Init)
				{
					return this.coreReader.ReadState;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002DF34 File Offset: 0x0002CF34
		public override void Skip()
		{
			int depth = this.Depth;
			switch (this.NodeType)
			{
			case XmlNodeType.Element:
				break;
			case XmlNodeType.Attribute:
				this.MoveToElement();
				break;
			default:
				goto IL_89;
			}
			if (!this.coreReader.IsEmptyElement)
			{
				bool flag = true;
				if ((this.xmlSchemaInfo.IsUnionType || this.xmlSchemaInfo.IsDefault) && this.coreReader is XsdCachingReader)
				{
					flag = false;
				}
				this.coreReader.Skip();
				this.validationState = XsdValidatingReader.ValidatingReaderState.ReadAhead;
				if (flag)
				{
					this.validator.SkipToEndElement(this.xmlSchemaInfo);
				}
			}
			IL_89:
			this.Read();
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002DFD1 File Offset: 0x0002CFD1
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002DFD9 File Offset: 0x0002CFD9
		public override string LookupNamespace(string prefix)
		{
			return this.thisNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002DFE7 File Offset: 0x0002CFE7
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002DFF0 File Offset: 0x0002CFF0
		public override bool ReadAttributeValue()
		{
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			if (this.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
			{
				this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue;
				return true;
			}
			return this.coreReader.ReadAttributeValue();
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002E06A File Offset: 0x0002D06A
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002E070 File Offset: 0x0002D070
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return result;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002E0E0 File Offset: 0x0002D0E0
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return result;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002E150 File Offset: 0x0002D150
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return result;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002E1C0 File Offset: 0x0002D1C0
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return result;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002E230 File Offset: 0x0002D230
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					if (!this.coreReader.IsEmptyElement)
					{
						this.GetIsDefault();
					}
					return this.xmlSchemaInfo.IsDefault;
				case XmlNodeType.Attribute:
					if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.IsDefault;
					}
					break;
				default:
					if (nodeType == XmlNodeType.EndElement)
					{
						return this.xmlSchemaInfo.IsDefault;
					}
					break;
				}
				return false;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0002E29C File Offset: 0x0002D29C
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				return (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.EndElement) && this.xmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0002E2C8 File Offset: 0x0002D2C8
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					if (this.coreReader.IsEmptyElement)
					{
						return this.xmlSchemaInfo.Validity;
					}
					if (this.xmlSchemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						return XmlSchemaValidity.NotKnown;
					}
					return this.xmlSchemaInfo.Validity;
				case XmlNodeType.Attribute:
					if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.Validity;
					}
					break;
				default:
					if (nodeType == XmlNodeType.EndElement)
					{
						return this.xmlSchemaInfo.Validity;
					}
					break;
				}
				return XmlSchemaValidity.NotKnown;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0002E34C File Offset: 0x0002D34C
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					if (!this.coreReader.IsEmptyElement)
					{
						this.GetMemberType();
					}
					return this.xmlSchemaInfo.MemberType;
				case XmlNodeType.Attribute:
					if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.MemberType;
					}
					return null;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return null;
					}
					return this.xmlSchemaInfo.MemberType;
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0002E3BC File Offset: 0x0002D3BC
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					break;
				case XmlNodeType.Attribute:
					if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.SchemaType;
					}
					return null;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return null;
					}
					break;
				}
				return this.xmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0002E408 File Offset: 0x0002D408
		XmlSchemaElement IXmlSchemaInfo.SchemaElement
		{
			get
			{
				if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
				{
					return this.xmlSchemaInfo.SchemaElement;
				}
				return null;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0002E42A File Offset: 0x0002D42A
		XmlSchemaAttribute IXmlSchemaInfo.SchemaAttribute
		{
			get
			{
				if (this.NodeType == XmlNodeType.Attribute && this.attributePSVI != null)
				{
					return this.AttributeSchemaInfo.SchemaAttribute;
				}
				return null;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002E44A File Offset: 0x0002D44A
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0002E44D File Offset: 0x0002D44D
		public int LineNumber
		{
			get
			{
				if (this.lineInfo != null)
				{
					return this.lineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0002E464 File Offset: 0x0002D464
		public int LinePosition
		{
			get
			{
				if (this.lineInfo != null)
				{
					return this.lineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002E47B File Offset: 0x0002D47B
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.GetNamespacesInScope(scope);
			}
			return this.nsManager.GetNamespacesInScope(scope);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002E49E File Offset: 0x0002D49E
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupNamespace(prefix);
			}
			return this.nsManager.LookupNamespace(prefix);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002E4C1 File Offset: 0x0002D4C1
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupPrefix(namespaceName);
			}
			return this.nsManager.LookupPrefix(namespaceName);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002E4E4 File Offset: 0x0002D4E4
		private object GetStringValue()
		{
			return this.coreReader.Value;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0002E4F1 File Offset: 0x0002D4F1
		private XmlSchemaType ElementXmlType
		{
			get
			{
				return this.xmlSchemaInfo.XmlType;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0002E4FE File Offset: 0x0002D4FE
		private XmlSchemaType AttributeXmlType
		{
			get
			{
				if (this.attributePSVI != null)
				{
					return this.AttributeSchemaInfo.XmlType;
				}
				return null;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002E515 File Offset: 0x0002D515
		private XmlSchemaInfo AttributeSchemaInfo
		{
			get
			{
				return this.attributePSVI.attributeSchemaInfo;
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002E524 File Offset: 0x0002D524
		private void ProcessReaderEvent()
		{
			if (this.replayCache)
			{
				return;
			}
			switch (this.coreReader.NodeType)
			{
			case XmlNodeType.Element:
				this.ProcessElementEvent();
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
				return;
			case XmlNodeType.EntityReference:
				throw new InvalidOperationException();
			case XmlNodeType.DocumentType:
				this.validator.SetDtdSchemaInfo(XmlReader.GetDtdSchemaInfo(this.coreReader));
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
				return;
			case XmlNodeType.EndElement:
				this.ProcessEndElementEvent();
				return;
			default:
				return;
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002E5E8 File Offset: 0x0002D5E8
		private void ProcessElementEvent()
		{
			if (!this.processInlineSchema || !this.IsXSDRoot(this.coreReader.LocalName, this.coreReader.NamespaceURI) || this.coreReader.Depth <= 0)
			{
				this.atomicValue = null;
				this.originalAtomicValueString = null;
				this.xmlSchemaInfo.Clear();
				if (this.manageNamespaces)
				{
					this.nsManager.PushScope();
				}
				string xsiSchemaLocation = null;
				string xsiNoNamespaceSchemaLocation = null;
				string xsiNil = null;
				string xsiType = null;
				if (this.coreReader.MoveToFirstAttribute())
				{
					do
					{
						string namespaceURI = this.coreReader.NamespaceURI;
						string localName = this.coreReader.LocalName;
						if (Ref.Equal(namespaceURI, this.NsXsi))
						{
							if (Ref.Equal(localName, this.XsiSchemaLocation))
							{
								xsiSchemaLocation = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNoNamespaceSchemaLocation))
							{
								xsiNoNamespaceSchemaLocation = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiType))
							{
								xsiType = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNil))
							{
								xsiNil = this.coreReader.Value;
							}
						}
						if (this.manageNamespaces && Ref.Equal(this.coreReader.NamespaceURI, this.NsXmlNs))
						{
							this.nsManager.AddNamespace((this.coreReader.Prefix.Length == 0) ? string.Empty : this.coreReader.LocalName, this.coreReader.Value);
						}
					}
					while (this.coreReader.MoveToNextAttribute());
					this.coreReader.MoveToElement();
				}
				this.validator.ValidateElement(this.coreReader.LocalName, this.coreReader.NamespaceURI, this.xmlSchemaInfo, xsiType, xsiNil, xsiSchemaLocation, xsiNoNamespaceSchemaLocation);
				this.ValidateAttributes();
				this.validator.ValidateEndOfAttributes(this.xmlSchemaInfo);
				if (this.coreReader.IsEmptyElement)
				{
					this.ProcessEndElementEvent();
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				return;
			}
			this.xmlSchemaInfo.Clear();
			this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
			if (!this.coreReader.IsEmptyElement)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XSD, this.coreReaderNameTable, this.validator.SchemaSet.GetSchemaNames(this.coreReaderNameTable), this.validationEvent);
				this.inlineSchemaParser.StartParsing(this.coreReader, null);
				this.inlineSchemaParser.ParseReaderNode();
				this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
				return;
			}
			this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002E874 File Offset: 0x0002D874
		private void ProcessEndElementEvent()
		{
			this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
			this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
			if (this.xmlSchemaInfo.IsDefault)
			{
				int depth = this.coreReader.Depth;
				this.coreReader = this.GetCachingReader();
				this.cachingReader.RecordTextNode(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString, depth + 1, 0, 0);
				this.cachingReader.RecordEndElementNode();
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
				return;
			}
			if (this.manageNamespaces)
			{
				this.nsManager.PopScope();
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002E930 File Offset: 0x0002D930
		private void ValidateAttributes()
		{
			this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
			int num = 0;
			bool flag = false;
			if (this.coreReader.MoveToFirstAttribute())
			{
				do
				{
					string localName = this.coreReader.LocalName;
					string namespaceURI = this.coreReader.NamespaceURI;
					AttributePSVIInfo attributePSVIInfo = this.AddAttributePSVI(num);
					attributePSVIInfo.localName = localName;
					attributePSVIInfo.namespaceUri = namespaceURI;
					if (namespaceURI == this.NsXmlNs)
					{
						num++;
					}
					else
					{
						attributePSVIInfo.typedAttributeValue = this.validator.ValidateAttribute(localName, namespaceURI, this.valueGetter, attributePSVIInfo.attributeSchemaInfo);
						if (!flag)
						{
							flag = (attributePSVIInfo.attributeSchemaInfo.Validity == XmlSchemaValidity.Invalid);
						}
						num++;
					}
				}
				while (this.coreReader.MoveToNextAttribute());
			}
			this.coreReader.MoveToElement();
			if (flag)
			{
				this.xmlSchemaInfo.Validity = XmlSchemaValidity.Invalid;
			}
			this.validator.GetUnspecifiedDefaultAttributes(this.defaultAttributes, true);
			this.attributeCount += this.defaultAttributes.Count;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002EA38 File Offset: 0x0002DA38
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.coreReaderAttributeCount = 0;
			this.currentAttrIndex = -1;
			this.defaultAttributes.Clear();
			this.attributePSVI = null;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002EA64 File Offset: 0x0002DA64
		private AttributePSVIInfo GetAttributePSVI(string name)
		{
			if (this.inlineSchemaParser != null)
			{
				return null;
			}
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			text = this.coreReaderNameTable.Add(text);
			text2 = this.coreReaderNameTable.Add(text2);
			string ns;
			if (text.Length == 0)
			{
				ns = string.Empty;
			}
			else
			{
				ns = this.thisNSResolver.LookupNamespace(text);
			}
			return this.GetAttributePSVI(text2, ns);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002EAC4 File Offset: 0x0002DAC4
		private AttributePSVIInfo GetAttributePSVI(string localName, string ns)
		{
			for (int i = 0; i < this.coreReaderAttributeCount; i++)
			{
				AttributePSVIInfo attributePSVIInfo = this.attributePSVINodes[i];
				if (attributePSVIInfo != null && Ref.Equal(localName, attributePSVIInfo.localName) && Ref.Equal(ns, attributePSVIInfo.namespaceUri))
				{
					this.currentAttrIndex = i;
					return attributePSVIInfo;
				}
			}
			return null;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002EB18 File Offset: 0x0002DB18
		private ValidatingReaderNodeData GetDefaultAttribute(string name, bool updatePosition)
		{
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			text = this.coreReaderNameTable.Add(text);
			text2 = this.coreReaderNameTable.Add(text2);
			string ns;
			if (text.Length == 0)
			{
				ns = string.Empty;
			}
			else
			{
				ns = this.thisNSResolver.LookupNamespace(text);
			}
			return this.GetDefaultAttribute(text2, ns, updatePosition);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002EB70 File Offset: 0x0002DB70
		private ValidatingReaderNodeData GetDefaultAttribute(string attrLocalName, string ns, bool updatePosition)
		{
			for (int i = 0; i < this.defaultAttributes.Count; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = (ValidatingReaderNodeData)this.defaultAttributes[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, attrLocalName) && Ref.Equal(validatingReaderNodeData.Namespace, ns))
				{
					if (updatePosition)
					{
						this.currentAttrIndex = this.coreReader.AttributeCount + i;
					}
					return validatingReaderNodeData;
				}
			}
			return null;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002EBDC File Offset: 0x0002DBDC
		private AttributePSVIInfo AddAttributePSVI(int attIndex)
		{
			AttributePSVIInfo attributePSVIInfo = this.attributePSVINodes[attIndex];
			if (attributePSVIInfo != null)
			{
				attributePSVIInfo.Reset();
				return attributePSVIInfo;
			}
			if (attIndex >= this.attributePSVINodes.Length - 1)
			{
				AttributePSVIInfo[] destinationArray = new AttributePSVIInfo[this.attributePSVINodes.Length * 2];
				Array.Copy(this.attributePSVINodes, 0, destinationArray, 0, this.attributePSVINodes.Length);
				this.attributePSVINodes = destinationArray;
			}
			attributePSVIInfo = this.attributePSVINodes[attIndex];
			if (attributePSVIInfo == null)
			{
				attributePSVIInfo = new AttributePSVIInfo();
				this.attributePSVINodes[attIndex] = attributePSVIInfo;
			}
			return attributePSVIInfo;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002EC53 File Offset: 0x0002DC53
		private bool IsXSDRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXs) && Ref.Equal(localName, this.XsdSchema);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002EC74 File Offset: 0x0002DC74
		private void ProcessInlineSchema()
		{
			if (this.coreReader.Read())
			{
				if (this.coreReader.NodeType == XmlNodeType.Element)
				{
					this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
				}
				else
				{
					this.ClearAttributesInfo();
				}
				if (!this.inlineSchemaParser.ParseReaderNode())
				{
					this.inlineSchemaParser.FinishParsing();
					XmlSchema xmlSchema = this.inlineSchemaParser.XmlSchema;
					this.validator.AddSchema(xmlSchema);
					this.inlineSchemaParser = null;
					this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				}
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002ECFD File Offset: 0x0002DCFD
		private object InternalReadContentAsObject()
		{
			return this.InternalReadContentAsObject(false);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002ED08 File Offset: 0x0002DD08
		private object InternalReadContentAsObject(bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadContentAsObject(unwrapTypedValue, out text);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002ED20 File Offset: 0x0002DD20
		private object InternalReadContentAsObject(bool unwrapTypedValue, out string originalStringValue)
		{
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.Attribute)
			{
				originalStringValue = this.Value;
				if (this.attributePSVI != null && this.attributePSVI.typedAttributeValue != null)
				{
					if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
					{
						XmlSchemaAttribute schemaAttribute = this.attributePSVI.attributeSchemaInfo.SchemaAttribute;
						originalStringValue = ((schemaAttribute.DefaultValue != null) ? schemaAttribute.DefaultValue : schemaAttribute.FixedValue);
					}
					return this.ReturnBoxedValue(this.attributePSVI.typedAttributeValue, this.AttributeSchemaInfo.XmlType, unwrapTypedValue);
				}
				return this.Value;
			}
			else if (nodeType == XmlNodeType.EndElement)
			{
				if (this.atomicValue != null)
				{
					originalStringValue = this.originalAtomicValueString;
					return this.atomicValue;
				}
				originalStringValue = string.Empty;
				return string.Empty;
			}
			else
			{
				if (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly)
				{
					object result = this.ReturnBoxedValue(this.ReadTillEndElement(), this.xmlSchemaInfo.XmlType, unwrapTypedValue);
					originalStringValue = this.originalAtomicValueString;
					return result;
				}
				XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
				if (xsdCachingReader != null)
				{
					originalStringValue = xsdCachingReader.ReadOriginalContentAsString();
				}
				else
				{
					originalStringValue = base.InternalReadContentAsString();
				}
				return originalStringValue;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002EE28 File Offset: 0x0002DE28
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType)
		{
			return this.InternalReadElementContentAsObject(out xmlType, false);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002EE34 File Offset: 0x0002DE34
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadElementContentAsObject(out xmlType, unwrapTypedValue, out text);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002EE4C File Offset: 0x0002DE4C
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue, out string originalString)
		{
			xmlType = null;
			object result;
			if (this.IsEmptyElement)
			{
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					result = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
				}
				else
				{
					result = this.atomicValue;
				}
				originalString = this.originalAtomicValueString;
				xmlType = this.ElementXmlType;
				this.Read();
				return result;
			}
			this.Read();
			if (this.NodeType == XmlNodeType.EndElement)
			{
				if (this.xmlSchemaInfo.IsDefault)
				{
					if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
					{
						result = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
					}
					else
					{
						result = this.atomicValue;
					}
					originalString = this.originalAtomicValueString;
				}
				else
				{
					result = string.Empty;
					originalString = string.Empty;
				}
			}
			else
			{
				if (this.NodeType == XmlNodeType.Element)
				{
					throw new XmlException("Xml_MixedReadElementContentAs", string.Empty, this);
				}
				result = this.InternalReadContentAsObject(unwrapTypedValue, out originalString);
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("Xml_MixedReadElementContentAs", string.Empty, this);
				}
			}
			xmlType = this.ElementXmlType;
			this.Read();
			return result;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002EF64 File Offset: 0x0002DF64
		private object ReadTillEndElement()
		{
			if (this.atomicValue == null)
			{
				while (this.coreReader.Read())
				{
					if (!this.replayCache)
					{
						switch (this.coreReader.NodeType)
						{
						case XmlNodeType.Element:
							this.ProcessReaderEvent();
							goto IL_10B;
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.EndElement:
							this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
							this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
							if (this.manageNamespaces)
							{
								this.nsManager.PopScope();
								goto IL_10B;
							}
							goto IL_10B;
						}
					}
				}
			}
			else
			{
				if (this.atomicValue == this)
				{
					this.atomicValue = null;
				}
				this.SwitchReader();
			}
			IL_10B:
			return this.atomicValue;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002F084 File Offset: 0x0002E084
		private void SwitchReader()
		{
			XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
			if (xsdCachingReader != null)
			{
				this.coreReader = xsdCachingReader.GetCoreReader();
			}
			this.replayCache = false;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002F0B4 File Offset: 0x0002E0B4
		private void ReadAheadForMemberType()
		{
			while (this.coreReader.Read())
			{
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
					break;
				case XmlNodeType.EndElement:
					this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
					this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
					if (this.atomicValue == null)
					{
						this.atomicValue = this;
						return;
					}
					if (this.xmlSchemaInfo.IsDefault)
					{
						this.cachingReader.SwitchTextNodeAndEndElement(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString);
						return;
					}
					return;
				}
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002F1D0 File Offset: 0x0002E1D0
		private void GetIsDefault()
		{
			if (!(this.coreReader is XsdCachingReader) && this.xmlSchemaInfo.HasDefaultValue)
			{
				this.coreReader = this.GetCachingReader();
				if (this.xmlSchemaInfo.IsUnionType && !this.xmlSchemaInfo.IsNil)
				{
					this.ReadAheadForMemberType();
				}
				else if (this.coreReader.Read())
				{
					switch (this.coreReader.NodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
						break;
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
						break;
					case XmlNodeType.EndElement:
						this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
						this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
						if (this.xmlSchemaInfo.IsDefault)
						{
							this.cachingReader.SwitchTextNodeAndEndElement(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString);
						}
						break;
					}
				}
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002F338 File Offset: 0x0002E338
		private void GetMemberType()
		{
			if (this.xmlSchemaInfo.MemberType != null || this.atomicValue == this)
			{
				return;
			}
			if (!(this.coreReader is XsdCachingReader) && this.xmlSchemaInfo.IsUnionType && !this.xmlSchemaInfo.IsNil)
			{
				this.coreReader = this.GetCachingReader();
				this.ReadAheadForMemberType();
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002F3AC File Offset: 0x0002E3AC
		private object ReturnBoxedValue(object typedValue, XmlSchemaType xmlType, bool unWrap)
		{
			if (typedValue != null)
			{
				if (unWrap && xmlType.Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					Datatype_List datatype_List = xmlType.Datatype as Datatype_List;
					if (datatype_List.ItemType.Variety == XmlSchemaDatatypeVariety.Union)
					{
						typedValue = xmlType.ValueConverter.ChangeType(typedValue, xmlType.Datatype.ValueType, this.thisNSResolver);
					}
				}
				return typedValue;
			}
			typedValue = this.validator.GetConcatenatedValue();
			return typedValue;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002F418 File Offset: 0x0002E418
		private XsdCachingReader GetCachingReader()
		{
			if (this.cachingReader == null)
			{
				this.cachingReader = new XsdCachingReader(this.coreReader, this.lineInfo, new CachingEventHandler(this.CachingCallBack));
			}
			else
			{
				this.cachingReader.Reset(this.coreReader);
			}
			this.lineInfo = this.cachingReader;
			return this.cachingReader;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002F475 File Offset: 0x0002E475
		internal ValidatingReaderNodeData CreateDummyTextNode(string attributeValue, int depth)
		{
			if (this.textNode == null)
			{
				this.textNode = new ValidatingReaderNodeData(XmlNodeType.Text);
			}
			this.textNode.Depth = depth;
			this.textNode.RawValue = attributeValue;
			return this.textNode;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002F4A9 File Offset: 0x0002E4A9
		internal void CachingCallBack(XsdCachingReader cachingReader)
		{
			this.coreReader = cachingReader.GetCoreReader();
			this.lineInfo = cachingReader.GetLineInfo();
			this.replayCache = false;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002F4CC File Offset: 0x0002E4CC
		private string GetOriginalAtomicValueStringOfElement()
		{
			if (!this.xmlSchemaInfo.IsDefault)
			{
				return this.validator.GetConcatenatedValue();
			}
			XmlSchemaElement schemaElement = this.xmlSchemaInfo.SchemaElement;
			if (schemaElement == null)
			{
				return string.Empty;
			}
			if (schemaElement.DefaultValue == null)
			{
				return schemaElement.FixedValue;
			}
			return schemaElement.DefaultValue;
		}

		// Token: 0x04000854 RID: 2132
		private const int InitialAttributeCount = 8;

		// Token: 0x04000855 RID: 2133
		private XmlReader coreReader;

		// Token: 0x04000856 RID: 2134
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x04000857 RID: 2135
		private IXmlNamespaceResolver thisNSResolver;

		// Token: 0x04000858 RID: 2136
		private XmlSchemaValidator validator;

		// Token: 0x04000859 RID: 2137
		private XmlResolver xmlResolver;

		// Token: 0x0400085A RID: 2138
		private ValidationEventHandler validationEvent;

		// Token: 0x0400085B RID: 2139
		private XsdValidatingReader.ValidatingReaderState validationState;

		// Token: 0x0400085C RID: 2140
		private XmlValueGetter valueGetter;

		// Token: 0x0400085D RID: 2141
		private XmlNamespaceManager nsManager;

		// Token: 0x0400085E RID: 2142
		private bool manageNamespaces;

		// Token: 0x0400085F RID: 2143
		private bool processInlineSchema;

		// Token: 0x04000860 RID: 2144
		private bool replayCache;

		// Token: 0x04000861 RID: 2145
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x04000862 RID: 2146
		private AttributePSVIInfo attributePSVI;

		// Token: 0x04000863 RID: 2147
		private int attributeCount;

		// Token: 0x04000864 RID: 2148
		private int coreReaderAttributeCount;

		// Token: 0x04000865 RID: 2149
		private int currentAttrIndex;

		// Token: 0x04000866 RID: 2150
		private AttributePSVIInfo[] attributePSVINodes;

		// Token: 0x04000867 RID: 2151
		private ArrayList defaultAttributes;

		// Token: 0x04000868 RID: 2152
		private Parser inlineSchemaParser;

		// Token: 0x04000869 RID: 2153
		private object atomicValue;

		// Token: 0x0400086A RID: 2154
		private XmlSchemaInfo xmlSchemaInfo;

		// Token: 0x0400086B RID: 2155
		private string originalAtomicValueString;

		// Token: 0x0400086C RID: 2156
		private XmlNameTable coreReaderNameTable;

		// Token: 0x0400086D RID: 2157
		private XsdCachingReader cachingReader;

		// Token: 0x0400086E RID: 2158
		private ValidatingReaderNodeData textNode;

		// Token: 0x0400086F RID: 2159
		private string NsXmlNs;

		// Token: 0x04000870 RID: 2160
		private string NsXs;

		// Token: 0x04000871 RID: 2161
		private string NsXsi;

		// Token: 0x04000872 RID: 2162
		private string XsiType;

		// Token: 0x04000873 RID: 2163
		private string XsiNil;

		// Token: 0x04000874 RID: 2164
		private string XsdSchema;

		// Token: 0x04000875 RID: 2165
		private string XsiSchemaLocation;

		// Token: 0x04000876 RID: 2166
		private string XsiNoNamespaceSchemaLocation;

		// Token: 0x04000877 RID: 2167
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000878 RID: 2168
		private IXmlLineInfo lineInfo;

		// Token: 0x04000879 RID: 2169
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x0400087A RID: 2170
		private XsdValidatingReader.ValidatingReaderState savedState;

		// Token: 0x0400087B RID: 2171
		private static Type TypeOfString;

		// Token: 0x020000B1 RID: 177
		private enum ValidatingReaderState
		{
			// Token: 0x0400087D RID: 2173
			None,
			// Token: 0x0400087E RID: 2174
			Init,
			// Token: 0x0400087F RID: 2175
			Read,
			// Token: 0x04000880 RID: 2176
			OnDefaultAttribute = -1,
			// Token: 0x04000881 RID: 2177
			OnReadAttributeValue = -2,
			// Token: 0x04000882 RID: 2178
			OnAttribute = 3,
			// Token: 0x04000883 RID: 2179
			ClearAttributes,
			// Token: 0x04000884 RID: 2180
			ParseInlineSchema,
			// Token: 0x04000885 RID: 2181
			ReadAhead,
			// Token: 0x04000886 RID: 2182
			OnReadBinaryContent,
			// Token: 0x04000887 RID: 2183
			ReaderClosed,
			// Token: 0x04000888 RID: 2184
			EOF,
			// Token: 0x04000889 RID: 2185
			Error
		}
	}
}
