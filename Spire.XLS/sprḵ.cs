using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x0200052F RID: 1327
internal class sprḵ : spr\u25AD, spr᥌
{
	// Token: 0x06005117 RID: 20759 RVA: 0x0032D068 File Offset: 0x0032C068
	public sprḵ() : base(TObjSubRecordType.ftSbsFormula)
	{
	}

	// Token: 0x06005118 RID: 20760 RVA: 0x0032D080 File Offset: 0x0032C080
	public sprḵ(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06005119 RID: 20761 RVA: 0x0032D098 File Offset: 0x0032C098
	public new Ptg[] ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x0600511A RID: 20762 RVA: 0x0032D0DC File Offset: 0x0032C0DC
	public new void ᜀ(Ptg[] A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x0600511B RID: 20763 RVA: 0x0032D120 File Offset: 0x0032C120
	protected override void ᜀ(byte[] A_0)
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
		int num = 0;
		int a_ = (int)BitConverter.ToInt16(A_0, num);
		num += 2;
		BitConverter.ToInt32(A_0, num);
		num += 4;
		spr\u24E5 a_2 = new spr\u24E5(A_0);
		int num2;
		this.ᜂ = FormulaUtil.ᜀ(a_2, num, a_, out num2, ExcelVersion.Version97to2003);
	}

	// Token: 0x0600511C RID: 20764 RVA: 0x0032D190 File Offset: 0x0032C190
	protected override void ᜁ(DataProvider A_0, int A_1)
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
		byte[] array = FormulaUtil.ᜀ(this.ᜂ, ExcelVersion.Version97to2003);
		int num = array.Length;
		A_0.WriteInt16(A_1, (short)num);
		A_1 += 2;
		A_0.WriteInt32(A_1, 0);
		A_1 += 4;
		A_0.WriteBytes(A_1, array);
		A_1 += num;
		A_0.WriteByte(A_1, 0);
	}

	// Token: 0x0600511D RID: 20765 RVA: 0x0032D20C File Offset: 0x0032C20C
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
		return sprᡣ.ᜀ(this.ᜂ, A_0, true) + 4 + 2 + 4 + 1;
	}

	// Token: 0x0400243F RID: 9279
	private new const int ᜀ = 7;

	// Token: 0x04002440 RID: 9280
	private new const int ᜁ = 9;

	// Token: 0x04002441 RID: 9281
	private Ptg[] ᜂ;
}
