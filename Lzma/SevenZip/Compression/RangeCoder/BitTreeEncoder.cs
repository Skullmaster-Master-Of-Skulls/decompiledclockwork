using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x02000002 RID: 2
	internal struct BitTreeEncoder
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public BitTreeEncoder(int numBitLevels)
		{
			this.NumBitLevels = numBitLevels;
			this.Models = new BitEncoder[1 << numBitLevels];
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
		public void Init()
		{
			uint num = 1U;
			while ((ulong)num < (ulong)(1L << (this.NumBitLevels & 31)))
			{
				this.Models[(int)((UIntPtr)num)].Init();
				num += 1U;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
		public void Encode(Encoder rangeEncoder, uint symbol)
		{
			uint num = 1U;
			int i = this.NumBitLevels;
			while (i > 0)
			{
				i--;
				uint num2 = symbol >> i & 1U;
				this.Models[(int)((UIntPtr)num)].Encode(rangeEncoder, num2);
				num = (num << 1 | num2);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public void ReverseEncode(Encoder rangeEncoder, uint symbol)
		{
			uint num = 1U;
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)this.NumBitLevels))
			{
				uint num3 = symbol & 1U;
				this.Models[(int)((UIntPtr)num)].Encode(rangeEncoder, num3);
				num = (num << 1 | num3);
				symbol >>= 1;
				num2 += 1U;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		public uint GetPrice(uint symbol)
		{
			uint num = 0U;
			uint num2 = 1U;
			int i = this.NumBitLevels;
			while (i > 0)
			{
				i--;
				uint num3 = symbol >> i & 1U;
				num += this.Models[(int)((UIntPtr)num2)].GetPrice(num3);
				num2 = (num2 << 1) + num3;
			}
			return num;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002178 File Offset: 0x00000378
		public uint ReverseGetPrice(uint symbol)
		{
			uint num = 0U;
			uint num2 = 1U;
			for (int i = this.NumBitLevels; i > 0; i--)
			{
				uint num3 = symbol & 1U;
				symbol >>= 1;
				num += this.Models[(int)((UIntPtr)num2)].GetPrice(num3);
				num2 = (num2 << 1 | num3);
			}
			return num;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021C0 File Offset: 0x000003C0
		public static uint ReverseGetPrice(BitEncoder[] Models, uint startIndex, int NumBitLevels, uint symbol)
		{
			uint num = 0U;
			uint num2 = 1U;
			for (int i = NumBitLevels; i > 0; i--)
			{
				uint num3 = symbol & 1U;
				symbol >>= 1;
				num += Models[(int)((UIntPtr)(startIndex + num2))].GetPrice(num3);
				num2 = (num2 << 1 | num3);
			}
			return num;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002200 File Offset: 0x00000400
		public static void ReverseEncode(BitEncoder[] Models, uint startIndex, Encoder rangeEncoder, int NumBitLevels, uint symbol)
		{
			uint num = 1U;
			for (int i = 0; i < NumBitLevels; i++)
			{
				uint num2 = symbol & 1U;
				Models[(int)((UIntPtr)(startIndex + num))].Encode(rangeEncoder, num2);
				num = (num << 1 | num2);
				symbol >>= 1;
			}
		}

		// Token: 0x04000001 RID: 1
		private BitEncoder[] Models;

		// Token: 0x04000002 RID: 2
		private int NumBitLevels;
	}
}
