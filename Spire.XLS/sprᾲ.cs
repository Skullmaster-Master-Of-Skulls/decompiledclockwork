using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200057B RID: 1403
internal class sprᾲ
{
	// Token: 0x0600543F RID: 21567 RVA: 0x00346C28 File Offset: 0x00345C28
	public sprᾲ(Stream A_0) : this(A_0, false)
	{
	}

	// Token: 0x06005440 RID: 21568 RVA: 0x00346C40 File Offset: 0x00345C40
	public sprᾲ(Stream A_0, bool A_1)
	{
		int a_ = 8;
		this.\u1712 = 1L;
		this.\u1715 = new byte[4];
		this.\u1716 = new byte[65535];
		this.\u171D = true;
		this.\u171E = true;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽㐿ぁ⅃❅╇", a_));
		}
		if (A_0.Length == 0L)
		{
			throw new ArgumentException(RecordTableEnumerator.b("䴽㐿ぁ⅃❅╇橉態湍⍏♑♓㽕㙗㵙籛㵝şౡ䑣ࡥݧṩ䱫౭ᕯ剱ᅳ᭵ࡷ๹ջ", a_));
		}
		this.ᜑ = A_0;
		this.\u1717 = A_1;
		if (!this.\u1717)
		{
			this.ᜊ();
		}
		this.ᜉ();
	}

	// Token: 0x06005441 RID: 21569 RVA: 0x00346CF0 File Offset: 0x00345CF0
	protected internal int ᜁ()
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
		return this.\u1714;
	}

	// Token: 0x06005442 RID: 21570 RVA: 0x00346D34 File Offset: 0x00345D34
	protected internal long ᜃ()
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
		return this.ᜑ.Length - this.ᜑ.Position + (long)this.\u1714 >> 3;
	}

	// Token: 0x06005443 RID: 21571 RVA: 0x00346D90 File Offset: 0x00345D90
	protected void ᜈ()
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
		this.\u1712 = 1L;
	}

	// Token: 0x06005444 RID: 21572 RVA: 0x00346DD4 File Offset: 0x00345DD4
	protected void ᜁ(byte[] A_0, int A_1, int A_2)
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
		sprṼ.ᜀ(ref this.\u1712, A_0, A_1, A_2);
	}

	// Token: 0x06005445 RID: 21573 RVA: 0x00346E20 File Offset: 0x00345E20
	protected internal void ᜄ()
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
		this.\u1713 >>= (this.\u1714 & 7);
		this.\u1714 &= -8;
	}

	// Token: 0x06005446 RID: 21574 RVA: 0x00346E84 File Offset: 0x00345E84
	protected internal int ᜂ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 17;
		int num = 19;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_182;
			case 1:
				goto IL_11C;
			case 2:
				if ((this.\u1714 & 7) != 0)
				{
					num = 10;
					continue;
				}
				num = 16;
				continue;
			case 3:
				goto IL_1C2;
			case 4:
				goto IL_2E4;
			case 5:
				num2 += this.ᜑ.Read(A_0, A_1, A_2);
				goto IL_1B7;
			case 6:
				goto IL_182;
			case 7:
				goto IL_1E8;
			case 8:
				if (A_2 > A_0.Length - A_1)
				{
					num = 7;
					continue;
				}
				num = 2;
				continue;
			case 9:
				goto IL_23B;
			case 10:
				goto IL_15E;
			case 11:
				if (A_2 < 0)
				{
					num = 13;
					continue;
				}
				num = 8;
				continue;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1B7;
				default:
					if (false)
					{
					}
					num = 21;
					continue;
				}
				break;
			case 13:
				goto IL_17D;
			case 14:
				if (A_2 > 0)
				{
					num = 5;
					continue;
				}
				return num2;
			case 15:
				num = 22;
				continue;
			case 16:
				if (A_2 == 0)
				{
					num = 4;
					continue;
				}
				num2 = 0;
				num = 6;
				continue;
			case 17:
				if (this.\u1714 > 0)
				{
					num = 15;
					continue;
				}
				goto IL_11C;
			case 18:
				goto IL_80;
			case 20:
				if (A_1 >= 0)
				{
					num = 12;
					continue;
				}
				goto IL_A8;
			case 21:
				if (A_1 > A_0.Length - 1)
				{
					num = 9;
					continue;
				}
				num = 11;
				continue;
			case 22:
				if (A_2 <= 0)
				{
					num = 1;
					continue;
				}
				A_0[A_1++] = (byte)this.\u1713;
				this.\u1714 -= 8;
				this.\u1713 >>= 8;
				A_2--;
				num2++;
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				num = 18;
				continue;
			}
			num = 20;
			continue;
			IL_11C:
			num = 14;
			continue;
			IL_182:
			num = 17;
			continue;
			IL_1B7:
			num = 3;
		}
		IL_80:
		throw new ArgumentNullException(RecordTableEnumerator.b("╆㱈ⵊ⭌⩎⍐", a_));
		IL_A8:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡆⽈ⵊ㹌⩎═", a_), RecordTableEnumerator.b("ࡆ⽈ⵊ㹌⩎═獒㙔㙖㝘筚㍜ぞᕠ䍢ݤɦ䥨ݪ࡬ᱮɰ卲Ŵὶᡸᕺ嵼վꞆ力권ﲘ붜즠슢쮤螦쮨\udeaa쮬즮풰솲閴\udbb6\udcb8햺\udabc쮾꧀", a_));
		IL_15E:
		throw new NotSupportedException(RecordTableEnumerator.b("ᕆⱈ⩊⥌♎㽐㑒畔㡖㽘筚⡜ㅞ`ར।๦๨ժ࡬୮兰ᝲᑴͶᡸ孺ᑼ౾ꆀꦈﾎﲒﲘﾚ뎜", a_));
		IL_17D:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭆ⱈ╊⩌㭎㥐", a_), RecordTableEnumerator.b("୆ⱈ╊⩌㭎㥐獒㙔㙖㝘筚㍜ぞᕠ䍢ݤɦ䥨ݪ࡬ᱮɰ卲Ŵὶᡸᕺ嵼վꦆ", a_));
		IL_1C2:
		return num2;
		IL_1E8:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭆ⱈ╊⩌㭎㥐", a_), RecordTableEnumerator.b("୆ⱈ╊⩌㭎㥐獒㱔⑖祘⽚㉜ぞ䅠རѤᕦ๨๪䍬", a_));
		IL_23B:
		goto IL_A8;
		IL_2E4:
		if (true)
		{
		}
		return 0;
	}

	// Token: 0x06005447 RID: 21575 RVA: 0x0034717C File Offset: 0x0034617C
	protected void ᜂ()
	{
		int num = 1;
		for (;;)
		{
			int num2;
			int num4;
			switch (num)
			{
			case 0:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A6;
				default:
				{
					if (false)
					{
					}
					if (num2 == 0)
					{
						num = 3;
						continue;
					}
					int num3 = this.ᜑ.Read(this.\u1715, 0, num2);
					num4 = 0;
					num = 4;
					continue;
				}
				}
				break;
			case 3:
				num = 7;
				continue;
			case 4:
				if (true)
				{
				}
				goto IL_84;
			case 5:
				goto IL_84;
			case 6:
			{
				int num3;
				if (num4 >= num3)
				{
					num = 0;
					continue;
				}
				goto IL_A6;
			}
			case 7:
				return;
			}
			num2 = 4 - (this.\u1714 >> 3) - (((this.\u1714 & 7) != 0) ? 1 : 0);
			num = 2;
			continue;
			IL_84:
			num = 6;
			continue;
			IL_A6:
			this.\u1713 |= (uint)((uint)this.\u1715[num4] << this.\u1714);
			this.\u1714 += 8;
			num4++;
			num = 5;
		}
	}

	// Token: 0x06005448 RID: 21576 RVA: 0x003472AC File Offset: 0x003462AC
	protected internal int ᜀ(int A_0)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E8;
			case 1:
				this.ᜂ();
				goto IL_77;
			case 3:
				if (this.\u1714 < A_0)
				{
					num = 6;
					continue;
				}
				goto IL_12B;
			case 4:
				goto IL_C2;
			case 5:
				goto IL_6F;
			case 6:
				return -1;
			case 7:
				if (this.\u1714 < A_0)
				{
					num = 1;
					continue;
				}
				goto IL_E8;
			case 8:
				if (A_0 > 32)
				{
					num = 4;
					continue;
				}
				num = 7;
				continue;
			}
			if (A_0 >= 0)
			{
				if (true)
				{
				}
				num = 8;
				continue;
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
				num = 5;
				continue;
			}
			IL_77:
			num = 0;
			continue;
			IL_E8:
			num = 3;
		}
		IL_6F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嬷唹䤻倽㐿", a_), RecordTableEnumerator.b("稷匹䠻䴽怿⅁⭃㍅♇㹉汋ⵍㅏ㱑瑓㡕㝗⹙籛㱝՟䉡ࡣͥ᭧ᥩ䱫ᩭᡯ፱ᩳ噵ɷό๻ᅽ깿", a_));
		IL_C2:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嬷唹䤻倽㐿", a_), RecordTableEnumerator.b("笷唹䤻倽㐿扁⭃⁅桇⡉╋㩍⍏牑㵓╕硗⹙㍛ㅝ䁟๡գᑥཧཀྵ䉫", a_));
		IL_12B:
		uint num2 = ~(uint.MaxValue << A_0);
		return (int)(this.\u1713 & num2);
	}

	// Token: 0x06005449 RID: 21577 RVA: 0x003473F8 File Offset: 0x003463F8
	protected internal void ᜁ(int A_0)
	{
		int a_ = 2;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_0 > 0)
				{
					num = 10;
					continue;
				}
				return;
			case 1:
				if (A_0 == 0)
				{
					num = 2;
					continue;
				}
				num = 4;
				continue;
			case 2:
				return;
			case 3:
				A_0 -= this.\u1714;
				this.\u1714 = 0;
				this.\u1713 = 0U;
				goto IL_B0;
			case 4:
				if (A_0 >= this.\u1714)
				{
					num = 3;
					continue;
				}
				this.\u1714 -= A_0;
				this.\u1713 >>= A_0;
				num = 9;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B0;
				default:
					if (false)
					{
					}
					this.ᜑ.Position += (long)(A_0 >> 3);
					A_0 &= 7;
					num = 0;
					continue;
				}
				break;
			case 7:
				if (A_0 > 0)
				{
					num = 6;
					continue;
				}
				return;
			case 8:
				goto IL_54;
			case 9:
				return;
			case 10:
				goto IL_93;
			}
			if (A_0 < 0)
			{
				num = 8;
				continue;
			}
			num = 1;
			continue;
			IL_B0:
			num = 7;
		}
		IL_54:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嬷唹䤻倽㐿", a_), RecordTableEnumerator.b("稷匹䠻䴽怿⅁⭃㍅♇㹉汋ⵍㅏ㱑瑓㡕㝗⹙籛㱝՟䉡ࡣͥ᭧ᥩ䱫ᩭᡯ፱ᩳ噵ɷό๻ᅽ깿", a_));
		IL_93:
		this.ᜂ();
		this.\u1714 -= A_0;
		this.\u1713 >>= A_0;
	}

	// Token: 0x0600544A RID: 21578 RVA: 0x003475BC File Offset: 0x003465BC
	protected internal int ᜂ(int A_0)
	{
		int num;
		for (;;)
		{
			num = this.ᜀ(A_0);
			if (num != -1)
			{
				goto IL_3E;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_24;
			}
		}
		IL_24:
		if (false)
		{
		}
		if (true)
		{
		}
		return -1;
		IL_3E:
		this.\u1714 -= A_0;
		this.\u1713 >>= A_0;
		return num;
	}

	// Token: 0x0600544B RID: 21579 RVA: 0x00347628 File Offset: 0x00346628
	protected internal int ᜇ()
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
		int num = this.ᜂ(8) << 8;
		return num | this.ᜂ(8);
	}

	// Token: 0x0600544C RID: 21580 RVA: 0x00347678 File Offset: 0x00346678
	protected internal int ᜅ()
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
		int num = this.ᜂ(8);
		return num | this.ᜂ(8) << 8;
	}

	// Token: 0x0600544D RID: 21581 RVA: 0x003476C8 File Offset: 0x003466C8
	protected internal long ᜆ()
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
		long num = (long)((ulong)((ulong)this.ᜂ(8) << 24));
		num |= (long)((ulong)((ulong)this.ᜂ(8) << 16));
		num |= (long)((ulong)((ulong)this.ᜂ(8) << 8));
		return num | (long)((ulong)this.ᜂ(8));
	}

	// Token: 0x0600544E RID: 21582 RVA: 0x00347738 File Offset: 0x00346738
	protected void ᜊ()
	{
		int a_ = 19;
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_18D;
				case 1:
					goto IL_14D;
				case 2:
					if ((num & 3840) != 2048)
					{
						num2 = 0;
						continue;
					}
					this.\u1718 = (int)Math.Pow(2.0, (double)(((num & 61440) >> 12) + 8));
					num2 = 3;
					continue;
				case 3:
					if (this.\u1718 > 65535)
					{
						num2 = 1;
						continue;
					}
					num2 = 9;
					continue;
				case 4:
					goto IL_54;
				case 5:
					goto IL_E8;
				case 6:
					if (num == -1)
					{
						num2 = 4;
						continue;
					}
					num2 = 7;
					continue;
				case 7:
					if (true)
					{
					}
					if (num % 31 == 0)
					{
						num2 = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D1;
					default:
						if (false)
						{
						}
						num2 = 8;
						continue;
					}
					break;
				case 8:
					goto IL_C1;
				case 9:
					goto IL_D1;
				}
				break;
				IL_D1:
				if ((num & 32) >> 5 != 1)
				{
					return;
				}
				num2 = 5;
			}
		}
		IL_54:
		throw new Exception(RecordTableEnumerator.b("ň⹊ⱌ⭎㑐⅒畔㡖㽘筚⥜㝞Ѡ䍢ᙤ፦᭨๪౬ɮ兰ၲᑴ᥶奸ᕺቼ୾ꆀꞆﮈ뾐", a_));
		IL_C1:
		throw new FormatException(RecordTableEnumerator.b("ň⹊ⱌ⭎㑐⅒畔㑖ㅘ㹚㹜㑞በᙢࡤ䝦hݪŬ੮ᙰቲᥴ", a_));
		IL_E8:
		throw new NotImplementedException(RecordTableEnumerator.b("ੈ㹊㹌㭎㹐㹒畔㍖じ㡚⥜㙞๠ൢѤᕦၨ䭪Ѭᱮ兰ᵲᩴͶ奸ࡺࡼཾ권뎒ﾖﲘ뮚철욢쮤펦螨", a_));
		IL_14D:
		throw new FormatException(RecordTableEnumerator.b("᱈╊㹌㩎⅐⍒㩔╖ⵘ㹚㥜罞ᙠ੢୤ͦ٨ᱪ䵬ᱮᡰॲၴ坶ὸᑺོ彾ﾊ꾎ﲒﺚ좠첢쮤螦쒨캪\ud9ac잮\udeb0ힲ鮴", a_));
		IL_18D:
		throw new FormatException(RecordTableEnumerator.b("᱈╊㹌㩎⅐⍒㩔╖ⵘ㹚㥜罞ɠౢࡤᝦ᭨๪Ṭᱮᡰᱲ᭴坶ᑸṺॼ᝾ꮄ", a_));
	}

	// Token: 0x0600544F RID: 21583 RVA: 0x003478EC File Offset: 0x003468EC
	protected string ᜀ(int A_0, int A_1)
	{
		int a_ = 3;
		string text;
		for (;;)
		{
			IL_49:
			text = "";
			int num = 0;
			int num2 = 6;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return text;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						text = RecordTableEnumerator.b("ᤸ", a_) + text;
						num2 = 4;
						continue;
					case 1:
						if (num >= A_1)
						{
							num2 = 5;
							continue;
						}
						num2 = 3;
						continue;
					case 2:
						if (true)
						{
						}
						goto IL_D1;
					case 3:
						if ((num & 7) == 0)
						{
							num2 = 0;
							continue;
						}
						goto IL_5B;
					case 4:
						goto IL_5B;
					case 5:
						return text;
					case 6:
						goto IL_D1;
					}
					goto IL_49;
					IL_5B:
					text = (A_0 & 1).ToString() + text;
					A_0 >>= 1;
					num++;
					num2 = 2;
					break;
					IL_D1:
					num2 = 1;
					break;
				}
			}
		}
		return text;
	}

	// Token: 0x06005450 RID: 21584 RVA: 0x003479E8 File Offset: 0x003469E8
	protected void ᜀ(out spr\u2072 A_0, out spr\u2072 A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			byte[] array;
			for (;;)
			{
				byte b = 0;
				num = this.ᜂ(5);
				num2 = this.ᜂ(5);
				int num3 = this.ᜂ(4);
				if (true)
				{
				}
				int num4 = 28;
				for (;;)
				{
					int num7;
					bool flag;
					int num9;
					int num10;
					switch (num4)
					{
					case 0:
						goto IL_27B;
					case 1:
					{
						if (num3 < 0)
						{
							num4 = 30;
							continue;
						}
						num += 257;
						num2++;
						int num5 = num + num2;
						array = new byte[num5];
						byte[] array2 = new byte[19];
						num3 += 4;
						int num6 = 0;
						num4 = 6;
						continue;
					}
					case 2:
					{
						if (num7-- <= 0)
						{
							num4 = 12;
							continue;
						}
						int num6;
						array[num6++] = b;
						num4 = 31;
						continue;
					}
					case 3:
						flag = true;
						num4 = 14;
						continue;
					case 4:
					{
						int num5;
						int num6;
						if (num6 + num7 > num5)
						{
							num4 = 13;
							continue;
						}
						goto IL_43F;
					}
					case 5:
						num4 = 11;
						continue;
					case 6:
						goto IL_323;
					case 7:
						goto IL_42E;
					case 8:
					{
						byte[] array2;
						spr\u2072 spr_u = new spr\u2072(array2);
						int num6 = 0;
						num4 = 7;
						continue;
					}
					case 9:
						num4 = 1;
						continue;
					case 10:
						goto IL_143;
					case 11:
						if (num2 >= 0)
						{
							num4 = 9;
							continue;
						}
						goto IL_41A;
					case 12:
						num4 = 23;
						continue;
					case 13:
						goto IL_18D;
					case 14:
						goto IL_1FF;
					case 15:
						goto IL_1FF;
					case 16:
					{
						int num5;
						int num6;
						if (num6 == num5)
						{
							num4 = 3;
							continue;
						}
						goto IL_3C4;
					}
					case 17:
					{
						int num6;
						if (num6 >= num3)
						{
							num4 = 8;
							continue;
						}
						int num8 = this.ᜂ(3);
						num4 = 33;
						continue;
					}
					case 18:
					{
						int num6;
						if (num6 == 0)
						{
							num4 = 29;
							continue;
						}
						goto IL_373;
					}
					case 19:
						if (num9 < 0)
						{
							num4 = 10;
							continue;
						}
						num4 = 34;
						continue;
					case 20:
						b = 0;
						num4 = 32;
						continue;
					case 21:
						if (num7 < 0)
						{
							num4 = 22;
							continue;
						}
						num7 += sprᾲ.ᜋ[num10];
						num4 = 4;
						continue;
					case 22:
						goto IL_3AE;
					case 23:
					{
						int num5;
						int num6;
						if (num6 == num5)
						{
							num4 = 0;
							continue;
						}
						goto IL_42E;
					}
					case 24:
						goto IL_31E;
					case 25:
						goto IL_323;
					case 26:
					{
						spr\u2072 spr_u;
						if (((num9 = spr_u.ᜀ(this)) & -16) != 0)
						{
							num4 = 15;
							continue;
						}
						int num6;
						b = (array[num6++] = (byte)num9);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_143;
						default:
							if (false)
							{
							}
							num4 = 16;
							continue;
						}
						break;
					}
					case 27:
						goto IL_3C4;
					case 28:
						if (num >= 0)
						{
							num4 = 5;
							continue;
						}
						goto IL_41A;
					case 29:
						goto IL_11E;
					case 30:
						goto IL_493;
					case 31:
						goto IL_43F;
					case 32:
						goto IL_373;
					case 33:
					{
						int num8;
						if (num8 < 0)
						{
							num4 = 24;
							continue;
						}
						byte[] array2;
						int num6;
						array2[sprៜ.ᜁ[num6++]] = (byte)num8;
						num4 = 25;
						continue;
					}
					case 34:
						if (num9 >= 17)
						{
							num4 = 20;
							continue;
						}
						num4 = 18;
						continue;
					case 35:
						num4 = 19;
						continue;
					case 36:
						if (!flag)
						{
							num4 = 35;
							continue;
						}
						goto IL_4BC;
					}
					break;
					IL_1FF:
					num4 = 36;
					continue;
					IL_323:
					num4 = 17;
					continue;
					IL_373:
					num10 = num9 - 16;
					int a_2 = sprᾲ.ᜌ[num10];
					num7 = this.ᜂ(a_2);
					num4 = 21;
					continue;
					IL_3C4:
					num4 = 26;
					continue;
					IL_42E:
					flag = false;
					num4 = 27;
					continue;
					IL_43F:
					num4 = 2;
				}
			}
			IL_11E:
			throw new FormatException(RecordTableEnumerator.b("၆㭈⑊⍌⡎煐㝒ⱔ㥖㡘㙚㑜㱞䅠ୢၤŦཨ٪౬Ů兰ၲᩴ፶ᱸࡺ卼", a_));
			IL_143:
			throw new FormatException(RecordTableEnumerator.b("၆㭈⑊⍌⡎煐㝒ⱔ㥖㡘㙚㑜㱞䅠ୢၤŦཨ٪౬Ů兰ၲᩴ፶ᱸࡺ卼", a_));
			IL_18D:
			throw new FormatException(RecordTableEnumerator.b("၆㭈⑊⍌⡎煐㝒ⱔ㥖㡘㙚㑜㱞䅠ୢၤŦཨ٪౬Ů兰ၲᩴ፶ᱸࡺ卼", a_));
			IL_27B:
			goto IL_4BC;
			IL_31E:
			throw new FormatException(RecordTableEnumerator.b("၆㭈⑊⍌⡎煐㝒ⱔ㥖㡘㙚㑜㱞䅠ୢၤŦཨ٪౬Ů兰ၲᩴ፶ᱸࡺ卼", a_));
			IL_3AE:
			throw new FormatException(RecordTableEnumerator.b("၆㭈⑊⍌⡎煐㝒ⱔ㥖㡘㙚㑜㱞䅠ୢၤŦཨ٪౬Ů兰ၲᩴ፶ᱸࡺ卼", a_));
			IL_41A:
			throw new FormatException(RecordTableEnumerator.b("၆㭈⑊⍌⡎煐㝒ⱔ㥖㡘㙚㑜㱞䅠ୢၤŦཨ٪౬Ů兰ၲᩴ፶ᱸࡺ卼", a_));
			IL_493:
			goto IL_41A;
			IL_4BC:
			byte[] array3 = new byte[num];
			Array.Copy(array, 0, array3, 0, num);
			A_0 = new spr\u2072(array3);
			array3 = new byte[num2];
			Array.Copy(array, num, array3, 0, num2);
			A_1 = new spr\u2072(array3);
			return;
		}
		}
	}

	// Token: 0x06005451 RID: 21585 RVA: 0x00347EEC File Offset: 0x00346EEC
	protected bool ᜉ()
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return false;
				case 1:
				{
					int num2;
					if (num2 == -1)
					{
						num = 9;
						continue;
					}
					int num3;
					this.\u171D = (num3 == 0);
					int num4 = num2;
					if (true)
					{
					}
					num = 15;
					continue;
				}
				case 2:
					goto IL_1EF;
				case 3:
				{
					int num3;
					if (num3 == -1)
					{
						num = 13;
						continue;
					}
					int num2 = this.ᜂ(2);
					num = 1;
					continue;
				}
				case 4:
					goto IL_D9;
				case 5:
					goto IL_27D;
				case 6:
				{
					int num5;
					if (num5 > 65535)
					{
						num = 11;
						continue;
					}
					this.\u171C = num5;
					this.\u171F = null;
					this.ᜠ = null;
					num = 2;
					continue;
				}
				case 7:
				{
					int num5;
					int num6;
					if (num5 != (num6 ^ 65535))
					{
						num = 5;
						continue;
					}
					num = 6;
					continue;
				}
				case 8:
					num = 12;
					continue;
				case 9:
					return false;
				case 10:
					goto IL_1C9;
				case 11:
					goto IL_14D;
				case 12:
					goto IL_172;
				case 13:
					return false;
				case 15:
				{
					int num4;
					switch (num4)
					{
					case 0:
					{
						this.\u171B = true;
						this.ᜄ();
						int num5 = this.ᜅ();
						int num6 = this.ᜅ();
						num = 7;
						continue;
					}
					case 1:
						this.\u171B = false;
						this.\u171C = -1;
						this.\u171F = spr\u2072.ᜁ();
						this.ᜠ = spr\u2072.ᜀ();
						num = 4;
						continue;
					case 2:
						this.\u171B = false;
						this.\u171C = -1;
						this.ᜀ(out this.\u171F, out this.ᜠ);
						num = 10;
						continue;
					default:
						num = 8;
						continue;
					}
					break;
				}
				}
				if (!this.\u171D)
				{
					num = 0;
				}
				else
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						int num3 = this.ᜂ(1);
						num = 3;
						break;
					}
					}
				}
			}
			return false;
			IL_D9:
			return true;
			IL_14D:
			throw new FormatException(RecordTableEnumerator.b("戶圸堺刼刾ㅀㅂ⁄㑆㩈⹊⥌潎㍐㽒㩔㑖㉘筚ㅜ㩞འѢᅤས䥨ࡪ౬Ů兰ᵲᩴͶ奸᥺᡼彾ꦈﾊﾐ뎒ꎔꊖ겘ꢚꢜ놞", a_));
			IL_172:
			throw new FormatException(RecordTableEnumerator.b("怶䬸吺匼堾慀⅂⥄⡆⩈⁊浌㭎⡐⍒ご祖", a_));
			IL_1C9:
			IL_1EF:
			return true;
			IL_27D:
			throw new FormatException(RecordTableEnumerator.b("怶䬸吺匼堾慀⅂⥄⡆⩈⁊浌⍎㑐㵒㉔⍖ㅘ畚", a_));
		}
		}
	}

	// Token: 0x06005452 RID: 21586 RVA: 0x0034817C File Offset: 0x0034717C
	private bool ᜀ()
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				int num = 65535 - (int)(this.\u171A - this.\u1719);
				flag = false;
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
					int num2 = 29;
					for (;;)
					{
						int num5;
						int num7;
						switch (num2)
						{
						case 0:
							goto IL_404;
						case 1:
							goto IL_3A1;
						case 2:
							flag = true;
							num2 = 13;
							continue;
						case 3:
							num2 = 9;
							continue;
						case 4:
							num2 = 21;
							continue;
						case 5:
							return true;
						case 6:
							if (--num < 258)
							{
								num2 = 5;
								continue;
							}
							goto IL_183;
						case 7:
						{
							int num3;
							if (num3 < 0)
							{
								num2 = 18;
								continue;
							}
							int num4;
							num4 += num3;
							num2 = 11;
							continue;
						}
						case 8:
							goto IL_3A1;
						case 9:
						{
							if (num5 > sprᾲ.ᜏ.Length)
							{
								num2 = 16;
								continue;
							}
							int num4 = sprᾲ.ᜏ[num5];
							int num6 = sprᾲ.ᜐ[num5];
							if (true)
							{
							}
							num2 = 30;
							continue;
						}
						case 10:
						{
							int num8;
							if (num7 >= num8)
							{
								num2 = 2;
								continue;
							}
							checked
							{
								int num4;
								this.\u1716[(int)((IntPtr)(this.\u171A % 65535L))] = this.\u1716[(int)((IntPtr)(unchecked(this.\u171A - (long)num4) % 65535L))];
							}
							this.\u171A += 1L;
							num--;
							num7++;
							num2 = 1;
							continue;
						}
						case 11:
							goto IL_16F;
						case 12:
						{
							if (((num5 = this.\u171F.ᜀ(this)) & -256) != 0)
							{
								num2 = 26;
								continue;
							}
							byte[] u = this.\u1716;
							long u171A;
							this.\u171A = (u171A = this.\u171A) + 1L;
							u[(int)(checked((IntPtr)(u171A % 65535L)))] = (byte)num5;
							flag = true;
							num2 = 6;
							continue;
						}
						case 13:
							goto IL_294;
						case 14:
							if (num5 >= 0)
							{
								num2 = 3;
								continue;
							}
							goto IL_41A;
						case 15:
						{
							int num6;
							int num9 = this.ᜂ(num6);
							num2 = 31;
							continue;
						}
						case 16:
							goto IL_495;
						case 17:
							goto IL_116;
						case 18:
							goto IL_111;
						case 19:
							if (num < 258)
							{
								num2 = 22;
								continue;
							}
							goto IL_183;
						case 20:
						{
							if (num5 > 285)
							{
								num2 = 28;
								continue;
							}
							int num8 = sprᾲ.\u170D[num5 - 257];
							int num6 = sprᾲ.ᜎ[num5 - 257];
							num2 = 25;
							continue;
						}
						case 21:
							if (num5 < 256)
							{
								num2 = 23;
								continue;
							}
							goto IL_228;
						case 22:
							return flag;
						case 23:
							goto IL_16A;
						case 24:
							if (num5 < 257)
							{
								num2 = 4;
								continue;
							}
							num2 = 20;
							continue;
						case 25:
						{
							int num6;
							if (num6 > 0)
							{
								num2 = 15;
								continue;
							}
							goto IL_116;
						}
						case 26:
							num2 = 24;
							continue;
						case 27:
						{
							int num6;
							int num3 = this.ᜂ(num6);
							num2 = 7;
							continue;
						}
						case 28:
							goto IL_451;
						case 29:
							goto IL_294;
						case 30:
						{
							int num6;
							if (num6 > 0)
							{
								num2 = 27;
								continue;
							}
							goto IL_16F;
						}
						case 31:
						{
							int num9;
							if (num9 < 0)
							{
								num2 = 0;
								continue;
							}
							int num8;
							num8 += num9;
							num2 = 17;
							continue;
						}
						}
						break;
						IL_116:
						num5 = this.ᜠ.ᜀ(this);
						num2 = 14;
						continue;
						IL_16F:
						num7 = 0;
						num2 = 8;
						continue;
						IL_183:
						num2 = 12;
						continue;
						IL_294:
						num2 = 19;
						continue;
						IL_3A1:
						num2 = 10;
					}
					break;
				}
				}
			}
			IL_111:
			throw new FormatException(RecordTableEnumerator.b("愵䨷唹刻夽怿♁╃㉅⥇摉", a_));
			IL_16A:
			throw new FormatException(RecordTableEnumerator.b("缵吷嘹夻夽ℿ⹁摃╅❇⹉⥋恍", a_));
			IL_228:
			return flag | (this.\u171E = this.ᜉ());
			IL_404:
			throw new FormatException(RecordTableEnumerator.b("愵䨷唹刻夽怿♁╃㉅⥇摉", a_));
			IL_41A:
			throw new FormatException(RecordTableEnumerator.b("愵䨷唹刻夽怿♁ⵃ㕅㱇⭉≋ⵍ㕏牑㝓㥕㱗㽙牛", a_));
			IL_451:
			throw new FormatException(RecordTableEnumerator.b("缵吷嘹夻夽ℿ⹁摃㑅ⵇ㩉⥋⽍⑏牑㝓㥕㱗㽙籛㉝՟ౡͣብg䑩", a_));
			IL_495:
			goto IL_41A;
		}
		}
	}

	// Token: 0x06005453 RID: 21587 RVA: 0x00348624 File Offset: 0x00347624
	public int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 11;
			int num10;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					long u171A;
					int num2 = (int)(u171A % 65535L);
					int num3 = (int)(this.\u171A % 65535L);
					num = 46;
					continue;
				}
				case 1:
					goto IL_4DE;
				case 2:
				{
					long num4;
					if (num4 != this.\u1712)
					{
						num = 5;
						continue;
					}
					goto IL_2AF;
				}
				case 3:
					goto IL_4DE;
				case 4:
					if (A_1 > A_0.Length - 1)
					{
						num = 30;
						continue;
					}
					num = 6;
					continue;
				case 5:
					goto IL_261;
				case 6:
					if (A_2 >= 0)
					{
						num = 39;
						continue;
					}
					goto IL_184;
				case 7:
					num = 4;
					continue;
				case 8:
					if (A_1 >= 0)
					{
						num = 7;
						continue;
					}
					goto IL_1CE;
				case 9:
				{
					if (this.\u171C == 0)
					{
						num = 10;
						continue;
					}
					int num5 = (int)(this.\u171A % 65535L);
					int num6 = Math.Min(this.\u171C, 65535 - num5);
					int num7 = this.ᜂ(this.\u1716, num5, num6);
					num = 25;
					continue;
				}
				case 10:
				{
					bool flag = this.\u171E = this.ᜉ();
					num = 41;
					continue;
				}
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2AF;
					default:
						if (false)
						{
						}
						num = 21;
						continue;
					}
					break;
				case 13:
					goto IL_2C2;
				case 14:
					num = 42;
					continue;
				case 15:
				{
					int num3;
					if (num3 > 0)
					{
						num = 38;
						continue;
					}
					goto IL_4DE;
				}
				case 16:
					if (A_2 <= 0)
					{
						num = 44;
						continue;
					}
					num = 31;
					continue;
				case 17:
					if (!this.\u1717)
					{
						num = 40;
						continue;
					}
					goto IL_641;
				case 18:
				{
					long u171A = this.\u171A;
					num = 43;
					continue;
				}
				case 19:
					num = 28;
					continue;
				case 20:
					goto IL_FE;
				case 21:
					goto IL_1A6;
				case 22:
					if (!this.\u171E)
					{
						num = 19;
						continue;
					}
					goto IL_641;
				case 23:
					goto IL_4DE;
				case 24:
				{
					int num2;
					int num3;
					this.ᜁ(this.\u1716, num2, num3 - num2);
					num = 3;
					continue;
				}
				case 25:
				{
					int num6;
					int num7;
					if (num6 != num7)
					{
						num = 47;
						continue;
					}
					this.\u171C -= num7;
					this.\u171A += (long)num7;
					num = 27;
					continue;
				}
				case 26:
					num = 17;
					continue;
				case 27:
					goto IL_589;
				case 28:
					if (!this.ᜡ)
					{
						num = 26;
						continue;
					}
					goto IL_641;
				case 29:
				{
					long u171A;
					if (u171A < this.\u171A)
					{
						num = 0;
						continue;
					}
					goto IL_4DE;
				}
				case 30:
					goto IL_554;
				case 31:
					if (this.\u1719 < this.\u171A)
					{
						num = 32;
						continue;
					}
					num = 37;
					continue;
				case 32:
				{
					int num8 = (int)(this.\u1719 % 65535L);
					int num9 = Math.Min(65535 - num8, (int)(this.\u171A - this.\u1719));
					num9 = Math.Min(num9, A_2);
					Array.Copy(this.\u1716, num8, A_0, A_1, num9);
					this.\u1719 += (long)num9;
					A_1 += num9;
					A_2 -= num9;
					num = 36;
					continue;
				}
				case 33:
					goto IL_34C;
				case 34:
					goto IL_1A6;
				case 35:
					if (A_2 > A_0.Length - A_1)
					{
						num = 33;
						continue;
					}
					num10 = A_2;
					num = 1;
					continue;
				case 36:
					goto IL_4DE;
				case 37:
					if (this.\u171E)
					{
						num = 18;
						continue;
					}
					goto IL_1A6;
				case 38:
				{
					int num3;
					this.ᜁ(this.\u1716, 0, num3);
					num = 23;
					continue;
				}
				case 39:
					num = 35;
					continue;
				case 40:
				{
					this.ᜄ();
					long num4 = this.ᜆ();
					num = 2;
					continue;
				}
				case 41:
				{
					bool flag;
					if (!flag)
					{
						num = 45;
						continue;
					}
					goto IL_589;
				}
				case 42:
					if (!this.ᜀ())
					{
						num = 12;
						continue;
					}
					goto IL_589;
				case 43:
					if (!this.\u171B)
					{
						num = 14;
						continue;
					}
					num = 9;
					continue;
				case 44:
					goto IL_1A6;
				case 45:
					num = 34;
					continue;
				case 46:
				{
					int num2;
					int num3;
					if (num2 < num3)
					{
						num = 24;
						continue;
					}
					this.ᜁ(this.\u1716, num2, 65535 - num2);
					num = 15;
					continue;
				}
				case 47:
					goto IL_17F;
				}
				if (A_0 == null)
				{
					num = 20;
					continue;
				}
				num = 8;
				continue;
				IL_1A6:
				num = 22;
				continue;
				IL_2AF:
				this.ᜡ = true;
				num = 13;
				continue;
				IL_4DE:
				num = 16;
				continue;
				IL_589:
				num = 29;
			}
			IL_FE:
			throw new ArgumentNullException(RecordTableEnumerator.b("唶䰸崺嬼娾㍀", a_));
			IL_17F:
			throw new FormatException(RecordTableEnumerator.b("礶嘸伺ᴼ娾⽀ⱂい⁆ⅈ歊⥌⹎═㉒畔㹖㝘筚⹜⭞፠٢Ѥ੦䝨", a_));
			IL_184:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嬶尸唺娼䬾⥀", a_), RecordTableEnumerator.b("笶尸唺娼䬾⥀捂ⱄ㑆楈≊⅌⍎㑐㑒㑔㭖睘", a_));
			IL_1CE:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("堶弸崺丼娾㕀", a_), RecordTableEnumerator.b("砶弸崺丼娾㕀捂⅄⡆ⱈ㡊浌ⅎ㹐❒畔㕖㱘㝚㉜ㅞ٠䍢ᅤࡦ䥨ᡪᵬ੮ተᩲ፴Ṷᱸὺ嵼ᵾﮈꖊ", a_));
			IL_261:
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("琶儸帺帼吾㉀㙂⡄杆⩈⍊⡌ⱎ㩐獒㍔㙖じ㝚㡜㭞你", a_));
			IL_2C2:
			goto IL_641;
			IL_34C:
			goto IL_184;
			IL_554:
			goto IL_1CE;
			IL_641:
			return num10 - A_2;
		}
		}
	}

	// Token: 0x06005454 RID: 21588 RVA: 0x00348C78 File Offset: 0x00347C78
	// Note: this type is marked as 'beforefieldinit'.
	static sprᾲ()
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
		sprᾲ.ᜋ = new int[]
		{
			3,
			3,
			11
		};
		sprᾲ.ᜌ = new int[]
		{
			2,
			3,
			7
		};
		sprᾲ.\u170D = new int[]
		{
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			13,
			15,
			17,
			19,
			23,
			27,
			31,
			35,
			43,
			51,
			59,
			67,
			83,
			99,
			115,
			131,
			163,
			195,
			227,
			258
		};
		sprᾲ.ᜎ = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			0
		};
		sprᾲ.ᜏ = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			7,
			9,
			13,
			17,
			25,
			33,
			49,
			65,
			97,
			129,
			193,
			257,
			385,
			513,
			769,
			1025,
			1537,
			2049,
			3073,
			4097,
			6145,
			8193,
			12289,
			16385,
			24577
		};
		sprᾲ.ᜐ = new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12,
			13,
			13
		};
	}

	// Token: 0x04002794 RID: 10132
	private const int ᜀ = 3840;

	// Token: 0x04002795 RID: 10133
	private const int ᜁ = 61440;

	// Token: 0x04002796 RID: 10134
	private const int ᜂ = 31;

	// Token: 0x04002797 RID: 10135
	private const int ᜃ = 32;

	// Token: 0x04002798 RID: 10136
	private const int ᜄ = 192;

	// Token: 0x04002799 RID: 10137
	private const int ᜅ = 65535;

	// Token: 0x0400279A RID: 10138
	private const int ᜆ = 258;

	// Token: 0x0400279B RID: 10139
	private const int ᜇ = 256;

	// Token: 0x0400279C RID: 10140
	private const int ᜈ = 257;

	// Token: 0x0400279D RID: 10141
	private const int ᜉ = 285;

	// Token: 0x0400279E RID: 10142
	private const int ᜊ = 29;

	// Token: 0x0400279F RID: 10143
	private static readonly int[] ᜋ;

	// Token: 0x040027A0 RID: 10144
	private static readonly int[] ᜌ;

	// Token: 0x040027A1 RID: 10145
	private static readonly int[] \u170D;

	// Token: 0x040027A2 RID: 10146
	private static readonly int[] ᜎ;

	// Token: 0x040027A3 RID: 10147
	private static readonly int[] ᜏ;

	// Token: 0x040027A4 RID: 10148
	private static readonly int[] ᜐ;

	// Token: 0x040027A5 RID: 10149
	private Stream ᜑ;

	// Token: 0x040027A6 RID: 10150
	private long \u1712;

	// Token: 0x040027A7 RID: 10151
	private uint \u1713;

	// Token: 0x040027A8 RID: 10152
	private int \u1714;

	// Token: 0x040027A9 RID: 10153
	private byte[] \u1715;

	// Token: 0x040027AA RID: 10154
	private byte[] \u1716;

	// Token: 0x040027AB RID: 10155
	private bool \u1717;

	// Token: 0x040027AC RID: 10156
	private int \u1718;

	// Token: 0x040027AD RID: 10157
	private long \u1719;

	// Token: 0x040027AE RID: 10158
	private long \u171A;

	// Token: 0x040027AF RID: 10159
	private bool \u171B;

	// Token: 0x040027B0 RID: 10160
	private int \u171C;

	// Token: 0x040027B1 RID: 10161
	private bool \u171D;

	// Token: 0x040027B2 RID: 10162
	private bool \u171E;

	// Token: 0x040027B3 RID: 10163
	private spr\u2072 \u171F;

	// Token: 0x040027B4 RID: 10164
	private spr\u2072 ᜠ;

	// Token: 0x040027B5 RID: 10165
	private bool ᜡ;
}
