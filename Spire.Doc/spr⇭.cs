using System;

// Token: 0x020003BC RID: 956
[CLSCompliant(false)]
internal class spr\u21ED
{
	// Token: 0x0600360A RID: 13834 RVA: 0x0032C324 File Offset: 0x0032B324
	internal spr\u21ED(spr\u21ED.KeySize A_0, byte[] A_1)
	{
		this.ᜉ = A_0;
		this.ᜀ(A_0);
		this.ᜃ = new byte[this.ᜁ * 4];
		A_1.CopyTo(this.ᜃ, 0);
		this.ᜊ();
	}

	// Token: 0x0600360B RID: 13835 RVA: 0x0032C36C File Offset: 0x0032B36C
	private void ᜊ()
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
		this.ᜉ();
		this.ᜈ();
		this.ᜇ();
		this.ᜀ();
	}

	// Token: 0x0600360C RID: 13836 RVA: 0x0032C3C0 File Offset: 0x0032B3C0
	internal void ᜁ(byte[] A_0, byte[] A_1)
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
			for (;;)
			{
				this.ᜊ();
				this.ᜈ = new byte[4, this.ᜀ];
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_CA;
					case 2:
					{
						int num3;
						if (num3 >= 4 * this.ᜀ)
						{
							num2 = 0;
							continue;
						}
						A_1[num3] = this.ᜈ[num3 % 4, num3 / 4];
						num3++;
						num2 = 6;
						continue;
					}
					case 3:
						if (true)
						{
						}
						goto IL_FA;
					case 4:
					{
						this.ᜆ();
						this.ᜄ();
						this.ᜀ(this.ᜂ);
						int num3 = 0;
						num2 = 3;
						continue;
					}
					case 5:
						goto IL_A4;
					case 6:
						goto IL_FA;
					case 7:
						goto IL_CA;
					case 8:
						goto IL_A4;
					case 9:
					{
						this.ᜀ(0);
						int num4 = 1;
						num2 = 8;
						continue;
					}
					case 10:
					{
						int num4;
						if (num4 > this.ᜂ - 1)
						{
							num2 = 4;
							continue;
						}
						this.ᜆ();
						this.ᜄ();
						this.ᜂ();
						this.ᜀ(num4);
						num4++;
						num2 = 5;
						continue;
					}
					case 11:
						if (num >= 4 * this.ᜀ)
						{
							num2 = 9;
							continue;
						}
						this.ᜈ[num % 4, num / 4] = A_0[num];
						num++;
						num2 = 7;
						continue;
					}
					break;
					IL_A4:
					num2 = 10;
					continue;
					IL_CA:
					num2 = 11;
					continue;
					IL_FA:
					num2 = 2;
				}
			}
			break;
		}
	}

	// Token: 0x0600360D RID: 13837 RVA: 0x0032C580 File Offset: 0x0032B580
	internal void ᜀ(byte[] A_0, byte[] A_1)
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
			for (;;)
			{
				this.ᜈ = new byte[4, this.ᜀ];
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_AD;
					case 1:
						goto IL_AD;
					case 2:
					{
						int num3;
						if (num3 >= 4 * this.ᜀ)
						{
							num2 = 8;
							continue;
						}
						A_1[num3] = this.ᜈ[num3 % 4, num3 / 4];
						num3++;
						num2 = 4;
						continue;
					}
					case 3:
						if (num >= 4 * this.ᜀ)
						{
							num2 = 5;
							continue;
						}
						this.ᜈ[num % 4, num / 4] = A_0[num];
						num++;
						num2 = 1;
						continue;
					case 4:
						goto IL_DD;
					case 5:
					{
						this.ᜀ(this.ᜂ);
						int num4 = this.ᜂ - 1;
						num2 = 6;
						continue;
					}
					case 6:
						goto IL_91;
					case 7:
						goto IL_DD;
					case 8:
						return;
					case 9:
					{
						int num4;
						if (num4 < 1)
						{
							num2 = 10;
							continue;
						}
						if (true)
						{
						}
						this.ᜃ();
						this.ᜅ();
						this.ᜀ(num4);
						this.ᜁ();
						num4--;
						num2 = 11;
						continue;
					}
					case 10:
					{
						this.ᜃ();
						this.ᜅ();
						this.ᜀ(0);
						int num3 = 0;
						num2 = 7;
						continue;
					}
					case 11:
						goto IL_91;
					}
					break;
					IL_91:
					num2 = 9;
					continue;
					IL_AD:
					num2 = 3;
					continue;
					IL_DD:
					num2 = 2;
				}
			}
			break;
		}
	}

	// Token: 0x0600360E RID: 13838 RVA: 0x0032C734 File Offset: 0x0032B734
	private void ᜀ(spr\u21ED.KeySize A_0)
	{
		for (;;)
		{
			this.ᜀ = 4;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5D;
				case 1:
					goto IL_B9;
				case 2:
					if (A_0 != spr\u21ED.KeySize.Bits128)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DD;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 3:
					goto IL_DD;
				case 4:
					this.ᜁ = 8;
					this.ᜂ = 14;
					num = 1;
					continue;
				case 5:
					if (A_0 == spr\u21ED.KeySize.Bits256)
					{
						num = 4;
						continue;
					}
					return;
				case 6:
					if (true)
					{
					}
					if (A_0 == spr\u21ED.KeySize.Bits192)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
				}
				break;
			}
		}
		IL_5D:
		this.ᜁ = 4;
		this.ᜂ = 10;
		return;
		IL_B9:
		return;
		IL_DD:
		this.ᜁ = 6;
		this.ᜂ = 12;
	}

	// Token: 0x0600360F RID: 13839 RVA: 0x0032C820 File Offset: 0x0032B820
	private void ᜉ()
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
		this.ᜄ = new byte[,]
		{
			{
				99,
				124,
				119,
				123,
				242,
				107,
				111,
				197,
				48,
				1,
				103,
				43,
				254,
				215,
				171,
				118
			},
			{
				202,
				130,
				201,
				125,
				250,
				89,
				71,
				240,
				173,
				212,
				162,
				175,
				156,
				164,
				114,
				192
			},
			{
				183,
				253,
				147,
				38,
				54,
				63,
				247,
				204,
				52,
				165,
				229,
				241,
				113,
				216,
				49,
				21
			},
			{
				4,
				199,
				35,
				195,
				24,
				150,
				5,
				154,
				7,
				18,
				128,
				226,
				235,
				39,
				178,
				117
			},
			{
				9,
				131,
				44,
				26,
				27,
				110,
				90,
				160,
				82,
				59,
				214,
				179,
				41,
				227,
				47,
				132
			},
			{
				83,
				209,
				0,
				237,
				32,
				252,
				177,
				91,
				106,
				203,
				190,
				57,
				74,
				76,
				88,
				207
			},
			{
				208,
				239,
				170,
				251,
				67,
				77,
				51,
				133,
				69,
				249,
				2,
				127,
				80,
				60,
				159,
				168
			},
			{
				81,
				163,
				64,
				143,
				146,
				157,
				56,
				245,
				188,
				182,
				218,
				33,
				16,
				byte.MaxValue,
				243,
				210
			},
			{
				205,
				12,
				19,
				236,
				95,
				151,
				68,
				23,
				196,
				167,
				126,
				61,
				100,
				93,
				25,
				115
			},
			{
				96,
				129,
				79,
				220,
				34,
				42,
				144,
				136,
				70,
				238,
				184,
				20,
				222,
				94,
				11,
				219
			},
			{
				224,
				50,
				58,
				10,
				73,
				6,
				36,
				92,
				194,
				211,
				172,
				98,
				145,
				149,
				228,
				121
			},
			{
				231,
				200,
				55,
				109,
				141,
				213,
				78,
				169,
				108,
				86,
				244,
				234,
				101,
				122,
				174,
				8
			},
			{
				186,
				120,
				37,
				46,
				28,
				166,
				180,
				198,
				232,
				221,
				116,
				31,
				75,
				189,
				139,
				138
			},
			{
				112,
				62,
				181,
				102,
				72,
				3,
				246,
				14,
				97,
				53,
				87,
				185,
				134,
				193,
				29,
				158
			},
			{
				225,
				248,
				152,
				17,
				105,
				217,
				142,
				148,
				155,
				30,
				135,
				233,
				206,
				85,
				40,
				223
			},
			{
				140,
				161,
				137,
				13,
				191,
				230,
				66,
				104,
				65,
				153,
				45,
				15,
				176,
				84,
				187,
				22
			}
		};
	}

	// Token: 0x06003610 RID: 13840 RVA: 0x0032C878 File Offset: 0x0032B878
	private void ᜈ()
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
		this.ᜅ = new byte[,]
		{
			{
				82,
				9,
				106,
				213,
				48,
				54,
				165,
				56,
				191,
				64,
				163,
				158,
				129,
				243,
				215,
				251
			},
			{
				124,
				227,
				57,
				130,
				155,
				47,
				byte.MaxValue,
				135,
				52,
				142,
				67,
				68,
				196,
				222,
				233,
				203
			},
			{
				84,
				123,
				148,
				50,
				166,
				194,
				35,
				61,
				238,
				76,
				149,
				11,
				66,
				250,
				195,
				78
			},
			{
				8,
				46,
				161,
				102,
				40,
				217,
				36,
				178,
				118,
				91,
				162,
				73,
				109,
				139,
				209,
				37
			},
			{
				114,
				248,
				246,
				100,
				134,
				104,
				152,
				22,
				212,
				164,
				92,
				204,
				93,
				101,
				182,
				146
			},
			{
				108,
				112,
				72,
				80,
				253,
				237,
				185,
				218,
				94,
				21,
				70,
				87,
				167,
				141,
				157,
				132
			},
			{
				144,
				216,
				171,
				0,
				140,
				188,
				211,
				10,
				247,
				228,
				88,
				5,
				184,
				179,
				69,
				6
			},
			{
				208,
				44,
				30,
				143,
				202,
				63,
				15,
				2,
				193,
				175,
				189,
				3,
				1,
				19,
				138,
				107
			},
			{
				58,
				145,
				17,
				65,
				79,
				103,
				220,
				234,
				151,
				242,
				207,
				206,
				240,
				180,
				230,
				115
			},
			{
				150,
				172,
				116,
				34,
				231,
				173,
				53,
				133,
				226,
				249,
				55,
				232,
				28,
				117,
				223,
				110
			},
			{
				71,
				241,
				26,
				113,
				29,
				41,
				197,
				137,
				111,
				183,
				98,
				14,
				170,
				24,
				190,
				27
			},
			{
				252,
				86,
				62,
				75,
				198,
				210,
				121,
				32,
				154,
				219,
				192,
				254,
				120,
				205,
				90,
				244
			},
			{
				31,
				221,
				168,
				51,
				136,
				7,
				199,
				49,
				177,
				18,
				16,
				89,
				39,
				128,
				236,
				95
			},
			{
				96,
				81,
				127,
				169,
				25,
				181,
				74,
				13,
				45,
				229,
				122,
				159,
				147,
				201,
				156,
				239
			},
			{
				160,
				224,
				59,
				77,
				174,
				42,
				245,
				176,
				200,
				235,
				187,
				60,
				131,
				83,
				153,
				97
			},
			{
				23,
				43,
				4,
				126,
				186,
				119,
				214,
				38,
				225,
				105,
				20,
				99,
				85,
				33,
				12,
				125
			}
		};
	}

	// Token: 0x06003611 RID: 13841 RVA: 0x0032C8D0 File Offset: 0x0032B8D0
	private void ᜇ()
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
		this.ᜇ = new byte[,]
		{
			{
				0,
				0,
				0,
				0
			},
			{
				1,
				0,
				0,
				0
			},
			{
				2,
				0,
				0,
				0
			},
			{
				4,
				0,
				0,
				0
			},
			{
				8,
				0,
				0,
				0
			},
			{
				16,
				0,
				0,
				0
			},
			{
				32,
				0,
				0,
				0
			},
			{
				64,
				0,
				0,
				0
			},
			{
				128,
				0,
				0,
				0
			},
			{
				27,
				0,
				0,
				0
			},
			{
				54,
				0,
				0,
				0
			}
		};
	}

	// Token: 0x06003612 RID: 13842 RVA: 0x0032C924 File Offset: 0x0032B924
	private void ᜀ(int A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_64;
				case 1:
					goto IL_42;
				case 2:
					return;
				case 3:
					goto IL_34;
				case 4:
				{
					int num3;
					if (num3 >= 4)
					{
						num2 = 3;
						continue;
					}
					this.ᜈ[num, num3] = (this.ᜈ[num, num3] ^ this.ᜆ[A_0 * 4 + num3, num]);
					num3++;
					num2 = 7;
					continue;
				}
				case 5:
				{
					if (num >= 4)
					{
						num2 = 2;
						continue;
					}
					int num3 = 0;
					num2 = 1;
					continue;
				}
				case 6:
					goto IL_64;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						goto IL_42;
					}
					break;
				}
				break;
				IL_34:
				num++;
				num2 = 0;
				continue;
				IL_42:
				num2 = 4;
				continue;
				IL_64:
				if (true)
				{
				}
				num2 = 5;
			}
		}
	}

	// Token: 0x06003613 RID: 13843 RVA: 0x0032CA1C File Offset: 0x0032BA1C
	private void ᜆ()
	{
		for (;;)
		{
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_6C;
				case 1:
					goto IL_4A;
				case 2:
					goto IL_34;
				case 3:
				{
					if (num >= 4)
					{
						num2 = 4;
						continue;
					}
					int num3 = 0;
					num2 = 1;
					continue;
				}
				case 4:
					return;
				case 5:
				{
					int num3;
					if (num3 >= 4)
					{
						num2 = 2;
						continue;
					}
					this.ᜈ[num, num3] = this.ᜄ[this.ᜈ[num, num3] >> 4, (int)(this.ᜈ[num, num3] & 15)];
					num3++;
					num2 = 7;
					continue;
				}
				case 6:
					goto IL_6C;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						goto IL_4A;
					}
					break;
				}
				break;
				IL_34:
				num++;
				if (true)
				{
				}
				num2 = 6;
				continue;
				IL_4A:
				num2 = 5;
				continue;
				IL_6C:
				num2 = 3;
			}
		}
	}

	// Token: 0x06003614 RID: 13844 RVA: 0x0032CB1C File Offset: 0x0032BB1C
	private void ᜅ()
	{
		for (;;)
		{
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num++;
					num2 = 6;
					continue;
				case 1:
					goto IL_8D;
				case 2:
				{
					if (num >= 4)
					{
						if (true)
						{
						}
						num2 = 7;
						continue;
					}
					int num3 = 0;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8D;
					default:
						if (false)
						{
						}
						num2 = 4;
						continue;
					}
					break;
				}
				case 3:
				{
					int num3;
					if (num3 >= 4)
					{
						num2 = 0;
						continue;
					}
					this.ᜈ[num, num3] = this.ᜅ[this.ᜈ[num, num3] >> 4, (int)(this.ᜈ[num, num3] & 15)];
					num3++;
					num2 = 5;
					continue;
				}
				case 4:
					goto IL_4C;
				case 5:
					goto IL_4C;
				case 6:
					goto IL_8D;
				case 7:
					return;
				}
				break;
				IL_4C:
				num2 = 3;
				continue;
				IL_8D:
				num2 = 2;
			}
		}
	}

	// Token: 0x06003615 RID: 13845 RVA: 0x0032CC28 File Offset: 0x0032BC28
	private void ᜄ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_57:
				if (true)
				{
				}
				byte[,] array = new byte[4, 4];
				int num = 0;
				for (;;)
				{
					IL_69:
					int num2 = 11;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							int num3;
							num3++;
							num2 = 5;
							continue;
						}
						case 1:
						{
							int num4;
							if (num4 >= 4)
							{
								num2 = 0;
								continue;
							}
							int num3;
							this.ᜈ[num3, num4] = array[num3, (num4 + num3) % this.ᜀ];
							num4++;
							num2 = 2;
							continue;
						}
						case 2:
							goto IL_111;
						case 3:
							goto IL_133;
						case 4:
						{
							if (num >= 4)
							{
								num2 = 6;
								continue;
							}
							int num5 = 0;
							num2 = 12;
							continue;
						}
						case 5:
							goto IL_133;
						case 6:
						{
							int num3 = 1;
							num2 = 3;
							continue;
						}
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_69;
							default:
								if (false)
								{
								}
								goto IL_154;
							}
							break;
						case 8:
							goto IL_175;
						case 9:
							goto IL_111;
						case 10:
							return;
						case 11:
							goto IL_154;
						case 12:
							goto IL_175;
						case 13:
						{
							int num5;
							if (num5 >= 4)
							{
								num2 = 14;
								continue;
							}
							array[num, num5] = this.ᜈ[num, num5];
							num5++;
							num2 = 8;
							continue;
						}
						case 14:
							num++;
							num2 = 7;
							continue;
						case 15:
						{
							int num3;
							if (num3 >= 4)
							{
								num2 = 10;
								continue;
							}
							int num4 = 0;
							num2 = 9;
							continue;
						}
						}
						goto IL_57;
						IL_111:
						num2 = 1;
						continue;
						IL_133:
						num2 = 15;
						continue;
						IL_154:
						num2 = 4;
						continue;
						IL_175:
						num2 = 13;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06003616 RID: 13846 RVA: 0x0032CE10 File Offset: 0x0032BE10
	private void ᜃ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_57:
				byte[,] array = new byte[4, 4];
				int num = 0;
				for (;;)
				{
					IL_61:
					int num2 = 14;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_111;
						case 1:
						{
							if (true)
							{
							}
							int num3;
							num3++;
							num2 = 13;
							continue;
						}
						case 2:
						{
							int num4;
							if (num4 >= 4)
							{
								num2 = 10;
								continue;
							}
							array[num, num4] = this.ᜈ[num, num4];
							num4++;
							num2 = 5;
							continue;
						}
						case 3:
						{
							int num5;
							if (num5 >= 4)
							{
								num2 = 1;
								continue;
							}
							int num3;
							this.ᜈ[num3, (num5 + num3) % this.ᜀ] = array[num3, num5];
							num5++;
							num2 = 9;
							continue;
						}
						case 4:
							goto IL_175;
						case 5:
							goto IL_175;
						case 6:
						{
							int num3;
							if (num3 >= 4)
							{
								num2 = 15;
								continue;
							}
							int num5 = 0;
							num2 = 0;
							continue;
						}
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_61;
							default:
								if (false)
								{
								}
								goto IL_154;
							}
							break;
						case 8:
						{
							if (num >= 4)
							{
								num2 = 12;
								continue;
							}
							int num4 = 0;
							num2 = 4;
							continue;
						}
						case 9:
							goto IL_111;
						case 10:
							num++;
							num2 = 7;
							continue;
						case 11:
							goto IL_133;
						case 12:
						{
							int num3 = 1;
							num2 = 11;
							continue;
						}
						case 13:
							goto IL_133;
						case 14:
							goto IL_154;
						case 15:
							return;
						}
						goto IL_57;
						IL_111:
						num2 = 3;
						continue;
						IL_133:
						num2 = 6;
						continue;
						IL_154:
						num2 = 8;
						continue;
						IL_175:
						num2 = 2;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06003617 RID: 13847 RVA: 0x0032CFF8 File Offset: 0x0032BFF8
	private void ᜂ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[,] array = new byte[4, 4];
				int num = 0;
				int num2 = 9;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_254;
					case 1:
					{
						int num3;
						if (num3 >= 4)
						{
							num2 = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23F;
						default:
							if (false)
							{
							}
							array[num, num3] = this.ᜈ[num, num3];
							num3++;
							num2 = 11;
							continue;
						}
						break;
					}
					case 2:
					{
						int num4;
						if (num4 >= 4)
						{
							num2 = 7;
							continue;
						}
						this.ᜈ[0, num4] = (spr\u21ED.ᜅ(array[0, num4]) ^ spr\u21ED.ᜄ(array[1, num4]) ^ spr\u21ED.ᜆ(array[2, num4]) ^ spr\u21ED.ᜆ(array[3, num4]));
						this.ᜈ[1, num4] = (spr\u21ED.ᜆ(array[0, num4]) ^ spr\u21ED.ᜅ(array[1, num4]) ^ spr\u21ED.ᜄ(array[2, num4]) ^ spr\u21ED.ᜆ(array[3, num4]));
						this.ᜈ[2, num4] = (spr\u21ED.ᜆ(array[0, num4]) ^ spr\u21ED.ᜆ(array[1, num4]) ^ spr\u21ED.ᜅ(array[2, num4]) ^ spr\u21ED.ᜄ(array[3, num4]));
						this.ᜈ[3, num4] = (spr\u21ED.ᜄ(array[0, num4]) ^ spr\u21ED.ᜆ(array[1, num4]) ^ spr\u21ED.ᜆ(array[2, num4]) ^ spr\u21ED.ᜅ(array[3, num4]));
						num4++;
						num2 = 5;
						continue;
					}
					case 3:
					{
						int num4 = 0;
						num2 = 4;
						continue;
					}
					case 4:
						goto IL_DF;
					case 5:
						goto IL_DF;
					case 6:
					{
						if (num >= 4)
						{
							num2 = 3;
							continue;
						}
						int num3 = 0;
						num2 = 0;
						continue;
					}
					case 7:
						return;
					case 8:
						goto IL_23F;
					case 9:
						goto IL_6C;
					case 10:
						goto IL_6C;
					case 11:
						goto IL_254;
					}
					break;
					IL_6C:
					if (true)
					{
					}
					num2 = 6;
					continue;
					IL_DF:
					num2 = 2;
					continue;
					IL_23F:
					num++;
					num2 = 10;
					continue;
					IL_254:
					num2 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06003618 RID: 13848 RVA: 0x0032D27C File Offset: 0x0032C27C
	private void ᜁ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[,] array = new byte[4, 4];
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_E2;
					case 1:
					{
						int num3 = 0;
						num2 = 10;
						continue;
					}
					case 2:
						goto IL_257;
					case 3:
						return;
					case 4:
						goto IL_257;
					case 5:
						goto IL_242;
					case 6:
						goto IL_6C;
					case 7:
						goto IL_6C;
					case 8:
					{
						if (num >= 4)
						{
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						int num4 = 0;
						num2 = 2;
						continue;
					}
					case 9:
					{
						int num4;
						if (num4 >= 4)
						{
							num2 = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_242;
						default:
							if (false)
							{
							}
							array[num, num4] = this.ᜈ[num, num4];
							num4++;
							num2 = 4;
							continue;
						}
						break;
					}
					case 10:
						goto IL_E2;
					case 11:
					{
						int num3;
						if (num3 >= 4)
						{
							num2 = 3;
							continue;
						}
						this.ᜈ[0, num3] = (spr\u21ED.ᜀ(array[0, num3]) ^ spr\u21ED.ᜂ(array[1, num3]) ^ spr\u21ED.ᜁ(array[2, num3]) ^ spr\u21ED.ᜃ(array[3, num3]));
						this.ᜈ[1, num3] = (spr\u21ED.ᜃ(array[0, num3]) ^ spr\u21ED.ᜀ(array[1, num3]) ^ spr\u21ED.ᜂ(array[2, num3]) ^ spr\u21ED.ᜁ(array[3, num3]));
						this.ᜈ[2, num3] = (spr\u21ED.ᜁ(array[0, num3]) ^ spr\u21ED.ᜃ(array[1, num3]) ^ spr\u21ED.ᜀ(array[2, num3]) ^ spr\u21ED.ᜂ(array[3, num3]));
						this.ᜈ[3, num3] = (spr\u21ED.ᜂ(array[0, num3]) ^ spr\u21ED.ᜁ(array[1, num3]) ^ spr\u21ED.ᜃ(array[2, num3]) ^ spr\u21ED.ᜀ(array[3, num3]));
						num3++;
						num2 = 0;
						continue;
					}
					}
					break;
					IL_6C:
					num2 = 8;
					continue;
					IL_E2:
					num2 = 11;
					continue;
					IL_242:
					num++;
					num2 = 7;
					continue;
					IL_257:
					num2 = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06003619 RID: 13849 RVA: 0x0032D504 File Offset: 0x0032C504
	private static byte ᜆ(byte A_0)
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
		return A_0;
	}

	// Token: 0x0600361A RID: 13850 RVA: 0x0032D540 File Offset: 0x0032C540
	private static byte ᜅ(byte A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			if (A_0 >= 128)
			{
				goto IL_3D;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_28;
			}
		}
		IL_28:
		if (false)
		{
		}
		return (byte)(A_0 << 1);
		IL_3D:
		return (byte)((int)A_0 << 1 ^ 27);
	}

	// Token: 0x0600361B RID: 13851 RVA: 0x0032D594 File Offset: 0x0032C594
	private static byte ᜄ(byte A_0)
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
		return spr\u21ED.ᜅ(A_0) ^ A_0;
	}

	// Token: 0x0600361C RID: 13852 RVA: 0x0032D5D8 File Offset: 0x0032C5D8
	private static byte ᜃ(byte A_0)
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
		return spr\u21ED.ᜅ(spr\u21ED.ᜅ(spr\u21ED.ᜅ(A_0))) ^ A_0;
	}

	// Token: 0x0600361D RID: 13853 RVA: 0x0032D628 File Offset: 0x0032C628
	private static byte ᜂ(byte A_0)
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
		return spr\u21ED.ᜅ(spr\u21ED.ᜅ(spr\u21ED.ᜅ(A_0))) ^ spr\u21ED.ᜅ(A_0) ^ A_0;
	}

	// Token: 0x0600361E RID: 13854 RVA: 0x0032D680 File Offset: 0x0032C680
	private static byte ᜁ(byte A_0)
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
		return spr\u21ED.ᜅ(spr\u21ED.ᜅ(spr\u21ED.ᜅ(A_0))) ^ spr\u21ED.ᜅ(spr\u21ED.ᜅ(A_0)) ^ A_0;
	}

	// Token: 0x0600361F RID: 13855 RVA: 0x0032D6DC File Offset: 0x0032C6DC
	private static byte ᜀ(byte A_0)
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
		return spr\u21ED.ᜅ(spr\u21ED.ᜅ(spr\u21ED.ᜅ(A_0))) ^ spr\u21ED.ᜅ(spr\u21ED.ᜅ(A_0)) ^ spr\u21ED.ᜅ(A_0);
	}

	// Token: 0x06003620 RID: 13856 RVA: 0x0032D73C File Offset: 0x0032C73C
	private void ᜀ()
	{
		for (;;)
		{
			this.ᜆ = new byte[this.ᜀ * (this.ᜂ + 1), 4];
			int num = 0;
			int num2 = 14;
			for (;;)
			{
				byte[] array;
				int num3;
				switch (num2)
				{
				case 0:
					num2 = 7;
					continue;
				case 1:
					goto IL_221;
				case 2:
					if (num >= this.ᜁ)
					{
						num2 = 4;
						continue;
					}
					this.ᜆ[num, 0] = this.ᜃ[4 * num];
					this.ᜆ[num, 1] = this.ᜃ[4 * num + 1];
					this.ᜆ[num, 2] = this.ᜃ[4 * num + 2];
					this.ᜆ[num, 3] = this.ᜃ[4 * num + 3];
					num++;
					num2 = 8;
					continue;
				case 3:
					return;
				case 4:
					array = new byte[4];
					num3 = this.ᜁ;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 11;
						continue;
					}
					break;
				case 5:
					if (this.ᜁ > 6)
					{
						num2 = 0;
						continue;
					}
					goto IL_CC;
				case 6:
					if (num3 % this.ᜁ == 0)
					{
						num2 = 12;
						continue;
					}
					num2 = 5;
					continue;
				case 7:
					if (num3 % this.ᜁ == 4)
					{
						num2 = 15;
						continue;
					}
					goto IL_CC;
				case 8:
					goto IL_A5;
				case 9:
					if (num3 >= this.ᜀ * (this.ᜂ + 1))
					{
						num2 = 3;
						continue;
					}
					array[0] = this.ᜆ[num3 - 1, 0];
					array[1] = this.ᜆ[num3 - 1, 1];
					array[2] = this.ᜆ[num3 - 1, 2];
					array[3] = this.ᜆ[num3 - 1, 3];
					num2 = 6;
					continue;
				case 10:
					goto IL_CC;
				case 11:
					goto IL_221;
				case 12:
					array = this.ᜁ(this.ᜀ(array));
					array[0] = (array[0] ^ this.ᜇ[num3 / this.ᜁ, 0]);
					array[1] = (array[1] ^ this.ᜇ[num3 / this.ᜁ, 1]);
					array[2] = (array[2] ^ this.ᜇ[num3 / this.ᜁ, 2]);
					array[3] = (array[3] ^ this.ᜇ[num3 / this.ᜁ, 3]);
					num2 = 10;
					continue;
				case 13:
					goto IL_CC;
				case 14:
					goto IL_A5;
				case 15:
					array = this.ᜁ(array);
					num2 = 13;
					continue;
				}
				break;
				IL_A5:
				num2 = 2;
				continue;
				IL_CC:
				this.ᜆ[num3, 0] = (this.ᜆ[num3 - this.ᜁ, 0] ^ array[0]);
				this.ᜆ[num3, 1] = (this.ᜆ[num3 - this.ᜁ, 1] ^ array[1]);
				this.ᜆ[num3, 2] = (this.ᜆ[num3 - this.ᜁ, 2] ^ array[2]);
				this.ᜆ[num3, 3] = (this.ᜆ[num3 - this.ᜁ, 3] ^ array[3]);
				num3++;
				num2 = 1;
				continue;
				IL_221:
				num2 = 9;
			}
		}
	}

	// Token: 0x06003621 RID: 13857 RVA: 0x0032DAD8 File Offset: 0x0032CAD8
	private byte[] ᜁ(byte[] A_0)
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
		return new byte[]
		{
			this.ᜄ[A_0[0] >> 4, (int)(A_0[0] & 15)],
			this.ᜄ[A_0[1] >> 4, (int)(A_0[1] & 15)],
			this.ᜄ[A_0[2] >> 4, (int)(A_0[2] & 15)],
			this.ᜄ[A_0[3] >> 4, (int)(A_0[3] & 15)]
		};
	}

	// Token: 0x06003622 RID: 13858 RVA: 0x0032DB80 File Offset: 0x0032CB80
	private byte[] ᜀ(byte[] A_0)
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
		return new byte[]
		{
			A_0[1],
			A_0[2],
			A_0[3],
			A_0[0]
		};
	}

	// Token: 0x0400296B RID: 10603
	private int ᜀ;

	// Token: 0x0400296C RID: 10604
	private int ᜁ;

	// Token: 0x0400296D RID: 10605
	private int ᜂ;

	// Token: 0x0400296E RID: 10606
	private byte[] ᜃ;

	// Token: 0x0400296F RID: 10607
	private byte[,] ᜄ;

	// Token: 0x04002970 RID: 10608
	private byte[,] ᜅ;

	// Token: 0x04002971 RID: 10609
	private byte[,] ᜆ;

	// Token: 0x04002972 RID: 10610
	private byte[,] ᜇ;

	// Token: 0x04002973 RID: 10611
	private byte[,] ᜈ;

	// Token: 0x04002974 RID: 10612
	private spr\u21ED.KeySize ᜉ;

	// Token: 0x020003BD RID: 957
	internal enum KeySize
	{
		// Token: 0x04002976 RID: 10614
		Bits128,
		// Token: 0x04002977 RID: 10615
		Bits192,
		// Token: 0x04002978 RID: 10616
		Bits256
	}
}
