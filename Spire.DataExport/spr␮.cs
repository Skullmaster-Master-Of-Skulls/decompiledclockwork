using System;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000061 RID: 97
internal class spr\u242E : sprạ
{
	// Token: 0x06000328 RID: 808 RVA: 0x0001E804 File Offset: 0x0001D804
	public double ᜂ()
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

	// Token: 0x06000329 RID: 809 RVA: 0x0001E848 File Offset: 0x0001D848
	public spr\u242E() : base(FormulaTokenCode.Num, 9, FormulaTokenType.Operand)
	{
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0001E860 File Offset: 0x0001D860
	public override void ᜀ(object[] A_0)
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
		this.ᜀ = (double)A_0[0];
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0001E8AC File Offset: 0x0001D8AC
	public override void ᜀ(byte[] A_0, int A_1)
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
		this.ᜀ = BitConverter.ToDouble(A_0, A_1);
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0001E8F4 File Offset: 0x0001D8F4
	public override byte[] ᜁ()
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
		byte[] array = base.ᜁ();
		BitConverter.GetBytes(this.ᜀ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0001E94C File Offset: 0x0001D94C
	public override string ᜀ()
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
		return this.ᜀ.ToString();
	}

	// Token: 0x04000255 RID: 597
	private new double ᜀ;
}
