using System;
using System.IO;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x02000022 RID: 34
	internal class Encoder
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00006D31 File Offset: 0x00004F31
		public void SetStream(Stream stream)
		{
			this.Stream = stream;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006D3A File Offset: 0x00004F3A
		public void ReleaseStream()
		{
			this.Stream = null;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006D43 File Offset: 0x00004F43
		public void Init()
		{
			this.StartPosition = this.Stream.Position;
			this.Low = 0UL;
			this.Range = uint.MaxValue;
			this._cacheSize = 1U;
			this._cache = 0;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006D74 File Offset: 0x00004F74
		public void FlushData()
		{
			for (int i = 0; i < 5; i++)
			{
				this.ShiftLow();
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006D93 File Offset: 0x00004F93
		public void FlushStream()
		{
			this.Stream.Flush();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public void CloseStream()
		{
			this.Stream.Close();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public void Encode(uint start, uint size, uint total)
		{
			this.Low += (ulong)(start * (this.Range /= total));
			this.Range *= size;
			while (this.Range < 16777216U)
			{
				this.Range <<= 8;
				this.ShiftLow();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006E10 File Offset: 0x00005010
		public void ShiftLow()
		{
			if ((uint)this.Low < 4278190080U || (uint)(this.Low >> 32) == 1U)
			{
				byte b = this._cache;
				do
				{
					this.Stream.WriteByte((byte)((ulong)b + (this.Low >> 32)));
					b = byte.MaxValue;
				}
				while ((this._cacheSize -= 1U) != 0U);
				this._cache = (byte)((uint)this.Low >> 24);
			}
			this._cacheSize += 1U;
			this.Low = (ulong)((ulong)((uint)this.Low) << 8);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006EA0 File Offset: 0x000050A0
		public void EncodeDirectBits(uint v, int numTotalBits)
		{
			for (int i = numTotalBits - 1; i >= 0; i--)
			{
				this.Range >>= 1;
				if ((v >> i & 1U) == 1U)
				{
					this.Low += (ulong)this.Range;
				}
				if (this.Range < 16777216U)
				{
					this.Range <<= 8;
					this.ShiftLow();
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006F0C File Offset: 0x0000510C
		public void EncodeBit(uint size0, int numTotalBits, uint symbol)
		{
			uint num = (this.Range >> numTotalBits) * size0;
			if (symbol == 0U)
			{
				this.Range = num;
			}
			else
			{
				this.Low += (ulong)num;
				this.Range -= num;
			}
			while (this.Range < 16777216U)
			{
				this.Range <<= 8;
				this.ShiftLow();
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006F73 File Offset: 0x00005173
		public long GetProcessedSizeAdd()
		{
			return (long)((ulong)this._cacheSize + (ulong)this.Stream.Position - (ulong)this.StartPosition + 4UL);
		}

		// Token: 0x040000D3 RID: 211
		public const uint kTopValue = 16777216U;

		// Token: 0x040000D4 RID: 212
		private Stream Stream;

		// Token: 0x040000D5 RID: 213
		public ulong Low;

		// Token: 0x040000D6 RID: 214
		public uint Range;

		// Token: 0x040000D7 RID: 215
		private uint _cacheSize;

		// Token: 0x040000D8 RID: 216
		private byte _cache;

		// Token: 0x040000D9 RID: 217
		private long StartPosition;
	}
}
