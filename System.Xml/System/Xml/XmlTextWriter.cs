using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000094 RID: 148
	public class XmlTextWriter : XmlWriter
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x00025004 File Offset: 0x00024004
		internal XmlTextWriter()
		{
			this.namespaces = true;
			this.formatting = Formatting.None;
			this.indentation = 2;
			this.indentChar = ' ';
			this.nsStack = new XmlTextWriter.Namespace[8];
			this.nsTop = -1;
			this.stack = new XmlTextWriter.TagInfo[10];
			this.top = 0;
			this.stack[this.top].Init(-1);
			this.quoteChar = '"';
			this.stateTable = XmlTextWriter.stateTableDefault;
			this.currentState = XmlTextWriter.State.Start;
			this.lastToken = XmlTextWriter.Token.Empty;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000250A0 File Offset: 0x000240A0
		public XmlTextWriter(Stream w, Encoding encoding) : this()
		{
			this.encoding = encoding;
			if (encoding != null)
			{
				this.textWriter = new StreamWriter(w, encoding);
			}
			else
			{
				this.textWriter = new StreamWriter(w);
			}
			this.xmlEncoder = new XmlTextEncoder(this.textWriter);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000250FA File Offset: 0x000240FA
		public XmlTextWriter(string filename, Encoding encoding) : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), encoding)
		{
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002510C File Offset: 0x0002410C
		public XmlTextWriter(TextWriter w) : this()
		{
			this.textWriter = w;
			this.encoding = w.Encoding;
			this.xmlEncoder = new XmlTextEncoder(w);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00025144 File Offset: 0x00024144
		public Stream BaseStream
		{
			get
			{
				StreamWriter streamWriter = this.textWriter as StreamWriter;
				if (streamWriter != null)
				{
					return streamWriter.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00025168 File Offset: 0x00024168
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00025170 File Offset: 0x00024170
		public bool Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				if (this.currentState != XmlTextWriter.State.Start)
				{
					throw new InvalidOperationException(Res.GetString("Xml_NotInWriteState"));
				}
				this.namespaces = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00025191 File Offset: 0x00024191
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x00025199 File Offset: 0x00024199
		public Formatting Formatting
		{
			get
			{
				return this.formatting;
			}
			set
			{
				this.formatting = value;
				this.indented = (value == Formatting.Indented);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000251AC File Offset: 0x000241AC
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x000251B4 File Offset: 0x000241B4
		public int Indentation
		{
			get
			{
				return this.indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("Xml_InvalidIndentation"));
				}
				this.indentation = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x000251D1 File Offset: 0x000241D1
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x000251D9 File Offset: 0x000241D9
		public char IndentChar
		{
			get
			{
				return this.indentChar;
			}
			set
			{
				this.indentChar = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x000251E2 File Offset: 0x000241E2
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x000251EA File Offset: 0x000241EA
		public char QuoteChar
		{
			get
			{
				return this.quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException(Res.GetString("Xml_InvalidQuote"));
				}
				this.quoteChar = value;
				this.xmlEncoder.QuoteChar = value;
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00025219 File Offset: 0x00024219
		public override void WriteStartDocument()
		{
			this.StartDocument(-1);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00025222 File Offset: 0x00024222
		public override void WriteStartDocument(bool standalone)
		{
			this.StartDocument(standalone ? 1 : 0);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00025234 File Offset: 0x00024234
		public override void WriteEndDocument()
		{
			try
			{
				this.AutoCompleteAll();
				if (this.currentState != XmlTextWriter.State.Epilog)
				{
					throw new ArgumentException(Res.GetString("Xml_NoRoot"));
				}
				this.stateTable = XmlTextWriter.stateTableDefault;
				this.currentState = XmlTextWriter.State.Start;
				this.lastToken = XmlTextWriter.Token.Empty;
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00025298 File Offset: 0x00024298
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			try
			{
				this.ValidateName(name, false);
				this.AutoComplete(XmlTextWriter.Token.Doctype);
				this.textWriter.Write("<!DOCTYPE ");
				this.textWriter.Write(name);
				if (pubid != null)
				{
					this.textWriter.Write(" PUBLIC " + this.quoteChar);
					this.textWriter.Write(pubid);
					this.textWriter.Write(this.quoteChar + " " + this.quoteChar);
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				else if (sysid != null)
				{
					this.textWriter.Write(" SYSTEM " + this.quoteChar);
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				if (subset != null)
				{
					this.textWriter.Write("[");
					this.textWriter.Write(subset);
					this.textWriter.Write("]");
				}
				this.textWriter.Write('>');
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000253F0 File Offset: 0x000243F0
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.StartElement);
				this.PushStack();
				this.textWriter.Write('<');
				if (this.namespaces)
				{
					this.stack[this.top].defaultNs = this.stack[this.top - 1].defaultNs;
					if (this.stack[this.top - 1].defaultNsState != XmlTextWriter.NamespaceState.Uninitialized)
					{
						this.stack[this.top].defaultNsState = XmlTextWriter.NamespaceState.NotDeclaredButInScope;
					}
					this.stack[this.top].mixed = this.stack[this.top - 1].mixed;
					if (ns == null)
					{
						if (prefix != null && prefix.Length != 0 && this.LookupNamespace(prefix) == -1)
						{
							throw new ArgumentException(Res.GetString("Xml_UndefPrefix"));
						}
					}
					else if (prefix == null)
					{
						string text = this.FindPrefix(ns);
						if (text != null)
						{
							prefix = text;
						}
						else
						{
							this.PushNamespace(null, ns, false);
						}
					}
					else if (prefix.Length == 0)
					{
						this.PushNamespace(null, ns, false);
					}
					else
					{
						if (ns.Length == 0)
						{
							prefix = null;
						}
						this.VerifyPrefixXml(prefix, ns);
						this.PushNamespace(prefix, ns, false);
					}
					this.stack[this.top].prefix = null;
					if (prefix != null && prefix.Length != 0)
					{
						this.stack[this.top].prefix = prefix;
						this.textWriter.Write(prefix);
						this.textWriter.Write(':');
					}
				}
				else if ((ns != null && ns.Length != 0) || (prefix != null && prefix.Length != 0))
				{
					throw new ArgumentException(Res.GetString("Xml_NoNamespaces"));
				}
				this.stack[this.top].name = localName;
				this.textWriter.Write(localName);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000255EC File Offset: 0x000245EC
		public override void WriteEndElement()
		{
			this.InternalWriteEndElement(false);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000255F5 File Offset: 0x000245F5
		public override void WriteFullEndElement()
		{
			this.InternalWriteEndElement(true);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00025600 File Offset: 0x00024600
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.StartAttribute);
				this.specialAttr = XmlTextWriter.SpecialAttr.None;
				if (this.namespaces)
				{
					if (prefix != null && prefix.Length == 0)
					{
						prefix = null;
					}
					if (ns == "http://www.w3.org/2000/xmlns/" && prefix == null && localName != "xmlns")
					{
						prefix = "xmlns";
					}
					if (prefix == "xml")
					{
						if (localName == "lang")
						{
							this.specialAttr = XmlTextWriter.SpecialAttr.XmlLang;
						}
						else if (localName == "space")
						{
							this.specialAttr = XmlTextWriter.SpecialAttr.XmlSpace;
						}
					}
					else if (prefix == "xmlns")
					{
						if ("http://www.w3.org/2000/xmlns/" != ns && ns != null)
						{
							throw new ArgumentException(Res.GetString("Xml_XmlnsBelongsToReservedNs"));
						}
						if (localName == null || localName.Length == 0)
						{
							localName = prefix;
							prefix = null;
							this.prefixForXmlNs = null;
						}
						else
						{
							this.prefixForXmlNs = localName;
						}
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlNs;
					}
					else if (prefix == null && localName == "xmlns")
					{
						if ("http://www.w3.org/2000/xmlns/" != ns && ns != null)
						{
							throw new ArgumentException(Res.GetString("Xml_XmlnsBelongsToReservedNs"));
						}
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlNs;
						this.prefixForXmlNs = null;
					}
					else if (ns == null)
					{
						if (prefix != null && this.LookupNamespace(prefix) == -1)
						{
							throw new ArgumentException(Res.GetString("Xml_UndefPrefix"));
						}
					}
					else if (ns.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						this.VerifyPrefixXml(prefix, ns);
						if (prefix != null && this.LookupNamespaceInCurrentScope(prefix) != -1)
						{
							prefix = null;
						}
						string text = this.FindPrefix(ns);
						if (text != null && (prefix == null || prefix == text))
						{
							prefix = text;
						}
						else
						{
							if (prefix == null)
							{
								prefix = this.GeneratePrefix();
							}
							this.PushNamespace(prefix, ns, false);
						}
					}
					if (prefix != null && prefix.Length != 0)
					{
						this.textWriter.Write(prefix);
						this.textWriter.Write(':');
					}
				}
				else
				{
					if ((ns != null && ns.Length != 0) || (prefix != null && prefix.Length != 0))
					{
						throw new ArgumentException(Res.GetString("Xml_NoNamespaces"));
					}
					if (localName == "xml:lang")
					{
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlLang;
					}
					else if (localName == "xml:space")
					{
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlSpace;
					}
				}
				this.xmlEncoder.StartAttribute(this.specialAttr != XmlTextWriter.SpecialAttr.None);
				this.textWriter.Write(localName);
				this.textWriter.Write('=');
				if (this.curQuoteChar != this.quoteChar)
				{
					this.curQuoteChar = this.quoteChar;
					this.xmlEncoder.QuoteChar = this.quoteChar;
				}
				this.textWriter.Write(this.curQuoteChar);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000258B8 File Offset: 0x000248B8
		public override void WriteEndAttribute()
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.EndAttribute);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000258E8 File Offset: 0x000248E8
		public override void WriteCData(string text)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.CData);
				if (text != null && text.IndexOf("]]>", StringComparison.Ordinal) >= 0)
				{
					throw new ArgumentException(Res.GetString("Xml_InvalidCDataChars"));
				}
				this.textWriter.Write("<![CDATA[");
				if (text != null)
				{
					this.xmlEncoder.WriteRawWithSurrogateChecking(text);
				}
				this.textWriter.Write("]]>");
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002596C File Offset: 0x0002496C
		public override void WriteComment(string text)
		{
			try
			{
				if (text != null && (text.IndexOf("--", StringComparison.Ordinal) >= 0 || (text.Length != 0 && text[text.Length - 1] == '-')))
				{
					throw new ArgumentException(Res.GetString("Xml_InvalidCommentChars"));
				}
				this.AutoComplete(XmlTextWriter.Token.Comment);
				this.textWriter.Write("<!--");
				if (text != null)
				{
					this.xmlEncoder.WriteRawWithSurrogateChecking(text);
				}
				this.textWriter.Write("-->");
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00025A08 File Offset: 0x00024A08
		public override void WriteProcessingInstruction(string name, string text)
		{
			try
			{
				if (text != null && text.IndexOf("?>", StringComparison.Ordinal) >= 0)
				{
					throw new ArgumentException(Res.GetString("Xml_InvalidPiChars"));
				}
				if (string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0 && this.stateTable == XmlTextWriter.stateTableDocument)
				{
					throw new ArgumentException(Res.GetString("Xml_DupXmlDecl"));
				}
				this.AutoComplete(XmlTextWriter.Token.PI);
				this.InternalWriteProcessingInstruction(name, text);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00025A90 File Offset: 0x00024A90
		public override void WriteEntityRef(string name)
		{
			try
			{
				this.ValidateName(name, false);
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteEntityRef(name);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00025AD8 File Offset: 0x00024AD8
		public override void WriteCharEntity(char ch)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteCharEntity(ch);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00025B18 File Offset: 0x00024B18
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null || ws.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_NonWhitespace"));
				}
				if (!this.xmlCharType.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Xml_NonWhitespace"));
				}
				this.AutoComplete(XmlTextWriter.Token.Whitespace);
				this.xmlEncoder.Write(ws);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00025B90 File Offset: 0x00024B90
		public override void WriteString(string text)
		{
			try
			{
				if (text != null && text.Length != 0)
				{
					this.AutoComplete(XmlTextWriter.Token.Content);
					this.xmlEncoder.Write(text);
				}
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00025BD8 File Offset: 0x00024BD8
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteSurrogateCharEntity(lowChar, highChar);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00025C18 File Offset: 0x00024C18
		public override void WriteChars(char[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.Write(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00025C58 File Offset: 0x00024C58
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.RawData);
				this.xmlEncoder.WriteRaw(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00025C98 File Offset: 0x00024C98
		public override void WriteRaw(string data)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.RawData);
				this.xmlEncoder.WriteRawWithSurrogateChecking(data);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00025CD8 File Offset: 0x00024CD8
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			try
			{
				if (!this.flush)
				{
					this.AutoComplete(XmlTextWriter.Token.Base64);
				}
				this.flush = true;
				if (this.base64Encoder == null)
				{
					this.base64Encoder = new XmlTextWriterBase64Encoder(this.xmlEncoder);
				}
				this.base64Encoder.Encode(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00025D40 File Offset: 0x00024D40
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				BinHexEncoder.Encode(buffer, index, count, this);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00025D7C File Offset: 0x00024D7C
		public override WriteState WriteState
		{
			get
			{
				switch (this.currentState)
				{
				case XmlTextWriter.State.Start:
					return WriteState.Start;
				case XmlTextWriter.State.Prolog:
				case XmlTextWriter.State.PostDTD:
					return WriteState.Prolog;
				case XmlTextWriter.State.Element:
					return WriteState.Element;
				case XmlTextWriter.State.Attribute:
				case XmlTextWriter.State.AttrOnly:
					return WriteState.Attribute;
				case XmlTextWriter.State.Content:
				case XmlTextWriter.State.Epilog:
					return WriteState.Content;
				case XmlTextWriter.State.Error:
					return WriteState.Error;
				case XmlTextWriter.State.Closed:
					return WriteState.Closed;
				default:
					return WriteState.Error;
				}
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00025DD0 File Offset: 0x00024DD0
		public override void Close()
		{
			try
			{
				this.AutoCompleteAll();
			}
			catch
			{
			}
			finally
			{
				this.currentState = XmlTextWriter.State.Closed;
				this.textWriter.Close();
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00025E1C File Offset: 0x00024E1C
		public override void Flush()
		{
			this.textWriter.Flush();
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00025E2C File Offset: 0x00024E2C
		public override void WriteName(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.InternalWriteName(name, false);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00025E68 File Offset: 0x00024E68
		public override void WriteQualifiedName(string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (this.namespaces)
				{
					if (ns != null && ns.Length != 0 && ns != this.stack[this.top].defaultNs)
					{
						string text = this.FindPrefix(ns);
						if (text == null)
						{
							if (this.currentState != XmlTextWriter.State.Attribute)
							{
								throw new ArgumentException(Res.GetString("Xml_UndefNamespace", new object[]
								{
									ns
								}));
							}
							text = this.GeneratePrefix();
							this.PushNamespace(text, ns, false);
						}
						if (text.Length != 0)
						{
							this.InternalWriteName(text, true);
							this.textWriter.Write(':');
						}
					}
				}
				else if (ns != null && ns.Length != 0)
				{
					throw new ArgumentException(Res.GetString("Xml_NoNamespaces"));
				}
				this.InternalWriteName(localName, true);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00025F58 File Offset: 0x00024F58
		public override string LookupPrefix(string ns)
		{
			if (ns == null || ns.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_EmptyName"));
			}
			string text = this.FindPrefix(ns);
			if (text == null && ns == this.stack[this.top].defaultNs)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00025FB0 File Offset: 0x00024FB0
		public override XmlSpace XmlSpace
		{
			get
			{
				for (int i = this.top; i > 0; i--)
				{
					XmlSpace xmlSpace = this.stack[i].xmlSpace;
					if (xmlSpace != XmlSpace.None)
					{
						return xmlSpace;
					}
				}
				return XmlSpace.None;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00025FE8 File Offset: 0x00024FE8
		public override string XmlLang
		{
			get
			{
				for (int i = this.top; i > 0; i--)
				{
					string xmlLang = this.stack[i].xmlLang;
					if (xmlLang != null)
					{
						return xmlLang;
					}
				}
				return null;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00026020 File Offset: 0x00025020
		public override void WriteNmToken(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyName"));
				}
				if (!this.xmlCharType.IsNmToken(name))
				{
					throw new ArgumentException(Res.GetString("Xml_InvalidNameChars", new object[]
					{
						name
					}));
				}
				this.textWriter.Write(name);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000260A4 File Offset: 0x000250A4
		private void StartDocument(int standalone)
		{
			try
			{
				if (this.currentState != XmlTextWriter.State.Start)
				{
					throw new InvalidOperationException(Res.GetString("Xml_NotTheFirst"));
				}
				this.stateTable = XmlTextWriter.stateTableDocument;
				this.currentState = XmlTextWriter.State.Prolog;
				StringBuilder stringBuilder = new StringBuilder(128);
				stringBuilder.Append(string.Concat(new object[]
				{
					"version=",
					this.quoteChar,
					"1.0",
					this.quoteChar
				}));
				if (this.encoding != null)
				{
					stringBuilder.Append(" encoding=");
					stringBuilder.Append(this.quoteChar);
					stringBuilder.Append(this.encoding.WebName);
					stringBuilder.Append(this.quoteChar);
				}
				if (standalone >= 0)
				{
					stringBuilder.Append(" standalone=");
					stringBuilder.Append(this.quoteChar);
					stringBuilder.Append((standalone == 0) ? "no" : "yes");
					stringBuilder.Append(this.quoteChar);
				}
				this.InternalWriteProcessingInstruction("xml", stringBuilder.ToString());
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000261E0 File Offset: 0x000251E0
		private void AutoComplete(XmlTextWriter.Token token)
		{
			if (this.currentState == XmlTextWriter.State.Closed)
			{
				throw new InvalidOperationException(Res.GetString("Xml_Closed"));
			}
			if (this.currentState == XmlTextWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Xml_WrongToken", new object[]
				{
					XmlTextWriter.tokenName[(int)token],
					XmlTextWriter.stateName[8]
				}));
			}
			XmlTextWriter.State state = this.stateTable[(int)(token * XmlTextWriter.Token.EndAttribute + (int)this.currentState)];
			if (state == XmlTextWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Xml_WrongToken", new object[]
				{
					XmlTextWriter.tokenName[(int)token],
					XmlTextWriter.stateName[(int)this.currentState]
				}));
			}
			switch (token)
			{
			case XmlTextWriter.Token.PI:
			case XmlTextWriter.Token.Comment:
			case XmlTextWriter.Token.CData:
			case XmlTextWriter.Token.StartElement:
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
					this.WriteEndStartTag(false);
				}
				else if (this.currentState == XmlTextWriter.State.Element)
				{
					this.WriteEndStartTag(false);
				}
				if (token == XmlTextWriter.Token.CData)
				{
					this.stack[this.top].mixed = true;
				}
				else if (this.indented && this.currentState != XmlTextWriter.State.Start)
				{
					this.Indent(false);
				}
				break;
			case XmlTextWriter.Token.Doctype:
				if (this.indented && this.currentState != XmlTextWriter.State.Start)
				{
					this.Indent(false);
				}
				break;
			case XmlTextWriter.Token.EndElement:
			case XmlTextWriter.Token.LongEndElement:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
				}
				if (this.currentState == XmlTextWriter.State.Content)
				{
					token = XmlTextWriter.Token.LongEndElement;
				}
				else
				{
					this.WriteEndStartTag(token == XmlTextWriter.Token.EndElement);
				}
				if (XmlTextWriter.stateTableDocument == this.stateTable && this.top == 1)
				{
					state = XmlTextWriter.State.Epilog;
				}
				break;
			case XmlTextWriter.Token.StartAttribute:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
					this.textWriter.Write(' ');
				}
				else if (this.currentState == XmlTextWriter.State.Element)
				{
					this.textWriter.Write(' ');
				}
				break;
			case XmlTextWriter.Token.EndAttribute:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				this.WriteEndAttributeQuote();
				break;
			case XmlTextWriter.Token.Content:
			case XmlTextWriter.Token.Base64:
			case XmlTextWriter.Token.RawData:
			case XmlTextWriter.Token.Whitespace:
				if (token != XmlTextWriter.Token.Base64 && this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Element && this.lastToken != XmlTextWriter.Token.Content)
				{
					this.WriteEndStartTag(false);
				}
				if (state == XmlTextWriter.State.Content)
				{
					this.stack[this.top].mixed = true;
				}
				break;
			default:
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
			this.currentState = state;
			this.lastToken = token;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002646D File Offset: 0x0002546D
		private void AutoCompleteAll()
		{
			if (this.flush)
			{
				this.FlushEncoders();
			}
			while (this.top > 0)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00026490 File Offset: 0x00025490
		private void InternalWriteEndElement(bool longFormat)
		{
			try
			{
				if (this.top <= 0)
				{
					throw new InvalidOperationException(Res.GetString("Xml_NoStartTag"));
				}
				this.AutoComplete(longFormat ? XmlTextWriter.Token.LongEndElement : XmlTextWriter.Token.EndElement);
				if (this.lastToken == XmlTextWriter.Token.LongEndElement)
				{
					if (this.indented)
					{
						this.Indent(true);
					}
					this.textWriter.Write('<');
					this.textWriter.Write('/');
					if (this.namespaces && this.stack[this.top].prefix != null)
					{
						this.textWriter.Write(this.stack[this.top].prefix);
						this.textWriter.Write(':');
					}
					this.textWriter.Write(this.stack[this.top].name);
					this.textWriter.Write('>');
				}
				int prevNsTop = this.stack[this.top].prevNsTop;
				if (this.useNsHashtable && prevNsTop < this.nsTop)
				{
					this.PopNamespaces(prevNsTop + 1, this.nsTop);
				}
				this.nsTop = prevNsTop;
				this.top--;
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000265E8 File Offset: 0x000255E8
		private void WriteEndStartTag(bool empty)
		{
			this.xmlEncoder.StartAttribute(false);
			for (int i = this.nsTop; i > this.stack[this.top].prevNsTop; i--)
			{
				if (!this.nsStack[i].declared)
				{
					this.textWriter.Write(" xmlns");
					this.textWriter.Write(':');
					this.textWriter.Write(this.nsStack[i].prefix);
					this.textWriter.Write('=');
					this.textWriter.Write(this.quoteChar);
					this.xmlEncoder.Write(this.nsStack[i].ns);
					this.textWriter.Write(this.quoteChar);
				}
			}
			if (this.stack[this.top].defaultNs != this.stack[this.top - 1].defaultNs && this.stack[this.top].defaultNsState == XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut)
			{
				this.textWriter.Write(" xmlns");
				this.textWriter.Write('=');
				this.textWriter.Write(this.quoteChar);
				this.xmlEncoder.Write(this.stack[this.top].defaultNs);
				this.textWriter.Write(this.quoteChar);
				this.stack[this.top].defaultNsState = XmlTextWriter.NamespaceState.DeclaredAndWrittenOut;
			}
			this.xmlEncoder.EndAttribute();
			if (empty)
			{
				this.textWriter.Write(" /");
			}
			this.textWriter.Write('>');
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000267BA File Offset: 0x000257BA
		private void WriteEndAttributeQuote()
		{
			if (this.specialAttr != XmlTextWriter.SpecialAttr.None)
			{
				this.HandleSpecialAttribute();
			}
			this.xmlEncoder.EndAttribute();
			this.textWriter.Write(this.curQuoteChar);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000267E8 File Offset: 0x000257E8
		private void Indent(bool beforeEndElement)
		{
			if (this.top == 0)
			{
				this.textWriter.WriteLine();
				return;
			}
			if (!this.stack[this.top].mixed)
			{
				this.textWriter.WriteLine();
				int i = beforeEndElement ? (this.top - 1) : this.top;
				for (i *= this.indentation; i > 0; i--)
				{
					this.textWriter.Write(this.indentChar);
				}
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00026864 File Offset: 0x00025864
		private void PushNamespace(string prefix, string ns, bool declared)
		{
			if ("http://www.w3.org/2000/xmlns/" == ns)
			{
				throw new ArgumentException(Res.GetString("Xml_CanNotBindToReservedNamespace"));
			}
			if (prefix == null)
			{
				switch (this.stack[this.top].defaultNsState)
				{
				case XmlTextWriter.NamespaceState.Uninitialized:
				case XmlTextWriter.NamespaceState.NotDeclaredButInScope:
					this.stack[this.top].defaultNs = ns;
					break;
				case XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut:
					break;
				default:
					return;
				}
				this.stack[this.top].defaultNsState = (declared ? XmlTextWriter.NamespaceState.DeclaredAndWrittenOut : XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut);
				return;
			}
			if (prefix.Length != 0 && ns.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_PrefixForEmptyNs"));
			}
			int num = this.LookupNamespace(prefix);
			if (num != -1 && this.nsStack[num].ns == ns)
			{
				if (declared)
				{
					this.nsStack[num].declared = true;
					return;
				}
			}
			else
			{
				if (declared && num != -1 && num > this.stack[this.top].prevNsTop)
				{
					this.nsStack[num].declared = true;
				}
				this.AddNamespace(prefix, ns, declared);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00026988 File Offset: 0x00025988
		private void AddNamespace(string prefix, string ns, bool declared)
		{
			int num = ++this.nsTop;
			if (num == this.nsStack.Length)
			{
				XmlTextWriter.Namespace[] destinationArray = new XmlTextWriter.Namespace[num * 2];
				Array.Copy(this.nsStack, destinationArray, num);
				this.nsStack = destinationArray;
			}
			this.nsStack[num].Set(prefix, ns, declared);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(num);
				return;
			}
			if (num == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(new SecureStringHasher());
				for (int i = 0; i <= num; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00026A24 File Offset: 0x00025A24
		private void AddToNamespaceHashtable(int namespaceIndex)
		{
			string prefix = this.nsStack[namespaceIndex].prefix;
			int prevNsIndex;
			if (this.nsHashtable.TryGetValue(prefix, out prevNsIndex))
			{
				this.nsStack[namespaceIndex].prevNsIndex = prevNsIndex;
			}
			this.nsHashtable[prefix] = namespaceIndex;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00026A74 File Offset: 0x00025A74
		private void PopNamespaces(int indexFrom, int indexTo)
		{
			for (int i = indexTo; i >= indexFrom; i--)
			{
				if (this.nsStack[i].prevNsIndex == -1)
				{
					this.nsHashtable.Remove(this.nsStack[i].prefix);
				}
				else
				{
					this.nsHashtable[this.nsStack[i].prefix] = this.nsStack[i].prevNsIndex;
				}
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00026AF0 File Offset: 0x00025AF0
		private string GeneratePrefix()
		{
			XmlTextWriter.TagInfo[] array = this.stack;
			int num = this.top;
			int prefixCount;
			array[num].prefixCount = (prefixCount = array[num].prefixCount) + 1;
			int num2 = prefixCount + 1;
			return "d" + this.top.ToString("d", CultureInfo.InvariantCulture) + "p" + num2.ToString("d", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00026B58 File Offset: 0x00025B58
		private void InternalWriteProcessingInstruction(string name, string text)
		{
			this.textWriter.Write("<?");
			this.ValidateName(name, false);
			this.textWriter.Write(name);
			this.textWriter.Write(' ');
			if (text != null)
			{
				this.xmlEncoder.WriteRawWithSurrogateChecking(text);
			}
			this.textWriter.Write("?>");
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00026BB8 File Offset: 0x00025BB8
		private int LookupNamespace(string prefix)
		{
			if (this.useNsHashtable)
			{
				int result;
				if (this.nsHashtable.TryGetValue(prefix, out result))
				{
					return result;
				}
			}
			else
			{
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00026C0C File Offset: 0x00025C0C
		private int LookupNamespaceInCurrentScope(string prefix)
		{
			if (this.useNsHashtable)
			{
				int num;
				if (this.nsHashtable.TryGetValue(prefix, out num) && num > this.stack[this.top].prevNsTop)
				{
					return num;
				}
			}
			else
			{
				for (int i = this.nsTop; i > this.stack[this.top].prevNsTop; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00026C90 File Offset: 0x00025C90
		private string FindPrefix(string ns)
		{
			for (int i = this.nsTop; i >= 0; i--)
			{
				if (this.nsStack[i].ns == ns && this.LookupNamespace(this.nsStack[i].prefix) == i)
				{
					return this.nsStack[i].prefix;
				}
			}
			return null;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00026CF4 File Offset: 0x00025CF4
		private void InternalWriteName(string name, bool NCName)
		{
			this.ValidateName(name, NCName);
			this.textWriter.Write(name);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00026D0C File Offset: 0x00025D0C
		private unsafe void ValidateName(string name, bool NCName)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_EmptyName"));
			}
			int length = name.Length;
			int num = 0;
			int num2 = -1;
			if (this.namespaces)
			{
				if ((this.xmlCharType.charProperties[name[num]] & 4) != 0)
				{
					for (;;)
					{
						num++;
						while (num < length && (this.xmlCharType.charProperties[name[num]] & 8) != 0)
						{
							num++;
						}
						if (num == length)
						{
							break;
						}
						if (name[num] != ':' || NCName || num2 != -1 || num + 1 >= length)
						{
							goto IL_E9;
						}
						num2 = num;
					}
					return;
				}
			}
			else if ((this.xmlCharType.charProperties[name[0]] & 4) != 0 || name[0] == ':')
			{
				num++;
				while (num < length && ((this.xmlCharType.charProperties[name[num]] & 8) != 0 || name[num] == ':'))
				{
					num++;
				}
				if (num == length)
				{
					return;
				}
			}
			IL_E9:
			throw new ArgumentException(Res.GetString("Xml_InvalidNameChars", new object[]
			{
				name
			}));
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00026E20 File Offset: 0x00025E20
		private void HandleSpecialAttribute()
		{
			string text = this.xmlEncoder.AttributeValue;
			switch (this.specialAttr)
			{
			case XmlTextWriter.SpecialAttr.XmlSpace:
				text = XmlConvert.TrimString(text);
				if (text == "default")
				{
					this.stack[this.top].xmlSpace = XmlSpace.Default;
					return;
				}
				if (text == "preserve")
				{
					this.stack[this.top].xmlSpace = XmlSpace.Preserve;
					return;
				}
				throw new ArgumentException(Res.GetString("Xml_InvalidXmlSpace", new object[]
				{
					text
				}));
			case XmlTextWriter.SpecialAttr.XmlLang:
				this.stack[this.top].xmlLang = text;
				return;
			case XmlTextWriter.SpecialAttr.XmlNs:
				this.VerifyPrefixXml(this.prefixForXmlNs, text);
				this.PushNamespace(this.prefixForXmlNs, text, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00026EF8 File Offset: 0x00025EF8
		private void VerifyPrefixXml(string prefix, string ns)
		{
			if (prefix != null && prefix.Length == 3 && (prefix[0] == 'x' || prefix[0] == 'X') && (prefix[1] == 'm' || prefix[1] == 'M') && (prefix[2] == 'l' || prefix[2] == 'L') && "http://www.w3.org/XML/1998/namespace" != ns)
			{
				throw new ArgumentException(Res.GetString("Xml_InvalidPrefix"));
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00026F70 File Offset: 0x00025F70
		private void PushStack()
		{
			if (this.top == this.stack.Length - 1)
			{
				XmlTextWriter.TagInfo[] destinationArray = new XmlTextWriter.TagInfo[this.stack.Length + 10];
				if (this.top > 0)
				{
					Array.Copy(this.stack, destinationArray, this.top + 1);
				}
				this.stack = destinationArray;
			}
			this.top++;
			this.stack[this.top].Init(this.nsTop);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00026FEE File Offset: 0x00025FEE
		private void FlushEncoders()
		{
			if (this.base64Encoder != null)
			{
				this.base64Encoder.Flush();
			}
			this.flush = false;
		}

		// Token: 0x0400074E RID: 1870
		private const int NamespaceStackInitialSize = 8;

		// Token: 0x0400074F RID: 1871
		private const int MaxNamespacesWalkCount = 16;

		// Token: 0x04000750 RID: 1872
		private TextWriter textWriter;

		// Token: 0x04000751 RID: 1873
		private XmlTextEncoder xmlEncoder;

		// Token: 0x04000752 RID: 1874
		private Encoding encoding;

		// Token: 0x04000753 RID: 1875
		private Formatting formatting;

		// Token: 0x04000754 RID: 1876
		private bool indented;

		// Token: 0x04000755 RID: 1877
		private int indentation;

		// Token: 0x04000756 RID: 1878
		private char indentChar;

		// Token: 0x04000757 RID: 1879
		private XmlTextWriter.TagInfo[] stack;

		// Token: 0x04000758 RID: 1880
		private int top;

		// Token: 0x04000759 RID: 1881
		private XmlTextWriter.State[] stateTable;

		// Token: 0x0400075A RID: 1882
		private XmlTextWriter.State currentState;

		// Token: 0x0400075B RID: 1883
		private XmlTextWriter.Token lastToken;

		// Token: 0x0400075C RID: 1884
		private XmlTextWriterBase64Encoder base64Encoder;

		// Token: 0x0400075D RID: 1885
		private char quoteChar;

		// Token: 0x0400075E RID: 1886
		private char curQuoteChar;

		// Token: 0x0400075F RID: 1887
		private bool namespaces;

		// Token: 0x04000760 RID: 1888
		private XmlTextWriter.SpecialAttr specialAttr;

		// Token: 0x04000761 RID: 1889
		private string prefixForXmlNs;

		// Token: 0x04000762 RID: 1890
		private bool flush;

		// Token: 0x04000763 RID: 1891
		private XmlTextWriter.Namespace[] nsStack;

		// Token: 0x04000764 RID: 1892
		private int nsTop;

		// Token: 0x04000765 RID: 1893
		private Dictionary<string, int> nsHashtable;

		// Token: 0x04000766 RID: 1894
		private bool useNsHashtable;

		// Token: 0x04000767 RID: 1895
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000768 RID: 1896
		private static string[] stateName = new string[]
		{
			"Start",
			"Prolog",
			"PostDTD",
			"Element",
			"Attribute",
			"Content",
			"AttrOnly",
			"Epilog",
			"Error",
			"Closed"
		};

		// Token: 0x04000769 RID: 1897
		private static string[] tokenName = new string[]
		{
			"PI",
			"Doctype",
			"Comment",
			"CData",
			"StartElement",
			"EndElement",
			"LongEndElement",
			"StartAttribute",
			"EndAttribute",
			"Content",
			"Base64",
			"RawData",
			"Whitespace",
			"Empty"
		};

		// Token: 0x0400076A RID: 1898
		private static readonly XmlTextWriter.State[] stateTableDefault = new XmlTextWriter.State[]
		{
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.AttrOnly,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog
		};

		// Token: 0x0400076B RID: 1899
		private static readonly XmlTextWriter.State[] stateTableDocument = new XmlTextWriter.State[]
		{
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog
		};

		// Token: 0x02000095 RID: 149
		private enum NamespaceState
		{
			// Token: 0x0400076D RID: 1901
			Uninitialized,
			// Token: 0x0400076E RID: 1902
			NotDeclaredButInScope,
			// Token: 0x0400076F RID: 1903
			DeclaredButNotWrittenOut,
			// Token: 0x04000770 RID: 1904
			DeclaredAndWrittenOut
		}

		// Token: 0x02000096 RID: 150
		private struct TagInfo
		{
			// Token: 0x06000856 RID: 2134 RVA: 0x00027515 File Offset: 0x00026515
			internal void Init(int nsTop)
			{
				this.name = null;
				this.defaultNs = string.Empty;
				this.defaultNsState = XmlTextWriter.NamespaceState.Uninitialized;
				this.xmlSpace = XmlSpace.None;
				this.xmlLang = null;
				this.prevNsTop = nsTop;
				this.prefixCount = 0;
				this.mixed = false;
			}

			// Token: 0x04000771 RID: 1905
			internal string name;

			// Token: 0x04000772 RID: 1906
			internal string prefix;

			// Token: 0x04000773 RID: 1907
			internal string defaultNs;

			// Token: 0x04000774 RID: 1908
			internal XmlTextWriter.NamespaceState defaultNsState;

			// Token: 0x04000775 RID: 1909
			internal XmlSpace xmlSpace;

			// Token: 0x04000776 RID: 1910
			internal string xmlLang;

			// Token: 0x04000777 RID: 1911
			internal int prevNsTop;

			// Token: 0x04000778 RID: 1912
			internal int prefixCount;

			// Token: 0x04000779 RID: 1913
			internal bool mixed;
		}

		// Token: 0x02000097 RID: 151
		private struct Namespace
		{
			// Token: 0x06000857 RID: 2135 RVA: 0x00027553 File Offset: 0x00026553
			internal void Set(string prefix, string ns, bool declared)
			{
				this.prefix = prefix;
				this.ns = ns;
				this.declared = declared;
				this.prevNsIndex = -1;
			}

			// Token: 0x0400077A RID: 1914
			internal string prefix;

			// Token: 0x0400077B RID: 1915
			internal string ns;

			// Token: 0x0400077C RID: 1916
			internal bool declared;

			// Token: 0x0400077D RID: 1917
			internal int prevNsIndex;
		}

		// Token: 0x02000098 RID: 152
		private enum SpecialAttr
		{
			// Token: 0x0400077F RID: 1919
			None,
			// Token: 0x04000780 RID: 1920
			XmlSpace,
			// Token: 0x04000781 RID: 1921
			XmlLang,
			// Token: 0x04000782 RID: 1922
			XmlNs
		}

		// Token: 0x02000099 RID: 153
		private enum State
		{
			// Token: 0x04000784 RID: 1924
			Start,
			// Token: 0x04000785 RID: 1925
			Prolog,
			// Token: 0x04000786 RID: 1926
			PostDTD,
			// Token: 0x04000787 RID: 1927
			Element,
			// Token: 0x04000788 RID: 1928
			Attribute,
			// Token: 0x04000789 RID: 1929
			Content,
			// Token: 0x0400078A RID: 1930
			AttrOnly,
			// Token: 0x0400078B RID: 1931
			Epilog,
			// Token: 0x0400078C RID: 1932
			Error,
			// Token: 0x0400078D RID: 1933
			Closed
		}

		// Token: 0x0200009A RID: 154
		private enum Token
		{
			// Token: 0x0400078F RID: 1935
			PI,
			// Token: 0x04000790 RID: 1936
			Doctype,
			// Token: 0x04000791 RID: 1937
			Comment,
			// Token: 0x04000792 RID: 1938
			CData,
			// Token: 0x04000793 RID: 1939
			StartElement,
			// Token: 0x04000794 RID: 1940
			EndElement,
			// Token: 0x04000795 RID: 1941
			LongEndElement,
			// Token: 0x04000796 RID: 1942
			StartAttribute,
			// Token: 0x04000797 RID: 1943
			EndAttribute,
			// Token: 0x04000798 RID: 1944
			Content,
			// Token: 0x04000799 RID: 1945
			Base64,
			// Token: 0x0400079A RID: 1946
			RawData,
			// Token: 0x0400079B RID: 1947
			Whitespace,
			// Token: 0x0400079C RID: 1948
			Empty
		}
	}
}
