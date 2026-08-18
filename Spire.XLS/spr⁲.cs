using System;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002FA RID: 762
internal class spr\u2072
{
	// Token: 0x06002F04 RID: 12036 RVA: 0x001A42A4 File Offset: 0x001A32A4
	static spr\u2072()
	{
		int a_ = 1;
		spr\u2072.ᜀ = 15;
		try
		{
			for (;;)
			{
				byte[] array = new byte[288];
				int num = 0;
				int num2 = 11;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E1;
						default:
							if (false)
							{
							}
							spr\u2072.ᜂ = new spr\u2072(array);
							array = new byte[32];
							num = 0;
							num2 = 15;
							continue;
						}
						break;
					case 1:
						goto IL_120;
					case 2:
						if (num >= 32)
						{
							num2 = 8;
							continue;
						}
						array[num++] = 5;
						num2 = 4;
						continue;
					case 3:
						if (num >= 144)
						{
							num2 = 14;
							continue;
						}
						array[num++] = 8;
						num2 = 19;
						continue;
					case 4:
						goto IL_100;
					case 5:
						if (num >= 280)
						{
							num2 = 9;
							continue;
						}
						array[num++] = 7;
						num2 = 17;
						continue;
					case 6:
						goto IL_1D5;
					case 7:
						if (true)
						{
						}
						goto IL_16B;
					case 8:
						spr\u2072.ᜃ = new spr\u2072(array);
						num2 = 20;
						continue;
					case 9:
						num2 = 18;
						continue;
					case 10:
						if (num >= 288)
						{
							num2 = 0;
							continue;
						}
						array[num++] = 8;
						num2 = 7;
						continue;
					case 11:
						goto IL_C0;
					case 12:
						goto IL_1D5;
					case 13:
						if (num >= 256)
						{
							num2 = 16;
							continue;
						}
						array[num++] = 9;
						num2 = 6;
						continue;
					case 14:
						goto IL_E1;
					case 15:
						goto IL_100;
					case 16:
						num2 = 1;
						continue;
					case 17:
						goto IL_120;
					case 18:
						goto IL_16B;
					case 19:
						goto IL_C0;
					case 20:
						goto IL_236;
					}
					break;
					IL_C0:
					num2 = 3;
					continue;
					IL_E1:
					num2 = 12;
					continue;
					IL_100:
					num2 = 2;
					continue;
					IL_120:
					num2 = 5;
					continue;
					IL_16B:
					num2 = 10;
					continue;
					IL_1D5:
					num2 = 13;
				}
			}
			IL_236:;
		}
		catch (Exception innerException)
		{
			throw new Exception(RecordTableEnumerator.b("猶尸堺刼刾ㅀㅂ⁄㑆㩈⑊㽌ݎ⑐㕒㍔㩖㡘㕚ड़ⵞѠ٢彤䝦ཨɪᕬ੮ᕰ卲ŴնᱸṺ๼彾ﮈ歷ﺐﶒ떔爵얠", a_), innerException);
		}
	}

	// Token: 0x06002F05 RID: 12037 RVA: 0x001A4528 File Offset: 0x001A3528
	public spr\u2072(byte[] A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002F06 RID: 12038 RVA: 0x001A4544 File Offset: 0x001A3544
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
				int num3 = 3;
				for (;;)
				{
					IL_10:
					int i;
					switch (num3)
					{
					case 0:
						goto IL_16A;
					case 1:
						i = 1;
						num3 = 0;
						continue;
					case 2:
						while (i >= 10)
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
								num3 = 11;
								goto IL_10;
							}
						}
						goto IL_D6;
					case 3:
						goto IL_18C;
					case 4:
						return num;
					case 5:
						goto IL_D6;
					case 6:
						goto IL_1BA;
					case 7:
						if (i > spr\u2072.ᜀ)
						{
							num3 = 4;
							continue;
						}
						A_1[i] = num;
						num += A_0[i] << 16 - i;
						num3 = 2;
						continue;
					case 8:
					{
						int num4;
						A_0[num4]++;
						num3 = 6;
						continue;
					}
					case 9:
						goto IL_16A;
					case 10:
						goto IL_18C;
					case 11:
					{
						int num5 = A_1[i] & 130944;
						int num6 = num & 130944;
						A_3 += num6 - num5 >> 16 - i;
						num3 = 5;
						continue;
					}
					case 12:
					{
						if (num2 >= A_2.Length)
						{
							num3 = 1;
							continue;
						}
						int num4 = (int)A_2[num2];
						num3 = 13;
						continue;
					}
					case 13:
					{
						int num4;
						if (num4 > 0)
						{
							num3 = 8;
							continue;
						}
						goto IL_1BA;
					}
					}
					break;
					IL_D6:
					i++;
					num3 = 9;
					continue;
					IL_16A:
					num3 = 7;
					continue;
					IL_18C:
					if (true)
					{
					}
					num3 = 12;
					continue;
					IL_1BA:
					num2++;
					num3 = 10;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06002F07 RID: 12039 RVA: 0x001A4720 File Offset: 0x001A3720
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
				int num3 = spr\u2072.ᜀ;
				int num4 = 9;
				for (;;)
				{
					int num5;
					int num6;
					int num8;
					int num9;
					switch (num4)
					{
					case 0:
						num5 = 0;
						num4 = 13;
						continue;
					case 1:
						goto IL_E5;
					case 2:
						goto IL_1B2;
					case 3:
					{
						int num7;
						if (num6 >= num7)
						{
							num4 = 6;
							continue;
						}
						goto IL_229;
					}
					case 4:
					{
						if (num8 <= 9)
						{
							num4 = 10;
							continue;
						}
						num9 = (int)array[num6 & 511];
						int num7 = 1 << (num9 & 15);
						num9 = -(num9 >> 4);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E5;
						default:
							if (false)
							{
							}
							num4 = 1;
							continue;
						}
						break;
					}
					case 5:
						return array;
					case 6:
						goto IL_152;
					case 7:
						A_3 = A_1[num8];
						num6 = (int)sprៜ.ᜀ(A_3);
						num4 = 4;
						continue;
					case 8:
						goto IL_152;
					case 9:
						goto IL_26A;
					case 10:
						goto IL_EA;
					case 11:
						goto IL_26A;
					case 12:
						goto IL_12F;
					case 13:
						goto IL_1B2;
					case 14:
						if (num5 >= A_2.Length)
						{
							num4 = 5;
							continue;
						}
						num8 = (int)A_2[num5];
						num4 = 17;
						continue;
					case 15:
						num3--;
						num4 = 11;
						continue;
					case 16:
						num4 = 8;
						continue;
					case 17:
						if (num8 != 0)
						{
							num4 = 7;
							continue;
						}
						goto IL_215;
					case 18:
						goto IL_12F;
					case 19:
						if (num6 >= 512)
						{
							num4 = 16;
							continue;
						}
						goto IL_EA;
					case 20:
					{
						int num10;
						int num11;
						if (num10 >= num11)
						{
							num4 = 15;
							continue;
						}
						array[(int)sprៜ.ᜀ(num10)] = (short)(-num << 4 | num3);
						num += 1 << num3 - 9;
						num10 += num2;
						num4 = 18;
						continue;
					}
					case 21:
						goto IL_215;
					case 22:
					{
						if (num3 < 10)
						{
							num4 = 0;
							continue;
						}
						int num11 = A_3 & 130944;
						A_3 -= A_0[num3] << 16 - num3;
						int num12 = A_3 & 130944;
						int num10 = num12;
						if (true)
						{
						}
						num4 = 12;
						continue;
					}
					}
					break;
					IL_EA:
					array[num6] = (short)(num5 << 4 | num8);
					num6 += 1 << num8;
					num4 = 19;
					continue;
					IL_12F:
					num4 = 20;
					continue;
					IL_152:
					A_1[num8] = A_3 + (1 << 16 - num8);
					num4 = 21;
					continue;
					IL_1B2:
					num4 = 14;
					continue;
					IL_215:
					num5++;
					num4 = 2;
					continue;
					IL_229:
					array[num9 | num6 >> 9] = (short)(num5 << 4 | num8);
					num6 += 1 << num8;
					num4 = 3;
					continue;
					IL_E5:
					goto IL_229;
					IL_26A:
					num4 = 22;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06002F08 RID: 12040 RVA: 0x001A4A44 File Offset: 0x001A3A44
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
		int[] a_ = new int[spr\u2072.ᜀ + 1];
		int[] a_2 = new int[spr\u2072.ᜀ + 1];
		int a_4;
		int a_3 = this.ᜀ(a_, a_2, A_0, out a_4);
		this.ᜁ = this.ᜀ(a_, a_2, A_0, a_3, a_4);
	}

	// Token: 0x06002F09 RID: 12041 RVA: 0x001A4AB8 File Offset: 0x001A3AB8
	public int ᜀ(sprᾲ A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			int num2;
			for (;;)
			{
				int num4;
				switch (num)
				{
				case 0:
				{
					int num3;
					if ((num2 & 15) <= num3)
					{
						num = 7;
						continue;
					}
					goto IL_116;
				}
				case 1:
					goto IL_ED;
				case 3:
					goto IL_18C;
				case 4:
					num = 8;
					continue;
				case 5:
					goto IL_114;
				case 6:
				{
					if ((num2 = (int)this.ᜁ[num4]) >= 0)
					{
						num = 5;
						continue;
					}
					int num5 = -(num2 >> 4);
					int a_ = num2 & 15;
					num = 9;
					continue;
				}
				case 7:
					goto IL_1DA;
				case 8:
				{
					int num6;
					if ((num2 & 15) <= num6)
					{
						num = 1;
						continue;
					}
					return -1;
				}
				case 9:
				{
					int a_;
					if ((num4 = A_0.ᜀ(a_)) >= 0)
					{
						num = 3;
						continue;
					}
					int num3 = A_0.ᜁ();
					num4 = A_0.ᜀ(num3);
					int num5;
					num2 = (int)this.ᜁ[num5 | num4 >> 9];
					num = 0;
					continue;
				}
				case 10:
					if (num2 >= 0)
					{
						num = 4;
						continue;
					}
					return -1;
				case 11:
					num = 6;
					continue;
				}
				if ((num4 = A_0.ᜀ(9)) >= 0)
				{
					num = 11;
				}
				else
				{
					int num6 = A_0.ᜁ();
					num4 = A_0.ᜀ(num6);
					num2 = (int)this.ᜁ[num4];
					num = 10;
				}
			}
			IL_ED:
			A_0.ᜁ(num2 & 15);
			return num2 >> 4;
			IL_114:
			A_0.ᜁ(num2 & 15);
			return num2 >> 4;
			IL_116:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_18C:
				int num4;
				int num5;
				num2 = (int)this.ᜁ[num5 | num4 >> 9];
				A_0.ᜁ(num2 & 15);
				return num2 >> 4;
			}
			default:
				if (false)
				{
				}
				return -1;
			}
			IL_1DA:
			if (true)
			{
			}
			A_0.ᜁ(num2 & 15);
			return num2 >> 4;
		}
		}
	}

	// Token: 0x06002F0A RID: 12042 RVA: 0x001A4CA8 File Offset: 0x001A3CA8
	public static spr\u2072 ᜁ()
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
		return spr\u2072.ᜂ;
	}

	// Token: 0x06002F0B RID: 12043 RVA: 0x001A4CE8 File Offset: 0x001A3CE8
	public static spr\u2072 ᜀ()
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
		return spr\u2072.ᜃ;
	}

	// Token: 0x0400151F RID: 5407
	private static int ᜀ;

	// Token: 0x04001520 RID: 5408
	private short[] ᜁ;

	// Token: 0x04001521 RID: 5409
	private static spr\u2072 ᜂ;

	// Token: 0x04001522 RID: 5410
	private static spr\u2072 ᜃ;
}
