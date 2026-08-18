using System;
using System.Drawing;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005A2 RID: 1442
[spr\u2400(FormulaToken.tAreaErr3)]
[spr\u2400(FormulaToken.tAreaErr1)]
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tAreaErr2)]
internal class spr\u255F : sprᲔ, sprỜ
{
	// Token: 0x06005767 RID: 22375 RVA: 0x00379928 File Offset: 0x00378928
	public spr\u255F()
	{
	}

	// Token: 0x06005768 RID: 22376 RVA: 0x0037993C File Offset: 0x0037893C
	public spr\u255F(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06005769 RID: 22377 RVA: 0x00379954 File Offset: 0x00378954
	public spr\u255F(sprᲔ A_0) : base(A_0)
	{
		this.TokenCode = A_0.TokenCode - 37 + 43;
	}

	// Token: 0x0600576A RID: 22378 RVA: 0x0037997C File Offset: 0x0037897C
	public spr\u255F(string A_0, IWorkbook A_1) : base(A_0, A_1)
	{
		this.TokenCode = FormulaToken.tAreaErr1;
	}

	// Token: 0x0600576B RID: 22379 RVA: 0x0037999C File Offset: 0x0037899C
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 1;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("ᐶ欸縺笼Ḿ", a_);
	}

	// Token: 0x0600576C RID: 22380 RVA: 0x003799F0 File Offset: 0x003789F0
	public override int ᜀ()
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
		return spr\u255F.ᜀ(this.TokenCode);
	}

	// Token: 0x0600576D RID: 22381 RVA: 0x00379A38 File Offset: 0x00378A38
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
		return (Ptg)base.Clone();
	}

	// Token: 0x0600576E RID: 22382 RVA: 0x00379A84 File Offset: 0x00378A84
	public new static FormulaToken ᜀ(int A_0)
	{
		int a_ = 17;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_79;
				case 1:
					num = 0;
					continue;
				case 2:
					switch (A_0)
					{
					case 1:
						return FormulaToken.tAreaErr1;
					case 2:
						return FormulaToken.tAreaErr2;
					case 3:
						return FormulaToken.tAreaErr3;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
		}
		return FormulaToken.tAreaErr2;
		IL_79:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆❈⽊⡌㝎", a_));
	}

	// Token: 0x0600576F RID: 22383 RVA: 0x00379B2C File Offset: 0x00378B2C
	public new static int ᜀ(FormulaToken A_0)
	{
		int a_ = 1;
		for (;;)
		{
			IL_2D:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6E:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 5;
					continue;
				case 1:
					if (A_0 != FormulaToken.tAreaErr1)
					{
						num = 2;
						continue;
					}
					return 1;
				case 2:
					goto IL_60;
				case 3:
					if (A_0 != FormulaToken.tAreaErr2)
					{
						num = 0;
						continue;
					}
					return 2;
				case 4:
					goto IL_6C;
				case 5:
					if (A_0 != FormulaToken.tAreaErr3)
					{
						num = 6;
						continue;
					}
					return 3;
				case 6:
					num = 4;
					continue;
				}
				goto IL_2D;
			}
			IL_60:
			goto IL_6E;
		}
		return 2;
		IL_6C:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶圸强堼䜾", a_));
	}

	// Token: 0x06005770 RID: 22384 RVA: 0x00379C04 File Offset: 0x00378C04
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
