using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002B6 RID: 694
[spr\u2400(FormulaToken.tMemErr1)]
[spr\u2400(FormulaToken.tMemErr2)]
[spr\u2400(FormulaToken.tMemErr3)]
internal class spr\u234E : Ptg
{
	// Token: 0x06002A0A RID: 10762 RVA: 0x00179D38 File Offset: 0x00178D38
	public spr\u234E()
	{
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x00179D58 File Offset: 0x00178D58
	public spr\u234E(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06002A0C RID: 10764 RVA: 0x00179D7C File Offset: 0x00178D7C
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 6;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("ᐻ猽┿⽁Ń㑅㩇橉≋⅍⑏牑㵓㭕⡗㙙㥛㍝՟ౡၣͥ౧䑩䕫", a_);
	}

	// Token: 0x06002A0D RID: 10765 RVA: 0x00179DD0 File Offset: 0x00178DD0
	public virtual byte[] ᜀ(ExcelVersion A_0)
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
		byte[] array = base.ToByteArray(A_0);
		this.ᜁ.CopyTo(array, 1);
		return array;
	}

	// Token: 0x06002A0E RID: 10766 RVA: 0x00179E24 File Offset: 0x00178E24
	public virtual int ᜁ(ExcelVersion A_0)
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
		return 7;
	}

	// Token: 0x06002A0F RID: 10767 RVA: 0x00179E60 File Offset: 0x00178E60
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
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
		A_0.CopyTo(A_1, this.ᜁ, 0, this.ᜁ.Length);
		A_1 += this.ᜁ.Length;
	}

	// Token: 0x040013F2 RID: 5106
	private const int ᜀ = 7;

	// Token: 0x040013F3 RID: 5107
	private byte[] ᜁ = new byte[6];
}
