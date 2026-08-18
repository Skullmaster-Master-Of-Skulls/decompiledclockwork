using System;
using System.IO;
using Spire.Compression;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000579 RID: 1401
internal class spr᥏
{
	// Token: 0x06005416 RID: 21526 RVA: 0x003434B4 File Offset: 0x003424B4
	static spr᥏()
	{
		for (;;)
		{
			spr᥏.\u1713 = new int[]
			{
				0,
				4,
				4,
				4,
				4,
				8,
				8,
				8,
				32,
				32
			};
			spr᥏.\u1714 = new int[]
			{
				0,
				4,
				5,
				6,
				4,
				16,
				16,
				32,
				128,
				258
			};
			spr᥏.\u1715 = new int[]
			{
				0,
				8,
				16,
				32,
				16,
				32,
				128,
				128,
				258,
				258
			};
			spr᥏.\u1716 = new int[]
			{
				0,
				4,
				8,
				32,
				16,
				32,
				128,
				256,
				1024,
				4096
			};
			spr᥏.\u1717 = new int[]
			{
				0,
				1,
				1,
				1,
				1,
				2,
				2,
				2,
				2,
				2
			};
			spr᥏.\u1718 = Math.Min(65535, 65531);
			spr᥏.ᜨ = new short[286];
			spr᥏.ᜩ = new byte[286];
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_211;
				case 1:
					num2 = 6;
					continue;
				case 2:
					if (num >= 144)
					{
						num2 = 14;
						continue;
					}
					spr᥏.ᜨ[num] = sprៜ.ᜀ(48 + num << 8);
					spr᥏.ᜩ[num++] = 8;
					num2 = 10;
					continue;
				case 3:
					goto IL_1C8;
				case 4:
					if (num >= 280)
					{
						num2 = 1;
						continue;
					}
					spr᥏.ᜨ[num] = sprៜ.ᜀ(-256 + num << 9);
					spr᥏.ᜩ[num++] = 7;
					num2 = 13;
					continue;
				case 5:
					if (num >= 30)
					{
						num2 = 19;
						continue;
					}
					spr᥏.ᜪ[num] = sprៜ.ᜀ(num << 11);
					spr᥏.ᜫ[num] = 5;
					num++;
					num2 = 16;
					continue;
				case 6:
					goto IL_1EE;
				case 7:
					spr᥏.ᜪ = new short[30];
					spr᥏.ᜫ = new byte[30];
					num = 0;
					num2 = 0;
					continue;
				case 8:
					goto IL_1EE;
				case 9:
					goto IL_13D;
				case 10:
					goto IL_1C8;
				case 11:
					goto IL_290;
				case 12:
					if (num >= 286)
					{
						num2 = 7;
						continue;
					}
					spr᥏.ᜨ[num] = sprៜ.ᜀ(-88 + num << 8);
					spr᥏.ᜩ[num++] = 8;
					num2 = 8;
					continue;
				case 13:
					goto IL_13D;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_290;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 17;
						continue;
					}
					break;
				case 15:
					if (num >= 256)
					{
						num2 = 18;
						continue;
					}
					spr᥏.ᜨ[num] = sprៜ.ᜀ(256 + num << 7);
					spr᥏.ᜩ[num++] = 9;
					num2 = 11;
					continue;
				case 16:
					goto IL_211;
				case 17:
					goto IL_2CE;
				case 18:
					num2 = 9;
					continue;
				case 19:
					return;
				}
				break;
				IL_13D:
				num2 = 4;
				continue;
				IL_1C8:
				num2 = 2;
				continue;
				IL_1EE:
				num2 = 12;
				continue;
				IL_211:
				num2 = 5;
				continue;
				IL_2CE:
				num2 = 15;
				continue;
				IL_290:
				goto IL_2CE;
			}
		}
	}

	// Token: 0x06005417 RID: 21527 RVA: 0x003437F0 File Offset: 0x003427F0
	internal spr᥏(Stream A_0, bool A_1, CompressionLevel A_2, bool A_3)
	{
		int a_ = 2;
		this.\u171A = new byte[65536];
		this.\u171F = 1L;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("圷伹䠻丽㔿㙁ᝃ㉅㩇⽉ⵋ⍍", a_));
		}
		if (!A_0.CanWrite)
		{
			throw new ArgumentException(RecordTableEnumerator.b("眷伹䠻丽㔿㙁摃㕅㱇㡉⥋⽍㵏牑こ㥕㵗⥙籛そཟᙡ䑣ᕥᵧᩩᱫŭɯٱ味ŵ੷፹ࡻ᝽ꪃ", a_), RecordTableEnumerator.b("圷伹䠻丽㔿㙁ᝃ㉅㩇⽉ⵋ⍍", a_));
		}
		this.ᜡ = new spr\u260B(this, 286, 257, 15);
		this.ᜢ = new spr\u260B(this, 30, 1, 15);
		this.ᜣ = new spr\u260B(this, 19, 4, 7);
		this.ᜦ = new short[16384];
		this.ᜥ = new byte[16384];
		this.\u1719 = A_0;
		this.ᜠ = A_2;
		this.\u171E = A_1;
		this.ᝀ = A_3;
		this.\u1736 = new byte[65536];
		this.ᜮ = new short[32768];
		this.ᜯ = new short[32768];
		this.\u1733 = (this.\u1734 = 1);
		this.\u173A = spr᥏.\u1713[(int)A_2];
		this.\u1738 = spr᥏.\u1714[(int)A_2];
		this.\u1739 = spr᥏.\u1715[(int)A_2];
		this.\u1737 = spr᥏.\u1716[(int)A_2];
		this.\u173B = spr᥏.\u1717[(int)A_2];
		if (!A_1)
		{
			this.ᜆ();
		}
	}

	// Token: 0x06005418 RID: 21528 RVA: 0x00343978 File Offset: 0x00342978
	public spr᥏(Stream A_0, bool A_1, bool A_2) : this(A_0, A_1, CompressionLevel.Normal, A_2)
	{
	}

	// Token: 0x06005419 RID: 21529 RVA: 0x00343990 File Offset: 0x00342990
	internal spr᥏(Stream A_0, CompressionLevel A_1, bool A_2) : this(A_0, false, A_1, A_2)
	{
	}

	// Token: 0x0600541A RID: 21530 RVA: 0x003439A8 File Offset: 0x003429A8
	public spr᥏(Stream A_0, bool A_1) : this(A_0, false, CompressionLevel.Normal, A_1)
	{
	}

	// Token: 0x0600541B RID: 21531 RVA: 0x003439C0 File Offset: 0x003429C0
	public void ᜂ(byte[] A_0, int A_1, int A_2, bool A_3)
	{
		int a_ = 1;
		int num = 4;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				this.ᜌ();
				this.ᜏ();
				num = 15;
				continue;
			case 1:
				goto IL_12D;
			case 2:
				num = 13;
				continue;
			case 3:
				if (this.ᜬ)
				{
					num = 6;
					continue;
				}
				sprṼ.ᜀ(ref this.\u171F, this.\u173C, this.\u173E, A_2);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_175;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				break;
			case 5:
				goto IL_93;
			case 6:
				goto IL_2C7;
			case 7:
				goto IL_194;
			case 8:
				goto IL_1DF;
			case 9:
				return;
			case 10:
				num = 17;
				continue;
			case 11:
				goto IL_194;
			case 12:
				if (A_3)
				{
					num = 0;
					continue;
				}
				goto IL_194;
			case 13:
				if (this.ᜋ())
				{
					num = 9;
					continue;
				}
				goto IL_300;
			case 14:
				if (0 <= A_1)
				{
					num = 10;
					continue;
				}
				goto IL_1CB;
			case 15:
				if (!this.\u171E)
				{
					num = 16;
					continue;
				}
				goto IL_1DF;
			case 16:
				this.ᜅ((int)(this.\u171F >> 16));
				this.ᜅ((int)(this.\u171F & 65535L));
				num = 8;
				continue;
			case 17:
				if (A_1 <= num2)
				{
					num = 20;
					continue;
				}
				goto IL_1CB;
			case 18:
				if (num2 > A_0.Length)
				{
					num = 1;
					continue;
				}
				this.\u173C = A_0;
				this.\u173E = A_1;
				this.\u173F = num2;
				num = 26;
				continue;
			case 19:
				if (!this.ᜀ(A_3))
				{
					num = 25;
					continue;
				}
				goto IL_194;
			case 20:
				num = 18;
				continue;
			case 21:
				if (this.ᜈ())
				{
					num = 2;
					continue;
				}
				goto IL_300;
			case 22:
				return;
			case 23:
				this.\u1719.Close();
				if (true)
				{
				}
				num = 7;
				continue;
			case 24:
				if (this.ᝀ)
				{
					num = 23;
					continue;
				}
				goto IL_194;
			case 25:
				goto IL_175;
			case 26:
				if (A_2 == 0)
				{
					num = 22;
					continue;
				}
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num2 = A_1 + A_2;
			num = 14;
			continue;
			IL_175:
			num = 12;
			continue;
			IL_194:
			num = 21;
			continue;
			IL_1DF:
			this.ᜌ();
			this.ᜬ = true;
			num = 24;
			continue;
			IL_300:
			this.ᜌ();
			num = 19;
		}
		IL_93:
		throw new ArgumentNullException(RecordTableEnumerator.b("匶堸伺尼", a_));
		IL_12D:
		IL_1CB:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("砶弸崺丼娾㕀捂⩄㕆楈❊⡌ⅎ㙐❒㵔睖じ⡚絜㙞འb੤ᕦ᭨๪๬᭮彰", a_));
		IL_2C7:
		throw new IOException(RecordTableEnumerator.b("搶䴸䤺堼帾ⱀ捂㉄♆㩈歊⹌⍎㹐⁒ご㍖睘", a_));
	}

	// Token: 0x0600541C RID: 21532 RVA: 0x00343CFC File Offset: 0x00342CFC
	public void ᜉ()
	{
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1A2;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_7B;
				}
				break;
			case 2:
				if (this.ᜈ())
				{
					num = 0;
					continue;
				}
				goto IL_8B;
			case 3:
				this.ᜌ();
				this.ᜏ();
				num = 12;
				continue;
			case 4:
				return;
			case 5:
				if (this.ᜋ())
				{
					num = 13;
					continue;
				}
				goto IL_8B;
			case 6:
				this.\u1719.Close();
				num = 4;
				continue;
			case 7:
				return;
			case 8:
				if (this.ᝀ)
				{
					num = 6;
					continue;
				}
				return;
			case 9:
				goto IL_EA;
			case 10:
				if (!this.ᜀ(true))
				{
					num = 3;
					continue;
				}
				goto IL_EA;
			case 12:
				if (!this.\u171E)
				{
					goto IL_1A2;
				}
				goto IL_7B;
			case 13:
				this.ᜬ = true;
				num = 8;
				continue;
			case 14:
				this.ᜅ((int)(this.\u171F >> 16));
				this.ᜅ((int)(this.\u171F & 65535L));
				num = 1;
				continue;
			}
			if (this.ᜬ)
			{
				num = 7;
				continue;
			}
			goto IL_8B;
			IL_7B:
			this.ᜌ();
			num = 9;
			continue;
			IL_8B:
			this.ᜌ();
			num = 10;
			continue;
			IL_EA:
			num = 2;
			continue;
			IL_1A2:
			num = 14;
		}
	}

	// Token: 0x0600541D RID: 21533 RVA: 0x00343EBC File Offset: 0x00342EBC
	public int ᜐ()
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
		return this.\u173D;
	}

	// Token: 0x0600541E RID: 21534 RVA: 0x00343F00 File Offset: 0x00342F00
	private bool ᜈ()
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
		return this.\u173F == this.\u173E;
	}

	// Token: 0x0600541F RID: 21535 RVA: 0x00343F4C File Offset: 0x00342F4C
	private bool ᜇ()
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
		return this.ᜤ >= 16384;
	}

	// Token: 0x06005420 RID: 21536 RVA: 0x00343F98 File Offset: 0x00342F98
	private void ᜆ()
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
		int num = 30720;
		num |= (int)((int)(this.ᜠ >> 2 & CompressionLevel.BelowNormal) << 6);
		num += 31 - num % 31;
		this.ᜅ(num);
	}

	// Token: 0x06005421 RID: 21537 RVA: 0x00343FFC File Offset: 0x00342FFC
	private void ᜅ()
	{
		int num = 10;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_129;
			case 1:
				if (num2 > this.\u173F - this.\u173E)
				{
					num = 11;
					continue;
				}
				goto IL_169;
			case 2:
				goto IL_8B;
			case 3:
				if (this.\u173E >= this.\u173F)
				{
					num = 0;
					continue;
				}
				goto IL_B3;
			case 4:
				return;
			case 5:
				if (this.\u1735 < 262)
				{
					num = 12;
					continue;
				}
				goto IL_129;
			case 6:
				goto IL_169;
			case 7:
				if (this.\u1735 >= 3)
				{
					num = 8;
					continue;
				}
				return;
			case 8:
				this.ᜃ();
				num = 4;
				continue;
			case 9:
				this.ᜄ();
				if (true)
				{
				}
				num = 2;
				continue;
			case 11:
				num2 = this.\u173F - this.\u173E;
				num = 6;
				continue;
			case 12:
				num = 3;
				continue;
			case 13:
				goto IL_8B;
			}
			if (this.\u1734 >= 65274)
			{
				num = 9;
				continue;
			}
			IL_8B:
			num = 5;
			continue;
			IL_B3:
			num2 = 65536 - this.\u1735 - this.\u1734;
			num = 1;
			continue;
			IL_129:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_B3;
			default:
				if (false)
				{
				}
				num = 7;
				continue;
			}
			IL_169:
			Array.Copy(this.\u173C, this.\u173E, this.\u1736, this.\u1734 + this.\u1735, num2);
			this.\u173E += num2;
			this.\u173D += num2;
			this.\u1735 += num2;
			num = 13;
		}
	}

	// Token: 0x06005422 RID: 21538 RVA: 0x003441FC File Offset: 0x003431FC
	private void ᜄ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Array.Copy(this.\u1736, 32768, this.\u1736, 0, 32768);
				this.ᜰ -= 32768;
				this.\u1734 -= 32768;
				this.\u1733 -= 32768;
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
					{
						if (num3 >= 32768)
						{
							num2 = 7;
							continue;
						}
						if (true)
						{
						}
						int num4 = (int)this.ᜯ[num3] & 65535;
						num2 = 1;
						continue;
					}
					case 1:
					{
						int num4;
						this.ᜯ[num3] = (short)((num4 >= 32768) ? (num4 - 32768) : 0);
						num3++;
						num2 = 3;
						continue;
					}
					case 2:
						goto IL_11A;
					case 3:
						goto IL_165;
					case 4:
					{
						int num5;
						this.ᜮ[num] = (short)((num5 >= 32768) ? (num5 - 32768) : 0);
						num++;
						num2 = 9;
						continue;
					}
					case 5:
					{
						if (num >= 32768)
						{
							num2 = 8;
							continue;
						}
						int num5 = (int)this.ᜮ[num] & 65535;
						num2 = 4;
						continue;
					}
					case 6:
						goto IL_165;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_13D;
						default:
							goto IL_1C1;
						}
						break;
					case 8:
						goto IL_13D;
					case 9:
						goto IL_11A;
					}
					break;
					IL_11A:
					num2 = 5;
					continue;
					IL_13D:
					num3 = 0;
					num2 = 6;
					continue;
					IL_165:
					num2 = 0;
				}
			}
			IL_1C1:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06005423 RID: 21539 RVA: 0x003443D0 File Offset: 0x003433D0
	private void ᜃ()
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
		this.ᜭ = ((int)this.\u1736[this.\u1734] << 5 ^ (int)this.\u1736[this.\u1734 + 1]);
	}

	// Token: 0x06005424 RID: 21540 RVA: 0x00344430 File Offset: 0x00343430
	private int ᜂ()
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
		int num = (this.ᜭ << 5 ^ (int)this.\u1736[this.\u1734 + 2]) & 32767;
		short num2 = this.ᜯ[this.\u1734 & 32767] = this.ᜮ[num];
		this.ᜮ[num] = (short)this.\u1734;
		this.ᜭ = num;
		return (int)num2 & 65535;
	}

	// Token: 0x06005425 RID: 21541 RVA: 0x003444C4 File Offset: 0x003434C4
	private bool ᜄ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num5;
			for (;;)
			{
				int num = this.\u1737;
				int num2 = this.\u1739;
				short[] array = this.ᜯ;
				int num3 = this.\u1734;
				int num4 = this.\u1734 + this.ᜱ;
				num5 = Math.Max(this.ᜱ, 2);
				int num6 = Math.Max(this.\u1734 - 32506, 0);
				int num7 = this.\u1734 + 258 - 1;
				byte b = this.\u1736[num4 - 1];
				byte b2 = this.\u1736[num4];
				int num8 = 28;
				for (;;)
				{
					int num9;
					switch (num8)
					{
					case 0:
						num8 = 33;
						continue;
					case 1:
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 41;
							continue;
						}
						goto IL_594;
					case 2:
						if (this.\u1736[A_0 + 1] == this.\u1736[num3 + 1])
						{
							num8 = 29;
							continue;
						}
						goto IL_274;
					case 3:
						goto IL_56B;
					case 4:
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 0;
							continue;
						}
						goto IL_594;
					case 5:
						num8 = 17;
						continue;
					case 6:
						num8 = 25;
						continue;
					case 7:
						num8 = 15;
						continue;
					case 8:
						goto IL_3CC;
					case 9:
						goto IL_376;
					case 10:
						b = this.\u1736[num4 - 1];
						b2 = this.\u1736[num4];
						num8 = 36;
						continue;
					case 11:
						this.ᜰ = A_0;
						num4 = num3;
						num5 = num3 - this.\u1734;
						num8 = 16;
						continue;
					case 12:
						if (this.\u1736[A_0 + num5] == b2)
						{
							num8 = 34;
							continue;
						}
						goto IL_274;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_502;
						default:
							if (false)
							{
							}
							if (--num == 0)
							{
								num8 = 9;
								continue;
							}
							goto IL_3CC;
						}
						break;
					case 14:
						goto IL_37B;
					case 15:
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 5;
							continue;
						}
						goto IL_594;
					case 16:
						if (num5 < num2)
						{
							num8 = 10;
							continue;
						}
						goto IL_5C3;
					case 17:
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 40;
							continue;
						}
						goto IL_594;
					case 18:
						num >>= 2;
						num8 = 3;
						continue;
					case 19:
						goto IL_274;
					case 20:
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 23;
							continue;
						}
						goto IL_594;
					case 21:
						if ((A_0 = ((int)array[A_0 & 32767] & 65535)) > num6)
						{
							if (true)
							{
							}
							num8 = 32;
							continue;
						}
						goto IL_5C3;
					case 22:
						if (this.\u1736[A_0 + num5 - 1] == b)
						{
							num8 = 26;
							continue;
						}
						goto IL_274;
					case 23:
						num8 = 1;
						continue;
					case 24:
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 6;
							continue;
						}
						goto IL_594;
					case 25:
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 37;
							continue;
						}
						goto IL_594;
					case 26:
						num8 = 30;
						continue;
					case 27:
						num8 = 2;
						continue;
					case 28:
						if (num5 >= this.\u173A)
						{
							num8 = 18;
							continue;
						}
						goto IL_56B;
					case 29:
						num9 = A_0 + 2;
						num3 += 2;
						num8 = 14;
						continue;
					case 30:
						if (this.\u1736[A_0] == this.\u1736[num3])
						{
							num8 = 27;
							continue;
						}
						goto IL_274;
					case 31:
						if (num2 > this.\u1735)
						{
							num8 = 35;
							continue;
						}
						goto IL_3CC;
					case 32:
						num8 = 13;
						continue;
					case 33:
						if (num3 >= num7)
						{
							num8 = 42;
							continue;
						}
						goto IL_37B;
					case 34:
						num8 = 22;
						continue;
					case 35:
						num2 = this.\u1735;
						num8 = 8;
						continue;
					case 36:
						goto IL_320;
					case 37:
						num8 = 39;
						continue;
					case 38:
						if (num3 > num4)
						{
							num8 = 11;
							continue;
						}
						goto IL_320;
					case 39:
						goto IL_502;
					case 40:
						num8 = 20;
						continue;
					case 41:
						num8 = 4;
						continue;
					case 42:
						goto IL_594;
					}
					break;
					IL_274:
					num8 = 21;
					continue;
					IL_320:
					num3 = this.\u1734;
					num8 = 19;
					continue;
					IL_37B:
					num8 = 24;
					continue;
					IL_3CC:
					num8 = 12;
					continue;
					IL_502:
					if (this.\u1736[++num3] == this.\u1736[++num9])
					{
						num8 = 7;
						continue;
					}
					goto IL_594;
					IL_56B:
					num8 = 31;
					continue;
					IL_594:
					num8 = 38;
				}
			}
			IL_376:
			IL_5C3:
			this.ᜱ = Math.Min(num5, this.\u1735);
			return this.ᜱ >= 3;
		}
		}
	}

	// Token: 0x06005426 RID: 21542 RVA: 0x00344AB4 File Offset: 0x00343AB4
	private bool ᜂ(bool A_0, bool A_1)
	{
		int num = 13;
		int num2;
		bool flag;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 11;
				continue;
			case 1:
				if (A_0)
				{
					num = 4;
					continue;
				}
				return true;
			case 2:
				if (num2 < spr᥏.\u1718)
				{
					num = 6;
					continue;
				}
				goto IL_5C;
			case 3:
				goto IL_DD;
			case 4:
				goto IL_5C;
			case 5:
				if (num2 > spr᥏.\u1718)
				{
					num = 8;
					continue;
				}
				goto IL_FE;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AD;
				default:
					if (false)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 7:
				num = 12;
				continue;
			case 8:
				if (true)
				{
				}
				num2 = spr᥏.\u1718;
				flag = false;
				goto IL_AD;
			case 9:
				if (this.\u1733 < 32768)
				{
					num = 0;
					continue;
				}
				goto IL_DD;
			case 10:
				goto IL_B8;
			case 11:
				if (num2 < 32506)
				{
					num = 3;
					continue;
				}
				goto IL_5C;
			case 12:
				if (this.\u1735 == 0)
				{
					num = 14;
					continue;
				}
				goto IL_173;
			case 14:
				return false;
			}
			if (!A_0)
			{
				num = 7;
				continue;
			}
			goto IL_173;
			IL_5C:
			flag = A_1;
			num = 5;
			continue;
			IL_AD:
			num = 10;
			continue;
			IL_DD:
			num = 1;
			continue;
			IL_173:
			this.\u1734 += this.\u1735;
			this.\u1735 = 0;
			num2 = this.\u1734 - this.\u1733;
			num = 2;
		}
		IL_B8:
		IL_FE:
		this.ᜁ(this.\u1736, this.\u1733, num2, flag);
		this.\u1733 += num2;
		return !flag;
	}

	// Token: 0x06005427 RID: 21543 RVA: 0x00344C84 File Offset: 0x00343C84
	private bool ᜁ(bool A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 26;
			for (;;)
			{
				bool flag;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_226;
				case 1:
					num = 31;
					continue;
				case 2:
					goto IL_3B2;
				case 3:
					num = 40;
					continue;
				case 4:
					goto IL_5A5;
				case 5:
					goto IL_559;
				case 6:
					if (A_1)
					{
						num = 11;
						continue;
					}
					num = 18;
					continue;
				case 7:
					this.ᜄ();
					num = 2;
					continue;
				case 8:
					if (this.ᜱ <= this.\u1738)
					{
						num = 45;
						continue;
					}
					goto IL_306;
				case 9:
					if (this.\u1735 >= 3)
					{
						num = 46;
						continue;
					}
					goto IL_306;
				case 10:
					this.ᜃ();
					num = 15;
					continue;
				case 11:
					num = 41;
					continue;
				case 12:
					if (this.\u1735 >= 3)
					{
						num = 3;
						continue;
					}
					goto IL_354;
				case 13:
					goto IL_5A5;
				case 14:
					if (this.ᜇ())
					{
						num = 39;
						continue;
					}
					goto IL_559;
				case 15:
					goto IL_226;
				case 16:
					if (!A_0)
					{
						num = 44;
						continue;
					}
					goto IL_559;
				case 17:
					if (true)
					{
					}
					if (this.\u1735 == 0)
					{
						num = 23;
						continue;
					}
					num = 37;
					continue;
				case 18:
					flag = false;
					goto IL_4D6;
				case 19:
					num = 33;
					continue;
				case 20:
					return true;
				case 21:
					if (A_1)
					{
						num = 1;
						continue;
					}
					num = 27;
					continue;
				case 22:
					num = 35;
					continue;
				case 23:
					goto IL_269;
				case 24:
					if (this.\u1735 < 262)
					{
						num = 36;
						continue;
					}
					goto IL_23E;
				case 25:
					if (!A_0)
					{
						num = 20;
						continue;
					}
					goto IL_23E;
				case 27:
					goto IL_34E;
				case 28:
					if (num2 <= 0)
					{
						num = 29;
						continue;
					}
					this.\u1734++;
					this.ᜂ();
					num = 4;
					continue;
				case 29:
					this.\u1734++;
					num = 0;
					continue;
				case 30:
					if (this.\u1735 >= 2)
					{
						num = 10;
						continue;
					}
					goto IL_226;
				case 31:
					goto IL_15E;
				case 32:
					goto IL_428;
				case 33:
					if (this.ᜀ(this.\u1734 - this.ᜰ, this.ᜱ))
					{
						num = 38;
						continue;
					}
					goto IL_428;
				case 34:
					num = 42;
					continue;
				case 35:
				{
					int num3;
					if (this.\u1734 - num3 <= 32506)
					{
						num = 34;
						continue;
					}
					goto IL_354;
				}
				case 36:
					num = 25;
					continue;
				case 37:
					if (this.\u1734 > 65274)
					{
						num = 7;
						continue;
					}
					goto IL_3B2;
				case 38:
					num = 6;
					continue;
				case 39:
					num = 21;
					continue;
				case 40:
				{
					int num3;
					if ((num3 = this.ᜂ()) != 0)
					{
						num = 22;
						continue;
					}
					goto IL_354;
				}
				case 41:
					flag = (this.\u1735 == 0);
					goto IL_4D6;
				case 42:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5A5;
					default:
					{
						if (false)
						{
						}
						int num3;
						if (this.ᜄ(num3))
						{
							num = 19;
							continue;
						}
						goto IL_354;
					}
					}
					break;
				case 43:
					num = 16;
					continue;
				case 44:
					return false;
				case 45:
					num = 9;
					continue;
				case 46:
					num = 13;
					continue;
				}
				if (this.\u1735 < 262)
				{
					num = 43;
					continue;
				}
				goto IL_559;
				IL_226:
				this.ᜱ = 2;
				num = 5;
				continue;
				IL_23E:
				num = 17;
				continue;
				IL_306:
				this.\u1734 += this.ᜱ;
				num = 30;
				continue;
				IL_354:
				this.ᜀ((int)(this.\u1736[this.\u1734] & byte.MaxValue));
				this.\u1734++;
				this.\u1735--;
				num = 14;
				continue;
				IL_3B2:
				num = 12;
				continue;
				IL_428:
				this.\u1735 -= this.ᜱ;
				num = 8;
				continue;
				IL_4D6:
				bool a_ = flag;
				this.ᜀ(this.\u1736, this.\u1733, this.\u1734 - this.\u1733, a_);
				this.\u1733 = this.\u1734;
				num = 32;
				continue;
				IL_559:
				num = 24;
				continue;
				IL_5A5:
				num2 = --this.ᜱ;
				num = 28;
			}
			IL_15E:
			bool flag2 = this.\u1735 == 0;
			IL_1B6:
			bool flag3 = flag2;
			this.ᜀ(this.\u1736, this.\u1733, this.\u1734 - this.\u1733, flag3);
			this.\u1733 = this.\u1734;
			return !flag3;
			IL_269:
			this.ᜀ(this.\u1736, this.\u1733, this.\u1734 - this.\u1733, A_1);
			this.\u1733 = this.\u1734;
			return false;
			IL_34E:
			flag2 = false;
			goto IL_1B6;
		}
		}
	}

	// Token: 0x06005428 RID: 21544 RVA: 0x0034526C File Offset: 0x0034426C
	private bool ᜀ(bool A_0, bool A_1)
	{
		int num3;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						if (this.\u1732)
						{
							num = 34;
							continue;
						}
						goto IL_2A1;
					case 1:
					{
						int num2;
						if (num2 != 0)
						{
							num = 18;
							continue;
						}
						goto IL_529;
					}
					case 2:
						num3 = this.\u1734 - this.\u1733;
						num = 0;
						continue;
					case 3:
						if (this.\u1734 >= 65274)
						{
							num = 46;
							continue;
						}
						goto IL_156;
					case 4:
						num = 42;
						continue;
					case 5:
						num = 53;
						continue;
					case 6:
						this.ᜀ((int)(this.\u1736[this.\u1734 - 1] & byte.MaxValue));
						num = 16;
						continue;
					case 7:
						this.ᜀ(this.\u1734 - 1 - num4, num5);
						num5 -= 2;
						num = 51;
						continue;
					case 8:
						num = 11;
						continue;
					case 9:
					{
						int num2;
						if (this.\u1734 - num2 <= 32506)
						{
							num = 31;
							continue;
						}
						goto IL_529;
					}
					case 10:
						num = 19;
						continue;
					case 11:
						if (this.\u1734 - this.ᜰ > 4096)
						{
							num = 37;
							continue;
						}
						goto IL_529;
					case 12:
						return true;
					case 14:
						this.\u1734++;
						this.\u1735--;
						this.\u1732 = false;
						this.ᜱ = 2;
						num = 30;
						continue;
					case 15:
						if (this.ᜇ())
						{
							num = 2;
							continue;
						}
						goto IL_4FC;
					case 16:
						goto IL_404;
					case 17:
						return false;
					case 18:
						num = 9;
						continue;
					case 19:
						if (this.ᜱ <= 5)
						{
							if (true)
							{
							}
							num = 20;
							continue;
						}
						goto IL_529;
					case 20:
						num = 49;
						continue;
					case 21:
						num = 40;
						continue;
					case 22:
						goto IL_409;
					case 23:
						goto IL_156;
					case 24:
						goto IL_2C1;
					case 25:
						if (this.\u1735 >= 3)
						{
							num = 33;
							continue;
						}
						goto IL_529;
					case 26:
						goto IL_529;
					case 27:
						goto IL_150;
					case 28:
						if (this.\u1735 < 262)
						{
							num = 4;
							continue;
						}
						goto IL_1F8;
					case 29:
						num = 32;
						continue;
					case 30:
						goto IL_2C1;
					case 31:
						num = 38;
						continue;
					case 32:
						if (this.\u1735 == 0)
						{
							num = 5;
							continue;
						}
						goto IL_144;
					case 33:
					{
						int num2 = this.ᜂ();
						num = 1;
						continue;
					}
					case 34:
						num3--;
						num = 50;
						continue;
					case 35:
						if (A_1)
						{
							num = 29;
							continue;
						}
						goto IL_144;
					case 36:
						this.ᜂ();
						num = 52;
						continue;
					case 37:
						this.ᜱ = 2;
						num = 26;
						continue;
					case 38:
					{
						int num2;
						if (this.ᜄ(num2))
						{
							num = 10;
							continue;
						}
						goto IL_529;
					}
					case 39:
						if (--num5 <= 0)
						{
							num = 14;
							continue;
						}
						goto IL_33F;
					case 40:
						if (this.ᜱ <= num5)
						{
							num = 7;
							continue;
						}
						goto IL_56D;
					case 41:
						if (this.\u1732)
						{
							num = 6;
							continue;
						}
						goto IL_18D;
					case 42:
						if (!A_0)
						{
							num = 12;
							continue;
						}
						goto IL_1F8;
					case 43:
						if (this.\u1735 >= 3)
						{
							num = 36;
							continue;
						}
						goto IL_24A;
					case 44:
						if (!A_0)
						{
							num = 17;
							continue;
						}
						goto IL_4FC;
					case 45:
						if (num5 >= 3)
						{
							num = 21;
							continue;
						}
						goto IL_56D;
					case 46:
						this.ᜄ();
						num = 23;
						continue;
					case 47:
						num = 41;
						continue;
					case 48:
						num = 44;
						continue;
					case 49:
						if (this.ᜱ == 3)
						{
							num = 8;
							continue;
						}
						goto IL_529;
					case 50:
						goto IL_2A1;
					case 51:
						goto IL_33F;
					case 52:
						goto IL_24A;
					case 53:
						goto IL_31A;
					case 54:
						if (this.\u1735 == 0)
						{
							num = 47;
							continue;
						}
						num = 3;
						continue;
					case 55:
						if (this.\u1732)
						{
							num = 56;
							continue;
						}
						goto IL_409;
					case 56:
						this.ᜀ((int)(this.\u1736[this.\u1734 - 1] & byte.MaxValue));
						num = 22;
						continue;
					}
					if (this.\u1735 >= 262)
					{
						goto IL_4FC;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 48;
						continue;
					}
					IL_144:
					num = 27;
					continue;
					IL_156:
					num4 = this.ᜰ;
					num5 = this.ᜱ;
					num = 25;
					continue;
					IL_1F8:
					num = 54;
					continue;
					IL_24A:
					num = 39;
					continue;
					IL_2A1:
					num = 35;
					continue;
					IL_2C1:
					num = 15;
					continue;
					IL_33F:
					this.\u1734++;
					this.\u1735--;
					num = 43;
					continue;
					IL_409:
					this.\u1732 = true;
					this.\u1734++;
					this.\u1735--;
					num = 24;
					continue;
					IL_4FC:
					num = 28;
					continue;
					IL_529:
					num = 45;
					continue;
					IL_56D:
					num = 55;
				}
				break;
			}
			}
		}
		IL_150:
		bool flag = false;
		goto IL_595;
		IL_18D:
		this.\u1732 = false;
		this.ᜀ(this.\u1736, this.\u1733, this.\u1734 - this.\u1733, A_1);
		this.\u1733 = this.\u1734;
		return false;
		IL_31A:
		flag = !this.\u1732;
		goto IL_595;
		IL_404:
		goto IL_18D;
		IL_595:
		bool flag2 = flag;
		this.ᜀ(this.\u1736, this.\u1733, num3, flag2);
		this.\u1733 += num3;
		return !flag2;
	}

	// Token: 0x06005429 RID: 21545 RVA: 0x00345940 File Offset: 0x00344940
	private bool ᜀ(bool A_0)
	{
		int a_ = 18;
		bool flag;
		for (;;)
		{
			for (;;)
			{
				if (true)
				{
				}
				this.ᜅ();
				int num = 0;
				for (;;)
				{
					bool flag2;
					int u173B;
					bool a_2;
					switch (num)
					{
					case 0:
						if (A_0)
						{
							num = 1;
							continue;
						}
						num = 5;
						continue;
					case 1:
						num = 8;
						continue;
					case 2:
						if (this.ᜋ())
						{
							num = 3;
							continue;
						}
						return flag;
					case 3:
						num = 11;
						continue;
					case 4:
						goto IL_A7;
					case 5:
						flag2 = false;
						goto IL_147;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_177;
						default:
							goto IL_13C;
						}
						break;
					case 7:
						goto IL_A7;
					case 8:
						flag2 = this.ᜈ();
						goto IL_147;
					case 9:
						goto IL_119;
					case 10:
						goto IL_177;
					case 11:
						if (!flag)
						{
							num = 9;
							continue;
						}
						break;
					case 12:
						goto IL_A7;
					case 13:
						switch (u173B)
						{
						case 0:
							flag = this.ᜂ(a_2, A_0);
							num = 4;
							continue;
						case 1:
							flag = this.ᜁ(a_2, A_0);
							num = 7;
							continue;
						case 2:
							flag = this.ᜀ(a_2, A_0);
							num = 12;
							continue;
						default:
							num = 10;
							continue;
						}
						break;
					}
					break;
					IL_A7:
					num = 2;
					continue;
					IL_147:
					a_2 = flag2;
					u173B = this.\u173B;
					num = 13;
					continue;
					IL_177:
					num = 6;
				}
			}
		}
		IL_119:
		return flag;
		IL_13C:
		if (false)
		{
		}
		throw new InvalidOperationException(RecordTableEnumerator.b("㵇⑉❋⁍㽏║㩓癕㕗ՙὛㅝൟቡᙣͥ᭧ᥩիŭṯ㑱ųᡵ᭷๹ᕻᅽ", a_));
	}

	// Token: 0x0600542A RID: 21546 RVA: 0x00345AE0 File Offset: 0x00344AE0
	private void ᜁ()
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
		this.ᜤ = 0;
		this.ᜧ = 0;
		this.ᜡ.ᜂ();
		this.ᜢ.ᜂ();
		this.ᜣ.ᜂ();
	}

	// Token: 0x0600542B RID: 21547 RVA: 0x00345B4C File Offset: 0x00344B4C
	private int ᜃ(int A_0)
	{
		int num = 4;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 < 8)
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				num2 += 4;
				A_0 >>= 1;
				num = 5;
				continue;
			case 1:
				return 285;
			case 2:
				goto IL_AB;
			case 3:
				goto IL_91;
			case 5:
				goto IL_91;
			}
			if (A_0 == 255)
			{
				num = 1;
				continue;
			}
			num2 = 257;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				num = 3;
				continue;
			}
			IL_91:
			num = 0;
		}
		return 285;
		IL_AB:
		return num2 + A_0;
	}

	// Token: 0x0600542C RID: 21548 RVA: 0x00345C0C File Offset: 0x00344C0C
	private int ᜂ(int A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (A_0 < 4)
					{
						num2 = 3;
						continue;
					}
					num += 2;
					A_0 >>= 1;
					goto IL_69;
				case 1:
					goto IL_24;
				case 2:
					goto IL_24;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_69;
					default:
						goto IL_58;
					}
					break;
				}
				break;
				IL_24:
				num2 = 0;
				continue;
				IL_69:
				num2 = 2;
			}
		}
		IL_58:
		if (false)
		{
		}
		if (true)
		{
		}
		return num + A_0;
	}

	// Token: 0x0600542D RID: 21549 RVA: 0x00345C98 File Offset: 0x00344C98
	private void ᜁ(int A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			this.ᜣ.ᜅ();
			this.ᜡ.ᜅ();
			this.ᜢ.ᜅ();
			this.ᜁ(this.ᜡ.ᜆ() - 257, 5);
			this.ᜁ(this.ᜢ.ᜆ() - 1, 5);
			this.ᜁ(A_0 - 4, 4);
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_83;
				case 1:
					if (num >= A_0)
					{
						num2 = 2;
						continue;
					}
					this.ᜁ((int)this.ᜣ.ᜃ()[sprៜ.ᜁ[num]], 3);
					num++;
					goto IL_E3;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						goto IL_BD;
					}
					break;
				case 3:
					goto IL_83;
				}
				break;
				IL_83:
				num2 = 1;
				continue;
				IL_E3:
				num2 = 3;
			}
		}
		IL_BD:
		if (false)
		{
		}
		this.ᜡ.ᜀ(this.ᜣ);
		this.ᜢ.ᜀ(this.ᜣ);
	}

	// Token: 0x0600542E RID: 21550 RVA: 0x00345DB8 File Offset: 0x00344DB8
	private void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 8;
				for (;;)
				{
					int num3;
					int num5;
					switch (num2)
					{
					case 0:
						goto IL_FE;
					case 1:
						if (num3 > 0)
						{
							num2 = 13;
							continue;
						}
						goto IL_FE;
					case 2:
					{
						int num4;
						this.ᜁ(num4 & (1 << num3) - 1, num3);
						num2 = 7;
						continue;
					}
					case 3:
					{
						if (num >= this.ᜤ)
						{
							num2 = 5;
							continue;
						}
						int num4 = (int)(this.ᜥ[num] & byte.MaxValue);
						num5 = (int)this.ᜦ[num];
						goto IL_12B;
					}
					case 4:
						num2 = 9;
						continue;
					case 5:
						goto IL_1BE;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_12B;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							int num4;
							int num6 = this.ᜃ(num4);
							this.ᜡ.ᜀ(num6);
							num3 = (num6 - 261) / 4;
							num2 = 10;
							continue;
						}
						}
						break;
					case 7:
						goto IL_7D;
					case 8:
						goto IL_19A;
					case 9:
						if (num3 <= 5)
						{
							num2 = 2;
							continue;
						}
						goto IL_7D;
					case 10:
						if (num3 > 0)
						{
							num2 = 4;
							continue;
						}
						goto IL_7D;
					case 11:
						goto IL_FE;
					case 12:
					{
						if (num5-- != 0)
						{
							num2 = 6;
							continue;
						}
						int num4;
						this.ᜡ.ᜀ(num4);
						num2 = 0;
						continue;
					}
					case 13:
						this.ᜁ(num5 & (1 << num3) - 1, num3);
						num2 = 11;
						continue;
					case 14:
						goto IL_19A;
					}
					break;
					IL_7D:
					int num7 = this.ᜂ(num5);
					this.ᜢ.ᜀ(num7);
					num3 = num7 / 2 - 1;
					num2 = 1;
					continue;
					IL_FE:
					num++;
					num2 = 14;
					continue;
					IL_12B:
					num2 = 12;
					continue;
					IL_19A:
					num2 = 3;
				}
			}
			IL_1BE:
			this.ᜡ.ᜀ(256);
			return;
		}
	}

	// Token: 0x0600542F RID: 21551 RVA: 0x00345FE8 File Offset: 0x00344FE8
	private void ᜁ(byte[] A_0, int A_1, int A_2, bool A_3)
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
		this.ᜁ(A_3 ? 1 : 0, 3);
		this.ᜏ();
		this.ᜈ(A_2);
		this.ᜈ(~A_2);
		this.ᜀ(A_0, A_1, A_2);
		this.ᜁ();
	}

	// Token: 0x06005430 RID: 21552 RVA: 0x0034605C File Offset: 0x0034505C
	private void ᜀ(byte[] A_0, int A_1, int A_2, bool A_3)
	{
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				IL_A6:
				short[] array = this.ᜡ.ᜀ();
				int num = 256;
				array[num] += 1;
				this.ᜡ.ᜁ();
				this.ᜢ.ᜁ();
				this.ᜡ.ᜁ(this.ᜣ);
				this.ᜢ.ᜁ(this.ᜣ);
				this.ᜣ.ᜁ();
				num2 = 4;
				int num3 = 18;
				int num4 = 14;
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
						if (true)
						{
						}
						switch (num4)
						{
						case 0:
							goto IL_161;
						case 1:
						{
							int num5 = 0;
							num4 = 18;
							continue;
						}
						case 2:
							if (this.ᜣ.ᜃ()[sprៜ.ᜁ[num3]] > 0)
							{
								num4 = 13;
								continue;
							}
							goto IL_161;
						case 3:
							goto IL_17F;
						case 4:
						{
							int num6;
							int num7;
							if (num6 == num7)
							{
								num4 = 10;
								continue;
							}
							num4 = 6;
							continue;
						}
						case 5:
						{
							int num8;
							if (num8 >= 286)
							{
								num4 = 1;
								continue;
							}
							int num7;
							num7 += (int)(this.ᜡ.ᜀ()[num8] * (short)spr᥏.ᜩ[num8]);
							num8++;
							num4 = 7;
							continue;
						}
						case 6:
							goto IL_391;
						case 7:
							goto IL_209;
						case 8:
						{
							int num6;
							if (A_2 + 4 < num6 >> 3)
							{
								num4 = 15;
								continue;
							}
							goto IL_30F;
						}
						case 9:
							goto IL_339;
						case 10:
							num4 = 9;
							continue;
						case 11:
							num4 = 8;
							continue;
						case 12:
							goto IL_2C3;
						case 13:
							num2 = num3 + 1;
							num4 = 0;
							continue;
						case 14:
							goto IL_173;
						case 15:
							goto IL_301;
						case 16:
						{
							int num6;
							int num7;
							if (num6 >= num7)
							{
								num4 = 17;
								continue;
							}
							goto IL_2C3;
						}
						case 17:
						{
							int num7;
							int num6 = num7;
							num4 = 12;
							continue;
						}
						case 18:
							goto IL_275;
						case 19:
							if (A_1 >= 0)
							{
								num4 = 11;
								continue;
							}
							goto IL_30F;
						case 20:
							goto IL_209;
						case 21:
							num4 = 16;
							continue;
						case 22:
						{
							int num5;
							if (num5 >= 30)
							{
								num4 = 21;
								continue;
							}
							int num7;
							num7 += (int)(this.ᜢ.ᜀ()[num5] * (short)spr᥏.ᜫ[num5]);
							num5++;
							num4 = 23;
							continue;
						}
						case 23:
							goto IL_275;
						case 24:
							goto IL_173;
						case 25:
						{
							int num6 = 14 + num2 * 3 + this.ᜣ.ᜄ() + this.ᜡ.ᜄ() + this.ᜢ.ᜄ() + this.ᜧ;
							int num7 = this.ᜧ;
							int num8 = 0;
							num4 = 20;
							continue;
						}
						}
						goto IL_A6;
						IL_161:
						num3--;
						num4 = 24;
						continue;
						IL_173:
						num4 = 3;
						continue;
						IL_209:
						num4 = 5;
						continue;
						IL_275:
						num4 = 22;
						continue;
						IL_2C3:
						num4 = 19;
						continue;
						IL_30F:
						num4 = 4;
						continue;
					}
					IL_17F:
					if (num3 <= num2)
					{
						num4 = 25;
					}
					else
					{
						num4 = 2;
					}
				}
			}
			IL_301:
			this.ᜁ(A_0, A_1, A_2, A_3);
			return;
			IL_339:
			this.ᜁ(2 + (A_3 ? 1 : 0), 3);
			this.ᜡ.ᜀ(spr᥏.ᜨ, spr᥏.ᜩ);
			this.ᜢ.ᜀ(spr᥏.ᜪ, spr᥏.ᜫ);
			this.ᜀ();
			this.ᜁ();
			return;
			IL_391:
			this.ᜁ(4 + (A_3 ? 1 : 0), 3);
			this.ᜁ(num2);
			this.ᜀ();
			this.ᜁ();
			return;
		}
		}
	}

	// Token: 0x06005431 RID: 21553 RVA: 0x00346424 File Offset: 0x00345424
	private bool ᜀ(int A_0)
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
		this.ᜦ[this.ᜤ] = 0;
		this.ᜥ[this.ᜤ++] = (byte)A_0;
		short[] array = this.ᜡ.ᜀ();
		array[A_0] += 1;
		return this.ᜇ();
	}

	// Token: 0x06005432 RID: 21554 RVA: 0x003464AC File Offset: 0x003454AC
	private bool ᜀ(int A_0, int A_1)
	{
		for (;;)
		{
			this.ᜦ[this.ᜤ] = (short)A_0;
			this.ᜥ[this.ᜤ++] = (byte)(A_1 - 3);
			int num = this.ᜃ(A_1 - 3);
			short[] array = this.ᜡ.ᜀ();
			int num2 = num;
			array[num2] += 1;
			int num3 = 2;
			for (;;)
			{
				int num4;
				switch (num3)
				{
				case 0:
					if (num4 < 4)
					{
						goto IL_175;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13E;
					default:
						if (false)
						{
						}
						num3 = 5;
						continue;
					}
					break;
				case 1:
					goto IL_9F;
				case 2:
					if (num >= 265)
					{
						num3 = 7;
						continue;
					}
					goto IL_9F;
				case 3:
					goto IL_13E;
				case 4:
					this.ᜧ += (num - 261) / 4;
					num3 = 1;
					continue;
				case 5:
					this.ᜧ += num4 / 2 - 1;
					num3 = 6;
					continue;
				case 6:
					goto IL_173;
				case 7:
					num3 = 3;
					continue;
				}
				break;
				IL_9F:
				num4 = this.ᜂ(A_0 - 1);
				short[] array2 = this.ᜢ.ᜀ();
				int num5 = num4;
				array2[num5] += 1;
				num3 = 0;
				continue;
				IL_13E:
				if (num >= 285)
				{
					goto IL_9F;
				}
				num3 = 4;
			}
		}
		IL_173:
		IL_175:
		if (true)
		{
		}
		return this.ᜇ();
	}

	// Token: 0x06005433 RID: 21555 RVA: 0x0034663C File Offset: 0x0034563C
	internal void ᜇ(int A_0)
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
		this.\u171A[this.\u171B++] = (byte)A_0;
	}

	// Token: 0x06005434 RID: 21556 RVA: 0x00346694 File Offset: 0x00345694
	internal void ᜈ(int A_0)
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
		this.\u171A[this.\u171B++] = (byte)A_0;
		this.\u171A[this.\u171B++] = (byte)(A_0 >> 8);
	}

	// Token: 0x06005435 RID: 21557 RVA: 0x00346708 File Offset: 0x00345708
	internal void ᜆ(int A_0)
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
		this.\u171A[this.\u171B++] = (byte)A_0;
		this.\u171A[this.\u171B++] = (byte)(A_0 >> 8);
		this.\u171A[this.\u171B++] = (byte)(A_0 >> 16);
		this.\u171A[this.\u171B++] = (byte)(A_0 >> 24);
	}

	// Token: 0x06005436 RID: 21558 RVA: 0x003467B4 File Offset: 0x003457B4
	internal void ᜀ(byte[] A_0, int A_1, int A_2)
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
		Array.Copy(A_0, A_1, this.\u171A, this.\u171B, A_2);
		this.\u171B += A_2;
	}

	// Token: 0x06005437 RID: 21559 RVA: 0x00346814 File Offset: 0x00345814
	internal int ᜊ()
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
		return this.\u171D;
	}

	// Token: 0x06005438 RID: 21560 RVA: 0x00346858 File Offset: 0x00345858
	internal void ᜏ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				if (this.\u171D > 8)
				{
					num = 2;
					continue;
				}
				goto IL_D3;
			case 2:
				this.\u171A[this.\u171B++] = (byte)(this.\u171C >> 8);
				num = 3;
				continue;
			case 3:
				goto IL_87;
			case 4:
				this.\u171A[this.\u171B++] = (byte)this.\u171C;
				goto IL_A8;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A8:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (this.\u171D <= 0)
				{
					goto IL_D3;
				}
				num = 4;
				break;
			}
		}
		IL_87:
		IL_D3:
		this.\u171C = 0U;
		this.\u171D = 0;
	}

	// Token: 0x06005439 RID: 21561 RVA: 0x00346948 File Offset: 0x00345948
	internal void ᜁ(int A_0, int A_1)
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
		this.\u171C |= (uint)((uint)A_0 << this.\u171D);
		this.\u171D += A_1;
		this.\u170D();
	}

	// Token: 0x0600543A RID: 21562 RVA: 0x003469B0 File Offset: 0x003459B0
	internal void ᜅ(int A_0)
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
		this.\u171A[this.\u171B++] = (byte)(A_0 >> 8);
		this.\u171A[this.\u171B++] = (byte)A_0;
	}

	// Token: 0x0600543B RID: 21563 RVA: 0x00346A24 File Offset: 0x00345A24
	internal bool ᜋ()
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
		return this.\u171B == 0;
	}

	// Token: 0x0600543C RID: 21564 RVA: 0x00346A68 File Offset: 0x00345A68
	internal void ᜌ()
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
		this.\u170D();
		this.\u1719.Write(this.\u171A, 0, this.\u171B);
		this.\u171B = 0;
		this.\u1719.Flush();
	}

	// Token: 0x0600543D RID: 21565 RVA: 0x00346AD4 File Offset: 0x00345AD4
	internal int \u170D()
	{
		int num;
		for (;;)
		{
			if (true)
			{
			}
			num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.\u171D >= 8)
					{
						num2 = 4;
						continue;
					}
					return num;
				case 1:
					if (this.\u171B >= 65536)
					{
						num2 = 2;
						continue;
					}
					this.\u171A[this.\u171B++] = (byte)this.\u171C;
					this.\u171C >>= 8;
					this.\u171D -= 8;
					num++;
					num2 = 5;
					continue;
				case 2:
					return num;
				case 3:
					goto IL_79;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 5:
					goto IL_79;
				}
				break;
				IL_79:
				num2 = 0;
			}
		}
		return num;
	}

	// Token: 0x0600543E RID: 21566 RVA: 0x00346BC8 File Offset: 0x00345BC8
	internal byte[] ᜎ()
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
		byte[] array = new byte[this.\u171B];
		Array.Copy(this.\u171A, 0, array, 0, array.Length);
		this.\u171B = 0;
		return array;
	}

	// Token: 0x0400274F RID: 10063
	private const int ᜀ = 30720;

	// Token: 0x04002750 RID: 10064
	private const int ᜁ = 8;

	// Token: 0x04002751 RID: 10065
	private const int ᜂ = 65536;

	// Token: 0x04002752 RID: 10066
	private const int ᜃ = 16384;

	// Token: 0x04002753 RID: 10067
	private const int ᜄ = 286;

	// Token: 0x04002754 RID: 10068
	private const int ᜅ = 30;

	// Token: 0x04002755 RID: 10069
	private const int ᜆ = 19;

	// Token: 0x04002756 RID: 10070
	private const int ᜇ = 256;

	// Token: 0x04002757 RID: 10071
	private const int ᜈ = 4096;

	// Token: 0x04002758 RID: 10072
	private const int ᜉ = 32768;

	// Token: 0x04002759 RID: 10073
	public const int ᜊ = 32767;

	// Token: 0x0400275A RID: 10074
	public const int ᜋ = 15;

	// Token: 0x0400275B RID: 10075
	public const int ᜌ = 32768;

	// Token: 0x0400275C RID: 10076
	public const int \u170D = 32767;

	// Token: 0x0400275D RID: 10077
	public const int ᜎ = 258;

	// Token: 0x0400275E RID: 10078
	public const int ᜏ = 3;

	// Token: 0x0400275F RID: 10079
	public const int ᜐ = 5;

	// Token: 0x04002760 RID: 10080
	public const int ᜑ = 262;

	// Token: 0x04002761 RID: 10081
	public const int \u1712 = 32506;

	// Token: 0x04002762 RID: 10082
	public static int[] \u1713;

	// Token: 0x04002763 RID: 10083
	public static int[] \u1714;

	// Token: 0x04002764 RID: 10084
	public static int[] \u1715;

	// Token: 0x04002765 RID: 10085
	public static int[] \u1716;

	// Token: 0x04002766 RID: 10086
	public static int[] \u1717;

	// Token: 0x04002767 RID: 10087
	public static int \u1718;

	// Token: 0x04002768 RID: 10088
	private Stream \u1719;

	// Token: 0x04002769 RID: 10089
	private byte[] \u171A;

	// Token: 0x0400276A RID: 10090
	private int \u171B;

	// Token: 0x0400276B RID: 10091
	private uint \u171C;

	// Token: 0x0400276C RID: 10092
	private int \u171D;

	// Token: 0x0400276D RID: 10093
	private bool \u171E;

	// Token: 0x0400276E RID: 10094
	private long \u171F;

	// Token: 0x0400276F RID: 10095
	private CompressionLevel ᜠ;

	// Token: 0x04002770 RID: 10096
	private spr\u260B ᜡ;

	// Token: 0x04002771 RID: 10097
	private spr\u260B ᜢ;

	// Token: 0x04002772 RID: 10098
	private spr\u260B ᜣ;

	// Token: 0x04002773 RID: 10099
	private int ᜤ;

	// Token: 0x04002774 RID: 10100
	private byte[] ᜥ;

	// Token: 0x04002775 RID: 10101
	private short[] ᜦ;

	// Token: 0x04002776 RID: 10102
	private int ᜧ;

	// Token: 0x04002777 RID: 10103
	private static short[] ᜨ;

	// Token: 0x04002778 RID: 10104
	private static byte[] ᜩ;

	// Token: 0x04002779 RID: 10105
	private static short[] ᜪ;

	// Token: 0x0400277A RID: 10106
	private static byte[] ᜫ;

	// Token: 0x0400277B RID: 10107
	private bool ᜬ;

	// Token: 0x0400277C RID: 10108
	private int ᜭ;

	// Token: 0x0400277D RID: 10109
	private short[] ᜮ;

	// Token: 0x0400277E RID: 10110
	private short[] ᜯ;

	// Token: 0x0400277F RID: 10111
	private int ᜰ;

	// Token: 0x04002780 RID: 10112
	private int ᜱ;

	// Token: 0x04002781 RID: 10113
	private bool \u1732;

	// Token: 0x04002782 RID: 10114
	private int \u1733;

	// Token: 0x04002783 RID: 10115
	private int \u1734;

	// Token: 0x04002784 RID: 10116
	private int \u1735;

	// Token: 0x04002785 RID: 10117
	private byte[] \u1736;

	// Token: 0x04002786 RID: 10118
	private int \u1737;

	// Token: 0x04002787 RID: 10119
	private int \u1738;

	// Token: 0x04002788 RID: 10120
	private int \u1739;

	// Token: 0x04002789 RID: 10121
	private int \u173A;

	// Token: 0x0400278A RID: 10122
	private int \u173B;

	// Token: 0x0400278B RID: 10123
	private byte[] \u173C;

	// Token: 0x0400278C RID: 10124
	private int \u173D;

	// Token: 0x0400278D RID: 10125
	private int \u173E;

	// Token: 0x0400278E RID: 10126
	private int \u173F;

	// Token: 0x0400278F RID: 10127
	private bool ᝀ;

	// Token: 0x0200057A RID: 1402
	private enum BlockType
	{
		// Token: 0x04002791 RID: 10129
		Stored,
		// Token: 0x04002792 RID: 10130
		FixedHuffmanCodes,
		// Token: 0x04002793 RID: 10131
		DynamicHuffmanCodes
	}
}
