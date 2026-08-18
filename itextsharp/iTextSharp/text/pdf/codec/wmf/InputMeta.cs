using System;
using System.IO;

namespace iTextSharp.text.pdf.codec.wmf
{
	// Token: 0x02000220 RID: 544
	public class InputMeta
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x00076D47 File Offset: 0x00075D47
		public InputMeta(Stream istr)
		{
			this.sr = istr;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00076D58 File Offset: 0x00075D58
		public int ReadWord()
		{
			this.length += 2;
			int num = this.sr.ReadByte();
			if (num < 0)
			{
				return 0;
			}
			return num + (this.sr.ReadByte() << 8) & 65535;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00076D9C File Offset: 0x00075D9C
		public int ReadShort()
		{
			int num = this.ReadWord();
			if (num > 32767)
			{
				num -= 65536;
			}
			return num;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00076DC4 File Offset: 0x00075DC4
		public int ReadInt()
		{
			this.length += 4;
			int num = this.sr.ReadByte();
			if (num < 0)
			{
				return 0;
			}
			int num2 = this.sr.ReadByte() << 8;
			int num3 = this.sr.ReadByte() << 16;
			return num + num2 + num3 + (this.sr.ReadByte() << 24);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00076E22 File Offset: 0x00075E22
		public int ReadByte()
		{
			this.length++;
			return this.sr.ReadByte() & 255;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00076E43 File Offset: 0x00075E43
		public void Skip(int len)
		{
			this.length += len;
			Utilities.Skip(this.sr, len);
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00076E5F File Offset: 0x00075E5F
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00076E68 File Offset: 0x00075E68
		public BaseColor ReadColor()
		{
			int red = this.ReadByte();
			int green = this.ReadByte();
			int blue = this.ReadByte();
			this.ReadByte();
			return new BaseColor(red, green, blue);
		}

		// Token: 0x04000E43 RID: 3651
		private Stream sr;

		// Token: 0x04000E44 RID: 3652
		private int length;
	}
}
