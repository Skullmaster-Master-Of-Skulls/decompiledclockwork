using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Compression;

// Token: 0x020003B8 RID: 952
internal class spr\u234C
{
	// Token: 0x060035C2 RID: 13762 RVA: 0x00326134 File Offset: 0x00325134
	static spr\u234C()
	{
		for (;;)
		{
			spr\u234C.\u1713 = new int[]
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
			spr\u234C.\u1714 = new int[]
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
			spr\u234C.\u1715 = new int[]
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
			spr\u234C.\u1716 = new int[]
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
			spr\u234C.\u1717 = new int[]
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
			spr\u234C.\u1718 = Math.Min(65535, 65531);
			spr\u234C.ᜨ = new short[286];
			spr\u234C.ᜩ = new byte[286];
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_1A2;
				case 1:
					if (num >= 256)
					{
						num2 = 7;
						continue;
					}
					spr\u234C.ᜨ[num] = sprᣬ.ᜀ(256 + num << 7);
					spr\u234C.ᜩ[num++] = 9;
					num2 = 14;
					continue;
				case 2:
					goto IL_1E8;
				case 3:
					goto IL_2C1;
				case 4:
					goto IL_13D;
				case 5:
					if (num >= 30)
					{
						num2 = 17;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_31C;
					default:
						if (false)
						{
						}
						spr\u234C.ᜪ[num] = sprᣬ.ᜀ(num << 11);
						spr\u234C.ᜫ[num] = 5;
						num++;
						num2 = 19;
						continue;
					}
					break;
				case 6:
					if (num >= 144)
					{
						num2 = 13;
						continue;
					}
					spr\u234C.ᜨ[num] = sprᣬ.ᜀ(48 + num << 8);
					spr\u234C.ᜩ[num++] = 8;
					num2 = 15;
					continue;
				case 7:
					goto IL_31C;
				case 8:
					if (num >= 286)
					{
						num2 = 16;
						continue;
					}
					spr\u234C.ᜨ[num] = sprᣬ.ᜀ(-88 + num << 8);
					spr\u234C.ᜩ[num++] = 8;
					num2 = 11;
					continue;
				case 9:
					goto IL_1C5;
				case 10:
					num2 = 9;
					continue;
				case 11:
					goto IL_1C5;
				case 12:
					if (num >= 280)
					{
						num2 = 10;
						continue;
					}
					spr\u234C.ᜨ[num] = sprᣬ.ᜀ(-256 + num << 9);
					spr\u234C.ᜩ[num++] = 7;
					num2 = 18;
					continue;
				case 13:
					num2 = 3;
					continue;
				case 14:
					goto IL_2C1;
				case 15:
					goto IL_1A2;
				case 16:
					if (true)
					{
					}
					spr\u234C.ᜪ = new short[30];
					spr\u234C.ᜫ = new byte[30];
					num = 0;
					num2 = 2;
					continue;
				case 17:
					return;
				case 18:
					goto IL_13D;
				case 19:
					goto IL_1E8;
				}
				break;
				IL_13D:
				num2 = 12;
				continue;
				IL_1A2:
				num2 = 6;
				continue;
				IL_1C5:
				num2 = 8;
				continue;
				IL_1E8:
				num2 = 5;
				continue;
				IL_2C1:
				num2 = 1;
				continue;
				IL_31C:
				num2 = 4;
			}
		}
	}

	// Token: 0x060035C3 RID: 13763 RVA: 0x00326470 File Offset: 0x00325470
	internal spr\u234C(Stream A_0, bool A_1, CompressionLevel A_2, bool A_3)
	{
		int a_ = 17;
		this.\u171A = new byte[65536];
		this.\u171F = 1L;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ᡶ౸ེർ੾킂", a_));
		}
		if (!A_0.CanWrite)
		{
			throw new ArgumentException(ClipboardData.b("㡶౸ེർ੾ꎂﮈ놐杖뮚햠莢횤튦\ud9a8\udbaa슬\uddae얰鎲슴얶킸쾺풼톾ꛀ", a_), ClipboardData.b("ᡶ౸ེർ੾킂", a_));
		}
		this.ᜡ = new sprᴂ(this, 286, 257, 15);
		this.ᜢ = new sprᴂ(this, 30, 1, 15);
		this.ᜣ = new sprᴂ(this, 19, 4, 7);
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
		this.\u173A = spr\u234C.\u1713[(int)A_2];
		this.\u1738 = spr\u234C.\u1714[(int)A_2];
		this.\u1739 = spr\u234C.\u1715[(int)A_2];
		this.\u1737 = spr\u234C.\u1716[(int)A_2];
		this.\u173B = spr\u234C.\u1717[(int)A_2];
		if (!A_1)
		{
			this.ᜆ();
		}
	}

	// Token: 0x060035C4 RID: 13764 RVA: 0x003265F8 File Offset: 0x003255F8
	public spr\u234C(Stream A_0, bool A_1, bool A_2) : this(A_0, A_1, CompressionLevel.Normal, A_2)
	{
	}

	// Token: 0x060035C5 RID: 13765 RVA: 0x00326610 File Offset: 0x00325610
	internal spr\u234C(Stream A_0, CompressionLevel A_1, bool A_2) : this(A_0, false, A_1, A_2)
	{
	}

	// Token: 0x060035C6 RID: 13766 RVA: 0x00326628 File Offset: 0x00325628
	public spr\u234C(Stream A_0, bool A_1) : this(A_0, false, CompressionLevel.Normal, A_1)
	{
	}

	// Token: 0x060035C7 RID: 13767 RVA: 0x00326640 File Offset: 0x00325640
	public void ᜂ(byte[] A_0, int A_1, int A_2, bool A_3)
	{
		int a_ = 19;
		int num = 12;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (A_1 <= num2)
				{
					num = 8;
					continue;
				}
				goto IL_1AF;
			case 1:
				this.\u1719.Close();
				num = 13;
				continue;
			case 2:
				if (this.ᝀ)
				{
					num = 1;
					continue;
				}
				goto IL_178;
			case 3:
				goto IL_178;
			case 4:
				return;
			case 5:
				if (this.ᜈ())
				{
					num = 17;
					continue;
				}
				goto IL_2F9;
			case 6:
				num = 25;
				continue;
			case 7:
				if (!this.\u171E)
				{
					num = 16;
					continue;
				}
				goto IL_1C3;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2B5;
				default:
					if (false)
					{
					}
					num = 23;
					continue;
				}
				break;
			case 9:
				return;
			case 10:
				goto IL_2C0;
			case 11:
				if (this.ᜋ())
				{
					num = 9;
					continue;
				}
				goto IL_2F9;
			case 13:
				goto IL_178;
			case 14:
				goto IL_93;
			case 15:
				goto IL_1C3;
			case 16:
				this.ᜅ((int)(this.\u171F >> 16));
				this.ᜅ((int)(this.\u171F & 65535L));
				num = 15;
				continue;
			case 17:
				num = 11;
				continue;
			case 18:
				if (A_2 == 0)
				{
					num = 4;
					continue;
				}
				num = 22;
				continue;
			case 19:
				num = 0;
				continue;
			case 20:
				if (0 <= A_1)
				{
					num = 19;
					continue;
				}
				goto IL_1AF;
			case 21:
				this.ᜌ();
				this.ᜏ();
				num = 7;
				continue;
			case 22:
				if (this.ᜬ)
				{
					goto IL_2B5;
				}
				spr\u2580.ᜀ(ref this.\u171F, this.\u173C, this.\u173E, A_2);
				num = 3;
				continue;
			case 23:
				if (num2 > A_0.Length)
				{
					num = 26;
					continue;
				}
				this.\u173C = A_0;
				this.\u173E = A_1;
				this.\u173F = num2;
				num = 18;
				continue;
			case 24:
				if (!this.ᜀ(A_3))
				{
					num = 6;
					continue;
				}
				goto IL_178;
			case 25:
				if (A_3)
				{
					num = 21;
					continue;
				}
				goto IL_178;
			case 26:
				goto IL_111;
			}
			if (A_0 == null)
			{
				num = 14;
				continue;
			}
			num2 = A_1 + A_2;
			if (true)
			{
			}
			num = 20;
			continue;
			IL_178:
			num = 5;
			continue;
			IL_1C3:
			this.ᜌ();
			this.ᜬ = true;
			num = 2;
			continue;
			IL_2B5:
			num = 10;
			continue;
			IL_2F9:
			this.ᜌ();
			num = 24;
		}
		IL_93:
		throw new ArgumentNullException(ClipboardData.b("ᵸ᩺ॼṾ", a_));
		IL_111:
		IL_1AF:
		throw new ArgumentOutOfRangeException(ClipboardData.b("㙸ᵺ᭼౾ꖄﮈꮊﾐﾖ릘뾞좠춢욤좦\udba8\ud9aa좬첮얰鶲", a_));
		IL_2C0:
		throw new IOException(ClipboardData.b("⩸ེོ᩾ꖄ권﶐ﲒﶘ떚", a_));
	}

	// Token: 0x060035C8 RID: 13768 RVA: 0x00326980 File Offset: 0x00325980
	public void ᜉ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!this.ᜀ(true))
				{
					num = 6;
					continue;
				}
				goto IL_106;
			case 1:
				return;
			case 2:
				this.ᜬ = true;
				num = 14;
				continue;
			case 4:
				if (!this.\u171E)
				{
					num = 13;
					continue;
				}
				goto IL_85;
			case 5:
				goto IL_85;
			case 6:
				this.ᜌ();
				this.ᜏ();
				num = 4;
				continue;
			case 7:
				if (this.ᜈ())
				{
					num = 8;
					continue;
				}
				goto IL_95;
			case 8:
				num = 9;
				continue;
			case 9:
				if (this.ᜋ())
				{
					num = 2;
					continue;
				}
				goto IL_95;
			case 10:
				return;
			case 11:
				goto IL_106;
			case 12:
				this.\u1719.Close();
				num = 1;
				continue;
			case 13:
				goto IL_144;
			case 14:
				if (this.ᝀ)
				{
					num = 12;
					continue;
				}
				return;
			}
			if (this.ᜬ)
			{
				num = 10;
				continue;
			}
			goto IL_95;
			IL_85:
			this.ᜌ();
			num = 11;
			continue;
			IL_95:
			this.ᜌ();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_144:
				this.ᜅ((int)(this.\u171F >> 16));
				this.ᜅ((int)(this.\u171F & 65535L));
				if (true)
				{
				}
				num = 5;
				continue;
			default:
				if (false)
				{
				}
				num = 0;
				continue;
			}
			IL_106:
			num = 7;
		}
	}

	// Token: 0x060035C9 RID: 13769 RVA: 0x00326B3C File Offset: 0x00325B3C
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

	// Token: 0x060035CA RID: 13770 RVA: 0x00326B80 File Offset: 0x00325B80
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

	// Token: 0x060035CB RID: 13771 RVA: 0x00326BCC File Offset: 0x00325BCC
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

	// Token: 0x060035CC RID: 13772 RVA: 0x00326C18 File Offset: 0x00325C18
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

	// Token: 0x060035CD RID: 13773 RVA: 0x00326C7C File Offset: 0x00325C7C
	private void ᜅ()
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					goto IL_A7;
				case 2:
					if (this.\u173E >= this.\u173F)
					{
						num = 4;
						continue;
					}
					num2 = 65536 - this.\u1735 - this.\u1734;
					num = 9;
					continue;
				case 3:
					num2 = this.\u173F - this.\u173E;
					num = 10;
					continue;
				case 4:
					goto IL_14C;
				case 5:
					num = 2;
					continue;
				case 6:
					this.ᜄ();
					num = 11;
					continue;
				case 7:
					if (this.\u1735 >= 3)
					{
						num = 8;
						continue;
					}
					return;
				case 8:
					this.ᜃ();
					if (true)
					{
					}
					num = 13;
					continue;
				case 9:
					if (num2 > this.\u173F - this.\u173E)
					{
						num = 3;
						continue;
					}
					goto IL_170;
				case 10:
					goto IL_170;
				case 11:
					goto IL_A7;
				case 12:
					if (this.\u1735 < 262)
					{
						num = 5;
						continue;
					}
					goto IL_14C;
				case 13:
					return;
				}
				if (this.\u1734 >= 65274)
				{
					num = 6;
					continue;
				}
				IL_A7:
				num = 12;
				continue;
				IL_14C:
				num = 7;
				continue;
				IL_170:
				Array.Copy(this.\u173C, this.\u173E, this.\u1736, this.\u1734 + this.\u1735, num2);
				this.\u173E += num2;
				this.\u173D += num2;
				this.\u1735 += num2;
				num = 1;
			}
		}
	}

	// Token: 0x060035CE RID: 13774 RVA: 0x00326E74 File Offset: 0x00325E74
	private void ᜄ()
	{
		for (;;)
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
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_16F;
						case 1:
							goto IL_16F;
						case 2:
						{
							if (num >= 32768)
							{
								num2 = 5;
								continue;
							}
							int num3 = (int)this.ᜮ[num] & 65535;
							num2 = 9;
							continue;
						}
						case 3:
							goto IL_124;
						case 4:
							goto IL_124;
						case 5:
						{
							int num4 = 0;
							num2 = 1;
							continue;
						}
						case 6:
						{
							int num4;
							int num5;
							this.ᜯ[num4] = (short)((num5 >= 32768) ? (num5 - 32768) : 0);
							num4++;
							num2 = 0;
							continue;
						}
						case 7:
						{
							int num4;
							if (num4 >= 32768)
							{
								num2 = 8;
								continue;
							}
							if (true)
							{
							}
							int num5 = (int)this.ᜯ[num4] & 65535;
							num2 = 6;
							continue;
						}
						case 8:
							goto IL_192;
						case 9:
						{
							int num3;
							this.ᜮ[num] = (short)((num3 >= 32768) ? (num3 - 32768) : 0);
							num++;
							num2 = 3;
							continue;
						}
						}
						break;
						IL_124:
						num2 = 2;
						continue;
						IL_16F:
						num2 = 7;
					}
				}
				IL_192:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_1A8;
				}
				break;
			}
		}
		IL_1A8:
		if (false)
		{
		}
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x00327048 File Offset: 0x00326048
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

	// Token: 0x060035D0 RID: 13776 RVA: 0x003270A8 File Offset: 0x003260A8
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

	// Token: 0x060035D1 RID: 13777 RVA: 0x0032713C File Offset: 0x0032613C
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
				int num8 = 18;
				for (;;)
				{
					switch (num8)
					{
					case 0:
						if ((A_0 = ((int)array[A_0 & 32767] & 65535)) > num6)
						{
							if (true)
							{
							}
							num8 = 22;
							continue;
						}
						goto IL_5C3;
					case 1:
						num8 = 4;
						continue;
					case 2:
						b = this.\u1736[num4 - 1];
						b2 = this.\u1736[num4];
						num8 = 40;
						continue;
					case 3:
						if (--num == 0)
						{
							num8 = 36;
							continue;
						}
						goto IL_3BA;
					case 4:
					{
						int num9;
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 6;
							continue;
						}
						goto IL_59E;
					}
					case 5:
					{
						int num9;
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 9;
							continue;
						}
						goto IL_59E;
					}
					case 6:
						num8 = 24;
						continue;
					case 7:
						num8 = 25;
						continue;
					case 8:
						if (num5 < num2)
						{
							num8 = 2;
							continue;
						}
						goto IL_5C3;
					case 9:
						num8 = 21;
						continue;
					case 10:
						if (this.\u1736[A_0] == this.\u1736[num3])
						{
							num8 = 17;
							continue;
						}
						goto IL_274;
					case 11:
						goto IL_575;
					case 12:
						num8 = 29;
						continue;
					case 13:
					{
						int num9 = A_0 + 2;
						num3 += 2;
						num8 = 27;
						continue;
					}
					case 14:
						num8 = 5;
						continue;
					case 15:
						num >>= 2;
						num8 = 11;
						continue;
					case 16:
						if (this.\u1736[A_0 + 1] == this.\u1736[num3 + 1])
						{
							num8 = 13;
							continue;
						}
						goto IL_274;
					case 17:
						goto IL_31B;
					case 18:
						if (num5 >= this.\u173A)
						{
							num8 = 15;
							continue;
						}
						goto IL_575;
					case 19:
						num8 = 34;
						continue;
					case 20:
						goto IL_59E;
					case 21:
					{
						int num9;
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 12;
							continue;
						}
						goto IL_59E;
					}
					case 22:
						num8 = 3;
						continue;
					case 23:
						num8 = 10;
						continue;
					case 24:
					{
						int num9;
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 14;
							continue;
						}
						goto IL_59E;
					}
					case 25:
					{
						int num9;
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 38;
							continue;
						}
						goto IL_59E;
					}
					case 26:
						if (num2 > this.\u1735)
						{
							num8 = 33;
							continue;
						}
						goto IL_3BA;
					case 27:
						goto IL_35F;
					case 28:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_31B;
						default:
							if (false)
							{
							}
							num8 = 32;
							continue;
						}
						break;
					case 29:
						if (num3 >= num7)
						{
							num8 = 20;
							continue;
						}
						goto IL_35F;
					case 30:
						this.ᜰ = A_0;
						num4 = num3;
						num5 = num3 - this.\u1734;
						num8 = 8;
						continue;
					case 31:
						goto IL_274;
					case 32:
					{
						int num9;
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 1;
							continue;
						}
						goto IL_59E;
					}
					case 33:
						num2 = this.\u1735;
						num8 = 37;
						continue;
					case 34:
						if (this.\u1736[A_0 + num5 - 1] == b)
						{
							num8 = 23;
							continue;
						}
						goto IL_274;
					case 35:
					{
						int num9;
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 28;
							continue;
						}
						goto IL_59E;
					}
					case 36:
						goto IL_35A;
					case 37:
						goto IL_3BA;
					case 38:
						num8 = 35;
						continue;
					case 39:
						if (num3 > num4)
						{
							num8 = 30;
							continue;
						}
						goto IL_320;
					case 40:
						goto IL_320;
					case 41:
						if (this.\u1736[A_0 + num5] == b2)
						{
							num8 = 19;
							continue;
						}
						goto IL_274;
					case 42:
					{
						int num9;
						if (this.\u1736[++num3] == this.\u1736[++num9])
						{
							num8 = 7;
							continue;
						}
						goto IL_59E;
					}
					}
					break;
					IL_274:
					num8 = 0;
					continue;
					IL_31B:
					num8 = 16;
					continue;
					IL_320:
					num3 = this.\u1734;
					num8 = 31;
					continue;
					IL_35F:
					num8 = 42;
					continue;
					IL_3BA:
					num8 = 41;
					continue;
					IL_575:
					num8 = 26;
					continue;
					IL_59E:
					num8 = 39;
				}
			}
			IL_35A:
			IL_5C3:
			this.ᜱ = Math.Min(num5, this.\u1735);
			return this.ᜱ >= 3;
		}
		}
	}

	// Token: 0x060035D2 RID: 13778 RVA: 0x0032772C File Offset: 0x0032672C
	private bool ᜂ(bool A_0, bool A_1)
	{
		int num = 10;
		int num2;
		bool flag;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D7;
			case 1:
				goto IL_FC;
			case 2:
				num = 7;
				continue;
			case 3:
				if (this.\u1733 < 32768)
				{
					num = 14;
					continue;
				}
				goto IL_FC;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_10D;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (num2 > spr\u234C.\u1718)
					{
						num = 6;
						continue;
					}
					goto IL_127;
				}
				break;
			case 5:
				goto IL_5C;
			case 6:
				num2 = spr\u234C.\u1718;
				flag = false;
				num = 0;
				continue;
			case 7:
				if (this.\u1735 == 0)
				{
					num = 8;
					continue;
				}
				goto IL_176;
			case 8:
				return false;
			case 9:
				num = 3;
				continue;
			case 11:
				if (A_0)
				{
					goto IL_10D;
				}
				return true;
			case 12:
				if (num2 < 32506)
				{
					num = 1;
					continue;
				}
				goto IL_5C;
			case 13:
				if (num2 < spr\u234C.\u1718)
				{
					num = 9;
					continue;
				}
				goto IL_5C;
			case 14:
				num = 12;
				continue;
			}
			if (!A_0)
			{
				num = 2;
				continue;
			}
			goto IL_176;
			IL_5C:
			flag = A_1;
			num = 4;
			continue;
			IL_FC:
			num = 11;
			continue;
			IL_10D:
			num = 5;
			continue;
			IL_176:
			this.\u1734 += this.\u1735;
			this.\u1735 = 0;
			num2 = this.\u1734 - this.\u1733;
			num = 13;
		}
		IL_D7:
		IL_127:
		this.ᜁ(this.\u1736, this.\u1733, num2, flag);
		this.\u1733 += num2;
		return !flag;
	}

	// Token: 0x060035D3 RID: 13779 RVA: 0x003278FC File Offset: 0x003268FC
	private bool ᜁ(bool A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				bool flag;
				int num3;
				switch (num)
				{
				case 0:
					if (!A_0)
					{
						num = 11;
						continue;
					}
					goto IL_24B;
				case 1:
					goto IL_35B;
				case 2:
				{
					int num2;
					if (this.ᜄ(num2))
					{
						num = 18;
						continue;
					}
					goto IL_361;
				}
				case 3:
					goto IL_42B;
				case 4:
					if (!A_0)
					{
						num = 28;
						continue;
					}
					goto IL_55C;
				case 5:
				{
					int num2;
					if ((num2 = this.ᜂ()) != 0)
					{
						num = 31;
						continue;
					}
					goto IL_361;
				}
				case 6:
					if (A_1)
					{
						num = 30;
						continue;
					}
					num = 1;
					continue;
				case 7:
					goto IL_3BF;
				case 8:
					if (this.\u1735 >= 3)
					{
						num = 15;
						continue;
					}
					goto IL_361;
				case 9:
					num = 34;
					continue;
				case 10:
					num = 0;
					continue;
				case 11:
					return true;
				case 12:
					this.ᜃ();
					num = 23;
					continue;
				case 13:
					goto IL_5A8;
				case 14:
					if (this.\u1735 >= 3)
					{
						num = 38;
						continue;
					}
					goto IL_313;
				case 15:
					goto IL_1F5;
				case 17:
					if (this.\u1735 >= 2)
					{
						num = 12;
						continue;
					}
					goto IL_233;
				case 18:
					num = 42;
					continue;
				case 19:
				{
					int num2;
					if (this.\u1734 - num2 <= 32506)
					{
						num = 35;
						continue;
					}
					goto IL_361;
				}
				case 20:
					if (this.ᜇ())
					{
						num = 46;
						continue;
					}
					goto IL_55C;
				case 21:
					this.ᜄ();
					num = 7;
					continue;
				case 22:
					if (this.\u1734 > 65274)
					{
						num = 21;
						continue;
					}
					goto IL_3BF;
				case 23:
					goto IL_233;
				case 24:
					if (true)
					{
					}
					if (this.\u1735 == 0)
					{
						num = 25;
						continue;
					}
					num = 22;
					continue;
				case 25:
					goto IL_276;
				case 26:
					goto IL_55C;
				case 27:
					this.\u1734++;
					num = 37;
					continue;
				case 28:
					return false;
				case 29:
					flag = false;
					goto IL_4F5;
				case 30:
					num = 45;
					continue;
				case 31:
					num = 19;
					continue;
				case 32:
					flag = (this.\u1735 == 0);
					goto IL_4F5;
				case 33:
					num = 32;
					continue;
				case 34:
					if (A_1)
					{
						num = 33;
						continue;
					}
					num = 29;
					continue;
				case 35:
					num = 2;
					continue;
				case 36:
					if (num3 <= 0)
					{
						num = 27;
						continue;
					}
					this.\u1734++;
					this.ᜂ();
					num = 39;
					continue;
				case 37:
					goto IL_233;
				case 38:
					num = 13;
					continue;
				case 39:
					goto IL_5A8;
				case 40:
					num = 14;
					continue;
				case 41:
					if (this.ᜱ <= this.\u1738)
					{
						num = 40;
						continue;
					}
					goto IL_313;
				case 42:
					if (this.ᜀ(this.\u1734 - this.ᜰ, this.ᜱ))
					{
						num = 9;
						continue;
					}
					goto IL_42B;
				case 43:
					num = 4;
					continue;
				case 44:
					if (this.\u1735 < 262)
					{
						num = 10;
						continue;
					}
					goto IL_24B;
				case 45:
					goto IL_15E;
				case 46:
					num = 6;
					continue;
				}
				if (this.\u1735 < 262)
				{
					num = 43;
					continue;
				}
				goto IL_55C;
				IL_1F5:
				num = 5;
				continue;
				IL_42B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1F5;
				default:
					if (false)
					{
					}
					this.\u1735 -= this.ᜱ;
					num = 41;
					continue;
				}
				IL_233:
				this.ᜱ = 2;
				num = 26;
				continue;
				IL_24B:
				num = 24;
				continue;
				IL_313:
				this.\u1734 += this.ᜱ;
				num = 17;
				continue;
				IL_361:
				this.ᜀ((int)(this.\u1736[this.\u1734] & byte.MaxValue));
				this.\u1734++;
				this.\u1735--;
				num = 20;
				continue;
				IL_3BF:
				num = 8;
				continue;
				IL_4F5:
				bool a_ = flag;
				this.ᜀ(this.\u1736, this.\u1733, this.\u1734 - this.\u1733, a_);
				this.\u1733 = this.\u1734;
				num = 3;
				continue;
				IL_55C:
				num = 44;
				continue;
				IL_5A8:
				num3 = --this.ᜱ;
				num = 36;
			}
			IL_15E:
			bool flag2 = this.\u1735 == 0;
			IL_1C3:
			bool flag3 = flag2;
			this.ᜀ(this.\u1736, this.\u1733, this.\u1734 - this.\u1733, flag3);
			this.\u1733 = this.\u1734;
			return !flag3;
			IL_276:
			this.ᜀ(this.\u1736, this.\u1733, this.\u1734 - this.\u1733, A_1);
			this.\u1733 = this.\u1734;
			return false;
			IL_35B:
			flag2 = false;
			goto IL_1C3;
		}
		}
	}

	// Token: 0x060035D4 RID: 13780 RVA: 0x00327EE8 File Offset: 0x00326EE8
	private bool ᜀ(bool A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 34;
			int num3;
			for (;;)
			{
				int num2;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_529;
				case 1:
					if (this.ᜱ <= num2)
					{
						num = 23;
						continue;
					}
					goto IL_577;
				case 2:
					this.ᜀ((int)(this.\u1736[this.\u1734 - 1] & byte.MaxValue));
					num = 46;
					continue;
				case 3:
					this.ᜄ();
					num = 42;
					continue;
				case 4:
					num = 35;
					continue;
				case 5:
					if (this.\u1734 - this.ᜰ > 4096)
					{
						num = 28;
						continue;
					}
					goto IL_529;
				case 6:
					if (num2 >= 3)
					{
						num = 39;
						continue;
					}
					goto IL_577;
				case 7:
					num = 32;
					continue;
				case 8:
					if (this.ᜱ == 3)
					{
						num = 15;
						continue;
					}
					goto IL_529;
				case 9:
					num3--;
					num = 43;
					continue;
				case 10:
					if (this.\u1732)
					{
						num = 9;
						continue;
					}
					goto IL_285;
				case 11:
					this.ᜀ((int)(this.\u1736[this.\u1734 - 1] & byte.MaxValue));
					num = 24;
					continue;
				case 12:
					if (this.ᜱ <= 5)
					{
						if (true)
						{
						}
						num = 47;
						continue;
					}
					goto IL_529;
				case 13:
					if (this.\u1735 == 0)
					{
						num = 14;
						continue;
					}
					num = 56;
					continue;
				case 14:
					num = 27;
					continue;
				case 15:
					num = 5;
					continue;
				case 16:
					if (!A_0)
					{
						num = 21;
						continue;
					}
					goto IL_4FC;
				case 17:
					if (this.\u1735 >= 3)
					{
						num = 53;
						continue;
					}
					goto IL_22E;
				case 18:
					goto IL_2A5;
				case 19:
					if (this.\u1735 >= 3)
					{
						num = 31;
						continue;
					}
					goto IL_529;
				case 20:
					num = 26;
					continue;
				case 21:
					return false;
				case 22:
				{
					int num4;
					if (this.ᜄ(num4))
					{
						num = 38;
						continue;
					}
					goto IL_529;
				}
				case 23:
					this.ᜀ(this.\u1734 - 1 - num5, num2);
					num2 -= 2;
					num = 50;
					continue;
				case 24:
					goto IL_404;
				case 25:
					if (A_1)
					{
						num = 20;
						continue;
					}
					goto IL_128;
				case 26:
					if (this.\u1735 != 0)
					{
						goto IL_128;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_33F;
					default:
						if (false)
						{
						}
						num = 55;
						continue;
					}
					break;
				case 27:
					if (this.\u1732)
					{
						num = 11;
						continue;
					}
					goto IL_171;
				case 28:
					this.ᜱ = 2;
					num = 0;
					continue;
				case 29:
					num = 22;
					continue;
				case 30:
					if (this.\u1735 < 262)
					{
						num = 4;
						continue;
					}
					goto IL_1DC;
				case 31:
				{
					int num4 = this.ᜂ();
					num = 45;
					continue;
				}
				case 32:
				{
					int num4;
					if (this.\u1734 - num4 <= 32506)
					{
						num = 29;
						continue;
					}
					goto IL_529;
				}
				case 33:
					goto IL_134;
				case 35:
					if (!A_0)
					{
						num = 44;
						continue;
					}
					goto IL_1DC;
				case 36:
					if (--num2 <= 0)
					{
						num = 37;
						continue;
					}
					goto IL_33F;
				case 37:
					this.\u1734++;
					this.\u1735--;
					this.\u1732 = false;
					this.ᜱ = 2;
					num = 49;
					continue;
				case 38:
					num = 12;
					continue;
				case 39:
					num = 1;
					continue;
				case 40:
					if (this.ᜇ())
					{
						num = 54;
						continue;
					}
					goto IL_4FC;
				case 41:
					num = 16;
					continue;
				case 42:
					goto IL_13A;
				case 43:
					goto IL_285;
				case 44:
					return true;
				case 45:
				{
					int num4;
					if (num4 != 0)
					{
						num = 7;
						continue;
					}
					goto IL_529;
				}
				case 46:
					goto IL_409;
				case 47:
					num = 8;
					continue;
				case 48:
					goto IL_31A;
				case 49:
					goto IL_2A5;
				case 50:
					goto IL_33F;
				case 51:
					if (this.\u1732)
					{
						num = 2;
						continue;
					}
					goto IL_409;
				case 52:
					goto IL_22E;
				case 53:
					this.ᜂ();
					num = 52;
					continue;
				case 54:
					num3 = this.\u1734 - this.\u1733;
					num = 10;
					continue;
				case 55:
					num = 48;
					continue;
				case 56:
					if (this.\u1734 >= 65274)
					{
						num = 3;
						continue;
					}
					goto IL_13A;
				}
				if (this.\u1735 < 262)
				{
					num = 41;
					continue;
				}
				goto IL_4FC;
				IL_128:
				num = 33;
				continue;
				IL_13A:
				num5 = this.ᜰ;
				num2 = this.ᜱ;
				num = 19;
				continue;
				IL_1DC:
				num = 13;
				continue;
				IL_22E:
				num = 36;
				continue;
				IL_285:
				num = 25;
				continue;
				IL_2A5:
				num = 40;
				continue;
				IL_33F:
				this.\u1734++;
				this.\u1735--;
				num = 17;
				continue;
				IL_409:
				this.\u1732 = true;
				this.\u1734++;
				this.\u1735--;
				num = 18;
				continue;
				IL_4FC:
				num = 30;
				continue;
				IL_529:
				num = 6;
				continue;
				IL_577:
				num = 51;
			}
			IL_134:
			bool flag = false;
			goto IL_59F;
			IL_171:
			this.\u1732 = false;
			this.ᜀ(this.\u1736, this.\u1733, this.\u1734 - this.\u1733, A_1);
			this.\u1733 = this.\u1734;
			return false;
			IL_31A:
			flag = !this.\u1732;
			goto IL_59F;
			IL_404:
			goto IL_171;
			IL_59F:
			bool flag2 = flag;
			this.ᜀ(this.\u1736, this.\u1733, num3, flag2);
			this.\u1733 += num3;
			return !flag2;
		}
		}
	}

	// Token: 0x060035D5 RID: 13781 RVA: 0x003285BC File Offset: 0x003275BC
	private bool ᜀ(bool A_0)
	{
		int a_ = 11;
		bool flag2;
		for (;;)
		{
			for (;;)
			{
				if (true)
				{
				}
				this.ᜅ();
				int num = 12;
				for (;;)
				{
					bool flag;
					int u173B;
					bool a_2;
					switch (num)
					{
					case 0:
						num = 10;
						continue;
					case 1:
						goto IL_119;
					case 2:
						goto IL_B1;
					case 3:
						goto IL_142;
					case 4:
						num = 5;
						continue;
					case 5:
						flag = this.ᜈ();
						goto IL_147;
					case 6:
						if (this.ᜋ())
						{
							num = 0;
							continue;
						}
						return flag2;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_182;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 8:
						switch (u173B)
						{
						case 0:
							flag2 = this.ᜂ(a_2, A_0);
							goto IL_182;
						case 1:
							flag2 = this.ᜁ(a_2, A_0);
							num = 2;
							continue;
						case 2:
							flag2 = this.ᜀ(a_2, A_0);
							num = 9;
							continue;
						default:
							num = 7;
							continue;
						}
						break;
					case 9:
						goto IL_B1;
					case 10:
						if (!flag2)
						{
							num = 1;
							continue;
						}
						break;
					case 11:
						flag = false;
						goto IL_147;
					case 12:
						if (A_0)
						{
							num = 4;
							continue;
						}
						num = 11;
						continue;
					case 13:
						goto IL_B1;
					}
					break;
					IL_B1:
					num = 6;
					continue;
					IL_147:
					a_2 = flag;
					u173B = this.\u173B;
					num = 8;
					continue;
					IL_182:
					num = 13;
				}
			}
		}
		IL_119:
		return flag2;
		IL_142:
		throw new InvalidOperationException(ClipboardData.b("ѰᵲṴ᥶ᙸ౺፼彾\udc82욄ﮊﾌﲔ\udd9a슠힢첤좦잨", a_));
	}

	// Token: 0x060035D6 RID: 13782 RVA: 0x0032875C File Offset: 0x0032775C
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

	// Token: 0x060035D7 RID: 13783 RVA: 0x003287C8 File Offset: 0x003277C8
	private int ᜃ(int A_0)
	{
		int num = 3;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return 285;
			case 1:
				goto IL_A1;
			case 2:
				goto IL_87;
			case 4:
				goto IL_87;
			case 5:
				if (A_0 < 8)
				{
					goto IL_96;
				}
				num2 += 4;
				A_0 >>= 1;
				num = 2;
				continue;
			}
			if (A_0 != 255)
			{
				num2 = 257;
				num = 4;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_96;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 0;
				continue;
			}
			IL_87:
			num = 5;
			continue;
			IL_96:
			num = 1;
		}
		return 285;
		IL_A1:
		return num2 + A_0;
	}

	// Token: 0x060035D8 RID: 13784 RVA: 0x00328888 File Offset: 0x00327888
	private int ᜂ(int A_0)
	{
		int num2;
		for (;;)
		{
			IL_18:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_69:
				num = 1;
				break;
			case 1:
				goto IL_38;
			default:
				goto IL_38;
			}
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					goto IL_4A;
				case 1:
					goto IL_4A;
				case 2:
					if (A_0 < 4)
					{
						num = 3;
						continue;
					}
					goto IL_60;
				case 3:
					goto IL_5E;
				}
				goto IL_18;
				IL_4A:
				num = 2;
			}
			IL_60:
			num2 += 2;
			A_0 >>= 1;
			goto IL_69;
			IL_38:
			if (false)
			{
			}
			num2 = 0;
			num = 0;
			goto IL_02;
		}
		IL_5E:
		if (true)
		{
		}
		return num2 + A_0;
	}

	// Token: 0x060035D9 RID: 13785 RVA: 0x00328914 File Offset: 0x00327914
	private void ᜁ(int A_0)
	{
		for (;;)
		{
			IL_18:
			if (true)
			{
			}
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_E6:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				this.ᜣ.ᜅ();
				this.ᜡ.ᜅ();
				this.ᜢ.ᜅ();
				this.ᜁ(this.ᜡ.ᜆ() - 257, 5);
				this.ᜁ(this.ᜢ.ᜆ() - 1, 5);
				this.ᜁ(A_0 - 4, 4);
				num2 = 0;
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (num2 >= A_0)
					{
						num = 1;
						continue;
					}
					goto IL_C8;
				case 1:
					goto IL_C6;
				case 2:
					goto IL_AC;
				case 3:
					goto IL_AC;
				}
				goto IL_18;
				IL_AC:
				num = 0;
			}
			IL_C8:
			this.ᜁ((int)this.ᜣ.ᜃ()[sprᣬ.ᜁ[num2]], 3);
			num2++;
			goto IL_E6;
		}
		IL_C6:
		this.ᜡ.ᜀ(this.ᜣ);
		this.ᜢ.ᜀ(this.ᜣ);
	}

	// Token: 0x060035DA RID: 13786 RVA: 0x00328A38 File Offset: 0x00327A38
	private void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 5;
				for (;;)
				{
					int num5;
					int num6;
					switch (num2)
					{
					case 0:
						num2 = 7;
						continue;
					case 1:
					{
						int num4;
						int num3 = this.ᜃ(num4);
						this.ᜡ.ᜀ(num3);
						num5 = (num3 - 261) / 4;
						num2 = 3;
						continue;
					}
					case 2:
						this.ᜁ(num6 & (1 << num5) - 1, num5);
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_ED;
						default:
							if (false)
							{
							}
							num2 = 6;
							continue;
						}
						break;
					case 3:
						if (num5 > 0)
						{
							goto IL_ED;
						}
						goto IL_7D;
					case 4:
					{
						if (num6-- != 0)
						{
							num2 = 1;
							continue;
						}
						int num4;
						this.ᜡ.ᜀ(num4);
						num2 = 9;
						continue;
					}
					case 5:
						goto IL_19A;
					case 6:
						goto IL_FE;
					case 7:
						if (num5 <= 5)
						{
							num2 = 14;
							continue;
						}
						goto IL_7D;
					case 8:
						goto IL_7D;
					case 9:
						goto IL_FE;
					case 10:
						if (num5 > 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_FE;
					case 11:
					{
						if (num >= this.ᜤ)
						{
							num2 = 12;
							continue;
						}
						int num4 = (int)(this.ᜥ[num] & byte.MaxValue);
						num6 = (int)this.ᜦ[num];
						num2 = 4;
						continue;
					}
					case 12:
						goto IL_1BE;
					case 13:
						goto IL_19A;
					case 14:
					{
						int num4;
						this.ᜁ(num4 & (1 << num5) - 1, num5);
						num2 = 8;
						continue;
					}
					}
					break;
					IL_7D:
					int num7 = this.ᜂ(num6);
					this.ᜢ.ᜀ(num7);
					num5 = num7 / 2 - 1;
					num2 = 10;
					continue;
					IL_ED:
					num2 = 0;
					continue;
					IL_FE:
					num++;
					num2 = 13;
					continue;
					IL_19A:
					num2 = 11;
				}
			}
			IL_1BE:
			this.ᜡ.ᜀ(256);
			return;
		}
	}

	// Token: 0x060035DB RID: 13787 RVA: 0x00328C68 File Offset: 0x00327C68
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

	// Token: 0x060035DC RID: 13788 RVA: 0x00328CDC File Offset: 0x00327CDC
	private void ᜀ(byte[] A_0, int A_1, int A_2, bool A_3)
	{
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
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
				if (true)
				{
				}
				int num4 = 12;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						num2 = num3 + 1;
						num4 = 19;
						continue;
					case 1:
						goto IL_268;
					case 2:
					{
						int num5;
						if (num5 >= 30)
						{
							num4 = 17;
							continue;
						}
						int num6;
						num6 += (int)(this.ᜢ.ᜀ()[num5] * (short)spr\u234C.ᜫ[num5]);
						num5++;
						num4 = 8;
						continue;
					}
					case 3:
						goto IL_38E;
					case 4:
						goto IL_336;
					case 5:
					{
						int num6;
						int num7;
						if (num7 >= num6)
						{
							num4 = 22;
							continue;
						}
						goto IL_2C0;
					}
					case 6:
					{
						int num6;
						int num7;
						if (num7 == num6)
						{
							num4 = 21;
							continue;
						}
						num4 = 3;
						continue;
					}
					case 7:
						goto IL_2C0;
					case 8:
						goto IL_268;
					case 9:
						if (this.ᜣ.ᜃ()[sprᣬ.ᜁ[num3]] > 0)
						{
							num4 = 0;
							continue;
						}
						goto IL_142;
					case 10:
						if (A_1 >= 0)
						{
							num4 = 24;
							continue;
						}
						goto IL_30C;
					case 11:
					{
						int num7 = 14 + num2 * 3 + this.ᜣ.ᜄ() + this.ᜡ.ᜄ() + this.ᜢ.ᜄ() + this.ᜧ;
						int num6 = this.ᜧ;
						int num8 = 0;
						num4 = 20;
						continue;
					}
					case 12:
						goto IL_FE;
					case 13:
						goto IL_206;
					case 14:
						goto IL_2FE;
					case 15:
					{
						int num5 = 0;
						num4 = 1;
						continue;
					}
					case 16:
					{
						int num8;
						if (num8 >= 286)
						{
							num4 = 15;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FE;
						default:
						{
							if (false)
							{
							}
							int num6;
							num6 += (int)(this.ᜡ.ᜀ()[num8] * (short)spr\u234C.ᜩ[num8]);
							num8++;
							num4 = 13;
							continue;
						}
						}
						break;
					}
					case 17:
						num4 = 5;
						continue;
					case 18:
						goto IL_154;
					case 19:
						goto IL_142;
					case 20:
						goto IL_206;
					case 21:
						num4 = 4;
						continue;
					case 22:
					{
						int num6;
						int num7 = num6;
						num4 = 7;
						continue;
					}
					case 23:
						if (num3 <= num2)
						{
							num4 = 11;
							continue;
						}
						num4 = 9;
						continue;
					case 24:
						num4 = 25;
						continue;
					case 25:
					{
						int num7;
						if (A_2 + 4 < num7 >> 3)
						{
							num4 = 14;
							continue;
						}
						goto IL_30C;
					}
					}
					break;
					IL_142:
					num3--;
					num4 = 18;
					continue;
					IL_154:
					num4 = 23;
					continue;
					IL_FE:
					goto IL_154;
					IL_206:
					num4 = 16;
					continue;
					IL_268:
					num4 = 2;
					continue;
					IL_2C0:
					num4 = 10;
					continue;
					IL_30C:
					num4 = 6;
				}
			}
			IL_2FE:
			this.ᜁ(A_0, A_1, A_2, A_3);
			return;
			IL_336:
			this.ᜁ(2 + (A_3 ? 1 : 0), 3);
			this.ᜡ.ᜀ(spr\u234C.ᜨ, spr\u234C.ᜩ);
			this.ᜢ.ᜀ(spr\u234C.ᜪ, spr\u234C.ᜫ);
			this.ᜀ();
			this.ᜁ();
			return;
			IL_38E:
			this.ᜁ(4 + (A_3 ? 1 : 0), 3);
			this.ᜁ(num2);
			this.ᜀ();
			this.ᜁ();
			return;
		}
		}
	}

	// Token: 0x060035DD RID: 13789 RVA: 0x003290A0 File Offset: 0x003280A0
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

	// Token: 0x060035DE RID: 13790 RVA: 0x00329128 File Offset: 0x00328128
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
			int num3 = 1;
			for (;;)
			{
				int num4;
				switch (num3)
				{
				case 0:
					if (num4 >= 4)
					{
						num3 = 7;
						continue;
					}
					goto IL_175;
				case 1:
					if (num >= 265)
					{
						num3 = 5;
						continue;
					}
					goto IL_9F;
				case 2:
					goto IL_9F;
				case 3:
					goto IL_173;
				case 4:
					this.ᜧ += (num - 261) / 4;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9F;
					default:
						if (false)
						{
						}
						num3 = 2;
						continue;
					}
					break;
				case 5:
					num3 = 6;
					continue;
				case 6:
					if (num < 285)
					{
						num3 = 4;
						continue;
					}
					goto IL_9F;
				case 7:
					this.ᜧ += num4 / 2 - 1;
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
			}
		}
		IL_173:
		IL_175:
		if (true)
		{
		}
		return this.ᜇ();
	}

	// Token: 0x060035DF RID: 13791 RVA: 0x003292B8 File Offset: 0x003282B8
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

	// Token: 0x060035E0 RID: 13792 RVA: 0x00329310 File Offset: 0x00328310
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

	// Token: 0x060035E1 RID: 13793 RVA: 0x00329384 File Offset: 0x00328384
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

	// Token: 0x060035E2 RID: 13794 RVA: 0x00329430 File Offset: 0x00328430
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

	// Token: 0x060035E3 RID: 13795 RVA: 0x00329490 File Offset: 0x00328490
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

	// Token: 0x060035E4 RID: 13796 RVA: 0x003294D4 File Offset: 0x003284D4
	internal void ᜏ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_87;
			case 1:
				for (;;)
				{
					this.\u171A[this.\u171B++] = (byte)(this.\u171C >> 8);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_79;
					}
				}
				IL_79:
				if (false)
				{
				}
				num = 0;
				continue;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				this.\u171A[this.\u171B++] = (byte)this.\u171C;
				num = 4;
				continue;
			case 4:
				if (this.\u171D > 8)
				{
					num = 1;
					continue;
				}
				goto IL_D6;
			}
			if (this.\u171D <= 0)
			{
				break;
			}
			num = 3;
		}
		IL_87:
		IL_D6:
		this.\u171C = 0U;
		this.\u171D = 0;
	}

	// Token: 0x060035E5 RID: 13797 RVA: 0x003295C8 File Offset: 0x003285C8
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

	// Token: 0x060035E6 RID: 13798 RVA: 0x00329630 File Offset: 0x00328630
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

	// Token: 0x060035E7 RID: 13799 RVA: 0x003296A4 File Offset: 0x003286A4
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

	// Token: 0x060035E8 RID: 13800 RVA: 0x003296E8 File Offset: 0x003286E8
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

	// Token: 0x060035E9 RID: 13801 RVA: 0x00329754 File Offset: 0x00328754
	internal int \u170D()
	{
		int num;
		for (;;)
		{
			if (true)
			{
			}
			num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.\u171B >= 65536)
					{
						num2 = 5;
						continue;
					}
					this.\u171A[this.\u171B++] = (byte)this.\u171C;
					this.\u171C >>= 8;
					this.\u171D -= 8;
					num++;
					num2 = 3;
					continue;
				case 1:
					goto IL_56;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E4;
					default:
						if (false)
						{
						}
						if (this.\u171D >= 8)
						{
							num2 = 4;
							continue;
						}
						return num;
					}
					break;
				case 3:
					goto IL_E4;
				case 4:
					num2 = 0;
					continue;
				case 5:
					return num;
				}
				break;
				IL_56:
				num2 = 2;
				continue;
				IL_E4:
				goto IL_56;
			}
		}
		return num;
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x0032984C File Offset: 0x0032884C
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

	// Token: 0x040028FE RID: 10494
	private const int ᜀ = 30720;

	// Token: 0x040028FF RID: 10495
	private const int ᜁ = 8;

	// Token: 0x04002900 RID: 10496
	private const int ᜂ = 65536;

	// Token: 0x04002901 RID: 10497
	private const int ᜃ = 16384;

	// Token: 0x04002902 RID: 10498
	private const int ᜄ = 286;

	// Token: 0x04002903 RID: 10499
	private const int ᜅ = 30;

	// Token: 0x04002904 RID: 10500
	private const int ᜆ = 19;

	// Token: 0x04002905 RID: 10501
	private const int ᜇ = 256;

	// Token: 0x04002906 RID: 10502
	private const int ᜈ = 4096;

	// Token: 0x04002907 RID: 10503
	private const int ᜉ = 32768;

	// Token: 0x04002908 RID: 10504
	public const int ᜊ = 32767;

	// Token: 0x04002909 RID: 10505
	public const int ᜋ = 15;

	// Token: 0x0400290A RID: 10506
	public const int ᜌ = 32768;

	// Token: 0x0400290B RID: 10507
	public const int \u170D = 32767;

	// Token: 0x0400290C RID: 10508
	public const int ᜎ = 258;

	// Token: 0x0400290D RID: 10509
	public const int ᜏ = 3;

	// Token: 0x0400290E RID: 10510
	public const int ᜐ = 5;

	// Token: 0x0400290F RID: 10511
	public const int ᜑ = 262;

	// Token: 0x04002910 RID: 10512
	public const int \u1712 = 32506;

	// Token: 0x04002911 RID: 10513
	public static int[] \u1713;

	// Token: 0x04002912 RID: 10514
	public static int[] \u1714;

	// Token: 0x04002913 RID: 10515
	public static int[] \u1715;

	// Token: 0x04002914 RID: 10516
	public static int[] \u1716;

	// Token: 0x04002915 RID: 10517
	public static int[] \u1717;

	// Token: 0x04002916 RID: 10518
	public static int \u1718;

	// Token: 0x04002917 RID: 10519
	private Stream \u1719;

	// Token: 0x04002918 RID: 10520
	private byte[] \u171A;

	// Token: 0x04002919 RID: 10521
	private int \u171B;

	// Token: 0x0400291A RID: 10522
	private uint \u171C;

	// Token: 0x0400291B RID: 10523
	private int \u171D;

	// Token: 0x0400291C RID: 10524
	private bool \u171E;

	// Token: 0x0400291D RID: 10525
	private long \u171F;

	// Token: 0x0400291E RID: 10526
	private CompressionLevel ᜠ;

	// Token: 0x0400291F RID: 10527
	private sprᴂ ᜡ;

	// Token: 0x04002920 RID: 10528
	private sprᴂ ᜢ;

	// Token: 0x04002921 RID: 10529
	private sprᴂ ᜣ;

	// Token: 0x04002922 RID: 10530
	private int ᜤ;

	// Token: 0x04002923 RID: 10531
	private byte[] ᜥ;

	// Token: 0x04002924 RID: 10532
	private short[] ᜦ;

	// Token: 0x04002925 RID: 10533
	private int ᜧ;

	// Token: 0x04002926 RID: 10534
	private static short[] ᜨ;

	// Token: 0x04002927 RID: 10535
	private static byte[] ᜩ;

	// Token: 0x04002928 RID: 10536
	private static short[] ᜪ;

	// Token: 0x04002929 RID: 10537
	private static byte[] ᜫ;

	// Token: 0x0400292A RID: 10538
	private bool ᜬ;

	// Token: 0x0400292B RID: 10539
	private int ᜭ;

	// Token: 0x0400292C RID: 10540
	private short[] ᜮ;

	// Token: 0x0400292D RID: 10541
	private short[] ᜯ;

	// Token: 0x0400292E RID: 10542
	private int ᜰ;

	// Token: 0x0400292F RID: 10543
	private int ᜱ;

	// Token: 0x04002930 RID: 10544
	private bool \u1732;

	// Token: 0x04002931 RID: 10545
	private int \u1733;

	// Token: 0x04002932 RID: 10546
	private int \u1734;

	// Token: 0x04002933 RID: 10547
	private int \u1735;

	// Token: 0x04002934 RID: 10548
	private byte[] \u1736;

	// Token: 0x04002935 RID: 10549
	private int \u1737;

	// Token: 0x04002936 RID: 10550
	private int \u1738;

	// Token: 0x04002937 RID: 10551
	private int \u1739;

	// Token: 0x04002938 RID: 10552
	private int \u173A;

	// Token: 0x04002939 RID: 10553
	private int \u173B;

	// Token: 0x0400293A RID: 10554
	private byte[] \u173C;

	// Token: 0x0400293B RID: 10555
	private int \u173D;

	// Token: 0x0400293C RID: 10556
	private int \u173E;

	// Token: 0x0400293D RID: 10557
	private int \u173F;

	// Token: 0x0400293E RID: 10558
	private bool ᝀ;

	// Token: 0x020003B9 RID: 953
	private enum BlockType
	{
		// Token: 0x04002940 RID: 10560
		Stored,
		// Token: 0x04002941 RID: 10561
		FixedHuffmanCodes,
		// Token: 0x04002942 RID: 10562
		DynamicHuffmanCodes
	}
}
