using System;
using System.Drawing;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000336 RID: 822
[spr\u2400(FormulaToken.tRefErr3d3)]
[spr\u2400(FormulaToken.tRefErr3d2)]
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tRefErr3d1)]
internal class spr\u1B37 : sprᣋ, spr\u2086, sprỜ
{
	// Token: 0x06003265 RID: 12901 RVA: 0x001D0E84 File Offset: 0x001CFE84
	public spr\u1B37()
	{
	}

	// Token: 0x06003266 RID: 12902 RVA: 0x001D0E98 File Offset: 0x001CFE98
	public spr\u1B37(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003267 RID: 12903 RVA: 0x001D0EB0 File Offset: 0x001CFEB0
	public spr\u1B37(string A_0, IWorkbook A_1) : base(A_0, A_1)
	{
		this.TokenCode = FormulaToken.tRefErr3d1;
	}

	// Token: 0x06003268 RID: 12904 RVA: 0x001D0ED0 File Offset: 0x001CFED0
	public spr\u1B37(sprᣋ A_0) : base(A_0)
	{
	}

	// Token: 0x06003269 RID: 12905 RVA: 0x001D0EE4 File Offset: 0x001CFEE4
	public override string ᜀ()
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("ᩇ⽉⩋୍≏⁑杓㉕硗牙", a_) + base.ᜁ().ToString() + base.ᜀ() + RecordTableEnumerator.b("慇", a_);
	}

	// Token: 0x0600326A RID: 12906 RVA: 0x001D0F60 File Offset: 0x001CFF60
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 9;
		string text;
		for (;;)
		{
			IL_21:
			text = sprᣋ.ᜀ(A_0.ParentWorkbook, (int)base.ᜁ());
			for (;;)
			{
				IL_33:
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9B;
					case 1:
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							if (false)
							{
							}
							if (text == null)
							{
								num = 0;
								continue;
							}
							goto IL_9D;
						}
						break;
					case 3:
						if (true)
						{
						}
						if (A_0 != null)
						{
							num = 1;
							continue;
						}
						goto IL_5A;
					}
					goto IL_21;
				}
			}
		}
		IL_5A:
		return RecordTableEnumerator.b("᰾ፀق̈́晆", a_);
		IL_9B:
		goto IL_5A;
		IL_9D:
		return string.Format(RecordTableEnumerator.b("ᠾ㩀獂㡄恆案お籌㉎", a_), text, RecordTableEnumerator.b("᰾ፀق̈́晆", a_));
	}

	// Token: 0x0600326B RID: 12907 RVA: 0x001D102C File Offset: 0x001D002C
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

	// Token: 0x0600326C RID: 12908 RVA: 0x001D106C File Offset: 0x001D006C
	public new static FormulaToken ᜀ(int A_0)
	{
		int a_ = 18;
		for (;;)
		{
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (A_0)
						{
						case 1:
							return FormulaToken.tRefErr3d1;
						case 2:
							return FormulaToken.tRefErr3d2;
						case 3:
							return FormulaToken.tRefErr3d3;
						default:
							num = 1;
							continue;
						}
						break;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_79;
					}
					break;
				}
			}
		}
		return FormulaToken.tRefErr3d2;
		IL_79:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ⑉⡋⭍⡏", a_));
	}

	// Token: 0x0600326D RID: 12909 RVA: 0x001D1114 File Offset: 0x001D0114
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
}
