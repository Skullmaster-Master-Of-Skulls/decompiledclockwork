using System;
using System.Drawing;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000486 RID: 1158
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tAreaErr3d2)]
[spr\u2400(FormulaToken.tAreaErr3d3)]
[spr\u2400(FormulaToken.tAreaErr3d1)]
internal class sprខ : spr\u1BFD, sprỜ
{
	// Token: 0x0600473B RID: 18235 RVA: 0x002B3E5C File Offset: 0x002B2E5C
	public sprខ()
	{
	}

	// Token: 0x0600473C RID: 18236 RVA: 0x002B3E70 File Offset: 0x002B2E70
	public sprខ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600473D RID: 18237 RVA: 0x002B3E88 File Offset: 0x002B2E88
	public sprខ(spr\u1BFD A_0) : base(A_0)
	{
		this.TokenCode = A_0.TokenCode - 59 + 61;
	}

	// Token: 0x0600473E RID: 18238 RVA: 0x002B3EB0 File Offset: 0x002B2EB0
	public sprខ(string A_0, IWorkbook A_1) : base(A_0, A_1)
	{
		this.TokenCode = FormulaToken.tAreaErr3d1;
	}

	// Token: 0x0600473F RID: 18239 RVA: 0x002B3ED0 File Offset: 0x002B2ED0
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 4;
		int num = 2;
		string arg;
		for (;;)
		{
			IWorkbook workbook;
			IWorkbook workbook2;
			switch (num)
			{
			case 0:
				if (workbook == null)
				{
					num = 4;
					continue;
				}
				goto IL_C2;
			case 1:
				if (true)
				{
				}
				num = 5;
				continue;
			case 3:
				IL_BA:
				workbook2 = A_0.ParentWorkbook;
				goto IL_6A;
			case 4:
				goto IL_AD;
			case 5:
				workbook2 = null;
				goto IL_6A;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 3;
			continue;
			IL_6A:
			workbook = workbook2;
			arg = sprᣋ.ᜀ(workbook, (int)base.ᜆ());
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_BA;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
		}
		IL_AD:
		return RecordTableEnumerator.b("᤹渻笽ؿ捁", a_);
		IL_C2:
		return string.Format(RecordTableEnumerator.b("ᴹ䜻฽㴿敁敃㵅祇㝉", a_), arg, RecordTableEnumerator.b("᤹渻笽ؿ捁", a_));
	}

	// Token: 0x06004740 RID: 18240 RVA: 0x002B3FC4 File Offset: 0x002B2FC4
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
		return sprខ.ᜀ(this.TokenCode);
	}

	// Token: 0x06004741 RID: 18241 RVA: 0x002B400C File Offset: 0x002B300C
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

	// Token: 0x06004742 RID: 18242 RVA: 0x002B4058 File Offset: 0x002B3058
	public new static FormulaToken ᜀ(int A_0)
	{
		int a_ = 6;
		for (;;)
		{
			IL_1D:
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_53;
					case 1:
						num = 0;
						continue;
					case 2:
						switch (A_0)
						{
						case 1:
							return FormulaToken.tAreaErr3d1;
						case 2:
							return FormulaToken.tAreaErr3d2;
						case 3:
							goto IL_5F;
						default:
							num = 1;
							continue;
						}
						break;
					}
					goto IL_1D;
				}
				IL_5F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_75;
				}
			}
		}
		return FormulaToken.tAreaErr3d2;
		IL_53:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唻倽␿❁㱃", a_), RecordTableEnumerator.b("焻䬽㌿㙁摃⑅ⵇ橉⁋⭍⍏⅑瑓≕し㭙㉛繝呟䉡գࡥ౧䩩୫ᱭᕯ፱s፵੷婹ࡻᙽꒃ꺍ꂏ벑", a_));
		IL_75:
		if (true)
		{
		}
		if (false)
		{
		}
		return FormulaToken.tAreaErr3d3;
	}

	// Token: 0x06004743 RID: 18243 RVA: 0x002B410C File Offset: 0x002B310C
	public new static int ᜀ(FormulaToken A_0)
	{
		int a_ = 4;
		for (;;)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5A;
				case 1:
					num = 2;
					continue;
				case 2:
					if (A_0 != FormulaToken.tAreaErr3d2)
					{
						num = 3;
						continue;
					}
					return 2;
				case 3:
					num = 4;
					continue;
				case 4:
					if (A_0 != FormulaToken.tAreaErr3d3)
					{
						num = 5;
						continue;
					}
					goto IL_73;
				case 5:
					num = 0;
					continue;
				case 6:
					if (A_0 != FormulaToken.tAreaErr3d1)
					{
						num = 1;
						continue;
					}
					return 1;
				}
				break;
			}
		}
		return 2;
		IL_5A:
		goto IL_B8;
		IL_73:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_B8:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匹刻娽┿㩁", a_));
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			return 3;
		}
		return 1;
	}

	// Token: 0x06004744 RID: 18244 RVA: 0x002B41E4 File Offset: 0x002B31E4
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
