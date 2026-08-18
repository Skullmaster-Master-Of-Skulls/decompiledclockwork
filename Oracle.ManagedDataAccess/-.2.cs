using System;

namespace \u0006
{
	// Token: 0x0200034A RID: 842
	internal class \u0001
	{
		// Token: 0x06001DA4 RID: 7588 RVA: 0x00123144 File Offset: 0x00121344
		internal static void \u0001(ushort[] \u0002, int \u0003, byte[] \u0004, int \u0005)
		{
			int num = \u0006.\u0001.\u0001(\u0004, \u0005) / 16 + 1;
			int num2 = \u0005 - 1;
			int num3 = 0;
			int num4 = (\u0003 < \u0005 / 2) ? \u0003 : (\u0005 / 2);
			\u0003 -= num4;
			\u0005 -= 2 * num4;
			while (num4-- > 0)
			{
				\u0002[num3] = (ushort)((int)(byte.MaxValue & \u0004[num2]) + ((int)(byte.MaxValue & \u0004[num2 - 1]) << 8));
				num3++;
				num2 -= 2;
			}
			if (\u0003 > 0 && \u0005 % 2 == 1)
			{
				\u0002[num3] = (ushort)(byte.MaxValue & \u0004[num2]);
				num2--;
				num3++;
				\u0003--;
				\u0005--;
			}
			while (\u0003-- > 0)
			{
				\u0002[num3++] = 0;
			}
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x001231E8 File Offset: 0x001213E8
		internal static void \u0001(byte[] \u0002, int \u0003, ushort[] \u0004, int \u0005)
		{
			int num = \u0003 - 1;
			int num2 = 0;
			int num3 = (\u0005 < \u0003 / 2) ? \u0005 : (\u0003 / 2);
			\u0005 -= num3;
			\u0003 -= 2 * num3;
			while (num3-- > 0)
			{
				\u0002[num--] = (byte.MaxValue & (byte)\u0004[num2]);
				\u0002[num--] = (byte)(\u0004[num2] >> 8);
				num2++;
			}
			if (\u0005 > 0 && \u0003 % 2 == 1)
			{
				\u0002[num--] = (byte.MaxValue & (byte)\u0004[num2]);
				num2++;
				\u0005--;
				\u0003--;
			}
			while (num-- > 0)
			{
				\u0002[num--] = 0;
			}
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00123280 File Offset: 0x00121480
		internal static void \u0001(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, ushort[] \u0005, int \u0006)
		{
			ushort[] array = new ushort[\u0006.\u0001.\u0001 + 2];
			bool[] array2 = new bool[64];
			ushort[][] array3 = new ushort[16][];
			ushort[] array4 = new ushort[\u0006.\u0001.\u0001];
			for (int i = 0; i < 16; i++)
			{
				array3[i] = new ushort[\u0006.\u0001.\u0001];
			}
			\u0006.\u0001.\u0003(array, \u0005, \u0006);
			int num = \u0006.\u0001.\u0002(\u0004, \u0006);
			int num2;
			if (num < 4)
			{
				num2 = 1;
			}
			else if (num < 16)
			{
				num2 = 2;
			}
			else if (num < 64)
			{
				num2 = 3;
			}
			else
			{
				num2 = 4;
			}
			\u0006.\u0001.\u0002(array3[0], 1, \u0006);
			\u0006.\u0001.\u0001(array3[1], \u0003, \u0006);
			array2[0] = true;
			array2[1] = true;
			for (int j = 2; j < 64; j++)
			{
				array2[j] = false;
			}
			int num3 = 0;
			bool flag = false;
			ushort num4 = (ushort)(1 << num % 16);
			for (int k = num; k >= 0; k--)
			{
				if (flag)
				{
					\u0006.\u0001.\u0002(array4, array4, \u0005, array, \u0006);
				}
				num3 <<= 1;
				if (!array2[num3])
				{
					\u0006.\u0001.\u0002(array3[num3], array3[num3 / 2], \u0005, array, \u0006);
					array2[num3] = true;
				}
				if ((\u0004[k / 16] & num4) > 0)
				{
					num3++;
				}
				if (num4 == 1)
				{
					num4 = 32768;
				}
				else
				{
					num4 = (ushort)(num4 >> 1 & 32767);
				}
				if (!array2[num3])
				{
					\u0006.\u0001.\u0001(array3[num3], array3[num3 - 1], \u0003, \u0005, array, \u0006);
					array2[num3] = true;
				}
				if (k == 0 || num3 >= 1 << num2 - 1)
				{
					if (flag)
					{
						\u0006.\u0001.\u0001(array4, array4, array3[num3], \u0005, array, \u0006);
					}
					else
					{
						\u0006.\u0001.\u0001(array4, array3[num3], \u0006);
					}
					num3 = 0;
					flag = true;
				}
			}
			\u0006.\u0001.\u0001(\u0002, array4, \u0006);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0012342C File Offset: 0x0012162C
		private static int \u0001(ushort[] \u0002, int \u0003)
		{
			if ((\u0002[\u0003 - 1] & 32768) > 0)
			{
				return -1;
			}
			for (int i = \u0003 - 1; i >= 0; i--)
			{
				if (\u0002[i] > 0)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00123460 File Offset: 0x00121660
		private static int \u0001(int \u0002)
		{
			\u0002--;
			int num = 0;
			while (\u0002 > 0)
			{
				num++;
				\u0002 >>= 1;
			}
			return num;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00123484 File Offset: 0x00121684
		private static int \u0001(byte[] \u0002, int \u0003)
		{
			int num = 0;
			while (num < \u0003 && \u0002[num] == 0)
			{
				num++;
			}
			if (num == \u0003)
			{
				return 0;
			}
			byte b = \u0002[num++];
			int num2 = 8;
			byte b2 = 128;
			while ((b & b2) == 0)
			{
				num2--;
				b2 = (byte)(b2 >> 1);
			}
			return 8 * (\u0003 - num) + num2;
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x001234D0 File Offset: 0x001216D0
		private static int \u0002(ushort[] \u0002, int \u0003)
		{
			ushort num = (ushort)(((\u0002[\u0003 - 1] & 32768) > 0) ? -1 : 0);
			int num2 = \u0003 - 1;
			while (num2 >= 0 && \u0002[num2] == num)
			{
				num2--;
			}
			if (num2 == -1)
			{
				return 1;
			}
			int num3 = 16;
			int num4 = 32768;
			while (num3 >= 0 && (num4 & (int)(num ^ \u0002[num2])) == 0)
			{
				num3--;
				num4 >>= 1;
			}
			return 16 * num2 + num3;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00123534 File Offset: 0x00121734
		private static int \u0002(int \u0002)
		{
			return 16 * ((\u0002 + 1 + 15) / 16);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00123544 File Offset: 0x00121744
		private static void \u0001(ushort[] \u0002, int \u0003, int \u0004)
		{
			for (int i = 0; i < \u0004; i++)
			{
				\u0002[i] = 0;
			}
			\u0002[\u0003 / 16] = (ushort)(1 << \u0003 % 16);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00123574 File Offset: 0x00121774
		private static void \u0001(ushort[] \u0002, int \u0003)
		{
			bool flag = true;
			int num = 0;
			while (num < \u0003 - 1 && flag)
			{
				\u0002[num] += 1;
				if (\u0002[num] > 0)
				{
					flag = false;
				}
				num++;
			}
			if (flag)
			{
				\u0002[num] += 1;
			}
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x001235B4 File Offset: 0x001217B4
		private static void \u0002(ushort[] \u0002, int \u0003)
		{
			int i = 0;
			while (i < \u0003)
			{
				\u0002[i++] = 0;
			}
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x001235D4 File Offset: 0x001217D4
		private static void \u0001(ushort[] \u0002, ushort[] \u0003, int \u0004)
		{
			for (int i = 0; i < \u0004; i++)
			{
				\u0002[i] = \u0003[i];
			}
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x001235F4 File Offset: 0x001217F4
		private static int \u0001(ushort[] \u0002, int \u0003, int \u0004)
		{
			for (int i = \u0004 - 1; i >= 0; i--)
			{
				if (\u0002[i + \u0003] > 0)
				{
					return i + 1;
				}
			}
			return 0;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0012361C File Offset: 0x0012181C
		private static ushort \u0001(ushort[] \u0002, int \u0003, ushort \u0004, ushort[] \u0005, int \u0006, int \u0007)
		{
			uint num = 0U;
			if (\u0004 <= 0)
			{
				return 0;
			}
			for (int i = 0; i < \u0007; i++)
			{
				num += (uint)(\u0004 * \u0005[i + \u0006]);
				num += (uint)\u0002[i + \u0003];
				\u0002[i + \u0003] = (ushort)num;
				num >>= 16;
			}
			return (ushort)num;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00123664 File Offset: 0x00121864
		private static void \u0001(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, int \u0005, int \u0006)
		{
			\u0006.\u0001.\u0002(\u0002, 2 * \u0006);
			int num = \u0006.\u0001.\u0001(\u0004, \u0005, \u0006);
			for (int i = 0; i < \u0006; i++)
			{
				\u0002[num + i] = \u0006.\u0001.\u0001(\u0002, i, \u0003[i], \u0004, \u0005, num);
			}
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x001236A4 File Offset: 0x001218A4
		private static void \u0001(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, int \u0005)
		{
			uint num = 0U;
			for (int i = 0; i < \u0005; i++)
			{
				num += (uint)\u0003[i];
				num += (uint)\u0004[i];
				\u0002[i] = (ushort)num;
				num >>= 16;
			}
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x001236D8 File Offset: 0x001218D8
		private static void \u0002(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, int \u0005, int \u0006)
		{
			uint num = 1U;
			for (int i = 0; i < \u0006; i++)
			{
				num += (uint)\u0003[i];
				num += (uint)(~\u0004[i + \u0005] & ushort.MaxValue);
				\u0002[i] = (ushort)num;
				num >>= 16;
			}
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00123714 File Offset: 0x00121914
		private static void \u0002(ushort[] \u0002, ushort[] \u0003, int \u0004)
		{
			uint num = 0U;
			\u0006.\u0001.\u0002(\u0002, 2 * \u0004);
			int num2 = \u0006.\u0001.\u0001(\u0003, 0, \u0004);
			if (num2 <= 0)
			{
				return;
			}
			int i;
			for (i = 0; i < num2 - 1; i++)
			{
				\u0002[num2 + i] = \u0006.\u0001.\u0001(\u0002, 2 * i + 1, \u0003[i], \u0003, i + 1, num2 - i - 1);
			}
			\u0006.\u0001.\u0001(\u0002, \u0002, \u0002, 2 * \u0004);
			for (i = 0; i < num2; i++)
			{
				num += (uint)(\u0003[i] * \u0003[i]);
				num += (uint)\u0002[2 * i];
				\u0002[2 * i] = (ushort)num;
				num >>= 16;
				num += (uint)\u0002[2 * i + 1];
				\u0002[2 * i + 1] = (ushort)num;
				num >>= 16;
			}
			\u0002[2 * i] = (ushort)num;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x001237B8 File Offset: 0x001219B8
		private static void \u0003(ushort[] \u0002, int \u0003)
		{
			bool flag = true;
			int num = 0;
			while (num < \u0003 - 1 && flag)
			{
				\u0002[num] -= 1;
				if (\u0002[num] != 65535)
				{
					flag = false;
				}
				num++;
			}
			if (flag)
			{
				\u0002[num] -= 1;
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x001237FC File Offset: 0x001219FC
		private static void \u0002(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, ushort[] \u0005, int \u0006)
		{
			ushort[] array = new ushort[2 * \u0006.\u0001.\u0001];
			\u0006.\u0001.\u0002(array, \u0003, \u0006);
			\u0006.\u0001.\u0003(\u0002, array, \u0004, \u0005, \u0006);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0012382C File Offset: 0x00121A2C
		private static void \u0003(ushort[] \u0002, ushort[] \u0003, int \u0004)
		{
			ushort[] array = new ushort[2 * (\u0006.\u0001.\u0001 + 2)];
			ushort[] array2 = new ushort[2 * (\u0006.\u0001.\u0001 + 3)];
			ushort[] array3 = new ushort[\u0006.\u0001.\u0001 + 3];
			int num = \u0006.\u0001.\u0002(\u0003, \u0004);
			int num2 = \u0006.\u0001.\u0002(2 * num);
			int num3 = num2 / 16;
			int num4 = (num - 2) / 16;
			\u0006.\u0001.\u0001(\u0002, num2 - num, \u0004 + 2);
			\u0006.\u0001.\u0001(\u0002, \u0004 + 2);
			\u0006.\u0001.\u0002(array3, \u0004 + 3);
			\u0006.\u0001.\u0001(array3, \u0003, \u0004);
			for (int i = 1 + \u0006.\u0001.\u0001(num2 - num + 1); i > 0; i--)
			{
				\u0006.\u0001.\u0002(array, \u0002, \u0004 + 2);
				\u0006.\u0001.\u0001(array2, array3, array, num4, \u0004 + 3);
				\u0006.\u0001.\u0001(\u0002, \u0002, \u0002, \u0004 + 2);
				\u0006.\u0001.\u0002(\u0002, \u0002, array2, num3 - num4, \u0004 + 2);
			}
			\u0006.\u0001.\u0001(\u0002, \u0004 + 2);
			for (;;)
			{
				\u0006.\u0001.\u0002(array, \u0002, array3, \u0004 + 2);
				\u0006.\u0001.\u0003(array, 2 * (\u0004 + 2));
				int num5 = \u0006.\u0001.\u0002(array, 2 * (\u0004 + 2));
				if (num5 <= num2)
				{
					break;
				}
				\u0006.\u0001.\u0003(\u0002, \u0004 + 2);
			}
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0012393C File Offset: 0x00121B3C
		private static void \u0002(ushort[] \u0002, int \u0003, int \u0004)
		{
			ushort num = (ushort)(((\u0003 & 32768) > 0) ? -1 : 0);
			\u0002[0] = (ushort)\u0003;
			for (int i = 1; i < \u0004; i++)
			{
				\u0002[i] = num;
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00123970 File Offset: 0x00121B70
		private static void \u0001(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, ushort[] \u0005, ushort[] \u0006, int \u0007)
		{
			ushort[] array = new ushort[2 * \u0006.\u0001.\u0001];
			\u0006.\u0001.\u0002(array, \u0003, \u0004, \u0007);
			\u0006.\u0001.\u0003(\u0002, array, \u0005, \u0006, \u0007);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x001239A0 File Offset: 0x00121BA0
		private static void \u0002(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, int \u0005)
		{
			\u0006.\u0001.\u0002(\u0002, 2 * \u0005);
			int num = \u0006.\u0001.\u0001(\u0004, 0, \u0005);
			for (int i = 0; i < \u0005; i++)
			{
				\u0002[num + i] = \u0006.\u0001.\u0001(\u0002, i, \u0003[i], \u0004, 0, num);
			}
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x001239E0 File Offset: 0x00121BE0
		private static void \u0003(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, ushort[] \u0005, int \u0006)
		{
			ushort[] u = new ushort[\u0006.\u0001.\u0001];
			\u0006.\u0001.\u0002(u, \u0002, \u0003, \u0004, \u0005, \u0006);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00123A04 File Offset: 0x00121C04
		private static void \u0001(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, int \u0005, int \u0006, int \u0007)
		{
			\u0006.\u0001.\u0002(\u0002, 2 * \u0007);
			int num = \u0006.\u0001.\u0001(\u0004, \u0005, \u0007);
			int num2 = (\u0006 >= \u0007 - 1) ? (\u0006 - (\u0007 - 1)) : 0;
			for (int i = num2; i < \u0007; i++)
			{
				int num3 = (\u0006 >= i) ? (\u0006 - i) : 0;
				\u0002[num + i] = \u0006.\u0001.\u0001(\u0002, i + num3, \u0003[i], \u0004, num3 + \u0005, (num >= num3) ? (num - num3) : 0);
			}
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00123A74 File Offset: 0x00121C74
		private static void \u0002(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, ushort[] \u0005, ushort[] \u0006, int \u0007)
		{
			ushort[] array = new ushort[2 * (\u0006.\u0001.\u0001 + 2)];
			ushort[] array2 = new ushort[2 * (\u0006.\u0001.\u0001 + 2)];
			ushort[] array3 = new ushort[2 * \u0006.\u0001.\u0001 + 2];
			int num = \u0006.\u0001.\u0002(\u0005, \u0007);
			int num2 = \u0006.\u0001.\u0002(2 * num);
			int num3 = num2 / 16;
			int num4 = (num - 2) / 16;
			int num5 = num3 - num4 - 3;
			if (num5 < 0)
			{
				num5 = 0;
			}
			\u0006.\u0001.\u0002(array3, 2 * \u0007 + 2);
			\u0006.\u0001.\u0001(array3, \u0004, 2 * \u0007);
			\u0006.\u0001.\u0001(array2, \u0006, array3, num4, num5, \u0007 + 2);
			for (int i = 0; i < \u0007; i++)
			{
				\u0002[i] = array2[i + (num3 - num4)];
			}
			\u0006.\u0001.\u0003(array, \u0002, \u0005, \u0007);
			\u0006.\u0001.\u0002(\u0003, \u0004, array, 0, \u0007);
			while (\u0006.\u0001.\u0001(\u0003, \u0005, \u0007) >= 0)
			{
				\u0006.\u0001.\u0002(\u0003, \u0003, \u0005, 0, \u0007);
				\u0006.\u0001.\u0001(\u0002, \u0007);
			}
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00123B60 File Offset: 0x00121D60
		private static int \u0001(ushort[] \u0002, ushort[] \u0003, int \u0004)
		{
			int num = \u0006.\u0001.\u0001(\u0002, \u0004);
			int num2 = \u0006.\u0001.\u0001(\u0003, \u0004);
			if (num > num2)
			{
				return 1;
			}
			if (num < num2)
			{
				return -1;
			}
			int num3 = \u0004 - 1;
			while (num3 >= 0 && \u0002[num3] == \u0003[num3])
			{
				num3--;
			}
			if (num3 == -1)
			{
				return 0;
			}
			if (\u0002[num3] > \u0003[num3])
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00123BB0 File Offset: 0x00121DB0
		private static void \u0003(ushort[] \u0002, ushort[] \u0003, ushort[] \u0004, int \u0005)
		{
			\u0006.\u0001.\u0002(\u0002, \u0005);
			int num = \u0006.\u0001.\u0001(\u0004, 0, \u0005);
			for (int i = 0; i < \u0005; i++)
			{
				if (num < \u0005 - i)
				{
					\u0002[num + i] = \u0006.\u0001.\u0001(\u0002, i, \u0003[i], \u0004, 0, num);
				}
				else
				{
					\u0006.\u0001.\u0001(\u0002, i, \u0003[i], \u0004, 0, \u0005 - i);
				}
			}
		}

		// Token: 0x04002011 RID: 8209
		private static readonly int \u0001 = 257;
	}
}
