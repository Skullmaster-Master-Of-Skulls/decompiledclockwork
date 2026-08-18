using System;
using Spire.CompoundFile.Doc;

// Token: 0x02000414 RID: 1044
internal class spr\u17CD
{
	// Token: 0x06003A00 RID: 14848 RVA: 0x0035EE14 File Offset: 0x0035DE14
	internal spr\u17CD()
	{
		this.ᜃ = new uint[20];
		this.ᜂ = 20U;
	}

	// Token: 0x06003A01 RID: 14849 RVA: 0x0035EE44 File Offset: 0x0035DE44
	internal spr\u17CD(spr\u17CD.Sign A_0, uint A_1)
	{
		this.ᜃ = new uint[A_1];
		this.ᜂ = A_1;
	}

	// Token: 0x06003A02 RID: 14850 RVA: 0x0035EE74 File Offset: 0x0035DE74
	internal spr\u17CD(spr\u17CD A_0)
	{
		this.ᜃ = (uint[])A_0.ᜃ.Clone();
		this.ᜂ = A_0.ᜂ;
	}

	// Token: 0x06003A03 RID: 14851 RVA: 0x0035EEB0 File Offset: 0x0035DEB0
	internal spr\u17CD(spr\u17CD A_0, uint A_1)
	{
		this.ᜃ = new uint[A_1];
		for (uint num = 0U; num < A_0.ᜂ; num += 1U)
		{
			this.ᜃ[(int)((UIntPtr)num)] = A_0.ᜃ[(int)((UIntPtr)num)];
		}
		this.ᜂ = A_0.ᜂ;
	}

	// Token: 0x06003A04 RID: 14852 RVA: 0x0035EF0C File Offset: 0x0035DF0C
	internal spr\u17CD(byte[] A_0)
	{
		this.ᜂ = (uint)A_0.Length >> 2;
		int num = A_0.Length & 3;
		if (num != 0)
		{
			this.ᜂ += 1U;
		}
		this.ᜃ = new uint[this.ᜂ];
		int i = A_0.Length - 1;
		int num2 = 0;
		while (i >= 3)
		{
			this.ᜃ[num2] = (uint)((int)A_0[i - 3] << 24 | (int)A_0[i - 2] << 16 | (int)A_0[i - 1] << 8 | (int)A_0[i]);
			i -= 4;
			num2++;
		}
		switch (num)
		{
		case 1:
			this.ᜃ[(int)((UIntPtr)(this.ᜂ - 1U))] = (uint)A_0[0];
			break;
		case 2:
			this.ᜃ[(int)((UIntPtr)(this.ᜂ - 1U))] = (uint)((int)A_0[0] << 8 | (int)A_0[1]);
			break;
		case 3:
			this.ᜃ[(int)((UIntPtr)(this.ᜂ - 1U))] = (uint)((int)A_0[0] << 16 | (int)A_0[1] << 8 | (int)A_0[2]);
			break;
		}
		this.ᜀ();
	}

	// Token: 0x06003A05 RID: 14853 RVA: 0x0035F018 File Offset: 0x0035E018
	internal spr\u17CD(uint[] A_0)
	{
		this.ᜂ = (uint)A_0.Length;
		this.ᜃ = new uint[this.ᜂ];
		int i = (int)(this.ᜂ - 1U);
		int num = 0;
		while (i >= 0)
		{
			this.ᜃ[num] = A_0[i];
			i--;
			num++;
		}
		this.ᜀ();
	}

	// Token: 0x06003A06 RID: 14854 RVA: 0x0035F07C File Offset: 0x0035E07C
	internal spr\u17CD(uint A_0)
	{
		this.ᜃ = new uint[]
		{
			A_0
		};
	}

	// Token: 0x06003A07 RID: 14855 RVA: 0x0035F0A8 File Offset: 0x0035E0A8
	internal spr\u17CD(ulong A_0)
	{
		this.ᜃ = new uint[]
		{
			(uint)A_0,
			(uint)(A_0 >> 32)
		};
		this.ᜂ = 2U;
		this.ᜀ();
	}

