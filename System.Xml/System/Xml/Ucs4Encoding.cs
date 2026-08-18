using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000035 RID: 53
	internal class Ucs4Encoding : Encoding
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000794A File Offset: 0x0000694A
		public override Decoder GetDecoder()
		{
			return this.ucs4Decoder;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007952 File Offset: 0x00006952
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return checked(count * 4);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007957 File Offset: 0x00006957
		public override int GetByteCount(char[] chars)
		{
			return chars.Length * 4;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000795E File Offset: 0x0000695E
		public override byte[] GetBytes(string s)
		{
			return null;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007961 File Offset: 0x00006961
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return 0;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007964 File Offset: 0x00006964
		public override int GetMaxByteCount(int charCount)
		{
			return 0;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007967 File Offset: 0x00006967
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.ucs4Decoder.GetCharCount(bytes, index, count);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007977 File Offset: 0x00006977
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.ucs4Decoder.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000798B File Offset: 0x0000698B
		public override int GetMaxCharCount(int byteCount)
		{
			return (byteCount + 3) / 4;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007992 File Offset: 0x00006992
		public override int CodePage
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007995 File Offset: 0x00006995
		public override int GetCharCount(byte[] bytes)
		{
			return bytes.Length / 4;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000799C File Offset: 0x0000699C
		public override Encoder GetEncoder()
		{
			return null;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000799F File Offset: 0x0000699F
		internal static Encoding UCS4_Littleendian
		{
			get
			{
				return new Ucs4Encoding4321();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000079A6 File Offset: 0x000069A6
		internal static Encoding UCS4_Bigendian
		{
			get
			{
				return new Ucs4Encoding1234();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000079AD File Offset: 0x000069AD
		internal static Encoding UCS4_2143
		{
			get
			{
				return new Ucs4Encoding2143();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000079B4 File Offset: 0x000069B4
		internal static Encoding UCS4_3412
		{
			get
			{
				return new Ucs4Encoding3412();
			}
		}

		// Token: 0x040004BE RID: 1214
		internal Ucs4Decoder ucs4Decoder;
	}
}
