using System;
using \u0002;

namespace \u0008
{
	// Token: 0x02000350 RID: 848
	internal sealed class \u0001 : \u0002
	{
		// Token: 0x06001DE4 RID: 7652 RVA: 0x00124618 File Offset: 0x00122818
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004)
		{
			this.\u0003(\u0002, \u0003, \u0004);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00124624 File Offset: 0x00122824
		void \u0002.\u0002()
		{
			this.\u0003();
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0012462C File Offset: 0x0012282C
		int \u0002.\u0002(byte[] \u0002, int \u0003)
		{
			return this.\u0003(\u0002, \u0003);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00124638 File Offset: 0x00122838
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
			this.\u0003(\u0002, \u0003, \u0004, \u0005, \u0006, ref \u0007);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0012464C File Offset: 0x0012284C
		private void \u0003(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x00124650 File Offset: 0x00122850
		private long \u0001(long \u0002, long \u0003, long \u0004)
		{
			return (\u0002 & \u0003) | (~\u0002 & \u0004);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0012465C File Offset: 0x0012285C
		private long \u0002(long \u0002, long \u0003, long \u0004)
		{
			return (\u0002 & \u0004) | (\u0003 & ~\u0004);
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00124668 File Offset: 0x00122868
		private long \u0003(long \u0002, long \u0003, long \u0004)
		{
			return \u0002 ^ \u0003 ^ \u0004;
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00124670 File Offset: 0x00122870
		private long \u0004(long \u0002, long \u0003, long \u0004)
		{
			return \u0003 ^ (\u0002 | ~\u0004);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00124678 File Offset: 0x00122878
		private long \u0001(long \u0002, int \u0003)
		{
			long num = (long)((ulong)-1);
			long num2 = \u0002 << \u0003;
			long num3 = num << \u0003;
			long num4 = ~num3 & num;
			long num5 = \u0002 >> 32 - \u0003 & num4;
			return num5 | num2;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x001246AC File Offset: 0x001228AC
		private long \u0001(long \u0002, long \u0003, long \u0004, long \u0005, long \u0006, int \u0007, long \u0008)
		{
			\u0002 += this.\u0001(\u0003, \u0004, \u0005) + \u0006 + \u0008;
			\u0002 = this.\u0001(\u0002, \u0007);
			\u0002 += \u0003;
			return \u0002;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x001246D4 File Offset: 0x001228D4
		private long \u0002(long \u0002, long \u0003, long \u0004, long \u0005, long \u0006, int \u0007, long \u0008)
		{
			\u0002 += this.\u0002(\u0003, \u0004, \u0005) + \u0006 + \u0008;
			\u0002 = this.\u0001(\u0002, \u0007);
			\u0002 += \u0003;
			return \u0002;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x001246FC File Offset: 0x001228FC
		private long \u0003(long \u0002, long \u0003, long \u0004, long \u0005, long \u0006, int \u0007, long \u0008)
		{
			\u0002 += this.\u0003(\u0003, \u0004, \u0005) + \u0006 + \u0008;
			\u0002 = this.\u0001(\u0002, \u0007);
			\u0002 += \u0003;
			return \u0002;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00124724 File Offset: 0x00122924
		private long \u0004(long \u0002, long \u0003, long \u0004, long \u0005, long \u0006, int \u0007, long \u0008)
		{
			\u0002 += this.\u0004(\u0003, \u0004, \u0005) + \u0006 + \u0008;
			\u0002 = this.\u0001(\u0002, \u0007);
			\u0002 += \u0003;
			return \u0002;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0012474C File Offset: 0x0012294C
		private void \u0001(byte[] \u0002, int \u0003, byte[] \u0004, int \u0005)
		{
			for (int i = 0; i < \u0005; i++)
			{
				\u0002[i + \u0003] = (\u0004[i] & byte.MaxValue);
			}
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00124778 File Offset: 0x00122978
		private void \u0001(byte[] \u0002, byte[] \u0003, int \u0004, int \u0005)
		{
			for (int i = 0; i < \u0005; i++)
			{
				\u0002[i] = \u0003[i + \u0004];
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x0012479C File Offset: 0x0012299C
		internal void \u0003()
		{
			this.\u0002[0] = 0L;
			this.\u0002[1] = 0L;
			this.\u0003[0] = 1732584193L;
			this.\u0003[1] = (long)((ulong)-271733879);
			this.\u0003[2] = (long)((ulong)-1732584194);
			this.\u0003[3] = 271733878L;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x001247F8 File Offset: 0x001229F8
		private void \u0001(byte[] \u0002, int \u0003, long[] \u0004, int \u0005)
		{
			int i = 0;
			int num = 0;
			while (i < \u0005)
			{
				\u0002[num + \u0003] = (byte)(\u0004[i] & 255L);
				\u0002[num + 1 + \u0003] = (byte)(\u0004[i] >> 8 & 255L);
				\u0002[num + 2 + \u0003] = (byte)(\u0004[i] >> 16 & 255L);
				\u0002[num + 3 + \u0003] = (byte)(\u0004[i] >> 24 & 255L);
				i++;
				num += 4;
			}
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00124868 File Offset: 0x00122A68
		private void \u0001(long[] \u0002, byte[] \u0003, int \u0004)
		{
			int num = 0;
			for (int i = 0; i < \u0004; i += 4)
			{
				\u0002[num] = (long)(((ulong)\u0003[i] & 255UL) | ((ulong)\u0003[i + 1] << 8 & 65280UL) | ((ulong)\u0003[i + 2] << 16 & 16711680UL) | ((ulong)\u0003[i + 3] << 24 & (ulong)-16777216));
				num++;
			}
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x001248C8 File Offset: 0x00122AC8
		private void \u0001(long[] \u0002, long[] \u0003)
		{
			long num = \u0002[0];
			long num2 = \u0002[1];
			long num3 = \u0002[2];
			long num4 = \u0002[3];
			int u = 7;
			int u2 = 12;
			int u3 = 17;
			int u4 = 22;
			num = this.\u0001(num, num2, num3, num4, \u0003[0], u, (long)((ulong)-680876936));
			num4 = this.\u0001(num4, num, num2, num3, \u0003[1], u2, (long)((ulong)-389564586));
			num3 = this.\u0001(num3, num4, num, num2, \u0003[2], u3, 606105819L);
			num2 = this.\u0001(num2, num3, num4, num, \u0003[3], u4, (long)((ulong)-1044525330));
			num = this.\u0001(num, num2, num3, num4, \u0003[4], u, (long)((ulong)-176418897));
			num4 = this.\u0001(num4, num, num2, num3, \u0003[5], u2, 1200080426L);
			num3 = this.\u0001(num3, num4, num, num2, \u0003[6], u3, (long)((ulong)-1473231341));
			num2 = this.\u0001(num2, num3, num4, num, \u0003[7], u4, (long)((ulong)-45705983));
			num = this.\u0001(num, num2, num3, num4, \u0003[8], u, 1770035416L);
			num4 = this.\u0001(num4, num, num2, num3, \u0003[9], u2, (long)((ulong)-1958414417));
			num3 = this.\u0001(num3, num4, num, num2, \u0003[10], u3, (long)((ulong)-42063));
			num2 = this.\u0001(num2, num3, num4, num, \u0003[11], u4, (long)((ulong)-1990404162));
			num = this.\u0001(num, num2, num3, num4, \u0003[12], u, 1804603682L);
			num4 = this.\u0001(num4, num, num2, num3, \u0003[13], u2, (long)((ulong)-40341101));
			num3 = this.\u0001(num3, num4, num, num2, \u0003[14], u3, (long)((ulong)-1502002290));
			num2 = this.\u0001(num2, num3, num4, num, \u0003[15], u4, 1236535329L);
			int u5 = 5;
			int u6 = 9;
			int u7 = 14;
			int u8 = 20;
			num = this.\u0002(num, num2, num3, num4, \u0003[1], u5, (long)((ulong)-165796510));
			num4 = this.\u0002(num4, num, num2, num3, \u0003[6], u6, (long)((ulong)-1069501632));
			num3 = this.\u0002(num3, num4, num, num2, \u0003[11], u7, 643717713L);
			num2 = this.\u0002(num2, num3, num4, num, \u0003[0], u8, (long)((ulong)-373897302));
			num = this.\u0002(num, num2, num3, num4, \u0003[5], u5, (long)((ulong)-701558691));
			num4 = this.\u0002(num4, num, num2, num3, \u0003[10], u6, 38016083L);
			num3 = this.\u0002(num3, num4, num, num2, \u0003[15], u7, (long)((ulong)-660478335));
			num2 = this.\u0002(num2, num3, num4, num, \u0003[4], u8, (long)((ulong)-405537848));
			num = this.\u0002(num, num2, num3, num4, \u0003[9], u5, 568446438L);
			num4 = this.\u0002(num4, num, num2, num3, \u0003[14], u6, (long)((ulong)-1019803690));
			num3 = this.\u0002(num3, num4, num, num2, \u0003[3], u7, (long)((ulong)-187363961));
			num2 = this.\u0002(num2, num3, num4, num, \u0003[8], u8, 1163531501L);
			num = this.\u0002(num, num2, num3, num4, \u0003[13], u5, (long)((ulong)-1444681467));
			num4 = this.\u0002(num4, num, num2, num3, \u0003[2], u6, (long)((ulong)-51403784));
			num3 = this.\u0002(num3, num4, num, num2, \u0003[7], u7, 1735328473L);
			num2 = this.\u0002(num2, num3, num4, num, \u0003[12], u8, (long)((ulong)-1926607734));
			int u9 = 4;
			int u10 = 11;
			int u11 = 16;
			int u12 = 23;
			num = this.\u0003(num, num2, num3, num4, \u0003[5], u9, (long)((ulong)-378558));
			num4 = this.\u0003(num4, num, num2, num3, \u0003[8], u10, (long)((ulong)-2022574463));
			num3 = this.\u0003(num3, num4, num, num2, \u0003[11], u11, 1839030562L);
			num2 = this.\u0003(num2, num3, num4, num, \u0003[14], u12, (long)((ulong)-35309556));
			num = this.\u0003(num, num2, num3, num4, \u0003[1], u9, (long)((ulong)-1530992060));
			num4 = this.\u0003(num4, num, num2, num3, \u0003[4], u10, 1272893353L);
			num3 = this.\u0003(num3, num4, num, num2, \u0003[7], u11, (long)((ulong)-155497632));
			num2 = this.\u0003(num2, num3, num4, num, \u0003[10], u12, (long)((ulong)-1094730640));
			num = this.\u0003(num, num2, num3, num4, \u0003[13], u9, 681279174L);
			num4 = this.\u0003(num4, num, num2, num3, \u0003[0], u10, (long)((ulong)-358537222));
			num3 = this.\u0003(num3, num4, num, num2, \u0003[3], u11, (long)((ulong)-722521979));
			num2 = this.\u0003(num2, num3, num4, num, \u0003[6], u12, 76029189L);
			num = this.\u0003(num, num2, num3, num4, \u0003[9], u9, (long)((ulong)-640364487));
			num4 = this.\u0003(num4, num, num2, num3, \u0003[12], u10, (long)((ulong)-421815835));
			num3 = this.\u0003(num3, num4, num, num2, \u0003[15], u11, 530742520L);
			num2 = this.\u0003(num2, num3, num4, num, \u0003[2], u12, (long)((ulong)-995338651));
			int u13 = 6;
			int u14 = 10;
			int u15 = 15;
			int u16 = 21;
			num = this.\u0004(num, num2, num3, num4, \u0003[0], u13, (long)((ulong)-198630844));
			num4 = this.\u0004(num4, num, num2, num3, \u0003[7], u14, 1126891415L);
			num3 = this.\u0004(num3, num4, num, num2, \u0003[14], u15, (long)((ulong)-1416354905));
			num2 = this.\u0004(num2, num3, num4, num, \u0003[5], u16, (long)((ulong)-57434055));
			num = this.\u0004(num, num2, num3, num4, \u0003[12], u13, 1700485571L);
			num4 = this.\u0004(num4, num, num2, num3, \u0003[3], u14, (long)((ulong)-1894986606));
			num3 = this.\u0004(num3, num4, num, num2, \u0003[10], u15, (long)((ulong)-1051523));
			num2 = this.\u0004(num2, num3, num4, num, \u0003[1], u16, (long)((ulong)-2054922799));
			num = this.\u0004(num, num2, num3, num4, \u0003[8], u13, 1873313359L);
			num4 = this.\u0004(num4, num, num2, num3, \u0003[15], u14, (long)((ulong)-30611744));
			num3 = this.\u0004(num3, num4, num, num2, \u0003[6], u15, (long)((ulong)-1560198380));
			num2 = this.\u0004(num2, num3, num4, num, \u0003[13], u16, 1309151649L);
			num = this.\u0004(num, num2, num3, num4, \u0003[4], u13, (long)((ulong)-145523070));
			num4 = this.\u0004(num4, num, num2, num3, \u0003[11], u14, (long)((ulong)-1120210379));
			num3 = this.\u0004(num3, num4, num, num2, \u0003[2], u15, 718787259L);
			num2 = this.\u0004(num2, num3, num4, num, \u0003[9], u16, (long)((ulong)-343485551));
			\u0002[0] += num;
			\u0002[1] += num2;
			\u0002[2] += num3;
			\u0002[3] += num4;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00124F10 File Offset: 0x00123110
		public \u0001()
		{
			this.\u0003();
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00124F50 File Offset: 0x00123150
		public int \u0001()
		{
			return \u0008.\u0001.\u0001;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00124F58 File Offset: 0x00123158
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004)
		{
			long[] array = new long[16];
			int num = (int)(this.\u0002[0] >> 3 & 63L);
			if (this.\u0002[0] + (long)((long)\u0004 << 3) < this.\u0002[0])
			{
				this.\u0002[1] += 1L;
			}
			this.\u0002[0] += (long)\u0004 << 3;
			this.\u0002[1] += (long)\u0004 >> 29;
			int num2 = 0;
			while (\u0004-- > 0)
			{
				this.\u0004[num++] = \u0002[\u0003 + num2++];
				if (num == 64)
				{
					this.\u0001(array, this.\u0004, 64);
					this.\u0001(this.\u0003, array);
					num = 0;
				}
			}
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0012502C File Offset: 0x0012322C
		internal int \u0003(byte[] \u0002, int \u0003)
		{
			if (\u0002.Length - \u0003 < \u0008.\u0001.\u0001)
			{
				return 0;
			}
			long[] array = new long[]
			{
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				0L,
				this.\u0002[0],
				this.\u0002[1]
			};
			int num = (int)(this.\u0002[0] >> 3 & 63L);
			int u = (num < 56) ? (56 - num) : (120 - num);
			this.\u0003(\u0008.\u0001.\u0006, 0, u);
			this.\u0001(array, this.\u0004, 56);
			this.\u0001(this.\u0003, array);
			this.\u0001(\u0002, \u0003, this.\u0003, 4);
			return \u0008.\u0001.\u0001;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x001250C4 File Offset: 0x001232C4
		// Note: this type is marked as 'beforefieldinit'.
		static \u0001()
		{
			byte[] array = new byte[64];
			array[0] = 128;
			\u0008.\u0001.\u0006 = array;
		}

		// Token: 0x04002039 RID: 8249
		private static int \u0001 = 16;

		// Token: 0x0400203A RID: 8250
		private long[] \u0002 = new long[2];

		// Token: 0x0400203B RID: 8251
		private long[] \u0003 = new long[4];

		// Token: 0x0400203C RID: 8252
		private byte[] \u0004 = new byte[64];

		// Token: 0x0400203D RID: 8253
		private byte[] \u0005 = new byte[16];

		// Token: 0x0400203E RID: 8254
		private static byte[] \u0006;
	}
}
