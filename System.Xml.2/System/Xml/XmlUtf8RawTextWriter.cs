using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000DD RID: 221
	internal class XmlUtf8RawTextWriter : XmlRawWriter
	{
		// Token: 0x06000CE7 RID: 3303 RVA: 0x00036754 File Offset: 0x00034954
		protected XmlUtf8RawTextWriter(XmlWriterSettings settings)
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

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0003681C File Offset: 0x00034A1C
		public XmlUtf8RawTextWriter(Stream stream, XmlWriterSettings settings) : this(settings)
		{
			this.stream = stream;
			this.encoding = settings.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufBytes = new byte[this.bufLen + 32];
			if (!stream.CanSeek || stream.Position == 0L)
			{
				byte[] preamble = this.encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					Buffer.BlockCopy(preamble, 0, this.bufBytes, 1, preamble.Length);
					this.bufPos += preamble.Length;
					this.textPos += preamble.Length;
				}
			}
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x000368D8 File Offset: 0x00034AD8
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

		// Token: 0x06000CEA RID: 3306 RVA: 0x00036968 File Offset: 0x00034B68
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
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

		// Token: 0x06000CEB RID: 3307 RVA: 0x000369EE File Offset: 0x00034BEE
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				this.WriteProcessingInstruction("xml", xmldecl);
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00036A0C File Offset: 0x00034C0C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
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
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 34;
			}
			else if (sysid != null)
			{
				this.RawText(" SYSTEM \"");
				this.RawText(sysid);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			else
			{
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
			}
			if (subset != null)
			{
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 91;
				this.RawText(subset);
				byte[] array5 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 93;
			}
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 62;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00036B18 File Offset: 0x00034D18
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00036B80 File Offset: 0x00034D80
		internal override void StartElementContent()
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 62;
			this.contentPos = this.bufPos;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00036BB4 File Offset: 0x00034DB4
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			int num;
			if (this.contentPos != this.bufPos)
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				if (prefix != null && prefix.Length != 0)
				{
					this.RawText(prefix);
					byte[] array3 = this.bufBytes;
					num = this.bufPos;
					this.bufPos = num + 1;
					array3[num] = 58;
				}
				this.RawText(localName);
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 62;
				return;
			}
			this.bufPos--;
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 32;
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 47;
			byte[] array7 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00036CB0 File Offset: 0x00034EB0
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 58;
			}
			this.RawText(localName);
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 62;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00036D40 File Offset: 0x00034F40
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			int num;
			if (this.attrEndPos == this.bufPos)
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			if (prefix != null && prefix.Length > 0)
			{
				this.RawText(prefix);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 61;
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00036DE4 File Offset: 0x00034FE4
		public override void WriteEndAttribute()
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00036E1E File Offset: 0x0003501E
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(namespaceName);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00036E34 File Offset: 0x00035034
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00036E38 File Offset: 0x00035038
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			if (prefix.Length == 0)
			{
				this.RawText(" xmlns=\"");
			}
			else
			{
				this.RawText(" xmlns:");
				this.RawText(prefix);
				byte[] array = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 61;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00036EA8 File Offset: 0x000350A8
		internal override void WriteEndNamespaceDeclaration()
		{
			this.inAttributeValue = false;
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00036EE4 File Offset: 0x000350E4
		public override void WriteCData(string text)
		{
			int num;
			if (this.mergeCDataSections && this.bufPos == this.cdataPos)
			{
				this.bufPos -= 3;
			}
			else
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 33;
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 91;
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 67;
				byte[] array5 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 68;
				byte[] array6 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array6[num] = 65;
				byte[] array7 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array7[num] = 84;
				byte[] array8 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array8[num] = 65;
				byte[] array9 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array9[num] = 91;
			}
			this.WriteCDataSection(text);
			byte[] array10 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array10[num] = 93;
			byte[] array11 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array11[num] = 93;
			byte[] array12 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array12[num] = 62;
			this.textPos = this.bufPos;
			this.cdataPos = this.bufPos;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00037074 File Offset: 0x00035274
		public override void WriteComment(string text)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 33;
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 45;
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 45;
			this.WriteCommentOrPi(text, 45);
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 45;
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 45;
			byte[] array7 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00037140 File Offset: 0x00035340
		public override void WriteProcessingInstruction(string name, string text)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 63;
			this.RawText(name);
			if (text.Length > 0)
			{
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
				this.WriteCommentOrPi(text, 63);
			}
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 63;
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 62;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000371E8 File Offset: 0x000353E8
		public override void WriteEntityRef(string name)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			this.RawText(name);
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00037250 File Offset: 0x00035450
		public override void WriteCharEntity(char ch)
		{
			int num = (int)ch;
			string s = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch, '\0');
			}
			byte[] array = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 35;
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 120;
			this.RawText(s);
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00037320 File Offset: 0x00035520
		public unsafe override void WriteWhitespace(string ws)
		{
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

		// Token: 0x06000CFD RID: 3325 RVA: 0x00037368 File Offset: 0x00035568
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
					this.WriteAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000373B0 File Offset: 0x000355B0
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			byte[] array = this.bufBytes;
			int num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array[num2] = 38;
			byte[] array2 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array2[num2] = 35;
			byte[] array3 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array3[num2] = 120;
			this.RawText(num.ToString("X", NumberFormatInfo.InvariantInfo));
			byte[] array4 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array4[num2] = 59;
			this.textPos = this.bufPos;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00037450 File Offset: 0x00035650
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
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

		// Token: 0x06000D00 RID: 3328 RVA: 0x00037490 File Offset: 0x00035690
		public unsafe override void WriteRaw(char[] buffer, int index, int count)
		{
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				this.WriteRawWithCharChecking(ptr2, ptr2 + count);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000374C4 File Offset: 0x000356C4
		public unsafe override void WriteRaw(string data)
		{
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

		// Token: 0x06000D02 RID: 3330 RVA: 0x00037504 File Offset: 0x00035704
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
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00037584 File Offset: 0x00035784
		public override void Flush()
		{
			this.FlushBuffer();
			this.FlushEncoder();
			if (this.stream != null)
			{
				this.stream.Flush();
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000375A8 File Offset: 0x000357A8
		protected virtual void FlushBuffer()
		{
			try
			{
				if (!this.writeToNull)
				{
					this.stream.Write(this.bufBytes, 1, this.bufPos - 1);
				}
			}
			catch
			{
				this.writeToNull = true;
				throw;
			}
			finally
			{
				this.bufBytes[0] = this.bufBytes[this.bufPos - 1];
				if (XmlUtf8RawTextWriter.IsSurrogateByte(this.bufBytes[0]))
				{
					this.bufBytes[1] = this.bufBytes[this.bufPos];
					this.bufBytes[2] = this.bufBytes[this.bufPos + 1];
					this.bufBytes[3] = this.bufBytes[this.bufPos + 2];
				}
				this.textPos = ((this.textPos == this.bufPos) ? 1 : 0);
				this.attrEndPos = ((this.attrEndPos == this.bufPos) ? 1 : 0);
				this.contentPos = 0;
				this.cdataPos = 0;
				this.bufPos = 1;
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000376B4 File Offset: 0x000358B4
		private void FlushEncoder()
		{
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000376B8 File Offset: 0x000358B8
		protected unsafe void WriteAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
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
			int num = 0;
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr2 = (byte)num;
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
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_1CD;
							}
							ptr2 = XmlUtf8RawTextWriter.TabEntity(ptr2);
							goto IL_1CD;
						case 10:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_1CD;
							}
							ptr2 = XmlUtf8RawTextWriter.LineFeedEntity(ptr2);
							goto IL_1CD;
						case 11:
						case 12:
							break;
						case 13:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_1CD;
							}
							ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_1CD;
						default:
							if (num == 34)
							{
								ptr2 = XmlUtf8RawTextWriter.QuoteEntity(ptr2);
								goto IL_1CD;
							}
							if (num == 38)
							{
								ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
								goto IL_1CD;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							*ptr2 = (byte)num;
							ptr2++;
							goto IL_1CD;
						}
						if (num == 60)
						{
							ptr2 = XmlUtf8RawTextWriter.LtEntity(ptr2);
							goto IL_1CD;
						}
						if (num == 62)
						{
							ptr2 = XmlUtf8RawTextWriter.GtEntity(ptr2);
							goto IL_1CD;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					pSrc++;
					continue;
					IL_1CD:
					pSrc++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000378AC File Offset: 0x00035AAC
		protected unsafe void WriteElementTextBlock(char* pSrc, char* pSrcEnd)
		{
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
			int num = 0;
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr2 = (byte)num;
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
							goto IL_109;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr2 = this.WriteNewLine(ptr2);
								goto IL_1D0;
							}
							*ptr2 = (byte)num;
							ptr2++;
							goto IL_1D0;
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
								goto IL_1D0;
							case NewLineHandling.Entitize:
								ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
								goto IL_1D0;
							case NewLineHandling.None:
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_1D0;
							default:
								goto IL_1D0;
							}
							break;
						default:
							if (num == 34)
							{
								goto IL_109;
							}
							if (num == 38)
							{
								ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
								goto IL_1D0;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							goto IL_109;
						}
						if (num == 60)
						{
							ptr2 = XmlUtf8RawTextWriter.LtEntity(ptr2);
							goto IL_1D0;
						}
						if (num == 62)
						{
							ptr2 = XmlUtf8RawTextWriter.GtEntity(ptr2);
							goto IL_1D0;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					pSrc++;
					continue;
					IL_1D0:
					pSrc++;
					continue;
					IL_109:
					*ptr2 = (byte)num;
					ptr2++;
					goto IL_1D0;
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

		// Token: 0x06000D08 RID: 3336 RVA: 0x00037AB8 File Offset: 0x00035CB8
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

		// Token: 0x06000D09 RID: 3337 RVA: 0x00037AEC File Offset: 0x00035CEC
		protected unsafe void RawText(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr2 + (long)(pSrcEnd - ptr3);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) <= 127)
				{
					ptr3++;
					*ptr2 = (byte)num;
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
					ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00037BE4 File Offset: 0x00035DE4
		protected unsafe void WriteRawWithCharChecking(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr2 = pSrcBegin;
			byte* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - ptr2);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
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
							goto IL_DA;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr3 = this.WriteNewLine(ptr3);
								goto IL_181;
							}
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_181;
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
								goto IL_181;
							}
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_181;
						default:
							if (num == 38)
							{
								goto IL_DA;
							}
							break;
						}
					}
					else if (num == 60 || num == 93)
					{
						goto IL_DA;
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
						ptr2 += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr3 = this.InvalidXmlChar(num, ptr3, false);
						ptr2++;
						continue;
					}
					ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
					ptr2++;
					continue;
					IL_181:
					ptr2++;
					continue;
					IL_DA:
					*ptr3 = (byte)num;
					ptr3++;
					goto IL_181;
				}
				this.bufPos = (int)((long)(ptr3 - ptr));
				this.FlushBuffer();
				ptr3 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00037D8C File Offset: 0x00035F8C
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
				byte[] array;
				byte* ptr2;
				if ((array = this.bufBytes) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				byte* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					byte* ptr6 = ptr5 + (long)(ptr4 - ptr3);
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 64) != 0 && num != stopChar && num <= 127)
					{
						*ptr5 = (byte)num;
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
								goto IL_221;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_290;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_290;
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
									goto IL_290;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_290;
							default:
								if (num == 38)
								{
									goto IL_221;
								}
								if (num == 45)
								{
									*ptr5 = 45;
									ptr5++;
									if (num == stopChar && (ptr3 + 1 == ptr4 || ptr3[1] == '-'))
									{
										*ptr5 = 32;
										ptr5++;
										goto IL_290;
									}
									goto IL_290;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_221;
							}
							if (num != 63)
							{
								if (num == 93)
								{
									*ptr5 = 93;
									ptr5++;
									goto IL_290;
								}
							}
							else
							{
								*ptr5 = 63;
								ptr5++;
								if (num == stopChar && ptr3 + 1 < ptr4 && ptr3[1] == '>')
								{
									*ptr5 = 32;
									ptr5++;
									goto IL_290;
								}
								goto IL_290;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						ptr5 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr5);
						ptr3++;
						continue;
						IL_290:
						ptr3++;
						continue;
						IL_221:
						*ptr5 = (byte)num;
						ptr5++;
						goto IL_290;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00038048 File Offset: 0x00036248
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
				byte[] array;
				byte* ptr2;
				if ((array = this.bufBytes) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				byte* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					byte* ptr6 = ptr5 + (long)(ptr4 - ptr3);
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 128) != 0 && num != 93 && num <= 127)
					{
						*ptr5 = (byte)num;
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
								goto IL_205;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_274;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_274;
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
									goto IL_274;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_274;
							default:
								if (num == 34 || num - 38 <= 1)
								{
									goto IL_205;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_205;
							}
							if (num == 62)
							{
								if (this.hadDoubleBracket && ptr5[-1] == 93)
								{
									ptr5 = XmlUtf8RawTextWriter.RawEndCData(ptr5);
									ptr5 = XmlUtf8RawTextWriter.RawStartCData(ptr5);
								}
								*ptr5 = 62;
								ptr5++;
								goto IL_274;
							}
							if (num == 93)
							{
								if (ptr5[-1] == 93)
								{
									this.hadDoubleBracket = true;
								}
								else
								{
									this.hadDoubleBracket = false;
								}
								*ptr5 = 93;
								ptr5++;
								goto IL_274;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						ptr5 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr5);
						ptr3++;
						continue;
						IL_274:
						ptr3++;
						continue;
						IL_205:
						*ptr5 = (byte)num;
						ptr5++;
						goto IL_274;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000382E6 File Offset: 0x000364E6
		private static bool IsSurrogateByte(byte b)
		{
			return (b & 248) == 240;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000382F8 File Offset: 0x000364F8
		private unsafe static byte* EncodeSurrogate(char* pSrc, char* pSrcEnd, byte* pDst)
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
				num = XmlCharType.CombineSurrogateChar(num2, num);
				*pDst = (byte)(240 | num >> 18);
				pDst[1] = (byte)(128 | (num >> 12 & 63));
				pDst[2] = (byte)(128 | (num >> 6 & 63));
				pDst[3] = (byte)(128 | (num & 63));
				pDst += 4;
				return pDst;
			}
			throw XmlConvert.CreateInvalidSurrogatePairException((char)num2, (char)num);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000383A4 File Offset: 0x000365A4
		private unsafe byte* InvalidXmlChar(int ch, byte* pDst, bool entitize)
		{
			if (this.checkCharacters)
			{
				throw XmlConvert.CreateInvalidCharException((char)ch, '\0');
			}
			if (entitize)
			{
				return XmlUtf8RawTextWriter.CharEntity(pDst, (char)ch);
			}
			if (ch < 128)
			{
				*pDst = (byte)ch;
				pDst++;
			}
			else
			{
				pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(ch, pDst);
			}
			return pDst;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000383E0 File Offset: 0x000365E0
		internal unsafe void EncodeChar(ref char* pSrc, char* pSrcEnd, ref byte* pDst)
		{
			int num = (int)(*pSrc);
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			if (num <= 127 || num >= 65534)
			{
				pDst = this.InvalidXmlChar(num, pDst, false);
				pSrc += 2;
				return;
			}
			pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, pDst);
			pSrc += 2;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00038440 File Offset: 0x00036640
		internal unsafe static byte* EncodeMultibyteUTF8(int ch, byte* pDst)
		{
			if (ch < 2048)
			{
				*pDst = (byte)(-64 | ch >> 6);
			}
			else
			{
				*pDst = (byte)(-32 | ch >> 12);
				pDst++;
				*pDst = (byte)(-128 | (ch >> 6 & 63));
			}
			pDst++;
			*pDst = (byte)(128 | (ch & 63));
			return pDst + 1;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00038490 File Offset: 0x00036690
		internal unsafe static void CharToUTF8(ref char* pSrc, char* pSrcEnd, ref byte* pDst)
		{
			int num = (int)(*pSrc);
			if (num <= 127)
			{
				*pDst = (byte)num;
				pDst++;
				pSrc += 2;
				return;
			}
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, pDst);
			pSrc += 2;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x000384E8 File Offset: 0x000366E8
		protected unsafe byte* WriteNewLine(byte* pDst)
		{
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
			this.bufPos = (int)((long)(pDst - ptr));
			this.RawText(this.newLineChars);
			return ptr + this.bufPos;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00038533 File Offset: 0x00036733
		protected unsafe static byte* LtEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 108;
			pDst[2] = 116;
			pDst[3] = 59;
			return pDst + 4;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003854E File Offset: 0x0003674E
		protected unsafe static byte* GtEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 103;
			pDst[2] = 116;
			pDst[3] = 59;
			return pDst + 4;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00038569 File Offset: 0x00036769
		protected unsafe static byte* AmpEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 97;
			pDst[2] = 109;
			pDst[3] = 112;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003858A File Offset: 0x0003678A
		protected unsafe static byte* QuoteEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 113;
			pDst[2] = 117;
			pDst[3] = 111;
			pDst[4] = 116;
			pDst[5] = 59;
			return pDst + 6;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000385B1 File Offset: 0x000367B1
		protected unsafe static byte* TabEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 57;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000385D2 File Offset: 0x000367D2
		protected unsafe static byte* LineFeedEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 65;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000385F3 File Offset: 0x000367F3
		protected unsafe static byte* CarriageReturnEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 68;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00038614 File Offset: 0x00036814
		private unsafe static byte* CharEntity(byte* pDst, char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst += 3;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while ((*(pDst++) = (byte)(*(ptr2++))) != 0)
				{
				}
			}
			pDst[-1] = 59;
			return pDst;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003867D File Offset: 0x0003687D
		protected unsafe static byte* RawStartCData(byte* pDst)
		{
			*pDst = 60;
			pDst[1] = 33;
			pDst[2] = 91;
			pDst[3] = 67;
			pDst[4] = 68;
			pDst[5] = 65;
			pDst[6] = 84;
			pDst[7] = 65;
			pDst[8] = 91;
			return pDst + 9;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000386B7 File Offset: 0x000368B7
		protected unsafe static byte* RawEndCData(byte* pDst)
		{
			*pDst = 93;
			pDst[1] = 93;
			pDst[2] = 62;
			return pDst + 3;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000386CC File Offset: 0x000368CC
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

		// Token: 0x06000D1F RID: 3359 RVA: 0x00038826 File Offset: 0x00036A26
		protected void CheckAsyncCall()
		{
			if (!this.useAsync)
			{
				throw new InvalidOperationException(Res.GetString("Xml_WriterAsyncNotSetException"));
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00038840 File Offset: 0x00036A40
		internal override Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86 <WriteXmlDeclarationAsync>d__;
			<WriteXmlDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteXmlDeclarationAsync>d__.<>4__this = this;
			<WriteXmlDeclarationAsync>d__.standalone = standalone;
			<WriteXmlDeclarationAsync>d__.<>1__state = -1;
			<WriteXmlDeclarationAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref <WriteXmlDeclarationAsync>d__);
			return <WriteXmlDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003888B File Offset: 0x00036A8B
		internal override Task WriteXmlDeclarationAsync(string xmldecl)
		{
			this.CheckAsyncCall();
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				return this.WriteProcessingInstructionAsync("xml", xmldecl);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000388B8 File Offset: 0x00036AB8
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0003891C File Offset: 0x00036B1C
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			byte[] array = this.bufBytes;
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

		// Token: 0x06000D24 RID: 3364 RVA: 0x00038983 File Offset: 0x00036B83
		private void WriteStartElementAsync_SetAttEndPos()
		{
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00038994 File Offset: 0x00036B94
		internal override Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			int num;
			if (this.contentPos == this.bufPos)
			{
				this.bufPos--;
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 62;
				return AsyncHelper.DoneTask;
			}
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 60;
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				return this.RawTextAsync(prefix + ":" + localName + ">");
			}
			return this.RawTextAsync(localName + ">");
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00038A80 File Offset: 0x00036C80
		internal override Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				return this.RawTextAsync(prefix + ":" + localName + ">");
			}
			return this.RawTextAsync(localName + ">");
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00038AFC File Offset: 0x00036CFC
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.attrEndPos == this.bufPos)
			{
				byte[] array = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			Task task;
			if (prefix != null && prefix.Length > 0)
			{
				task = this.RawTextAsync(prefix + ":" + localName + "=\"");
			}
			else
			{
				task = this.RawTextAsync(localName + "=\"");
			}
			return task.CallVoidFuncWhenFinish(new Action(this.WriteStartAttribute_SetInAttribute));
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00038B81 File Offset: 0x00036D81
		private void WriteStartAttribute_SetInAttribute()
		{
			this.inAttributeValue = true;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00038B8C File Offset: 0x00036D8C
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsyncCall();
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00038BD4 File Offset: 0x00036DD4
		internal override Task WriteNamespaceDeclarationAsync(string prefix, string namespaceName)
		{
			XmlUtf8RawTextWriter.<WriteNamespaceDeclarationAsync>d__96 <WriteNamespaceDeclarationAsync>d__;
			<WriteNamespaceDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNamespaceDeclarationAsync>d__.<>4__this = this;
			<WriteNamespaceDeclarationAsync>d__.prefix = prefix;
			<WriteNamespaceDeclarationAsync>d__.namespaceName = namespaceName;
			<WriteNamespaceDeclarationAsync>d__.<>1__state = -1;
			<WriteNamespaceDeclarationAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteNamespaceDeclarationAsync>d__96>(ref <WriteNamespaceDeclarationAsync>d__);
			return <WriteNamespaceDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00038C28 File Offset: 0x00036E28
		internal override Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			XmlUtf8RawTextWriter.<WriteStartNamespaceDeclarationAsync>d__97 <WriteStartNamespaceDeclarationAsync>d__;
			<WriteStartNamespaceDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartNamespaceDeclarationAsync>d__.<>4__this = this;
			<WriteStartNamespaceDeclarationAsync>d__.prefix = prefix;
			<WriteStartNamespaceDeclarationAsync>d__.<>1__state = -1;
			<WriteStartNamespaceDeclarationAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteStartNamespaceDeclarationAsync>d__97>(ref <WriteStartNamespaceDeclarationAsync>d__);
			return <WriteStartNamespaceDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00038C74 File Offset: 0x00036E74
		internal override Task WriteEndNamespaceDeclarationAsync()
		{
			this.CheckAsyncCall();
			this.inAttributeValue = false;
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00038CBC File Offset: 0x00036EBC
		public override Task WriteCDataAsync(string text)
		{
			XmlUtf8RawTextWriter.<WriteCDataAsync>d__99 <WriteCDataAsync>d__;
			<WriteCDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataAsync>d__.<>4__this = this;
			<WriteCDataAsync>d__.text = text;
			<WriteCDataAsync>d__.<>1__state = -1;
			<WriteCDataAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCDataAsync>d__99>(ref <WriteCDataAsync>d__);
			return <WriteCDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00038D08 File Offset: 0x00036F08
		public override Task WriteCommentAsync(string text)
		{
			XmlUtf8RawTextWriter.<WriteCommentAsync>d__100 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCommentAsync>d__100>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00038D54 File Offset: 0x00036F54
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			XmlUtf8RawTextWriter.<WriteProcessingInstructionAsync>d__101 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.name = name;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteProcessingInstructionAsync>d__101>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00038DA8 File Offset: 0x00036FA8
		public override Task WriteEntityRefAsync(string name)
		{
			XmlUtf8RawTextWriter.<WriteEntityRefAsync>d__102 <WriteEntityRefAsync>d__;
			<WriteEntityRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEntityRefAsync>d__.<>4__this = this;
			<WriteEntityRefAsync>d__.name = name;
			<WriteEntityRefAsync>d__.<>1__state = -1;
			<WriteEntityRefAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteEntityRefAsync>d__102>(ref <WriteEntityRefAsync>d__);
			return <WriteEntityRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00038DF4 File Offset: 0x00036FF4
		public override Task WriteCharEntityAsync(char ch)
		{
			XmlUtf8RawTextWriter.<WriteCharEntityAsync>d__103 <WriteCharEntityAsync>d__;
			<WriteCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCharEntityAsync>d__.<>4__this = this;
			<WriteCharEntityAsync>d__.ch = ch;
			<WriteCharEntityAsync>d__.<>1__state = -1;
			<WriteCharEntityAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCharEntityAsync>d__103>(ref <WriteCharEntityAsync>d__);
			return <WriteCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00038E3F File Offset: 0x0003703F
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(ws);
			}
			return this.WriteElementTextBlockAsync(ws);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00038E5E File Offset: 0x0003705E
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(text);
			}
			return this.WriteElementTextBlockAsync(text);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00038E80 File Offset: 0x00037080
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			XmlUtf8RawTextWriter.<WriteSurrogateCharEntityAsync>d__106 <WriteSurrogateCharEntityAsync>d__;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteSurrogateCharEntityAsync>d__.<>4__this = this;
			<WriteSurrogateCharEntityAsync>d__.lowChar = lowChar;
			<WriteSurrogateCharEntityAsync>d__.highChar = highChar;
			<WriteSurrogateCharEntityAsync>d__.<>1__state = -1;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteSurrogateCharEntityAsync>d__106>(ref <WriteSurrogateCharEntityAsync>d__);
			return <WriteSurrogateCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00038ED3 File Offset: 0x000370D3
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(buffer, index, count);
			}
			return this.WriteElementTextBlockAsync(buffer, index, count);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00038EF8 File Offset: 0x000370F8
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			XmlUtf8RawTextWriter.<WriteRawAsync>d__108 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.buffer = buffer;
			<WriteRawAsync>d__.index = index;
			<WriteRawAsync>d__.count = count;
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteRawAsync>d__108>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00038F54 File Offset: 0x00037154
		public override Task WriteRawAsync(string data)
		{
			XmlUtf8RawTextWriter.<WriteRawAsync>d__109 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.data = data;
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteRawAsync>d__109>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00038FA0 File Offset: 0x000371A0
		public override Task FlushAsync()
		{
			XmlUtf8RawTextWriter.<FlushAsync>d__110 <FlushAsync>d__;
			<FlushAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsync>d__.<>4__this = this;
			<FlushAsync>d__.<>1__state = -1;
			<FlushAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<FlushAsync>d__110>(ref <FlushAsync>d__);
			return <FlushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00038FE4 File Offset: 0x000371E4
		protected virtual Task FlushBufferAsync()
		{
			XmlUtf8RawTextWriter.<FlushBufferAsync>d__111 <FlushBufferAsync>d__;
			<FlushBufferAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushBufferAsync>d__.<>4__this = this;
			<FlushBufferAsync>d__.<>1__state = -1;
			<FlushBufferAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<FlushBufferAsync>d__111>(ref <FlushBufferAsync>d__);
			return <FlushBufferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00039027 File Offset: 0x00037227
		private Task FlushEncoderAsync()
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00039030 File Offset: 0x00037230
		[SecuritySafeCritical]
		protected unsafe int WriteAttributeTextBlockNoFlush(char* pSrc, char* pSrcEnd)
		{
			char* ptr = pSrc;
			byte[] array;
			byte* ptr2;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			byte* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - pSrc);
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_1E9;
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
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_1DF;
						}
						ptr3 = XmlUtf8RawTextWriter.TabEntity(ptr3);
						goto IL_1DF;
					case 10:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_1DF;
						}
						ptr3 = XmlUtf8RawTextWriter.LineFeedEntity(ptr3);
						goto IL_1DF;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_1DF;
						}
						ptr3 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr3);
						goto IL_1DF;
					default:
						if (num == 34)
						{
							ptr3 = XmlUtf8RawTextWriter.QuoteEntity(ptr3);
							goto IL_1DF;
						}
						if (num == 38)
						{
							ptr3 = XmlUtf8RawTextWriter.AmpEntity(ptr3);
							goto IL_1DF;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_1DF;
					}
					if (num == 60)
					{
						ptr3 = XmlUtf8RawTextWriter.LtEntity(ptr3);
						goto IL_1DF;
					}
					if (num == 62)
					{
						ptr3 = XmlUtf8RawTextWriter.GtEntity(ptr3);
						goto IL_1DF;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				pSrc++;
				continue;
				IL_1DF:
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			IL_1E9:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			array = null;
			return -1;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00039238 File Offset: 0x00037438
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

		// Token: 0x06000D3D RID: 3389 RVA: 0x00039268 File Offset: 0x00037468
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

		// Token: 0x06000D3E RID: 3390 RVA: 0x000392A0 File Offset: 0x000374A0
		protected Task WriteAttributeTextBlockAsync(char[] chars, int index, int count)
		{
			XmlUtf8RawTextWriter.<WriteAttributeTextBlockAsync>d__116 <WriteAttributeTextBlockAsync>d__;
			<WriteAttributeTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAttributeTextBlockAsync>d__.<>4__this = this;
			<WriteAttributeTextBlockAsync>d__.chars = chars;
			<WriteAttributeTextBlockAsync>d__.index = index;
			<WriteAttributeTextBlockAsync>d__.count = count;
			<WriteAttributeTextBlockAsync>d__.<>1__state = -1;
			<WriteAttributeTextBlockAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteAttributeTextBlockAsync>d__116>(ref <WriteAttributeTextBlockAsync>d__);
			return <WriteAttributeTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000392FC File Offset: 0x000374FC
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

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003933C File Offset: 0x0003753C
		private Task _WriteAttributeTextBlockAsync(string text, int curIndex, int leftCount)
		{
			XmlUtf8RawTextWriter.<_WriteAttributeTextBlockAsync>d__118 <_WriteAttributeTextBlockAsync>d__;
			<_WriteAttributeTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_WriteAttributeTextBlockAsync>d__.<>4__this = this;
			<_WriteAttributeTextBlockAsync>d__.text = text;
			<_WriteAttributeTextBlockAsync>d__.curIndex = curIndex;
			<_WriteAttributeTextBlockAsync>d__.leftCount = leftCount;
			<_WriteAttributeTextBlockAsync>d__.<>1__state = -1;
			<_WriteAttributeTextBlockAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<_WriteAttributeTextBlockAsync>d__118>(ref <_WriteAttributeTextBlockAsync>d__);
			return <_WriteAttributeTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00039398 File Offset: 0x00037598
		[SecuritySafeCritical]
		protected unsafe int WriteElementTextBlockNoFlush(char* pSrc, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			char* ptr = pSrc;
			byte[] array;
			byte* ptr2;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			byte* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - pSrc);
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_20A;
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
						goto IL_115;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_14;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_200;
					case 11:
					case 12:
						break;
					case 13:
						switch (this.newLineHandling)
						{
						case NewLineHandling.Replace:
							goto IL_171;
						case NewLineHandling.Entitize:
							ptr3 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr3);
							goto IL_200;
						case NewLineHandling.None:
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_200;
						default:
							goto IL_200;
						}
						break;
					default:
						if (num == 34)
						{
							goto IL_115;
						}
						if (num == 38)
						{
							ptr3 = XmlUtf8RawTextWriter.AmpEntity(ptr3);
							goto IL_200;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						goto IL_115;
					}
					if (num == 60)
					{
						ptr3 = XmlUtf8RawTextWriter.LtEntity(ptr3);
						goto IL_200;
					}
					if (num == 62)
					{
						ptr3 = XmlUtf8RawTextWriter.GtEntity(ptr3);
						goto IL_200;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				pSrc++;
				continue;
				IL_200:
				pSrc++;
				continue;
				IL_115:
				*ptr3 = (byte)num;
				ptr3++;
				goto IL_200;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			Block_14:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_171:
			if (pSrc[1] == '\n')
			{
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_20A:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
			return -1;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000395D4 File Offset: 0x000377D4
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

		// Token: 0x06000D43 RID: 3395 RVA: 0x00039610 File Offset: 0x00037810
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

		// Token: 0x06000D44 RID: 3396 RVA: 0x00039658 File Offset: 0x00037858
		protected Task WriteElementTextBlockAsync(char[] chars, int index, int count)
		{
			XmlUtf8RawTextWriter.<WriteElementTextBlockAsync>d__122 <WriteElementTextBlockAsync>d__;
			<WriteElementTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteElementTextBlockAsync>d__.<>4__this = this;
			<WriteElementTextBlockAsync>d__.chars = chars;
			<WriteElementTextBlockAsync>d__.index = index;
			<WriteElementTextBlockAsync>d__.count = count;
			<WriteElementTextBlockAsync>d__.<>1__state = -1;
			<WriteElementTextBlockAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteElementTextBlockAsync>d__122>(ref <WriteElementTextBlockAsync>d__);
			return <WriteElementTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000396B4 File Offset: 0x000378B4
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

		// Token: 0x06000D46 RID: 3398 RVA: 0x00039704 File Offset: 0x00037904
		private Task _WriteElementTextBlockAsync(bool newLine, string text, int curIndex, int leftCount)
		{
			XmlUtf8RawTextWriter.<_WriteElementTextBlockAsync>d__124 <_WriteElementTextBlockAsync>d__;
			<_WriteElementTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_WriteElementTextBlockAsync>d__.<>4__this = this;
			<_WriteElementTextBlockAsync>d__.newLine = newLine;
			<_WriteElementTextBlockAsync>d__.text = text;
			<_WriteElementTextBlockAsync>d__.curIndex = curIndex;
			<_WriteElementTextBlockAsync>d__.leftCount = leftCount;
			<_WriteElementTextBlockAsync>d__.<>1__state = -1;
			<_WriteElementTextBlockAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<_WriteElementTextBlockAsync>d__124>(ref <_WriteElementTextBlockAsync>d__);
			return <_WriteElementTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00039768 File Offset: 0x00037968
		[SecuritySafeCritical]
		protected unsafe int RawTextNoFlush(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr2 + (long)(pSrcEnd - ptr3);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) <= 127)
				{
					ptr3++;
					*ptr2 = (byte)num;
					ptr2++;
				}
				if (ptr3 >= pSrcEnd)
				{
					goto IL_E7;
				}
				if (ptr2 >= ptr4)
				{
					break;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			return (int)((long)(ptr3 - pSrcBegin));
			IL_E7:
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003986C File Offset: 0x00037A6C
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

		// Token: 0x06000D49 RID: 3401 RVA: 0x000398A4 File Offset: 0x00037AA4
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

		// Token: 0x06000D4A RID: 3402 RVA: 0x000398E4 File Offset: 0x00037AE4
		private Task _RawTextAsync(string text, int curIndex, int leftCount)
		{
			XmlUtf8RawTextWriter.<_RawTextAsync>d__128 <_RawTextAsync>d__;
			<_RawTextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_RawTextAsync>d__.<>4__this = this;
			<_RawTextAsync>d__.text = text;
			<_RawTextAsync>d__.curIndex = curIndex;
			<_RawTextAsync>d__.leftCount = leftCount;
			<_RawTextAsync>d__.<>1__state = -1;
			<_RawTextAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<_RawTextAsync>d__128>(ref <_RawTextAsync>d__);
			return <_RawTextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00039940 File Offset: 0x00037B40
		[SecuritySafeCritical]
		protected unsafe int WriteRawWithCharCheckingNoFlush(char* pSrcBegin, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
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
			char* ptr2 = pSrcBegin;
			byte* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - ptr2);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					ptr2++;
				}
				if (ptr2 >= pSrcEnd)
				{
					goto IL_1C6;
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
						goto IL_E6;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_13;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_1BD;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_11;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_1BD;
					default:
						if (num == 38)
						{
							goto IL_E6;
						}
						break;
					}
				}
				else if (num == 60 || num == 93)
				{
					goto IL_E6;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
					ptr2 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, false);
					ptr2++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				ptr2++;
				continue;
				IL_1BD:
				ptr2++;
				continue;
				IL_E6:
				*ptr3 = (byte)num;
				ptr3++;
				goto IL_1BD;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			return (int)((long)(ptr2 - pSrcBegin));
			Block_11:
			if (ptr2[1] == '\n')
			{
				ptr2++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			Block_13:
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			IL_1C6:
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00039B24 File Offset: 0x00037D24
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

		// Token: 0x06000D4D RID: 3405 RVA: 0x00039B58 File Offset: 0x00037D58
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

		// Token: 0x06000D4E RID: 3406 RVA: 0x00039B98 File Offset: 0x00037D98
		protected Task WriteRawWithCharCheckingAsync(char[] chars, int index, int count)
		{
			XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__132 <WriteRawWithCharCheckingAsync>d__;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawWithCharCheckingAsync>d__.<>4__this = this;
			<WriteRawWithCharCheckingAsync>d__.chars = chars;
			<WriteRawWithCharCheckingAsync>d__.index = index;
			<WriteRawWithCharCheckingAsync>d__.count = count;
			<WriteRawWithCharCheckingAsync>d__.<>1__state = -1;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__132>(ref <WriteRawWithCharCheckingAsync>d__);
			return <WriteRawWithCharCheckingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00039BF4 File Offset: 0x00037DF4
		protected Task WriteRawWithCharCheckingAsync(string text)
		{
			XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__133 <WriteRawWithCharCheckingAsync>d__;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawWithCharCheckingAsync>d__.<>4__this = this;
			<WriteRawWithCharCheckingAsync>d__.text = text;
			<WriteRawWithCharCheckingAsync>d__.<>1__state = -1;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__133>(ref <WriteRawWithCharCheckingAsync>d__);
			return <WriteRawWithCharCheckingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00039C40 File Offset: 0x00037E40
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
			byte[] array;
			byte* ptr3;
			if ((array = this.bufBytes) == null || array.Length == 0)
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
			byte* ptr7 = ptr3 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr8 = ptr7 + (long)(ptr6 - ptr4);
				if (ptr8 != ptr3 + this.bufLen)
				{
					ptr8 = ptr3 + this.bufLen;
				}
				while (ptr7 < ptr8 && (this.xmlCharType.charProperties[num = (int)(*ptr4)] & 64) != 0 && num != stopChar && num <= 127)
				{
					*ptr7 = (byte)num;
					ptr7++;
					ptr4++;
				}
				if (ptr4 >= ptr6)
				{
					goto IL_2AA;
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
						goto IL_230;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_24;
						}
						*ptr7 = (byte)num;
						ptr7++;
						goto IL_29F;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_22;
						}
						*ptr7 = (byte)num;
						ptr7++;
						goto IL_29F;
					default:
						if (num == 38)
						{
							goto IL_230;
						}
						if (num == 45)
						{
							*ptr7 = 45;
							ptr7++;
							if (num == stopChar && (ptr4 + 1 == ptr6 || ptr4[1] == '-'))
							{
								*ptr7 = 32;
								ptr7++;
								goto IL_29F;
							}
							goto IL_29F;
						}
						break;
					}
				}
				else
				{
					if (num == 60)
					{
						goto IL_230;
					}
					if (num != 63)
					{
						if (num == 93)
						{
							*ptr7 = 93;
							ptr7++;
							goto IL_29F;
						}
					}
					else
					{
						*ptr7 = 63;
						ptr7++;
						if (num == stopChar && ptr4 + 1 < ptr6 && ptr4[1] == '>')
						{
							*ptr7 = 32;
							ptr7++;
							goto IL_29F;
						}
						goto IL_29F;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr7 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr4, ptr6, ptr7);
					ptr4 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr7 = this.InvalidXmlChar(num, ptr7, false);
					ptr4++;
					continue;
				}
				ptr7 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr7);
				ptr4++;
				continue;
				IL_29F:
				ptr4++;
				continue;
				IL_230:
				*ptr7 = (byte)num;
				ptr7++;
				goto IL_29F;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			return (int)((long)(ptr4 - ptr5));
			Block_22:
			if (ptr4[1] == '\n')
			{
				ptr4++;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr5));
			Block_24:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr5));
			IL_2AA:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			array = null;
			return -1;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00039F0C File Offset: 0x0003810C
		protected Task WriteCommentOrPiAsync(string text, int stopChar)
		{
			XmlUtf8RawTextWriter.<WriteCommentOrPiAsync>d__135 <WriteCommentOrPiAsync>d__;
			<WriteCommentOrPiAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentOrPiAsync>d__.<>4__this = this;
			<WriteCommentOrPiAsync>d__.text = text;
			<WriteCommentOrPiAsync>d__.stopChar = stopChar;
			<WriteCommentOrPiAsync>d__.<>1__state = -1;
			<WriteCommentOrPiAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCommentOrPiAsync>d__135>(ref <WriteCommentOrPiAsync>d__);
			return <WriteCommentOrPiAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00039F60 File Offset: 0x00038160
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
			byte[] array;
			byte* ptr3;
			if ((array = this.bufBytes) == null || array.Length == 0)
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
			byte* ptr7 = ptr3 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr8 = ptr7 + (long)(ptr5 - ptr4);
				if (ptr8 != ptr3 + this.bufLen)
				{
					ptr8 = ptr3 + this.bufLen;
				}
				while (ptr7 < ptr8 && (this.xmlCharType.charProperties[num = (int)(*ptr4)] & 128) != 0 && num != 93 && num <= 127)
				{
					*ptr7 = (byte)num;
					ptr7++;
					ptr4++;
				}
				if (ptr4 >= ptr5)
				{
					goto IL_28B;
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
						goto IL_211;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_22;
						}
						*ptr7 = (byte)num;
						ptr7++;
						goto IL_280;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_20;
						}
						*ptr7 = (byte)num;
						ptr7++;
						goto IL_280;
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
						if (this.hadDoubleBracket && ptr7[-1] == 93)
						{
							ptr7 = XmlUtf8RawTextWriter.RawEndCData(ptr7);
							ptr7 = XmlUtf8RawTextWriter.RawStartCData(ptr7);
						}
						*ptr7 = 62;
						ptr7++;
						goto IL_280;
					}
					if (num == 93)
					{
						if (ptr7[-1] == 93)
						{
							this.hadDoubleBracket = true;
						}
						else
						{
							this.hadDoubleBracket = false;
						}
						*ptr7 = 93;
						ptr7++;
						goto IL_280;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr7 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr4, ptr5, ptr7);
					ptr4 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr7 = this.InvalidXmlChar(num, ptr7, false);
					ptr4++;
					continue;
				}
				ptr7 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr7);
				ptr4++;
				continue;
				IL_280:
				ptr4++;
				continue;
				IL_211:
				*ptr7 = (byte)num;
				ptr7++;
				goto IL_280;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			return (int)((long)(ptr4 - ptr6));
			Block_20:
			if (ptr4[1] == '\n')
			{
				ptr4++;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr6));
			Block_22:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr6));
			IL_28B:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			array = null;
			return -1;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0003A20C File Offset: 0x0003840C
		protected Task WriteCDataSectionAsync(string text)
		{
			XmlUtf8RawTextWriter.<WriteCDataSectionAsync>d__137 <WriteCDataSectionAsync>d__;
			<WriteCDataSectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataSectionAsync>d__.<>4__this = this;
			<WriteCDataSectionAsync>d__.text = text;
			<WriteCDataSectionAsync>d__.<>1__state = -1;
			<WriteCDataSectionAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCDataSectionAsync>d__137>(ref <WriteCDataSectionAsync>d__);
			return <WriteCDataSectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040003EC RID: 1004
		private readonly bool useAsync;

		// Token: 0x040003ED RID: 1005
		protected byte[] bufBytes;

		// Token: 0x040003EE RID: 1006
		protected Stream stream;

		// Token: 0x040003EF RID: 1007
		protected Encoding encoding;

		// Token: 0x040003F0 RID: 1008
		protected XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040003F1 RID: 1009
		protected int bufPos = 1;

		// Token: 0x040003F2 RID: 1010
		protected int textPos = 1;

		// Token: 0x040003F3 RID: 1011
		protected int contentPos;

		// Token: 0x040003F4 RID: 1012
		protected int cdataPos;

		// Token: 0x040003F5 RID: 1013
		protected int attrEndPos;

		// Token: 0x040003F6 RID: 1014
		protected int bufLen = 6144;

		// Token: 0x040003F7 RID: 1015
		protected bool writeToNull;

		// Token: 0x040003F8 RID: 1016
		protected bool hadDoubleBracket;

		// Token: 0x040003F9 RID: 1017
		protected bool inAttributeValue;

		// Token: 0x040003FA RID: 1018
		protected NewLineHandling newLineHandling;

		// Token: 0x040003FB RID: 1019
		protected bool closeOutput;

		// Token: 0x040003FC RID: 1020
		protected bool omitXmlDeclaration;

		// Token: 0x040003FD RID: 1021
		protected string newLineChars;

		// Token: 0x040003FE RID: 1022
		protected bool checkCharacters;

		// Token: 0x040003FF RID: 1023
		protected XmlStandalone standalone;

		// Token: 0x04000400 RID: 1024
		protected XmlOutputMethod outputMethod;

		// Token: 0x04000401 RID: 1025
		protected bool autoXmlDeclaration;

		// Token: 0x04000402 RID: 1026
		protected bool mergeCDataSections;

		// Token: 0x04000403 RID: 1027
		private const int BUFSIZE = 6144;

		// Token: 0x04000404 RID: 1028
		private const int ASYNCBUFSIZE = 65536;

		// Token: 0x04000405 RID: 1029
		private const int OVERFLOW = 32;

		// Token: 0x04000406 RID: 1030
		private const int INIT_MARKS_COUNT = 64;
	}
}
