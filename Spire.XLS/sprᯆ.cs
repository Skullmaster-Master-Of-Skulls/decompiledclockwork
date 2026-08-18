using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000481 RID: 1153
[spr\u2400(FormulaToken.tMissingArgument)]
internal class sprᯆ : Ptg
{
	// Token: 0x060046C4 RID: 18116 RVA: 0x002AE5EC File Offset: 0x002AD5EC
	public sprᯆ()
	{
		this.TokenCode = FormulaToken.tMissingArgument;
	}

	// Token: 0x060046C5 RID: 18117 RVA: 0x002AE608 File Offset: 0x002AD608
	public sprᯆ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060046C6 RID: 18118 RVA: 0x002AE620 File Offset: 0x002AD620
	public sprᯆ(string A_0)
	{
		int a_ = 4;
		base..ctor();
		if (A_0 != string.Empty)
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤹䠻䰽ؿⵁ㙃⭅㵇♉ⵋ", a_), RecordTableEnumerator.b("䤹吻儽㔿⹁⁃晅⩇⽉汋⭍㵏≑⁓⽕硗⥙⡛ⱝय़ౡͣ", a_));
		}
		this.TokenCode = FormulaToken.tMissingArgument;
	}

	// Token: 0x060046C7 RID: 18119 RVA: 0x002AE678 File Offset: 0x002AD678
	public virtual int ᜀ(ExcelVersion A_0)
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

	// Token: 0x060046C8 RID: 18120 RVA: 0x002AE6B4 File Offset: 0x002AD6B4
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
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
		return "";
	}
}
