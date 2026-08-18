using System;

// Token: 0x020001FF RID: 511
internal class spr\u2580
{
	// Token: 0x0600165F RID: 5727 RVA: 0x0016A150 File Offset: 0x00169150
	public static void ᜀ(ref long A_0, byte[] A_1, int A_2, int A_3)
	{
		switch (0)
		{
		default:
		{
			uint num;
			uint num2;
			uint num3;
			for (;;)
			{
				num = (uint)A_0;
				num2 = (num & 65535U);
				num3 = num >> 16;
				int num4 = 0;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						goto IL_B1;
					case 1:
						num2 %= 65521U;
						num3 %= 65521U;
						if (true)
						{
						}
						num4 = 4;
						continue;
					case 2:
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (A_3 <= 0)
							{
								num4 = 5;
								continue;
							}
							break;
						}
						int num5 = Math.Min(A_3, 3800);
						A_3 -= num5;
						num4 = 7;
						continue;
					}
					case 3:
					{
						int num5;
						if (--num5 < 0)
						{
							num4 = 1;
							continue;
						}
						num2 += (uint)(A_1[A_2++] & byte.MaxValue);
						num3 += num2;
						num4 = 6;
						continue;
					}
					case 4:
						goto IL_B1;
					case 5:
						goto IL_F3;
					case 6:
						goto IL_76;
					case 7:
						goto IL_76;
					}
					break;
					IL_76:
					num4 = 3;
					continue;
					IL_B1:
					num4 = 2;
				}
			}
			IL_F3:
			num = (num3 << 16 | num2);
			A_0 = (long)((ulong)num);
			return;
		}
		}
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x0016A284 File Offset: 0x00169284
	public static long ᜀ(byte[] A_0, int A_1, int A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		long result = 1L;
		spr\u2580.ᜀ(ref result, A_0, A_1, A_2);
		return result;
	}

	// Token: 0x04001A26 RID: 6694
	private const int ᜀ = 16;

	// Token: 0x04001A27 RID: 6695
	private const int ᜁ = 65521;

	// Token: 0x04001A28 RID: 6696
	private const int ᜂ = 3800;
}
