using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x0200001A RID: 26
	internal struct BitEncoder
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00005DF0 File Offset: 0x00003FF0
		public void Init()
		{
			this.Prob = 1024U;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005DFD File Offset: 0x00003FFD
		public void UpdateModel(uint symbol)
		{
			if (symbol == 0U)
			{
				this.Prob += 2048U - this.Prob >> 5;
				return;
			}
			this.Prob -= this.Prob >> 5;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005E34 File Offset: 0x00004034
		public void Encode(Encoder encoder, uint symbol)
		{
			uint num = (encoder.Range >> 11) * this.Prob;
			if (symbol == 0U)
			{
				encoder.Range = num;
				this.Prob += 2048U - this.Prob >> 5;
			}
			else
			{
				encoder.Low += (ulong)num;
				encoder.Range -= num;
				this.Prob -= this.Prob >> 5;
			}
			if (encoder.Range < 16777216U)
			{
				encoder.Range <<= 8;
				encoder.ShiftLow();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005ECC File Offset: 0x000040CC
		static BitEncoder()
		{
			for (int i = 8; i >= 0; i--)
			{
				uint num = 1U << 9 - i - 1;
				uint num2 = 1U << 9 - i;
				for (uint num3 = num; num3 < num2; num3 += 1U)
				{
					BitEncoder.ProbPrices[(int)((UIntPtr)num3)] = (uint)((i << 6) + (int)(num2 - num3 << 6 >> 9 - i - 1));
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005F32 File Offset: 0x00004132
		public uint GetPrice(uint symbol)
		{
			return BitEncoder.ProbPrices[(int)(checked((IntPtr)((unchecked((ulong)(this.Prob - symbol) ^ (ulong)((long)(-(long)symbol))) & 2047UL) >> 2)))];
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005F51 File Offset: 0x00004151
		public uint GetPrice0()
		{
			return BitEncoder.ProbPrices[(int)((UIntPtr)(this.Prob >> 2))];
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005F62 File Offset: 0x00004162
		public uint GetPrice1()
		{
			return BitEncoder.ProbPrices[(int)((UIntPtr)(2048U - this.Prob >> 2))];
		}

		// Token: 0x040000A6 RID: 166
		public const int kNumBitModelTotalBits = 11;

		// Token: 0x040000A7 RID: 167
		public const uint kBitModelTotal = 2048U;

		// Token: 0x040000A8 RID: 168
		private const int kNumMoveBits = 5;

		// Token: 0x040000A9 RID: 169
		private const int kNumMoveReducingBits = 2;

		// Token: 0x040000AA RID: 170
		public const int kNumBitPriceShiftBits = 6;

		// Token: 0x040000AB RID: 171
		private uint Prob;

		// Token: 0x040000AC RID: 172
		private static uint[] ProbPrices = new uint[512];
	}
}
