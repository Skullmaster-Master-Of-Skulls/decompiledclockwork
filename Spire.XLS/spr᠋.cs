using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x02000403 RID: 1027
[spr\u2400(FormulaToken.tNumber)]
internal class spr\u180B : Ptg
{
	// Token: 0x06003DC2 RID: 15810 RVA: 0x0022643C File Offset: 0x0022543C
	public spr\u180B()
	{
	}

	// Token: 0x06003DC3 RID: 15811 RVA: 0x00226450 File Offset: 0x00225450
	public spr\u180B(double A_0)
	{
		this.TokenCode = FormulaToken.tNumber;
		this.ᜀ(A_0);
	}

	// Token: 0x06003DC4 RID: 15812 RVA: 0x00226474 File Offset: 0x00225474
	public spr\u180B(string A_0) : this(A_0, null)
	{
	}

	// Token: 0x06003DC5 RID: 15813 RVA: 0x0022648C File Offset: 0x0022548C
	public spr\u180B(string A_0, NumberFormatInfo A_1)
	{
		double a_ = (A_1 == null) ? double.Parse(A_0) : double.Parse(A_0, A_1);
		this.TokenCode = FormulaToken.tNumber;
		this.ᜀ(a_);
	}

	// Token: 0x06003DC6 RID: 15814 RVA: 0x002264C8 File Offset: 0x002254C8
	public spr\u180B(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003DC7 RID: 15815 RVA: 0x002264E0 File Offset: 0x002254E0
	public double ᜀ()
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

	// Token: 0x06003DC8 RID: 15816 RVA: 0x00226524 File Offset: 0x00225524
	public void ᜀ(double A_0)
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

	// Token: 0x06003DC9 RID: 15817 RVA: 0x00226568 File Offset: 0x00225568
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
		return 9;
	}

	// Token: 0x06003DCA RID: 15818 RVA: 0x002265A8 File Offset: 0x002255A8
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		while (A_4 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.ToString();
			}
		}
		return this.ᜀ.ToString(A_4);
	}

	// Token: 0x06003DCB RID: 15819 RVA: 0x00226604 File Offset: 0x00225604
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4)
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
		return this.ToString(A_0, A_1, A_2, A_3, A_4, false);
	}

	// Token: 0x06003DCC RID: 15820 RVA: 0x00226650 File Offset: 0x00225650
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

	// Token: 0x06003DCD RID: 15821 RVA: 0x002266A8 File Offset: 0x002256A8
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
		this.ᜀ = A_0.ReadDouble(A_1);
		A_1 += 8;
	}

	// Token: 0x04001A94 RID: 6804
	private double ᜀ;
}
