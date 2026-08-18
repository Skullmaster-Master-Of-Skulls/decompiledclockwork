using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020003BA RID: 954
internal class sprᢹ
{
	// Token: 0x060035EB RID: 13803 RVA: 0x003298AC File Offset: 0x003288AC
	public sprᢹ(Stream A_0) : this(A_0, false)
	{
	}

	// Token: 0x060035EC RID: 13804 RVA: 0x003298C4 File Offset: 0x003288C4
	public sprᢹ(Stream A_0, bool A_1)
	{
		int a_ = 4;
		this.\u1712 = 1L;
		this.\u1715 = new byte[4];
		this.\u1716 = new byte[65535];
		this.\u171D = true;
		this.\u171E = true;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ᥩᡫᱭᕯ፱ᥳ", a_));
		}
		if (A_0.Length == 0L)
		{
			throw new ArgumentException(ClipboardData.b("ᥩᡫᱭᕯ፱ᥳ噵啷婹ཻ੽ꢇ낏ﲑﮓ뢗鍊뺝얟쾡풣튥톧", a_));
		}
		this.ᜑ = A_0;
		this.\u1717 = A_1;
		if (!this.\u1717)
		{
			this.ᜊ();
		}
		this.ᜉ();
	}

	// Token: 0x060035ED RID: 13805 RVA: 0x00329974 File Offset: 0x00328974
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

	// Token: 0x060035EE RID: 13806 RVA: 0x003299B8 File Offset: 0x003289B8
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

	// Token: 0x060035EF RID: 13807 RVA: 0x00329A14 File Offset: 0x00328A14
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

	// Token: 0x060035F0 RID: 13808 RVA: 0x00329A58 File Offset: 0x00328A58
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
		spr\u2580.ᜀ(ref this.\u1712, A_0, A_1, A_2);
	}

	// Token: 0x060035F1 RID: 13809 RVA: 0x00329AA4 File Offset: 0x00328AA4
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

	// Token: 0x060035F2 RID: 13810 RVA: 0x00329B08 File Offset: 0x00328B08
	protected internal int ᜂ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 4;
		int num = 16;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 >= 0)
				{
					num = 19;
					continue;
				}
				goto IL_A8;
			case 1:
				if (this.\u1714 > 0)
				{
					num = 20;
					continue;
				}
				goto IL_112;
			case 2:
				if (A_2 > 0)
				{
					num = 9;
					continue;
				}
				return num2;
			case 3:
				if (A_2 >= 0)
				{
					num = 21;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_157;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 4:
				goto IL_112;
			case 5:
				if ((this.\u1714 & 7) != 0)
				{
					num = 7;
					continue;
				}
				num = 17;
				continue;
			case 6:
				if (A_2 <= 0)
				{
					num = 4;
					continue;
				}
				A_0[A_1++] = (byte)this.\u1713;
				this.\u1714 -= 8;
				this.\u1713 >>= 8;
				A_2--;
				num2++;
				num = 12;
				continue;
			case 7:
				goto IL_157;
			case 8:
				goto IL_244;
			case 9:
				num2 += this.ᜑ.Read(A_0, A_1, A_2);
				num = 22;
				continue;
			case 10:
				goto IL_19F;
			case 11:
				goto IL_20D;
			case 12:
				goto IL_1A4;
			case 13:
				goto IL_1A4;
			case 14:
				goto IL_2ED;
			case 15:
				if (A_1 > A_0.Length - 1)
				{
					num = 8;
					continue;
				}
				num = 3;
				continue;
			case 17:
				if (A_2 == 0)
				{
					num = 14;
					continue;
				}
				num2 = 0;
				num = 13;
				continue;
			case 18:
				goto IL_80;
			case 19:
				num = 15;
				continue;
			case 20:
				num = 6;
				continue;
			case 21:
				if (A_2 > A_0.Length - A_1)
				{
					num = 11;
					continue;
				}
				num = 5;
				continue;
			case 22:
				goto IL_1E7;
			}
			if (A_0 == null)
			{
				num = 18;
				continue;
			}
			num = 0;
			continue;
			IL_112:
			num = 2;
			continue;
			IL_1A4:
			num = 1;
		}
		IL_80:
		throw new ArgumentNullException(ClipboardData.b("ࡩᥫ࡭ᙯ᝱ٳ", a_));
		IL_A8:
		throw new ArgumentOutOfRangeException(ClipboardData.b("թ੫࡭ͯ᝱s", a_), ClipboardData.b("╩੫࡭ͯ᝱s噵᭷᭹ቻ幽ꚅ겋뚕ﶛ肟\ud8a1솣풥잧誩쎫\udcad邯햱욳펵\ud9b7캹\ud9bb첽뛁곃Ʂꛇ껋믍뛏듑뇓ꓕ뛙맛냝蟟雡賣웥엧쫩\uddeb샭", a_));
		IL_157:
		throw new NotSupportedException(ClipboardData.b("㡩५཭ᑯ᭱ᩳᅵ塷ᕹ᩻幽뒓聯ﶛ뺝즟톡蒣좥잧\udea9貫\uddad얯슱쒳\ud9b5쪷캹\ud9bb\udabd", a_));
		IL_19F:
		throw new ArgumentOutOfRangeException(ClipboardData.b("٩५mᝯٱᱳ", a_), ClipboardData.b("♩५mᝯٱᱳ噵᭷᭹ቻ幽ꚅ겋뚕ﶛ肟\ud8a1솣풥잧蒩", a_));
		IL_1E7:
		return num2;
		IL_20D:
		throw new ArgumentOutOfRangeException(ClipboardData.b("٩५mᝯٱᱳ", a_), ClipboardData.b("♩५mᝯٱᱳ噵ᅷॹ屻੽ꒃ뺏", a_));
		IL_244:
		goto IL_A8;
		IL_2ED:
		if (true)
		{
		}
		return 0;
	}

	// Token: 0x060035F3 RID: 13811 RVA: 0x00329E08 File Offset: 0x00328E08
	protected void ᜂ()
	{
		int num = 2;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_7A;
			case 3:
				if (num2 != 0)
				{
					int num3 = this.ᜑ.Read(this.\u1715, 0, num2);
					int num4 = 0;
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_111;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 4:
				goto IL_111;
			case 5:
				if (true)
				{
				}
				goto IL_7A;
			case 6:
			{
				int num3;
				int num4;
				if (num4 >= num3)
				{
					num = 7;
					continue;
				}
				this.\u1713 |= (uint)((uint)this.\u1715[num4] << this.\u1714);
				this.\u1714 += 8;
				num4++;
				num = 1;
				continue;
			}
			case 7:
				return;
			}
			num2 = 4 - (this.\u1714 >> 3) - (((this.\u1714 & 7) != 0) ? 1 : 0);
			num = 3;
			continue;
			IL_7A:
			num = 6;
			continue;
			IL_111:
			num = 0;
		}
	}

	// Token: 0x060035F4 RID: 13812 RVA: 0x00329F38 File Offset: 0x00328F38
	protected internal int ᜀ(int A_0)
	{
		int a_ = 0;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.\u1714 < A_0)
				{
					num = 1;
					continue;
				}
				goto IL_125;
			case 1:
				return -1;
			case 2:
				if (A_0 > 32)
				{
					num = 3;
					continue;
				}
				num = 4;
				continue;
			case 3:
				goto IL_98;
			case 4:
				if (this.\u1714 < A_0)
				{
					num = 8;
					continue;
				}
				goto IL_C6;
			case 5:
				goto IL_C6;
			case 6:
				goto IL_49;
			case 8:
				this.ᜂ();
				num = 5;
				continue;
			}
			if (A_0 < 0)
			{
				num = 6;
				continue;
			}
			num = 2;
			continue;
			IL_C6:
			num = 0;
		}
		IL_49:
		throw new ArgumentOutOfRangeException(ClipboardData.b("եݧὩɫᩭ", a_), ClipboardData.b("⑥ŧṩὫ乭፯ᵱųᡵ౷婹ύώꊁﲇꪉ낏ﺑ몙솟첡蒣\udca5춧\ud8a9쎫肭", a_));
		IL_98:
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_49;
		default:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(ClipboardData.b("եݧὩɫᩭ", a_), ClipboardData.b("╥ݧὩɫᩭ偯ᵱታ噵᩷፹ࡻൽꁿꚅﲇ꺍ﲏﶗ뒙", a_));
		}
		return -1;
		IL_125:
		uint num2 = ~(uint.MaxValue << A_0);
		return (int)(this.\u1713 & num2);
	}

	// Token: 0x060035F5 RID: 13813 RVA: 0x0032A07C File Offset: 0x0032907C
	protected internal void ᜁ(int A_0)
	{
		int a_ = 18;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜑ.Position += (long)(A_0 >> 3);
				A_0 &= 7;
				goto IL_74;
			case 2:
				A_0 -= this.\u1714;
				this.\u1714 = 0;
				this.\u1713 = 0U;
				num = 3;
				continue;
			case 3:
				if (A_0 > 0)
				{
					num = 0;
					continue;
				}
				return;
			case 4:
				if (A_0 == 0)
				{
					num = 6;
					continue;
				}
				num = 8;
				continue;
			case 5:
				goto IL_54;
			case 6:
				return;
			case 7:
				return;
			case 8:
				if (A_0 >= this.\u1714)
				{
					num = 2;
					continue;
				}
				this.\u1714 -= A_0;
				this.\u1713 >>= A_0;
				num = 7;
				continue;
			case 9:
				if (true)
				{
				}
				if (A_0 <= 0)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_74;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 10:
				goto IL_BC;
			}
			if (A_0 < 0)
			{
				num = 5;
				continue;
			}
			num = 4;
			continue;
			IL_74:
			num = 9;
		}
		IL_54:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᭷ᕹॻၽ", a_), ClipboardData.b("㩷፹ࡻൽꁿﺉ겋ﲑ뒓벛ﲝ얟芡좣쎥\udba7\ud9a9貫\udaad\ud8af펱\udab3隵슷\udfb9캻톽", a_));
		IL_BC:
		this.ᜂ();
		this.\u1714 -= A_0;
		this.\u1713 >>= A_0;
	}

	// Token: 0x060035F6 RID: 13814 RVA: 0x0032A244 File Offset: 0x00329244
	protected internal int ᜂ(int A_0)
	{
		int num = this.ᜀ(A_0);
		if (num == -1)
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
				return -1;
			}
		}
		this.\u1714 -= A_0;
		this.\u1713 >>= A_0;
		return num;
	}

	// Token: 0x060035F7 RID: 13815 RVA: 0x0032A2B0 File Offset: 0x003292B0
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

	// Token: 0x060035F8 RID: 13816 RVA: 0x0032A300 File Offset: 0x00329300
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

	// Token: 0x060035F9 RID: 13817 RVA: 0x0032A350 File Offset: 0x00329350
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

	// Token: 0x060035FA RID: 13818 RVA: 0x0032A3C0 File Offset: 0x003293C0
	protected void ᜊ()
	{
		int a_ = 8;
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num == -1)
					{
						num2 = 2;
						continue;
					}
					num2 = 1;
					continue;
				case 1:
					if (num % 31 != 0)
					{
						num2 = 6;
						continue;
					}
					num2 = 9;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DD;
					default:
						goto IL_6A;
					}
					break;
				case 3:
					goto IL_143;
				case 4:
					goto IL_E8;
				case 5:
					if ((num & 32) >> 5 == 1)
					{
						goto IL_DD;
					}
					return;
				case 6:
					goto IL_C1;
				case 7:
					goto IL_18D;
				case 8:
					if (this.\u1718 > 65535)
					{
						num2 = 3;
						continue;
					}
					num2 = 5;
					continue;
				case 9:
					if ((num & 3840) != 2048)
					{
						num2 = 7;
						continue;
					}
					this.\u1718 = (int)Math.Pow(2.0, (double)(((num & 61440) >> 12) + 8));
					num2 = 8;
					continue;
				}
				break;
				IL_DD:
				num2 = 4;
			}
		}
		IL_6A:
		if (false)
		{
		}
		throw new Exception(ClipboardData.b("♭ᕯ፱ၳ፵੷婹፻᡽ꁿꢇ黎ﲍ煉뚕ﮗﮙ뺝캟춡킣蚥쪧쾩貫\udcad햯펱킳颵", a_));
		IL_C1:
		throw new FormatException(ClipboardData.b("♭ᕯ፱ၳ፵੷婹ύᙽﶇ겋ﲏﺑ聯", a_));
		IL_E8:
		throw new NotImplementedException(ClipboardData.b("⵭կűs᥵ᕷ婹᡻᝽ﺋ낏ﮑ뚕뺝펟힡풣횥잧\ud8a9\ud8ab쮭풯銱햳습颷캹풻\udbbd꿁ꯃꯅ귇꓉룋", a_));
		IL_143:
		if (true)
		{
		}
		throw new FormatException(ClipboardData.b("㭭ṯűųٵࡷᕹ๻੽ꒃ늑ﾕﾙ벛쾟킡蒣슥춧첩삫쾭쒯ힱ钳햵ힷힹ첻첽ꖿ뇁럃꿅Ꟈ꓉ꏍ뗏ꛑ볓맕볗", a_));
		IL_18D:
		throw new FormatException(ClipboardData.b("㭭ṯűųٵࡷᕹ๻੽ꒃﲋﲍﾕ벛얟횡첣즥첧蒩", a_));
	}

	// Token: 0x060035FB RID: 13819 RVA: 0x0032A574 File Offset: 0x00329574
	protected string ᜀ(int A_0, int A_1)
	{
		int a_ = 5;
		string text;
		for (;;)
		{
			text = "";
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return text;
				case 1:
					goto IL_B2;
				case 2:
					if (num >= A_1)
					{
						num2 = 0;
						continue;
					}
					num2 = 6;
					continue;
				case 3:
					text = ClipboardData.b("䭪", a_) + text;
					num2 = 5;
					continue;
				case 4:
					goto IL_B2;
				case 5:
					if (true)
					{
					}
					goto IL_3F;
				case 6:
					if ((num & 7) == 0)
					{
						num2 = 3;
						continue;
					}
					goto IL_3F;
				}
				break;
				IL_5A:
				num2 = 1;
				continue;
				IL_3F:
				text = (A_0 & 1).ToString() + text;
				A_0 >>= 1;
				num++;
				goto IL_5A;
				IL_B2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5A;
				default:
					if (false)
					{
					}
					num2 = 2;
					break;
				}
			}
		}
		return text;
	}

	// Token: 0x060035FC RID: 13820 RVA: 0x0032A66C File Offset: 0x0032966C
	protected void ᜀ(out sprẒ A_0, out sprẒ A_1)
	{
		int a_ = 8;
		int num2;
		byte[] array;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_314:
			int num;
			if (num < 0)
			{
				num2 = 8;
			}
			else
			{
				array[sprᣬ.ᜁ[num3++]] = (byte)num;
				num2 = 13;
			}
			break;
		}
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_DB;
			}
			break;
		}
		byte[] array2;
		byte b;
		int num7;
		int num8;
		int num9;
		for (;;)
		{
			IL_38:
			if (true)
			{
			}
			int num4;
			int num5;
			bool flag;
			int num10;
			switch (num2)
			{
			case 0:
				goto IL_21B;
			case 1:
				goto IL_438;
			case 2:
				goto IL_3B8;
			case 3:
				if (num4 >= 17)
				{
					num2 = 29;
					continue;
				}
				num2 = 36;
				continue;
			case 4:
				goto IL_449;
			case 5:
				goto IL_1A9;
			case 6:
				goto IL_32D;
			case 7:
				goto IL_37D;
			case 8:
				goto IL_328;
			case 9:
				if (num5-- <= 0)
				{
					num2 = 25;
					continue;
				}
				array2[num3++] = b;
				num2 = 4;
				continue;
			case 10:
				goto IL_3CE;
			case 11:
			{
				int num6;
				if (num3 == num6)
				{
					num2 = 30;
					continue;
				}
				goto IL_438;
			}
			case 12:
				goto IL_21B;
			case 13:
				goto IL_32D;
			case 14:
				if (num7 >= 0)
				{
					num2 = 22;
					continue;
				}
				goto IL_424;
			case 15:
				num2 = 31;
				continue;
			case 16:
			{
				int num6;
				if (num3 == num6)
				{
					num2 = 18;
					continue;
				}
				goto IL_3CE;
			}
			case 17:
				goto IL_13A;
			case 18:
				flag = true;
				num2 = 12;
				continue;
			case 19:
			{
				if (num3 >= num8)
				{
					num2 = 26;
					continue;
				}
				int num = this.ᜂ(3);
				num2 = 32;
				continue;
			}
			case 20:
				if (num9 >= 0)
				{
					num2 = 24;
					continue;
				}
				goto IL_424;
			case 21:
				goto IL_15F;
			case 22:
				num2 = 20;
				continue;
			case 23:
			{
				sprẒ sprẒ;
				if (((num4 = sprẒ.ᜀ(this)) & -16) != 0)
				{
					num2 = 0;
					continue;
				}
				b = (array2[num3++] = (byte)num4);
				num2 = 16;
				continue;
			}
			case 24:
				num2 = 27;
				continue;
			case 25:
				num2 = 11;
				continue;
			case 26:
			{
				sprẒ sprẒ = new sprẒ(array);
				num3 = 0;
				num2 = 1;
				continue;
			}
			case 27:
			{
				if (num8 < 0)
				{
					num2 = 34;
					continue;
				}
				num7 += 257;
				num9++;
				int num6 = num7 + num9;
				array2 = new byte[num6];
				array = new byte[19];
				num8 += 4;
				num3 = 0;
				num2 = 6;
				continue;
			}
			case 28:
				if (num5 < 0)
				{
					num2 = 2;
					continue;
				}
				num5 += sprᢹ.ᜋ[num10];
				num2 = 35;
				continue;
			case 29:
				b = 0;
				num2 = 7;
				continue;
			case 30:
				goto IL_2A1;
			case 31:
				if (num4 < 0)
				{
					num2 = 21;
					continue;
				}
				num2 = 3;
				continue;
			case 32:
				goto IL_314;
			case 33:
				if (!flag)
				{
					num2 = 15;
					continue;
				}
				goto IL_4BC;
			case 34:
				goto IL_493;
			case 35:
			{
				int num6;
				if (num3 + num5 > num6)
				{
					num2 = 5;
					continue;
				}
				goto IL_449;
			}
			case 36:
				if (num3 == 0)
				{
					num2 = 17;
					continue;
				}
				goto IL_37D;
			}
			goto IL_DB;
			IL_21B:
			num2 = 33;
			continue;
			IL_32D:
			num2 = 19;
			continue;
			IL_37D:
			num10 = num4 - 16;
			int a_2 = sprᢹ.ᜌ[num10];
			num5 = this.ᜂ(a_2);
			num2 = 28;
			continue;
			IL_3CE:
			num2 = 23;
			continue;
			IL_438:
			flag = false;
			num2 = 10;
			continue;
			IL_449:
			num2 = 9;
		}
		IL_13A:
		throw new FormatException(ClipboardData.b("㥭ɯᵱᩳᅵ塷ṹջၽꢇ曆ﾑ뢗蓮瞧얟톡誣", a_));
		IL_15F:
		throw new FormatException(ClipboardData.b("㥭ɯᵱᩳᅵ塷ṹջၽꢇ曆ﾑ뢗蓮瞧얟톡誣", a_));
		IL_1A9:
		throw new FormatException(ClipboardData.b("㥭ɯᵱᩳᅵ塷ṹջၽꢇ曆ﾑ뢗蓮瞧얟톡誣", a_));
		IL_2A1:
		goto IL_4BC;
		IL_328:
		throw new FormatException(ClipboardData.b("㥭ɯᵱᩳᅵ塷ṹջၽꢇ曆ﾑ뢗蓮瞧얟톡誣", a_));
		IL_3B8:
		throw new FormatException(ClipboardData.b("㥭ɯᵱᩳᅵ塷ṹջၽꢇ曆ﾑ뢗蓮瞧얟톡誣", a_));
		IL_424:
		throw new FormatException(ClipboardData.b("㥭ɯᵱᩳᅵ塷ṹջၽꢇ曆ﾑ뢗蓮瞧얟톡誣", a_));
		IL_493:
		goto IL_424;
		IL_4BC:
		byte[] array3 = new byte[num7];
		Array.Copy(array2, 0, array3, 0, num7);
		A_0 = new sprẒ(array3);
		array3 = new byte[num9];
		Array.Copy(array2, num7, array3, 0, num9);
		A_1 = new sprẒ(array3);
		return;
		IL_DB:
		b = 0;
		num7 = this.ᜂ(5);
		num9 = this.ᜂ(5);
		num8 = this.ᜂ(4);
		num2 = 14;
		goto IL_38;
	}

	// Token: 0x060035FD RID: 13821 RVA: 0x0032AB70 File Offset: 0x00329B70
	protected bool ᜉ()
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				int num5;
				switch (num)
				{
				case 0:
					return false;
				case 1:
				{
					int num2;
					int num3;
					if (num2 != (num3 ^ 65535))
					{
						num = 11;
						continue;
					}
					num = 13;
					continue;
				}
				case 2:
					return false;
				case 3:
					goto IL_D9;
				case 4:
					goto IL_1A6;
				case 5:
				{
					int num4;
					if (num4 == -1)
					{
						num = 0;
						continue;
					}
					this.\u171D = (num5 == 0);
					int num6 = num4;
					if (true)
					{
					}
					num = 9;
					continue;
				}
				case 6:
				{
					if (num5 == -1)
					{
						num = 10;
						continue;
					}
					int num4 = this.ᜂ(2);
					num = 5;
					continue;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_225;
					default:
						goto IL_1E2;
					}
					break;
				case 8:
					goto IL_14C;
				case 9:
				{
					int num6;
					switch (num6)
					{
					case 0:
					{
						this.\u171B = true;
						this.ᜄ();
						int num2 = this.ᜅ();
						int num3 = this.ᜅ();
						num = 1;
						continue;
					}
					case 1:
						this.\u171B = false;
						this.\u171C = -1;
						this.\u171F = sprẒ.ᜁ();
						this.ᜠ = sprẒ.ᜀ();
						num = 3;
						continue;
					case 2:
						this.\u171B = false;
						this.\u171C = -1;
						this.ᜀ(out this.\u171F, out this.ᜠ);
						num = 4;
						continue;
					}
					goto IL_225;
				}
				case 10:
					return false;
				case 11:
					goto IL_280;
				case 12:
					goto IL_127;
				case 13:
				{
					int num2;
					if (num2 > 65535)
					{
						num = 12;
						continue;
					}
					this.\u171C = num2;
					this.\u171F = null;
					this.ᜠ = null;
					num = 7;
					continue;
				}
				case 14:
					num = 8;
					continue;
				}
				if (!this.\u171D)
				{
					num = 2;
					continue;
				}
				num5 = this.ᜂ(1);
				num = 6;
				continue;
				IL_225:
				num = 14;
			}
			return false;
			IL_D9:
			return true;
			IL_127:
			throw new FormatException(ClipboardData.b("ⱸᕺṼၾ愈놐璉滛붜쒠춢스펦솨讪캬캮\udfb0鎲\udbb4\ud8b6춸鮺\udfbc\udabe껂꫄뗆곈만꟎냐뷒쿠", a_));
			IL_14C:
			throw new FormatException(ClipboardData.b("⹸ॺቼᅾꎂ꾎래", a_));
			IL_1A6:
			return true;
			IL_1E2:
			if (false)
			{
			}
			return true;
			IL_280:
			throw new FormatException(ClipboardData.b("⹸ॺቼᅾꎂ꾎﶐ﮔ뎜", a_));
		}
		}
	}

	// Token: 0x060035FE RID: 13822 RVA: 0x0032AE04 File Offset: 0x00329E04
	private bool ᜀ()
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				int num = 65535 - (int)(this.\u171A - this.\u1719);
				flag = false;
				int num2 = 25;
				for (;;)
				{
					int num6;
					int num9;
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (num3 < 0)
						{
							num2 = 6;
							continue;
						}
						int num4;
						num4 += num3;
						num2 = 15;
						continue;
					}
					case 1:
					{
						if (true)
						{
						}
						int num5;
						int num3 = this.ᜂ(num5);
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_3A1;
					case 3:
						goto IL_495;
					case 4:
					{
						if (((num6 = this.\u171F.ᜀ(this)) & -256) != 0)
						{
							num2 = 8;
							continue;
						}
						byte[] u = this.\u1716;
						long u171A;
						this.\u171A = (u171A = this.\u171A) + 1L;
						u[(int)(checked((IntPtr)(u171A % 65535L)))] = (byte)num6;
						flag = true;
						num2 = 7;
						continue;
					}
					case 5:
						if (num < 258)
						{
							num2 = 26;
							continue;
						}
						goto IL_171;
					case 6:
						goto IL_FF;
					case 7:
						if (--num < 258)
						{
							num2 = 27;
							continue;
						}
						goto IL_171;
					case 8:
						num2 = 14;
						continue;
					case 9:
						if (num6 < 256)
						{
							num2 = 24;
							continue;
						}
						goto IL_20C;
					case 10:
						goto IL_404;
					case 11:
					{
						int num7;
						if (num7 < 0)
						{
							num2 = 10;
							continue;
						}
						int num8;
						num8 += num7;
						num2 = 20;
						continue;
					}
					case 12:
					{
						int num5;
						int num7 = this.ᜂ(num5);
						num2 = 11;
						continue;
					}
					case 13:
						goto IL_451;
					case 14:
						if (num6 < 257)
						{
							num2 = 29;
							continue;
						}
						num2 = 19;
						continue;
					case 15:
						goto IL_15D;
					case 16:
						if (num6 >= 0)
						{
							num2 = 22;
							continue;
						}
						goto IL_41A;
					case 17:
						flag = true;
						num2 = 30;
						continue;
					case 18:
					{
						int num5;
						if (num5 <= 0)
						{
							goto IL_104;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_451;
						default:
							if (false)
							{
							}
							num2 = 12;
							continue;
						}
						break;
					}
					case 19:
					{
						if (num6 > 285)
						{
							num2 = 13;
							continue;
						}
						int num8 = sprᢹ.\u170D[num6 - 257];
						int num5 = sprᢹ.ᜎ[num6 - 257];
						num2 = 18;
						continue;
					}
					case 20:
						goto IL_104;
					case 21:
					{
						int num8;
						if (num9 >= num8)
						{
							num2 = 17;
							continue;
						}
						checked
						{
							int num4;
							this.\u1716[(int)((IntPtr)(this.\u171A % 65535L))] = this.\u1716[(int)((IntPtr)(unchecked(this.\u171A - (long)num4) % 65535L))];
						}
						this.\u171A += 1L;
						num--;
						num9++;
						num2 = 23;
						continue;
					}
					case 22:
						num2 = 28;
						continue;
					case 23:
						goto IL_3A1;
					case 24:
						goto IL_158;
					case 25:
						goto IL_278;
					case 26:
						return flag;
					case 27:
						return true;
					case 28:
					{
						if (num6 > sprᢹ.ᜏ.Length)
						{
							num2 = 3;
							continue;
						}
						int num4 = sprᢹ.ᜏ[num6];
						int num5 = sprᢹ.ᜐ[num6];
						num2 = 31;
						continue;
					}
					case 29:
						num2 = 9;
						continue;
					case 30:
						goto IL_278;
					case 31:
					{
						int num5;
						if (num5 > 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_15D;
					}
					}
					break;
					IL_104:
					num6 = this.ᜠ.ᜀ(this);
					num2 = 16;
					continue;
					IL_15D:
					num9 = 0;
					num2 = 2;
					continue;
					IL_171:
					num2 = 4;
					continue;
					IL_278:
					num2 = 5;
					continue;
					IL_3A1:
					num2 = 21;
				}
			}
			IL_FF:
			throw new FormatException(ClipboardData.b("⽷ࡹ፻ၽꊁﲇꊋ", a_));
			IL_158:
			throw new FormatException(ClipboardData.b("ㅷᙹၻ᭽ꚅ뺏", a_));
			IL_20C:
			return flag | (this.\u171E = this.ᜉ());
			IL_404:
			throw new FormatException(ClipboardData.b("⽷ࡹ፻ၽꊁﲇꊋ", a_));
			IL_41A:
			throw new FormatException(ClipboardData.b("⽷ࡹ፻ၽꊁﮇﺉ뒓ﺙ鍊낝", a_));
			IL_451:
			throw new FormatException(ClipboardData.b("ㅷᙹၻ᭽ꚅ慎ﲋ뒓ﺙ鍊뺝첟잡쪣솥\udca7슩芫", a_));
			IL_495:
			goto IL_41A;
		}
		}
	}

	// Token: 0x060035FF RID: 13823 RVA: 0x0032B2AC File Offset: 0x0032A2AC
	public int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 30;
			int num9;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					this.ᜁ(this.\u1716, 0, num2);
					num = 42;
					continue;
				}
				case 1:
					num = 39;
					continue;
				case 2:
					goto IL_261;
				case 3:
				{
					long u171A;
					int num3 = (int)(u171A % 65535L);
					int num2 = (int)(this.\u171A % 65535L);
					num = 37;
					continue;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_294;
					default:
						if (false)
						{
						}
						if (this.\u1719 < this.\u171A)
						{
							num = 10;
							continue;
						}
						num = 36;
						continue;
					}
					break;
				case 5:
					if (A_2 <= 0)
					{
						num = 18;
						continue;
					}
					num = 4;
					continue;
				case 6:
					num = 7;
					continue;
				case 7:
					goto IL_1A6;
				case 8:
				{
					int num2;
					int num3;
					this.ᜁ(this.\u1716, num3, num2 - num3);
					num = 19;
					continue;
				}
				case 9:
					goto IL_58C;
				case 10:
				{
					int num4 = (int)(this.\u1719 % 65535L);
					int num5 = Math.Min(65535 - num4, (int)(this.\u171A - this.\u1719));
					num5 = Math.Min(num5, A_2);
					Array.Copy(this.\u1716, num4, A_0, A_1, num5);
					this.\u1719 += (long)num5;
					A_1 += num5;
					A_2 -= num5;
					num = 43;
					continue;
				}
				case 11:
				{
					long u171A;
					if (u171A < this.\u171A)
					{
						num = 3;
						continue;
					}
					goto IL_4E1;
				}
				case 12:
				{
					bool flag;
					if (!flag)
					{
						num = 24;
						continue;
					}
					goto IL_58C;
				}
				case 13:
					if (!this.ᜀ())
					{
						num = 6;
						continue;
					}
					goto IL_58C;
				case 14:
					if (A_1 >= 0)
					{
						num = 21;
						continue;
					}
					goto IL_1CE;
				case 15:
					goto IL_4E1;
				case 16:
					goto IL_557;
				case 17:
					if (!this.\u1717)
					{
						num = 45;
						continue;
					}
					goto IL_644;
				case 18:
					goto IL_1A6;
				case 19:
					goto IL_4E1;
				case 20:
					goto IL_333;
				case 21:
					num = 33;
					continue;
				case 22:
					if (!this.\u171E)
					{
						num = 38;
						continue;
					}
					goto IL_644;
				case 23:
					if (!this.ᜡ)
					{
						num = 26;
						continue;
					}
					goto IL_644;
				case 24:
					num = 47;
					continue;
				case 25:
				{
					int num2;
					if (num2 > 0)
					{
						num = 0;
						continue;
					}
					goto IL_4E1;
				}
				case 26:
					goto IL_294;
				case 27:
					num = 13;
					continue;
				case 28:
					if (!this.\u171B)
					{
						num = 27;
						continue;
					}
					num = 31;
					continue;
				case 29:
					goto IL_17F;
				case 31:
				{
					if (this.\u171C == 0)
					{
						num = 44;
						continue;
					}
					int num6 = (int)(this.\u171A % 65535L);
					int num7 = Math.Min(this.\u171C, 65535 - num6);
					int num8 = this.ᜂ(this.\u1716, num6, num7);
					num = 46;
					continue;
				}
				case 32:
					if (A_2 >= 0)
					{
						num = 1;
						continue;
					}
					goto IL_184;
				case 33:
					if (A_1 > A_0.Length - 1)
					{
						num = 16;
						continue;
					}
					num = 32;
					continue;
				case 34:
					goto IL_2CF;
				case 35:
					goto IL_FE;
				case 36:
					if (this.\u171E)
					{
						num = 41;
						continue;
					}
					goto IL_1A6;
				case 37:
				{
					int num2;
					int num3;
					if (num3 < num2)
					{
						num = 8;
						continue;
					}
					this.ᜁ(this.\u1716, num3, 65535 - num3);
					num = 25;
					continue;
				}
				case 38:
					num = 23;
					continue;
				case 39:
					if (A_2 > A_0.Length - A_1)
					{
						num = 20;
						continue;
					}
					num9 = A_2;
					num = 15;
					continue;
				case 40:
				{
					long num10;
					if (num10 != this.\u1712)
					{
						num = 2;
						continue;
					}
					this.ᜡ = true;
					num = 34;
					continue;
				}
				case 41:
				{
					long u171A = this.\u171A;
					num = 28;
					continue;
				}
				case 42:
					goto IL_4E1;
				case 43:
					goto IL_4E1;
				case 44:
				{
					bool flag = this.\u171E = this.ᜉ();
					num = 12;
					continue;
				}
				case 45:
				{
					this.ᜄ();
					long num10 = this.ᜆ();
					num = 40;
					continue;
				}
				case 46:
				{
					int num7;
					int num8;
					if (num7 != num8)
					{
						num = 29;
						continue;
					}
					this.\u171C -= num8;
					this.\u171A += (long)num8;
					num = 9;
					continue;
				}
				case 47:
					goto IL_1A6;
				}
				if (A_0 == null)
				{
					num = 35;
					continue;
				}
				num = 14;
				continue;
				IL_1A6:
				num = 22;
				continue;
				IL_294:
				num = 17;
				continue;
				IL_4E1:
				num = 5;
				continue;
				IL_58C:
				num = 11;
			}
			IL_FE:
			throw new ArgumentNullException(ClipboardData.b("൮Ѱᕲ፴ቶ୸", a_));
			IL_17F:
			throw new FormatException(ClipboardData.b("ⅮṰݲ啴ቶ᝸ᑺࡼ᡾ꎂﶈ권ﾐ뎒ﺚﲜ辠", a_));
			IL_184:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ͮᑰᵲቴͶᅸ", a_), ClipboardData.b("⍮ᑰᵲቴͶᅸ孺ᑼ౾ꆀ뾐", a_));
			IL_1CE:
			throw new ArgumentOutOfRangeException(ClipboardData.b("nᝰᕲٴቶ൸", a_), ClipboardData.b("⁮ᝰᕲٴቶ൸孺᥼ၾꖄﾊ권ﾒ杖練ﺘ뮚膠킢햤슦쪨슪쮬욮풰ힲ閴햶첸\uddba\udbbc\udabe돀", a_));
			IL_261:
			if (true)
			{
			}
			throw new Exception(ClipboardData.b("Ɱᥰᙲᙴᱶ੸๺ၼ彾ꮊﾒ래", a_));
			IL_2CF:
			goto IL_644;
			IL_333:
			goto IL_184;
			IL_557:
			goto IL_1CE;
			IL_644:
			return num9 - A_2;
		}
		}
	}

	// Token: 0x06003600 RID: 13824 RVA: 0x0032B900 File Offset: 0x0032A900
	// Note: this type is marked as 'beforefieldinit'.
	static sprᢹ()
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
		sprᢹ.ᜋ = new int[]
		{
			3,
			3,
			11
		};
		sprᢹ.ᜌ = new int[]
		{
			2,
			3,
			7
		};
		sprᢹ.\u170D = new int[]
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
		sprᢹ.ᜎ = new int[]
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
		sprᢹ.ᜏ = new int[]
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
		sprᢹ.ᜐ = new int[]
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

	// Token: 0x04002943 RID: 10563
	private const int ᜀ = 3840;

	// Token: 0x04002944 RID: 10564
	private const int ᜁ = 61440;

	// Token: 0x04002945 RID: 10565
	private const int ᜂ = 31;

	// Token: 0x04002946 RID: 10566
	private const int ᜃ = 32;

	// Token: 0x04002947 RID: 10567
	private const int ᜄ = 192;

	// Token: 0x04002948 RID: 10568
	private const int ᜅ = 65535;

	// Token: 0x04002949 RID: 10569
	private const int ᜆ = 258;

	// Token: 0x0400294A RID: 10570
	private const int ᜇ = 256;

	// Token: 0x0400294B RID: 10571
	private const int ᜈ = 257;

	// Token: 0x0400294C RID: 10572
	private const int ᜉ = 285;

	// Token: 0x0400294D RID: 10573
	private const int ᜊ = 29;

	// Token: 0x0400294E RID: 10574
	private static readonly int[] ᜋ;

	// Token: 0x0400294F RID: 10575
	private static readonly int[] ᜌ;

	// Token: 0x04002950 RID: 10576
	private static readonly int[] \u170D;

	// Token: 0x04002951 RID: 10577
	private static readonly int[] ᜎ;

	// Token: 0x04002952 RID: 10578
	private static readonly int[] ᜏ;

	// Token: 0x04002953 RID: 10579
	private static readonly int[] ᜐ;

	// Token: 0x04002954 RID: 10580
	private Stream ᜑ;

	// Token: 0x04002955 RID: 10581
	private long \u1712;

	// Token: 0x04002956 RID: 10582
	private uint \u1713;

	// Token: 0x04002957 RID: 10583
	private int \u1714;

	// Token: 0x04002958 RID: 10584
	private byte[] \u1715;

	// Token: 0x04002959 RID: 10585
	private byte[] \u1716;

	// Token: 0x0400295A RID: 10586
	private bool \u1717;

	// Token: 0x0400295B RID: 10587
	private int \u1718;

	// Token: 0x0400295C RID: 10588
	private long \u1719;

	// Token: 0x0400295D RID: 10589
	private long \u171A;

	// Token: 0x0400295E RID: 10590
	private bool \u171B;

	// Token: 0x0400295F RID: 10591
	private int \u171C;

	// Token: 0x04002960 RID: 10592
	private bool \u171D;

	// Token: 0x04002961 RID: 10593
	private bool \u171E;

	// Token: 0x04002962 RID: 10594
	private sprẒ \u171F;

	// Token: 0x04002963 RID: 10595
	private sprẒ ᜠ;

	// Token: 0x04002964 RID: 10596
	private bool ᜡ;
}
