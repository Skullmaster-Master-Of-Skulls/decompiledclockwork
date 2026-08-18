using System;
using Spire.CompoundFile.Doc;

// Token: 0x020001CB RID: 459
internal class sprẒ
{
	// Token: 0x060013C6 RID: 5062 RVA: 0x001488E0 File Offset: 0x001478E0
	static sprẒ()
	{
		int a_ = 6;
		sprẒ.ᜀ = 15;
		try
		{
			for (;;)
			{
				byte[] array = new byte[288];
				int num = 0;
				int num2 = 18;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_168;
					case 1:
						if (num >= 256)
						{
							num2 = 14;
							continue;
						}
						array[num++] = 9;
						num2 = 11;
						continue;
					case 2:
						if (num >= 32)
						{
							num2 = 13;
							continue;
						}
						array[num++] = 5;
						goto IL_15B;
					case 3:
						goto IL_120;
					case 4:
						if (num >= 144)
						{
							num2 = 15;
							continue;
						}
						array[num++] = 8;
						num2 = 8;
						continue;
					case 5:
						goto IL_1B3;
					case 6:
						if (num >= 280)
						{
							num2 = 9;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15B;
						default:
							if (false)
							{
							}
							array[num++] = 7;
							num2 = 10;
							continue;
						}
						break;
					case 7:
						goto IL_100;
					case 8:
						goto IL_C0;
					case 9:
						num2 = 0;
						continue;
					case 10:
						goto IL_120;
					case 11:
						goto IL_1B3;
					case 12:
						goto IL_230;
					case 13:
						sprẒ.ᜃ = new sprẒ(array);
						num2 = 12;
						continue;
					case 14:
						num2 = 3;
						continue;
					case 15:
						num2 = 5;
						continue;
					case 16:
						if (true)
						{
						}
						goto IL_168;
					case 17:
						sprẒ.ᜂ = new sprẒ(array);
						array = new byte[32];
						num = 0;
						num2 = 7;
						continue;
					case 18:
						goto IL_C0;
					case 19:
						goto IL_100;
					case 20:
						if (num >= 288)
						{
							num2 = 17;
							continue;
						}
						array[num++] = 8;
						num2 = 16;
						continue;
					}
					break;
					IL_C0:
					num2 = 4;
					continue;
					IL_100:
					num2 = 2;
					continue;
					IL_120:
					num2 = 6;
					continue;
					IL_15B:
					num2 = 19;
					continue;
					IL_168:
					num2 = 20;
					continue;
					IL_1B3:
					num2 = 1;
				}
			}
			IL_230:;
		}
		catch (Exception innerException)
		{
			throw new Exception(ClipboardData.b("⡫୭፯ᵱᥳٵ੷όཻൽ첃ﺏ욑ﶗꂙ벛즟\udaa1솣슥袧\udea9\udeab쮭햯솱钳통\uddb7풹\ud9bb첽ꆿ뛁귃꧅ꛇ꫋꿍맏뻑뇓닕", a_), innerException);
		}
	}

	// Token: 0x060013C7 RID: 5063 RVA: 0x00148B5C File Offset: 0x00147B5C
	public sprẒ(byte[] A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x060013C8 RID: 5064 RVA: 0x00148B78 File Offset: 0x00147B78
	private int ᜀ(int[] A_0, int[] A_1, byte[] A_2, out int A_3)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				A_3 = 512;
				int num2 = 0;
				int num3 = 9;
				for (;;)
				{
					int num5;
					switch (num3)
					{
					case 0:
						goto IL_189;
					case 1:
					{
						int num4;
						A_0[num4]++;
						num3 = 2;
						continue;
					}
					case 2:
						goto IL_1B7;
					case 3:
						goto IL_C2;
					case 4:
						goto IL_167;
					case 5:
						if (num5 > sprẒ.ᜀ)
						{
							num3 = 7;
							continue;
						}
						A_1[num5] = num;
						num += A_0[num5] << 16 - num5;
						num3 = 11;
						continue;
					case 6:
						num5 = 1;
						num3 = 8;
						continue;
					case 7:
						return num;
					case 8:
						goto IL_167;
					case 9:
						goto IL_189;
					case 10:
					{
						int num4;
						if (num4 > 0)
						{
							num3 = 1;
							continue;
						}
						goto IL_1B7;
					}
					case 11:
						if (num5 >= 10)
						{
							num3 = 13;
							continue;
						}
						goto IL_FC;
					case 12:
					{
						if (num2 >= A_2.Length)
						{
							num3 = 6;
							continue;
						}
						int num4 = (int)A_2[num2];
						num3 = 10;
						continue;
					}
					case 13:
					{
						int num6 = A_1[num5] & 130944;
						int num7 = num & 130944;
						A_3 += num7 - num6 >> 16 - num5;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C2;
						default:
							if (false)
							{
							}
							num3 = 3;
							continue;
						}
						break;
					}
					}
					break;
					IL_FC:
					num5++;
					num3 = 4;
					continue;
					IL_C2:
					goto IL_FC;
					IL_167:
					num3 = 5;
					continue;
					IL_189:
					if (true)
					{
					}
					num3 = 12;
					continue;
					IL_1B7:
					num2++;
					num3 = 0;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x060013C9 RID: 5065 RVA: 0x00148D50 File Offset: 0x00147D50
	private short[] ᜀ(int[] A_0, int[] A_1, byte[] A_2, int A_3, int A_4)
	{
		switch (0)
		{
		default:
		{
			short[] array;
			for (;;)
			{
				array = new short[A_4];
				int num = 512;
				int num2 = 128;
				int num3 = sprẒ.ᜀ;
				int num4 = 16;
				for (;;)
				{
					int num5;
					int num8;
					int num9;
					int num11;
					switch (num4)
					{
					case 0:
						if (num5 != 0)
						{
							num4 = 13;
							continue;
						}
						goto IL_1F9;
					case 1:
						goto IL_136;
					case 2:
					{
						int num6;
						int num7;
						if (num6 >= num7)
						{
							num4 = 7;
							continue;
						}
						array[(int)sprᣬ.ᜀ(num6)] = (short)(-num << 4 | num3);
						num += 1 << num3 - 9;
						num6 += num2;
						num4 = 22;
						continue;
					}
					case 3:
						return array;
					case 4:
						goto IL_196;
					case 5:
						num8 = 0;
						num4 = 4;
						continue;
					case 6:
						num4 = 12;
						continue;
					case 7:
						num3--;
						num4 = 20;
						continue;
					case 8:
					{
						int num10;
						if (num9 >= num10)
						{
							num4 = 1;
							continue;
						}
						goto IL_20D;
					}
					case 9:
					{
						if (num5 <= 9)
						{
							num4 = 14;
							continue;
						}
						num11 = (int)array[num9 & 511];
						int num10 = 1 << (num11 & 15);
						num11 = -(num11 >> 4);
						num4 = 18;
						continue;
					}
					case 10:
						if (num8 >= A_2.Length)
						{
							num4 = 3;
							continue;
						}
						num5 = (int)A_2[num8];
						num4 = 0;
						continue;
					case 11:
						goto IL_196;
					case 12:
						goto IL_136;
					case 13:
						A_3 = A_1[num5];
						num9 = (int)sprᣬ.ᜀ(A_3);
						num4 = 9;
						continue;
					case 14:
						goto IL_D8;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F7;
						default:
						{
							if (false)
							{
							}
							if (num3 < 10)
							{
								num4 = 5;
								continue;
							}
							int num7 = A_3 & 130944;
							A_3 -= A_0[num3] << 16 - num3;
							int num12 = A_3 & 130944;
							int num6 = num12;
							if (true)
							{
							}
							num4 = 19;
							continue;
						}
						}
						break;
					case 16:
						goto IL_24E;
					case 17:
						goto IL_1F9;
					case 18:
						goto IL_20D;
					case 19:
						goto IL_113;
					case 20:
						goto IL_1F7;
					case 21:
						if (num9 >= 512)
						{
							num4 = 6;
							continue;
						}
						goto IL_D8;
					case 22:
						goto IL_113;
					}
					break;
					IL_D8:
					array[num9] = (short)(num8 << 4 | num5);
					num9 += 1 << num5;
					num4 = 21;
					continue;
					IL_113:
					num4 = 2;
					continue;
					IL_136:
					A_1[num5] = A_3 + (1 << 16 - num5);
					num4 = 17;
					continue;
					IL_196:
					num4 = 10;
					continue;
					IL_1F9:
					num8++;
					num4 = 11;
					continue;
					IL_20D:
					array[num11 | num9 >> 9] = (short)(num8 << 4 | num5);
					num9 += 1 << num5;
					num4 = 8;
					continue;
					IL_24E:
					num4 = 15;
					continue;
					IL_1F7:
					goto IL_24E;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x00149074 File Offset: 0x00148074
	private void ᜀ(byte[] A_0)
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
		int[] a_ = new int[sprẒ.ᜀ + 1];
		int[] a_2 = new int[sprẒ.ᜀ + 1];
		int a_4;
		int a_3 = this.ᜀ(a_, a_2, A_0, out a_4);
		this.ᜁ = this.ᜀ(a_, a_2, A_0, a_3, a_4);
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x001490E8 File Offset: 0x001480E8
	public int ᜀ(sprᢹ A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 11;
			int num2;
			int num4;
			int num6;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num3;
					if ((num2 & 15) <= num3)
					{
						goto IL_10A;
					}
					return -1;
				}
				case 1:
					num = 8;
					continue;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_13D;
				case 4:
					goto IL_116;
				case 5:
					goto IL_1DD;
				case 6:
					goto IL_199;
				case 7:
				{
					int a_;
					if ((num4 = A_0.ᜀ(a_)) >= 0)
					{
						num = 6;
						continue;
					}
					int num5 = A_0.ᜁ();
					num4 = A_0.ᜀ(num5);
					num2 = (int)this.ᜁ[num6 | num4 >> 9];
					num = 10;
					continue;
				}
				case 8:
				{
					if ((num2 = (int)this.ᜁ[num4]) >= 0)
					{
						num = 3;
						continue;
					}
					num6 = -(num2 >> 4);
					int a_ = num2 & 15;
					num = 7;
					continue;
				}
				case 9:
					if (true)
					{
					}
					if (num2 >= 0)
					{
						num = 2;
						continue;
					}
					return -1;
				case 10:
				{
					int num5;
					if ((num2 & 15) <= num5)
					{
						num = 5;
						continue;
					}
					return -1;
				}
				}
				if ((num4 = A_0.ᜀ(9)) >= 0)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num3 = A_0.ᜁ();
					num4 = A_0.ᜀ(num3);
					num2 = (int)this.ᜁ[num4];
					num = 9;
					continue;
				}
				}
				IL_10A:
				num = 4;
			}
			IL_116:
			A_0.ᜁ(num2 & 15);
			return num2 >> 4;
			IL_13D:
			A_0.ᜁ(num2 & 15);
			return num2 >> 4;
			IL_199:
			num2 = (int)this.ᜁ[num6 | num4 >> 9];
			A_0.ᜁ(num2 & 15);
			return num2 >> 4;
			IL_1DD:
			A_0.ᜁ(num2 & 15);
			return num2 >> 4;
		}
		}
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x001492D8 File Offset: 0x001482D8
	public static sprẒ ᜁ()
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
		return sprẒ.ᜂ;
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x00149318 File Offset: 0x00148318
	public static sprẒ ᜀ()
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
		return sprẒ.ᜃ;
	}

	// Token: 0x040018E2 RID: 6370
	private static int ᜀ;

	// Token: 0x040018E3 RID: 6371
	private short[] ᜁ;

	// Token: 0x040018E4 RID: 6372
	private static sprẒ ᜂ;

	// Token: 0x040018E5 RID: 6373
	private static sprẒ ᜃ;
}
