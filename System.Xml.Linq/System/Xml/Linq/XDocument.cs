using System;
using System.IO;
using System.Text;

namespace System.Xml.Linq
{
	// Token: 0x02000025 RID: 37
	[__DynamicallyInvokable]
	public class XDocument : XContainer
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00007EFD File Offset: 0x000060FD
		[__DynamicallyInvokable]
		public XDocument()
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007F05 File Offset: 0x00006105
		[__DynamicallyInvokable]
		public XDocument(params object[] content) : this()
		{
			base.AddContentSkipNotify(content);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007F14 File Offset: 0x00006114
		[__DynamicallyInvokable]
		public XDocument(XDeclaration declaration, params object[] content) : this(content)
		{
			this.declaration = declaration;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007F24 File Offset: 0x00006124
		[__DynamicallyInvokable]
		public XDocument(XDocument other) : base(other)
		{
			if (other.declaration != null)
			{
				this.declaration = new XDeclaration(other.declaration);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007F46 File Offset: 0x00006146
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00007F4E File Offset: 0x0000614E
		[__DynamicallyInvokable]
		public XDeclaration Declaration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.declaration;
			}
			[__DynamicallyInvokable]
			set
			{
				this.declaration = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007F57 File Offset: 0x00006157
		[__DynamicallyInvokable]
		public XDocumentType DocumentType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetFirstNode<XDocumentType>();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007F5F File Offset: 0x0000615F
		[__DynamicallyInvokable]
		public override XmlNodeType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlNodeType.Document;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007F63 File Offset: 0x00006163
		[__DynamicallyInvokable]
		public XElement Root
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetFirstNode<XElement>();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007F6B File Offset: 0x0000616B
		[__DynamicallyInvokable]
		public static XDocument Load(string uri)
		{
			return XDocument.Load(uri, LoadOptions.None);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007F74 File Offset: 0x00006174
		[__DynamicallyInvokable]
		public static XDocument Load(string uri, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XDocument result;
			using (XmlReader xmlReader = XmlReader.Create(uri, xmlReaderSettings))
			{
				result = XDocument.Load(xmlReader, options);
			}
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007FB8 File Offset: 0x000061B8
		[__DynamicallyInvokable]
		public static XDocument Load(Stream stream)
		{
			return XDocument.Load(stream, LoadOptions.None);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007FC4 File Offset: 0x000061C4
		[__DynamicallyInvokable]
		public static XDocument Load(Stream stream, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XDocument result;
			using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
			{
				result = XDocument.Load(xmlReader, options);
			}
			return result;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008008 File Offset: 0x00006208
		[__DynamicallyInvokable]
		public static XDocument Load(TextReader textReader)
		{
			return XDocument.Load(textReader, LoadOptions.None);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008014 File Offset: 0x00006214
		[__DynamicallyInvokable]
		public static XDocument Load(TextReader textReader, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XDocument result;
			using (XmlReader xmlReader = XmlReader.Create(textReader, xmlReaderSettings))
			{
				result = XDocument.Load(xmlReader, options);
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008058 File Offset: 0x00006258
		[__DynamicallyInvokable]
		public static XDocument Load(XmlReader reader)
		{
			return XDocument.Load(reader, LoadOptions.None);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008064 File Offset: 0x00006264
		[__DynamicallyInvokable]
		public static XDocument Load(XmlReader reader, LoadOptions options)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
			}
			XDocument xdocument = new XDocument();
			if ((options & LoadOptions.SetBaseUri) != LoadOptions.None)
			{
				string baseURI = reader.BaseURI;
				if (baseURI != null && baseURI.Length != 0)
				{
					xdocument.SetBaseUri(baseURI);
				}
			}
			if ((options & LoadOptions.SetLineInfo) != LoadOptions.None)
			{
				IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
				if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
				{
					xdocument.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
				}
			}
			if (reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				xdocument.Declaration = new XDeclaration(reader);
			}
			xdocument.ReadContentFrom(reader, options);
			if (!reader.EOF)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExpectedEndOfFile"));
			}
			if (xdocument.Root == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_MissingRoot"));
			}
			return xdocument;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000812A File Offset: 0x0000632A
		[__DynamicallyInvokable]
		public static XDocument Parse(string text)
		{
			return XDocument.Parse(text, LoadOptions.None);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008134 File Offset: 0x00006334
		[__DynamicallyInvokable]
		public static XDocument Parse(string text, LoadOptions options)
		{
			XDocument result;
			using (StringReader stringReader = new StringReader(text))
			{
				XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					result = XDocument.Load(xmlReader, options);
				}
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008194 File Offset: 0x00006394
		public void Save(string fileName)
		{
			this.Save(fileName, base.GetSaveOptionsFromAnnotations());
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000081A4 File Offset: 0x000063A4
		public void Save(string fileName, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			if (this.declaration != null && !string.IsNullOrEmpty(this.declaration.Encoding))
			{
				try
				{
					xmlWriterSettings.Encoding = Encoding.GetEncoding(this.declaration.Encoding);
				}
				catch (ArgumentException)
				{
				}
			}
			using (XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008224 File Offset: 0x00006424
		[__DynamicallyInvokable]
		public void Save(Stream stream)
		{
			this.Save(stream, base.GetSaveOptionsFromAnnotations());
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008234 File Offset: 0x00006434
		[__DynamicallyInvokable]
		public void Save(Stream stream, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			if (this.declaration != null && !string.IsNullOrEmpty(this.declaration.Encoding))
			{
				try
				{
					xmlWriterSettings.Encoding = Encoding.GetEncoding(this.declaration.Encoding);
				}
				catch (ArgumentException)
				{
				}
			}
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000082B4 File Offset: 0x000064B4
		[__DynamicallyInvokable]
		public void Save(TextWriter textWriter)
		{
			this.Save(textWriter, base.GetSaveOptionsFromAnnotations());
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000082C4 File Offset: 0x000064C4
		[__DynamicallyInvokable]
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008304 File Offset: 0x00006504
		[__DynamicallyInvokable]
		public void Save(XmlWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008310 File Offset: 0x00006510
		[__DynamicallyInvokable]
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (this.declaration != null && this.declaration.Standalone == "yes")
			{
				writer.WriteStartDocument(true);
			}
			else if (this.declaration != null && this.declaration.Standalone == "no")
			{
				writer.WriteStartDocument(false);
			}
			else
			{
				writer.WriteStartDocument();
			}
			base.WriteContentTo(writer);
			writer.WriteEndDocument();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000838E File Offset: 0x0000658E
		internal override void AddAttribute(XAttribute a)
		{
			throw new ArgumentException(Res.GetString("Argument_AddAttribute"));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000839F File Offset: 0x0000659F
		internal override void AddAttributeSkipNotify(XAttribute a)
		{
			throw new ArgumentException(Res.GetString("Argument_AddAttribute"));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000083B0 File Offset: 0x000065B0
		internal override XNode CloneNode()
		{
			return new XDocument(this);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000083B8 File Offset: 0x000065B8
		internal override bool DeepEquals(XNode node)
		{
			XDocument xdocument = node as XDocument;
			return xdocument != null && base.ContentsEqual(xdocument);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000083D8 File Offset: 0x000065D8
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000083E0 File Offset: 0x000065E0
		private T GetFirstNode<T>() where T : XNode
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				T t;
				for (;;)
				{
					xnode = xnode.next;
					t = (xnode as T);
					if (t != null)
					{
						break;
					}
					if (xnode == this.content)
					{
						goto IL_35;
					}
				}
				return t;
			}
			IL_35:
			return default(T);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000842C File Offset: 0x0000662C
		internal static bool IsWhitespace(string s)
		{
			foreach (char c in s)
			{
				if (c != ' ' && c != '\t' && c != '\r' && c != '\n')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000846C File Offset: 0x0000666C
		internal override void ValidateNode(XNode node, XNode previous)
		{
			XmlNodeType nodeType = node.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				this.ValidateDocument(previous, XmlNodeType.DocumentType, XmlNodeType.None);
				return;
			case XmlNodeType.Attribute:
				return;
			case XmlNodeType.Text:
				this.ValidateString(((XText)node).Value);
				return;
			case XmlNodeType.CDATA:
				throw new ArgumentException(Res.GetString("Argument_AddNode", new object[]
				{
					XmlNodeType.CDATA
				}));
			default:
				if (nodeType == XmlNodeType.Document)
				{
					throw new ArgumentException(Res.GetString("Argument_AddNode", new object[]
					{
						XmlNodeType.Document
					}));
				}
				if (nodeType != XmlNodeType.DocumentType)
				{
					return;
				}
				this.ValidateDocument(previous, XmlNodeType.None, XmlNodeType.Element);
				return;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000850C File Offset: 0x0000670C
		private void ValidateDocument(XNode previous, XmlNodeType allowBefore, XmlNodeType allowAfter)
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				if (previous == null)
				{
					allowBefore = allowAfter;
				}
				for (;;)
				{
					xnode = xnode.next;
					XmlNodeType nodeType = xnode.NodeType;
					if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.DocumentType)
					{
						if (nodeType != allowBefore)
						{
							break;
						}
						allowBefore = XmlNodeType.None;
					}
					if (xnode == previous)
					{
						allowBefore = allowAfter;
					}
					if (xnode == this.content)
					{
						return;
					}
				}
				throw new InvalidOperationException(Res.GetString("InvalidOperation_DocumentStructure"));
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000856C File Offset: 0x0000676C
		internal override void ValidateString(string s)
		{
			if (!XDocument.IsWhitespace(s))
			{
				throw new ArgumentException(Res.GetString("Argument_AddNonWhitespace"));
			}
		}

		// Token: 0x040000A1 RID: 161
		private XDeclaration declaration;
	}
}
