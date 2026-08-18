using System;
using System.Collections;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020002C0 RID: 704
internal class spr\u206E
{
	// Token: 0x0600265B RID: 9819 RVA: 0x0025F51C File Offset: 0x0025E51C
	internal spr\u206E(string A_0)
	{
		this.ᜄ(A_0);
		this.ᜃ = new sprỬ[this.ᜁ.Count];
		for (int i = 0; i < this.ᜁ.Count; i++)
		{
			this.ᜃ[i] = (sprỬ)this.ᜁ[i];
		}
		this.ᜄ = new spr\u2055[this.ᜂ.Count / 2];
		for (int j = 0; j < this.ᜂ.Count / 2; j++)
		{
			this.ᜄ[j] = new spr\u2055((sprṚ)this.ᜂ[2 * j], (sprṚ)this.ᜂ[2 * j + 1]);
		}
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x0025F610 File Offset: 0x0025E610
	private void ᜄ(string A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u206E.PathParserState pathParserState;
			for (;;)
			{
				pathParserState = spr\u206E.PathParserState.Command;
				int num = 0;
				int num2 = 29;
				for (;;)
				{
					char c;
					spr\u206E.PathParserState pathParserState5;
					switch (num2)
					{
					case 0:
						num2 = 10;
						continue;
					case 1:
						goto IL_461;
					case 2:
					{
						spr\u206E.PathParserState pathParserState2;
						switch (pathParserState2)
						{
						case spr\u206E.PathParserState.Restart:
							goto IL_20D;
						case spr\u206E.PathParserState.Command:
							this.ᜁ();
							num2 = 14;
							continue;
						case spr\u206E.PathParserState.Ref:
						case spr\u206E.PathParserState.Number:
							this.ᜀ();
							num2 = 38;
							continue;
						default:
							num2 = 40;
							continue;
						}
						break;
					}
					case 3:
						goto IL_41F;
					case 4:
						if (!char.IsDigit(c))
						{
							num2 = 24;
							continue;
						}
						goto IL_3BB;
					case 5:
						if (num >= A_0.Length)
						{
							num2 = 18;
							continue;
						}
						c = A_0[num];
						num2 = 4;
						continue;
					case 6:
						goto IL_3BB;
					case 7:
						this.ᜁ();
						num2 = 32;
						continue;
					case 8:
						num2 = 23;
						continue;
					case 9:
						if (spr\u206E.ᜃ(this.ᜀ.ToString()))
						{
							num2 = 7;
							continue;
						}
						goto IL_41F;
					case 10:
						goto IL_2B8;
					case 11:
					{
						spr\u206E.PathParserState pathParserState3 = pathParserState;
						num2 = 34;
						continue;
					}
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40B;
						default:
							if (false)
							{
							}
							if (c == '@')
							{
								num2 = 19;
								continue;
							}
							num2 = 35;
							continue;
						}
						break;
					case 13:
						goto IL_461;
					case 14:
						goto IL_20D;
					case 15:
						goto IL_18C;
					case 16:
						goto IL_461;
					case 17:
						goto IL_461;
					case 18:
						goto IL_251;
					case 19:
					{
						spr\u206E.PathParserState pathParserState2 = pathParserState;
						num2 = 2;
						continue;
					}
					case 20:
						if (char.IsLower(c))
						{
							num2 = 11;
							continue;
						}
						num2 = 33;
						continue;
					case 21:
						goto IL_2B8;
					case 22:
						goto IL_20D;
					case 23:
						goto IL_41F;
					case 24:
						num2 = 31;
						continue;
					case 25:
						goto IL_41F;
					case 26:
						num2 = 15;
						continue;
					case 27:
						goto IL_18C;
					case 28:
					{
						spr\u206E.PathParserState pathParserState4 = pathParserState;
						num2 = 36;
						continue;
					}
					case 29:
						goto IL_22D;
					case 30:
						goto IL_2B8;
					case 31:
						if (c == '-')
						{
							num2 = 6;
							continue;
						}
						num2 = 20;
						continue;
					case 32:
						goto IL_41F;
					case 33:
						if (c == ',')
						{
							num2 = 28;
							continue;
						}
						num2 = 12;
						continue;
					case 34:
					{
						spr\u206E.PathParserState pathParserState3;
						switch (pathParserState3)
						{
						case spr\u206E.PathParserState.Restart:
							this.ᜀ();
							num2 = 25;
							continue;
						case spr\u206E.PathParserState.Command:
							num2 = 9;
							continue;
						case spr\u206E.PathParserState.Ref:
						case spr\u206E.PathParserState.Number:
							goto IL_40B;
						default:
							num2 = 8;
							continue;
						}
						break;
					}
					case 35:
						if (true)
						{
						}
						goto IL_461;
					case 36:
					{
						spr\u206E.PathParserState pathParserState4;
						switch (pathParserState4)
						{
						case spr\u206E.PathParserState.Restart:
						case spr\u206E.PathParserState.Ref:
						case spr\u206E.PathParserState.Number:
							goto IL_18C;
						case spr\u206E.PathParserState.Command:
							this.ᜁ();
							num2 = 27;
							continue;
						default:
							num2 = 26;
							continue;
						}
						break;
					}
					case 37:
						goto IL_22D;
					case 38:
						goto IL_20D;
					case 39:
						switch (pathParserState5)
						{
						case spr\u206E.PathParserState.Restart:
							pathParserState = spr\u206E.PathParserState.Number;
							num2 = 21;
							continue;
						case spr\u206E.PathParserState.Command:
							this.ᜁ();
							pathParserState = spr\u206E.PathParserState.Number;
							num2 = 30;
							continue;
						case spr\u206E.PathParserState.Ref:
						case spr\u206E.PathParserState.Number:
							goto IL_2B8;
						default:
							num2 = 0;
							continue;
						}
						break;
					case 40:
						num2 = 22;
						continue;
					}
					break;
					IL_18C:
					this.ᜀ();
					pathParserState = spr\u206E.PathParserState.Restart;
					num2 = 16;
					continue;
					IL_20D:
					pathParserState = spr\u206E.PathParserState.Ref;
					this.ᜀ.Append(c);
					num2 = 17;
					continue;
					IL_22D:
					num2 = 5;
					continue;
					IL_2B8:
					this.ᜀ.Append(c);
					num2 = 13;
					continue;
					IL_3BB:
					pathParserState5 = pathParserState;
					num2 = 39;
					continue;
					IL_40B:
					this.ᜀ();
					num2 = 3;
					continue;
					IL_41F:
					pathParserState = spr\u206E.PathParserState.Command;
					this.ᜀ.Append(c);
					num2 = 1;
					continue;
					IL_461:
					num++;
					num2 = 37;
				}
			}
			IL_251:
			this.ᜀ(pathParserState);
			return;
		}
		}
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x0025FAB4 File Offset: 0x0025EAB4
	private void ᜀ(spr\u206E.PathParserState A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9C;
				case 1:
					goto IL_8F;
				case 2:
					if (true)
					{
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
						switch (A_0)
						{
						case spr\u206E.PathParserState.Restart:
						case spr\u206E.PathParserState.Command:
							goto IL_9E;
						case spr\u206E.PathParserState.Ref:
						case spr\u206E.PathParserState.Number:
							break;
						default:
							num = 3;
							continue;
						}
						break;
					}
					this.ᜀ();
					this.ᜀ.Append('e');
					num = 1;
					continue;
				case 3:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_8F:
		IL_9C:
		IL_9E:
		this.ᜁ();
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x0025FB68 File Offset: 0x0025EB68
	private void ᜁ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				sprỬ sprỬ = (sprỬ)this.ᜁ[this.ᜁ.Count - 1];
				this.ᜁ[this.ᜁ.Count - 1] = new sprỬ(sprỬ.ᜀ(), spr\u206E.ᜀ(sprỬ, this.ᜅ / 2));
				if (true)
				{
				}
				num = 2;
				continue;
			}
			case 2:
				goto IL_BF;
			}
			if (this.ᜁ.Count <= 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_BF;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
		}
		IL_BF:
		string a_ = this.ᜀ.ToString();
		this.ᜀ.Length = 0;
		sprỬ value = new sprỬ(spr\u206E.ᜂ(a_), 0);
		this.ᜁ.Add(value);
		this.ᜆ = spr\u206E.ᜁ(a_);
		this.ᜅ = 0;
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x0025FC7C File Offset: 0x0025EC7C
	private static int ᜀ(sprỬ A_0, int A_1)
	{
		int num2;
		for (;;)
		{
			PathType pathType = A_0.ᜀ();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return 1;
				case 1:
					goto IL_6E;
				case 2:
					if (num2 <= 0)
					{
						num = 1;
						continue;
					}
					goto IL_7A;
				case 3:
					if (pathType == PathType.Close)
					{
						num = 0;
						continue;
					}
					num2 = A_0.ᜂ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7A;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				break;
			}
		}
		return 1;
		IL_6E:
		if (true)
		{
		}
		return 0;
		IL_7A:
		return A_1 / num2;
	}

	// Token: 0x06002660 RID: 9824 RVA: 0x0025FD10 File Offset: 0x0025ED10
	private static bool ᜃ(string A_0)
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
		return spr\u206E.ᜂ(A_0) != PathType.Unknown;
	}

	// Token: 0x06002661 RID: 9825 RVA: 0x0025FD58 File Offset: 0x0025ED58
	private static PathType ᜂ(string A_0)
	{
		int a_ = 14;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("ɳ", a_)))
				{
					num = 8;
					continue;
				}
				return PathType.CurveTo;
			case 3:
				num = 7;
				continue;
			case 4:
				goto IL_59;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A5;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 6:
				if (!(A_0 == ClipboardData.b("s", a_)))
				{
					num = 0;
					continue;
				}
				return PathType.MoveTo;
			case 7:
				goto IL_A5;
			case 8:
				num = 6;
				continue;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			goto IL_117;
			IL_A5:
			if (A_0 == ClipboardData.b("ٳ", a_))
			{
				break;
			}
			num = 5;
		}
		return PathType.LineTo;
		IL_59:
		if (true)
		{
		}
		IL_117:
		return sprᥜ.ᜀ(A_0);
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x0025FE84 File Offset: 0x0025EE84
	private static bool ᜁ(string A_0)
	{
		int a_ = 12;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("q", a_)))
				{
					num = 3;
					continue;
				}
				return true;
			case 1:
				return true;
			case 3:
				num = 4;
				continue;
			case 4:
				if (!(A_0 == ClipboardData.b("ѱ", a_)))
				{
					num = 5;
					continue;
				}
				return true;
			case 5:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return true;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 6:
				num = 0;
				continue;
			case 7:
				if (A_0 == ClipboardData.b("ٱ", a_))
				{
					num = 1;
					continue;
				}
				return false;
			}
			if (A_0 == null)
			{
				return false;
			}
			num = 6;
		}
		return true;
	}

	// Token: 0x06002663 RID: 9827 RVA: 0x0025FF9C File Offset: 0x0025EF9C
	private void ᜀ()
	{
		sprṚ sprṚ;
		for (;;)
		{
			IL_30:
			string a_ = this.ᜀ.ToString();
			this.ᜀ.Length = 0;
			sprṚ = spr\u206E.ᜀ(a_);
			bool flag = spr\u1CC6.ᜀ((long)this.ᜅ);
			for (;;)
			{
				IL_5C:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 9;
						continue;
					case 1:
						goto IL_BD;
					case 2:
						if (this.ᜆ)
						{
							num = 0;
							continue;
						}
						num = 6;
						continue;
					case 3:
						goto IL_138;
					case 4:
						goto IL_118;
					case 5:
						this.ᜈ = sprṚ.ᜂ();
						num = 8;
						continue;
					case 6:
						if (flag)
						{
							num = 5;
							continue;
						}
						this.ᜇ = sprṚ.ᜂ();
						num = 4;
						continue;
					case 7:
						sprṚ = new sprṚ(sprṚ.ᜂ() + this.ᜈ);
						num = 3;
						continue;
					case 8:
						goto IL_9A;
					case 9:
						if (!flag)
						{
							sprṚ = new sprṚ(sprṚ.ᜂ() + this.ᜇ);
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5C;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 7;
							continue;
						}
						break;
					}
					goto IL_30;
				}
			}
		}
		IL_9A:
		IL_BD:
		IL_118:
		IL_138:
		this.ᜂ.Add(sprṚ);
		this.ᜅ++;
	}

	// Token: 0x06002664 RID: 9828 RVA: 0x0026011C File Offset: 0x0025F11C
	internal static sprṚ ᜀ(string A_0)
	{
		int a_ = 15;
		int num = 4;
		bool a_2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_42;
				default:
					if (false)
					{
					}
					if (A_0.StartsWith(ClipboardData.b("㕴", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_54;
				}
				break;
			case 1:
				a_2 = false;
				num = 0;
				continue;
			case 2:
				goto IL_80;
			case 3:
				a_2 = true;
				A_0 = A_0.TrimStart(new char[]
				{
					'@'
				});
				num = 2;
				continue;
			}
			goto IL_2D;
			IL_42:
			if (true)
			{
			}
			num = 1;
			continue;
			IL_2D:
			if (spr\u1CC6.ᜋ(A_0))
			{
				goto IL_42;
			}
			goto IL_CB;
		}
		IL_54:
		return new sprṚ(sprᜌ.ᜆ(A_0), a_2);
		IL_80:
		goto IL_54;
		IL_CB:
		return new sprṚ();
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x002601FC File Offset: 0x0025F1FC
	internal sprỬ[] ᜃ()
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
		return this.ᜃ;
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x00260240 File Offset: 0x0025F240
	internal spr\u2055[] ᜂ()
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
		return this.ᜄ;
	}

	// Token: 0x04002234 RID: 8756
	private readonly StringBuilder ᜀ = new StringBuilder();

	// Token: 0x04002235 RID: 8757
	private readonly ArrayList ᜁ = new ArrayList();

	// Token: 0x04002236 RID: 8758
	private readonly ArrayList ᜂ = new ArrayList();

	// Token: 0x04002237 RID: 8759
	private readonly sprỬ[] ᜃ;

	// Token: 0x04002238 RID: 8760
	private readonly spr\u2055[] ᜄ;

	// Token: 0x04002239 RID: 8761
	private int ᜅ;

	// Token: 0x0400223A RID: 8762
	private bool ᜆ;

	// Token: 0x0400223B RID: 8763
	private int ᜇ;

	// Token: 0x0400223C RID: 8764
	private int ᜈ;

	// Token: 0x020002C1 RID: 705
	private enum PathParserState
	{
		// Token: 0x0400223E RID: 8766
		Restart,
		// Token: 0x0400223F RID: 8767
		Command,
		// Token: 0x04002240 RID: 8768
		Ref,
		// Token: 0x04002241 RID: 8769
		Number
	}
}
