using System;
using System.IO;

namespace Spire.CompoundFile.Doc
{
	// Token: 0x02000490 RID: 1168
	public class ClipboardData : ICloneable
	{
		// Token: 0x06003FFB RID: 16379 RVA: 0x003B01A4 File Offset: 0x003AF1A4
		public object Clone()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			ClipboardData clipboardData = (ClipboardData)base.MemberwiseClone();
			clipboardData.Data = sprᰓ.ᜀ(this.Data);
			return clipboardData;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x003B0200 File Offset: 0x003AF200
		public int Serialize(Stream stream)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			int num = 0;
			int num2 = this.Data.Length;
			num += sprữ.ᜂ(stream, num2);
			num += sprữ.ᜂ(stream, this.Format);
			stream.Write(this.Data, 0, num2);
			return num + num2;
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x003B0274 File Offset: 0x003AF274
		public void Parse(Stream stream)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			byte[] a_ = new byte[4];
			int num = sprữ.ᜁ(stream, a_);
			this.Format = sprữ.ᜁ(stream, a_);
			this.Data = new byte[num];
			stream.Read(this.Data, 0, num);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x003B02FC File Offset: 0x003AF2FC
		internal static string b(string A_0, int A_1)
		{
			char[] array = A_0.ToCharArray();
			int num = 1867485029 + A_1;
			int num3;
			int num2;
			if ((num2 = (num3 = 0)) < 1)
			{
				goto IL_47;
			}
			IL_14:
			int num5;
			int num4 = num5 = num2;
			char[] array2 = array;
			int num6 = num5;
			char c = array[num5];
			byte b = (byte)((int)(c & 'ÿ') ^ num++);
			byte b2 = (byte)((int)(c >> 8) ^ num++);
			byte b3 = b2;
			b2 = b;
			b = b3;
			array2[num6] = (ushort)((int)b2 << 8 | (int)b);
			num3 = num4 + 1;
			IL_47:
			if ((num2 = num3) >= array.Length)
			{
				return string.Intern(new string(array));
			}
			goto IL_14;
		}

		// Token: 0x04002F7C RID: 12156
		private byte \u2460\u00AF\u00AF\u00A7;

		// Token: 0x04002F7D RID: 12157
		private long \u25D9\u009A\u008A\u0096;

		// Token: 0x04002F7E RID: 12158
		private string \u25D9\u009C\u0089\u009B;

		// Token: 0x04002F7F RID: 12159
		private int \u25D8\u00A9\u0084\u0097;

		// Token: 0x04002F80 RID: 12160
		private bool \u2593\u009E\u0088\u0091;

		// Token: 0x04002F81 RID: 12161
		public int Format;

		// Token: 0x04002F82 RID: 12162
		private byte \u2460\u0091\u00A4\u008C;

		// Token: 0x04002F83 RID: 12163
		public byte[] Data;
	}
}
