using System;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000524 RID: 1316
internal class spr\u1C4C
{
	// Token: 0x0600506F RID: 20591 RVA: 0x003274F4 File Offset: 0x003264F4
	public spr\u1C4C(spr\u1C4C.KeySize A_0, byte[] A_1)
	{
		this.ᜉ = A_0;
		this.ᜀ(A_0);
		this.ᜃ = new byte[this.ᜁ * 4];
		A_1.CopyTo(this.ᜃ, 0);
		this.ᜊ();
	}

	// Token: 0x06005070 RID: 20592 RVA: 0x0032753C File Offset: 0x0032653C
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

	// Token: 0x06005071 RID: 20593 RVA: 0x00327590 File Offset: 0x00326590
	public void ᜁ(byte[] A_0, byte[] A_1)
	{
		for (;;)
		{
			this.ᜊ();
			this.ᜈ = new byte[4, this.ᜀ];
			int num = 0;
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_D4;
				case 1:
					goto IL_AE;
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
					num2 = 0;
					continue;
				}
				case 3:
					if (num >= 4 * this.ᜀ)
					{
						num2 = 4;
						continue;
					}
					this.ᜈ[num % 4, num / 4] = A_0[num];
					num++;
					num2 = 1;
					continue;
				case 4:
				{
					this.ᜀ(0);
					int num4 = 1;
					num2 = 10;
					continue;
				}
				case 5:
					goto IL_D4;
				case 6:
					goto IL_AE;
				case 7:
				{
					this.ᜆ();
					this.ᜄ();
					this.ᜀ(this.ᜂ);
					int num3 = 0;
					num2 = 5;
					continue;
				}
				case 8:
					return;
				case 9:
				{
					int num4;
					if (num4 > this.ᜂ - 1)
					{
						if (true)
						{
						}
						num2 = 7;
						continue;
					}
					this.ᜆ();
					this.ᜄ();
					this.ᜂ();
					this.ᜀ(num4);
					num4++;
					num2 = 11;
					continue;
				}
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_176;
					default:
						if (false)
						{
						}
						goto IL_80;
					}
					break;
				case 11:
					goto IL_176;
				}
				break;
				IL_80:
				num2 = 9;
				continue;
				IL_176:
				goto IL_80;
				IL_AE:
				num2 = 3;
				continue;
				IL_D4:
				num2 = 2;
			}
		}
	}

	// Token: 0x06005072 RID: 20594 RVA: 0x00327750 File Offset: 0x00326750
	public void ᜀ(byte[] A_0, byte[] A_1)
	{
		for (;;)
		{
			this.ᜈ = new byte[4, this.ᜀ];
			int num = 0;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					this.ᜃ();
					this.ᜅ();
					this.ᜀ(0);
					int num3 = 0;
					num2 = 6;
					continue;
				}
				case 1:
				{
					int num4;
					if (num4 < 1)
					{
						num2 = 0;
						continue;
					}
					this.ᜃ();
					this.ᜅ();
					this.ᜀ(num4);
					this.ᜁ();
					num4--;
					num2 = 3;
					continue;
				}
				case 2:
					return;
				case 3:
					goto IL_159;
				case 4:
					goto IL_B7;
				case 5:
					goto IL_91;
				case 6:
					goto IL_B7;
				case 7:
				{
					if (true)
					{
					}
					this.ᜀ(this.ᜂ);
					int num4 = this.ᜂ - 1;
					num2 = 8;
					continue;
				}
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_159;
					default:
						if (false)
						{
						}
						goto IL_75;
					}
					break;
				case 9:
					if (num >= 4 * this.ᜀ)
					{
						num2 = 7;
						continue;
					}
					this.ᜈ[num % 4, num / 4] = A_0[num];
					num++;
					num2 = 10;
					continue;
				case 10:
					goto IL_91;
				case 11:
				{
					int num3;
					if (num3 >= 4 * this.ᜀ)
					{
						num2 = 2;
						continue;
					}
					A_1[num3] = this.ᜈ[num3 % 4, num3 / 4];
					num3++;
					num2 = 4;
					continue;
				}
				}
				break;
				IL_75:
				num2 = 1;
				continue;
				IL_159:
				goto IL_75;
				IL_91:
				num2 = 9;
				continue;
				IL_B7:
				num2 = 11;
			}
		}
	}

	// Token: 0x06005073 RID: 20595 RVA: 0x00327904 File Offset: 0x00326904
	private void ᜀ(spr\u1C4C.KeySize A_0)
	{
		for (;;)
		{
			this.ᜀ = 4;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3E;
				case 1:
					if (A_0 == spr\u1C4C.KeySize.Bits256)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						this.ᜁ = 8;
						this.ᜂ = 14;
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_AF;
				case 4:
					goto IL_D6;
				case 5:
					if (true)
					{
					}
					if (A_0 == spr\u1C4C.KeySize.Bits192)
					{
						num = 4;
						continue;
					}
					num = 1;
					continue;
				case 6:
					if (A_0 == spr\u1C4C.KeySize.Bits128)
					{
						num = 0;
						continue;
					}
					num = 5;
					continue;
				}
				break;
			}
		}
		IL_3E:
		this.ᜁ = 4;
		this.ᜂ = 10;
		return;
		IL_AF:
		return;
		IL_D6:
		this.ᜁ = 6;
		this.ᜂ = 12;
	}

	// Token: 0x06005074 RID: 20596 RVA: 0x003279F4 File Offset: 0x003269F4
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

	// Token: 0x06005075 RID: 20597 RVA: 0x00327A4C File Offset: 0x00326A4C
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

	// Token: 0x06005076 RID: 20598 RVA: 0x00327AA4 File Offset: 0x00326AA4
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

	// Token: 0x06005077 RID: 20599 RVA: 0x00327AF8 File Offset: 0x00326AF8
	private void ᜀ(int A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
				{
					if (num >= 4)
					{
						num2 = 0;
						continue;
					}
					int num3 = 0;
					num2 = 6;
					continue;
				}
				case 2:
					num++;
					num2 = 5;
					continue;
				case 3:
				{
					int num3;
					if (num3 >= 4)
					{
						num2 = 2;
						continue;
					}
					this.ᜈ[num, num3] = (this.ᜈ[num, num3] ^ this.ᜆ[A_0 * 4 + num3, num]);
					num3++;
					num2 = 7;
					continue;
				}
				case 4:
					goto IL_88;
				case 5:
					goto IL_88;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_42;
					}
					break;
				case 7:
					goto IL_42;
				}
				break;
				IL_42:
				num2 = 3;
				continue;
				IL_88:
				num2 = 1;
			}
		}
	}

	// Token: 0x06005078 RID: 20600 RVA: 0x00327BF4 File Offset: 0x00326BF4
	private void ᜆ()
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
					goto IL_4A;
				case 1:
					goto IL_88;
				case 2:
				{
					int num3;
					if (num3 >= 4)
					{
						num2 = 7;
						continue;
					}
					this.ᜈ[num, num3] = this.ᜄ[this.ᜈ[num, num3] >> 4, (int)(this.ᜈ[num, num3] & 15)];
					num3++;
					num2 = 0;
					continue;
				}
				case 3:
				{
					if (num >= 4)
					{
						num2 = 4;
						continue;
					}
					int num3 = 0;
					num2 = 6;
					continue;
				}
				case 4:
					return;
				case 5:
					goto IL_88;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						goto IL_4A;
					}
					break;
				case 7:
					num++;
					if (true)
					{
					}
					num2 = 5;
					continue;
				}
				break;
				IL_4A:
				num2 = 2;
				continue;
				IL_88:
				num2 = 3;
			}
		}
	}

	// Token: 0x06005079 RID: 20601 RVA: 0x00327CFC File Offset: 0x00326CFC
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
				{
					int num3;
					if (num3 >= 4)
					{
						num2 = 5;
						continue;
					}
					this.ᜈ[num, num3] = this.ᜅ[this.ᜈ[num, num3] >> 4, (int)(this.ᜈ[num, num3] & 15)];
					num3++;
					num2 = 2;
					continue;
				}
				case 1:
					goto IL_80;
				case 2:
					goto IL_42;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_80;
					default:
						if (false)
						{
						}
						goto IL_42;
					}
					break;
				case 4:
					return;
				case 5:
					num++;
					num2 = 7;
					continue;
				case 6:
				{
					if (num >= 4)
					{
						if (true)
						{
						}
						num2 = 4;
						continue;
					}
					int num3 = 0;
					num2 = 3;
					continue;
				}
				case 7:
					goto IL_80;
				}
				break;
				IL_42:
				num2 = 0;
				continue;
				IL_80:
				num2 = 6;
			}
		}
	}

	// Token: 0x0600507A RID: 20602 RVA: 0x00327E04 File Offset: 0x00326E04
	private void ᜄ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				byte[,] array = new byte[4, 4];
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						num3++;
						num2 = 11;
						continue;
					}
					case 1:
						goto IL_16B;
					case 2:
						goto IL_14A;
					case 3:
						goto IL_14A;
					case 4:
						goto IL_16B;
					case 5:
					{
						if (num >= 4)
						{
							num2 = 15;
							continue;
						}
						int num4 = 0;
						num2 = 1;
						continue;
					}
					case 6:
						goto IL_129;
					case 7:
						num++;
						num2 = 3;
						continue;
					case 8:
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
							int num4;
							if (num4 >= 4)
							{
								num2 = 7;
								continue;
							}
							array[num, num4] = this.ᜈ[num, num4];
							num4++;
							break;
						}
						}
						num2 = 4;
						continue;
					case 9:
						goto IL_107;
					case 10:
						return;
					case 11:
						goto IL_129;
					case 12:
						goto IL_107;
					case 13:
					{
						int num5;
						if (num5 >= 4)
						{
							num2 = 0;
							continue;
						}
						int num3;
						this.ᜈ[num3, num5] = array[num3, (num5 + num3) % this.ᜀ];
						num5++;
						num2 = 12;
						continue;
					}
					case 14:
					{
						int num3;
						if (num3 >= 4)
						{
							num2 = 10;
							continue;
						}
						int num5 = 0;
						num2 = 9;
						continue;
					}
					case 15:
					{
						int num3 = 1;
						num2 = 6;
						continue;
					}
					}
					break;
					IL_107:
					num2 = 13;
					continue;
					IL_129:
					num2 = 14;
					continue;
					IL_14A:
					num2 = 5;
					continue;
					IL_16B:
					num2 = 8;
				}
			}
			return;
		}
	}

	// Token: 0x0600507B RID: 20603 RVA: 0x00327FEC File Offset: 0x00326FEC
	private void ᜃ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[,] array = new byte[4, 4];
				int num = 0;
				int num2 = 14;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_16B;
					case 1:
					{
						int num3;
						if (num3 >= 4)
						{
							num2 = 2;
							continue;
						}
						int num4;
						this.ᜈ[num4, (num3 + num4) % this.ᜀ] = array[num4, num3];
						num3++;
						num2 = 9;
						continue;
					}
					case 2:
					{
						if (true)
						{
						}
						int num4;
						num4++;
						num2 = 7;
						continue;
					}
					case 3:
						goto IL_129;
					case 4:
					{
						int num4 = 1;
						num2 = 3;
						continue;
					}
					case 5:
						goto IL_14A;
					case 6:
					{
						if (num >= 4)
						{
							num2 = 4;
							continue;
						}
						int num5 = 0;
						num2 = 10;
						continue;
					}
					case 7:
						goto IL_129;
					case 8:
					{
						int num4;
						if (num4 >= 4)
						{
							num2 = 15;
							continue;
						}
						int num3 = 0;
						num2 = 13;
						continue;
					}
					case 9:
						goto IL_107;
					case 10:
						goto IL_16B;
					case 11:
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
							int num5;
							if (num5 >= 4)
							{
								num2 = 12;
								continue;
							}
							array[num, num5] = this.ᜈ[num, num5];
							num5++;
							break;
						}
						}
						num2 = 0;
						continue;
					case 12:
						num++;
						num2 = 5;
						continue;
					case 13:
						goto IL_107;
					case 14:
						goto IL_14A;
					case 15:
						return;
					}
					break;
					IL_107:
					num2 = 1;
					continue;
					IL_129:
					num2 = 8;
					continue;
					IL_14A:
					num2 = 6;
					continue;
					IL_16B:
					num2 = 11;
				}
			}
			return;
		}
	}

	// Token: 0x0600507C RID: 20604 RVA: 0x003281D4 File Offset: 0x003271D4
	private void ᜂ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_63:
				byte[,] array = new byte[4, 4];
				int num = 0;
				int num2 = 10;
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
						{
							int num3;
							if (num3 >= 4)
							{
								goto IL_26D;
							}
							array[num, num3] = this.ᜈ[num, num3];
							num3++;
							num2 = 11;
							continue;
						}
						case 1:
							goto IL_9D;
						case 2:
							goto IL_E5;
						case 3:
							return;
						case 4:
						{
							if (num >= 4)
							{
								num2 = 6;
								continue;
							}
							int num3 = 0;
							num2 = 5;
							continue;
						}
						case 5:
							goto IL_25A;
						case 6:
						{
							if (true)
							{
							}
							int num4 = 0;
							num2 = 2;
							continue;
						}
						case 7:
							goto IL_E5;
						case 8:
						{
							int num4;
							if (num4 >= 4)
							{
								num2 = 3;
								continue;
							}
							this.ᜈ[0, num4] = (spr\u1C4C.ᜅ(array[0, num4]) ^ spr\u1C4C.ᜄ(array[1, num4]) ^ spr\u1C4C.ᜆ(array[2, num4]) ^ spr\u1C4C.ᜆ(array[3, num4]));
							this.ᜈ[1, num4] = (spr\u1C4C.ᜆ(array[0, num4]) ^ spr\u1C4C.ᜅ(array[1, num4]) ^ spr\u1C4C.ᜄ(array[2, num4]) ^ spr\u1C4C.ᜆ(array[3, num4]));
							this.ᜈ[2, num4] = (spr\u1C4C.ᜆ(array[0, num4]) ^ spr\u1C4C.ᜆ(array[1, num4]) ^ spr\u1C4C.ᜅ(array[2, num4]) ^ spr\u1C4C.ᜄ(array[3, num4]));
							this.ᜈ[3, num4] = (spr\u1C4C.ᜄ(array[0, num4]) ^ spr\u1C4C.ᜆ(array[1, num4]) ^ spr\u1C4C.ᜆ(array[2, num4]) ^ spr\u1C4C.ᜅ(array[3, num4]));
							num4++;
							num2 = 7;
							continue;
						}
						case 9:
							num++;
							num2 = 1;
							continue;
						case 10:
							goto IL_9D;
						case 11:
							goto IL_25A;
						}
						goto IL_63;
						IL_9D:
						num2 = 4;
						continue;
						IL_E5:
						num2 = 8;
						continue;
						IL_25A:
						num2 = 0;
						continue;
					}
					IL_26D:
					num2 = 9;
				}
			}
			return;
		}
	}

	// Token: 0x0600507D RID: 20605 RVA: 0x0032845C File Offset: 0x0032745C
	private void ᜁ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_63:
				byte[,] array = new byte[4, 4];
				int num = 0;
				int num2 = 1;
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
							goto IL_257;
						case 1:
							goto IL_9A;
						case 2:
							goto IL_257;
						case 3:
							num++;
							num2 = 9;
							continue;
						case 4:
						{
							if (num >= 4)
							{
								num2 = 5;
								continue;
							}
							int num3 = 0;
							num2 = 0;
							continue;
						}
						case 5:
						{
							int num4 = 0;
							num2 = 10;
							continue;
						}
						case 6:
							return;
						case 7:
						{
							int num3;
							if (num3 >= 4)
							{
								goto IL_26A;
							}
							array[num, num3] = this.ᜈ[num, num3];
							num3++;
							num2 = 2;
							continue;
						}
						case 8:
						{
							int num4;
							if (num4 >= 4)
							{
								num2 = 6;
								continue;
							}
							this.ᜈ[0, num4] = (spr\u1C4C.ᜀ(array[0, num4]) ^ spr\u1C4C.ᜂ(array[1, num4]) ^ spr\u1C4C.ᜁ(array[2, num4]) ^ spr\u1C4C.ᜃ(array[3, num4]));
							this.ᜈ[1, num4] = (spr\u1C4C.ᜃ(array[0, num4]) ^ spr\u1C4C.ᜀ(array[1, num4]) ^ spr\u1C4C.ᜂ(array[2, num4]) ^ spr\u1C4C.ᜁ(array[3, num4]));
							this.ᜈ[2, num4] = (spr\u1C4C.ᜁ(array[0, num4]) ^ spr\u1C4C.ᜃ(array[1, num4]) ^ spr\u1C4C.ᜀ(array[2, num4]) ^ spr\u1C4C.ᜂ(array[3, num4]));
							this.ᜈ[3, num4] = (spr\u1C4C.ᜂ(array[0, num4]) ^ spr\u1C4C.ᜁ(array[1, num4]) ^ spr\u1C4C.ᜃ(array[2, num4]) ^ spr\u1C4C.ᜀ(array[3, num4]));
							num4++;
							num2 = 11;
							continue;
						}
						case 9:
							goto IL_9A;
						case 10:
							if (true)
							{
							}
							goto IL_E2;
						case 11:
							goto IL_E2;
						}
						goto IL_63;
						IL_9A:
						num2 = 4;
						continue;
						IL_E2:
						num2 = 8;
						continue;
						IL_257:
						num2 = 7;
						continue;
					}
					IL_26A:
					num2 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x0600507E RID: 20606 RVA: 0x003286E4 File Offset: 0x003276E4
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

	// Token: 0x0600507F RID: 20607 RVA: 0x00328720 File Offset: 0x00327720
	private static byte ᜅ(byte A_0)
	{
		if (true)
		{
		}
		if (A_0 < 128)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (false)
			{
			}
			return (byte)(A_0 << 1);
		}
		return (byte)((int)A_0 << 1 ^ 27);
	}

	// Token: 0x06005080 RID: 20608 RVA: 0x00328774 File Offset: 0x00327774
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
		return spr\u1C4C.ᜅ(A_0) ^ A_0;
	}

	// Token: 0x06005081 RID: 20609 RVA: 0x003287B8 File Offset: 0x003277B8
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
		return spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(A_0))) ^ A_0;
	}

	// Token: 0x06005082 RID: 20610 RVA: 0x00328808 File Offset: 0x00327808
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
		return spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(A_0))) ^ spr\u1C4C.ᜅ(A_0) ^ A_0;
	}

	// Token: 0x06005083 RID: 20611 RVA: 0x00328860 File Offset: 0x00327860
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
		return spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(A_0))) ^ spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(A_0)) ^ A_0;
	}

	// Token: 0x06005084 RID: 20612 RVA: 0x003288BC File Offset: 0x003278BC
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
		return spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(A_0))) ^ spr\u1C4C.ᜅ(spr\u1C4C.ᜅ(A_0)) ^ spr\u1C4C.ᜅ(A_0);
	}

	// Token: 0x06005085 RID: 20613 RVA: 0x0032891C File Offset: 0x0032791C
	private void ᜀ()
	{
		for (;;)
		{
			this.ᜆ = new byte[this.ᜀ * (this.ᜂ + 1), 4];
			int num = 0;
			int num2 = 8;
			for (;;)
			{
				int num3;
				byte[] array;
				switch (num2)
				{
				case 0:
					goto IL_D6;
				case 1:
					goto IL_AF;
				case 2:
					if (this.ᜁ > 6)
					{
						num2 = 4;
						continue;
					}
					goto IL_D6;
				case 3:
					if (num3 >= this.ᜀ * (this.ᜂ + 1))
					{
						num2 = 5;
						continue;
					}
					array[0] = this.ᜆ[num3 - 1, 0];
					array[1] = this.ᜆ[num3 - 1, 1];
					array[2] = this.ᜆ[num3 - 1, 2];
					array[3] = this.ᜆ[num3 - 1, 3];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_221;
					default:
						if (false)
						{
						}
						num2 = 11;
						continue;
					}
					break;
				case 4:
					num2 = 12;
					continue;
				case 5:
					return;
				case 6:
					goto IL_D6;
				case 7:
					array = new byte[4];
					num3 = this.ᜁ;
					num2 = 9;
					continue;
				case 8:
					goto IL_AF;
				case 9:
					goto IL_221;
				case 10:
					if (num >= this.ᜁ)
					{
						num2 = 7;
						continue;
					}
					this.ᜆ[num, 0] = this.ᜃ[4 * num];
					this.ᜆ[num, 1] = this.ᜃ[4 * num + 1];
					this.ᜆ[num, 2] = this.ᜃ[4 * num + 2];
					this.ᜆ[num, 3] = this.ᜃ[4 * num + 3];
					num++;
					num2 = 1;
					continue;
				case 11:
					if (num3 % this.ᜁ == 0)
					{
						num2 = 15;
						continue;
					}
					num2 = 2;
					continue;
				case 12:
					if (num3 % this.ᜁ == 4)
					{
						num2 = 13;
						continue;
					}
					goto IL_D6;
				case 13:
					array = this.ᜁ(array);
					num2 = 6;
					continue;
				case 14:
					goto IL_221;
				case 15:
					if (true)
					{
					}
					array = this.ᜁ(this.ᜀ(array));
					array[0] = (array[0] ^ this.ᜇ[num3 / this.ᜁ, 0]);
					array[1] = (array[1] ^ this.ᜇ[num3 / this.ᜁ, 1]);
					array[2] = (array[2] ^ this.ᜇ[num3 / this.ᜁ, 2]);
					array[3] = (array[3] ^ this.ᜇ[num3 / this.ᜁ, 3]);
					num2 = 0;
					continue;
				}
				break;
				IL_AF:
				num2 = 10;
				continue;
				IL_D6:
				this.ᜆ[num3, 0] = (this.ᜆ[num3 - this.ᜁ, 0] ^ array[0]);
				this.ᜆ[num3, 1] = (this.ᜆ[num3 - this.ᜁ, 1] ^ array[1]);
				this.ᜆ[num3, 2] = (this.ᜆ[num3 - this.ᜁ, 2] ^ array[2]);
				this.ᜆ[num3, 3] = (this.ᜆ[num3 - this.ᜁ, 3] ^ array[3]);
				num3++;
				num2 = 14;
				continue;
				IL_221:
				num2 = 3;
			}
		}
	}

	// Token: 0x06005086 RID: 20614 RVA: 0x00328CB8 File Offset: 0x00327CB8
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

	// Token: 0x06005087 RID: 20615 RVA: 0x00328D60 File Offset: 0x00327D60
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

	// Token: 0x06005088 RID: 20616 RVA: 0x00328DBC File Offset: 0x00327DBC
	public void ᜋ()
	{
		int a_ = 5;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Console.WriteLine(string.Concat(new object[]
		{
			RecordTableEnumerator.b("町弼Ἶ籀捂", a_),
			this.ᜀ,
			RecordTableEnumerator.b("ᬺ猼吾慀繂敄", a_),
			this.ᜁ,
			RecordTableEnumerator.b("ᬺ猼䴾慀繂敄", a_),
			this.ᜂ
		}));
		Console.WriteLine(RecordTableEnumerator.b("ㄺ椼圾⑀捂⹄≆え歊⑌㱎煐奒", a_) + this.ᜌ());
		Console.WriteLine(RecordTableEnumerator.b("ㄺ椼圾⑀捂ᙄ╆♈㍊浌♎≐獒彔", a_) + this.ᜀ(this.ᜄ));
		Console.WriteLine(RecordTableEnumerator.b("ㄺ椼圾⑀捂㉄杆⡈㥊㽌⹎⡐獒㱔⑖祘党", a_) + this.ᜀ(this.ᜆ));
		Console.WriteLine(RecordTableEnumerator.b("ㄺ椼圾⑀捂ᙄ㍆⡈㽊⡌潎ぐ⅒❔㙖⁘筚㑜ⱞ䅠楢", a_) + this.ᜀ(this.ᜈ));
	}

	// Token: 0x06005089 RID: 20617 RVA: 0x00328EFC File Offset: 0x00327EFC
	public string ᜌ()
	{
		int a_ = 16;
		string text;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return text;
			}
			if (false)
			{
			}
			text = "";
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_59;
				case 1:
					goto IL_59;
				case 2:
					if (num >= this.ᜃ.Length)
					{
						num2 = 3;
						continue;
					}
					text = text + this.ᜃ[num].ToString(RecordTableEnumerator.b("㹅穇", a_)) + RecordTableEnumerator.b("晅", a_);
					num++;
					if (true)
					{
					}
					num2 = 0;
					continue;
				case 3:
					return text;
				}
				break;
				IL_59:
				num2 = 2;
			}
		}
		return text;
	}

	// Token: 0x0600508A RID: 20618 RVA: 0x00328FD0 File Offset: 0x00327FD0
	public string ᜀ(byte[,] A_0)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				text = "";
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_10B;
					case 1:
						goto IL_80;
					case 2:
						return text;
					case 3:
					{
						if (num >= A_0.GetLength(0))
						{
							num2 = 2;
							continue;
						}
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							RecordTableEnumerator.b("洵", a_),
							num,
							RecordTableEnumerator.b("欵ᠷ", a_)
						});
						int num3 = 0;
						num2 = 5;
						continue;
					}
					case 4:
					{
						int num3;
						if (num3 >= A_0.GetLength(1))
						{
							num2 = 7;
							continue;
						}
						text = text + A_0[num, num3].ToString(RecordTableEnumerator.b("丵਷", a_)) + RecordTableEnumerator.b("ᘵ", a_);
						num3++;
						num2 = 6;
						continue;
					}
					case 5:
						goto IL_85;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_80;
						default:
							if (false)
							{
							}
							goto IL_85;
						}
						break;
					case 7:
						if (true)
						{
						}
						text += RecordTableEnumerator.b("㰵", a_);
						num++;
						num2 = 1;
						continue;
					}
					break;
					IL_85:
					num2 = 4;
					continue;
					IL_10B:
					num2 = 3;
					continue;
					IL_80:
					goto IL_10B;
				}
			}
			return text;
		}
		}
	}

	// Token: 0x04002413 RID: 9235
	private int ᜀ;

	// Token: 0x04002414 RID: 9236
	private int ᜁ;

	// Token: 0x04002415 RID: 9237
	private int ᜂ;

	// Token: 0x04002416 RID: 9238
	private byte[] ᜃ;

	// Token: 0x04002417 RID: 9239
	private byte[,] ᜄ;

	// Token: 0x04002418 RID: 9240
	private byte[,] ᜅ;

	// Token: 0x04002419 RID: 9241
	private byte[,] ᜆ;

	// Token: 0x0400241A RID: 9242
	private byte[,] ᜇ;

	// Token: 0x0400241B RID: 9243
	private byte[,] ᜈ;

	// Token: 0x0400241C RID: 9244
	private spr\u1C4C.KeySize ᜉ;

	// Token: 0x02000525 RID: 1317
	public enum KeySize
	{
		// Token: 0x0400241E RID: 9246
		Bits128,
		// Token: 0x0400241F RID: 9247
		Bits192,
		// Token: 0x04002420 RID: 9248
		Bits256
	}
}
