using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000052 RID: 82
	internal class XmlEncodedRawTextWriter : XmlRawWriter
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000A18C File Offset: 0x0000918C
		protected XmlEncodedRawTextWriter(XmlWriterSettings settings, bool closeOutput)
		{
			this.newLineHandling = settings.NewLineHandling;
			this.omitXmlDeclaration = settings.OmitXmlDeclaration;
			this.newLineChars = settings.NewLineChars;
			this.standalone = settings.Standalone;
			this.outputMethod = settings.OutputMethod;
			this.checkCharacters = settings.CheckCharacters;
			this.mergeCDataSections = settings.MergeCDataSections;
			this.closeOutput = closeOutput;
			if (this.checkCharacters && this.newLineHandling == NewLineHandling.Replace)
			{
				this.ValidateContentChars(this.newLineChars, "NewLineChars", false);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A240 File Offset: 0x00009240
		public XmlEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings) : this(settings, settings.CloseOutput)
		{
			this.writer = writer;
			this.encoding = writer.Encoding;
			this.bufChars = new char[6176];
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A298 File Offset: 0x00009298
		public XmlEncodedRawTextWriter(Stream stream, Encoding encoding, XmlWriterSettings settings, bool closeOutput) : this(settings, closeOutput)
		{
			this.stream = stream;
			this.encoding = encoding;
			this.bufChars = new char[6176];
			this.bufBytes = new byte[this.bufChars.Length];
			this.bufBytesUsed = 0;
			this.trackTextContent = true;
			this.inTextContent = false;
			this.lastMarkPos = 0;
			this.textContentMarks = new int[64];
			this.textContentMarks[0] = 1;
			this.charEntityFallback = new CharEntityEncoderFallback();
			encoding = (Encoding)encoding.Clone();
			encoding.EncoderFallback = this.charEntityFallback;
			this.encoding = encoding;
			this.encoder = encoding.GetEncoder();
			if (!stream.CanSeek || stream.Position == 0L)
			{
				byte[] preamble = encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					this.stream.Write(preamble, 0, preamble.Length);
				}
			}
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000A394 File Offset: 0x00009394
		public override XmlWriterSettings Settings
		{
			get
			{
				return new XmlWriterSettings
				{
					Encoding = this.encoding,
					OmitXmlDeclaration = this.omitXmlDeclaration,
					NewLineHandling = this.newLineHandling,
					NewLineChars = this.newLineChars,
					CloseOutput = this.closeOutput,
					ConformanceLevel = ConformanceLevel.Auto,
					AutoXmlDeclaration = this.autoXmlDeclaration,
					Standalone = this.standalone,
					OutputMethod = this.outputMethod,
					CheckCharacters = this.checkCharacters,
					ReadOnly = true
				};
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A424 File Offset: 0x00009424
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					this.ChangeTextContentMark(false);
				}
				this.RawText("<?xml version=\"");
				this.RawText("1.0");
				if (this.encoding != null)
				{
					this.RawText("\" encoding=\"");
					this.RawText((this.encoding.CodePage == 1201) ? "UTF-16BE" : this.encoding.WebName);
				}
				if (standalone != XmlStandalone.Omit)
				{
					this.RawText("\" standalone=\"");
					this.RawText((standalone == XmlStandalone.Yes) ? "yes" : "no");
				}
				this.RawText("\"?>");
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A4E0 File Offset: 0x000094E0
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				this.WriteProcessingInstruction("xml", xmldecl);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A500 File Offset: 0x00009500
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.RawText("<!DOCTYPE ");
			this.RawText(name);
			if (pubid != null)
			{
				this.RawText(" PUBLIC \"");
				this.RawText(pubid);
				this.RawText("\" \"");
				if (sysid != null)
				{
					this.RawText(sysid);
				}
				this.bufChars[this.bufPos++] = '"';
			}
			else if (sysid != null)
			{
				this.RawText(" SYSTEM \"");
				this.RawText(sysid);
				this.bufChars[this.bufPos++] = '"';
			}
			else
			{
				this.bufChars[this.bufPos++] = ' ';
			}
			if (subset != null)
			{
				this.bufChars[this.bufPos++] = '[';
				this.RawText(subset);
				this.bufChars[this.bufPos++] = ']';
			}
			this.bufChars[this.bufPos++] = '>';
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A628 File Offset: 0x00009628
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.bufChars[this.bufPos++] = '<';
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				this.bufChars[this.bufPos++] = ':';
			}
			this.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A6A8 File Offset: 0x000096A8
		internal override void StartElementContent()
		{
			this.bufChars[this.bufPos++] = '>';
			this.contentPos = this.bufPos;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A6DC File Offset: 0x000096DC
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.contentPos != this.bufPos)
			{
				this.bufChars[this.bufPos++] = '<';
				this.bufChars[this.bufPos++] = '/';
				if (prefix != null && prefix.Length != 0)
				{
					this.RawText(prefix);
					this.bufChars[this.bufPos++] = ':';
				}
				this.RawText(localName);
				this.bufChars[this.bufPos++] = '>';
				return;
			}
			this.bufPos--;
			this.bufChars[this.bufPos++] = ' ';
			this.bufChars[this.bufPos++] = '/';
			this.bufChars[this.bufPos++] = '>';
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A7F8 File Offset: 0x000097F8
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.bufChars[this.bufPos++] = '<';
			this.bufChars[this.bufPos++] = '/';
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				this.bufChars[this.bufPos++] = ':';
			}
			this.RawText(localName);
			this.bufChars[this.bufPos++] = '>';
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A8A0 File Offset: 0x000098A0
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.attrEndPos == this.bufPos)
			{
				this.bufChars[this.bufPos++] = ' ';
			}
			if (prefix != null && prefix.Length > 0)
			{
				this.RawText(prefix);
				this.bufChars[this.bufPos++] = ':';
			}
			this.RawText(localName);
			this.bufChars[this.bufPos++] = '=';
			this.bufChars[this.bufPos++] = '"';
			this.inAttributeValue = true;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A95C File Offset: 0x0000995C
		public override void WriteEndAttribute()
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.bufChars[this.bufPos++] = '"';
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A9B0 File Offset: 0x000099B0
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (prefix.Length == 0)
			{
				this.RawText(" xmlns=\"");
			}
			else
			{
				this.RawText(" xmlns:");
				this.RawText(prefix);
				this.bufChars[this.bufPos++] = '=';
				this.bufChars[this.bufPos++] = '"';
			}
			this.inAttributeValue = true;
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			this.WriteString(namespaceName);
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.inAttributeValue = false;
			this.bufChars[this.bufPos++] = '"';
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000AA98 File Offset: 0x00009A98
		public override void WriteCData(string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.mergeCDataSections && this.bufPos == this.cdataPos)
			{
				this.bufPos -= 3;
			}
			else
			{
				this.bufChars[this.bufPos++] = '<';
				this.bufChars[this.bufPos++] = '!';
				this.bufChars[this.bufPos++] = '[';
				this.bufChars[this.bufPos++] = 'C';
				this.bufChars[this.bufPos++] = 'D';
				this.bufChars[this.bufPos++] = 'A';
				this.bufChars[this.bufPos++] = 'T';
				this.bufChars[this.bufPos++] = 'A';
				this.bufChars[this.bufPos++] = '[';
			}
			this.WriteCDataSection(text);
			this.bufChars[this.bufPos++] = ']';
			this.bufChars[this.bufPos++] = ']';
			this.bufChars[this.bufPos++] = '>';
			this.textPos = this.bufPos;
			this.cdataPos = this.bufPos;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000AC4C File Offset: 0x00009C4C
		public override void WriteComment(string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.bufChars[this.bufPos++] = '<';
			this.bufChars[this.bufPos++] = '!';
			this.bufChars[this.bufPos++] = '-';
			this.bufChars[this.bufPos++] = '-';
			this.WriteCommentOrPi(text, 45);
			this.bufChars[this.bufPos++] = '-';
			this.bufChars[this.bufPos++] = '-';
			this.bufChars[this.bufPos++] = '>';
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000AD38 File Offset: 0x00009D38
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.bufChars[this.bufPos++] = '<';
			this.bufChars[this.bufPos++] = '?';
			this.RawText(name);
			if (text.Length > 0)
			{
				this.bufChars[this.bufPos++] = ' ';
				this.WriteCommentOrPi(text, 63);
			}
			this.bufChars[this.bufPos++] = '?';
			this.bufChars[this.bufPos++] = '>';
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000ADFC File Offset: 0x00009DFC
		public override void WriteEntityRef(string name)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.bufChars[this.bufPos++] = '&';
			this.RawText(name);
			this.bufChars[this.bufPos++] = ';';
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AE7C File Offset: 0x00009E7C
		public override void WriteCharEntity(char ch)
		{
			int num = (int)ch;
			string s = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch);
			}
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.bufChars[this.bufPos++] = '&';
			this.bufChars[this.bufPos++] = '#';
			this.bufChars[this.bufPos++] = 'x';
			this.RawText(s);
			this.bufChars[this.bufPos++] = ';';
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AF64 File Offset: 0x00009F64
		public unsafe override void WriteWhitespace(string ws)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (char* ptr = ws)
			{
				char* pSrcEnd = ptr + ws.Length;
				if (this.inAttributeValue)
				{
					this.WriteAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000AFC0 File Offset: 0x00009FC0
		public unsafe override void WriteString(string text)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			fixed (char* ptr = text)
			{
				char* pSrcEnd = ptr + text.Length;
				if (this.inAttributeValue)
				{
					this.WriteAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B01C File Offset: 0x0000A01C
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num = (int)(lowChar - '\udc00') | (int)((int)(highChar - '\ud800') << 10) + 65536;
			this.bufChars[this.bufPos++] = '&';
			this.bufChars[this.bufPos++] = '#';
			this.bufChars[this.bufPos++] = 'x';
			this.RawText(num.ToString("X", NumberFormatInfo.InvariantInfo));
			this.bufChars[this.bufPos++] = ';';
			this.textPos = this.bufPos;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B0E8 File Offset: 0x0000A0E8
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			fixed (char* ptr = &buffer[index])
			{
				if (this.inAttributeValue)
				{
					this.WriteAttributeTextBlock(ptr, ptr + count);
				}
				else
				{
					this.WriteElementTextBlock(ptr, ptr + count);
				}
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B140 File Offset: 0x0000A140
		public unsafe override void WriteRaw(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (char* ptr = &buffer[index])
			{
				this.WriteRawWithCharChecking(ptr, ptr + count);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B18C File Offset: 0x0000A18C
		public unsafe override void WriteRaw(string data)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (char* ptr = data)
			{
				this.WriteRawWithCharChecking(ptr, ptr + data.Length);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B1E0 File Offset: 0x0000A1E0
		public override void Close()
		{
			this.FlushBuffer();
			this.FlushEncoder();
			this.writeToNull = true;
			if (this.stream != null)
			{
				this.stream.Flush();
				if (this.closeOutput)
				{
					this.stream.Close();
				}
				this.stream = null;
				return;
			}
			if (this.writer != null)
			{
				this.writer.Flush();
				if (this.closeOutput)
				{
					this.writer.Close();
				}
				this.writer = null;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000B25B File Offset: 0x0000A25B
		public override void Flush()
		{
			this.FlushBuffer();
			this.FlushEncoder();
			if (this.stream != null)
			{
				this.stream.Flush();
				return;
			}
			if (this.writer != null)
			{
				this.writer.Flush();
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B290 File Offset: 0x0000A290
		protected virtual void FlushBuffer()
		{
			try
			{
				if (!this.writeToNull)
				{
					if (this.stream != null)
					{
						if (this.trackTextContent)
						{
							this.charEntityFallback.Reset(this.textContentMarks, this.lastMarkPos);
							if ((this.lastMarkPos & 1) != 0)
							{
								this.textContentMarks[1] = 1;
								this.lastMarkPos = 1;
							}
							else
							{
								this.lastMarkPos = 0;
							}
						}
						this.EncodeChars(1, this.bufPos, true);
					}
					else
					{
						this.writer.Write(this.bufChars, 1, this.bufPos - 1);
					}
				}
			}
			catch
			{
				this.writeToNull = true;
				throw;
			}
			finally
			{
				this.bufChars[0] = this.bufChars[this.bufPos - 1];
				this.textPos = ((this.textPos == this.bufPos) ? 1 : 0);
				this.attrEndPos = ((this.attrEndPos == this.bufPos) ? 1 : 0);
				this.contentPos = 0;
				this.cdataPos = 0;
				this.bufPos = 1;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B3A0 File Offset: 0x0000A3A0
		private void EncodeChars(int startOffset, int endOffset, bool writeAllToStream)
		{
			while (startOffset < endOffset)
			{
				if (this.charEntityFallback != null)
				{
					this.charEntityFallback.StartOffset = startOffset;
				}
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, startOffset, endOffset - startOffset, this.bufBytes, this.bufBytesUsed, this.bufBytes.Length - this.bufBytesUsed, false, out num, out num2, out flag);
				startOffset += num;
				this.bufBytesUsed += num2;
				if (this.bufBytesUsed >= this.bufBytes.Length - 16)
				{
					this.stream.Write(this.bufBytes, 0, this.bufBytesUsed);
					this.bufBytesUsed = 0;
				}
			}
			if (writeAllToStream && this.bufBytesUsed > 0)
			{
				this.stream.Write(this.bufBytes, 0, this.bufBytesUsed);
				this.bufBytesUsed = 0;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B474 File Offset: 0x0000A474
		private void FlushEncoder()
		{
			if (this.stream != null)
			{
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, 1, 0, this.bufBytes, 0, this.bufBytes.Length, true, out num, out num2, out flag);
				if (num2 != 0)
				{
					this.stream.Write(this.bufBytes, 0, num2);
				}
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B4C8 File Offset: 0x0000A4C8
		protected unsafe void WriteAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
			fixed (char* ptr = this.bufChars)
			{
				char* ptr2 = ptr + this.bufPos;
				int num = 0;
				for (;;)
				{
					char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
					if (ptr3 != ptr + this.bufLen)
					{
						ptr3 = ptr + this.bufLen;
					}
					while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
					{
						*ptr2 = (char)num;
						ptr2++;
						pSrc++;
					}
					if (pSrc >= pSrcEnd)
					{
						break;
					}
					if (ptr2 >= ptr3)
					{
						this.bufPos = (int)((long)(ptr2 - ptr));
						this.FlushBuffer();
						ptr2 = ptr + 1;
					}
					else
					{
						int num2 = num;
						if (num2 <= 34)
						{
							switch (num2)
							{
							case 9:
								if (this.newLineHandling == NewLineHandling.None)
								{
									*ptr2 = (char)num;
									ptr2++;
									goto IL_205;
								}
								ptr2 = XmlEncodedRawTextWriter.TabEntity(ptr2);
								goto IL_205;
							case 10:
								if (this.newLineHandling == NewLineHandling.None)
								{
									*ptr2 = (char)num;
									ptr2++;
									goto IL_205;
								}
								ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
								goto IL_205;
							case 11:
							case 12:
								break;
							case 13:
								if (this.newLineHandling == NewLineHandling.None)
								{
									*ptr2 = (char)num;
									ptr2++;
									goto IL_205;
								}
								ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
								goto IL_205;
							default:
								if (num2 == 34)
								{
									ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
									goto IL_205;
								}
								break;
							}
						}
						else
						{
							switch (num2)
							{
							case 38:
								ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
								goto IL_205;
							case 39:
								*ptr2 = (char)num;
								ptr2++;
								goto IL_205;
							default:
								switch (num2)
								{
								case 60:
									ptr2 = XmlEncodedRawTextWriter.LtEntity(ptr2);
									goto IL_205;
								case 62:
									ptr2 = XmlEncodedRawTextWriter.GtEntity(ptr2);
									goto IL_205;
								}
								break;
							}
						}
						if (XmlEncodedRawTextWriter.InRange(num, 55296, 57343))
						{
							ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
							pSrc += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr2 = this.InvalidXmlChar(num, ptr2, true);
							pSrc++;
							continue;
						}
						*ptr2 = (char)num;
						ptr2++;
						pSrc++;
						continue;
						IL_205:
						pSrc++;
					}
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B6F8 File Offset: 0x0000A6F8
		protected unsafe void WriteElementTextBlock(char* pSrc, char* pSrcEnd)
		{
			fixed (char* ptr = this.bufChars)
			{
				char* ptr2 = ptr + this.bufPos;
				int num = 0;
				for (;;)
				{
					char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
					if (ptr3 != ptr + this.bufLen)
					{
						ptr3 = ptr + this.bufLen;
					}
					while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
					{
						*ptr2 = (char)num;
						ptr2++;
						pSrc++;
					}
					if (pSrc >= pSrcEnd)
					{
						break;
					}
					if (ptr2 < ptr3)
					{
						int num2 = num;
						if (num2 <= 34)
						{
							switch (num2)
							{
							case 9:
								goto IL_12F;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr2 = this.WriteNewLine(ptr2);
									goto IL_209;
								}
								*ptr2 = (char)num;
								ptr2++;
								goto IL_209;
							case 11:
							case 12:
								break;
							case 13:
								switch (this.newLineHandling)
								{
								case NewLineHandling.Replace:
									if (pSrc[1] == '\n')
									{
										pSrc++;
									}
									ptr2 = this.WriteNewLine(ptr2);
									goto IL_209;
								case NewLineHandling.Entitize:
									ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
									goto IL_209;
								case NewLineHandling.None:
									*ptr2 = (char)num;
									ptr2++;
									goto IL_209;
								default:
									goto IL_209;
								}
								break;
							default:
								if (num2 == 34)
								{
									goto IL_12F;
								}
								break;
							}
						}
						else
						{
							switch (num2)
							{
							case 38:
								ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
								goto IL_209;
							case 39:
								goto IL_12F;
							default:
								switch (num2)
								{
								case 60:
									ptr2 = XmlEncodedRawTextWriter.LtEntity(ptr2);
									goto IL_209;
								case 62:
									ptr2 = XmlEncodedRawTextWriter.GtEntity(ptr2);
									goto IL_209;
								}
								break;
							}
						}
						if (XmlEncodedRawTextWriter.InRange(num, 55296, 57343))
						{
							ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
							pSrc += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr2 = this.InvalidXmlChar(num, ptr2, true);
							pSrc++;
							continue;
						}
						*ptr2 = (char)num;
						ptr2++;
						pSrc++;
						continue;
						IL_209:
						pSrc++;
						continue;
						IL_12F:
						*ptr2 = (char)num;
						ptr2++;
						goto IL_209;
					}
					this.bufPos = (int)((long)(ptr2 - ptr));
					this.FlushBuffer();
					ptr2 = ptr + 1;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.textPos = this.bufPos;
				this.contentPos = 0;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B940 File Offset: 0x0000A940
		protected unsafe void RawText(string s)
		{
			fixed (char* ptr = s)
			{
				this.RawText(ptr, ptr + s.Length);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000B970 File Offset: 0x0000A970
		protected unsafe void RawText(char* pSrcBegin, char* pSrcEnd)
		{
			fixed (char* ptr = this.bufChars)
			{
				char* ptr2 = ptr + this.bufPos;
				char* ptr3 = pSrcBegin;
				int num = 0;
				for (;;)
				{
					char* ptr4 = ptr2 + (long)(pSrcEnd - ptr3) * 2L / 2L;
					if (ptr4 != ptr + this.bufLen)
					{
						ptr4 = ptr + this.bufLen;
					}
					while (ptr2 < ptr4 && (num = (int)(*ptr3)) < 55296)
					{
						ptr3++;
						*ptr2 = (char)num;
						ptr2++;
					}
					if (ptr3 >= pSrcEnd)
					{
						break;
					}
					if (ptr2 >= ptr4)
					{
						this.bufPos = (int)((long)(ptr2 - ptr));
						this.FlushBuffer();
						ptr2 = ptr + 1;
					}
					else if (XmlEncodedRawTextWriter.InRange(num, 55296, 57343))
					{
						ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
						ptr3 += 2;
					}
					else if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, false);
						ptr3++;
					}
					else
					{
						*ptr2 = (char)num;
						ptr2++;
						ptr3++;
					}
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000BA88 File Offset: 0x0000AA88
		protected unsafe void WriteRawWithCharChecking(char* pSrcBegin, char* pSrcEnd)
		{
			fixed (char* ptr = this.bufChars)
			{
				char* ptr2 = pSrcBegin;
				char* ptr3 = ptr + this.bufPos;
				int num = 0;
				for (;;)
				{
					char* ptr4 = ptr3 + (long)(pSrcEnd - ptr2) * 2L / 2L;
					if (ptr4 != ptr + this.bufLen)
					{
						ptr4 = ptr + this.bufLen;
					}
					while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0)
					{
						*ptr3 = (char)num;
						ptr3++;
						ptr2++;
					}
					if (ptr2 >= pSrcEnd)
					{
						break;
					}
					if (ptr3 < ptr4)
					{
						int num2 = num;
						if (num2 <= 38)
						{
							switch (num2)
							{
							case 9:
								goto IL_EA;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr3 = this.WriteNewLine(ptr3);
									goto IL_19C;
								}
								*ptr3 = (char)num;
								ptr3++;
								goto IL_19C;
							case 11:
							case 12:
								break;
							case 13:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									if (ptr2[1] == '\n')
									{
										ptr2++;
									}
									ptr3 = this.WriteNewLine(ptr3);
									goto IL_19C;
								}
								*ptr3 = (char)num;
								ptr3++;
								goto IL_19C;
							default:
								if (num2 == 38)
								{
									goto IL_EA;
								}
								break;
							}
						}
						else if (num2 == 60 || num2 == 93)
						{
							goto IL_EA;
						}
						if (XmlEncodedRawTextWriter.InRange(num, 55296, 57343))
						{
							ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
							ptr2 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr3 = this.InvalidXmlChar(num, ptr3, false);
							ptr2++;
							continue;
						}
						*ptr3 = (char)num;
						ptr3++;
						ptr2++;
						continue;
						IL_19C:
						ptr2++;
						continue;
						IL_EA:
						*ptr3 = (char)num;
						ptr3++;
						goto IL_19C;
					}
					this.bufPos = (int)((long)(ptr3 - ptr));
					this.FlushBuffer();
					ptr3 = ptr + 1;
				}
				this.bufPos = (int)((long)(ptr3 - ptr));
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000BC4C File Offset: 0x0000AC4C
		protected unsafe void WriteCommentOrPi(string text, int stopChar)
		{
			fixed (char* ptr = text)
			{
				fixed (char* ptr2 = this.bufChars)
				{
					char* ptr3 = ptr;
					char* ptr4 = ptr + text.Length;
					char* ptr5 = ptr2 + this.bufPos;
					int num = 0;
					for (;;)
					{
						char* ptr6 = ptr5 + (long)(ptr4 - ptr3) * 2L / 2L;
						if (ptr6 != ptr2 + this.bufLen)
						{
							ptr6 = ptr2 + this.bufLen;
						}
						while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 64) != 0 && num != stopChar)
						{
							*ptr5 = (char)num;
							ptr5++;
							ptr3++;
						}
						if (ptr3 >= ptr4)
						{
							break;
						}
						if (ptr5 < ptr6)
						{
							int num2 = num;
							if (num2 <= 45)
							{
								switch (num2)
								{
								case 9:
									goto IL_216;
								case 10:
									if (this.newLineHandling == NewLineHandling.Replace)
									{
										ptr5 = this.WriteNewLine(ptr5);
										goto IL_28A;
									}
									*ptr5 = (char)num;
									ptr5++;
									goto IL_28A;
								case 11:
								case 12:
									break;
								case 13:
									if (this.newLineHandling == NewLineHandling.Replace)
									{
										if (ptr3[1] == '\n')
										{
											ptr3++;
										}
										ptr5 = this.WriteNewLine(ptr5);
										goto IL_28A;
									}
									*ptr5 = (char)num;
									ptr5++;
									goto IL_28A;
								default:
									if (num2 == 38)
									{
										goto IL_216;
									}
									if (num2 == 45)
									{
										*ptr5 = '-';
										ptr5++;
										if (num == stopChar && (ptr3 + 1 == ptr4 || ptr3[1] == '-'))
										{
											*ptr5 = ' ';
											ptr5++;
											goto IL_28A;
										}
										goto IL_28A;
									}
									break;
								}
							}
							else
							{
								if (num2 == 60)
								{
									goto IL_216;
								}
								if (num2 != 63)
								{
									if (num2 == 93)
									{
										*ptr5 = ']';
										ptr5++;
										goto IL_28A;
									}
								}
								else
								{
									*ptr5 = '?';
									ptr5++;
									if (num == stopChar && ptr3 + 1 < ptr4 && ptr3[1] == '>')
									{
										*ptr5 = ' ';
										ptr5++;
										goto IL_28A;
									}
									goto IL_28A;
								}
							}
							if (XmlEncodedRawTextWriter.InRange(num, 55296, 57343))
							{
								ptr5 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
								ptr3 += 2;
								continue;
							}
							if (num <= 127 || num >= 65534)
							{
								ptr5 = this.InvalidXmlChar(num, ptr5, false);
								ptr3++;
								continue;
							}
							*ptr5 = (char)num;
							ptr5++;
							ptr3++;
							continue;
							IL_28A:
							ptr3++;
							continue;
							IL_216:
							*ptr5 = (char)num;
							ptr5++;
							goto IL_28A;
						}
						this.bufPos = (int)((long)(ptr5 - ptr2));
						this.FlushBuffer();
						ptr5 = ptr2 + 1;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
				}
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000BF04 File Offset: 0x0000AF04
		protected unsafe void WriteCDataSection(string text)
		{
			fixed (char* ptr = text)
			{
				fixed (char* ptr2 = this.bufChars)
				{
					char* ptr3 = ptr;
					char* ptr4 = ptr + text.Length;
					char* ptr5 = ptr2 + this.bufPos;
					int num = 0;
					for (;;)
					{
						char* ptr6 = ptr5 + (long)(ptr4 - ptr3) * 2L / 2L;
						if (ptr6 != ptr2 + this.bufLen)
						{
							ptr6 = ptr2 + this.bufLen;
						}
						while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 128) != 0 && num != 93)
						{
							*ptr5 = (char)num;
							ptr5++;
							ptr3++;
						}
						if (ptr3 >= ptr4)
						{
							break;
						}
						if (ptr5 < ptr6)
						{
							int num2 = num;
							if (num2 <= 34)
							{
								switch (num2)
								{
								case 9:
									goto IL_20B;
								case 10:
									if (this.newLineHandling == NewLineHandling.Replace)
									{
										ptr5 = this.WriteNewLine(ptr5);
										goto IL_27F;
									}
									*ptr5 = (char)num;
									ptr5++;
									goto IL_27F;
								case 11:
								case 12:
									break;
								case 13:
									if (this.newLineHandling == NewLineHandling.Replace)
									{
										if (ptr3[1] == '\n')
										{
											ptr3++;
										}
										ptr5 = this.WriteNewLine(ptr5);
										goto IL_27F;
									}
									*ptr5 = (char)num;
									ptr5++;
									goto IL_27F;
								default:
									if (num2 == 34)
									{
										goto IL_20B;
									}
									break;
								}
							}
							else
							{
								switch (num2)
								{
								case 38:
								case 39:
									goto IL_20B;
								default:
									switch (num2)
									{
									case 60:
										goto IL_20B;
									case 61:
										break;
									case 62:
										if (this.hadDoubleBracket && ptr5[-1] == ']')
										{
											ptr5 = XmlEncodedRawTextWriter.RawEndCData(ptr5);
											ptr5 = XmlEncodedRawTextWriter.RawStartCData(ptr5);
										}
										*ptr5 = '>';
										ptr5++;
										goto IL_27F;
									default:
										if (num2 == 93)
										{
											if (ptr5[-1] == ']')
											{
												this.hadDoubleBracket = true;
											}
											else
											{
												this.hadDoubleBracket = false;
											}
											*ptr5 = ']';
											ptr5++;
											goto IL_27F;
										}
										break;
									}
									break;
								}
							}
							if (XmlEncodedRawTextWriter.InRange(num, 55296, 57343))
							{
								ptr5 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
								ptr3 += 2;
								continue;
							}
							if (num <= 127 || num >= 65534)
							{
								ptr5 = this.InvalidXmlChar(num, ptr5, false);
								ptr3++;
								continue;
							}
							*ptr5 = (char)num;
							ptr5++;
							ptr3++;
							continue;
							IL_27F:
							ptr3++;
							continue;
							IL_20B:
							*ptr5 = (char)num;
							ptr5++;
							goto IL_27F;
						}
						this.bufPos = (int)((long)(ptr5 - ptr2));
						this.FlushBuffer();
						ptr5 = ptr2 + 1;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000C1B0 File Offset: 0x0000B1B0
		private unsafe static char* EncodeSurrogate(char* pSrc, char* pSrcEnd, char* pDst)
		{
			int num = (int)(*pSrc);
			if (num > 56319)
			{
				throw XmlConvert.CreateInvalidHighSurrogateCharException((char)num);
			}
			if (pSrc + 1 >= pSrcEnd)
			{
				throw new ArgumentException(Res.GetString("Xml_InvalidSurrogateMissingLowChar"));
			}
			int num2 = (int)pSrc[1];
			if (num2 >= 56320)
			{
				*pDst = (char)num;
				pDst[1] = (char)num2;
				pDst += 2;
				return pDst;
			}
			throw XmlConvert.CreateInvalidSurrogatePairException((char)num2, (char)num);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000C211 File Offset: 0x0000B211
		private unsafe char* InvalidXmlChar(int ch, char* pDst, bool entitize)
		{
			if (this.checkCharacters)
			{
				throw XmlConvert.CreateInvalidCharException((char)ch);
			}
			if (entitize)
			{
				return XmlEncodedRawTextWriter.CharEntity(pDst, (char)ch);
			}
			*pDst = (char)ch;
			pDst++;
			return pDst;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000C23C File Offset: 0x0000B23C
		internal unsafe void EncodeChar(ref char* pSrc, char* pSrcEnd, ref char* pDst)
		{
			int num = (int)(*pSrc);
			if (XmlEncodedRawTextWriter.InRange(num, 55296, 57343))
			{
				pDst = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)4;
				return;
			}
			if (num <= 127 || num >= 65534)
			{
				pDst = this.InvalidXmlChar(num, pDst, false);
				pSrc += (IntPtr)2;
				return;
			}
			*pDst = (short)((ushort)num);
			pDst += (IntPtr)2;
			pSrc += (IntPtr)2;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C2A8 File Offset: 0x0000B2A8
		protected void ChangeTextContentMark(bool value)
		{
			this.inTextContent = value;
			if (this.lastMarkPos + 1 == this.textContentMarks.Length)
			{
				this.GrowTextContentMarks();
			}
			this.textContentMarks[++this.lastMarkPos] = this.bufPos;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C2F4 File Offset: 0x0000B2F4
		private void GrowTextContentMarks()
		{
			int[] destinationArray = new int[this.textContentMarks.Length * 2];
			Array.Copy(this.textContentMarks, destinationArray, this.textContentMarks.Length);
			this.textContentMarks = destinationArray;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C32C File Offset: 0x0000B32C
		protected unsafe char* WriteNewLine(char* pDst)
		{
			fixed (char* ptr = this.bufChars)
			{
				this.bufPos = (int)((long)(pDst - ptr));
				this.RawText(this.newLineChars);
				return ptr + this.bufPos;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C37F File Offset: 0x0000B37F
		protected unsafe static char* LtEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'l';
			pDst[2] = 't';
			pDst[3] = ';';
			return pDst + 4;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000C39E File Offset: 0x0000B39E
		protected unsafe static char* GtEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'g';
			pDst[2] = 't';
			pDst[3] = ';';
			return pDst + 4;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C3BD File Offset: 0x0000B3BD
		protected unsafe static char* AmpEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'a';
			pDst[2] = 'm';
			pDst[3] = 'p';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000C3E4 File Offset: 0x0000B3E4
		protected unsafe static char* QuoteEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'q';
			pDst[2] = 'u';
			pDst[3] = 'o';
			pDst[4] = 't';
			pDst[5] = ';';
			return pDst + 6;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000C413 File Offset: 0x0000B413
		protected unsafe static char* TabEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = '9';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000C43A File Offset: 0x0000B43A
		protected unsafe static char* LineFeedEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = 'A';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C461 File Offset: 0x0000B461
		protected unsafe static char* CarriageReturnEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = 'D';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C488 File Offset: 0x0000B488
		private unsafe static char* CharEntity(char* pDst, char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst += 3;
			fixed (char* ptr = text)
			{
				char* ptr2 = ptr;
				while ((*(pDst++) = *(ptr2++)) != '\0')
				{
				}
			}
			pDst[-1] = ';';
			return pDst;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C4F8 File Offset: 0x0000B4F8
		protected unsafe static char* RawStartCData(char* pDst)
		{
			*pDst = '<';
			pDst[1] = '!';
			pDst[2] = '[';
			pDst[3] = 'C';
			pDst[4] = 'D';
			pDst[5] = 'A';
			pDst[6] = 'T';
			pDst[7] = 'A';
			pDst[8] = '[';
			return pDst + 9;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C54A File Offset: 0x0000B54A
		protected unsafe static char* RawEndCData(char* pDst)
		{
			*pDst = ']';
			pDst[1] = ']';
			pDst[2] = '>';
			return pDst + 3;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000C562 File Offset: 0x0000B562
		private static bool InRange(int ch, int start, int end)
		{
			return ch - start <= end - start;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C570 File Offset: 0x0000B570
		protected void ValidateContentChars(string chars, string propertyName, bool allowOnlyWhitespace)
		{
			if (!allowOnlyWhitespace)
			{
				for (int i = 0; i < chars.Length; i++)
				{
					if (!this.xmlCharType.IsTextChar(chars[i]))
					{
						char c = chars[i];
						if (c <= '&')
						{
							switch (c)
							{
							case '\t':
							case '\n':
							case '\r':
								goto IL_152;
							case '\v':
							case '\f':
								goto IL_A7;
							default:
								if (c != '&')
								{
									goto IL_A7;
								}
								break;
							}
						}
						else if (c != '<' && c != ']')
						{
							goto IL_A7;
						}
						string @string = Res.GetString("Xml_InvalidCharacter", XmlException.BuildCharExceptionStr(chars[i]));
						goto IL_163;
						IL_A7:
						if (chars[i] >= '\ud800' && chars[i] <= '\udbff')
						{
							if (i + 1 < chars.Length && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
							{
								i++;
								goto IL_152;
							}
							@string = Res.GetString("Xml_InvalidSurrogateMissingLowChar");
						}
						else
						{
							if (chars[i] < '\udc00' || chars[i] > '\udfff')
							{
								goto IL_152;
							}
							@string = Res.GetString("Xml_InvalidSurrogateHighChar", new object[]
							{
								((uint)chars[i]).ToString("X", CultureInfo.InvariantCulture)
							});
						}
						IL_163:
						throw new ArgumentException(Res.GetString("Xml_InvalidCharsInIndent", new string[]
						{
							propertyName,
							@string
						}));
					}
					IL_152:;
				}
				return;
			}
			if (!this.xmlCharType.IsOnlyWhitespace(chars))
			{
				throw new ArgumentException(Res.GetString("Xml_IndentCharsNotWhitespace", new object[]
				{
					propertyName
				}));
			}
		}

		// Token: 0x0400052C RID: 1324
		private const int BUFSIZE = 6144;

		// Token: 0x0400052D RID: 1325
		private const int OVERFLOW = 32;

		// Token: 0x0400052E RID: 1326
		private const int INIT_MARKS_COUNT = 64;

		// Token: 0x0400052F RID: 1327
		protected byte[] bufBytes;

		// Token: 0x04000530 RID: 1328
		protected Stream stream;

		// Token: 0x04000531 RID: 1329
		protected Encoding encoding;

		// Token: 0x04000532 RID: 1330
		protected XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000533 RID: 1331
		protected int bufPos = 1;

		// Token: 0x04000534 RID: 1332
		protected int textPos = 1;

		// Token: 0x04000535 RID: 1333
		protected int contentPos;

		// Token: 0x04000536 RID: 1334
		protected int cdataPos;

		// Token: 0x04000537 RID: 1335
		protected int attrEndPos;

		// Token: 0x04000538 RID: 1336
		protected int bufLen = 6144;

		// Token: 0x04000539 RID: 1337
		protected bool writeToNull;

		// Token: 0x0400053A RID: 1338
		protected bool hadDoubleBracket;

		// Token: 0x0400053B RID: 1339
		protected bool inAttributeValue;

		// Token: 0x0400053C RID: 1340
		protected int bufBytesUsed;

		// Token: 0x0400053D RID: 1341
		protected char[] bufChars;

		// Token: 0x0400053E RID: 1342
		protected Encoder encoder;

		// Token: 0x0400053F RID: 1343
		protected TextWriter writer;

		// Token: 0x04000540 RID: 1344
		protected bool trackTextContent;

		// Token: 0x04000541 RID: 1345
		protected bool inTextContent;

		// Token: 0x04000542 RID: 1346
		private int lastMarkPos;

		// Token: 0x04000543 RID: 1347
		private int[] textContentMarks;

		// Token: 0x04000544 RID: 1348
		private CharEntityEncoderFallback charEntityFallback;

		// Token: 0x04000545 RID: 1349
		protected NewLineHandling newLineHandling;

		// Token: 0x04000546 RID: 1350
		protected bool closeOutput;

		// Token: 0x04000547 RID: 1351
		protected bool omitXmlDeclaration;

		// Token: 0x04000548 RID: 1352
		protected bool autoXmlDeclaration;

		// Token: 0x04000549 RID: 1353
		protected string newLineChars;

		// Token: 0x0400054A RID: 1354
		protected XmlStandalone standalone;

		// Token: 0x0400054B RID: 1355
		protected XmlOutputMethod outputMethod;

		// Token: 0x0400054C RID: 1356
		protected bool checkCharacters;

		// Token: 0x0400054D RID: 1357
		protected bool mergeCDataSections;
	}
}
