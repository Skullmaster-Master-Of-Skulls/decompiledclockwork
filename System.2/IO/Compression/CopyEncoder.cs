using System;

namespace System.IO.Compression
{
	// Token: 0x0200041C RID: 1052
	internal class CopyEncoder
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x000B5CB4 File Offset: 0x000B3EB4
		public void GetBlock(DeflateInput input, OutputBuffer output, bool isFinal)
		{
			int num = 0;
			if (input != null)
			{
				num = Math.Min(input.Count, output.FreeBytes - 5 - output.BitsInBuffer);
				if (num > 65531)
				{
					num = 65531;
				}
			}
			if (isFinal)
			{
				output.WriteBits(3, 1U);
			}
			else
			{
				output.WriteBits(3, 0U);
			}
			output.FlushBits();
			this.WriteLenNLen((ushort)num, output);
			if (input != null && num > 0)
			{
				output.WriteBytes(input.Buffer, input.StartIndex, num);
				input.ConsumeBytes(num);
			}
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000B5D34 File Offset: 0x000B3F34
		private void WriteLenNLen(ushort len, OutputBuffer output)
		{
			output.WriteUInt16(len);
			ushort value = ~len;
			output.WriteUInt16(value);
		}

		// Token: 0x04002171 RID: 8561
		private const int PaddingSize = 5;

		// Token: 0x04002172 RID: 8562
		private const int MaxUncompressedBlockSize = 65536;
	}
}
