using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x02000537 RID: 1335
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tInteger)]
internal class sprℿ : Ptg
{
	// Token: 0x06005166 RID: 20838 RVA: 0x0032E680 File Offset: 0x0032D680
	public sprℿ()
	{
	}

	// Token: 0x06005167 RID: 20839 RVA: 0x0032E694 File Offset: 0x0032D694
	public sprℿ(ushort A_0)
	{
		this.TokenCode = FormulaToken.tInteger;
		this.ᜀ(A_0);
	}

	// Token: 0x06005168 RID: 20840 RVA: 0x0032E6B8 File Offset: 0x0032D6B8
	public sprℿ(string A_0) : this(ushort.Parse(A_0))
	{
	}

	// Token: 0x06005169 RID: 20841 RVA: 0x0032E6D4 File Offset: 0x0032D6D4
	public sprℿ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600516A RID: 20842 RVA: 0x0032E6EC File Offset: 0x0032D6EC
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
		return this.ᜀ;
	}

	// Token: 0x0600516B RID: 20843 RVA: 0x0032E730 File Offset: 0x0032D730
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600516C RID: 20844 RVA: 0x0032E774 File Offset: 0x0032D774
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

	// Token: 0x0600516D RID: 20845 RVA: 0x0032E7B0 File Offset: 0x0032D7B0
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
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
		return this.ᜀ.ToString();
	}

	// Token: 0x0600516E RID: 20846 RVA: 0x0032E7F8 File Offset: 0x0032D7F8
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
		BitConverter.GetBytes(this.ᜀ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x0600516F RID: 20847 RVA: 0x0032E850 File Offset: 0x0032D850
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
		this.ᜀ = A_0.ReadUInt16(A_1);
		A_1 += 2;
	}

	// Token: 0x04002458 RID: 9304
	public ushort ᜀ;
}
