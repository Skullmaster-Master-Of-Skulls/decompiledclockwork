using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005AE RID: 1454
internal class spr\u2575 : CollectionExtended<sprᨠ>
{
	// Token: 0x060057F5 RID: 22517 RVA: 0x0037CE24 File Offset: 0x0037BE24
	private spr\u2575(spr\u2158 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060057F6 RID: 22518 RVA: 0x0037CE3C File Offset: 0x0037BE3C
	public spr\u2575(spr\u2158 A_0, object A_1, IList<sprἏ> A_2)
	{
		int a_ = 9;
		base..ctor(A_0, A_1);
		if (A_2 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䬾⹀⡂⁄⥆㩈", a_));
		}
		this.ᜀ(A_2);
	}

	// Token: 0x060057F7 RID: 22519 RVA: 0x0037CE7C File Offset: 0x0037BE7C
	public new CellFormatType ᜂ(double A_0)
	{
		int a_ = 6;
		sprᨠ sprᨠ = this.ᜀ(A_0);
		if (sprᨠ == null)
		{
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
				throw new FormatException(RecordTableEnumerator.b("稻儽㈿⽁╃㉅桇㥉⥋ⵍ⑏㭑㭓㡕硗㥙㵛そ๟ൡၣ䙥੧ཀྵ䱫࡭Ὧݱᩳት噷", a_));
			}
		}
		return sprᨠ.ᜑ();
	}

