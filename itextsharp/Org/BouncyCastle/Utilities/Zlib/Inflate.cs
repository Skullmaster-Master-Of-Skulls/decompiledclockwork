using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020000FF RID: 255
	internal sealed class Inflate
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x0003371C File Offset: 0x0003271C
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

		// Token: 0x06000A20 RID: 2592 RVA: 0x0003377E File Offset: 0x0003277E
		internal int inflateEnd(ZStream z)
		{
			if (this.blocks != null)
			{
				this.blocks.free(z);
			}
			this.blocks = null;
			return 0;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0003379C File Offset: 0x0003279C
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

		// Token: 0x06000A22 RID: 2594 RVA: 0x0003381C File Offset: 0x0003281C
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

		// Token: 0x06000A23 RID: 2595 RVA: 0x00033E9C File Offset: 0x00032E9C
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

		// Token: 0x06000A24 RID: 2596 RVA: 0x00033F44 File Offset: 0x00032F44
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

		// Token: 0x06000A25 RID: 2597 RVA: 0x0003404B File Offset: 0x0003304B
		internal int inflateSyncPoint(ZStream z)
		{
			if (z == null || z.istate == null || z.istate.blocks == null)
			{
				return -2;
			}
			return z.istate.blocks.sync_point();
		}

		// Token: 0x0400081C RID: 2076
		private const int MAX_WBITS = 15;

		// Token: 0x0400081D RID: 2077
		private const int PRESET_DICT = 32;

		// Token: 0x0400081E RID: 2078
		internal const int Z_NO_FLUSH = 0;

		// Token: 0x0400081F RID: 2079
		internal const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04000820 RID: 2080
		internal const int Z_SYNC_FLUSH = 2;

		// Token: 0x04000821 RID: 2081
		internal const int Z_FULL_FLUSH = 3;

		// Token: 0x04000822 RID: 2082
		internal const int Z_FINISH = 4;

		// Token: 0x04000823 RID: 2083
		private const int Z_DEFLATED = 8;

		// Token: 0x04000824 RID: 2084
		private const int Z_OK = 0;

		// Token: 0x04000825 RID: 2085
		private const int Z_STREAM_END = 1;

		// Token: 0x04000826 RID: 2086
		private const int Z_NEED_DICT = 2;

		// Token: 0x04000827 RID: 2087
		private const int Z_ERRNO = -1;

		// Token: 0x04000828 RID: 2088
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04000829 RID: 2089
		private const int Z_DATA_ERROR = -3;

		// Token: 0x0400082A RID: 2090
		private const int Z_MEM_ERROR = -4;

		// Token: 0x0400082B RID: 2091
		private const int Z_BUF_ERROR = -5;

		// Token: 0x0400082C RID: 2092
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x0400082D RID: 2093
		private const int METHOD = 0;

		// Token: 0x0400082E RID: 2094
		private const int FLAG = 1;

		// Token: 0x0400082F RID: 2095
		private const int DICT4 = 2;

		// Token: 0x04000830 RID: 2096
		private const int DICT3 = 3;

		// Token: 0x04000831 RID: 2097
		private const int DICT2 = 4;

		// Token: 0x04000832 RID: 2098
		private const int DICT1 = 5;

		// Token: 0x04000833 RID: 2099
		private const int DICT0 = 6;

		// Token: 0x04000834 RID: 2100
		private const int BLOCKS = 7;

		// Token: 0x04000835 RID: 2101
		private const int CHECK4 = 8;

		// Token: 0x04000836 RID: 2102
		private const int CHECK3 = 9;

		// Token: 0x04000837 RID: 2103
		private const int CHECK2 = 10;

		// Token: 0x04000838 RID: 2104
		private const int CHECK1 = 11;

		// Token: 0x04000839 RID: 2105
		private const int DONE = 12;

		// Token: 0x0400083A RID: 2106
		private const int BAD = 13;

		// Token: 0x0400083B RID: 2107
		internal int mode;

		// Token: 0x0400083C RID: 2108
		internal int method;

		// Token: 0x0400083D RID: 2109
		internal long[] was = new long[1];

		// Token: 0x0400083E RID: 2110
		internal long need;

		// Token: 0x0400083F RID: 2111
		internal int marker;

		// Token: 0x04000840 RID: 2112
		internal int nowrap;

		// Token: 0x04000841 RID: 2113
		internal int wbits;

		// Token: 0x04000842 RID: 2114
		internal InfBlocks blocks;

		// Token: 0x04000843 RID: 2115
		private static readonly byte[] mark = new byte[]
		{
			0,
			0,
			byte.MaxValue,
			byte.MaxValue
		};
	}
}
