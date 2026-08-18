using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200009D RID: 157
	internal sealed class XmlValidatingReaderImpl : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x00027CB0 File Offset: 0x00026CB0
		internal XmlValidatingReaderImpl(XmlReader reader)
		{
			this.outerReader = this;
			this.coreReader = reader;
			this.coreReaderNSResolver = (reader as IXmlNamespaceResolver);
			this.coreReaderImpl = (reader as XmlTextReaderImpl);
			if (this.coreReaderImpl == null)
			{
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					this.coreReaderImpl = xmlTextReader.Impl;
				}
			}
			if (this.coreReaderImpl == null)
			{
				throw new ArgumentException(Res.GetString("Arg_ExpectingXmlTextReader"), "reader");
			}
			this.coreReaderImpl.EntityHandling = EntityHandling.ExpandEntities;
			this.coreReaderImpl.XmlValidatingReaderCompatibilityMode = true;
			this.processIdentityConstraints = true;
			this.schemaCollection = new XmlSchemaCollection(this.coreReader.NameTable);
			this.schemaCollection.XmlResolver = this.GetResolver();
			this.internalEventHandler = new ValidationEventHandler(this.InternalValidationCallback);
			this.eventHandler = this.internalEventHandler;
			this.coreReaderImpl.ValidationEventHandler = this.internalEventHandler;
			this.validationType = ValidationType.Auto;
			this.SetupValidation(ValidationType.Auto);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00027DB0 File Offset: 0x00026DB0
		internal XmlValidatingReaderImpl(string xmlFragment, XmlNodeType fragType, XmlParserContext context) : this(new XmlTextReader(xmlFragment, fragType, context))
		{
			if (this.coreReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = this.GetResolver().ResolveUri(null, this.coreReader.BaseURI);
			}
			if (context != null)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext;
				this.parserContext = context;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00027E14 File Offset: 0x00026E14
		internal XmlValidatingReaderImpl(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context) : this(new XmlTextReader(xmlFragment, fragType, context))
		{
			if (this.coreReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = this.GetResolver().ResolveUri(null, this.coreReader.BaseURI);
			}
			if (context != null)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext;
				this.parserContext = context;
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00027E78 File Offset: 0x00026E78
		internal XmlValidatingReaderImpl(XmlReader reader, ValidationEventHandler settingsEventHandler, bool processIdentityConstraints)
		{
			this.outerReader = this;
			this.coreReader = reader;
			this.coreReaderImpl = (reader as XmlTextReaderImpl);
			if (this.coreReaderImpl == null)
			{
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					this.coreReaderImpl = xmlTextReader.Impl;
				}
			}
			if (this.coreReaderImpl == null)
			{
				throw new ArgumentException(Res.GetString("Arg_ExpectingXmlTextReader"), "reader");
			}
			this.coreReaderImpl.XmlValidatingReaderCompatibilityMode = true;
			this.coreReaderNSResolver = (reader as IXmlNamespaceResolver);
			this.processIdentityConstraints = processIdentityConstraints;
			this.schemaCollection = new XmlSchemaCollection(this.coreReader.NameTable);
			this.schemaCollection.XmlResolver = this.GetResolver();
			if (settingsEventHandler == null)
			{
				this.internalEventHandler = new ValidationEventHandler(this.InternalValidationCallback);
				this.eventHandler = this.internalEventHandler;
				this.coreReaderImpl.ValidationEventHandler = this.internalEventHandler;
			}
			else
			{
				this.eventHandler = settingsEventHandler;
				this.coreReaderImpl.ValidationEventHandler = settingsEventHandler;
			}
			this.validationType = ValidationType.DTD;
			this.SetupValidation(ValidationType.DTD);
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00027F84 File Offset: 0x00026F84
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings;
				if (this.coreReaderImpl.V1Compat)
				{
					xmlReaderSettings = null;
				}
				else
				{
					xmlReaderSettings = this.coreReader.Settings;
				}
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				else
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.ValidationType = ValidationType.DTD;
				if (!this.processIdentityConstraints)
				{
					xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessIdentityConstraints;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00027FE6 File Offset: 0x00026FE6
		public override XmlNodeType NodeType
		{
			get
			{
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00027FF3 File Offset: 0x00026FF3
		public override string Name
		{
			get
			{
				return this.coreReader.Name;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00028000 File Offset: 0x00027000
		public override string LocalName
		{
			get
			{
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0002800D File Offset: 0x0002700D
		public override string NamespaceURI
		{
			get
			{
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0002801A File Offset: 0x0002701A
		public override string Prefix
		{
			get
			{
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00028027 File Offset: 0x00027027
		public override bool HasValue
		{
			get
			{
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00028034 File Offset: 0x00027034
		public override string Value
		{
			get
			{
				return this.coreReader.Value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00028041 File Offset: 0x00027041
		public override int Depth
		{
			get
			{
				return this.coreReader.Depth;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0002804E File Offset: 0x0002704E
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0002805B File Offset: 0x0002705B
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00028068 File Offset: 0x00027068
		public override bool IsDefault
		{
			get
			{
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00028075 File Offset: 0x00027075
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00028082 File Offset: 0x00027082
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0002808F File Offset: 0x0002708F
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002809C File Offset: 0x0002709C
		public override ReadState ReadState
		{
			get
			{
				if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.Init)
				{
					return this.coreReader.ReadState;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000280B4 File Offset: 0x000270B4
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000280C1 File Offset: 0x000270C1
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x000280CE File Offset: 0x000270CE
		internal Encoding Encoding
		{
			get
			{
				return this.coreReaderImpl.Encoding;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x000280DB File Offset: 0x000270DB
		public override int AttributeCount
		{
			get
			{
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000280E8 File Offset: 0x000270E8
		public override string GetAttribute(string name)
		{
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000280F6 File Offset: 0x000270F6
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.coreReader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00028105 File Offset: 0x00027105
		public override string GetAttribute(int i)
		{
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00028113 File Offset: 0x00027113
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002812D File Offset: 0x0002712D
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (!this.coreReader.MoveToAttribute(localName, namespaceURI))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00028148 File Offset: 0x00027148
		public override void MoveToAttribute(int i)
		{
			this.coreReader.MoveToAttribute(i);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002815D File Offset: 0x0002715D
		public override bool MoveToFirstAttribute()
		{
			if (!this.coreReader.MoveToFirstAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00028176 File Offset: 0x00027176
		public override bool MoveToNextAttribute()
		{
			if (!this.coreReader.MoveToNextAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002818F File Offset: 0x0002718F
		public override bool MoveToElement()
		{
			if (!this.coreReader.MoveToElement())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000281A8 File Offset: 0x000271A8
		public override bool Read()
		{
			switch (this.parsingFunction)
			{
			case XmlValidatingReaderImpl.ParsingFunction.Read:
				break;
			case XmlValidatingReaderImpl.ParsingFunction.Init:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessCoreReaderEvent();
					return true;
				}
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.ParseDtdFromParserContext();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.ResolveEntityInternally();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.readBinaryHelper.Finish();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ReaderClosed:
			case XmlValidatingReaderImpl.ParsingFunction.Error:
				return false;
			default:
				return false;
			}
			if (this.coreReader.Read())
			{
				this.ProcessCoreReaderEvent();
				return true;
			}
			this.validator.CompleteValidation();
			return false;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00028254 File Offset: 0x00027254
		public override void Close()
		{
			this.coreReader.Close();
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00028268 File Offset: 0x00027268
		public override string LookupNamespace(string prefix)
		{
			return this.coreReaderImpl.LookupNamespace(prefix);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00028276 File Offset: 0x00027276
		public override bool ReadAttributeValue()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.readBinaryHelper.Finish();
			}
			if (!this.coreReader.ReadAttributeValue())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000282AA File Offset: 0x000272AA
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000282B0 File Offset: 0x000272B0
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return result;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00028308 File Offset: 0x00027308
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return result;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00028360 File Offset: 0x00027360
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return result;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000283B8 File Offset: 0x000273B8
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return result;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002840E File Offset: 0x0002740E
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00028411 File Offset: 0x00027411
		public override void ResolveEntity()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			}
			this.coreReader.ResolveEntity();
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0002842E File Offset: 0x0002742E
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x00028436 File Offset: 0x00027436
		internal XmlReader OuterReader
		{
			get
			{
				return this.outerReader;
			}
			set
			{
				this.outerReader = value;
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002843F File Offset: 0x0002743F
		internal void MoveOffEntityReference()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally && !this.outerReader.Read())
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00028475 File Offset: 0x00027475
		public override string ReadString()
		{
			this.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00028483 File Offset: 0x00027483
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00028486 File Offset: 0x00027486
		public int LineNumber
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LineNumber;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00028498 File Offset: 0x00027498
		public int LinePosition
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LinePosition;
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000284AA File Offset: 0x000274AA
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000284B3 File Offset: 0x000274B3
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000284BC File Offset: 0x000274BC
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.LookupPrefix(namespaceName);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000284C5 File Offset: 0x000274C5
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.coreReaderNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000284D3 File Offset: 0x000274D3
		internal string LookupPrefix(string namespaceName)
		{
			return this.coreReaderNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060008E7 RID: 2279 RVA: 0x000284E4 File Offset: 0x000274E4
		// (remove) Token: 0x060008E8 RID: 2280 RVA: 0x0002853E File Offset: 0x0002753E
		internal event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, this.internalEventHandler);
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, value);
				if (this.eventHandler == null)
				{
					this.eventHandler = this.internalEventHandler;
				}
				this.UpdateHandlers();
			}
			remove
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, value);
				if (this.eventHandler == null)
				{
					this.eventHandler = this.internalEventHandler;
				}
				this.UpdateHandlers();
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00028574 File Offset: 0x00027574
		internal object SchemaType
		{
			get
			{
				if (this.validationType == ValidationType.None)
				{
					return null;
				}
				XmlSchemaType xmlSchemaType = this.coreReaderImpl.InternalSchemaType as XmlSchemaType;
				if (xmlSchemaType != null && xmlSchemaType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return xmlSchemaType.Datatype;
				}
				return this.coreReaderImpl.InternalSchemaType;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x000285C8 File Offset: 0x000275C8
		internal XmlReader Reader
		{
			get
			{
				return this.coreReader;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000285D0 File Offset: 0x000275D0
		internal XmlTextReaderImpl ReaderImpl
		{
			get
			{
				return this.coreReaderImpl;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x000285D8 File Offset: 0x000275D8
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x000285E0 File Offset: 0x000275E0
		internal ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				if (this.ReadState != ReadState.Initial)
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
				}
				this.validationType = value;
				this.SetupValidation(value);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00028608 File Offset: 0x00027608
		internal XmlSchemaCollection Schemas
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00028610 File Offset: 0x00027610
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0002861D File Offset: 0x0002761D
		internal EntityHandling EntityHandling
		{
			get
			{
				return this.coreReaderImpl.EntityHandling;
			}
			set
			{
				this.coreReaderImpl.EntityHandling = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0002862B File Offset: 0x0002762B
		internal XmlResolver XmlResolver
		{
			set
			{
				this.coreReaderImpl.XmlResolver = value;
				this.validator.XmlResolver = value;
				this.schemaCollection.XmlResolver = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00028651 File Offset: 0x00027651
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x0002865E File Offset: 0x0002765E
		internal bool Namespaces
		{
			get
			{
				return this.coreReaderImpl.Namespaces;
			}
			set
			{
				this.coreReaderImpl.Namespaces = value;
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002866C File Offset: 0x0002766C
		public object ReadTypedValue()
		{
			if (this.validationType == ValidationType.None)
			{
				return null;
			}
			XmlNodeType nodeType = this.outerReader.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
			{
				if (this.SchemaType == null)
				{
					return null;
				}
				XmlSchemaDatatype xmlSchemaDatatype = (this.SchemaType is XmlSchemaDatatype) ? ((XmlSchemaDatatype)this.SchemaType) : ((XmlSchemaType)this.SchemaType).Datatype;
				if (xmlSchemaDatatype != null)
				{
					if (!this.outerReader.IsEmptyElement)
					{
						while (this.outerReader.Read())
						{
							XmlNodeType nodeType2 = this.outerReader.NodeType;
							if (nodeType2 != XmlNodeType.CDATA && nodeType2 != XmlNodeType.Text && nodeType2 != XmlNodeType.Whitespace && nodeType2 != XmlNodeType.SignificantWhitespace && nodeType2 != XmlNodeType.Comment && nodeType2 != XmlNodeType.ProcessingInstruction)
							{
								if (this.outerReader.NodeType != XmlNodeType.EndElement)
								{
									throw new XmlException("Xml_InvalidNodeType", this.outerReader.NodeType.ToString());
								}
								goto IL_F9;
							}
						}
						throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
					}
					IL_F9:
					return this.coreReaderImpl.InternalTypedValue;
				}
				return null;
			}
			case XmlNodeType.Attribute:
				return this.coreReaderImpl.InternalTypedValue;
			default:
				if (nodeType == XmlNodeType.EndElement)
				{
					return null;
				}
				if (this.coreReaderImpl.V1Compat)
				{
					return null;
				}
				return this.Value;
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00028798 File Offset: 0x00027798
		private void ParseDtdFromParserContext()
		{
			if (this.parserContext.DocTypeName == null || this.parserContext.DocTypeName.Length == 0)
			{
				return;
			}
			this.coreReaderImpl.DtdSchemaInfo = DtdParser.Parse(this.coreReaderImpl, this.parserContext.BaseURI, this.parserContext.DocTypeName, this.parserContext.PublicId, this.parserContext.SystemId, this.parserContext.InternalSubset);
			this.ValidateDtd();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00028818 File Offset: 0x00027818
		private void ValidateDtd()
		{
			SchemaInfo dtdSchemaInfo = this.coreReaderImpl.DtdSchemaInfo;
			if (dtdSchemaInfo != null)
			{
				switch (this.validationType)
				{
				case ValidationType.None:
				case ValidationType.DTD:
					break;
				case ValidationType.Auto:
					this.SetupValidation(ValidationType.DTD);
					break;
				default:
					return;
				}
				this.validator.SchemaInfo = dtdSchemaInfo;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00028864 File Offset: 0x00027864
		private void ResolveEntityInternally()
		{
			int depth = this.coreReader.Depth;
			this.outerReader.ResolveEntity();
			while (this.outerReader.Read() && this.coreReader.Depth > depth)
			{
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000288A3 File Offset: 0x000278A3
		private void UpdateHandlers()
		{
			this.validator.EventHandler = this.eventHandler;
			this.coreReaderImpl.ValidationEventHandler = ((this.validationType != ValidationType.None) ? this.eventHandler : null);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000288D4 File Offset: 0x000278D4
		private void SetupValidation(ValidationType valType)
		{
			this.validator = BaseValidator.CreateInstance(valType, this, this.schemaCollection, this.eventHandler, this.processIdentityConstraints);
			XmlResolver resolver = this.GetResolver();
			this.validator.XmlResolver = resolver;
			if (this.outerReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = ((resolver == null) ? new Uri(this.outerReader.BaseURI, UriKind.RelativeOrAbsolute) : resolver.ResolveUri(null, this.outerReader.BaseURI));
			}
			this.UpdateHandlers();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002895F File Offset: 0x0002795F
		private XmlResolver GetResolver()
		{
			return this.coreReaderImpl.GetResolver();
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002896C File Offset: 0x0002796C
		private void InternalValidationCallback(object sender, ValidationEventArgs e)
		{
			if (this.validationType != ValidationType.None && e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00028988 File Offset: 0x00027988
		private void ProcessCoreReaderEvent()
		{
			XmlNodeType nodeType = this.coreReader.NodeType;
			if (nodeType != XmlNodeType.EntityReference)
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.ValidateDtd();
					return;
				}
				if (nodeType == XmlNodeType.Whitespace && (this.coreReader.Depth > 0 || this.coreReaderImpl.FragmentType != XmlNodeType.Document) && this.validator.PreserveWhitespace)
				{
					this.coreReaderImpl.ChangeCurrentNodeType(XmlNodeType.SignificantWhitespace);
				}
			}
			else
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally;
			}
			this.coreReaderImpl.InternalSchemaType = null;
			this.coreReaderImpl.InternalTypedValue = null;
			this.validator.Validate();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00028A19 File Offset: 0x00027A19
		internal void Close(bool closeStream)
		{
			this.coreReaderImpl.Close(closeStream);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00028A2E File Offset: 0x00027A2E
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x00028A36 File Offset: 0x00027A36
		internal BaseValidator Validator
		{
			get
			{
				return this.validator;
			}
			set
			{
				this.validator = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00028A3F File Offset: 0x00027A3F
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.coreReaderImpl.NamespaceManager;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00028A4C File Offset: 0x00027A4C
		internal bool StandAlone
		{
			get
			{
				return this.coreReaderImpl.StandAlone;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00028A59 File Offset: 0x00027A59
		internal object SchemaTypeObject
		{
			set
			{
				this.coreReaderImpl.InternalSchemaType = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00028A67 File Offset: 0x00027A67
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00028A74 File Offset: 0x00027A74
		internal object TypedValueObject
		{
			get
			{
				return this.coreReaderImpl.InternalTypedValue;
			}
			set
			{
				this.coreReaderImpl.InternalTypedValue = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00028A82 File Offset: 0x00027A82
		internal bool Normalization
		{
			get
			{
				return this.coreReaderImpl.Normalization;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00028A8F File Offset: 0x00027A8F
		internal bool AddDefaultAttribute(SchemaAttDef attdef)
		{
			return this.coreReaderImpl.AddDefaultAttribute(attdef, false);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00028A9E File Offset: 0x00027A9E
		internal SchemaInfo GetSchemaInfo()
		{
			return this.validator.SchemaInfo;
		}

		// Token: 0x040007A4 RID: 1956
		private XmlReader coreReader;

		// Token: 0x040007A5 RID: 1957
		private XmlTextReaderImpl coreReaderImpl;

		// Token: 0x040007A6 RID: 1958
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x040007A7 RID: 1959
		private ValidationType validationType;

		// Token: 0x040007A8 RID: 1960
		private BaseValidator validator;

		// Token: 0x040007A9 RID: 1961
		private XmlSchemaCollection schemaCollection;

		// Token: 0x040007AA RID: 1962
		private bool processIdentityConstraints;

		// Token: 0x040007AB RID: 1963
		private XmlValidatingReaderImpl.ParsingFunction parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Init;

		// Token: 0x040007AC RID: 1964
		private ValidationEventHandler internalEventHandler;

		// Token: 0x040007AD RID: 1965
		private ValidationEventHandler eventHandler;

		// Token: 0x040007AE RID: 1966
		private XmlParserContext parserContext;

		// Token: 0x040007AF RID: 1967
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x040007B0 RID: 1968
		private XmlReader outerReader;

		// Token: 0x0200009E RID: 158
		private enum ParsingFunction
		{
			// Token: 0x040007B2 RID: 1970
			Read,
			// Token: 0x040007B3 RID: 1971
			Init,
			// Token: 0x040007B4 RID: 1972
			ParseDtdFromContext,
			// Token: 0x040007B5 RID: 1973
			ResolveEntityInternally,
			// Token: 0x040007B6 RID: 1974
			InReadBinaryContent,
			// Token: 0x040007B7 RID: 1975
			ReaderClosed,
			// Token: 0x040007B8 RID: 1976
			Error,
			// Token: 0x040007B9 RID: 1977
			None
		}
	}
}