	// Token: 0x060057F8 RID: 22520 RVA: 0x0037CEE8 File Offset: 0x0037BEE8
	public new CellFormatType ᜀ(string A_0)
	{
		if (!this.ᜀ())
		{
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
				return this.ᜀ(3).ᜑ();
			}
		}
		return CellFormatType.DateTime;
	}

	// Token: 0x060057F9 RID: 22521 RVA: 0x0037CF3C File Offset: 0x0037BF3C
	private new void ᜀ(IList<sprἏ> A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					int count;
					if (num2 >= count)
					{
						num = 8;
						continue;
					}
					sprἏ sprἏ = A_0[num2];
					num = 13;
					continue;
				}
				case 1:
					goto IL_B7;
				case 2:
					goto IL_170;
				case 3:
					goto IL_12D;
				case 5:
					goto IL_B7;
				case 6:
					if (base[0].ᜌ())
					{
						num = 14;
						continue;
					}
					num = 10;
					continue;
				case 7:
					if (base.Count > 3)
					{
						num = 2;
						continue;
					}
					goto IL_1CF;
				case 8:
				{
					List<sprἏ> list;
					base.InnerList.Add(new sprᨠ((spr\u2158)base.ReservedHandle, this, list));
					if (true)
					{
					}
					num = 6;
					continue;
				}
				case 9:
					goto IL_9E;
				case 10:
					if (base.Count > 4)
					{
						num = 3;
						continue;
					}
					return;
				case 11:
				{
					List<sprἏ> list;
					base.InnerList.Add(new sprᨠ(base.AppImplementation as spr\u2158, this, list));
					list = new List<sprἏ>();
					num = 15;
					continue;
				}
				case 12:
					goto IL_189;
				case 13:
				{
					sprἏ sprἏ;
					if (sprἏ.ᜀ() == TokenType.Section)
					{
						num = 11;
						continue;
					}
					List<sprἏ> list;
					list.Add(sprἏ);
					num = 12;
					continue;
				}
				case 14:
					num = 7;
					continue;
				case 15:
					goto IL_189;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9E;
				default:
				{
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					List<sprἏ> list = new List<sprἏ>();
					num2 = 0;
					int count = A_0.Count;
					num = 1;
					continue;
				}
				}
				IL_B7:
				num = 0;
				continue;
				IL_189:
				num2++;
				num = 5;
			}
			IL_9E:
			throw new ArgumentNullException(RecordTableEnumerator.b("伺刼吾⑀ⵂ㙄", a_));
			IL_12D:
			throw new FormatException(RecordTableEnumerator.b("漺䨼倾慀⹂⑄⥆え歊㹌⩎㉐❒㱔㡖㝘⡚絜㙞འ䍢ͤࡦ᭨٪౬᭮彰", a_));
			IL_170:
			throw new FormatException(RecordTableEnumerator.b("漺䨼倾慀⹂⑄⥆え歊㹌⩎㉐❒㱔㡖㝘⡚絜㙞འ䍢ͤࡦ᭨٪౬᭮彰", a_));
			IL_1CF:
			this.ᜇ = true;
			return;
		}
		}
	}

	// Token: 0x060057FA RID: 22522 RVA: 0x0037D1A0 File Offset: 0x0037C1A0
	public new string ᜀ(double A_0, bool A_1)
	{
		int a_ = 13;
		sprᨠ sprᨠ;
		for (;;)
		{
			sprᨠ = this.ᜀ(A_0);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5D;
					default:
						if (false)
						{
						}
						A_0 = -A_0;
						num = 7;
						continue;
					}
					break;
				case 1:
					num = 8;
					continue;
				case 2:
					if (!this.ᜇ)
					{
						num = 6;
						continue;
					}
					goto IL_C4;
				case 3:
					if (sprᨠ != null)
					{
						num = 5;
						continue;
					}
					goto IL_FC;
				case 4:
					if (A_0 < 0.0)
					{
						num = 1;
						continue;
					}
					goto IL_C4;
				case 5:
					goto IL_5D;
				case 6:
					if (true)
					{
					}
					num = 4;
					continue;
				case 7:
					goto IL_6B;
				case 8:
					if (base.Count > 1)
					{
						num = 0;
						continue;
					}
					goto IL_C4;
				}
				break;
				IL_5D:
				num = 2;
			}
		}
		IL_6B:
		IL_C4:
		return sprᨠ.ᜀ(A_0, A_1);
		IL_FC:
		throw new FormatException(RecordTableEnumerator.b("Ղ⩄㕆⑈⩊㥌潎≐㙒㙔⍖じ㑚㍜罞ɠɢ୤०٨Ὢ䵬൮ᑰ卲፴ᡶ౸ᕺ᥼兾", a_));
	}

	// Token: 0x060057FB RID: 22523 RVA: 0x0037D2BC File Offset: 0x0037C2BC
	public new string ᜀ(string A_0, bool A_1)
	{
		sprᨠ sprᨠ = this.ᜁ();
		if (sprᨠ == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return A_0;
		}
		IL_3C:
		return sprᨠ.ᜀ(A_0, A_1);
	}

	// Token: 0x060057FC RID: 22524 RVA: 0x0037D310 File Offset: 0x0037C310
	private new sprᨠ ᜀ(int A_0)
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
		return base[A_0 % base.Count];
	}

	// Token: 0x060057FD RID: 22525 RVA: 0x0037D35C File Offset: 0x0037C35C
	private new sprᨠ ᜀ(double A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_21E:
				num = 11;
				break;
			default:
				if (false)
				{
				}
				goto IL_91;
			}
			int num2;
			int count;
			sprᨠ result;
			for (;;)
			{
				IL_36:
				sprᨠ sprᨠ;
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					sprᨠ = base[num2];
					bool flag = sprᨠ.ᜌ();
					num = 12;
					continue;
				}
				case 1:
					num = 20;
					continue;
				case 2:
				{
					bool flag;
					if (flag)
					{
						num = 4;
						continue;
					}
					goto IL_1A6;
				}
				case 3:
					goto IL_CE;
				case 4:
					num = 16;
					continue;
				case 5:
					if (A_0 > 0.0)
					{
						num = 13;
						continue;
					}
					num = 9;
					continue;
				case 6:
					goto IL_22F;
				case 7:
					result = this.ᜀ(1);
					num = 18;
					continue;
				case 8:
					if (this.ᜇ)
					{
						num = 19;
						continue;
					}
					num = 5;
					continue;
				case 9:
					if (A_0 < 0.0)
					{
						num = 7;
						continue;
					}
					result = this.ᜂ();
					num = 14;
					continue;
				case 10:
					num = 2;
					continue;
				case 11:
					goto IL_CE;
				case 12:
				{
					bool flag;
					if (flag)
					{
						num = 10;
						continue;
					}
					goto IL_22F;
				}
				case 13:
					result = this.ᜀ(0);
					num = 15;
					continue;
				case 14:
					return result;
				case 15:
					return result;
				case 16:
					if (sprᨠ.ᜁ(A_0))
					{
						num = 6;
						continue;
					}
					goto IL_1A6;
				case 17:
					return result;
				case 18:
					return result;
				case 19:
					goto IL_B0;
				case 20:
					return result;
				}
				goto IL_91;
				IL_CE:
				num = 0;
				continue;
				IL_1A6:
				num2++;
				num = 3;
				continue;
				IL_22F:
				result = sprᨠ;
				num = 17;
			}
			IL_B0:
			count = base.Count;
			num2 = 0;
			goto IL_21E;
			IL_91:
			result = null;
			num = 8;
			goto IL_36;
		}
		}
	}

	// Token: 0x060057FE RID: 22526 RVA: 0x0037D5AC File Offset: 0x0037C5AC
	private new sprᨠ ᜂ()
	{
		int a_ = 5;
		int num = 8;
		sprᨠ sprᨠ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				sprᨠ = base[2];
				num = 9;
				continue;
			case 1:
			{
				int num2;
				if (num2 <= 2)
				{
					sprᨠ = base[2];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
				}
				num = 0;
				continue;
			}
			case 2:
				goto IL_C6;
			case 3:
				sprᨠ = base[0];
				num = 5;
				continue;
			case 4:
				if (sprᨠ.ᜑ() == CellFormatType.Text)
				{
					num = 10;
					continue;
				}
				return sprᨠ;
			case 5:
				return sprᨠ;
			case 6:
			{
				int num2;
				if (num2 < 2)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			}
			case 7:
				goto IL_55;
			case 9:
				goto IL_67;
			case 10:
				sprᨠ = base[0];
				num = 2;
				continue;
			}
			if (this.ᜇ)
			{
				num = 7;
			}
			else
			{
				int count = base.InnerList.Count;
				int num2 = count - 1;
				sprᨠ = null;
				num = 6;
			}
		}
		IL_55:
		throw new NotSupportedException(RecordTableEnumerator.b("漺唼嘾㉀捂⡄≆㵈⍊≌⭎煐㩒♔睖㝘㑚⥜罞በᙢᕤᝦ٨ᥪᥬ੮ᕰ卲፴ᡶ୸孺፼੾ꦈﶎﲐ릘즠莢욤좦잨쾪쒬\udbae\ud8b0\udcb2\udbb4쒶鞸", a_));
		IL_67:
		return sprᨠ;
		IL_C6:
		if (true)
		{
		}
		return sprᨠ;
	}

	// Token: 0x060057FF RID: 22527 RVA: 0x0037D704 File Offset: 0x0037C704
	private new sprᨠ ᜁ()
	{
		sprᨠ sprᨠ;
		for (;;)
		{
			sprᨠ = null;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 >= 3)
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
							num = 4;
							continue;
						}
					}
					else
					{
						sprᨠ = base[num2];
					}
					num = 7;
					continue;
				}
				case 1:
					if (!this.ᜇ)
					{
						num = 5;
						continue;
					}
					return sprᨠ;
				case 2:
					sprᨠ = base[0];
					num = 3;
					continue;
				case 3:
					return sprᨠ;
				case 4:
					sprᨠ = base[3];
					num = 6;
					continue;
				case 5:
				{
					if (true)
					{
					}
					int count = base.InnerList.Count;
					int num2 = count - 1;
					num = 0;
					continue;
				}
				case 6:
					return sprᨠ;
				case 7:
					if (sprᨠ.ᜑ() != CellFormatType.Text)
					{
						num = 2;
						continue;
					}
					return sprᨠ;
				}
				break;
			}
		}
		return sprᨠ;
	}

	// Token: 0x06005800 RID: 22528 RVA: 0x0037D804 File Offset: 0x0037C804
	internal new bool ᜁ(double A_0)
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
			if (A_0 >= 0.0)
			{
				return this.ᜀ(A_0).ᜋ();
			}
			break;
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06005801 RID: 22529 RVA: 0x0037D85C File Offset: 0x0037C85C
	private new bool ᜀ()
	{
		if (true)
		{
		}
		bool result;
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
			IEnumerator<sprᨠ> enumerator = base.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						sprᨠ sprᨠ = enumerator.Current;
						num = 1;
						continue;
					}
					case 1:
					{
						sprᨠ sprᨠ;
						if (sprᨠ.ᜑ() == CellFormatType.DateTime)
						{
							num = 5;
							continue;
						}
						break;
					}
					case 2:
						num = 3;
						continue;
					case 3:
						goto IL_B7;
					case 5:
						result = true;
						num = 6;
						continue;
					case 6:
						goto IL_AD;
					}
					IL_67:
					num = 0;
					continue;
					goto IL_67;
				}
				IL_AD:
				break;
				IL_B7:
				return false;
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_F3;
					case 2:
						enumerator.Dispose();
						num = 1;
						continue;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 2;
				}
				IL_F3:;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x06005802 RID: 22530 RVA: 0x0037D970 File Offset: 0x0037C970
	public virtual object ᜀ(object A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_42;
				default:
					goto IL_42;
				}
				IL_67:
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				spr\u2575 spr_u = new spr\u2575((spr\u2158)base.ReservedHandle, A_0);
				List<sprᨠ> innerList = base.InnerList;
				List<sprᨠ> innerList2 = spr_u.InnerList;
				int num2 = 0;
				int count = base.Count;
				num = 4;
				continue;
				goto IL_67;
				IL_42:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					return spr_u;
				case 2:
					goto IL_FB;
				case 3:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					sprᨠ sprᨠ = innerList[num2];
					sprᨠ = (sprᨠ)sprᨠ.ᜀ(spr_u);
					innerList2.Add(sprᨠ);
					num2++;
					num = 2;
					continue;
				}
				case 4:
					goto IL_FB;
				case 5:
					goto IL_7B;
				}
				goto IL_67;
				IL_FB:
				num = 3;
			}
			IL_7B:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉁╃㑅ⵇ⑉㡋", a_));
		}
		}
	}

	// Token: 0x040029D4 RID: 10708
	private new const string ᜀ = "Two many sections in format.";

	// Token: 0x040029D5 RID: 10709
	private new const int ᜁ = 3;

	// Token: 0x040029D6 RID: 10710
	private new const int ᜂ = 4;

	// Token: 0x040029D7 RID: 10711
	private const int ᜃ = 0;

	// Token: 0x040029D8 RID: 10712
	private const int ᜄ = 1;

	// Token: 0x040029D9 RID: 10713
	private const int ᜅ = 2;

	// Token: 0x040029DA RID: 10714
	private const int ᜆ = 3;

	// Token: 0x040029DB RID: 10715
	private bool ᜇ;
}
