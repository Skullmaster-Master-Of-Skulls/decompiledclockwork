using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000427 RID: 1063
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.PivotDouble)]
internal class spr\u1AF2 : BiffRecordRaw, spr\u1929
{
	// Token: 0x0600406F RID: 16495 RVA: 0x00243100 File Offset: 0x00242100
	public spr\u1AF2()
	{
	}

	// Token: 0x06004070 RID: 16496 RVA: 0x0024311C File Offset: 0x0024211C
	public spr\u1AF2(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004071 RID: 16497 RVA: 0x00243138 File Offset: 0x00242138
	public spr\u1AF2(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004072 RID: 16498 RVA: 0x00243154 File Offset: 0x00242154
	public new double ᜃ()
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

	// Token: 0x06004073 RID: 16499 RVA: 0x00243198 File Offset: 0x00242198
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
		this.ᜁ = A_0;
	}

	// Token: 0x06004074 RID: 16500 RVA: 0x002431DC File Offset: 0x002421DC
	public virtual int ᜂ()
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
		return this.ᜀ;
	}

	// Token: 0x06004075 RID: 16501 RVA: 0x00243220 File Offset: 0x00242220
	public virtual int ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x06004076 RID: 16502 RVA: 0x00243264 File Offset: 0x00242264
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadDouble(A_1);
	}

	// Token: 0x06004077 RID: 16503 RVA: 0x002432AC File Offset: 0x002422AC
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteDouble(A_1, this.ᜁ);
		this.m_iLength = this.ᜀ;
	}

	// Token: 0x06004078 RID: 16504 RVA: 0x00243300 File Offset: 0x00242300
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
		return this.ᜀ;
	}

	// Token: 0x06004079 RID: 16505 RVA: 0x00243344 File Offset: 0x00242344
	object spr\u1929.ᜁ()
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
		return this.ᜃ();
	}

	// Token: 0x0600407A RID: 16506 RVA: 0x0024338C File Offset: 0x0024238C
	void spr\u1929.ᜀ(object A_0)
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
		this.ᜀ((double)A_0);
	}

	// Token: 0x04001CD6 RID: 7382
	private new int ᜀ = 8;

	// Token: 0x04001CD7 RID: 7383
	[spr\u2429(0, 8, TFieldType.Float)]
	private double ᜁ;
}
