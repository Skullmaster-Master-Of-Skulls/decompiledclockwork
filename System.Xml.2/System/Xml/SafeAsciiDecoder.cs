using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000083 RID: 131
	internal class SafeAsciiDecoder : Decoder
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0001278D File Offset: 0x0001098D
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00012790 File Offset: 0x00010990
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			int i = byteIndex;
			int num = charIndex;
			while (i < byteIndex + byteCount)
			{
				chars[num++] = (char)bytes[i++];
			}
			return byteCount;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000127BC File Offset: 0x000109BC
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (charCount < byteCount)
			{
				byteCount = charCount;
				completed = false;
			}
			else
			{
				completed = true;
			}
			int i = byteIndex;
			int num = charIndex;
			int num2 = byteIndex + byteCount;
			while (i < num2)
			{
				chars[num++] = (char)bytes[i++];
			}
			charsUsed = byteCount;
			bytesUsed = byteCount;
		}
	}
}
