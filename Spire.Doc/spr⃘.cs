using System;
using System.Drawing;
using Spire.Doc.Fields.Shape;

// Token: 0x020002F7 RID: 759
internal class spr\u20D8
{
	// Token: 0x06002996 RID: 10646 RVA: 0x0029552C File Offset: 0x0029452C
	private spr\u20D8()
	{
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x00295540 File Offset: 0x00294540
	internal static Word97Color ᜁ(Color A_0)
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
		return spr\u20D8.ᜁ(spr\u20D8.ᜀ(A_0));
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x00295588 File Offset: 0x00294588
	internal static Color ᜁ(Word97Color A_0)
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
		return spr\u20D8.ᜀ(spr\u20D8.ᜀ(A_0));
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x002955D0 File Offset: 0x002945D0
	internal static Word97Color ᜁ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			Word97Color result;
			for (;;)
			{
				int num2;
				double num3;
				switch (num)
				{
				case 0:
				{
					result = (Word97Color)num2;
					double num4;
					num3 = num4;
					num = 4;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E9;
					default:
						goto IL_D3;
					}
					break;
				case 2:
				{
					if (num2 >= spr\u20D8.ᜀ.Length)
					{
						num = 1;
						continue;
					}
					double num4 = spr\u20D8.ᜀ(spr\u20D8.ᜀ[num2], A_0);
					num = 3;
					continue;
				}
				case 3:
				{
					double num4;
					if (num4 <= num3)
					{
						num = 0;
						continue;
					}
					goto IL_64;
				}
				case 4:
					goto IL_64;
				case 6:
					goto IL_9B;
				case 7:
					goto IL_9B;
				case 8:
					return Word97Color.Auto;
				}
				if (A_0 == -16777216)
				{
					num = 8;
					continue;
				}
				num3 = double.MaxValue;
				result = Word97Color.Black;
				num2 = 0;
				goto IL_E9;
				IL_64:
				num2++;
				num = 6;
				continue;
				IL_9B:
				num = 2;
				continue;
				IL_E9:
				if (true)
				{
				}
				num = 7;
			}
			return Word97Color.Auto;
			IL_D3:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x002956F4 File Offset: 0x002946F4
	private static double ᜀ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			double num7;
			for (;;)
			{
				int num = A_0 & 255;
				int num2 = (A_0 & 65280) >> 8;
				int num3 = (A_0 & 16711680) >> 16;
				int num4 = A_1 & 255;
				int num5 = (A_1 & 65280) >> 8;
				int num6 = (A_1 & 16711680) >> 16;
				spr\u21F9 spr_u21F = new spr\u21F9(Color.FromArgb(num, num2, num3));
				spr\u21F9 spr_u21F2 = new spr\u21F9(Color.FromArgb(num4, num5, num6));
				num7 = Math.Abs(spr_u21F.ᜀ() - spr_u21F2.ᜀ()) + Math.Abs(spr_u21F.ᜃ() - spr_u21F2.ᜃ());
				int num8 = 3;
				for (;;)
				{
					switch (num8)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10C;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num7 += Math.Abs(spr_u21F.ᜂ() - spr_u21F2.ᜂ());
							num8 = 2;
							continue;
						}
						break;
					case 1:
						goto IL_10C;
					case 2:
						return num7;
					case 3:
						if (!spr\u20D8.ᜀ(num, num2, num3))
						{
							num8 = 4;
							continue;
						}
						return num7;
					case 4:
						num8 = 1;
						continue;
					}
					break;
					IL_10C:
					if (spr\u20D8.ᜀ(num4, num5, num6))
					{
						return num7;
					}
					num8 = 0;
				}
			}
			return num7;
		}
		}
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x00295858 File Offset: 0x00294858
	private static bool ᜀ(int A_0, int A_1, int A_2)
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
			if (A_0 != A_1)
			{
				return false;
			}
			if (true)
			{
			}
			break;
		}
		return A_1 == A_2;
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x002958A0 File Offset: 0x002948A0
	internal static int ᜀ(Word97Color A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 >= (Word97Color)spr\u20D8.ᜀ.Length)
			{
				return -16777216;
			}
			break;
		}
		return spr\u20D8.ᜀ[(int)A_0];
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x002958F4 File Offset: 0x002948F4
	internal static int ᜀ(Color A_0)
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
		int num = 0;
		num |= (int)A_0.R;
		num |= (int)A_0.G << 8;
		num |= (int)A_0.B << 16;
		return num | (int)(~(int)A_0.A) << 24;
	}

	// Token: 0x0600299E RID: 10654 RVA: 0x00295964 File Offset: 0x00294964
	internal static Color ᜀ(int A_0)
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
		int red = A_0 & 255;
		int green = A_0 >> 8 & 255;
		int blue = A_0 >> 16 & 255;
		int num = A_0 >> 24 & 255;
		int alpha = ~num & 255;
		return Color.FromArgb(alpha, red, green, blue);
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x002959DC File Offset: 0x002949DC
	static spr\u20D8()
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
		spr\u20D8.ᜀ = new int[]
		{
			-16777216,
			0,
			16711680,
			16776960,
			65280,
			16711935,
			255,
			65535,
			16777215,
			8388608,
			8421376,
			32768,
			8388736,
			128,
			32896,
			8421504,
			12632256
		};
	}

	// Token: 0x04002412 RID: 9234
	private static readonly int[] ᜀ;
}
