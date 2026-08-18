using System;
using System.IO;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x02000023 RID: 35
	internal class Decoder
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00006F9C File Offset: 0x0000519C
		public void Init(Stream stream)
		{
			this.Stream = stream;
			this.Code = 0U;
			this.Range = uint.MaxValue;
			for (int i = 0; i < 5; i++)
			{
				this.Code = (this.Code << 8 | (uint)((byte)this.Stream.ReadByte()));
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006FE5 File Offset: 0x000051E5
		public void ReleaseStream()
		{
			this.Stream = null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006FEE File Offset: 0x000051EE
		public void CloseStream()
		{
			this.Stream.Close();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006FFB File Offset: 0x000051FB
		public void Normalize()
		{
			while (this.Range < 16777216U)
			{
				this.Code = (this.Code << 8 | (uint)((byte)this.Stream.ReadByte()));
				this.Range <<= 8;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007035 File Offset: 0x00005235
		public void Normalize2()
		{
			if (this.Range < 16777216U)
			{
				this.Code = (this.Code << 8 | (uint)((byte)this.Stream.ReadByte()));
				this.Range <<= 8;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007070 File Offset: 0x00005270
		public uint GetThreshold(uint total)
		{
			return this.Code / (this.Range /= total);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007095 File Offset: 0x00005295
		public void Decode(uint start, uint size, uint total)
		{
			this.Code -= start * this.Range;
			this.Range *= size;
			this.Normalize();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000070C0 File Offset: 0x000052C0
		public uint DecodeDirectBits(int numTotalBits)
		{
			uint num = this.Range;
			uint num2 = this.Code;
			uint num3 = 0U;
			for (int i = numTotalBits; i > 0; i--)
			{
				num >>= 1;
				uint num4 = num2 - num >> 31;
				num2 -= (num & num4 - 1U);
				num3 = (num3 << 1 | 1U - num4);
				if (num < 16777216U)
				{
					num2 = (num2 << 8 | (uint)((byte)this.Stream.ReadByte()));
					num <<= 8;
				}
			}
			this.Range = num;
			this.Code = num2;
			return num3;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007134 File Offset: 0x00005334
		public uint DecodeBit(uint size0, int numTotalBits)
		{
			uint num = (this.Range >> numTotalBits) * size0;
			uint result;
			if (this.Code < num)
			{
				result = 0U;
				this.Range = num;
			}
			else
			{
				result = 1U;
				this.Code -= num;
				this.Range -= num;
			}
			this.Normalize();
			return result;
		}

		// Token: 0x040000DA RID: 218
		public const uint kTopValue = 16777216U;

		// Token: 0x040000DB RID: 219
		public uint Range;

		// Token: 0x040000DC RID: 220
		public uint Code;

		// Token: 0x040000DD RID: 221
		public Stream Stream;
	}
}
