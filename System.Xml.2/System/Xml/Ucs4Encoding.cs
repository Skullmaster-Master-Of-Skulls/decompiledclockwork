using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000084 RID: 132
	internal class Ucs4Encoding : Encoding
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00012802 File Offset: 0x00010A02
		public override string WebName
		{
			get
			{
				return this.EncodingName;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001280A File Offset: 0x00010A0A
		public override Decoder GetDecoder()
		{
			return this.ucs4Decoder;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00012812 File Offset: 0x00010A12
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return checked(count * 4);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00012817 File Offset: 0x00010A17
		public override int GetByteCount(char[] chars)
		{
			return chars.Length * 4;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001281E File Offset: 0x00010A1E
		public override byte[] GetBytes(string s)
		{
			return null;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00012821 File Offset: 0x00010A21
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return 0;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00012824 File Offset: 0x00010A24
		public override int GetMaxByteCount(int charCount)
		{
			return 0;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00012827 File Offset: 0x00010A27
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.ucs4Decoder.GetCharCount(bytes, index, count);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00012837 File Offset: 0x00010A37
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.ucs4Decoder.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001284B File Offset: 0x00010A4B
		public override int GetMaxCharCount(int byteCount)
		{
			return (byteCount + 3) / 4;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00012852 File Offset: 0x00010A52
		public override int CodePage
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012855 File Offset: 0x00010A55
		public override int GetCharCount(byte[] bytes)
		{
			return bytes.Length / 4;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001285C File Offset: 0x00010A5C
		public override Encoder GetEncoder()
		{
			return null;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001285F File Offset: 0x00010A5F
		internal static Encoding UCS4_Littleendian
		{
			get
			{
				return new Ucs4Encoding4321();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00012866 File Offset: 0x00010A66
		internal static Encoding UCS4_Bigendian
		{
			get
			{
				return new Ucs4Encoding1234();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001286D File Offset: 0x00010A6D
		internal static Encoding UCS4_2143
		{
			get
			{
				return new Ucs4Encoding2143();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00012874 File Offset: 0x00010A74
		internal static Encoding UCS4_3412
		{
			get
			{
				return new Ucs4Encoding3412();
			}
		}

		// Token: 0x040001F7 RID: 503
		internal Ucs4Decoder ucs4Decoder;
	}
}
