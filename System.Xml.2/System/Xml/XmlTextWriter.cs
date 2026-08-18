using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000DC RID: 220
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlTextWriter : XmlWriter
	{
		// Token: 0x06000CA2 RID: 3234 RVA: 0x000346F4 File Offset: 0x000328F4
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

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00034790 File Offset: 0x00032990
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

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000347EA File Offset: 0x000329EA
		public XmlTextWriter(string filename, Encoding encoding) : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), encoding)
		{
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000347FC File Offset: 0x000329FC
		public XmlTextWriter(TextWriter w) : this()
		{
			this.textWriter = w;
			this.encoding = w.Encoding;
			this.xmlEncoder = new XmlTextEncoder(w);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00034834 File Offset: 0x00032A34
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00034858 File Offset: 0x00032A58
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x00034860 File Offset: 0x00032A60
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

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00034881 File Offset: 0x00032A81
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x00034889 File Offset: 0x00032A89
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0003489C File Offset: 0x00032A9C
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x000348A4 File Offset: 0x00032AA4
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

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x000348C1 File Offset: 0x00032AC1
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x000348C9 File Offset: 0x00032AC9
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

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x000348D2 File Offset: 0x00032AD2
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x000348DA File Offset: 0x00032ADA
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

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00034909 File Offset: 0x00032B09
		public override void WriteStartDocument()
		{
			this.StartDocument(-1);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00034912 File Offset: 0x00032B12
		public override void WriteStartDocument(bool standalone)
		{
			this.StartDocument(standalone ? 1 : 0);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00034924 File Offset: 0x00032B24
		public override void WriteEndDocument()
		{
			try
			{
				this.AutoCompleteAll();
				if (this.currentState != XmlTextWriter.State.Epilog)
				{
					if (this.currentState == XmlTextWriter.State.Closed)
					{
						throw new ArgumentException(Res.GetString("Xml_ClosedOrError"));
					}
					throw new ArgumentException(Res.GetString("Xml_NoRoot"));
				}
				else
				{
					this.stateTable = XmlTextWriter.stateTableDefault;
					this.currentState = XmlTextWriter.State.Start;
					this.lastToken = XmlTextWriter.Token.Empty;
				}
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000349A0 File Offset: 0x00032BA0
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
					this.textWriter.Write(" PUBLIC " + this.quoteChar.ToString());
					this.textWriter.Write(pubid);
					this.textWriter.Write(this.quoteChar.ToString() + " " + this.quoteChar.ToString());
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				else if (sysid != null)
				{
					this.textWriter.Write(" SYSTEM " + this.quoteChar.ToString());
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

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00034AF8 File Offset: 0x00032CF8
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

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00034CF4 File Offset: 0x00032EF4
		public override void WriteEndElement()
		{
			this.InternalWriteEndElement(false);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00034CFD File Offset: 0x00032EFD
		public override void WriteFullEndElement()
		{
			this.InternalWriteEndElement(true);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00034D08 File Offset: 0x00032F08
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
				this.xmlEncoder.StartAttribute(this.specialAttr > XmlTextWriter.SpecialAttr.None);
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

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00034FBC File Offset: 0x000331BC
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

		// Token: 0x06000CBA RID: 3258 RVA: 0x00034FEC File Offset: 0x000331EC
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

		// Token: 0x06000CBB RID: 3259 RVA: 0x00035070 File Offset: 0x00033270
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

		// Token: 0x06000CBC RID: 3260 RVA: 0x0003510C File Offset: 0x0003330C
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

		// Token: 0x06000CBD RID: 3261 RVA: 0x00035194 File Offset: 0x00033394
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

		// Token: 0x06000CBE RID: 3262 RVA: 0x000351DC File Offset: 0x000333DC
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

		// Token: 0x06000CBF RID: 3263 RVA: 0x0003521C File Offset: 0x0003341C
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
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

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00035284 File Offset: 0x00033484
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

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000352CC File Offset: 0x000334CC
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

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0003530C File Offset: 0x0003350C
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

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0003534C File Offset: 0x0003354C
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

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0003538C File Offset: 0x0003358C
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

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000353CC File Offset: 0x000335CC
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

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00035434 File Offset: 0x00033634
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

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00035470 File Offset: 0x00033670
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

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000354C4 File Offset: 0x000336C4
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

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0003550C File Offset: 0x0003370C
		public override void Flush()
		{
			this.textWriter.Flush();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003551C File Offset: 0x0003371C
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

		// Token: 0x06000CCB RID: 3275 RVA: 0x00035558 File Offset: 0x00033758
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

		// Token: 0x06000CCC RID: 3276 RVA: 0x00035644 File Offset: 0x00033844
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

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0003569C File Offset: 0x0003389C
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

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x000356D4 File Offset: 0x000338D4
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

		// Token: 0x06000CCF RID: 3279 RVA: 0x0003570C File Offset: 0x0003390C
		public override void WriteNmToken(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyName"));
				}
				if (!ValidateNames.IsNmtokenNoNamespaces(name))
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

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00035788 File Offset: 0x00033988
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
				stringBuilder.Append("version=" + this.quoteChar.ToString() + "1.0" + this.quoteChar.ToString());
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

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000358A4 File Offset: 0x00033AA4
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

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00035B2B File Offset: 0x00033D2B
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

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00035B4C File Offset: 0x00033D4C
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

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00035CA4 File Offset: 0x00033EA4
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

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00035E76 File Offset: 0x00034076
		private void WriteEndAttributeQuote()
		{
			if (this.specialAttr != XmlTextWriter.SpecialAttr.None)
			{
				this.HandleSpecialAttribute();
			}
			this.xmlEncoder.EndAttribute();
			this.textWriter.Write(this.curQuoteChar);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00035EA4 File Offset: 0x000340A4
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

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00035F20 File Offset: 0x00034120
		private void PushNamespace(string prefix, string ns, bool declared)
		{
			if ("http://www.w3.org/2000/xmlns/" == ns)
			{
				throw new ArgumentException(Res.GetString("Xml_CanNotBindToReservedNamespace"));
			}
			if (prefix == null)
			{
				XmlTextWriter.NamespaceState defaultNsState = this.stack[this.top].defaultNsState;
				if (defaultNsState > XmlTextWriter.NamespaceState.NotDeclaredButInScope)
				{
					if (defaultNsState != XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut)
					{
						return;
					}
				}
				else
				{
					this.stack[this.top].defaultNs = ns;
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

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00036038 File Offset: 0x00034238
		private void AddNamespace(string prefix, string ns, bool declared)
		{
			int num = this.nsTop + 1;
			this.nsTop = num;
			int num2 = num;
			if (num2 == this.nsStack.Length)
			{
				XmlTextWriter.Namespace[] destinationArray = new XmlTextWriter.Namespace[num2 * 2];
				Array.Copy(this.nsStack, destinationArray, num2);
				this.nsStack = destinationArray;
			}
			this.nsStack[num2].Set(prefix, ns, declared);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(num2);
				return;
			}
			if (num2 == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(new SecureStringHasher());
				for (int i = 0; i <= num2; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000360D4 File Offset: 0x000342D4
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

		// Token: 0x06000CDA RID: 3290 RVA: 0x00036124 File Offset: 0x00034324
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

		// Token: 0x06000CDB RID: 3291 RVA: 0x000361A0 File Offset: 0x000343A0
		private string GeneratePrefix()
		{
			XmlTextWriter.TagInfo[] array = this.stack;
			int num = this.top;
			int prefixCount = array[num].prefixCount;
			array[num].prefixCount = prefixCount + 1;
			int num2 = prefixCount + 1;
			return "d" + this.top.ToString("d", CultureInfo.InvariantCulture) + "p" + num2.ToString("d", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00036204 File Offset: 0x00034404
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

		// Token: 0x06000CDD RID: 3293 RVA: 0x00036264 File Offset: 0x00034464
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

		// Token: 0x06000CDE RID: 3294 RVA: 0x000362B8 File Offset: 0x000344B8
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

		// Token: 0x06000CDF RID: 3295 RVA: 0x0003633C File Offset: 0x0003453C
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

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000363A0 File Offset: 0x000345A0
		private void InternalWriteName(string name, bool isNCName)
		{
			this.ValidateName(name, isNCName);
			this.textWriter.Write(name);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000363B8 File Offset: 0x000345B8
		private void ValidateName(string name, bool isNCName)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_EmptyName"));
			}
			int length = name.Length;
			if (this.namespaces)
			{
				int num = -1;
				for (int num2 = ValidateNames.ParseNCName(name); num2 != length; num2 += ValidateNames.ParseNmtoken(name, num2))
				{
					if (name[num2] != ':' || isNCName || num != -1 || num2 <= 0 || num2 + 1 >= length)
					{
						goto IL_6F;
					}
					num = num2;
					num2++;
				}
				return;
			}
			if (ValidateNames.IsNameNoNamespaces(name))
			{
				return;
			}
			IL_6F:
			throw new ArgumentException(Res.GetString("Xml_InvalidNameChars", new object[]
			{
				name
			}));
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00036450 File Offset: 0x00034650
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

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00036524 File Offset: 0x00034724
		private void VerifyPrefixXml(string prefix, string ns)
		{
			if (prefix != null && prefix.Length == 3 && (prefix[0] == 'x' || prefix[0] == 'X') && (prefix[1] == 'm' || prefix[1] == 'M') && (prefix[2] == 'l' || prefix[2] == 'L') && "http://www.w3.org/XML/1998/namespace" != ns)
			{
				throw new ArgumentException(Res.GetString("Xml_InvalidPrefix"));
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003659C File Offset: 0x0003479C
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

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003661A File Offset: 0x0003481A
		private void FlushEncoders()
		{
			if (this.base64Encoder != null)
			{
				this.base64Encoder.Flush();
			}
			this.flush = false;
		}

		// Token: 0x040003CE RID: 974
		private TextWriter textWriter;

		// Token: 0x040003CF RID: 975
		private XmlTextEncoder xmlEncoder;

		// Token: 0x040003D0 RID: 976
		private Encoding encoding;

		// Token: 0x040003D1 RID: 977
		private Formatting formatting;

		// Token: 0x040003D2 RID: 978
		private bool indented;

		// Token: 0x040003D3 RID: 979
		private int indentation;

		// Token: 0x040003D4 RID: 980
		private char indentChar;

		// Token: 0x040003D5 RID: 981
		private XmlTextWriter.TagInfo[] stack;

		// Token: 0x040003D6 RID: 982
		private int top;

		// Token: 0x040003D7 RID: 983
		private XmlTextWriter.State[] stateTable;

		// Token: 0x040003D8 RID: 984
		private XmlTextWriter.State currentState;

		// Token: 0x040003D9 RID: 985
		private XmlTextWriter.Token lastToken;

		// Token: 0x040003DA RID: 986
		private XmlTextWriterBase64Encoder base64Encoder;

		// Token: 0x040003DB RID: 987
		private char quoteChar;

		// Token: 0x040003DC RID: 988
		private char curQuoteChar;

		// Token: 0x040003DD RID: 989
		private bool namespaces;

		// Token: 0x040003DE RID: 990
		private XmlTextWriter.SpecialAttr specialAttr;

		// Token: 0x040003DF RID: 991
		private string prefixForXmlNs;

		// Token: 0x040003E0 RID: 992
		private bool flush;

		// Token: 0x040003E1 RID: 993
		private XmlTextWriter.Namespace[] nsStack;

		// Token: 0x040003E2 RID: 994
		private int nsTop;

		// Token: 0x040003E3 RID: 995
		private Dictionary<string, int> nsHashtable;

		// Token: 0x040003E4 RID: 996
		private bool useNsHashtable;

		// Token: 0x040003E5 RID: 997
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040003E6 RID: 998
		private const int NamespaceStackInitialSize = 8;

		// Token: 0x040003E7 RID: 999
		private const int MaxNamespacesWalkCount = 16;

		// Token: 0x040003E8 RID: 1000
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

		// Token: 0x040003E9 RID: 1001
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

		// Token: 0x040003EA RID: 1002
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

		// Token: 0x040003EB RID: 1003
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

		// Token: 0x020003C3 RID: 963
		private enum NamespaceState
		{
			// Token: 0x0400191B RID: 6427
			Uninitialized,
			// Token: 0x0400191C RID: 6428
			NotDeclaredButInScope,
			// Token: 0x0400191D RID: 6429
			DeclaredButNotWrittenOut,
			// Token: 0x0400191E RID: 6430
			DeclaredAndWrittenOut
		}

		// Token: 0x020003C4 RID: 964
		private struct TagInfo
		{
			// Token: 0x06002F5C RID: 12124 RVA: 0x00106382 File Offset: 0x00104582
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

			// Token: 0x0400191F RID: 6431
			internal string name;

			// Token: 0x04001920 RID: 6432
			internal string prefix;

			// Token: 0x04001921 RID: 6433
			internal string defaultNs;

			// Token: 0x04001922 RID: 6434
			internal XmlTextWriter.NamespaceState defaultNsState;

			// Token: 0x04001923 RID: 6435
			internal XmlSpace xmlSpace;

			// Token: 0x04001924 RID: 6436
			internal string xmlLang;

			// Token: 0x04001925 RID: 6437
			internal int prevNsTop;

			// Token: 0x04001926 RID: 6438
			internal int prefixCount;

			// Token: 0x04001927 RID: 6439
			internal bool mixed;
		}

		// Token: 0x020003C5 RID: 965
		private struct Namespace
		{
			// Token: 0x06002F5D RID: 12125 RVA: 0x001063C0 File Offset: 0x001045C0
			internal void Set(string prefix, string ns, bool declared)
			{
				this.prefix = prefix;
				this.ns = ns;
				this.declared = declared;
				this.prevNsIndex = -1;
			}

			// Token: 0x04001928 RID: 6440
			internal string prefix;

			// Token: 0x04001929 RID: 6441
			internal string ns;

			// Token: 0x0400192A RID: 6442
			internal bool declared;

			// Token: 0x0400192B RID: 6443
			internal int prevNsIndex;
		}

		// Token: 0x020003C6 RID: 966
		private enum SpecialAttr
		{
			// Token: 0x0400192D RID: 6445
			None,
			// Token: 0x0400192E RID: 6446
			XmlSpace,
			// Token: 0x0400192F RID: 6447
			XmlLang,
			// Token: 0x04001930 RID: 6448
			XmlNs
		}

		// Token: 0x020003C7 RID: 967
		private enum State
		{
			// Token: 0x04001932 RID: 6450
			Start,
			// Token: 0x04001933 RID: 6451
			Prolog,
			// Token: 0x04001934 RID: 6452
			PostDTD,
			// Token: 0x04001935 RID: 6453
			Element,
			// Token: 0x04001936 RID: 6454
			Attribute,
			// Token: 0x04001937 RID: 6455
			Content,
			// Token: 0x04001938 RID: 6456
			AttrOnly,
			// Token: 0x04001939 RID: 6457
			Epilog,
			// Token: 0x0400193A RID: 6458
			Error,
			// Token: 0x0400193B RID: 6459
			Closed
		}

		// Token: 0x020003C8 RID: 968
		private enum Token
		{
			// Token: 0x0400193D RID: 6461
			PI,
			// Token: 0x0400193E RID: 6462
			Doctype,
			// Token: 0x0400193F RID: 6463
			Comment,
			// Token: 0x04001940 RID: 6464
			CData,
			// Token: 0x04001941 RID: 6465
			StartElement,
			// Token: 0x04001942 RID: 6466
			EndElement,
			// Token: 0x04001943 RID: 6467
			LongEndElement,
			// Token: 0x04001944 RID: 6468
			StartAttribute,
			// Token: 0x04001945 RID: 6469
			EndAttribute,
			// Token: 0x04001946 RID: 6470
			Content,
			// Token: 0x04001947 RID: 6471
			Base64,
			// Token: 0x04001948 RID: 6472
			RawData,
			// Token: 0x04001949 RID: 6473
			Whitespace,
			// Token: 0x0400194A RID: 6474
			Empty
		}
	}
}
