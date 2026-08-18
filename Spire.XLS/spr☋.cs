using System;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005AF RID: 1455
internal class spr\u260B
{
	// Token: 0x06005803 RID: 22531 RVA: 0x0037DA9C File Offset: 0x0037CA9C
	public spr\u260B(spr᥏ A_0, int A_1, int A_2, int A_3)
	{
		this.ᜇ = A_0;
		this.ᜄ = A_2;
		this.ᜆ = A_3;
		this.ᜀ = new short[A_1];
		this.ᜃ = new int[A_3];
	}

	// Token: 0x06005804 RID: 22532 RVA: 0x0037DAE0 File Offset: 0x0037CAE0
	public void ᜂ()
	{
		for (;;)
		{
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= this.ᜀ.Length)
					{
						goto IL_5B;
					}
					this.ᜀ[num] = 0;
					num++;
					num2 = 1;
					continue;
				case 1:
					goto IL_2C;
				case 2:
					if (true)
					{
					}
					goto IL_2C;
				case 3:
					goto IL_63;
				}
				break;
				IL_2C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5B:
					num2 = 3;
					break;
				default:
					if (false)
					{
					}
					num2 = 0;
					break;
				}
			}
		}
		IL_63:
		this.ᜁ = null;
		this.ᜂ = null;
	}

	// Token: 0x06005805 RID: 22533 RVA: 0x0037DB84 File Offset: 0x0037CB84
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

	// Token: 0x06005806 RID: 22534 RVA: 0x0037DBE0 File Offset: 0x0037CBE0
	public void ᜇ()
	{
		for (;;)
		{
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_24;
				case 2:
					if (num >= this.ᜀ.Length)
					{
						goto IL_53;
					}
					if (true)
					{
					}
					num++;
					num2 = 1;
					continue;
				case 3:
					goto IL_24;
				}
				break;
				IL_24:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_53:
					num2 = 0;
					break;
				default:
					if (false)
					{
					}
					num2 = 2;
					break;
				}
			}
		}
	}

	// Token: 0x06005807 RID: 22535 RVA: 0x0037DC6C File Offset: 0x0037CC6C
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

	// Token: 0x06005808 RID: 22536 RVA: 0x0037DCCC File Offset: 0x0037CCCC
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
				int num3 = 3;
				for (;;)
				{
					int num5;
					switch (num3)
					{
					case 0:
					{
						int num4;
						if (num4 > 0)
						{
							num3 = 10;
							continue;
						}
						goto IL_175;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num5 = 0;
							num3 = 9;
							continue;
						}
						break;
					case 2:
						return;
					case 3:
						goto IL_DF;
					case 4:
						if (num2 >= this.ᜆ)
						{
							num3 = 1;
							continue;
						}
						array[num2] = num;
						num += this.ᜃ[num2] << 15 - num2;
						num2++;
						num3 = 5;
						continue;
					case 5:
						goto IL_DF;
					case 6:
						goto IL_11E;
					case 7:
						goto IL_175;
					case 8:
					{
						if (num5 >= this.ᜅ)
						{
							if (true)
							{
							}
							num3 = 2;
							continue;
						}
						int num4 = (int)this.ᜂ[num5];
						num3 = 0;
						continue;
					}
					case 9:
						goto IL_11E;
					case 10:
					{
						int num4;
						this.ᜁ[num5] = sprៜ.ᜀ(array[num4 - 1]);
						array[num4 - 1] += 1 << 16 - num4;
						num3 = 7;
						continue;
					}
					}
					break;
					IL_DF:
					num3 = 4;
					continue;
					IL_11E:
					num3 = 8;
					continue;
					IL_175:
					num5++;
					num3 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06005809 RID: 22537 RVA: 0x0037DE7C File Offset: 0x0037CE7C
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
				int num5 = 5;
				for (;;)
				{
					int num6;
					int num8;
					int[] array;
					int num10;
					int num11;
					switch (num5)
					{
					case 0:
						goto IL_41C;
					case 1:
						if (this.ᜃ[--num6] != 0)
						{
							num5 = 0;
							continue;
						}
						goto IL_FF;
					case 2:
						goto IL_287;
					case 3:
						goto IL_25B;
					case 4:
						goto IL_DE;
					case 5:
						goto IL_4A8;
					case 6:
						goto IL_3FA;
					case 7:
						return;
					case 8:
					{
						int num7;
						num7--;
						num5 = 2;
						continue;
					}
					case 9:
						goto IL_2AB;
					case 10:
						goto IL_4A8;
					case 11:
						num8 = this.ᜆ;
						num3++;
						num5 = 33;
						continue;
					case 12:
						if (num8 > this.ᜆ)
						{
							num5 = 11;
							continue;
						}
						goto IL_2E0;
					case 13:
					{
						int num7;
						if (num7 == 0)
						{
							num5 = 19;
							continue;
						}
						int num9 = this.ᜃ[num7 - 1];
						num5 = 22;
						continue;
					}
					case 14:
						num8 = array[num10] + 1;
						num5 = 12;
						continue;
					case 15:
						num5 = 21;
						continue;
					case 16:
						num5 = 31;
						continue;
					case 17:
						if (num3 <= 0)
						{
							num5 = 27;
							continue;
						}
						goto IL_FF;
					case 18:
					{
						if (A_0[num11] != -1)
						{
							num5 = 14;
							continue;
						}
						int num12 = array[num10];
						this.ᜃ[num12 - 1]++;
						this.ᜂ[A_0[num11 - 1]] = (byte)array[num10];
						num5 = 23;
						continue;
					}
					case 19:
						return;
					case 20:
						goto IL_3FA;
					case 21:
						if (num6 >= this.ᜆ - 1)
						{
							num5 = 4;
							continue;
						}
						goto IL_41C;
					case 22:
						goto IL_25B;
					case 23:
						goto IL_2AB;
					case 24:
						array = new int[num];
						array[num - 1] = 0;
						num10 = num - 1;
						num5 = 6;
						continue;
					case 25:
					{
						int num13;
						if (A_0[num13 + 1] == -1)
						{
							num5 = 34;
							continue;
						}
						goto IL_25B;
					}
					case 26:
						if (num10 < 0)
						{
							num5 = 16;
							continue;
						}
						num11 = 2 * num10 + 1;
						num5 = 18;
						continue;
					case 27:
					{
						this.ᜃ[this.ᜆ - 1] += num3;
						this.ᜃ[this.ᜆ - 2] -= num3;
						int num14 = 2 * num2;
						int num7 = this.ᜆ;
						num5 = 35;
						continue;
					}
					case 28:
						if (num4 >= this.ᜆ)
						{
							num5 = 24;
							continue;
						}
						this.ᜃ[num4] = 0;
						num4++;
						num5 = 10;
						continue;
					case 29:
					{
						int num9;
						if (num9 <= 0)
						{
							num5 = 8;
							continue;
						}
						int num14;
						int num13 = 2 * A_0[num14++];
						num5 = 25;
						continue;
					}
					case 30:
						if (num3 <= 0)
						{
							goto IL_DE;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2AB;
						default:
							if (false)
							{
							}
							num5 = 15;
							continue;
						}
						break;
					case 31:
						if (num3 == 0)
						{
							num5 = 7;
							continue;
						}
						if (true)
						{
						}
						num6 = this.ᜆ - 1;
						num5 = 32;
						continue;
					case 32:
						goto IL_FF;
					case 33:
						goto IL_2E0;
					case 34:
					{
						int num7;
						int num13;
						this.ᜂ[A_0[num13]] = (byte)num7;
						int num9;
						num9--;
						num5 = 3;
						continue;
					}
					case 35:
						goto IL_287;
					}
					break;
					IL_DE:
					num5 = 17;
					continue;
					IL_FF:
					num5 = 1;
					continue;
					IL_25B:
					num5 = 29;
					continue;
					IL_287:
					num5 = 13;
					continue;
					IL_2AB:
					num10--;
					num5 = 20;
					continue;
					IL_2E0:
					array[A_0[num11 - 1]] = (array[A_0[num11]] = num8);
					num5 = 9;
					continue;
					IL_3FA:
					num5 = 26;
					continue;
					IL_41C:
					this.ᜃ[num6]--;
					this.ᜃ[++num6]++;
					num3 -= 1 << this.ᜆ - 1 - num6;
					num5 = 30;
					continue;
					IL_4A8:
					num5 = 28;
				}
			}
			return;
		}
	}

	// Token: 0x0600580A RID: 22538 RVA: 0x0037E37C File Offset: 0x0037D37C
	public void ᜁ()
	{
		int a_ = 17;
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
				int num5 = 22;
				for (;;)
				{
					int num6;
					int num8;
					int num11;
					int[] array3;
					int num13;
					int num14;
					switch (num5)
					{
					case 0:
						if (array[0] != array2.Length / 2 - 1)
						{
							num5 = 44;
							continue;
						}
						goto IL_703;
					case 1:
						goto IL_484;
					case 2:
						goto IL_4C3;
					case 3:
						if (num6 >= num2)
						{
							num5 = 52;
							continue;
						}
						num5 = 50;
						continue;
					case 4:
						goto IL_44D;
					case 5:
					{
						int num7;
						int num9;
						if ((int)this.ᜀ[array[num7 = (num8 - 1) / 2]] <= num9)
						{
							num5 = 1;
							continue;
						}
						array[num8] = array[num7];
						num8 = num7;
						num5 = 11;
						continue;
					}
					case 6:
					{
						if (num4 >= num)
						{
							num5 = 21;
							continue;
						}
						int num9 = (int)this.ᜀ[num4];
						num5 = 12;
						continue;
					}
					case 7:
					{
						this.ᜅ = Math.Max(num3 + 1, this.ᜄ);
						int num10 = num2;
						num11 = num10;
						array2 = new int[4 * num2 - 2];
						array3 = new int[2 * num2 - 1];
						int num12 = 0;
						num5 = 16;
						continue;
					}
					case 8:
						goto IL_2B8;
					case 9:
						num8 = num2++;
						num5 = 19;
						continue;
					case 10:
						num5 = 0;
						continue;
					case 11:
						goto IL_61C;
					case 12:
					{
						int num9;
						if (num9 != 0)
						{
							num5 = 9;
							continue;
						}
						goto IL_36F;
					}
					case 13:
						num5 = 25;
						continue;
					case 14:
						if (array3[array[num6]] > array3[array[num6 + 1]])
						{
							num5 = 31;
							continue;
						}
						goto IL_49E;
					case 15:
						goto IL_2B8;
					case 16:
						goto IL_2F4;
					case 17:
						goto IL_5F5;
					case 18:
						array[num2++] = ((num3 < 2) ? (++num3) : 0);
						num5 = 24;
						continue;
					case 19:
						goto IL_61C;
					case 20:
						num6++;
						num5 = 38;
						continue;
					case 21:
						num5 = 2;
						continue;
					case 22:
						goto IL_34D;
					case 23:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24F;
						default:
							if (false)
							{
							}
							goto IL_34D;
						}
						break;
					case 24:
						goto IL_4C3;
					case 25:
						if (array3[array[num6]] > array3[array[num6 + 1]])
						{
							num5 = 20;
							continue;
						}
						goto IL_275;
					case 26:
						goto IL_152;
					case 27:
						if ((num6 = num13) > 0)
						{
							num5 = 32;
							continue;
						}
						goto IL_1CF;
					case 28:
						goto IL_2F4;
					case 29:
						goto IL_36F;
					case 30:
						num5 = 5;
						continue;
					case 31:
						num6++;
						num5 = 41;
						continue;
					case 32:
						num5 = 48;
						continue;
					case 33:
						goto IL_5AC;
					case 34:
						if (num8 > 0)
						{
							num5 = 30;
							continue;
						}
						goto IL_484;
					case 35:
						goto IL_1CF;
					case 36:
						goto IL_421;
					case 37:
						if ((num6 = num13) > 0)
						{
							num5 = 46;
							continue;
						}
						goto IL_5F5;
					case 38:
						goto IL_275;
					case 39:
						goto IL_24F;
					case 40:
						if (num2 >= 2)
						{
							num5 = 7;
							continue;
						}
						num5 = 18;
						continue;
					case 41:
						goto IL_49E;
					case 42:
						goto IL_5AC;
					case 43:
						goto IL_152;
					case 44:
						goto IL_683;
					case 45:
						goto IL_44D;
					case 46:
						num5 = 39;
						continue;
					case 47:
						if (num6 + 1 < num2)
						{
							num5 = 53;
							continue;
						}
						goto IL_49E;
					case 48:
						if (array3[array[num13 = (num6 - 1) / 2]] <= num14)
						{
							num5 = 35;
							continue;
						}
						array[num6] = array[num13];
						num5 = 45;
						continue;
					case 49:
						num5 = 15;
						continue;
					case 50:
						if (num6 + 1 < num2)
						{
							num5 = 13;
							continue;
						}
						goto IL_275;
					case 51:
						if (num6 >= num2)
						{
							num5 = 49;
							continue;
						}
						num5 = 47;
						continue;
					case 52:
						num5 = 4;
						continue;
					case 53:
						num5 = 14;
						continue;
					case 54:
					{
						if (true)
						{
						}
						int num12;
						if (num12 >= num2)
						{
							num5 = 36;
							continue;
						}
						int num15 = array[num12];
						int num16 = 2 * num12;
						array2[num16] = num15;
						array2[num16 + 1] = -1;
						array3[num12] = (int)this.ᜀ[num15] << 8;
						array[num12] = num12;
						num12++;
						num5 = 28;
						continue;
					}
					case 55:
						if (num2 <= 1)
						{
							num5 = 10;
							continue;
						}
						goto IL_421;
					}
					break;
					IL_152:
					num5 = 3;
					continue;
					IL_1CF:
					int num17;
					array[num6] = num17;
					int num18 = array[0];
					num17 = num11++;
					int num19;
					array2[2 * num17] = num19;
					array2[2 * num17 + 1] = num18;
					int num20 = Math.Min(array3[num19] & 255, array3[num18] & 255);
					num14 = (array3[num17] = array3[num19] + array3[num18] - num20 + 1);
					num13 = 0;
					num6 = 1;
					num5 = 33;
					continue;
					IL_24F:
					if (array3[array[num13 = (num6 - 1) / 2]] <= num14)
					{
						num5 = 17;
						continue;
					}
					array[num6] = array[num13];
					num5 = 8;
					continue;
					IL_275:
					array[num13] = array[num6];
					num13 = num6;
					num6 = num13 * 2 + 1;
					num5 = 43;
					continue;
					IL_2B8:
					num5 = 37;
					continue;
					IL_2F4:
					num5 = 54;
					continue;
					IL_34D:
					num5 = 6;
					continue;
					IL_36F:
					num4++;
					num5 = 23;
					continue;
					IL_421:
					num19 = array[0];
					num17 = array[--num2];
					num14 = array3[num17];
					num13 = 0;
					num6 = 1;
					num5 = 26;
					continue;
					IL_44D:
					num5 = 27;
					continue;
					IL_484:
					array[num8] = num4;
					num3 = num4;
					num5 = 29;
					continue;
					IL_49E:
					array[num13] = array[num6];
					num13 = num6;
					num6 = num13 * 2 + 1;
					num5 = 42;
					continue;
					IL_4C3:
					num5 = 40;
					continue;
					IL_5AC:
					num5 = 51;
					continue;
					IL_5F5:
					array[num6] = num17;
					num5 = 55;
					continue;
					IL_61C:
					num5 = 34;
				}
			}
			IL_683:
			throw new ApplicationException(RecordTableEnumerator.b("ཆⱈ⩊㵌潎㡐㵒⍔㙖⭘㉚㱜ㅞᕠ䍢፤๦٨ݪ౬᭮ᑰᝲ", a_));
			IL_703:
			this.ᜀ(array2);
			return;
		}
		}
	}

	// Token: 0x0600580B RID: 22539 RVA: 0x0037EA94 File Offset: 0x0037DA94
	public int ᜄ()
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 0;
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num2 < this.ᜀ.Length)
					{
						if (true)
						{
						}
						num += (int)(this.ᜀ[num2] * (short)this.ᜂ[num2]);
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num3 = 2;
							continue;
						}
					}
					num3 = 3;
					continue;
				case 1:
					goto IL_30;
				case 2:
					goto IL_30;
				case 3:
					return num;
				}
				break;
				IL_30:
				num3 = 0;
			}
		}
		return num;
	}

	// Token: 0x0600580C RID: 22540 RVA: 0x0037EB38 File Offset: 0x0037DB38
	public void ᜁ(spr\u260B A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = -1;
				int num2 = 0;
				int num3 = 7;
				for (;;)
				{
					int num4;
					switch (num3)
					{
					case 0:
					{
						if (num4 == 0)
						{
							num3 = 4;
							continue;
						}
						int num5 = 6;
						int num6 = 3;
						num3 = 16;
						continue;
					}
					case 1:
						goto IL_2CC;
					case 2:
						num3 = 17;
						continue;
					case 3:
					{
						short[] array = A_0.ᜀ;
						int num7 = num;
						int num8;
						array[num7] += (short)num8;
						num3 = 12;
						continue;
					}
					case 4:
					{
						int num5 = 138;
						int num6 = 3;
						num3 = 1;
						continue;
					}
					case 5:
					{
						int num6;
						int num8;
						if (num8 < num6)
						{
							num3 = 3;
							continue;
						}
						num3 = 18;
						continue;
					}
					case 6:
					{
						if (num2 >= this.ᜅ)
						{
							num3 = 15;
							continue;
						}
						int num8 = 1;
						num4 = (int)this.ᜂ[num2];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_223;
						default:
							if (false)
							{
							}
							num3 = 0;
							continue;
						}
						break;
					}
					case 7:
						goto IL_17D;
					case 8:
					{
						int num5;
						int num8;
						if (++num8 < num5)
						{
							num3 = 11;
							continue;
						}
						goto IL_BE;
					}
					case 9:
						goto IL_2E3;
					case 10:
						goto IL_17D;
					case 11:
						goto IL_2E3;
					case 12:
						goto IL_17D;
					case 13:
						goto IL_223;
					case 14:
						goto IL_17D;
					case 15:
						return;
					case 16:
						if (num != num4)
						{
							num3 = 19;
							continue;
						}
						goto IL_2CC;
					case 17:
						if (num != (int)this.ᜂ[num2])
						{
							num3 = 20;
							continue;
						}
						num2++;
						num3 = 8;
						continue;
					case 18:
						if (num != 0)
						{
							num3 = 21;
							continue;
						}
						num3 = 25;
						continue;
					case 19:
					{
						short[] array2 = A_0.ᜀ;
						int num9 = num4;
						array2[num9] += 1;
						int num8 = 0;
						num3 = 23;
						continue;
					}
					case 20:
						goto IL_BE;
					case 21:
					{
						short[] array3 = A_0.ᜀ;
						int num10 = 16;
						array3[num10] += 1;
						num3 = 14;
						continue;
					}
					case 22:
					{
						short[] array4 = A_0.ᜀ;
						int num11 = 17;
						array4[num11] += 1;
						if (true)
						{
						}
						num3 = 10;
						continue;
					}
					case 23:
						goto IL_2CC;
					case 24:
						if (num2 < this.ᜅ)
						{
							num3 = 2;
							continue;
						}
						goto IL_BE;
					case 25:
					{
						int num8;
						if (num8 <= 10)
						{
							num3 = 22;
							continue;
						}
						short[] array5 = A_0.ᜀ;
						int num12 = 18;
						array5[num12] += 1;
						num3 = 13;
						continue;
					}
					}
					break;
					IL_BE:
					num3 = 5;
					continue;
					IL_17D:
					num3 = 6;
					continue;
					IL_223:
					goto IL_17D;
					IL_2CC:
					num = num4;
					num2++;
					num3 = 9;
					continue;
					IL_2E3:
					num3 = 24;
				}
			}
			return;
		}
	}

	// Token: 0x0600580D RID: 22541 RVA: 0x0037EE80 File Offset: 0x0037DE80
	public void ᜀ(spr\u260B A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = -1;
				int num2 = 0;
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_317:
					num3 = 16;
					break;
				default:
					if (false)
					{
					}
					num3 = 15;
					break;
				}
				for (;;)
				{
					int num4;
					switch (num3)
					{
					case 0:
					{
						A_0.ᜀ(num4);
						int num5 = 0;
						num3 = 24;
						continue;
					}
					case 1:
					{
						int num6 = 138;
						int num7 = 3;
						num3 = 3;
						continue;
					}
					case 2:
					{
						int num5;
						if (num5-- <= 0)
						{
							goto IL_317;
						}
						A_0.ᜀ(num);
						num3 = 18;
						continue;
					}
					case 3:
						goto IL_1A1;
					case 4:
						if (num != num4)
						{
							num3 = 0;
							continue;
						}
						goto IL_1A1;
					case 5:
						if (num2 < this.ᜅ)
						{
							num3 = 28;
							continue;
						}
						goto IL_328;
					case 6:
						goto IL_328;
					case 7:
						goto IL_207;
					case 8:
						return;
					case 9:
					{
						A_0.ᜀ(17);
						int num5;
						this.ᜇ.ᜁ(num5 - 3, 3);
						num3 = 14;
						continue;
					}
					case 10:
						num3 = 25;
						continue;
					case 11:
					{
						if (num4 == 0)
						{
							num3 = 1;
							continue;
						}
						int num6 = 6;
						int num7 = 3;
						num3 = 4;
						continue;
					}
					case 12:
					{
						if (num2 >= this.ᜅ)
						{
							num3 = 8;
							continue;
						}
						int num5 = 1;
						num4 = (int)this.ᜂ[num2];
						num3 = 11;
						continue;
					}
					case 13:
					{
						int num5;
						int num6;
						if (++num5 < num6)
						{
							num3 = 27;
							continue;
						}
						goto IL_328;
					}
					case 14:
						goto IL_1DD;
					case 15:
						goto IL_1DD;
					case 16:
						num3 = 17;
						continue;
					case 17:
						goto IL_1DD;
					case 18:
						goto IL_300;
					case 19:
						if (true)
						{
						}
						if (num != (int)this.ᜂ[num2])
						{
							num3 = 6;
							continue;
						}
						num2++;
						num3 = 13;
						continue;
					case 20:
					{
						int num5;
						int num7;
						if (num5 < num7)
						{
							num3 = 10;
							continue;
						}
						num3 = 22;
						continue;
					}
					case 21:
						goto IL_1DD;
					case 22:
						if (num != 0)
						{
							num3 = 29;
							continue;
						}
						num3 = 26;
						continue;
					case 23:
						goto IL_1DD;
					case 24:
						goto IL_1A1;
					case 25:
						goto IL_300;
					case 26:
					{
						int num5;
						if (num5 <= 10)
						{
							num3 = 9;
							continue;
						}
						A_0.ᜀ(18);
						this.ᜇ.ᜁ(num5 - 11, 7);
						num3 = 21;
						continue;
					}
					case 27:
						goto IL_207;
					case 28:
						num3 = 19;
						continue;
					case 29:
					{
						A_0.ᜀ(16);
						int num5;
						this.ᜇ.ᜁ(num5 - 3, 2);
						num3 = 23;
						continue;
					}
					}
					break;
					IL_1A1:
					num = num4;
					num2++;
					num3 = 7;
					continue;
					IL_1DD:
					num3 = 12;
					continue;
					IL_207:
					num3 = 5;
					continue;
					IL_300:
					num3 = 2;
					continue;
					IL_328:
					num3 = 20;
				}
			}
			return;
		}
	}

	// Token: 0x0600580E RID: 22542 RVA: 0x0037F1DC File Offset: 0x0037E1DC
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

	// Token: 0x0600580F RID: 22543 RVA: 0x0037F220 File Offset: 0x0037E220
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

	// Token: 0x06005810 RID: 22544 RVA: 0x0037F264 File Offset: 0x0037E264
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

	// Token: 0x040029DC RID: 10716
	private short[] ᜀ;

	// Token: 0x040029DD RID: 10717
	private short[] ᜁ;

	// Token: 0x040029DE RID: 10718
	private byte[] ᜂ;

	// Token: 0x040029DF RID: 10719
	private int[] ᜃ;

	// Token: 0x040029E0 RID: 10720
	private int ᜄ;

	// Token: 0x040029E1 RID: 10721
	private int ᜅ;

	// Token: 0x040029E2 RID: 10722
	private int ᜆ;

	// Token: 0x040029E3 RID: 10723
	private spr᥏ ᜇ;
}
