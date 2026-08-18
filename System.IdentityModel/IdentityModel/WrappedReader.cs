using System;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000B6 RID: 182
	internal sealed class WrappedReader : DelegatingXmlDictionaryReader, IXmlLineInfo
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000149FC File Offset: 0x00012BFC
		public WrappedReader(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InnerReaderMustBeAtElement")));
			}
			this.xmlTokens = new XmlTokenStream(32);
			base.InitializeInnerReader(reader);
			this.Record();
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00014A60 File Offset: 0x00012C60
		public int LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = base.InnerReader as IXmlLineInfo;
				if (xmlLineInfo == null)
				{
					return 1;
				}
				return xmlLineInfo.LineNumber;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00014A84 File Offset: 0x00012C84
		public int LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = base.InnerReader as IXmlLineInfo;
				if (xmlLineInfo == null)
				{
					return 1;
				}
				return xmlLineInfo.LinePosition;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public XmlTokenStream XmlTokens
		{
			get
			{
				return this.xmlTokens;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00014AB0 File Offset: 0x00012CB0
		public override void Close()
		{
			this.OnEndOfContent();
			base.InnerReader.Close();
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00014AC4 File Offset: 0x00012CC4
		public bool HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = base.InnerReader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00014AE8 File Offset: 0x00012CE8
		public override void MoveToAttribute(int index)
		{
			this.OnEndOfContent();
			base.InnerReader.MoveToAttribute(index);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00014AFC File Offset: 0x00012CFC
		public override bool MoveToAttribute(string name)
		{
			this.OnEndOfContent();
			return base.InnerReader.MoveToAttribute(name);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00014B10 File Offset: 0x00012D10
		public override bool MoveToAttribute(string name, string ns)
		{
			this.OnEndOfContent();
			return base.InnerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00014B25 File Offset: 0x00012D25
		public override bool MoveToElement()
		{
			this.OnEndOfContent();
			return base.MoveToElement();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00014B33 File Offset: 0x00012D33
		public override bool MoveToFirstAttribute()
		{
			this.OnEndOfContent();
			return base.MoveToFirstAttribute();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00014B41 File Offset: 0x00012D41
		public override bool MoveToNextAttribute()
		{
			this.OnEndOfContent();
			return base.MoveToNextAttribute();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00014B4F File Offset: 0x00012D4F
		private void OnEndOfContent()
		{
			if (this.contentReader != null)
			{
				this.contentReader.Close();
				this.contentReader = null;
			}
			if (this.contentStream != null)
			{
				this.contentStream.Close();
				this.contentStream = null;
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00014B85 File Offset: 0x00012D85
		public override bool Read()
		{
			this.OnEndOfContent();
			if (!base.Read())
			{
				return false;
			}
			if (!this.recordDone)
			{
				this.Record();
			}
			return true;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00014BA8 File Offset: 0x00012DA8
		private int ReadBinaryContent(byte[] buffer, int offset, int count, bool isBase64)
		{
			CryptoHelper.ValidateBufferBounds(buffer, offset, count);
			if (this.contentStream == null)
			{
				string text;
				if (this.NodeType == XmlNodeType.Attribute)
				{
					text = this.Value;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(1000);
					while (this.NodeType != XmlNodeType.Element && this.NodeType != XmlNodeType.EndElement)
					{
						XmlNodeType nodeType = this.NodeType;
						if (nodeType != XmlNodeType.Text)
						{
							if (nodeType != XmlNodeType.Whitespace)
							{
							}
						}
						else
						{
							stringBuilder.Append(this.Value);
						}
						this.Read();
					}
					text = stringBuilder.ToString();
				}
				byte[] buffer2 = isBase64 ? Convert.FromBase64String(text) : SoapHexBinary.Parse(text).Value;
				this.contentStream = new MemoryStream(buffer2);
			}
			int num = this.contentStream.Read(buffer, offset, count);
			if (num == 0)
			{
				this.contentStream.Close();
				this.contentStream = null;
			}
			return num;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00014C73 File Offset: 0x00012E73
		public override int ReadContentAsBase64(byte[] buffer, int offset, int count)
		{
			return this.ReadBinaryContent(buffer, offset, count, true);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00014C7F File Offset: 0x00012E7F
		public override int ReadContentAsBinHex(byte[] buffer, int offset, int count)
		{
			return this.ReadBinaryContent(buffer, offset, count, false);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00014C8B File Offset: 0x00012E8B
		public override int ReadValueChunk(char[] chars, int offset, int count)
		{
			if (this.contentReader == null)
			{
				this.contentReader = new StringReader(this.Value);
			}
			return this.contentReader.Read(chars, offset, count);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00014CB4 File Offset: 0x00012EB4
		private void Record()
		{
			switch (this.NodeType)
			{
			case XmlNodeType.Element:
			{
				bool isEmptyElement = base.InnerReader.IsEmptyElement;
				this.xmlTokens.AddElement(base.InnerReader.Prefix, base.InnerReader.LocalName, base.InnerReader.NamespaceURI, isEmptyElement);
				if (base.InnerReader.MoveToFirstAttribute())
				{
					do
					{
						this.xmlTokens.AddAttribute(base.InnerReader.Prefix, base.InnerReader.LocalName, base.InnerReader.NamespaceURI, base.InnerReader.Value);
					}
					while (base.InnerReader.MoveToNextAttribute());
					base.InnerReader.MoveToElement();
				}
				if (!isEmptyElement)
				{
					this.depth++;
					return;
				}
				if (this.depth == 0)
				{
					this.recordDone = true;
					return;
				}
				return;
			}
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.EndEntity:
				this.xmlTokens.Add(this.NodeType, this.Value);
				return;
			case XmlNodeType.DocumentType:
			case XmlNodeType.XmlDeclaration:
				return;
			case XmlNodeType.EndElement:
			{
				this.xmlTokens.Add(this.NodeType, this.Value);
				int num = this.depth - 1;
				this.depth = num;
				if (num == 0)
				{
					this.recordDone = true;
					return;
				}
				return;
			}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnsupportedNodeTypeInReader", new object[]
			{
				base.InnerReader.NodeType,
				base.InnerReader.Name
			})));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00014E60 File Offset: 0x00013060
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				if (this.contentReader != null)
				{
					this.contentReader.Dispose();
					this.contentReader = null;
				}
				if (this.contentStream != null)
				{
					this.contentStream.Dispose();
					this.contentStream = null;
				}
			}
			this.disposed = true;
		}

		// Token: 0x040004D1 RID: 1233
		private XmlTokenStream xmlTokens;

		// Token: 0x040004D2 RID: 1234
		private MemoryStream contentStream;

		// Token: 0x040004D3 RID: 1235
		private TextReader contentReader;

		// Token: 0x040004D4 RID: 1236
		private bool recordDone;

		// Token: 0x040004D5 RID: 1237
		private int depth;

		// Token: 0x040004D6 RID: 1238
		private bool disposed;
	}
}
