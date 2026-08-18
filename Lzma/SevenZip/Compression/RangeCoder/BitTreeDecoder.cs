using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x02000003 RID: 3
	internal struct BitTreeDecoder
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000223D File Offset: 0x0000043D
		public BitTreeDecoder(int numBitLevels)
		{
			this.NumBitLevels = numBitLevels;
			this.Models = new BitDecoder[1 << numBitLevels];
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002258 File Offset: 0x00000458
		public void Init()
		{
			uint num = 1U;
			while ((ulong)num < (ulong)(1L << (this.NumBitLevels & 31)))
			{
				this.Models[(int)((UIntPtr)num)].Init();
				num += 1U;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002290 File Offset: 0x00000490
		public uint Decode(Decoder rangeDecoder)
		{
			uint num = 1U;
			for (int i = this.NumBitLevels; i > 0; i--)
			{
				num = (num << 1) + this.Models[(int)((UIntPtr)num)].Decode(rangeDecoder);
			}
			return num - (1U << this.NumBitLevels);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022D8 File Offset: 0x000004D8
		public uint ReverseDecode(Decoder rangeDecoder)
		{
			uint num = 1U;
			uint num2 = 0U;
			for (int i = 0; i < this.NumBitLevels; i++)
			{
				uint num3 = this.Models[(int)((UIntPtr)num)].Decode(rangeDecoder);
				num <<= 1;
				num += num3;
				num2 |= num3 << i;
			}
			return num2;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002320 File Offset: 0x00000520
		public static uint ReverseDecode(BitDecoder[] Models, uint startIndex, Decoder rangeDecoder, int NumBitLevels)
		{
			uint num = 1U;
			uint num2 = 0U;
			for (int i = 0; i < NumBitLevels; i++)
			{
				uint num3 = Models[(int)((UIntPtr)(startIndex + num))].Decode(rangeDecoder);
				num <<= 1;
				num += num3;
				num2 |= num3 << i;
			}
			return num2;
		}

		// Token: 0x04000003 RID: 3
		private BitDecoder[] Models;

		// Token: 0x04000004 RID: 4
		private int NumBitLevels;
	}
}
