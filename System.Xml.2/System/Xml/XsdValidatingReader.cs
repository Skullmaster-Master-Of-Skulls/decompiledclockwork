using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000EC RID: 236
	internal class XsdValidatingReader : XmlReader, IXmlSchemaInfo, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000FB3 RID: 4019 RVA: 0x0004173C File Offset: 0x0003F93C
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
			this.processInlineSchema = ((readerSettings.ValidationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) > XmlSchemaValidationFlags.None);
			this.Init();
			this.SetupValidator(readerSettings, reader, partialValidationType);
			this.validationEvent = readerSettings.GetEventHandler();
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000417E5 File Offset: 0x0003F9E5
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings) : this(reader, xmlResolver, readerSettings, null)
		{
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000417F4 File Offset: 0x0003F9F4
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

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00041904 File Offset: 0x0003FB04
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
				this.validator.SchemaSet.ReaderSettings.DtdProcessing = readerSettings.DtdProcessing;
			}
			this.validator.SetDtdSchemaInfo(reader.DtdInfo);
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
				return;
			}
			this.validator.Initialize();
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x000419E0 File Offset: 0x0003FBE0
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

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00041A40 File Offset: 0x0003FC40
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.NodeType;
				}
				XmlNodeType nodeType = this.coreReader.NodeType;
				if (nodeType == XmlNodeType.Whitespace && (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly || this.validator.CurrentContentType == XmlSchemaContentType.Mixed))
				{
					return XmlNodeType.SignificantWhitespace;
				}
				return nodeType;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00041A94 File Offset: 0x0003FC94
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

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00041B08 File Offset: 0x0003FD08
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

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00041B2A File Offset: 0x0003FD2A
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

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00041B4C File Offset: 0x0003FD4C
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

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00041B6E File Offset: 0x0003FD6E
		public override bool HasValue
		{
			get
			{
				return this.validationState < XsdValidatingReader.ValidatingReaderState.None || this.coreReader.HasValue;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00041B86 File Offset: 0x0003FD86
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

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00041BA8 File Offset: 0x0003FDA8
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

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00041BCA File Offset: 0x0003FDCA
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00041BD7 File Offset: 0x0003FDD7
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00041BE4 File Offset: 0x0003FDE4
		public override bool IsDefault
		{
			get
			{
				return this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute || this.coreReader.IsDefault;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00041BFC File Offset: 0x0003FDFC
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00041C09 File Offset: 0x0003FE09
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00041C16 File Offset: 0x0003FE16
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00041C23 File Offset: 0x0003FE23
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00041C28 File Offset: 0x0003FE28
		public override Type ValueType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							goto IL_62;
						}
					}
					else
					{
						if (this.attributePSVI != null && this.AttributeSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
						{
							return this.AttributeSchemaInfo.SchemaType.Datatype.ValueType;
						}
						goto IL_62;
					}
				}
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					return this.xmlSchemaInfo.SchemaType.Datatype.ValueType;
				}
				IL_62:
				return XsdValidatingReader.TypeOfString;
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00041C9E File Offset: 0x0003FE9E
		public override object ReadContentAsObject()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsObject(true);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00041CC0 File Offset: 0x0003FEC0
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

		// Token: 0x06000FCA RID: 4042 RVA: 0x00041D8C File Offset: 0x0003FF8C
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

		// Token: 0x06000FCB RID: 4043 RVA: 0x00041E58 File Offset: 0x00040058
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

		// Token: 0x06000FCC RID: 4044 RVA: 0x00041F24 File Offset: 0x00040124
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

		// Token: 0x06000FCD RID: 4045 RVA: 0x00041FF0 File Offset: 0x000401F0
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

		// Token: 0x06000FCE RID: 4046 RVA: 0x000420BC File Offset: 0x000402BC
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

		// Token: 0x06000FCF RID: 4047 RVA: 0x00042188 File Offset: 0x00040388
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

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00042254 File Offset: 0x00040454
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

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0004231C File Offset: 0x0004051C
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

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00042414 File Offset: 0x00040614
		public override object ReadElementContentAsObject()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsObject");
			}
			XmlSchemaType xmlSchemaType;
			return this.InternalReadElementContentAsObject(out xmlSchemaType, true);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00042440 File Offset: 0x00040640
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

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000424F4 File Offset: 0x000406F4
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

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000425A8 File Offset: 0x000407A8
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

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0004265C File Offset: 0x0004085C
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

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00042710 File Offset: 0x00040910
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

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000427C4 File Offset: 0x000409C4
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

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00042878 File Offset: 0x00040A78
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

		// Token: 0x06000FDA RID: 4058 RVA: 0x0004292C File Offset: 0x00040B2C
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

		// Token: 0x06000FDB RID: 4059 RVA: 0x000429D8 File Offset: 0x00040BD8
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

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00042AB8 File Offset: 0x00040CB8
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00042AC0 File Offset: 0x00040CC0
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

		// Token: 0x06000FDE RID: 4062 RVA: 0x00042AFC File Offset: 0x00040CFC
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

		// Token: 0x06000FDF RID: 4063 RVA: 0x00042B68 File Offset: 0x00040D68
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

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00042BB8 File Offset: 0x00040DB8
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

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00042C40 File Offset: 0x00040E40
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

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00042D00 File Offset: 0x00040F00
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
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

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00042DC4 File Offset: 0x00040FC4
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

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00042E78 File Offset: 0x00041078
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.coreReaderAttributeCount)
			{
				bool flag = this.coreReader.MoveToNextAttribute();
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
				int num = this.currentAttrIndex + 1;
				this.currentAttrIndex = num;
				int index = num - this.coreReaderAttributeCount;
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

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00042F59 File Offset: 0x00041159
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

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00042F84 File Offset: 0x00041184
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0004308B File Offset: 0x0004128B
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00043098 File Offset: 0x00041298
		public override void Close()
		{
			this.coreReader.Close();
			this.validationState = XsdValidatingReader.ValidatingReaderState.ReaderClosed;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x000430AC File Offset: 0x000412AC
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

		// Token: 0x06000FEA RID: 4074 RVA: 0x000430C4 File Offset: 0x000412C4
		public override void Skip()
		{
			int depth = this.Depth;
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					goto IL_81;
				}
				this.MoveToElement();
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
			IL_81:
			this.Read();
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00043159 File Offset: 0x00041359
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00043161 File Offset: 0x00041361
		public override string LookupNamespace(string prefix)
		{
			return this.thisNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0004316F File Offset: 0x0004136F
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00043178 File Offset: 0x00041378
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

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x000431F2 File Offset: 0x000413F2
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000431F8 File Offset: 0x000413F8
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

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00043268 File Offset: 0x00041468
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

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000432D8 File Offset: 0x000414D8
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

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00043348 File Offset: 0x00041548
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x000433B8 File Offset: 0x000415B8
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							return this.xmlSchemaInfo.IsDefault;
						}
					}
					else if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.IsDefault;
					}
					return false;
				}
				if (!this.coreReader.IsEmptyElement)
				{
					this.GetIsDefault();
				}
				return this.xmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0004341C File Offset: 0x0004161C
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				return (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.EndElement) && this.xmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00043448 File Offset: 0x00041648
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							return this.xmlSchemaInfo.Validity;
						}
					}
					else if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.Validity;
					}
					return XmlSchemaValidity.NotKnown;
				}
				if (this.coreReader.IsEmptyElement)
				{
					return this.xmlSchemaInfo.Validity;
				}
				if (this.xmlSchemaInfo.Validity == XmlSchemaValidity.Valid)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return this.xmlSchemaInfo.Validity;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000434C4 File Offset: 0x000416C4
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					if (!this.coreReader.IsEmptyElement)
					{
						this.GetMemberType();
					}
					return this.xmlSchemaInfo.MemberType;
				}
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.EndElement)
					{
						return null;
					}
					return this.xmlSchemaInfo.MemberType;
				}
				else
				{
					if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.MemberType;
					}
					return null;
				}
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0004352C File Offset: 0x0004172C
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							return null;
						}
					}
					else
					{
						if (this.attributePSVI != null)
						{
							return this.AttributeSchemaInfo.SchemaType;
						}
						return null;
					}
				}
				return this.xmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00043570 File Offset: 0x00041770
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

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00043592 File Offset: 0x00041792
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

		// Token: 0x06000FFB RID: 4091 RVA: 0x000435B2 File Offset: 0x000417B2
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x000435B5 File Offset: 0x000417B5
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

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x000435CC File Offset: 0x000417CC
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

		// Token: 0x06000FFE RID: 4094 RVA: 0x000435E3 File Offset: 0x000417E3
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.GetNamespacesInScope(scope);
			}
			return this.nsManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00043606 File Offset: 0x00041806
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupNamespace(prefix);
			}
			return this.nsManager.LookupNamespace(prefix);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00043629 File Offset: 0x00041829
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupPrefix(namespaceName);
			}
			return this.nsManager.LookupPrefix(namespaceName);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004364C File Offset: 0x0004184C
		private object GetStringValue()
		{
			return this.coreReader.Value;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x00043659 File Offset: 0x00041859
		private XmlSchemaType ElementXmlType
		{
			get
			{
				return this.xmlSchemaInfo.XmlType;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00043666 File Offset: 0x00041866
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

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0004367D File Offset: 0x0004187D
		private XmlSchemaInfo AttributeSchemaInfo
		{
			get
			{
				return this.attributePSVI.attributeSchemaInfo;
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0004368C File Offset: 0x0004188C
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
				this.validator.SetDtdSchemaInfo(this.coreReader.DtdInfo);
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

		// Token: 0x06001006 RID: 4102 RVA: 0x00043750 File Offset: 0x00041950
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

		// Token: 0x06001007 RID: 4103 RVA: 0x000439DC File Offset: 0x00041BDC
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

		// Token: 0x06001008 RID: 4104 RVA: 0x00043A98 File Offset: 0x00041C98
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

		// Token: 0x06001009 RID: 4105 RVA: 0x00043BA1 File Offset: 0x00041DA1
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.coreReaderAttributeCount = 0;
			this.currentAttrIndex = -1;
			this.defaultAttributes.Clear();
			this.attributePSVI = null;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00043BCC File Offset: 0x00041DCC
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

		// Token: 0x0600100B RID: 4107 RVA: 0x00043C2C File Offset: 0x00041E2C
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

		// Token: 0x0600100C RID: 4108 RVA: 0x00043C80 File Offset: 0x00041E80
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

		// Token: 0x0600100D RID: 4109 RVA: 0x00043CD8 File Offset: 0x00041ED8
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

		// Token: 0x0600100E RID: 4110 RVA: 0x00043D44 File Offset: 0x00041F44
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

		// Token: 0x0600100F RID: 4111 RVA: 0x00043DBB File Offset: 0x00041FBB
		private bool IsXSDRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXs) && Ref.Equal(localName, this.XsdSchema);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00043DDC File Offset: 0x00041FDC
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

		// Token: 0x06001011 RID: 4113 RVA: 0x00043E65 File Offset: 0x00042065
		private object InternalReadContentAsObject()
		{
			return this.InternalReadContentAsObject(false);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00043E70 File Offset: 0x00042070
		private object InternalReadContentAsObject(bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadContentAsObject(unwrapTypedValue, out text);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00043E88 File Offset: 0x00042088
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

		// Token: 0x06001014 RID: 4116 RVA: 0x00043F90 File Offset: 0x00042190
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType)
		{
			return this.InternalReadElementContentAsObject(out xmlType, false);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00043F9C File Offset: 0x0004219C
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadElementContentAsObject(out xmlType, unwrapTypedValue, out text);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00043FB4 File Offset: 0x000421B4
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

		// Token: 0x06001017 RID: 4119 RVA: 0x000440CC File Offset: 0x000422CC
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

		// Token: 0x06001018 RID: 4120 RVA: 0x000441EC File Offset: 0x000423EC
		private void SwitchReader()
		{
			XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
			if (xsdCachingReader != null)
			{
				this.coreReader = xsdCachingReader.GetCoreReader();
			}
			this.replayCache = false;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0004421C File Offset: 0x0004241C
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

		// Token: 0x0600101A RID: 4122 RVA: 0x00044338 File Offset: 0x00042538
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

		// Token: 0x0600101B RID: 4123 RVA: 0x000444A0 File Offset: 0x000426A0
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

		// Token: 0x0600101C RID: 4124 RVA: 0x00044514 File Offset: 0x00042714
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

		// Token: 0x0600101D RID: 4125 RVA: 0x00044580 File Offset: 0x00042780
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

		// Token: 0x0600101E RID: 4126 RVA: 0x000445DD File Offset: 0x000427DD
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

		// Token: 0x0600101F RID: 4127 RVA: 0x00044611 File Offset: 0x00042811
		internal void CachingCallBack(XsdCachingReader cachingReader)
		{
			this.coreReader = cachingReader.GetCoreReader();
			this.lineInfo = cachingReader.GetLineInfo();
			this.replayCache = false;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00044634 File Offset: 0x00042834
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

		// Token: 0x06001021 RID: 4129 RVA: 0x00044684 File Offset: 0x00042884
		public override Task<string> GetValueAsync()
		{
			if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
			{
				return Task.FromResult<string>(this.cachedNode.RawValue);
			}
			return this.coreReader.GetValueAsync();
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000446AB File Offset: 0x000428AB
		public override Task<object> ReadContentAsObjectAsync()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsObjectAsync(true);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000446D0 File Offset: 0x000428D0
		public override Task<string> ReadContentAsStringAsync()
		{
			XsdValidatingReader.<ReadContentAsStringAsync>d__187 <ReadContentAsStringAsync>d__;
			<ReadContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadContentAsStringAsync>d__.<>4__this = this;
			<ReadContentAsStringAsync>d__.<>1__state = -1;
			<ReadContentAsStringAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadContentAsStringAsync>d__187>(ref <ReadContentAsStringAsync>d__);
			return <ReadContentAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00044714 File Offset: 0x00042914
		public override Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XsdValidatingReader.<ReadContentAsAsync>d__188 <ReadContentAsAsync>d__;
			<ReadContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadContentAsAsync>d__.<>4__this = this;
			<ReadContentAsAsync>d__.returnType = returnType;
			<ReadContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadContentAsAsync>d__.<>1__state = -1;
			<ReadContentAsAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadContentAsAsync>d__188>(ref <ReadContentAsAsync>d__);
			return <ReadContentAsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00044768 File Offset: 0x00042968
		public override Task<object> ReadElementContentAsObjectAsync()
		{
			XsdValidatingReader.<ReadElementContentAsObjectAsync>d__189 <ReadElementContentAsObjectAsync>d__;
			<ReadElementContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadElementContentAsObjectAsync>d__.<>4__this = this;
			<ReadElementContentAsObjectAsync>d__.<>1__state = -1;
			<ReadElementContentAsObjectAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsObjectAsync>d__189>(ref <ReadElementContentAsObjectAsync>d__);
			return <ReadElementContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000447AC File Offset: 0x000429AC
		public override Task<string> ReadElementContentAsStringAsync()
		{
			XsdValidatingReader.<ReadElementContentAsStringAsync>d__190 <ReadElementContentAsStringAsync>d__;
			<ReadElementContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadElementContentAsStringAsync>d__.<>4__this = this;
			<ReadElementContentAsStringAsync>d__.<>1__state = -1;
			<ReadElementContentAsStringAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsStringAsync>d__190>(ref <ReadElementContentAsStringAsync>d__);
			return <ReadElementContentAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000447F0 File Offset: 0x000429F0
		public override Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XsdValidatingReader.<ReadElementContentAsAsync>d__191 <ReadElementContentAsAsync>d__;
			<ReadElementContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadElementContentAsAsync>d__.<>4__this = this;
			<ReadElementContentAsAsync>d__.returnType = returnType;
			<ReadElementContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadElementContentAsAsync>d__.<>1__state = -1;
			<ReadElementContentAsAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsAsync>d__191>(ref <ReadElementContentAsAsync>d__);
			return <ReadElementContentAsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00044844 File Offset: 0x00042A44
		private Task<bool> ReadAsync_Read(Task<bool> task)
		{
			if (!task.IsSuccess())
			{
				return this._ReadAsync_Read(task);
			}
			if (task.Result)
			{
				return this.ProcessReaderEventAsync().ReturnTaskBoolWhenFinish(true);
			}
			this.validator.EndValidation();
			if (this.coreReader.EOF)
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
			}
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0004489C File Offset: 0x00042A9C
		private Task<bool> _ReadAsync_Read(Task<bool> task)
		{
			XsdValidatingReader.<_ReadAsync_Read>d__193 <_ReadAsync_Read>d__;
			<_ReadAsync_Read>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ReadAsync_Read>d__.<>4__this = this;
			<_ReadAsync_Read>d__.task = task;
			<_ReadAsync_Read>d__.<>1__state = -1;
			<_ReadAsync_Read>d__.<>t__builder.Start<XsdValidatingReader.<_ReadAsync_Read>d__193>(ref <_ReadAsync_Read>d__);
			return <_ReadAsync_Read>d__.<>t__builder.Task;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000448E7 File Offset: 0x00042AE7
		private Task<bool> ReadAsync_ReadAhead(Task task)
		{
			if (task.IsSuccess())
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				return AsyncHelper.DoneTaskTrue;
			}
			return this._ReadAsync_ReadAhead(task);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00044908 File Offset: 0x00042B08
		private Task<bool> _ReadAsync_ReadAhead(Task task)
		{
			XsdValidatingReader.<_ReadAsync_ReadAhead>d__195 <_ReadAsync_ReadAhead>d__;
			<_ReadAsync_ReadAhead>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ReadAsync_ReadAhead>d__.<>4__this = this;
			<_ReadAsync_ReadAhead>d__.task = task;
			<_ReadAsync_ReadAhead>d__.<>1__state = -1;
			<_ReadAsync_ReadAhead>d__.<>t__builder.Start<XsdValidatingReader.<_ReadAsync_ReadAhead>d__195>(ref <_ReadAsync_ReadAhead>d__);
			return <_ReadAsync_ReadAhead>d__.<>t__builder.Task;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00044954 File Offset: 0x00042B54
		public override Task<bool> ReadAsync()
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
					goto IL_59;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				break;
			case XsdValidatingReader.ValidatingReaderState.None:
				goto IL_F0;
			case XsdValidatingReader.ValidatingReaderState.Init:
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					return this.ProcessReaderEventAsync().ReturnTaskBoolWhenFinish(true);
				}
				break;
			case XsdValidatingReader.ValidatingReaderState.Read:
				break;
			case XsdValidatingReader.ValidatingReaderState.ParseInlineSchema:
				goto IL_59;
			case XsdValidatingReader.ValidatingReaderState.ReadAhead:
			{
				this.ClearAttributesInfo();
				Task task = this.ProcessReaderEventAsync();
				return this.ReadAsync_ReadAhead(task);
			}
			case XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent:
				this.validationState = this.savedState;
				return this.readBinaryHelper.FinishAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			case XsdValidatingReader.ValidatingReaderState.ReaderClosed:
			case XsdValidatingReader.ValidatingReaderState.EOF:
				return AsyncHelper.DoneTaskFalse;
			default:
				goto IL_F0;
			}
			Task<bool> task2 = this.coreReader.ReadAsync();
			return this.ReadAsync_Read(task2);
			IL_59:
			return this.ProcessInlineSchemaAsync().ReturnTaskBoolWhenFinish(true);
			IL_F0:
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00044A58 File Offset: 0x00042C58
		public override Task SkipAsync()
		{
			XsdValidatingReader.<SkipAsync>d__197 <SkipAsync>d__;
			<SkipAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipAsync>d__.<>4__this = this;
			<SkipAsync>d__.<>1__state = -1;
			<SkipAsync>d__.<>t__builder.Start<XsdValidatingReader.<SkipAsync>d__197>(ref <SkipAsync>d__);
			return <SkipAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00044A9C File Offset: 0x00042C9C
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XsdValidatingReader.<ReadContentAsBase64Async>d__198 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<XsdValidatingReader.<ReadContentAsBase64Async>d__198>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00044AF8 File Offset: 0x00042CF8
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XsdValidatingReader.<ReadContentAsBinHexAsync>d__199 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadContentAsBinHexAsync>d__199>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00044B54 File Offset: 0x00042D54
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XsdValidatingReader.<ReadElementContentAsBase64Async>d__200 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsBase64Async>d__200>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00044BB0 File Offset: 0x00042DB0
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XsdValidatingReader.<ReadElementContentAsBinHexAsync>d__201 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadElementContentAsBinHexAsync>d__201>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00044C0C File Offset: 0x00042E0C
		private Task ProcessReaderEventAsync()
		{
			if (this.replayCache)
			{
				return AsyncHelper.DoneTask;
			}
			switch (this.coreReader.NodeType)
			{
			case XmlNodeType.Element:
				return this.ProcessElementEventAsync();
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
				break;
			case XmlNodeType.EntityReference:
				throw new InvalidOperationException();
			case XmlNodeType.DocumentType:
				this.validator.SetDtdSchemaInfo(this.coreReader.DtdInfo);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
				break;
			case XmlNodeType.EndElement:
				return this.ProcessEndElementEventAsync();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00044CDC File Offset: 0x00042EDC
		private Task ProcessElementEventAsync()
		{
			XsdValidatingReader.<ProcessElementEventAsync>d__203 <ProcessElementEventAsync>d__;
			<ProcessElementEventAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessElementEventAsync>d__.<>4__this = this;
			<ProcessElementEventAsync>d__.<>1__state = -1;
			<ProcessElementEventAsync>d__.<>t__builder.Start<XsdValidatingReader.<ProcessElementEventAsync>d__203>(ref <ProcessElementEventAsync>d__);
			return <ProcessElementEventAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00044D20 File Offset: 0x00042F20
		private Task ProcessEndElementEventAsync()
		{
			XsdValidatingReader.<ProcessEndElementEventAsync>d__204 <ProcessEndElementEventAsync>d__;
			<ProcessEndElementEventAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessEndElementEventAsync>d__.<>4__this = this;
			<ProcessEndElementEventAsync>d__.<>1__state = -1;
			<ProcessEndElementEventAsync>d__.<>t__builder.Start<XsdValidatingReader.<ProcessEndElementEventAsync>d__204>(ref <ProcessEndElementEventAsync>d__);
			return <ProcessEndElementEventAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00044D64 File Offset: 0x00042F64
		private Task ProcessInlineSchemaAsync()
		{
			XsdValidatingReader.<ProcessInlineSchemaAsync>d__205 <ProcessInlineSchemaAsync>d__;
			<ProcessInlineSchemaAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessInlineSchemaAsync>d__.<>4__this = this;
			<ProcessInlineSchemaAsync>d__.<>1__state = -1;
			<ProcessInlineSchemaAsync>d__.<>t__builder.Start<XsdValidatingReader.<ProcessInlineSchemaAsync>d__205>(ref <ProcessInlineSchemaAsync>d__);
			return <ProcessInlineSchemaAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00044DA7 File Offset: 0x00042FA7
		private Task<object> InternalReadContentAsObjectAsync()
		{
			return this.InternalReadContentAsObjectAsync(false);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00044DB0 File Offset: 0x00042FB0
		private Task<object> InternalReadContentAsObjectAsync(bool unwrapTypedValue)
		{
			XsdValidatingReader.<InternalReadContentAsObjectAsync>d__207 <InternalReadContentAsObjectAsync>d__;
			<InternalReadContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<InternalReadContentAsObjectAsync>d__.<>4__this = this;
			<InternalReadContentAsObjectAsync>d__.unwrapTypedValue = unwrapTypedValue;
			<InternalReadContentAsObjectAsync>d__.<>1__state = -1;
			<InternalReadContentAsObjectAsync>d__.<>t__builder.Start<XsdValidatingReader.<InternalReadContentAsObjectAsync>d__207>(ref <InternalReadContentAsObjectAsync>d__);
			return <InternalReadContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00044DFC File Offset: 0x00042FFC
		private Task<Tuple<string, object>> InternalReadContentAsObjectTupleAsync(bool unwrapTypedValue)
		{
			XsdValidatingReader.<InternalReadContentAsObjectTupleAsync>d__208 <InternalReadContentAsObjectTupleAsync>d__;
			<InternalReadContentAsObjectTupleAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<string, object>>.Create();
			<InternalReadContentAsObjectTupleAsync>d__.<>4__this = this;
			<InternalReadContentAsObjectTupleAsync>d__.unwrapTypedValue = unwrapTypedValue;
			<InternalReadContentAsObjectTupleAsync>d__.<>1__state = -1;
			<InternalReadContentAsObjectTupleAsync>d__.<>t__builder.Start<XsdValidatingReader.<InternalReadContentAsObjectTupleAsync>d__208>(ref <InternalReadContentAsObjectTupleAsync>d__);
			return <InternalReadContentAsObjectTupleAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00044E47 File Offset: 0x00043047
		private Task<Tuple<XmlSchemaType, object>> InternalReadElementContentAsObjectAsync()
		{
			return this.InternalReadElementContentAsObjectAsync(false);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00044E50 File Offset: 0x00043050
		private Task<Tuple<XmlSchemaType, object>> InternalReadElementContentAsObjectAsync(bool unwrapTypedValue)
		{
			XsdValidatingReader.<InternalReadElementContentAsObjectAsync>d__210 <InternalReadElementContentAsObjectAsync>d__;
			<InternalReadElementContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<XmlSchemaType, object>>.Create();
			<InternalReadElementContentAsObjectAsync>d__.<>4__this = this;
			<InternalReadElementContentAsObjectAsync>d__.unwrapTypedValue = unwrapTypedValue;
			<InternalReadElementContentAsObjectAsync>d__.<>1__state = -1;
			<InternalReadElementContentAsObjectAsync>d__.<>t__builder.Start<XsdValidatingReader.<InternalReadElementContentAsObjectAsync>d__210>(ref <InternalReadElementContentAsObjectAsync>d__);
			return <InternalReadElementContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00044E9C File Offset: 0x0004309C
		private Task<Tuple<XmlSchemaType, string, object>> InternalReadElementContentAsObjectTupleAsync(bool unwrapTypedValue)
		{
			XsdValidatingReader.<InternalReadElementContentAsObjectTupleAsync>d__211 <InternalReadElementContentAsObjectTupleAsync>d__;
			<InternalReadElementContentAsObjectTupleAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<XmlSchemaType, string, object>>.Create();
			<InternalReadElementContentAsObjectTupleAsync>d__.<>4__this = this;
			<InternalReadElementContentAsObjectTupleAsync>d__.unwrapTypedValue = unwrapTypedValue;
			<InternalReadElementContentAsObjectTupleAsync>d__.<>1__state = -1;
			<InternalReadElementContentAsObjectTupleAsync>d__.<>t__builder.Start<XsdValidatingReader.<InternalReadElementContentAsObjectTupleAsync>d__211>(ref <InternalReadElementContentAsObjectTupleAsync>d__);
			return <InternalReadElementContentAsObjectTupleAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00044EE8 File Offset: 0x000430E8
		private Task<object> ReadTillEndElementAsync()
		{
			XsdValidatingReader.<ReadTillEndElementAsync>d__212 <ReadTillEndElementAsync>d__;
			<ReadTillEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadTillEndElementAsync>d__.<>4__this = this;
			<ReadTillEndElementAsync>d__.<>1__state = -1;
			<ReadTillEndElementAsync>d__.<>t__builder.Start<XsdValidatingReader.<ReadTillEndElementAsync>d__212>(ref <ReadTillEndElementAsync>d__);
			return <ReadTillEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000473 RID: 1139
		private XmlReader coreReader;

		// Token: 0x04000474 RID: 1140
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x04000475 RID: 1141
		private IXmlNamespaceResolver thisNSResolver;

		// Token: 0x04000476 RID: 1142
		private XmlSchemaValidator validator;

		// Token: 0x04000477 RID: 1143
		private XmlResolver xmlResolver;

		// Token: 0x04000478 RID: 1144
		private ValidationEventHandler validationEvent;

		// Token: 0x04000479 RID: 1145
		private XsdValidatingReader.ValidatingReaderState validationState;

		// Token: 0x0400047A RID: 1146
		private XmlValueGetter valueGetter;

		// Token: 0x0400047B RID: 1147
		private XmlNamespaceManager nsManager;

		// Token: 0x0400047C RID: 1148
		private bool manageNamespaces;

		// Token: 0x0400047D RID: 1149
		private bool processInlineSchema;

		// Token: 0x0400047E RID: 1150
		private bool replayCache;

		// Token: 0x0400047F RID: 1151
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x04000480 RID: 1152
		private AttributePSVIInfo attributePSVI;

		// Token: 0x04000481 RID: 1153
		private int attributeCount;

		// Token: 0x04000482 RID: 1154
		private int coreReaderAttributeCount;

		// Token: 0x04000483 RID: 1155
		private int currentAttrIndex;

		// Token: 0x04000484 RID: 1156
		private AttributePSVIInfo[] attributePSVINodes;

		// Token: 0x04000485 RID: 1157
		private ArrayList defaultAttributes;

		// Token: 0x04000486 RID: 1158
		private Parser inlineSchemaParser;

		// Token: 0x04000487 RID: 1159
		private object atomicValue;

		// Token: 0x04000488 RID: 1160
		private XmlSchemaInfo xmlSchemaInfo;

		// Token: 0x04000489 RID: 1161
		private string originalAtomicValueString;

		// Token: 0x0400048A RID: 1162
		private XmlNameTable coreReaderNameTable;

		// Token: 0x0400048B RID: 1163
		private XsdCachingReader cachingReader;

		// Token: 0x0400048C RID: 1164
		private ValidatingReaderNodeData textNode;

		// Token: 0x0400048D RID: 1165
		private string NsXmlNs;

		// Token: 0x0400048E RID: 1166
		private string NsXs;

		// Token: 0x0400048F RID: 1167
		private string NsXsi;

		// Token: 0x04000490 RID: 1168
		private string XsiType;

		// Token: 0x04000491 RID: 1169
		private string XsiNil;

		// Token: 0x04000492 RID: 1170
		private string XsdSchema;

		// Token: 0x04000493 RID: 1171
		private string XsiSchemaLocation;

		// Token: 0x04000494 RID: 1172
		private string XsiNoNamespaceSchemaLocation;

		// Token: 0x04000495 RID: 1173
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000496 RID: 1174
		private IXmlLineInfo lineInfo;

		// Token: 0x04000497 RID: 1175
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04000498 RID: 1176
		private XsdValidatingReader.ValidatingReaderState savedState;

		// Token: 0x04000499 RID: 1177
		private const int InitialAttributeCount = 8;

		// Token: 0x0400049A RID: 1178
		private static volatile Type TypeOfString;

		// Token: 0x0200041F RID: 1055
		private enum ValidatingReaderState
		{
			// Token: 0x04001B90 RID: 7056
			None,
			// Token: 0x04001B91 RID: 7057
			Init,
			// Token: 0x04001B92 RID: 7058
			Read,
			// Token: 0x04001B93 RID: 7059
			OnDefaultAttribute = -1,
			// Token: 0x04001B94 RID: 7060
			OnReadAttributeValue = -2,
			// Token: 0x04001B95 RID: 7061
			OnAttribute = 3,
			// Token: 0x04001B96 RID: 7062
			ClearAttributes,
			// Token: 0x04001B97 RID: 7063
			ParseInlineSchema,
			// Token: 0x04001B98 RID: 7064
			ReadAhead,
			// Token: 0x04001B99 RID: 7065
			OnReadBinaryContent,
			// Token: 0x04001B9A RID: 7066
			ReaderClosed,
			// Token: 0x04001B9B RID: 7067
			EOF,
			// Token: 0x04001B9C RID: 7068
			Error
		}
	}
}
