using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x020004E0 RID: 1248
internal class spr᧗ : spr\u25AD, spr᥌
{
	// Token: 0x06004CA3 RID: 19619 RVA: 0x002ED19C File Offset: 0x002EC19C
	public spr᧗() : base(TObjSubRecordType.ftCblsFmla)
	{
	}

	// Token: 0x06004CA4 RID: 19620 RVA: 0x002ED1B4 File Offset: 0x002EC1B4
	public spr᧗(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004CA5 RID: 19621 RVA: 0x002ED1CC File Offset: 0x002EC1CC
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
		return this.ᜀ;
	}

	// Token: 0x06004CA6 RID: 19622 RVA: 0x002ED210 File Offset: 0x002EC210
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
		this.ᜀ = A_0;
	}

	// Token: 0x06004CA7 RID: 19623 RVA: 0x002ED254 File Offset: 0x002EC254
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
		this.ᜀ = FormulaUtil.ᜀ(a_2, num, a_, out num2, ExcelVersion.Version97to2003);
	}

	// Token: 0x06004CA8 RID: 19624 RVA: 0x002ED2C4 File Offset: 0x002EC2C4
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
		byte[] array = FormulaUtil.ᜀ(this.ᜀ, ExcelVersion.Version97to2003);
		int num = array.Length;
		A_0.WriteInt16(A_1, (short)num);
		A_1 += 2;
		A_0.WriteInt32(A_1, 0);
		A_1 += 4;
		A_0.WriteBytes(A_1, array);
		A_1 += num;
		A_0.WriteByte(A_1, 0);
	}

	// Token: 0x06004CA9 RID: 19625 RVA: 0x002ED340 File Offset: 0x002EC340
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
		return sprᡣ.ᜀ(this.ᜀ, A_0, true) + 4 + 2 + 4 + 1;
	}

	// Token: 0x040022E7 RID: 8935
	private new Ptg[] ᜀ;
}
