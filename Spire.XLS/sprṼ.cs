using System;

// Token: 0x02000519 RID: 1305
internal class sprṼ
{
	// Token: 0x06004F4F RID: 20303 RVA: 0x002FFC60 File Offset: 0x002FEC60
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
						goto IL_CD;
					case 1:
						goto IL_E9;
					case 2:
						goto IL_CD;
					case 3:
						if (A_3 <= 0)
						{
							num4 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_53;
						default:
						{
							if (false)
							{
							}
							int num5 = Math.Min(A_3, 3800);
							A_3 -= num5;
							num4 = 5;
							continue;
						}
						}
						break;
					case 4:
						goto IL_76;
					case 5:
						goto IL_76;
					case 6:
					{
						int num5;
						if (--num5 < 0)
						{
							num4 = 7;
							continue;
						}
						num2 += (uint)(A_1[A_2++] & byte.MaxValue);
						num3 += num2;
						num4 = 4;
						continue;
					}
					case 7:
						goto IL_53;
					}
					break;
					IL_53:
					num2 %= 65521U;
					num3 %= 65521U;
					if (true)
					{
					}
					num4 = 2;
					continue;
					IL_76:
					num4 = 6;
					continue;
					IL_CD:
					num4 = 3;
				}
			}
			IL_E9:
			num = (num3 << 16 | num2);
			A_0 = (long)((ulong)num);
			return;
		}
		}
	}

	// Token: 0x06004F50 RID: 20304 RVA: 0x002FFD94 File Offset: 0x002FED94
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
		sprṼ.ᜀ(ref result, A_0, A_1, A_2);
		return result;
	}

	// Token: 0x040023C3 RID: 9155
	private const int ᜀ = 16;

	// Token: 0x040023C4 RID: 9156
	private const int ᜁ = 65521;

	// Token: 0x040023C5 RID: 9157
	private const int ᜂ = 3800;
}
