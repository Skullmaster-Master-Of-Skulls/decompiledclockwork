using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x020000A3 RID: 163
	internal class HtmlUtf8RawTextWriter : XmlUtf8RawTextWriter
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00015434 File Offset: 0x00013634
		public HtmlUtf8RawTextWriter(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00015445 File Offset: 0x00013645
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00015447 File Offset: 0x00013647
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001544C File Offset: 0x0001364C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			base.RawText("<!DOCTYPE ");
			if (name == "HTML")
			{
				base.RawText("HTML");
			}
			else
			{
				base.RawText("html");
			}
			int bufPos;
			if (pubid != null)
			{
				base.RawText(" PUBLIC \"");
				base.RawText(pubid);
				if (sysid != null)
				{
					base.RawText("\" \"");
					base.RawText(sysid);
				}
				byte[] bufBytes = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes[bufPos] = 34;
			}
			else if (sysid != null)
			{
				base.RawText(" SYSTEM \"");
				base.RawText(sysid);
				byte[] bufBytes2 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes2[bufPos] = 34;
			}
			else
			{
				byte[] bufBytes3 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes3[bufPos] = 32;
			}
			if (subset != null)
			{
				byte[] bufBytes4 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes4[bufPos] = 91;
				base.RawText(subset);
				byte[] bufBytes5 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes5[bufPos] = 93;
			}
			byte[] bufBytes6 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes6[bufPos] = 62;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00015574 File Offset: 0x00013774
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				this.currentElementProperties = (ElementProperties)HtmlUtf8RawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				byte[] bufBytes = this.bufBytes;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes[bufPos] = 60;
				base.RawText(localName);
				this.attrEndPos = this.bufPos;
				return;
			}
			this.currentElementProperties = ElementProperties.HAS_NS;
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000155F0 File Offset: 0x000137F0
		internal override void StartElementContent()
		{
			byte[] bufBytes = this.bufBytes;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteMetaElement();
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00015634 File Offset: 0x00013834
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					byte[] bufBytes = this.bufBytes;
					int bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes[bufPos] = 60;
					byte[] bufBytes2 = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes2[bufPos] = 47;
					base.RawText(localName);
					byte[] bufBytes3 = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes3[bufPos] = 62;
				}
			}
			else
			{
				base.WriteEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000156C4 File Offset: 0x000138C4
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					byte[] bufBytes = this.bufBytes;
					int bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes[bufPos] = 60;
					byte[] bufBytes2 = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes2[bufPos] = 47;
					base.RawText(localName);
					byte[] bufBytes3 = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes3[bufPos] = 62;
				}
			}
			else
			{
				base.WriteFullEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00015754 File Offset: 0x00013954
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				int bufPos;
				if (this.attrEndPos == this.bufPos)
				{
					byte[] bufBytes = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes[bufPos] = 32;
				}
				base.RawText(localName);
				if ((this.currentElementProperties & (ElementProperties)7U) != ElementProperties.DEFAULT)
				{
					this.currentAttributeProperties = (AttributeProperties)((ElementProperties)HtmlUtf8RawTextWriter.attributePropertySearch.FindCaseInsensitiveString(localName) & this.currentElementProperties);
					if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
					{
						this.inAttributeValue = true;
						return;
					}
				}
				else
				{
					this.currentAttributeProperties = AttributeProperties.DEFAULT;
				}
				byte[] bufBytes2 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes2[bufPos] = 61;
				byte[] bufBytes3 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes3[bufPos] = 34;
			}
			else
			{
				base.WriteStartAttribute(prefix, localName, ns);
				this.currentAttributeProperties = AttributeProperties.DEFAULT;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00015824 File Offset: 0x00013A24
		public override void WriteEndAttribute()
		{
			if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
			{
				this.attrEndPos = this.bufPos;
			}
			else
			{
				if (this.endsWithAmpersand)
				{
					this.OutputRestAmps();
					this.endsWithAmpersand = false;
				}
				byte[] bufBytes = this.bufBytes;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes[bufPos] = 34;
			}
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001588C File Offset: 0x00013A8C
		public override void WriteProcessingInstruction(string target, string text)
		{
			byte[] bufBytes = this.bufBytes;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes[bufPos] = 60;
			byte[] bufBytes2 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes2[bufPos] = 63;
			base.RawText(target);
			byte[] bufBytes3 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes3[bufPos] = 32;
			base.WriteCommentOrPi(text, 63);
			byte[] bufBytes4 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes4[bufPos] = 62;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00015928 File Offset: 0x00013B28
		public unsafe override void WriteString(string text)
		{
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* pSrcEnd = ptr + text.Length;
				if (this.inAttributeValue)
				{
					this.WriteHtmlAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteHtmlElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001596D File Offset: 0x00013B6D
		public override void WriteEntityRef(string name)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001597E File Offset: 0x00013B7E
		public override void WriteCharEntity(char ch)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001598F File Offset: 0x00013B8F
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000159A0 File Offset: 0x00013BA0
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				if (this.inAttributeValue)
				{
					base.WriteAttributeTextBlock(ptr2, ptr2 + count);
				}
				else
				{
					base.WriteElementTextBlock(ptr2, ptr2 + count);
				}
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000159E0 File Offset: 0x00013BE0
		private void Init(XmlWriterSettings settings)
		{
			if (HtmlUtf8RawTextWriter.elementPropertySearch == null)
			{
				HtmlUtf8RawTextWriter.attributePropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlAttributes);
				HtmlUtf8RawTextWriter.elementPropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlElements);
			}
			this.elementScope = new ByteStack(10);
			this.uriEscapingBuffer = new byte[5];
			this.currentElementProperties = ElementProperties.DEFAULT;
			this.mediaType = settings.MediaType;
			this.doNotEscapeUriAttributes = settings.DoNotEscapeUriAttributes;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00015A4C File Offset: 0x00013C4C
		protected void WriteMetaElement()
		{
			base.RawText("<META http-equiv=\"Content-Type\"");
			if (this.mediaType == null)
			{
				this.mediaType = "text/html";
			}
			base.RawText(" content=\"");
			base.RawText(this.mediaType);
			base.RawText("; charset=");
			base.RawText(this.encoding.WebName);
			base.RawText("\">");
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00015AB5 File Offset: 0x00013CB5
		protected unsafe void WriteHtmlElementTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentElementProperties & ElementProperties.NO_ENTITIES) != ElementProperties.DEFAULT)
			{
				base.RawText(pSrc, pSrcEnd);
				return;
			}
			base.WriteElementTextBlock(pSrc, pSrcEnd);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00015AD4 File Offset: 0x00013CD4
		protected unsafe void WriteHtmlAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentAttributeProperties & (AttributeProperties)7U) != AttributeProperties.DEFAULT)
			{
				if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
				{
					return;
				}
				if ((this.currentAttributeProperties & (AttributeProperties)5U) != AttributeProperties.DEFAULT && !this.doNotEscapeUriAttributes)
				{
					this.WriteUriAttributeText(pSrc, pSrcEnd);
					return;
				}
				this.WriteHtmlAttributeText(pSrc, pSrcEnd);
				return;
			}
			else
			{
				if ((this.currentElementProperties & ElementProperties.HAS_NS) != ElementProperties.DEFAULT)
				{
					base.WriteAttributeTextBlock(pSrc, pSrcEnd);
					return;
				}
				this.WriteHtmlAttributeText(pSrc, pSrcEnd);
				return;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00015B3C File Offset: 0x00013D3C
		private unsafe void WriteHtmlAttributeText(char* pSrc, char* pSrcEnd)
		{
			if (this.endsWithAmpersand)
			{
				if ((long)(pSrcEnd - pSrc) > 0L && *pSrc != '{')
				{
					this.OutputRestAmps();
				}
				this.endsWithAmpersand = false;
			}
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[c = *pSrc] & 128) != 0 && c <= '\u007f')
				{
					*(ptr2++) = (byte)c;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							goto IL_137;
						case '\n':
							ptr2 = XmlUtf8RawTextWriter.LineFeedEntity(ptr2);
							goto IL_163;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_163;
						default:
							if (c == '"')
							{
								ptr2 = XmlUtf8RawTextWriter.QuoteEntity(ptr2);
								goto IL_163;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
									goto IL_163;
								}
								*(ptr2++) = (byte)c;
								goto IL_163;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_137;
					}
					base.EncodeChar(ref pSrc, pSrcEnd, ref ptr2);
					continue;
					IL_163:
					pSrc++;
					continue;
					IL_137:
					*(ptr2++) = (byte)c;
					goto IL_163;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00015CC8 File Offset: 0x00013EC8
		private unsafe void WriteUriAttributeText(char* pSrc, char* pSrcEnd)
		{
			if (this.endsWithAmpersand)
			{
				if ((long)(pSrcEnd - pSrc) > 0L && *pSrc != '{')
				{
					this.OutputRestAmps();
				}
				this.endsWithAmpersand = false;
			}
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[c = *pSrc] & 128) != 0 && c < '\u0080')
				{
					*(ptr2++) = (byte)c;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							goto IL_143;
						case '\n':
							ptr2 = XmlUtf8RawTextWriter.LineFeedEntity(ptr2);
							goto IL_1E4;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_1E4;
						default:
							if (c == '"')
							{
								ptr2 = XmlUtf8RawTextWriter.QuoteEntity(ptr2);
								goto IL_1E4;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
									goto IL_1E4;
								}
								*(ptr2++) = (byte)c;
								goto IL_1E4;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_143;
					}
					byte[] array2;
					byte* ptr4;
					if ((array2 = this.uriEscapingBuffer) == null || array2.Length == 0)
					{
						ptr4 = null;
					}
					else
					{
						ptr4 = &array2[0];
					}
					byte* ptr5 = ptr4;
					byte* ptr6 = ptr5;
					XmlUtf8RawTextWriter.CharToUTF8(ref pSrc, pSrcEnd, ref ptr6);
					while (ptr5 < ptr6)
					{
						*(ptr2++) = 37;
						*(ptr2++) = (byte)"0123456789ABCDEF"[*ptr5 >> 4];
						*(ptr2++) = (byte)"0123456789ABCDEF"[(int)(*ptr5 & 15)];
						ptr5++;
					}
					array2 = null;
					continue;
					IL_1E4:
					pSrc++;
					continue;
					IL_143:
					*(ptr2++) = (byte)c;
					goto IL_1E4;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00015ED4 File Offset: 0x000140D4
		private void OutputRestAmps()
		{
			byte[] bufBytes = this.bufBytes;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes[bufPos] = 97;
			byte[] bufBytes2 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes2[bufPos] = 109;
			byte[] bufBytes3 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes3[bufPos] = 112;
			byte[] bufBytes4 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes4[bufPos] = 59;
		}

		// Token: 0x0400026E RID: 622
		protected ByteStack elementScope;

		// Token: 0x0400026F RID: 623
		protected ElementProperties currentElementProperties;

		// Token: 0x04000270 RID: 624
		private AttributeProperties currentAttributeProperties;

		// Token: 0x04000271 RID: 625
		private bool endsWithAmpersand;

		// Token: 0x04000272 RID: 626
		private byte[] uriEscapingBuffer;

		// Token: 0x04000273 RID: 627
		private string mediaType;

		// Token: 0x04000274 RID: 628
		private bool doNotEscapeUriAttributes;

		// Token: 0x04000275 RID: 629
		protected static TernaryTreeReadOnly elementPropertySearch;

		// Token: 0x04000276 RID: 630
		protected static TernaryTreeReadOnly attributePropertySearch;

		// Token: 0x04000277 RID: 631
		private const int StackIncrement = 10;
	}
}
