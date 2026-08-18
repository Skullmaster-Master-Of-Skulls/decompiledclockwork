using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000127 RID: 295
	internal sealed class XmlSqlBinaryReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x060014B1 RID: 5297 RVA: 0x00055770 File Offset: 0x00053970
		public XmlSqlBinaryReader(Stream stream, byte[] data, int len, string baseUri, bool closeInput, XmlReaderSettings settings)
		{
			this.unicode = Encoding.Unicode;
			this.xmlCharType = XmlCharType.Instance;
			this.xnt = settings.NameTable;
			if (this.xnt == null)
			{
				this.xnt = new NameTable();
				this.xntFromSettings = false;
			}
			else
			{
				this.xntFromSettings = true;
			}
			this.xml = this.xnt.Add("xml");
			this.xmlns = this.xnt.Add("xmlns");
			this.nsxmlns = this.xnt.Add("http://www.w3.org/2000/xmlns/");
			this.baseUri = baseUri;
			this.state = XmlSqlBinaryReader.ScanState.Init;
			this.nodetype = XmlNodeType.None;
			this.token = BinXmlToken.Error;
			this.elementStack = new XmlSqlBinaryReader.ElemInfo[16];
			this.attributes = new XmlSqlBinaryReader.AttrInfo[8];
			this.attrHashTbl = new int[8];
			this.symbolTables.Init();
			this.qnameOther.Clear();
			this.qnameElement.Clear();
			this.xmlspacePreserve = false;
			this.hasher = new SecureStringHasher();
			this.namespaces = new Dictionary<string, XmlSqlBinaryReader.NamespaceDecl>(this.hasher);
			this.AddInitNamespace(string.Empty, string.Empty);
			this.AddInitNamespace(this.xml, this.xnt.Add("http://www.w3.org/XML/1998/namespace"));
			this.AddInitNamespace(this.xmlns, this.nsxmlns);
			this.valueType = XmlSqlBinaryReader.TypeOfString;
			this.inStrm = stream;
			if (data != null)
			{
				this.data = data;
				this.end = len;
				this.pos = 2;
				this.sniffed = true;
			}
			else
			{
				this.data = new byte[4096];
				this.end = stream.Read(this.data, 0, 4096);
				this.pos = 0;
				this.sniffed = false;
			}
			this.mark = -1;
			this.eof = (this.end == 0);
			this.offset = 0L;
			this.closeInput = closeInput;
			switch (settings.ConformanceLevel)
			{
			case ConformanceLevel.Auto:
				this.docState = 0;
				break;
			case ConformanceLevel.Fragment:
				this.docState = 9;
				break;
			case ConformanceLevel.Document:
				this.docState = 1;
				break;
			}
			this.checkCharacters = settings.CheckCharacters;
			this.dtdProcessing = settings.DtdProcessing;
			this.ignoreWhitespace = settings.IgnoreWhitespace;
			this.ignorePIs = settings.IgnoreProcessingInstructions;
			this.ignoreComments = settings.IgnoreComments;
			if (XmlSqlBinaryReader.TokenTypeMap == null)
			{
				this.GenerateTokenTypeMap();
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x000559E4 File Offset: 0x00053BE4
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				if (this.xntFromSettings)
				{
					xmlReaderSettings.NameTable = this.xnt;
				}
				int num = this.docState;
				if (num != 0)
				{
					if (num != 9)
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
					}
					else
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
					}
				}
				else
				{
					xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
				}
				xmlReaderSettings.CheckCharacters = this.checkCharacters;
				xmlReaderSettings.IgnoreWhitespace = this.ignoreWhitespace;
				xmlReaderSettings.IgnoreProcessingInstructions = this.ignorePIs;
				xmlReaderSettings.IgnoreComments = this.ignoreComments;
				xmlReaderSettings.DtdProcessing = this.dtdProcessing;
				xmlReaderSettings.CloseInput = this.closeInput;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00055A85 File Offset: 0x00053C85
		public override XmlNodeType NodeType
		{
			get
			{
				return this.nodetype;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00055A8D File Offset: 0x00053C8D
		public override string LocalName
		{
			get
			{
				return this.qnameOther.localname;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00055A9A File Offset: 0x00053C9A
		public override string NamespaceURI
		{
			get
			{
				return this.qnameOther.namespaceUri;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00055AA7 File Offset: 0x00053CA7
		public override string Prefix
		{
			get
			{
				return this.qnameOther.prefix;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00055AB4 File Offset: 0x00053CB4
		public override bool HasValue
		{
			get
			{
				if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
				{
					return this.textXmlReader.HasValue;
				}
				return XmlReader.HasValueInternal(this.nodetype);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00055AD8 File Offset: 0x00053CD8
		public override string Value
		{
			get
			{
				if (this.stringValue != null)
				{
					return this.stringValue;
				}
				switch (this.state)
				{
				case XmlSqlBinaryReader.ScanState.Doc:
					switch (this.nodetype)
					{
					case XmlNodeType.Text:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						return this.stringValue = this.ValueAsString(this.token);
					case XmlNodeType.CDATA:
						return this.stringValue = this.CDATAValue();
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.DocumentType:
						return this.stringValue = this.GetString(this.tokDataPos, this.tokLen);
					case XmlNodeType.XmlDeclaration:
						return this.stringValue = this.XmlDeclValue();
					}
					break;
				case XmlSqlBinaryReader.ScanState.XmlText:
					return this.textXmlReader.Value;
				case XmlSqlBinaryReader.ScanState.Attr:
				case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
					return this.stringValue = this.GetAttributeText(this.attrIndex - 1);
				case XmlSqlBinaryReader.ScanState.AttrVal:
					return this.stringValue = this.ValueAsString(this.token);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00055BFC File Offset: 0x00053DFC
		public override int Depth
		{
			get
			{
				int num = 0;
				switch (this.state)
				{
				case XmlSqlBinaryReader.ScanState.Doc:
					if (this.nodetype == XmlNodeType.Element || this.nodetype == XmlNodeType.EndElement)
					{
						num = -1;
					}
					break;
				case XmlSqlBinaryReader.ScanState.XmlText:
					num = this.textXmlReader.Depth;
					break;
				case XmlSqlBinaryReader.ScanState.Attr:
					if (this.parentNodeType != XmlNodeType.Element)
					{
						num = 1;
					}
					break;
				case XmlSqlBinaryReader.ScanState.AttrVal:
				case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
					if (this.parentNodeType != XmlNodeType.Element)
					{
						num = 1;
					}
					num++;
					break;
				default:
					return 0;
				}
				return this.elemDepth + num;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00055C7B File Offset: 0x00053E7B
		public override string BaseURI
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00055C84 File Offset: 0x00053E84
		public override bool IsEmptyElement
		{
			get
			{
				XmlSqlBinaryReader.ScanState scanState = this.state;
				return scanState <= XmlSqlBinaryReader.ScanState.XmlText && this.isEmpty;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00055CA4 File Offset: 0x00053EA4
		public override XmlSpace XmlSpace
		{
			get
			{
				if (XmlSqlBinaryReader.ScanState.XmlText != this.state)
				{
					for (int i = this.elemDepth; i >= 0; i--)
					{
						XmlSpace xmlSpace = this.elementStack[i].xmlSpace;
						if (xmlSpace != XmlSpace.None)
						{
							return xmlSpace;
						}
					}
					return XmlSpace.None;
				}
				return this.textXmlReader.XmlSpace;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00055CF0 File Offset: 0x00053EF0
		public override string XmlLang
		{
			get
			{
				if (XmlSqlBinaryReader.ScanState.XmlText != this.state)
				{
					for (int i = this.elemDepth; i >= 0; i--)
					{
						string xmlLang = this.elementStack[i].xmlLang;
						if (xmlLang != null)
						{
							return xmlLang;
						}
					}
					return string.Empty;
				}
				return this.textXmlReader.XmlLang;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00055D3F File Offset: 0x00053F3F
		public override Type ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00055D48 File Offset: 0x00053F48
		public override int AttributeCount
		{
			get
			{
				switch (this.state)
				{
				case XmlSqlBinaryReader.ScanState.Doc:
				case XmlSqlBinaryReader.ScanState.Attr:
				case XmlSqlBinaryReader.ScanState.AttrVal:
				case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
					return this.attrCount;
				case XmlSqlBinaryReader.ScanState.XmlText:
					return this.textXmlReader.AttributeCount;
				default:
					return 0;
				}
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00055D8C File Offset: 0x00053F8C
		public override string GetAttribute(string name, string ns)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.textXmlReader.GetAttribute(name, ns);
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int num = this.LocateAttribute(name, ns);
			if (-1 == num)
			{
				return null;
			}
			return this.GetAttribute(num);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00055DE0 File Offset: 0x00053FE0
		public override string GetAttribute(string name)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.textXmlReader.GetAttribute(name);
			}
			int num = this.LocateAttribute(name);
			if (-1 == num)
			{
				return null;
			}
			return this.GetAttribute(num);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00055E18 File Offset: 0x00054018
		public override string GetAttribute(int i)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.textXmlReader.GetAttribute(i);
			}
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.GetAttributeText(i);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00055E50 File Offset: 0x00054050
		public override bool MoveToAttribute(string name, string ns)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.UpdateFromTextReader(this.textXmlReader.MoveToAttribute(name, ns));
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int num = this.LocateAttribute(name, ns);
			if (-1 != num && this.state < XmlSqlBinaryReader.ScanState.Init)
			{
				this.PositionOnAttribute(num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00055EB4 File Offset: 0x000540B4
		public override bool MoveToAttribute(string name)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.UpdateFromTextReader(this.textXmlReader.MoveToAttribute(name));
			}
			int num = this.LocateAttribute(name);
			if (-1 != num && this.state < XmlSqlBinaryReader.ScanState.Init)
			{
				this.PositionOnAttribute(num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00055F00 File Offset: 0x00054100
		public override void MoveToAttribute(int i)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				this.textXmlReader.MoveToAttribute(i);
				this.UpdateFromTextReader(true);
				return;
			}
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.PositionOnAttribute(i + 1);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00055F4C File Offset: 0x0005414C
		public override bool MoveToFirstAttribute()
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.UpdateFromTextReader(this.textXmlReader.MoveToFirstAttribute());
			}
			if (this.attrCount == 0)
			{
				return false;
			}
			this.PositionOnAttribute(1);
			return true;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00055F7C File Offset: 0x0005417C
		public override bool MoveToNextAttribute()
		{
			switch (this.state)
			{
			case XmlSqlBinaryReader.ScanState.Doc:
			case XmlSqlBinaryReader.ScanState.Attr:
			case XmlSqlBinaryReader.ScanState.AttrVal:
			case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
			{
				if (this.attrIndex >= this.attrCount)
				{
					return false;
				}
				int i = this.attrIndex + 1;
				this.attrIndex = i;
				this.PositionOnAttribute(i);
				return true;
			}
			case XmlSqlBinaryReader.ScanState.XmlText:
				return this.UpdateFromTextReader(this.textXmlReader.MoveToNextAttribute());
			default:
				return false;
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00055FE8 File Offset: 0x000541E8
		public override bool MoveToElement()
		{
			XmlSqlBinaryReader.ScanState scanState = this.state;
			if (scanState == XmlSqlBinaryReader.ScanState.XmlText)
			{
				return this.UpdateFromTextReader(this.textXmlReader.MoveToElement());
			}
			if (scanState - XmlSqlBinaryReader.ScanState.Attr <= 2)
			{
				this.attrIndex = 0;
				this.qnameOther = this.qnameElement;
				if (XmlNodeType.Element == this.parentNodeType)
				{
					this.token = BinXmlToken.Element;
				}
				else if (XmlNodeType.XmlDeclaration == this.parentNodeType)
				{
					this.token = BinXmlToken.XmlDecl;
				}
				else if (XmlNodeType.DocumentType == this.parentNodeType)
				{
					this.token = BinXmlToken.DocType;
				}
				this.nodetype = this.parentNodeType;
				this.state = XmlSqlBinaryReader.ScanState.Doc;
				this.pos = this.posAfterAttrs;
				this.stringValue = null;
				return true;
			}
			return false;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x0005609C File Offset: 0x0005429C
		public override bool EOF
		{
			get
			{
				return this.state == XmlSqlBinaryReader.ScanState.EOF;
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x000560A8 File Offset: 0x000542A8
		public override bool ReadAttributeValue()
		{
			this.stringValue = null;
			switch (this.state)
			{
			case XmlSqlBinaryReader.ScanState.XmlText:
				return this.UpdateFromTextReader(this.textXmlReader.ReadAttributeValue());
			case XmlSqlBinaryReader.ScanState.Attr:
				if (this.attributes[this.attrIndex - 1].val == null)
				{
					this.pos = this.attributes[this.attrIndex - 1].contentPos;
					BinXmlToken binXmlToken = this.RescanNextToken();
					if (BinXmlToken.Attr == binXmlToken || BinXmlToken.EndAttrs == binXmlToken)
					{
						return false;
					}
					this.token = binXmlToken;
					this.ReScanOverValue(binXmlToken);
					this.valueType = this.GetValueType(binXmlToken);
					this.state = XmlSqlBinaryReader.ScanState.AttrVal;
				}
				else
				{
					this.token = BinXmlToken.Error;
					this.valueType = XmlSqlBinaryReader.TypeOfString;
					this.state = XmlSqlBinaryReader.ScanState.AttrValPseudoValue;
				}
				this.qnameOther.Clear();
				this.nodetype = XmlNodeType.Text;
				return true;
			case XmlSqlBinaryReader.ScanState.AttrVal:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00056194 File Offset: 0x00054394
		public override void Close()
		{
			this.state = XmlSqlBinaryReader.ScanState.Closed;
			this.nodetype = XmlNodeType.None;
			this.token = BinXmlToken.Error;
			this.stringValue = null;
			if (this.textXmlReader != null)
			{
				this.textXmlReader.Close();
				this.textXmlReader = null;
			}
			if (this.inStrm != null && this.closeInput)
			{
				this.inStrm.Close();
			}
			this.inStrm = null;
			this.pos = (this.end = 0);
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00056209 File Offset: 0x00054409
		public override XmlNameTable NameTable
		{
			get
			{
				return this.xnt;
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00056214 File Offset: 0x00054414
		public override string LookupNamespace(string prefix)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.textXmlReader.LookupNamespace(prefix);
			}
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl;
			if (prefix != null && this.namespaces.TryGetValue(prefix, out namespaceDecl))
			{
				return namespaceDecl.uri;
			}
			return null;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00056252 File Offset: 0x00054452
		public override void ResolveEntity()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00056259 File Offset: 0x00054459
		public override ReadState ReadState
		{
			get
			{
				return XmlSqlBinaryReader.ScanState2ReadState[(int)this.state];
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00056268 File Offset: 0x00054468
		public override bool Read()
		{
			bool result;
			try
			{
				switch (this.state)
				{
				case XmlSqlBinaryReader.ScanState.Doc:
					break;
				case XmlSqlBinaryReader.ScanState.XmlText:
					if (this.textXmlReader.Read())
					{
						return this.UpdateFromTextReader(true);
					}
					this.state = XmlSqlBinaryReader.ScanState.Doc;
					this.nodetype = XmlNodeType.None;
					this.isEmpty = false;
					break;
				case XmlSqlBinaryReader.ScanState.Attr:
				case XmlSqlBinaryReader.ScanState.AttrVal:
				case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
					this.MoveToElement();
					break;
				case XmlSqlBinaryReader.ScanState.Init:
					return this.ReadInit(false);
				default:
					return false;
				}
				result = this.ReadDoc();
			}
			catch (OverflowException ex)
			{
				this.state = XmlSqlBinaryReader.ScanState.Error;
				throw new XmlException(ex.Message, ex);
			}
			catch
			{
				this.state = XmlSqlBinaryReader.ScanState.Error;
				throw;
			}
			return result;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00056328 File Offset: 0x00054528
		private bool SetupContentAsXXX(string name)
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException(name);
			}
			switch (this.state)
			{
			case XmlSqlBinaryReader.ScanState.Doc:
				if (this.NodeType == XmlNodeType.EndElement)
				{
					return true;
				}
				if (this.NodeType == XmlNodeType.ProcessingInstruction || this.NodeType == XmlNodeType.Comment)
				{
					while (this.Read() && (this.NodeType == XmlNodeType.ProcessingInstruction || this.NodeType == XmlNodeType.Comment))
					{
					}
					if (this.NodeType == XmlNodeType.EndElement)
					{
						return true;
					}
				}
				if (this.hasTypedValue)
				{
					return true;
				}
				break;
			case XmlSqlBinaryReader.ScanState.Attr:
			{
				this.pos = this.attributes[this.attrIndex - 1].contentPos;
				BinXmlToken binXmlToken = this.RescanNextToken();
				if (BinXmlToken.Attr != binXmlToken && BinXmlToken.EndAttrs != binXmlToken)
				{
					this.token = binXmlToken;
					this.ReScanOverValue(binXmlToken);
					return true;
				}
				break;
			}
			case XmlSqlBinaryReader.ScanState.AttrVal:
				return true;
			}
			return false;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00056404 File Offset: 0x00054604
		private int FinishContentAsXXX(int origPos)
		{
			if (this.state == XmlSqlBinaryReader.ScanState.Doc)
			{
				if (this.NodeType != XmlNodeType.Element && this.NodeType != XmlNodeType.EndElement)
				{
					while (this.Read())
					{
						XmlNodeType nodeType = this.NodeType;
						if (nodeType == XmlNodeType.Element)
						{
							break;
						}
						if (nodeType - XmlNodeType.ProcessingInstruction > 1)
						{
							if (nodeType != XmlNodeType.EndElement)
							{
								throw this.ThrowNotSupported("XmlBinary_ListsOfValuesNotSupported");
							}
							break;
						}
					}
				}
				return this.pos;
			}
			return origPos;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00056460 File Offset: 0x00054660
		public override bool ReadContentAsBoolean()
		{
			int origPos = this.pos;
			bool result = false;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsBoolean"))
				{
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
						case BinXmlToken.SQL_SMALLMONEY:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							break;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_187;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_143;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
							case BinXmlToken.XSD_QNAME:
								break;
							case (BinXmlToken)128:
								goto IL_143;
							case BinXmlToken.XSD_BOOLEAN:
								result = (this.data[this.tokDataPos] > 0);
								goto IL_171;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_143;
								}
								return XmlConvert.ToBoolean(string.Empty);
							}
							break;
						}
						throw new InvalidCastException(Res.GetString("XmlBinary_CastNotSupported", new object[]
						{
							this.token,
							"Boolean"
						}));
						IL_143:
						goto IL_187;
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException2, null);
					}
					IL_171:
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			IL_187:
			return base.ReadContentAsBoolean();
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00056648 File Offset: 0x00054848
		public override DateTime ReadContentAsDateTime()
		{
			int origPos = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsDateTime"))
				{
					DateTime result;
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_SMALLMONEY:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							goto IL_FC;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_191;
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
							break;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_138;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
								break;
							case (BinXmlToken)128:
								goto IL_138;
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_BOOLEAN:
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
							case BinXmlToken.XSD_QNAME:
								goto IL_FC;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_138;
								}
								return XmlConvert.ToDateTime(string.Empty, XmlDateTimeSerializationMode.RoundtripKind);
							}
							break;
						}
						result = this.ValueAsDateTime();
						goto IL_17B;
						IL_FC:
						throw new InvalidCastException(Res.GetString("XmlBinary_CastNotSupported", new object[]
						{
							this.token,
							"DateTime"
						}));
						IL_138:
						goto IL_191;
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException2, null);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException3, null);
					}
					IL_17B:
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			IL_191:
			return base.ReadContentAsDateTime();
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00056854 File Offset: 0x00054A54
		public override double ReadContentAsDouble()
		{
			int origPos = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsDouble"))
				{
					double result;
					try
					{
						BinXmlToken binXmlToken = this.token;
						if (binXmlToken <= BinXmlToken.XSD_KATMAI_DATE)
						{
							switch (binXmlToken)
							{
							case BinXmlToken.SQL_SMALLINT:
							case BinXmlToken.SQL_INT:
							case BinXmlToken.SQL_MONEY:
							case BinXmlToken.SQL_BIT:
							case BinXmlToken.SQL_TINYINT:
							case BinXmlToken.SQL_BIGINT:
							case BinXmlToken.SQL_UUID:
							case BinXmlToken.SQL_DECIMAL:
							case BinXmlToken.SQL_NUMERIC:
							case BinXmlToken.SQL_BINARY:
							case BinXmlToken.SQL_VARBINARY:
							case BinXmlToken.SQL_DATETIME:
							case BinXmlToken.SQL_SMALLDATETIME:
							case BinXmlToken.SQL_SMALLMONEY:
							case BinXmlToken.SQL_IMAGE:
							case BinXmlToken.SQL_UDT:
								break;
							case BinXmlToken.SQL_REAL:
							case BinXmlToken.SQL_FLOAT:
								result = this.ValueAsDouble();
								goto IL_13E;
							case BinXmlToken.SQL_CHAR:
							case BinXmlToken.SQL_NCHAR:
							case BinXmlToken.SQL_VARCHAR:
							case BinXmlToken.SQL_NVARCHAR:
							case BinXmlToken.SQL_TEXT:
							case BinXmlToken.SQL_NTEXT:
								goto IL_154;
							case (BinXmlToken)21:
							case (BinXmlToken)25:
							case (BinXmlToken)26:
								goto IL_FB;
							default:
								if (binXmlToken - BinXmlToken.XSD_KATMAI_TIMEOFFSET > 5)
								{
									goto IL_FB;
								}
								break;
							}
						}
						else if (binXmlToken - BinXmlToken.XSD_TIME > 11)
						{
							if (binXmlToken - BinXmlToken.EndElem > 1)
							{
								goto IL_FB;
							}
							return XmlConvert.ToDouble(string.Empty);
						}
						throw new InvalidCastException(Res.GetString("XmlBinary_CastNotSupported", new object[]
						{
							this.token,
							"Double"
						}));
						IL_FB:
						goto IL_154;
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException2, null);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException3, null);
					}
					IL_13E:
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			IL_154:
			return base.ReadContentAsDouble();
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00056A24 File Offset: 0x00054C24
		public override float ReadContentAsFloat()
		{
			int origPos = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsFloat"))
				{
					float result;
					try
					{
						BinXmlToken binXmlToken = this.token;
						if (binXmlToken <= BinXmlToken.XSD_KATMAI_DATE)
						{
							switch (binXmlToken)
							{
							case BinXmlToken.SQL_SMALLINT:
							case BinXmlToken.SQL_INT:
							case BinXmlToken.SQL_MONEY:
							case BinXmlToken.SQL_BIT:
							case BinXmlToken.SQL_TINYINT:
							case BinXmlToken.SQL_BIGINT:
							case BinXmlToken.SQL_UUID:
							case BinXmlToken.SQL_DECIMAL:
							case BinXmlToken.SQL_NUMERIC:
							case BinXmlToken.SQL_BINARY:
							case BinXmlToken.SQL_VARBINARY:
							case BinXmlToken.SQL_DATETIME:
							case BinXmlToken.SQL_SMALLDATETIME:
							case BinXmlToken.SQL_SMALLMONEY:
							case BinXmlToken.SQL_IMAGE:
							case BinXmlToken.SQL_UDT:
								break;
							case BinXmlToken.SQL_REAL:
							case BinXmlToken.SQL_FLOAT:
								result = (float)this.ValueAsDouble();
								goto IL_13F;
							case BinXmlToken.SQL_CHAR:
							case BinXmlToken.SQL_NCHAR:
							case BinXmlToken.SQL_VARCHAR:
							case BinXmlToken.SQL_NVARCHAR:
							case BinXmlToken.SQL_TEXT:
							case BinXmlToken.SQL_NTEXT:
								goto IL_155;
							case (BinXmlToken)21:
							case (BinXmlToken)25:
							case (BinXmlToken)26:
								goto IL_FC;
							default:
								if (binXmlToken - BinXmlToken.XSD_KATMAI_TIMEOFFSET > 5)
								{
									goto IL_FC;
								}
								break;
							}
						}
						else if (binXmlToken - BinXmlToken.XSD_TIME > 11)
						{
							if (binXmlToken - BinXmlToken.EndElem > 1)
							{
								goto IL_FC;
							}
							return XmlConvert.ToSingle(string.Empty);
						}
						throw new InvalidCastException(Res.GetString("XmlBinary_CastNotSupported", new object[]
						{
							this.token,
							"Float"
						}));
						IL_FC:
						goto IL_155;
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException2, null);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException3, null);
					}
					IL_13F:
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			IL_155:
			return base.ReadContentAsFloat();
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00056BF4 File Offset: 0x00054DF4
		public override decimal ReadContentAsDecimal()
		{
			int origPos = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsDecimal"))
				{
					decimal result;
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_SMALLMONEY:
							break;
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							goto IL_FC;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_190;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_137;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_BOOLEAN:
							case BinXmlToken.XSD_QNAME:
								goto IL_FC;
							case (BinXmlToken)128:
								goto IL_137;
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
								break;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_137;
								}
								return XmlConvert.ToDecimal(string.Empty);
							}
							break;
						}
						result = this.ValueAsDecimal();
						goto IL_17A;
						IL_FC:
						throw new InvalidCastException(Res.GetString("XmlBinary_CastNotSupported", new object[]
						{
							this.token,
							"Decimal"
						}));
						IL_137:
						goto IL_190;
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException2, null);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException3, null);
					}
					IL_17A:
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			IL_190:
			return base.ReadContentAsDecimal();
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00056E00 File Offset: 0x00055000
		public override int ReadContentAsInt()
		{
			int origPos = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsInt"))
				{
					int result;
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_SMALLMONEY:
							break;
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							goto IL_FD;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_191;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_138;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_BOOLEAN:
							case BinXmlToken.XSD_QNAME:
								goto IL_FD;
							case (BinXmlToken)128:
								goto IL_138;
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
								break;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_138;
								}
								return XmlConvert.ToInt32(string.Empty);
							}
							break;
						}
						result = checked((int)this.ValueAsLong());
						goto IL_17B;
						IL_FD:
						throw new InvalidCastException(Res.GetString("XmlBinary_CastNotSupported", new object[]
						{
							this.token,
							"Int32"
						}));
						IL_138:
						goto IL_191;
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Int32", innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Int32", innerException2, null);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Int32", innerException3, null);
					}
					IL_17B:
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			IL_191:
			return base.ReadContentAsInt();
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0005700C File Offset: 0x0005520C
		public override long ReadContentAsLong()
		{
			int origPos = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsLong"))
				{
					long result;
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_SMALLMONEY:
							break;
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							goto IL_FC;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_190;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_137;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_BOOLEAN:
							case BinXmlToken.XSD_QNAME:
								goto IL_FC;
							case (BinXmlToken)128:
								goto IL_137;
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
								break;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_137;
								}
								return XmlConvert.ToInt64(string.Empty);
							}
							break;
						}
						result = this.ValueAsLong();
						goto IL_17A;
						IL_FC:
						throw new InvalidCastException(Res.GetString("XmlBinary_CastNotSupported", new object[]
						{
							this.token,
							"Int64"
						}));
						IL_137:
						goto IL_190;
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Int64", innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Int64", innerException2, null);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Int64", innerException3, null);
					}
					IL_17A:
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			IL_190:
			return base.ReadContentAsLong();
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00057218 File Offset: 0x00055418
		public override object ReadContentAsObject()
		{
			int origPos = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsObject"))
				{
					object result;
					try
					{
						if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
						{
							result = string.Empty;
						}
						else
						{
							result = this.ValueAsObject(this.token, false);
						}
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Object", innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Object", innerException2, null);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", "Object", innerException3, null);
					}
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			return base.ReadContentAsObject();
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x000572F4 File Offset: 0x000554F4
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			int origPos = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAs"))
				{
					object result;
					try
					{
						if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
						{
							result = string.Empty;
						}
						else if (returnType == this.ValueType || returnType == typeof(object))
						{
							result = this.ValueAsObject(this.token, false);
						}
						else
						{
							result = this.ValueAs(this.token, returnType, namespaceResolver);
						}
					}
					catch (InvalidCastException innerException)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException, null);
					}
					catch (FormatException innerException2)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException2, null);
					}
					catch (OverflowException innerException3)
					{
						throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException3, null);
					}
					origPos = this.FinishContentAsXXX(origPos);
					return result;
				}
			}
			finally
			{
				this.pos = origPos;
			}
			return base.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00057408 File Offset: 0x00055608
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				IXmlNamespaceResolver xmlNamespaceResolver = (IXmlNamespaceResolver)this.textXmlReader;
				return xmlNamespaceResolver.GetNamespacesInScope(scope);
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (XmlNamespaceScope.Local == scope)
			{
				if (this.elemDepth > 0)
				{
					for (XmlSqlBinaryReader.NamespaceDecl namespaceDecl = this.elementStack[this.elemDepth].nsdecls; namespaceDecl != null; namespaceDecl = namespaceDecl.scopeLink)
					{
						dictionary.Add(namespaceDecl.prefix, namespaceDecl.uri);
					}
				}
			}
			else
			{
				foreach (XmlSqlBinaryReader.NamespaceDecl namespaceDecl2 in this.namespaces.Values)
				{
					if ((namespaceDecl2.scope != -1 || (scope == XmlNamespaceScope.All && "xml" == namespaceDecl2.prefix)) && (namespaceDecl2.prefix.Length > 0 || namespaceDecl2.uri.Length > 0))
					{
						dictionary.Add(namespaceDecl2.prefix, namespaceDecl2.uri);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0005751C File Offset: 0x0005571C
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				IXmlNamespaceResolver xmlNamespaceResolver = (IXmlNamespaceResolver)this.textXmlReader;
				return xmlNamespaceResolver.LookupPrefix(namespaceName);
			}
			if (namespaceName == null)
			{
				return null;
			}
			namespaceName = this.xnt.Get(namespaceName);
			if (namespaceName == null)
			{
				return null;
			}
			for (int i = this.elemDepth; i >= 0; i--)
			{
				for (XmlSqlBinaryReader.NamespaceDecl namespaceDecl = this.elementStack[i].nsdecls; namespaceDecl != null; namespaceDecl = namespaceDecl.scopeLink)
				{
					if (namespaceDecl.uri == namespaceName)
					{
						return namespaceDecl.prefix;
					}
				}
			}
			return null;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0005759E File Offset: 0x0005579E
		private void VerifyVersion(int requiredVersion, BinXmlToken token)
		{
			if ((int)this.version < requiredVersion)
			{
				throw this.ThrowUnexpectedToken(token);
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000575B4 File Offset: 0x000557B4
		private void AddInitNamespace(string prefix, string uri)
		{
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl = new XmlSqlBinaryReader.NamespaceDecl(prefix, uri, this.elementStack[0].nsdecls, null, -1, true);
			this.elementStack[0].nsdecls = namespaceDecl;
			this.namespaces.Add(prefix, namespaceDecl);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000575FC File Offset: 0x000557FC
		private void AddName()
		{
			string array = this.ParseText();
			int symCount = this.symbolTables.symCount;
			this.symbolTables.symCount = symCount + 1;
			int num = symCount;
			string[] array2 = this.symbolTables.symtable;
			if (num == array2.Length)
			{
				string[] array3 = new string[checked(num * 2)];
				Array.Copy(array2, 0, array3, 0, num);
				array2 = (this.symbolTables.symtable = array3);
			}
			array2[num] = this.xnt.Add(array);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0005766C File Offset: 0x0005586C
		private void AddQName()
		{
			int num = this.ReadNameRef();
			int num2 = this.ReadNameRef();
			int num3 = this.ReadNameRef();
			int qnameCount = this.symbolTables.qnameCount;
			this.symbolTables.qnameCount = qnameCount + 1;
			int num4 = qnameCount;
			XmlSqlBinaryReader.QName[] array = this.symbolTables.qnametable;
			if (num4 == array.Length)
			{
				XmlSqlBinaryReader.QName[] array2 = new XmlSqlBinaryReader.QName[checked(num4 * 2)];
				Array.Copy(array, 0, array2, 0, num4);
				array = (this.symbolTables.qnametable = array2);
			}
			string[] symtable = this.symbolTables.symtable;
			string text = symtable[num2];
			string lname;
			string nsUri;
			if (num3 == 0)
			{
				if (num2 == 0 && num == 0)
				{
					return;
				}
				if (text.StartsWith("xmlns", StringComparison.Ordinal))
				{
					if (5 < text.Length)
					{
						if (6 == text.Length || ':' != text[5])
						{
							goto IL_106;
						}
						lname = this.xnt.Add(text.Substring(6));
						text = this.xmlns;
					}
					else
					{
						lname = text;
						text = string.Empty;
					}
					nsUri = this.nsxmlns;
					goto IL_F2;
				}
				IL_106:
				throw new XmlException("Xml_BadNamespaceDecl", null);
			}
			else
			{
				lname = symtable[num3];
				nsUri = symtable[num];
			}
			IL_F2:
			array[num4].Set(text, lname, nsUri);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0005778C File Offset: 0x0005598C
		private void NameFlush()
		{
			this.symbolTables.symCount = (this.symbolTables.qnameCount = 1);
			Array.Clear(this.symbolTables.symtable, 1, this.symbolTables.symtable.Length - 1);
			Array.Clear(this.symbolTables.qnametable, 0, this.symbolTables.qnametable.Length);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000577F4 File Offset: 0x000559F4
		private void SkipExtn()
		{
			int num = this.ParseMB32();
			checked
			{
				this.pos += num;
				this.Fill(-1);
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00057820 File Offset: 0x00055A20
		private int ReadQNameRef()
		{
			int num = this.ParseMB32();
			if (num < 0 || num >= this.symbolTables.qnameCount)
			{
				throw new XmlException("XmlBin_InvalidQNameID", string.Empty);
			}
			return num;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00057858 File Offset: 0x00055A58
		private int ReadNameRef()
		{
			int num = this.ParseMB32();
			if (num < 0 || num >= this.symbolTables.symCount)
			{
				throw new XmlException("XmlBin_InvalidQNameID", string.Empty);
			}
			return num;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00057890 File Offset: 0x00055A90
		private bool FillAllowEOF()
		{
			if (this.eof)
			{
				return false;
			}
			byte[] array = this.data;
			int num = this.pos;
			int num2 = this.mark;
			int num3 = this.end;
			if (num2 == -1)
			{
				num2 = num;
			}
			if (num2 >= 0 && num2 < num3)
			{
				int num4 = num3 - num2;
				if (num4 > 7 * (array.Length / 8))
				{
					byte[] destinationArray = new byte[checked(array.Length * 2)];
					Array.Copy(array, num2, destinationArray, 0, num4);
					array = (this.data = destinationArray);
				}
				else
				{
					Array.Copy(array, num2, array, 0, num4);
				}
				num -= num2;
				num3 -= num2;
				this.tokDataPos -= num2;
				for (int i = 0; i < this.attrCount; i++)
				{
					this.attributes[i].AdjustPosition(-num2);
				}
				this.pos = num;
				this.mark = 0;
				this.offset += (long)num2;
			}
			else
			{
				this.pos -= num3;
				this.mark -= num3;
				this.offset += (long)num3;
				this.tokDataPos -= num3;
				num3 = 0;
			}
			int count = array.Length - num3;
			int num5 = this.inStrm.Read(array, num3, count);
			this.end = num3 + num5;
			this.eof = (num5 <= 0);
			return num5 > 0;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000579E4 File Offset: 0x00055BE4
		private void Fill_(int require)
		{
			while (this.FillAllowEOF() && this.pos + require >= this.end)
			{
			}
			if (this.pos + require >= this.end)
			{
				throw this.ThrowXmlException("Xml_UnexpectedEOF1");
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00057A1A File Offset: 0x00055C1A
		private void Fill(int require)
		{
			if (this.pos + require >= this.end)
			{
				this.Fill_(require);
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00057A34 File Offset: 0x00055C34
		private byte ReadByte()
		{
			this.Fill(0);
			byte[] array = this.data;
			int num = this.pos;
			this.pos = num + 1;
			return array[num];
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00057A60 File Offset: 0x00055C60
		private ushort ReadUShort()
		{
			this.Fill(1);
			int num = this.pos;
			byte[] array = this.data;
			ushort result = (ushort)((int)array[num] + ((int)array[num + 1] << 8));
			this.pos += 2;
			return result;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00057AA0 File Offset: 0x00055CA0
		private int ParseMB32()
		{
			byte b = this.ReadByte();
			if (b > 127)
			{
				return this.ParseMB32_(b);
			}
			return (int)b;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00057AC4 File Offset: 0x00055CC4
		private int ParseMB32_(byte b)
		{
			uint num = (uint)(b & 127);
			b = this.ReadByte();
			uint num2 = (uint)(b & 127);
			num += num2 << 7;
			if (b > 127)
			{
				b = this.ReadByte();
				num2 = (uint)(b & 127);
				num += num2 << 14;
				if (b > 127)
				{
					b = this.ReadByte();
					num2 = (uint)(b & 127);
					num += num2 << 21;
					if (b > 127)
					{
						b = this.ReadByte();
						num2 = (uint)(b & 7);
						if (b > 7)
						{
							throw this.ThrowXmlException("XmlBinary_ValueTooBig");
						}
						num += num2 << 28;
					}
				}
			}
			return (int)num;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00057B44 File Offset: 0x00055D44
		private int ParseMB32(int pos)
		{
			byte[] array = this.data;
			byte b = array[pos++];
			uint num = (uint)(b & 127);
			if (b > 127)
			{
				b = array[pos++];
				uint num2 = (uint)(b & 127);
				num += num2 << 7;
				if (b > 127)
				{
					b = array[pos++];
					num2 = (uint)(b & 127);
					num += num2 << 14;
					if (b > 127)
					{
						b = array[pos++];
						num2 = (uint)(b & 127);
						num += num2 << 21;
						if (b > 127)
						{
							b = array[pos++];
							num2 = (uint)(b & 7);
							if (b > 7)
							{
								throw this.ThrowXmlException("XmlBinary_ValueTooBig");
							}
							num += num2 << 28;
						}
					}
				}
			}
			return (int)num;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00057BE0 File Offset: 0x00055DE0
		private int ParseMB64()
		{
			byte b = this.ReadByte();
			if (b > 127)
			{
				return this.ParseMB32_(b);
			}
			return (int)b;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00057C02 File Offset: 0x00055E02
		private BinXmlToken PeekToken()
		{
			while (this.pos >= this.end && this.FillAllowEOF())
			{
			}
			if (this.pos >= this.end)
			{
				return BinXmlToken.EOF;
			}
			return (BinXmlToken)this.data[this.pos];
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00057C38 File Offset: 0x00055E38
		private BinXmlToken ReadToken()
		{
			while (this.pos >= this.end && this.FillAllowEOF())
			{
			}
			if (this.pos >= this.end)
			{
				return BinXmlToken.EOF;
			}
			byte[] array = this.data;
			int num = this.pos;
			this.pos = num + 1;
			return array[num];
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00057C84 File Offset: 0x00055E84
		private BinXmlToken NextToken2(BinXmlToken token)
		{
			for (;;)
			{
				if (token <= BinXmlToken.Extn)
				{
					if (token != BinXmlToken.NmFlush)
					{
						if (token != BinXmlToken.Extn)
						{
							break;
						}
						this.SkipExtn();
					}
					else
					{
						this.NameFlush();
					}
				}
				else if (token != BinXmlToken.QName)
				{
					if (token != BinXmlToken.Name)
					{
						break;
					}
					this.AddName();
				}
				else
				{
					this.AddQName();
				}
				token = this.ReadToken();
			}
			return token;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00057CE8 File Offset: 0x00055EE8
		private BinXmlToken NextToken1()
		{
			int num = this.pos;
			BinXmlToken binXmlToken;
			if (num >= this.end)
			{
				binXmlToken = this.ReadToken();
			}
			else
			{
				binXmlToken = (BinXmlToken)this.data[num];
				this.pos = num + 1;
			}
			if (binXmlToken >= BinXmlToken.NmFlush && binXmlToken <= BinXmlToken.Name)
			{
				return this.NextToken2(binXmlToken);
			}
			return binXmlToken;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00057D3C File Offset: 0x00055F3C
		private BinXmlToken NextToken()
		{
			int num = this.pos;
			if (num < this.end)
			{
				BinXmlToken binXmlToken = (BinXmlToken)this.data[num];
				if (binXmlToken < BinXmlToken.NmFlush || binXmlToken > BinXmlToken.Name)
				{
					this.pos = num + 1;
					return binXmlToken;
				}
			}
			return this.NextToken1();
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00057D84 File Offset: 0x00055F84
		private BinXmlToken PeekNextToken()
		{
			BinXmlToken binXmlToken = this.NextToken();
			if (BinXmlToken.EOF != binXmlToken)
			{
				this.pos--;
			}
			return binXmlToken;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00057DAC File Offset: 0x00055FAC
		private BinXmlToken RescanNextToken()
		{
			checked
			{
				BinXmlToken binXmlToken;
				for (;;)
				{
					binXmlToken = this.ReadToken();
					if (binXmlToken <= BinXmlToken.Extn)
					{
						if (binXmlToken != BinXmlToken.NmFlush)
						{
							if (binXmlToken != BinXmlToken.Extn)
							{
								break;
							}
							int num = this.ParseMB32();
							this.pos += num;
						}
					}
					else if (binXmlToken != BinXmlToken.QName)
					{
						if (binXmlToken != BinXmlToken.Name)
						{
							break;
						}
						int num2 = this.ParseMB32();
						this.pos += 2 * num2;
					}
					else
					{
						this.ParseMB32();
						this.ParseMB32();
						this.ParseMB32();
					}
				}
				return binXmlToken;
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00057E34 File Offset: 0x00056034
		private string ParseText()
		{
			int num = this.mark;
			string @string;
			try
			{
				if (num < 0)
				{
					this.mark = this.pos;
				}
				int num2;
				int cch = this.ScanText(out num2);
				@string = this.GetString(num2, cch);
			}
			finally
			{
				if (num < 0)
				{
					this.mark = -1;
				}
			}
			return @string;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00057E8C File Offset: 0x0005608C
		private int ScanText(out int start)
		{
			int num = this.ParseMB32();
			int num2 = this.mark;
			int num3 = this.pos;
			checked
			{
				this.pos += num * 2;
				if (this.pos > this.end)
				{
					this.Fill(-1);
				}
			}
			start = num3 - (num2 - this.mark);
			return num;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00057EE0 File Offset: 0x000560E0
		private string GetString(int pos, int cch)
		{
			checked
			{
				if (pos + cch * 2 > this.end)
				{
					throw new XmlException("Xml_UnexpectedEOF1", null);
				}
				if (cch == 0)
				{
					return string.Empty;
				}
				if ((pos & 1) == 0)
				{
					return this.GetStringAligned(this.data, pos, cch);
				}
				return this.unicode.GetString(this.data, pos, cch * 2);
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00057F38 File Offset: 0x00056138
		private unsafe string GetStringAligned(byte[] data, int offset, int cch)
		{
			byte* ptr;
			if (data == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &data[0];
			}
			char* value = (char*)(ptr + offset);
			return new string(value, 0, cch);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00057F6C File Offset: 0x0005616C
		private string GetAttributeText(int i)
		{
			string val = this.attributes[i].val;
			if (val != null)
			{
				return val;
			}
			int num = this.pos;
			string result;
			try
			{
				this.pos = this.attributes[i].contentPos;
				BinXmlToken binXmlToken = this.RescanNextToken();
				if (BinXmlToken.Attr == binXmlToken || BinXmlToken.EndAttrs == binXmlToken)
				{
					result = "";
				}
				else
				{
					this.token = binXmlToken;
					this.ReScanOverValue(binXmlToken);
					result = this.ValueAsString(binXmlToken);
				}
			}
			finally
			{
				this.pos = num;
			}
			return result;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00058000 File Offset: 0x00056200
		private int LocateAttribute(string name, string ns)
		{
			for (int i = 0; i < this.attrCount; i++)
			{
				if (this.attributes[i].name.MatchNs(name, ns))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0005803C File Offset: 0x0005623C
		private int LocateAttribute(string name)
		{
			string prefix;
			string lname;
			ValidateNames.SplitQName(name, out prefix, out lname);
			for (int i = 0; i < this.attrCount; i++)
			{
				if (this.attributes[i].name.MatchPrefix(prefix, lname))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00058084 File Offset: 0x00056284
		private void PositionOnAttribute(int i)
		{
			this.attrIndex = i;
			this.qnameOther = this.attributes[i - 1].name;
			if (this.state == XmlSqlBinaryReader.ScanState.Doc)
			{
				this.parentNodeType = this.nodetype;
			}
			this.token = BinXmlToken.Attr;
			this.nodetype = XmlNodeType.Attribute;
			this.state = XmlSqlBinaryReader.ScanState.Attr;
			this.valueType = XmlSqlBinaryReader.TypeOfObject;
			this.stringValue = null;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000580F0 File Offset: 0x000562F0
		private void GrowElements()
		{
			int num = this.elementStack.Length * 2;
			XmlSqlBinaryReader.ElemInfo[] destinationArray = new XmlSqlBinaryReader.ElemInfo[num];
			Array.Copy(this.elementStack, 0, destinationArray, 0, this.elementStack.Length);
			this.elementStack = destinationArray;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0005812C File Offset: 0x0005632C
		private void GrowAttributes()
		{
			int num = this.attributes.Length * 2;
			XmlSqlBinaryReader.AttrInfo[] destinationArray = new XmlSqlBinaryReader.AttrInfo[num];
			Array.Copy(this.attributes, 0, destinationArray, 0, this.attrCount);
			this.attributes = destinationArray;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00058166 File Offset: 0x00056366
		private void ClearAttributes()
		{
			if (this.attrCount != 0)
			{
				this.attrCount = 0;
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00058178 File Offset: 0x00056378
		private void PushNamespace(string prefix, string ns, bool implied)
		{
			if (prefix == "xml")
			{
				return;
			}
			int num = this.elemDepth;
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl;
			this.namespaces.TryGetValue(prefix, out namespaceDecl);
			if (namespaceDecl != null)
			{
				if (namespaceDecl.uri == ns)
				{
					if (!implied && namespaceDecl.implied && namespaceDecl.scope == num)
					{
						namespaceDecl.implied = false;
					}
					return;
				}
				this.qnameElement.CheckPrefixNS(prefix, ns);
				if (prefix.Length != 0)
				{
					for (int i = 0; i < this.attrCount; i++)
					{
						if (this.attributes[i].name.prefix.Length != 0)
						{
							this.attributes[i].name.CheckPrefixNS(prefix, ns);
						}
					}
				}
			}
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl2 = new XmlSqlBinaryReader.NamespaceDecl(prefix, ns, this.elementStack[num].nsdecls, namespaceDecl, num, implied);
			this.elementStack[num].nsdecls = namespaceDecl2;
			this.namespaces[prefix] = namespaceDecl2;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00058270 File Offset: 0x00056470
		private void PopNamespaces(XmlSqlBinaryReader.NamespaceDecl firstInScopeChain)
		{
			XmlSqlBinaryReader.NamespaceDecl scopeLink;
			for (XmlSqlBinaryReader.NamespaceDecl namespaceDecl = firstInScopeChain; namespaceDecl != null; namespaceDecl = scopeLink)
			{
				if (namespaceDecl.prevLink == null)
				{
					this.namespaces.Remove(namespaceDecl.prefix);
				}
				else
				{
					this.namespaces[namespaceDecl.prefix] = namespaceDecl.prevLink;
				}
				scopeLink = namespaceDecl.scopeLink;
				namespaceDecl.prevLink = null;
				namespaceDecl.scopeLink = null;
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000582D0 File Offset: 0x000564D0
		private void GenerateImpliedXmlnsAttrs()
		{
			for (XmlSqlBinaryReader.NamespaceDecl namespaceDecl = this.elementStack[this.elemDepth].nsdecls; namespaceDecl != null; namespaceDecl = namespaceDecl.scopeLink)
			{
				if (namespaceDecl.implied)
				{
					if (this.attrCount == this.attributes.Length)
					{
						this.GrowAttributes();
					}
					XmlSqlBinaryReader.QName n;
					if (namespaceDecl.prefix.Length == 0)
					{
						n = new XmlSqlBinaryReader.QName(string.Empty, this.xmlns, this.nsxmlns);
					}
					else
					{
						n = new XmlSqlBinaryReader.QName(this.xmlns, this.xnt.Add(namespaceDecl.prefix), this.nsxmlns);
					}
					this.attributes[this.attrCount].Set(n, namespaceDecl.uri);
					this.attrCount++;
				}
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000583A0 File Offset: 0x000565A0
		private bool ReadInit(bool skipXmlDecl)
		{
			string res;
			if (!this.sniffed)
			{
				ushort num = this.ReadUShort();
				if (num != 65503)
				{
					res = "XmlBinary_InvalidSignature";
					goto IL_1E6;
				}
			}
			this.version = this.ReadByte();
			if (this.version != 1 && this.version != 2)
			{
				res = "XmlBinary_InvalidProtocolVersion";
			}
			else
			{
				if (1200 == this.ReadUShort())
				{
					this.state = XmlSqlBinaryReader.ScanState.Doc;
					if (BinXmlToken.XmlDecl == this.PeekToken())
					{
						this.pos++;
						this.attributes[0].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("version"), string.Empty), this.ParseText());
						this.attrCount = 1;
						if (BinXmlToken.Encoding == this.PeekToken())
						{
							this.pos++;
							this.attributes[1].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("encoding"), string.Empty), this.ParseText());
							this.attrCount++;
						}
						byte b = this.ReadByte();
						if (b != 0)
						{
							if (b - 1 > 1)
							{
								res = "XmlBinary_InvalidStandalone";
								goto IL_1E6;
							}
							this.attributes[this.attrCount].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("standalone"), string.Empty), (b == 1) ? "yes" : "no");
							this.attrCount++;
						}
						if (!skipXmlDecl)
						{
							XmlSqlBinaryReader.QName qname = new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("xml"), string.Empty);
							this.qnameOther = (this.qnameElement = qname);
							this.nodetype = XmlNodeType.XmlDeclaration;
							this.posAfterAttrs = this.pos;
							return true;
						}
					}
					return this.ReadDoc();
				}
				res = "XmlBinary_UnsupportedCodePage";
			}
			IL_1E6:
			this.state = XmlSqlBinaryReader.ScanState.Error;
			throw new XmlException(res, null);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000585A4 File Offset: 0x000567A4
		private void ScanAttributes()
		{
			int num = -1;
			int num2 = -1;
			this.mark = this.pos;
			string text = null;
			bool flag = false;
			BinXmlToken binXmlToken;
			while (BinXmlToken.EndAttrs != (binXmlToken = this.NextToken()))
			{
				if (BinXmlToken.Attr == binXmlToken)
				{
					if (text != null)
					{
						this.PushNamespace(text, string.Empty, false);
						text = null;
					}
					if (this.attrCount == this.attributes.Length)
					{
						this.GrowAttributes();
					}
					XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[this.ReadQNameRef()];
					this.attributes[this.attrCount].Set(qname, this.pos);
					if (qname.prefix == "xml")
					{
						if (qname.localname == "lang")
						{
							num2 = this.attrCount;
						}
						else if (qname.localname == "space")
						{
							num = this.attrCount;
						}
					}
					else if (Ref.Equal(qname.namespaceUri, this.nsxmlns))
					{
						text = qname.localname;
						if (text == "xmlns")
						{
							text = string.Empty;
						}
					}
					else if (qname.prefix.Length != 0)
					{
						if (qname.namespaceUri.Length == 0)
						{
							throw new XmlException("Xml_PrefixForEmptyNs", string.Empty);
						}
						this.PushNamespace(qname.prefix, qname.namespaceUri, true);
					}
					else if (qname.namespaceUri.Length != 0)
					{
						throw this.ThrowXmlException("XmlBinary_AttrWithNsNoPrefix", qname.localname, qname.namespaceUri);
					}
					this.attrCount++;
					flag = false;
				}
				else
				{
					this.ScanOverValue(binXmlToken, true, true);
					if (flag)
					{
						throw this.ThrowNotSupported("XmlBinary_ListsOfValuesNotSupported");
					}
					string text2 = this.stringValue;
					if (text2 != null)
					{
						this.attributes[this.attrCount - 1].val = text2;
						this.stringValue = null;
					}
					if (text != null)
					{
						string ns = this.xnt.Add(this.ValueAsString(binXmlToken));
						this.PushNamespace(text, ns, false);
						text = null;
					}
					flag = true;
				}
			}
			if (num != -1)
			{
				string attributeText = this.GetAttributeText(num);
				XmlSpace xmlSpace = XmlSpace.None;
				if (attributeText == "preserve")
				{
					xmlSpace = XmlSpace.Preserve;
				}
				else if (attributeText == "default")
				{
					xmlSpace = XmlSpace.Default;
				}
				this.elementStack[this.elemDepth].xmlSpace = xmlSpace;
				this.xmlspacePreserve = (XmlSpace.Preserve == xmlSpace);
			}
			if (num2 != -1)
			{
				this.elementStack[this.elemDepth].xmlLang = this.GetAttributeText(num2);
			}
			if (this.attrCount < 200)
			{
				this.SimpleCheckForDuplicateAttributes();
				return;
			}
			this.HashCheckForDuplicateAttributes();
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00058848 File Offset: 0x00056A48
		private void SimpleCheckForDuplicateAttributes()
		{
			for (int i = 0; i < this.attrCount; i++)
			{
				string localname;
				string namespaceUri;
				this.attributes[i].GetLocalnameAndNamespaceUri(out localname, out namespaceUri);
				for (int j = i + 1; j < this.attrCount; j++)
				{
					if (this.attributes[j].MatchNS(localname, namespaceUri))
					{
						throw new XmlException("Xml_DupAttributeName", this.attributes[i].name.ToString());
					}
				}
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000588CC File Offset: 0x00056ACC
		private void HashCheckForDuplicateAttributes()
		{
			int i;
			checked
			{
				for (i = 256; i < this.attrCount; i *= 2)
				{
				}
				if (this.attrHashTbl.Length < i)
				{
					this.attrHashTbl = new int[i];
				}
			}
			for (int j = 0; j < this.attrCount; j++)
			{
				string localname;
				string namespaceUri;
				int localnameAndNamespaceUriAndHash = this.attributes[j].GetLocalnameAndNamespaceUriAndHash(this.hasher, out localname, out namespaceUri);
				int num = localnameAndNamespaceUriAndHash & i - 1;
				int num2 = this.attrHashTbl[num];
				this.attrHashTbl[num] = j + 1;
				this.attributes[j].prevHash = num2;
				while (num2 != 0)
				{
					num2--;
					if (this.attributes[num2].MatchHashNS(localnameAndNamespaceUriAndHash, localname, namespaceUri))
					{
						throw new XmlException("Xml_DupAttributeName", this.attributes[j].name.ToString());
					}
					num2 = this.attributes[num2].prevHash;
				}
			}
			Array.Clear(this.attrHashTbl, 0, i);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000589D8 File Offset: 0x00056BD8
		private string XmlDeclValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.attrCount; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(this.attributes[i].name.localname);
				stringBuilder.Append("=\"");
				stringBuilder.Append(this.attributes[i].val);
				stringBuilder.Append('"');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00058A5C File Offset: 0x00056C5C
		private string CDATAValue()
		{
			string text = this.GetString(this.tokDataPos, this.tokLen);
			StringBuilder stringBuilder = null;
			while (this.PeekToken() == BinXmlToken.CData)
			{
				this.pos++;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(text.Length + text.Length / 2);
					stringBuilder.Append(text);
				}
				stringBuilder.Append(this.ParseText());
			}
			if (stringBuilder != null)
			{
				text = stringBuilder.ToString();
			}
			this.stringValue = text;
			return text;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00058ADC File Offset: 0x00056CDC
		private void FinishCDATA()
		{
			for (;;)
			{
				BinXmlToken binXmlToken = this.PeekToken();
				if (binXmlToken == BinXmlToken.EndCData)
				{
					break;
				}
				if (binXmlToken != BinXmlToken.CData)
				{
					goto IL_3F;
				}
				this.pos++;
				int num;
				this.ScanText(out num);
			}
			this.pos++;
			return;
			IL_3F:
			throw new XmlException("XmlBin_MissingEndCDATA");
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00058B34 File Offset: 0x00056D34
		private void FinishEndElement()
		{
			XmlSqlBinaryReader.NamespaceDecl firstInScopeChain = this.elementStack[this.elemDepth].Clear();
			this.PopNamespaces(firstInScopeChain);
			this.elemDepth--;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00058B70 File Offset: 0x00056D70
		private bool ReadDoc()
		{
			XmlNodeType xmlNodeType = this.nodetype;
			if (xmlNodeType != XmlNodeType.Element)
			{
				if (xmlNodeType != XmlNodeType.CDATA)
				{
					if (xmlNodeType == XmlNodeType.EndElement)
					{
						this.FinishEndElement();
					}
				}
				else
				{
					this.FinishCDATA();
				}
			}
			else if (this.isEmpty)
			{
				this.FinishEndElement();
				this.isEmpty = false;
			}
			for (;;)
			{
				this.nodetype = XmlNodeType.None;
				this.mark = -1;
				if (this.qnameOther.localname.Length != 0)
				{
					this.qnameOther.Clear();
				}
				this.ClearAttributes();
				this.attrCount = 0;
				this.valueType = XmlSqlBinaryReader.TypeOfString;
				this.stringValue = null;
				this.hasTypedValue = false;
				this.token = this.NextToken();
				BinXmlToken binXmlToken = this.token;
				if (binXmlToken <= BinXmlToken.XSD_KATMAI_DATE)
				{
					switch (binXmlToken)
					{
					case BinXmlToken.EOF:
						goto IL_191;
					case BinXmlToken.Error:
					case (BinXmlToken)21:
					case (BinXmlToken)25:
					case (BinXmlToken)26:
						goto IL_27C;
					case BinXmlToken.SQL_SMALLINT:
					case BinXmlToken.SQL_INT:
					case BinXmlToken.SQL_REAL:
					case BinXmlToken.SQL_FLOAT:
					case BinXmlToken.SQL_MONEY:
					case BinXmlToken.SQL_BIT:
					case BinXmlToken.SQL_TINYINT:
					case BinXmlToken.SQL_BIGINT:
					case BinXmlToken.SQL_UUID:
					case BinXmlToken.SQL_DECIMAL:
					case BinXmlToken.SQL_NUMERIC:
					case BinXmlToken.SQL_BINARY:
					case BinXmlToken.SQL_CHAR:
					case BinXmlToken.SQL_NCHAR:
					case BinXmlToken.SQL_VARBINARY:
					case BinXmlToken.SQL_VARCHAR:
					case BinXmlToken.SQL_NVARCHAR:
					case BinXmlToken.SQL_DATETIME:
					case BinXmlToken.SQL_SMALLDATETIME:
					case BinXmlToken.SQL_SMALLMONEY:
					case BinXmlToken.SQL_TEXT:
					case BinXmlToken.SQL_IMAGE:
					case BinXmlToken.SQL_NTEXT:
					case BinXmlToken.SQL_UDT:
						break;
					default:
						if (binXmlToken - BinXmlToken.XSD_KATMAI_TIMEOFFSET > 5)
						{
							goto Block_8;
						}
						break;
					}
				}
				else if (binXmlToken - BinXmlToken.XSD_TIME > 11)
				{
					switch (binXmlToken)
					{
					case BinXmlToken.EndNest:
						goto IL_22D;
					case BinXmlToken.Nest:
						goto IL_218;
					case BinXmlToken.XmlText:
						goto IL_242;
					case (BinXmlToken)238:
					case BinXmlToken.QName:
					case BinXmlToken.Name:
					case BinXmlToken.EndCData:
					case BinXmlToken.EndAttrs:
					case BinXmlToken.Attr:
						goto IL_27C;
					case BinXmlToken.CData:
						goto IL_210;
					case BinXmlToken.Comment:
						this.ImplReadComment();
						if (this.ignoreComments)
						{
							continue;
						}
						return true;
					case BinXmlToken.PI:
						this.ImplReadPI();
						if (this.ignorePIs)
						{
							continue;
						}
						return true;
					case BinXmlToken.EndElem:
						goto IL_1BA;
					case BinXmlToken.Element:
						goto IL_1AF;
					default:
						if (binXmlToken != BinXmlToken.DocType)
						{
							goto Block_11;
						}
						this.ImplReadDoctype();
						if (this.dtdProcessing == DtdProcessing.Ignore)
						{
							continue;
						}
						if (this.prevNameInfo != null)
						{
							continue;
						}
						return true;
					}
				}
				this.ImplReadData(this.token);
				if (XmlNodeType.Text == this.nodetype)
				{
					goto Block_18;
				}
				if (!this.ignoreWhitespace || this.xmlspacePreserve)
				{
					return true;
				}
			}
			Block_8:
			Block_11:
			goto IL_27C;
			IL_191:
			if (this.elemDepth > 0)
			{
				throw new XmlException("Xml_UnexpectedEOF1", null);
			}
			this.state = XmlSqlBinaryReader.ScanState.EOF;
			return false;
			IL_1AF:
			this.ImplReadElement();
			return true;
			IL_1BA:
			this.ImplReadEndElement();
			return true;
			IL_210:
			this.ImplReadCDATA();
			return true;
			IL_218:
			this.ImplReadNest();
			this.sniffed = false;
			return this.ReadInit(true);
			IL_22D:
			if (this.prevNameInfo != null)
			{
				this.ImplReadEndNest();
				return this.ReadDoc();
			}
			goto IL_27C;
			IL_242:
			this.ImplReadXmlText();
			return true;
			Block_18:
			this.CheckAllowContent();
			return true;
			IL_27C:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00058E08 File Offset: 0x00057008
		private void ImplReadData(BinXmlToken tokenType)
		{
			this.mark = this.pos;
			if (tokenType <= BinXmlToken.SQL_NVARCHAR)
			{
				if (tokenType - BinXmlToken.SQL_CHAR > 1 && tokenType - BinXmlToken.SQL_VARCHAR > 1)
				{
					goto IL_3F;
				}
			}
			else if (tokenType != BinXmlToken.SQL_TEXT && tokenType != BinXmlToken.SQL_NTEXT)
			{
				goto IL_3F;
			}
			this.valueType = XmlSqlBinaryReader.TypeOfString;
			this.hasTypedValue = false;
			goto IL_58;
			IL_3F:
			this.valueType = this.GetValueType(this.token);
			this.hasTypedValue = true;
			IL_58:
			this.nodetype = this.ScanOverValue(this.token, false, true);
			BinXmlToken binXmlToken = this.PeekNextToken();
			switch (binXmlToken)
			{
			case BinXmlToken.SQL_SMALLINT:
			case BinXmlToken.SQL_INT:
			case BinXmlToken.SQL_REAL:
			case BinXmlToken.SQL_FLOAT:
			case BinXmlToken.SQL_MONEY:
			case BinXmlToken.SQL_BIT:
			case BinXmlToken.SQL_TINYINT:
			case BinXmlToken.SQL_BIGINT:
			case BinXmlToken.SQL_UUID:
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
			case BinXmlToken.SQL_SMALLMONEY:
			case BinXmlToken.SQL_TEXT:
			case BinXmlToken.SQL_IMAGE:
			case BinXmlToken.SQL_NTEXT:
			case BinXmlToken.SQL_UDT:
				break;
			case (BinXmlToken)21:
			case (BinXmlToken)25:
			case (BinXmlToken)26:
				return;
			default:
				if (binXmlToken - BinXmlToken.XSD_KATMAI_TIMEOFFSET > 5 && binXmlToken - BinXmlToken.XSD_TIME > 11)
				{
					return;
				}
				break;
			}
			throw this.ThrowNotSupported("XmlBinary_ListsOfValuesNotSupported");
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00058F1C File Offset: 0x0005711C
		private void ImplReadElement()
		{
			if (3 != this.docState || 9 != this.docState)
			{
				switch (this.docState)
				{
				case -1:
					throw this.ThrowUnexpectedToken(this.token);
				case 0:
					this.docState = 9;
					break;
				case 1:
				case 2:
					this.docState = 3;
					break;
				}
			}
			this.elemDepth++;
			if (this.elemDepth == this.elementStack.Length)
			{
				this.GrowElements();
			}
			XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[this.ReadQNameRef()];
			this.qnameOther = (this.qnameElement = qname);
			this.elementStack[this.elemDepth].Set(qname, this.xmlspacePreserve);
			this.PushNamespace(qname.prefix, qname.namespaceUri, true);
			BinXmlToken binXmlToken = this.PeekNextToken();
			if (BinXmlToken.Attr == binXmlToken)
			{
				this.ScanAttributes();
				binXmlToken = this.PeekNextToken();
			}
			this.GenerateImpliedXmlnsAttrs();
			if (BinXmlToken.EndElem == binXmlToken)
			{
				this.NextToken();
				this.isEmpty = true;
			}
			else if (BinXmlToken.SQL_NVARCHAR == binXmlToken)
			{
				if (this.mark < 0)
				{
					this.mark = this.pos;
				}
				this.pos++;
				if (this.ReadByte() == 0)
				{
					if (247 != this.ReadByte())
					{
						this.pos -= 3;
					}
					else
					{
						this.pos--;
					}
				}
				else
				{
					this.pos -= 2;
				}
			}
			this.nodetype = XmlNodeType.Element;
			this.valueType = XmlSqlBinaryReader.TypeOfObject;
			this.posAfterAttrs = this.pos;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x000590BC File Offset: 0x000572BC
		private void ImplReadEndElement()
		{
			if (this.elemDepth == 0)
			{
				throw this.ThrowXmlException("Xml_UnexpectedEndTag");
			}
			int num = this.elemDepth;
			if (1 == num && 3 == this.docState)
			{
				this.docState = -1;
			}
			this.qnameOther = this.elementStack[num].name;
			this.xmlspacePreserve = this.elementStack[num].xmlspacePreserve;
			this.nodetype = XmlNodeType.EndElement;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00059130 File Offset: 0x00057330
		private void ImplReadDoctype()
		{
			if (this.dtdProcessing == DtdProcessing.Prohibit)
			{
				throw this.ThrowXmlException("Xml_DtdIsProhibited");
			}
			int num = this.docState;
			if (num <= 1)
			{
				this.docState = 2;
				this.qnameOther.localname = this.ParseText();
				if (BinXmlToken.System == this.PeekToken())
				{
					this.pos++;
					XmlSqlBinaryReader.AttrInfo[] array = this.attributes;
					int num2 = this.attrCount;
					this.attrCount = num2 + 1;
					array[num2].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("SYSTEM"), string.Empty), this.ParseText());
				}
				if (BinXmlToken.Public == this.PeekToken())
				{
					this.pos++;
					XmlSqlBinaryReader.AttrInfo[] array2 = this.attributes;
					int num2 = this.attrCount;
					this.attrCount = num2 + 1;
					array2[num2].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("PUBLIC"), string.Empty), this.ParseText());
				}
				if (BinXmlToken.Subset == this.PeekToken())
				{
					this.pos++;
					this.mark = this.pos;
					this.tokLen = this.ScanText(out this.tokDataPos);
				}
				else
				{
					this.tokLen = (this.tokDataPos = 0);
				}
				this.nodetype = XmlNodeType.DocumentType;
				this.posAfterAttrs = this.pos;
				return;
			}
			if (num == 9)
			{
				throw this.ThrowXmlException("Xml_DtdNotAllowedInFragment");
			}
			throw this.ThrowXmlException("Xml_BadDTDLocation");
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000592B4 File Offset: 0x000574B4
		private void ImplReadPI()
		{
			this.qnameOther.localname = this.symbolTables.symtable[this.ReadNameRef()];
			this.mark = this.pos;
			this.tokLen = this.ScanText(out this.tokDataPos);
			this.nodetype = XmlNodeType.ProcessingInstruction;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00059303 File Offset: 0x00057503
		private void ImplReadComment()
		{
			this.nodetype = XmlNodeType.Comment;
			this.mark = this.pos;
			this.tokLen = this.ScanText(out this.tokDataPos);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0005932A File Offset: 0x0005752A
		private void ImplReadCDATA()
		{
			this.CheckAllowContent();
			this.nodetype = XmlNodeType.CDATA;
			this.mark = this.pos;
			this.tokLen = this.ScanText(out this.tokDataPos);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00059357 File Offset: 0x00057557
		private void ImplReadNest()
		{
			this.CheckAllowContent();
			this.prevNameInfo = new XmlSqlBinaryReader.NestedBinXml(this.symbolTables, this.docState, this.prevNameInfo);
			this.symbolTables.Init();
			this.docState = 0;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00059390 File Offset: 0x00057590
		private void ImplReadEndNest()
		{
			XmlSqlBinaryReader.NestedBinXml nestedBinXml = this.prevNameInfo;
			this.symbolTables = nestedBinXml.symbolTables;
			this.docState = nestedBinXml.docState;
			this.prevNameInfo = nestedBinXml.next;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x000593C8 File Offset: 0x000575C8
		private void ImplReadXmlText()
		{
			this.CheckAllowContent();
			string xmlFragment = this.ParseText();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.xnt);
			foreach (XmlSqlBinaryReader.NamespaceDecl namespaceDecl in this.namespaces.Values)
			{
				if (namespaceDecl.scope > 0)
				{
					xmlNamespaceManager.AddNamespace(namespaceDecl.prefix, namespaceDecl.uri);
				}
			}
			XmlReaderSettings settings = this.Settings;
			settings.ReadOnly = false;
			settings.NameTable = this.xnt;
			settings.DtdProcessing = DtdProcessing.Prohibit;
			if (this.elemDepth != 0)
			{
				settings.ConformanceLevel = ConformanceLevel.Fragment;
			}
			settings.ReadOnly = true;
			XmlParserContext context = new XmlParserContext(this.xnt, xmlNamespaceManager, this.XmlLang, this.XmlSpace);
			this.textXmlReader = new XmlTextReaderImpl(xmlFragment, context, settings);
			if (!this.textXmlReader.Read() || (this.textXmlReader.NodeType == XmlNodeType.XmlDeclaration && !this.textXmlReader.Read()))
			{
				this.state = XmlSqlBinaryReader.ScanState.Doc;
				this.ReadDoc();
				return;
			}
			this.state = XmlSqlBinaryReader.ScanState.XmlText;
			this.UpdateFromTextReader();
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x000594F8 File Offset: 0x000576F8
		private void UpdateFromTextReader()
		{
			XmlReader xmlReader = this.textXmlReader;
			this.nodetype = xmlReader.NodeType;
			this.qnameOther.prefix = xmlReader.Prefix;
			this.qnameOther.localname = xmlReader.LocalName;
			this.qnameOther.namespaceUri = xmlReader.NamespaceURI;
			this.valueType = xmlReader.ValueType;
			this.isEmpty = xmlReader.IsEmptyElement;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00059563 File Offset: 0x00057763
		private bool UpdateFromTextReader(bool needUpdate)
		{
			if (needUpdate)
			{
				this.UpdateFromTextReader();
			}
			return needUpdate;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00059570 File Offset: 0x00057770
		private void CheckAllowContent()
		{
			int num = this.docState;
			if (num == 0)
			{
				this.docState = 9;
				return;
			}
			if (num != 3 && num != 9)
			{
				throw this.ThrowXmlException("Xml_InvalidRootData");
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000595A8 File Offset: 0x000577A8
		private void GenerateTokenTypeMap()
		{
			Type[] array = new Type[256];
			array[134] = typeof(bool);
			array[7] = typeof(byte);
			array[136] = typeof(sbyte);
			array[1] = typeof(short);
			array[137] = typeof(ushort);
			array[138] = typeof(uint);
			array[3] = typeof(float);
			array[4] = typeof(double);
			array[8] = typeof(long);
			array[139] = typeof(ulong);
			array[140] = typeof(XmlQualifiedName);
			Type typeFromHandle = typeof(int);
			array[6] = typeFromHandle;
			array[2] = typeFromHandle;
			Type typeFromHandle2 = typeof(decimal);
			array[20] = typeFromHandle2;
			array[5] = typeFromHandle2;
			array[10] = typeFromHandle2;
			array[11] = typeFromHandle2;
			array[135] = typeFromHandle2;
			Type typeFromHandle3 = typeof(DateTime);
			array[19] = typeFromHandle3;
			array[18] = typeFromHandle3;
			array[129] = typeFromHandle3;
			array[130] = typeFromHandle3;
			array[131] = typeFromHandle3;
			array[127] = typeFromHandle3;
			array[126] = typeFromHandle3;
			array[125] = typeFromHandle3;
			Type typeFromHandle4 = typeof(DateTimeOffset);
			array[124] = typeFromHandle4;
			array[123] = typeFromHandle4;
			array[122] = typeFromHandle4;
			Type typeFromHandle5 = typeof(byte[]);
			array[15] = typeFromHandle5;
			array[12] = typeFromHandle5;
			array[23] = typeFromHandle5;
			array[27] = typeFromHandle5;
			array[132] = typeFromHandle5;
			array[133] = typeFromHandle5;
			array[13] = XmlSqlBinaryReader.TypeOfString;
			array[16] = XmlSqlBinaryReader.TypeOfString;
			array[22] = XmlSqlBinaryReader.TypeOfString;
			array[14] = XmlSqlBinaryReader.TypeOfString;
			array[17] = XmlSqlBinaryReader.TypeOfString;
			array[24] = XmlSqlBinaryReader.TypeOfString;
			array[9] = XmlSqlBinaryReader.TypeOfString;
			if (XmlSqlBinaryReader.TokenTypeMap == null)
			{
				XmlSqlBinaryReader.TokenTypeMap = array;
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00059780 File Offset: 0x00057980
		private Type GetValueType(BinXmlToken token)
		{
			Type type = XmlSqlBinaryReader.TokenTypeMap[(int)token];
			if (type == null)
			{
				throw this.ThrowUnexpectedToken(token);
			}
			return type;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000597A9 File Offset: 0x000579A9
		private void ReScanOverValue(BinXmlToken token)
		{
			this.ScanOverValue(token, true, false);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000597B8 File Offset: 0x000579B8
		private XmlNodeType ScanOverValue(BinXmlToken token, bool attr, bool checkChars)
		{
			if (token != BinXmlToken.SQL_NVARCHAR)
			{
				return this.ScanOverAnyValue(token, attr, checkChars);
			}
			if (this.mark < 0)
			{
				this.mark = this.pos;
			}
			this.tokLen = this.ParseMB32();
			this.tokDataPos = this.pos;
			checked
			{
				this.pos += this.tokLen * 2;
				this.Fill(-1);
				if (checkChars && this.checkCharacters)
				{
					return this.CheckText(attr);
				}
				if (!attr)
				{
					return this.CheckTextIsWS();
				}
				return XmlNodeType.Text;
			}
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0005983C File Offset: 0x00057A3C
		private XmlNodeType ScanOverAnyValue(BinXmlToken token, bool attr, bool checkChars)
		{
			if (this.mark < 0)
			{
				this.mark = this.pos;
			}
			checked
			{
				switch (token)
				{
				case BinXmlToken.SQL_SMALLINT:
					goto IL_109;
				case BinXmlToken.SQL_INT:
				case BinXmlToken.SQL_REAL:
				case BinXmlToken.SQL_SMALLDATETIME:
				case BinXmlToken.SQL_SMALLMONEY:
					goto IL_12F;
				case BinXmlToken.SQL_FLOAT:
				case BinXmlToken.SQL_MONEY:
				case BinXmlToken.SQL_BIGINT:
				case BinXmlToken.SQL_DATETIME:
					goto IL_155;
				case BinXmlToken.SQL_BIT:
				case BinXmlToken.SQL_TINYINT:
					break;
				case BinXmlToken.SQL_UUID:
					this.tokDataPos = this.pos;
					this.tokLen = 16;
					this.pos += 16;
					goto IL_2BA;
				case BinXmlToken.SQL_DECIMAL:
				case BinXmlToken.SQL_NUMERIC:
					goto IL_1A3;
				case BinXmlToken.SQL_BINARY:
				case BinXmlToken.SQL_VARBINARY:
				case BinXmlToken.SQL_IMAGE:
				case BinXmlToken.SQL_UDT:
					goto IL_1D3;
				case BinXmlToken.SQL_CHAR:
				case BinXmlToken.SQL_VARCHAR:
				case BinXmlToken.SQL_TEXT:
					this.tokLen = this.ParseMB64();
					this.tokDataPos = this.pos;
					this.pos += this.tokLen;
					if (checkChars && this.checkCharacters)
					{
						this.Fill(-1);
						string text = this.ValueAsString(token);
						XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException, ExceptionType.XmlException);
						this.stringValue = text;
						goto IL_2BA;
					}
					goto IL_2BA;
				case BinXmlToken.SQL_NCHAR:
				case BinXmlToken.SQL_NVARCHAR:
				case BinXmlToken.SQL_NTEXT:
					return this.ScanOverValue(BinXmlToken.SQL_NVARCHAR, attr, checkChars);
				case (BinXmlToken)21:
				case (BinXmlToken)25:
				case (BinXmlToken)26:
					goto IL_2B2;
				default:
					switch (token)
					{
					case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
					case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
					case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					case BinXmlToken.XSD_KATMAI_TIME:
					case BinXmlToken.XSD_KATMAI_DATETIME:
					case BinXmlToken.XSD_KATMAI_DATE:
						this.VerifyVersion(2, token);
						this.tokDataPos = this.pos;
						this.tokLen = this.GetXsdKatmaiTokenLength(token);
						this.pos += this.tokLen;
						goto IL_2BA;
					case (BinXmlToken)128:
						goto IL_2B2;
					case BinXmlToken.XSD_TIME:
					case BinXmlToken.XSD_DATETIME:
					case BinXmlToken.XSD_DATE:
					case BinXmlToken.XSD_UNSIGNEDLONG:
						goto IL_155;
					case BinXmlToken.XSD_BINHEX:
					case BinXmlToken.XSD_BASE64:
						goto IL_1D3;
					case BinXmlToken.XSD_BOOLEAN:
					case BinXmlToken.XSD_BYTE:
						break;
					case BinXmlToken.XSD_DECIMAL:
						goto IL_1A3;
					case BinXmlToken.XSD_UNSIGNEDSHORT:
						goto IL_109;
					case BinXmlToken.XSD_UNSIGNEDINT:
						goto IL_12F;
					case BinXmlToken.XSD_QNAME:
						this.tokDataPos = this.pos;
						this.ParseMB32();
						goto IL_2BA;
					default:
						goto IL_2B2;
					}
					break;
				}
				this.tokDataPos = this.pos;
				this.tokLen = 1;
				this.pos++;
				goto IL_2BA;
				IL_109:
				this.tokDataPos = this.pos;
				this.tokLen = 2;
				this.pos += 2;
				goto IL_2BA;
				IL_12F:
				this.tokDataPos = this.pos;
				this.tokLen = 4;
				this.pos += 4;
				goto IL_2BA;
				IL_155:
				this.tokDataPos = this.pos;
				this.tokLen = 8;
				this.pos += 8;
				goto IL_2BA;
				IL_1A3:
				this.tokDataPos = this.pos;
				this.tokLen = this.ParseMB64();
				this.pos += this.tokLen;
				goto IL_2BA;
				IL_1D3:
				this.tokLen = this.ParseMB64();
				this.tokDataPos = this.pos;
				this.pos += this.tokLen;
				goto IL_2BA;
				IL_2B2:
				throw this.ThrowUnexpectedToken(token);
				IL_2BA:
				this.Fill(-1);
				return XmlNodeType.Text;
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00059B0C File Offset: 0x00057D0C
		private unsafe XmlNodeType CheckText(bool attr)
		{
			XmlCharType xmlCharType = this.xmlCharType;
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int num = this.pos;
			int num2 = this.tokDataPos;
			if (!attr)
			{
				for (;;)
				{
					int num3 = num2 + 2;
					if (num3 > num)
					{
						break;
					}
					if (ptr[num2 + 1] != 0 || (xmlCharType.charProperties[ptr[num2]] & 1) == 0)
					{
						goto IL_6F;
					}
					num2 = num3;
				}
				if (!this.xmlspacePreserve)
				{
					return XmlNodeType.Whitespace;
				}
				return XmlNodeType.SignificantWhitespace;
			}
			char c;
			char c2;
			for (;;)
			{
				IL_6F:
				int num4 = num2 + 2;
				if (num4 > num)
				{
					break;
				}
				c = (char)((int)ptr[num2] | (int)ptr[num2 + 1] << 8);
				if ((xmlCharType.charProperties[c] & 16) != 0)
				{
					num2 = num4;
				}
				else
				{
					if (!XmlCharType.IsHighSurrogate((int)c))
					{
						goto Block_8;
					}
					if (num2 + 4 > num)
					{
						goto Block_9;
					}
					c2 = (char)((int)ptr[num2 + 2] | (int)ptr[num2 + 3] << 8);
					if (!XmlCharType.IsLowSurrogate((int)c2))
					{
						goto Block_10;
					}
					num2 += 4;
				}
			}
			return XmlNodeType.Text;
			Block_8:
			throw XmlConvert.CreateInvalidCharException(c, '\0', ExceptionType.XmlException);
			Block_9:
			throw this.ThrowXmlException("Xml_InvalidSurrogateMissingLowChar");
			Block_10:
			throw XmlConvert.CreateInvalidSurrogatePairException(c, c2);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00059C14 File Offset: 0x00057E14
		private XmlNodeType CheckTextIsWS()
		{
			byte[] array = this.data;
			int i = this.tokDataPos;
			while (i < this.pos)
			{
				if (array[i + 1] == 0)
				{
					byte b = array[i];
					if (b - 9 <= 1 || b == 13 || b == 32)
					{
						i += 2;
						continue;
					}
				}
				return XmlNodeType.Text;
			}
			if (this.xmlspacePreserve)
			{
				return XmlNodeType.SignificantWhitespace;
			}
			return XmlNodeType.Whitespace;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00059C69 File Offset: 0x00057E69
		private void CheckValueTokenBounds()
		{
			if (this.end - this.tokDataPos < this.tokLen)
			{
				throw this.ThrowXmlException("Xml_UnexpectedEOF1");
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00059C8C File Offset: 0x00057E8C
		private int GetXsdKatmaiTokenLength(BinXmlToken token)
		{
			switch (token)
			{
			case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
			case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
			case BinXmlToken.XSD_KATMAI_DATEOFFSET:
			{
				this.Fill(0);
				byte scale = this.data[this.pos];
				return 6 + this.XsdKatmaiTimeScaleToValueLength(scale);
			}
			case BinXmlToken.XSD_KATMAI_TIME:
			case BinXmlToken.XSD_KATMAI_DATETIME:
			{
				this.Fill(0);
				byte scale = this.data[this.pos];
				return 4 + this.XsdKatmaiTimeScaleToValueLength(scale);
			}
			case BinXmlToken.XSD_KATMAI_DATE:
				return 3;
			default:
				throw this.ThrowUnexpectedToken(this.token);
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00059D08 File Offset: 0x00057F08
		private int XsdKatmaiTimeScaleToValueLength(byte scale)
		{
			if (scale > 7)
			{
				throw new XmlException("SqlTypes_ArithOverflow", null);
			}
			return (int)XmlSqlBinaryReader.XsdKatmaiTimeScaleToValueLengthMap[(int)scale];
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00059D24 File Offset: 0x00057F24
		private long ValueAsLong()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			switch (binXmlToken)
			{
			case BinXmlToken.SQL_SMALLINT:
				return (long)this.GetInt16(this.tokDataPos);
			case BinXmlToken.SQL_INT:
				return (long)this.GetInt32(this.tokDataPos);
			case BinXmlToken.SQL_REAL:
			case BinXmlToken.SQL_FLOAT:
			{
				double num = this.ValueAsDouble();
				return (long)num;
			}
			case BinXmlToken.SQL_MONEY:
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
			case BinXmlToken.SQL_SMALLMONEY:
				break;
			case BinXmlToken.SQL_BIT:
			case BinXmlToken.SQL_TINYINT:
			{
				byte b = this.data[this.tokDataPos];
				return (long)((ulong)b);
			}
			case BinXmlToken.SQL_BIGINT:
				return this.GetInt64(this.tokDataPos);
			case BinXmlToken.SQL_UUID:
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_11E;
			default:
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_DECIMAL:
					break;
				case BinXmlToken.XSD_BYTE:
				{
					sbyte b2 = (sbyte)this.data[this.tokDataPos];
					return (long)b2;
				}
				case BinXmlToken.XSD_UNSIGNEDSHORT:
					return (long)((ulong)this.GetUInt16(this.tokDataPos));
				case BinXmlToken.XSD_UNSIGNEDINT:
					return (long)((ulong)this.GetUInt32(this.tokDataPos));
				case BinXmlToken.XSD_UNSIGNEDLONG:
				{
					ulong @uint = this.GetUInt64(this.tokDataPos);
					return checked((long)@uint);
				}
				default:
					goto IL_11E;
				}
				break;
			}
			decimal value = this.ValueAsDecimal();
			return (long)value;
			IL_11E:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00059E5B File Offset: 0x0005805B
		private ulong ValueAsULong()
		{
			if (BinXmlToken.XSD_UNSIGNEDLONG == this.token)
			{
				this.CheckValueTokenBounds();
				return this.GetUInt64(this.tokDataPos);
			}
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00059E8C File Offset: 0x0005808C
		private decimal ValueAsDecimal()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			switch (binXmlToken)
			{
			case BinXmlToken.SQL_SMALLINT:
			case BinXmlToken.SQL_INT:
			case BinXmlToken.SQL_BIT:
			case BinXmlToken.SQL_TINYINT:
			case BinXmlToken.SQL_BIGINT:
				break;
			case BinXmlToken.SQL_REAL:
				return new decimal(this.GetSingle(this.tokDataPos));
			case BinXmlToken.SQL_FLOAT:
				return new decimal(this.GetDouble(this.tokDataPos));
			case BinXmlToken.SQL_MONEY:
			{
				BinXmlSqlMoney binXmlSqlMoney = new BinXmlSqlMoney(this.GetInt64(this.tokDataPos));
				return binXmlSqlMoney.ToDecimal();
			}
			case BinXmlToken.SQL_UUID:
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_124;
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
				goto IL_FC;
			case BinXmlToken.SQL_SMALLMONEY:
			{
				BinXmlSqlMoney binXmlSqlMoney2 = new BinXmlSqlMoney(this.GetInt32(this.tokDataPos));
				return binXmlSqlMoney2.ToDecimal();
			}
			default:
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_DECIMAL:
					goto IL_FC;
				case BinXmlToken.XSD_BYTE:
				case BinXmlToken.XSD_UNSIGNEDSHORT:
				case BinXmlToken.XSD_UNSIGNEDINT:
					break;
				case BinXmlToken.XSD_UNSIGNEDLONG:
					return new decimal(this.ValueAsULong());
				default:
					goto IL_124;
				}
				break;
			}
			return new decimal(this.ValueAsLong());
			IL_FC:
			BinXmlSqlDecimal binXmlSqlDecimal = new BinXmlSqlDecimal(this.data, this.tokDataPos, this.token == BinXmlToken.XSD_DECIMAL);
			return binXmlSqlDecimal.ToDecimal();
			IL_124:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00059FCC File Offset: 0x000581CC
		private double ValueAsDouble()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			switch (binXmlToken)
			{
			case BinXmlToken.SQL_SMALLINT:
			case BinXmlToken.SQL_INT:
			case BinXmlToken.SQL_BIT:
			case BinXmlToken.SQL_TINYINT:
			case BinXmlToken.SQL_BIGINT:
				break;
			case BinXmlToken.SQL_REAL:
				return (double)this.GetSingle(this.tokDataPos);
			case BinXmlToken.SQL_FLOAT:
				return this.GetDouble(this.tokDataPos);
			case BinXmlToken.SQL_MONEY:
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
			case BinXmlToken.SQL_SMALLMONEY:
				goto IL_B3;
			case BinXmlToken.SQL_UUID:
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_C0;
			default:
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_DECIMAL:
					goto IL_B3;
				case BinXmlToken.XSD_BYTE:
				case BinXmlToken.XSD_UNSIGNEDSHORT:
				case BinXmlToken.XSD_UNSIGNEDINT:
					break;
				case BinXmlToken.XSD_UNSIGNEDLONG:
					return this.ValueAsULong();
				default:
					goto IL_C0;
				}
				break;
			}
			return (double)this.ValueAsLong();
			IL_B3:
			return (double)this.ValueAsDecimal();
			IL_C0:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0005A0A8 File Offset: 0x000582A8
		private DateTime ValueAsDateTime()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			if (binXmlToken == BinXmlToken.SQL_DATETIME)
			{
				int num = this.tokDataPos;
				int @int = this.GetInt32(num);
				uint @uint = this.GetUInt32(num + 4);
				return BinXmlDateTime.SqlDateTimeToDateTime(@int, @uint);
			}
			if (binXmlToken != BinXmlToken.SQL_SMALLDATETIME)
			{
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
					return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
					return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					return BinXmlDateTime.XsdKatmaiDateOffsetToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_TIME:
					return BinXmlDateTime.XsdKatmaiTimeToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATETIME:
					return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATE:
					return BinXmlDateTime.XsdKatmaiDateToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_TIME:
				{
					long int2 = this.GetInt64(this.tokDataPos);
					return BinXmlDateTime.XsdTimeToDateTime(int2);
				}
				case BinXmlToken.XSD_DATETIME:
				{
					long int3 = this.GetInt64(this.tokDataPos);
					return BinXmlDateTime.XsdDateTimeToDateTime(int3);
				}
				case BinXmlToken.XSD_DATE:
				{
					long int4 = this.GetInt64(this.tokDataPos);
					return BinXmlDateTime.XsdDateToDateTime(int4);
				}
				}
				throw this.ThrowUnexpectedToken(this.token);
			}
			int num2 = this.tokDataPos;
			short int5 = this.GetInt16(num2);
			ushort uint2 = this.GetUInt16(num2 + 2);
			return BinXmlDateTime.SqlSmallDateTimeToDateTime(int5, uint2);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0005A208 File Offset: 0x00058408
		private DateTimeOffset ValueAsDateTimeOffset()
		{
			this.CheckValueTokenBounds();
			switch (this.token)
			{
			case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
				return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(this.data, this.tokDataPos);
			case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
				return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(this.data, this.tokDataPos);
			case BinXmlToken.XSD_KATMAI_DATEOFFSET:
				return BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(this.data, this.tokDataPos);
			default:
				throw this.ThrowUnexpectedToken(this.token);
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0005A27C File Offset: 0x0005847C
		private string ValueAsDateTimeString()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			if (binXmlToken == BinXmlToken.SQL_DATETIME)
			{
				int num = this.tokDataPos;
				int @int = this.GetInt32(num);
				uint @uint = this.GetUInt32(num + 4);
				return BinXmlDateTime.SqlDateTimeToString(@int, @uint);
			}
			if (binXmlToken != BinXmlToken.SQL_SMALLDATETIME)
			{
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
					return BinXmlDateTime.XsdKatmaiTimeOffsetToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
					return BinXmlDateTime.XsdKatmaiDateTimeOffsetToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					return BinXmlDateTime.XsdKatmaiDateOffsetToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_TIME:
					return BinXmlDateTime.XsdKatmaiTimeToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATETIME:
					return BinXmlDateTime.XsdKatmaiDateTimeToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATE:
					return BinXmlDateTime.XsdKatmaiDateToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_TIME:
				{
					long int2 = this.GetInt64(this.tokDataPos);
					return BinXmlDateTime.XsdTimeToString(int2);
				}
				case BinXmlToken.XSD_DATETIME:
				{
					long int3 = this.GetInt64(this.tokDataPos);
					return BinXmlDateTime.XsdDateTimeToString(int3);
				}
				case BinXmlToken.XSD_DATE:
				{
					long int4 = this.GetInt64(this.tokDataPos);
					return BinXmlDateTime.XsdDateToString(int4);
				}
				}
				throw this.ThrowUnexpectedToken(this.token);
			}
			int num2 = this.tokDataPos;
			short int5 = this.GetInt16(num2);
			ushort uint2 = this.GetUInt16(num2 + 2);
			return BinXmlDateTime.SqlSmallDateTimeToString(int5, uint2);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0005A3DC File Offset: 0x000585DC
		private string ValueAsString(BinXmlToken token)
		{
			try
			{
				this.CheckValueTokenBounds();
				switch (token)
				{
				case BinXmlToken.SQL_SMALLINT:
				case BinXmlToken.SQL_INT:
				case BinXmlToken.SQL_BIT:
				case BinXmlToken.SQL_TINYINT:
				case BinXmlToken.SQL_BIGINT:
					break;
				case BinXmlToken.SQL_REAL:
					return XmlConvert.ToString(this.GetSingle(this.tokDataPos));
				case BinXmlToken.SQL_FLOAT:
					return XmlConvert.ToString(this.GetDouble(this.tokDataPos));
				case BinXmlToken.SQL_MONEY:
				{
					BinXmlSqlMoney binXmlSqlMoney = new BinXmlSqlMoney(this.GetInt64(this.tokDataPos));
					return binXmlSqlMoney.ToString();
				}
				case BinXmlToken.SQL_UUID:
				{
					int num = this.tokDataPos;
					int @int = this.GetInt32(num);
					short int2 = this.GetInt16(num + 4);
					short int3 = this.GetInt16(num + 6);
					Guid guid = new Guid(@int, int2, int3, this.data[num + 8], this.data[num + 9], this.data[num + 10], this.data[num + 11], this.data[num + 12], this.data[num + 13], this.data[num + 14], this.data[num + 15]);
					return guid.ToString();
				}
				case BinXmlToken.SQL_DECIMAL:
				case BinXmlToken.SQL_NUMERIC:
					goto IL_264;
				case BinXmlToken.SQL_BINARY:
				case BinXmlToken.SQL_VARBINARY:
				case BinXmlToken.SQL_IMAGE:
				case BinXmlToken.SQL_UDT:
					goto IL_2CC;
				case BinXmlToken.SQL_CHAR:
				case BinXmlToken.SQL_VARCHAR:
				case BinXmlToken.SQL_TEXT:
				{
					int num2 = this.tokDataPos;
					int int4 = this.GetInt32(num2);
					Encoding encoding = Encoding.GetEncoding(int4);
					return encoding.GetString(this.data, num2 + 4, this.tokLen - 4);
				}
				case BinXmlToken.SQL_NCHAR:
				case BinXmlToken.SQL_NVARCHAR:
				case BinXmlToken.SQL_NTEXT:
					return this.GetString(this.tokDataPos, this.tokLen);
				case BinXmlToken.SQL_DATETIME:
				case BinXmlToken.SQL_SMALLDATETIME:
					goto IL_306;
				case BinXmlToken.SQL_SMALLMONEY:
				{
					BinXmlSqlMoney binXmlSqlMoney2 = new BinXmlSqlMoney(this.GetInt32(this.tokDataPos));
					return binXmlSqlMoney2.ToString();
				}
				case (BinXmlToken)21:
				case (BinXmlToken)25:
				case (BinXmlToken)26:
					goto IL_38B;
				default:
					switch (token)
					{
					case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
					case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
					case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					case BinXmlToken.XSD_KATMAI_TIME:
					case BinXmlToken.XSD_KATMAI_DATETIME:
					case BinXmlToken.XSD_KATMAI_DATE:
					case BinXmlToken.XSD_TIME:
					case BinXmlToken.XSD_DATETIME:
					case BinXmlToken.XSD_DATE:
						goto IL_306;
					case (BinXmlToken)128:
						goto IL_38B;
					case BinXmlToken.XSD_BINHEX:
						return BinHexEncoder.Encode(this.data, this.tokDataPos, this.tokLen);
					case BinXmlToken.XSD_BASE64:
						goto IL_2CC;
					case BinXmlToken.XSD_BOOLEAN:
						if (this.data[this.tokDataPos] == 0)
						{
							return "false";
						}
						return "true";
					case BinXmlToken.XSD_DECIMAL:
						goto IL_264;
					case BinXmlToken.XSD_BYTE:
					case BinXmlToken.XSD_UNSIGNEDSHORT:
					case BinXmlToken.XSD_UNSIGNEDINT:
						break;
					case BinXmlToken.XSD_UNSIGNEDLONG:
						return this.ValueAsULong().ToString(CultureInfo.InvariantCulture);
					case BinXmlToken.XSD_QNAME:
					{
						int num3 = this.ParseMB32(this.tokDataPos);
						if (num3 < 0 || num3 >= this.symbolTables.qnameCount)
						{
							throw new XmlException("XmlBin_InvalidQNameID", string.Empty);
						}
						XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[num3];
						if (qname.prefix.Length == 0)
						{
							return qname.localname;
						}
						return qname.prefix + ":" + qname.localname;
					}
					default:
						goto IL_38B;
					}
					break;
				}
				return this.ValueAsLong().ToString(CultureInfo.InvariantCulture);
				IL_264:
				BinXmlSqlDecimal binXmlSqlDecimal = new BinXmlSqlDecimal(this.data, this.tokDataPos, token == BinXmlToken.XSD_DECIMAL);
				return binXmlSqlDecimal.ToString();
				IL_2CC:
				return Convert.ToBase64String(this.data, this.tokDataPos, this.tokLen);
				IL_306:
				return this.ValueAsDateTimeString();
				IL_38B:
				throw this.ThrowUnexpectedToken(this.token);
			}
			catch
			{
				this.state = XmlSqlBinaryReader.ScanState.Error;
				throw;
			}
			string result;
			return result;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0005A7A8 File Offset: 0x000589A8
		private object ValueAsObject(BinXmlToken token, bool returnInternalTypes)
		{
			this.CheckValueTokenBounds();
			switch (token)
			{
			case BinXmlToken.SQL_SMALLINT:
				return this.GetInt16(this.tokDataPos);
			case BinXmlToken.SQL_INT:
				return this.GetInt32(this.tokDataPos);
			case BinXmlToken.SQL_REAL:
				return this.GetSingle(this.tokDataPos);
			case BinXmlToken.SQL_FLOAT:
				return this.GetDouble(this.tokDataPos);
			case BinXmlToken.SQL_MONEY:
			{
				BinXmlSqlMoney binXmlSqlMoney = new BinXmlSqlMoney(this.GetInt64(this.tokDataPos));
				if (returnInternalTypes)
				{
					return binXmlSqlMoney;
				}
				return binXmlSqlMoney.ToDecimal();
			}
			case BinXmlToken.SQL_BIT:
				return (int)this.data[this.tokDataPos];
			case BinXmlToken.SQL_TINYINT:
				return this.data[this.tokDataPos];
			case BinXmlToken.SQL_BIGINT:
				return this.GetInt64(this.tokDataPos);
			case BinXmlToken.SQL_UUID:
			{
				int num = this.tokDataPos;
				int @int = this.GetInt32(num);
				short int2 = this.GetInt16(num + 4);
				short int3 = this.GetInt16(num + 6);
				Guid guid = new Guid(@int, int2, int3, this.data[num + 8], this.data[num + 9], this.data[num + 10], this.data[num + 11], this.data[num + 12], this.data[num + 13], this.data[num + 14], this.data[num + 15]);
				return guid.ToString();
			}
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
				break;
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_IMAGE:
			case BinXmlToken.SQL_UDT:
				goto IL_325;
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_TEXT:
			{
				int num2 = this.tokDataPos;
				int int4 = this.GetInt32(num2);
				Encoding encoding = Encoding.GetEncoding(int4);
				return encoding.GetString(this.data, num2 + 4, this.tokLen - 4);
			}
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_NTEXT:
				return this.GetString(this.tokDataPos, this.tokLen);
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_34F;
			case BinXmlToken.SQL_SMALLMONEY:
			{
				BinXmlSqlMoney binXmlSqlMoney2 = new BinXmlSqlMoney(this.GetInt32(this.tokDataPos));
				if (returnInternalTypes)
				{
					return binXmlSqlMoney2;
				}
				return binXmlSqlMoney2.ToDecimal();
			}
			case (BinXmlToken)21:
			case (BinXmlToken)25:
			case (BinXmlToken)26:
				goto IL_3C1;
			default:
				switch (token)
				{
				case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
				case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
				case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					return this.ValueAsDateTimeOffset();
				case BinXmlToken.XSD_KATMAI_TIME:
				case BinXmlToken.XSD_KATMAI_DATETIME:
				case BinXmlToken.XSD_KATMAI_DATE:
				case BinXmlToken.XSD_TIME:
				case BinXmlToken.XSD_DATETIME:
				case BinXmlToken.XSD_DATE:
					goto IL_34F;
				case (BinXmlToken)128:
					goto IL_3C1;
				case BinXmlToken.XSD_BINHEX:
				case BinXmlToken.XSD_BASE64:
					goto IL_325;
				case BinXmlToken.XSD_BOOLEAN:
					return this.data[this.tokDataPos] > 0;
				case BinXmlToken.XSD_DECIMAL:
					break;
				case BinXmlToken.XSD_BYTE:
				{
					sbyte b = (sbyte)this.data[this.tokDataPos];
					return b;
				}
				case BinXmlToken.XSD_UNSIGNEDSHORT:
					return this.GetUInt16(this.tokDataPos);
				case BinXmlToken.XSD_UNSIGNEDINT:
					return this.GetUInt32(this.tokDataPos);
				case BinXmlToken.XSD_UNSIGNEDLONG:
					return this.GetUInt64(this.tokDataPos);
				case BinXmlToken.XSD_QNAME:
				{
					int num3 = this.ParseMB32(this.tokDataPos);
					if (num3 < 0 || num3 >= this.symbolTables.qnameCount)
					{
						throw new XmlException("XmlBin_InvalidQNameID", string.Empty);
					}
					XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[num3];
					return new XmlQualifiedName(qname.localname, qname.namespaceUri);
				}
				default:
					goto IL_3C1;
				}
				break;
			}
			BinXmlSqlDecimal binXmlSqlDecimal = new BinXmlSqlDecimal(this.data, this.tokDataPos, token == BinXmlToken.XSD_DECIMAL);
			if (returnInternalTypes)
			{
				return binXmlSqlDecimal;
			}
			return binXmlSqlDecimal.ToDecimal();
			IL_325:
			byte[] array = new byte[this.tokLen];
			Array.Copy(this.data, this.tokDataPos, array, 0, this.tokLen);
			return array;
			IL_34F:
			return this.ValueAsDateTime();
			IL_3C1:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0005AB84 File Offset: 0x00058D84
		private XmlValueConverter GetValueConverter(XmlTypeCode typeCode)
		{
			XmlSchemaSimpleType simpleTypeFromTypeCode = DatatypeImplementation.GetSimpleTypeFromTypeCode(typeCode);
			return simpleTypeFromTypeCode.ValueConverter;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0005ABA0 File Offset: 0x00058DA0
		private object ValueAs(BinXmlToken token, Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckValueTokenBounds();
			switch (token)
			{
			case BinXmlToken.SQL_SMALLINT:
			{
				int @int = (int)this.GetInt16(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Short).ChangeType(@int, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_INT:
			{
				int int2 = this.GetInt32(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Int).ChangeType(int2, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_REAL:
			{
				float single = this.GetSingle(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Float).ChangeType(single, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_FLOAT:
			{
				double @double = this.GetDouble(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Double).ChangeType(@double, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_MONEY:
				return this.GetValueConverter(XmlTypeCode.Decimal).ChangeType(new BinXmlSqlMoney(this.GetInt64(this.tokDataPos)).ToDecimal(), returnType, namespaceResolver);
			case BinXmlToken.SQL_BIT:
				return this.GetValueConverter(XmlTypeCode.NonNegativeInteger).ChangeType((int)this.data[this.tokDataPos], returnType, namespaceResolver);
			case BinXmlToken.SQL_TINYINT:
				return this.GetValueConverter(XmlTypeCode.UnsignedByte).ChangeType(this.data[this.tokDataPos], returnType, namespaceResolver);
			case BinXmlToken.SQL_BIGINT:
			{
				long int3 = this.GetInt64(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Long).ChangeType(int3, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_UUID:
				return this.GetValueConverter(XmlTypeCode.String).ChangeType(this.ValueAsString(token), returnType, namespaceResolver);
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
				break;
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_IMAGE:
			case BinXmlToken.SQL_UDT:
				goto IL_3F4;
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_TEXT:
			{
				int num = this.tokDataPos;
				int int4 = this.GetInt32(num);
				Encoding encoding = Encoding.GetEncoding(int4);
				return this.GetValueConverter(XmlTypeCode.UntypedAtomic).ChangeType(encoding.GetString(this.data, num + 4, this.tokLen - 4), returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_NTEXT:
				return this.GetValueConverter(XmlTypeCode.UntypedAtomic).ChangeType(this.GetString(this.tokDataPos, this.tokLen), returnType, namespaceResolver);
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_43E;
			case BinXmlToken.SQL_SMALLMONEY:
				return this.GetValueConverter(XmlTypeCode.Decimal).ChangeType(new BinXmlSqlMoney(this.GetInt32(this.tokDataPos)).ToDecimal(), returnType, namespaceResolver);
			case (BinXmlToken)21:
			case (BinXmlToken)25:
			case (BinXmlToken)26:
				goto IL_526;
			default:
				switch (token)
				{
				case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
				case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
				case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					return this.GetValueConverter(XmlTypeCode.DateTime).ChangeType(this.ValueAsDateTimeOffset(), returnType, namespaceResolver);
				case BinXmlToken.XSD_KATMAI_TIME:
				case BinXmlToken.XSD_KATMAI_DATETIME:
				case BinXmlToken.XSD_KATMAI_DATE:
				case BinXmlToken.XSD_DATETIME:
					goto IL_43E;
				case (BinXmlToken)128:
					goto IL_526;
				case BinXmlToken.XSD_TIME:
					return this.GetValueConverter(XmlTypeCode.Time).ChangeType(this.ValueAsDateTime(), returnType, namespaceResolver);
				case BinXmlToken.XSD_DATE:
					return this.GetValueConverter(XmlTypeCode.Date).ChangeType(this.ValueAsDateTime(), returnType, namespaceResolver);
				case BinXmlToken.XSD_BINHEX:
				case BinXmlToken.XSD_BASE64:
					goto IL_3F4;
				case BinXmlToken.XSD_BOOLEAN:
					return this.GetValueConverter(XmlTypeCode.Boolean).ChangeType(this.data[this.tokDataPos] > 0, returnType, namespaceResolver);
				case BinXmlToken.XSD_DECIMAL:
					break;
				case BinXmlToken.XSD_BYTE:
					return this.GetValueConverter(XmlTypeCode.Byte).ChangeType((int)((sbyte)this.data[this.tokDataPos]), returnType, namespaceResolver);
				case BinXmlToken.XSD_UNSIGNEDSHORT:
				{
					int @uint = (int)this.GetUInt16(this.tokDataPos);
					return this.GetValueConverter(XmlTypeCode.UnsignedShort).ChangeType(@uint, returnType, namespaceResolver);
				}
				case BinXmlToken.XSD_UNSIGNEDINT:
				{
					long num2 = (long)((ulong)this.GetUInt32(this.tokDataPos));
					return this.GetValueConverter(XmlTypeCode.UnsignedInt).ChangeType(num2, returnType, namespaceResolver);
				}
				case BinXmlToken.XSD_UNSIGNEDLONG:
				{
					decimal num3 = this.GetUInt64(this.tokDataPos);
					return this.GetValueConverter(XmlTypeCode.UnsignedLong).ChangeType(num3, returnType, namespaceResolver);
				}
				case BinXmlToken.XSD_QNAME:
				{
					int num4 = this.ParseMB32(this.tokDataPos);
					if (num4 < 0 || num4 >= this.symbolTables.qnameCount)
					{
						throw new XmlException("XmlBin_InvalidQNameID", string.Empty);
					}
					XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[num4];
					return this.GetValueConverter(XmlTypeCode.QName).ChangeType(new XmlQualifiedName(qname.localname, qname.namespaceUri), returnType, namespaceResolver);
				}
				default:
					goto IL_526;
				}
				break;
			}
			return this.GetValueConverter(XmlTypeCode.Decimal).ChangeType(new BinXmlSqlDecimal(this.data, this.tokDataPos, token == BinXmlToken.XSD_DECIMAL).ToDecimal(), returnType, namespaceResolver);
			IL_3F4:
			byte[] array = new byte[this.tokLen];
			Array.Copy(this.data, this.tokDataPos, array, 0, this.tokLen);
			return this.GetValueConverter((token == BinXmlToken.XSD_BINHEX) ? XmlTypeCode.HexBinary : XmlTypeCode.Base64Binary).ChangeType(array, returnType, namespaceResolver);
			IL_43E:
			return this.GetValueConverter(XmlTypeCode.DateTime).ChangeType(this.ValueAsDateTime(), returnType, namespaceResolver);
			IL_526:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0005B0E4 File Offset: 0x000592E4
		private short GetInt16(int pos)
		{
			byte[] array = this.data;
			return (short)((int)array[pos] | (int)array[pos + 1] << 8);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0005B104 File Offset: 0x00059304
		private ushort GetUInt16(int pos)
		{
			byte[] array = this.data;
			return (ushort)((int)array[pos] | (int)array[pos + 1] << 8);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0005B124 File Offset: 0x00059324
		private int GetInt32(int pos)
		{
			byte[] array = this.data;
			return (int)array[pos] | (int)array[pos + 1] << 8 | (int)array[pos + 2] << 16 | (int)array[pos + 3] << 24;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0005B158 File Offset: 0x00059358
		private uint GetUInt32(int pos)
		{
			byte[] array = this.data;
			return (uint)((int)array[pos] | (int)array[pos + 1] << 8 | (int)array[pos + 2] << 16 | (int)array[pos + 3] << 24);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0005B18C File Offset: 0x0005938C
		private long GetInt64(int pos)
		{
			byte[] array = this.data;
			uint num = (uint)((int)array[pos] | (int)array[pos + 1] << 8 | (int)array[pos + 2] << 16 | (int)array[pos + 3] << 24);
			uint num2 = (uint)((int)array[pos + 4] | (int)array[pos + 5] << 8 | (int)array[pos + 6] << 16 | (int)array[pos + 7] << 24);
			return (long)((ulong)num2 << 32 | (ulong)num);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0005B1E8 File Offset: 0x000593E8
		private ulong GetUInt64(int pos)
		{
			byte[] array = this.data;
			uint num = (uint)((int)array[pos] | (int)array[pos + 1] << 8 | (int)array[pos + 2] << 16 | (int)array[pos + 3] << 24);
			uint num2 = (uint)((int)array[pos + 4] | (int)array[pos + 5] << 8 | (int)array[pos + 6] << 16 | (int)array[pos + 7] << 24);
			return (ulong)num2 << 32 | (ulong)num;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0005B244 File Offset: 0x00059444
		private unsafe float GetSingle(int offset)
		{
			byte[] array = this.data;
			uint num = (uint)((int)array[offset] | (int)array[offset + 1] << 8 | (int)array[offset + 2] << 16 | (int)array[offset + 3] << 24);
			return *(float*)(&num);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0005B27C File Offset: 0x0005947C
		private unsafe double GetDouble(int offset)
		{
			uint num = (uint)((int)this.data[offset] | (int)this.data[offset + 1] << 8 | (int)this.data[offset + 2] << 16 | (int)this.data[offset + 3] << 24);
			uint num2 = (uint)((int)this.data[offset + 4] | (int)this.data[offset + 5] << 8 | (int)this.data[offset + 6] << 16 | (int)this.data[offset + 7] << 24);
			ulong num3 = (ulong)num2 << 32 | (ulong)num;
			return *(double*)(&num3);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0005B2FC File Offset: 0x000594FC
		private Exception ThrowUnexpectedToken(BinXmlToken token)
		{
			return this.ThrowXmlException("XmlBinary_UnexpectedToken");
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0005B309 File Offset: 0x00059509
		private Exception ThrowXmlException(string res)
		{
			this.state = XmlSqlBinaryReader.ScanState.Error;
			return new XmlException(res, null);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0005B319 File Offset: 0x00059519
		private Exception ThrowXmlException(string res, string arg1, string arg2)
		{
			this.state = XmlSqlBinaryReader.ScanState.Error;
			return new XmlException(res, new string[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0005B336 File Offset: 0x00059536
		private Exception ThrowNotSupported(string res)
		{
			this.state = XmlSqlBinaryReader.ScanState.Error;
			return new NotSupportedException(Res.GetString(res));
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0005B34A File Offset: 0x0005954A
		public override Task<string> GetValueAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005B351 File Offset: 0x00059551
		public override Task<bool> ReadAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0005B358 File Offset: 0x00059558
		public override Task<object> ReadContentAsObjectAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0005B35F File Offset: 0x0005955F
		public override Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0005B366 File Offset: 0x00059566
		public override Task<XmlNodeType> MoveToContentAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0005B36D File Offset: 0x0005956D
		public override Task<string> ReadContentAsStringAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0005B374 File Offset: 0x00059574
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0005B37B File Offset: 0x0005957B
		public override Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0005B382 File Offset: 0x00059582
		public override Task<object> ReadElementContentAsObjectAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005B389 File Offset: 0x00059589
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0005B390 File Offset: 0x00059590
		public override Task<string> ReadInnerXmlAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0005B397 File Offset: 0x00059597
		public override Task<string> ReadOuterXmlAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0005B39E File Offset: 0x0005959E
		public override Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0005B3A5 File Offset: 0x000595A5
		public override Task SkipAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0005B3AC File Offset: 0x000595AC
		public override Task<string> ReadElementContentAsStringAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040005F0 RID: 1520
		internal static readonly Type TypeOfObject = typeof(object);

		// Token: 0x040005F1 RID: 1521
		internal static readonly Type TypeOfString = typeof(string);

		// Token: 0x040005F2 RID: 1522
		private static volatile Type[] TokenTypeMap = null;

		// Token: 0x040005F3 RID: 1523
		private static byte[] XsdKatmaiTimeScaleToValueLengthMap = new byte[]
		{
			3,
			3,
			3,
			4,
			4,
			5,
			5,
			5
		};

		// Token: 0x040005F4 RID: 1524
		private static ReadState[] ScanState2ReadState = new ReadState[]
		{
			ReadState.Interactive,
			ReadState.Interactive,
			ReadState.Interactive,
			ReadState.Interactive,
			ReadState.Interactive,
			ReadState.Initial,
			ReadState.Error,
			ReadState.EndOfFile,
			ReadState.Closed
		};

		// Token: 0x040005F5 RID: 1525
		private Stream inStrm;

		// Token: 0x040005F6 RID: 1526
		private byte[] data;

		// Token: 0x040005F7 RID: 1527
		private int pos;

		// Token: 0x040005F8 RID: 1528
		private int mark;

		// Token: 0x040005F9 RID: 1529
		private int end;

		// Token: 0x040005FA RID: 1530
		private long offset;

		// Token: 0x040005FB RID: 1531
		private bool eof;

		// Token: 0x040005FC RID: 1532
		private bool sniffed;

		// Token: 0x040005FD RID: 1533
		private bool isEmpty;

		// Token: 0x040005FE RID: 1534
		private int docState;

		// Token: 0x040005FF RID: 1535
		private XmlSqlBinaryReader.SymbolTables symbolTables;

		// Token: 0x04000600 RID: 1536
		private XmlNameTable xnt;

		// Token: 0x04000601 RID: 1537
		private bool xntFromSettings;

		// Token: 0x04000602 RID: 1538
		private string xml;

		// Token: 0x04000603 RID: 1539
		private string xmlns;

		// Token: 0x04000604 RID: 1540
		private string nsxmlns;

		// Token: 0x04000605 RID: 1541
		private string baseUri;

		// Token: 0x04000606 RID: 1542
		private XmlSqlBinaryReader.ScanState state;

		// Token: 0x04000607 RID: 1543
		private XmlNodeType nodetype;

		// Token: 0x04000608 RID: 1544
		private BinXmlToken token;

		// Token: 0x04000609 RID: 1545
		private int attrIndex;

		// Token: 0x0400060A RID: 1546
		private XmlSqlBinaryReader.QName qnameOther;

		// Token: 0x0400060B RID: 1547
		private XmlSqlBinaryReader.QName qnameElement;

		// Token: 0x0400060C RID: 1548
		private XmlNodeType parentNodeType;

		// Token: 0x0400060D RID: 1549
		private XmlSqlBinaryReader.ElemInfo[] elementStack;

		// Token: 0x0400060E RID: 1550
		private int elemDepth;

		// Token: 0x0400060F RID: 1551
		private XmlSqlBinaryReader.AttrInfo[] attributes;

		// Token: 0x04000610 RID: 1552
		private int[] attrHashTbl;

		// Token: 0x04000611 RID: 1553
		private int attrCount;

		// Token: 0x04000612 RID: 1554
		private int posAfterAttrs;

		// Token: 0x04000613 RID: 1555
		private bool xmlspacePreserve;

		// Token: 0x04000614 RID: 1556
		private int tokLen;

		// Token: 0x04000615 RID: 1557
		private int tokDataPos;

		// Token: 0x04000616 RID: 1558
		private bool hasTypedValue;

		// Token: 0x04000617 RID: 1559
		private Type valueType;

		// Token: 0x04000618 RID: 1560
		private string stringValue;

		// Token: 0x04000619 RID: 1561
		private Dictionary<string, XmlSqlBinaryReader.NamespaceDecl> namespaces;

		// Token: 0x0400061A RID: 1562
		private XmlSqlBinaryReader.NestedBinXml prevNameInfo;

		// Token: 0x0400061B RID: 1563
		private XmlReader textXmlReader;

		// Token: 0x0400061C RID: 1564
		private bool closeInput;

		// Token: 0x0400061D RID: 1565
		private bool checkCharacters;

		// Token: 0x0400061E RID: 1566
		private bool ignoreWhitespace;

		// Token: 0x0400061F RID: 1567
		private bool ignorePIs;

		// Token: 0x04000620 RID: 1568
		private bool ignoreComments;

		// Token: 0x04000621 RID: 1569
		private DtdProcessing dtdProcessing;

		// Token: 0x04000622 RID: 1570
		private SecureStringHasher hasher;

		// Token: 0x04000623 RID: 1571
		private XmlCharType xmlCharType;

		// Token: 0x04000624 RID: 1572
		private Encoding unicode;

		// Token: 0x04000625 RID: 1573
		private byte version;

		// Token: 0x0200043A RID: 1082
		private enum ScanState
		{
			// Token: 0x04001C3B RID: 7227
			Doc,
			// Token: 0x04001C3C RID: 7228
			XmlText,
			// Token: 0x04001C3D RID: 7229
			Attr,
			// Token: 0x04001C3E RID: 7230
			AttrVal,
			// Token: 0x04001C3F RID: 7231
			AttrValPseudoValue,
			// Token: 0x04001C40 RID: 7232
			Init,
			// Token: 0x04001C41 RID: 7233
			Error,
			// Token: 0x04001C42 RID: 7234
			EOF,
			// Token: 0x04001C43 RID: 7235
			Closed
		}

		// Token: 0x0200043B RID: 1083
		internal struct QName
		{
			// Token: 0x06003045 RID: 12357 RVA: 0x00113FAE File Offset: 0x001121AE
			public QName(string prefix, string lname, string nsUri)
			{
				this.prefix = prefix;
				this.localname = lname;
				this.namespaceUri = nsUri;
			}

			// Token: 0x06003046 RID: 12358 RVA: 0x00113FC5 File Offset: 0x001121C5
			public void Set(string prefix, string lname, string nsUri)
			{
				this.prefix = prefix;
				this.localname = lname;
				this.namespaceUri = nsUri;
			}

			// Token: 0x06003047 RID: 12359 RVA: 0x00113FDC File Offset: 0x001121DC
			public void Clear()
			{
				this.prefix = (this.localname = (this.namespaceUri = string.Empty));
			}

			// Token: 0x06003048 RID: 12360 RVA: 0x00114006 File Offset: 0x00112206
			public bool MatchNs(string lname, string nsUri)
			{
				return lname == this.localname && nsUri == this.namespaceUri;
			}

			// Token: 0x06003049 RID: 12361 RVA: 0x00114024 File Offset: 0x00112224
			public bool MatchPrefix(string prefix, string lname)
			{
				return lname == this.localname && prefix == this.prefix;
			}

			// Token: 0x0600304A RID: 12362 RVA: 0x00114042 File Offset: 0x00112242
			public void CheckPrefixNS(string prefix, string namespaceUri)
			{
				if (this.prefix == prefix && this.namespaceUri != namespaceUri)
				{
					throw new XmlException("XmlBinary_NoRemapPrefix", new string[]
					{
						prefix,
						this.namespaceUri,
						namespaceUri
					});
				}
			}

			// Token: 0x0600304B RID: 12363 RVA: 0x00114082 File Offset: 0x00112282
			public override int GetHashCode()
			{
				return this.prefix.GetHashCode() ^ this.localname.GetHashCode();
			}

			// Token: 0x0600304C RID: 12364 RVA: 0x0011409B File Offset: 0x0011229B
			public int GetNSHashCode(SecureStringHasher hasher)
			{
				return hasher.GetHashCode(this.namespaceUri) ^ hasher.GetHashCode(this.localname);
			}

			// Token: 0x0600304D RID: 12365 RVA: 0x001140B8 File Offset: 0x001122B8
			public override bool Equals(object other)
			{
				if (other is XmlSqlBinaryReader.QName)
				{
					XmlSqlBinaryReader.QName b = (XmlSqlBinaryReader.QName)other;
					return this == b;
				}
				return false;
			}

			// Token: 0x0600304E RID: 12366 RVA: 0x001140E2 File Offset: 0x001122E2
			public override string ToString()
			{
				if (this.prefix.Length == 0)
				{
					return this.localname;
				}
				return this.prefix + ":" + this.localname;
			}

			// Token: 0x0600304F RID: 12367 RVA: 0x0011410E File Offset: 0x0011230E
			public static bool operator ==(XmlSqlBinaryReader.QName a, XmlSqlBinaryReader.QName b)
			{
				return a.prefix == b.prefix && a.localname == b.localname && a.namespaceUri == b.namespaceUri;
			}

			// Token: 0x06003050 RID: 12368 RVA: 0x00114149 File Offset: 0x00112349
			public static bool operator !=(XmlSqlBinaryReader.QName a, XmlSqlBinaryReader.QName b)
			{
				return !(a == b);
			}

			// Token: 0x04001C44 RID: 7236
			public string prefix;

			// Token: 0x04001C45 RID: 7237
			public string localname;

			// Token: 0x04001C46 RID: 7238
			public string namespaceUri;
		}

		// Token: 0x0200043C RID: 1084
		private struct ElemInfo
		{
			// Token: 0x06003051 RID: 12369 RVA: 0x00114155 File Offset: 0x00112355
			public void Set(XmlSqlBinaryReader.QName name, bool xmlspacePreserve)
			{
				this.name = name;
				this.xmlLang = null;
				this.xmlSpace = XmlSpace.None;
				this.xmlspacePreserve = xmlspacePreserve;
			}

			// Token: 0x06003052 RID: 12370 RVA: 0x00114174 File Offset: 0x00112374
			public XmlSqlBinaryReader.NamespaceDecl Clear()
			{
				XmlSqlBinaryReader.NamespaceDecl result = this.nsdecls;
				this.nsdecls = null;
				return result;
			}

			// Token: 0x04001C47 RID: 7239
			public XmlSqlBinaryReader.QName name;

			// Token: 0x04001C48 RID: 7240
			public string xmlLang;

			// Token: 0x04001C49 RID: 7241
			public XmlSpace xmlSpace;

			// Token: 0x04001C4A RID: 7242
			public bool xmlspacePreserve;

			// Token: 0x04001C4B RID: 7243
			public XmlSqlBinaryReader.NamespaceDecl nsdecls;
		}

		// Token: 0x0200043D RID: 1085
		private struct AttrInfo
		{
			// Token: 0x06003053 RID: 12371 RVA: 0x00114190 File Offset: 0x00112390
			public void Set(XmlSqlBinaryReader.QName n, string v)
			{
				this.name = n;
				this.val = v;
				this.contentPos = 0;
				this.hashCode = 0;
				this.prevHash = 0;
			}

			// Token: 0x06003054 RID: 12372 RVA: 0x001141B5 File Offset: 0x001123B5
			public void Set(XmlSqlBinaryReader.QName n, int pos)
			{
				this.name = n;
				this.val = null;
				this.contentPos = pos;
				this.hashCode = 0;
				this.prevHash = 0;
			}

			// Token: 0x06003055 RID: 12373 RVA: 0x001141DA File Offset: 0x001123DA
			public void GetLocalnameAndNamespaceUri(out string localname, out string namespaceUri)
			{
				localname = this.name.localname;
				namespaceUri = this.name.namespaceUri;
			}

			// Token: 0x06003056 RID: 12374 RVA: 0x001141F8 File Offset: 0x001123F8
			public int GetLocalnameAndNamespaceUriAndHash(SecureStringHasher hasher, out string localname, out string namespaceUri)
			{
				localname = this.name.localname;
				namespaceUri = this.name.namespaceUri;
				return this.hashCode = this.name.GetNSHashCode(hasher);
			}

			// Token: 0x06003057 RID: 12375 RVA: 0x00114234 File Offset: 0x00112434
			public bool MatchNS(string localname, string namespaceUri)
			{
				return this.name.MatchNs(localname, namespaceUri);
			}

			// Token: 0x06003058 RID: 12376 RVA: 0x00114243 File Offset: 0x00112443
			public bool MatchHashNS(int hash, string localname, string namespaceUri)
			{
				return this.hashCode == hash && this.name.MatchNs(localname, namespaceUri);
			}

			// Token: 0x06003059 RID: 12377 RVA: 0x0011425D File Offset: 0x0011245D
			public void AdjustPosition(int adj)
			{
				if (this.contentPos != 0)
				{
					this.contentPos += adj;
				}
			}

			// Token: 0x04001C4C RID: 7244
			public XmlSqlBinaryReader.QName name;

			// Token: 0x04001C4D RID: 7245
			public string val;

			// Token: 0x04001C4E RID: 7246
			public int contentPos;

			// Token: 0x04001C4F RID: 7247
			public int hashCode;

			// Token: 0x04001C50 RID: 7248
			public int prevHash;
		}

		// Token: 0x0200043E RID: 1086
		private class NamespaceDecl
		{
			// Token: 0x0600305A RID: 12378 RVA: 0x00114275 File Offset: 0x00112475
			public NamespaceDecl(string prefix, string nsuri, XmlSqlBinaryReader.NamespaceDecl nextInScope, XmlSqlBinaryReader.NamespaceDecl prevDecl, int scope, bool implied)
			{
				this.prefix = prefix;
				this.uri = nsuri;
				this.scopeLink = nextInScope;
				this.prevLink = prevDecl;
				this.scope = scope;
				this.implied = implied;
			}

			// Token: 0x04001C51 RID: 7249
			public string prefix;

			// Token: 0x04001C52 RID: 7250
			public string uri;

			// Token: 0x04001C53 RID: 7251
			public XmlSqlBinaryReader.NamespaceDecl scopeLink;

			// Token: 0x04001C54 RID: 7252
			public XmlSqlBinaryReader.NamespaceDecl prevLink;

			// Token: 0x04001C55 RID: 7253
			public int scope;

			// Token: 0x04001C56 RID: 7254
			public bool implied;
		}

		// Token: 0x0200043F RID: 1087
		private struct SymbolTables
		{
			// Token: 0x0600305B RID: 12379 RVA: 0x001142AA File Offset: 0x001124AA
			public void Init()
			{
				this.symtable = new string[64];
				this.qnametable = new XmlSqlBinaryReader.QName[16];
				this.symtable[0] = string.Empty;
				this.symCount = 1;
				this.qnameCount = 1;
			}

			// Token: 0x04001C57 RID: 7255
			public string[] symtable;

			// Token: 0x04001C58 RID: 7256
			public int symCount;

			// Token: 0x04001C59 RID: 7257
			public XmlSqlBinaryReader.QName[] qnametable;

			// Token: 0x04001C5A RID: 7258
			public int qnameCount;
		}

		// Token: 0x02000440 RID: 1088
		private class NestedBinXml
		{
			// Token: 0x0600305C RID: 12380 RVA: 0x001142E1 File Offset: 0x001124E1
			public NestedBinXml(XmlSqlBinaryReader.SymbolTables symbolTables, int docState, XmlSqlBinaryReader.NestedBinXml next)
			{
				this.symbolTables = symbolTables;
				this.docState = docState;
				this.next = next;
			}

			// Token: 0x04001C5B RID: 7259
			public XmlSqlBinaryReader.SymbolTables symbolTables;

			// Token: 0x04001C5C RID: 7260
			public int docState;

			// Token: 0x04001C5D RID: 7261
			public XmlSqlBinaryReader.NestedBinXml next;
		}
	}
}
