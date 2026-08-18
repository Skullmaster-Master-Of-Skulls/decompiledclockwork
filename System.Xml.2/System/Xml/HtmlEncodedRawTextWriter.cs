using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x020000A0 RID: 160
	internal class HtmlEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x000144F8 File Offset: 0x000126F8
		public HtmlEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00014509 File Offset: 0x00012709
		public HtmlEncodedRawTextWriter(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001451A File Offset: 0x0001271A
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001451C File Offset: 0x0001271C
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00014520 File Offset: 0x00012720
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
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
				char[] bufChars = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 34;
			}
			else if (sysid != null)
			{
				base.RawText(" SYSTEM \"");
				base.RawText(sysid);
				char[] bufChars2 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars2[bufPos] = 34;
			}
			else
			{
				char[] bufChars3 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars3[bufPos] = 32;
			}
			if (subset != null)
			{
				char[] bufChars4 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars4[bufPos] = 91;
				base.RawText(subset);
				char[] bufChars5 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars5[bufPos] = 93;
			}
			char[] bufChars6 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars6[bufPos] = 62;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00014660 File Offset: 0x00012860
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				this.currentElementProperties = (ElementProperties)HtmlEncodedRawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 60;
				base.RawText(localName);
				this.attrEndPos = this.bufPos;
				return;
			}
			this.currentElementProperties = ElementProperties.HAS_NS;
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000146F4 File Offset: 0x000128F4
		internal override void StartElementContent()
		{
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteMetaElement();
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00014738 File Offset: 0x00012938
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					char[] bufChars = this.bufChars;
					int bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars[bufPos] = 60;
					char[] bufChars2 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars2[bufPos] = 47;
					base.RawText(localName);
					char[] bufChars3 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars3[bufPos] = 62;
				}
			}
			else
			{
				base.WriteEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000147E0 File Offset: 0x000129E0
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					char[] bufChars = this.bufChars;
					int bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars[bufPos] = 60;
					char[] bufChars2 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars2[bufPos] = 47;
					base.RawText(localName);
					char[] bufChars3 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars3[bufPos] = 62;
				}
			}
			else
			{
				base.WriteFullEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00014888 File Offset: 0x00012A88
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				int bufPos;
				if (this.attrEndPos == this.bufPos)
				{
					char[] bufChars = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars[bufPos] = 32;
				}
				base.RawText(localName);
				if ((this.currentElementProperties & (ElementProperties)7U) != ElementProperties.DEFAULT)
				{
					this.currentAttributeProperties = (AttributeProperties)((ElementProperties)HtmlEncodedRawTextWriter.attributePropertySearch.FindCaseInsensitiveString(localName) & this.currentElementProperties);
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
				char[] bufChars2 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars2[bufPos] = 61;
				char[] bufChars3 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars3[bufPos] = 34;
			}
			else
			{
				base.WriteStartAttribute(prefix, localName, ns);
				this.currentAttributeProperties = AttributeProperties.DEFAULT;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00014970 File Offset: 0x00012B70
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
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 34;
			}
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000149F0 File Offset: 0x00012BF0
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 60;
			char[] bufChars2 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars2[bufPos] = 63;
			base.RawText(target);
			char[] bufChars3 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars3[bufPos] = 32;
			base.WriteCommentOrPi(text, 63);
			char[] bufChars4 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars4[bufPos] = 62;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00014AA0 File Offset: 0x00012CA0
		public unsafe override void WriteString(string text)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				base.ChangeTextContentMark(true);
			}
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

		// Token: 0x0600058A RID: 1418 RVA: 0x00014AFC File Offset: 0x00012CFC
		public override void WriteEntityRef(string name)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00014B0D File Offset: 0x00012D0D
		public override void WriteCharEntity(char ch)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00014B1E File Offset: 0x00012D1E
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00014B30 File Offset: 0x00012D30
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				base.ChangeTextContentMark(true);
			}
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

		// Token: 0x0600058E RID: 1422 RVA: 0x00014B88 File Offset: 0x00012D88
		private void Init(XmlWriterSettings settings)
		{
			if (HtmlEncodedRawTextWriter.elementPropertySearch == null)
			{
				HtmlEncodedRawTextWriter.attributePropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlAttributes);
				HtmlEncodedRawTextWriter.elementPropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlElements);
			}
			this.elementScope = new ByteStack(10);
			this.uriEscapingBuffer = new byte[5];
			this.currentElementProperties = ElementProperties.DEFAULT;
			this.mediaType = settings.MediaType;
			this.doNotEscapeUriAttributes = settings.DoNotEscapeUriAttributes;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00014BF4 File Offset: 0x00012DF4
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

		// Token: 0x06000590 RID: 1424 RVA: 0x00014C5D File Offset: 0x00012E5D
		protected unsafe void WriteHtmlElementTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentElementProperties & ElementProperties.NO_ENTITIES) != ElementProperties.DEFAULT)
			{
				base.RawText(pSrc, pSrcEnd);
				return;
			}
			base.WriteElementTextBlock(pSrc, pSrcEnd);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00014C7C File Offset: 0x00012E7C
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

		// Token: 0x06000592 RID: 1426 RVA: 0x00014CE4 File Offset: 0x00012EE4
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
			char[] array;
			char* ptr;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[c = *pSrc] & 128) != 0)
				{
					*(ptr2++) = c;
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
							goto IL_13C;
						case '\n':
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_167;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_167;
						default:
							if (c == '"')
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_167;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
									goto IL_167;
								}
								*(ptr2++) = c;
								goto IL_167;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_13C;
					}
					base.EncodeChar(ref pSrc, pSrcEnd, ref ptr2);
					continue;
					IL_167:
					pSrc++;
					continue;
					IL_13C:
					*(ptr2++) = c;
					goto IL_167;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00014E74 File Offset: 0x00013074
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
			char[] array;
			char* ptr;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[c = *pSrc] & 128) != 0 && c < '\u0080')
				{
					*(ptr2++) = c;
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
							goto IL_150;
						case '\n':
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_1EE;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_1EE;
						default:
							if (c == '"')
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_1EE;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
									goto IL_1EE;
								}
								*(ptr2++) = c;
								goto IL_1EE;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_150;
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
						*(ptr2++) = '%';
						*(ptr2++) = "0123456789ABCDEF"[*ptr5 >> 4];
						*(ptr2++) = "0123456789ABCDEF"[(int)(*ptr5 & 15)];
						ptr5++;
					}
					array2 = null;
					continue;
					IL_1EE:
					pSrc++;
					continue;
					IL_150:
					*(ptr2++) = c;
					goto IL_1EE;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00015088 File Offset: 0x00013288
		private void OutputRestAmps()
		{
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 97;
			char[] bufChars2 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars2[bufPos] = 109;
			char[] bufChars3 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars3[bufPos] = 112;
			char[] bufChars4 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars4[bufPos] = 59;
		}

		// Token: 0x0400025E RID: 606
		protected ByteStack elementScope;

		// Token: 0x0400025F RID: 607
		protected ElementProperties currentElementProperties;

		// Token: 0x04000260 RID: 608
		private AttributeProperties currentAttributeProperties;

		// Token: 0x04000261 RID: 609
		private bool endsWithAmpersand;

		// Token: 0x04000262 RID: 610
		private byte[] uriEscapingBuffer;

		// Token: 0x04000263 RID: 611
		private string mediaType;

		// Token: 0x04000264 RID: 612
		private bool doNotEscapeUriAttributes;

		// Token: 0x04000265 RID: 613
		protected static TernaryTreeReadOnly elementPropertySearch;

		// Token: 0x04000266 RID: 614
		protected static TernaryTreeReadOnly attributePropertySearch;

		// Token: 0x04000267 RID: 615
		private const int StackIncrement = 10;
	}
}
