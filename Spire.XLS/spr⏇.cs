using System;
using System.Drawing;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000429 RID: 1065
[spr\u2400(FormulaToken.tRefErr3)]
[spr\u2400(FormulaToken.tRefErr2)]
[spr\u2400(FormulaToken.tRefErr1)]
[spr\u1CD7("#REF!", 23)]
internal class spr\u23C7 : sprᦊ, sprỜ
{
	// Token: 0x06004087 RID: 16519 RVA: 0x002438D8 File Offset: 0x002428D8
	static spr\u23C7()
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
	}

	// Token: 0x06004088 RID: 16520 RVA: 0x00243914 File Offset: 0x00242914
	public spr\u23C7()
	{
	}

	// Token: 0x06004089 RID: 16521 RVA: 0x00243928 File Offset: 0x00242928
	public spr\u23C7(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600408A RID: 16522 RVA: 0x00243940 File Offset: 0x00242940
	public spr\u23C7(string A_0)
	{
		int a_ = 18;
		base..ctor(RecordTableEnumerator.b("े等", a_));
		this.TokenCode = FormulaToken.tRefErr2;
	}

	// Token: 0x0600408B RID: 16523 RVA: 0x00243974 File Offset: 0x00242974
	public spr\u23C7(string A_0, IWorkbook A_1) : this(A_0)
	{
	}

	// Token: 0x0600408C RID: 16524 RVA: 0x00243988 File Offset: 0x00242988
	public spr\u23C7(sprᦊ A_0) : base(A_0)
	{
		int a_ = sprᦊ.ᜀ(A_0.TokenCode);
		this.TokenCode = spr\u23C7.ᜀ(a_);
	}

	// Token: 0x0600408D RID: 16525 RVA: 0x002439B4 File Offset: 0x002429B4
	public override string ᜀ()
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("၁⅃⁅േ㡉㹋湍硏", a_) + base.ᜀ() + RecordTableEnumerator.b("歁", a_);
	}

	// Token: 0x0600408E RID: 16526 RVA: 0x00243A20 File Offset: 0x00242A20
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return RecordTableEnumerator.b("所ᅂDņ案", a_);
	}

	// Token: 0x0600408F RID: 16527 RVA: 0x00243A74 File Offset: 0x00242A74
	public override Ptg ᜀ(int A_0, int A_1, int A_2, int A_3, Rectangle A_4, int A_5, Rectangle A_6, out bool A_7, XlsWorkbook A_8)
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
		A_7 = false;
		return this;
	}

	// Token: 0x06004090 RID: 16528 RVA: 0x00243AB4 File Offset: 0x00242AB4
	public new static FormulaToken ᜀ(int A_0)
	{
		int a_ = 8;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_81;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
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
					break;
				case 2:
					switch (A_0)
					{
					case 1:
						return FormulaToken.tRefErr1;
					case 2:
						return FormulaToken.tRefErr2;
					case 3:
						return FormulaToken.tRefErr3;
					}
					goto IL_45;
				}
				break;
				IL_45:
				num = 1;
			}
		}
		return FormulaToken.tRefErr2;
		IL_81:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽⸿♁⅃㹅", a_));
	}

	// Token: 0x06004091 RID: 16529 RVA: 0x00243B5C File Offset: 0x00242B5C
	public override Ptg ᜀ(IWorkbook A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 9;
			spr\u23C7 spr_u23C;
			for (;;)
			{
				int a_;
				int a_2;
				int num2;
				int num3;
				switch (num)
				{
				case 0:
				{
					if (A_0.Version == ExcelVersion.Version97to2003)
					{
						num = 4;
						continue;
					}
					sprᦊ sprᦊ = spr_u23C;
					sprᦊ.ᜂ(a_);
					sprᦊ.ᜃ(a_2);
					if (true)
					{
					}
					num = 8;
					continue;
				}
				case 1:
					num2 = this.ᜇ();
					goto IL_14E;
				case 2:
					num3 = this.ᜆ();
					goto IL_115;
				case 3:
					goto IL_DE;
				case 4:
					spr_u23C.ᜂ(a_);
					spr_u23C.ᜃ(a_2);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_66;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 5:
					num = 2;
					continue;
				case 6:
					if (!this.ᜃ())
					{
						num = 11;
						continue;
					}
					num = 7;
					continue;
				case 7:
					num2 = this.ᜇ() - A_1;
					goto IL_14E;
				case 8:
					goto IL_A3;
				case 10:
					num3 = this.ᜆ() - A_2;
					goto IL_115;
				case 11:
					num = 1;
					continue;
				}
				if (!this.ᜅ())
				{
					num = 5;
					continue;
				}
				IL_66:
				num = 10;
				continue;
				IL_115:
				a_2 = num3;
				num = 6;
				continue;
				IL_14E:
				a_ = num2;
				FormulaToken a_3 = spr\u23C7.ᜀ(this.ᜄ());
				spr_u23C = (spr\u23C7)FormulaUtil.ᜁ(a_3);
				num = 0;
			}
			IL_A3:
			IL_DE:
			spr_u23C.ᜀ(base.ᜊ());
			return spr_u23C;
		}
		}
	}

	// Token: 0x06004092 RID: 16530 RVA: 0x00243D08 File Offset: 0x00242D08
	public new IXLSRange ᜀ(IWorkbook A_0, IWorksheet A_1)
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
		return null;
	}

	// Token: 0x04001CDC RID: 7388
	public new const string ᜀ = "#REF!";
}
