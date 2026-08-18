using System;
using System.Drawing;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x020001E6 RID: 486
internal class spr\u25D1
{
	// Token: 0x0600151C RID: 5404 RVA: 0x00158C98 File Offset: 0x00157C98
	private spr\u25D1()
	{
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x00158CAC File Offset: 0x00157CAC
	internal static string ᜈ(string A_0)
	{
		for (;;)
		{
			IL_34:
			if (true)
			{
			}
			int num = 0;
			int num2 = 3;
			for (;;)
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
					switch (num2)
					{
					case 0:
						return A_0;
					case 1:
						if (num >= 9)
						{
							num2 = 0;
							continue;
						}
						A_0 = A_0.Replace(((char)num).ToString(), spr\u25D1.ᜂ(num));
						num++;
						goto IL_85;
					case 2:
						goto IL_48;
					case 3:
						goto IL_48;
					}
					goto IL_34;
					IL_48:
					num2 = 1;
					continue;
				}
				IL_85:
				num2 = 2;
			}
		}
		return A_0;
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x00158D4C File Offset: 0x00157D4C
	internal static string ᜂ(int A_0)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(ClipboardData.b("側౶䥸ٺ", a_), A_0 + 1);
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x00158DAC File Offset: 0x00157DAC
	internal static string ᜀ(int[] A_0)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder();
			int i = 0;
			int num = 0;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					goto IL_54;
				case 1:
					num = 2;
					continue;
				case 2:
					if (stringBuilder.Length > 0)
					{
						num = 5;
						continue;
					}
					goto IL_E1;
				case 3:
					while (i >= A_0.Length)
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
							num = 1;
							goto IL_02;
						}
					}
					stringBuilder.Append(sprᜌ.\u170D(A_0[i]));
					stringBuilder.Append(',');
					i++;
					num = 4;
					continue;
				case 4:
					if (true)
					{
					}
					goto IL_54;
				case 5:
					stringBuilder.Length--;
					num = 6;
					continue;
				case 6:
					goto IL_DF;
				}
				break;
				IL_54:
				num = 3;
			}
		}
		IL_DF:
		IL_E1:
		return stringBuilder.ToString();
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x00158EA0 File Offset: 0x00157EA0
	internal static string ᜀ(byte[] A_0, int A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return spr\u1CC6.ᜀ(A_0, A_1, 4, true);
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x00158EE4 File Offset: 0x00157EE4
	internal static string ᜁ(int A_0)
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
		return sprᜌ.ᜀ(A_0);
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x00158F28 File Offset: 0x00157F28
	internal static string ᜀ(object A_0)
	{
		if (!(A_0 is int))
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
				return null;
			}
		}
		if (true)
		{
		}
		return sprᜌ.ᜀ((int)A_0);
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x00158F7C File Offset: 0x00157F7C
	internal static int ᜇ(string A_0)
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
		return sprᜌ.ᜃ(A_0);
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x00158FC0 File Offset: 0x00157FC0
	internal static int ᜆ(string A_0)
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
		return sprᜌ.ᜄ(A_0);
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x00159004 File Offset: 0x00158004
	internal static int ᜅ(string A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (spr\u1CC6.ᜅ(A_0))
				{
					num = 2;
					continue;
				}
				goto IL_8D;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_84;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_84;
			case 3:
				goto IL_55;
			}
			if (A_0.Length == 8)
			{
				num = 3;
			}
			else
			{
				num = 0;
			}
		}
		IL_55:
		return spr\u25D1.ᜆ(A_0);
		IL_84:
		return (int)(sprᜌ.ᜅ(A_0) & (long)((ulong)-1));
		IL_8D:
		return spr\u25D1.ᜆ(A_0);
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x001590A4 File Offset: 0x001580A4
	internal static void ᜁ(string A_0, byte[] A_1, int A_2)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int num2 = 2;
			for (;;)
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
					switch (num2)
					{
					case 0:
						if (num >= A_0.Length)
						{
							num2 = 3;
							continue;
						}
						if (true)
						{
						}
						A_1[A_2++] = (byte)sprᜌ.ᜄ(A_0.Substring(num, 2));
						num += 2;
						goto IL_87;
					case 1:
						goto IL_40;
					case 2:
						goto IL_40;
					case 3:
						return;
					}
					goto IL_34;
					IL_40:
					num2 = 0;
					continue;
				}
				IL_87:
				num2 = 1;
			}
		}
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x00159148 File Offset: 0x00158148
	internal static void ᜀ(string A_0, byte[] A_1, int A_2)
	{
		for (;;)
		{
			IL_3C:
			A_2 = A_2 + A_0.Length / 2 - 1;
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_56;
					case 1:
						return;
					case 2:
						goto IL_56;
					case 3:
						if (num >= A_0.Length)
						{
							num2 = 1;
							continue;
						}
						A_1[A_2--] = (byte)sprᜌ.ᜄ(A_0.Substring(num, 2));
						num += 2;
						goto IL_95;
					}
					goto IL_3C;
					IL_56:
					num2 = 3;
					continue;
				}
				IL_95:
				num2 = 0;
			}
		}
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x001591F8 File Offset: 0x001581F8
	internal static string ᜀ(Color A_0)
	{
		int a_ = 8;
		if (!A_0.IsEmpty)
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
				return sprᜌ.ᜀ(A_0.ToArgb()).Substring(2);
			}
		}
		if (true)
		{
		}
		return ClipboardData.b("཭կٱ᭳", a_);
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x00159268 File Offset: 0x00158268
	internal static Color ᜄ(string A_0)
	{
		int a_ = 16;
		for (;;)
		{
			A_0 = A_0.ToLower();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (A_0 == ClipboardData.b("᝵൷๹፻", a_))
					{
						num = 2;
						continue;
					}
					A_0 = A_0.Trim(spr\u25D1.ᜂ);
					num = 1;
					continue;
				case 1:
					goto IL_94;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_94;
					default:
						goto IL_6D;
					}
					break;
				case 3:
					goto IL_A7;
				}
				break;
				IL_94:
				if (!spr\u25D1.ᜂ(A_0))
				{
					goto IL_B9;
				}
				num = 3;
			}
		}
		IL_6D:
		if (false)
		{
		}
		return Color.Empty;
		IL_A7:
		return spr\u25D1.ᜃ(A_0);
		IL_B9:
		return Color.FromName(A_0);
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x00159334 File Offset: 0x00158334
	private static Color ᜃ(string A_0)
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
		int num = spr\u25D1.ᜆ(A_0);
		int red = (num & 16711680) >> 16;
		int green = (num & 65280) >> 8;
		int blue = num & 255;
		return Color.FromArgb(red, green, blue);
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x0015939C File Offset: 0x0015839C
	private static bool ᜂ(string A_0)
	{
		int num = 0;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 1:
				goto IL_88;
			case 2:
				return true;
			case 3:
				goto IL_44;
			case 4:
				goto IL_88;
			case 5:
				if (num2 >= A_0.Length)
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
			case 6:
				return false;
			case 7:
				if (!spr\u1CC6.ᜀ(A_0[num2]))
				{
					if (true)
					{
					}
					num = 6;
					continue;
				}
				num2++;
				num = 1;
				continue;
			}
			if (A_0.Length != 6)
			{
				num = 3;
				continue;
			}
			num2 = 0;
			num = 4;
			continue;
			IL_88:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			default:
				if (false)
				{
				}
				num = 5;
				break;
			}
		}
		IL_44:
		return false;
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x00159480 File Offset: 0x00158480
	internal static string ᜀ(int A_0)
	{
		int a_ = 16;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		int a_2 = A_0 >> 16;
		int a_3 = A_0 % 65536;
		string arg = sprᜌ.\u170D(a_2);
		return string.Format(ClipboardData.b("൵䡷ݹ剻ս녿ﾁ", a_), arg, sprᜌ.ᜉ(a_3));
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x001594F4 File Offset: 0x001584F4
	internal static int ᜁ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = 0;
				int num = 1;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (spr\u1CC6.ᜋ(A_0))
						{
							num = 3;
							continue;
						}
						return result;
					case 2:
						goto IL_50;
					case 3:
					{
						string[] array = A_0.Split(new char[]
						{
							'.'
						});
						num2 = sprᜌ.ᜐ(array[0]);
						num3 = 0;
						if (true)
						{
						}
						num = 4;
						continue;
					}
					case 4:
					{
						string[] array;
						if (array.Length > 1)
						{
							num = 5;
							continue;
						}
						goto IL_50;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
						{
							if (false)
							{
							}
							string[] array;
							num3 = sprᜌ.ᜐ(array[1]);
							num = 2;
							continue;
						}
						}
						break;
					}
					break;
					IL_50:
					result = (num2 << 16 | num3);
					num = 0;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x001595F4 File Offset: 0x001585F4
	internal static string ᜀ(byte[] A_0)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return sprឈ.ᜀ(A_0, ClipboardData.b("硱", a_));
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x0015964C File Offset: 0x0015864C
	internal static int ᜀ(string A_0)
	{
		int num = 4;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return int.MinValue;
			case 1:
				goto IL_DD;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				default:
					if (false)
					{
					}
					if (A_0[num2] > '9')
					{
						num = 5;
						continue;
					}
					num2--;
					num = 1;
					continue;
				}
				break;
			case 3:
				num = 7;
				continue;
			case 5:
				goto IL_C5;
			case 6:
				goto IL_88;
			case 7:
				if (A_0[num2] >= '0')
				{
					num = 6;
					continue;
				}
				goto IL_FC;
			case 8:
				goto IL_DD;
			case 9:
				if (num2 >= 0)
				{
					num = 3;
					continue;
				}
				goto IL_FC;
			}
			if (!spr\u1CC6.ᜋ(A_0))
			{
				num = 0;
				continue;
			}
			num2 = A_0.Length - 1;
			num = 8;
			continue;
			IL_88:
			num = 2;
			continue;
			IL_DD:
			num = 9;
		}
		return int.MinValue;
		IL_C5:
		IL_FC:
		if (true)
		{
		}
		return sprᜌ.ᜊ(A_0.Substring(num2 + 1));
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x0015976C File Offset: 0x0015876C
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u25D1()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u25D1.ᜂ = new char[]
		{
			'#'
		};
	}

	// Token: 0x040019AF RID: 6575
	internal const int ᜀ = 0;

	// Token: 0x040019B0 RID: 6576
	internal const int ᜁ = 9;

	// Token: 0x040019B1 RID: 6577
	private static readonly char[] ᜂ;
}
