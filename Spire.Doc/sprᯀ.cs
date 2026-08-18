using System;
using System.Collections;
using System.Globalization;
using Spire.CompoundFile.Doc;
using Spire.Doc.Convertors.Sgml;

// Token: 0x020001FB RID: 507
internal class sprᯀ
{
	// Token: 0x0600162B RID: 5675 RVA: 0x00166054 File Offset: 0x00165054
	public Occurrence ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x00166098 File Offset: 0x00165098
	public bool ᜂ()
	{
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
			if (this.ᜄ)
			{
				return this.ᜁ.Count == 0;
			}
			break;
		}
		return false;
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x001660F0 File Offset: 0x001650F0
	public sprᯀ ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x00166134 File Offset: 0x00165134
	public sprᯀ(sprᯀ A_0)
	{
		this.ᜀ = A_0;
		this.ᜁ = new ArrayList();
		this.ᜂ = GroupType.None;
		this.ᜃ = Occurrence.Required;
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x00166168 File Offset: 0x00165168
	public void ᜀ(sprᯀ A_0)
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
		this.ᜁ.Add(A_0);
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x001661B0 File Offset: 0x001651B0
	public void ᜀ(string A_0)
	{
		int a_ = 2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (string.Equals(A_0, ClipboardData.b("䭧㩩⽫⩭ㅯ♱㕳", a_), StringComparison.OrdinalIgnoreCase))
			{
				if (true)
				{
				}
				this.ᜄ = true;
				return;
			}
			break;
		}
		this.ᜁ.Add(A_0);
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x00166224 File Offset: 0x00165224
	public void ᜁ(char A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 10;
			GroupType groupType;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 17;
					continue;
				case 1:
					if (A_0 != '|')
					{
						num = 13;
						continue;
					}
					groupType = GroupType.Or;
					num = 9;
					continue;
				case 2:
					goto IL_1D0;
				case 3:
					num = 4;
					continue;
				case 4:
					if (A_0 != ',')
					{
						num = 11;
						continue;
					}
					groupType = GroupType.Sequence;
					num = 5;
					continue;
				case 5:
					goto IL_1D5;
				case 6:
					if (this.ᜂ != GroupType.None)
					{
						num = 7;
						continue;
					}
					goto IL_249;
				case 7:
					num = 14;
					continue;
				case 8:
					goto IL_1D5;
				case 9:
					goto IL_1D5;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13D;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 12:
					goto IL_F9;
				case 13:
					num = 8;
					continue;
				case 14:
					if (this.ᜂ != groupType)
					{
						num = 12;
						continue;
					}
					goto IL_249;
				case 15:
					goto IL_1D5;
				case 16:
					if (true)
					{
					}
					if (A_0 != '&')
					{
						num = 3;
						continue;
					}
					groupType = GroupType.And;
					num = 15;
					continue;
				case 17:
					if (this.ᜁ.Count == 0)
					{
						num = 2;
						continue;
					}
					goto IL_A7;
				}
				if (!this.ᜄ)
				{
					num = 0;
					continue;
				}
				IL_A7:
				groupType = GroupType.None;
				num = 16;
				continue;
				IL_1D5:
				num = 6;
			}
			IL_F9:
			IL_13D:
			throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, ClipboardData.b("㍯ᵱᩳᡵᵷ᥹ࡻᅽꊁꎃﶅ뢇ꮋ꺍憐뒓ﾕ蓮펟쮡힣튥춧쒩\ud8ab躭잯\udbb1삳\udeb5颷솹趻쎽ꗁ뛃꧅뷇뫉", a_), new object[]
			{
				A_0,
				this.ᜂ.ToString()
			}));
			IL_1D0:
			throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, ClipboardData.b("㵯᭱ݳյᅷᑹ᭻幽ꪉ﶑뢗蓮캟잡잣튥잧\ud8a9貫覭쮯花즳醵隷", a_), new object[]
			{
				A_0
			}));
			IL_249:
			this.ᜂ = groupType;
			return;
		}
		}
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x00166484 File Offset: 0x00165484
	public void ᜀ(char A_0)
	{
		Occurrence occurrence;
		for (;;)
		{
			occurrence = Occurrence.Required;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B8;
				case 1:
					switch (A_0)
					{
					case '*':
						occurrence = Occurrence.ZeroOrMore;
						num = 0;
						continue;
					case '+':
						goto IL_53;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					if (true)
					{
					}
					num = 3;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_53;
					default:
						if (false)
						{
						}
						if (A_0 == '?')
						{
							num = 5;
							continue;
						}
						goto IL_BA;
					}
					break;
				case 4:
					goto IL_69;
				case 5:
					occurrence = Occurrence.Optional;
					num = 4;
					continue;
				case 6:
					goto IL_5D;
				}
				break;
				IL_53:
				occurrence = Occurrence.OneOrMore;
				num = 6;
			}
		}
		IL_5D:
		IL_69:
		IL_B8:
		IL_BA:
		this.ᜃ = occurrence;
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x00166554 File Offset: 0x00165554
	public bool ᜀ(string A_0, spr\u2057 A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				IEnumerator enumerator;
				IEnumerator enumerator2;
				switch (num)
				{
				case 1:
				{
					bool result;
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								result = true;
								num = 6;
								continue;
							case 1:
								num = 7;
								continue;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								object obj = enumerator.Current;
								num = 5;
								continue;
							}
							case 3:
								num = 8;
								continue;
							case 5:
							{
								object obj;
								if (obj is string)
								{
									num = 3;
									continue;
								}
								break;
							}
							case 6:
								goto IL_2F4;
							case 7:
								goto IL_352;
							case 8:
							{
								object obj;
								if (string.Equals((string)obj, A_0, StringComparison.OrdinalIgnoreCase))
								{
									num = 0;
									continue;
								}
								break;
							}
							}
							IL_2CB:
							num = 2;
							continue;
							goto IL_2CB;
						}
						IL_2F4:
						return result;
						IL_352:
						goto IL_23C;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_39D;
								case 1:
									disposable.Dispose();
									num = 0;
									continue;
								case 2:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_39F;
								}
								break;
							}
						}
						IL_39D:
						IL_39F:;
					}
					return result;
				}
				case 2:
					try
					{
						num = 15;
						bool result;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1AA;
							case 1:
							{
								spr\u1D66 spr_u1D;
								if (spr_u1D.ᜀ(A_0, A_1))
								{
									num = 3;
									continue;
								}
								break;
							}
							case 2:
							{
								string text;
								spr\u1D66 spr_u1D = A_1.ᜃ(text);
								num = 9;
								continue;
							}
							case 3:
								result = true;
								num = 0;
								continue;
							case 4:
								num = 14;
								continue;
							case 5:
							{
								sprᯀ sprᯀ;
								if (sprᯀ.ᜀ(A_0, A_1))
								{
									num = 13;
									continue;
								}
								break;
							}
							case 6:
							{
								string text;
								if (text != null)
								{
									num = 2;
									continue;
								}
								object obj2;
								sprᯀ sprᯀ = (sprᯀ)obj2;
								goto IL_1B7;
							}
							case 7:
							{
								spr\u1D66 spr_u1D;
								if (spr_u1D.ᜂ())
								{
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_1B7;
									default:
										if (false)
										{
										}
										num = 12;
										continue;
									}
								}
								break;
							}
							case 8:
							{
								if (!enumerator2.MoveNext())
								{
									num = 4;
									continue;
								}
								object obj2 = enumerator2.Current;
								string text = obj2 as string;
								num = 6;
								continue;
							}
							case 9:
							{
								spr\u1D66 spr_u1D;
								if (spr_u1D != null)
								{
									num = 10;
									continue;
								}
								break;
							}
							case 10:
								num = 7;
								continue;
							case 11:
								goto IL_FF;
							case 12:
								num = 1;
								continue;
							case 13:
								result = true;
								num = 11;
								continue;
							case 14:
								goto IL_1EE;
							}
							IL_CD:
							num = 8;
							continue;
							goto IL_CD;
							IL_1B7:
							num = 5;
						}
						IL_FF:
						IL_1AA:
						return result;
						IL_1EE:
						return false;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable2 = enumerator2 as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable2 != null)
									{
										num = 1;
										continue;
									}
									goto IL_23B;
								case 1:
									disposable2.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_239;
								}
								break;
							}
						}
						IL_239:
						IL_23B:;
					}
					goto IL_23C;
				case 3:
					goto IL_52;
				}
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				enumerator = this.ᜁ.GetEnumerator();
				num = 1;
				continue;
				IL_23C:
				enumerator2 = this.ᜁ.GetEnumerator();
				num = 2;
			}
			IL_52:
			throw new ArgumentNullException(ClipboardData.b("፶൸ὺ", a_));
		}
		}
	}

	// Token: 0x04001A11 RID: 6673
	private sprᯀ ᜀ;

	// Token: 0x04001A12 RID: 6674
	private ArrayList ᜁ;

	// Token: 0x04001A13 RID: 6675
	private GroupType ᜂ;

	// Token: 0x04001A14 RID: 6676
	private Occurrence ᜃ;

	// Token: 0x04001A15 RID: 6677
	private bool ᜄ;
}
