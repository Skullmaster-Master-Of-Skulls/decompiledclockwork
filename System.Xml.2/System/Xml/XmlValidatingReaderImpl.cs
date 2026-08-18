using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000E0 RID: 224
	internal sealed class XmlValidatingReaderImpl : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003ADA0 File Offset: 0x00038FA0
		internal XmlValidatingReaderImpl(XmlReader reader)
		{
			XmlAsyncCheckReader xmlAsyncCheckReader = reader as XmlAsyncCheckReader;
			if (xmlAsyncCheckReader != null)
			{
				reader = xmlAsyncCheckReader.CoreReader;
			}
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
			this.eventHandling = new XmlValidatingReaderImpl.ValidationEventHandling(this);
			this.coreReaderImpl.ValidationEventHandling = this.eventHandling;
			this.coreReaderImpl.OnDefaultAttributeUse = new XmlTextReaderImpl.OnDefaultAttributeUseDelegate(this.ValidateDefaultAttributeOnUse);
			this.validationType = ValidationType.Auto;
			this.SetupValidation(ValidationType.Auto);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003AEB8 File Offset: 0x000390B8
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

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003AF1C File Offset: 0x0003911C
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

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003AF80 File Offset: 0x00039180
		internal XmlValidatingReaderImpl(XmlReader reader, ValidationEventHandler settingsEventHandler, bool processIdentityConstraints)
		{
			XmlAsyncCheckReader xmlAsyncCheckReader = reader as XmlAsyncCheckReader;
			if (xmlAsyncCheckReader != null)
			{
				reader = xmlAsyncCheckReader.CoreReader;
			}
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
			this.eventHandling = new XmlValidatingReaderImpl.ValidationEventHandling(this);
			if (settingsEventHandler != null)
			{
				this.eventHandling.AddHandler(settingsEventHandler);
			}
			this.coreReaderImpl.ValidationEventHandling = this.eventHandling;
			this.coreReaderImpl.OnDefaultAttributeUse = new XmlTextReaderImpl.OnDefaultAttributeUseDelegate(this.ValidateDefaultAttributeOnUse);
			this.validationType = ValidationType.DTD;
			this.SetupValidation(ValidationType.DTD);
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0003B098 File Offset: 0x00039298
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0003B0FA File Offset: 0x000392FA
		public override XmlNodeType NodeType
		{
			get
			{
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0003B107 File Offset: 0x00039307
		public override string Name
		{
			get
			{
				return this.coreReader.Name;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0003B114 File Offset: 0x00039314
		public override string LocalName
		{
			get
			{
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003B121 File Offset: 0x00039321
		public override string NamespaceURI
		{
			get
			{
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0003B12E File Offset: 0x0003932E
		public override string Prefix
		{
			get
			{
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0003B13B File Offset: 0x0003933B
		public override bool HasValue
		{
			get
			{
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0003B148 File Offset: 0x00039348
		public override string Value
		{
			get
			{
				return this.coreReader.Value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003B155 File Offset: 0x00039355
		public override int Depth
		{
			get
			{
				return this.coreReader.Depth;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0003B162 File Offset: 0x00039362
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0003B16F File Offset: 0x0003936F
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0003B17C File Offset: 0x0003937C
		public override bool IsDefault
		{
			get
			{
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0003B189 File Offset: 0x00039389
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0003B196 File Offset: 0x00039396
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003B1A3 File Offset: 0x000393A3
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0003B1B0 File Offset: 0x000393B0
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003B1C8 File Offset: 0x000393C8
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0003B1D5 File Offset: 0x000393D5
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003B1E2 File Offset: 0x000393E2
		internal Encoding Encoding
		{
			get
			{
				return this.coreReaderImpl.Encoding;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0003B1EF File Offset: 0x000393EF
		public override int AttributeCount
		{
			get
			{
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003B1FC File Offset: 0x000393FC
		public override string GetAttribute(string name)
		{
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003B20A File Offset: 0x0003940A
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.coreReader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003B219 File Offset: 0x00039419
		public override string GetAttribute(int i)
		{
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003B227 File Offset: 0x00039427
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003B241 File Offset: 0x00039441
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (!this.coreReader.MoveToAttribute(localName, namespaceURI))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003B25C File Offset: 0x0003945C
		public override void MoveToAttribute(int i)
		{
			this.coreReader.MoveToAttribute(i);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003B271 File Offset: 0x00039471
		public override bool MoveToFirstAttribute()
		{
			if (!this.coreReader.MoveToFirstAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003B28A File Offset: 0x0003948A
		public override bool MoveToNextAttribute()
		{
			if (!this.coreReader.MoveToNextAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003B2A3 File Offset: 0x000394A3
		public override bool MoveToElement()
		{
			if (!this.coreReader.MoveToElement())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003B2BC File Offset: 0x000394BC
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

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003B368 File Offset: 0x00039568
		public override void Close()
		{
			this.coreReader.Close();
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003B37C File Offset: 0x0003957C
		public override string LookupNamespace(string prefix)
		{
			return this.coreReaderImpl.LookupNamespace(prefix);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003B38A File Offset: 0x0003958A
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

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0003B3BE File Offset: 0x000395BE
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003B3C4 File Offset: 0x000395C4
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

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003B41C File Offset: 0x0003961C
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

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003B474 File Offset: 0x00039674
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

		// Token: 0x06000DED RID: 3565 RVA: 0x0003B4CC File Offset: 0x000396CC
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0003B522 File Offset: 0x00039722
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003B525 File Offset: 0x00039725
		public override void ResolveEntity()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			}
			this.coreReader.ResolveEntity();
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003B542 File Offset: 0x00039742
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0003B54A File Offset: 0x0003974A
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

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003B553 File Offset: 0x00039753
		internal void MoveOffEntityReference()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally && !this.outerReader.Read())
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003B589 File Offset: 0x00039789
		public override string ReadString()
		{
			this.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003B597 File Offset: 0x00039797
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0003B59A File Offset: 0x0003979A
		public int LineNumber
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LineNumber;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0003B5AC File Offset: 0x000397AC
		public int LinePosition
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LinePosition;
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003B5BE File Offset: 0x000397BE
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003B5C7 File Offset: 0x000397C7
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003B5D0 File Offset: 0x000397D0
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.LookupPrefix(namespaceName);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003B5D9 File Offset: 0x000397D9
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.coreReaderNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003B5E7 File Offset: 0x000397E7
		internal string LookupPrefix(string namespaceName)
		{
			return this.coreReaderNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000DFC RID: 3580 RVA: 0x0003B5F5 File Offset: 0x000397F5
		// (remove) Token: 0x06000DFD RID: 3581 RVA: 0x0003B603 File Offset: 0x00039803
		internal event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandling.AddHandler(value);
			}
			remove
			{
				this.eventHandling.RemoveHandler(value);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0003B614 File Offset: 0x00039814
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0003B668 File Offset: 0x00039868
		internal XmlReader Reader
		{
			get
			{
				return this.coreReader;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0003B670 File Offset: 0x00039870
		internal XmlTextReaderImpl ReaderImpl
		{
			get
			{
				return this.coreReaderImpl;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0003B678 File Offset: 0x00039878
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x0003B680 File Offset: 0x00039880
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

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0003B6A8 File Offset: 0x000398A8
		internal XmlSchemaCollection Schemas
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0003B6B0 File Offset: 0x000398B0
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x0003B6BD File Offset: 0x000398BD
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

		// Token: 0x1700029A RID: 666
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0003B6CB File Offset: 0x000398CB
		internal XmlResolver XmlResolver
		{
			set
			{
				this.coreReaderImpl.XmlResolver = value;
				this.validator.XmlResolver = value;
				this.schemaCollection.XmlResolver = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003B6F1 File Offset: 0x000398F1
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x0003B6FE File Offset: 0x000398FE
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

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003B70C File Offset: 0x0003990C
		public object ReadTypedValue()
		{
			if (this.validationType == ValidationType.None)
			{
				return null;
			}
			XmlNodeType nodeType = this.outerReader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.coreReaderImpl.InternalTypedValue;
				}
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
			else
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
								goto IL_F5;
							}
						}
						throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
					}
					IL_F5:
					return this.coreReaderImpl.InternalTypedValue;
				}
				return null;
			}
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003B834 File Offset: 0x00039A34
		private void ParseDtdFromParserContext()
		{
			if (this.parserContext.DocTypeName == null || this.parserContext.DocTypeName.Length == 0)
			{
				return;
			}
			IDtdParser dtdParser = DtdParser.Create();
			XmlTextReaderImpl.DtdParserProxy adapter = new XmlTextReaderImpl.DtdParserProxy(this.coreReaderImpl);
			IDtdInfo dtdInfo = dtdParser.ParseFreeFloatingDtd(this.parserContext.BaseURI, this.parserContext.DocTypeName, this.parserContext.PublicId, this.parserContext.SystemId, this.parserContext.InternalSubset, adapter);
			this.coreReaderImpl.SetDtdInfo(dtdInfo);
			this.ValidateDtd();
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003B8C4 File Offset: 0x00039AC4
		private void ValidateDtd()
		{
			IDtdInfo dtdInfo = this.coreReaderImpl.DtdInfo;
			if (dtdInfo != null)
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
				this.validator.DtdInfo = dtdInfo;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003B910 File Offset: 0x00039B10
		private void ResolveEntityInternally()
		{
			int depth = this.coreReader.Depth;
			this.outerReader.ResolveEntity();
			while (this.outerReader.Read() && this.coreReader.Depth > depth)
			{
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003B950 File Offset: 0x00039B50
		private void SetupValidation(ValidationType valType)
		{
			this.validator = BaseValidator.CreateInstance(valType, this, this.schemaCollection, this.eventHandling, this.processIdentityConstraints);
			XmlResolver resolver = this.GetResolver();
			this.validator.XmlResolver = resolver;
			if (this.outerReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = ((resolver == null) ? new Uri(this.outerReader.BaseURI, UriKind.RelativeOrAbsolute) : resolver.ResolveUri(null, this.outerReader.BaseURI));
			}
			this.coreReaderImpl.ValidationEventHandling = ((this.validationType == ValidationType.None) ? null : this.eventHandling);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003B9F4 File Offset: 0x00039BF4
		private XmlResolver GetResolver()
		{
			XmlResolver resolver = this.coreReaderImpl.GetResolver();
			if (resolver == null && !this.coreReaderImpl.IsResolverSet && !XmlReaderSettings.EnableLegacyXmlSettings())
			{
				if (XmlValidatingReaderImpl.s_tempResolver == null)
				{
					XmlValidatingReaderImpl.s_tempResolver = new XmlUrlResolver();
				}
				return XmlValidatingReaderImpl.s_tempResolver;
			}
			return resolver;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003BA3C File Offset: 0x00039C3C
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

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003BACD File Offset: 0x00039CCD
		internal void Close(bool closeStream)
		{
			this.coreReaderImpl.Close(closeStream);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0003BAE2 File Offset: 0x00039CE2
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0003BAEA File Offset: 0x00039CEA
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0003BAF3 File Offset: 0x00039CF3
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.coreReaderImpl.NamespaceManager;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0003BB00 File Offset: 0x00039D00
		internal bool StandAlone
		{
			get
			{
				return this.coreReaderImpl.StandAlone;
			}
		}

		// Token: 0x1700029F RID: 671
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0003BB0D File Offset: 0x00039D0D
		internal object SchemaTypeObject
		{
			set
			{
				this.coreReaderImpl.InternalSchemaType = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003BB1B File Offset: 0x00039D1B
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0003BB28 File Offset: 0x00039D28
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

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0003BB36 File Offset: 0x00039D36
		internal bool Normalization
		{
			get
			{
				return this.coreReaderImpl.Normalization;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003BB43 File Offset: 0x00039D43
		internal bool AddDefaultAttribute(SchemaAttDef attdef)
		{
			return this.coreReaderImpl.AddDefaultAttributeNonDtd(attdef);
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0003BB51 File Offset: 0x00039D51
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.coreReaderImpl.DtdInfo;
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003BB60 File Offset: 0x00039D60
		internal void ValidateDefaultAttributeOnUse(IDtdDefaultAttributeInfo defaultAttribute, XmlTextReaderImpl coreReader)
		{
			SchemaAttDef schemaAttDef = defaultAttribute as SchemaAttDef;
			if (schemaAttDef == null)
			{
				return;
			}
			if (!schemaAttDef.DefaultValueChecked)
			{
				SchemaInfo schemaInfo = coreReader.DtdInfo as SchemaInfo;
				if (schemaInfo == null)
				{
					return;
				}
				DtdValidator.CheckDefaultValue(schemaAttDef, schemaInfo, this.eventHandling, coreReader.BaseURI);
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003BBA3 File Offset: 0x00039DA3
		public override Task<string> GetValueAsync()
		{
			return this.coreReader.GetValueAsync();
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003BBB0 File Offset: 0x00039DB0
		public override Task<bool> ReadAsync()
		{
			XmlValidatingReaderImpl.<ReadAsync>d__145 <ReadAsync>d__;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadAsync>d__145>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003BBF4 File Offset: 0x00039DF4
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlValidatingReaderImpl.<ReadContentAsBase64Async>d__146 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadContentAsBase64Async>d__146>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003BC50 File Offset: 0x00039E50
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlValidatingReaderImpl.<ReadContentAsBinHexAsync>d__147 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadContentAsBinHexAsync>d__147>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003BCAC File Offset: 0x00039EAC
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlValidatingReaderImpl.<ReadElementContentAsBase64Async>d__148 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadElementContentAsBase64Async>d__148>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003BD08 File Offset: 0x00039F08
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlValidatingReaderImpl.<ReadElementContentAsBinHexAsync>d__149 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadElementContentAsBinHexAsync>d__149>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003BD64 File Offset: 0x00039F64
		internal Task MoveOffEntityReferenceAsync()
		{
			XmlValidatingReaderImpl.<MoveOffEntityReferenceAsync>d__150 <MoveOffEntityReferenceAsync>d__;
			<MoveOffEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MoveOffEntityReferenceAsync>d__.<>4__this = this;
			<MoveOffEntityReferenceAsync>d__.<>1__state = -1;
			<MoveOffEntityReferenceAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<MoveOffEntityReferenceAsync>d__150>(ref <MoveOffEntityReferenceAsync>d__);
			return <MoveOffEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003BDA8 File Offset: 0x00039FA8
		public Task<object> ReadTypedValueAsync()
		{
			XmlValidatingReaderImpl.<ReadTypedValueAsync>d__151 <ReadTypedValueAsync>d__;
			<ReadTypedValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadTypedValueAsync>d__.<>4__this = this;
			<ReadTypedValueAsync>d__.<>1__state = -1;
			<ReadTypedValueAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ReadTypedValueAsync>d__151>(ref <ReadTypedValueAsync>d__);
			return <ReadTypedValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003BDEC File Offset: 0x00039FEC
		private Task ParseDtdFromParserContextAsync()
		{
			XmlValidatingReaderImpl.<ParseDtdFromParserContextAsync>d__152 <ParseDtdFromParserContextAsync>d__;
			<ParseDtdFromParserContextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseDtdFromParserContextAsync>d__.<>4__this = this;
			<ParseDtdFromParserContextAsync>d__.<>1__state = -1;
			<ParseDtdFromParserContextAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ParseDtdFromParserContextAsync>d__152>(ref <ParseDtdFromParserContextAsync>d__);
			return <ParseDtdFromParserContextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003BE30 File Offset: 0x0003A030
		private Task ResolveEntityInternallyAsync()
		{
			XmlValidatingReaderImpl.<ResolveEntityInternallyAsync>d__153 <ResolveEntityInternallyAsync>d__;
			<ResolveEntityInternallyAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ResolveEntityInternallyAsync>d__.<>4__this = this;
			<ResolveEntityInternallyAsync>d__.<>1__state = -1;
			<ResolveEntityInternallyAsync>d__.<>t__builder.Start<XmlValidatingReaderImpl.<ResolveEntityInternallyAsync>d__153>(ref <ResolveEntityInternallyAsync>d__);
			return <ResolveEntityInternallyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400040E RID: 1038
		private XmlReader coreReader;

		// Token: 0x0400040F RID: 1039
		private XmlTextReaderImpl coreReaderImpl;

		// Token: 0x04000410 RID: 1040
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x04000411 RID: 1041
		private ValidationType validationType;

		// Token: 0x04000412 RID: 1042
		private BaseValidator validator;

		// Token: 0x04000413 RID: 1043
		private XmlSchemaCollection schemaCollection;

		// Token: 0x04000414 RID: 1044
		private bool processIdentityConstraints;

		// Token: 0x04000415 RID: 1045
		private XmlValidatingReaderImpl.ParsingFunction parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Init;

		// Token: 0x04000416 RID: 1046
		private XmlValidatingReaderImpl.ValidationEventHandling eventHandling;

		// Token: 0x04000417 RID: 1047
		private XmlParserContext parserContext;

		// Token: 0x04000418 RID: 1048
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04000419 RID: 1049
		private XmlReader outerReader;

		// Token: 0x0400041A RID: 1050
		private static XmlResolver s_tempResolver;

		// Token: 0x020003E8 RID: 1000
		private enum ParsingFunction
		{
			// Token: 0x04001A22 RID: 6690
			Read,
			// Token: 0x04001A23 RID: 6691
			Init,
			// Token: 0x04001A24 RID: 6692
			ParseDtdFromContext,
			// Token: 0x04001A25 RID: 6693
			ResolveEntityInternally,
			// Token: 0x04001A26 RID: 6694
			InReadBinaryContent,
			// Token: 0x04001A27 RID: 6695
			ReaderClosed,
			// Token: 0x04001A28 RID: 6696
			Error,
			// Token: 0x04001A29 RID: 6697
			None
		}

		// Token: 0x020003E9 RID: 1001
		internal class ValidationEventHandling : IValidationEventHandling
		{
			// Token: 0x06002F9C RID: 12188 RVA: 0x0010A0EE File Offset: 0x001082EE
			internal ValidationEventHandling(XmlValidatingReaderImpl reader)
			{
				this.reader = reader;
			}

			// Token: 0x17000A3E RID: 2622
			// (get) Token: 0x06002F9D RID: 12189 RVA: 0x0010A0FD File Offset: 0x001082FD
			object IValidationEventHandling.EventHandler
			{
				get
				{
					return this.eventHandler;
				}
			}

			// Token: 0x06002F9E RID: 12190 RVA: 0x0010A105 File Offset: 0x00108305
			void IValidationEventHandling.SendEvent(Exception exception, XmlSeverityType severity)
			{
				if (this.eventHandler != null)
				{
					this.eventHandler(this.reader, new ValidationEventArgs((XmlSchemaException)exception, severity));
					return;
				}
				if (this.reader.ValidationType != ValidationType.None && severity == XmlSeverityType.Error)
				{
					throw exception;
				}
			}

			// Token: 0x06002F9F RID: 12191 RVA: 0x0010A13F File Offset: 0x0010833F
			internal void AddHandler(ValidationEventHandler handler)
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, handler);
			}

			// Token: 0x06002FA0 RID: 12192 RVA: 0x0010A158 File Offset: 0x00108358
			internal void RemoveHandler(ValidationEventHandler handler)
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, handler);
			}

			// Token: 0x04001A2A RID: 6698
			private XmlValidatingReaderImpl reader;

			// Token: 0x04001A2B RID: 6699
			private ValidationEventHandler eventHandler;
		}
	}
}
