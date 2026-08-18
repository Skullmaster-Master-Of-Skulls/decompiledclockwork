using System;
using System.Diagnostics;

namespace System.IO.Compression
{
	// Token: 0x0200042A RID: 1066
	internal class FastEncoderWindow
	{
		// Token: 0x06002806 RID: 10246 RVA: 0x000B7DDE File Offset: 0x000B5FDE
		public FastEncoderWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x000B7DEC File Offset: 0x000B5FEC
		public int BytesAvailable
		{
			get
			{
				return this.bufEnd - this.bufPos;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x000B7DFC File Offset: 0x000B5FFC
		public DeflateInput UnprocessedInput
		{
			get
			{
				return new DeflateInput
				{
					Buffer = this.window,
					StartIndex = this.bufPos,
					Count = this.bufEnd - this.bufPos
				};
			}
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000B7E3B File Offset: 0x000B603B
		public void FlushWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000B7E44 File Offset: 0x000B6044
		private void ResetWindow()
		{
			this.window = new byte[16646];
			this.prev = new ushort[8450];
			this.lookup = new ushort[2048];
			this.bufPos = 8192;
			this.bufEnd = this.bufPos;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x000B7E98 File Offset: 0x000B6098
		public int FreeWindowSpace
		{
			get
			{
				return 16384 - this.bufEnd;
			}
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000B7EA6 File Offset: 0x000B60A6
		public void CopyBytes(byte[] inputBuffer, int startIndex, int count)
		{
			Array.Copy(inputBuffer, startIndex, this.window, this.bufEnd, count);
			this.bufEnd += count;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000B7ECC File Offset: 0x000B60CC
		public void MoveWindows()
		{
			Array.Copy(this.window, this.bufPos - 8192, this.window, 0, 8192);
			for (int i = 0; i < 2048; i++)
			{
				int num = (int)(this.lookup[i] - 8192);
				if (num <= 0)
				{
					this.lookup[i] = 0;
				}
				else
				{
					this.lookup[i] = (ushort)num;
				}
			}
			for (int i = 0; i < 8192; i++)
			{
				long num2 = (long)((ulong)this.prev[i] - 8192UL);
				if (num2 <= 0L)
				{
					this.prev[i] = 0;
				}
				else
				{
					this.prev[i] = (ushort)num2;
				}
			}
			this.bufPos = 8192;
			this.bufEnd = this.bufPos;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000B7F86 File Offset: 0x000B6186
		private uint HashValue(uint hash, byte b)
		{
			return hash << 4 ^ (uint)b;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000B7F90 File Offset: 0x000B6190
		private uint InsertString(ref uint hash)
		{
			hash = this.HashValue(hash, this.window[this.bufPos + 2]);
			uint num = (uint)this.lookup[(int)(hash & 2047U)];
			this.lookup[(int)(hash & 2047U)] = (ushort)this.bufPos;
			this.prev[this.bufPos & 8191] = (ushort)num;
			return num;
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000B7FF4 File Offset: 0x000B61F4
		private void InsertStrings(ref uint hash, int matchLen)
		{
			if (this.bufEnd - this.bufPos <= matchLen)
			{
				this.bufPos += matchLen - 1;
				return;
			}
			while (--matchLen > 0)
			{
				this.InsertString(ref hash);
				this.bufPos++;
			}
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000B8044 File Offset: 0x000B6244
		internal bool GetNextSymbolOrMatch(Match match)
		{
			uint hash = this.HashValue(0U, this.window[this.bufPos]);
			hash = this.HashValue(hash, this.window[this.bufPos + 1]);
			int position = 0;
			int num;
			if (this.bufEnd - this.bufPos <= 3)
			{
				num = 0;
			}
			else
			{
				int num2 = (int)this.InsertString(ref hash);
				if (num2 != 0)
				{
					num = this.FindMatch(num2, out position, 32, 32);
					if (this.bufPos + num > this.bufEnd)
					{
						num = this.bufEnd - this.bufPos;
					}
				}
				else
				{
					num = 0;
				}
			}
			if (num < 3)
			{
				match.State = MatchState.HasSymbol;
				match.Symbol = this.window[this.bufPos];
				this.bufPos++;
			}
			else
			{
				this.bufPos++;
				if (num <= 6)
				{
					int position2 = 0;
					int num3 = (int)this.InsertString(ref hash);
					int num4;
					if (num3 != 0)
					{
						num4 = this.FindMatch(num3, out position2, (num < 4) ? 32 : 8, 32);
						if (this.bufPos + num4 > this.bufEnd)
						{
							num4 = this.bufEnd - this.bufPos;
						}
					}
					else
					{
						num4 = 0;
					}
					if (num4 > num)
					{
						match.State = MatchState.HasSymbolAndMatch;
						match.Symbol = this.window[this.bufPos - 1];
						match.Position = position2;
						match.Length = num4;
						this.bufPos++;
						num = num4;
						this.InsertStrings(ref hash, num);
					}
					else
					{
						match.State = MatchState.HasMatch;
						match.Position = position;
						match.Length = num;
						num--;
						this.bufPos++;
						this.InsertStrings(ref hash, num);
					}
				}
				else
				{
					match.State = MatchState.HasMatch;
					match.Position = position;
					match.Length = num;
					this.InsertStrings(ref hash, num);
				}
			}
			if (this.bufPos == 16384)
			{
				this.MoveWindows();
			}
			return true;
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000B8214 File Offset: 0x000B6414
		private int FindMatch(int search, out int matchPos, int searchDepth, int niceLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = this.bufPos - 8192;
			byte b = this.window[this.bufPos];
			while (search > num3)
			{
				if (this.window[search + num] == b)
				{
					int num4 = 0;
					while (num4 < 258 && this.window[this.bufPos + num4] == this.window[search + num4])
					{
						num4++;
					}
					if (num4 > num)
					{
						num = num4;
						num2 = search;
						if (num4 > 32)
						{
							break;
						}
						b = this.window[this.bufPos + num4];
					}
				}
				if (--searchDepth == 0)
				{
					break;
				}
				search = (int)this.prev[search & 8191];
			}
			matchPos = this.bufPos - num2 - 1;
			if (num == 3 && matchPos >= 16384)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000B82DC File Offset: 0x000B64DC
		[Conditional("DEBUG")]
		private void VerifyHashes()
		{
			for (int i = 0; i < 2048; i++)
			{
				ushort num = this.lookup[i];
				while (num != 0 && this.bufPos - (int)num < 8192)
				{
					ushort num2 = this.prev[(int)(num & 8191)];
					if (this.bufPos - (int)num2 >= 8192)
					{
						break;
					}
					num = num2;
				}
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000B8336 File Offset: 0x000B6536
		private uint RecalculateHash(int position)
		{
			return (uint)(((int)this.window[position] << 8 ^ (int)this.window[position + 1] << 4 ^ (int)this.window[position + 2]) & 2047);
		}

		// Token: 0x040021C6 RID: 8646
		private byte[] window;

		// Token: 0x040021C7 RID: 8647
		private int bufPos;

		// Token: 0x040021C8 RID: 8648
		private int bufEnd;

		// Token: 0x040021C9 RID: 8649
		private const int FastEncoderHashShift = 4;

		// Token: 0x040021CA RID: 8650
		private const int FastEncoderHashtableSize = 2048;

		// Token: 0x040021CB RID: 8651
		private const int FastEncoderHashMask = 2047;

		// Token: 0x040021CC RID: 8652
		private const int FastEncoderWindowSize = 8192;

		// Token: 0x040021CD RID: 8653
		private const int FastEncoderWindowMask = 8191;

		// Token: 0x040021CE RID: 8654
		private const int FastEncoderMatch3DistThreshold = 16384;

		// Token: 0x040021CF RID: 8655
		internal const int MaxMatch = 258;

		// Token: 0x040021D0 RID: 8656
		internal const int MinMatch = 3;

		// Token: 0x040021D1 RID: 8657
		private const int SearchDepth = 32;

		// Token: 0x040021D2 RID: 8658
		private const int GoodLength = 4;

		// Token: 0x040021D3 RID: 8659
		private const int NiceLength = 32;

		// Token: 0x040021D4 RID: 8660
		private const int LazyMatchThreshold = 6;

		// Token: 0x040021D5 RID: 8661
		private ushort[] prev;

		// Token: 0x040021D6 RID: 8662
		private ushort[] lookup;
	}
}
