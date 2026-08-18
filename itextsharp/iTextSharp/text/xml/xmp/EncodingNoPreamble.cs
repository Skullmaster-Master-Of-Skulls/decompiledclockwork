using System;
using System.Text;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x02000585 RID: 1413
	public class EncodingNoPreamble : Encoding
	{
		// Token: 0x06003001 RID: 12289 RVA: 0x00127FB8 File Offset: 0x00126FB8
		public EncodingNoPreamble(Encoding encoding)
		{
			this.encoding = encoding;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x00127FC7 File Offset: 0x00126FC7
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return this.encoding.GetByteCount(chars, index, count);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x00127FD7 File Offset: 0x00126FD7
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return this.encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x00127FEB File Offset: 0x00126FEB
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.encoding.GetCharCount(bytes, index, count);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x00127FFB File Offset: 0x00126FFB
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x0012800F File Offset: 0x0012700F
		public override int GetMaxByteCount(int charCount)
		{
			return this.encoding.GetMaxByteCount(charCount);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x0012801D File Offset: 0x0012701D
		public override int GetMaxCharCount(int byteCount)
		{
			return this.encoding.GetMaxCharCount(byteCount);
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x0012802B File Offset: 0x0012702B
		public override string BodyName
		{
			get
			{
				return this.encoding.BodyName;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x00128038 File Offset: 0x00127038
		public override int CodePage
		{
			get
			{
				return this.encoding.CodePage;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x00128045 File Offset: 0x00127045
		public override string EncodingName
		{
			get
			{
				return this.encoding.EncodingName;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x00128052 File Offset: 0x00127052
		public override string HeaderName
		{
			get
			{
				return this.encoding.HeaderName;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x0012805F File Offset: 0x0012705F
		public override bool IsBrowserDisplay
		{
			get
			{
				return this.encoding.IsBrowserDisplay;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x0012806C File Offset: 0x0012706C
		public override bool IsBrowserSave
		{
			get
			{
				return this.encoding.IsBrowserSave;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x00128079 File Offset: 0x00127079
		public override bool IsMailNewsDisplay
		{
			get
			{
				return this.encoding.IsMailNewsDisplay;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x00128086 File Offset: 0x00127086
		public override bool IsMailNewsSave
		{
			get
			{
				return this.encoding.IsMailNewsSave;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06003010 RID: 12304 RVA: 0x00128093 File Offset: 0x00127093
		public override string WebName
		{
			get
			{
				return this.encoding.WebName;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x001280A0 File Offset: 0x001270A0
		public override int WindowsCodePage
		{
			get
			{
				return this.encoding.WindowsCodePage;
			}
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x001280AD File Offset: 0x001270AD
		public override Decoder GetDecoder()
		{
			return this.encoding.GetDecoder();
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x001280BA File Offset: 0x001270BA
		public override Encoder GetEncoder()
		{
			return this.encoding.GetEncoder();
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x001280C7 File Offset: 0x001270C7
		public override byte[] GetPreamble()
		{
			return EncodingNoPreamble.emptyPreamble;
		}

		// Token: 0x04002108 RID: 8456
		private Encoding encoding;

		// Token: 0x04002109 RID: 8457
		private static byte[] emptyPreamble = new byte[0];
	}
}
