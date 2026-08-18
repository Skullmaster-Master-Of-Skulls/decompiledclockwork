using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x0200001B RID: 27
	internal struct BitDecoder
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00005F79 File Offset: 0x00004179
		public void UpdateModel(int numMoveBits, uint symbol)
		{
			if (symbol == 0U)
			{
				this.Prob += 2048U - this.Prob >> numMoveBits;
				return;
			}
			this.Prob -= this.Prob >> numMoveBits;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005FB5 File Offset: 0x000041B5
		public void Init()
		{
			this.Prob = 1024U;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005FC4 File Offset: 0x000041C4
		public uint Decode(Decoder rangeDecoder)
		{
			uint num = (rangeDecoder.Range >> 11) * this.Prob;
			if (rangeDecoder.Code < num)
			{
				rangeDecoder.Range = num;
				this.Prob += 2048U - this.Prob >> 5;
				if (rangeDecoder.Range < 16777216U)
				{
					rangeDecoder.Code = (rangeDecoder.Code << 8 | (uint)((byte)rangeDecoder.Stream.ReadByte()));
					rangeDecoder.Range <<= 8;
				}
				return 0U;
			}
			rangeDecoder.Range -= num;
			rangeDecoder.Code -= num;
			this.Prob -= this.Prob >> 5;
			if (rangeDecoder.Range < 16777216U)
			{
				rangeDecoder.Code = (rangeDecoder.Code << 8 | (uint)((byte)rangeDecoder.Stream.ReadByte()));
				rangeDecoder.Range <<= 8;
			}
			return 1U;
		}

		// Token: 0x040000AD RID: 173
		public const int kNumBitModelTotalBits = 11;

		// Token: 0x040000AE RID: 174
		public const uint kBitModelTotal = 2048U;

		// Token: 0x040000AF RID: 175
		private const int kNumMoveBits = 5;

		// Token: 0x040000B0 RID: 176
		private uint Prob;
	}
}
