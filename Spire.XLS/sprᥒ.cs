using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x0200039C RID: 924
[spr\u2400(FormulaToken.tBoolean)]
internal class sprᥒ : Ptg
{
	// Token: 0x06003841 RID: 14401 RVA: 0x001F7008 File Offset: 0x001F6008
	public sprᥒ()
	{
	}

	// Token: 0x06003842 RID: 14402 RVA: 0x001F701C File Offset: 0x001F601C
	public sprᥒ(bool A_0)
	{
		this.TokenCode = FormulaToken.tBoolean;
		this.ᜀ(A_0);
	}

	// Token: 0x06003843 RID: 14403 RVA: 0x001F7040 File Offset: 0x001F6040
	public sprᥒ(string A_0) : this(bool.Parse(A_0))
	{
	}

	// Token: 0x06003844 RID: 14404 RVA: 0x001F705C File Offset: 0x001F605C
	public sprᥒ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003845 RID: 14405 RVA: 0x001F7074 File Offset: 0x001F6074
	public bool ᜀ()
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

	// Token: 0x06003846 RID: 14406 RVA: 0x001F70B8 File Offset: 0x001F60B8
	public void ᜀ(bool A_0)
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

	// Token: 0x06003847 RID: 14407 RVA: 0x001F70FC File Offset: 0x001F60FC
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
		return 2;
	}

	// Token: 0x06003848 RID: 14408 RVA: 0x001F7138 File Offset: 0x001F6138
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

	// Token: 0x06003849 RID: 14409 RVA: 0x001F7180 File Offset: 0x001F6180
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

	// Token: 0x0600384A RID: 14410 RVA: 0x001F71D8 File Offset: 0x001F61D8
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
		this.ᜀ = A_0.ReadBoolean(A_1++);
	}

	// Token: 0x040018CD RID: 6349
	private bool ᜀ;
}
