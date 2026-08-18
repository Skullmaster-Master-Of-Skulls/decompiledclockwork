using System;
using Spire.CompoundFile.Doc;

// Token: 0x020002FD RID: 765
internal class sprᴂ
{
	// Token: 0x060029B7 RID: 10679 RVA: 0x00299EA8 File Offset: 0x00298EA8
	public sprᴂ(spr\u234C A_0, int A_1, int A_2, int A_3)
	{
		this.ᜇ = A_0;
		this.ᜄ = A_2;
		this.ᜆ = A_3;
		this.ᜀ = new short[A_1];
		this.ᜃ = new int[A_3];
	}

	// Token: 0x060029B8 RID: 10680 RVA: 0x00299EEC File Offset: 0x00298EEC
	public void ᜂ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_7C:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_6D;
			case 1:
				if (num2 >= this.ᜀ.Length)
				{
					num = 0;
					continue;
				}
				goto IL_6F;
			case 2:
				goto IL_52;
			case 3:
				goto IL_52;
			}
			goto IL_3E;
			IL_52:
			num = 1;
		}
		IL_6D:
		this.ᜁ = null;
		this.ᜂ = null;
		return;
		IL_6F:
		this.ᜀ[num2] = 0;
		num2++;
		goto IL_7C;
		IL_3E:
		if (true)
		{
		}
		num2 = 0;
		num = 3;
		goto IL_28;
	}

	// Token: 0x060029B9 RID: 10681 RVA: 0x00299F90 File Offset: 0x00298F90
	public void ᜀ(int A_0)
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
		this.ᜇ.ᜁ((int)this.ᜁ[A_0] & 65535, (int)this.ᜂ[A_0]);
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x00299FEC File Offset: 0x00298FEC
	public void ᜇ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_73:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_4A;
			case 1:
				if (num2 >= this.ᜀ.Length)
				{
					num = 2;
					continue;
				}
				goto IL_67;
			case 2:
				return;
			case 3:
				goto IL_4A;
			}
			goto IL_3E;
			IL_4A:
			num = 1;
		}
		return;
		IL_67:
		if (true)
		{
		}
		num2++;
		goto IL_73;
		IL_3E:
		num2 = 0;
		num = 0;
		goto IL_28;
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x0029A078 File Offset: 0x00299078
	public void ᜀ(short[] A_0, byte[] A_1)
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
		this.ᜁ = (short[])A_0.Clone();
		this.ᜂ = (byte[])A_1.Clone();
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x0029A0D8 File Offset: 0x002990D8
	public void ᜅ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int[] array = new int[this.ᜆ];
				this.ᜁ = new short[this.ᜅ];
				int num = 0;
				int num2 = 0;
				int num3 = 10;
				for (;;)
				{
					int num4;
					int num5;
					switch (num3)
					{
					case 0:
						goto IL_12E;
					case 1:
						goto IL_D6;
					case 2:
						return;
					case 3:
						goto IL_185;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D6;
						default:
							if (false)
							{
							}
							this.ᜁ[num4] = sprᣬ.ᜀ(array[num5 - 1]);
							array[num5 - 1] += 1 << 16 - num5;
							num3 = 3;
							continue;
						}
						break;
					case 5:
						if (num4 >= this.ᜅ)
						{
							if (true)
							{
							}
							num3 = 2;
							continue;
						}
						num5 = (int)this.ᜂ[num4];
						num3 = 1;
						continue;
					case 6:
						if (num2 >= this.ᜆ)
						{
							num3 = 8;
							continue;
						}
						array[num2] = num;
						num += this.ᜃ[num2] << 15 - num2;
						num2++;
						num3 = 9;
						continue;
					case 7:
						goto IL_12E;
					case 8:
						num4 = 0;
						num3 = 0;
						continue;
					case 9:
						goto IL_10B;
					case 10:
						goto IL_10B;
					}
					break;
					IL_D6:
					if (num5 > 0)
					{
						num3 = 4;
						continue;
					}
					goto IL_185;
					IL_10B:
					num3 = 6;
					continue;
					IL_12E:
					num3 = 5;
					continue;
					IL_185:
					num4++;
					num3 = 7;
				}
			}
			return;
		}
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x0029A28C File Offset: 0x0029928C
	private void ᜀ(int[] A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜂ = new byte[this.ᜀ.Length];
				int num = A_0.Length / 2;
				int num2 = (num + 1) / 2;
				int num3 = 0;
				int num4 = 0;
				int num5 = 4;
				for (;;)
				{
					int num6;
					int num7;
					int num9;
					int num10;
					int[] array;
					int num12;
					int num14;
					switch (num5)
					{
					case 0:
						goto IL_29F;
					case 1:
						goto IL_29F;
					case 2:
						if (A_0[num6 + 1] == -1)
						{
							num5 = 28;
							continue;
						}
						goto IL_27A;
					case 3:
						goto IL_DE;
					case 4:
						goto IL_4A4;
					case 5:
						goto IL_27A;
					case 6:
						num7 = this.ᜆ;
						num3++;
						num5 = 11;
						continue;
					case 7:
					{
						int num8;
						num8--;
						num5 = 1;
						continue;
					}
					case 8:
						if (this.ᜃ[--num9] != 0)
						{
							num5 = 25;
							continue;
						}
						goto IL_FF;
					case 9:
					{
						if (A_0[num10] != -1)
						{
							num5 = 21;
							continue;
						}
						int num11 = array[num12];
						this.ᜃ[num11 - 1]++;
						this.ᜂ[A_0[num10 - 1]] = (byte)array[num12];
						num5 = 19;
						continue;
					}
					case 10:
						return;
					case 11:
						goto IL_2F8;
					case 12:
						if (num12 < 0)
						{
							num5 = 26;
							continue;
						}
						num10 = 2 * num12 + 1;
						num5 = 9;
						continue;
					case 13:
						goto IL_27A;
					case 14:
						if (num7 > this.ᜆ)
						{
							num5 = 6;
							continue;
						}
						goto IL_2F8;
					case 15:
						if (num4 >= this.ᜆ)
						{
							num5 = 34;
							continue;
						}
						this.ᜃ[num4] = 0;
						num4++;
						num5 = 35;
						continue;
					case 16:
						num5 = 20;
						continue;
					case 17:
						if (num3 <= 0)
						{
							num5 = 22;
							continue;
						}
						goto IL_FF;
					case 18:
					{
						int num8;
						if (num8 == 0)
						{
							num5 = 10;
							continue;
						}
						int num13 = this.ᜃ[num8 - 1];
						num5 = 5;
						continue;
					}
					case 19:
						goto IL_2C3;
					case 20:
						if (num9 >= this.ᜆ - 1)
						{
							num5 = 3;
							continue;
						}
						goto IL_434;
					case 21:
						num7 = array[num12] + 1;
						num5 = 14;
						continue;
					case 22:
					{
						this.ᜃ[this.ᜆ - 1] += num3;
						this.ᜃ[this.ᜆ - 2] -= num3;
						num14 = 2 * num2;
						int num8 = this.ᜆ;
						num5 = 0;
						continue;
					}
					case 23:
						goto IL_FF;
					case 24:
						goto IL_412;
					case 25:
						goto IL_434;
					case 26:
						num5 = 30;
						continue;
					case 27:
						if (num3 > 0)
						{
							num5 = 16;
							continue;
						}
						goto IL_DE;
					case 28:
					{
						int num8;
						this.ᜂ[A_0[num6]] = (byte)num8;
						int num13;
						num13--;
						num5 = 13;
						continue;
					}
					case 29:
					{
						int num13;
						if (num13 <= 0)
						{
							num5 = 7;
							continue;
						}
						goto IL_390;
					}
					case 30:
						if (num3 == 0)
						{
							num5 = 32;
							continue;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_390;
						default:
							if (false)
							{
							}
							num9 = this.ᜆ - 1;
							num5 = 23;
							continue;
						}
						break;
					case 31:
						goto IL_412;
					case 32:
						return;
					case 33:
						goto IL_2C3;
					case 34:
						array = new int[num];
						array[num - 1] = 0;
						num12 = num - 1;
						num5 = 24;
						continue;
					case 35:
						goto IL_4A4;
					}
					break;
					IL_DE:
					num5 = 17;
					continue;
					IL_FF:
					num5 = 8;
					continue;
					IL_27A:
					num5 = 29;
					continue;
					IL_29F:
					num5 = 18;
					continue;
					IL_2C3:
					num12--;
					num5 = 31;
					continue;
					IL_2F8:
					array[A_0[num10 - 1]] = (array[A_0[num10]] = num7);
					num5 = 33;
					continue;
					IL_390:
					num6 = 2 * A_0[num14++];
					num5 = 2;
					continue;
					IL_412:
					num5 = 12;
					continue;
					IL_434:
					this.ᜃ[num9]--;
					this.ᜃ[++num9]++;
					num3 -= 1 << this.ᜆ - 1 - num9;
					num5 = 27;
					continue;
					IL_4A4:
					num5 = 15;
				}
			}
			return;
		}
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x0029A790 File Offset: 0x00299790
	public void ᜁ()
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int[] array2;
			for (;;)
			{
				int num = this.ᜀ.Length;
				int[] array = new int[num];
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 31;
				for (;;)
				{
					int num6;
					int num7;
					int[] array3;
					int num14;
					int num15;
					int num16;
					switch (num5)
					{
					case 0:
						goto IL_683;
					case 1:
						if (num6 > 0)
						{
							num5 = 3;
							continue;
						}
						goto IL_472;
					case 2:
						if (num2 <= 1)
						{
							num5 = 22;
							continue;
						}
						goto IL_40F;
					case 3:
						num5 = 34;
						continue;
					case 4:
						num7++;
						num5 = 50;
						continue;
					case 5:
						goto IL_40F;
					case 6:
						num5 = 42;
						continue;
					case 7:
					{
						if (true)
						{
						}
						int num8;
						if (num8 >= num2)
						{
							num5 = 5;
							continue;
						}
						int num9 = array[num8];
						int num10 = 2 * num8;
						array2[num10] = num9;
						array2[num10 + 1] = -1;
						array3[num8] = (int)this.ᜀ[num9] << 8;
						array[num8] = num8;
						num8++;
						num5 = 51;
						continue;
					}
					case 8:
						if (num2 >= 2)
						{
							num5 = 36;
							continue;
						}
						num5 = 24;
						continue;
					case 9:
						goto IL_379;
					case 10:
						num5 = 21;
						continue;
					case 11:
						num5 = 23;
						continue;
					case 12:
						num7++;
						num5 = 49;
						continue;
					case 13:
						num5 = 28;
						continue;
					case 14:
						if (num7 >= num2)
						{
							num5 = 11;
							continue;
						}
						num5 = 52;
						continue;
					case 15:
					{
						if (num4 >= num)
						{
							num5 = 10;
							continue;
						}
						int num11 = (int)this.ᜀ[num4];
						num5 = 18;
						continue;
					}
					case 16:
						goto IL_2F4;
					case 17:
						if (num7 + 1 < num2)
						{
							num5 = 40;
							continue;
						}
						goto IL_275;
					case 18:
					{
						int num11;
						if (num11 != 0)
						{
							num5 = 37;
							continue;
						}
						goto IL_379;
					}
					case 19:
						goto IL_5D9;
					case 20:
						num5 = 47;
						continue;
					case 21:
						goto IL_4B1;
					case 22:
						num5 = 32;
						continue;
					case 23:
						goto IL_2B8;
					case 24:
						array[num2++] = ((num3 < 2) ? (++num3) : 0);
						num5 = 55;
						continue;
					case 25:
						goto IL_152;
					case 26:
						goto IL_152;
					case 27:
						if (num7 >= num2)
						{
							num5 = 6;
							continue;
						}
						num5 = 17;
						continue;
					case 28:
						if (array3[array[num7]] > array3[array[num7 + 1]])
						{
							num5 = 12;
							continue;
						}
						goto IL_48C;
					case 29:
						goto IL_590;
					case 30:
						goto IL_590;
					case 31:
						goto IL_357;
					case 32:
						if (array[0] != array2.Length / 2 - 1)
						{
							num5 = 0;
							continue;
						}
						goto IL_703;
					case 33:
						goto IL_1CF;
					case 34:
					{
						int num11;
						int num12;
						if ((int)this.ᜀ[array[num12 = (num6 - 1) / 2]] <= num11)
						{
							num5 = 38;
							continue;
						}
						array[num6] = array[num12];
						num6 = num12;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_289;
						default:
							if (false)
							{
							}
							num5 = 35;
							continue;
						}
						break;
					}
					case 35:
						goto IL_600;
					case 36:
					{
						this.ᜅ = Math.Max(num3 + 1, this.ᜄ);
						int num13 = num2;
						num14 = num13;
						array2 = new int[4 * num2 - 2];
						array3 = new int[2 * num2 - 1];
						int num8 = 0;
						num5 = 16;
						continue;
					}
					case 37:
						num6 = num2++;
						num5 = 41;
						continue;
					case 38:
						goto IL_472;
					case 39:
						if ((num7 = num15) > 0)
						{
							num5 = 46;
							continue;
						}
						goto IL_1CF;
					case 40:
						num5 = 53;
						continue;
					case 41:
						goto IL_600;
					case 42:
						goto IL_43B;
					case 43:
						if (array3[array[num15 = (num7 - 1) / 2]] <= num16)
						{
							num5 = 33;
							continue;
						}
						array[num7] = array[num15];
						num5 = 54;
						continue;
					case 44:
						goto IL_357;
					case 45:
						if ((num7 = num15) > 0)
						{
							num5 = 20;
							continue;
						}
						goto IL_5D9;
					case 46:
						num5 = 43;
						continue;
					case 47:
						if (array3[array[num15 = (num7 - 1) / 2]] <= num16)
						{
							num5 = 19;
							continue;
						}
						array[num7] = array[num15];
						num5 = 48;
						continue;
					case 48:
						goto IL_2B8;
					case 49:
						goto IL_48C;
					case 50:
						goto IL_275;
					case 51:
						goto IL_2F4;
					case 52:
						if (num7 + 1 < num2)
						{
							num5 = 13;
							continue;
						}
						goto IL_48C;
					case 53:
						if (array3[array[num7]] > array3[array[num7 + 1]])
						{
							num5 = 4;
							continue;
						}
						goto IL_275;
					case 54:
						goto IL_43B;
					case 55:
						goto IL_4B1;
					}
					break;
					IL_152:
					num5 = 27;
					continue;
					IL_1CF:
					int num17;
					array[num7] = num17;
					int num18 = array[0];
					num17 = num14++;
					int num19;
					array2[2 * num17] = num19;
					array2[2 * num17 + 1] = num18;
					int num20 = Math.Min(array3[num19] & 255, array3[num18] & 255);
					num16 = (array3[num17] = array3[num19] + array3[num18] - num20 + 1);
					num15 = 0;
					num7 = 1;
					num5 = 30;
					continue;
					IL_289:
					num5 = 25;
					continue;
					IL_275:
					array[num15] = array[num7];
					num15 = num7;
					num7 = num15 * 2 + 1;
					goto IL_289;
					IL_2B8:
					num5 = 45;
					continue;
					IL_2F4:
					num5 = 7;
					continue;
					IL_357:
					num5 = 15;
					continue;
					IL_379:
					num4++;
					num5 = 44;
					continue;
					IL_40F:
					num19 = array[0];
					num17 = array[--num2];
					num16 = array3[num17];
					num15 = 0;
					num7 = 1;
					num5 = 26;
					continue;
					IL_43B:
					num5 = 39;
					continue;
					IL_472:
					array[num6] = num4;
					num3 = num4;
					num5 = 9;
					continue;
					IL_48C:
					array[num15] = array[num7];
					num15 = num7;
					num7 = num15 * 2 + 1;
					num5 = 29;
					continue;
					IL_4B1:
					num5 = 8;
					continue;
					IL_590:
					num5 = 14;
					continue;
					IL_5D9:
					array[num7] = num17;
					num5 = 2;
					continue;
					IL_600:
					num5 = 1;
				}
			}
			IL_683:
			throw new ApplicationException(ClipboardData.b("⹥൧୩ᱫ乭᥯ᱱɳ᝵੷፹ᵻၽꊁ揄", a_));
			IL_703:
			this.ᜀ(array2);
			return;
		}
		}
	}

	// Token: 0x060029BF RID: 10687 RVA: 0x0029AEA8 File Offset: 0x00299EA8
	public int ᜄ()
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 0;
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num2 >= this.ᜀ.Length)
					{
						num3 = 1;
						continue;
					}
					if (true)
					{
					}
					num += (int)(this.ᜀ[num2] * (short)this.ᜂ[num2]);
					num2++;
					num3 = 2;
					continue;
				case 1:
					return num;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_30;
					}
					break;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num3 = 0;
			}
		}
		return num;
	}

	// Token: 0x060029C0 RID: 10688 RVA: 0x0029AF4C File Offset: 0x00299F4C
	public void ᜁ(sprᴂ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = -1;
				int num2 = 0;
				int num3 = 3;
				for (;;)
				{
					int num10;
					switch (num3)
					{
					case 0:
					{
						int num4;
						int num5;
						if (++num4 < num5)
						{
							num3 = 23;
							continue;
						}
						goto IL_BE;
					}
					case 1:
						goto IL_17D;
					case 2:
						if (num != 0)
						{
							num3 = 20;
							continue;
						}
						num3 = 22;
						continue;
					case 3:
						goto IL_17D;
					case 4:
						goto IL_BE;
					case 5:
					{
						short[] array = A_0.ᜀ;
						int num6 = 17;
						array[num6] += 1;
						if (true)
						{
						}
						num3 = 1;
						continue;
					}
					case 6:
						num3 = 17;
						continue;
					case 7:
						return;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							goto IL_17D;
						}
						break;
					case 9:
						goto IL_17D;
					case 10:
					{
						int num5 = 138;
						int num7 = 3;
						num3 = 19;
						continue;
					}
					case 11:
						goto IL_17D;
					case 12:
						goto IL_2E3;
					case 13:
					{
						short[] array2 = A_0.ᜀ;
						int num8 = num;
						int num4;
						array2[num8] += (short)num4;
						num3 = 9;
						continue;
					}
					case 14:
					{
						short[] array3 = A_0.ᜀ;
						int num9 = num10;
						array3[num9] += 1;
						int num4 = 0;
						num3 = 18;
						continue;
					}
					case 15:
						if (num2 < this.ᜅ)
						{
							num3 = 6;
							continue;
						}
						goto IL_BE;
					case 16:
					{
						int num4;
						int num7;
						if (num4 < num7)
						{
							num3 = 13;
							continue;
						}
						num3 = 2;
						continue;
					}
					case 17:
						if (num != (int)this.ᜂ[num2])
						{
							num3 = 4;
							continue;
						}
						num2++;
						num3 = 0;
						continue;
					case 18:
						goto IL_2CC;
					case 19:
						goto IL_2CC;
					case 20:
					{
						short[] array4 = A_0.ᜀ;
						int num11 = 16;
						array4[num11] += 1;
						num3 = 11;
						continue;
					}
					case 21:
					{
						if (num2 >= this.ᜅ)
						{
							num3 = 7;
							continue;
						}
						int num4 = 1;
						num10 = (int)this.ᜂ[num2];
						num3 = 25;
						continue;
					}
					case 22:
					{
						int num4;
						if (num4 <= 10)
						{
							num3 = 5;
							continue;
						}
						short[] array5 = A_0.ᜀ;
						int num12 = 18;
						array5[num12] += 1;
						num3 = 8;
						continue;
					}
					case 23:
						goto IL_2E3;
					case 24:
						if (num != num10)
						{
							num3 = 14;
							continue;
						}
						goto IL_2CC;
					case 25:
					{
						if (num10 == 0)
						{
							num3 = 10;
							continue;
						}
						int num5 = 6;
						int num7 = 3;
						num3 = 24;
						continue;
					}
					}
					break;
					IL_BE:
					num3 = 16;
					continue;
					IL_17D:
					num3 = 21;
					continue;
					IL_2CC:
					num = num10;
					num2++;
					num3 = 12;
					continue;
					IL_2E3:
					num3 = 15;
				}
			}
			return;
		}
	}

	// Token: 0x060029C1 RID: 10689 RVA: 0x0029B294 File Offset: 0x0029A294
	public void ᜀ(sprᴂ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = -1;
				int num2 = 0;
				int num3 = 15;
				for (;;)
				{
					int num7;
					switch (num3)
					{
					case 0:
						goto IL_185;
					case 1:
						if (true)
						{
						}
						if (num != (int)this.ᜂ[num2])
						{
							num3 = 20;
							continue;
						}
						num2++;
						num3 = 2;
						continue;
					case 2:
					{
						int num4;
						int num5;
						if (++num4 < num5)
						{
							num3 = 11;
							continue;
						}
						goto IL_331;
					}
					case 3:
						goto IL_2FF;
					case 4:
					{
						int num4;
						if (num4-- <= 0)
						{
							num3 = 16;
							continue;
						}
						A_0.ᜀ(num);
						num3 = 21;
						continue;
					}
					case 5:
					{
						int num5 = 138;
						int num6 = 3;
						num3 = 25;
						continue;
					}
					case 6:
						goto IL_1C1;
					case 7:
						goto IL_1C1;
					case 8:
					{
						A_0.ᜀ(16);
						int num4;
						this.ᜇ.ᜁ(num4 - 3, 2);
						num3 = 14;
						continue;
					}
					case 9:
					{
						int num4;
						if (num4 <= 10)
						{
							num3 = 27;
							continue;
						}
						A_0.ᜀ(18);
						this.ᜇ.ᜁ(num4 - 11, 7);
						num3 = 29;
						continue;
					}
					case 10:
						goto IL_1EB;
					case 11:
						goto IL_1EB;
					case 12:
					{
						A_0.ᜀ(num7);
						int num4 = 0;
						num3 = 0;
						continue;
					}
					case 13:
						return;
					case 14:
						goto IL_1C1;
					case 15:
						goto IL_1C1;
					case 16:
						num3 = 6;
						continue;
					case 17:
						if (num != 0)
						{
							goto IL_B7;
						}
						num3 = 9;
						continue;
					case 18:
						num3 = 3;
						continue;
					case 19:
					{
						int num4;
						int num6;
						if (num4 < num6)
						{
							num3 = 18;
							continue;
						}
						num3 = 17;
						continue;
					}
					case 20:
						goto IL_331;
					case 21:
						goto IL_2FF;
					case 22:
					{
						if (num2 >= this.ᜅ)
						{
							num3 = 13;
							continue;
						}
						int num4 = 1;
						num7 = (int)this.ᜂ[num2];
						num3 = 24;
						continue;
					}
					case 23:
						num3 = 1;
						continue;
					case 24:
					{
						if (num7 == 0)
						{
							num3 = 5;
							continue;
						}
						int num5 = 6;
						int num6 = 3;
						num3 = 28;
						continue;
					}
					case 25:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B7;
						default:
							if (false)
							{
							}
							goto IL_185;
						}
						break;
					case 26:
						if (num2 < this.ᜅ)
						{
							num3 = 23;
							continue;
						}
						goto IL_331;
					case 27:
					{
						A_0.ᜀ(17);
						int num4;
						this.ᜇ.ᜁ(num4 - 3, 3);
						num3 = 7;
						continue;
					}
					case 28:
						if (num != num7)
						{
							num3 = 12;
							continue;
						}
						goto IL_185;
					case 29:
						goto IL_1C1;
					}
					break;
					IL_B7:
					num3 = 8;
					continue;
					IL_185:
					num = num7;
					num2++;
					num3 = 10;
					continue;
					IL_1C1:
					num3 = 22;
					continue;
					IL_1EB:
					num3 = 26;
					continue;
					IL_2FF:
					num3 = 4;
					continue;
					IL_331:
					num3 = 19;
				}
			}
			return;
		}
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x0029B5F8 File Offset: 0x0029A5F8
	public int ᜆ()
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
		return this.ᜅ;
	}

	// Token: 0x060029C3 RID: 10691 RVA: 0x0029B63C File Offset: 0x0029A63C
	public byte[] ᜃ()
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
		return this.ᜂ;
	}

	// Token: 0x060029C4 RID: 10692 RVA: 0x0029B680 File Offset: 0x0029A680
	public short[] ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x040024AF RID: 9391
	private short[] ᜀ;

	// Token: 0x040024B0 RID: 9392
	private short[] ᜁ;

	// Token: 0x040024B1 RID: 9393
	private byte[] ᜂ;

	// Token: 0x040024B2 RID: 9394
	private int[] ᜃ;

	// Token: 0x040024B3 RID: 9395
	private int ᜄ;

	// Token: 0x040024B4 RID: 9396
	private int ᜅ;

	// Token: 0x040024B5 RID: 9397
	private int ᜆ;

	// Token: 0x040024B6 RID: 9398
	private spr\u234C ᜇ;
}
