using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002E8 RID: 744
[spr\u2400(FormulaToken.tMul, "*")]
[spr\u2400(FormulaToken.tPower, "^")]
[spr\u2400(FormulaToken.tLessThan, "<")]
[spr\u2400(FormulaToken.tConcat, "&")]
[spr\u2400(FormulaToken.tGreaterEqual, ">=")]
[spr\u2400(FormulaToken.tAdd, "+")]
[spr\u2400(FormulaToken.tDiv, "/")]
[spr\u2400(FormulaToken.tEqual, "=")]
[spr\u2400(FormulaToken.tSub, "-")]
[spr\u2400(FormulaToken.tNotEqual, "<>")]
[spr\u2400(FormulaToken.tLessEqual, "<=")]
[spr\u2400(FormulaToken.tGreater, ">")]
[spr\u2400(FormulaToken.tCellRangeIntersection, " ")]
[spr\u2400(FormulaToken.tCellRange, ":")]
internal class spr᱃ : sprឯ
{
	// Token: 0x06002E3B RID: 11835 RVA: 0x0019F8E0 File Offset: 0x0019E8E0
	static spr᱃()
	{
		int a_ = 1;
		switch (0)
		{
		default:
			for (;;)
			{
				spr᱃.ᜀ = new Dictionary<string, FormulaToken>(16);
				spr᱃.ᜁ = new Dictionary<FormulaToken, string>(16);
				spr᱃.ᜂ = new spr\u2400[]
				{
					new spr\u2400(FormulaToken.tAdd, RecordTableEnumerator.b("ᰶ", a_)),
					new spr\u2400(FormulaToken.tDiv, RecordTableEnumerator.b("ᠶ", a_)),
					new spr\u2400(FormulaToken.tMul, RecordTableEnumerator.b("ᴶ", a_)),
					new spr\u2400(FormulaToken.tSub, RecordTableEnumerator.b("ᨶ", a_)),
					new spr\u2400(FormulaToken.tPower, RecordTableEnumerator.b("椶", a_)),
					new spr\u2400(FormulaToken.tConcat, RecordTableEnumerator.b("ᄶ", a_)),
					new spr\u2400(FormulaToken.tLessThan, RecordTableEnumerator.b("ଶ", a_)),
					new spr\u2400(FormulaToken.tLessEqual, RecordTableEnumerator.b("ଶи", a_)),
					new spr\u2400(FormulaToken.tEqual, RecordTableEnumerator.b("ਸ਼", a_)),
					new spr\u2400(FormulaToken.tNotEqual, RecordTableEnumerator.b("ଶܸ", a_)),
					new spr\u2400(FormulaToken.tGreater, RecordTableEnumerator.b("श", a_)),
					new spr\u2400(FormulaToken.tGreaterEqual, RecordTableEnumerator.b("शи", a_)),
					new spr\u2400(FormulaToken.tCellRangeIntersection, RecordTableEnumerator.b("᜶", a_)),
					new spr\u2400(FormulaToken.tCellRange, RecordTableEnumerator.b("ശ", a_))
				};
				int num = 0;
				int num2 = spr᱃.ᜂ.Length;
				if (true)
				{
				}
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (num < num2)
						{
							spr\u2400 spr_u = spr᱃.ᜂ[num];
							FormulaToken formulaToken = spr_u.ᜀ();
							string text = spr_u.ᜂ();
							spr᱃.ᜀ.Add(text, formulaToken);
							spr᱃.ᜁ.Add(formulaToken, text);
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num3 = 1;
								continue;
							}
						}
						num3 = 3;
						continue;
					case 1:
						goto IL_1D5;
					case 2:
						goto IL_1D5;
					case 3:
						return;
					}
					break;
					IL_1D5:
					num3 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06002E3C RID: 11836 RVA: 0x0019FB4C File Offset: 0x0019EB4C
	public static FormulaToken ᜀ(string A_0)
	{
		int a_ = 11;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_90;
				case 2:
					if (A_0.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_A6;
				case 3:
					goto IL_3C;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 3;
				}
				else
				{
					num = 2;
				}
			}
			IL_3C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_52;
			}
		}
		IL_52:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⹀㍂⁄㕆⡈㽊⑌⁎㽐R㱔ざ㝘", a_));
		IL_90:
		throw new ArgumentException(RecordTableEnumerator.b("⹀㍂⁄㕆⡈㽊⑌⁎㽐R㱔ざ㝘筚灜罞በᝢᝤ๦ݨ౪䵬౮ၰᵲ᭴ᡶ൸孺ὼ᩾ꆀﶈ", a_));
		IL_A6:
		return spr᱃.ᜀ[A_0];
	}

	// Token: 0x06002E3D RID: 11837 RVA: 0x0019FC0C File Offset: 0x0019EC0C
	public static string ᜀ(FormulaToken A_0)
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
		return spr᱃.ᜁ[A_0];
	}

	// Token: 0x06002E3E RID: 11838 RVA: 0x0019FC54 File Offset: 0x0019EC54
	public spr᱃()
	{
	}

	// Token: 0x06002E3F RID: 11839 RVA: 0x0019FC68 File Offset: 0x0019EC68
	public spr᱃(string A_0)
	{
		int a_ = 17;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("⡆㥈⹊㽌⹎═㩒㩔㥖", a_));
		}
		if (A_0.Length == 0)
		{
			throw new ArgumentException(RecordTableEnumerator.b("⡆㥈⹊㽌⹎═㩒㩔㥖祘癚絜ⱞᕠᅢ౤०๨䭪๬๮ὰᵲᩴͶ奸᥺᡼彾", a_));
		}
		if (!spr᱃.ᜀ.ContainsKey(A_0))
		{
			throw new ArgumentException(RecordTableEnumerator.b("⡆㥈⹊㽌⹎═㩒㩔㥖", a_), RecordTableEnumerator.b("ቆ❈⁊⍌⁎♐㵒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ᡪᑬɮ፰ᱲᥴ", a_));
		}
		base.ᜁ(A_0);
		this.TokenCode = spr᱃.ᜀ(A_0);
	}

	// Token: 0x06002E40 RID: 11840 RVA: 0x0019FD00 File Offset: 0x0019ED00
	public spr᱃(FormulaToken A_0)
	{
		int a_ = 8;
		base..ctor();
		if (!spr᱃.ᜁ.ContainsKey(A_0))
		{
			throw new ArgumentException(RecordTableEnumerator.b("儽〿❁㙃❅㱇⍉⍋⁍", a_), RecordTableEnumerator.b("欽⸿⥁⩃⥅㽇⑉汋⅍⁏㝑♓㝕ⱗ㍙㍛そ䁟ᅡᵣ୥੧թk", a_));
		}
		base.ᜁ(spr᱃.ᜀ(A_0));
		this.TokenCode = A_0;
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x0019FD60 File Offset: 0x0019ED60
	public spr᱃(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x0019FD78 File Offset: 0x0019ED78
	public override int ᜃ()
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
		return 2;
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x0019FDB4 File Offset: 0x0019EDB4
	public override TOperation ᜂ()
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
		return TOperation.TYPE_BINARY;
	}

	// Token: 0x06002E44 RID: 11844 RVA: 0x0019FDF0 File Offset: 0x0019EDF0
	protected override spr\u2400[] ᜀ()
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
		return spr᱃.ᜂ;
	}

	// Token: 0x06002E45 RID: 11845 RVA: 0x0019FE30 File Offset: 0x0019EE30
	public override void ᜀ(FormulaUtil A_0, Stack<object> A_1, bool A_2)
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
		FormulaUtil.PushOperandToStack(A_1, this.ToString(A_0));
		string str = (string)A_1.Pop();
		string str2 = (string)A_1.Pop();
		string str3 = (string)A_1.Pop();
		A_1.Push(str3 + str + str2);
	}

	// Token: 0x06002E46 RID: 11846 RVA: 0x0019FEAC File Offset: 0x0019EEAC
	public override string[] ᜀ(string A_0, ref int A_1, FormulaUtil A_2)
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
		A_1 += base.\u1712().Length;
		string rightBinaryOperand = A_2.GetRightBinaryOperand(A_0, A_1, this.ToString());
		A_1 += rightBinaryOperand.Length;
		return new string[]
		{
			rightBinaryOperand
		};
	}

	// Token: 0x06002E47 RID: 11847 RVA: 0x0019FF20 File Offset: 0x0019EF20
	public override int ᜀ(ExcelVersion A_0)
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
		return 1;
	}

	// Token: 0x040014DD RID: 5341
	private new static readonly Dictionary<string, FormulaToken> ᜀ;

	// Token: 0x040014DE RID: 5342
	private static readonly Dictionary<FormulaToken, string> ᜁ;

	// Token: 0x040014DF RID: 5343
	private static readonly spr\u2400[] ᜂ;
}
