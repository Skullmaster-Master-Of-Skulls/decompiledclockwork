using System;

namespace System.util.zlib
{
	// Token: 0x02000539 RID: 1337
	internal sealed class Inflate
	{
		// Token: 0x06002E08 RID: 11784 RVA: 0x0011C3C0 File Offset: 0x0011B3C0
		internal int inflateReset(ZStream z)
		{
			if (z == null || z.istate == null)
			{
				return -2;
			}
			z.total_in = (z.total_out = 0L);
			z.msg = null;
			z.istate.mode = ((z.istate.nowrap != 0) ? 7 : 0);
			z.istate.blocks.reset(z, null);
			return 0;
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x0011C422 File Offset: 0x0011B422
		internal int inflateEnd(ZStream z)
		{
			if (this.blocks != null)
			{
				this.blocks.free(z);
			}
			this.blocks = null;
			return 0;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x0011C440 File Offset: 0x0011B440
		internal int inflateInit(ZStream z, int w)
		{
			z.msg = null;
			this.blocks = null;
			this.nowrap = 0;
			if (w < 0)
			{
				w = -w;
				this.nowrap = 1;
			}
			if (w < 8 || w > 15)
			{
				this.inflateEnd(z);
				return -2;
			}
			this.wbits = w;
			z.istate.blocks = new InfBlocks(z, (z.istate.nowrap != 0) ? null : this, 1 << w);
			this.inflateReset(z);
			return 0;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0011C4C0 File Offset: 0x0011B4C0
		internal int inflate(ZStream z, int f)
		{
			if (z == null || z.istate == null || z.next_in == null)
			{
				return -2;
			}
			f = ((f == 4) ? -5 : 0);
			int num = -5;
			for (;;)
			{
				switch (z.istate.mode)
				{
				case 0:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in += 1L;
					if (((z.istate.method = (int)z.next_in[z.next_in_index++]) & 15) != 8)
					{
						z.istate.mode = 13;
						z.msg = "unknown compression method";
						z.istate.marker = 5;
						continue;
					}
					if ((z.istate.method >> 4) + 8 > z.istate.wbits)
					{
						z.istate.mode = 13;
						z.msg = "invalid window size";
						z.istate.marker = 5;
						continue;
					}
					z.istate.mode = 1;
					goto IL_144;
				case 1:
					goto IL_144;
				case 2:
					goto IL_1EE;
				case 3:
					goto IL_259;
				case 4:
					goto IL_2CB;
				case 5:
					goto IL_33C;
				case 6:
					goto IL_3B8;
				case 7:
					num = z.istate.blocks.proc(z, num);
					if (num == -3)
					{
						z.istate.mode = 13;
						z.istate.marker = 0;
						continue;
					}
					if (num == 0)
					{
						num = f;
					}
					if (num != 1)
					{
						return num;
					}
					num = f;
					z.istate.blocks.reset(z, z.istate.was);
					if (z.istate.nowrap != 0)
					{
						z.istate.mode = 12;
						continue;
					}
					z.istate.mode = 8;
					goto IL_469;
				case 8:
					goto IL_469;
				case 9:
					goto IL_4D5;
				case 10:
					goto IL_548;
				case 11:
					goto IL_5BA;
				case 12:
					return 1;
				case 13:
					return -3;
				}
				break;
				IL_144:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				int num2 = (int)(z.next_in[z.next_in_index++] & byte.MaxValue);
				if (((z.istate.method << 8) + num2) % 31 != 0)
				{
					z.istate.mode = 13;
					z.msg = "incorrect header check";
					z.istate.marker = 5;
					continue;
				}
				if ((num2 & 32) == 0)
				{
					z.istate.mode = 7;
					continue;
				}
				goto IL_1E2;
				IL_5BA:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				z.istate.need += (long)((ulong)z.next_in[z.next_in_index++] & 255UL);
				if ((int)z.istate.was[0] != (int)z.istate.need)
				{
					z.istate.mode = 13;
					z.msg = "incorrect data check";
					z.istate.marker = 5;
					continue;
				}
				goto IL_65C;
				IL_548:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				z.istate.need += ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 8) & 65280L);
				z.istate.mode = 11;
				goto IL_5BA;
				IL_4D5:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				z.istate.need += ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 16) & 16711680L);
				z.istate.mode = 10;
				goto IL_548;
				IL_469:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				z.istate.need = ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 24) & (long)((ulong)-16777216));
				z.istate.mode = 9;
				goto IL_4D5;
			}
			return -2;
			IL_1E2:
			z.istate.mode = 2;
			IL_1EE:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			z.istate.need = ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 24) & (long)((ulong)-16777216));
			z.istate.mode = 3;
			IL_259:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			z.istate.need += ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 16) & 16711680L);
			z.istate.mode = 4;
			IL_2CB:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			z.istate.need += ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 8) & 65280L);
			z.istate.mode = 5;
			IL_33C:
			if (z.avail_in == 0)
			{
				return num;
			}
			z.avail_in--;
			z.total_in += 1L;
			z.istate.need += (long)((ulong)z.next_in[z.next_in_index++] & 255UL);
			z.adler = z.istate.need;
			z.istate.mode = 6;
			return 2;
			IL_3B8:
			z.istate.mode = 13;
			z.msg = "need dictionary";
			z.istate.marker = 0;
			return -2;
			IL_65C:
			z.istate.mode = 12;
			return 1;
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0011CB40 File Offset: 0x0011BB40
		internal int inflateSetDictionary(ZStream z, byte[] dictionary, int dictLength)
		{
			int start = 0;
			int num = dictLength;
			if (z == null || z.istate == null || z.istate.mode != 6)
			{
				return -2;
			}
			if (z._adler.adler32(1L, dictionary, 0, dictLength) != z.adler)
			{
				return -3;
			}
			z.adler = z._adler.adler32(0L, null, 0, 0);
			if (num >= 1 << z.istate.wbits)
			{
				num = (1 << z.istate.wbits) - 1;
				start = dictLength - num;
			}
			z.istate.blocks.set_dictionary(dictionary, start, num);
			z.istate.mode = 7;
			return 0;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x0011CBE8 File Offset: 0x0011BBE8
		internal int inflateSync(ZStream z)
		{
			if (z == null || z.istate == null)
			{
				return -2;
			}
			if (z.istate.mode != 13)
			{
				z.istate.mode = 13;
				z.istate.marker = 0;
			}
			int num;
			if ((num = z.avail_in) == 0)
			{
				return -5;
			}
			int num2 = z.next_in_index;
			int num3 = z.istate.marker;
			while (num != 0 && num3 < 4)
			{
				if (z.next_in[num2] == Inflate.mark[num3])
				{
					num3++;
				}
				else if (z.next_in[num2] != 0)
				{
					num3 = 0;
				}
				else
				{
					num3 = 4 - num3;
				}
				num2++;
				num--;
			}
			z.total_in += (long)(num2 - z.next_in_index);
			z.next_in_index = num2;
			z.avail_in = num;
			z.istate.marker = num3;
			if (num3 != 4)
			{
				return -3;
			}
			long total_in = z.total_in;
			long total_out = z.total_out;
			this.inflateReset(z);
			z.total_in = total_in;
			z.total_out = total_out;
			z.istate.mode = 7;
			return 0;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0011CCEF File Offset: 0x0011BCEF
		internal int inflateSyncPoint(ZStream z)
		{
			if (z == null || z.istate == null || z.istate.blocks == null)
			{
				return -2;
			}
			return z.istate.blocks.sync_point();
		}

		// Token: 0x04001FC7 RID: 8135
		private const int MAX_WBITS = 15;

		// Token: 0x04001FC8 RID: 8136
		private const int PRESET_DICT = 32;

		// Token: 0x04001FC9 RID: 8137
		internal const int Z_NO_FLUSH = 0;

		// Token: 0x04001FCA RID: 8138
		internal const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04001FCB RID: 8139
		internal const int Z_SYNC_FLUSH = 2;

		// Token: 0x04001FCC RID: 8140
		internal const int Z_FULL_FLUSH = 3;

		// Token: 0x04001FCD RID: 8141
		internal const int Z_FINISH = 4;

		// Token: 0x04001FCE RID: 8142
		private const int Z_DEFLATED = 8;

		// Token: 0x04001FCF RID: 8143
		private const int Z_OK = 0;

		// Token: 0x04001FD0 RID: 8144
		private const int Z_STREAM_END = 1;

		// Token: 0x04001FD1 RID: 8145
		private const int Z_NEED_DICT = 2;

		// Token: 0x04001FD2 RID: 8146
		private const int Z_ERRNO = -1;

		// Token: 0x04001FD3 RID: 8147
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04001FD4 RID: 8148
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04001FD5 RID: 8149
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04001FD6 RID: 8150
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04001FD7 RID: 8151
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x04001FD8 RID: 8152
		private const int METHOD = 0;

		// Token: 0x04001FD9 RID: 8153
		private const int FLAG = 1;

		// Token: 0x04001FDA RID: 8154
		private const int DICT4 = 2;

		// Token: 0x04001FDB RID: 8155
		private const int DICT3 = 3;

		// Token: 0x04001FDC RID: 8156
		private const int DICT2 = 4;

		// Token: 0x04001FDD RID: 8157
		private const int DICT1 = 5;

		// Token: 0x04001FDE RID: 8158
		private const int DICT0 = 6;

		// Token: 0x04001FDF RID: 8159
		private const int BLOCKS = 7;

		// Token: 0x04001FE0 RID: 8160
		private const int CHECK4 = 8;

		// Token: 0x04001FE1 RID: 8161
		private const int CHECK3 = 9;

		// Token: 0x04001FE2 RID: 8162
		private const int CHECK2 = 10;

		// Token: 0x04001FE3 RID: 8163
		private const int CHECK1 = 11;

		// Token: 0x04001FE4 RID: 8164
		private const int DONE = 12;

		// Token: 0x04001FE5 RID: 8165
		private const int BAD = 13;

		// Token: 0x04001FE6 RID: 8166
		internal int mode;

		// Token: 0x04001FE7 RID: 8167
		internal int method;

		// Token: 0x04001FE8 RID: 8168
		internal long[] was = new long[1];

		// Token: 0x04001FE9 RID: 8169
		internal long need;

		// Token: 0x04001FEA RID: 8170
		internal int marker;

		// Token: 0x04001FEB RID: 8171
		internal int nowrap;

		// Token: 0x04001FEC RID: 8172
		internal int wbits;

		// Token: 0x04001FED RID: 8173
		internal InfBlocks blocks;

		// Token: 0x04001FEE RID: 8174
		private static byte[] mark = new byte[]
		{
			0,
			0,
			byte.MaxValue,
			byte.MaxValue
		};
	}
}