	// Token: 0x06003A08 RID: 14856 RVA: 0x0035F0EC File Offset: 0x0035E0EC
	public static spr\u17CD ᜀ(uint A_0)
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
		return new spr\u17CD(A_0);
	}

	// Token: 0x06003A09 RID: 14857 RVA: 0x0035F130 File Offset: 0x0035E130
	public static spr\u17CD ᜀ(int A_0)
	{
		int a_ = 13;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 >= 0)
			{
				return new spr\u17CD((uint)A_0);
			}
			break;
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ղᑴ᭶౸Ṻ", a_));
	}

	// Token: 0x06003A0A RID: 14858 RVA: 0x0035F194 File Offset: 0x0035E194
	public static spr\u17CD ᜀ(ulong A_0)
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
		return new spr\u17CD(A_0);
	}

	// Token: 0x06003A0B RID: 14859 RVA: 0x0035F1D8 File Offset: 0x0035E1D8
	internal static spr\u17CD ᜀ(string A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				bool flag;
				spr\u17CD spr_u17CD;
				int length;
				switch (num)
				{
				case 0:
					goto IL_AF;
				case 1:
					goto IL_153;
				case 2:
					if (A_0[num2] == '+')
					{
						num = 3;
						continue;
					}
					num = 15;
					continue;
				case 3:
					num2++;
					num = 11;
					continue;
				case 5:
				{
					char c;
					if (char.IsWhiteSpace(c))
					{
						num = 22;
						continue;
					}
					goto IL_2AF;
				}
				case 6:
					goto IL_FB;
				case 7:
					goto IL_114;
				case 8:
					goto IL_1E5;
				case 9:
					if (!flag)
					{
						num = 24;
						continue;
					}
					return spr_u17CD;
				case 10:
				{
					char c;
					if (c >= '0')
					{
						num = 23;
						continue;
					}
					goto IL_176;
				}
				case 11:
					goto IL_1E5;
				case 12:
					num2 = length;
					num = 7;
					continue;
				case 13:
					num = 19;
					continue;
				case 14:
				{
					char c;
					if (c <= '9')
					{
						num = 27;
						continue;
					}
					goto IL_176;
				}
				case 15:
					if (A_0[num2] == '-')
					{
						num = 21;
						continue;
					}
					goto IL_1E5;
				case 16:
					IL_30A:
					goto IL_B4;
				case 17:
				{
					if (num2 >= length)
					{
						num = 1;
						continue;
					}
					char c = A_0[num2];
					num = 20;
					continue;
				}
				case 18:
					goto IL_114;
				case 19:
					goto IL_153;
				case 20:
				{
					char c;
					if (c == '\0')
					{
						num = 12;
						continue;
					}
					num = 10;
					continue;
				}
				case 21:
					goto IL_268;
				case 22:
					num2++;
					num = 16;
					continue;
				case 23:
					num = 14;
					continue;
				case 24:
					goto IL_171;
				case 25:
					if (!char.IsWhiteSpace(A_0[num2]))
					{
						num = 6;
						continue;
					}
					num2++;
					num = 28;
					continue;
				case 26:
					if (num2 >= length)
					{
						num = 13;
						continue;
					}
					num = 25;
					continue;
				case 27:
				{
					if (true)
					{
					}
					char c;
					spr_u17CD = spr\u17CD.ᜏ(spr\u17CD.ᜅ(spr_u17CD, 10), spr\u17CD.ᜀ((int)(c - '0')));
					flag = true;
					num = 18;
					continue;
				}
				case 28:
					goto IL_B4;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num2 = 0;
				length = A_0.Length;
				flag = false;
				spr_u17CD = new spr\u17CD(0U);
				num = 2;
				continue;
				IL_B4:
				num = 26;
				continue;
				IL_176:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_30A;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				IL_114:
				num2++;
				num = 8;
				continue;
				IL_153:
				num = 9;
				continue;
				IL_1E5:
				num = 17;
			}
			IL_AF:
			throw new ArgumentNullException(ClipboardData.b("ṯݱᥳᑵᵷࡹ", a_));
			IL_FB:
			throw new FormatException();
			IL_171:
			throw new FormatException();
			IL_268:
			throw new ArgumentException(ClipboardData.b("㽯ɱᅳѵ᥷๹ᕻᅽꊁﶇ꺍벛ﾝ肟첡솣솥즧\udea9얫\ud8ad햯銱슳ힵ풷쾹\ud9bb", a_));
			IL_2AF:
			throw new FormatException();
		}
		}
	}

	// Token: 0x06003A0C RID: 14860 RVA: 0x0035F53C File Offset: 0x0035E53C
	public static spr\u17CD ᜏ(spr\u17CD A_0, spr\u17CD A_1)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_81;
				case 2:
					if (spr\u17CD.ᜁ(A_1, 0U))
					{
						num = 0;
						continue;
					}
					goto IL_8A;
				case 3:
					goto IL_31;
				}
				if (spr\u17CD.ᜁ(A_0, 0U))
				{
					num = 3;
				}
				else
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 2;
						break;
					}
				}
			}
		}
		IL_31:
		return new spr\u17CD(A_1);
		IL_81:
		return new spr\u17CD(A_0);
		IL_8A:
		return spr\u17CD.ᜀ.ᜇ(A_0, A_1);
	}

	// Token: 0x06003A0D RID: 14861 RVA: 0x0035F5DC File Offset: 0x0035E5DC
	public static spr\u17CD ᜎ(spr\u17CD A_0, spr\u17CD A_1)
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				spr\u17CD.Sign sign;
				switch (sign)
				{
				case spr\u17CD.Sign.Negative:
					goto IL_A8;
				case spr\u17CD.Sign.Zero:
					goto IL_6C;
				case spr\u17CD.Sign.Positive:
					goto IL_C3;
				default:
					num = 1;
					continue;
				}
				break;
			}
			case 1:
				num = 6;
				continue;
			case 3:
				goto IL_6A;
			case 4:
				goto IL_A6;
			case 5:
			{
				if (spr\u17CD.ᜁ(A_0, 0U))
				{
					num = 4;
					continue;
				}
				spr\u17CD.Sign sign = spr\u17CD.ᜀ.ᜃ(A_0, A_1);
				num = 0;
				continue;
			}
			case 6:
				goto IL_7B;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_A6;
			default:
				if (false)
				{
				}
				if (spr\u17CD.ᜁ(A_1, 0U))
				{
					if (true)
					{
					}
					num = 3;
				}
				else
				{
					num = 5;
				}
				break;
			}
		}
		IL_6A:
		return new spr\u17CD(A_0);
		IL_6C:
		return spr\u17CD.ᜀ(0);
		IL_7B:
		throw new InvalidOperationException();
		IL_A6:
		throw new ArithmeticException(ClipboardData.b("⁮Űᙲݴᙶ൸ቺቼᅾꆀ권ﶎ뮚ﲜ뾞쾠욢스욦\udda8슪\udbac쪮醰얲풴\udbb6첸\udeba", a_));
		IL_A8:
		throw new ArithmeticException(ClipboardData.b("⁮Űᙲݴᙶ൸ቺቼᅾꆀ권ﶎ뮚ﲜ뾞쾠욢스욦\udda8슪\udbac쪮醰얲풴\udbb6첸\udeba", a_));
		IL_C3:
		return spr\u17CD.ᜀ.ᜆ(A_0, A_1);
	}

	// Token: 0x06003A0E RID: 14862 RVA: 0x0035F704 File Offset: 0x0035E704
	public static int ᜇ(spr\u17CD A_0, int A_1)
	{
		if (A_1 > 0)
		{
			for (;;)
			{
				if (true)
				{
				}
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
			return (int)spr\u17CD.ᜀ.ᜄ(A_0, (uint)A_1);
		}
		return (int)(-(int)spr\u17CD.ᜀ.ᜄ(A_0, (uint)(-(uint)A_1)));
	}

	// Token: 0x06003A0F RID: 14863 RVA: 0x0035F758 File Offset: 0x0035E758
	public static uint ᜃ(spr\u17CD A_0, uint A_1)
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
		return spr\u17CD.ᜀ.ᜄ(A_0, A_1);
	}

	// Token: 0x06003A10 RID: 14864 RVA: 0x0035F79C File Offset: 0x0035E79C
	public static spr\u17CD \u170D(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜀ.ᜂ(A_0, A_1)[1];
	}

	// Token: 0x06003A11 RID: 14865 RVA: 0x0035F7E0 File Offset: 0x0035E7E0
	public static spr\u17CD ᜆ(spr\u17CD A_0, int A_1)
	{
		int a_ = 11;
		if (A_1 > 0)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_25;
				}
			}
			IL_25:
			if (false)
			{
			}
			if (true)
			{
			}
			return spr\u17CD.ᜀ.ᜃ(A_0, (uint)A_1);
		}
		throw new ArithmeticException(ClipboardData.b("㹰Ͳၴնᡸེᑼၾꎂﲈ꾎붜ﺞ膠춢삤삦좨\udfaa쒬\ud9ae풰鎲쎴횶햸캺\ud8bc", a_));
	}

	// Token: 0x06003A12 RID: 14866 RVA: 0x0035F848 File Offset: 0x0035E848
	public static spr\u17CD ᜌ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜀ.ᜂ(A_0, A_1)[0];
	}

	// Token: 0x06003A13 RID: 14867 RVA: 0x0035F88C File Offset: 0x0035E88C
	public static spr\u17CD ᜋ(spr\u17CD A_0, spr\u17CD A_1)
	{
		int a_ = 0;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E3;
			case 1:
				if (spr\u17CD.ᜁ(A_1, 0U))
				{
					num = 0;
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
				break;
			case 3:
				goto IL_C2;
			case 4:
				num = 1;
				continue;
			case 5:
				if ((long)A_0.ᜃ.Length < (long)((ulong)A_0.ᜂ))
				{
					num = 3;
					continue;
				}
				num = 6;
				continue;
			case 6:
				if (true)
				{
				}
				if ((long)A_1.ᜃ.Length < (long)((ulong)A_1.ᜂ))
				{
					num = 7;
					continue;
				}
				goto IL_11E;
			case 7:
				goto IL_7C;
			}
			if (spr\u17CD.ᜁ(A_0, 0U))
			{
				goto IL_F9;
			}
			num = 4;
		}
		IL_7C:
		throw new IndexOutOfRangeException(ClipboardData.b("ѥŧ塩䱫ŭկٱ味᥵ṷ婹๻ώ", a_));
		IL_C2:
		throw new IndexOutOfRangeException(ClipboardData.b("ѥŧ孩䱫ŭկٱ味᥵ṷ婹๻ώ", a_));
		IL_E3:
		IL_F9:
		return spr\u17CD.ᜀ(0);
		IL_11E:
		spr\u17CD spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ + A_1.ᜂ);
		spr\u17CD.ᜀ.ᜀ(A_0.ᜃ, 0U, A_0.ᜂ, A_1.ᜃ, 0U, A_1.ᜂ, spr_u17CD.ᜃ, 0U);
		spr_u17CD.ᜀ();
		return spr_u17CD;
	}

	// Token: 0x06003A14 RID: 14868 RVA: 0x0035F9F8 File Offset: 0x0035E9F8
	public static spr\u17CD ᜅ(spr\u17CD A_0, int A_1)
	{
		int a_ = 14;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_BE;
			case 2:
				goto IL_3D;
			case 3:
				if (A_1 == 0)
				{
					num = 1;
					continue;
				}
				num = 4;
				continue;
			case 4:
				if (A_1 == 1)
				{
					num = 5;
					continue;
				}
				goto IL_C0;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_7B;
				}
				break;
			}
			if (A_1 < 0)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
		}
		IL_3D:
		if (true)
		{
		}
		throw new ArithmeticException(ClipboardData.b("㭳ٵᵷࡹᵻ੽ꚅﾇ曆늑肟쎡蒣좥춧충춫\udaad\ud9af쒱톳隵캷\udbb9킻쮽ꖿ", a_));
		IL_7B:
		if (false)
		{
		}
		return new spr\u17CD(A_0);
		IL_BE:
		return spr\u17CD.ᜀ(0);
		IL_C0:
		return spr\u17CD.ᜀ.ᜁ(A_0, (uint)A_1);
	}

	// Token: 0x06003A15 RID: 14869 RVA: 0x0035FACC File Offset: 0x0035EACC
	public static spr\u17CD ᜄ(spr\u17CD A_0, int A_1)
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
		return spr\u17CD.ᜀ.ᜁ(A_0, A_1);
	}

	// Token: 0x06003A16 RID: 14870 RVA: 0x0035FB10 File Offset: 0x0035EB10
	public static spr\u17CD ᜃ(spr\u17CD A_0, int A_1)
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
		return spr\u17CD.ᜀ.ᜀ(A_0, A_1);
	}

	// Token: 0x06003A17 RID: 14871 RVA: 0x0035FB54 File Offset: 0x0035EB54
	internal static spr\u17CD ᜊ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜏ(A_0, A_1);
	}

	// Token: 0x06003A18 RID: 14872 RVA: 0x0035FB98 File Offset: 0x0035EB98
	internal static spr\u17CD ᜉ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜎ(A_0, A_1);
	}

	// Token: 0x06003A19 RID: 14873 RVA: 0x0035FBDC File Offset: 0x0035EBDC
	internal static int ᜂ(spr\u17CD A_0, int A_1)
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
		return spr\u17CD.ᜇ(A_0, A_1);
	}

	// Token: 0x06003A1A RID: 14874 RVA: 0x0035FC20 File Offset: 0x0035EC20
	internal static uint ᜂ(spr\u17CD A_0, uint A_1)
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
		return spr\u17CD.ᜃ(A_0, A_1);
	}

	// Token: 0x06003A1B RID: 14875 RVA: 0x0035FC64 File Offset: 0x0035EC64
	internal static spr\u17CD ᜈ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.\u170D(A_0, A_1);
	}

	// Token: 0x06003A1C RID: 14876 RVA: 0x0035FCA8 File Offset: 0x0035ECA8
	internal static spr\u17CD ᜁ(spr\u17CD A_0, int A_1)
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
		return spr\u17CD.ᜆ(A_0, A_1);
	}

	// Token: 0x06003A1D RID: 14877 RVA: 0x0035FCEC File Offset: 0x0035ECEC
	internal static spr\u17CD ᜇ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜌ(A_0, A_1);
	}

	// Token: 0x06003A1E RID: 14878 RVA: 0x0035FD30 File Offset: 0x0035ED30
	internal static spr\u17CD ᜆ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜋ(A_0, A_1);
	}

	// Token: 0x06003A1F RID: 14879 RVA: 0x0035FD74 File Offset: 0x0035ED74
	internal static spr\u17CD ᜀ(spr\u17CD A_0, int A_1)
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
		return spr\u17CD.ᜅ(A_0, A_1);
	}

	// Token: 0x06003A20 RID: 14880 RVA: 0x0035FDB8 File Offset: 0x0035EDB8
	public int ᜆ()
	{
		uint num3;
		for (;;)
		{
			this.ᜀ();
			uint num = this.ᜃ[(int)((UIntPtr)(this.ᜂ - 1U))];
			uint num2 = 2147483648U;
			num3 = 32U;
			int num4 = 2;
			for (;;)
			{
				switch (num4)
				{
				case 0:
					num4 = 5;
					continue;
				case 1:
					goto IL_88;
				case 2:
					goto IL_48;
				case 3:
					if (num3 > 0U)
					{
						num4 = 0;
						continue;
					}
					goto IL_BB;
				case 4:
					goto IL_8A;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_48;
					default:
						if (false)
						{
						}
						if ((num & num2) != 0U)
						{
							num4 = 1;
							continue;
						}
						num3 -= 1U;
						num2 >>= 1;
						num4 = 4;
						continue;
					}
					break;
				}
				break;
				IL_8A:
				num4 = 3;
				continue;
				IL_48:
				goto IL_8A;
			}
		}
		IL_88:
		IL_BB:
		if (true)
		{
		}
		return (int)(num3 + (this.ᜂ - 1U << 5));
	}

	// Token: 0x06003A21 RID: 14881 RVA: 0x0035FE98 File Offset: 0x0035EE98
	internal bool ᜂ(uint A_0)
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
		uint num = A_0 >> 5;
		byte b = (byte)(A_0 & 31U);
		uint num2 = 1U << (int)b;
		return (this.ᜃ[(int)((UIntPtr)num)] & num2) != 0U;
	}

	// Token: 0x06003A22 RID: 14882 RVA: 0x0035FEF8 File Offset: 0x0035EEF8
	internal bool ᜁ(int A_0)
	{
		int a_ = 7;
		if (A_0 < 0)
		{
			for (;;)
			{
				if (true)
				{
				}
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
			throw new IndexOutOfRangeException(ClipboardData.b("ཬٮհ㵲t᩶奸ᑺࡼ୾ꆀꞆﮈ", a_));
		}
		uint num = (uint)A_0 >> 5;
		byte b = (byte)(A_0 & 31);
		uint num2 = 1U << (int)b;
		return (this.ᜃ[(int)((UIntPtr)num)] | num2) == this.ᜃ[(int)((UIntPtr)num)];
	}

	// Token: 0x06003A23 RID: 14883 RVA: 0x0035FF80 File Offset: 0x0035EF80
	internal void ᜄ(uint A_0)
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
		this.ᜀ(A_0, true);
	}

	// Token: 0x06003A24 RID: 14884 RVA: 0x0035FFC4 File Offset: 0x0035EFC4
	internal void ᜃ(uint A_0)
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
		this.ᜀ(A_0, false);
	}

	// Token: 0x06003A25 RID: 14885 RVA: 0x00360008 File Offset: 0x0035F008
	internal void ᜀ(uint A_0, bool A_1)
	{
		uint num;
		uint num3;
		for (;;)
		{
			if (true)
			{
			}
			num = A_0 >> 5;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_30;
				case 1:
					goto IL_D2;
				case 2:
					goto IL_92;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						if (false)
						{
						}
						num3 = 1U << (int)A_0;
						num2 = 4;
						continue;
					}
					break;
				case 4:
					if (A_1)
					{
						num2 = 1;
						continue;
					}
					this.ᜃ[(int)((UIntPtr)num)] &= ~num3;
					num2 = 2;
					continue;
				}
				break;
				IL_30:
				if (num >= this.ᜂ)
				{
					return;
				}
				num2 = 3;
			}
		}
		IL_92:
		return;
		IL_D2:
		this.ᜃ[(int)((UIntPtr)num)] |= num3;
	}

	// Token: 0x06003A26 RID: 14886 RVA: 0x003600EC File Offset: 0x0035F0EC
	internal int ᜁ()
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
				goto IL_85;
			case 2:
				return -1;
			case 3:
				goto IL_85;
			case 4:
			{
				int num2;
				return num2;
			}
			case 5:
			{
				int num2;
				if (this.ᜁ(num2))
				{
					num = 4;
					continue;
				}
				num2++;
				num = 1;
				continue;
			}
			}
			if (spr\u17CD.ᜁ(this, 0U))
			{
				num = 2;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
			{
				if (false)
				{
				}
				int num2 = 0;
				num = 3;
				continue;
			}
			}
			IL_85:
			num = 5;
		}
		return -1;
	}

	// Token: 0x06003A27 RID: 14887 RVA: 0x003601A0 File Offset: 0x0035F1A0
	internal byte[] ᜂ()
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				byte[] array;
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				switch (num)
				{
				case 0:
					return array;
				case 1:
					goto IL_10F;
				case 2:
					goto IL_72;
				case 3:
					if ((num2 & 7) != 0)
					{
						num = 9;
						continue;
					}
					goto IL_1C4;
				case 4:
					num3 += num4;
					num4 = 4;
					num5--;
					num = 11;
					continue;
				case 5:
					if (num4 == 0)
					{
						num = 14;
						continue;
					}
					goto IL_A1;
				case 6:
					goto IL_1C4;
				case 7:
					goto IL_A1;
				case 9:
					num6++;
					num = 6;
					continue;
				case 10:
					goto IL_131;
				case 11:
					goto IL_131;
				case 12:
				{
					if (true)
					{
					}
					if (num5 < 0)
					{
						num = 0;
						continue;
					}
					uint num7 = this.ᜃ[num5];
					int num8 = num4 - 1;
					num = 13;
					continue;
				}
				case 13:
					goto IL_10F;
				case 14:
					num4 = 4;
					num = 7;
					continue;
				case 15:
				{
					int num8;
					if (num8 < 0)
					{
						num = 4;
						continue;
					}
					uint num7;
					array[num3 + num8] = (byte)(num7 & 255U);
					num7 >>= 8;
					num8--;
					num = 1;
					continue;
				}
				}
				if (spr\u17CD.ᜁ(this, 0U))
				{
					num = 2;
					continue;
				}
				num2 = this.ᜆ();
				num6 = num2 >> 3;
				num = 3;
				continue;
				IL_A1:
				num3 = 0;
				num5 = (int)(this.ᜂ - 1U);
				num = 10;
				continue;
				IL_10F:
				num = 15;
				continue;
				IL_131:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				IL_1C4:
				array = new byte[num6];
				num4 = (num6 & 3);
				num = 5;
			}
			IL_72:
			return new byte[1];
		}
		}
	}

	// Token: 0x06003A28 RID: 14888 RVA: 0x003603A0 File Offset: 0x0035F3A0
	public static bool ᜁ(spr\u17CD A_0, uint A_1)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.ᜂ == 1U)
				{
					num = 3;
					continue;
				}
				return false;
			case 1:
				goto IL_71;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8A;
				default:
					if (false)
					{
					}
					A_0.ᜀ();
					num = 1;
					continue;
				}
				break;
			case 3:
				goto IL_8A;
			}
			if (A_0.ᜂ != 1U)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			IL_71:
			num = 0;
		}
		IL_8A:
		return A_0.ᜃ[0] == A_1;
	}

	// Token: 0x06003A29 RID: 14889 RVA: 0x00360450 File Offset: 0x0035F450
	public static bool ᜀ(spr\u17CD A_0, uint A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_90;
			case 2:
				if (A_0.ᜂ == 1U)
				{
					num = 1;
					continue;
				}
				return true;
			case 3:
				goto IL_74;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_90;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					A_0.ᜀ();
					num = 3;
					continue;
				}
				break;
			}
			if (A_0.ᜂ != 1U)
			{
				num = 4;
				continue;
			}
			IL_74:
			num = 2;
		}
		IL_90:
		return A_0.ᜃ[0] != A_1;
	}

	// Token: 0x06003A2A RID: 14890 RVA: 0x00360504 File Offset: 0x0035F504
	public static bool ᜅ(spr\u17CD A_0, spr\u17CD A_1)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!spr\u17CD.ᜅ(null, A_0))
				{
					num = 3;
					continue;
				}
				return false;
			case 1:
				if (true)
				{
				}
				if (spr\u17CD.ᜅ(null, A_1))
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				return false;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 5:
				return true;
			}
			goto IL_28;
			IL_2C:
			num = 5;
			continue;
			IL_28:
			if (A_0 == A_1)
			{
				goto IL_2C;
			}
			num = 0;
		}
		return true;
		IL_A1:
		return spr\u17CD.ᜀ.ᜃ(A_0, A_1) == spr\u17CD.Sign.Zero;
	}

	// Token: 0x06003A2B RID: 14891 RVA: 0x003605BC File Offset: 0x0035F5BC
	public static bool ᜄ(spr\u17CD A_0, spr\u17CD A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return true;
			case 2:
				return false;
			case 3:
				if (spr\u17CD.ᜅ(null, A_1))
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 5:
				if (!spr\u17CD.ᜅ(null, A_0))
				{
					num = 4;
					continue;
				}
				return true;
			}
			goto IL_28;
			IL_34:
			num = 2;
			continue;
			IL_28:
			if (true)
			{
			}
			if (A_0 == A_1)
			{
				goto IL_34;
			}
			num = 5;
		}
		return false;
		IL_A1:
		return spr\u17CD.ᜀ.ᜃ(A_0, A_1) != spr\u17CD.Sign.Zero;
	}

	// Token: 0x06003A2C RID: 14892 RVA: 0x00360678 File Offset: 0x0035F678
	public static bool ᜃ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜀ.ᜃ(A_0, A_1) > spr\u17CD.Sign.Zero;
	}

	// Token: 0x06003A2D RID: 14893 RVA: 0x003606C0 File Offset: 0x0035F6C0
	public static bool ᜂ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜀ.ᜃ(A_0, A_1) < spr\u17CD.Sign.Zero;
	}

	// Token: 0x06003A2E RID: 14894 RVA: 0x00360708 File Offset: 0x0035F708
	public static bool ᜁ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜀ.ᜃ(A_0, A_1) >= spr\u17CD.Sign.Zero;
	}

	// Token: 0x06003A2F RID: 14895 RVA: 0x00360750 File Offset: 0x0035F750
	public static bool ᜀ(spr\u17CD A_0, spr\u17CD A_1)
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
		return spr\u17CD.ᜀ.ᜃ(A_0, A_1) <= spr\u17CD.Sign.Zero;
	}

	// Token: 0x06003A30 RID: 14896 RVA: 0x00360798 File Offset: 0x0035F798
	internal spr\u17CD.Sign ᜂ(spr\u17CD A_0)
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
		return spr\u17CD.ᜀ.ᜃ(this, A_0);
	}

	// Token: 0x06003A31 RID: 14897 RVA: 0x003607DC File Offset: 0x0035F7DC
	internal string ᜁ(uint A_0)
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ(A_0, ClipboardData.b("幭䅯䁱䝳䉵䵷䱹䭻䙽륿쎁욃얅첇쾉쪋즍\ud88f\udb91\ude93\udd95풗힙튛톝ﲧﾩ磌玲", a_));
	}

	// Token: 0x06003A32 RID: 14898 RVA: 0x00360838 File Offset: 0x0035F838
	internal string ᜀ(uint A_0, string A_1)
	{
		int a_ = 3;
		int num = 2;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_11C;
			case 1:
				goto IL_97;
			case 3:
				goto IL_185;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_17A;
				default:
					goto IL_15B;
				}
				break;
			case 5:
				goto IL_5C;
			case 6:
			{
				spr\u17CD a_2;
				if (!spr\u17CD.ᜀ(a_2, 0U))
				{
					num = 4;
					continue;
				}
				uint index = spr\u17CD.ᜀ.ᜅ(a_2, A_0);
				text = A_1[(int)index] + text;
				num = 0;
				continue;
			}
			case 7:
				if (spr\u17CD.ᜁ(this, 0U))
				{
					goto IL_17A;
				}
				num = 10;
				continue;
			case 8:
				if (A_0 == 1U)
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
			case 9:
				goto IL_B6;
			case 10:
			{
				if (spr\u17CD.ᜁ(this, 1U))
				{
					num = 1;
					continue;
				}
				text = "";
				spr\u17CD a_2 = new spr\u17CD(this);
				num = 11;
				continue;
			}
			case 11:
				goto IL_11C;
			}
			if ((long)A_1.Length < (long)((ulong)A_0))
			{
				num = 5;
				continue;
			}
			num = 8;
			continue;
			IL_11C:
			num = 6;
			continue;
			IL_17A:
			num = 3;
		}
		IL_5C:
		if (true)
		{
		}
		throw new ArgumentException(ClipboardData.b("੨ͪ౬ᵮ≰ᙲŴ坶ᕸṺ፼᡾ꖄﺌ꾎ﮒ練릘ﲜﮞ좠\udba2", a_), ClipboardData.b("੨ͪ౬ᵮၰၲŴቶ୸⡺᡼୾", a_));
		IL_97:
		return ClipboardData.b("塨", a_);
		IL_B6:
		throw new ArgumentException(ClipboardData.b("㵨ͪ࡬ᵮᑰ卲ᱴѶ奸ᕺቼ彾ꦈﾊﾐ떔뮚ﺞ얠쪢\udda4螦욨얪좬辮\udfb0\udcb2솴횶춸튺튼톾", a_), ClipboardData.b("᭨੪६ٮ॰", a_));
		IL_15B:
		if (false)
		{
		}
		return text;
		IL_185:
		return ClipboardData.b("奨", a_);
	}

	// Token: 0x06003A33 RID: 14899 RVA: 0x00360A00 File Offset: 0x0035FA00
	private void ᜀ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜂ == 0U)
				{
					num = 4;
					continue;
				}
				return;
			case 2:
				return;
			case 4:
				this.ᜂ += 1U;
				num = 2;
				continue;
			case 5:
				goto IL_39;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 7:
				if (this.ᜂ > 0U)
				{
					num = 6;
					continue;
				}
				goto IL_39;
			case 8:
				if (this.ᜃ[(int)((UIntPtr)(this.ᜂ - 1U))] != 0U)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				this.ᜂ -= 1U;
				num = 3;
				continue;
			}
			goto IL_34;
			IL_39:
			num = 0;
			continue;
			IL_E1:
			num = 7;
			continue;
			IL_34:
			goto IL_E1;
		}
	}

	// Token: 0x06003A34 RID: 14900 RVA: 0x00360B18 File Offset: 0x0035FB18
	internal void ᜅ()
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_24;
				case 1:
					return;
				case 2:
					goto IL_24;
				case 3:
					if ((long)num >= (long)((ulong)this.ᜂ))
					{
						num2 = 1;
						continue;
					}
					this.ᜃ[num] = 0U;
					num++;
					num2 = 2;
					continue;
				}
				break;
				IL_24:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_18;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num2 = 3;
					break;
				}
			}
		}
	}

	// Token: 0x06003A35 RID: 14901 RVA: 0x00360BB0 File Offset: 0x0035FBB0
	public virtual int ᜃ()
	{
		uint num;
		for (;;)
		{
			IL_18:
			num = 0U;
			uint num2 = 0U;
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num2 >= this.ᜂ)
					{
						num3 = 1;
						continue;
					}
					num ^= this.ᜃ[(int)((UIntPtr)num2)];
					num2 += 1U;
					num3 = 2;
					continue;
				case 1:
					return (int)num;
				case 2:
					goto IL_26;
				case 3:
					goto IL_26;
				}
				break;
				IL_26:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_18;
				default:
					if (false)
					{
					}
					num3 = 0;
					break;
				}
			}
		}
		return (int)num;
	}

	// Token: 0x06003A36 RID: 14902 RVA: 0x00360C4C File Offset: 0x0035FC4C
	public virtual string ᜄ()
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
		return this.ᜁ(10U);
	}

	// Token: 0x06003A37 RID: 14903 RVA: 0x00360C90 File Offset: 0x0035FC90
	public virtual bool ᜀ(object A_0)
	{
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_89;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_89;
				case 2:
					num = 5;
					continue;
				case 3:
					if (A_0 is int)
					{
						num = 2;
						continue;
					}
					goto IL_AF;
				case 4:
					return false;
				case 5:
					if ((int)A_0 >= 0)
					{
						num = 0;
						continue;
					}
					return false;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 3;
				}
				break;
			}
		}
		return false;
		IL_89:
		return spr\u17CD.ᜁ(this, (uint)A_0);
		IL_AF:
		return spr\u17CD.ᜀ.ᜃ(this, (spr\u17CD)A_0) == spr\u17CD.Sign.Zero;
	}

	// Token: 0x06003A38 RID: 14904 RVA: 0x00360D5C File Offset: 0x0035FD5C
	internal spr\u17CD ᜁ(spr\u17CD A_0)
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
		return spr\u17CD.ᜀ.ᜁ(this, A_0);
	}

	// Token: 0x06003A39 RID: 14905 RVA: 0x00360DA0 File Offset: 0x0035FDA0
	internal spr\u17CD ᜀ(spr\u17CD A_0)
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
		return spr\u17CD.ᜀ.ᜀ(this, A_0);
	}

	// Token: 0x06003A3A RID: 14906 RVA: 0x00360DE4 File Offset: 0x0035FDE4
	internal spr\u17CD ᜐ(spr\u17CD A_0, spr\u17CD A_1)
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
		spr\u17CD.ᜂ ᜂ = new spr\u17CD.ᜂ(A_1);
		return ᜂ.ᜂ(this, A_0);
	}

	// Token: 0x06003A3B RID: 14907 RVA: 0x00360E30 File Offset: 0x0035FE30
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u17CD()
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
		spr\u17CD.ᜄ = new uint[]
		{
			2U,
			3U,
			5U,
			7U,
			11U,
			13U,
			17U,
			19U,
			23U,
			29U,
			31U,
			37U,
			41U,
			43U,
			47U,
			53U,
			59U,
			61U,
			67U,
			71U,
			73U,
			79U,
			83U,
			89U,
			97U,
			101U,
			103U,
			107U,
			109U,
			113U,
			127U,
			131U,
			137U,
			139U,
			149U,
			151U,
			157U,
			163U,
			167U,
			173U,
			179U,
			181U,
			191U,
			193U,
			197U,
			199U,
			211U,
			223U,
			227U,
			229U,
			233U,
			239U,
			241U,
			251U,
			257U,
			263U,
			269U,
			271U,
			277U,
			281U,
			283U,
			293U,
			307U,
			311U,
			313U,
			317U,
			331U,
			337U,
			347U,
			349U,
			353U,
			359U,
			367U,
			373U,
			379U,
			383U,
			389U,
			397U,
			401U,
			409U,
			419U,
			421U,
			431U,
			433U,
			439U,
			443U,
			449U,
			457U,
			461U,
			463U,
			467U,
			479U,
			487U,
			491U,
			499U,
			503U,
			509U,
			521U,
			523U,
			541U,
			547U,
			557U,
			563U,
			569U,
			571U,
			577U,
			587U,
			593U,
			599U,
			601U,
			607U,
			613U,
			617U,
			619U,
			631U,
			641U,
			643U,
			647U,
			653U,
			659U,
			661U,
			673U,
			677U,
			683U,
			691U,
			701U,
			709U,
			719U,
			727U,
			733U,
			739U,
			743U,
			751U,
			757U,
			761U,
			769U,
			773U,
			787U,
			797U,
			809U,
			811U,
			821U,
			823U,
			827U,
			829U,
			839U,
			853U,
			857U,
			859U,
			863U,
			877U,
			881U,
			883U,
			887U,
			907U,
			911U,
			919U,
			929U,
			937U,
			941U,
			947U,
			953U,
			967U,
			971U,
			977U,
			983U,
			991U,
			997U,
			1009U,
			1013U,
			1019U,
			1021U,
			1031U,
			1033U,
			1039U,
			1049U,
			1051U,
			1061U,
			1063U,
			1069U,
			1087U,
			1091U,
			1093U,
			1097U,
			1103U,
			1109U,
			1117U,
			1123U,
			1129U,
			1151U,
			1153U,
			1163U,
			1171U,
			1181U,
			1187U,
			1193U,
			1201U,
			1213U,
			1217U,
			1223U,
			1229U,
			1231U,
			1237U,
			1249U,
			1259U,
			1277U,
			1279U,
			1283U,
			1289U,
			1291U,
			1297U,
			1301U,
			1303U,
			1307U,
			1319U,
			1321U,
			1327U,
			1361U,
			1367U,
			1373U,
			1381U,
			1399U,
			1409U,
			1423U,
			1427U,
			1429U,
			1433U,
			1439U,
			1447U,
			1451U,
			1453U,
			1459U,
			1471U,
			1481U,
			1483U,
			1487U,
			1489U,
			1493U,
			1499U,
			1511U,
			1523U,
			1531U,
			1543U,
			1549U,
			1553U,
			1559U,
			1567U,
			1571U,
			1579U,
			1583U,
			1597U,
			1601U,
			1607U,
			1609U,
			1613U,
			1619U,
			1621U,
			1627U,
			1637U,
			1657U,
			1663U,
			1667U,
			1669U,
			1693U,
			1697U,
			1699U,
			1709U,
			1721U,
			1723U,
			1733U,
			1741U,
			1747U,
			1753U,
			1759U,
			1777U,
			1783U,
			1787U,
			1789U,
			1801U,
			1811U,
			1823U,
			1831U,
			1847U,
			1861U,
			1867U,
			1871U,
			1873U,
			1877U,
			1879U,
			1889U,
			1901U,
			1907U,
			1913U,
			1931U,
			1933U,
			1949U,
			1951U,
			1973U,
			1979U,
			1987U,
			1993U,
			1997U,
			1999U,
			2003U,
			2011U,
			2017U,
			2027U,
			2029U,
			2039U,
			2053U,
			2063U,
			2069U,
			2081U,
			2083U,
			2087U,
			2089U,
			2099U,
			2111U,
			2113U,
			2129U,
			2131U,
			2137U,
			2141U,
			2143U,
			2153U,
			2161U,
			2179U,
			2203U,
			2207U,
			2213U,
			2221U,
			2237U,
			2239U,
			2243U,
			2251U,
			2267U,
			2269U,
			2273U,
			2281U,
			2287U,
			2293U,
			2297U,
			2309U,
			2311U,
			2333U,
			2339U,
			2341U,
			2347U,
			2351U,
			2357U,
			2371U,
			2377U,
			2381U,
			2383U,
			2389U,
			2393U,
			2399U,
			2411U,
			2417U,
			2423U,
			2437U,
			2441U,
			2447U,
			2459U,
			2467U,
			2473U,
			2477U,
			2503U,
			2521U,
			2531U,
			2539U,
			2543U,
			2549U,
			2551U,
			2557U,
			2579U,
			2591U,
			2593U,
			2609U,
			2617U,
			2621U,
			2633U,
			2647U,
			2657U,
			2659U,
			2663U,
			2671U,
			2677U,
			2683U,
			2687U,
			2689U,
			2693U,
			2699U,
			2707U,
			2711U,
			2713U,
			2719U,
			2729U,
			2731U,
			2741U,
			2749U,
			2753U,
			2767U,
			2777U,
			2789U,
			2791U,
			2797U,
			2801U,
			2803U,
			2819U,
			2833U,
			2837U,
			2843U,
			2851U,
			2857U,
			2861U,
			2879U,
			2887U,
			2897U,
			2903U,
			2909U,
			2917U,
			2927U,
			2939U,
			2953U,
			2957U,
			2963U,
			2969U,
			2971U,
			2999U,
			3001U,
			3011U,
			3019U,
			3023U,
			3037U,
			3041U,
			3049U,
			3061U,
			3067U,
			3079U,
			3083U,
			3089U,
			3109U,
			3119U,
			3121U,
			3137U,
			3163U,
			3167U,
			3169U,
			3181U,
			3187U,
			3191U,
			3203U,
			3209U,
			3217U,
			3221U,
			3229U,
			3251U,
			3253U,
			3257U,
			3259U,
			3271U,
			3299U,
			3301U,
			3307U,
			3313U,
			3319U,
			3323U,
			3329U,
			3331U,
			3343U,
			3347U,
			3359U,
			3361U,
			3371U,
			3373U,
			3389U,
			3391U,
			3407U,
			3413U,
			3433U,
			3449U,
			3457U,
			3461U,
			3463U,
			3467U,
			3469U,
			3491U,
			3499U,
			3511U,
			3517U,
			3527U,
			3529U,
			3533U,
			3539U,
			3541U,
			3547U,
			3557U,
			3559U,
			3571U,
			3581U,
			3583U,
			3593U,
			3607U,
			3613U,
			3617U,
			3623U,
			3631U,
			3637U,
			3643U,
			3659U,
			3671U,
			3673U,
			3677U,
			3691U,
			3697U,
			3701U,
			3709U,
			3719U,
			3727U,
			3733U,
			3739U,
			3761U,
			3767U,
			3769U,
			3779U,
			3793U,
			3797U,
			3803U,
			3821U,
			3823U,
			3833U,
			3847U,
			3851U,
			3853U,
			3863U,
			3877U,
			3881U,
			3889U,
			3907U,
			3911U,
			3917U,
			3919U,
			3923U,
			3929U,
			3931U,
			3943U,
			3947U,
			3967U,
			3989U,
			4001U,
			4003U,
			4007U,
			4013U,
			4019U,
			4021U,
			4027U,
			4049U,
			4051U,
			4057U,
			4073U,
			4079U,
			4091U,
			4093U,
			4099U,
			4111U,
			4127U,
			4129U,
			4133U,
			4139U,
			4153U,
			4157U,
			4159U,
			4177U,
			4201U,
			4211U,
			4217U,
			4219U,
			4229U,
			4231U,
			4241U,
			4243U,
			4253U,
			4259U,
			4261U,
			4271U,
			4273U,
			4283U,
			4289U,
			4297U,
			4327U,
			4337U,
			4339U,
			4349U,
			4357U,
			4363U,
			4373U,
			4391U,
			4397U,
			4409U,
			4421U,
			4423U,
			4441U,
			4447U,
			4451U,
			4457U,
			4463U,
			4481U,
			4483U,
			4493U,
			4507U,
			4513U,
			4517U,
			4519U,
			4523U,
			4547U,
			4549U,
			4561U,
			4567U,
			4583U,
			4591U,
			4597U,
			4603U,
			4621U,
			4637U,
			4639U,
			4643U,
			4649U,
			4651U,
			4657U,
			4663U,
			4673U,
			4679U,
			4691U,
			4703U,
			4721U,
			4723U,
			4729U,
			4733U,
			4751U,
			4759U,
			4783U,
			4787U,
			4789U,
			4793U,
			4799U,
			4801U,
			4813U,
			4817U,
			4831U,
			4861U,
			4871U,
			4877U,
			4889U,
			4903U,
			4909U,
			4919U,
			4931U,
			4933U,
			4937U,
			4943U,
			4951U,
			4957U,
			4967U,
			4969U,
			4973U,
			4987U,
			4993U,
			4999U,
			5003U,
			5009U,
			5011U,
			5021U,
			5023U,
			5039U,
			5051U,
			5059U,
			5077U,
			5081U,
			5087U,
			5099U,
			5101U,
			5107U,
			5113U,
			5119U,
			5147U,
			5153U,
			5167U,
			5171U,
			5179U,
			5189U,
			5197U,
			5209U,
			5227U,
			5231U,
			5233U,
			5237U,
			5261U,
			5273U,
			5279U,
			5281U,
			5297U,
			5303U,
			5309U,
			5323U,
			5333U,
			5347U,
			5351U,
			5381U,
			5387U,
			5393U,
			5399U,
			5407U,
			5413U,
			5417U,
			5419U,
			5431U,
			5437U,
			5441U,
			5443U,
			5449U,
			5471U,
			5477U,
			5479U,
			5483U,
			5501U,
			5503U,
			5507U,
			5519U,
			5521U,
			5527U,
			5531U,
			5557U,
			5563U,
			5569U,
			5573U,
			5581U,
			5591U,
			5623U,
			5639U,
			5641U,
			5647U,
			5651U,
			5653U,
			5657U,
			5659U,
			5669U,
			5683U,
			5689U,
			5693U,
			5701U,
			5711U,
			5717U,
			5737U,
			5741U,
			5743U,
			5749U,
			5779U,
			5783U,
			5791U,
			5801U,
			5807U,
			5813U,
			5821U,
			5827U,
			5839U,
			5843U,
			5849U,
			5851U,
			5857U,
			5861U,
			5867U,
			5869U,
			5879U,
			5881U,
			5897U,
			5903U,
			5923U,
			5927U,
			5939U,
			5953U,
			5981U,
			5987U
		};
	}

	// Token: 0x04002B06 RID: 11014
	private const uint ᜀ = 20U;

	// Token: 0x04002B07 RID: 11015
	private const string ᜁ = "Operation would return a negative value";

	// Token: 0x04002B08 RID: 11016
	private uint ᜂ = 1U;

	// Token: 0x04002B09 RID: 11017
	private uint[] ᜃ;

	// Token: 0x04002B0A RID: 11018
	internal static readonly uint[] ᜄ;

	// Token: 0x02000415 RID: 1045
	internal enum Sign
	{
		// Token: 0x04002B0C RID: 11020
		Negative = -1,
		// Token: 0x04002B0D RID: 11021
		Zero,
		// Token: 0x04002B0E RID: 11022
		Positive
	}

	// Token: 0x02000416 RID: 1046
	internal class ᜂ
	{
		// Token: 0x06003A3C RID: 14908 RVA: 0x00360E88 File Offset: 0x0035FE88
		internal ᜂ(spr\u17CD A_0)
		{
			this.ᜀ = A_0;
			uint num = this.ᜀ.ᜂ << 1;
			this.ᜁ = new spr\u17CD(spr\u17CD.Sign.Positive, num + 1U);
			this.ᜁ.ᜃ[(int)((UIntPtr)num)] = 1U;
			this.ᜁ = spr\u17CD.ᜌ(this.ᜁ, this.ᜀ);
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x00360EE8 File Offset: 0x0035FEE8
		internal void ᜀ(spr\u17CD A_0)
		{
			int a_ = 0;
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u17CD spr_u17CD = this.ᜀ;
					uint ᜂ = spr_u17CD.ᜂ;
					uint num = ᜂ + 1U;
					uint num2 = ᜂ - 1U;
					int num3 = 5;
					for (;;)
					{
						spr\u17CD spr_u17CD2;
						uint num4;
						spr\u17CD spr_u17CD4;
						switch (num3)
						{
						case 0:
							if (A_0.ᜂ <= num)
							{
								num3 = 4;
								continue;
							}
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
								num3 = 10;
								continue;
							}
							break;
						case 1:
							goto IL_118;
						case 2:
							if (!spr\u17CD.ᜁ(A_0, spr_u17CD))
							{
								num3 = 3;
								continue;
							}
							spr\u17CD.ᜀ.ᜅ(A_0, spr_u17CD);
							num3 = 14;
							continue;
						case 3:
							return;
						case 4:
							num3 = 11;
							continue;
						case 5:
							if (A_0.ᜂ < ᜂ)
							{
								num3 = 7;
								continue;
							}
							num3 = 13;
							continue;
						case 6:
						{
							if (spr\u17CD.ᜀ(spr_u17CD2, A_0))
							{
								num3 = 8;
								continue;
							}
							spr\u17CD spr_u17CD3 = new spr\u17CD(spr\u17CD.Sign.Positive, num + 1U);
							spr_u17CD3.ᜃ[(int)((UIntPtr)num)] = 1U;
							spr\u17CD.ᜀ.ᜅ(spr_u17CD3, spr_u17CD2);
							spr\u17CD.ᜀ.ᜄ(A_0, spr_u17CD3);
							num3 = 12;
							continue;
						}
						case 7:
							return;
						case 8:
							spr\u17CD.ᜀ.ᜅ(A_0, spr_u17CD2);
							num3 = 9;
							continue;
						case 9:
							goto IL_168;
						case 10:
							num4 = num;
							goto IL_1B6;
						case 11:
							num4 = A_0.ᜂ;
							goto IL_1B6;
						case 12:
							goto IL_168;
						case 13:
							if ((long)A_0.ᜃ.Length < (long)((ulong)A_0.ᜂ))
							{
								num3 = 1;
								continue;
							}
							spr_u17CD4 = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ - num2 + this.ᜁ.ᜂ);
							spr\u17CD.ᜀ.ᜀ(A_0.ᜃ, num2, A_0.ᜂ - num2, this.ᜁ.ᜃ, 0U, this.ᜁ.ᜂ, spr_u17CD4.ᜃ, 0U);
							num3 = 0;
							continue;
						case 14:
							goto IL_168;
						}
						break;
						IL_168:
						num3 = 2;
						continue;
						IL_1B6:
						uint ᜂ2 = num4;
						A_0.ᜂ = ᜂ2;
						A_0.ᜀ();
						spr_u17CD2 = new spr\u17CD(spr\u17CD.Sign.Positive, num);
						spr\u17CD.ᜀ.ᜀ(spr_u17CD4.ᜃ, (int)num, (int)(spr_u17CD4.ᜂ - num), spr_u17CD.ᜃ, 0, (int)spr_u17CD.ᜂ, spr_u17CD2.ᜃ, 0, (int)num);
						spr_u17CD2.ᜀ();
						num3 = 6;
					}
				}
				return;
				IL_118:
				throw new IndexOutOfRangeException(ClipboardData.b("ṥ䡧թᥫᩭ偯ᵱታ噵੷᭹ቻ᥽", a_));
			}
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x0036119C File Offset: 0x0036019C
		internal spr\u17CD ᜁ(spr\u17CD A_0, spr\u17CD A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_CA;
				case 2:
					if (A_0.ᜂ >= this.ᜀ.ᜂ << 1)
					{
						num = 14;
						continue;
					}
					goto IL_1A3;
				case 3:
					goto IL_117;
				case 4:
					num = 13;
					continue;
				case 5:
					if (A_1.ᜂ >= this.ᜀ.ᜂ << 1)
					{
						num = 12;
						continue;
					}
					goto IL_119;
				case 6:
					if (A_1.ᜂ >= this.ᜀ.ᜂ)
					{
						num = 10;
						continue;
					}
					goto IL_1D6;
				case 7:
					goto IL_1A3;
				case 8:
					goto IL_119;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F6;
					default:
						if (false)
						{
						}
						this.ᜀ(A_0);
						num = 1;
						continue;
					}
					break;
				case 10:
					goto IL_F6;
				case 11:
					if (A_0.ᜂ >= this.ᜀ.ᜂ)
					{
						num = 9;
						continue;
					}
					goto IL_CA;
				case 12:
					A_1 = spr\u17CD.\u170D(A_1, this.ᜀ);
					if (true)
					{
					}
					num = 8;
					continue;
				case 13:
					if (spr\u17CD.ᜁ(A_1, 0U))
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				case 14:
					A_0 = spr\u17CD.\u170D(A_0, this.ᜀ);
					num = 7;
					continue;
				case 15:
					goto IL_159;
				}
				if (!spr\u17CD.ᜁ(A_0, 0U))
				{
					num = 4;
					continue;
				}
				break;
				IL_CA:
				num = 6;
				continue;
				IL_F6:
				this.ᜀ(A_1);
				num = 15;
				continue;
				IL_119:
				num = 11;
				continue;
				IL_1A3:
				num = 5;
			}
			IL_C3:
			return spr\u17CD.ᜀ(0);
			IL_117:
			goto IL_C3;
			IL_159:
			IL_1D6:
			spr\u17CD spr_u17CD = new spr\u17CD(spr\u17CD.ᜋ(A_0, A_1));
			this.ᜀ(spr_u17CD);
			return spr_u17CD;
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00361394 File Offset: 0x00360394
		internal spr\u17CD ᜃ(spr\u17CD A_0, spr\u17CD A_1)
		{
			for (;;)
			{
				IL_40:
				spr\u17CD.Sign sign = spr\u17CD.ᜀ.ᜃ(A_0, A_1);
				spr\u17CD.Sign sign2 = sign;
				for (;;)
				{
					IL_4A:
					int num = 12;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (sign == spr\u17CD.Sign.Negative)
							{
								num = 5;
								continue;
							}
							spr\u17CD spr_u17CD;
							return spr_u17CD;
						}
						case 1:
						{
							spr\u17CD spr_u17CD = spr\u17CD.\u170D(spr_u17CD, this.ᜀ);
							num = 6;
							continue;
						}
						case 2:
						{
							spr\u17CD spr_u17CD;
							if (spr_u17CD.ᜂ >= this.ᜀ.ᜂ << 1)
							{
								num = 1;
								continue;
							}
							this.ᜀ(spr_u17CD);
							num = 11;
							continue;
						}
						case 3:
						{
							spr\u17CD spr_u17CD;
							return spr_u17CD;
						}
						case 4:
							goto IL_8D;
						case 5:
						{
							spr\u17CD spr_u17CD = spr\u17CD.ᜎ(this.ᜀ, spr_u17CD);
							num = 3;
							continue;
						}
						case 6:
							goto IL_BD;
						case 7:
							goto IL_103;
						case 8:
							num = 7;
							continue;
						case 9:
						{
							spr\u17CD spr_u17CD;
							if (spr\u17CD.ᜁ(spr_u17CD, this.ᜀ))
							{
								num = 10;
								continue;
							}
							goto IL_BD;
						}
						case 10:
							if (true)
							{
							}
							num = 2;
							continue;
						case 11:
							goto IL_BD;
						case 12:
							switch (sign2)
							{
							case spr\u17CD.Sign.Negative:
							{
								spr\u17CD spr_u17CD = spr\u17CD.ᜎ(A_1, A_0);
								num = 4;
								continue;
							}
							case spr\u17CD.Sign.Zero:
								goto IL_B6;
							case spr\u17CD.Sign.Positive:
							{
								spr\u17CD spr_u17CD = spr\u17CD.ᜎ(A_0, A_1);
								num = 13;
								continue;
							}
							default:
								num = 8;
								continue;
							}
							break;
						case 13:
							goto IL_8D;
						}
						goto IL_40;
						IL_8D:
						num = 9;
						continue;
						IL_BD:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4A;
						default:
							if (false)
							{
							}
							num = 0;
							break;
						}
					}
				}
			}
			IL_B6:
			return spr\u17CD.ᜀ(0);
			IL_103:
			throw new InvalidOperationException();
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x00361550 File Offset: 0x00360550
		internal spr\u17CD ᜂ(spr\u17CD A_0, spr\u17CD A_1)
		{
			while ((this.ᜀ.ᜃ[0] & 1U) == 1U)
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
					return this.ᜀ(A_0, A_1);
				}
			}
			return this.ᜄ(A_0, A_1);
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x003615B0 File Offset: 0x003605B0
		internal spr\u17CD ᜄ(spr\u17CD A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
			{
				spr\u17CD spr_u17CD;
				for (;;)
				{
					spr_u17CD = new spr\u17CD(spr\u17CD.ᜀ(1), this.ᜀ.ᜂ << 1);
					spr\u17CD spr_u17CD2 = new spr\u17CD(spr\u17CD.\u170D(A_0, this.ᜀ), this.ᜀ.ᜂ << 1);
					uint num = (uint)A_1.ᜆ();
					uint[] array = new uint[this.ᜀ.ᜂ << 1];
					uint num2 = 0U;
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return spr_u17CD;
						case 1:
							return spr_u17CD;
						case 2:
							goto IL_133;
						case 3:
							goto IL_133;
						case 4:
							if (!A_1.ᜂ(num2))
							{
								goto IL_B6;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return spr_u17CD;
							default:
								if (false)
								{
								}
								num3 = 7;
								continue;
							}
							break;
						case 5:
							if (num2 >= num)
							{
								num3 = 0;
								continue;
							}
							num3 = 4;
							continue;
						case 6:
							if (spr\u17CD.ᜁ(spr_u17CD2, 1U))
							{
								num3 = 1;
								continue;
							}
							num2 += 1U;
							num3 = 3;
							continue;
						case 7:
						{
							if (true)
							{
							}
							Array.Clear(array, 0, array.Length);
							spr\u17CD.ᜀ.ᜀ(spr_u17CD.ᜃ, 0U, spr_u17CD.ᜂ, spr_u17CD2.ᜃ, 0U, spr_u17CD2.ᜂ, array, 0U);
							spr_u17CD.ᜂ += spr_u17CD2.ᜂ;
							uint[] ᜃ = array;
							array = spr_u17CD.ᜃ;
							spr_u17CD.ᜃ = ᜃ;
							this.ᜀ(spr_u17CD);
							num3 = 8;
							continue;
						}
						case 8:
							goto IL_B6;
						}
						break;
						IL_B6:
						spr\u17CD.ᜀ.ᜀ(spr_u17CD2, ref array);
						this.ᜀ(spr_u17CD2);
						num3 = 6;
						continue;
						IL_133:
						num3 = 5;
					}
				}
				return spr_u17CD;
			}
			}
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x00361788 File Offset: 0x00360788
		private spr\u17CD ᜀ(spr\u17CD A_0, spr\u17CD A_1)
		{
			spr\u17CD spr_u17CD;
			uint a_;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						spr_u17CD = new spr\u17CD(spr\u17CD.ᜁ.ᜀ(spr\u17CD.ᜀ(1), this.ᜀ), this.ᜀ.ᜂ << 1);
						spr\u17CD spr_u17CD2 = new spr\u17CD(spr\u17CD.ᜁ.ᜀ(A_0, this.ᜀ), this.ᜀ.ᜂ << 1);
						a_ = spr\u17CD.ᜁ.ᜀ(this.ᜀ.ᜃ[0]);
						uint num = (uint)A_1.ᜆ();
						uint[] array = new uint[this.ᜀ.ᜂ << 1];
						uint num2 = 0U;
						int num3 = 2;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_1B2;
							case 1:
								goto IL_B9;
							case 2:
								goto IL_1B2;
							case 3:
								if (num2 >= num)
								{
									num3 = 6;
									continue;
								}
								num3 = 5;
								continue;
							case 4:
							{
								Array.Clear(array, 0, array.Length);
								spr\u17CD.ᜀ.ᜀ(spr_u17CD.ᜃ, 0U, spr_u17CD.ᜂ, spr_u17CD2.ᜃ, 0U, spr_u17CD2.ᜂ, array, 0U);
								spr_u17CD.ᜂ += spr_u17CD2.ᜂ;
								uint[] ᜃ = array;
								array = spr_u17CD.ᜃ;
								spr_u17CD.ᜃ = ᜃ;
								spr\u17CD.ᜁ.ᜀ(spr_u17CD, this.ᜀ, a_);
								num3 = 1;
								continue;
							}
							case 5:
								if (!A_1.ᜂ(num2))
								{
									goto IL_B9;
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
									num3 = 4;
									continue;
								}
								break;
							case 6:
								goto IL_1D2;
							}
							break;
							IL_B9:
							spr\u17CD.ᜀ.ᜀ(spr_u17CD2, ref array);
							spr\u17CD.ᜁ.ᜀ(spr_u17CD2, this.ᜀ, a_);
							num2 += 1U;
							if (true)
							{
							}
							num3 = 0;
							continue;
							IL_1B2:
							num3 = 3;
						}
					}
					break;
				}
			}
			IL_1D2:
			spr\u17CD.ᜁ.ᜀ(spr_u17CD, this.ᜀ, a_);
			return spr_u17CD;
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x00361978 File Offset: 0x00360978
		internal spr\u17CD ᜂ(uint A_0, spr\u17CD A_1)
		{
			while ((this.ᜀ.ᜃ[0] & 1U) == 1U)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				return this.ᜁ(A_0, A_1);
			}
			return this.ᜀ(A_0, A_1);
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x003619D8 File Offset: 0x003609D8
		private spr\u17CD ᜁ(uint A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
			{
				spr\u17CD spr_u17CD;
				uint a_;
				for (;;)
				{
					A_1.ᜀ();
					uint[] array = new uint[this.ᜀ.ᜂ << 2];
					spr_u17CD = spr\u17CD.ᜁ.ᜀ(spr\u17CD.ᜀ(A_0), this.ᜀ);
					spr_u17CD = new spr\u17CD(spr_u17CD, this.ᜀ.ᜂ << 2);
					a_ = spr\u17CD.ᜁ.ᜀ(this.ᜀ.ᜃ[0]);
					uint a_2 = (uint)(A_1.ᜆ() - 2);
					int num = 3;
					for (;;)
					{
						ulong num2;
						uint num5;
						uint[] ᜃ;
						uint num6;
						uint num7;
						uint num8;
						uint[] ᜃ2;
						uint num9;
						switch (num)
						{
						case 0:
							goto IL_239;
						case 1:
							goto IL_275;
						case 2:
							if (!spr\u17CD.ᜁ(spr_u17CD, this.ᜀ))
							{
								num = 9;
								continue;
							}
							spr\u17CD.ᜀ.ᜅ(spr_u17CD, this.ᜀ);
							num = 45;
							continue;
						case 3:
							goto IL_2FB;
						case 4:
							if (num2 != 0UL)
							{
								num = 12;
								continue;
							}
							goto IL_65A;
						case 5:
						{
							uint num3;
							uint num4;
							num3 -= num4;
							num = 39;
							continue;
						}
						case 6:
							if (!spr\u17CD.ᜁ(spr_u17CD, this.ᜀ))
							{
								num = 25;
								continue;
							}
							spr\u17CD.ᜀ.ᜅ(spr_u17CD, this.ᜀ);
							num = 29;
							continue;
						case 7:
							if ((num5 += 1U) >= spr_u17CD.ᜂ)
							{
								num = 18;
								continue;
							}
							goto IL_49B;
						case 8:
							ᜃ = spr_u17CD.ᜃ;
							num5 = 0U;
							num2 = 0UL;
							num = 34;
							continue;
						case 9:
							num = 19;
							continue;
						case 10:
							goto IL_3D9;
						case 11:
							goto IL_44E;
						case 12:
						{
							uint num3 = (uint)num2;
							num = 35;
							continue;
						}
						case 13:
						{
							uint num4 = 1U;
							num = 10;
							continue;
						}
						case 14:
						{
							uint num4;
							if ((num6 += num4) < num4 | (ᜃ[(int)((UIntPtr)num7)] -= num6) > ~num6)
							{
								num = 13;
								continue;
							}
							num4 = 0U;
							num = 36;
							continue;
						}
						case 15:
							if (num7 >= spr_u17CD.ᜂ)
							{
								num = 5;
								continue;
							}
							goto IL_5D5;
						case 16:
						{
							uint num3;
							if (num3 != 0U)
							{
								num = 26;
								continue;
							}
							goto IL_3AB;
						}
						case 17:
							goto IL_44E;
						case 18:
							num = 40;
							continue;
						case 19:
							goto IL_275;
						case 20:
							if (num5 >= spr_u17CD.ᜂ)
							{
								num = 37;
								continue;
							}
							goto IL_540;
						case 21:
							if (num2 != 0UL)
							{
								num = 44;
								continue;
							}
							goto IL_275;
						case 22:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								uint num3;
								num8 = (uint)(((ulong)num3 << 32 | (ulong)ᜃ[(int)((UIntPtr)(num5 - 1U))]) / (ulong)(this.ᜀ.ᜃ[(int)((UIntPtr)(this.ᜀ.ᜂ - 1U))] + 1U));
								num = 17;
								continue;
							}
							}
							break;
						case 23:
							goto IL_540;
						case 24:
							goto IL_5D5;
						case 25:
							num = 1;
							continue;
						case 26:
						{
							uint num4 = 0U;
							num7 = 0U;
							ᜃ2 = this.ᜀ.ᜃ;
							num = 24;
							continue;
						}
						case 27:
							goto IL_65A;
						case 28:
							goto IL_153;
						case 29:
							goto IL_3AB;
						case 30:
							if (ᜃ[(int)((UIntPtr)num5)] > num9)
							{
								num = 41;
								continue;
							}
							goto IL_153;
						case 31:
							if (a_2-- <= 0U)
							{
								num = 43;
								continue;
							}
							goto IL_2FB;
						case 32:
							num = 21;
							continue;
						case 33:
							if (A_1.ᜂ(a_2))
							{
								num = 8;
								continue;
							}
							goto IL_275;
						case 34:
							goto IL_49B;
						case 35:
						{
							if (this.ᜀ.ᜃ[(int)((UIntPtr)(this.ᜀ.ᜂ - 1U))] < 4294967295U)
							{
								num = 22;
								continue;
							}
							if (true)
							{
							}
							uint num3;
							num8 = (uint)(((ulong)num3 << 32 | (ulong)ᜃ[(int)((UIntPtr)(num5 - 1U))]) / (ulong)this.ᜀ.ᜃ[(int)((UIntPtr)(this.ᜀ.ᜂ - 1U))]);
							num = 11;
							continue;
						}
						case 36:
							goto IL_3D9;
						case 37:
						{
							uint num3;
							num3 -= (uint)num2;
							num = 16;
							continue;
						}
						case 38:
							if (!spr\u17CD.ᜁ(spr_u17CD, this.ᜀ))
							{
								num = 42;
								continue;
							}
							spr\u17CD.ᜀ.ᜅ(spr_u17CD, this.ᜀ);
							num = 27;
							continue;
						case 39:
							goto IL_3AB;
						case 40:
							if (spr_u17CD.ᜂ < this.ᜀ.ᜂ)
							{
								num = 32;
								continue;
							}
							num = 4;
							continue;
						case 41:
							num2 += 1UL;
							num = 28;
							continue;
						case 42:
							goto IL_275;
						case 43:
							goto IL_298;
						case 44:
							ᜃ[(int)((UIntPtr)num5)] = (uint)num2;
							spr_u17CD.ᜂ += 1U;
							num = 0;
							continue;
						case 45:
							goto IL_239;
						}
						break;
						IL_153:
						num5 += 1U;
						num = 20;
						continue;
						IL_239:
						num = 2;
						continue;
						IL_275:
						num = 31;
						continue;
						IL_2FB:
						spr\u17CD.ᜀ.ᜀ(spr_u17CD, ref array);
						spr_u17CD = spr\u17CD.ᜁ.ᜀ(spr_u17CD, this.ᜀ, a_);
						num = 33;
						continue;
						IL_3AB:
						num = 6;
						continue;
						IL_3D9:
						num7 += 1U;
						num = 15;
						continue;
						IL_44E:
						num5 = 0U;
						num2 = 0UL;
						num = 23;
						continue;
						IL_49B:
						num2 += (ulong)ᜃ[(int)((UIntPtr)num5)] * (ulong)A_0;
						ᜃ[(int)((UIntPtr)num5)] = (uint)num2;
						num2 >>= 32;
						num = 7;
						continue;
						IL_540:
						num2 += (ulong)this.ᜀ.ᜃ[(int)((UIntPtr)num5)] * (ulong)num8;
						num9 = ᜃ[(int)((UIntPtr)num5)];
						ᜃ[(int)((UIntPtr)num5)] -= (uint)num2;
						num2 >>= 32;
						num = 30;
						continue;
						IL_5D5:
						num6 = ᜃ2[(int)((UIntPtr)num7)];
						num = 14;
						continue;
						IL_65A:
						num = 38;
					}
				}
				IL_298:
				return spr\u17CD.ᜁ.ᜀ(spr_u17CD, this.ᜀ, a_);
			}
			}
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x0036207C File Offset: 0x0036107C
		private spr\u17CD ᜀ(uint A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
			{
				spr\u17CD spr_u17CD;
				for (;;)
				{
					A_1.ᜀ();
					uint[] array = new uint[this.ᜀ.ᜂ << 2];
					spr_u17CD = new spr\u17CD(spr\u17CD.ᜀ(A_0), this.ᜀ.ᜂ << 2);
					uint a_ = (uint)(A_1.ᜆ() - 2);
					int num = 17;
					for (;;)
					{
						uint num2;
						ulong num4;
						uint[] ᜃ;
						uint num6;
						uint num7;
						uint[] ᜃ2;
						uint num8;
						uint num9;
						switch (num)
						{
						case 0:
							goto IL_4AE;
						case 1:
							num = 5;
							continue;
						case 2:
							if ((num2 += 1U) >= spr_u17CD.ᜂ)
							{
								num = 20;
								continue;
							}
							goto IL_34B;
						case 3:
							goto IL_3DF;
						case 4:
						{
							uint num3 = 1U;
							num = 3;
							continue;
						}
						case 5:
							goto IL_29C;
						case 6:
							goto IL_18B;
						case 7:
							if (num4 != 0UL)
							{
								num = 36;
								continue;
							}
							goto IL_29C;
						case 8:
						{
							uint num5 = (uint)num4;
							num6 = (uint)(((ulong)num5 << 32 | (ulong)ᜃ[(int)((UIntPtr)(num2 - 1U))]) / (ulong)(this.ᜀ.ᜃ[(int)((UIntPtr)(this.ᜀ.ᜂ - 1U))] + 1U));
							num2 = 0U;
							num4 = 0UL;
							num = 16;
							continue;
						}
						case 9:
							goto IL_29C;
						case 10:
							this.ᜀ(spr_u17CD);
							num = 0;
							continue;
						case 11:
						{
							uint num3 = 0U;
							num7 = 0U;
							ᜃ2 = this.ᜀ.ᜃ;
							num = 12;
							continue;
						}
						case 12:
							goto IL_5B2;
						case 13:
						{
							uint num5;
							num5 -= (uint)num4;
							num = 35;
							continue;
						}
						case 14:
							goto IL_2D0;
						case 15:
							if (!spr\u17CD.ᜁ(spr_u17CD, this.ᜀ))
							{
								num = 43;
								continue;
							}
							spr\u17CD.ᜀ.ᜅ(spr_u17CD, this.ᜀ);
							num = 22;
							continue;
						case 16:
							goto IL_4EF;
						case 17:
							goto IL_2EA;
						case 18:
							if (spr_u17CD.ᜂ >= this.ᜀ.ᜂ)
							{
								num = 10;
								continue;
							}
							goto IL_4AE;
						case 19:
							if (spr_u17CD.ᜂ < this.ᜀ.ᜂ)
							{
								num = 38;
								continue;
							}
							num = 14;
							continue;
						case 20:
							num = 19;
							continue;
						case 21:
							goto IL_34B;
						case 22:
							if (true)
							{
							}
							goto IL_584;
						case 23:
							if (!spr\u17CD.ᜁ(spr_u17CD, this.ᜀ))
							{
								num = 40;
								continue;
							}
							spr\u17CD.ᜀ.ᜅ(spr_u17CD, this.ᜀ);
							num = 6;
							continue;
						case 24:
							return spr_u17CD;
						case 25:
							if (a_-- <= 0U)
							{
								num = 24;
								continue;
							}
							goto IL_2EA;
						case 26:
							if (num7 >= spr_u17CD.ᜂ)
							{
								num = 39;
								continue;
							}
							goto IL_5B2;
						case 27:
							if (!spr\u17CD.ᜁ(spr_u17CD, this.ᜀ))
							{
								num = 1;
								continue;
							}
							spr\u17CD.ᜀ.ᜅ(spr_u17CD, this.ᜀ);
							num = 41;
							continue;
						case 28:
							ᜃ = spr_u17CD.ᜃ;
							num2 = 0U;
							num4 = 0UL;
							num = 21;
							continue;
						case 29:
							goto IL_18B;
						case 30:
							goto IL_3DF;
						case 31:
							if (ᜃ[(int)((UIntPtr)num2)] > num8)
							{
								num = 42;
								continue;
							}
							goto IL_211;
						case 32:
							goto IL_211;
						case 33:
							goto IL_394;
						case 34:
							if (A_1.ᜂ(a_))
							{
								num = 28;
								continue;
							}
							goto IL_29C;
						case 35:
						{
							uint num5;
							if (num5 != 0U)
							{
								num = 11;
								continue;
							}
							goto IL_18B;
						}
						case 36:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D0;
							default:
								if (false)
								{
								}
								ᜃ[(int)((UIntPtr)num2)] = (uint)num4;
								spr_u17CD.ᜂ += 1U;
								num = 33;
								continue;
							}
							break;
						case 37:
						{
							uint num3;
							if ((num9 += num3) < num3 | (ᜃ[(int)((UIntPtr)num7)] -= num9) > ~num9)
							{
								num = 4;
								continue;
							}
							num3 = 0U;
							num = 30;
							continue;
						}
						case 38:
							num = 7;
							continue;
						case 39:
						{
							uint num3;
							uint num5;
							num5 -= num3;
							num = 29;
							continue;
						}
						case 40:
							num = 9;
							continue;
						case 41:
							goto IL_394;
						case 42:
							num4 += 1UL;
							num = 32;
							continue;
						case 43:
							goto IL_29C;
						case 44:
							if (num2 >= spr_u17CD.ᜂ)
							{
								num = 13;
								continue;
							}
							goto IL_4EF;
						}
						break;
						IL_18B:
						num = 23;
						continue;
						IL_211:
						num2 += 1U;
						num = 44;
						continue;
						IL_29C:
						num = 25;
						continue;
						IL_2D0:
						if (num4 != 0UL)
						{
							num = 8;
							continue;
						}
						goto IL_584;
						IL_2EA:
						spr\u17CD.ᜀ.ᜀ(spr_u17CD, ref array);
						num = 18;
						continue;
						IL_34B:
						num4 += (ulong)ᜃ[(int)((UIntPtr)num2)] * (ulong)A_0;
						ᜃ[(int)((UIntPtr)num2)] = (uint)num4;
						num4 >>= 32;
						num = 2;
						continue;
						IL_394:
						num = 27;
						continue;
						IL_3DF:
						num7 += 1U;
						num = 26;
						continue;
						IL_4AE:
						num = 34;
						continue;
						IL_4EF:
						num4 += (ulong)this.ᜀ.ᜃ[(int)((UIntPtr)num2)] * (ulong)num6;
						num8 = ᜃ[(int)((UIntPtr)num2)];
						ᜃ[(int)((UIntPtr)num2)] -= (uint)num4;
						num4 >>= 32;
						num = 31;
						continue;
						IL_584:
						num = 15;
						continue;
						IL_5B2:
						num9 = ᜃ2[(int)((UIntPtr)num7)];
						num = 37;
					}
				}
				return spr_u17CD;
			}
			}
		}

		// Token: 0x04002B0F RID: 11023
		private spr\u17CD ᜀ;

		// Token: 0x04002B10 RID: 11024
		private spr\u17CD ᜁ;
	}

	// Token: 0x02000417 RID: 1047
	internal class ᜁ
	{
		// Token: 0x06003A46 RID: 14918 RVA: 0x00362694 File Offset: 0x00361694
		private ᜁ()
		{
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x003626A8 File Offset: 0x003616A8
		internal static uint ᜀ(uint A_0)
		{
			uint num;
			for (;;)
			{
				num = A_0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						uint num3;
						if ((num3 = A_0 * num) == 1U)
						{
							goto IL_62;
						}
						num *= 2U - num3;
						num2 = 1;
						continue;
					}
					case 1:
						goto IL_48;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_62;
						default:
							if (false)
							{
							}
							goto IL_48;
						}
						break;
					case 3:
						goto IL_6A;
					}
					break;
					IL_48:
					num2 = 0;
					continue;
					IL_62:
					num2 = 3;
				}
			}
			IL_6A:
			return (uint)(-(uint)((ulong)num));
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x00362738 File Offset: 0x00361738
		internal static spr\u17CD ᜀ(spr\u17CD A_0, spr\u17CD A_1)
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
			A_0.ᜀ();
			A_1.ᜀ();
			A_0 = spr\u17CD.ᜄ(A_0, (int)(A_1.ᜂ * 32U));
			A_0 = spr\u17CD.\u170D(A_0, A_1);
			return A_0;
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x0036279C File Offset: 0x0036179C
		internal static spr\u17CD ᜀ(spr\u17CD A_0, spr\u17CD A_1, uint A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					uint[] ᜃ = A_0.ᜃ;
					uint[] ᜃ2 = A_1.ᜃ;
					uint num = 0U;
					int num2 = 13;
					for (;;)
					{
						uint num3;
						uint num5;
						uint num6;
						switch (num2)
						{
						case 0:
							num2 = 5;
							continue;
						case 1:
						{
							if (num3 >= A_0.ᜂ)
							{
								num2 = 10;
								continue;
							}
							ulong num4;
							num4 += (ulong)ᜃ[(int)((UIntPtr)(num5++))];
							ᜃ[(int)((UIntPtr)(num6++))] = (uint)num4;
							num4 >>= 32;
							num2 = 22;
							continue;
						}
						case 2:
							if (num3 >= A_0.ᜂ)
							{
								num2 = 19;
								continue;
							}
							goto IL_19C;
						case 3:
							goto IL_F3;
						case 4:
							goto IL_B8;
						case 5:
							goto IL_338;
						case 6:
						{
							if (num >= A_1.ᜂ)
							{
								num2 = 15;
								continue;
							}
							uint num7 = ᜃ[0] * A_2;
							uint num8 = 0U;
							num5 = 0U;
							num6 = 0U;
							ulong num4 = (ulong)num7 * (ulong)ᜃ2[(int)((UIntPtr)(num8++))] + (ulong)ᜃ[(int)((UIntPtr)(num5++))];
							num4 >>= 32;
							num3 = 1U;
							num2 = 11;
							continue;
						}
						case 7:
							num3 += 1U;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_19C;
							default:
								if (false)
								{
								}
								num2 = 4;
								continue;
							}
							break;
						case 8:
							goto IL_F3;
						case 9:
							if (A_0.ᜂ > 1U)
							{
								num2 = 26;
								continue;
							}
							goto IL_169;
						case 10:
							num2 = 27;
							continue;
						case 11:
							goto IL_119;
						case 12:
							goto IL_140;
						case 13:
							goto IL_140;
						case 14:
							goto IL_169;
						case 15:
							num2 = 8;
							continue;
						case 16:
							goto IL_B8;
						case 17:
							return A_0;
						case 18:
						{
							if (num3 >= A_1.ᜂ)
							{
								num2 = 0;
								continue;
							}
							ulong num4;
							uint num7;
							uint num8;
							num4 += (ulong)num7 * (ulong)ᜃ2[(int)((UIntPtr)(num8++))] + (ulong)ᜃ[(int)((UIntPtr)(num5++))];
							ᜃ[(int)((UIntPtr)(num6++))] = (uint)num4;
							num4 >>= 32;
							num3 += 1U;
							num2 = 24;
							continue;
						}
						case 19:
						{
							ulong num4;
							ᜃ[(int)((UIntPtr)(num6++))] = (uint)num4;
							num += 1U;
							num2 = 12;
							continue;
						}
						case 20:
							goto IL_338;
						case 21:
							spr\u17CD.ᜀ.ᜅ(A_0, A_1);
							num2 = 17;
							continue;
						case 22:
						{
							ulong num4;
							if (num4 == 0UL)
							{
								num2 = 7;
								continue;
							}
							num3 += 1U;
							num2 = 20;
							continue;
						}
						case 23:
							if (spr\u17CD.ᜁ(A_0, A_1))
							{
								num2 = 21;
								continue;
							}
							return A_0;
						case 24:
							if (true)
							{
							}
							goto IL_119;
						case 25:
							if (ᜃ[(int)((UIntPtr)(A_0.ᜂ - 1U))] != 0U)
							{
								num2 = 14;
								continue;
							}
							A_0.ᜂ -= 1U;
							num2 = 3;
							continue;
						case 26:
							num2 = 25;
							continue;
						case 27:
							goto IL_B8;
						}
						break;
						IL_B8:
						num2 = 2;
						continue;
						IL_F3:
						num2 = 9;
						continue;
						IL_119:
						num2 = 18;
						continue;
						IL_140:
						num2 = 6;
						continue;
						IL_169:
						num2 = 23;
						continue;
						IL_19C:
						ᜃ[(int)((UIntPtr)(num6++))] = ᜃ[(int)((UIntPtr)(num5++))];
						num3 += 1U;
						num2 = 16;
						continue;
						IL_338:
						num2 = 1;
					}
				}
				return A_0;
			}
		}
	}

	// Token: 0x02000418 RID: 1048
	private class ᜀ
	{
		// Token: 0x06003A4A RID: 14922 RVA: 0x00362B5C File Offset: 0x00361B5C
		internal static spr\u17CD ᜇ(spr\u17CD A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
			{
				uint num;
				uint[] ᜃ3;
				spr\u17CD spr_u17CD;
				for (;;)
				{
					num = 0U;
					int num2 = 12;
					for (;;)
					{
						uint ᜂ;
						bool flag;
						uint[] ᜃ;
						uint[] ᜃ2;
						ulong num3;
						switch (num2)
						{
						case 0:
							if (num < ᜂ)
							{
								num2 = 3;
								continue;
							}
							goto IL_99;
						case 1:
							goto IL_283;
						case 2:
							if (!flag)
							{
								num2 = 4;
								continue;
							}
							goto IL_BD;
						case 3:
							goto IL_BD;
						case 4:
							goto IL_99;
						case 5:
							goto IL_1E8;
						case 6:
							if (true)
							{
							}
							num2 = 2;
							continue;
						case 7:
							if ((num += 1U) < ᜂ)
							{
								goto IL_EA;
							}
							goto IL_99;
						case 8:
							goto IL_103;
						case 9:
							num2 = 0;
							continue;
						case 10:
						{
							uint ᜂ2;
							if ((num += 1U) >= ᜂ2)
							{
								num2 = 19;
								continue;
							}
							goto IL_283;
						}
						case 11:
							if (flag)
							{
								num2 = 20;
								continue;
							}
							goto IL_12C;
						case 12:
							if (A_0.ᜂ < A_1.ᜂ)
							{
								num2 = 14;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_EA;
							default:
							{
								if (false)
								{
								}
								ᜃ = A_0.ᜃ;
								ᜂ = A_0.ᜂ;
								ᜃ2 = A_1.ᜃ;
								uint ᜂ2 = A_1.ᜂ;
								num2 = 15;
								continue;
							}
							}
							break;
						case 13:
							if (flag)
							{
								num2 = 9;
								continue;
							}
							goto IL_12C;
						case 14:
						{
							ᜃ = A_1.ᜃ;
							ᜂ = A_1.ᜂ;
							ᜃ2 = A_0.ᜃ;
							uint ᜂ2 = A_0.ᜂ;
							num2 = 8;
							continue;
						}
						case 15:
							goto IL_103;
						case 16:
							if (num < ᜂ)
							{
								num2 = 18;
								continue;
							}
							goto IL_2D6;
						case 17:
							if ((num += 1U) >= ᜂ)
							{
								num2 = 5;
								continue;
							}
							goto IL_1BB;
						case 18:
							goto IL_1BB;
						case 19:
							flag = (num3 != 0UL);
							num2 = 13;
							continue;
						case 20:
							goto IL_B8;
						}
						break;
						IL_99:
						num2 = 11;
						continue;
						IL_BD:
						flag = ((ᜃ3[(int)((UIntPtr)num)] = ᜃ[(int)((UIntPtr)num)] + 1U) == 0U);
						num2 = 7;
						continue;
						IL_EA:
						num2 = 6;
						continue;
						IL_103:
						spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, ᜂ + 1U);
						ᜃ3 = spr_u17CD.ᜃ;
						num3 = 0UL;
						num2 = 1;
						continue;
						IL_12C:
						num2 = 16;
						continue;
						IL_1BB:
						ᜃ3[(int)((UIntPtr)num)] = ᜃ[(int)((UIntPtr)num)];
						num2 = 17;
						continue;
						IL_283:
						num3 = (ulong)ᜃ[(int)((UIntPtr)num)] + (ulong)ᜃ2[(int)((UIntPtr)num)] + num3;
						ᜃ3[(int)((UIntPtr)num)] = (uint)num3;
						num3 >>= 32;
						num2 = 10;
					}
				}
				IL_B8:
				ᜃ3[(int)((UIntPtr)num)] = 1U;
				spr_u17CD.ᜂ = num + 1U;
				return spr_u17CD;
				IL_1E8:
				IL_2D6:
				spr_u17CD.ᜀ();
				return spr_u17CD;
			}
			}
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x00362E48 File Offset: 0x00361E48
		internal static spr\u17CD ᜆ(spr\u17CD A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
			{
				spr\u17CD spr_u17CD;
				for (;;)
				{
					spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ);
					uint[] ᜃ = spr_u17CD.ᜃ;
					uint[] ᜃ2 = A_0.ᜃ;
					uint[] ᜃ3 = A_1.ᜃ;
					uint num = 0U;
					uint num2 = 0U;
					int num3 = 7;
					for (;;)
					{
						uint num4;
						switch (num3)
						{
						case 0:
							num2 = 1U;
							if (true)
							{
							}
							num3 = 3;
							continue;
						case 1:
							if (num != A_0.ᜂ)
							{
								num3 = 17;
								continue;
							}
							goto IL_266;
						case 2:
							if ((num4 += num2) < num2 | (ᜃ[(int)((UIntPtr)num)] = ᜃ2[(int)((UIntPtr)num)] - num4) > ~num4)
							{
								num3 = 0;
								continue;
							}
							num2 = 0U;
							num3 = 8;
							continue;
						case 3:
							goto IL_202;
						case 4:
							if ((num += 1U) >= A_1.ᜂ)
							{
								num3 = 15;
								continue;
							}
							goto IL_14A;
						case 5:
							if (ᜃ2[(int)((UIntPtr)(num++))] == 0U)
							{
								goto IL_1BE;
							}
							goto IL_104;
						case 6:
							if (num != A_0.ᜂ)
							{
								num3 = 10;
								continue;
							}
							goto IL_266;
						case 7:
							goto IL_14A;
						case 8:
							goto IL_202;
						case 9:
							if ((num += 1U) >= A_0.ᜂ)
							{
								num3 = 13;
								continue;
							}
							goto IL_1CF;
						case 10:
							goto IL_1CF;
						case 11:
							goto IL_197;
						case 12:
							num3 = 16;
							continue;
						case 13:
							goto IL_200;
						case 14:
							if (num2 == 1U)
							{
								num3 = 11;
								continue;
							}
							goto IL_1CF;
						case 15:
							num3 = 1;
							continue;
						case 16:
							if (num >= A_0.ᜂ)
							{
								num3 = 18;
								continue;
							}
							goto IL_197;
						case 17:
							num3 = 14;
							continue;
						case 18:
							goto IL_104;
						}
						break;
						IL_104:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_1BE:
							num3 = 12;
							continue;
						default:
							if (false)
							{
							}
							num3 = 6;
							continue;
						}
						IL_14A:
						num4 = ᜃ3[(int)((UIntPtr)num)];
						num3 = 2;
						continue;
						IL_197:
						ᜃ[(int)((UIntPtr)num)] = ᜃ2[(int)((UIntPtr)num)] - 1U;
						num3 = 5;
						continue;
						IL_1CF:
						ᜃ[(int)((UIntPtr)num)] = ᜃ2[(int)((UIntPtr)num)];
						num3 = 9;
						continue;
						IL_202:
						num3 = 4;
					}
				}
				IL_200:
				IL_266:
				spr_u17CD.ᜀ();
				return spr_u17CD;
			}
			}
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x003630CC File Offset: 0x003620CC
		internal static void ᜅ(spr\u17CD A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					uint[] ᜃ = A_0.ᜃ;
					uint[] ᜃ2 = A_1.ᜃ;
					uint num = 0U;
					uint num2 = 0U;
					int num3 = 6;
					for (;;)
					{
						uint num4;
						switch (num3)
						{
						case 0:
							goto IL_186;
						case 1:
							num3 = 19;
							continue;
						case 2:
							if (num != A_0.ᜂ)
							{
								num3 = 11;
								continue;
							}
							goto IL_141;
						case 3:
							if (A_0.ᜂ == 0U)
							{
								num3 = 23;
								continue;
							}
							return;
						case 4:
							return;
						case 5:
							num3 = 9;
							continue;
						case 6:
							goto IL_95;
						case 7:
							num3 = 2;
							continue;
						case 8:
							goto IL_1E9;
						case 9:
							if (num >= A_0.ᜂ)
							{
								num3 = 22;
								continue;
							}
							goto IL_186;
						case 10:
							goto IL_141;
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_95;
							default:
								if (false)
								{
								}
								num3 = 17;
								continue;
							}
							break;
						case 12:
							if ((num4 += num2) < num2 | (ᜃ[(int)((UIntPtr)num)] -= num4) > ~num4)
							{
								num3 = 13;
								continue;
							}
							num2 = 0U;
							num3 = 21;
							continue;
						case 13:
							num2 = 1U;
							num3 = 14;
							continue;
						case 14:
							goto IL_1C2;
						case 15:
							if ((num += 1U) >= A_1.ᜂ)
							{
								num3 = 7;
								continue;
							}
							goto IL_248;
						case 16:
							if (ᜃ[(int)((UIntPtr)(num++))] == 0U)
							{
								num3 = 5;
								continue;
							}
							goto IL_141;
						case 17:
							if (num2 == 1U)
							{
								num3 = 0;
								continue;
							}
							goto IL_141;
						case 18:
							if (A_0.ᜂ > 0U)
							{
								num3 = 1;
								continue;
							}
							goto IL_1E9;
						case 19:
							if (A_0.ᜃ[(int)((UIntPtr)(A_0.ᜂ - 1U))] != 0U)
							{
								num3 = 8;
								continue;
							}
							A_0.ᜂ -= 1U;
							num3 = 20;
							continue;
						case 20:
							goto IL_141;
						case 21:
							goto IL_1C2;
						case 22:
							num3 = 10;
							continue;
						case 23:
							A_0.ᜂ += 1U;
							num3 = 4;
							continue;
						}
						break;
						IL_141:
						num3 = 18;
						continue;
						IL_186:
						ᜃ[(int)((UIntPtr)num)] -= 1U;
						num3 = 16;
						continue;
						IL_1C2:
						num3 = 15;
						continue;
						IL_1E9:
						num3 = 3;
						continue;
						IL_248:
						num4 = ᜃ2[(int)((UIntPtr)num)];
						if (true)
						{
						}
						num3 = 12;
						continue;
						IL_95:
						goto IL_248;
					}
				}
				return;
			}
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x003633BC File Offset: 0x003623BC
		internal static void ᜄ(spr\u17CD A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
			{
				uint num;
				uint ᜂ;
				uint[] ᜃ3;
				for (;;)
				{
					num = 0U;
					bool flag = false;
					int num2 = 12;
					for (;;)
					{
						uint[] ᜃ;
						uint[] ᜃ2;
						bool flag2;
						ulong num3;
						switch (num2)
						{
						case 0:
							goto IL_F7;
						case 1:
							if (true)
							{
							}
							goto IL_162;
						case 2:
							if (num < ᜂ - 1U)
							{
								num2 = 14;
								continue;
							}
							goto IL_2FB;
						case 3:
							if ((num += 1U) < ᜂ)
							{
								num2 = 8;
								continue;
							}
							goto IL_162;
						case 4:
							if (flag)
							{
								num2 = 5;
								continue;
							}
							goto IL_2FB;
						case 5:
							num2 = 2;
							continue;
						case 6:
							goto IL_1F9;
						case 7:
						{
							flag = true;
							ᜃ = A_1.ᜃ;
							ᜂ = A_1.ᜂ;
							ᜃ2 = A_0.ᜃ;
							uint ᜂ2 = A_0.ᜂ;
							num2 = 0;
							continue;
						}
						case 8:
							num2 = 11;
							continue;
						case 9:
						{
							uint ᜂ2;
							if ((num += 1U) >= ᜂ2)
							{
								num2 = 20;
								continue;
							}
							goto IL_1F9;
						}
						case 10:
							if ((num += 1U) >= ᜂ)
							{
								num2 = 21;
								continue;
							}
							goto IL_186;
						case 11:
							if (!flag2)
							{
								num2 = 1;
								continue;
							}
							goto IL_B9;
						case 12:
						{
							if (A_0.ᜂ < A_1.ᜂ)
							{
								num2 = 7;
								continue;
							}
							ᜃ = A_0.ᜃ;
							ᜂ = A_0.ᜂ;
							ᜃ2 = A_1.ᜃ;
							uint ᜂ2 = A_1.ᜂ;
							num2 = 22;
							continue;
						}
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D0;
							default:
								if (false)
								{
								}
								if (num < ᜂ)
								{
									num2 = 15;
									continue;
								}
								goto IL_162;
							}
							break;
						case 14:
							goto IL_186;
						case 15:
							goto IL_B9;
						case 16:
							if (flag2)
							{
								num2 = 18;
								continue;
							}
							goto IL_24C;
						case 17:
							if (flag2)
							{
								num2 = 19;
								continue;
							}
							goto IL_24C;
						case 18:
							num2 = 13;
							continue;
						case 19:
							goto IL_181;
						case 20:
							flag2 = (num3 != 0UL);
							num2 = 16;
							continue;
						case 21:
							goto IL_1B3;
						case 22:
							goto IL_F7;
						}
						break;
						IL_D0:
						num2 = 3;
						continue;
						IL_B9:
						flag2 = ((ᜃ3[(int)((UIntPtr)num)] = ᜃ[(int)((UIntPtr)num)] + 1U) == 0U);
						goto IL_D0;
						IL_F7:
						ᜃ3 = A_0.ᜃ;
						num3 = 0UL;
						num2 = 6;
						continue;
						IL_162:
						num2 = 17;
						continue;
						IL_186:
						ᜃ3[(int)((UIntPtr)num)] = ᜃ[(int)((UIntPtr)num)];
						num2 = 10;
						continue;
						IL_1F9:
						num3 += (ulong)ᜃ[(int)((UIntPtr)num)] + (ulong)ᜃ2[(int)((UIntPtr)num)];
						ᜃ3[(int)((UIntPtr)num)] = (uint)num3;
						num3 >>= 32;
						num2 = 9;
						continue;
						IL_24C:
						num2 = 4;
					}
				}
				IL_181:
				ᜃ3[(int)((UIntPtr)num)] = 1U;
				A_0.ᜂ = num + 1U;
				return;
				IL_1B3:
				IL_2FB:
				A_0.ᜂ = ᜂ + 1U;
				A_0.ᜀ();
				return;
			}
			}
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x003636D4 File Offset: 0x003626D4
		internal static spr\u17CD.Sign ᜃ(spr\u17CD A_0, spr\u17CD A_1)
		{
			for (;;)
			{
				uint num = A_0.ᜂ;
				uint num2 = A_1.ᜂ;
				int num3 = 1;
				for (;;)
				{
					uint num4;
					switch (num3)
					{
					case 0:
						num3 = 29;
						continue;
					case 1:
						goto IL_2CF;
					case 2:
						goto IL_2CF;
					case 3:
						return spr\u17CD.Sign.Negative;
					case 4:
						if (num4 != 0U)
						{
							num3 = 12;
							continue;
						}
						goto IL_107;
					case 5:
						num3 = 22;
						continue;
					case 6:
						if (A_0.ᜃ[(int)((UIntPtr)num4)] < A_1.ᜃ[(int)((UIntPtr)num4)])
						{
							num3 = 11;
							continue;
						}
						num3 = 17;
						continue;
					case 7:
						return spr\u17CD.Sign.Positive;
					case 8:
						if (num == 0U)
						{
							num3 = 24;
							continue;
						}
						goto IL_1A4;
					case 9:
						if (A_1.ᜃ[(int)((UIntPtr)(num2 - 1U))] != 0U)
						{
							num3 = 13;
							continue;
						}
						num2 -= 1U;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_284;
						default:
							if (false)
							{
							}
							num3 = 18;
							continue;
						}
						break;
					case 10:
						if (num2 > 0U)
						{
							num3 = 15;
							continue;
						}
						goto IL_258;
					case 11:
						return spr\u17CD.Sign.Negative;
					case 12:
						num3 = 14;
						continue;
					case 13:
						goto IL_258;
					case 14:
						goto IL_284;
					case 15:
						if (true)
						{
						}
						num3 = 9;
						continue;
					case 16:
						goto IL_107;
					case 17:
						if (A_0.ᜃ[(int)((UIntPtr)num4)] > A_1.ᜃ[(int)((UIntPtr)num4)])
						{
							num3 = 7;
							continue;
						}
						return spr\u17CD.Sign.Zero;
					case 18:
						goto IL_1C6;
					case 19:
						goto IL_2AB;
					case 20:
						if (num2 == 0U)
						{
							num3 = 21;
							continue;
						}
						goto IL_1A4;
					case 21:
						return spr\u17CD.Sign.Zero;
					case 22:
						if (A_0.ᜃ[(int)((UIntPtr)(num - 1U))] != 0U)
						{
							num3 = 0;
							continue;
						}
						num -= 1U;
						num3 = 2;
						continue;
					case 23:
						if (num > num2)
						{
							num3 = 26;
							continue;
						}
						num4 = num - 1U;
						num3 = 19;
						continue;
					case 24:
						num3 = 20;
						continue;
					case 25:
						goto IL_2AB;
					case 26:
						return spr\u17CD.Sign.Positive;
					case 27:
						if (num > 0U)
						{
							num3 = 5;
							continue;
						}
						goto IL_1C6;
					case 28:
						if (num < num2)
						{
							num3 = 3;
							continue;
						}
						num3 = 23;
						continue;
					case 29:
						goto IL_1C6;
					}
					break;
					IL_284:
					if (A_0.ᜃ[(int)((UIntPtr)num4)] != A_1.ᜃ[(int)((UIntPtr)num4)])
					{
						num3 = 16;
						continue;
					}
					num4 -= 1U;
					num3 = 25;
					continue;
					IL_107:
					num3 = 6;
					continue;
					IL_1A4:
					num3 = 28;
					continue;
					IL_1C6:
					num3 = 10;
					continue;
					IL_258:
					num3 = 8;
					continue;
					IL_2AB:
					num3 = 4;
					continue;
					IL_2CF:
					num3 = 27;
				}
			}
			return spr\u17CD.Sign.Positive;
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x003639E0 File Offset: 0x003629E0
		internal static uint ᜅ(spr\u17CD A_0, uint A_1)
		{
			ulong num;
			for (;;)
			{
				num = 0UL;
				uint ᜂ = A_0.ᜂ;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_58:
					if (ᜂ-- <= 0U)
					{
						num2 = 3;
					}
					else
					{
						num <<= 32;
						num |= (ulong)A_0.ᜃ[(int)((UIntPtr)ᜂ)];
						A_0.ᜃ[(int)((UIntPtr)ᜂ)] = (uint)(num / (ulong)A_1);
						num %= (ulong)A_1;
						num2 = 2;
					}
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num2 = 1;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_58;
					case 1:
						goto IL_50;
					case 2:
						goto IL_50;
					case 3:
						goto IL_68;
					}
					break;
					IL_50:
					num2 = 0;
				}
			}
			IL_68:
			A_0.ᜀ();
			return (uint)num;
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x00363A9C File Offset: 0x00362A9C
		internal static uint ᜄ(spr\u17CD A_0, uint A_1)
		{
			ulong num;
			for (;;)
			{
				num = 0UL;
				uint ᜂ = A_0.ᜂ;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_58:
					if (ᜂ-- <= 0U)
					{
						num2 = 3;
					}
					else
					{
						num <<= 32;
						num |= (ulong)A_0.ᜃ[(int)((UIntPtr)ᜂ)];
						num %= (ulong)A_1;
						num2 = 1;
					}
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num2 = 0;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_50;
					case 1:
						goto IL_50;
					case 2:
						goto IL_58;
					case 3:
						goto IL_68;
					}
					break;
					IL_50:
					num2 = 2;
				}
			}
			IL_68:
			return (uint)num;
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x00363B44 File Offset: 0x00362B44
		internal static spr\u17CD ᜃ(spr\u17CD A_0, uint A_1)
		{
			spr\u17CD spr_u17CD;
			for (;;)
			{
				spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ);
				ulong num = 0UL;
				uint ᜂ = A_0.ᜂ;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5D:
					if (true)
					{
					}
					if (ᜂ-- <= 0U)
					{
						num2 = 1;
					}
					else
					{
						num <<= 32;
						num |= (ulong)A_0.ᜃ[(int)((UIntPtr)ᜂ)];
						spr_u17CD.ᜃ[(int)((UIntPtr)ᜂ)] = (uint)(num / (ulong)A_1);
						num %= (ulong)A_1;
						num2 = 0;
					}
					break;
				default:
					if (false)
					{
					}
					num2 = 2;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_55;
					case 1:
						goto IL_75;
					case 2:
						goto IL_55;
					case 3:
						goto IL_5D;
					}
					break;
					IL_55:
					num2 = 3;
				}
			}
			IL_75:
			spr_u17CD.ᜀ();
			return spr_u17CD;
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x00363C0C File Offset: 0x00362C0C
		internal static spr\u17CD[] ᜂ(spr\u17CD A_0, uint A_1)
		{
			switch (0)
			{
			default:
			{
				spr\u17CD spr_u17CD;
				ulong num;
				for (;;)
				{
					if (true)
					{
					}
					spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ);
					num = 0UL;
					uint ᜂ = A_0.ᜂ;
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
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_94;
							case 1:
								goto IL_6D;
							case 2:
								goto IL_6D;
							case 3:
								if (ᜂ-- <= 0U)
								{
									num2 = 0;
									continue;
								}
								num <<= 32;
								num |= (ulong)A_0.ᜃ[(int)((UIntPtr)ᜂ)];
								spr_u17CD.ᜃ[(int)((UIntPtr)ᜂ)] = (uint)(num / (ulong)A_1);
								num %= (ulong)A_1;
								num2 = 2;
								continue;
							}
							break;
							IL_6D:
							num2 = 3;
						}
						break;
					}
					}
				}
				IL_94:
				spr_u17CD.ᜀ();
				spr\u17CD spr_u17CD2 = spr\u17CD.ᜀ((uint)num);
				return new spr\u17CD[]
				{
					spr_u17CD,
					spr_u17CD2
				};
			}
			}
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x00363D04 File Offset: 0x00362D04
		internal static spr\u17CD[] ᜂ(spr\u17CD A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 12;
				spr\u17CD[] array;
				for (;;)
				{
					ulong num2;
					ulong num3;
					ulong num4;
					ulong num5;
					uint[] ᜃ;
					int num6;
					uint num7;
					int num8;
					int num9;
					spr\u17CD spr_u17CD;
					spr\u17CD spr_u17CD2;
					int num12;
					uint num13;
					uint num14;
					ulong num15;
					uint num16;
					int num17;
					int num18;
					uint num20;
					switch (num)
					{
					case 0:
						goto IL_FB;
					case 1:
						goto IL_44F;
					case 2:
						num2 += 1UL;
						num = 34;
						continue;
					case 3:
						if (num3 != 4294967296UL)
						{
							num = 10;
							continue;
						}
						goto IL_1F5;
					case 4:
						goto IL_13B;
					case 5:
						goto IL_1F5;
					case 6:
						if (num3 * num4 > (num5 << 32) + (ulong)ᜃ[num6 - 2])
						{
							if (true)
							{
							}
							num = 5;
							continue;
						}
						goto IL_FB;
					case 7:
						if ((ulong)num7 >= (ulong)((long)num8))
						{
							num = 4;
							continue;
						}
						goto IL_3D7;
					case 8:
						goto IL_4B6;
					case 9:
						num9 = num6 - num8 + 1;
						num7 = 0U;
						num = 19;
						continue;
					case 10:
						num = 6;
						continue;
					case 11:
						spr_u17CD.ᜀ();
						spr_u17CD2.ᜀ();
						array = new spr\u17CD[]
						{
							spr_u17CD,
							spr_u17CD2
						};
						num = 16;
						continue;
					case 13:
						goto IL_231;
					case 14:
					{
						uint num10;
						uint num11;
						if ((num10 & num11) != 0U)
						{
							num = 32;
							continue;
						}
						num12++;
						num11 >>= 1;
						num = 13;
						continue;
					}
					case 15:
					{
						spr\u17CD[] array2;
						(array2 = array)[1] = spr\u17CD.ᜃ(array2[1], num12);
						num = 18;
						continue;
					}
					case 16:
						if (num12 != 0)
						{
							num = 15;
							continue;
						}
						return array;
					case 17:
						goto IL_231;
					case 18:
						goto IL_297;
					case 19:
						if (num2 != 0UL)
						{
							num = 24;
							continue;
						}
						goto IL_13B;
					case 20:
						goto IL_549;
					case 21:
					{
						uint num11;
						if (num11 != 0U)
						{
							goto IL_243;
						}
						goto IL_35E;
					}
					case 22:
						if (ᜃ[num9] > num13)
						{
							num = 2;
							continue;
						}
						goto IL_178;
					case 23:
						if (num5 >= 4294967296UL)
						{
							num = 0;
							continue;
						}
						goto IL_44F;
					case 24:
						num14 -= 1U;
						num15 = 0UL;
						num = 29;
						continue;
					case 25:
						num = 14;
						continue;
					case 26:
					{
						if (A_1.ᜂ == 1U)
						{
							num = 20;
							continue;
						}
						num16 = A_0.ᜂ + 1U;
						num8 = (int)(A_1.ᜂ + 1U);
						uint num11 = 2147483648U;
						uint num10 = A_1.ᜃ[(int)((UIntPtr)(A_1.ᜂ - 1U))];
						num12 = 0;
						num17 = (int)(A_0.ᜂ - A_1.ᜂ);
						num = 17;
						continue;
					}
					case 27:
						if ((ulong)num7 >= (ulong)((long)num8))
						{
							num = 9;
							continue;
						}
						goto IL_4B6;
					case 28:
						goto IL_C8;
					case 29:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_243;
						default:
							if (false)
							{
							}
							goto IL_3D7;
						}
						break;
					case 30:
					{
						if (num18 <= 0)
						{
							num = 11;
							continue;
						}
						ulong num19 = ((ulong)ᜃ[num6] << 32) + (ulong)ᜃ[num6 - 1];
						num3 = num19 / (ulong)num20;
						num5 = num19 % (ulong)num20;
						num = 1;
						continue;
					}
					case 31:
						goto IL_254;
					case 32:
						goto IL_35E;
					case 33:
						goto IL_254;
					case 34:
						goto IL_178;
					}
					if (spr\u17CD.ᜀ.ᜃ(A_0, A_1) == spr\u17CD.Sign.Negative)
					{
						num = 28;
						continue;
					}
					A_0.ᜀ();
					A_1.ᜀ();
					num = 26;
					continue;
					IL_FB:
					num7 = 0U;
					num9 = num6 - num8 + 1;
					num2 = 0UL;
					num14 = (uint)num3;
					num = 8;
					continue;
					IL_13B:
					spr_u17CD.ᜃ[num17--] = num14;
					num6--;
					num18--;
					num = 31;
					continue;
					IL_178:
					num7 += 1U;
					num9++;
					num = 27;
					continue;
					IL_1F5:
					num3 -= 1UL;
					num5 += (ulong)num20;
					num = 23;
					continue;
					IL_231:
					num = 21;
					continue;
					IL_243:
					num = 25;
					continue;
					IL_254:
					num = 30;
					continue;
					IL_35E:
					spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ - A_1.ᜂ + 1U);
					spr_u17CD2 = spr\u17CD.ᜄ(A_0, num12);
					ᜃ = spr_u17CD2.ᜃ;
					A_1 = spr\u17CD.ᜄ(A_1, num12);
					num18 = (int)(num16 - A_1.ᜂ);
					num6 = (int)(num16 - 1U);
					num20 = A_1.ᜃ[(int)((UIntPtr)(A_1.ᜂ - 1U))];
					num4 = (ulong)A_1.ᜃ[(int)((UIntPtr)(A_1.ᜂ - 2U))];
					num = 33;
					continue;
					IL_3D7:
					num15 = (ulong)ᜃ[num9] + (ulong)A_1.ᜃ[(int)((UIntPtr)num7)] + num15;
					ᜃ[num9] = (uint)num15;
					num15 >>= 32;
					num7 += 1U;
					num9++;
					num = 7;
					continue;
					IL_44F:
					num = 3;
					continue;
					IL_4B6:
					num2 += (ulong)A_1.ᜃ[(int)((UIntPtr)num7)] * (ulong)num14;
					num13 = ᜃ[num9];
					ᜃ[num9] -= (uint)num2;
					num2 >>= 32;
					num = 22;
				}
				IL_C8:
				return new spr\u17CD[]
				{
					spr\u17CD.ᜀ(0),
					new spr\u17CD(A_0)
				};
				IL_297:
				return array;
				IL_549:
				return spr\u17CD.ᜀ.ᜂ(A_0, A_1.ᜃ[0]);
			}
			}
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x00364298 File Offset: 0x00363298
		internal static spr\u17CD ᜁ(spr\u17CD A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				spr\u17CD spr_u17CD;
				for (;;)
				{
					uint num2;
					uint ᜂ;
					int num3;
					uint num5;
					switch (num)
					{
					case 0:
						if (num2 >= ᜂ)
						{
							num = 8;
							continue;
						}
						spr_u17CD.ᜃ[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num3)))))] = A_0.ᜃ[(int)((UIntPtr)num2)];
						num2 += 1U;
						num = 10;
						continue;
					case 1:
						goto IL_1A6;
					case 2:
					{
						if (num2 >= ᜂ)
						{
							num = 3;
							continue;
						}
						uint num4 = A_0.ᜃ[(int)((UIntPtr)num2)];
						spr_u17CD.ᜃ[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num3)))))] = (num4 << A_1 | num5);
						num5 = num4 >> 32 - A_1;
						num2 += 1U;
						num = 11;
						continue;
					}
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A6;
						default:
							if (false)
							{
							}
							spr_u17CD.ᜃ[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num3)))))] = num5;
							num = 5;
							continue;
						}
						break;
					case 4:
						goto IL_86;
					case 5:
						goto IL_81;
					case 6:
						if (A_1 != 0)
						{
							num = 1;
							continue;
						}
						goto IL_101;
					case 7:
						if (true)
						{
						}
						break;
					case 8:
						goto IL_127;
					case 9:
						goto IL_64;
					case 10:
						goto IL_101;
					case 11:
						goto IL_86;
					}
					if (A_1 == 0)
					{
						num = 9;
						continue;
					}
					num3 = A_1 >> 5;
					A_1 &= 31;
					spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ + 1U + (uint)num3);
					num2 = 0U;
					ᜂ = A_0.ᜂ;
					num = 6;
					continue;
					IL_86:
					num = 2;
					continue;
					IL_101:
					num = 0;
					continue;
					IL_1A6:
					num5 = 0U;
					num = 4;
				}
				IL_64:
				return new spr\u17CD(A_0, A_0.ᜂ + 1U);
				IL_81:
				IL_127:
				spr_u17CD.ᜀ();
				return spr_u17CD;
			}
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x00364468 File Offset: 0x00363468
		internal static spr\u17CD ᜀ(spr\u17CD A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 9;
				spr\u17CD spr_u17CD;
				for (;;)
				{
					uint num2;
					int num4;
					switch (num)
					{
					case 0:
						num = 7;
						continue;
					case 1:
					{
						if (num2-- <= 0U)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						uint num3 = A_0.ᜃ[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num4)))))];
						uint num5;
						spr_u17CD.ᜃ[(int)((UIntPtr)num2)] = (num3 >> A_1 | num5);
						num5 = num3 << 32 - A_1;
						num = 3;
						continue;
					}
					case 2:
						goto IL_C7;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C7;
						default:
							if (false)
							{
							}
							goto IL_6F;
						}
						break;
					case 4:
						goto IL_DB;
					case 5:
						goto IL_5C;
					case 6:
					{
						uint num5 = 0U;
						num = 11;
						continue;
					}
					case 7:
						goto IL_6A;
					case 8:
						if (num2-- <= 0U)
						{
							num = 10;
							continue;
						}
						spr_u17CD.ᜃ[(int)((UIntPtr)num2)] = A_0.ᜃ[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num4)))))];
						num = 4;
						continue;
					case 10:
						goto IL_FB;
					case 11:
						goto IL_6F;
					}
					if (A_1 == 0)
					{
						num = 5;
						continue;
					}
					num4 = A_1 >> 5;
					int num6 = A_1 & 31;
					spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ - (uint)num4 + 1U);
					num2 = (uint)(spr_u17CD.ᜃ.Length - 1);
					num = 2;
					continue;
					IL_6F:
					num = 1;
					continue;
					IL_C7:
					if (num6 != 0)
					{
						num = 6;
						continue;
					}
					IL_DB:
					num = 8;
				}
				IL_5C:
				return new spr\u17CD(A_0);
				IL_6A:
				IL_FB:
				spr_u17CD.ᜀ();
				return spr_u17CD;
			}
			}
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x00364620 File Offset: 0x00363620
		internal static spr\u17CD ᜁ(spr\u17CD A_0, uint A_1)
		{
			spr\u17CD spr_u17CD;
			uint num;
			ulong num2;
			for (;;)
			{
				for (;;)
				{
					spr_u17CD = new spr\u17CD(spr\u17CD.Sign.Positive, A_0.ᜂ + 1U);
					num = 0U;
					num2 = 0UL;
					if (true)
					{
					}
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if ((num += 1U) >= A_0.ᜂ)
							{
								num3 = 1;
								continue;
							}
							goto IL_44;
						case 1:
							goto IL_81;
						case 2:
							goto IL_44;
						}
						break;
						IL_44:
						num2 += (ulong)A_0.ᜃ[(int)((UIntPtr)num)] * (ulong)A_1;
						spr_u17CD.ᜃ[(int)((UIntPtr)num)] = (uint)num2;
						num2 >>= 32;
						num3 = 0;
					}
				}
				IL_81:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_99;
				}
			}
			IL_99:
			if (false)
			{
			}
			spr_u17CD.ᜃ[(int)((UIntPtr)num)] = (uint)num2;
			spr_u17CD.ᜀ();
			return spr_u17CD;
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x003646E0 File Offset: 0x003636E0
		internal static void ᜀ(uint[] A_0, uint A_1, uint A_2, uint[] A_3, uint A_4, uint A_5, uint[] A_6, uint A_7)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					uint num = A_1;
					uint num2 = num + A_2;
					uint num3 = A_4 + A_5;
					uint num4 = A_7;
					int num5 = 2;
					for (;;)
					{
						switch (num5)
						{
						case 0:
							goto IL_C0;
						case 1:
						{
							uint num6;
							if (num6 >= num3)
							{
								num5 = 3;
								continue;
							}
							ulong num7;
							uint num8;
							num7 += (ulong)A_0[(int)((UIntPtr)num)] * (ulong)A_3[(int)((UIntPtr)num6)] + (ulong)A_6[(int)((UIntPtr)num8)];
							A_6[(int)((UIntPtr)num8)] = (uint)num7;
							num7 >>= 32;
							num6 += 1U;
							num8 += 1U;
							num5 = 11;
							continue;
						}
						case 2:
							goto IL_C0;
						case 3:
							num5 = 9;
							continue;
						case 4:
						{
							ulong num7;
							uint num8;
							A_6[(int)((UIntPtr)num8)] = (uint)num7;
							num5 = 10;
							continue;
						}
						case 5:
						{
							ulong num7 = 0UL;
							uint num8 = num4;
							uint num6 = A_4;
							num5 = 12;
							continue;
						}
						case 6:
							if (num >= num2)
							{
								num5 = 7;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								num5 = 8;
								continue;
							}
							break;
						case 7:
							return;
						case 8:
							if (A_0[(int)((UIntPtr)num)] != 0U)
							{
								num5 = 5;
								continue;
							}
							goto IL_11D;
						case 9:
						{
							ulong num7;
							if (num7 != 0UL)
							{
								if (true)
								{
								}
								num5 = 4;
								continue;
							}
							goto IL_11D;
						}
						case 10:
							goto IL_11D;
						case 11:
							goto IL_135;
						case 12:
							goto IL_135;
						}
						break;
						IL_C0:
						num5 = 6;
						continue;
						IL_11D:
						num += 1U;
						num4 += 1U;
						num5 = 0;
						continue;
						IL_135:
						num5 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x00364894 File Offset: 0x00363894
		internal static void ᜀ(uint[] A_0, int A_1, int A_2, uint[] A_3, int A_4, int A_5, uint[] A_6, int A_7, int A_8)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					uint num = (uint)A_1;
					uint num2 = num + (uint)A_2;
					uint num3 = (uint)(A_4 + A_5);
					uint num4 = (uint)A_7;
					uint num5 = num4 + (uint)A_8;
					int num6 = 11;
					for (;;)
					{
						switch (num6)
						{
						case 0:
							goto IL_159;
						case 1:
							goto IL_159;
						case 2:
						{
							uint num7;
							ulong num8;
							A_6[(int)((UIntPtr)num7)] = (uint)num8;
							num6 = 6;
							continue;
						}
						case 3:
						{
							ulong num8 = 0UL;
							uint num7 = num4;
							uint num9 = (uint)A_4;
							num6 = 0;
							continue;
						}
						case 4:
						{
							uint num7;
							if (num7 >= num5)
							{
								num6 = 16;
								continue;
							}
							ulong num8;
							uint num9;
							num8 += (ulong)A_0[(int)((UIntPtr)num)] * (ulong)A_3[(int)((UIntPtr)num9)] + (ulong)A_6[(int)((UIntPtr)num7)];
							A_6[(int)((UIntPtr)num7)] = (uint)num8;
							num8 >>= 32;
							num9 += 1U;
							num7 += 1U;
							num6 = 1;
							continue;
						}
						case 5:
							num6 = 4;
							continue;
						case 6:
							goto IL_D5;
						case 7:
							return;
						case 8:
						{
							ulong num8;
							if (num8 != 0UL)
							{
								num6 = 13;
								continue;
							}
							goto IL_D5;
						}
						case 9:
							goto IL_138;
						case 10:
							if (A_0[(int)((UIntPtr)num)] != 0U)
							{
								if (true)
								{
								}
								num6 = 3;
								continue;
							}
							goto IL_D5;
						case 11:
							goto IL_138;
						case 12:
						{
							uint num7;
							if (num7 < num5)
							{
								num6 = 2;
								continue;
							}
							goto IL_D5;
						}
						case 13:
							num6 = 12;
							continue;
						case 14:
							if (num >= num2)
							{
								num6 = 7;
								continue;
							}
							num6 = 10;
							continue;
						case 15:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_159;
							default:
							{
								if (false)
								{
								}
								uint num9;
								if (num9 < num3)
								{
									num6 = 5;
									continue;
								}
								goto IL_99;
							}
							}
							break;
						case 16:
							goto IL_99;
						}
						break;
						IL_99:
						num6 = 8;
						continue;
						IL_D5:
						num += 1U;
						num4 += 1U;
						num6 = 9;
						continue;
						IL_138:
						num6 = 14;
						continue;
						IL_159:
						num6 = 15;
					}
				}
				return;
			}
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x00364AAC File Offset: 0x00363AAC
		internal static void ᜀ(spr\u17CD A_0, ref uint[] A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					uint[] array = A_1;
					A_1 = A_0.ᜃ;
					uint[] ᜃ = A_0.ᜃ;
					uint ᜂ = A_0.ᜂ;
					A_0.ᜃ = array;
					uint num = (uint)array.Length;
					uint num2 = 0U;
					int num3 = 12;
					for (;;)
					{
						uint num4;
						uint num5;
						uint num7;
						uint num15;
						switch (num3)
						{
						case 0:
							goto IL_1B2;
						case 1:
							goto IL_2D7;
						case 2:
							goto IL_388;
						case 3:
							goto IL_3A5;
						case 4:
							goto IL_44D;
						case 5:
						{
							if (num4 >= num5)
							{
								num3 = 19;
								continue;
							}
							ulong num6 = (ulong)ᜃ[(int)((UIntPtr)num4)] * (ulong)ᜃ[(int)((UIntPtr)num4)] + (ulong)array[(int)((UIntPtr)num7)];
							array[(int)((UIntPtr)num7)] = (uint)num6;
							num6 >>= 32;
							array[(int)((UIntPtr)(num7 += 1U))] += (uint)num6;
							num3 = 30;
							continue;
						}
						case 6:
						{
							uint num8 = num7;
							array[(int)((UIntPtr)(num8 += 1U))] += 1U;
							num3 = 26;
							continue;
						}
						case 7:
							if (num2 >= num)
							{
								num3 = 15;
								continue;
							}
							array[(int)((UIntPtr)num2)] = 0U;
							num2 += 1U;
							num3 = 16;
							continue;
						case 8:
							num3 = 27;
							continue;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
							{
								if (false)
								{
								}
								uint num9;
								array[(int)((UIntPtr)num7)] = num9;
								num3 = 32;
								continue;
							}
							}
							break;
						case 10:
						{
							uint num10;
							if (num10 >= ᜂ)
							{
								num3 = 8;
								continue;
							}
							ulong num11;
							uint num12;
							uint num13;
							uint num14;
							num11 += (ulong)num12 * (ulong)ᜃ[(int)((UIntPtr)num13)] + (ulong)array[(int)((UIntPtr)num14)];
							array[(int)((UIntPtr)num14)] = (uint)num11;
							num11 >>= 32;
							num10 += 1U;
							num14 += 1U;
							num13 += 1U;
							num3 = 22;
							continue;
						}
						case 11:
							return;
						case 12:
							if (true)
							{
							}
							goto IL_4A1;
						case 13:
							if (ᜃ[(int)((UIntPtr)num4)] != 0U)
							{
								num3 = 34;
								continue;
							}
							goto IL_44D;
						case 14:
						{
							num7 = 0U;
							uint num9 = 0U;
							num3 = 28;
							continue;
						}
						case 15:
							num4 = 0U;
							num7 = 0U;
							num15 = 0U;
							num3 = 1;
							continue;
						case 16:
							goto IL_4A1;
						case 17:
							if (num15 >= ᜂ)
							{
								num3 = 14;
								continue;
							}
							num3 = 13;
							continue;
						case 18:
							goto IL_1B2;
						case 19:
							A_0.ᜂ <<= 1;
							num3 = 21;
							continue;
						case 20:
						{
							uint num9;
							if (num9 != 0U)
							{
								num3 = 9;
								continue;
							}
							goto IL_416;
						}
						case 21:
							goto IL_474;
						case 22:
							goto IL_26A;
						case 23:
							goto IL_26A;
						case 24:
						{
							uint num8;
							if (array[(int)((UIntPtr)(num8++))] != 0U)
							{
								num3 = 2;
								continue;
							}
							array[(int)((UIntPtr)num8)] += 1U;
							num3 = 3;
							continue;
						}
						case 25:
							num3 = 33;
							continue;
						case 26:
							goto IL_3A5;
						case 27:
						{
							ulong num11;
							if (num11 != 0UL)
							{
								num3 = 37;
								continue;
							}
							goto IL_44D;
						}
						case 28:
							goto IL_245;
						case 29:
							if (array[(int)((UIntPtr)(A_0.ᜂ - 1U))] == 0U)
							{
								num3 = 25;
								continue;
							}
							return;
						case 30:
						{
							ulong num6;
							if (array[(int)((UIntPtr)num7)] < (uint)num6)
							{
								num3 = 6;
								continue;
							}
							goto IL_388;
						}
						case 31:
							goto IL_474;
						case 32:
							goto IL_416;
						case 33:
							if (A_0.ᜂ <= 1U)
							{
								num3 = 11;
								continue;
							}
							A_0.ᜂ -= 1U;
							num3 = 31;
							continue;
						case 34:
						{
							ulong num11 = 0UL;
							uint num12 = ᜃ[(int)((UIntPtr)num4)];
							uint num13 = num4 + 1U;
							uint num14 = num7 + 2U * num15 + 1U;
							uint num10 = num15 + 1U;
							num3 = 23;
							continue;
						}
						case 35:
							num3 = 20;
							continue;
						case 36:
							goto IL_245;
						case 37:
						{
							ulong num11;
							uint num14;
							array[(int)((UIntPtr)num14)] = (uint)num11;
							num3 = 4;
							continue;
						}
						case 38:
						{
							if (num7 >= num)
							{
								num3 = 35;
								continue;
							}
							uint num16 = array[(int)((UIntPtr)num7)];
							uint num9;
							array[(int)((UIntPtr)num7)] = (num16 << 1 | num9);
							num9 = num16 >> 31;
							num7 += 1U;
							num3 = 36;
							continue;
						}
						case 39:
							goto IL_2D7;
						}
						break;
						IL_1B2:
						num3 = 5;
						continue;
						IL_245:
						num3 = 38;
						continue;
						IL_26A:
						num3 = 10;
						continue;
						IL_2D7:
						num3 = 17;
						continue;
						IL_388:
						num4 += 1U;
						num7 += 1U;
						num3 = 0;
						continue;
						IL_3A5:
						num3 = 24;
						continue;
						IL_416:
						num4 = 0U;
						num7 = 0U;
						num5 = num4 + ᜂ;
						num3 = 18;
						continue;
						IL_44D:
						num15 += 1U;
						num4 += 1U;
						num3 = 39;
						continue;
						IL_474:
						num3 = 29;
						continue;
						IL_4A1:
						num3 = 7;
					}
				}
				return;
			}
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x00365004 File Offset: 0x00364004
		internal static spr\u17CD ᜁ(spr\u17CD A_0, spr\u17CD A_1)
		{
			switch (0)
			{
			default:
			{
				uint num3;
				int num4;
				for (;;)
				{
					spr\u17CD spr_u17CD = A_0;
					spr\u17CD spr_u17CD2 = A_1;
					spr\u17CD spr_u17CD3 = spr_u17CD2;
					int num = 15;
					for (;;)
					{
						uint num2;
						switch (num)
						{
						case 0:
							goto IL_DB;
						case 1:
							goto IL_1AD;
						case 2:
							goto IL_104;
						case 3:
							return spr_u17CD3;
						case 4:
							num = 2;
							continue;
						case 5:
							goto IL_104;
						case 6:
							if (num2 == 0U)
							{
								num = 13;
								continue;
							}
							goto IL_9A;
						case 7:
							if ((num3 & 1U) != 0U)
							{
								num = 8;
								continue;
							}
							num3 >>= 1;
							num = 5;
							continue;
						case 8:
							num = 19;
							continue;
						case 9:
							if ((num2 & 1U) != 0U)
							{
								num = 4;
								continue;
							}
							num2 >>= 1;
							num = 17;
							continue;
						case 10:
							if (((num2 | num3) & 1U) != 0U)
							{
								num = 22;
								continue;
							}
							num2 >>= 1;
							num3 >>= 1;
							num4++;
							num = 18;
							continue;
						case 11:
							goto IL_247;
						case 12:
							if (spr_u17CD.ᜂ <= 1U)
							{
								num = 14;
								continue;
							}
							spr_u17CD3 = spr_u17CD;
							spr_u17CD = spr\u17CD.\u170D(spr_u17CD2, spr_u17CD);
							spr_u17CD2 = spr_u17CD3;
							num = 0;
							continue;
						case 13:
							goto IL_1CC;
						case 14:
							num = 23;
							continue;
						case 15:
							goto IL_DB;
						case 16:
							goto IL_1A8;
						case 17:
							goto IL_9A;
						case 18:
							goto IL_247;
						case 19:
							if (num2 >= num3)
							{
								num = 16;
								continue;
							}
							num3 = num3 - num2 >> 1;
							num = 1;
							continue;
						case 20:
							goto IL_1AD;
						case 21:
							goto IL_1AD;
						case 22:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1A8;
							default:
								if (false)
								{
								}
								num = 20;
								continue;
							}
							break;
						case 23:
							if (spr\u17CD.ᜁ(spr_u17CD, 0U))
							{
								if (true)
								{
								}
								num = 3;
								continue;
							}
							num3 = spr_u17CD.ᜃ[0];
							num2 = spr\u17CD.ᜃ(spr_u17CD2, num3);
							num4 = 0;
							num = 11;
							continue;
						}
						break;
						IL_9A:
						num = 9;
						continue;
						IL_DB:
						num = 12;
						continue;
						IL_104:
						num = 7;
						continue;
						IL_1A8:
						num2 = num2 - num3 >> 1;
						num = 21;
						continue;
						IL_1AD:
						num = 6;
						continue;
						IL_247:
						num = 10;
					}
				}
				IL_1CC:
				return spr\u17CD.ᜀ(num3 << num4);
			}
			}
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x003652B4 File Offset: 0x003642B4
		internal static uint ᜀ(spr\u17CD A_0, uint A_1)
		{
			switch (0)
			{
			default:
			{
				uint num3;
				for (;;)
				{
					uint num = A_1;
					uint num2 = spr\u17CD.ᜃ(A_0, A_1);
					num3 = 0U;
					uint num4 = 1U;
					int num5 = 6;
					for (;;)
					{
						switch (num5)
						{
						case 0:
							if (num != 0U)
							{
								num5 = 3;
								continue;
							}
							return 0U;
						case 1:
							goto IL_88;
						case 2:
							if (num2 == 0U)
							{
								num5 = 7;
								continue;
							}
							num5 = 8;
							continue;
						case 3:
							num5 = 9;
							continue;
						case 4:
							return num4;
						case 5:
							goto IL_7C;
						case 6:
							goto IL_88;
						case 7:
							goto IL_A3;
						case 8:
							if (num2 == 1U)
							{
								num5 = 4;
								continue;
							}
							num3 += num / num2 * num4;
							num %= num2;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D2;
							default:
								if (false)
								{
								}
								num5 = 0;
								continue;
							}
							break;
						case 9:
							if (num == 1U)
							{
								num5 = 5;
								continue;
							}
							num4 += num2 / num * num3;
							num2 %= num;
							goto IL_D2;
						}
						break;
						IL_88:
						num5 = 2;
						continue;
						IL_D2:
						num5 = 1;
					}
				}
				IL_7C:
				if (true)
				{
				}
				return A_1 - num3;
				IL_A3:
				return 0U;
			}
			}
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x003653EC File Offset: 0x003643EC
		internal static spr\u17CD ᜀ(spr\u17CD A_0, spr\u17CD A_1)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 6;
				spr\u17CD.ᜂ ᜂ;
				spr\u17CD[] array2;
				spr\u17CD[] array3;
				for (;;)
				{
					spr\u17CD[] array;
					spr\u17CD spr_u17CD;
					int num2;
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						if (spr\u17CD.ᜀ(array[0], 1U))
						{
							num = 3;
							continue;
						}
						goto IL_204;
					case 2:
						goto IL_93;
					case 3:
						goto IL_1AE;
					case 4:
						goto IL_6F;
					case 5:
						if (!spr\u17CD.ᜀ(spr_u17CD, 0U))
						{
							num = 7;
							continue;
						}
						num = 8;
						continue;
					case 7:
						num = 1;
						continue;
					case 8:
						if (num2 > 1)
						{
							num = 10;
							continue;
						}
						goto IL_93;
					case 9:
						goto IL_6A;
					case 10:
					{
						spr\u17CD spr_u17CD2 = ᜂ.ᜃ(array2[0], spr\u17CD.ᜋ(array2[1], array3[0]));
						array2[0] = array2[1];
						array2[1] = spr_u17CD2;
						num = 2;
						continue;
					}
					}
					IL_55:
					if (A_1.ᜂ == 1U)
					{
						num = 9;
						continue;
					}
					array2 = new spr\u17CD[]
					{
						spr\u17CD.ᜀ(0),
						spr\u17CD.ᜀ(1)
					};
					array3 = new spr\u17CD[2];
					array = new spr\u17CD[]
					{
						spr\u17CD.ᜀ(0),
						spr\u17CD.ᜀ(0)
					};
					num2 = 0;
					spr\u17CD a_2 = A_1;
					spr_u17CD = A_0;
					ᜂ = new spr\u17CD.ᜂ(A_1);
					num = 0;
					continue;
					IL_6F:
					num = 5;
					continue;
					IL_93:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
					{
						if (false)
						{
						}
						spr\u17CD[] array4 = spr\u17CD.ᜀ.ᜂ(a_2, spr_u17CD);
						array3[0] = array3[1];
						array3[1] = array4[0];
						array[0] = array[1];
						array[1] = array4[1];
						a_2 = spr_u17CD;
						spr_u17CD = array4[1];
						num2++;
						num = 4;
						break;
					}
					}
				}
				IL_6A:
				return spr\u17CD.ᜀ(spr\u17CD.ᜀ.ᜀ(A_0, A_1.ᜃ[0]));
				IL_1AE:
				throw new ArithmeticException(ClipboardData.b("⁭Ὧ剱ᵳᡵ๷ό๻ൽꎁ", a_));
				IL_204:
				if (true)
				{
				}
				return ᜂ.ᜃ(array2[0], spr\u17CD.ᜋ(array2[1], array3[0]));
			}
			}
		}
	}
}
