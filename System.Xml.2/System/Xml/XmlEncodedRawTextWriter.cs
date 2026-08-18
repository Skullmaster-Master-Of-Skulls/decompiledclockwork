using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000CE RID: 206
	internal class XmlEncodedRawTextWriter : XmlRawWriter
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x0001AFCC File Offset: 0x000191CC
		protected XmlEncodedRawTextWriter(XmlWriterSettings settings)
		{
			this.useAsync = settings.Async;
			this.newLineHandling = settings.NewLineHandling;
			this.omitXmlDeclaration = settings.OmitXmlDeclaration;
			this.newLineChars = settings.NewLineChars;
			this.checkCharacters = settings.CheckCharacters;
			this.closeOutput = settings.CloseOutput;
			this.standalone = settings.Standalone;
			this.outputMethod = settings.OutputMethod;
			this.mergeCDataSections = settings.MergeCDataSections;
			if (this.checkCharacters && this.newLineHandling == NewLineHandling.Replace)
			{
				this.ValidateContentChars(this.newLineChars, "NewLineChars", false);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001B094 File Offset: 0x00019294
		public XmlEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings) : this(settings)
		{
			this.writer = writer;
			this.encoding = writer.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufChars = new char[this.bufLen + 32];
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001B100 File Offset: 0x00019300
		public XmlEncodedRawTextWriter(Stream stream, XmlWriterSettings settings) : this(settings)
		{
			this.stream = stream;
			this.encoding = settings.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufChars = new char[this.bufLen + 32];
			this.bufBytes = new byte[this.bufChars.Length];
			this.bufBytesUsed = 0;
			this.trackTextContent = true;
			this.inTextContent = false;
			this.lastMarkPos = 0;
			this.textContentMarks = new int[64];
			this.textContentMarks[0] = 1;
			this.charEntityFallback = new CharEntityEncoderFallback();
			this.encoding = (Encoding)settings.Encoding.Clone();
			this.encoding.EncoderFallback = this.charEntityFallback;
			this.encoder = this.encoding.GetEncoder();
			if (!stream.CanSeek || stream.Position == 0L)
			{
				byte[] preamble = this.encoding.GetPreamble();
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001B224 File Offset: 0x00019424
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
					CheckCharacters = this.checkCharacters,
					AutoXmlDeclaration = this.autoXmlDeclaration,
					Standalone = this.standalone,
					OutputMethod = this.outputMethod,
					ReadOnly = true
				};
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001B2B4 File Offset: 0x000194B4
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
					this.RawText(this.encoding.WebName);
				}
				if (standalone != XmlStandalone.Omit)
				{
					this.RawText("\" standalone=\"");
					this.RawText((standalone == XmlStandalone.Yes) ? "yes" : "no");
				}
				this.RawText("\"?>");
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001B357 File Offset: 0x00019557
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				this.WriteProcessingInstruction("xml", xmldecl);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001B378 File Offset: 0x00019578
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.RawText("<!DOCTYPE ");
			this.RawText(name);
			int num;
			if (pubid != null)
			{
				this.RawText(" PUBLIC \"");
				this.RawText(pubid);
				this.RawText("\" \"");
				if (sysid != null)
				{
					this.RawText(sysid);
				}
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 34;
			}
			else if (sysid != null)
			{
				this.RawText(" SYSTEM \"");
				this.RawText(sysid);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			else
			{
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
			}
			if (subset != null)
			{
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 91;
				this.RawText(subset);
				char[] array5 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 93;
			}
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 62;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001B49C File Offset: 0x0001969C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001B51C File Offset: 0x0001971C
		internal override void StartElementContent()
		{
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 62;
			this.contentPos = this.bufPos;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001B550 File Offset: 0x00019750
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.contentPos != this.bufPos)
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				if (prefix != null && prefix.Length != 0)
				{
					this.RawText(prefix);
					char[] array3 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					array3[num] = 58;
				}
				this.RawText(localName);
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 62;
				return;
			}
			this.bufPos--;
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 32;
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 47;
			char[] array7 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001B664 File Offset: 0x00019864
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 58;
			}
			this.RawText(localName);
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 62;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001B70C File Offset: 0x0001990C
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.attrEndPos == this.bufPos)
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			if (prefix != null && prefix.Length > 0)
			{
				this.RawText(prefix);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 61;
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001B7C8 File Offset: 0x000199C8
		public override void WriteEndAttribute()
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B819 File Offset: 0x00019A19
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(namespaceName);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001B82F File Offset: 0x00019A2F
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001B834 File Offset: 0x00019A34
		internal override void WriteStartNamespaceDeclaration(string prefix)
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
				char[] array = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 61;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			this.inAttributeValue = true;
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001B8D4 File Offset: 0x00019AD4
		internal override void WriteEndNamespaceDeclaration()
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.inAttributeValue = false;
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001B928 File Offset: 0x00019B28
		public override void WriteCData(string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.mergeCDataSections && this.bufPos == this.cdataPos)
			{
				this.bufPos -= 3;
			}
			else
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 33;
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 91;
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 67;
				char[] array5 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 68;
				char[] array6 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array6[num] = 65;
				char[] array7 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array7[num] = 84;
				char[] array8 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array8[num] = 65;
				char[] array9 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array9[num] = 91;
			}
			this.WriteCDataSection(text);
			char[] array10 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array10[num] = 93;
			char[] array11 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array11[num] = 93;
			char[] array12 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array12[num] = 62;
			this.textPos = this.bufPos;
			this.cdataPos = this.bufPos;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001BACC File Offset: 0x00019CCC
		public override void WriteComment(string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 33;
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 45;
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 45;
			this.WriteCommentOrPi(text, 45);
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 45;
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 45;
			char[] array7 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001BBB0 File Offset: 0x00019DB0
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 63;
			this.RawText(name);
			if (text.Length > 0)
			{
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
				this.WriteCommentOrPi(text, 63);
			}
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 63;
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 62;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001BC70 File Offset: 0x00019E70
		public override void WriteEntityRef(string name)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			this.RawText(name);
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001BCF0 File Offset: 0x00019EF0
		public override void WriteCharEntity(char ch)
		{
			int num = (int)ch;
			string s = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch, '\0');
			}
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 35;
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 120;
			this.RawText(s);
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001BDD8 File Offset: 0x00019FD8
		public unsafe override void WriteWhitespace(string ws)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (string text = ws)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
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

		// Token: 0x06000845 RID: 2117 RVA: 0x0001BE34 File Offset: 0x0001A034
		public unsafe override void WriteString(string text)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
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
					this.WriteAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001BE90 File Offset: 0x0001A090
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			char[] array = this.bufChars;
			int num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array[num2] = 38;
			char[] array2 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array2[num2] = 35;
			char[] array3 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array3[num2] = 120;
			this.RawText(num.ToString("X", NumberFormatInfo.InvariantInfo));
			char[] array4 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array4[num2] = 59;
			this.textPos = this.bufPos;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001BF48 File Offset: 0x0001A148
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				if (this.inAttributeValue)
				{
					this.WriteAttributeTextBlock(ptr2, ptr2 + count);
				}
				else
				{
					this.WriteElementTextBlock(ptr2, ptr2 + count);
				}
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public unsafe override void WriteRaw(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				this.WriteRawWithCharChecking(ptr2, ptr2 + count);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public unsafe override void WriteRaw(string data)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (string text = data)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.WriteRawWithCharChecking(ptr, ptr + data.Length);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001C040 File Offset: 0x0001A240
		public override void Close()
		{
			try
			{
				this.FlushBuffer();
				this.FlushEncoder();
			}
			finally
			{
				this.writeToNull = true;
				if (this.stream != null)
				{
					try
					{
						this.stream.Flush();
						goto IL_7D;
					}
					finally
					{
						try
						{
							if (this.closeOutput)
							{
								this.stream.Close();
							}
						}
						finally
						{
							this.stream = null;
						}
					}
				}
				if (this.writer != null)
				{
					try
					{
						this.writer.Flush();
					}
					finally
					{
						try
						{
							if (this.closeOutput)
							{
								this.writer.Close();
							}
						}
						finally
						{
							this.writer = null;
						}
					}
				}
				IL_7D:;
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001C10C File Offset: 0x0001A30C
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

		// Token: 0x0600084C RID: 2124 RVA: 0x0001C144 File Offset: 0x0001A344
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

		// Token: 0x0600084D RID: 2125 RVA: 0x0001C254 File Offset: 0x0001A454
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

		// Token: 0x0600084E RID: 2126 RVA: 0x0001C328 File Offset: 0x0001A528
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

		// Token: 0x0600084F RID: 2127 RVA: 0x0001C37C File Offset: 0x0001A57C
		protected unsafe void WriteAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
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
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (char)num;
								ptr2++;
								goto IL_1D4;
							}
							ptr2 = XmlEncodedRawTextWriter.TabEntity(ptr2);
							goto IL_1D4;
						case 10:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (char)num;
								ptr2++;
								goto IL_1D4;
							}
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_1D4;
						case 11:
						case 12:
							break;
						case 13:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (char)num;
								ptr2++;
								goto IL_1D4;
							}
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_1D4;
						default:
							if (num == 34)
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_1D4;
							}
							if (num == 38)
							{
								ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
								goto IL_1D4;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							*ptr2 = (char)num;
							ptr2++;
							goto IL_1D4;
						}
						if (num == 60)
						{
							ptr2 = XmlEncodedRawTextWriter.LtEntity(ptr2);
							goto IL_1D4;
						}
						if (num == 62)
						{
							ptr2 = XmlEncodedRawTextWriter.GtEntity(ptr2);
							goto IL_1D4;
						}
					}
					if (XmlCharType.IsSurrogate(num))
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
					IL_1D4:
					pSrc++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001C578 File Offset: 0x0001A778
		protected unsafe void WriteElementTextBlock(char* pSrc, char* pSrcEnd)
		{
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
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							goto IL_110;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr2 = this.WriteNewLine(ptr2);
								goto IL_1D7;
							}
							*ptr2 = (char)num;
							ptr2++;
							goto IL_1D7;
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
								goto IL_1D7;
							case NewLineHandling.Entitize:
								ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
								goto IL_1D7;
							case NewLineHandling.None:
								*ptr2 = (char)num;
								ptr2++;
								goto IL_1D7;
							default:
								goto IL_1D7;
							}
							break;
						default:
							if (num == 34)
							{
								goto IL_110;
							}
							if (num == 38)
							{
								ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
								goto IL_1D7;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							goto IL_110;
						}
						if (num == 60)
						{
							ptr2 = XmlEncodedRawTextWriter.LtEntity(ptr2);
							goto IL_1D7;
						}
						if (num == 62)
						{
							ptr2 = XmlEncodedRawTextWriter.GtEntity(ptr2);
							goto IL_1D7;
						}
					}
					if (XmlCharType.IsSurrogate(num))
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
					IL_1D7:
					pSrc++;
					continue;
					IL_110:
					*ptr2 = (char)num;
					ptr2++;
					goto IL_1D7;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001C788 File Offset: 0x0001A988
		protected unsafe void RawText(string s)
		{
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.RawText(ptr, ptr + s.Length);
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		protected unsafe void RawText(char* pSrcBegin, char* pSrcEnd)
		{
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
				else if (XmlCharType.IsSurrogate(num))
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
			array = null;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
		protected unsafe void WriteRawWithCharChecking(char* pSrcBegin, char* pSrcEnd)
		{
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
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							goto IL_E0;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr3 = this.WriteNewLine(ptr3);
								goto IL_187;
							}
							*ptr3 = (char)num;
							ptr3++;
							goto IL_187;
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
								goto IL_187;
							}
							*ptr3 = (char)num;
							ptr3++;
							goto IL_187;
						default:
							if (num == 38)
							{
								goto IL_E0;
							}
							break;
						}
					}
					else if (num == 60 || num == 93)
					{
						goto IL_E0;
					}
					if (XmlCharType.IsSurrogate(num))
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
					IL_187:
					ptr2++;
					continue;
					IL_E0:
					*ptr3 = (char)num;
					ptr3++;
					goto IL_187;
				}
				this.bufPos = (int)((long)(ptr3 - ptr));
				this.FlushBuffer();
				ptr3 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001CA74 File Offset: 0x0001AC74
		protected unsafe void WriteCommentOrPi(string text, int stopChar)
		{
			if (text.Length == 0)
			{
				if (this.bufPos >= this.bufLen)
				{
					this.FlushBuffer();
				}
				return;
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char[] array;
				char* ptr2;
				if ((array = this.bufChars) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
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
						if (num <= 45)
						{
							switch (num)
							{
							case 9:
								goto IL_227;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_297;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_297;
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
									goto IL_297;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_297;
							default:
								if (num == 38)
								{
									goto IL_227;
								}
								if (num == 45)
								{
									*ptr5 = '-';
									ptr5++;
									if (num == stopChar && (ptr3 + 1 == ptr4 || ptr3[1] == '-'))
									{
										*ptr5 = ' ';
										ptr5++;
										goto IL_297;
									}
									goto IL_297;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_227;
							}
							if (num != 63)
							{
								if (num == 93)
								{
									*ptr5 = ']';
									ptr5++;
									goto IL_297;
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
									goto IL_297;
								}
								goto IL_297;
							}
						}
						if (XmlCharType.IsSurrogate(num))
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
						IL_297:
						ptr3++;
						continue;
						IL_227:
						*ptr5 = (char)num;
						ptr5++;
						goto IL_297;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001CD38 File Offset: 0x0001AF38
		protected unsafe void WriteCDataSection(string text)
		{
			if (text.Length == 0)
			{
				if (this.bufPos >= this.bufLen)
				{
					this.FlushBuffer();
				}
				return;
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char[] array;
				char* ptr2;
				if ((array = this.bufChars) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
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
						if (num <= 39)
						{
							switch (num)
							{
							case 9:
								goto IL_211;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_281;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_281;
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
									goto IL_281;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_281;
							default:
								if (num == 34 || num - 38 <= 1)
								{
									goto IL_211;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_211;
							}
							if (num == 62)
							{
								if (this.hadDoubleBracket && ptr5[-1] == ']')
								{
									ptr5 = XmlEncodedRawTextWriter.RawEndCData(ptr5);
									ptr5 = XmlEncodedRawTextWriter.RawStartCData(ptr5);
								}
								*ptr5 = '>';
								ptr5++;
								goto IL_281;
							}
							if (num == 93)
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
								goto IL_281;
							}
						}
						if (XmlCharType.IsSurrogate(num))
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
						IL_281:
						ptr3++;
						continue;
						IL_211:
						*ptr5 = (char)num;
						ptr5++;
						goto IL_281;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001CFE4 File Offset: 0x0001B1E4
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
			if (num2 >= 56320 && (LocalAppContextSwitches.DontThrowOnInvalidSurrogatePairs || num2 <= 57343))
			{
				*pDst = (char)num;
				pDst[1] = (char)num2;
				pDst += 2;
				return pDst;
			}
			throw XmlConvert.CreateInvalidSurrogatePairException((char)num2, (char)num);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001D053 File Offset: 0x0001B253
		private unsafe char* InvalidXmlChar(int ch, char* pDst, bool entitize)
		{
			if (this.checkCharacters)
			{
				throw XmlConvert.CreateInvalidCharException((char)ch, '\0');
			}
			if (entitize)
			{
				return XmlEncodedRawTextWriter.CharEntity(pDst, (char)ch);
			}
			*pDst = (char)ch;
			pDst++;
			return pDst;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001D07C File Offset: 0x0001B27C
		internal unsafe void EncodeChar(ref char* pSrc, char* pSrcEnd, ref char* pDst)
		{
			int num = (int)(*pSrc);
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			if (num <= 127 || num >= 65534)
			{
				pDst = this.InvalidXmlChar(num, pDst, false);
				pSrc += 2;
				return;
			}
			*pDst = (short)((ushort)num);
			pDst += 2;
			pSrc += 2;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		protected void ChangeTextContentMark(bool value)
		{
			this.inTextContent = value;
			if (this.lastMarkPos + 1 == this.textContentMarks.Length)
			{
				this.GrowTextContentMarks();
			}
			int[] array = this.textContentMarks;
			int num = this.lastMarkPos + 1;
			this.lastMarkPos = num;
			array[num] = this.bufPos;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001D128 File Offset: 0x0001B328
		private void GrowTextContentMarks()
		{
			int[] destinationArray = new int[this.textContentMarks.Length * 2];
			Array.Copy(this.textContentMarks, destinationArray, this.textContentMarks.Length);
			this.textContentMarks = destinationArray;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001D160 File Offset: 0x0001B360
		protected unsafe char* WriteNewLine(char* pDst)
		{
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
			this.bufPos = (int)((long)(pDst - ptr));
			this.RawText(this.newLineChars);
			return ptr + this.bufPos;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001D1AE File Offset: 0x0001B3AE
		protected unsafe static char* LtEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'l';
			pDst[2] = 't';
			pDst[3] = ';';
			return pDst + 4;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001D1D2 File Offset: 0x0001B3D2
		protected unsafe static char* GtEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'g';
			pDst[2] = 't';
			pDst[3] = ';';
			return pDst + 4;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001D1F6 File Offset: 0x0001B3F6
		protected unsafe static char* AmpEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'a';
			pDst[2] = 'm';
			pDst[3] = 'p';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001D223 File Offset: 0x0001B423
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

		// Token: 0x06000860 RID: 2144 RVA: 0x0001D259 File Offset: 0x0001B459
		protected unsafe static char* TabEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = '9';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001D286 File Offset: 0x0001B486
		protected unsafe static char* LineFeedEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = 'A';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001D2B3 File Offset: 0x0001B4B3
		protected unsafe static char* CarriageReturnEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = 'D';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		private unsafe static char* CharEntity(char* pDst, char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst += 3;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while ((*(pDst++) = *(ptr2++)) != '\0')
				{
				}
			}
			pDst[-1] = ';';
			return pDst;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001D354 File Offset: 0x0001B554
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

		// Token: 0x06000865 RID: 2149 RVA: 0x0001D3B1 File Offset: 0x0001B5B1
		protected unsafe static char* RawEndCData(char* pDst)
		{
			*pDst = ']';
			pDst[1] = ']';
			pDst[2] = '>';
			return pDst + 3;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001D3CC File Offset: 0x0001B5CC
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
								goto IL_11C;
							case '\v':
							case '\f':
								goto IL_A2;
							default:
								if (c != '&')
								{
									goto IL_A2;
								}
								break;
							}
						}
						else if (c != '<' && c != ']')
						{
							goto IL_A2;
						}
						string name = "Xml_InvalidCharacter";
						object[] args = XmlException.BuildCharExceptionArgs(chars, i);
						string @string = Res.GetString(name, args);
						goto IL_12D;
						IL_A2:
						if (XmlCharType.IsHighSurrogate((int)chars[i]))
						{
							if (i + 1 < chars.Length && XmlCharType.IsLowSurrogate((int)chars[i + 1]))
							{
								i++;
								goto IL_11C;
							}
							@string = Res.GetString("Xml_InvalidSurrogateMissingLowChar");
						}
						else
						{
							if (!XmlCharType.IsLowSurrogate((int)chars[i]))
							{
								goto IL_11C;
							}
							@string = Res.GetString("Xml_InvalidSurrogateHighChar", new object[]
							{
								((uint)chars[i]).ToString("X", CultureInfo.InvariantCulture)
							});
						}
						IL_12D:
						string name2 = "Xml_InvalidCharsInIndent";
						args = new string[]
						{
							propertyName,
							@string
						};
						throw new ArgumentException(Res.GetString(name2, args));
					}
					IL_11C:;
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

		// Token: 0x06000867 RID: 2151 RVA: 0x0001D526 File Offset: 0x0001B726
		protected void CheckAsyncCall()
		{
			if (!this.useAsync)
			{
				throw new InvalidOperationException(Res.GetString("Xml_WriterAsyncNotSetException"));
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001D540 File Offset: 0x0001B740
		internal override Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96 <WriteXmlDeclarationAsync>d__;
			<WriteXmlDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteXmlDeclarationAsync>d__.<>4__this = this;
			<WriteXmlDeclarationAsync>d__.standalone = standalone;
			<WriteXmlDeclarationAsync>d__.<>1__state = -1;
			<WriteXmlDeclarationAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref <WriteXmlDeclarationAsync>d__);
			return <WriteXmlDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001D58B File Offset: 0x0001B78B
		internal override Task WriteXmlDeclarationAsync(string xmldecl)
		{
			this.CheckAsyncCall();
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				return this.WriteProcessingInstructionAsync("xml", xmldecl);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001D61C File Offset: 0x0001B81C
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			Task task;
			if (prefix != null && prefix.Length != 0)
			{
				task = this.RawTextAsync(prefix + ":" + localName);
			}
			else
			{
				task = this.RawTextAsync(localName);
			}
			return task.CallVoidFuncWhenFinish(new Action(this.WriteStartElementAsync_SetAttEndPos));
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001D69A File Offset: 0x0001B89A
		private void WriteStartElementAsync_SetAttEndPos()
		{
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001D6A8 File Offset: 0x0001B8A8
		internal override Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.contentPos == this.bufPos)
			{
				this.bufPos--;
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 62;
				return AsyncHelper.DoneTask;
			}
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 60;
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				return this.RawTextAsync(prefix + ":" + localName + ">");
			}
			return this.RawTextAsync(localName + ">");
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		internal override Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				return this.RawTextAsync(prefix + ":" + localName + ">");
			}
			return this.RawTextAsync(localName + ">");
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001D840 File Offset: 0x0001BA40
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.attrEndPos == this.bufPos)
			{
				char[] array = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			Task task;
			if (prefix != null && prefix.Length > 0)
			{
				task = this.RawTextAsync(prefix + ":" + localName);
			}
			else
			{
				task = this.RawTextAsync(localName);
			}
			return task.CallVoidFuncWhenFinish(new Action(this.WriteStartAttribute_SetInAttribute));
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		private void WriteStartAttribute_SetInAttribute()
		{
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 61;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001D918 File Offset: 0x0001BB18
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001D974 File Offset: 0x0001BB74
		internal override Task WriteNamespaceDeclarationAsync(string prefix, string namespaceName)
		{
			XmlEncodedRawTextWriter.<WriteNamespaceDeclarationAsync>d__106 <WriteNamespaceDeclarationAsync>d__;
			<WriteNamespaceDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNamespaceDeclarationAsync>d__.<>4__this = this;
			<WriteNamespaceDeclarationAsync>d__.prefix = prefix;
			<WriteNamespaceDeclarationAsync>d__.namespaceName = namespaceName;
			<WriteNamespaceDeclarationAsync>d__.<>1__state = -1;
			<WriteNamespaceDeclarationAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteNamespaceDeclarationAsync>d__106>(ref <WriteNamespaceDeclarationAsync>d__);
			return <WriteNamespaceDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001D9C8 File Offset: 0x0001BBC8
		internal override Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			XmlEncodedRawTextWriter.<WriteStartNamespaceDeclarationAsync>d__107 <WriteStartNamespaceDeclarationAsync>d__;
			<WriteStartNamespaceDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartNamespaceDeclarationAsync>d__.<>4__this = this;
			<WriteStartNamespaceDeclarationAsync>d__.prefix = prefix;
			<WriteStartNamespaceDeclarationAsync>d__.<>1__state = -1;
			<WriteStartNamespaceDeclarationAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteStartNamespaceDeclarationAsync>d__107>(ref <WriteStartNamespaceDeclarationAsync>d__);
			return <WriteStartNamespaceDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001DA14 File Offset: 0x0001BC14
		internal override Task WriteEndNamespaceDeclarationAsync()
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.inAttributeValue = false;
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001DA70 File Offset: 0x0001BC70
		public override Task WriteCDataAsync(string text)
		{
			XmlEncodedRawTextWriter.<WriteCDataAsync>d__109 <WriteCDataAsync>d__;
			<WriteCDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataAsync>d__.<>4__this = this;
			<WriteCDataAsync>d__.text = text;
			<WriteCDataAsync>d__.<>1__state = -1;
			<WriteCDataAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCDataAsync>d__109>(ref <WriteCDataAsync>d__);
			return <WriteCDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001DABC File Offset: 0x0001BCBC
		public override Task WriteCommentAsync(string text)
		{
			XmlEncodedRawTextWriter.<WriteCommentAsync>d__110 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCommentAsync>d__110>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001DB08 File Offset: 0x0001BD08
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			XmlEncodedRawTextWriter.<WriteProcessingInstructionAsync>d__111 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.name = name;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteProcessingInstructionAsync>d__111>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001DB5C File Offset: 0x0001BD5C
		public override Task WriteEntityRefAsync(string name)
		{
			XmlEncodedRawTextWriter.<WriteEntityRefAsync>d__112 <WriteEntityRefAsync>d__;
			<WriteEntityRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEntityRefAsync>d__.<>4__this = this;
			<WriteEntityRefAsync>d__.name = name;
			<WriteEntityRefAsync>d__.<>1__state = -1;
			<WriteEntityRefAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteEntityRefAsync>d__112>(ref <WriteEntityRefAsync>d__);
			return <WriteEntityRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		public override Task WriteCharEntityAsync(char ch)
		{
			XmlEncodedRawTextWriter.<WriteCharEntityAsync>d__113 <WriteCharEntityAsync>d__;
			<WriteCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCharEntityAsync>d__.<>4__this = this;
			<WriteCharEntityAsync>d__.ch = ch;
			<WriteCharEntityAsync>d__.<>1__state = -1;
			<WriteCharEntityAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCharEntityAsync>d__113>(ref <WriteCharEntityAsync>d__);
			return <WriteCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001DBF3 File Offset: 0x0001BDF3
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(ws);
			}
			return this.WriteElementTextBlockAsync(ws);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001DC29 File Offset: 0x0001BE29
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(text);
			}
			return this.WriteElementTextBlockAsync(text);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001DC60 File Offset: 0x0001BE60
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			XmlEncodedRawTextWriter.<WriteSurrogateCharEntityAsync>d__116 <WriteSurrogateCharEntityAsync>d__;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteSurrogateCharEntityAsync>d__.<>4__this = this;
			<WriteSurrogateCharEntityAsync>d__.lowChar = lowChar;
			<WriteSurrogateCharEntityAsync>d__.highChar = highChar;
			<WriteSurrogateCharEntityAsync>d__.<>1__state = -1;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteSurrogateCharEntityAsync>d__116>(ref <WriteSurrogateCharEntityAsync>d__);
			return <WriteSurrogateCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001DCB3 File Offset: 0x0001BEB3
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(buffer, index, count);
			}
			return this.WriteElementTextBlockAsync(buffer, index, count);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001DCF0 File Offset: 0x0001BEF0
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			XmlEncodedRawTextWriter.<WriteRawAsync>d__118 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.buffer = buffer;
			<WriteRawAsync>d__.index = index;
			<WriteRawAsync>d__.count = count;
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteRawAsync>d__118>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		public override Task WriteRawAsync(string data)
		{
			XmlEncodedRawTextWriter.<WriteRawAsync>d__119 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.data = data;
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteRawAsync>d__119>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001DD98 File Offset: 0x0001BF98
		public override Task FlushAsync()
		{
			XmlEncodedRawTextWriter.<FlushAsync>d__120 <FlushAsync>d__;
			<FlushAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsync>d__.<>4__this = this;
			<FlushAsync>d__.<>1__state = -1;
			<FlushAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<FlushAsync>d__120>(ref <FlushAsync>d__);
			return <FlushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001DDDC File Offset: 0x0001BFDC
		protected virtual Task FlushBufferAsync()
		{
			XmlEncodedRawTextWriter.<FlushBufferAsync>d__121 <FlushBufferAsync>d__;
			<FlushBufferAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushBufferAsync>d__.<>4__this = this;
			<FlushBufferAsync>d__.<>1__state = -1;
			<FlushBufferAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<FlushBufferAsync>d__121>(ref <FlushBufferAsync>d__);
			return <FlushBufferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001DE20 File Offset: 0x0001C020
		private Task EncodeCharsAsync(int startOffset, int endOffset, bool writeAllToStream)
		{
			XmlEncodedRawTextWriter.<EncodeCharsAsync>d__122 <EncodeCharsAsync>d__;
			<EncodeCharsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EncodeCharsAsync>d__.<>4__this = this;
			<EncodeCharsAsync>d__.startOffset = startOffset;
			<EncodeCharsAsync>d__.endOffset = endOffset;
			<EncodeCharsAsync>d__.writeAllToStream = writeAllToStream;
			<EncodeCharsAsync>d__.<>1__state = -1;
			<EncodeCharsAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<EncodeCharsAsync>d__122>(ref <EncodeCharsAsync>d__);
			return <EncodeCharsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001DE7C File Offset: 0x0001C07C
		private Task FlushEncoderAsync()
		{
			if (this.stream != null)
			{
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, 1, 0, this.bufBytes, 0, this.bufBytes.Length, true, out num, out num2, out flag);
				if (num2 != 0)
				{
					return this.stream.WriteAsync(this.bufBytes, 0, num2);
				}
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		[SecuritySafeCritical]
		protected unsafe int WriteAttributeTextBlockNoFlush(char* pSrc, char* pSrcEnd)
		{
			char* ptr = pSrc;
			char[] array;
			char* ptr2;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			char* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_1EF;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (char)num;
							ptr3++;
							goto IL_1E5;
						}
						ptr3 = XmlEncodedRawTextWriter.TabEntity(ptr3);
						goto IL_1E5;
					case 10:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (char)num;
							ptr3++;
							goto IL_1E5;
						}
						ptr3 = XmlEncodedRawTextWriter.LineFeedEntity(ptr3);
						goto IL_1E5;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (char)num;
							ptr3++;
							goto IL_1E5;
						}
						ptr3 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr3);
						goto IL_1E5;
					default:
						if (num == 34)
						{
							ptr3 = XmlEncodedRawTextWriter.QuoteEntity(ptr3);
							goto IL_1E5;
						}
						if (num == 38)
						{
							ptr3 = XmlEncodedRawTextWriter.AmpEntity(ptr3);
							goto IL_1E5;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						*ptr3 = (char)num;
						ptr3++;
						goto IL_1E5;
					}
					if (num == 60)
					{
						ptr3 = XmlEncodedRawTextWriter.LtEntity(ptr3);
						goto IL_1E5;
					}
					if (num == 62)
					{
						ptr3 = XmlEncodedRawTextWriter.GtEntity(ptr3);
						goto IL_1E5;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				*ptr3 = (char)num;
				ptr3++;
				pSrc++;
				continue;
				IL_1E5:
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			IL_1EF:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			array = null;
			return -1;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001E0E4 File Offset: 0x0001C2E4
		[SecuritySafeCritical]
		protected unsafe int WriteAttributeTextBlockNoFlush(char[] chars, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2;
				char* pSrcEnd = ptr3 + count;
				return this.WriteAttributeTextBlockNoFlush(ptr3, pSrcEnd);
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001E114 File Offset: 0x0001C314
		[SecuritySafeCritical]
		protected unsafe int WriteAttributeTextBlockNoFlush(string text, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* pSrcEnd = ptr2 + count;
			return this.WriteAttributeTextBlockNoFlush(ptr2, pSrcEnd);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001E14C File Offset: 0x0001C34C
		protected Task WriteAttributeTextBlockAsync(char[] chars, int index, int count)
		{
			XmlEncodedRawTextWriter.<WriteAttributeTextBlockAsync>d__127 <WriteAttributeTextBlockAsync>d__;
			<WriteAttributeTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAttributeTextBlockAsync>d__.<>4__this = this;
			<WriteAttributeTextBlockAsync>d__.chars = chars;
			<WriteAttributeTextBlockAsync>d__.index = index;
			<WriteAttributeTextBlockAsync>d__.count = count;
			<WriteAttributeTextBlockAsync>d__.<>1__state = -1;
			<WriteAttributeTextBlockAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteAttributeTextBlockAsync>d__127>(ref <WriteAttributeTextBlockAsync>d__);
			return <WriteAttributeTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		protected Task WriteAttributeTextBlockAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			int num3 = this.WriteAttributeTextBlockNoFlush(text, num, num2);
			num += num3;
			num2 -= num3;
			if (num3 >= 0)
			{
				return this._WriteAttributeTextBlockAsync(text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001E1E8 File Offset: 0x0001C3E8
		private Task _WriteAttributeTextBlockAsync(string text, int curIndex, int leftCount)
		{
			XmlEncodedRawTextWriter.<_WriteAttributeTextBlockAsync>d__129 <_WriteAttributeTextBlockAsync>d__;
			<_WriteAttributeTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_WriteAttributeTextBlockAsync>d__.<>4__this = this;
			<_WriteAttributeTextBlockAsync>d__.text = text;
			<_WriteAttributeTextBlockAsync>d__.curIndex = curIndex;
			<_WriteAttributeTextBlockAsync>d__.leftCount = leftCount;
			<_WriteAttributeTextBlockAsync>d__.<>1__state = -1;
			<_WriteAttributeTextBlockAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<_WriteAttributeTextBlockAsync>d__129>(ref <_WriteAttributeTextBlockAsync>d__);
			return <_WriteAttributeTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001E244 File Offset: 0x0001C444
		[SecuritySafeCritical]
		protected unsafe int WriteElementTextBlockNoFlush(char* pSrc, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			char* ptr = pSrc;
			char[] array;
			char* ptr2;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			char* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_210;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						goto IL_11B;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_13;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_206;
					case 11:
					case 12:
						break;
					case 13:
						switch (this.newLineHandling)
						{
						case NewLineHandling.Replace:
							goto IL_177;
						case NewLineHandling.Entitize:
							ptr3 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr3);
							goto IL_206;
						case NewLineHandling.None:
							*ptr3 = (char)num;
							ptr3++;
							goto IL_206;
						default:
							goto IL_206;
						}
						break;
					default:
						if (num == 34)
						{
							goto IL_11B;
						}
						if (num == 38)
						{
							ptr3 = XmlEncodedRawTextWriter.AmpEntity(ptr3);
							goto IL_206;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						goto IL_11B;
					}
					if (num == 60)
					{
						ptr3 = XmlEncodedRawTextWriter.LtEntity(ptr3);
						goto IL_206;
					}
					if (num == 62)
					{
						ptr3 = XmlEncodedRawTextWriter.GtEntity(ptr3);
						goto IL_206;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				*ptr3 = (char)num;
				ptr3++;
				pSrc++;
				continue;
				IL_206:
				pSrc++;
				continue;
				IL_11B:
				*ptr3 = (char)num;
				ptr3++;
				goto IL_206;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			Block_13:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_177:
			if (pSrc[1] == '\n')
			{
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_210:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
			return -1;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001E484 File Offset: 0x0001C684
		[SecuritySafeCritical]
		protected unsafe int WriteElementTextBlockNoFlush(char[] chars, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				this.contentPos = 0;
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2;
				char* pSrcEnd = ptr3 + count;
				return this.WriteElementTextBlockNoFlush(ptr3, pSrcEnd, out needWriteNewLine);
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
		[SecuritySafeCritical]
		protected unsafe int WriteElementTextBlockNoFlush(string text, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				this.contentPos = 0;
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* pSrcEnd = ptr2 + count;
			return this.WriteElementTextBlockNoFlush(ptr2, pSrcEnd, out needWriteNewLine);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001E508 File Offset: 0x0001C708
		protected Task WriteElementTextBlockAsync(char[] chars, int index, int count)
		{
			XmlEncodedRawTextWriter.<WriteElementTextBlockAsync>d__133 <WriteElementTextBlockAsync>d__;
			<WriteElementTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteElementTextBlockAsync>d__.<>4__this = this;
			<WriteElementTextBlockAsync>d__.chars = chars;
			<WriteElementTextBlockAsync>d__.index = index;
			<WriteElementTextBlockAsync>d__.count = count;
			<WriteElementTextBlockAsync>d__.<>1__state = -1;
			<WriteElementTextBlockAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteElementTextBlockAsync>d__133>(ref <WriteElementTextBlockAsync>d__);
			return <WriteElementTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001E564 File Offset: 0x0001C764
		protected Task WriteElementTextBlockAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			bool flag = false;
			int num3 = this.WriteElementTextBlockNoFlush(text, num, num2, out flag);
			num += num3;
			num2 -= num3;
			if (flag)
			{
				return this._WriteElementTextBlockAsync(true, text, num, num2);
			}
			if (num3 >= 0)
			{
				return this._WriteElementTextBlockAsync(false, text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
		private Task _WriteElementTextBlockAsync(bool newLine, string text, int curIndex, int leftCount)
		{
			XmlEncodedRawTextWriter.<_WriteElementTextBlockAsync>d__135 <_WriteElementTextBlockAsync>d__;
			<_WriteElementTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_WriteElementTextBlockAsync>d__.<>4__this = this;
			<_WriteElementTextBlockAsync>d__.newLine = newLine;
			<_WriteElementTextBlockAsync>d__.text = text;
			<_WriteElementTextBlockAsync>d__.curIndex = curIndex;
			<_WriteElementTextBlockAsync>d__.leftCount = leftCount;
			<_WriteElementTextBlockAsync>d__.<>1__state = -1;
			<_WriteElementTextBlockAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<_WriteElementTextBlockAsync>d__135>(ref <_WriteElementTextBlockAsync>d__);
			return <_WriteElementTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001E618 File Offset: 0x0001C818
		[SecuritySafeCritical]
		protected unsafe int RawTextNoFlush(char* pSrcBegin, char* pSrcEnd)
		{
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
					goto IL_F9;
				}
				if (ptr2 >= ptr4)
				{
					break;
				}
				if (XmlCharType.IsSurrogate(num))
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
			return (int)((long)(ptr3 - pSrcBegin));
			IL_F9:
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001E730 File Offset: 0x0001C930
		[SecuritySafeCritical]
		protected unsafe int RawTextNoFlush(string text, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* pSrcEnd = ptr2 + count;
			return this.RawTextNoFlush(ptr2, pSrcEnd);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001E768 File Offset: 0x0001C968
		protected Task RawTextAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			int num3 = this.RawTextNoFlush(text, num, num2);
			num += num3;
			num2 -= num3;
			if (num3 >= 0)
			{
				return this._RawTextAsync(text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		private Task _RawTextAsync(string text, int curIndex, int leftCount)
		{
			XmlEncodedRawTextWriter.<_RawTextAsync>d__139 <_RawTextAsync>d__;
			<_RawTextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_RawTextAsync>d__.<>4__this = this;
			<_RawTextAsync>d__.text = text;
			<_RawTextAsync>d__.curIndex = curIndex;
			<_RawTextAsync>d__.leftCount = leftCount;
			<_RawTextAsync>d__.<>1__state = -1;
			<_RawTextAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<_RawTextAsync>d__139>(ref <_RawTextAsync>d__);
			return <_RawTextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001E804 File Offset: 0x0001CA04
		[SecuritySafeCritical]
		protected unsafe int WriteRawWithCharCheckingNoFlush(char* pSrcBegin, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
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
					goto IL_1CD;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						goto IL_EC;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_12;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_1C4;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_10;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_1C4;
					default:
						if (num == 38)
						{
							goto IL_EC;
						}
						break;
					}
				}
				else if (num == 60 || num == 93)
				{
					goto IL_EC;
				}
				if (XmlCharType.IsSurrogate(num))
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
				IL_1C4:
				ptr2++;
				continue;
				IL_EC:
				*ptr3 = (char)num;
				ptr3++;
				goto IL_1C4;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			return (int)((long)(ptr2 - pSrcBegin));
			Block_10:
			if (ptr2[1] == '\n')
			{
				ptr2++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			Block_12:
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			IL_1CD:
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		[SecuritySafeCritical]
		protected unsafe int WriteRawWithCharCheckingNoFlush(char[] chars, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2;
				char* pSrcEnd = ptr3 + count;
				return this.WriteRawWithCharCheckingNoFlush(ptr3, pSrcEnd, out needWriteNewLine);
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001EA24 File Offset: 0x0001CC24
		[SecuritySafeCritical]
		protected unsafe int WriteRawWithCharCheckingNoFlush(string text, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* pSrcEnd = ptr2 + count;
			return this.WriteRawWithCharCheckingNoFlush(ptr2, pSrcEnd, out needWriteNewLine);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001EA64 File Offset: 0x0001CC64
		protected Task WriteRawWithCharCheckingAsync(char[] chars, int index, int count)
		{
			XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__143 <WriteRawWithCharCheckingAsync>d__;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawWithCharCheckingAsync>d__.<>4__this = this;
			<WriteRawWithCharCheckingAsync>d__.chars = chars;
			<WriteRawWithCharCheckingAsync>d__.index = index;
			<WriteRawWithCharCheckingAsync>d__.count = count;
			<WriteRawWithCharCheckingAsync>d__.<>1__state = -1;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__143>(ref <WriteRawWithCharCheckingAsync>d__);
			return <WriteRawWithCharCheckingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		protected Task WriteRawWithCharCheckingAsync(string text)
		{
			XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__144 <WriteRawWithCharCheckingAsync>d__;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawWithCharCheckingAsync>d__.<>4__this = this;
			<WriteRawWithCharCheckingAsync>d__.text = text;
			<WriteRawWithCharCheckingAsync>d__.<>1__state = -1;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__144>(ref <WriteRawWithCharCheckingAsync>d__);
			return <WriteRawWithCharCheckingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001EB0C File Offset: 0x0001CD0C
		[SecuritySafeCritical]
		protected unsafe int WriteCommentOrPiNoFlush(string text, int index, int count, int stopChar, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char[] array;
			char* ptr3;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr3 = null;
			}
			else
			{
				ptr3 = &array[0];
			}
			char* ptr4 = ptr2;
			char* ptr5 = ptr4;
			char* ptr6 = ptr2 + count;
			char* ptr7 = ptr3 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr8 = ptr7 + (long)(ptr6 - ptr4) * 2L / 2L;
				if (ptr8 != ptr3 + this.bufLen)
				{
					ptr8 = ptr3 + this.bufLen;
				}
				while (ptr7 < ptr8 && (this.xmlCharType.charProperties[num = (int)(*ptr4)] & 64) != 0 && num != stopChar)
				{
					*ptr7 = (char)num;
					ptr7++;
					ptr4++;
				}
				if (ptr4 >= ptr6)
				{
					goto IL_2B1;
				}
				if (ptr7 >= ptr8)
				{
					break;
				}
				if (num <= 45)
				{
					switch (num)
					{
					case 9:
						goto IL_236;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_23;
						}
						*ptr7 = (char)num;
						ptr7++;
						goto IL_2A6;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_21;
						}
						*ptr7 = (char)num;
						ptr7++;
						goto IL_2A6;
					default:
						if (num == 38)
						{
							goto IL_236;
						}
						if (num == 45)
						{
							*ptr7 = '-';
							ptr7++;
							if (num == stopChar && (ptr4 + 1 == ptr6 || ptr4[1] == '-'))
							{
								*ptr7 = ' ';
								ptr7++;
								goto IL_2A6;
							}
							goto IL_2A6;
						}
						break;
					}
				}
				else
				{
					if (num == 60)
					{
						goto IL_236;
					}
					if (num != 63)
					{
						if (num == 93)
						{
							*ptr7 = ']';
							ptr7++;
							goto IL_2A6;
						}
					}
					else
					{
						*ptr7 = '?';
						ptr7++;
						if (num == stopChar && ptr4 + 1 < ptr6 && ptr4[1] == '>')
						{
							*ptr7 = ' ';
							ptr7++;
							goto IL_2A6;
						}
						goto IL_2A6;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr7 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr4, ptr6, ptr7);
					ptr4 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr7 = this.InvalidXmlChar(num, ptr7, false);
					ptr4++;
					continue;
				}
				*ptr7 = (char)num;
				ptr7++;
				ptr4++;
				continue;
				IL_2A6:
				ptr4++;
				continue;
				IL_236:
				*ptr7 = (char)num;
				ptr7++;
				goto IL_2A6;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			return (int)((long)(ptr4 - ptr5));
			Block_21:
			if (ptr4[1] == '\n')
			{
				ptr4++;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr5));
			Block_23:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr5));
			IL_2B1:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			array = null;
			return -1;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001EDDC File Offset: 0x0001CFDC
		protected Task WriteCommentOrPiAsync(string text, int stopChar)
		{
			XmlEncodedRawTextWriter.<WriteCommentOrPiAsync>d__146 <WriteCommentOrPiAsync>d__;
			<WriteCommentOrPiAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentOrPiAsync>d__.<>4__this = this;
			<WriteCommentOrPiAsync>d__.text = text;
			<WriteCommentOrPiAsync>d__.stopChar = stopChar;
			<WriteCommentOrPiAsync>d__.<>1__state = -1;
			<WriteCommentOrPiAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCommentOrPiAsync>d__146>(ref <WriteCommentOrPiAsync>d__);
			return <WriteCommentOrPiAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001EE30 File Offset: 0x0001D030
		[SecuritySafeCritical]
		protected unsafe int WriteCDataSectionNoFlush(string text, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char[] array;
			char* ptr3;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr3 = null;
			}
			else
			{
				ptr3 = &array[0];
			}
			char* ptr4 = ptr2;
			char* ptr5 = ptr2 + count;
			char* ptr6 = ptr4;
			char* ptr7 = ptr3 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr8 = ptr7 + (long)(ptr5 - ptr4) * 2L / 2L;
				if (ptr8 != ptr3 + this.bufLen)
				{
					ptr8 = ptr3 + this.bufLen;
				}
				while (ptr7 < ptr8 && (this.xmlCharType.charProperties[num = (int)(*ptr4)] & 128) != 0 && num != 93)
				{
					*ptr7 = (char)num;
					ptr7++;
					ptr4++;
				}
				if (ptr4 >= ptr5)
				{
					goto IL_298;
				}
				if (ptr7 >= ptr8)
				{
					break;
				}
				if (num <= 39)
				{
					switch (num)
					{
					case 9:
						goto IL_21D;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_21;
						}
						*ptr7 = (char)num;
						ptr7++;
						goto IL_28D;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_19;
						}
						*ptr7 = (char)num;
						ptr7++;
						goto IL_28D;
					default:
						if (num == 34 || num - 38 <= 1)
						{
							goto IL_21D;
						}
						break;
					}
				}
				else
				{
					if (num == 60)
					{
						goto IL_21D;
					}
					if (num == 62)
					{
						if (this.hadDoubleBracket && ptr7[-1] == ']')
						{
							ptr7 = XmlEncodedRawTextWriter.RawEndCData(ptr7);
							ptr7 = XmlEncodedRawTextWriter.RawStartCData(ptr7);
						}
						*ptr7 = '>';
						ptr7++;
						goto IL_28D;
					}
					if (num == 93)
					{
						if (ptr7[-1] == ']')
						{
							this.hadDoubleBracket = true;
						}
						else
						{
							this.hadDoubleBracket = false;
						}
						*ptr7 = ']';
						ptr7++;
						goto IL_28D;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr7 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr4, ptr5, ptr7);
					ptr4 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr7 = this.InvalidXmlChar(num, ptr7, false);
					ptr4++;
					continue;
				}
				*ptr7 = (char)num;
				ptr7++;
				ptr4++;
				continue;
				IL_28D:
				ptr4++;
				continue;
				IL_21D:
				*ptr7 = (char)num;
				ptr7++;
				goto IL_28D;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			return (int)((long)(ptr4 - ptr6));
			Block_19:
			if (ptr4[1] == '\n')
			{
				ptr4++;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr6));
			Block_21:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr6));
			IL_298:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			array = null;
			return -1;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		protected Task WriteCDataSectionAsync(string text)
		{
			XmlEncodedRawTextWriter.<WriteCDataSectionAsync>d__148 <WriteCDataSectionAsync>d__;
			<WriteCDataSectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataSectionAsync>d__.<>4__this = this;
			<WriteCDataSectionAsync>d__.text = text;
			<WriteCDataSectionAsync>d__.<>1__state = -1;
			<WriteCDataSectionAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCDataSectionAsync>d__148>(ref <WriteCDataSectionAsync>d__);
			return <WriteCDataSectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040002F7 RID: 759
		private readonly bool useAsync;

		// Token: 0x040002F8 RID: 760
		protected byte[] bufBytes;

		// Token: 0x040002F9 RID: 761
		protected Stream stream;

		// Token: 0x040002FA RID: 762
		protected Encoding encoding;

		// Token: 0x040002FB RID: 763
		protected XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040002FC RID: 764
		protected int bufPos = 1;

		// Token: 0x040002FD RID: 765
		protected int textPos = 1;

		// Token: 0x040002FE RID: 766
		protected int contentPos;

		// Token: 0x040002FF RID: 767
		protected int cdataPos;

		// Token: 0x04000300 RID: 768
		protected int attrEndPos;

		// Token: 0x04000301 RID: 769
		protected int bufLen = 6144;

		// Token: 0x04000302 RID: 770
		protected bool writeToNull;

		// Token: 0x04000303 RID: 771
		protected bool hadDoubleBracket;

		// Token: 0x04000304 RID: 772
		protected bool inAttributeValue;

		// Token: 0x04000305 RID: 773
		protected int bufBytesUsed;

		// Token: 0x04000306 RID: 774
		protected char[] bufChars;

		// Token: 0x04000307 RID: 775
		protected Encoder encoder;

		// Token: 0x04000308 RID: 776
		protected TextWriter writer;

		// Token: 0x04000309 RID: 777
		protected bool trackTextContent;

		// Token: 0x0400030A RID: 778
		protected bool inTextContent;

		// Token: 0x0400030B RID: 779
		private int lastMarkPos;

		// Token: 0x0400030C RID: 780
		private int[] textContentMarks;

		// Token: 0x0400030D RID: 781
		private CharEntityEncoderFallback charEntityFallback;

		// Token: 0x0400030E RID: 782
		protected NewLineHandling newLineHandling;

		// Token: 0x0400030F RID: 783
		protected bool closeOutput;

		// Token: 0x04000310 RID: 784
		protected bool omitXmlDeclaration;

		// Token: 0x04000311 RID: 785
		protected string newLineChars;

		// Token: 0x04000312 RID: 786
		protected bool checkCharacters;

		// Token: 0x04000313 RID: 787
		protected XmlStandalone standalone;

		// Token: 0x04000314 RID: 788
		protected XmlOutputMethod outputMethod;

		// Token: 0x04000315 RID: 789
		protected bool autoXmlDeclaration;

		// Token: 0x04000316 RID: 790
		protected bool mergeCDataSections;

		// Token: 0x04000317 RID: 791
		private const int BUFSIZE = 6144;

		// Token: 0x04000318 RID: 792
		private const int ASYNCBUFSIZE = 65536;

		// Token: 0x04000319 RID: 793
		private const int OVERFLOW = 32;

		// Token: 0x0400031A RID: 794
		private const int INIT_MARKS_COUNT = 64;
	}
}
