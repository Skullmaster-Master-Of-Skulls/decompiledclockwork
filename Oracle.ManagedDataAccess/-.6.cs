using System;

namespace \u0002
{
	// Token: 0x02000354 RID: 852
	internal sealed class \u0003 : \u0002
	{
		// Token: 0x06001E0E RID: 7694 RVA: 0x0012567C File Offset: 0x0012387C
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004)
		{
			this.\u0003(\u0002, \u0003, \u0004);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00125688 File Offset: 0x00123888
		void \u0002.\u0002()
		{
			this.\u0003();
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00125690 File Offset: 0x00123890
		int \u0002.\u0002(byte[] \u0002, int \u0003)
		{
			return this.\u0003(\u0002, \u0003);
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0012569C File Offset: 0x0012389C
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
			this.\u0003(\u0002, \u0003, \u0004, \u0005, \u0006, ref \u0007);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x001256B0 File Offset: 0x001238B0
		internal \u0003()
		{
			this.\u0003();
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x001256D8 File Offset: 0x001238D8
		public int \u0001()
		{
			return global::\u0002.\u0003.\u0001;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x001256E0 File Offset: 0x001238E0
		private void \u0001(byte \u0002)
		{
			int num = ((int)this.\u0004 & global::\u0002.\u0003.\u0002) >> 2;
			int num2 = (~(int)this.\u0004 & 3) << 3;
			this.\u0003[num] = ((this.\u0003[num] & ~(255U << num2)) | (uint)((uint)(\u0002 & byte.MaxValue) << num2));
			if (((int)this.\u0004 & global::\u0002.\u0003.\u0002) == global::\u0002.\u0003.\u0002)
			{
				this.\u0004();
			}
			this.\u0004 += 1L;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0012575C File Offset: 0x0012395C
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004)
		{
			while (\u0004 > 0)
			{
				if (((int)this.\u0004 & 3) != 0 || \u0004 < 4)
				{
					this.\u0001(\u0002[\u0003]);
					\u0003++;
					\u0004--;
				}
				else
				{
					int num = ((int)this.\u0004 & global::\u0002.\u0003.\u0002) >> 2;
					this.\u0003[num] = (uint)((int)(\u0002[\u0003] & byte.MaxValue) << 24 | (int)(\u0002[\u0003 + 1] & byte.MaxValue) << 16 | (int)(\u0002[\u0003 + 2] & byte.MaxValue) << 8 | (int)(\u0002[\u0003 + 3] & byte.MaxValue));
					this.\u0004 += 4L;
					if (((int)this.\u0004 & global::\u0002.\u0003.\u0002) == 0)
					{
						this.\u0004();
					}
					\u0003 += 4;
					\u0004 -= 4;
				}
			}
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00125814 File Offset: 0x00123A14
		internal void \u0003()
		{
			this.\u0005[0] = 1732584193U;
			this.\u0005[1] = 4023233417U;
			this.\u0005[2] = 2562383102U;
			this.\u0005[3] = 271733878U;
			this.\u0005[4] = 3285377520U;
			for (int i = 0; i < 80; i++)
			{
				this.\u0003[i] = 0U;
			}
			this.\u0004 = 0L;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00125880 File Offset: 0x00123A80
		internal int \u0003(byte[] \u0002, int \u0003)
		{
			if (\u0002.Length - \u0003 < global::\u0002.\u0003.\u0001)
			{
				return 0;
			}
			long num = this.\u0004 << 3;
			this.\u0001(128);
			while ((int)(this.\u0004 & (long)global::\u0002.\u0003.\u0002) != 56)
			{
				this.\u0001(0);
			}
			this.\u0003[14] = (uint)(num >> 32);
			this.\u0003[15] = (uint)num;
			this.\u0004 += 8L;
			this.\u0004();
			int i = 0;
			int num2 = 0;
			while (i < this.\u0005.Length)
			{
				\u0002[\u0003 + num2] = (byte)(this.\u0005[i] >> 24);
				\u0002[\u0003 + num2 + 1] = (byte)(this.\u0005[i] >> 16);
				\u0002[\u0003 + num2 + 2] = (byte)(this.\u0005[i] >> 8);
				\u0002[\u0003 + num2 + 3] = (byte)this.\u0005[i];
				i++;
				num2 += 4;
			}
			this.\u0003();
			return global::\u0002.\u0003.\u0001;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00125964 File Offset: 0x00123B64
		private void \u0004()
		{
			this.\u0001(this.\u0003);
			uint num = this.\u0005[0];
			uint num2 = this.\u0005[1];
			uint num3 = this.\u0005[2];
			uint num4 = this.\u0005[3];
			uint num5 = this.\u0005[4];
			for (int i = 0; i < 20; i++)
			{
				this.\u0001(this.\u0005, this.\u0003, this.\u0005[0], this.\u0005[1], this.\u0005[2], this.\u0005[3], this.\u0005[4], i);
			}
			for (int j = 20; j < 40; j++)
			{
				this.\u0002(this.\u0005, this.\u0003, this.\u0005[0], this.\u0005[1], this.\u0005[2], this.\u0005[3], this.\u0005[4], j);
			}
			for (int k = 40; k < 60; k++)
			{
				this.\u0003(this.\u0005, this.\u0003, this.\u0005[0], this.\u0005[1], this.\u0005[2], this.\u0005[3], this.\u0005[4], k);
			}
			for (int l = 60; l < 80; l++)
			{
				this.\u0004(this.\u0005, this.\u0003, this.\u0005[0], this.\u0005[1], this.\u0005[2], this.\u0005[3], this.\u0005[4], l);
			}
			this.\u0005[0] += num;
			this.\u0005[1] += num2;
			this.\u0005[2] += num3;
			this.\u0005[3] += num4;
			this.\u0005[4] += num5;
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00125B60 File Offset: 0x00123D60
		private void \u0001(uint[] \u0002)
		{
			for (int i = 16; i < 80; i++)
			{
				uint num = \u0002[i - 16] ^ \u0002[i - 14] ^ \u0002[i - 8] ^ \u0002[i - 3];
				\u0002[i] = (num << 1 | num >> 31);
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00125BA0 File Offset: 0x00123DA0
		private void \u0001(uint[] \u0002, uint[] \u0003, uint \u0004, uint \u0005, uint \u0006, uint \u0007, uint \u0008, int \u000E)
		{
			uint num = (\u0004 << 5 | \u0004 >> 27) + ((\u0005 & \u0006) | (~\u0005 & \u0007)) + \u0008 + \u0003[\u000E] + global::\u0002.\u0003.\u0006;
			\u0002[4] = \u0002[3];
			\u0002[3] = \u0002[2];
			\u0002[2] = (\u0002[1] << 30 | \u0002[1] >> 2);
			\u0002[1] = \u0002[0];
			\u0002[0] = num;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00125BF8 File Offset: 0x00123DF8
		private void \u0002(uint[] \u0002, uint[] \u0003, uint \u0004, uint \u0005, uint \u0006, uint \u0007, uint \u0008, int \u000E)
		{
			uint num = (\u0004 << 5 | \u0004 >> 27) + (\u0005 ^ \u0006 ^ \u0007) + \u0008 + \u0003[\u000E] + global::\u0002.\u0003.\u0007;
			\u0002[4] = \u0002[3];
			\u0002[3] = \u0002[2];
			\u0002[2] = (\u0002[1] << 30 | \u0002[1] >> 2);
			\u0002[1] = \u0002[0];
			\u0002[0] = num;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00125C4C File Offset: 0x00123E4C
		private void \u0003(uint[] \u0002, uint[] \u0003, uint \u0004, uint \u0005, uint \u0006, uint \u0007, uint \u0008, int \u000E)
		{
			uint num = (\u0004 << 5 | \u0004 >> 27) + ((\u0005 & \u0006) | (\u0005 & \u0007) | (\u0006 & \u0007)) + \u0008 + \u0003[\u000E] + global::\u0002.\u0003.\u0008;
			\u0002[4] = \u0002[3];
			\u0002[3] = \u0002[2];
			\u0002[2] = (\u0002[1] << 30 | \u0002[1] >> 2);
			\u0002[1] = \u0002[0];
			\u0002[0] = num;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00125CA8 File Offset: 0x00123EA8
		private void \u0004(uint[] \u0002, uint[] \u0003, uint \u0004, uint \u0005, uint \u0006, uint \u0007, uint \u0008, int \u000E)
		{
			uint num = (\u0004 << 5 | \u0004 >> 27) + (\u0005 ^ \u0006 ^ \u0007) + \u0008 + \u0003[\u000E] + global::\u0002.\u0003.\u000E;
			\u0002[4] = \u0002[3];
			\u0002[3] = \u0002[2];
			\u0002[2] = (\u0002[1] << 30 | \u0002[1] >> 2);
			\u0002[1] = \u0002[0];
			\u0002[0] = num;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00125CFC File Offset: 0x00123EFC
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
		}

		// Token: 0x0400204F RID: 8271
		private static int \u0001 = 20;

		// Token: 0x04002050 RID: 8272
		private static int \u0002 = 63;

		// Token: 0x04002051 RID: 8273
		private uint[] \u0003 = new uint[80];

		// Token: 0x04002052 RID: 8274
		private long \u0004;

		// Token: 0x04002053 RID: 8275
		private uint[] \u0005 = new uint[5];

		// Token: 0x04002054 RID: 8276
		private static uint \u0006 = 1518500249U;

		// Token: 0x04002055 RID: 8277
		private static uint \u0007 = 1859775393U;

		// Token: 0x04002056 RID: 8278
		private static uint \u0008 = 2400959708U;

		// Token: 0x04002057 RID: 8279
		private static uint \u000E = 3395469782U;
	}
}
