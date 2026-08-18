using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XmlConfiguration;

namespace System.Xml
{
	// Token: 0x020000DA RID: 218
	internal class XmlTextReaderImpl : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x00025D00 File Offset: 0x00023F00
		internal XmlTextReaderImpl()
		{
			this.curNode = new XmlTextReaderImpl.NodeData();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.NoData;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00025D88 File Offset: 0x00023F88
		internal XmlTextReaderImpl(XmlNameTable nt)
		{
			this.v1Compat = true;
			this.outerReader = this;
			this.nameTable = nt;
			nt.Add(string.Empty);
			if (!XmlReaderSettings.EnableLegacyXmlSettings())
			{
				this.xmlResolver = null;
			}
			else
			{
				this.xmlResolver = new XmlUrlResolver();
			}
			this.Xml = nt.Add("xml");
			this.XmlNs = nt.Add("xmlns");
			this.nodes = new XmlTextReaderImpl.NodeData[8];
			this.nodes[0] = new XmlTextReaderImpl.NodeData();
			this.curNode = this.nodes[0];
			this.stringBuilder = new StringBuilder();
			this.xmlContext = new XmlTextReaderImpl.XmlContext();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl;
			this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			this.entityHandling = EntityHandling.ExpandCharEntities;
			this.whitespaceHandling = WhitespaceHandling.All;
			this.closeInput = true;
			this.maxCharactersInDocument = 0L;
			this.maxCharactersFromEntities = 10000000L;
			this.charactersInDocument = 0L;
			this.charactersFromEntities = 0L;
			this.ps.lineNo = 1;
			this.ps.lineStartPos = -1;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00025EF8 File Offset: 0x000240F8
		private XmlTextReaderImpl(XmlResolver resolver, XmlReaderSettings settings, XmlParserContext context)
		{
			this.useAsync = settings.Async;
			this.v1Compat = false;
			this.outerReader = this;
			this.xmlContext = new XmlTextReaderImpl.XmlContext();
			XmlNameTable xmlNameTable = settings.NameTable;
			if (context == null)
			{
				if (xmlNameTable == null)
				{
					xmlNameTable = new NameTable();
				}
				else
				{
					this.nameTableFromSettings = true;
				}
				this.nameTable = xmlNameTable;
				this.namespaceManager = new XmlNamespaceManager(xmlNameTable);
			}
			else
			{
				this.SetupFromParserContext(context, settings);
				xmlNameTable = this.nameTable;
			}
			xmlNameTable.Add(string.Empty);
			this.Xml = xmlNameTable.Add("xml");
			this.XmlNs = xmlNameTable.Add("xmlns");
			this.xmlResolver = resolver;
			this.nodes = new XmlTextReaderImpl.NodeData[8];
			this.nodes[0] = new XmlTextReaderImpl.NodeData();
			this.curNode = this.nodes[0];
			this.stringBuilder = new StringBuilder();
			this.entityHandling = EntityHandling.ExpandEntities;
			this.xmlResolverIsSet = settings.IsXmlResolverSet;
			this.whitespaceHandling = (settings.IgnoreWhitespace ? WhitespaceHandling.Significant : WhitespaceHandling.All);
			this.normalize = true;
			this.ignorePIs = settings.IgnoreProcessingInstructions;
			this.ignoreComments = settings.IgnoreComments;
			this.checkCharacters = settings.CheckCharacters;
			this.lineNumberOffset = settings.LineNumberOffset;
			this.linePositionOffset = settings.LinePositionOffset;
			this.ps.lineNo = this.lineNumberOffset + 1;
			this.ps.lineStartPos = -this.linePositionOffset - 1;
			this.curNode.SetLineInfo(this.ps.LineNo - 1, this.ps.LinePos - 1);
			this.dtdProcessing = settings.DtdProcessing;
			this.maxCharactersInDocument = settings.MaxCharactersInDocument;
			this.maxCharactersFromEntities = settings.MaxCharactersFromEntities;
			this.charactersInDocument = 0L;
			this.charactersFromEntities = 0L;
			this.fragmentParserContext = context;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl;
			this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			switch (settings.ConformanceLevel)
			{
			case ConformanceLevel.Auto:
				this.fragmentType = XmlNodeType.None;
				this.fragment = true;
				return;
			case ConformanceLevel.Fragment:
				this.fragmentType = XmlNodeType.Element;
				this.fragment = true;
				return;
			}
			this.fragmentType = XmlNodeType.Document;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00026175 File Offset: 0x00024375
		internal XmlTextReaderImpl(Stream input) : this(string.Empty, input, new NameTable())
		{
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00026188 File Offset: 0x00024388
		internal XmlTextReaderImpl(Stream input, XmlNameTable nt) : this(string.Empty, input, nt)
		{
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00026197 File Offset: 0x00024397
		internal XmlTextReaderImpl(string url, Stream input) : this(url, input, new NameTable())
		{
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000261A8 File Offset: 0x000243A8
		internal XmlTextReaderImpl(string url, Stream input, XmlNameTable nt) : this(nt)
		{
			this.namespaceManager = new XmlNamespaceManager(nt);
			if (url == null || url.Length == 0)
			{
				this.InitStreamInput(input, null);
			}
			else
			{
				this.InitStreamInput(url, input, null);
			}
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00026208 File Offset: 0x00024408
		internal XmlTextReaderImpl(TextReader input) : this(string.Empty, input, new NameTable())
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002621B File Offset: 0x0002441B
		internal XmlTextReaderImpl(TextReader input, XmlNameTable nt) : this(string.Empty, input, nt)
		{
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002622A File Offset: 0x0002442A
		internal XmlTextReaderImpl(string url, TextReader input) : this(url, input, new NameTable())
		{
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002623C File Offset: 0x0002443C
		internal XmlTextReaderImpl(string url, TextReader input, XmlNameTable nt) : this(nt)
		{
			this.namespaceManager = new XmlNamespaceManager(nt);
			this.reportedBaseUri = ((url != null) ? url : string.Empty);
			this.InitTextReaderInput(this.reportedBaseUri, input);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002628C File Offset: 0x0002448C
		internal XmlTextReaderImpl(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context) : this((context != null && context.NameTable != null) ? context.NameTable : new NameTable())
		{
			Encoding encoding = (context != null) ? context.Encoding : null;
			if (context == null || context.BaseURI == null || context.BaseURI.Length == 0)
			{
				this.InitStreamInput(xmlFragment, encoding);
			}
			else
			{
				this.InitStreamInput(this.GetTempResolver().ResolveUri(null, context.BaseURI), xmlFragment, encoding);
			}
			this.InitFragmentReader(fragType, context, false);
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002632C File Offset: 0x0002452C
		internal XmlTextReaderImpl(string xmlFragment, XmlNodeType fragType, XmlParserContext context) : this((context == null || context.NameTable == null) ? new NameTable() : context.NameTable)
		{
			if (xmlFragment == null)
			{
				xmlFragment = string.Empty;
			}
			if (context == null)
			{
				this.InitStringInput(string.Empty, Encoding.Unicode, xmlFragment);
			}
			else
			{
				this.reportedBaseUri = context.BaseURI;
				this.InitStringInput(context.BaseURI, Encoding.Unicode, xmlFragment);
			}
			this.InitFragmentReader(fragType, context, false);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000263B0 File Offset: 0x000245B0
		internal XmlTextReaderImpl(string xmlFragment, XmlParserContext context) : this((context == null || context.NameTable == null) ? new NameTable() : context.NameTable)
		{
			this.InitStringInput((context == null) ? string.Empty : context.BaseURI, Encoding.Unicode, "<?xml " + xmlFragment + "?>");
			this.InitFragmentReader(XmlNodeType.XmlDeclaration, context, true);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00026410 File Offset: 0x00024610
		public XmlTextReaderImpl(string url) : this(url, new NameTable())
		{
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00026420 File Offset: 0x00024620
		public XmlTextReaderImpl(string url, XmlNameTable nt) : this(nt)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_EmptyUrl"), "url");
			}
			this.namespaceManager = new XmlNamespaceManager(nt);
			this.compressedStack = CompressedStack.Capture();
			this.url = url;
			this.ps.baseUri = this.GetTempResolver().ResolveUri(null, url);
			this.ps.baseUriStr = this.ps.baseUri.ToString();
			this.reportedBaseUri = this.ps.baseUriStr;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.OpenUrl;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000264C8 File Offset: 0x000246C8
		internal XmlTextReaderImpl(string uriStr, XmlReaderSettings settings, XmlParserContext context, XmlResolver uriResolver) : this(settings.GetXmlResolver(), settings, context)
		{
			Uri uri = uriResolver.ResolveUri(null, uriStr);
			string text = uri.ToString();
			if (context != null && context.BaseURI != null && context.BaseURI.Length > 0 && !this.UriEqual(uri, text, context.BaseURI, settings.GetXmlResolver()))
			{
				if (text.Length > 0)
				{
					this.Throw("Xml_DoubleBaseUri");
				}
				text = context.BaseURI;
			}
			this.reportedBaseUri = text;
			this.closeInput = true;
			this.laterInitParam = new XmlTextReaderImpl.LaterInitParam();
			this.laterInitParam.inputUriStr = uriStr;
			this.laterInitParam.inputbaseUri = uri;
			this.laterInitParam.inputContext = context;
			this.laterInitParam.inputUriResolver = uriResolver;
			this.laterInitParam.initType = XmlTextReaderImpl.InitInputType.UriString;
			if (!settings.Async)
			{
				this.FinishInitUriString();
				return;
			}
			this.laterInitParam.useAsync = true;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000265B0 File Offset: 0x000247B0
		private void FinishInitUriString()
		{
			Stream stream = null;
			if (this.laterInitParam.useAsync)
			{
				Task<object> entityAsync = this.laterInitParam.inputUriResolver.GetEntityAsync(this.laterInitParam.inputbaseUri, string.Empty, typeof(Stream));
				entityAsync.Wait();
				stream = (Stream)entityAsync.Result;
			}
			else
			{
				stream = (Stream)this.laterInitParam.inputUriResolver.GetEntity(this.laterInitParam.inputbaseUri, string.Empty, typeof(Stream));
			}
			if (stream == null)
			{
				throw new XmlException("Xml_CannotResolveUrl", this.laterInitParam.inputUriStr);
			}
			Encoding encoding = null;
			if (this.laterInitParam.inputContext != null)
			{
				encoding = this.laterInitParam.inputContext.Encoding;
			}
			try
			{
				this.InitStreamInput(this.laterInitParam.inputbaseUri, this.reportedBaseUri, stream, null, 0, encoding);
				this.reportedEncoding = this.ps.encoding;
				if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
				{
					this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
				}
			}
			catch
			{
				stream.Close();
				throw;
			}
			this.laterInitParam = null;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000266F4 File Offset: 0x000248F4
		internal XmlTextReaderImpl(Stream stream, byte[] bytes, int byteCount, XmlReaderSettings settings, Uri baseUri, string baseUriStr, XmlParserContext context, bool closeInput) : this(settings.GetXmlResolver(), settings, context)
		{
			if (context != null && context.BaseURI != null && context.BaseURI.Length > 0 && !this.UriEqual(baseUri, baseUriStr, context.BaseURI, settings.GetXmlResolver()))
			{
				if (baseUriStr.Length > 0)
				{
					this.Throw("Xml_DoubleBaseUri");
				}
				baseUriStr = context.BaseURI;
			}
			this.reportedBaseUri = baseUriStr;
			this.closeInput = closeInput;
			this.laterInitParam = new XmlTextReaderImpl.LaterInitParam();
			this.laterInitParam.inputStream = stream;
			this.laterInitParam.inputBytes = bytes;
			this.laterInitParam.inputByteCount = byteCount;
			this.laterInitParam.inputbaseUri = baseUri;
			this.laterInitParam.inputContext = context;
			this.laterInitParam.initType = XmlTextReaderImpl.InitInputType.Stream;
			if (!settings.Async)
			{
				this.FinishInitStream();
				return;
			}
			this.laterInitParam.useAsync = true;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x000267E8 File Offset: 0x000249E8
		private void FinishInitStream()
		{
			Encoding encoding = null;
			if (this.laterInitParam.inputContext != null)
			{
				encoding = this.laterInitParam.inputContext.Encoding;
			}
			this.InitStreamInput(this.laterInitParam.inputbaseUri, this.reportedBaseUri, this.laterInitParam.inputStream, this.laterInitParam.inputBytes, this.laterInitParam.inputByteCount, encoding);
			this.reportedEncoding = this.ps.encoding;
			if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
			{
				this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
			}
			this.laterInitParam = null;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00026898 File Offset: 0x00024A98
		internal XmlTextReaderImpl(TextReader input, XmlReaderSettings settings, string baseUriStr, XmlParserContext context) : this(settings.GetXmlResolver(), settings, context)
		{
			if (context != null && context.BaseURI != null)
			{
				baseUriStr = context.BaseURI;
			}
			this.reportedBaseUri = baseUriStr;
			this.closeInput = settings.CloseInput;
			this.laterInitParam = new XmlTextReaderImpl.LaterInitParam();
			this.laterInitParam.inputTextReader = input;
			this.laterInitParam.inputContext = context;
			this.laterInitParam.initType = XmlTextReaderImpl.InitInputType.TextReader;
			if (!settings.Async)
			{
				this.FinishInitTextReader();
				return;
			}
			this.laterInitParam.useAsync = true;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00026928 File Offset: 0x00024B28
		private void FinishInitTextReader()
		{
			this.InitTextReaderInput(this.reportedBaseUri, this.laterInitParam.inputTextReader);
			this.reportedEncoding = this.ps.encoding;
			if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
			{
				this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
			}
			this.laterInitParam = null;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00026994 File Offset: 0x00024B94
		internal XmlTextReaderImpl(string xmlFragment, XmlParserContext context, XmlReaderSettings settings) : this(null, settings, context)
		{
			this.InitStringInput(string.Empty, Encoding.Unicode, xmlFragment);
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000269D4 File Offset: 0x00024BD4
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				if (this.nameTableFromSettings)
				{
					xmlReaderSettings.NameTable = this.nameTable;
				}
				XmlNodeType xmlNodeType = this.fragmentType;
				if (xmlNodeType != XmlNodeType.None)
				{
					if (xmlNodeType == XmlNodeType.Element)
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
						goto IL_46;
					}
					if (xmlNodeType == XmlNodeType.Document)
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
						goto IL_46;
					}
				}
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
				IL_46:
				xmlReaderSettings.CheckCharacters = this.checkCharacters;
				xmlReaderSettings.LineNumberOffset = this.lineNumberOffset;
				xmlReaderSettings.LinePositionOffset = this.linePositionOffset;
				xmlReaderSettings.IgnoreWhitespace = (this.whitespaceHandling == WhitespaceHandling.Significant);
				xmlReaderSettings.IgnoreProcessingInstructions = this.ignorePIs;
				xmlReaderSettings.IgnoreComments = this.ignoreComments;
				xmlReaderSettings.DtdProcessing = this.dtdProcessing;
				xmlReaderSettings.MaxCharactersInDocument = this.maxCharactersInDocument;
				xmlReaderSettings.MaxCharactersFromEntities = this.maxCharactersFromEntities;
				if (!XmlReaderSettings.EnableLegacyXmlSettings())
				{
					xmlReaderSettings.XmlResolver = this.xmlResolver;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00026AB1 File Offset: 0x00024CB1
		public override XmlNodeType NodeType
		{
			get
			{
				return this.curNode.type;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00026ABE File Offset: 0x00024CBE
		public override string Name
		{
			get
			{
				return this.curNode.GetNameWPrefix(this.nameTable);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00026AD1 File Offset: 0x00024CD1
		public override string LocalName
		{
			get
			{
				return this.curNode.localName;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00026ADE File Offset: 0x00024CDE
		public override string NamespaceURI
		{
			get
			{
				return this.curNode.ns;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00026AEB File Offset: 0x00024CEB
		public override string Prefix
		{
			get
			{
				return this.curNode.prefix;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00026AF8 File Offset: 0x00024CF8
		public override string Value
		{
			get
			{
				if (this.parsingFunction >= XmlTextReaderImpl.ParsingFunction.PartialTextValue)
				{
					if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
					{
						this.FinishPartialValue();
						this.parsingFunction = this.nextParsingFunction;
					}
					else
					{
						this.FinishOtherValueIterator();
					}
				}
				return this.curNode.StringValue;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00026B33 File Offset: 0x00024D33
		public override int Depth
		{
			get
			{
				return this.curNode.depth;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00026B40 File Offset: 0x00024D40
		public override string BaseURI
		{
			get
			{
				return this.reportedBaseUri;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00026B48 File Offset: 0x00024D48
		public override bool IsEmptyElement
		{
			get
			{
				return this.curNode.IsEmptyElement;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00026B55 File Offset: 0x00024D55
		public override bool IsDefault
		{
			get
			{
				return this.curNode.IsDefaultAttribute;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00026B62 File Offset: 0x00024D62
		public override char QuoteChar
		{
			get
			{
				if (this.curNode.type != XmlNodeType.Attribute)
				{
					return '"';
				}
				return this.curNode.quoteChar;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00026B80 File Offset: 0x00024D80
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xmlContext.xmlSpace;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00026B8D File Offset: 0x00024D8D
		public override string XmlLang
		{
			get
			{
				return this.xmlContext.xmlLang;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00026B9A File Offset: 0x00024D9A
		public override ReadState ReadState
		{
			get
			{
				return this.readState;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00026BA2 File Offset: 0x00024DA2
		public override bool EOF
		{
			get
			{
				return this.parsingFunction == XmlTextReaderImpl.ParsingFunction.Eof;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00026BAE File Offset: 0x00024DAE
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00026BB6 File Offset: 0x00024DB6
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00026BB9 File Offset: 0x00024DB9
		public override int AttributeCount
		{
			get
			{
				return this.attrCount;
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00026BC4 File Offset: 0x00024DC4
		public override string GetAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetIndexOfAttributeWithoutPrefix(name);
			}
			else
			{
				num = this.GetIndexOfAttributeWithPrefix(name);
			}
			if (num < 0)
			{
				return null;
			}
			return this.nodes[num].StringValue;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00026C04 File Offset: 0x00024E04
		public override string GetAttribute(string localName, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.nameTable.Get(namespaceURI));
			localName = this.nameTable.Get(localName);
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, localName) && Ref.Equal(this.nodes[i].ns, namespaceURI))
				{
					return this.nodes[i].StringValue;
				}
			}
			return null;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00026C91 File Offset: 0x00024E91
		public override string GetAttribute(int i)
		{
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.nodes[this.index + i + 1].StringValue;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00026CC4 File Offset: 0x00024EC4
		public override bool MoveToAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetIndexOfAttributeWithoutPrefix(name);
			}
			else
			{
				num = this.GetIndexOfAttributeWithPrefix(name);
			}
			if (num >= 0)
			{
				if (this.InAttributeValueIterator)
				{
					this.FinishAttributeValueIterator();
				}
				this.curAttrIndex = num - this.index - 1;
				this.curNode = this.nodes[num];
				return true;
			}
			return false;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00026D24 File Offset: 0x00024F24
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.nameTable.Get(namespaceURI));
			localName = this.nameTable.Get(localName);
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, localName) && Ref.Equal(this.nodes[i].ns, namespaceURI))
				{
					this.curAttrIndex = i - this.index - 1;
					this.curNode = this.nodes[i];
					if (this.InAttributeValueIterator)
					{
						this.FinishAttributeValueIterator();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00026DD4 File Offset: 0x00024FD4
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			this.curAttrIndex = i;
			this.curNode = this.nodes[this.index + 1 + this.curAttrIndex];
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00026E2A File Offset: 0x0002502A
		public override bool MoveToFirstAttribute()
		{
			if (this.attrCount == 0)
			{
				return false;
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			this.curAttrIndex = 0;
			this.curNode = this.nodes[this.index + 1];
			return true;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00026E64 File Offset: 0x00025064
		public override bool MoveToNextAttribute()
		{
			if (this.curAttrIndex + 1 < this.attrCount)
			{
				if (this.InAttributeValueIterator)
				{
					this.FinishAttributeValueIterator();
				}
				XmlTextReaderImpl.NodeData[] array = this.nodes;
				int num = this.index + 1;
				int num2 = this.curAttrIndex + 1;
				this.curAttrIndex = num2;
				this.curNode = array[num + num2];
				return true;
			}
			return false;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00026EB9 File Offset: 0x000250B9
		public override bool MoveToElement()
		{
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			else if (this.curNode.type != XmlNodeType.Attribute)
			{
				return false;
			}
			this.curAttrIndex = -1;
			this.curNode = this.nodes[this.index];
			return true;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00026EF8 File Offset: 0x000250F8
		private void FinishInit()
		{
			switch (this.laterInitParam.initType)
			{
			case XmlTextReaderImpl.InitInputType.UriString:
				this.FinishInitUriString();
				return;
			case XmlTextReaderImpl.InitInputType.Stream:
				this.FinishInitStream();
				return;
			case XmlTextReaderImpl.InitInputType.TextReader:
				this.FinishInitTextReader();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00026F38 File Offset: 0x00025138
		public override bool Read()
		{
			if (this.laterInitParam != null)
			{
				this.FinishInit();
			}
			for (;;)
			{
				switch (this.parsingFunction)
				{
				case XmlTextReaderImpl.ParsingFunction.ElementContent:
					goto IL_85;
				case XmlTextReaderImpl.ParsingFunction.NoData:
					goto IL_2E7;
				case XmlTextReaderImpl.ParsingFunction.OpenUrl:
					this.OpenUrl();
					break;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractive:
					this.readState = ReadState.Interactive;
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl:
					break;
				case XmlTextReaderImpl.ParsingFunction.DocumentContent:
					goto IL_8C;
				case XmlTextReaderImpl.ParsingFunction.MoveToElementContent:
					this.ResetAttributes();
					this.index++;
					this.curNode = this.AddNode(this.index, this.index);
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ElementContent;
					continue;
				case XmlTextReaderImpl.ParsingFunction.PopElementContext:
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext:
					this.curNode = this.nodes[this.index];
					this.curNode.IsEmptyElement = false;
					this.ResetAttributes();
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel:
					this.ResetAttributes();
					this.curNode = this.nodes[this.index];
					this.parsingFunction = ((this.index == 0) ? XmlTextReaderImpl.ParsingFunction.DocumentContent : XmlTextReaderImpl.ParsingFunction.ElementContent);
					continue;
				case XmlTextReaderImpl.ParsingFunction.Error:
				case XmlTextReaderImpl.ParsingFunction.Eof:
				case XmlTextReaderImpl.ParsingFunction.ReaderClosed:
					return false;
				case XmlTextReaderImpl.ParsingFunction.EntityReference:
					goto IL_1B3;
				case XmlTextReaderImpl.ParsingFunction.InIncrementalRead:
					goto IL_2BE;
				case XmlTextReaderImpl.ParsingFunction.FragmentAttribute:
					goto IL_2C6;
				case XmlTextReaderImpl.ParsingFunction.ReportEndEntity:
					goto IL_1C7;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent:
					this.curNode = this.AddNode(this.index, this.index);
					this.reportedEncoding = this.ps.encoding;
					this.reportedBaseUri = this.ps.baseUriStr;
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent:
					goto IL_226;
				case XmlTextReaderImpl.ParsingFunction.XmlDeclarationFragment:
					goto IL_2CD;
				case XmlTextReaderImpl.ParsingFunction.GoToEof:
					goto IL_2DD;
				case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
					this.SkipPartialTextValue();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
					this.FinishAttributeValueIterator();
					this.curNode = this.nodes[this.index];
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
					this.FinishReadValueChunk();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
					this.FinishReadContentAsBinary();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
					this.FinishReadElementContentAsBinary();
					continue;
				default:
					continue;
				}
				this.readState = ReadState.Interactive;
				this.parsingFunction = this.nextParsingFunction;
				if (this.ParseXmlDeclaration(false))
				{
					goto Block_3;
				}
				this.reportedEncoding = this.ps.encoding;
			}
			IL_85:
			return this.ParseElementContent();
			IL_8C:
			return this.ParseDocumentContent();
			Block_3:
			this.reportedEncoding = this.ps.encoding;
			return true;
			IL_1B3:
			this.parsingFunction = this.nextParsingFunction;
			this.ParseEntityReference();
			return true;
			IL_1C7:
			this.SetupEndEntityNodeInContent();
			this.parsingFunction = this.nextParsingFunction;
			return true;
			IL_226:
			this.curNode = this.AddNode(this.index, this.index);
			this.curNode.SetValueNode(XmlNodeType.Text, string.Empty);
			this.curNode.SetLineInfo(this.ps.lineNo, this.ps.LinePos);
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			this.parsingFunction = this.nextParsingFunction;
			return true;
			IL_2BE:
			this.FinishIncrementalRead();
			return true;
			IL_2C6:
			return this.ParseFragmentAttribute();
			IL_2CD:
			this.ParseXmlDeclarationFragment();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.GoToEof;
			return true;
			IL_2DD:
			this.OnEof();
			return false;
			IL_2E7:
			this.ThrowWithoutLineInfo("Xml_MissingRoot");
			return false;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00027264 File Offset: 0x00025464
		public override void Close()
		{
			this.Close(this.closeInput);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00027274 File Offset: 0x00025474
		public override void Skip()
		{
			if (this.readState != ReadState.Interactive)
			{
				return;
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
				this.curNode = this.nodes[this.index];
			}
			else
			{
				XmlTextReaderImpl.ParsingFunction parsingFunction = this.parsingFunction;
				if (parsingFunction != XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
				{
					switch (parsingFunction)
					{
					case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
						this.SkipPartialTextValue();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
						this.FinishReadValueChunk();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
						this.FinishReadContentAsBinary();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
						this.FinishReadElementContentAsBinary();
						break;
					}
				}
				else
				{
					this.FinishIncrementalRead();
				}
			}
			XmlNodeType type = this.curNode.type;
			if (type != XmlNodeType.Element)
			{
				if (type != XmlNodeType.Attribute)
				{
					goto IL_DC;
				}
				this.outerReader.MoveToElement();
			}
			if (!this.curNode.IsEmptyElement)
			{
				int num = this.index;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipContent;
				while (this.outerReader.Read() && this.index > num)
				{
				}
				this.parsingMode = XmlTextReaderImpl.ParsingMode.Full;
			}
			IL_DC:
			this.outerReader.Read();
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00027369 File Offset: 0x00025569
		public override string LookupNamespace(string prefix)
		{
			if (!this.supportNamespaces)
			{
				return null;
			}
			return this.namespaceManager.LookupNamespace(prefix);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00027384 File Offset: 0x00025584
		public override bool ReadAttributeValue()
		{
			if (this.parsingFunction != XmlTextReaderImpl.ParsingFunction.InReadAttributeValue)
			{
				if (this.curNode.type != XmlNodeType.Attribute)
				{
					return false;
				}
				if (this.readState != ReadState.Interactive || this.curAttrIndex < 0)
				{
					return false;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
				{
					this.FinishReadValueChunk();
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					this.FinishReadContentAsBinary();
				}
				if (this.curNode.nextAttrValueChunk == null || this.entityHandling == EntityHandling.ExpandEntities)
				{
					XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, this.curNode.depth + 1);
					nodeData.SetValueNode(XmlNodeType.Text, this.curNode.StringValue);
					nodeData.lineInfo = this.curNode.lineInfo2;
					nodeData.depth = this.curNode.depth + 1;
					this.curNode = nodeData;
					nodeData.nextAttrValueChunk = null;
				}
				else
				{
					this.curNode = this.curNode.nextAttrValueChunk;
					this.AddNode(this.index + this.attrCount + 1, this.index + 2);
					this.nodes[this.index + this.attrCount + 1] = this.curNode;
					this.fullAttrCleanup = true;
				}
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
				this.attributeValueBaseEntityId = this.ps.entityId;
				return true;
			}
			else
			{
				if (this.ps.entityId != this.attributeValueBaseEntityId)
				{
					return this.ParseAttributeValueChunk();
				}
				if (this.curNode.nextAttrValueChunk != null)
				{
					this.curNode = this.curNode.nextAttrValueChunk;
					this.nodes[this.index + this.attrCount + 1] = this.curNode;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00027534 File Offset: 0x00025734
		public override void ResolveEntity()
		{
			if (this.curNode.type != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadAttributeValue || this.parsingFunction == XmlTextReaderImpl.ParsingFunction.FragmentAttribute)
			{
				switch (this.HandleGeneralEntityReference(this.curNode.localName, true, true, this.curNode.LinePos))
				{
				case XmlTextReaderImpl.EntityType.Expanded:
				case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
					if (this.ps.charsUsed - this.ps.charPos == 0)
					{
						this.emptyEntityInAttributeResolved = true;
						goto IL_164;
					}
					goto IL_164;
				case XmlTextReaderImpl.EntityType.FakeExpanded:
					this.emptyEntityInAttributeResolved = true;
					goto IL_164;
				}
				throw new XmlException("Xml_InternalError", string.Empty);
			}
			switch (this.HandleGeneralEntityReference(this.curNode.localName, false, true, this.curNode.LinePos))
			{
			case XmlTextReaderImpl.EntityType.Expanded:
			case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
				this.nextParsingFunction = this.parsingFunction;
				if (this.ps.charsUsed - this.ps.charPos == 0 && !this.ps.entity.IsExternal)
				{
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent;
					goto IL_164;
				}
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent;
				goto IL_164;
			case XmlTextReaderImpl.EntityType.FakeExpanded:
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent;
				goto IL_164;
			}
			throw new XmlException("Xml_InternalError", string.Empty);
			IL_164:
			this.ps.entityResolvedManually = true;
			this.index++;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x000276BF File Offset: 0x000258BF
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x000276C7 File Offset: 0x000258C7
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

		// Token: 0x06000B38 RID: 2872 RVA: 0x000276D0 File Offset: 0x000258D0
		internal void MoveOffEntityReference()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction == XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent && !this.outerReader.Read())
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00027707 File Offset: 0x00025907
		public override string ReadString()
		{
			this.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00027715 File Offset: 0x00025915
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00027718 File Offset: 0x00025918
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
				}
				if (!XmlReader.CanReadContentAs(this.curNode.type))
				{
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				}
				if (!this.InitReadContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBase64Decoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000277E4 File Offset: 0x000259E4
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				if (this.incReadDecoder == this.binHexDecoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
				}
				if (!XmlReader.CanReadContentAs(this.curNode.type))
				{
					throw base.CreateReadContentAsException("ReadContentAsBinHex");
				}
				if (!this.InitReadContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBinHexDecoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000278B0 File Offset: 0x00025AB0
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
				}
				if (this.curNode.type != XmlNodeType.Element)
				{
					throw base.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				if (!this.InitReadElementContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBase64Decoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00027978 File Offset: 0x00025B78
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
			{
				if (this.incReadDecoder == this.binHexDecoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
				}
				if (this.curNode.type != XmlNodeType.Element)
				{
					throw base.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				if (!this.InitReadElementContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBinHexDecoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00027A3E File Offset: 0x00025C3E
		public override bool CanReadValueChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00027A44 File Offset: 0x00025C44
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			if (!XmlReader.HasValueInternal(this.curNode.type))
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidReadValueChunk", new object[]
				{
					this.curNode.type
				}));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction != XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
				{
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue;
				}
				else
				{
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue;
					this.nextNextParsingFunction = this.nextParsingFunction;
					this.nextParsingFunction = this.parsingFunction;
				}
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadValueChunk;
				this.readValueOffset = 0;
			}
			if (count == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = this.curNode.CopyTo(this.readValueOffset, buffer, index + num, count - num);
			num += num2;
			this.readValueOffset += num2;
			if (num == count)
			{
				char ch = buffer[index + count - 1];
				if (XmlCharType.IsHighSurrogate((int)ch))
				{
					num--;
					this.readValueOffset--;
					if (num == 0)
					{
						this.Throw("Xml_NotEnoughSpaceForSurrogatePair");
					}
				}
				return num;
			}
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
			{
				this.curNode.SetValue(string.Empty);
				bool flag = false;
				int num3 = 0;
				int num4 = 0;
				while (num < count && !flag)
				{
					int num5 = 0;
					flag = this.ParseText(out num3, out num4, ref num5);
					int num6 = count - num;
					if (num6 > num4 - num3)
					{
						num6 = num4 - num3;
					}
					XmlTextReaderImpl.BlockCopyChars(this.ps.chars, num3, buffer, index + num, num6);
					num += num6;
					num3 += num6;
				}
				this.incReadState = (flag ? XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue : XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue);
				if (num == count)
				{
					char ch2 = buffer[index + count - 1];
					if (XmlCharType.IsHighSurrogate((int)ch2))
					{
						num--;
						num3--;
						if (num == 0)
						{
							this.Throw("Xml_NotEnoughSpaceForSurrogatePair");
						}
					}
				}
				this.readValueOffset = 0;
				this.curNode.SetValue(this.ps.chars, num3, num4 - num3);
			}
			return num;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00027C64 File Offset: 0x00025E64
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00027C67 File Offset: 0x00025E67
		public int LineNumber
		{
			get
			{
				return this.curNode.LineNo;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00027C74 File Offset: 0x00025E74
		public int LinePosition
		{
			get
			{
				return this.curNode.LinePos;
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00027C81 File Offset: 0x00025E81
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00027C8A File Offset: 0x00025E8A
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00027C93 File Offset: 0x00025E93
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.LookupPrefix(namespaceName);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00027C9C File Offset: 0x00025E9C
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.namespaceManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00027CAA File Offset: 0x00025EAA
		internal string LookupPrefix(string namespaceName)
		{
			return this.namespaceManager.LookupPrefix(namespaceName);
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00027CB8 File Offset: 0x00025EB8
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x00027CC0 File Offset: 0x00025EC0
		internal bool Namespaces
		{
			get
			{
				return this.supportNamespaces;
			}
			set
			{
				if (this.readState != ReadState.Initial)
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
				}
				this.supportNamespaces = value;
				if (value)
				{
					if (this.namespaceManager is XmlTextReaderImpl.NoNamespaceManager)
					{
						if (this.fragment && this.fragmentParserContext != null && this.fragmentParserContext.NamespaceManager != null)
						{
							this.namespaceManager = this.fragmentParserContext.NamespaceManager;
						}
						else
						{
							this.namespaceManager = new XmlNamespaceManager(this.nameTable);
						}
					}
					this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
					return;
				}
				if (!(this.namespaceManager is XmlTextReaderImpl.NoNamespaceManager))
				{
					this.namespaceManager = new XmlTextReaderImpl.NoNamespaceManager();
				}
				this.xmlContext.defaultNamespace = string.Empty;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00027D81 File Offset: 0x00025F81
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x00027D8C File Offset: 0x00025F8C
		internal bool Normalization
		{
			get
			{
				return this.normalize;
			}
			set
			{
				if (this.readState == ReadState.Closed)
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
				}
				this.normalize = value;
				if (this.ps.entity == null || this.ps.entity.IsExternal)
				{
					this.ps.eolNormalized = !value;
				}
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00027DE7 File Offset: 0x00025FE7
		internal Encoding Encoding
		{
			get
			{
				if (this.readState != ReadState.Interactive)
				{
					return null;
				}
				return this.reportedEncoding;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00027DFA File Offset: 0x00025FFA
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00027E02 File Offset: 0x00026002
		internal WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.whitespaceHandling;
			}
			set
			{
				if (this.readState == ReadState.Closed)
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
				}
				if (value > WhitespaceHandling.None)
				{
					throw new XmlException("Xml_WhitespaceHandling", string.Empty);
				}
				this.whitespaceHandling = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00027E38 File Offset: 0x00026038
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00027E40 File Offset: 0x00026040
		internal DtdProcessing DtdProcessing
		{
			get
			{
				return this.dtdProcessing;
			}
			set
			{
				if (value > DtdProcessing.Parse)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.dtdProcessing = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00027E58 File Offset: 0x00026058
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00027E60 File Offset: 0x00026060
		internal EntityHandling EntityHandling
		{
			get
			{
				return this.entityHandling;
			}
			set
			{
				if (value != EntityHandling.ExpandEntities && value != EntityHandling.ExpandCharEntities)
				{
					throw new XmlException("Xml_EntityHandling", string.Empty);
				}
				this.entityHandling = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00027E81 File Offset: 0x00026081
		internal bool IsResolverSet
		{
			get
			{
				return this.xmlResolverIsSet;
			}
		}

		// Token: 0x1700022E RID: 558
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00027E8C File Offset: 0x0002608C
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
				this.xmlResolverIsSet = true;
				this.ps.baseUri = null;
				for (int i = 0; i <= this.parsingStatesStackTop; i++)
				{
					this.parsingStatesStack[i].baseUri = null;
				}
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00027ED8 File Offset: 0x000260D8
		internal void ResetState()
		{
			if (this.fragment)
			{
				this.Throw(new InvalidOperationException(Res.GetString("Xml_InvalidResetStateCall")));
			}
			if (this.readState == ReadState.Initial)
			{
				return;
			}
			this.ResetAttributes();
			while (this.namespaceManager.PopScope())
			{
			}
			while (this.InEntity)
			{
				this.HandleEntityEnd(true);
			}
			this.readState = ReadState.Initial;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl;
			this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			this.curNode = this.nodes[0];
			this.curNode.Clear(XmlNodeType.None);
			this.curNode.SetLineInfo(0, 0);
			this.index = 0;
			this.rootElementParsed = false;
			this.charactersInDocument = 0L;
			this.charactersFromEntities = 0L;
			this.afterResetState = true;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00027F94 File Offset: 0x00026194
		internal TextReader GetRemainder()
		{
			XmlTextReaderImpl.ParsingFunction parsingFunction = this.parsingFunction;
			if (parsingFunction != XmlTextReaderImpl.ParsingFunction.OpenUrl)
			{
				if (parsingFunction - XmlTextReaderImpl.ParsingFunction.Eof <= 1)
				{
					return new StringReader(string.Empty);
				}
				if (parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
				{
					if (!this.InEntity)
					{
						this.stringBuilder.Append(this.ps.chars, this.incReadLeftStartPos, this.incReadLeftEndPos - this.incReadLeftStartPos);
					}
				}
			}
			else
			{
				this.OpenUrl();
			}
			while (this.InEntity)
			{
				this.HandleEntityEnd(true);
			}
			this.ps.appendMode = false;
			do
			{
				this.stringBuilder.Append(this.ps.chars, this.ps.charPos, this.ps.charsUsed - this.ps.charPos);
				this.ps.charPos = this.ps.charsUsed;
			}
			while (this.ReadData() != 0);
			this.OnEof();
			string s = this.stringBuilder.ToString();
			this.stringBuilder.Length = 0;
			return new StringReader(s);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00028098 File Offset: 0x00026298
		internal int ReadChars(char[] buffer, int index, int count)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				if (this.incReadDecoder != this.readCharsDecoder)
				{
					if (this.readCharsDecoder == null)
					{
						this.readCharsDecoder = new IncrementalReadCharsDecoder();
					}
					this.readCharsDecoder.Reset();
					this.incReadDecoder = this.readCharsDecoder;
				}
				return this.IncrementalRead(buffer, index, count);
			}
			if (this.curNode.type != XmlNodeType.Element)
			{
				return 0;
			}
			if (this.curNode.IsEmptyElement)
			{
				this.outerReader.Read();
				return 0;
			}
			if (this.readCharsDecoder == null)
			{
				this.readCharsDecoder = new IncrementalReadCharsDecoder();
			}
			this.InitIncrementalRead(this.readCharsDecoder);
			return this.IncrementalRead(buffer, index, count);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00028144 File Offset: 0x00026344
		internal int ReadBase64(byte[] array, int offset, int len)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				if (this.incReadDecoder != this.base64Decoder)
				{
					this.InitBase64Decoder();
				}
				return this.IncrementalRead(array, offset, len);
			}
			if (this.curNode.type != XmlNodeType.Element)
			{
				return 0;
			}
			if (this.curNode.IsEmptyElement)
			{
				this.outerReader.Read();
				return 0;
			}
			if (this.base64Decoder == null)
			{
				this.base64Decoder = new Base64Decoder();
			}
			this.InitIncrementalRead(this.base64Decoder);
			return this.IncrementalRead(array, offset, len);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000281CC File Offset: 0x000263CC
		internal int ReadBinHex(byte[] array, int offset, int len)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				if (this.incReadDecoder != this.binHexDecoder)
				{
					this.InitBinHexDecoder();
				}
				return this.IncrementalRead(array, offset, len);
			}
			if (this.curNode.type != XmlNodeType.Element)
			{
				return 0;
			}
			if (this.curNode.IsEmptyElement)
			{
				this.outerReader.Read();
				return 0;
			}
			if (this.binHexDecoder == null)
			{
				this.binHexDecoder = new BinHexDecoder();
			}
			this.InitIncrementalRead(this.binHexDecoder);
			return this.IncrementalRead(array, offset, len);
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00028254 File Offset: 0x00026454
		internal XmlNameTable DtdParserProxy_NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0002825C File Offset: 0x0002645C
		internal IXmlNamespaceResolver DtdParserProxy_NamespaceResolver
		{
			get
			{
				return this.namespaceManager;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00028264 File Offset: 0x00026464
		internal bool DtdParserProxy_DtdValidation
		{
			get
			{
				return this.DtdValidation;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002826C File Offset: 0x0002646C
		internal bool DtdParserProxy_Normalization
		{
			get
			{
				return this.normalize;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00028274 File Offset: 0x00026474
		internal bool DtdParserProxy_Namespaces
		{
			get
			{
				return this.supportNamespaces;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0002827C File Offset: 0x0002647C
		internal bool DtdParserProxy_V1CompatibilityMode
		{
			get
			{
				return this.v1Compat;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00028284 File Offset: 0x00026484
		internal Uri DtdParserProxy_BaseUri
		{
			get
			{
				if (this.ps.baseUriStr.Length > 0 && this.ps.baseUri == null && this.xmlResolver != null)
				{
					this.ps.baseUri = this.xmlResolver.ResolveUri(null, this.ps.baseUriStr);
				}
				return this.ps.baseUri;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000282EC File Offset: 0x000264EC
		internal bool DtdParserProxy_IsEof
		{
			get
			{
				return this.ps.isEof;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x000282F9 File Offset: 0x000264F9
		internal char[] DtdParserProxy_ParsingBuffer
		{
			get
			{
				return this.ps.chars;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00028306 File Offset: 0x00026506
		internal int DtdParserProxy_ParsingBufferLength
		{
			get
			{
				return this.ps.charsUsed;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00028313 File Offset: 0x00026513
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00028320 File Offset: 0x00026520
		internal int DtdParserProxy_CurrentPosition
		{
			get
			{
				return this.ps.charPos;
			}
			set
			{
				this.ps.charPos = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002832E File Offset: 0x0002652E
		internal int DtdParserProxy_EntityStackLength
		{
			get
			{
				return this.parsingStatesStackTop + 1;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00028338 File Offset: 0x00026538
		internal bool DtdParserProxy_IsEntityEolNormalized
		{
			get
			{
				return this.ps.eolNormalized;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00028345 File Offset: 0x00026545
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x0002834D File Offset: 0x0002654D
		internal IValidationEventHandling DtdParserProxy_ValidationEventHandling
		{
			get
			{
				return this.validationEventHandling;
			}
			set
			{
				this.validationEventHandling = value;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00028356 File Offset: 0x00026556
		internal void DtdParserProxy_OnNewLine(int pos)
		{
			this.OnNewLine(pos);
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002835F File Offset: 0x0002655F
		internal int DtdParserProxy_LineNo
		{
			get
			{
				return this.ps.LineNo;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002836C File Offset: 0x0002656C
		internal int DtdParserProxy_LineStartPosition
		{
			get
			{
				return this.ps.lineStartPos;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00028379 File Offset: 0x00026579
		internal int DtdParserProxy_ReadData()
		{
			return this.ReadData();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00028384 File Offset: 0x00026584
		internal int DtdParserProxy_ParseNumericCharRef(StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.EntityType entityType;
			return this.ParseNumericCharRef(true, internalSubsetBuilder, out entityType);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002839B File Offset: 0x0002659B
		internal int DtdParserProxy_ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
		{
			return this.ParseNamedCharRef(expand, internalSubsetBuilder);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000283A8 File Offset: 0x000265A8
		internal void DtdParserProxy_ParsePI(StringBuilder sb)
		{
			if (sb == null)
			{
				XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
				this.ParsePI(null);
				this.parsingMode = parsingMode;
				return;
			}
			this.ParsePI(sb);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000283E0 File Offset: 0x000265E0
		internal void DtdParserProxy_ParseComment(StringBuilder sb)
		{
			try
			{
				if (sb == null)
				{
					XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
					this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
					this.ParseCDataOrComment(XmlNodeType.Comment);
					this.parsingMode = parsingMode;
				}
				else
				{
					XmlTextReaderImpl.NodeData nodeData = this.curNode;
					this.curNode = this.AddNode(this.index + this.attrCount + 1, this.index);
					this.ParseCDataOrComment(XmlNodeType.Comment);
					this.curNode.CopyTo(0, sb);
					this.curNode = nodeData;
				}
			}
			catch (XmlException ex)
			{
				if (!(ex.ResString == "Xml_UnexpectedEOF") || this.ps.entity == null)
				{
					throw;
				}
				this.SendValidationEvent(XmlSeverityType.Error, "Sch_ParEntityRefNesting", null, this.ps.LineNo, this.ps.LinePos);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x000284AC File Offset: 0x000266AC
		private bool IsResolverNull
		{
			get
			{
				return this.xmlResolver == null || (XmlReaderSection.ProhibitDefaultUrlResolver && !this.xmlResolverIsSet);
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000284CA File Offset: 0x000266CA
		private XmlResolver GetTempResolver()
		{
			if (this.xmlResolver != null)
			{
				return this.xmlResolver;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000284E0 File Offset: 0x000266E0
		internal bool DtdParserProxy_PushEntity(IDtdEntityInfo entity, out int entityId)
		{
			bool result;
			if (entity.IsExternal)
			{
				if (this.IsResolverNull)
				{
					entityId = -1;
					return false;
				}
				result = this.PushExternalEntity(entity);
			}
			else
			{
				this.PushInternalEntity(entity);
				result = true;
			}
			entityId = this.ps.entityId;
			return result;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00028523 File Offset: 0x00026723
		internal bool DtdParserProxy_PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId)
		{
			if (this.parsingStatesStackTop == -1)
			{
				oldEntity = null;
				newEntityId = -1;
				return false;
			}
			oldEntity = this.ps.entity;
			this.PopEntity();
			newEntityId = this.ps.entityId;
			return true;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00028558 File Offset: 0x00026758
		internal bool DtdParserProxy_PushExternalSubset(string systemId, string publicId)
		{
			if (this.IsResolverNull)
			{
				return false;
			}
			if (this.ps.baseUri == null && !string.IsNullOrEmpty(this.ps.baseUriStr))
			{
				this.ps.baseUri = this.xmlResolver.ResolveUri(null, this.ps.baseUriStr);
			}
			this.PushExternalEntityOrSubset(publicId, systemId, this.ps.baseUri, null);
			this.ps.entity = null;
			this.ps.entityId = 0;
			int charPos = this.ps.charPos;
			if (this.v1Compat)
			{
				this.EatWhitespaces(null);
			}
			if (!this.ParseXmlDeclaration(true))
			{
				this.ps.charPos = charPos;
			}
			return true;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00028614 File Offset: 0x00026814
		internal void DtdParserProxy_PushInternalDtd(string baseUri, string internalDtd)
		{
			this.PushParsingState();
			this.RegisterConsumedCharacters((long)internalDtd.Length, false);
			this.InitStringInput(baseUri, Encoding.Unicode, internalDtd);
			this.ps.entity = null;
			this.ps.entityId = 0;
			this.ps.eolNormalized = false;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00028666 File Offset: 0x00026866
		internal void DtdParserProxy_Throw(Exception e)
		{
			this.Throw(e);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00028670 File Offset: 0x00026870
		internal void DtdParserProxy_OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttributeNoChecks("SYSTEM", this.index + 1);
			nodeData.SetValue(systemId);
			nodeData.lineInfo = keywordLineInfo;
			nodeData.lineInfo2 = systemLiteralLineInfo;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000286A8 File Offset: 0x000268A8
		internal void DtdParserProxy_OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttributeNoChecks("PUBLIC", this.index + 1);
			nodeData.SetValue(publicId);
			nodeData.lineInfo = keywordLineInfo;
			nodeData.lineInfo2 = publicLiteralLineInfo;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000286DE File Offset: 0x000268DE
		private void Throw(int pos, string res, string arg)
		{
			this.ps.charPos = pos;
			this.Throw(res, arg);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000286F4 File Offset: 0x000268F4
		private void Throw(int pos, string res, string[] args)
		{
			this.ps.charPos = pos;
			this.Throw(res, args);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002870A File Offset: 0x0002690A
		private void Throw(int pos, string res)
		{
			this.ps.charPos = pos;
			this.Throw(res, string.Empty);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00028724 File Offset: 0x00026924
		private void Throw(string res)
		{
			this.Throw(res, string.Empty);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00028732 File Offset: 0x00026932
		private void Throw(string res, int lineNo, int linePos)
		{
			this.Throw(new XmlException(res, string.Empty, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00028752 File Offset: 0x00026952
		private void Throw(string res, string arg)
		{
			this.Throw(new XmlException(res, arg, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00028782 File Offset: 0x00026982
		private void Throw(string res, string arg, int lineNo, int linePos)
		{
			this.Throw(new XmlException(res, arg, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002879F File Offset: 0x0002699F
		private void Throw(string res, string[] args)
		{
			this.Throw(new XmlException(res, args, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000287CF File Offset: 0x000269CF
		private void Throw(string res, string arg, Exception innerException)
		{
			this.Throw(res, new string[]
			{
				arg
			}, innerException);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000287E3 File Offset: 0x000269E3
		private void Throw(string res, string[] args, Exception innerException)
		{
			this.Throw(new XmlException(res, args, innerException, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00028814 File Offset: 0x00026A14
		private void Throw(Exception e)
		{
			this.SetErrorState();
			XmlException ex = e as XmlException;
			if (ex != null)
			{
				this.curNode.SetLineInfo(ex.LineNumber, ex.LinePosition);
			}
			throw e;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00028849 File Offset: 0x00026A49
		private void ReThrow(Exception e, int lineNo, int linePos)
		{
			this.Throw(new XmlException(e.Message, null, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002886A File Offset: 0x00026A6A
		private void ThrowWithoutLineInfo(string res)
		{
			this.Throw(new XmlException(res, string.Empty, this.ps.baseUriStr));
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00028888 File Offset: 0x00026A88
		private void ThrowWithoutLineInfo(string res, string arg)
		{
			this.Throw(new XmlException(res, arg, this.ps.baseUriStr));
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000288A2 File Offset: 0x00026AA2
		private void ThrowWithoutLineInfo(string res, string[] args, Exception innerException)
		{
			this.Throw(new XmlException(res, args, innerException, 0, 0, this.ps.baseUriStr));
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000288BF File Offset: 0x00026ABF
		private void ThrowInvalidChar(char[] data, int length, int invCharPos)
		{
			this.Throw(invCharPos, "Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(data, length, invCharPos));
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000288D5 File Offset: 0x00026AD5
		private void SetErrorState()
		{
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.Error;
			this.readState = ReadState.Error;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000288E6 File Offset: 0x00026AE6
		private void SendValidationEvent(XmlSeverityType severity, string code, string arg, int lineNo, int linePos)
		{
			this.SendValidationEvent(severity, new XmlSchemaException(code, arg, this.ps.baseUriStr, lineNo, linePos));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00028905 File Offset: 0x00026B05
		private void SendValidationEvent(XmlSeverityType severity, XmlSchemaException exception)
		{
			if (this.validationEventHandling != null)
			{
				this.validationEventHandling.SendEvent(exception, severity);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002891C File Offset: 0x00026B1C
		private bool InAttributeValueIterator
		{
			get
			{
				return this.attrCount > 0 && this.parsingFunction >= XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00028938 File Offset: 0x00026B38
		private void FinishAttributeValueIterator()
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				this.FinishReadValueChunk();
			}
			else if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				this.FinishReadContentAsBinary();
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadAttributeValue)
			{
				while (this.ps.entityId != this.attributeValueBaseEntityId)
				{
					this.HandleEntityEnd(false);
				}
				this.emptyEntityInAttributeResolved = false;
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = ((this.index > 0) ? XmlTextReaderImpl.ParsingFunction.ElementContent : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x000289B4 File Offset: 0x00026BB4
		private bool DtdValidation
		{
			get
			{
				return this.validationEventHandling != null;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000289BF File Offset: 0x00026BBF
		private void InitStreamInput(Stream stream, Encoding encoding)
		{
			this.InitStreamInput(null, string.Empty, stream, null, 0, encoding);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000289D1 File Offset: 0x00026BD1
		private void InitStreamInput(string baseUriStr, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(null, baseUriStr, stream, null, 0, encoding);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000289DF File Offset: 0x00026BDF
		private void InitStreamInput(Uri baseUri, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(baseUri, baseUri.ToString(), stream, null, 0, encoding);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000289F2 File Offset: 0x00026BF2
		private void InitStreamInput(Uri baseUri, string baseUriStr, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(baseUri, baseUriStr, stream, null, 0, encoding);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00028A04 File Offset: 0x00026C04
		private void InitStreamInput(Uri baseUri, string baseUriStr, Stream stream, byte[] bytes, int byteCount, Encoding encoding)
		{
			this.ps.stream = stream;
			this.ps.baseUri = baseUri;
			this.ps.baseUriStr = baseUriStr;
			int num;
			if (bytes != null)
			{
				this.ps.bytes = bytes;
				this.ps.bytesUsed = byteCount;
				num = this.ps.bytes.Length;
			}
			else
			{
				if (this.laterInitParam != null && this.laterInitParam.useAsync)
				{
					num = 65536;
				}
				else
				{
					num = XmlReader.CalcBufferSize(stream);
				}
				if (this.ps.bytes == null || this.ps.bytes.Length < num)
				{
					this.ps.bytes = new byte[num];
				}
			}
			if (this.ps.chars == null || this.ps.chars.Length < num + 1)
			{
				this.ps.chars = new char[num + 1];
			}
			this.ps.bytePos = 0;
			while (this.ps.bytesUsed < 4 && this.ps.bytes.Length - this.ps.bytesUsed > 0)
			{
				int num2 = stream.Read(this.ps.bytes, this.ps.bytesUsed, this.ps.bytes.Length - this.ps.bytesUsed);
				if (num2 == 0)
				{
					this.ps.isStreamEof = true;
					break;
				}
				this.ps.bytesUsed = this.ps.bytesUsed + num2;
			}
			if (encoding == null)
			{
				encoding = this.DetectEncoding();
			}
			this.SetupEncoding(encoding);
			byte[] preamble = this.ps.encoding.GetPreamble();
			int num3 = preamble.Length;
			int num4 = 0;
			while (num4 < num3 && num4 < this.ps.bytesUsed && this.ps.bytes[num4] == preamble[num4])
			{
				num4++;
			}
			if (num4 == num3)
			{
				this.ps.bytePos = num3;
			}
			this.documentStartBytePos = this.ps.bytePos;
			this.ps.eolNormalized = !this.normalize;
			this.ps.appendMode = true;
			this.ReadData();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00028C19 File Offset: 0x00026E19
		private void InitTextReaderInput(string baseUriStr, TextReader input)
		{
			this.InitTextReaderInput(baseUriStr, null, input);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00028C24 File Offset: 0x00026E24
		private void InitTextReaderInput(string baseUriStr, Uri baseUri, TextReader input)
		{
			this.ps.textReader = input;
			this.ps.baseUriStr = baseUriStr;
			this.ps.baseUri = baseUri;
			if (this.ps.chars == null)
			{
				if (this.laterInitParam != null && this.laterInitParam.useAsync)
				{
					this.ps.chars = new char[65537];
				}
				else
				{
					this.ps.chars = new char[4097];
				}
			}
			this.ps.encoding = Encoding.Unicode;
			this.ps.eolNormalized = !this.normalize;
			this.ps.appendMode = true;
			this.ReadData();
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00028CDC File Offset: 0x00026EDC
		private void InitStringInput(string baseUriStr, Encoding originalEncoding, string str)
		{
			this.ps.baseUriStr = baseUriStr;
			this.ps.baseUri = null;
			int length = str.Length;
			this.ps.chars = new char[length + 1];
			str.CopyTo(0, this.ps.chars, 0, str.Length);
			this.ps.charsUsed = length;
			this.ps.chars[length] = '\0';
			this.ps.encoding = originalEncoding;
			this.ps.eolNormalized = !this.normalize;
			this.ps.isEof = true;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00028D7C File Offset: 0x00026F7C
		private void InitFragmentReader(XmlNodeType fragmentType, XmlParserContext parserContext, bool allowXmlDeclFragment)
		{
			this.fragmentParserContext = parserContext;
			if (parserContext != null)
			{
				if (parserContext.NamespaceManager != null)
				{
					this.namespaceManager = parserContext.NamespaceManager;
					this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
				}
				else
				{
					this.namespaceManager = new XmlNamespaceManager(this.nameTable);
				}
				this.ps.baseUriStr = parserContext.BaseURI;
				this.ps.baseUri = null;
				this.xmlContext.xmlLang = parserContext.XmlLang;
				this.xmlContext.xmlSpace = parserContext.XmlSpace;
			}
			else
			{
				this.namespaceManager = new XmlNamespaceManager(this.nameTable);
				this.ps.baseUriStr = string.Empty;
				this.ps.baseUri = null;
			}
			this.reportedBaseUri = this.ps.baseUriStr;
			if (fragmentType <= XmlNodeType.Attribute)
			{
				if (fragmentType == XmlNodeType.Element)
				{
					this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
					goto IL_147;
				}
				if (fragmentType == XmlNodeType.Attribute)
				{
					this.ps.appendMode = false;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractive;
					this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.FragmentAttribute;
					goto IL_147;
				}
			}
			else
			{
				if (fragmentType == XmlNodeType.Document)
				{
					goto IL_147;
				}
				if (fragmentType == XmlNodeType.XmlDeclaration)
				{
					if (allowXmlDeclFragment)
					{
						this.ps.appendMode = false;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractive;
						this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.XmlDeclarationFragment;
						goto IL_147;
					}
				}
			}
			this.Throw("Xml_PartialContentNodeTypeNotSupportedEx", fragmentType.ToString());
			return;
			IL_147:
			this.fragmentType = fragmentType;
			this.fragment = true;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00028EE0 File Offset: 0x000270E0
		private void ProcessDtdFromParserContext(XmlParserContext context)
		{
			switch (this.dtdProcessing)
			{
			case DtdProcessing.Prohibit:
				this.ThrowWithoutLineInfo("Xml_DtdIsProhibitedEx");
				return;
			case DtdProcessing.Ignore:
				break;
			case DtdProcessing.Parse:
				this.ParseDtdFromParserContext();
				break;
			default:
				return;
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00028F1C File Offset: 0x0002711C
		private void OpenUrl()
		{
			XmlResolver tempResolver = this.GetTempResolver();
			if (!(this.ps.baseUri != null))
			{
				this.ps.baseUri = tempResolver.ResolveUri(null, this.url);
				this.ps.baseUriStr = this.ps.baseUri.ToString();
			}
			try
			{
				CompressedStack.Run(this.compressedStack, new ContextCallback(this.OpenUrlDelegate), tempResolver);
			}
			catch
			{
				this.SetErrorState();
				throw;
			}
			if (this.ps.stream == null)
			{
				this.ThrowWithoutLineInfo("Xml_CannotResolveUrl", this.ps.baseUriStr);
			}
			this.InitStreamInput(this.ps.baseUri, this.ps.baseUriStr, this.ps.stream, null);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00029008 File Offset: 0x00027208
		private void OpenUrlDelegate(object xmlResolver)
		{
			this.ps.stream = (Stream)this.GetTempResolver().GetEntity(this.ps.baseUri, null, typeof(Stream));
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002903C File Offset: 0x0002723C
		private Encoding DetectEncoding()
		{
			if (this.ps.bytesUsed < 2)
			{
				return null;
			}
			int num = (int)this.ps.bytes[0] << 8 | (int)this.ps.bytes[1];
			int num2 = (this.ps.bytesUsed >= 4) ? ((int)this.ps.bytes[2] << 8 | (int)this.ps.bytes[3]) : 0;
			if (num <= 15360)
			{
				if (num != 0)
				{
					if (num != 60)
					{
						if (num == 15360)
						{
							if (num2 == 0)
							{
								return Ucs4Encoding.UCS4_Littleendian;
							}
							return Encoding.Unicode;
						}
					}
					else
					{
						if (num2 == 0)
						{
							return Ucs4Encoding.UCS4_3412;
						}
						return Encoding.BigEndianUnicode;
					}
				}
				else if (num2 <= 15360)
				{
					if (num2 == 60)
					{
						return Ucs4Encoding.UCS4_Bigendian;
					}
					if (num2 == 15360)
					{
						return Ucs4Encoding.UCS4_2143;
					}
				}
				else
				{
					if (num2 == 65279)
					{
						return Ucs4Encoding.UCS4_Bigendian;
					}
					if (num2 == 65534)
					{
						return Ucs4Encoding.UCS4_2143;
					}
				}
			}
			else if (num <= 61371)
			{
				if (num != 19567)
				{
					if (num == 61371)
					{
						if ((num2 & 65280) == 48896)
						{
							return new UTF8Encoding(true, true);
						}
					}
				}
				else if (num2 == 42900)
				{
					this.Throw("Xml_UnknownEncoding", "ebcdic");
				}
			}
			else if (num != 65279)
			{
				if (num == 65534)
				{
					if (num2 == 0)
					{
						return Ucs4Encoding.UCS4_Littleendian;
					}
					return Encoding.Unicode;
				}
			}
			else
			{
				if (num2 == 0)
				{
					return Ucs4Encoding.UCS4_3412;
				}
				return Encoding.BigEndianUnicode;
			}
			return null;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000291B8 File Offset: 0x000273B8
		private void SetupEncoding(Encoding encoding)
		{
			if (encoding == null)
			{
				this.ps.encoding = Encoding.UTF8;
				this.ps.decoder = new SafeAsciiDecoder();
				return;
			}
			this.ps.encoding = encoding;
			string webName = this.ps.encoding.WebName;
			if (webName == "utf-16")
			{
				this.ps.decoder = new UTF16Decoder(false);
				return;
			}
			if (!(webName == "utf-16BE"))
			{
				this.ps.decoder = encoding.GetDecoder();
				return;
			}
			this.ps.decoder = new UTF16Decoder(true);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00029258 File Offset: 0x00027458
		private void SwitchEncoding(Encoding newEncoding)
		{
			if ((newEncoding.WebName != this.ps.encoding.WebName || this.ps.decoder is SafeAsciiDecoder) && !this.afterResetState)
			{
				this.UnDecodeChars();
				this.ps.appendMode = false;
				this.SetupEncoding(newEncoding);
				this.ReadData();
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000292BC File Offset: 0x000274BC
		private Encoding CheckEncoding(string newEncodingName)
		{
			if (this.ps.stream == null)
			{
				return this.ps.encoding;
			}
			if (string.Compare(newEncodingName, "ucs-2", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "utf-16", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "iso-10646-ucs-2", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "ucs-4", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (this.ps.encoding.WebName != "utf-16BE" && this.ps.encoding.WebName != "utf-16" && string.Compare(newEncodingName, "ucs-4", StringComparison.OrdinalIgnoreCase) != 0)
				{
					if (this.afterResetState)
					{
						this.Throw("Xml_EncodingSwitchAfterResetState", newEncodingName);
					}
					else
					{
						this.ThrowWithoutLineInfo("Xml_MissingByteOrderMark");
					}
				}
				return this.ps.encoding;
			}
			Encoding encoding = null;
			if (string.Compare(newEncodingName, "utf-8", StringComparison.OrdinalIgnoreCase) == 0)
			{
				encoding = new UTF8Encoding(true, true);
			}
			else
			{
				try
				{
					encoding = Encoding.GetEncoding(newEncodingName);
				}
				catch (NotSupportedException innerException)
				{
					this.Throw("Xml_UnknownEncoding", newEncodingName, innerException);
				}
				catch (ArgumentException innerException2)
				{
					this.Throw("Xml_UnknownEncoding", newEncodingName, innerException2);
				}
			}
			if (this.afterResetState && this.ps.encoding.WebName != encoding.WebName)
			{
				this.Throw("Xml_EncodingSwitchAfterResetState", newEncodingName);
			}
			return encoding;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00029420 File Offset: 0x00027620
		private void UnDecodeChars()
		{
			if (this.maxCharactersInDocument > 0L)
			{
				this.charactersInDocument -= (long)(this.ps.charsUsed - this.ps.charPos);
			}
			if (this.maxCharactersFromEntities > 0L && this.InEntity)
			{
				this.charactersFromEntities -= (long)(this.ps.charsUsed - this.ps.charPos);
			}
			this.ps.bytePos = this.documentStartBytePos;
			if (this.ps.charPos > 0)
			{
				this.ps.bytePos = this.ps.bytePos + this.ps.encoding.GetByteCount(this.ps.chars, 0, this.ps.charPos);
			}
			this.ps.charsUsed = this.ps.charPos;
			this.ps.isEof = false;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002950A File Offset: 0x0002770A
		private void SwitchEncodingToUTF8()
		{
			this.SwitchEncoding(new UTF8Encoding(true, true));
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002951C File Offset: 0x0002771C
		private int ReadData()
		{
			if (this.ps.isEof)
			{
				return 0;
			}
			int num;
			if (this.ps.appendMode)
			{
				if (this.ps.charsUsed == this.ps.chars.Length - 1)
				{
					for (int i = 0; i < this.attrCount; i++)
					{
						this.nodes[this.index + i + 1].OnBufferInvalidated();
					}
					char[] array = new char[this.ps.chars.Length * 2];
					XmlTextReaderImpl.BlockCopyChars(this.ps.chars, 0, array, 0, this.ps.chars.Length);
					this.ps.chars = array;
				}
				if (this.ps.stream != null && this.ps.bytesUsed - this.ps.bytePos < 6 && this.ps.bytes.Length - this.ps.bytesUsed < 6)
				{
					byte[] array2 = new byte[this.ps.bytes.Length * 2];
					XmlTextReaderImpl.BlockCopy(this.ps.bytes, 0, array2, 0, this.ps.bytesUsed);
					this.ps.bytes = array2;
				}
				num = this.ps.chars.Length - this.ps.charsUsed - 1;
				if (num > 80)
				{
					num = 80;
				}
			}
			else
			{
				int num2 = this.ps.chars.Length;
				if (num2 - this.ps.charsUsed <= num2 / 2)
				{
					for (int j = 0; j < this.attrCount; j++)
					{
						this.nodes[this.index + j + 1].OnBufferInvalidated();
					}
					int num3 = this.ps.charsUsed - this.ps.charPos;
					if (num3 < num2 - 1)
					{
						this.ps.lineStartPos = this.ps.lineStartPos - this.ps.charPos;
						if (num3 > 0)
						{
							XmlTextReaderImpl.BlockCopyChars(this.ps.chars, this.ps.charPos, this.ps.chars, 0, num3);
						}
						this.ps.charPos = 0;
						this.ps.charsUsed = num3;
					}
					else
					{
						char[] array3 = new char[this.ps.chars.Length * 2];
						XmlTextReaderImpl.BlockCopyChars(this.ps.chars, 0, array3, 0, this.ps.chars.Length);
						this.ps.chars = array3;
					}
				}
				if (this.ps.stream != null)
				{
					int num4 = this.ps.bytesUsed - this.ps.bytePos;
					if (num4 <= 128)
					{
						if (num4 == 0)
						{
							this.ps.bytesUsed = 0;
						}
						else
						{
							XmlTextReaderImpl.BlockCopy(this.ps.bytes, this.ps.bytePos, this.ps.bytes, 0, num4);
							this.ps.bytesUsed = num4;
						}
						this.ps.bytePos = 0;
					}
				}
				num = this.ps.chars.Length - this.ps.charsUsed - 1;
			}
			if (this.ps.stream != null)
			{
				if (!this.ps.isStreamEof && this.ps.bytePos == this.ps.bytesUsed && this.ps.bytes.Length - this.ps.bytesUsed > 0)
				{
					int num5 = this.ps.stream.Read(this.ps.bytes, this.ps.bytesUsed, this.ps.bytes.Length - this.ps.bytesUsed);
					if (num5 == 0)
					{
						this.ps.isStreamEof = true;
					}
					this.ps.bytesUsed = this.ps.bytesUsed + num5;
				}
				int bytePos = this.ps.bytePos;
				num = this.GetChars(num);
				if (num == 0 && this.ps.bytePos != bytePos)
				{
					return this.ReadData();
				}
			}
			else if (this.ps.textReader != null)
			{
				num = this.ps.textReader.Read(this.ps.chars, this.ps.charsUsed, this.ps.chars.Length - this.ps.charsUsed - 1);
				this.ps.charsUsed = this.ps.charsUsed + num;
			}
			else
			{
				num = 0;
			}
			this.RegisterConsumedCharacters((long)num, this.InEntity);
			if (num == 0)
			{
				this.ps.isEof = true;
			}
			this.ps.chars[this.ps.charsUsed] = '\0';
			return num;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000299B8 File Offset: 0x00027BB8
		private int GetChars(int maxCharsCount)
		{
			int num = this.ps.bytesUsed - this.ps.bytePos;
			if (num == 0)
			{
				return 0;
			}
			int num2;
			try
			{
				bool flag;
				this.ps.decoder.Convert(this.ps.bytes, this.ps.bytePos, num, this.ps.chars, this.ps.charsUsed, maxCharsCount, false, out num, out num2, out flag);
			}
			catch (ArgumentException)
			{
				this.InvalidCharRecovery(ref num, out num2);
			}
			this.ps.bytePos = this.ps.bytePos + num;
			this.ps.charsUsed = this.ps.charsUsed + num2;
			return num2;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00029A68 File Offset: 0x00027C68
		private void InvalidCharRecovery(ref int bytesCount, out int charsCount)
		{
			int num = 0;
			int i = 0;
			try
			{
				while (i < bytesCount)
				{
					int num2;
					int num3;
					bool flag;
					this.ps.decoder.Convert(this.ps.bytes, this.ps.bytePos + i, 1, this.ps.chars, this.ps.charsUsed + num, 1, false, out num2, out num3, out flag);
					num += num3;
					i += num2;
				}
			}
			catch (ArgumentException)
			{
			}
			if (num == 0)
			{
				this.Throw(this.ps.charsUsed, "Xml_InvalidCharInThisEncoding");
			}
			charsCount = num;
			bytesCount = i;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00029B08 File Offset: 0x00027D08
		internal void Close(bool closeInput)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ReaderClosed)
			{
				return;
			}
			while (this.InEntity)
			{
				this.PopParsingState();
			}
			this.ps.Close(closeInput);
			this.curNode = XmlTextReaderImpl.NodeData.None;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ReaderClosed;
			this.reportedEncoding = null;
			this.reportedBaseUri = string.Empty;
			this.readState = ReadState.Closed;
			this.fullAttrCleanup = false;
			this.ResetAttributes();
			this.laterInitParam = null;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00029B7A File Offset: 0x00027D7A
		private void ShiftBuffer(int sourcePos, int destPos, int count)
		{
			XmlTextReaderImpl.BlockCopyChars(this.ps.chars, sourcePos, this.ps.chars, destPos, count);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00029B9C File Offset: 0x00027D9C
		private unsafe bool ParseXmlDeclaration(bool isTextDecl)
		{
			while (this.ps.charsUsed - this.ps.charPos < 6)
			{
				if (this.ReadData() == 0)
				{
					IL_7E1:
					if (!isTextDecl)
					{
						this.parsingFunction = this.nextParsingFunction;
					}
					if (this.afterResetState)
					{
						string webName = this.ps.encoding.WebName;
						if (webName != "utf-8" && webName != "utf-16" && webName != "utf-16BE" && !(this.ps.encoding is Ucs4Encoding))
						{
							this.Throw("Xml_EncodingSwitchAfterResetState", (this.ps.encoding.GetByteCount("A") == 1) ? "UTF-8" : "UTF-16");
						}
					}
					if (this.ps.decoder is SafeAsciiDecoder)
					{
						this.SwitchEncodingToUTF8();
					}
					this.ps.appendMode = false;
					return false;
				}
			}
			if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 5, "<?xml") && !this.xmlCharType.IsNameSingleChar(this.ps.chars[this.ps.charPos + 5]))
			{
				if (!isTextDecl)
				{
					this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos + 2);
					this.curNode.SetNamedNode(XmlNodeType.XmlDeclaration, this.Xml);
				}
				this.ps.charPos = this.ps.charPos + 5;
				StringBuilder stringBuilder = isTextDecl ? new StringBuilder() : this.stringBuilder;
				int num = 0;
				Encoding encoding = null;
				for (;;)
				{
					int length = stringBuilder.Length;
					int num2 = this.EatWhitespaces((num == 0) ? null : stringBuilder);
					if (this.ps.chars[this.ps.charPos] == '?')
					{
						stringBuilder.Length = length;
						if (this.ps.chars[this.ps.charPos + 1] == '>')
						{
							break;
						}
						if (this.ps.charPos + 1 == this.ps.charsUsed)
						{
							goto IL_7B9;
						}
						this.ThrowUnexpectedToken("'>'");
					}
					if (num2 == 0 && num != 0)
					{
						this.ThrowUnexpectedToken("?>");
					}
					int num3 = this.ParseName();
					XmlTextReaderImpl.NodeData nodeData = null;
					char c = this.ps.chars[this.ps.charPos];
					if (c != 'e')
					{
						if (c != 's')
						{
							if (c != 'v' || !XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "version") || num != 0)
							{
								goto IL_3B5;
							}
							if (!isTextDecl)
							{
								nodeData = this.AddAttributeNoChecks("version", 1);
							}
						}
						else
						{
							if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "standalone") || (num != 1 && num != 2) || isTextDecl)
							{
								goto IL_3B5;
							}
							if (!isTextDecl)
							{
								nodeData = this.AddAttributeNoChecks("standalone", 1);
							}
							num = 2;
						}
					}
					else
					{
						if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "encoding") || (num != 1 && (!isTextDecl || num != 0)))
						{
							goto IL_3B5;
						}
						if (!isTextDecl)
						{
							nodeData = this.AddAttributeNoChecks("encoding", 1);
						}
						num = 1;
					}
					IL_3CA:
					if (!isTextDecl)
					{
						nodeData.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
					}
					stringBuilder.Append(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos);
					this.ps.charPos = num3;
					if (this.ps.chars[this.ps.charPos] != '=')
					{
						this.EatWhitespaces(stringBuilder);
						if (this.ps.chars[this.ps.charPos] != '=')
						{
							this.ThrowUnexpectedToken("=");
						}
					}
					stringBuilder.Append('=');
					this.ps.charPos = this.ps.charPos + 1;
					char c2 = this.ps.chars[this.ps.charPos];
					if (c2 != '"' && c2 != '\'')
					{
						this.EatWhitespaces(stringBuilder);
						c2 = this.ps.chars[this.ps.charPos];
						if (c2 != '"' && c2 != '\'')
						{
							this.ThrowUnexpectedToken("\"", "'");
						}
					}
					stringBuilder.Append(c2);
					this.ps.charPos = this.ps.charPos + 1;
					if (!isTextDecl)
					{
						nodeData.quoteChar = c2;
						nodeData.SetLineInfo2(this.ps.LineNo, this.ps.LinePos);
					}
					int num4 = this.ps.charPos;
					char[] chars;
					for (;;)
					{
						chars = this.ps.chars;
						while ((this.xmlCharType.charProperties[chars[num4]] & 128) != 0)
						{
							num4++;
						}
						if (this.ps.chars[num4] == c2)
						{
							break;
						}
						if (num4 != this.ps.charsUsed)
						{
							goto IL_7A4;
						}
						if (this.ReadData() == 0)
						{
							goto Block_57;
						}
					}
					switch (num)
					{
					case 0:
						if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "1.0"))
						{
							if (!isTextDecl)
							{
								nodeData.SetValue(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
							}
							num = 1;
						}
						else
						{
							string arg = new string(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
							this.Throw("Xml_InvalidVersionNumber", arg);
						}
						break;
					case 1:
					{
						string text = new string(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
						encoding = this.CheckEncoding(text);
						if (!isTextDecl)
						{
							nodeData.SetValue(text);
						}
						num = 2;
						break;
					}
					case 2:
						if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "yes"))
						{
							this.standalone = true;
						}
						else if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "no"))
						{
							this.standalone = false;
						}
						else
						{
							this.Throw("Xml_InvalidXmlDecl", this.ps.LineNo, this.ps.LinePos - 1);
						}
						if (!isTextDecl)
						{
							nodeData.SetValue(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
						}
						num = 3;
						break;
					}
					stringBuilder.Append(chars, this.ps.charPos, num4 - this.ps.charPos);
					stringBuilder.Append(c2);
					this.ps.charPos = num4 + 1;
					continue;
					Block_57:
					this.Throw("Xml_UnclosedQuote");
					goto IL_7B9;
					IL_7A4:
					this.Throw(isTextDecl ? "Xml_InvalidTextDecl" : "Xml_InvalidXmlDecl");
					goto IL_7B9;
					IL_3B5:
					this.Throw(isTextDecl ? "Xml_InvalidTextDecl" : "Xml_InvalidXmlDecl");
					goto IL_3CA;
					IL_7B9:
					if (this.ps.isEof || this.ReadData() == 0)
					{
						this.Throw("Xml_UnexpectedEOF1");
					}
				}
				if (num == 0)
				{
					this.Throw(isTextDecl ? "Xml_InvalidTextDecl" : "Xml_InvalidXmlDecl");
				}
				this.ps.charPos = this.ps.charPos + 2;
				if (!isTextDecl)
				{
					this.curNode.SetValue(stringBuilder.ToString());
					stringBuilder.Length = 0;
					this.nextParsingFunction = this.parsingFunction;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel;
				}
				if (encoding == null)
				{
					if (isTextDecl)
					{
						this.Throw("Xml_InvalidTextDecl");
					}
					if (this.afterResetState)
					{
						string webName2 = this.ps.encoding.WebName;
						if (webName2 != "utf-8" && webName2 != "utf-16" && webName2 != "utf-16BE" && !(this.ps.encoding is Ucs4Encoding))
						{
							this.Throw("Xml_EncodingSwitchAfterResetState", (this.ps.encoding.GetByteCount("A") == 1) ? "UTF-8" : "UTF-16");
						}
					}
					if (this.ps.decoder is SafeAsciiDecoder)
					{
						this.SwitchEncodingToUTF8();
					}
				}
				else
				{
					this.SwitchEncoding(encoding);
				}
				this.ps.appendMode = false;
				return true;
			}
			goto IL_7E1;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002A444 File Offset: 0x00028644
		private bool ParseDocumentContent()
		{
			bool flag = false;
			int num;
			for (;;)
			{
				bool flag2 = false;
				num = this.ps.charPos;
				char[] chars = this.ps.chars;
				if (chars[num] == '<')
				{
					flag2 = true;
					if (this.ps.charsUsed - num >= 4)
					{
						num++;
						char c = chars[num];
						if (c != '!')
						{
							if (c != '/')
							{
								if (c != '?')
								{
									goto IL_1D3;
								}
								this.ps.charPos = num + 1;
								if (this.ParsePI())
								{
									break;
								}
								continue;
							}
							else
							{
								this.Throw(num + 1, "Xml_UnexpectedEndTag");
							}
						}
						else
						{
							num++;
							if (this.ps.charsUsed - num >= 2)
							{
								if (chars[num] == '-')
								{
									if (chars[num + 1] == '-')
									{
										this.ps.charPos = num + 2;
										if (this.ParseComment())
										{
											return true;
										}
										continue;
									}
									else
									{
										this.ThrowUnexpectedToken(num + 1, "-");
									}
								}
								else if (chars[num] == '[')
								{
									if (this.fragmentType != XmlNodeType.Document)
									{
										num++;
										if (this.ps.charsUsed - num >= 6)
										{
											if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
											{
												goto Block_14;
											}
											this.ThrowUnexpectedToken(num, "CDATA[");
										}
									}
									else
									{
										this.Throw(this.ps.charPos, "Xml_InvalidRootData");
									}
								}
								else if (this.fragmentType == XmlNodeType.Document || this.fragmentType == XmlNodeType.None)
								{
									this.fragmentType = XmlNodeType.Document;
									this.ps.charPos = num;
									if (this.ParseDoctypeDecl())
									{
										return true;
									}
									continue;
								}
								else if (this.ParseUnexpectedToken(num) == "DOCTYPE")
								{
									this.Throw("Xml_BadDTDLocation");
								}
								else
								{
									this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
								}
							}
						}
					}
				}
				else if (chars[num] == '&')
				{
					if (this.fragmentType == XmlNodeType.Document)
					{
						this.Throw(num, "Xml_InvalidRootData");
					}
					else
					{
						if (this.fragmentType == XmlNodeType.None)
						{
							this.fragmentType = XmlNodeType.Element;
						}
						int num2;
						XmlTextReaderImpl.EntityType entityType = this.HandleEntityReference(false, XmlTextReaderImpl.EntityExpandType.OnlyGeneral, out num2);
						if (entityType > XmlTextReaderImpl.EntityType.CharacterNamed)
						{
							if (entityType == XmlTextReaderImpl.EntityType.Unexpanded)
							{
								goto Block_26;
							}
							chars = this.ps.chars;
							num = this.ps.charPos;
							continue;
						}
						else
						{
							if (this.ParseText())
							{
								return true;
							}
							continue;
						}
					}
				}
				else if (num != this.ps.charsUsed && ((!this.v1Compat && !flag) || chars[num] != '\0'))
				{
					if (this.fragmentType == XmlNodeType.Document)
					{
						if (this.ParseRootLevelWhitespace())
						{
							return true;
						}
						continue;
					}
					else
					{
						if (this.ParseText())
						{
							goto Block_33;
						}
						continue;
					}
				}
				if (this.ReadData() != 0)
				{
					num = this.ps.charPos;
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				else
				{
					if (flag2)
					{
						this.Throw("Xml_InvalidRootData");
					}
					if (!this.InEntity)
					{
						goto IL_34B;
					}
					if (this.HandleEntityEnd(true))
					{
						goto Block_39;
					}
				}
			}
			return true;
			Block_14:
			this.ps.charPos = num + 6;
			this.ParseCData();
			if (this.fragmentType == XmlNodeType.None)
			{
				this.fragmentType = XmlNodeType.Element;
			}
			return true;
			IL_1D3:
			if (this.rootElementParsed)
			{
				if (this.fragmentType == XmlNodeType.Document)
				{
					this.Throw(num, "Xml_MultipleRoots");
				}
				if (this.fragmentType == XmlNodeType.None)
				{
					this.fragmentType = XmlNodeType.Element;
				}
			}
			this.ps.charPos = num;
			this.rootElementParsed = true;
			this.ParseElement();
			return true;
			Block_26:
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
			{
				this.parsingFunction = this.nextParsingFunction;
			}
			this.ParseEntityReference();
			return true;
			Block_33:
			if (this.fragmentType == XmlNodeType.None && this.curNode.type == XmlNodeType.Text)
			{
				this.fragmentType = XmlNodeType.Element;
			}
			return true;
			Block_39:
			this.SetupEndEntityNodeInContent();
			return true;
			IL_34B:
			if (!this.rootElementParsed && this.fragmentType == XmlNodeType.Document)
			{
				this.ThrowWithoutLineInfo("Xml_MissingRoot");
			}
			if (this.fragmentType == XmlNodeType.None)
			{
				this.fragmentType = (this.rootElementParsed ? XmlNodeType.Document : XmlNodeType.Element);
			}
			this.OnEof();
			return false;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002A7F8 File Offset: 0x000289F8
		private bool ParseElementContent()
		{
			int num;
			for (;;)
			{
				num = this.ps.charPos;
				char[] chars = this.ps.chars;
				char c = chars[num];
				if (c != '&')
				{
					if (c == '<')
					{
						char c2 = chars[num + 1];
						if (c2 != '!')
						{
							if (c2 == '/')
							{
								goto IL_13B;
							}
							if (c2 == '?')
							{
								this.ps.charPos = num + 2;
								if (this.ParsePI())
								{
									break;
								}
								continue;
							}
							else if (num + 1 != this.ps.charsUsed)
							{
								goto Block_14;
							}
						}
						else
						{
							num += 2;
							if (this.ps.charsUsed - num >= 2)
							{
								if (chars[num] == '-')
								{
									if (chars[num + 1] == '-')
									{
										this.ps.charPos = num + 2;
										if (this.ParseComment())
										{
											return true;
										}
										continue;
									}
									else
									{
										this.ThrowUnexpectedToken(num + 1, "-");
									}
								}
								else if (chars[num] == '[')
								{
									num++;
									if (this.ps.charsUsed - num >= 6)
									{
										if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
										{
											goto Block_12;
										}
										this.ThrowUnexpectedToken(num, "CDATA[");
									}
								}
								else if (this.ParseUnexpectedToken(num) == "DOCTYPE")
								{
									this.Throw("Xml_BadDTDLocation");
								}
								else
								{
									this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
								}
							}
						}
					}
					else if (num != this.ps.charsUsed)
					{
						if (this.ParseText())
						{
							return true;
						}
						continue;
					}
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos != 0)
						{
							this.ThrowUnclosedElements();
						}
						if (!this.InEntity)
						{
							if (this.index == 0 && this.fragmentType != XmlNodeType.Document)
							{
								goto Block_22;
							}
							this.ThrowUnclosedElements();
						}
						if (this.HandleEntityEnd(true))
						{
							goto Block_23;
						}
					}
				}
				else if (this.ParseText())
				{
					return true;
				}
			}
			return true;
			Block_12:
			this.ps.charPos = num + 6;
			this.ParseCData();
			return true;
			IL_13B:
			this.ps.charPos = num + 2;
			this.ParseEndElement();
			return true;
			Block_14:
			this.ps.charPos = num + 1;
			this.ParseElement();
			return true;
			Block_22:
			this.OnEof();
			return false;
			Block_23:
			this.SetupEndEntityNodeInContent();
			return true;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002AA0C File Offset: 0x00028C0C
		private void ThrowUnclosedElements()
		{
			if (this.index == 0 && this.curNode.type != XmlNodeType.Element)
			{
				this.Throw(this.ps.charsUsed, "Xml_UnexpectedEOF1");
				return;
			}
			int i = (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead) ? this.index : (this.index - 1);
			this.stringBuilder.Length = 0;
			while (i >= 0)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				if (nodeData.type == XmlNodeType.Element)
				{
					this.stringBuilder.Append(nodeData.GetNameWPrefix(this.nameTable));
					if (i > 0)
					{
						this.stringBuilder.Append(", ");
					}
					else
					{
						this.stringBuilder.Append(".");
					}
				}
				i--;
			}
			this.Throw(this.ps.charsUsed, "Xml_UnexpectedEOFInElementContent", this.stringBuilder.ToString());
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002AAEC File Offset: 0x00028CEC
		private unsafe void ParseElement()
		{
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = -1;
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			while ((this.xmlCharType.charProperties[chars[num]] & 4) != 0)
			{
				num++;
				for (;;)
				{
					if ((this.xmlCharType.charProperties[chars[num]] & 8) != 0)
					{
						num++;
					}
					else
					{
						if (chars[num] != ':')
						{
							goto IL_A4;
						}
						if (num2 == -1)
						{
							break;
						}
						if (this.supportNamespaces)
						{
							goto Block_5;
						}
						num++;
					}
				}
				num2 = num;
				num++;
				continue;
				Block_5:
				this.Throw(num, "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(':', '\0'));
				break;
				IL_A4:
				if (num + 1 >= this.ps.charsUsed)
				{
					break;
				}
				IL_C9:
				this.namespaceManager.PushScope();
				if (num2 == -1 || !this.supportNamespaces)
				{
					this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, this.ps.charPos, num - this.ps.charPos));
				}
				else
				{
					int charPos = this.ps.charPos;
					int num3 = num2 - charPos;
					if (num3 == this.lastPrefix.Length && XmlConvert.StrEqual(chars, charPos, num3, this.lastPrefix))
					{
						this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, num2 + 1, num - num2 - 1), this.lastPrefix, null);
					}
					else
					{
						this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, num2 + 1, num - num2 - 1), this.nameTable.Add(chars, this.ps.charPos, num3), null);
						this.lastPrefix = this.curNode.prefix;
					}
				}
				char c = chars[num];
				bool flag = (this.xmlCharType.charProperties[c] & 1) > 0;
				if (flag)
				{
					this.ps.charPos = num;
					this.ParseAttributes();
					return;
				}
				if (c == '>')
				{
					this.ps.charPos = num + 1;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
				}
				else if (c == '/')
				{
					if (num + 1 == this.ps.charsUsed)
					{
						this.ps.charPos = num;
						if (this.ReadData() == 0)
						{
							this.Throw(num, "Xml_UnexpectedEOF", ">");
						}
						num = this.ps.charPos;
						chars = this.ps.chars;
					}
					if (chars[num + 1] == '>')
					{
						this.curNode.IsEmptyElement = true;
						this.nextParsingFunction = this.parsingFunction;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
						this.ps.charPos = num + 2;
					}
					else
					{
						this.ThrowUnexpectedToken(num, ">");
					}
				}
				else
				{
					this.Throw(num, "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(chars, this.ps.charsUsed, num));
				}
				if (this.addDefaultAttributesAndNormalize)
				{
					this.AddDefaultAttributesAndNormalize();
				}
				this.ElementNamespaceLookup();
				return;
			}
			num = this.ParseQName(out num2);
			chars = this.ps.chars;
			goto IL_C9;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002ADD4 File Offset: 0x00028FD4
		private void AddDefaultAttributesAndNormalize()
		{
			IDtdAttributeListInfo dtdAttributeListInfo = this.dtdInfo.LookupAttributeList(this.curNode.localName, this.curNode.prefix);
			if (dtdAttributeListInfo == null)
			{
				return;
			}
			if (this.normalize && dtdAttributeListInfo.HasNonCDataAttributes)
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
					IDtdAttributeInfo dtdAttributeInfo = dtdAttributeListInfo.LookupAttribute(nodeData.prefix, nodeData.localName);
					if (dtdAttributeInfo != null && dtdAttributeInfo.IsNonCDataType)
					{
						if (this.DtdValidation && this.standalone && dtdAttributeInfo.IsDeclaredInExternal)
						{
							string stringValue = nodeData.StringValue;
							nodeData.TrimSpacesInValue();
							if (stringValue != nodeData.StringValue)
							{
								this.SendValidationEvent(XmlSeverityType.Error, "Sch_StandAloneNormalization", nodeData.GetNameWPrefix(this.nameTable), nodeData.LineNo, nodeData.LinePos);
							}
						}
						else
						{
							nodeData.TrimSpacesInValue();
						}
					}
				}
			}
			IEnumerable<IDtdDefaultAttributeInfo> enumerable = dtdAttributeListInfo.LookupDefaultAttributes();
			if (enumerable != null)
			{
				int num = this.attrCount;
				XmlTextReaderImpl.NodeData[] array = null;
				if (this.attrCount >= 64)
				{
					array = new XmlTextReaderImpl.NodeData[this.attrCount];
					Array.Copy(this.nodes, this.index + 1, array, 0, this.attrCount);
					object[] array2 = array;
					Array.Sort<object>(array2, XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.Instance);
				}
				foreach (IDtdDefaultAttributeInfo dtdDefaultAttributeInfo in enumerable)
				{
					if (this.AddDefaultAttributeDtd(dtdDefaultAttributeInfo, true, array) && this.DtdValidation && this.standalone && dtdDefaultAttributeInfo.IsDeclaredInExternal)
					{
						string prefix = dtdDefaultAttributeInfo.Prefix;
						string arg = (prefix.Length == 0) ? dtdDefaultAttributeInfo.LocalName : (prefix + ":" + dtdDefaultAttributeInfo.LocalName);
						this.SendValidationEvent(XmlSeverityType.Error, "Sch_UnSpecifiedDefaultAttributeInExternalStandalone", arg, this.curNode.LineNo, this.curNode.LinePos);
					}
				}
				if (num == 0 && this.attrNeedNamespaceLookup)
				{
					this.AttributeNamespaceLookup();
					this.attrNeedNamespaceLookup = false;
				}
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002B000 File Offset: 0x00029200
		private unsafe void ParseEndElement()
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[this.index - 1];
			int length = nodeData.prefix.Length;
			int length2 = nodeData.localName.Length;
			while (this.ps.charsUsed - this.ps.charPos < length + length2 + 1 && this.ReadData() != 0)
			{
			}
			char[] chars = this.ps.chars;
			int num;
			if (nodeData.prefix.Length == 0)
			{
				if (!XmlConvert.StrEqual(chars, this.ps.charPos, length2, nodeData.localName))
				{
					this.ThrowTagMismatch(nodeData);
				}
				num = length2;
			}
			else
			{
				int num2 = this.ps.charPos + length;
				if (!XmlConvert.StrEqual(chars, this.ps.charPos, length, nodeData.prefix) || chars[num2] != ':' || !XmlConvert.StrEqual(chars, num2 + 1, length2, nodeData.localName))
				{
					this.ThrowTagMismatch(nodeData);
				}
				num = length2 + length + 1;
			}
			LineInfo lineInfo = new LineInfo(this.ps.lineNo, this.ps.LinePos);
			int num3;
			for (;;)
			{
				num3 = this.ps.charPos + num;
				chars = this.ps.chars;
				if (num3 != this.ps.charsUsed)
				{
					if ((this.xmlCharType.charProperties[chars[num3]] & 8) != 0 || chars[num3] == ':')
					{
						this.ThrowTagMismatch(nodeData);
					}
					if (chars[num3] != '>')
					{
						char c;
						while (this.xmlCharType.IsWhiteSpace(c = chars[num3]))
						{
							num3++;
							if (c != '\n')
							{
								if (c == '\r')
								{
									if (chars[num3] == '\n')
									{
										num3++;
									}
									else if (num3 == this.ps.charsUsed && !this.ps.isEof)
									{
										continue;
									}
									this.OnNewLine(num3);
								}
							}
							else
							{
								this.OnNewLine(num3);
							}
						}
					}
					if (chars[num3] == '>')
					{
						break;
					}
					if (num3 != this.ps.charsUsed)
					{
						this.ThrowUnexpectedToken(num3, ">");
					}
				}
				if (this.ReadData() == 0)
				{
					this.ThrowUnclosedElements();
				}
			}
			this.index--;
			this.curNode = this.nodes[this.index];
			nodeData.lineInfo = lineInfo;
			nodeData.type = XmlNodeType.EndElement;
			this.ps.charPos = num3 + 1;
			this.nextParsingFunction = ((this.index > 0) ? this.parsingFunction : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopElementContext;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002B270 File Offset: 0x00029470
		private void ThrowTagMismatch(XmlTextReaderImpl.NodeData startTag)
		{
			if (startTag.type == XmlNodeType.Element)
			{
				int num2;
				int num = this.ParseQName(out num2);
				this.Throw("Xml_TagMismatchEx", new string[]
				{
					startTag.GetNameWPrefix(this.nameTable),
					startTag.lineInfo.lineNo.ToString(CultureInfo.InvariantCulture),
					startTag.lineInfo.linePos.ToString(CultureInfo.InvariantCulture),
					new string(this.ps.chars, this.ps.charPos, num - this.ps.charPos)
				});
				return;
			}
			this.Throw("Xml_UnexpectedEndTag");
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002B31C File Offset: 0x0002951C
		private unsafe void ParseAttributes()
		{
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			for (;;)
			{
				IL_1A:
				int num2 = 0;
				char c;
				while ((this.xmlCharType.charProperties[c = chars[num]] & 1) != 0)
				{
					if (c == '\n')
					{
						this.OnNewLine(num + 1);
						num2++;
					}
					else if (c == '\r')
					{
						if (chars[num + 1] == '\n')
						{
							this.OnNewLine(num + 2);
							num2++;
							num++;
						}
						else if (num + 1 != this.ps.charsUsed)
						{
							this.OnNewLine(num + 1);
							num2++;
						}
						else
						{
							this.ps.charPos = num;
							IL_431:
							this.ps.lineNo = this.ps.lineNo - num2;
							if (this.ReadData() != 0)
							{
								num = this.ps.charPos;
								chars = this.ps.chars;
								goto IL_1A;
							}
							this.ThrowUnclosedElements();
							goto IL_1A;
						}
					}
					num++;
				}
				int num3 = 0;
				char c2;
				if ((this.xmlCharType.charProperties[c2 = chars[num]] & 4) != 0)
				{
					num3 = 1;
				}
				if (num3 == 0)
				{
					if (c2 == '>')
					{
						break;
					}
					if (c2 == '/')
					{
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_431;
						}
						if (chars[num + 1] == '>')
						{
							goto Block_11;
						}
						this.ThrowUnexpectedToken(num + 1, ">");
					}
					else
					{
						if (num == this.ps.charsUsed)
						{
							goto IL_431;
						}
						if (c2 != ':' || this.supportNamespaces)
						{
							this.Throw(num, "Xml_BadStartNameChar", XmlException.BuildCharExceptionArgs(chars, this.ps.charsUsed, num));
						}
					}
				}
				if (num == this.ps.charPos)
				{
					this.ThrowExpectingWhitespace(num);
				}
				this.ps.charPos = num;
				int linePos = this.ps.LinePos;
				int num4 = -1;
				num += num3;
				for (;;)
				{
					char c3;
					if ((this.xmlCharType.charProperties[c3 = chars[num]] & 8) != 0)
					{
						num++;
					}
					else
					{
						if (c3 != ':')
						{
							goto IL_242;
						}
						if (num4 != -1)
						{
							if (this.supportNamespaces)
							{
								goto Block_18;
							}
							num++;
						}
						else
						{
							num4 = num;
							num++;
							if ((this.xmlCharType.charProperties[chars[num]] & 4) == 0)
							{
								goto IL_22B;
							}
							num++;
						}
					}
				}
				IL_267:
				XmlTextReaderImpl.NodeData nodeData = this.AddAttribute(num, num4);
				nodeData.SetLineInfo(this.ps.LineNo, linePos);
				if (chars[num] != '=')
				{
					this.ps.charPos = num;
					this.EatWhitespaces(null);
					num = this.ps.charPos;
					if (chars[num] != '=')
					{
						this.ThrowUnexpectedToken("=");
					}
				}
				num++;
				char c4 = chars[num];
				if (c4 != '"' && c4 != '\'')
				{
					this.ps.charPos = num;
					this.EatWhitespaces(null);
					num = this.ps.charPos;
					c4 = chars[num];
					if (c4 != '"' && c4 != '\'')
					{
						this.ThrowUnexpectedToken("\"", "'");
					}
				}
				num++;
				this.ps.charPos = num;
				nodeData.quoteChar = c4;
				nodeData.SetLineInfo2(this.ps.LineNo, this.ps.LinePos);
				char c5;
				while ((this.xmlCharType.charProperties[c5 = chars[num]] & 128) != 0)
				{
					num++;
				}
				if (c5 == c4)
				{
					nodeData.SetValue(chars, this.ps.charPos, num - this.ps.charPos);
					num++;
					this.ps.charPos = num;
				}
				else
				{
					this.ParseAttributeValueSlow(num, c4, nodeData);
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				if (nodeData.prefix.Length == 0)
				{
					if (Ref.Equal(nodeData.localName, this.XmlNs))
					{
						this.OnDefaultNamespaceDecl(nodeData);
						continue;
					}
					continue;
				}
				else
				{
					if (Ref.Equal(nodeData.prefix, this.XmlNs))
					{
						this.OnNamespaceDecl(nodeData);
						continue;
					}
					if (Ref.Equal(nodeData.prefix, this.Xml))
					{
						this.OnXmlReservedAttribute(nodeData);
						continue;
					}
					continue;
				}
				Block_18:
				this.Throw(num, "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(':', '\0'));
				goto IL_267;
				IL_22B:
				num = this.ParseQName(out num4);
				chars = this.ps.chars;
				goto IL_267;
				IL_242:
				if (num + 1 >= this.ps.charsUsed)
				{
					num = this.ParseQName(out num4);
					chars = this.ps.chars;
					goto IL_267;
				}
				goto IL_267;
			}
			this.ps.charPos = num + 1;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
			goto IL_471;
			Block_11:
			this.ps.charPos = num + 2;
			this.curNode.IsEmptyElement = true;
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
			IL_471:
			if (this.addDefaultAttributesAndNormalize)
			{
				this.AddDefaultAttributesAndNormalize();
			}
			this.ElementNamespaceLookup();
			if (this.attrNeedNamespaceLookup)
			{
				this.AttributeNamespaceLookup();
				this.attrNeedNamespaceLookup = false;
			}
			if (this.attrDuplWalkCount >= 64)
			{
				this.AttributeDuplCheck();
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002B7D4 File Offset: 0x000299D4
		private void ElementNamespaceLookup()
		{
			if (this.curNode.prefix.Length == 0)
			{
				this.curNode.ns = this.xmlContext.defaultNamespace;
				return;
			}
			this.curNode.ns = this.LookupNamespace(this.curNode);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002B824 File Offset: 0x00029A24
		private void AttributeNamespaceLookup()
		{
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				if (nodeData.type == XmlNodeType.Attribute && nodeData.prefix.Length > 0)
				{
					nodeData.ns = this.LookupNamespace(nodeData);
				}
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002B880 File Offset: 0x00029A80
		private void AttributeDuplCheck()
		{
			if (this.attrCount < 64)
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
					for (int j = i + 1; j < this.index + 1 + this.attrCount; j++)
					{
						if (Ref.Equal(nodeData.localName, this.nodes[j].localName) && Ref.Equal(nodeData.ns, this.nodes[j].ns))
						{
							this.Throw("Xml_DupAttributeName", this.nodes[j].GetNameWPrefix(this.nameTable), this.nodes[j].LineNo, this.nodes[j].LinePos);
						}
					}
				}
				return;
			}
			if (this.attrDuplSet == null)
			{
				this.attrDuplSet = new HashSet<XmlTextReaderImpl.NodeData>(XmlTextReaderImpl.NodeData.AtomizedNameEqualityComparer.Instance);
			}
			this.attrDuplSet.Clear();
			for (int k = this.index + 1; k < this.index + 1 + this.attrCount; k++)
			{
				if (!this.attrDuplSet.Add(this.nodes[k]))
				{
					this.Throw("Xml_DupAttributeName", this.nodes[k].GetNameWPrefix(this.nameTable), this.nodes[k].LineNo, this.nodes[k].LinePos);
				}
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002B9E8 File Offset: 0x00029BE8
		private void OnDefaultNamespaceDecl(XmlTextReaderImpl.NodeData attr)
		{
			if (!this.supportNamespaces)
			{
				return;
			}
			string text = this.nameTable.Add(attr.StringValue);
			attr.ns = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			if (!this.curNode.xmlContextPushed)
			{
				this.PushXmlContext();
			}
			this.xmlContext.defaultNamespace = text;
			this.AddNamespace(string.Empty, text, attr);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002BA54 File Offset: 0x00029C54
		private void OnNamespaceDecl(XmlTextReaderImpl.NodeData attr)
		{
			if (!this.supportNamespaces)
			{
				return;
			}
			string text = this.nameTable.Add(attr.StringValue);
			if (text.Length == 0)
			{
				this.Throw("Xml_BadNamespaceDecl", attr.lineInfo2.lineNo, attr.lineInfo2.linePos - 1);
			}
			this.AddNamespace(attr.localName, text, attr);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		private void OnXmlReservedAttribute(XmlTextReaderImpl.NodeData attr)
		{
			string localName = attr.localName;
			if (!(localName == "space"))
			{
				if (!(localName == "lang"))
				{
					return;
				}
				if (!this.curNode.xmlContextPushed)
				{
					this.PushXmlContext();
				}
				this.xmlContext.xmlLang = attr.StringValue;
				return;
			}
			else
			{
				if (!this.curNode.xmlContextPushed)
				{
					this.PushXmlContext();
				}
				string a = XmlConvert.TrimString(attr.StringValue);
				if (a == "preserve")
				{
					this.xmlContext.xmlSpace = XmlSpace.Preserve;
					return;
				}
				if (!(a == "default"))
				{
					this.Throw("Xml_InvalidXmlSpace", attr.StringValue, attr.lineInfo.lineNo, attr.lineInfo.linePos);
					return;
				}
				this.xmlContext.xmlSpace = XmlSpace.Default;
				return;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002BB88 File Offset: 0x00029D88
		private unsafe void ParseAttributeValueSlow(int curPos, char quoteChar, XmlTextReaderImpl.NodeData attr)
		{
			int num = curPos;
			char[] chars = this.ps.chars;
			int entityId = this.ps.entityId;
			int num2 = 0;
			LineInfo lineInfo = new LineInfo(this.ps.lineNo, this.ps.LinePos);
			XmlTextReaderImpl.NodeData nodeData = null;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[chars[num]] & 128) == 0)
				{
					if (num - this.ps.charPos > 0)
					{
						this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
						this.ps.charPos = num;
					}
					if (chars[num] == quoteChar && entityId == this.ps.entityId)
					{
						goto IL_644;
					}
					char c = chars[num];
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							num++;
							if (this.normalize)
							{
								this.stringBuilder.Append(' ');
								this.ps.charPos = this.ps.charPos + 1;
								continue;
							}
							continue;
						case '\n':
							num++;
							this.OnNewLine(num);
							if (this.normalize)
							{
								this.stringBuilder.Append(' ');
								this.ps.charPos = this.ps.charPos + 1;
								continue;
							}
							continue;
						case '\v':
						case '\f':
							goto IL_4F9;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								num += 2;
								if (this.normalize)
								{
									this.stringBuilder.Append(this.ps.eolNormalized ? "  " : " ");
									this.ps.charPos = num;
								}
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_54F;
								}
								num++;
								if (this.normalize)
								{
									this.stringBuilder.Append(' ');
									this.ps.charPos = num;
								}
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c != '"')
							{
								if (c != '&')
								{
									goto IL_4F9;
								}
								if (num - this.ps.charPos > 0)
								{
									this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
								}
								this.ps.charPos = num;
								int entityId2 = this.ps.entityId;
								LineInfo lineInfo2 = new LineInfo(this.ps.lineNo, this.ps.LinePos + 1);
								switch (this.HandleEntityReference(true, XmlTextReaderImpl.EntityExpandType.All, out num))
								{
								case XmlTextReaderImpl.EntityType.CharacterDec:
								case XmlTextReaderImpl.EntityType.CharacterHex:
								case XmlTextReaderImpl.EntityType.CharacterNamed:
									break;
								case XmlTextReaderImpl.EntityType.Expanded:
								case XmlTextReaderImpl.EntityType.Skipped:
								case XmlTextReaderImpl.EntityType.FakeExpanded:
									goto IL_4DC;
								case XmlTextReaderImpl.EntityType.Unexpanded:
									if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && this.ps.entityId == entityId)
									{
										int num3 = this.stringBuilder.Length - num2;
										if (num3 > 0)
										{
											XmlTextReaderImpl.NodeData nodeData2 = new XmlTextReaderImpl.NodeData();
											nodeData2.lineInfo = lineInfo;
											nodeData2.depth = attr.depth + 1;
											nodeData2.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num3));
											this.AddAttributeChunkToList(attr, nodeData2, ref nodeData);
										}
										this.ps.charPos = this.ps.charPos + 1;
										string text = this.ParseEntityName();
										XmlTextReaderImpl.NodeData nodeData3 = new XmlTextReaderImpl.NodeData();
										nodeData3.lineInfo = lineInfo2;
										nodeData3.depth = attr.depth + 1;
										nodeData3.SetNamedNode(XmlNodeType.EntityReference, text);
										this.AddAttributeChunkToList(attr, nodeData3, ref nodeData);
										this.stringBuilder.Append('&');
										this.stringBuilder.Append(text);
										this.stringBuilder.Append(';');
										num2 = this.stringBuilder.Length;
										lineInfo.Set(this.ps.LineNo, this.ps.LinePos);
										this.fullAttrCleanup = true;
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
										this.ParseEntityName();
									}
									num = this.ps.charPos;
									break;
								case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
									if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && entityId2 == entityId)
									{
										int num4 = this.stringBuilder.Length - num2;
										if (num4 > 0)
										{
											XmlTextReaderImpl.NodeData nodeData4 = new XmlTextReaderImpl.NodeData();
											nodeData4.lineInfo = lineInfo;
											nodeData4.depth = attr.depth + 1;
											nodeData4.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num4));
											this.AddAttributeChunkToList(attr, nodeData4, ref nodeData);
										}
										XmlTextReaderImpl.NodeData nodeData5 = new XmlTextReaderImpl.NodeData();
										nodeData5.lineInfo = lineInfo2;
										nodeData5.depth = attr.depth + 1;
										nodeData5.SetNamedNode(XmlNodeType.EntityReference, this.ps.entity.Name);
										this.AddAttributeChunkToList(attr, nodeData5, ref nodeData);
										this.fullAttrCleanup = true;
									}
									num = this.ps.charPos;
									break;
								default:
									goto IL_4DC;
								}
								IL_4E8:
								chars = this.ps.chars;
								continue;
								IL_4DC:
								num = this.ps.charPos;
								goto IL_4E8;
							}
							break;
						}
					}
					else if (c != '\'')
					{
						if (c == '<')
						{
							this.Throw(num, "Xml_BadAttributeChar", XmlException.BuildCharExceptionArgs('<', '\0'));
							goto IL_54F;
						}
						if (c != '>')
						{
							goto IL_4F9;
						}
					}
					num++;
					continue;
					IL_4F9:
					if (num != this.ps.charsUsed)
					{
						char ch = chars[num];
						if (XmlCharType.IsHighSurrogate((int)ch))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_54F;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)chars[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
					}
					IL_54F:
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r')
							{
								this.Throw("Xml_UnexpectedEOF1");
							}
						}
						else
						{
							if (!this.InEntity)
							{
								if (this.fragmentType == XmlNodeType.Attribute)
								{
									break;
								}
								this.Throw("Xml_UnclosedQuote");
							}
							if (this.HandleEntityEnd(true))
							{
								this.Throw("Xml_InternalError");
							}
							if (entityId == this.ps.entityId)
							{
								num2 = this.stringBuilder.Length;
								lineInfo.Set(this.ps.LineNo, this.ps.LinePos);
							}
						}
					}
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				else
				{
					num++;
				}
			}
			if (entityId != this.ps.entityId)
			{
				this.Throw("Xml_EntityRefNesting");
			}
			IL_644:
			if (attr.nextAttrValueChunk != null)
			{
				int num5 = this.stringBuilder.Length - num2;
				if (num5 > 0)
				{
					XmlTextReaderImpl.NodeData nodeData6 = new XmlTextReaderImpl.NodeData();
					nodeData6.lineInfo = lineInfo;
					nodeData6.depth = attr.depth + 1;
					nodeData6.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num5));
					this.AddAttributeChunkToList(attr, nodeData6, ref nodeData);
				}
			}
			this.ps.charPos = num + 1;
			attr.SetValue(this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002C260 File Offset: 0x0002A460
		private void AddAttributeChunkToList(XmlTextReaderImpl.NodeData attr, XmlTextReaderImpl.NodeData chunk, ref XmlTextReaderImpl.NodeData lastChunk)
		{
			if (lastChunk == null)
			{
				lastChunk = chunk;
				attr.nextAttrValueChunk = chunk;
				return;
			}
			lastChunk.nextAttrValueChunk = chunk;
			lastChunk = chunk;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002C27C File Offset: 0x0002A47C
		private bool ParseText()
		{
			int num = 0;
			if (this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
			{
				int num2;
				int num3;
				while (!this.ParseText(out num2, out num3, ref num))
				{
				}
			}
			else
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				int num2;
				int num3;
				if (this.ParseText(out num2, out num3, ref num))
				{
					if (num3 - num2 != 0)
					{
						XmlNodeType textNodeType = this.GetTextNodeType(num);
						if (textNodeType != XmlNodeType.None)
						{
							this.curNode.SetValueNode(textNodeType, this.ps.chars, num2, num3 - num2);
							return true;
						}
					}
				}
				else if (this.v1Compat)
				{
					do
					{
						if (num3 - num2 > 0)
						{
							this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
						}
					}
					while (!this.ParseText(out num2, out num3, ref num));
					if (num3 - num2 > 0)
					{
						this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
					}
					XmlNodeType textNodeType2 = this.GetTextNodeType(num);
					if (textNodeType2 != XmlNodeType.None)
					{
						this.curNode.SetValueNode(textNodeType2, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						return true;
					}
					this.stringBuilder.Length = 0;
				}
				else
				{
					if (num > 32)
					{
						this.curNode.SetValueNode(XmlNodeType.Text, this.ps.chars, num2, num3 - num2);
						this.nextParsingFunction = this.parsingFunction;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PartialTextValue;
						return true;
					}
					if (num3 - num2 > 0)
					{
						this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
					}
					bool flag;
					do
					{
						flag = this.ParseText(out num2, out num3, ref num);
						if (num3 - num2 > 0)
						{
							this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
						}
					}
					while (!flag && num <= 32 && this.stringBuilder.Length < 4096);
					XmlNodeType xmlNodeType = (this.stringBuilder.Length < 4096) ? this.GetTextNodeType(num) : XmlNodeType.Text;
					if (xmlNodeType != XmlNodeType.None)
					{
						this.curNode.SetValueNode(xmlNodeType, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						if (!flag)
						{
							this.nextParsingFunction = this.parsingFunction;
							this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PartialTextValue;
						}
						return true;
					}
					this.stringBuilder.Length = 0;
					if (!flag)
					{
						while (!this.ParseText(out num2, out num3, ref num))
						{
						}
					}
				}
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ReportEndEntity)
			{
				this.SetupEndEntityNodeInContent();
				this.parsingFunction = this.nextParsingFunction;
				return true;
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
			{
				this.parsingFunction = this.nextNextParsingFunction;
				this.ParseEntityReference();
				return true;
			}
			return false;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002C508 File Offset: 0x0002A708
		private unsafe bool ParseText(out int startPos, out int endPos, ref int outOrChars)
		{
			char[] chars = this.ps.chars;
			int num = this.ps.charPos;
			int num2 = 0;
			int num3 = -1;
			int num4 = outOrChars;
			char c;
			int num7;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[c = chars[num]] & 64) == 0)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							num++;
							continue;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							break;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
								{
									if (num - this.ps.charPos > 0)
									{
										if (num2 == 0)
										{
											num2 = 1;
											num3 = num;
										}
										else
										{
											this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
											num3 = num - num2;
											num2++;
										}
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
									}
								}
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_367;
								}
								if (!this.ps.eolNormalized)
								{
									chars[num] = '\n';
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c == '&')
							{
								int num6;
								XmlTextReaderImpl.EntityType entityType;
								int num5;
								if ((num5 = this.ParseCharRefInline(num, out num6, out entityType)) > 0)
								{
									if (num2 > 0)
									{
										this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
									}
									num3 = num - num2;
									num2 += num5 - num - num6;
									num = num5;
									if (!this.xmlCharType.IsWhiteSpace(chars[num5 - num6]) || (this.v1Compat && entityType == XmlTextReaderImpl.EntityType.CharacterDec))
									{
										num4 |= 255;
										continue;
									}
									continue;
								}
								else
								{
									if (num > this.ps.charPos)
									{
										goto IL_430;
									}
									switch (this.HandleEntityReference(false, XmlTextReaderImpl.EntityExpandType.All, out num))
									{
									case XmlTextReaderImpl.EntityType.CharacterDec:
										if (!this.v1Compat)
										{
											goto IL_222;
										}
										num4 |= 255;
										break;
									case XmlTextReaderImpl.EntityType.CharacterHex:
									case XmlTextReaderImpl.EntityType.CharacterNamed:
										goto IL_222;
									case XmlTextReaderImpl.EntityType.Expanded:
									case XmlTextReaderImpl.EntityType.Skipped:
									case XmlTextReaderImpl.EntityType.FakeExpanded:
										goto IL_24A;
									case XmlTextReaderImpl.EntityType.Unexpanded:
										goto IL_1F5;
									default:
										goto IL_24A;
									}
									IL_256:
									chars = this.ps.chars;
									continue;
									IL_24A:
									num = this.ps.charPos;
									goto IL_256;
									IL_222:
									if (!this.xmlCharType.IsWhiteSpace(this.ps.chars[num - 1]))
									{
										num4 |= 255;
										goto IL_256;
									}
									goto IL_256;
								}
							}
							break;
						}
					}
					else
					{
						if (c == '<')
						{
							goto IL_430;
						}
						if (c == ']')
						{
							if (this.ps.charsUsed - num >= 3 || this.ps.isEof)
							{
								if (chars[num + 1] == ']' && chars[num + 2] == '>')
								{
									this.Throw(num, "Xml_CDATAEndInText");
								}
								num4 |= 93;
								num++;
								continue;
							}
							goto IL_367;
						}
					}
					if (num != this.ps.charsUsed)
					{
						char c2 = chars[num];
						if (XmlCharType.IsHighSurrogate((int)c2))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_367;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)chars[num]))
							{
								num++;
								num4 |= (int)c2;
								continue;
							}
						}
						num7 = num - this.ps.charPos;
						if (this.ZeroEndingStream(num))
						{
							goto Block_29;
						}
						this.ThrowInvalidChar(this.ps.chars, this.ps.charsUsed, this.ps.charPos + num7);
					}
					IL_367:
					if (num > this.ps.charPos)
					{
						goto IL_430;
					}
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r' && this.ps.chars[this.ps.charPos] != ']')
							{
								this.Throw("Xml_UnexpectedEOF1");
							}
						}
						else
						{
							if (!this.InEntity)
							{
								goto IL_424;
							}
							if (this.HandleEntityEnd(true))
							{
								goto Block_36;
							}
						}
					}
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				else
				{
					num4 |= (int)c;
					num++;
				}
			}
			IL_1F5:
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.EntityReference;
			goto IL_424;
			Block_29:
			chars = this.ps.chars;
			num = this.ps.charPos + num7;
			goto IL_430;
			Block_36:
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ReportEndEntity;
			IL_424:
			startPos = (endPos = num);
			return true;
			IL_430:
			if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
			}
			startPos = this.ps.charPos;
			endPos = num - num2;
			this.ps.charPos = num;
			outOrChars = num4;
			return c == '<';
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002C988 File Offset: 0x0002AB88
		private void FinishPartialValue()
		{
			this.curNode.CopyTo(this.readValueOffset, this.stringBuilder);
			int num = 0;
			int num2;
			int num3;
			while (!this.ParseText(out num2, out num3, ref num))
			{
				this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
			}
			this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
			this.curNode.SetValue(this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002CA18 File Offset: 0x0002AC18
		private void FinishOtherValueIterator()
		{
			switch (this.parsingFunction)
			{
			case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
				break;
			case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
				if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
				{
					this.FinishPartialValue();
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue;
					return;
				}
				if (this.readValueOffset > 0)
				{
					this.curNode.SetValue(this.curNode.StringValue.Substring(this.readValueOffset));
					this.readValueOffset = 0;
					return;
				}
				break;
			case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
			case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
				switch (this.incReadState)
				{
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue:
					if (this.readValueOffset > 0)
					{
						this.curNode.SetValue(this.curNode.StringValue.Substring(this.readValueOffset));
						this.readValueOffset = 0;
						return;
					}
					break;
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue:
					this.FinishPartialValue();
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue;
					return;
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End:
					this.curNode.SetValue(string.Empty);
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002CB04 File Offset: 0x0002AD04
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SkipPartialTextValue()
		{
			int num = 0;
			this.parsingFunction = this.nextParsingFunction;
			int num2;
			int num3;
			while (!this.ParseText(out num2, out num3, ref num))
			{
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002CB2D File Offset: 0x0002AD2D
		private void FinishReadValueChunk()
		{
			this.readValueOffset = 0;
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
			{
				this.SkipPartialTextValue();
				return;
			}
			this.parsingFunction = this.nextParsingFunction;
			this.nextParsingFunction = this.nextNextParsingFunction;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002CB60 File Offset: 0x0002AD60
		private void FinishReadContentAsBinary()
		{
			this.readValueOffset = 0;
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue)
			{
				this.SkipPartialTextValue();
			}
			else
			{
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.nextNextParsingFunction;
			}
			if (this.incReadState != XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End)
			{
				while (this.MoveToNextContentNode(true))
				{
				}
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002CBB4 File Offset: 0x0002ADB4
		private void FinishReadElementContentAsBinary()
		{
			this.FinishReadContentAsBinary();
			if (this.curNode.type != XmlNodeType.EndElement)
			{
				this.Throw("Xml_InvalidNodeType", this.curNode.type.ToString());
			}
			this.outerReader.Read();
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002CC04 File Offset: 0x0002AE04
		private bool ParseRootLevelWhitespace()
		{
			XmlNodeType whitespaceType = this.GetWhitespaceType();
			if (whitespaceType == XmlNodeType.None)
			{
				this.EatWhitespaces(null);
				if (this.ps.chars[this.ps.charPos] == '<' || this.ps.charsUsed - this.ps.charPos == 0 || this.ZeroEndingStream(this.ps.charPos))
				{
					return false;
				}
			}
			else
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				this.EatWhitespaces(this.stringBuilder);
				if (this.ps.chars[this.ps.charPos] == '<' || this.ps.charsUsed - this.ps.charPos == 0 || this.ZeroEndingStream(this.ps.charPos))
				{
					if (this.stringBuilder.Length > 0)
					{
						this.curNode.SetValueNode(whitespaceType, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						return true;
					}
					return false;
				}
			}
			if (this.xmlCharType.IsCharData(this.ps.chars[this.ps.charPos]))
			{
				this.Throw("Xml_InvalidRootData");
			}
			else
			{
				this.ThrowInvalidChar(this.ps.chars, this.ps.charsUsed, this.ps.charPos);
			}
			return false;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002CD74 File Offset: 0x0002AF74
		private void ParseEntityReference()
		{
			this.ps.charPos = this.ps.charPos + 1;
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			this.curNode.SetNamedNode(XmlNodeType.EntityReference, this.ParseEntityName());
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002CDC4 File Offset: 0x0002AFC4
		private XmlTextReaderImpl.EntityType HandleEntityReference(bool isInAttributeValue, XmlTextReaderImpl.EntityExpandType expandType, out int charRefEndPos)
		{
			if (this.ps.charPos + 1 == this.ps.charsUsed && this.ReadData() == 0)
			{
				this.Throw("Xml_UnexpectedEOF1");
			}
			if (this.ps.chars[this.ps.charPos + 1] == '#')
			{
				XmlTextReaderImpl.EntityType result;
				charRefEndPos = this.ParseNumericCharRef(expandType != XmlTextReaderImpl.EntityExpandType.OnlyGeneral, null, out result);
				return result;
			}
			charRefEndPos = this.ParseNamedCharRef(expandType != XmlTextReaderImpl.EntityExpandType.OnlyGeneral, null);
			if (charRefEndPos >= 0)
			{
				return XmlTextReaderImpl.EntityType.CharacterNamed;
			}
			if (expandType == XmlTextReaderImpl.EntityExpandType.OnlyCharacter || (this.entityHandling != EntityHandling.ExpandEntities && (!isInAttributeValue || !this.validatingReaderCompatFlag)))
			{
				return XmlTextReaderImpl.EntityType.Unexpanded;
			}
			this.ps.charPos = this.ps.charPos + 1;
			int linePos = this.ps.LinePos;
			int num;
			try
			{
				num = this.ParseName();
			}
			catch (XmlException)
			{
				this.Throw("Xml_ErrorParsingEntityName", this.ps.LineNo, linePos);
				return XmlTextReaderImpl.EntityType.Skipped;
			}
			if (this.ps.chars[num] != ';')
			{
				this.ThrowUnexpectedToken(num, ";");
			}
			int linePos2 = this.ps.LinePos;
			string name = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			this.ps.charPos = num + 1;
			charRefEndPos = -1;
			XmlTextReaderImpl.EntityType result2 = this.HandleGeneralEntityReference(name, isInAttributeValue, false, linePos2);
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
			return result2;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002CF54 File Offset: 0x0002B154
		private XmlTextReaderImpl.EntityType HandleGeneralEntityReference(string name, bool isInAttributeValue, bool pushFakeEntityIfNullResolver, int entityStartLinePos)
		{
			IDtdEntityInfo dtdEntityInfo = null;
			if (this.dtdInfo == null && this.fragmentParserContext != null && this.fragmentParserContext.HasDtdInfo && this.dtdProcessing == DtdProcessing.Parse)
			{
				this.ParseDtdFromParserContext();
			}
			if (this.dtdInfo == null || (dtdEntityInfo = this.dtdInfo.LookupEntity(name)) == null)
			{
				if (this.disableUndeclaredEntityCheck)
				{
					dtdEntityInfo = new SchemaEntity(new XmlQualifiedName(name), false)
					{
						Text = string.Empty
					};
				}
				else
				{
					this.Throw("Xml_UndeclaredEntity", name, this.ps.LineNo, entityStartLinePos);
				}
			}
			if (dtdEntityInfo.IsUnparsedEntity)
			{
				if (this.disableUndeclaredEntityCheck)
				{
					dtdEntityInfo = new SchemaEntity(new XmlQualifiedName(name), false)
					{
						Text = string.Empty
					};
				}
				else
				{
					this.Throw("Xml_UnparsedEntityRef", name, this.ps.LineNo, entityStartLinePos);
				}
			}
			if (this.standalone && dtdEntityInfo.IsDeclaredInExternal)
			{
				this.Throw("Xml_ExternalEntityInStandAloneDocument", dtdEntityInfo.Name, this.ps.LineNo, entityStartLinePos);
			}
			if (dtdEntityInfo.IsExternal)
			{
				if (isInAttributeValue)
				{
					this.Throw("Xml_ExternalEntityInAttValue", name, this.ps.LineNo, entityStartLinePos);
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				if (this.parsingMode == XmlTextReaderImpl.ParsingMode.SkipContent)
				{
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				if (this.IsResolverNull)
				{
					if (pushFakeEntityIfNullResolver)
					{
						this.PushExternalEntity(dtdEntityInfo);
						this.curNode.entityId = this.ps.entityId;
						return XmlTextReaderImpl.EntityType.FakeExpanded;
					}
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				else
				{
					this.PushExternalEntity(dtdEntityInfo);
					this.curNode.entityId = this.ps.entityId;
					if (!isInAttributeValue || !this.validatingReaderCompatFlag)
					{
						return XmlTextReaderImpl.EntityType.Expanded;
					}
					return XmlTextReaderImpl.EntityType.ExpandedInAttribute;
				}
			}
			else
			{
				if (this.parsingMode == XmlTextReaderImpl.ParsingMode.SkipContent)
				{
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				this.PushInternalEntity(dtdEntityInfo);
				this.curNode.entityId = this.ps.entityId;
				if (!isInAttributeValue || !this.validatingReaderCompatFlag)
				{
					return XmlTextReaderImpl.EntityType.Expanded;
				}
				return XmlTextReaderImpl.EntityType.ExpandedInAttribute;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002D11B File Offset: 0x0002B31B
		private bool InEntity
		{
			get
			{
				return this.parsingStatesStackTop >= 0;
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002D12C File Offset: 0x0002B32C
		private bool HandleEntityEnd(bool checkEntityNesting)
		{
			if (this.parsingStatesStackTop == -1)
			{
				this.Throw("Xml_InternalError");
			}
			if (this.ps.entityResolvedManually)
			{
				this.index--;
				if (checkEntityNesting && this.ps.entityId != this.nodes[this.index].entityId)
				{
					this.Throw("Xml_IncompleteEntity");
				}
				this.lastEntity = this.ps.entity;
				this.PopEntity();
				return true;
			}
			if (checkEntityNesting && this.ps.entityId != this.nodes[this.index].entityId)
			{
				this.Throw("Xml_IncompleteEntity");
			}
			this.PopEntity();
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			return false;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002D204 File Offset: 0x0002B404
		private void SetupEndEntityNodeInContent()
		{
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			this.curNode = this.nodes[this.index];
			this.curNode.SetNamedNode(XmlNodeType.EndEntity, this.lastEntity.Name);
			this.curNode.lineInfo.Set(this.ps.lineNo, this.ps.LinePos - 1);
			if (this.index == 0 && this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ElementContent)
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002D2A0 File Offset: 0x0002B4A0
		private void SetupEndEntityNodeInAttribute()
		{
			this.curNode = this.nodes[this.index + this.attrCount + 1];
			XmlTextReaderImpl.NodeData nodeData = this.curNode;
			nodeData.lineInfo.linePos = nodeData.lineInfo.linePos + this.curNode.localName.Length;
			this.curNode.type = XmlNodeType.EndEntity;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002D2FA File Offset: 0x0002B4FA
		private bool ParsePI()
		{
			return this.ParsePI(null);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002D304 File Offset: 0x0002B504
		private bool ParsePI(StringBuilder piInDtdStringBuilder)
		{
			if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			}
			int num = this.ParseName();
			string text = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			if (string.Compare(text, "xml", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.Throw(text.Equals("xml") ? "Xml_XmlDeclNotFirst" : "Xml_InvalidPIName", text);
			}
			this.ps.charPos = num;
			if (piInDtdStringBuilder == null)
			{
				if (!this.ignorePIs && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
				{
					this.curNode.SetNamedNode(XmlNodeType.ProcessingInstruction, text);
				}
			}
			else
			{
				piInDtdStringBuilder.Append(text);
			}
			char c = this.ps.chars[this.ps.charPos];
			if (this.EatWhitespaces(piInDtdStringBuilder) == 0)
			{
				if (this.ps.charsUsed - this.ps.charPos < 2)
				{
					this.ReadData();
				}
				if (c != '?' || this.ps.chars[this.ps.charPos + 1] != '>')
				{
					this.Throw("Xml_BadNameChar", XmlException.BuildCharExceptionArgs(this.ps.chars, this.ps.charsUsed, this.ps.charPos));
				}
			}
			int num2;
			int num3;
			if (this.ParsePIValue(out num2, out num3))
			{
				if (piInDtdStringBuilder == null)
				{
					if (this.ignorePIs)
					{
						return false;
					}
					if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
					{
						this.curNode.SetValue(this.ps.chars, num2, num3 - num2);
					}
				}
				else
				{
					piInDtdStringBuilder.Append(this.ps.chars, num2, num3 - num2);
				}
			}
			else
			{
				StringBuilder stringBuilder;
				if (piInDtdStringBuilder == null)
				{
					if (this.ignorePIs || this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
					{
						while (!this.ParsePIValue(out num2, out num3))
						{
						}
						return false;
					}
					stringBuilder = this.stringBuilder;
				}
				else
				{
					stringBuilder = piInDtdStringBuilder;
				}
				do
				{
					stringBuilder.Append(this.ps.chars, num2, num3 - num2);
				}
				while (!this.ParsePIValue(out num2, out num3));
				stringBuilder.Append(this.ps.chars, num2, num3 - num2);
				if (piInDtdStringBuilder == null)
				{
					this.curNode.SetValue(this.stringBuilder.ToString());
					this.stringBuilder.Length = 0;
				}
			}
			return true;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002D55C File Offset: 0x0002B75C
		private unsafe bool ParsePIValue(out int outStartPos, out int outEndPos)
		{
			if (this.ps.charsUsed - this.ps.charPos < 2 && this.ReadData() == 0)
			{
				this.Throw(this.ps.charsUsed, "Xml_UnexpectedEOF", "PI");
			}
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = 0;
			int num3 = -1;
			for (;;)
			{
				char c;
				if ((this.xmlCharType.charProperties[c = chars[num]] & 64) == 0 || c == '?')
				{
					char c2 = chars[num];
					if (c2 <= '&')
					{
						switch (c2)
						{
						case '\t':
							break;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							goto IL_1F1;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
								{
									if (num - this.ps.charPos > 0)
									{
										if (num2 == 0)
										{
											num2 = 1;
											num3 = num;
										}
										else
										{
											this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
											num3 = num - num2;
											num2++;
										}
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
									}
								}
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_24C;
								}
								if (!this.ps.eolNormalized)
								{
									chars[num] = '\n';
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c2 != '&')
							{
								goto IL_1F1;
							}
							break;
						}
					}
					else if (c2 != '<')
					{
						if (c2 != '?')
						{
							if (c2 != ']')
							{
								goto IL_1F1;
							}
						}
						else
						{
							if (chars[num + 1] == '>')
							{
								break;
							}
							if (num + 1 != this.ps.charsUsed)
							{
								num++;
								continue;
							}
							goto IL_24C;
						}
					}
					num++;
					continue;
					IL_1F1:
					if (num == this.ps.charsUsed)
					{
						goto IL_24C;
					}
					char ch = chars[num];
					if (XmlCharType.IsHighSurrogate((int)ch))
					{
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_24C;
						}
						num++;
						if (XmlCharType.IsLowSurrogate((int)chars[num]))
						{
							num++;
							continue;
						}
					}
					this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
				}
				else
				{
					num++;
				}
			}
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num + 2;
			return true;
			IL_24C:
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num;
			return false;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002D7EC File Offset: 0x0002B9EC
		private bool ParseComment()
		{
			if (this.ignoreComments)
			{
				XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
				this.ParseCDataOrComment(XmlNodeType.Comment);
				this.parsingMode = parsingMode;
				return false;
			}
			this.ParseCDataOrComment(XmlNodeType.Comment);
			return true;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002D827 File Offset: 0x0002BA27
		private void ParseCData()
		{
			this.ParseCDataOrComment(XmlNodeType.CDATA);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002D830 File Offset: 0x0002BA30
		private void ParseCDataOrComment(XmlNodeType type)
		{
			int num;
			int num2;
			if (this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
			{
				while (!this.ParseCDataOrComment(type, out num, out num2))
				{
				}
				return;
			}
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			if (this.ParseCDataOrComment(type, out num, out num2))
			{
				this.curNode.SetValueNode(type, this.ps.chars, num, num2 - num);
				return;
			}
			do
			{
				this.stringBuilder.Append(this.ps.chars, num, num2 - num);
			}
			while (!this.ParseCDataOrComment(type, out num, out num2));
			this.stringBuilder.Append(this.ps.chars, num, num2 - num);
			this.curNode.SetValueNode(type, this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002D908 File Offset: 0x0002BB08
		private unsafe bool ParseCDataOrComment(XmlNodeType type, out int outStartPos, out int outEndPos)
		{
			if (this.ps.charsUsed - this.ps.charPos < 3 && this.ReadData() == 0)
			{
				this.Throw("Xml_UnexpectedEOF", (type == XmlNodeType.Comment) ? "Comment" : "CDATA");
			}
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = 0;
			int num3 = -1;
			char c = (type == XmlNodeType.Comment) ? '-' : ']';
			for (;;)
			{
				char c2;
				if ((this.xmlCharType.charProperties[c2 = chars[num]] & 64) == 0 || c2 == c)
				{
					if (chars[num] == c)
					{
						if (chars[num + 1] == c)
						{
							if (chars[num + 2] == '>')
							{
								break;
							}
							if (num + 2 == this.ps.charsUsed)
							{
								goto IL_285;
							}
							if (type == XmlNodeType.Comment)
							{
								this.Throw(num, "Xml_InvalidCommentChars");
							}
						}
						else if (num + 1 == this.ps.charsUsed)
						{
							goto IL_285;
						}
						num++;
					}
					else
					{
						char c3 = chars[num];
						if (c3 <= '&')
						{
							switch (c3)
							{
							case '\t':
								break;
							case '\n':
								num++;
								this.OnNewLine(num);
								continue;
							case '\v':
							case '\f':
								goto IL_22F;
							case '\r':
								if (chars[num + 1] == '\n')
								{
									if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
									{
										if (num - this.ps.charPos > 0)
										{
											if (num2 == 0)
											{
												num2 = 1;
												num3 = num;
											}
											else
											{
												this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
												num3 = num - num2;
												num2++;
											}
										}
										else
										{
											this.ps.charPos = this.ps.charPos + 1;
										}
									}
									num += 2;
								}
								else
								{
									if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
									{
										goto IL_285;
									}
									if (!this.ps.eolNormalized)
									{
										chars[num] = '\n';
									}
									num++;
								}
								this.OnNewLine(num);
								continue;
							default:
								if (c3 != '&')
								{
									goto IL_22F;
								}
								break;
							}
						}
						else if (c3 != '<' && c3 != ']')
						{
							goto IL_22F;
						}
						num++;
						continue;
						IL_22F:
						if (num == this.ps.charsUsed)
						{
							goto IL_285;
						}
						char ch = chars[num];
						if (!XmlCharType.IsHighSurrogate((int)ch))
						{
							goto IL_272;
						}
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_285;
						}
						num++;
						if (!XmlCharType.IsLowSurrogate((int)chars[num]))
						{
							goto IL_272;
						}
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num + 3;
			return true;
			IL_272:
			this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
			IL_285:
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num;
			return false;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
		private bool ParseDoctypeDecl()
		{
			if (this.dtdProcessing == DtdProcessing.Prohibit)
			{
				this.ThrowWithoutLineInfo(this.v1Compat ? "Xml_DtdIsProhibited" : "Xml_DtdIsProhibitedEx");
			}
			while (this.ps.charsUsed - this.ps.charPos < 8)
			{
				if (this.ReadData() == 0)
				{
					this.Throw("Xml_UnexpectedEOF", "DOCTYPE");
				}
			}
			if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 7, "DOCTYPE"))
			{
				this.ThrowUnexpectedToken((!this.rootElementParsed && this.dtdInfo == null) ? "DOCTYPE" : "<!--");
			}
			if (!this.xmlCharType.IsWhiteSpace(this.ps.chars[this.ps.charPos + 7]))
			{
				this.ThrowExpectingWhitespace(this.ps.charPos + 7);
			}
			if (this.dtdInfo != null)
			{
				this.Throw(this.ps.charPos - 2, "Xml_MultipleDTDsProvided");
			}
			if (this.rootElementParsed)
			{
				this.Throw(this.ps.charPos - 2, "Xml_DtdAfterRootElement");
			}
			this.ps.charPos = this.ps.charPos + 8;
			this.EatWhitespaces(null);
			if (this.dtdProcessing == DtdProcessing.Parse)
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				this.ParseDtd();
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel;
				return true;
			}
			this.SkipDtd();
			return false;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002DD5C File Offset: 0x0002BF5C
		private void ParseDtd()
		{
			IDtdParser dtdParser = DtdParser.Create();
			this.dtdInfo = dtdParser.ParseInternalDtd(new XmlTextReaderImpl.DtdParserProxy(this), true);
			if ((this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
			this.curNode.SetNamedNode(XmlNodeType.DocumentType, this.dtdInfo.Name.ToString(), string.Empty, null);
			this.curNode.SetValue(this.dtdInfo.InternalDtdSubset);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002DDEC File Offset: 0x0002BFEC
		private void SkipDtd()
		{
			int num;
			int charPos = this.ParseQName(out num);
			this.ps.charPos = charPos;
			this.EatWhitespaces(null);
			if (this.ps.chars[this.ps.charPos] == 'P')
			{
				while (this.ps.charsUsed - this.ps.charPos < 6)
				{
					if (this.ReadData() == 0)
					{
						this.Throw("Xml_UnexpectedEOF1");
					}
				}
				if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 6, "PUBLIC"))
				{
					this.ThrowUnexpectedToken("PUBLIC");
				}
				this.ps.charPos = this.ps.charPos + 6;
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				this.EatWhitespaces(null);
			}
			else if (this.ps.chars[this.ps.charPos] == 'S')
			{
				while (this.ps.charsUsed - this.ps.charPos < 6)
				{
					if (this.ReadData() == 0)
					{
						this.Throw("Xml_UnexpectedEOF1");
					}
				}
				if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 6, "SYSTEM"))
				{
					this.ThrowUnexpectedToken("SYSTEM");
				}
				this.ps.charPos = this.ps.charPos + 6;
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				this.EatWhitespaces(null);
			}
			else if (this.ps.chars[this.ps.charPos] != '[' && this.ps.chars[this.ps.charPos] != '>')
			{
				this.Throw("Xml_ExpectExternalOrClose");
			}
			if (this.ps.chars[this.ps.charPos] == '[')
			{
				this.ps.charPos = this.ps.charPos + 1;
				this.SkipUntil(']', true);
				this.EatWhitespaces(null);
				if (this.ps.chars[this.ps.charPos] != '>')
				{
					this.ThrowUnexpectedToken(">");
				}
			}
			else if (this.ps.chars[this.ps.charPos] == '>')
			{
				this.curNode.SetValue(string.Empty);
			}
			else
			{
				this.Throw("Xml_ExpectSubOrClose");
			}
			this.ps.charPos = this.ps.charPos + 1;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002E090 File Offset: 0x0002C290
		private void SkipPublicOrSystemIdLiteral()
		{
			char c = this.ps.chars[this.ps.charPos];
			if (c != '"' && c != '\'')
			{
				this.ThrowUnexpectedToken("\"", "'");
			}
			this.ps.charPos = this.ps.charPos + 1;
			this.SkipUntil(c, false);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		private unsafe void SkipUntil(char stopChar, bool recognizeLiterals)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			char c = '"';
			char[] chars = this.ps.chars;
			int num = this.ps.charPos;
			for (;;)
			{
				char c2;
				if ((this.xmlCharType.charProperties[c2 = chars[num]] & 128) == 0 || chars[num] == stopChar || c2 == '-' || c2 == '?')
				{
					if (c2 == stopChar && !flag)
					{
						break;
					}
					this.ps.charPos = num;
					if (c2 <= '&')
					{
						switch (c2)
						{
						case '\t':
							break;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							goto IL_2D2;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_334;
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c2 == '"')
							{
								goto IL_2AD;
							}
							if (c2 != '&')
							{
								goto IL_2D2;
							}
							break;
						}
					}
					else if (c2 <= '-')
					{
						if (c2 == '\'')
						{
							goto IL_2AD;
						}
						if (c2 != '-')
						{
							goto IL_2D2;
						}
						if (flag2)
						{
							if (num + 2 >= this.ps.charsUsed && !this.ps.isEof)
							{
								goto IL_334;
							}
							if (chars[num + 1] == '-' && chars[num + 2] == '>')
							{
								flag2 = false;
								num += 2;
								continue;
							}
						}
						num++;
						continue;
					}
					else
					{
						switch (c2)
						{
						case '<':
							if (chars[num + 1] == '?')
							{
								if (recognizeLiterals && !flag && !flag2)
								{
									flag3 = true;
									num += 2;
									continue;
								}
							}
							else if (chars[num + 1] == '!')
							{
								if (num + 3 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_334;
								}
								if (chars[num + 2] == '-' && chars[num + 3] == '-' && recognizeLiterals && !flag && !flag3)
								{
									flag2 = true;
									num += 4;
									continue;
								}
							}
							else if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
							{
								goto IL_334;
							}
							num++;
							continue;
						case '=':
							goto IL_2D2;
						case '>':
							break;
						case '?':
							if (flag3)
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_334;
								}
								if (chars[num + 1] == '>')
								{
									flag3 = false;
									num++;
									continue;
								}
							}
							num++;
							continue;
						default:
							if (c2 != ']')
							{
								goto IL_2D2;
							}
							break;
						}
					}
					num++;
					continue;
					IL_2AD:
					if (flag)
					{
						if (c == c2)
						{
							flag = false;
						}
					}
					else if (recognizeLiterals && !flag2 && !flag3)
					{
						flag = true;
						c = c2;
					}
					num++;
					continue;
					IL_2D2:
					if (num != this.ps.charsUsed)
					{
						char ch = chars[num];
						if (XmlCharType.IsHighSurrogate((int)ch))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_334;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)chars[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
					}
					IL_334:
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r')
							{
								this.Throw("Xml_UnexpectedEOF1");
							}
						}
						else
						{
							this.Throw("Xml_UnexpectedEOF1");
						}
					}
					chars = this.ps.chars;
					num = this.ps.charPos;
				}
				else
				{
					num++;
				}
			}
			this.ps.charPos = num + 1;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002E49C File Offset: 0x0002C69C
		private int EatWhitespaces(StringBuilder sb)
		{
			int num = this.ps.charPos;
			int num2 = 0;
			char[] chars = this.ps.chars;
			for (;;)
			{
				char c = chars[num];
				switch (c)
				{
				case '\t':
					break;
				case '\n':
					num++;
					this.OnNewLine(num);
					continue;
				case '\v':
				case '\f':
					goto IL_FE;
				case '\r':
					if (chars[num + 1] == '\n')
					{
						int num3 = num - this.ps.charPos;
						if (sb != null && !this.ps.eolNormalized)
						{
							if (num3 > 0)
							{
								sb.Append(chars, this.ps.charPos, num3);
								num2 += num3;
							}
							this.ps.charPos = num + 1;
						}
						num += 2;
					}
					else
					{
						if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
						{
							goto IL_155;
						}
						if (!this.ps.eolNormalized)
						{
							chars[num] = '\n';
						}
						num++;
					}
					this.OnNewLine(num);
					continue;
				default:
					if (c != ' ')
					{
						goto IL_FE;
					}
					break;
				}
				num++;
				continue;
				IL_155:
				int num4 = num - this.ps.charPos;
				if (num4 > 0)
				{
					if (sb != null)
					{
						sb.Append(this.ps.chars, this.ps.charPos, num4);
					}
					this.ps.charPos = num;
					num2 += num4;
				}
				if (this.ReadData() == 0)
				{
					if (this.ps.charsUsed - this.ps.charPos == 0)
					{
						return num2;
					}
					if (this.ps.chars[this.ps.charPos] != '\r')
					{
						this.Throw("Xml_UnexpectedEOF1");
					}
				}
				num = this.ps.charPos;
				chars = this.ps.chars;
				continue;
				IL_FE:
				if (num != this.ps.charsUsed)
				{
					break;
				}
				goto IL_155;
			}
			int num5 = num - this.ps.charPos;
			if (num5 > 0)
			{
				if (sb != null)
				{
					sb.Append(this.ps.chars, this.ps.charPos, num5);
				}
				this.ps.charPos = num;
				num2 += num5;
			}
			return num2;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002E6A6 File Offset: 0x0002C8A6
		private int ParseCharRefInline(int startPos, out int charCount, out XmlTextReaderImpl.EntityType entityType)
		{
			if (this.ps.chars[startPos + 1] == '#')
			{
				return this.ParseNumericCharRefInline(startPos, true, null, out charCount, out entityType);
			}
			charCount = 1;
			entityType = XmlTextReaderImpl.EntityType.CharacterNamed;
			return this.ParseNamedCharRefInline(startPos, true, null);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002E6D8 File Offset: 0x0002C8D8
		private int ParseNumericCharRef(bool expand, StringBuilder internalSubsetBuilder, out XmlTextReaderImpl.EntityType entityType)
		{
			int num3;
			int num;
			for (;;)
			{
				int num2;
				num = (num2 = this.ParseNumericCharRefInline(this.ps.charPos, expand, internalSubsetBuilder, out num3, out entityType));
				if (num2 != -2)
				{
					break;
				}
				if (this.ReadData() == 0)
				{
					this.Throw("Xml_UnexpectedEOF");
				}
			}
			if (expand)
			{
				this.ps.charPos = num - num3;
			}
			return num;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002E72C File Offset: 0x0002C92C
		private int ParseNumericCharRefInline(int startPos, bool expand, StringBuilder internalSubsetBuilder, out int charCount, out XmlTextReaderImpl.EntityType entityType)
		{
			int num = 0;
			string res = null;
			char[] chars = this.ps.chars;
			int num2 = startPos + 2;
			charCount = 0;
			int num3 = 0;
			try
			{
				if (chars[num2] == 'x')
				{
					num2++;
					num3 = num2;
					res = "Xml_BadHexEntity";
					for (;;)
					{
						char c = chars[num2];
						checked
						{
							if (c >= '0' && c <= '9')
							{
								num = num * 16 + (int)c - 48;
							}
							else if (c >= 'a' && c <= 'f')
							{
								num = num * 16 + 10 + (int)c - 97;
							}
							else
							{
								if (c < 'A' || c > 'F')
								{
									break;
								}
								num = num * 16 + 10 + (int)c - 65;
							}
						}
						num2++;
					}
					entityType = XmlTextReaderImpl.EntityType.CharacterHex;
				}
				else
				{
					if (num2 >= this.ps.charsUsed)
					{
						entityType = XmlTextReaderImpl.EntityType.Skipped;
						return -2;
					}
					num3 = num2;
					res = "Xml_BadDecimalEntity";
					while (chars[num2] >= '0' && chars[num2] <= '9')
					{
						num = checked(num * 10 + (int)chars[num2] - 48);
						num2++;
					}
					entityType = XmlTextReaderImpl.EntityType.CharacterDec;
				}
			}
			catch (OverflowException innerException)
			{
				this.ps.charPos = num2;
				entityType = XmlTextReaderImpl.EntityType.Skipped;
				this.Throw("Xml_CharEntityOverflow", null, innerException);
			}
			if (chars[num2] != ';' || num3 == num2)
			{
				if (num2 == this.ps.charsUsed)
				{
					return -2;
				}
				this.Throw(num2, res);
			}
			if (num <= 65535)
			{
				char c2 = (char)num;
				if (!this.xmlCharType.IsCharData(c2) && ((this.v1Compat && this.normalize) || (!this.v1Compat && this.checkCharacters)))
				{
					this.Throw((this.ps.chars[startPos + 2] == 'x') ? (startPos + 3) : (startPos + 2), "Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(c2, '\0'));
				}
				if (expand)
				{
					if (internalSubsetBuilder != null)
					{
						internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num2 - this.ps.charPos + 1);
					}
					chars[num2] = c2;
				}
				charCount = 1;
				return num2 + 1;
			}
			char c3;
			char c4;
			XmlCharType.SplitSurrogateChar(num, out c3, out c4);
			if (this.normalize && (!XmlCharType.IsHighSurrogate((int)c4) || !XmlCharType.IsLowSurrogate((int)c3)))
			{
				this.Throw((this.ps.chars[startPos + 2] == 'x') ? (startPos + 3) : (startPos + 2), "Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(c4, c3));
			}
			if (expand)
			{
				if (internalSubsetBuilder != null)
				{
					internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num2 - this.ps.charPos + 1);
				}
				chars[num2 - 1] = c4;
				chars[num2] = c3;
			}
			charCount = 2;
			return num2 + 1;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002E9B4 File Offset: 0x0002CBB4
		private int ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
		{
			int num2;
			int num;
			for (;;)
			{
				num = (num2 = this.ParseNamedCharRefInline(this.ps.charPos, expand, internalSubsetBuilder));
				if (num2 != -2)
				{
					break;
				}
				if (this.ReadData() == 0)
				{
					return -1;
				}
			}
			if (num2 == -1)
			{
				return -1;
			}
			if (expand)
			{
				this.ps.charPos = num - 1;
			}
			return num;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002EA00 File Offset: 0x0002CC00
		private int ParseNamedCharRefInline(int startPos, bool expand, StringBuilder internalSubsetBuilder)
		{
			int num = startPos + 1;
			char[] chars = this.ps.chars;
			char c = chars[num];
			if (c <= 'g')
			{
				if (c != 'a')
				{
					if (c == 'g')
					{
						if (this.ps.charsUsed - num < 3)
						{
							return -2;
						}
						if (chars[num + 1] == 't' && chars[num + 2] == ';')
						{
							num += 3;
							char c2 = '>';
							goto IL_175;
						}
						return -1;
					}
				}
				else
				{
					num++;
					if (chars[num] == 'm')
					{
						if (this.ps.charsUsed - num < 3)
						{
							return -2;
						}
						if (chars[num + 1] == 'p' && chars[num + 2] == ';')
						{
							num += 3;
							char c2 = '&';
							goto IL_175;
						}
						return -1;
					}
					else if (chars[num] == 'p')
					{
						if (this.ps.charsUsed - num < 4)
						{
							return -2;
						}
						if (chars[num + 1] == 'o' && chars[num + 2] == 's' && chars[num + 3] == ';')
						{
							num += 4;
							char c2 = '\'';
							goto IL_175;
						}
						return -1;
					}
					else
					{
						if (num < this.ps.charsUsed)
						{
							return -1;
						}
						return -2;
					}
				}
			}
			else if (c != 'l')
			{
				if (c == 'q')
				{
					if (this.ps.charsUsed - num < 5)
					{
						return -2;
					}
					if (chars[num + 1] == 'u' && chars[num + 2] == 'o' && chars[num + 3] == 't' && chars[num + 4] == ';')
					{
						num += 5;
						char c2 = '"';
						goto IL_175;
					}
					return -1;
				}
			}
			else
			{
				if (this.ps.charsUsed - num < 3)
				{
					return -2;
				}
				if (chars[num + 1] == 't' && chars[num + 2] == ';')
				{
					num += 3;
					char c2 = '<';
					goto IL_175;
				}
				return -1;
			}
			return -1;
			IL_175:
			if (expand)
			{
				if (internalSubsetBuilder != null)
				{
					internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
				}
				char c2;
				this.ps.chars[num - 1] = c2;
			}
			return num;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002EBC4 File Offset: 0x0002CDC4
		private int ParseName()
		{
			int num;
			return this.ParseQName(false, 0, out num);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002EBDB File Offset: 0x0002CDDB
		private int ParseQName(out int colonPos)
		{
			return this.ParseQName(true, 0, out colonPos);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
		private unsafe int ParseQName(bool isQName, int startOffset, out int colonPos)
		{
			int num = -1;
			int num2 = this.ps.charPos + startOffset;
			for (;;)
			{
				char[] chars = this.ps.chars;
				if ((this.xmlCharType.charProperties[chars[num2]] & 4) != 0)
				{
					num2++;
				}
				else
				{
					if (num2 + 1 >= this.ps.charsUsed)
					{
						if (this.ReadDataInName(ref num2))
						{
							continue;
						}
						this.Throw(num2, "Xml_UnexpectedEOF", "Name");
					}
					if (chars[num2] != ':' || this.supportNamespaces)
					{
						this.Throw(num2, "Xml_BadStartNameChar", XmlException.BuildCharExceptionArgs(chars, this.ps.charsUsed, num2));
					}
				}
				for (;;)
				{
					if ((this.xmlCharType.charProperties[chars[num2]] & 8) != 0)
					{
						num2++;
					}
					else if (chars[num2] == ':')
					{
						if (this.supportNamespaces)
						{
							break;
						}
						num = num2 - this.ps.charPos;
						num2++;
					}
					else
					{
						if (num2 != this.ps.charsUsed)
						{
							goto IL_137;
						}
						if (!this.ReadDataInName(ref num2))
						{
							goto IL_126;
						}
						chars = this.ps.chars;
					}
				}
				if (num != -1 || !isQName)
				{
					this.Throw(num2, "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(':', '\0'));
				}
				num = num2 - this.ps.charPos;
				num2++;
			}
			IL_126:
			this.Throw(num2, "Xml_UnexpectedEOF", "Name");
			IL_137:
			colonPos = ((num == -1) ? -1 : (this.ps.charPos + num));
			return num2;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002ED44 File Offset: 0x0002CF44
		private bool ReadDataInName(ref int pos)
		{
			int num = pos - this.ps.charPos;
			bool result = this.ReadData() != 0;
			pos = this.ps.charPos + num;
			return result;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002ED7C File Offset: 0x0002CF7C
		private string ParseEntityName()
		{
			int num;
			try
			{
				num = this.ParseName();
			}
			catch (XmlException)
			{
				this.Throw("Xml_ErrorParsingEntityName");
				return null;
			}
			if (this.ps.chars[num] != ';')
			{
				this.Throw("Xml_ErrorParsingEntityName");
			}
			string result = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			this.ps.charPos = num + 1;
			return result;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002EE10 File Offset: 0x0002D010
		private XmlTextReaderImpl.NodeData AddNode(int nodeIndex, int nodeDepth)
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[nodeIndex];
			if (nodeData != null)
			{
				nodeData.depth = nodeDepth;
				return nodeData;
			}
			return this.AllocNode(nodeIndex, nodeDepth);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002EE3C File Offset: 0x0002D03C
		private XmlTextReaderImpl.NodeData AllocNode(int nodeIndex, int nodeDepth)
		{
			if (nodeIndex >= this.nodes.Length - 1)
			{
				XmlTextReaderImpl.NodeData[] destinationArray = new XmlTextReaderImpl.NodeData[this.nodes.Length * 2];
				Array.Copy(this.nodes, 0, destinationArray, 0, this.nodes.Length);
				this.nodes = destinationArray;
			}
			XmlTextReaderImpl.NodeData nodeData = this.nodes[nodeIndex];
			if (nodeData == null)
			{
				nodeData = new XmlTextReaderImpl.NodeData();
				this.nodes[nodeIndex] = nodeData;
			}
			nodeData.depth = nodeDepth;
			return nodeData;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002EEA8 File Offset: 0x0002D0A8
		private XmlTextReaderImpl.NodeData AddAttributeNoChecks(string name, int attrDepth)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, attrDepth);
			nodeData.SetNamedNode(XmlNodeType.Attribute, this.nameTable.Add(name));
			this.attrCount++;
			return nodeData;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002EEF0 File Offset: 0x0002D0F0
		private XmlTextReaderImpl.NodeData AddAttribute(int endNamePos, int colonPos)
		{
			if (colonPos == -1 || !this.supportNamespaces)
			{
				string text = this.nameTable.Add(this.ps.chars, this.ps.charPos, endNamePos - this.ps.charPos);
				return this.AddAttribute(text, string.Empty, text);
			}
			this.attrNeedNamespaceLookup = true;
			int charPos = this.ps.charPos;
			int num = colonPos - charPos;
			if (num == this.lastPrefix.Length && XmlConvert.StrEqual(this.ps.chars, charPos, num, this.lastPrefix))
			{
				return this.AddAttribute(this.nameTable.Add(this.ps.chars, colonPos + 1, endNamePos - colonPos - 1), this.lastPrefix, null);
			}
			string prefix = this.nameTable.Add(this.ps.chars, charPos, num);
			this.lastPrefix = prefix;
			return this.AddAttribute(this.nameTable.Add(this.ps.chars, colonPos + 1, endNamePos - colonPos - 1), prefix, null);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002EFF8 File Offset: 0x0002D1F8
		private XmlTextReaderImpl.NodeData AddAttribute(string localName, string prefix, string nameWPrefix)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, this.index + 1);
			nodeData.SetNamedNode(XmlNodeType.Attribute, localName, prefix, nameWPrefix);
			int num = 1 << (int)localName[0];
			if ((this.attrHashtable & num) == 0)
			{
				this.attrHashtable |= num;
			}
			else if (this.attrDuplWalkCount < 64)
			{
				this.attrDuplWalkCount++;
				for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
				{
					XmlTextReaderImpl.NodeData nodeData2 = this.nodes[i];
					if (Ref.Equal(nodeData2.localName, nodeData.localName))
					{
						this.attrDuplWalkCount = 64;
						break;
					}
				}
			}
			this.attrCount++;
			return nodeData;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002F0C5 File Offset: 0x0002D2C5
		private void PopElementContext()
		{
			this.namespaceManager.PopScope();
			if (this.curNode.xmlContextPushed)
			{
				this.PopXmlContext();
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002F0E6 File Offset: 0x0002D2E6
		private void OnNewLine(int pos)
		{
			this.ps.lineNo = this.ps.lineNo + 1;
			this.ps.lineStartPos = pos - 1;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002F108 File Offset: 0x0002D308
		private void OnEof()
		{
			this.curNode = this.nodes[0];
			this.curNode.Clear(XmlNodeType.None);
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.Eof;
			this.readState = ReadState.EndOfFile;
			this.reportedEncoding = null;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002F168 File Offset: 0x0002D368
		private string LookupNamespace(XmlTextReaderImpl.NodeData node)
		{
			string text = this.namespaceManager.LookupNamespace(node.prefix);
			if (text != null)
			{
				return text;
			}
			this.Throw("Xml_UnknownNs", node.prefix, node.LineNo, node.LinePos);
			return null;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002F1AC File Offset: 0x0002D3AC
		private void AddNamespace(string prefix, string uri, XmlTextReaderImpl.NodeData attr)
		{
			if (uri == "http://www.w3.org/2000/xmlns/")
			{
				if (Ref.Equal(prefix, this.XmlNs))
				{
					this.Throw("Xml_XmlnsPrefix", attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
				}
				else
				{
					this.Throw("Xml_NamespaceDeclXmlXmlns", prefix, attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
				}
			}
			else if (uri == "http://www.w3.org/XML/1998/namespace" && !Ref.Equal(prefix, this.Xml) && !this.v1Compat)
			{
				this.Throw("Xml_NamespaceDeclXmlXmlns", prefix, attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
			}
			if (uri.Length == 0 && prefix.Length > 0)
			{
				this.Throw("Xml_BadNamespaceDecl", attr.lineInfo.lineNo, attr.lineInfo.linePos);
			}
			try
			{
				this.namespaceManager.AddNamespace(prefix, uri);
			}
			catch (ArgumentException e)
			{
				this.ReThrow(e, attr.lineInfo.lineNo, attr.lineInfo.linePos);
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002F2D4 File Offset: 0x0002D4D4
		private void ResetAttributes()
		{
			if (this.fullAttrCleanup)
			{
				this.FullAttributeCleanup();
			}
			this.curAttrIndex = -1;
			this.attrCount = 0;
			this.attrHashtable = 0;
			this.attrDuplWalkCount = 0;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002F300 File Offset: 0x0002D500
		private void FullAttributeCleanup()
		{
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				nodeData.nextAttrValueChunk = null;
				nodeData.IsDefaultAttribute = false;
			}
			this.fullAttrCleanup = false;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002F34C File Offset: 0x0002D54C
		private void PushXmlContext()
		{
			this.xmlContext = new XmlTextReaderImpl.XmlContext(this.xmlContext);
			this.curNode.xmlContextPushed = true;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002F36B File Offset: 0x0002D56B
		private void PopXmlContext()
		{
			this.xmlContext = this.xmlContext.previousContext;
			this.curNode.xmlContextPushed = false;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002F38A File Offset: 0x0002D58A
		private XmlNodeType GetWhitespaceType()
		{
			if (this.whitespaceHandling != WhitespaceHandling.None)
			{
				if (this.xmlContext.xmlSpace == XmlSpace.Preserve)
				{
					return XmlNodeType.SignificantWhitespace;
				}
				if (this.whitespaceHandling == WhitespaceHandling.All)
				{
					return XmlNodeType.Whitespace;
				}
			}
			return XmlNodeType.None;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002F3B2 File Offset: 0x0002D5B2
		private XmlNodeType GetTextNodeType(int orChars)
		{
			if (orChars > 32)
			{
				return XmlNodeType.Text;
			}
			return this.GetWhitespaceType();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002F3C4 File Offset: 0x0002D5C4
		private void PushExternalEntityOrSubset(string publicId, string systemId, Uri baseUri, string entityName)
		{
			Uri uri;
			if (!string.IsNullOrEmpty(publicId))
			{
				try
				{
					uri = this.xmlResolver.ResolveUri(baseUri, publicId);
					if (this.OpenAndPush(uri))
					{
						return;
					}
				}
				catch (Exception)
				{
				}
			}
			uri = this.xmlResolver.ResolveUri(baseUri, systemId);
			try
			{
				if (this.OpenAndPush(uri))
				{
					return;
				}
			}
			catch (Exception ex)
			{
				if (this.v1Compat)
				{
					throw;
				}
				string message = ex.Message;
				this.Throw(new XmlException((entityName == null) ? "Xml_ErrorOpeningExternalDtd" : "Xml_ErrorOpeningExternalEntity", new string[]
				{
					uri.ToString(),
					message
				}, ex, 0, 0));
			}
			if (entityName == null)
			{
				this.ThrowWithoutLineInfo("Xml_CannotResolveExternalSubset", new string[]
				{
					(publicId != null) ? publicId : string.Empty,
					systemId
				}, null);
				return;
			}
			this.Throw((this.dtdProcessing == DtdProcessing.Ignore) ? "Xml_CannotResolveEntityDtdIgnored" : "Xml_CannotResolveEntity", entityName);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002F4C0 File Offset: 0x0002D6C0
		private bool OpenAndPush(Uri uri)
		{
			if (this.xmlResolver.SupportsType(uri, typeof(TextReader)))
			{
				TextReader textReader = (TextReader)this.xmlResolver.GetEntity(uri, null, typeof(TextReader));
				if (textReader == null)
				{
					return false;
				}
				this.PushParsingState();
				this.InitTextReaderInput(uri.ToString(), uri, textReader);
			}
			else
			{
				Stream stream = (Stream)this.xmlResolver.GetEntity(uri, null, typeof(Stream));
				if (stream == null)
				{
					return false;
				}
				this.PushParsingState();
				this.InitStreamInput(uri, stream, null);
			}
			return true;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002F550 File Offset: 0x0002D750
		private bool PushExternalEntity(IDtdEntityInfo entity)
		{
			if (!this.IsResolverNull)
			{
				Uri baseUri = null;
				if (!string.IsNullOrEmpty(entity.BaseUriString))
				{
					baseUri = this.xmlResolver.ResolveUri(null, entity.BaseUriString);
				}
				this.PushExternalEntityOrSubset(entity.PublicId, entity.SystemId, baseUri, entity.Name);
				this.RegisterEntity(entity);
				int charPos = this.ps.charPos;
				if (this.v1Compat)
				{
					this.EatWhitespaces(null);
				}
				if (!this.ParseXmlDeclaration(true))
				{
					this.ps.charPos = charPos;
				}
				return true;
			}
			Encoding encoding = this.ps.encoding;
			this.PushParsingState();
			this.InitStringInput(entity.SystemId, encoding, string.Empty);
			this.RegisterEntity(entity);
			this.RegisterConsumedCharacters(0L, true);
			return false;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002F610 File Offset: 0x0002D810
		private void PushInternalEntity(IDtdEntityInfo entity)
		{
			Encoding encoding = this.ps.encoding;
			this.PushParsingState();
			this.InitStringInput((entity.DeclaredUriString != null) ? entity.DeclaredUriString : string.Empty, encoding, entity.Text ?? string.Empty);
			this.RegisterEntity(entity);
			this.ps.lineNo = entity.LineNumber;
			this.ps.lineStartPos = -entity.LinePosition - 1;
			this.ps.eolNormalized = true;
			this.RegisterConsumedCharacters((long)entity.Text.Length, true);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002F6A8 File Offset: 0x0002D8A8
		private void PopEntity()
		{
			if (this.ps.stream != null)
			{
				this.ps.stream.Close();
			}
			this.UnregisterEntity();
			this.PopParsingState();
			this.curNode.entityId = this.ps.entityId;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002F6F4 File Offset: 0x0002D8F4
		private void RegisterEntity(IDtdEntityInfo entity)
		{
			if (this.currentEntities != null && this.currentEntities.ContainsKey(entity))
			{
				this.Throw(entity.IsParameterEntity ? "Xml_RecursiveParEntity" : "Xml_RecursiveGenEntity", entity.Name, this.parsingStatesStack[this.parsingStatesStackTop].LineNo, this.parsingStatesStack[this.parsingStatesStackTop].LinePos);
			}
			this.ps.entity = entity;
			int num = this.nextEntityId;
			this.nextEntityId = num + 1;
			this.ps.entityId = num;
			if (entity != null)
			{
				if (this.currentEntities == null)
				{
					this.currentEntities = new Dictionary<IDtdEntityInfo, IDtdEntityInfo>();
				}
				this.currentEntities.Add(entity, entity);
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002F7AE File Offset: 0x0002D9AE
		private void UnregisterEntity()
		{
			if (this.ps.entity != null)
			{
				this.currentEntities.Remove(this.ps.entity);
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002F7D4 File Offset: 0x0002D9D4
		private void PushParsingState()
		{
			if (this.parsingStatesStack == null)
			{
				this.parsingStatesStack = new XmlTextReaderImpl.ParsingState[2];
			}
			else if (this.parsingStatesStackTop + 1 == this.parsingStatesStack.Length)
			{
				XmlTextReaderImpl.ParsingState[] destinationArray = new XmlTextReaderImpl.ParsingState[this.parsingStatesStack.Length * 2];
				Array.Copy(this.parsingStatesStack, 0, destinationArray, 0, this.parsingStatesStack.Length);
				this.parsingStatesStack = destinationArray;
			}
			this.parsingStatesStackTop++;
			this.parsingStatesStack[this.parsingStatesStackTop] = this.ps;
			this.ps.Clear();
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002F868 File Offset: 0x0002DA68
		private void PopParsingState()
		{
			this.ps.Close(true);
			XmlTextReaderImpl.ParsingState[] array = this.parsingStatesStack;
			int num = this.parsingStatesStackTop;
			this.parsingStatesStackTop = num - 1;
			this.ps = array[num];
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002F8A4 File Offset: 0x0002DAA4
		private void InitIncrementalRead(IncrementalReadDecoder decoder)
		{
			this.ResetAttributes();
			decoder.Reset();
			this.incReadDecoder = decoder;
			this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
			this.incReadDepth = 1;
			this.incReadLeftStartPos = this.ps.charPos;
			this.incReadLeftEndPos = this.ps.charPos;
			this.incReadLineInfo.Set(this.ps.LineNo, this.ps.LinePos);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InIncrementalRead;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002F920 File Offset: 0x0002DB20
		private int IncrementalRead(Array array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException((this.incReadDecoder is IncrementalReadCharsDecoder) ? "buffer" : "array");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException((this.incReadDecoder is IncrementalReadCharsDecoder) ? "count" : "len");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException((this.incReadDecoder is IncrementalReadCharsDecoder) ? "index" : "offset");
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException((this.incReadDecoder is IncrementalReadCharsDecoder) ? "count" : "len");
			}
			if (count == 0)
			{
				return 0;
			}
			this.curNode.lineInfo = this.incReadLineInfo;
			this.incReadDecoder.SetNextOutputBuffer(array, index, count);
			this.IncrementalRead();
			return this.incReadDecoder.DecodedCount;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
		private unsafe int IncrementalRead()
		{
			int num = 0;
			int num3;
			int num4;
			int num5;
			int num7;
			for (;;)
			{
				int num2 = this.incReadLeftEndPos - this.incReadLeftStartPos;
				if (num2 > 0)
				{
					try
					{
						num3 = this.incReadDecoder.Decode(this.ps.chars, this.incReadLeftStartPos, num2);
					}
					catch (XmlException e)
					{
						this.ReThrow(e, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						return 0;
					}
					if (num3 < num2)
					{
						break;
					}
					this.incReadLeftStartPos = 0;
					this.incReadLeftEndPos = 0;
					this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num3;
					if (this.incReadDecoder.IsFull)
					{
						return num3;
					}
				}
				num4 = 0;
				num5 = 0;
				int num10;
				for (;;)
				{
					switch (this.incReadState)
					{
					case XmlTextReaderImpl.IncrementalReadState.Text:
					case XmlTextReaderImpl.IncrementalReadState.StartTag:
					case XmlTextReaderImpl.IncrementalReadState.Attributes:
					case XmlTextReaderImpl.IncrementalReadState.AttributeValue:
						goto IL_1D7;
					case XmlTextReaderImpl.IncrementalReadState.PI:
						if (this.ParsePIValue(out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 2;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.CDATA:
						if (this.ParseCDataOrComment(XmlNodeType.CDATA, out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 3;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.Comment:
						if (this.ParseCDataOrComment(XmlNodeType.Comment, out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 3;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.ReadData:
						if (this.ReadData() == 0)
						{
							this.ThrowUnclosedElements();
						}
						this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						num4 = this.ps.charPos;
						num5 = num4;
						goto IL_1D7;
					case XmlTextReaderImpl.IncrementalReadState.EndElement:
						goto IL_17A;
					case XmlTextReaderImpl.IncrementalReadState.End:
						return num;
					default:
						goto IL_1D7;
					}
					IL_6A6:
					int num6 = num5 - num4;
					if (num6 <= 0)
					{
						continue;
					}
					try
					{
						num7 = this.incReadDecoder.Decode(this.ps.chars, num4, num6);
					}
					catch (XmlException e2)
					{
						this.ReThrow(e2, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						return 0;
					}
					num += num7;
					if (this.incReadDecoder.IsFull)
					{
						goto Block_54;
					}
					continue;
					IL_1D7:
					char[] chars = this.ps.chars;
					num4 = this.ps.charPos;
					num5 = num4;
					int num8;
					for (;;)
					{
						this.incReadLineInfo.Set(this.ps.LineNo, this.ps.LinePos);
						if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
						{
							char c;
							while ((this.xmlCharType.charProperties[c = chars[num5]] & 128) != 0)
							{
								if (c == '/')
								{
									break;
								}
								num5++;
							}
						}
						else
						{
							while ((this.xmlCharType.charProperties[chars[num5]] & 128) != 0)
							{
								num5++;
							}
						}
						if (chars[num5] == '&' || chars[num5] == '\t')
						{
							num5++;
						}
						else
						{
							if (num5 - num4 > 0)
							{
								break;
							}
							char c2 = chars[num5];
							if (c2 <= '"')
							{
								if (c2 == '\n')
								{
									num5++;
									this.OnNewLine(num5);
									continue;
								}
								if (c2 == '\r')
								{
									if (chars[num5 + 1] == '\n')
									{
										num5 += 2;
									}
									else
									{
										if (num5 + 1 >= this.ps.charsUsed)
										{
											goto IL_693;
										}
										num5++;
									}
									this.OnNewLine(num5);
									continue;
								}
								if (c2 != '"')
								{
									goto IL_67C;
								}
							}
							else if (c2 <= '/')
							{
								if (c2 != '\'')
								{
									if (c2 != '/')
									{
										goto IL_67C;
									}
									if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
									{
										if (this.ps.charsUsed - num5 < 2)
										{
											goto IL_693;
										}
										if (chars[num5 + 1] == '>')
										{
											this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
											this.incReadDepth--;
										}
									}
									num5++;
									continue;
								}
							}
							else if (c2 != '<')
							{
								if (c2 != '>')
								{
									goto IL_67C;
								}
								if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
								{
									this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
								}
								num5++;
								continue;
							}
							else
							{
								if (this.incReadState != XmlTextReaderImpl.IncrementalReadState.Text)
								{
									num5++;
									continue;
								}
								if (this.ps.charsUsed - num5 < 2)
								{
									goto IL_693;
								}
								char c3 = chars[num5 + 1];
								if (c3 != '!')
								{
									if (c3 != '/')
									{
										if (c3 == '?')
										{
											goto Block_31;
										}
										int num9;
										num8 = this.ParseQName(true, 1, out num9);
										if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos + 1, num8 - this.ps.charPos - 1, this.curNode.localName) && (this.ps.chars[num8] == '>' || this.ps.chars[num8] == '/' || this.xmlCharType.IsWhiteSpace(this.ps.chars[num8])))
										{
											goto IL_596;
										}
										num5 = num8;
										num4 = this.ps.charPos;
										chars = this.ps.chars;
										continue;
									}
									else
									{
										int num11;
										num10 = this.ParseQName(true, 2, out num11);
										if (!XmlConvert.StrEqual(chars, this.ps.charPos + 2, num10 - this.ps.charPos - 2, this.curNode.GetNameWPrefix(this.nameTable)) || (this.ps.chars[num10] != '>' && !this.xmlCharType.IsWhiteSpace(this.ps.chars[num10])))
										{
											num5 = num10;
											num4 = this.ps.charPos;
											chars = this.ps.chars;
											continue;
										}
										int num12 = this.incReadDepth - 1;
										this.incReadDepth = num12;
										if (num12 > 0)
										{
											num5 = num10 + 1;
											continue;
										}
										goto IL_47E;
									}
								}
								else
								{
									if (this.ps.charsUsed - num5 < 4)
									{
										goto IL_693;
									}
									if (chars[num5 + 2] == '-' && chars[num5 + 3] == '-')
									{
										goto Block_34;
									}
									if (this.ps.charsUsed - num5 < 9)
									{
										goto IL_693;
									}
									if (XmlConvert.StrEqual(chars, num5 + 2, 7, "[CDATA["))
									{
										goto Block_36;
									}
									continue;
								}
							}
							XmlTextReaderImpl.IncrementalReadState incrementalReadState = this.incReadState;
							if (incrementalReadState != XmlTextReaderImpl.IncrementalReadState.Attributes)
							{
								if (incrementalReadState == XmlTextReaderImpl.IncrementalReadState.AttributeValue && chars[num5] == this.curNode.quoteChar)
								{
									this.incReadState = XmlTextReaderImpl.IncrementalReadState.Attributes;
								}
							}
							else
							{
								this.curNode.quoteChar = chars[num5];
								this.incReadState = XmlTextReaderImpl.IncrementalReadState.AttributeValue;
							}
							num5++;
							continue;
							IL_67C:
							if (num5 == this.ps.charsUsed)
							{
								goto IL_693;
							}
							num5++;
						}
					}
					IL_69A:
					this.ps.charPos = num5;
					goto IL_6A6;
					IL_693:
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadData;
					goto IL_69A;
					IL_596:
					this.incReadDepth++;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.Attributes;
					num5 = num8;
					goto IL_69A;
					Block_36:
					num5 += 9;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.CDATA;
					goto IL_69A;
					Block_34:
					num5 += 4;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.Comment;
					goto IL_69A;
					Block_31:
					num5 += 2;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.PI;
					goto IL_69A;
				}
				IL_47E:
				this.ps.charPos = num10;
				if (this.xmlCharType.IsWhiteSpace(this.ps.chars[num10]))
				{
					this.EatWhitespaces(null);
				}
				if (this.ps.chars[this.ps.charPos] != '>')
				{
					this.ThrowUnexpectedToken(">");
				}
				this.ps.charPos = this.ps.charPos + 1;
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.EndElement;
			}
			this.incReadLeftStartPos += num3;
			this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num3;
			return num3;
			IL_17A:
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopElementContext;
			this.nextParsingFunction = ((this.index > 0 || this.fragmentType != XmlNodeType.Document) ? XmlTextReaderImpl.ParsingFunction.ElementContent : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			this.outerReader.Read();
			this.incReadState = XmlTextReaderImpl.IncrementalReadState.End;
			return num;
			Block_54:
			this.incReadLeftStartPos = num4 + num7;
			this.incReadLeftEndPos = num5;
			this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num7;
			return num;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00030154 File Offset: 0x0002E354
		private void FinishIncrementalRead()
		{
			this.incReadDecoder = new IncrementalReadDummyDecoder();
			this.IncrementalRead();
			this.incReadDecoder = null;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00030170 File Offset: 0x0002E370
		private bool ParseFragmentAttribute()
		{
			if (this.curNode.type == XmlNodeType.None)
			{
				this.curNode.type = XmlNodeType.Attribute;
				this.curAttrIndex = 0;
				this.ParseAttributeValueSlow(this.ps.charPos, ' ', this.curNode);
			}
			else
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
			}
			if (this.ReadAttributeValue())
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.FragmentAttribute;
				return true;
			}
			this.OnEof();
			return false;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000301DC File Offset: 0x0002E3DC
		private unsafe bool ParseAttributeValueChunk()
		{
			char[] chars = this.ps.chars;
			int num = this.ps.charPos;
			this.curNode = this.AddNode(this.index + this.attrCount + 1, this.index + 2);
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			if (this.emptyEntityInAttributeResolved)
			{
				this.curNode.SetValueNode(XmlNodeType.Text, string.Empty);
				this.emptyEntityInAttributeResolved = false;
				return true;
			}
			for (;;)
			{
				if ((this.xmlCharType.charProperties[chars[num]] & 128) == 0)
				{
					char c = chars[num];
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
						case '\n':
							if (this.normalize)
							{
								chars[num] = ' ';
							}
							num++;
							continue;
						case '\v':
						case '\f':
							goto IL_220;
						case '\r':
							num++;
							continue;
						default:
							if (c != '"')
							{
								if (c != '&')
								{
									goto IL_220;
								}
								if (num - this.ps.charPos > 0)
								{
									this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
								}
								this.ps.charPos = num;
								XmlTextReaderImpl.EntityType entityType = this.HandleEntityReference(true, XmlTextReaderImpl.EntityExpandType.OnlyCharacter, out num);
								if (entityType > XmlTextReaderImpl.EntityType.CharacterNamed)
								{
									if (entityType == XmlTextReaderImpl.EntityType.Unexpanded)
									{
										goto IL_1C6;
									}
								}
								else
								{
									chars = this.ps.chars;
									if (this.normalize && this.xmlCharType.IsWhiteSpace(chars[this.ps.charPos]) && num - this.ps.charPos == 1)
									{
										chars[this.ps.charPos] = ' ';
									}
								}
								chars = this.ps.chars;
								continue;
							}
							break;
						}
					}
					else if (c != '\'')
					{
						if (c == '<')
						{
							this.Throw(num, "Xml_BadAttributeChar", XmlException.BuildCharExceptionArgs('<', '\0'));
							goto IL_276;
						}
						if (c != '>')
						{
							goto IL_220;
						}
					}
					num++;
					continue;
					IL_220:
					if (num != this.ps.charsUsed)
					{
						char ch = chars[num];
						if (XmlCharType.IsHighSurrogate((int)ch))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_276;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)chars[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
					}
					IL_276:
					if (num - this.ps.charPos > 0)
					{
						this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
						this.ps.charPos = num;
					}
					if (this.ReadData() == 0)
					{
						if (this.stringBuilder.Length > 0)
						{
							goto IL_2FB;
						}
						if (this.HandleEntityEnd(false))
						{
							goto Block_25;
						}
					}
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				else
				{
					num++;
				}
			}
			IL_1C6:
			if (this.stringBuilder.Length == 0)
			{
				XmlTextReaderImpl.NodeData nodeData = this.curNode;
				nodeData.lineInfo.linePos = nodeData.lineInfo.linePos + 1;
				this.ps.charPos = this.ps.charPos + 1;
				this.curNode.SetNamedNode(XmlNodeType.EntityReference, this.ParseEntityName());
				return true;
			}
			goto IL_2FB;
			Block_25:
			this.SetupEndEntityNodeInAttribute();
			return true;
			IL_2FB:
			if (num - this.ps.charPos > 0)
			{
				this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
				this.ps.charPos = num;
			}
			this.curNode.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
			return true;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0003054C File Offset: 0x0002E74C
		private void ParseXmlDeclarationFragment()
		{
			try
			{
				this.ParseXmlDeclaration(false);
			}
			catch (XmlException ex)
			{
				this.ReThrow(ex, ex.LineNumber, ex.LinePosition - 6);
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0003058C File Offset: 0x0002E78C
		private void ThrowUnexpectedToken(int pos, string expectedToken)
		{
			this.ThrowUnexpectedToken(pos, expectedToken, null);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00030597 File Offset: 0x0002E797
		private void ThrowUnexpectedToken(string expectedToken1)
		{
			this.ThrowUnexpectedToken(expectedToken1, null);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000305A1 File Offset: 0x0002E7A1
		private void ThrowUnexpectedToken(int pos, string expectedToken1, string expectedToken2)
		{
			this.ps.charPos = pos;
			this.ThrowUnexpectedToken(expectedToken1, expectedToken2);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000305B8 File Offset: 0x0002E7B8
		private void ThrowUnexpectedToken(string expectedToken1, string expectedToken2)
		{
			string text = this.ParseUnexpectedToken();
			if (text == null)
			{
				this.Throw("Xml_UnexpectedEOF1");
			}
			if (expectedToken2 != null)
			{
				this.Throw("Xml_UnexpectedTokens2", new string[]
				{
					text,
					expectedToken1,
					expectedToken2
				});
				return;
			}
			this.Throw("Xml_UnexpectedTokenEx", new string[]
			{
				text,
				expectedToken1
			});
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00030614 File Offset: 0x0002E814
		private string ParseUnexpectedToken(int pos)
		{
			this.ps.charPos = pos;
			return this.ParseUnexpectedToken();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00030628 File Offset: 0x0002E828
		private string ParseUnexpectedToken()
		{
			if (this.ps.charPos == this.ps.charsUsed)
			{
				return null;
			}
			if (this.xmlCharType.IsNCNameSingleChar(this.ps.chars[this.ps.charPos]))
			{
				int num = this.ps.charPos + 1;
				while (this.xmlCharType.IsNCNameSingleChar(this.ps.chars[num]))
				{
					num++;
				}
				return new string(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			}
			return new string(this.ps.chars, this.ps.charPos, 1);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000306E8 File Offset: 0x0002E8E8
		private void ThrowExpectingWhitespace(int pos)
		{
			string text = this.ParseUnexpectedToken(pos);
			if (text == null)
			{
				this.Throw(pos, "Xml_UnexpectedEOF1");
				return;
			}
			this.Throw(pos, "Xml_ExpectingWhiteSpace", text);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0003071C File Offset: 0x0002E91C
		private int GetIndexOfAttributeWithoutPrefix(string name)
		{
			name = this.nameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, name) && this.nodes[i].prefix.Length == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0003078C File Offset: 0x0002E98C
		private int GetIndexOfAttributeWithPrefix(string name)
		{
			name = this.nameTable.Add(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].GetNameWPrefix(this.nameTable), name))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x000307EC File Offset: 0x0002E9EC
		private bool ZeroEndingStream(int pos)
		{
			if (this.v1Compat && pos == this.ps.charsUsed - 1 && this.ps.chars[pos] == '\0' && this.ReadData() == 0 && this.ps.isStreamEof)
			{
				this.ps.charsUsed = this.ps.charsUsed - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00030848 File Offset: 0x0002EA48
		private void ParseDtdFromParserContext()
		{
			IDtdParser dtdParser = DtdParser.Create();
			this.dtdInfo = dtdParser.ParseFreeFloatingDtd(this.fragmentParserContext.BaseURI, this.fragmentParserContext.DocTypeName, this.fragmentParserContext.PublicId, this.fragmentParserContext.SystemId, this.fragmentParserContext.InternalSubset, new XmlTextReaderImpl.DtdParserProxy(this));
			if ((this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000308D8 File Offset: 0x0002EAD8
		private bool InitReadContentAsBinary()
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				throw new InvalidOperationException(Res.GetString("Xml_MixingReadValueChunkWithBinary"));
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				throw new InvalidOperationException(Res.GetString("Xml_MixingV1StreamingWithV2Binary"));
			}
			if (!XmlReader.IsTextualNode(this.curNode.type) && !this.MoveToNextContentNode(false))
			{
				return false;
			}
			this.SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary);
			this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			return true;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00030960 File Offset: 0x0002EB60
		private bool InitReadElementContentAsBinary()
		{
			bool isEmptyElement = this.curNode.IsEmptyElement;
			this.outerReader.Read();
			if (isEmptyElement)
			{
				return false;
			}
			if (!this.MoveToNextContentNode(false))
			{
				if (this.curNode.type != XmlNodeType.EndElement)
				{
					this.Throw("Xml_InvalidNodeType", this.curNode.type.ToString());
				}
				this.outerReader.Read();
				return false;
			}
			this.SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary);
			this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			return true;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000309FC File Offset: 0x0002EBFC
		private bool MoveToNextContentNode(bool moveIfOnContentNode)
		{
			for (;;)
			{
				switch (this.curNode.type)
				{
				case XmlNodeType.Attribute:
					goto IL_52;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (!moveIfOnContentNode)
					{
						return true;
					}
					goto IL_6B;
				case XmlNodeType.EntityReference:
					this.outerReader.ResolveEntity();
					goto IL_6B;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					goto IL_6B;
				}
				break;
				IL_6B:
				moveIfOnContentNode = false;
				if (!this.outerReader.Read())
				{
					return false;
				}
			}
			return false;
			IL_52:
			return !moveIfOnContentNode;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00030A88 File Offset: 0x0002EC88
		private void SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction inReadBinaryFunction)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
			{
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue;
			}
			else
			{
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue;
				this.nextNextParsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.parsingFunction;
			}
			this.readValueOffset = 0;
			this.parsingFunction = inReadBinaryFunction;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00030AD8 File Offset: 0x0002ECD8
		private void SetupFromParserContext(XmlParserContext context, XmlReaderSettings settings)
		{
			XmlNameTable xmlNameTable = settings.NameTable;
			this.nameTableFromSettings = (xmlNameTable != null);
			if (context.NamespaceManager != null)
			{
				if (xmlNameTable != null && xmlNameTable != context.NamespaceManager.NameTable)
				{
					throw new XmlException("Xml_NametableMismatch");
				}
				this.namespaceManager = context.NamespaceManager;
				this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
				xmlNameTable = this.namespaceManager.NameTable;
			}
			else if (context.NameTable != null)
			{
				if (xmlNameTable != null && xmlNameTable != context.NameTable)
				{
					throw new XmlException("Xml_NametableMismatch", string.Empty);
				}
				xmlNameTable = context.NameTable;
			}
			else if (xmlNameTable == null)
			{
				xmlNameTable = new NameTable();
			}
			this.nameTable = xmlNameTable;
			if (this.namespaceManager == null)
			{
				this.namespaceManager = new XmlNamespaceManager(xmlNameTable);
			}
			this.xmlContext.xmlSpace = context.XmlSpace;
			this.xmlContext.xmlLang = context.XmlLang;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00030BC2 File Offset: 0x0002EDC2
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.dtdInfo;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00030BCC File Offset: 0x0002EDCC
		internal void SetDtdInfo(IDtdInfo newDtdInfo)
		{
			this.dtdInfo = newDtdInfo;
			if (this.dtdInfo != null && (this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
		}

		// Token: 0x17000244 RID: 580
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00030C19 File Offset: 0x0002EE19
		internal IValidationEventHandling ValidationEventHandling
		{
			set
			{
				this.validationEventHandling = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x00030C22 File Offset: 0x0002EE22
		internal XmlTextReaderImpl.OnDefaultAttributeUseDelegate OnDefaultAttributeUse
		{
			set
			{
				this.onDefaultAttributeUse = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00030C2B File Offset: 0x0002EE2B
		internal bool XmlValidatingReaderCompatibilityMode
		{
			set
			{
				this.validatingReaderCompatFlag = value;
				if (value)
				{
					this.nameTable.Add("http://www.w3.org/2001/XMLSchema");
					this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
					this.nameTable.Add("urn:schemas-microsoft-com:datatypes");
				}
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00030C6A File Offset: 0x0002EE6A
		internal XmlNodeType FragmentType
		{
			get
			{
				return this.fragmentType;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00030C72 File Offset: 0x0002EE72
		internal void ChangeCurrentNodeType(XmlNodeType newNodeType)
		{
			this.curNode.type = newNodeType;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00030C80 File Offset: 0x0002EE80
		internal XmlResolver GetResolver()
		{
			if (this.IsResolverNull)
			{
				return null;
			}
			return this.xmlResolver;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00030C92 File Offset: 0x0002EE92
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00030C9F File Offset: 0x0002EE9F
		internal object InternalSchemaType
		{
			get
			{
				return this.curNode.schemaType;
			}
			set
			{
				this.curNode.schemaType = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00030CAD File Offset: 0x0002EEAD
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x00030CBA File Offset: 0x0002EEBA
		internal object InternalTypedValue
		{
			get
			{
				return this.curNode.typedValue;
			}
			set
			{
				this.curNode.typedValue = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00030CC8 File Offset: 0x0002EEC8
		internal bool StandAlone
		{
			get
			{
				return this.standalone;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00030CD0 File Offset: 0x0002EED0
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.namespaceManager;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00030CD8 File Offset: 0x0002EED8
		internal bool V1Compat
		{
			get
			{
				return this.v1Compat;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00030CE0 File Offset: 0x0002EEE0
		internal ConformanceLevel V1ComformanceLevel
		{
			get
			{
				if (this.fragmentType != XmlNodeType.Element)
				{
					return ConformanceLevel.Document;
				}
				return ConformanceLevel.Fragment;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00030CF0 File Offset: 0x0002EEF0
		private bool AddDefaultAttributeDtd(IDtdDefaultAttributeInfo defAttrInfo, bool definedInDtd, XmlTextReaderImpl.NodeData[] nameSortedNodeData)
		{
			if (defAttrInfo.Prefix.Length > 0)
			{
				this.attrNeedNamespaceLookup = true;
			}
			string localName = defAttrInfo.LocalName;
			string prefix = defAttrInfo.Prefix;
			if (nameSortedNodeData != null)
			{
				if (Array.BinarySearch<object>(nameSortedNodeData, defAttrInfo, XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.Instance) >= 0)
				{
					return false;
				}
			}
			else
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					if (this.nodes[i].localName == localName && this.nodes[i].prefix == prefix)
					{
						return false;
					}
				}
			}
			XmlTextReaderImpl.NodeData nodeData = this.AddDefaultAttributeInternal(defAttrInfo.LocalName, null, defAttrInfo.Prefix, defAttrInfo.DefaultValueExpanded, defAttrInfo.LineNumber, defAttrInfo.LinePosition, defAttrInfo.ValueLineNumber, defAttrInfo.ValueLinePosition, defAttrInfo.IsXmlAttribute);
			if (this.DtdValidation)
			{
				if (this.onDefaultAttributeUse != null)
				{
					this.onDefaultAttributeUse(defAttrInfo, this);
				}
				nodeData.typedValue = defAttrInfo.DefaultValueTyped;
			}
			return nodeData != null;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00030DE4 File Offset: 0x0002EFE4
		internal bool AddDefaultAttributeNonDtd(SchemaAttDef attrDef)
		{
			string text = this.nameTable.Add(attrDef.Name.Name);
			string text2 = this.nameTable.Add(attrDef.Prefix);
			string text3 = this.nameTable.Add(attrDef.Name.Namespace);
			if (text2.Length == 0 && text3.Length > 0)
			{
				text2 = this.namespaceManager.LookupPrefix(text3);
				if (text2 == null)
				{
					text2 = string.Empty;
				}
			}
			for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
			{
				if (this.nodes[i].localName == text && (this.nodes[i].prefix == text2 || (this.nodes[i].ns == text3 && text3 != null)))
				{
					return false;
				}
			}
			XmlTextReaderImpl.NodeData nodeData = this.AddDefaultAttributeInternal(text, text3, text2, attrDef.DefaultValueExpanded, attrDef.LineNumber, attrDef.LinePosition, attrDef.ValueLineNumber, attrDef.ValueLinePosition, attrDef.Reserved > SchemaAttDef.Reserve.None);
			nodeData.schemaType = ((attrDef.SchemaType == null) ? attrDef.Datatype : attrDef.SchemaType);
			nodeData.typedValue = attrDef.DefaultValueTyped;
			return true;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00030F10 File Offset: 0x0002F110
		private XmlTextReaderImpl.NodeData AddDefaultAttributeInternal(string localName, string ns, string prefix, string value, int lineNo, int linePos, int valueLineNo, int valueLinePos, bool isXmlAttribute)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttribute(localName, prefix, (prefix.Length > 0) ? null : localName);
			if (ns != null)
			{
				nodeData.ns = ns;
			}
			nodeData.SetValue(value);
			nodeData.IsDefaultAttribute = true;
			nodeData.lineInfo.Set(lineNo, linePos);
			nodeData.lineInfo2.Set(valueLineNo, valueLinePos);
			if (nodeData.prefix.Length == 0)
			{
				if (Ref.Equal(nodeData.localName, this.XmlNs))
				{
					this.OnDefaultNamespaceDecl(nodeData);
					if (!this.attrNeedNamespaceLookup && this.nodes[this.index].prefix.Length == 0)
					{
						this.nodes[this.index].ns = this.xmlContext.defaultNamespace;
					}
				}
			}
			else if (Ref.Equal(nodeData.prefix, this.XmlNs))
			{
				this.OnNamespaceDecl(nodeData);
				if (!this.attrNeedNamespaceLookup)
				{
					string localName2 = nodeData.localName;
					for (int i = this.index; i < this.index + this.attrCount + 1; i++)
					{
						if (this.nodes[i].prefix.Equals(localName2))
						{
							this.nodes[i].ns = this.namespaceManager.LookupNamespace(localName2);
						}
					}
				}
			}
			else if (isXmlAttribute)
			{
				this.OnXmlReservedAttribute(nodeData);
			}
			this.fullAttrCleanup = true;
			return nodeData;
		}

		// Token: 0x1700024E RID: 590
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x00031068 File Offset: 0x0002F268
		internal bool DisableUndeclaredEntityCheck
		{
			set
			{
				this.disableUndeclaredEntityCheck = value;
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00031074 File Offset: 0x0002F274
		private int ReadContentAsBinary(byte[] buffer, int index, int count)
		{
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End)
			{
				return 0;
			}
			this.incReadDecoder.SetNextOutputBuffer(buffer, index, count);
			int num;
			int num2;
			int num3;
			XmlTextReaderImpl.ParsingFunction inReadBinaryFunction;
			for (;;)
			{
				num = 0;
				try
				{
					num = this.curNode.CopyToBinary(this.incReadDecoder, this.readValueOffset);
				}
				catch (XmlException e)
				{
					this.curNode.AdjustLineInfo(this.readValueOffset, this.ps.eolNormalized, ref this.incReadLineInfo);
					this.ReThrow(e, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
				}
				this.readValueOffset += num;
				if (this.incReadDecoder.IsFull)
				{
					break;
				}
				if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue)
				{
					this.curNode.SetValue(string.Empty);
					bool flag = false;
					num2 = 0;
					num3 = 0;
					while (!this.incReadDecoder.IsFull && !flag)
					{
						int num4 = 0;
						this.incReadLineInfo.Set(this.ps.LineNo, this.ps.LinePos);
						flag = this.ParseText(out num2, out num3, ref num4);
						try
						{
							num = this.incReadDecoder.Decode(this.ps.chars, num2, num3 - num2);
						}
						catch (XmlException e2)
						{
							this.ReThrow(e2, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						}
						num2 += num;
					}
					this.incReadState = (flag ? XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue : XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue);
					this.readValueOffset = 0;
					if (this.incReadDecoder.IsFull)
					{
						goto Block_8;
					}
				}
				inReadBinaryFunction = this.parsingFunction;
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.nextNextParsingFunction;
				if (!this.MoveToNextContentNode(true))
				{
					goto Block_9;
				}
				this.SetupReadContentAsBinaryState(inReadBinaryFunction);
				this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			}
			return this.incReadDecoder.DecodedCount;
			Block_8:
			this.curNode.SetValue(this.ps.chars, num2, num3 - num2);
			XmlTextReaderImpl.AdjustLineInfo(this.ps.chars, num2 - num, num2, this.ps.eolNormalized, ref this.incReadLineInfo);
			this.curNode.SetLineInfo(this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
			return this.incReadDecoder.DecodedCount;
			Block_9:
			this.SetupReadContentAsBinaryState(inReadBinaryFunction);
			this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End;
			return this.incReadDecoder.DecodedCount;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000312F4 File Offset: 0x0002F4F4
		private int ReadElementContentAsBinary(byte[] buffer, int index, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = this.ReadContentAsBinary(buffer, index, count);
			if (num > 0)
			{
				return num;
			}
			if (this.curNode.type != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.curNode.type.ToString(), this);
			}
			this.parsingFunction = this.nextParsingFunction;
			this.nextParsingFunction = this.nextNextParsingFunction;
			this.outerReader.Read();
			return 0;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0003136C File Offset: 0x0002F56C
		private void InitBase64Decoder()
		{
			if (this.base64Decoder == null)
			{
				this.base64Decoder = new Base64Decoder();
			}
			else
			{
				this.base64Decoder.Reset();
			}
			this.incReadDecoder = this.base64Decoder;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0003139A File Offset: 0x0002F59A
		private void InitBinHexDecoder()
		{
			if (this.binHexDecoder == null)
			{
				this.binHexDecoder = new BinHexDecoder();
			}
			else
			{
				this.binHexDecoder.Reset();
			}
			this.incReadDecoder = this.binHexDecoder;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000313C8 File Offset: 0x0002F5C8
		private bool UriEqual(Uri uri1, string uri1Str, string uri2Str, XmlResolver resolver)
		{
			if (resolver == null)
			{
				return uri1Str == uri2Str;
			}
			if (uri1 == null)
			{
				uri1 = resolver.ResolveUri(null, uri1Str);
			}
			Uri obj = resolver.ResolveUri(null, uri2Str);
			return uri1.Equals(obj);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00031408 File Offset: 0x0002F608
		private void RegisterConsumedCharacters(long characters, bool inEntityReference)
		{
			if (this.maxCharactersInDocument > 0L)
			{
				long num = this.charactersInDocument + characters;
				if (num < this.charactersInDocument)
				{
					this.ThrowWithoutLineInfo("Xml_LimitExceeded", "MaxCharactersInDocument");
				}
				else
				{
					this.charactersInDocument = num;
				}
				if (this.charactersInDocument > this.maxCharactersInDocument)
				{
					this.ThrowWithoutLineInfo("Xml_LimitExceeded", "MaxCharactersInDocument");
				}
			}
			if (this.maxCharactersFromEntities > 0L && inEntityReference)
			{
				long num2 = this.charactersFromEntities + characters;
				if (num2 < this.charactersFromEntities)
				{
					this.ThrowWithoutLineInfo("Xml_LimitExceeded", "MaxCharactersFromEntities");
				}
				else
				{
					this.charactersFromEntities = num2;
				}
				if (this.charactersFromEntities > this.maxCharactersFromEntities)
				{
					this.ThrowWithoutLineInfo("Xml_LimitExceeded", "MaxCharactersFromEntities");
				}
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000314C0 File Offset: 0x0002F6C0
		internal unsafe static void AdjustLineInfo(char[] chars, int startPos, int endPos, bool isNormalized, ref LineInfo lineInfo)
		{
			fixed (char* ptr = &chars[startPos])
			{
				char* pChars = ptr;
				XmlTextReaderImpl.AdjustLineInfo(pChars, endPos - startPos, isNormalized, ref lineInfo);
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x000314E8 File Offset: 0x0002F6E8
		internal unsafe static void AdjustLineInfo(string str, int startPos, int endPos, bool isNormalized, ref LineInfo lineInfo)
		{
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				XmlTextReaderImpl.AdjustLineInfo(ptr + startPos, endPos - startPos, isNormalized, ref lineInfo);
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00031518 File Offset: 0x0002F718
		internal unsafe static void AdjustLineInfo(char* pChars, int length, bool isNormalized, ref LineInfo lineInfo)
		{
			int num = -1;
			for (int i = 0; i < length; i++)
			{
				char c = pChars[i];
				if (c != '\n')
				{
					if (c == '\r')
					{
						if (!isNormalized)
						{
							lineInfo.lineNo++;
							num = i;
							if (i + 1 < length && pChars[i + 1] == '\n')
							{
								i++;
								num++;
							}
						}
					}
				}
				else
				{
					lineInfo.lineNo++;
					num = i;
				}
			}
			if (num >= 0)
			{
				lineInfo.linePos = length - num;
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00031590 File Offset: 0x0002F790
		internal static string StripSpaces(string value)
		{
			int length = value.Length;
			if (length <= 0)
			{
				return string.Empty;
			}
			int num = 0;
			StringBuilder stringBuilder = null;
			while (value[num] == ' ')
			{
				num++;
				if (num == length)
				{
					return " ";
				}
			}
			int i;
			for (i = num; i < length; i++)
			{
				if (value[i] == ' ')
				{
					int num2 = i + 1;
					while (num2 < length && value[num2] == ' ')
					{
						num2++;
					}
					if (num2 == length)
					{
						if (stringBuilder == null)
						{
							return value.Substring(num, i - num);
						}
						stringBuilder.Append(value, num, i - num);
						return stringBuilder.ToString();
					}
					else if (num2 > i + 1)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(length);
						}
						stringBuilder.Append(value, num, i - num + 1);
						num = num2;
						i = num2 - 1;
					}
				}
			}
			if (stringBuilder != null)
			{
				if (i > num)
				{
					stringBuilder.Append(value, num, i - num);
				}
				return stringBuilder.ToString();
			}
			if (num != 0)
			{
				return value.Substring(num, length - num);
			}
			return value;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00031678 File Offset: 0x0002F878
		internal static void StripSpaces(char[] value, int index, ref int len)
		{
			if (len <= 0)
			{
				return;
			}
			int num = index;
			int num2 = index + len;
			while (value[num] == ' ')
			{
				num++;
				if (num == num2)
				{
					len = 1;
					return;
				}
			}
			int num3 = num - index;
			for (int i = num; i < num2; i++)
			{
				char c;
				if ((c = value[i]) == ' ')
				{
					int num4 = i + 1;
					while (num4 < num2 && value[num4] == ' ')
					{
						num4++;
					}
					if (num4 == num2)
					{
						num3 += num4 - i;
						break;
					}
					if (num4 > i + 1)
					{
						num3 += num4 - i - 1;
						i = num4 - 1;
					}
				}
				value[i - num3] = c;
			}
			len -= num3;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0003170B File Offset: 0x0002F90B
		internal static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0003171E File Offset: 0x0002F91E
		internal static void BlockCopy(byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0003172B File Offset: 0x0002F92B
		private void CheckAsyncCall()
		{
			if (!this.useAsync)
			{
				throw new InvalidOperationException(Res.GetString("Xml_ReaderAsyncNotSetException"));
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00031745 File Offset: 0x0002F945
		public override Task<string> GetValueAsync()
		{
			this.CheckAsyncCall();
			if (this.parsingFunction >= XmlTextReaderImpl.ParsingFunction.PartialTextValue)
			{
				return this._GetValueAsync();
			}
			return Task.FromResult<string>(this.curNode.StringValue);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00031770 File Offset: 0x0002F970
		private Task<string> _GetValueAsync()
		{
			XmlTextReaderImpl.<_GetValueAsync>d__474 <_GetValueAsync>d__;
			<_GetValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<_GetValueAsync>d__.<>4__this = this;
			<_GetValueAsync>d__.<>1__state = -1;
			<_GetValueAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<_GetValueAsync>d__474>(ref <_GetValueAsync>d__);
			return <_GetValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000317B4 File Offset: 0x0002F9B4
		private Task FinishInitAsync()
		{
			switch (this.laterInitParam.initType)
			{
			case XmlTextReaderImpl.InitInputType.UriString:
				return this.FinishInitUriStringAsync();
			case XmlTextReaderImpl.InitInputType.Stream:
				return this.FinishInitStreamAsync();
			case XmlTextReaderImpl.InitInputType.TextReader:
				return this.FinishInitTextReaderAsync();
			default:
				return AsyncHelper.DoneTask;
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000317FC File Offset: 0x0002F9FC
		private Task FinishInitUriStringAsync()
		{
			XmlTextReaderImpl.<FinishInitUriStringAsync>d__476 <FinishInitUriStringAsync>d__;
			<FinishInitUriStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishInitUriStringAsync>d__.<>4__this = this;
			<FinishInitUriStringAsync>d__.<>1__state = -1;
			<FinishInitUriStringAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishInitUriStringAsync>d__476>(ref <FinishInitUriStringAsync>d__);
			return <FinishInitUriStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00031840 File Offset: 0x0002FA40
		private Task FinishInitStreamAsync()
		{
			XmlTextReaderImpl.<FinishInitStreamAsync>d__477 <FinishInitStreamAsync>d__;
			<FinishInitStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishInitStreamAsync>d__.<>4__this = this;
			<FinishInitStreamAsync>d__.<>1__state = -1;
			<FinishInitStreamAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishInitStreamAsync>d__477>(ref <FinishInitStreamAsync>d__);
			return <FinishInitStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00031884 File Offset: 0x0002FA84
		private Task FinishInitTextReaderAsync()
		{
			XmlTextReaderImpl.<FinishInitTextReaderAsync>d__478 <FinishInitTextReaderAsync>d__;
			<FinishInitTextReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishInitTextReaderAsync>d__.<>4__this = this;
			<FinishInitTextReaderAsync>d__.<>1__state = -1;
			<FinishInitTextReaderAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishInitTextReaderAsync>d__478>(ref <FinishInitTextReaderAsync>d__);
			return <FinishInitTextReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000318C8 File Offset: 0x0002FAC8
		public override Task<bool> ReadAsync()
		{
			this.CheckAsyncCall();
			if (this.laterInitParam != null)
			{
				return this.FinishInitAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			}
			for (;;)
			{
				switch (this.parsingFunction)
				{
				case XmlTextReaderImpl.ParsingFunction.ElementContent:
					goto IL_9E;
				case XmlTextReaderImpl.ParsingFunction.NoData:
					goto IL_2DC;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractive:
					this.readState = ReadState.Interactive;
					this.parsingFunction = this.nextParsingFunction;
					break;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl:
					goto IL_C4;
				case XmlTextReaderImpl.ParsingFunction.DocumentContent:
					goto IL_A5;
				case XmlTextReaderImpl.ParsingFunction.MoveToElementContent:
					this.ResetAttributes();
					this.index++;
					this.curNode = this.AddNode(this.index, this.index);
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ElementContent;
					break;
				case XmlTextReaderImpl.ParsingFunction.PopElementContext:
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					break;
				case XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext:
					this.curNode = this.nodes[this.index];
					this.curNode.IsEmptyElement = false;
					this.ResetAttributes();
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					break;
				case XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel:
					this.ResetAttributes();
					this.curNode = this.nodes[this.index];
					this.parsingFunction = ((this.index == 0) ? XmlTextReaderImpl.ParsingFunction.DocumentContent : XmlTextReaderImpl.ParsingFunction.ElementContent);
					break;
				case XmlTextReaderImpl.ParsingFunction.Error:
				case XmlTextReaderImpl.ParsingFunction.Eof:
				case XmlTextReaderImpl.ParsingFunction.ReaderClosed:
					goto IL_2D6;
				case XmlTextReaderImpl.ParsingFunction.EntityReference:
					goto IL_186;
				case XmlTextReaderImpl.ParsingFunction.InIncrementalRead:
					goto IL_29E;
				case XmlTextReaderImpl.ParsingFunction.FragmentAttribute:
					goto IL_2AA;
				case XmlTextReaderImpl.ParsingFunction.ReportEndEntity:
					goto IL_19F;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent:
					this.curNode = this.AddNode(this.index, this.index);
					this.reportedEncoding = this.ps.encoding;
					this.reportedBaseUri = this.ps.baseUriStr;
					this.parsingFunction = this.nextParsingFunction;
					break;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent:
					goto IL_202;
				case XmlTextReaderImpl.ParsingFunction.XmlDeclarationFragment:
					goto IL_2B6;
				case XmlTextReaderImpl.ParsingFunction.GoToEof:
					goto IL_2CA;
				case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
					goto IL_2ED;
				case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
					this.FinishAttributeValueIterator();
					this.curNode = this.nodes[this.index];
					break;
				case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
					goto IL_306;
				case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
					goto IL_31F;
				case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
					goto IL_338;
				}
			}
			IL_9E:
			return this.ParseElementContentAsync();
			IL_A5:
			return this.ParseDocumentContentAsync();
			IL_C4:
			return this.ReadAsync_SwitchToInteractiveXmlDecl();
			IL_186:
			this.parsingFunction = this.nextParsingFunction;
			return this.ParseEntityReferenceAsync().ReturnTaskBoolWhenFinish(true);
			IL_19F:
			this.SetupEndEntityNodeInContent();
			this.parsingFunction = this.nextParsingFunction;
			return AsyncHelper.DoneTaskTrue;
			IL_202:
			this.curNode = this.AddNode(this.index, this.index);
			this.curNode.SetValueNode(XmlNodeType.Text, string.Empty);
			this.curNode.SetLineInfo(this.ps.lineNo, this.ps.LinePos);
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			this.parsingFunction = this.nextParsingFunction;
			return AsyncHelper.DoneTaskTrue;
			IL_29E:
			this.FinishIncrementalRead();
			return AsyncHelper.DoneTaskTrue;
			IL_2AA:
			return Task.FromResult<bool>(this.ParseFragmentAttribute());
			IL_2B6:
			this.ParseXmlDeclarationFragment();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.GoToEof;
			return AsyncHelper.DoneTaskTrue;
			IL_2CA:
			this.OnEof();
			return AsyncHelper.DoneTaskFalse;
			IL_2D6:
			return AsyncHelper.DoneTaskFalse;
			IL_2DC:
			this.ThrowWithoutLineInfo("Xml_MissingRoot");
			return AsyncHelper.DoneTaskFalse;
			IL_2ED:
			return this.SkipPartialTextValueAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			IL_306:
			return this.FinishReadValueChunkAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			IL_31F:
			return this.FinishReadContentAsBinaryAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			IL_338:
			return this.FinishReadElementContentAsBinaryAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00031C28 File Offset: 0x0002FE28
		private Task<bool> ReadAsync_SwitchToInteractiveXmlDecl()
		{
			this.readState = ReadState.Interactive;
			this.parsingFunction = this.nextParsingFunction;
			Task<bool> task = this.ParseXmlDeclarationAsync(false);
			if (task.IsSuccess())
			{
				return this.ReadAsync_SwitchToInteractiveXmlDecl_Helper(task.Result);
			}
			return this._ReadAsync_SwitchToInteractiveXmlDecl(task);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00031C6C File Offset: 0x0002FE6C
		private Task<bool> _ReadAsync_SwitchToInteractiveXmlDecl(Task<bool> task)
		{
			XmlTextReaderImpl.<_ReadAsync_SwitchToInteractiveXmlDecl>d__481 <_ReadAsync_SwitchToInteractiveXmlDecl>d__;
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>4__this = this;
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.task = task;
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>1__state = -1;
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>t__builder.Start<XmlTextReaderImpl.<_ReadAsync_SwitchToInteractiveXmlDecl>d__481>(ref <_ReadAsync_SwitchToInteractiveXmlDecl>d__);
			return <_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>t__builder.Task;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00031CB7 File Offset: 0x0002FEB7
		private Task<bool> ReadAsync_SwitchToInteractiveXmlDecl_Helper(bool finish)
		{
			if (finish)
			{
				this.reportedEncoding = this.ps.encoding;
				return AsyncHelper.DoneTaskTrue;
			}
			this.reportedEncoding = this.ps.encoding;
			return this.ReadAsync();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00031CEC File Offset: 0x0002FEEC
		public override Task SkipAsync()
		{
			XmlTextReaderImpl.<SkipAsync>d__483 <SkipAsync>d__;
			<SkipAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipAsync>d__.<>4__this = this;
			<SkipAsync>d__.<>1__state = -1;
			<SkipAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<SkipAsync>d__483>(ref <SkipAsync>d__);
			return <SkipAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00031D30 File Offset: 0x0002FF30
		private Task<int> ReadContentAsBase64_AsyncHelper(Task<bool> task, byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadContentAsBase64_AsyncHelper>d__484 <ReadContentAsBase64_AsyncHelper>d__;
			<ReadContentAsBase64_AsyncHelper>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64_AsyncHelper>d__.<>4__this = this;
			<ReadContentAsBase64_AsyncHelper>d__.task = task;
			<ReadContentAsBase64_AsyncHelper>d__.buffer = buffer;
			<ReadContentAsBase64_AsyncHelper>d__.index = index;
			<ReadContentAsBase64_AsyncHelper>d__.count = count;
			<ReadContentAsBase64_AsyncHelper>d__.<>1__state = -1;
			<ReadContentAsBase64_AsyncHelper>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadContentAsBase64_AsyncHelper>d__484>(ref <ReadContentAsBase64_AsyncHelper>d__);
			return <ReadContentAsBase64_AsyncHelper>d__.<>t__builder.Task;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00031D94 File Offset: 0x0002FF94
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadContentAsBinaryAsync(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return AsyncHelper.DoneTaskZero;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
				}
				if (!XmlReader.CanReadContentAs(this.curNode.type))
				{
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				}
				Task<bool> task = this.InitReadContentAsBinaryAsync();
				if (!task.IsSuccess())
				{
					return this.ReadContentAsBase64_AsyncHelper(task, buffer, index, count);
				}
				if (!task.Result)
				{
					return AsyncHelper.DoneTaskZero;
				}
			}
			this.InitBase64Decoder();
			return this.ReadContentAsBinaryAsync(buffer, index, count);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00031E88 File Offset: 0x00030088
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadContentAsBinHexAsync>d__486 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadContentAsBinHexAsync>d__486>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00031EE4 File Offset: 0x000300E4
		private Task<int> ReadElementContentAsBase64Async_Helper(Task<bool> task, byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadElementContentAsBase64Async_Helper>d__487 <ReadElementContentAsBase64Async_Helper>d__;
			<ReadElementContentAsBase64Async_Helper>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async_Helper>d__.<>4__this = this;
			<ReadElementContentAsBase64Async_Helper>d__.task = task;
			<ReadElementContentAsBase64Async_Helper>d__.buffer = buffer;
			<ReadElementContentAsBase64Async_Helper>d__.index = index;
			<ReadElementContentAsBase64Async_Helper>d__.count = count;
			<ReadElementContentAsBase64Async_Helper>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async_Helper>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadElementContentAsBase64Async_Helper>d__487>(ref <ReadElementContentAsBase64Async_Helper>d__);
			return <ReadElementContentAsBase64Async_Helper>d__.<>t__builder.Task;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00031F48 File Offset: 0x00030148
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadElementContentAsBinaryAsync(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return AsyncHelper.DoneTaskZero;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
				}
				if (this.curNode.type != XmlNodeType.Element)
				{
					throw base.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				Task<bool> task = this.InitReadElementContentAsBinaryAsync();
				if (!task.IsSuccess())
				{
					return this.ReadElementContentAsBase64Async_Helper(task, buffer, index, count);
				}
				if (!task.Result)
				{
					return AsyncHelper.DoneTaskZero;
				}
			}
			this.InitBase64Decoder();
			return this.ReadElementContentAsBinaryAsync(buffer, index, count);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00032038 File Offset: 0x00030238
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadElementContentAsBinHexAsync>d__489 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadElementContentAsBinHexAsync>d__489>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00032094 File Offset: 0x00030294
		public override Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadValueChunkAsync>d__490 <ReadValueChunkAsync>d__;
			<ReadValueChunkAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadValueChunkAsync>d__.<>4__this = this;
			<ReadValueChunkAsync>d__.buffer = buffer;
			<ReadValueChunkAsync>d__.index = index;
			<ReadValueChunkAsync>d__.count = count;
			<ReadValueChunkAsync>d__.<>1__state = -1;
			<ReadValueChunkAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadValueChunkAsync>d__490>(ref <ReadValueChunkAsync>d__);
			return <ReadValueChunkAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000320EF File Offset: 0x000302EF
		internal Task<int> DtdParserProxy_ReadDataAsync()
		{
			this.CheckAsyncCall();
			return this.ReadDataAsync();
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00032100 File Offset: 0x00030300
		internal Task<int> DtdParserProxy_ParseNumericCharRefAsync(StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.<DtdParserProxy_ParseNumericCharRefAsync>d__492 <DtdParserProxy_ParseNumericCharRefAsync>d__;
			<DtdParserProxy_ParseNumericCharRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<DtdParserProxy_ParseNumericCharRefAsync>d__.<>4__this = this;
			<DtdParserProxy_ParseNumericCharRefAsync>d__.internalSubsetBuilder = internalSubsetBuilder;
			<DtdParserProxy_ParseNumericCharRefAsync>d__.<>1__state = -1;
			<DtdParserProxy_ParseNumericCharRefAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_ParseNumericCharRefAsync>d__492>(ref <DtdParserProxy_ParseNumericCharRefAsync>d__);
			return <DtdParserProxy_ParseNumericCharRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0003214B File Offset: 0x0003034B
		internal Task<int> DtdParserProxy_ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder)
		{
			this.CheckAsyncCall();
			return this.ParseNamedCharRefAsync(expand, internalSubsetBuilder);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0003215C File Offset: 0x0003035C
		internal Task DtdParserProxy_ParsePIAsync(StringBuilder sb)
		{
			XmlTextReaderImpl.<DtdParserProxy_ParsePIAsync>d__494 <DtdParserProxy_ParsePIAsync>d__;
			<DtdParserProxy_ParsePIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DtdParserProxy_ParsePIAsync>d__.<>4__this = this;
			<DtdParserProxy_ParsePIAsync>d__.sb = sb;
			<DtdParserProxy_ParsePIAsync>d__.<>1__state = -1;
			<DtdParserProxy_ParsePIAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_ParsePIAsync>d__494>(ref <DtdParserProxy_ParsePIAsync>d__);
			return <DtdParserProxy_ParsePIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000321A8 File Offset: 0x000303A8
		internal Task DtdParserProxy_ParseCommentAsync(StringBuilder sb)
		{
			XmlTextReaderImpl.<DtdParserProxy_ParseCommentAsync>d__495 <DtdParserProxy_ParseCommentAsync>d__;
			<DtdParserProxy_ParseCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DtdParserProxy_ParseCommentAsync>d__.<>4__this = this;
			<DtdParserProxy_ParseCommentAsync>d__.sb = sb;
			<DtdParserProxy_ParseCommentAsync>d__.<>1__state = -1;
			<DtdParserProxy_ParseCommentAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_ParseCommentAsync>d__495>(ref <DtdParserProxy_ParseCommentAsync>d__);
			return <DtdParserProxy_ParseCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000321F4 File Offset: 0x000303F4
		internal Task<Tuple<int, bool>> DtdParserProxy_PushEntityAsync(IDtdEntityInfo entity)
		{
			XmlTextReaderImpl.<DtdParserProxy_PushEntityAsync>d__496 <DtdParserProxy_PushEntityAsync>d__;
			<DtdParserProxy_PushEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, bool>>.Create();
			<DtdParserProxy_PushEntityAsync>d__.<>4__this = this;
			<DtdParserProxy_PushEntityAsync>d__.entity = entity;
			<DtdParserProxy_PushEntityAsync>d__.<>1__state = -1;
			<DtdParserProxy_PushEntityAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_PushEntityAsync>d__496>(ref <DtdParserProxy_PushEntityAsync>d__);
			return <DtdParserProxy_PushEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00032240 File Offset: 0x00030440
		internal Task<bool> DtdParserProxy_PushExternalSubsetAsync(string systemId, string publicId)
		{
			XmlTextReaderImpl.<DtdParserProxy_PushExternalSubsetAsync>d__497 <DtdParserProxy_PushExternalSubsetAsync>d__;
			<DtdParserProxy_PushExternalSubsetAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DtdParserProxy_PushExternalSubsetAsync>d__.<>4__this = this;
			<DtdParserProxy_PushExternalSubsetAsync>d__.systemId = systemId;
			<DtdParserProxy_PushExternalSubsetAsync>d__.publicId = publicId;
			<DtdParserProxy_PushExternalSubsetAsync>d__.<>1__state = -1;
			<DtdParserProxy_PushExternalSubsetAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_PushExternalSubsetAsync>d__497>(ref <DtdParserProxy_PushExternalSubsetAsync>d__);
			return <DtdParserProxy_PushExternalSubsetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00032293 File Offset: 0x00030493
		private Task InitStreamInputAsync(Uri baseUri, Stream stream, Encoding encoding)
		{
			return this.InitStreamInputAsync(baseUri, baseUri.ToString(), stream, null, 0, encoding);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000322A6 File Offset: 0x000304A6
		private Task InitStreamInputAsync(Uri baseUri, string baseUriStr, Stream stream, Encoding encoding)
		{
			return this.InitStreamInputAsync(baseUri, baseUriStr, stream, null, 0, encoding);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000322B8 File Offset: 0x000304B8
		private Task InitStreamInputAsync(Uri baseUri, string baseUriStr, Stream stream, byte[] bytes, int byteCount, Encoding encoding)
		{
			XmlTextReaderImpl.<InitStreamInputAsync>d__500 <InitStreamInputAsync>d__;
			<InitStreamInputAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitStreamInputAsync>d__.<>4__this = this;
			<InitStreamInputAsync>d__.baseUri = baseUri;
			<InitStreamInputAsync>d__.baseUriStr = baseUriStr;
			<InitStreamInputAsync>d__.stream = stream;
			<InitStreamInputAsync>d__.bytes = bytes;
			<InitStreamInputAsync>d__.byteCount = byteCount;
			<InitStreamInputAsync>d__.encoding = encoding;
			<InitStreamInputAsync>d__.<>1__state = -1;
			<InitStreamInputAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<InitStreamInputAsync>d__500>(ref <InitStreamInputAsync>d__);
			return <InitStreamInputAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0003232E File Offset: 0x0003052E
		private Task InitTextReaderInputAsync(string baseUriStr, TextReader input)
		{
			return this.InitTextReaderInputAsync(baseUriStr, null, input);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0003233C File Offset: 0x0003053C
		private Task InitTextReaderInputAsync(string baseUriStr, Uri baseUri, TextReader input)
		{
			this.ps.textReader = input;
			this.ps.baseUriStr = baseUriStr;
			this.ps.baseUri = baseUri;
			if (this.ps.chars == null)
			{
				int num;
				if (this.laterInitParam != null && this.laterInitParam.useAsync)
				{
					num = 65536;
				}
				else
				{
					num = 4096;
				}
				this.ps.chars = new char[num + 1];
			}
			this.ps.encoding = Encoding.Unicode;
			this.ps.eolNormalized = !this.normalize;
			this.ps.appendMode = true;
			return this.ReadDataAsync();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000323E8 File Offset: 0x000305E8
		private Task ProcessDtdFromParserContextAsync(XmlParserContext context)
		{
			switch (this.dtdProcessing)
			{
			case DtdProcessing.Prohibit:
				this.ThrowWithoutLineInfo("Xml_DtdIsProhibitedEx");
				break;
			case DtdProcessing.Parse:
				return this.ParseDtdFromParserContextAsync();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0003242C File Offset: 0x0003062C
		private Task SwitchEncodingAsync(Encoding newEncoding)
		{
			if ((newEncoding.WebName != this.ps.encoding.WebName || this.ps.decoder is SafeAsciiDecoder) && !this.afterResetState)
			{
				this.UnDecodeChars();
				this.ps.appendMode = false;
				this.SetupEncoding(newEncoding);
				return this.ReadDataAsync();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00032495 File Offset: 0x00030695
		private Task SwitchEncodingToUTF8Async()
		{
			return this.SwitchEncodingAsync(new UTF8Encoding(true, true));
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x000324A4 File Offset: 0x000306A4
		private Task<int> ReadDataAsync()
		{
			XmlTextReaderImpl.<ReadDataAsync>d__506 <ReadDataAsync>d__;
			<ReadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadDataAsync>d__.<>4__this = this;
			<ReadDataAsync>d__.<>1__state = -1;
			<ReadDataAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadDataAsync>d__506>(ref <ReadDataAsync>d__);
			return <ReadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x000324E8 File Offset: 0x000306E8
		private Task<bool> ParseXmlDeclarationAsync(bool isTextDecl)
		{
			XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__507 <ParseXmlDeclarationAsync>d__;
			<ParseXmlDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseXmlDeclarationAsync>d__.<>4__this = this;
			<ParseXmlDeclarationAsync>d__.isTextDecl = isTextDecl;
			<ParseXmlDeclarationAsync>d__.<>1__state = -1;
			<ParseXmlDeclarationAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__507>(ref <ParseXmlDeclarationAsync>d__);
			return <ParseXmlDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00032534 File Offset: 0x00030734
		private Task<bool> ParseDocumentContentAsync()
		{
			bool needMoreChars;
			int num;
			char[] chars;
			char c;
			for (;;)
			{
				needMoreChars = false;
				num = this.ps.charPos;
				chars = this.ps.chars;
				if (chars[num] != '<')
				{
					goto IL_24E;
				}
				needMoreChars = true;
				if (this.ps.charsUsed - num < 4)
				{
					break;
				}
				num++;
				c = chars[num];
				if (c != '!')
				{
					if (c != '/')
					{
						goto Block_3;
					}
					this.Throw(num + 1, "Xml_UnexpectedEndTag");
				}
				else
				{
					num++;
					if (this.ps.charsUsed - num < 2)
					{
						goto Block_5;
					}
					if (chars[num] == '-')
					{
						if (chars[num + 1] == '-')
						{
							goto Block_7;
						}
						this.ThrowUnexpectedToken(num + 1, "-");
					}
					else if (chars[num] == '[')
					{
						if (this.fragmentType != XmlNodeType.Document)
						{
							num++;
							if (this.ps.charsUsed - num < 6)
							{
								goto Block_10;
							}
							if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
							{
								goto Block_11;
							}
							this.ThrowUnexpectedToken(num, "CDATA[");
						}
						else
						{
							this.Throw(this.ps.charPos, "Xml_InvalidRootData");
						}
					}
					else
					{
						if (this.fragmentType == XmlNodeType.Document || this.fragmentType == XmlNodeType.None)
						{
							goto IL_189;
						}
						if (this.ParseUnexpectedToken(num) == "DOCTYPE")
						{
							this.Throw("Xml_BadDTDLocation");
						}
						else
						{
							this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
						}
					}
				}
			}
			return this.ParseDocumentContentAsync_ReadData(needMoreChars);
			Block_3:
			if (c == '?')
			{
				this.ps.charPos = num + 1;
				return this.ParsePIAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseDocumentContentAsync));
			}
			if (this.rootElementParsed)
			{
				if (this.fragmentType == XmlNodeType.Document)
				{
					this.Throw(num, "Xml_MultipleRoots");
				}
				if (this.fragmentType == XmlNodeType.None)
				{
					this.fragmentType = XmlNodeType.Element;
				}
			}
			this.ps.charPos = num;
			this.rootElementParsed = true;
			return this.ParseElementAsync().ReturnTaskBoolWhenFinish(true);
			Block_5:
			return this.ParseDocumentContentAsync_ReadData(needMoreChars);
			Block_7:
			this.ps.charPos = num + 2;
			return this.ParseCommentAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseDocumentContentAsync));
			Block_10:
			return this.ParseDocumentContentAsync_ReadData(needMoreChars);
			Block_11:
			this.ps.charPos = num + 6;
			return this.ParseCDataAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ParseDocumentContentAsync_CData));
			IL_189:
			this.fragmentType = XmlNodeType.Document;
			this.ps.charPos = num;
			return this.ParseDoctypeDeclAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseDocumentContentAsync));
			IL_24E:
			if (chars[num] == '&')
			{
				return this.ParseDocumentContentAsync_ParseEntity();
			}
			if (num == this.ps.charsUsed || (this.v1Compat && chars[num] == '\0'))
			{
				return this.ParseDocumentContentAsync_ReadData(needMoreChars);
			}
			if (this.fragmentType == XmlNodeType.Document)
			{
				return this.ParseRootLevelWhitespaceAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseDocumentContentAsync));
			}
			return this.ParseDocumentContentAsync_WhiteSpace();
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000327E8 File Offset: 0x000309E8
		private Task<bool> ParseDocumentContentAsync_CData()
		{
			if (this.fragmentType == XmlNodeType.None)
			{
				this.fragmentType = XmlNodeType.Element;
			}
			return AsyncHelper.DoneTaskTrue;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00032800 File Offset: 0x00030A00
		private Task<bool> ParseDocumentContentAsync_ParseEntity()
		{
			XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__510 <ParseDocumentContentAsync_ParseEntity>d__;
			<ParseDocumentContentAsync_ParseEntity>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseDocumentContentAsync_ParseEntity>d__.<>4__this = this;
			<ParseDocumentContentAsync_ParseEntity>d__.<>1__state = -1;
			<ParseDocumentContentAsync_ParseEntity>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__510>(ref <ParseDocumentContentAsync_ParseEntity>d__);
			return <ParseDocumentContentAsync_ParseEntity>d__.<>t__builder.Task;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00032844 File Offset: 0x00030A44
		private Task<bool> ParseDocumentContentAsync_WhiteSpace()
		{
			Task<bool> task = this.ParseTextAsync();
			if (!task.IsSuccess())
			{
				return this._ParseDocumentContentAsync_WhiteSpace(task);
			}
			if (task.Result)
			{
				if (this.fragmentType == XmlNodeType.None && this.curNode.type == XmlNodeType.Text)
				{
					this.fragmentType = XmlNodeType.Element;
				}
				return AsyncHelper.DoneTaskTrue;
			}
			return this.ParseDocumentContentAsync();
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0003289C File Offset: 0x00030A9C
		private Task<bool> _ParseDocumentContentAsync_WhiteSpace(Task<bool> task)
		{
			XmlTextReaderImpl.<_ParseDocumentContentAsync_WhiteSpace>d__512 <_ParseDocumentContentAsync_WhiteSpace>d__;
			<_ParseDocumentContentAsync_WhiteSpace>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ParseDocumentContentAsync_WhiteSpace>d__.<>4__this = this;
			<_ParseDocumentContentAsync_WhiteSpace>d__.task = task;
			<_ParseDocumentContentAsync_WhiteSpace>d__.<>1__state = -1;
			<_ParseDocumentContentAsync_WhiteSpace>d__.<>t__builder.Start<XmlTextReaderImpl.<_ParseDocumentContentAsync_WhiteSpace>d__512>(ref <_ParseDocumentContentAsync_WhiteSpace>d__);
			return <_ParseDocumentContentAsync_WhiteSpace>d__.<>t__builder.Task;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000328E8 File Offset: 0x00030AE8
		private Task<bool> ParseDocumentContentAsync_ReadData(bool needMoreChars)
		{
			XmlTextReaderImpl.<ParseDocumentContentAsync_ReadData>d__513 <ParseDocumentContentAsync_ReadData>d__;
			<ParseDocumentContentAsync_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseDocumentContentAsync_ReadData>d__.<>4__this = this;
			<ParseDocumentContentAsync_ReadData>d__.needMoreChars = needMoreChars;
			<ParseDocumentContentAsync_ReadData>d__.<>1__state = -1;
			<ParseDocumentContentAsync_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDocumentContentAsync_ReadData>d__513>(ref <ParseDocumentContentAsync_ReadData>d__);
			return <ParseDocumentContentAsync_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00032934 File Offset: 0x00030B34
		private Task<bool> ParseElementContentAsync()
		{
			int num;
			char c2;
			for (;;)
			{
				num = this.ps.charPos;
				char[] chars = this.ps.chars;
				char c = chars[num];
				if (c == '&')
				{
					goto IL_1B4;
				}
				if (c != '<')
				{
					goto IL_1CC;
				}
				c2 = chars[num + 1];
				if (c2 != '!')
				{
					break;
				}
				num += 2;
				if (this.ps.charsUsed - num < 2)
				{
					goto Block_5;
				}
				if (chars[num] == '-')
				{
					if (chars[num + 1] == '-')
					{
						goto Block_7;
					}
					this.ThrowUnexpectedToken(num + 1, "-");
				}
				else if (chars[num] == '[')
				{
					num++;
					if (this.ps.charsUsed - num < 6)
					{
						goto Block_9;
					}
					if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
					{
						goto Block_10;
					}
					this.ThrowUnexpectedToken(num, "CDATA[");
				}
				else if (this.ParseUnexpectedToken(num) == "DOCTYPE")
				{
					this.Throw("Xml_BadDTDLocation");
				}
				else
				{
					this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
				}
			}
			if (c2 == '/')
			{
				this.ps.charPos = num + 2;
				return this.ParseEndElementAsync().ReturnTaskBoolWhenFinish(true);
			}
			if (c2 == '?')
			{
				this.ps.charPos = num + 2;
				return this.ParsePIAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseElementContentAsync));
			}
			if (num + 1 == this.ps.charsUsed)
			{
				return this.ParseElementContent_ReadData();
			}
			this.ps.charPos = num + 1;
			return this.ParseElementAsync().ReturnTaskBoolWhenFinish(true);
			Block_5:
			return this.ParseElementContent_ReadData();
			Block_7:
			this.ps.charPos = num + 2;
			return this.ParseCommentAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseElementContentAsync));
			Block_9:
			return this.ParseElementContent_ReadData();
			Block_10:
			this.ps.charPos = num + 6;
			return this.ParseCDataAsync().ReturnTaskBoolWhenFinish(true);
			IL_1B4:
			return this.ParseTextAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseElementContentAsync));
			IL_1CC:
			if (num == this.ps.charsUsed)
			{
				return this.ParseElementContent_ReadData();
			}
			return this.ParseTextAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseElementContentAsync));
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00032B3C File Offset: 0x00030D3C
		private Task<bool> ParseElementContent_ReadData()
		{
			XmlTextReaderImpl.<ParseElementContent_ReadData>d__515 <ParseElementContent_ReadData>d__;
			<ParseElementContent_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseElementContent_ReadData>d__.<>4__this = this;
			<ParseElementContent_ReadData>d__.<>1__state = -1;
			<ParseElementContent_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseElementContent_ReadData>d__515>(ref <ParseElementContent_ReadData>d__);
			return <ParseElementContent_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00032B80 File Offset: 0x00030D80
		private unsafe Task ParseElementAsync()
		{
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = -1;
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			while ((this.xmlCharType.charProperties[chars[num]] & 4) != 0)
			{
				num++;
				for (;;)
				{
					if ((this.xmlCharType.charProperties[chars[num]] & 8) != 0)
					{
						num++;
					}
					else
					{
						if (chars[num] != ':')
						{
							goto IL_A4;
						}
						if (num2 == -1)
						{
							break;
						}
						if (this.supportNamespaces)
						{
							goto Block_5;
						}
						num++;
					}
				}
				num2 = num;
				num++;
				continue;
				Block_5:
				this.Throw(num, "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(':', '\0'));
				break;
				IL_A4:
				if (num + 1 >= this.ps.charsUsed)
				{
					break;
				}
				return this.ParseElementAsync_SetElement(num2, num);
			}
			Task<Tuple<int, int>> task = this.ParseQNameAsync();
			return this.ParseElementAsync_ContinueWithSetElement(task);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00032C58 File Offset: 0x00030E58
		private Task ParseElementAsync_ContinueWithSetElement(Task<Tuple<int, int>> task)
		{
			if (task.IsSuccess())
			{
				Tuple<int, int> result = task.Result;
				int item = result.Item1;
				int item2 = result.Item2;
				return this.ParseElementAsync_SetElement(item, item2);
			}
			return this._ParseElementAsync_ContinueWithSetElement(task);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00032C94 File Offset: 0x00030E94
		private Task _ParseElementAsync_ContinueWithSetElement(Task<Tuple<int, int>> task)
		{
			XmlTextReaderImpl.<_ParseElementAsync_ContinueWithSetElement>d__518 <_ParseElementAsync_ContinueWithSetElement>d__;
			<_ParseElementAsync_ContinueWithSetElement>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_ParseElementAsync_ContinueWithSetElement>d__.<>4__this = this;
			<_ParseElementAsync_ContinueWithSetElement>d__.task = task;
			<_ParseElementAsync_ContinueWithSetElement>d__.<>1__state = -1;
			<_ParseElementAsync_ContinueWithSetElement>d__.<>t__builder.Start<XmlTextReaderImpl.<_ParseElementAsync_ContinueWithSetElement>d__518>(ref <_ParseElementAsync_ContinueWithSetElement>d__);
			return <_ParseElementAsync_ContinueWithSetElement>d__.<>t__builder.Task;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00032CE0 File Offset: 0x00030EE0
		private unsafe Task ParseElementAsync_SetElement(int colonPos, int pos)
		{
			char[] chars = this.ps.chars;
			this.namespaceManager.PushScope();
			if (colonPos == -1 || !this.supportNamespaces)
			{
				this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, this.ps.charPos, pos - this.ps.charPos));
			}
			else
			{
				int charPos = this.ps.charPos;
				int num = colonPos - charPos;
				if (num == this.lastPrefix.Length && XmlConvert.StrEqual(chars, charPos, num, this.lastPrefix))
				{
					this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, colonPos + 1, pos - colonPos - 1), this.lastPrefix, null);
				}
				else
				{
					this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, colonPos + 1, pos - colonPos - 1), this.nameTable.Add(chars, this.ps.charPos, num), null);
					this.lastPrefix = this.curNode.prefix;
				}
			}
			char c = chars[pos];
			bool flag = (this.xmlCharType.charProperties[c] & 1) > 0;
			this.ps.charPos = pos;
			if (flag)
			{
				return this.ParseAttributesAsync();
			}
			return this.ParseElementAsync_NoAttributes();
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00032E20 File Offset: 0x00031020
		private Task ParseElementAsync_NoAttributes()
		{
			int charPos = this.ps.charPos;
			char[] chars = this.ps.chars;
			char c = chars[charPos];
			if (c == '>')
			{
				this.ps.charPos = charPos + 1;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
			}
			else if (c == '/')
			{
				if (charPos + 1 == this.ps.charsUsed)
				{
					this.ps.charPos = charPos;
					return this.ParseElementAsync_ReadData(charPos);
				}
				if (chars[charPos + 1] == '>')
				{
					this.curNode.IsEmptyElement = true;
					this.nextParsingFunction = this.parsingFunction;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
					this.ps.charPos = charPos + 2;
				}
				else
				{
					this.ThrowUnexpectedToken(charPos, ">");
				}
			}
			else
			{
				this.Throw(charPos, "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(chars, this.ps.charsUsed, charPos));
			}
			if (this.addDefaultAttributesAndNormalize)
			{
				this.AddDefaultAttributesAndNormalize();
			}
			this.ElementNamespaceLookup();
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00032F10 File Offset: 0x00031110
		private Task ParseElementAsync_ReadData(int pos)
		{
			XmlTextReaderImpl.<ParseElementAsync_ReadData>d__521 <ParseElementAsync_ReadData>d__;
			<ParseElementAsync_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseElementAsync_ReadData>d__.<>4__this = this;
			<ParseElementAsync_ReadData>d__.pos = pos;
			<ParseElementAsync_ReadData>d__.<>1__state = -1;
			<ParseElementAsync_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseElementAsync_ReadData>d__521>(ref <ParseElementAsync_ReadData>d__);
			return <ParseElementAsync_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00032F5C File Offset: 0x0003115C
		private Task ParseEndElementAsync()
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[this.index - 1];
			int length = nodeData.prefix.Length;
			int length2 = nodeData.localName.Length;
			if (this.ps.charsUsed - this.ps.charPos < length + length2 + 1)
			{
				return this._ParseEndElmentAsync();
			}
			return this.ParseEndElementAsync_CheckNameAndParse();
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00032FBC File Offset: 0x000311BC
		private Task _ParseEndElmentAsync()
		{
			XmlTextReaderImpl.<_ParseEndElmentAsync>d__523 <_ParseEndElmentAsync>d__;
			<_ParseEndElmentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_ParseEndElmentAsync>d__.<>4__this = this;
			<_ParseEndElmentAsync>d__.<>1__state = -1;
			<_ParseEndElmentAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<_ParseEndElmentAsync>d__523>(ref <_ParseEndElmentAsync>d__);
			return <_ParseEndElmentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00033000 File Offset: 0x00031200
		private Task ParseEndElmentAsync_PrepareData()
		{
			XmlTextReaderImpl.<ParseEndElmentAsync_PrepareData>d__524 <ParseEndElmentAsync_PrepareData>d__;
			<ParseEndElmentAsync_PrepareData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEndElmentAsync_PrepareData>d__.<>4__this = this;
			<ParseEndElmentAsync_PrepareData>d__.<>1__state = -1;
			<ParseEndElmentAsync_PrepareData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEndElmentAsync_PrepareData>d__524>(ref <ParseEndElmentAsync_PrepareData>d__);
			return <ParseEndElmentAsync_PrepareData>d__.<>t__builder.Task;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00033044 File Offset: 0x00031244
		private Task ParseEndElementAsync_CheckNameAndParse()
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[this.index - 1];
			int length = nodeData.prefix.Length;
			int length2 = nodeData.localName.Length;
			char[] chars = this.ps.chars;
			int nameLen;
			if (nodeData.prefix.Length == 0)
			{
				if (!XmlConvert.StrEqual(chars, this.ps.charPos, length2, nodeData.localName))
				{
					return this.ThrowTagMismatchAsync(nodeData);
				}
				nameLen = length2;
			}
			else
			{
				int num = this.ps.charPos + length;
				if (!XmlConvert.StrEqual(chars, this.ps.charPos, length, nodeData.prefix) || chars[num] != ':' || !XmlConvert.StrEqual(chars, num + 1, length2, nodeData.localName))
				{
					return this.ThrowTagMismatchAsync(nodeData);
				}
				nameLen = length2 + length + 1;
			}
			LineInfo endTagLineInfo = new LineInfo(this.ps.lineNo, this.ps.LinePos);
			return this.ParseEndElementAsync_Finish(nameLen, nodeData, endTagLineInfo);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00033138 File Offset: 0x00031338
		private Task ParseEndElementAsync_Finish(int nameLen, XmlTextReaderImpl.NodeData startTagNode, LineInfo endTagLineInfo)
		{
			Task task = this.ParseEndElementAsync_CheckEndTag(nameLen, startTagNode, endTagLineInfo);
			while (task.IsSuccess())
			{
				switch (this.parseEndElement_NextFunc)
				{
				case XmlTextReaderImpl.ParseEndElementParseFunction.CheckEndTag:
					task = this.ParseEndElementAsync_CheckEndTag(nameLen, startTagNode, endTagLineInfo);
					break;
				case XmlTextReaderImpl.ParseEndElementParseFunction.ReadData:
					task = this.ParseEndElementAsync_ReadData();
					break;
				case XmlTextReaderImpl.ParseEndElementParseFunction.Done:
					return task;
				}
			}
			return this.ParseEndElementAsync_Finish(task, nameLen, startTagNode, endTagLineInfo);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00033194 File Offset: 0x00031394
		private Task ParseEndElementAsync_Finish(Task task, int nameLen, XmlTextReaderImpl.NodeData startTagNode, LineInfo endTagLineInfo)
		{
			XmlTextReaderImpl.<ParseEndElementAsync_Finish>d__529 <ParseEndElementAsync_Finish>d__;
			<ParseEndElementAsync_Finish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEndElementAsync_Finish>d__.<>4__this = this;
			<ParseEndElementAsync_Finish>d__.task = task;
			<ParseEndElementAsync_Finish>d__.nameLen = nameLen;
			<ParseEndElementAsync_Finish>d__.startTagNode = startTagNode;
			<ParseEndElementAsync_Finish>d__.endTagLineInfo = endTagLineInfo;
			<ParseEndElementAsync_Finish>d__.<>1__state = -1;
			<ParseEndElementAsync_Finish>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEndElementAsync_Finish>d__529>(ref <ParseEndElementAsync_Finish>d__);
			return <ParseEndElementAsync_Finish>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000331F8 File Offset: 0x000313F8
		private unsafe Task ParseEndElementAsync_CheckEndTag(int nameLen, XmlTextReaderImpl.NodeData startTagNode, LineInfo endTagLineInfo)
		{
			int num;
			for (;;)
			{
				num = this.ps.charPos + nameLen;
				char[] chars = this.ps.chars;
				if (num == this.ps.charsUsed)
				{
					break;
				}
				bool flag = false;
				if ((this.xmlCharType.charProperties[chars[num]] & 8) != 0 || chars[num] == ':')
				{
					flag = true;
				}
				if (flag)
				{
					goto Block_2;
				}
				if (chars[num] != '>')
				{
					char c;
					while (this.xmlCharType.IsWhiteSpace(c = chars[num]))
					{
						num++;
						if (c != '\n')
						{
							if (c == '\r')
							{
								if (chars[num] == '\n')
								{
									num++;
								}
								else if (num == this.ps.charsUsed && !this.ps.isEof)
								{
									continue;
								}
								this.OnNewLine(num);
							}
						}
						else
						{
							this.OnNewLine(num);
						}
					}
				}
				if (chars[num] == '>')
				{
					goto IL_F5;
				}
				if (num == this.ps.charsUsed)
				{
					goto Block_9;
				}
				this.ThrowUnexpectedToken(num, ">");
			}
			this.parseEndElement_NextFunc = XmlTextReaderImpl.ParseEndElementParseFunction.ReadData;
			return AsyncHelper.DoneTask;
			Block_2:
			return this.ThrowTagMismatchAsync(startTagNode);
			Block_9:
			this.parseEndElement_NextFunc = XmlTextReaderImpl.ParseEndElementParseFunction.ReadData;
			return AsyncHelper.DoneTask;
			IL_F5:
			this.index--;
			this.curNode = this.nodes[this.index];
			startTagNode.lineInfo = endTagLineInfo;
			startTagNode.type = XmlNodeType.EndElement;
			this.ps.charPos = num + 1;
			this.nextParsingFunction = ((this.index > 0) ? this.parsingFunction : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopElementContext;
			this.parseEndElement_NextFunc = XmlTextReaderImpl.ParseEndElementParseFunction.Done;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00033364 File Offset: 0x00031564
		private Task ParseEndElementAsync_ReadData()
		{
			XmlTextReaderImpl.<ParseEndElementAsync_ReadData>d__531 <ParseEndElementAsync_ReadData>d__;
			<ParseEndElementAsync_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEndElementAsync_ReadData>d__.<>4__this = this;
			<ParseEndElementAsync_ReadData>d__.<>1__state = -1;
			<ParseEndElementAsync_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEndElementAsync_ReadData>d__531>(ref <ParseEndElementAsync_ReadData>d__);
			return <ParseEndElementAsync_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000333A8 File Offset: 0x000315A8
		private Task ThrowTagMismatchAsync(XmlTextReaderImpl.NodeData startTag)
		{
			XmlTextReaderImpl.<ThrowTagMismatchAsync>d__532 <ThrowTagMismatchAsync>d__;
			<ThrowTagMismatchAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ThrowTagMismatchAsync>d__.<>4__this = this;
			<ThrowTagMismatchAsync>d__.startTag = startTag;
			<ThrowTagMismatchAsync>d__.<>1__state = -1;
			<ThrowTagMismatchAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ThrowTagMismatchAsync>d__532>(ref <ThrowTagMismatchAsync>d__);
			return <ThrowTagMismatchAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000333F4 File Offset: 0x000315F4
		private Task ParseAttributesAsync()
		{
			XmlTextReaderImpl.<ParseAttributesAsync>d__533 <ParseAttributesAsync>d__;
			<ParseAttributesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttributesAsync>d__.<>4__this = this;
			<ParseAttributesAsync>d__.<>1__state = -1;
			<ParseAttributesAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseAttributesAsync>d__533>(ref <ParseAttributesAsync>d__);
			return <ParseAttributesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00033438 File Offset: 0x00031638
		private Task ParseAttributeValueSlowAsync(int curPos, char quoteChar, XmlTextReaderImpl.NodeData attr)
		{
			XmlTextReaderImpl.<ParseAttributeValueSlowAsync>d__534 <ParseAttributeValueSlowAsync>d__;
			<ParseAttributeValueSlowAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttributeValueSlowAsync>d__.<>4__this = this;
			<ParseAttributeValueSlowAsync>d__.curPos = curPos;
			<ParseAttributeValueSlowAsync>d__.quoteChar = quoteChar;
			<ParseAttributeValueSlowAsync>d__.attr = attr;
			<ParseAttributeValueSlowAsync>d__.<>1__state = -1;
			<ParseAttributeValueSlowAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseAttributeValueSlowAsync>d__534>(ref <ParseAttributeValueSlowAsync>d__);
			return <ParseAttributeValueSlowAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00033494 File Offset: 0x00031694
		private Task<bool> ParseTextAsync()
		{
			int num = 0;
			if (this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
			{
				return this._ParseTextAsync(null);
			}
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			Task<Tuple<int, int, int, bool>> task = this.ParseTextAsync(num);
			if (!task.IsSuccess())
			{
				return this._ParseTextAsync(task);
			}
			Tuple<int, int, int, bool> result = task.Result;
			int item = result.Item1;
			int item2 = result.Item2;
			num = result.Item3;
			bool item3 = result.Item4;
			if (!item3)
			{
				return this._ParseTextAsync(task);
			}
			if (item2 - item == 0)
			{
				return this.ParseTextAsync_IgnoreNode();
			}
			XmlNodeType textNodeType = this.GetTextNodeType(num);
			if (textNodeType == XmlNodeType.None)
			{
				return this.ParseTextAsync_IgnoreNode();
			}
			this.curNode.SetValueNode(textNodeType, this.ps.chars, item, item2 - item);
			return AsyncHelper.DoneTaskTrue;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00033568 File Offset: 0x00031768
		private Task<bool> _ParseTextAsync(Task<Tuple<int, int, int, bool>> parseTask)
		{
			XmlTextReaderImpl.<_ParseTextAsync>d__536 <_ParseTextAsync>d__;
			<_ParseTextAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ParseTextAsync>d__.<>4__this = this;
			<_ParseTextAsync>d__.parseTask = parseTask;
			<_ParseTextAsync>d__.<>1__state = -1;
			<_ParseTextAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<_ParseTextAsync>d__536>(ref <_ParseTextAsync>d__);
			return <_ParseTextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000335B4 File Offset: 0x000317B4
		private Task<bool> ParseTextAsync_IgnoreNode()
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ReportEndEntity)
			{
				this.SetupEndEntityNodeInContent();
				this.parsingFunction = this.nextParsingFunction;
				return AsyncHelper.DoneTaskTrue;
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
			{
				this.parsingFunction = this.nextNextParsingFunction;
				return this.ParseEntityReferenceAsync().ReturnTaskBoolWhenFinish(true);
			}
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003360C File Offset: 0x0003180C
		private Task<Tuple<int, int, int, bool>> ParseTextAsync(int outOrChars)
		{
			Task<Tuple<int, int, int, bool>> task = this.ParseTextAsync(outOrChars, this.ps.chars, this.ps.charPos, 0, -1, outOrChars, '\0');
			while (task.IsSuccess())
			{
				outOrChars = this.lastParseTextState.outOrChars;
				char[] chars = this.lastParseTextState.chars;
				int pos = this.lastParseTextState.pos;
				int rcount = this.lastParseTextState.rcount;
				int rpos = this.lastParseTextState.rpos;
				int orChars = this.lastParseTextState.orChars;
				char c = this.lastParseTextState.c;
				switch (this.parseText_NextFunction)
				{
				case XmlTextReaderImpl.ParseTextFunction.ParseText:
					task = this.ParseTextAsync(outOrChars, chars, pos, rcount, rpos, orChars, c);
					break;
				case XmlTextReaderImpl.ParseTextFunction.Entity:
					task = this.ParseTextAsync_ParseEntity(outOrChars, chars, pos, rcount, rpos, orChars, c);
					break;
				case XmlTextReaderImpl.ParseTextFunction.Surrogate:
					task = this.ParseTextAsync_Surrogate(outOrChars, chars, pos, rcount, rpos, orChars, c);
					break;
				case XmlTextReaderImpl.ParseTextFunction.ReadData:
					task = this.ParseTextAsync_ReadData(outOrChars, chars, pos, rcount, rpos, orChars, c);
					break;
				case XmlTextReaderImpl.ParseTextFunction.NoValue:
					return this.ParseTextAsync_NoValue(outOrChars, pos);
				case XmlTextReaderImpl.ParseTextFunction.PartialValue:
					return this.ParseTextAsync_PartialValue(pos, rcount, rpos, orChars, c);
				}
			}
			return this.ParseTextAsync_AsyncFunc(task);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00033740 File Offset: 0x00031940
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_AsyncFunc(Task<Tuple<int, int, int, bool>> task)
		{
			XmlTextReaderImpl.<ParseTextAsync_AsyncFunc>d__544 <ParseTextAsync_AsyncFunc>d__;
			<ParseTextAsync_AsyncFunc>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, int, bool>>.Create();
			<ParseTextAsync_AsyncFunc>d__.<>4__this = this;
			<ParseTextAsync_AsyncFunc>d__.task = task;
			<ParseTextAsync_AsyncFunc>d__.<>1__state = -1;
			<ParseTextAsync_AsyncFunc>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseTextAsync_AsyncFunc>d__544>(ref <ParseTextAsync_AsyncFunc>d__);
			return <ParseTextAsync_AsyncFunc>d__.<>t__builder.Task;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0003378C File Offset: 0x0003198C
		private unsafe Task<Tuple<int, int, int, bool>> ParseTextAsync(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
		{
			for (;;)
			{
				if ((this.xmlCharType.charProperties[c = chars[pos]] & 64) == 0)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							pos++;
							continue;
						case '\n':
							pos++;
							this.OnNewLine(pos);
							continue;
						case '\v':
						case '\f':
							goto IL_215;
						case '\r':
							if (chars[pos + 1] == '\n')
							{
								if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
								{
									if (pos - this.ps.charPos > 0)
									{
										if (rcount == 0)
										{
											rcount = 1;
											rpos = pos;
										}
										else
										{
											this.ShiftBuffer(rpos + rcount, rpos, pos - rpos - rcount);
											rpos = pos - rcount;
											rcount++;
										}
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
									}
								}
								pos += 2;
							}
							else
							{
								if (pos + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_12D;
								}
								if (!this.ps.eolNormalized)
								{
									chars[pos] = '\n';
								}
								pos++;
							}
							this.OnNewLine(pos);
							continue;
						}
						break;
					}
					if (c == '<')
					{
						goto IL_15D;
					}
					if (c != ']')
					{
						goto Block_6;
					}
					if (this.ps.charsUsed - pos < 3 && !this.ps.isEof)
					{
						goto Block_15;
					}
					if (chars[pos + 1] == ']' && chars[pos + 2] == '>')
					{
						this.Throw(pos, "Xml_CDATAEndInText");
					}
					orChars |= 93;
					pos++;
				}
				else
				{
					orChars |= (int)c;
					pos++;
				}
			}
			if (c == '&')
			{
				this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
				this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.Entity;
				return this.parseText_dummyTask;
			}
			Block_6:
			goto IL_215;
			IL_12D:
			this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
			this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ReadData;
			return this.parseText_dummyTask;
			IL_15D:
			this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
			this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.PartialValue;
			return this.parseText_dummyTask;
			Block_15:
			this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
			this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ReadData;
			return this.parseText_dummyTask;
			IL_215:
			if (pos == this.ps.charsUsed)
			{
				this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
				this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ReadData;
				return this.parseText_dummyTask;
			}
			this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
			this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.Surrogate;
			return this.parseText_dummyTask;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00033A04 File Offset: 0x00031C04
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_ParseEntity(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
		{
			XmlTextReaderImpl.<ParseTextAsync_ParseEntity>d__546 <ParseTextAsync_ParseEntity>d__;
			<ParseTextAsync_ParseEntity>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, int, bool>>.Create();
			<ParseTextAsync_ParseEntity>d__.<>4__this = this;
			<ParseTextAsync_ParseEntity>d__.outOrChars = outOrChars;
			<ParseTextAsync_ParseEntity>d__.chars = chars;
			<ParseTextAsync_ParseEntity>d__.pos = pos;
			<ParseTextAsync_ParseEntity>d__.rcount = rcount;
			<ParseTextAsync_ParseEntity>d__.rpos = rpos;
			<ParseTextAsync_ParseEntity>d__.orChars = orChars;
			<ParseTextAsync_ParseEntity>d__.c = c;
			<ParseTextAsync_ParseEntity>d__.<>1__state = -1;
			<ParseTextAsync_ParseEntity>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseTextAsync_ParseEntity>d__546>(ref <ParseTextAsync_ParseEntity>d__);
			return <ParseTextAsync_ParseEntity>d__.<>t__builder.Task;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00033A84 File Offset: 0x00031C84
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_Surrogate(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
		{
			XmlTextReaderImpl.<ParseTextAsync_Surrogate>d__547 <ParseTextAsync_Surrogate>d__;
			<ParseTextAsync_Surrogate>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, int, bool>>.Create();
			<ParseTextAsync_Surrogate>d__.<>4__this = this;
			<ParseTextAsync_Surrogate>d__.outOrChars = outOrChars;
			<ParseTextAsync_Surrogate>d__.chars = chars;
			<ParseTextAsync_Surrogate>d__.pos = pos;
			<ParseTextAsync_Surrogate>d__.rcount = rcount;
			<ParseTextAsync_Surrogate>d__.rpos = rpos;
			<ParseTextAsync_Surrogate>d__.orChars = orChars;
			<ParseTextAsync_Surrogate>d__.c = c;
			<ParseTextAsync_Surrogate>d__.<>1__state = -1;
			<ParseTextAsync_Surrogate>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseTextAsync_Surrogate>d__547>(ref <ParseTextAsync_Surrogate>d__);
			return <ParseTextAsync_Surrogate>d__.<>t__builder.Task;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00033B04 File Offset: 0x00031D04
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_ReadData(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
		{
			XmlTextReaderImpl.<ParseTextAsync_ReadData>d__548 <ParseTextAsync_ReadData>d__;
			<ParseTextAsync_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, int, bool>>.Create();
			<ParseTextAsync_ReadData>d__.<>4__this = this;
			<ParseTextAsync_ReadData>d__.outOrChars = outOrChars;
			<ParseTextAsync_ReadData>d__.chars = chars;
			<ParseTextAsync_ReadData>d__.pos = pos;
			<ParseTextAsync_ReadData>d__.rcount = rcount;
			<ParseTextAsync_ReadData>d__.rpos = rpos;
			<ParseTextAsync_ReadData>d__.orChars = orChars;
			<ParseTextAsync_ReadData>d__.c = c;
			<ParseTextAsync_ReadData>d__.<>1__state = -1;
			<ParseTextAsync_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseTextAsync_ReadData>d__548>(ref <ParseTextAsync_ReadData>d__);
			return <ParseTextAsync_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00033B83 File Offset: 0x00031D83
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_NoValue(int outOrChars, int pos)
		{
			return Task.FromResult<Tuple<int, int, int, bool>>(new Tuple<int, int, int, bool>(pos, pos, outOrChars, true));
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00033B94 File Offset: 0x00031D94
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_PartialValue(int pos, int rcount, int rpos, int orChars, char c)
		{
			if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && rcount > 0)
			{
				this.ShiftBuffer(rpos + rcount, rpos, pos - rpos - rcount);
			}
			int charPos = this.ps.charPos;
			int item = pos - rcount;
			this.ps.charPos = pos;
			return Task.FromResult<Tuple<int, int, int, bool>>(new Tuple<int, int, int, bool>(charPos, item, orChars, c == '<'));
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00033BF0 File Offset: 0x00031DF0
		private Task FinishPartialValueAsync()
		{
			XmlTextReaderImpl.<FinishPartialValueAsync>d__551 <FinishPartialValueAsync>d__;
			<FinishPartialValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishPartialValueAsync>d__.<>4__this = this;
			<FinishPartialValueAsync>d__.<>1__state = -1;
			<FinishPartialValueAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishPartialValueAsync>d__551>(ref <FinishPartialValueAsync>d__);
			return <FinishPartialValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00033C34 File Offset: 0x00031E34
		private Task FinishOtherValueIteratorAsync()
		{
			XmlTextReaderImpl.<FinishOtherValueIteratorAsync>d__552 <FinishOtherValueIteratorAsync>d__;
			<FinishOtherValueIteratorAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishOtherValueIteratorAsync>d__.<>4__this = this;
			<FinishOtherValueIteratorAsync>d__.<>1__state = -1;
			<FinishOtherValueIteratorAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishOtherValueIteratorAsync>d__552>(ref <FinishOtherValueIteratorAsync>d__);
			return <FinishOtherValueIteratorAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00033C78 File Offset: 0x00031E78
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Task SkipPartialTextValueAsync()
		{
			XmlTextReaderImpl.<SkipPartialTextValueAsync>d__553 <SkipPartialTextValueAsync>d__;
			<SkipPartialTextValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipPartialTextValueAsync>d__.<>4__this = this;
			<SkipPartialTextValueAsync>d__.<>1__state = -1;
			<SkipPartialTextValueAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<SkipPartialTextValueAsync>d__553>(ref <SkipPartialTextValueAsync>d__);
			return <SkipPartialTextValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00033CBB File Offset: 0x00031EBB
		private Task FinishReadValueChunkAsync()
		{
			this.readValueOffset = 0;
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
			{
				return this.SkipPartialTextValueAsync();
			}
			this.parsingFunction = this.nextParsingFunction;
			this.nextParsingFunction = this.nextNextParsingFunction;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00033CF4 File Offset: 0x00031EF4
		private Task FinishReadContentAsBinaryAsync()
		{
			XmlTextReaderImpl.<FinishReadContentAsBinaryAsync>d__555 <FinishReadContentAsBinaryAsync>d__;
			<FinishReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishReadContentAsBinaryAsync>d__.<>4__this = this;
			<FinishReadContentAsBinaryAsync>d__.<>1__state = -1;
			<FinishReadContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishReadContentAsBinaryAsync>d__555>(ref <FinishReadContentAsBinaryAsync>d__);
			return <FinishReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00033D38 File Offset: 0x00031F38
		private Task FinishReadElementContentAsBinaryAsync()
		{
			XmlTextReaderImpl.<FinishReadElementContentAsBinaryAsync>d__556 <FinishReadElementContentAsBinaryAsync>d__;
			<FinishReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<FinishReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<FinishReadElementContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishReadElementContentAsBinaryAsync>d__556>(ref <FinishReadElementContentAsBinaryAsync>d__);
			return <FinishReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00033D7C File Offset: 0x00031F7C
		private Task<bool> ParseRootLevelWhitespaceAsync()
		{
			XmlTextReaderImpl.<ParseRootLevelWhitespaceAsync>d__557 <ParseRootLevelWhitespaceAsync>d__;
			<ParseRootLevelWhitespaceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseRootLevelWhitespaceAsync>d__.<>4__this = this;
			<ParseRootLevelWhitespaceAsync>d__.<>1__state = -1;
			<ParseRootLevelWhitespaceAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseRootLevelWhitespaceAsync>d__557>(ref <ParseRootLevelWhitespaceAsync>d__);
			return <ParseRootLevelWhitespaceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00033DC0 File Offset: 0x00031FC0
		private Task ParseEntityReferenceAsync()
		{
			XmlTextReaderImpl.<ParseEntityReferenceAsync>d__558 <ParseEntityReferenceAsync>d__;
			<ParseEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEntityReferenceAsync>d__.<>4__this = this;
			<ParseEntityReferenceAsync>d__.<>1__state = -1;
			<ParseEntityReferenceAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEntityReferenceAsync>d__558>(ref <ParseEntityReferenceAsync>d__);
			return <ParseEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00033E04 File Offset: 0x00032004
		private Task<Tuple<int, XmlTextReaderImpl.EntityType>> HandleEntityReferenceAsync(bool isInAttributeValue, XmlTextReaderImpl.EntityExpandType expandType)
		{
			XmlTextReaderImpl.<HandleEntityReferenceAsync>d__559 <HandleEntityReferenceAsync>d__;
			<HandleEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, XmlTextReaderImpl.EntityType>>.Create();
			<HandleEntityReferenceAsync>d__.<>4__this = this;
			<HandleEntityReferenceAsync>d__.isInAttributeValue = isInAttributeValue;
			<HandleEntityReferenceAsync>d__.expandType = expandType;
			<HandleEntityReferenceAsync>d__.<>1__state = -1;
			<HandleEntityReferenceAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<HandleEntityReferenceAsync>d__559>(ref <HandleEntityReferenceAsync>d__);
			return <HandleEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00033E58 File Offset: 0x00032058
		private Task<XmlTextReaderImpl.EntityType> HandleGeneralEntityReferenceAsync(string name, bool isInAttributeValue, bool pushFakeEntityIfNullResolver, int entityStartLinePos)
		{
			XmlTextReaderImpl.<HandleGeneralEntityReferenceAsync>d__560 <HandleGeneralEntityReferenceAsync>d__;
			<HandleGeneralEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<XmlTextReaderImpl.EntityType>.Create();
			<HandleGeneralEntityReferenceAsync>d__.<>4__this = this;
			<HandleGeneralEntityReferenceAsync>d__.name = name;
			<HandleGeneralEntityReferenceAsync>d__.isInAttributeValue = isInAttributeValue;
			<HandleGeneralEntityReferenceAsync>d__.pushFakeEntityIfNullResolver = pushFakeEntityIfNullResolver;
			<HandleGeneralEntityReferenceAsync>d__.entityStartLinePos = entityStartLinePos;
			<HandleGeneralEntityReferenceAsync>d__.<>1__state = -1;
			<HandleGeneralEntityReferenceAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<HandleGeneralEntityReferenceAsync>d__560>(ref <HandleGeneralEntityReferenceAsync>d__);
			return <HandleGeneralEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00033EBC File Offset: 0x000320BC
		private Task<bool> ParsePIAsync()
		{
			return this.ParsePIAsync(null);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00033EC8 File Offset: 0x000320C8
		private Task<bool> ParsePIAsync(StringBuilder piInDtdStringBuilder)
		{
			XmlTextReaderImpl.<ParsePIAsync>d__562 <ParsePIAsync>d__;
			<ParsePIAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParsePIAsync>d__.<>4__this = this;
			<ParsePIAsync>d__.piInDtdStringBuilder = piInDtdStringBuilder;
			<ParsePIAsync>d__.<>1__state = -1;
			<ParsePIAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParsePIAsync>d__562>(ref <ParsePIAsync>d__);
			return <ParsePIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00033F14 File Offset: 0x00032114
		private Task<Tuple<int, int, bool>> ParsePIValueAsync()
		{
			XmlTextReaderImpl.<ParsePIValueAsync>d__563 <ParsePIValueAsync>d__;
			<ParsePIValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, bool>>.Create();
			<ParsePIValueAsync>d__.<>4__this = this;
			<ParsePIValueAsync>d__.<>1__state = -1;
			<ParsePIValueAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParsePIValueAsync>d__563>(ref <ParsePIValueAsync>d__);
			return <ParsePIValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00033F58 File Offset: 0x00032158
		private Task<bool> ParseCommentAsync()
		{
			XmlTextReaderImpl.<ParseCommentAsync>d__564 <ParseCommentAsync>d__;
			<ParseCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseCommentAsync>d__.<>4__this = this;
			<ParseCommentAsync>d__.<>1__state = -1;
			<ParseCommentAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseCommentAsync>d__564>(ref <ParseCommentAsync>d__);
			return <ParseCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00033F9B File Offset: 0x0003219B
		private Task ParseCDataAsync()
		{
			return this.ParseCDataOrCommentAsync(XmlNodeType.CDATA);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00033FA4 File Offset: 0x000321A4
		private Task ParseCDataOrCommentAsync(XmlNodeType type)
		{
			XmlTextReaderImpl.<ParseCDataOrCommentAsync>d__566 <ParseCDataOrCommentAsync>d__;
			<ParseCDataOrCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseCDataOrCommentAsync>d__.<>4__this = this;
			<ParseCDataOrCommentAsync>d__.type = type;
			<ParseCDataOrCommentAsync>d__.<>1__state = -1;
			<ParseCDataOrCommentAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseCDataOrCommentAsync>d__566>(ref <ParseCDataOrCommentAsync>d__);
			return <ParseCDataOrCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00033FF0 File Offset: 0x000321F0
		private Task<Tuple<int, int, bool>> ParseCDataOrCommentTupleAsync(XmlNodeType type)
		{
			XmlTextReaderImpl.<ParseCDataOrCommentTupleAsync>d__567 <ParseCDataOrCommentTupleAsync>d__;
			<ParseCDataOrCommentTupleAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, bool>>.Create();
			<ParseCDataOrCommentTupleAsync>d__.<>4__this = this;
			<ParseCDataOrCommentTupleAsync>d__.type = type;
			<ParseCDataOrCommentTupleAsync>d__.<>1__state = -1;
			<ParseCDataOrCommentTupleAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseCDataOrCommentTupleAsync>d__567>(ref <ParseCDataOrCommentTupleAsync>d__);
			return <ParseCDataOrCommentTupleAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003403C File Offset: 0x0003223C
		private Task<bool> ParseDoctypeDeclAsync()
		{
			XmlTextReaderImpl.<ParseDoctypeDeclAsync>d__568 <ParseDoctypeDeclAsync>d__;
			<ParseDoctypeDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseDoctypeDeclAsync>d__.<>4__this = this;
			<ParseDoctypeDeclAsync>d__.<>1__state = -1;
			<ParseDoctypeDeclAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDoctypeDeclAsync>d__568>(ref <ParseDoctypeDeclAsync>d__);
			return <ParseDoctypeDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00034080 File Offset: 0x00032280
		private Task ParseDtdAsync()
		{
			XmlTextReaderImpl.<ParseDtdAsync>d__569 <ParseDtdAsync>d__;
			<ParseDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseDtdAsync>d__.<>4__this = this;
			<ParseDtdAsync>d__.<>1__state = -1;
			<ParseDtdAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDtdAsync>d__569>(ref <ParseDtdAsync>d__);
			return <ParseDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000340C4 File Offset: 0x000322C4
		private Task SkipDtdAsync()
		{
			XmlTextReaderImpl.<SkipDtdAsync>d__570 <SkipDtdAsync>d__;
			<SkipDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipDtdAsync>d__.<>4__this = this;
			<SkipDtdAsync>d__.<>1__state = -1;
			<SkipDtdAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<SkipDtdAsync>d__570>(ref <SkipDtdAsync>d__);
			return <SkipDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00034108 File Offset: 0x00032308
		private Task SkipPublicOrSystemIdLiteralAsync()
		{
			char c = this.ps.chars[this.ps.charPos];
			if (c != '"' && c != '\'')
			{
				this.ThrowUnexpectedToken("\"", "'");
			}
			this.ps.charPos = this.ps.charPos + 1;
			return this.SkipUntilAsync(c, false);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00034160 File Offset: 0x00032360
		private Task SkipUntilAsync(char stopChar, bool recognizeLiterals)
		{
			XmlTextReaderImpl.<SkipUntilAsync>d__572 <SkipUntilAsync>d__;
			<SkipUntilAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipUntilAsync>d__.<>4__this = this;
			<SkipUntilAsync>d__.stopChar = stopChar;
			<SkipUntilAsync>d__.recognizeLiterals = recognizeLiterals;
			<SkipUntilAsync>d__.<>1__state = -1;
			<SkipUntilAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<SkipUntilAsync>d__572>(ref <SkipUntilAsync>d__);
			return <SkipUntilAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000341B4 File Offset: 0x000323B4
		private Task<int> EatWhitespacesAsync(StringBuilder sb)
		{
			XmlTextReaderImpl.<EatWhitespacesAsync>d__573 <EatWhitespacesAsync>d__;
			<EatWhitespacesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<EatWhitespacesAsync>d__.<>4__this = this;
			<EatWhitespacesAsync>d__.sb = sb;
			<EatWhitespacesAsync>d__.<>1__state = -1;
			<EatWhitespacesAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<EatWhitespacesAsync>d__573>(ref <EatWhitespacesAsync>d__);
			return <EatWhitespacesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00034200 File Offset: 0x00032400
		private Task<Tuple<XmlTextReaderImpl.EntityType, int>> ParseNumericCharRefAsync(bool expand, StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.<ParseNumericCharRefAsync>d__574 <ParseNumericCharRefAsync>d__;
			<ParseNumericCharRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<XmlTextReaderImpl.EntityType, int>>.Create();
			<ParseNumericCharRefAsync>d__.<>4__this = this;
			<ParseNumericCharRefAsync>d__.expand = expand;
			<ParseNumericCharRefAsync>d__.internalSubsetBuilder = internalSubsetBuilder;
			<ParseNumericCharRefAsync>d__.<>1__state = -1;
			<ParseNumericCharRefAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseNumericCharRefAsync>d__574>(ref <ParseNumericCharRefAsync>d__);
			return <ParseNumericCharRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00034254 File Offset: 0x00032454
		private Task<int> ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.<ParseNamedCharRefAsync>d__575 <ParseNamedCharRefAsync>d__;
			<ParseNamedCharRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ParseNamedCharRefAsync>d__.<>4__this = this;
			<ParseNamedCharRefAsync>d__.expand = expand;
			<ParseNamedCharRefAsync>d__.internalSubsetBuilder = internalSubsetBuilder;
			<ParseNamedCharRefAsync>d__.<>1__state = -1;
			<ParseNamedCharRefAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseNamedCharRefAsync>d__575>(ref <ParseNamedCharRefAsync>d__);
			return <ParseNamedCharRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x000342A8 File Offset: 0x000324A8
		private Task<int> ParseNameAsync()
		{
			XmlTextReaderImpl.<ParseNameAsync>d__576 <ParseNameAsync>d__;
			<ParseNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ParseNameAsync>d__.<>4__this = this;
			<ParseNameAsync>d__.<>1__state = -1;
			<ParseNameAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseNameAsync>d__576>(ref <ParseNameAsync>d__);
			return <ParseNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000342EB File Offset: 0x000324EB
		private Task<Tuple<int, int>> ParseQNameAsync()
		{
			return this.ParseQNameAsync(true, 0);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000342F8 File Offset: 0x000324F8
		private Task<Tuple<int, int>> ParseQNameAsync(bool isQName, int startOffset)
		{
			XmlTextReaderImpl.<ParseQNameAsync>d__578 <ParseQNameAsync>d__;
			<ParseQNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int>>.Create();
			<ParseQNameAsync>d__.<>4__this = this;
			<ParseQNameAsync>d__.isQName = isQName;
			<ParseQNameAsync>d__.startOffset = startOffset;
			<ParseQNameAsync>d__.<>1__state = -1;
			<ParseQNameAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseQNameAsync>d__578>(ref <ParseQNameAsync>d__);
			return <ParseQNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003434C File Offset: 0x0003254C
		private Task<Tuple<int, bool>> ReadDataInNameAsync(int pos)
		{
			XmlTextReaderImpl.<ReadDataInNameAsync>d__579 <ReadDataInNameAsync>d__;
			<ReadDataInNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, bool>>.Create();
			<ReadDataInNameAsync>d__.<>4__this = this;
			<ReadDataInNameAsync>d__.pos = pos;
			<ReadDataInNameAsync>d__.<>1__state = -1;
			<ReadDataInNameAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadDataInNameAsync>d__579>(ref <ReadDataInNameAsync>d__);
			return <ReadDataInNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00034398 File Offset: 0x00032598
		private Task<string> ParseEntityNameAsync()
		{
			XmlTextReaderImpl.<ParseEntityNameAsync>d__580 <ParseEntityNameAsync>d__;
			<ParseEntityNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ParseEntityNameAsync>d__.<>4__this = this;
			<ParseEntityNameAsync>d__.<>1__state = -1;
			<ParseEntityNameAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEntityNameAsync>d__580>(ref <ParseEntityNameAsync>d__);
			return <ParseEntityNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000343DC File Offset: 0x000325DC
		private Task PushExternalEntityOrSubsetAsync(string publicId, string systemId, Uri baseUri, string entityName)
		{
			XmlTextReaderImpl.<PushExternalEntityOrSubsetAsync>d__581 <PushExternalEntityOrSubsetAsync>d__;
			<PushExternalEntityOrSubsetAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PushExternalEntityOrSubsetAsync>d__.<>4__this = this;
			<PushExternalEntityOrSubsetAsync>d__.publicId = publicId;
			<PushExternalEntityOrSubsetAsync>d__.systemId = systemId;
			<PushExternalEntityOrSubsetAsync>d__.baseUri = baseUri;
			<PushExternalEntityOrSubsetAsync>d__.entityName = entityName;
			<PushExternalEntityOrSubsetAsync>d__.<>1__state = -1;
			<PushExternalEntityOrSubsetAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<PushExternalEntityOrSubsetAsync>d__581>(ref <PushExternalEntityOrSubsetAsync>d__);
			return <PushExternalEntityOrSubsetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00034440 File Offset: 0x00032640
		private Task<bool> OpenAndPushAsync(Uri uri)
		{
			XmlTextReaderImpl.<OpenAndPushAsync>d__582 <OpenAndPushAsync>d__;
			<OpenAndPushAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<OpenAndPushAsync>d__.<>4__this = this;
			<OpenAndPushAsync>d__.uri = uri;
			<OpenAndPushAsync>d__.<>1__state = -1;
			<OpenAndPushAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<OpenAndPushAsync>d__582>(ref <OpenAndPushAsync>d__);
			return <OpenAndPushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003448C File Offset: 0x0003268C
		private Task<bool> PushExternalEntityAsync(IDtdEntityInfo entity)
		{
			XmlTextReaderImpl.<PushExternalEntityAsync>d__583 <PushExternalEntityAsync>d__;
			<PushExternalEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<PushExternalEntityAsync>d__.<>4__this = this;
			<PushExternalEntityAsync>d__.entity = entity;
			<PushExternalEntityAsync>d__.<>1__state = -1;
			<PushExternalEntityAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<PushExternalEntityAsync>d__583>(ref <PushExternalEntityAsync>d__);
			return <PushExternalEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000344D8 File Offset: 0x000326D8
		private Task<bool> ZeroEndingStreamAsync(int pos)
		{
			XmlTextReaderImpl.<ZeroEndingStreamAsync>d__584 <ZeroEndingStreamAsync>d__;
			<ZeroEndingStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ZeroEndingStreamAsync>d__.<>4__this = this;
			<ZeroEndingStreamAsync>d__.pos = pos;
			<ZeroEndingStreamAsync>d__.<>1__state = -1;
			<ZeroEndingStreamAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ZeroEndingStreamAsync>d__584>(ref <ZeroEndingStreamAsync>d__);
			return <ZeroEndingStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00034524 File Offset: 0x00032724
		private Task ParseDtdFromParserContextAsync()
		{
			XmlTextReaderImpl.<ParseDtdFromParserContextAsync>d__585 <ParseDtdFromParserContextAsync>d__;
			<ParseDtdFromParserContextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseDtdFromParserContextAsync>d__.<>4__this = this;
			<ParseDtdFromParserContextAsync>d__.<>1__state = -1;
			<ParseDtdFromParserContextAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDtdFromParserContextAsync>d__585>(ref <ParseDtdFromParserContextAsync>d__);
			return <ParseDtdFromParserContextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00034568 File Offset: 0x00032768
		private Task<bool> InitReadContentAsBinaryAsync()
		{
			XmlTextReaderImpl.<InitReadContentAsBinaryAsync>d__586 <InitReadContentAsBinaryAsync>d__;
			<InitReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitReadContentAsBinaryAsync>d__.<>4__this = this;
			<InitReadContentAsBinaryAsync>d__.<>1__state = -1;
			<InitReadContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<InitReadContentAsBinaryAsync>d__586>(ref <InitReadContentAsBinaryAsync>d__);
			return <InitReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000345AC File Offset: 0x000327AC
		private Task<bool> InitReadElementContentAsBinaryAsync()
		{
			XmlTextReaderImpl.<InitReadElementContentAsBinaryAsync>d__587 <InitReadElementContentAsBinaryAsync>d__;
			<InitReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<InitReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<InitReadElementContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<InitReadElementContentAsBinaryAsync>d__587>(ref <InitReadElementContentAsBinaryAsync>d__);
			return <InitReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000345F0 File Offset: 0x000327F0
		private Task<bool> MoveToNextContentNodeAsync(bool moveIfOnContentNode)
		{
			XmlTextReaderImpl.<MoveToNextContentNodeAsync>d__588 <MoveToNextContentNodeAsync>d__;
			<MoveToNextContentNodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<MoveToNextContentNodeAsync>d__.<>4__this = this;
			<MoveToNextContentNodeAsync>d__.moveIfOnContentNode = moveIfOnContentNode;
			<MoveToNextContentNodeAsync>d__.<>1__state = -1;
			<MoveToNextContentNodeAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<MoveToNextContentNodeAsync>d__588>(ref <MoveToNextContentNodeAsync>d__);
			return <MoveToNextContentNodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003463C File Offset: 0x0003283C
		private Task<int> ReadContentAsBinaryAsync(byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadContentAsBinaryAsync>d__589 <ReadContentAsBinaryAsync>d__;
			<ReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinaryAsync>d__.<>4__this = this;
			<ReadContentAsBinaryAsync>d__.buffer = buffer;
			<ReadContentAsBinaryAsync>d__.index = index;
			<ReadContentAsBinaryAsync>d__.count = count;
			<ReadContentAsBinaryAsync>d__.<>1__state = -1;
			<ReadContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadContentAsBinaryAsync>d__589>(ref <ReadContentAsBinaryAsync>d__);
			return <ReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00034698 File Offset: 0x00032898
		private Task<int> ReadElementContentAsBinaryAsync(byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadElementContentAsBinaryAsync>d__590 <ReadElementContentAsBinaryAsync>d__;
			<ReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<ReadElementContentAsBinaryAsync>d__.buffer = buffer;
			<ReadElementContentAsBinaryAsync>d__.index = index;
			<ReadElementContentAsBinaryAsync>d__.count = count;
			<ReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadElementContentAsBinaryAsync>d__590>(ref <ReadElementContentAsBinaryAsync>d__);
			return <ReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400036C RID: 876
		private readonly bool useAsync;

		// Token: 0x0400036D RID: 877
		private XmlTextReaderImpl.LaterInitParam laterInitParam;

		// Token: 0x0400036E RID: 878
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x0400036F RID: 879
		private XmlTextReaderImpl.ParsingState ps;

		// Token: 0x04000370 RID: 880
		private XmlTextReaderImpl.ParsingFunction parsingFunction;

		// Token: 0x04000371 RID: 881
		private XmlTextReaderImpl.ParsingFunction nextParsingFunction;

		// Token: 0x04000372 RID: 882
		private XmlTextReaderImpl.ParsingFunction nextNextParsingFunction;

		// Token: 0x04000373 RID: 883
		private XmlTextReaderImpl.NodeData[] nodes;

		// Token: 0x04000374 RID: 884
		private XmlTextReaderImpl.NodeData curNode;

		// Token: 0x04000375 RID: 885
		private int index;

		// Token: 0x04000376 RID: 886
		private int curAttrIndex = -1;

		// Token: 0x04000377 RID: 887
		private int attrCount;

		// Token: 0x04000378 RID: 888
		private int attrHashtable;

		// Token: 0x04000379 RID: 889
		private int attrDuplWalkCount;

		// Token: 0x0400037A RID: 890
		private bool attrNeedNamespaceLookup;

		// Token: 0x0400037B RID: 891
		private bool fullAttrCleanup;

		// Token: 0x0400037C RID: 892
		private HashSet<XmlTextReaderImpl.NodeData> attrDuplSet;

		// Token: 0x0400037D RID: 893
		private XmlNameTable nameTable;

		// Token: 0x0400037E RID: 894
		private bool nameTableFromSettings;

		// Token: 0x0400037F RID: 895
		private XmlResolver xmlResolver;

		// Token: 0x04000380 RID: 896
		private string url = string.Empty;

		// Token: 0x04000381 RID: 897
		private CompressedStack compressedStack;

		// Token: 0x04000382 RID: 898
		private bool normalize;

		// Token: 0x04000383 RID: 899
		private bool supportNamespaces = true;

		// Token: 0x04000384 RID: 900
		private WhitespaceHandling whitespaceHandling;

		// Token: 0x04000385 RID: 901
		private DtdProcessing dtdProcessing = DtdProcessing.Parse;

		// Token: 0x04000386 RID: 902
		private EntityHandling entityHandling;

		// Token: 0x04000387 RID: 903
		private bool ignorePIs;

		// Token: 0x04000388 RID: 904
		private bool ignoreComments;

		// Token: 0x04000389 RID: 905
		private bool checkCharacters;

		// Token: 0x0400038A RID: 906
		private int lineNumberOffset;

		// Token: 0x0400038B RID: 907
		private int linePositionOffset;

		// Token: 0x0400038C RID: 908
		private bool closeInput;

		// Token: 0x0400038D RID: 909
		private long maxCharactersInDocument;

		// Token: 0x0400038E RID: 910
		private long maxCharactersFromEntities;

		// Token: 0x0400038F RID: 911
		private bool v1Compat;

		// Token: 0x04000390 RID: 912
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04000391 RID: 913
		private string lastPrefix = string.Empty;

		// Token: 0x04000392 RID: 914
		private XmlTextReaderImpl.XmlContext xmlContext;

		// Token: 0x04000393 RID: 915
		private XmlTextReaderImpl.ParsingState[] parsingStatesStack;

		// Token: 0x04000394 RID: 916
		private int parsingStatesStackTop = -1;

		// Token: 0x04000395 RID: 917
		private string reportedBaseUri;

		// Token: 0x04000396 RID: 918
		private Encoding reportedEncoding;

		// Token: 0x04000397 RID: 919
		private IDtdInfo dtdInfo;

		// Token: 0x04000398 RID: 920
		private XmlNodeType fragmentType = XmlNodeType.Document;

		// Token: 0x04000399 RID: 921
		private XmlParserContext fragmentParserContext;

		// Token: 0x0400039A RID: 922
		private bool fragment;

		// Token: 0x0400039B RID: 923
		private IncrementalReadDecoder incReadDecoder;

		// Token: 0x0400039C RID: 924
		private XmlTextReaderImpl.IncrementalReadState incReadState;

		// Token: 0x0400039D RID: 925
		private LineInfo incReadLineInfo;

		// Token: 0x0400039E RID: 926
		private BinHexDecoder binHexDecoder;

		// Token: 0x0400039F RID: 927
		private Base64Decoder base64Decoder;

		// Token: 0x040003A0 RID: 928
		private int incReadDepth;

		// Token: 0x040003A1 RID: 929
		private int incReadLeftStartPos;

		// Token: 0x040003A2 RID: 930
		private int incReadLeftEndPos;

		// Token: 0x040003A3 RID: 931
		private IncrementalReadCharsDecoder readCharsDecoder;

		// Token: 0x040003A4 RID: 932
		private int attributeValueBaseEntityId;

		// Token: 0x040003A5 RID: 933
		private bool emptyEntityInAttributeResolved;

		// Token: 0x040003A6 RID: 934
		private IValidationEventHandling validationEventHandling;

		// Token: 0x040003A7 RID: 935
		private XmlTextReaderImpl.OnDefaultAttributeUseDelegate onDefaultAttributeUse;

		// Token: 0x040003A8 RID: 936
		private bool validatingReaderCompatFlag;

		// Token: 0x040003A9 RID: 937
		private bool addDefaultAttributesAndNormalize;

		// Token: 0x040003AA RID: 938
		private StringBuilder stringBuilder;

		// Token: 0x040003AB RID: 939
		private bool rootElementParsed;

		// Token: 0x040003AC RID: 940
		private bool standalone;

		// Token: 0x040003AD RID: 941
		private int nextEntityId = 1;

		// Token: 0x040003AE RID: 942
		private XmlTextReaderImpl.ParsingMode parsingMode;

		// Token: 0x040003AF RID: 943
		private ReadState readState;

		// Token: 0x040003B0 RID: 944
		private IDtdEntityInfo lastEntity;

		// Token: 0x040003B1 RID: 945
		private bool afterResetState;

		// Token: 0x040003B2 RID: 946
		private int documentStartBytePos;

		// Token: 0x040003B3 RID: 947
		private int readValueOffset;

		// Token: 0x040003B4 RID: 948
		private long charactersInDocument;

		// Token: 0x040003B5 RID: 949
		private long charactersFromEntities;

		// Token: 0x040003B6 RID: 950
		private Dictionary<IDtdEntityInfo, IDtdEntityInfo> currentEntities;

		// Token: 0x040003B7 RID: 951
		private bool disableUndeclaredEntityCheck;

		// Token: 0x040003B8 RID: 952
		private XmlReader outerReader;

		// Token: 0x040003B9 RID: 953
		private bool xmlResolverIsSet;

		// Token: 0x040003BA RID: 954
		private string Xml;

		// Token: 0x040003BB RID: 955
		private string XmlNs;

		// Token: 0x040003BC RID: 956
		private const int MaxBytesToMove = 128;

		// Token: 0x040003BD RID: 957
		private const int ApproxXmlDeclLength = 80;

		// Token: 0x040003BE RID: 958
		private const int NodesInitialSize = 8;

		// Token: 0x040003BF RID: 959
		private const int InitialAttributesCount = 4;

		// Token: 0x040003C0 RID: 960
		private const int InitialParsingStateStackSize = 2;

		// Token: 0x040003C1 RID: 961
		private const int InitialParsingStatesDepth = 2;

		// Token: 0x040003C2 RID: 962
		private const int DtdChidrenInitialSize = 2;

		// Token: 0x040003C3 RID: 963
		private const int MaxByteSequenceLen = 6;

		// Token: 0x040003C4 RID: 964
		private const int MaxAttrDuplWalkCount = 64;

		// Token: 0x040003C5 RID: 965
		private const int MinWhitespaceLookahedCount = 4096;

		// Token: 0x040003C6 RID: 966
		private const string XmlDeclarationBegining = "<?xml";

		// Token: 0x040003C7 RID: 967
		private XmlTextReaderImpl.ParseEndElementParseFunction parseEndElement_NextFunc;

		// Token: 0x040003C8 RID: 968
		private XmlTextReaderImpl.ParseTextFunction parseText_NextFunction;

		// Token: 0x040003C9 RID: 969
		private XmlTextReaderImpl.ParseTextState lastParseTextState;

		// Token: 0x040003CA RID: 970
		private Task<Tuple<int, int, int, bool>> parseText_dummyTask = Task.FromResult<Tuple<int, int, int, bool>>(new Tuple<int, int, int, bool>(0, 0, 0, false));

		// Token: 0x0200036A RID: 874
		private enum ParsingFunction
		{
			// Token: 0x040016B7 RID: 5815
			ElementContent,
			// Token: 0x040016B8 RID: 5816
			NoData,
			// Token: 0x040016B9 RID: 5817
			OpenUrl,
			// Token: 0x040016BA RID: 5818
			SwitchToInteractive,
			// Token: 0x040016BB RID: 5819
			SwitchToInteractiveXmlDecl,
			// Token: 0x040016BC RID: 5820
			DocumentContent,
			// Token: 0x040016BD RID: 5821
			MoveToElementContent,
			// Token: 0x040016BE RID: 5822
			PopElementContext,
			// Token: 0x040016BF RID: 5823
			PopEmptyElementContext,
			// Token: 0x040016C0 RID: 5824
			ResetAttributesRootLevel,
			// Token: 0x040016C1 RID: 5825
			Error,
			// Token: 0x040016C2 RID: 5826
			Eof,
			// Token: 0x040016C3 RID: 5827
			ReaderClosed,
			// Token: 0x040016C4 RID: 5828
			EntityReference,
			// Token: 0x040016C5 RID: 5829
			InIncrementalRead,
			// Token: 0x040016C6 RID: 5830
			FragmentAttribute,
			// Token: 0x040016C7 RID: 5831
			ReportEndEntity,
			// Token: 0x040016C8 RID: 5832
			AfterResolveEntityInContent,
			// Token: 0x040016C9 RID: 5833
			AfterResolveEmptyEntityInContent,
			// Token: 0x040016CA RID: 5834
			XmlDeclarationFragment,
			// Token: 0x040016CB RID: 5835
			GoToEof,
			// Token: 0x040016CC RID: 5836
			PartialTextValue,
			// Token: 0x040016CD RID: 5837
			InReadAttributeValue,
			// Token: 0x040016CE RID: 5838
			InReadValueChunk,
			// Token: 0x040016CF RID: 5839
			InReadContentAsBinary,
			// Token: 0x040016D0 RID: 5840
			InReadElementContentAsBinary
		}

		// Token: 0x0200036B RID: 875
		private enum ParsingMode
		{
			// Token: 0x040016D2 RID: 5842
			Full,
			// Token: 0x040016D3 RID: 5843
			SkipNode,
			// Token: 0x040016D4 RID: 5844
			SkipContent
		}

		// Token: 0x0200036C RID: 876
		private enum EntityType
		{
			// Token: 0x040016D6 RID: 5846
			CharacterDec,
			// Token: 0x040016D7 RID: 5847
			CharacterHex,
			// Token: 0x040016D8 RID: 5848
			CharacterNamed,
			// Token: 0x040016D9 RID: 5849
			Expanded,
			// Token: 0x040016DA RID: 5850
			Skipped,
			// Token: 0x040016DB RID: 5851
			FakeExpanded,
			// Token: 0x040016DC RID: 5852
			Unexpanded,
			// Token: 0x040016DD RID: 5853
			ExpandedInAttribute
		}

		// Token: 0x0200036D RID: 877
		private enum EntityExpandType
		{
			// Token: 0x040016DF RID: 5855
			All,
			// Token: 0x040016E0 RID: 5856
			OnlyGeneral,
			// Token: 0x040016E1 RID: 5857
			OnlyCharacter
		}

		// Token: 0x0200036E RID: 878
		private enum IncrementalReadState
		{
			// Token: 0x040016E3 RID: 5859
			Text,
			// Token: 0x040016E4 RID: 5860
			StartTag,
			// Token: 0x040016E5 RID: 5861
			PI,
			// Token: 0x040016E6 RID: 5862
			CDATA,
			// Token: 0x040016E7 RID: 5863
			Comment,
			// Token: 0x040016E8 RID: 5864
			Attributes,
			// Token: 0x040016E9 RID: 5865
			AttributeValue,
			// Token: 0x040016EA RID: 5866
			ReadData,
			// Token: 0x040016EB RID: 5867
			EndElement,
			// Token: 0x040016EC RID: 5868
			End,
			// Token: 0x040016ED RID: 5869
			ReadValueChunk_OnCachedValue,
			// Token: 0x040016EE RID: 5870
			ReadValueChunk_OnPartialValue,
			// Token: 0x040016EF RID: 5871
			ReadContentAsBinary_OnCachedValue,
			// Token: 0x040016F0 RID: 5872
			ReadContentAsBinary_OnPartialValue,
			// Token: 0x040016F1 RID: 5873
			ReadContentAsBinary_End
		}

		// Token: 0x0200036F RID: 879
		private class LaterInitParam
		{
			// Token: 0x040016F2 RID: 5874
			public bool useAsync;

			// Token: 0x040016F3 RID: 5875
			public Stream inputStream;

			// Token: 0x040016F4 RID: 5876
			public byte[] inputBytes;

			// Token: 0x040016F5 RID: 5877
			public int inputByteCount;

			// Token: 0x040016F6 RID: 5878
			public Uri inputbaseUri;

			// Token: 0x040016F7 RID: 5879
			public string inputUriStr;

			// Token: 0x040016F8 RID: 5880
			public XmlResolver inputUriResolver;

			// Token: 0x040016F9 RID: 5881
			public XmlParserContext inputContext;

			// Token: 0x040016FA RID: 5882
			public TextReader inputTextReader;

			// Token: 0x040016FB RID: 5883
			public XmlTextReaderImpl.InitInputType initType = XmlTextReaderImpl.InitInputType.Invalid;
		}

		// Token: 0x02000370 RID: 880
		private enum InitInputType
		{
			// Token: 0x040016FD RID: 5885
			UriString,
			// Token: 0x040016FE RID: 5886
			Stream,
			// Token: 0x040016FF RID: 5887
			TextReader,
			// Token: 0x04001700 RID: 5888
			Invalid
		}

		// Token: 0x02000371 RID: 881
		private enum ParseEndElementParseFunction
		{
			// Token: 0x04001702 RID: 5890
			CheckEndTag,
			// Token: 0x04001703 RID: 5891
			ReadData,
			// Token: 0x04001704 RID: 5892
			Done
		}

		// Token: 0x02000372 RID: 882
		private class ParseTextState
		{
			// Token: 0x06002E6F RID: 11887 RVA: 0x000F899D File Offset: 0x000F6B9D
			public ParseTextState(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
			{
				this.outOrChars = outOrChars;
				this.chars = chars;
				this.pos = pos;
				this.rcount = rcount;
				this.rpos = rpos;
				this.orChars = orChars;
				this.c = c;
			}

			// Token: 0x04001705 RID: 5893
			public int outOrChars;

			// Token: 0x04001706 RID: 5894
			public char[] chars;

			// Token: 0x04001707 RID: 5895
			public int pos;

			// Token: 0x04001708 RID: 5896
			public int rcount;

			// Token: 0x04001709 RID: 5897
			public int rpos;

			// Token: 0x0400170A RID: 5898
			public int orChars;

			// Token: 0x0400170B RID: 5899
			public char c;
		}

		// Token: 0x02000373 RID: 883
		private enum ParseTextFunction
		{
			// Token: 0x0400170D RID: 5901
			ParseText,
			// Token: 0x0400170E RID: 5902
			Entity,
			// Token: 0x0400170F RID: 5903
			Surrogate,
			// Token: 0x04001710 RID: 5904
			ReadData,
			// Token: 0x04001711 RID: 5905
			NoValue,
			// Token: 0x04001712 RID: 5906
			PartialValue
		}

		// Token: 0x02000374 RID: 884
		private struct ParsingState
		{
			// Token: 0x06002E70 RID: 11888 RVA: 0x000F89DC File Offset: 0x000F6BDC
			internal void Clear()
			{
				this.chars = null;
				this.charPos = 0;
				this.charsUsed = 0;
				this.encoding = null;
				this.stream = null;
				this.decoder = null;
				this.bytes = null;
				this.bytePos = 0;
				this.bytesUsed = 0;
				this.textReader = null;
				this.lineNo = 1;
				this.lineStartPos = -1;
				this.baseUriStr = string.Empty;
				this.baseUri = null;
				this.isEof = false;
				this.isStreamEof = false;
				this.eolNormalized = true;
				this.entityResolvedManually = false;
			}

			// Token: 0x06002E71 RID: 11889 RVA: 0x000F8A6B File Offset: 0x000F6C6B
			internal void Close(bool closeInput)
			{
				if (closeInput)
				{
					if (this.stream != null)
					{
						this.stream.Close();
						return;
					}
					if (this.textReader != null)
					{
						this.textReader.Close();
					}
				}
			}

			// Token: 0x17000A23 RID: 2595
			// (get) Token: 0x06002E72 RID: 11890 RVA: 0x000F8A97 File Offset: 0x000F6C97
			internal int LineNo
			{
				get
				{
					return this.lineNo;
				}
			}

			// Token: 0x17000A24 RID: 2596
			// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000F8A9F File Offset: 0x000F6C9F
			internal int LinePos
			{
				get
				{
					return this.charPos - this.lineStartPos;
				}
			}

			// Token: 0x04001713 RID: 5907
			internal char[] chars;

			// Token: 0x04001714 RID: 5908
			internal int charPos;

			// Token: 0x04001715 RID: 5909
			internal int charsUsed;

			// Token: 0x04001716 RID: 5910
			internal Encoding encoding;

			// Token: 0x04001717 RID: 5911
			internal bool appendMode;

			// Token: 0x04001718 RID: 5912
			internal Stream stream;

			// Token: 0x04001719 RID: 5913
			internal Decoder decoder;

			// Token: 0x0400171A RID: 5914
			internal byte[] bytes;

			// Token: 0x0400171B RID: 5915
			internal int bytePos;

			// Token: 0x0400171C RID: 5916
			internal int bytesUsed;

			// Token: 0x0400171D RID: 5917
			internal TextReader textReader;

			// Token: 0x0400171E RID: 5918
			internal int lineNo;

			// Token: 0x0400171F RID: 5919
			internal int lineStartPos;

			// Token: 0x04001720 RID: 5920
			internal string baseUriStr;

			// Token: 0x04001721 RID: 5921
			internal Uri baseUri;

			// Token: 0x04001722 RID: 5922
			internal bool isEof;

			// Token: 0x04001723 RID: 5923
			internal bool isStreamEof;

			// Token: 0x04001724 RID: 5924
			internal IDtdEntityInfo entity;

			// Token: 0x04001725 RID: 5925
			internal int entityId;

			// Token: 0x04001726 RID: 5926
			internal bool eolNormalized;

			// Token: 0x04001727 RID: 5927
			internal bool entityResolvedManually;
		}

		// Token: 0x02000375 RID: 885
		private class XmlContext
		{
			// Token: 0x06002E74 RID: 11892 RVA: 0x000F8AAE File Offset: 0x000F6CAE
			internal XmlContext()
			{
				this.xmlSpace = XmlSpace.None;
				this.xmlLang = string.Empty;
				this.defaultNamespace = string.Empty;
				this.previousContext = null;
			}

			// Token: 0x06002E75 RID: 11893 RVA: 0x000F8ADA File Offset: 0x000F6CDA
			internal XmlContext(XmlTextReaderImpl.XmlContext previousContext)
			{
				this.xmlSpace = previousContext.xmlSpace;
				this.xmlLang = previousContext.xmlLang;
				this.defaultNamespace = previousContext.defaultNamespace;
				this.previousContext = previousContext;
			}

			// Token: 0x04001728 RID: 5928
			internal XmlSpace xmlSpace;

			// Token: 0x04001729 RID: 5929
			internal string xmlLang;

			// Token: 0x0400172A RID: 5930
			internal string defaultNamespace;

			// Token: 0x0400172B RID: 5931
			internal XmlTextReaderImpl.XmlContext previousContext;
		}

		// Token: 0x02000376 RID: 886
		private class NoNamespaceManager : XmlNamespaceManager
		{
			// Token: 0x17000A25 RID: 2597
			// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000F8B15 File Offset: 0x000F6D15
			public override string DefaultNamespace
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06002E78 RID: 11896 RVA: 0x000F8B1C File Offset: 0x000F6D1C
			public override void PushScope()
			{
			}

			// Token: 0x06002E79 RID: 11897 RVA: 0x000F8B1E File Offset: 0x000F6D1E
			public override bool PopScope()
			{
				return false;
			}

			// Token: 0x06002E7A RID: 11898 RVA: 0x000F8B21 File Offset: 0x000F6D21
			public override void AddNamespace(string prefix, string uri)
			{
			}

			// Token: 0x06002E7B RID: 11899 RVA: 0x000F8B23 File Offset: 0x000F6D23
			public override void RemoveNamespace(string prefix, string uri)
			{
			}

			// Token: 0x06002E7C RID: 11900 RVA: 0x000F8B25 File Offset: 0x000F6D25
			public override IEnumerator GetEnumerator()
			{
				return null;
			}

			// Token: 0x06002E7D RID: 11901 RVA: 0x000F8B28 File Offset: 0x000F6D28
			public override IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
			{
				return null;
			}

			// Token: 0x06002E7E RID: 11902 RVA: 0x000F8B2B File Offset: 0x000F6D2B
			public override string LookupNamespace(string prefix)
			{
				return string.Empty;
			}

			// Token: 0x06002E7F RID: 11903 RVA: 0x000F8B32 File Offset: 0x000F6D32
			public override string LookupPrefix(string uri)
			{
				return null;
			}

			// Token: 0x06002E80 RID: 11904 RVA: 0x000F8B35 File Offset: 0x000F6D35
			public override bool HasNamespace(string prefix)
			{
				return false;
			}
		}

		// Token: 0x02000377 RID: 887
		internal class DtdParserProxy : IDtdParserAdapterV1, IDtdParserAdapterWithValidation, IDtdParserAdapter
		{
			// Token: 0x06002E81 RID: 11905 RVA: 0x000F8B38 File Offset: 0x000F6D38
			internal DtdParserProxy(XmlTextReaderImpl reader)
			{
				this.reader = reader;
			}

			// Token: 0x17000A26 RID: 2598
			// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000F8B47 File Offset: 0x000F6D47
			XmlNameTable IDtdParserAdapter.NameTable
			{
				get
				{
					return this.reader.DtdParserProxy_NameTable;
				}
			}

			// Token: 0x17000A27 RID: 2599
			// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000F8B54 File Offset: 0x000F6D54
			IXmlNamespaceResolver IDtdParserAdapter.NamespaceResolver
			{
				get
				{
					return this.reader.DtdParserProxy_NamespaceResolver;
				}
			}

			// Token: 0x17000A28 RID: 2600
			// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000F8B61 File Offset: 0x000F6D61
			Uri IDtdParserAdapter.BaseUri
			{
				get
				{
					return this.reader.DtdParserProxy_BaseUri;
				}
			}

			// Token: 0x17000A29 RID: 2601
			// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000F8B6E File Offset: 0x000F6D6E
			bool IDtdParserAdapter.IsEof
			{
				get
				{
					return this.reader.DtdParserProxy_IsEof;
				}
			}

			// Token: 0x17000A2A RID: 2602
			// (get) Token: 0x06002E86 RID: 11910 RVA: 0x000F8B7B File Offset: 0x000F6D7B
			char[] IDtdParserAdapter.ParsingBuffer
			{
				get
				{
					return this.reader.DtdParserProxy_ParsingBuffer;
				}
			}

			// Token: 0x17000A2B RID: 2603
			// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000F8B88 File Offset: 0x000F6D88
			int IDtdParserAdapter.ParsingBufferLength
			{
				get
				{
					return this.reader.DtdParserProxy_ParsingBufferLength;
				}
			}

			// Token: 0x17000A2C RID: 2604
			// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000F8B95 File Offset: 0x000F6D95
			// (set) Token: 0x06002E89 RID: 11913 RVA: 0x000F8BA2 File Offset: 0x000F6DA2
			int IDtdParserAdapter.CurrentPosition
			{
				get
				{
					return this.reader.DtdParserProxy_CurrentPosition;
				}
				set
				{
					this.reader.DtdParserProxy_CurrentPosition = value;
				}
			}

			// Token: 0x17000A2D RID: 2605
			// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000F8BB0 File Offset: 0x000F6DB0
			int IDtdParserAdapter.EntityStackLength
			{
				get
				{
					return this.reader.DtdParserProxy_EntityStackLength;
				}
			}

			// Token: 0x17000A2E RID: 2606
			// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000F8BBD File Offset: 0x000F6DBD
			bool IDtdParserAdapter.IsEntityEolNormalized
			{
				get
				{
					return this.reader.DtdParserProxy_IsEntityEolNormalized;
				}
			}

			// Token: 0x06002E8C RID: 11916 RVA: 0x000F8BCA File Offset: 0x000F6DCA
			void IDtdParserAdapter.OnNewLine(int pos)
			{
				this.reader.DtdParserProxy_OnNewLine(pos);
			}

			// Token: 0x17000A2F RID: 2607
			// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000F8BD8 File Offset: 0x000F6DD8
			int IDtdParserAdapter.LineNo
			{
				get
				{
					return this.reader.DtdParserProxy_LineNo;
				}
			}

			// Token: 0x17000A30 RID: 2608
			// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000F8BE5 File Offset: 0x000F6DE5
			int IDtdParserAdapter.LineStartPosition
			{
				get
				{
					return this.reader.DtdParserProxy_LineStartPosition;
				}
			}

			// Token: 0x06002E8F RID: 11919 RVA: 0x000F8BF2 File Offset: 0x000F6DF2
			int IDtdParserAdapter.ReadData()
			{
				return this.reader.DtdParserProxy_ReadData();
			}

			// Token: 0x06002E90 RID: 11920 RVA: 0x000F8BFF File Offset: 0x000F6DFF
			int IDtdParserAdapter.ParseNumericCharRef(StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNumericCharRef(internalSubsetBuilder);
			}

			// Token: 0x06002E91 RID: 11921 RVA: 0x000F8C0D File Offset: 0x000F6E0D
			int IDtdParserAdapter.ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNamedCharRef(expand, internalSubsetBuilder);
			}

			// Token: 0x06002E92 RID: 11922 RVA: 0x000F8C1C File Offset: 0x000F6E1C
			void IDtdParserAdapter.ParsePI(StringBuilder sb)
			{
				this.reader.DtdParserProxy_ParsePI(sb);
			}

			// Token: 0x06002E93 RID: 11923 RVA: 0x000F8C2A File Offset: 0x000F6E2A
			void IDtdParserAdapter.ParseComment(StringBuilder sb)
			{
				this.reader.DtdParserProxy_ParseComment(sb);
			}

			// Token: 0x06002E94 RID: 11924 RVA: 0x000F8C38 File Offset: 0x000F6E38
			bool IDtdParserAdapter.PushEntity(IDtdEntityInfo entity, out int entityId)
			{
				return this.reader.DtdParserProxy_PushEntity(entity, out entityId);
			}

			// Token: 0x06002E95 RID: 11925 RVA: 0x000F8C47 File Offset: 0x000F6E47
			bool IDtdParserAdapter.PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId)
			{
				return this.reader.DtdParserProxy_PopEntity(out oldEntity, out newEntityId);
			}

			// Token: 0x06002E96 RID: 11926 RVA: 0x000F8C56 File Offset: 0x000F6E56
			bool IDtdParserAdapter.PushExternalSubset(string systemId, string publicId)
			{
				return this.reader.DtdParserProxy_PushExternalSubset(systemId, publicId);
			}

			// Token: 0x06002E97 RID: 11927 RVA: 0x000F8C65 File Offset: 0x000F6E65
			void IDtdParserAdapter.PushInternalDtd(string baseUri, string internalDtd)
			{
				this.reader.DtdParserProxy_PushInternalDtd(baseUri, internalDtd);
			}

			// Token: 0x06002E98 RID: 11928 RVA: 0x000F8C74 File Offset: 0x000F6E74
			void IDtdParserAdapter.Throw(Exception e)
			{
				this.reader.DtdParserProxy_Throw(e);
			}

			// Token: 0x06002E99 RID: 11929 RVA: 0x000F8C82 File Offset: 0x000F6E82
			void IDtdParserAdapter.OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo)
			{
				this.reader.DtdParserProxy_OnSystemId(systemId, keywordLineInfo, systemLiteralLineInfo);
			}

			// Token: 0x06002E9A RID: 11930 RVA: 0x000F8C92 File Offset: 0x000F6E92
			void IDtdParserAdapter.OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo)
			{
				this.reader.DtdParserProxy_OnPublicId(publicId, keywordLineInfo, publicLiteralLineInfo);
			}

			// Token: 0x17000A31 RID: 2609
			// (get) Token: 0x06002E9B RID: 11931 RVA: 0x000F8CA2 File Offset: 0x000F6EA2
			bool IDtdParserAdapterWithValidation.DtdValidation
			{
				get
				{
					return this.reader.DtdParserProxy_DtdValidation;
				}
			}

			// Token: 0x17000A32 RID: 2610
			// (get) Token: 0x06002E9C RID: 11932 RVA: 0x000F8CAF File Offset: 0x000F6EAF
			IValidationEventHandling IDtdParserAdapterWithValidation.ValidationEventHandling
			{
				get
				{
					return this.reader.DtdParserProxy_ValidationEventHandling;
				}
			}

			// Token: 0x17000A33 RID: 2611
			// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000F8CBC File Offset: 0x000F6EBC
			bool IDtdParserAdapterV1.Normalization
			{
				get
				{
					return this.reader.DtdParserProxy_Normalization;
				}
			}

			// Token: 0x17000A34 RID: 2612
			// (get) Token: 0x06002E9E RID: 11934 RVA: 0x000F8CC9 File Offset: 0x000F6EC9
			bool IDtdParserAdapterV1.Namespaces
			{
				get
				{
					return this.reader.DtdParserProxy_Namespaces;
				}
			}

			// Token: 0x17000A35 RID: 2613
			// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000F8CD6 File Offset: 0x000F6ED6
			bool IDtdParserAdapterV1.V1CompatibilityMode
			{
				get
				{
					return this.reader.DtdParserProxy_V1CompatibilityMode;
				}
			}

			// Token: 0x06002EA0 RID: 11936 RVA: 0x000F8CE3 File Offset: 0x000F6EE3
			Task<int> IDtdParserAdapter.ReadDataAsync()
			{
				return this.reader.DtdParserProxy_ReadDataAsync();
			}

			// Token: 0x06002EA1 RID: 11937 RVA: 0x000F8CF0 File Offset: 0x000F6EF0
			Task<int> IDtdParserAdapter.ParseNumericCharRefAsync(StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNumericCharRefAsync(internalSubsetBuilder);
			}

			// Token: 0x06002EA2 RID: 11938 RVA: 0x000F8CFE File Offset: 0x000F6EFE
			Task<int> IDtdParserAdapter.ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNamedCharRefAsync(expand, internalSubsetBuilder);
			}

			// Token: 0x06002EA3 RID: 11939 RVA: 0x000F8D0D File Offset: 0x000F6F0D
			Task IDtdParserAdapter.ParsePIAsync(StringBuilder sb)
			{
				return this.reader.DtdParserProxy_ParsePIAsync(sb);
			}

			// Token: 0x06002EA4 RID: 11940 RVA: 0x000F8D1B File Offset: 0x000F6F1B
			Task IDtdParserAdapter.ParseCommentAsync(StringBuilder sb)
			{
				return this.reader.DtdParserProxy_ParseCommentAsync(sb);
			}

			// Token: 0x06002EA5 RID: 11941 RVA: 0x000F8D29 File Offset: 0x000F6F29
			Task<Tuple<int, bool>> IDtdParserAdapter.PushEntityAsync(IDtdEntityInfo entity)
			{
				return this.reader.DtdParserProxy_PushEntityAsync(entity);
			}

			// Token: 0x06002EA6 RID: 11942 RVA: 0x000F8D37 File Offset: 0x000F6F37
			Task<bool> IDtdParserAdapter.PushExternalSubsetAsync(string systemId, string publicId)
			{
				return this.reader.DtdParserProxy_PushExternalSubsetAsync(systemId, publicId);
			}

			// Token: 0x0400172C RID: 5932
			private XmlTextReaderImpl reader;
		}

		// Token: 0x02000378 RID: 888
		private class NodeData : IComparable
		{
			// Token: 0x17000A36 RID: 2614
			// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000F8D46 File Offset: 0x000F6F46
			internal static XmlTextReaderImpl.NodeData None
			{
				get
				{
					if (XmlTextReaderImpl.NodeData.s_None == null)
					{
						XmlTextReaderImpl.NodeData.s_None = new XmlTextReaderImpl.NodeData();
					}
					return XmlTextReaderImpl.NodeData.s_None;
				}
			}

			// Token: 0x06002EA8 RID: 11944 RVA: 0x000F8D64 File Offset: 0x000F6F64
			internal NodeData()
			{
				this.Clear(XmlNodeType.None);
				this.xmlContextPushed = false;
			}

			// Token: 0x17000A37 RID: 2615
			// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000F8D7A File Offset: 0x000F6F7A
			internal int LineNo
			{
				get
				{
					return this.lineInfo.lineNo;
				}
			}

			// Token: 0x17000A38 RID: 2616
			// (get) Token: 0x06002EAA RID: 11946 RVA: 0x000F8D87 File Offset: 0x000F6F87
			internal int LinePos
			{
				get
				{
					return this.lineInfo.linePos;
				}
			}

			// Token: 0x17000A39 RID: 2617
			// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000F8D94 File Offset: 0x000F6F94
			// (set) Token: 0x06002EAC RID: 11948 RVA: 0x000F8DA7 File Offset: 0x000F6FA7
			internal bool IsEmptyElement
			{
				get
				{
					return this.type == XmlNodeType.Element && this.isEmptyOrDefault;
				}
				set
				{
					this.isEmptyOrDefault = value;
				}
			}

			// Token: 0x17000A3A RID: 2618
			// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000F8DB0 File Offset: 0x000F6FB0
			// (set) Token: 0x06002EAE RID: 11950 RVA: 0x000F8DC3 File Offset: 0x000F6FC3
			internal bool IsDefaultAttribute
			{
				get
				{
					return this.type == XmlNodeType.Attribute && this.isEmptyOrDefault;
				}
				set
				{
					this.isEmptyOrDefault = value;
				}
			}

			// Token: 0x17000A3B RID: 2619
			// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000F8DCC File Offset: 0x000F6FCC
			internal bool ValueBuffered
			{
				get
				{
					return this.value == null;
				}
			}

			// Token: 0x17000A3C RID: 2620
			// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000F8DD7 File Offset: 0x000F6FD7
			internal string StringValue
			{
				get
				{
					if (this.value == null)
					{
						this.value = new string(this.chars, this.valueStartPos, this.valueLength);
					}
					return this.value;
				}
			}

			// Token: 0x06002EB1 RID: 11953 RVA: 0x000F8E04 File Offset: 0x000F7004
			internal void TrimSpacesInValue()
			{
				if (this.ValueBuffered)
				{
					XmlTextReaderImpl.StripSpaces(this.chars, this.valueStartPos, ref this.valueLength);
					return;
				}
				this.value = XmlTextReaderImpl.StripSpaces(this.value);
			}

			// Token: 0x06002EB2 RID: 11954 RVA: 0x000F8E37 File Offset: 0x000F7037
			internal void Clear(XmlNodeType type)
			{
				this.type = type;
				this.ClearName();
				this.value = string.Empty;
				this.valueStartPos = -1;
				this.nameWPrefix = string.Empty;
				this.schemaType = null;
				this.typedValue = null;
			}

			// Token: 0x06002EB3 RID: 11955 RVA: 0x000F8E71 File Offset: 0x000F7071
			internal void ClearName()
			{
				this.localName = string.Empty;
				this.prefix = string.Empty;
				this.ns = string.Empty;
				this.nameWPrefix = string.Empty;
			}

			// Token: 0x06002EB4 RID: 11956 RVA: 0x000F8E9F File Offset: 0x000F709F
			internal void SetLineInfo(int lineNo, int linePos)
			{
				this.lineInfo.Set(lineNo, linePos);
			}

			// Token: 0x06002EB5 RID: 11957 RVA: 0x000F8EAE File Offset: 0x000F70AE
			internal void SetLineInfo2(int lineNo, int linePos)
			{
				this.lineInfo2.Set(lineNo, linePos);
			}

			// Token: 0x06002EB6 RID: 11958 RVA: 0x000F8EBD File Offset: 0x000F70BD
			internal void SetValueNode(XmlNodeType type, string value)
			{
				this.type = type;
				this.ClearName();
				this.value = value;
				this.valueStartPos = -1;
			}

			// Token: 0x06002EB7 RID: 11959 RVA: 0x000F8EDA File Offset: 0x000F70DA
			internal void SetValueNode(XmlNodeType type, char[] chars, int startPos, int len)
			{
				this.type = type;
				this.ClearName();
				this.value = null;
				this.chars = chars;
				this.valueStartPos = startPos;
				this.valueLength = len;
			}

			// Token: 0x06002EB8 RID: 11960 RVA: 0x000F8F06 File Offset: 0x000F7106
			internal void SetNamedNode(XmlNodeType type, string localName)
			{
				this.SetNamedNode(type, localName, string.Empty, localName);
			}

			// Token: 0x06002EB9 RID: 11961 RVA: 0x000F8F16 File Offset: 0x000F7116
			internal void SetNamedNode(XmlNodeType type, string localName, string prefix, string nameWPrefix)
			{
				this.type = type;
				this.localName = localName;
				this.prefix = prefix;
				this.nameWPrefix = nameWPrefix;
				this.ns = string.Empty;
				this.value = string.Empty;
				this.valueStartPos = -1;
			}

			// Token: 0x06002EBA RID: 11962 RVA: 0x000F8F52 File Offset: 0x000F7152
			internal void SetValue(string value)
			{
				this.valueStartPos = -1;
				this.value = value;
			}

			// Token: 0x06002EBB RID: 11963 RVA: 0x000F8F62 File Offset: 0x000F7162
			internal void SetValue(char[] chars, int startPos, int len)
			{
				this.value = null;
				this.chars = chars;
				this.valueStartPos = startPos;
				this.valueLength = len;
			}

			// Token: 0x06002EBC RID: 11964 RVA: 0x000F8F80 File Offset: 0x000F7180
			internal void OnBufferInvalidated()
			{
				if (this.value == null)
				{
					this.value = new string(this.chars, this.valueStartPos, this.valueLength);
				}
				this.valueStartPos = -1;
			}

			// Token: 0x06002EBD RID: 11965 RVA: 0x000F8FB0 File Offset: 0x000F71B0
			internal void CopyTo(int valueOffset, StringBuilder sb)
			{
				if (this.value == null)
				{
					sb.Append(this.chars, this.valueStartPos + valueOffset, this.valueLength - valueOffset);
					return;
				}
				if (valueOffset <= 0)
				{
					sb.Append(this.value);
					return;
				}
				sb.Append(this.value, valueOffset, this.value.Length - valueOffset);
			}

			// Token: 0x06002EBE RID: 11966 RVA: 0x000F9010 File Offset: 0x000F7210
			internal int CopyTo(int valueOffset, char[] buffer, int offset, int length)
			{
				if (this.value == null)
				{
					int num = this.valueLength - valueOffset;
					if (num > length)
					{
						num = length;
					}
					XmlTextReaderImpl.BlockCopyChars(this.chars, this.valueStartPos + valueOffset, buffer, offset, num);
					return num;
				}
				int num2 = this.value.Length - valueOffset;
				if (num2 > length)
				{
					num2 = length;
				}
				this.value.CopyTo(valueOffset, buffer, offset, num2);
				return num2;
			}

			// Token: 0x06002EBF RID: 11967 RVA: 0x000F9074 File Offset: 0x000F7274
			internal int CopyToBinary(IncrementalReadDecoder decoder, int valueOffset)
			{
				if (this.value == null)
				{
					return decoder.Decode(this.chars, this.valueStartPos + valueOffset, this.valueLength - valueOffset);
				}
				return decoder.Decode(this.value, valueOffset, this.value.Length - valueOffset);
			}

			// Token: 0x06002EC0 RID: 11968 RVA: 0x000F90C0 File Offset: 0x000F72C0
			internal void AdjustLineInfo(int valueOffset, bool isNormalized, ref LineInfo lineInfo)
			{
				if (valueOffset == 0)
				{
					return;
				}
				if (this.valueStartPos != -1)
				{
					XmlTextReaderImpl.AdjustLineInfo(this.chars, this.valueStartPos, this.valueStartPos + valueOffset, isNormalized, ref lineInfo);
					return;
				}
				XmlTextReaderImpl.AdjustLineInfo(this.value, 0, valueOffset, isNormalized, ref lineInfo);
			}

			// Token: 0x06002EC1 RID: 11969 RVA: 0x000F90FA File Offset: 0x000F72FA
			internal string GetNameWPrefix(XmlNameTable nt)
			{
				if (this.nameWPrefix != null)
				{
					return this.nameWPrefix;
				}
				return this.CreateNameWPrefix(nt);
			}

			// Token: 0x06002EC2 RID: 11970 RVA: 0x000F9114 File Offset: 0x000F7314
			internal string CreateNameWPrefix(XmlNameTable nt)
			{
				if (this.prefix.Length == 0)
				{
					this.nameWPrefix = this.localName;
				}
				else
				{
					this.nameWPrefix = nt.Add(this.prefix + ":" + this.localName);
				}
				return this.nameWPrefix;
			}

			// Token: 0x06002EC3 RID: 11971 RVA: 0x000F9164 File Offset: 0x000F7364
			int IComparable.CompareTo(object obj)
			{
				XmlTextReaderImpl.NodeData nodeData = obj as XmlTextReaderImpl.NodeData;
				if (nodeData == null)
				{
					return 1;
				}
				if (!Ref.Equal(this.localName, nodeData.localName))
				{
					return string.CompareOrdinal(this.localName, nodeData.localName);
				}
				if (Ref.Equal(this.ns, nodeData.ns))
				{
					return 0;
				}
				return string.CompareOrdinal(this.ns, nodeData.ns);
			}

			// Token: 0x0400172D RID: 5933
			private static volatile XmlTextReaderImpl.NodeData s_None;

			// Token: 0x0400172E RID: 5934
			internal XmlNodeType type;

			// Token: 0x0400172F RID: 5935
			internal string localName;

			// Token: 0x04001730 RID: 5936
			internal string prefix;

			// Token: 0x04001731 RID: 5937
			internal string ns;

			// Token: 0x04001732 RID: 5938
			internal string nameWPrefix;

			// Token: 0x04001733 RID: 5939
			private string value;

			// Token: 0x04001734 RID: 5940
			private char[] chars;

			// Token: 0x04001735 RID: 5941
			private int valueStartPos;

			// Token: 0x04001736 RID: 5942
			private int valueLength;

			// Token: 0x04001737 RID: 5943
			internal LineInfo lineInfo;

			// Token: 0x04001738 RID: 5944
			internal LineInfo lineInfo2;

			// Token: 0x04001739 RID: 5945
			internal char quoteChar;

			// Token: 0x0400173A RID: 5946
			internal int depth;

			// Token: 0x0400173B RID: 5947
			private bool isEmptyOrDefault;

			// Token: 0x0400173C RID: 5948
			internal int entityId;

			// Token: 0x0400173D RID: 5949
			internal bool xmlContextPushed;

			// Token: 0x0400173E RID: 5950
			internal XmlTextReaderImpl.NodeData nextAttrValueChunk;

			// Token: 0x0400173F RID: 5951
			internal object schemaType;

			// Token: 0x04001740 RID: 5952
			internal object typedValue;

			// Token: 0x020004DB RID: 1243
			internal sealed class AtomizedNameEqualityComparer : IEqualityComparer<XmlTextReaderImpl.NodeData>
			{
				// Token: 0x060031C1 RID: 12737 RVA: 0x0012118B File Offset: 0x0011F38B
				public bool Equals(XmlTextReaderImpl.NodeData x, XmlTextReaderImpl.NodeData y)
				{
					if (x == null)
					{
						return y == null;
					}
					return y != null && Ref.Equal(x.localName, y.localName) && Ref.Equal(x.ns, y.ns);
				}

				// Token: 0x060031C2 RID: 12738 RVA: 0x001211BE File Offset: 0x0011F3BE
				public int GetHashCode(XmlTextReaderImpl.NodeData node)
				{
					if (node == null)
					{
						return 0;
					}
					return Ref.CombineHashRef(RuntimeHelpers.GetHashCode(node.localName), node.ns);
				}

				// Token: 0x04001FA6 RID: 8102
				internal static readonly XmlTextReaderImpl.NodeData.AtomizedNameEqualityComparer Instance = new XmlTextReaderImpl.NodeData.AtomizedNameEqualityComparer();
			}
		}

		// Token: 0x02000379 RID: 889
		private class DtdDefaultAttributeInfoToNodeDataComparer : IComparer<object>
		{
			// Token: 0x17000A3D RID: 2621
			// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000F91C8 File Offset: 0x000F73C8
			internal static IComparer<object> Instance
			{
				get
				{
					return XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.s_instance;
				}
			}

			// Token: 0x06002EC5 RID: 11973 RVA: 0x000F91D0 File Offset: 0x000F73D0
			public int Compare(object x, object y)
			{
				if (x == null)
				{
					if (y != null)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (y == null)
					{
						return 1;
					}
					XmlTextReaderImpl.NodeData nodeData = x as XmlTextReaderImpl.NodeData;
					string localName;
					string prefix;
					if (nodeData != null)
					{
						localName = nodeData.localName;
						prefix = nodeData.prefix;
					}
					else
					{
						IDtdDefaultAttributeInfo dtdDefaultAttributeInfo = x as IDtdDefaultAttributeInfo;
						if (dtdDefaultAttributeInfo == null)
						{
							throw new XmlException("Xml_DefaultException", string.Empty);
						}
						localName = dtdDefaultAttributeInfo.LocalName;
						prefix = dtdDefaultAttributeInfo.Prefix;
					}
					nodeData = (y as XmlTextReaderImpl.NodeData);
					string localName2;
					string prefix2;
					if (nodeData != null)
					{
						localName2 = nodeData.localName;
						prefix2 = nodeData.prefix;
					}
					else
					{
						IDtdDefaultAttributeInfo dtdDefaultAttributeInfo2 = y as IDtdDefaultAttributeInfo;
						if (dtdDefaultAttributeInfo2 == null)
						{
							throw new XmlException("Xml_DefaultException", string.Empty);
						}
						localName2 = dtdDefaultAttributeInfo2.LocalName;
						prefix2 = dtdDefaultAttributeInfo2.Prefix;
					}
					int num = string.Compare(localName, localName2, StringComparison.Ordinal);
					if (num != 0)
					{
						return num;
					}
					return string.Compare(prefix, prefix2, StringComparison.Ordinal);
				}
			}

			// Token: 0x04001741 RID: 5953
			private static IComparer<object> s_instance = new XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer();
		}

		// Token: 0x0200037A RID: 890
		// (Invoke) Token: 0x06002EC9 RID: 11977
		internal delegate void OnDefaultAttributeUseDelegate(IDtdDefaultAttributeInfo defaultAttribute, XmlTextReaderImpl coreReader);
	}
}
