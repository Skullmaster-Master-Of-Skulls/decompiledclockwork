using System;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.fonts.cmaps;

namespace iTextSharp.text.pdf
{
	// Token: 0x020003A6 RID: 934
	public class CMapAwareDocumentFont : DocumentFont
	{
		// Token: 0x0600208D RID: 8333 RVA: 0x000C177C File Offset: 0x000C077C
		public CMapAwareDocumentFont(PRIndirectReference refFont) : base(refFont)
		{
			this.fontDic = (PdfDictionary)PdfReader.GetPdfObjectRelease(refFont);
			this.ProcessToUnicode();
			if (this.toUnicodeCmap == null)
			{
				this.ProcessUni2Byte();
			}
			this.spaceWidth = base.GetWidth(32);
			if (this.spaceWidth == 0)
			{
				this.spaceWidth = this.ComputeAverageWidth();
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000C17D8 File Offset: 0x000C07D8
		private void ProcessToUnicode()
		{
			PdfObject pdfObject = this.fontDic.Get(PdfName.TOUNICODE);
			if (pdfObject != null)
			{
				try
				{
					byte[] streamBytes = PdfReader.GetStreamBytes((PRStream)PdfReader.GetPdfObjectRelease(pdfObject));
					CMapParser cmapParser = new CMapParser();
					this.toUnicodeCmap = cmapParser.Parse(new MemoryStream(streamBytes));
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000C1838 File Offset: 0x000C0838
		private void ProcessUni2Byte()
		{
			IntHashtable uni2Byte = base.Uni2Byte;
			int[] array = uni2Byte.ToOrderedKeys();
			this.cidbyte2uni = new char[256];
			for (int i = 0; i < array.Length; i++)
			{
				int num = uni2Byte[array[i]];
				if (this.cidbyte2uni[num] == '\0')
				{
					this.cidbyte2uni[num] = (char)array[i];
				}
			}
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000C1894 File Offset: 0x000C0894
		private int ComputeAverageWidth()
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.widths.Length; i++)
			{
				if (this.widths[i] != 0)
				{
					num2 += this.widths[i];
					num++;
				}
			}
			if (num == 0)
			{
				return 0;
			}
			return num2 / num;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000C18D9 File Offset: 0x000C08D9
		public override int GetWidth(int char1)
		{
			if (char1 == 32)
			{
				return this.spaceWidth;
			}
			return base.GetWidth(char1);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000C18F0 File Offset: 0x000C08F0
		private string DecodeSingleCID(byte[] bytes, int offset, int len)
		{
			if (this.toUnicodeCmap != null)
			{
				if (offset + len > bytes.Length)
				{
					throw new IndexOutOfRangeException(MessageLocalization.GetComposedMessage("invalid.index.1", offset + len));
				}
				return this.toUnicodeCmap.Lookup(bytes, offset, len);
			}
			else
			{
				if (len == 1)
				{
					return new string(this.cidbyte2uni, (int)(byte.MaxValue & bytes[offset]), 1);
				}
				throw new ArgumentException("Multi-byte glyphs not implemented yet");
			}
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000C1958 File Offset: 0x000C0958
		public string Decode(byte[] cidbytes, int offset, int len)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = offset; i < offset + len; i++)
			{
				string text = this.DecodeSingleCID(cidbytes, i, 1);
				if (text == null && i < offset + len - 1)
				{
					text = this.DecodeSingleCID(cidbytes, i, 2);
					i++;
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000C19AA File Offset: 0x000C09AA
		public string Encode(byte[] bytes, int offset, int len)
		{
			return this.Decode(bytes, offset, len);
		}

		// Token: 0x04001660 RID: 5728
		private PdfDictionary fontDic;

		// Token: 0x04001661 RID: 5729
		private int spaceWidth;

		// Token: 0x04001662 RID: 5730
		private CMap toUnicodeCmap;

		// Token: 0x04001663 RID: 5731
		private char[] cidbyte2uni;
	}
}
