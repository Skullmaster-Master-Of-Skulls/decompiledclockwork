using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005A1 RID: 1441
[spr\u2400(FormulaToken.tMemFunc3)]
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tMemFunc1)]
[spr\u2400(FormulaToken.tMemFunc2)]
internal class spr\u1DFC : Ptg
{
	// Token: 0x0600575E RID: 22366 RVA: 0x003796A8 File Offset: 0x003786A8
	public spr\u1DFC()
	{
		this.ᜂ = new byte[2];
		base..ctor();
	}

	// Token: 0x0600575F RID: 22367 RVA: 0x003796C8 File Offset: 0x003786C8
	public spr\u1DFC(int A_0)
	{
		int a_ = 11;
		this.ᜂ = new byte[2];
		base..ctor();
		if (A_0 >= 0)
		{
			if (A_0 <= 65535)
			{
				this.TokenCode = FormulaToken.tMemFunc1;
				this.ᜁ = (ushort)A_0;
				return;
			}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉀⩂㽄≆", a_), RecordTableEnumerator.b("ᝀ≂⥄㉆ⱈ歊⹌⹎㽐㵒㩔⍖祘㥚㡜罞ൠ٢ᙤᑦ䥨Ὢլ๮ὰ卲䕴坶ᙸॺ嵼᡾力권ﮎ戀ﮔ랖펠힢认좨펪ﮬ캮\uddb0욲킴", a_));
	}

	// Token: 0x06005760 RID: 22368 RVA: 0x00379734 File Offset: 0x00378734
	public spr\u1DFC(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		this.ᜂ = new byte[2];
		base..ctor(A_0, A_1, A_2);
	}

	// Token: 0x06005761 RID: 22369 RVA: 0x00379758 File Offset: 0x00378758
	public ushort ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x06005762 RID: 22370 RVA: 0x0037979C File Offset: 0x0037879C
	public void ᜀ(ushort A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06005763 RID: 22371 RVA: 0x003797E0 File Offset: 0x003787E0
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
		return 3;
	}

	// Token: 0x06005764 RID: 22372 RVA: 0x0037981C File Offset: 0x0037881C
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return RecordTableEnumerator.b("歂敄੆ⱈ♊ୌ㩎㽐げ畔㥖㙘⽚絜㙞ౠ።।ɦѨ๪ͬ᭮ᑰᝲ啴并奸⡺ᑼվꎂꦈ", a_) + this.ᜁ;
	}

	// Token: 0x06005765 RID: 22373 RVA: 0x00379880 File Offset: 0x00378880
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
		BitConverter.GetBytes(this.ᜁ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x06005766 RID: 22374 RVA: 0x003798D8 File Offset: 0x003788D8
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
		A_1 += 2;
	}

	// Token: 0x0400298F RID: 10639
	private const int ᜀ = 3;

	// Token: 0x04002990 RID: 10640
	private ushort ᜁ;

	// Token: 0x04002991 RID: 10641
	private byte[] ᜂ;
}
