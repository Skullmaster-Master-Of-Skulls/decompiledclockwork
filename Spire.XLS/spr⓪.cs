using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004A6 RID: 1190
[spr\u2593(TBIFFRecord.LeftMargin)]
[spr\u2593(TBIFFRecord.TopMargin)]
[spr\u2593(TBIFFRecord.RightMargin)]
[spr\u2593(TBIFFRecord.BottomMargin)]
[CLSCompliant(false)]
internal class spr\u24EA : BiffRecordRaw
{
	// Token: 0x0600498F RID: 18831 RVA: 0x002CA64C File Offset: 0x002C964C
	public double ᜁ()
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

	// Token: 0x06004990 RID: 18832 RVA: 0x002CA690 File Offset: 0x002C9690
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
		this.ᜂ = A_0;
	}

	// Token: 0x06004991 RID: 18833 RVA: 0x002CA6D4 File Offset: 0x002C96D4
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
		return 8;
	}

	// Token: 0x06004992 RID: 18834 RVA: 0x002CA710 File Offset: 0x002C9710
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
		return 8;
	}

	// Token: 0x06004993 RID: 18835 RVA: 0x002CA74C File Offset: 0x002C974C
	public spr\u24EA()
	{
	}

	// Token: 0x06004994 RID: 18836 RVA: 0x002CA760 File Offset: 0x002C9760
	public spr\u24EA(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004995 RID: 18837 RVA: 0x002CA778 File Offset: 0x002C9778
	public spr\u24EA(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004996 RID: 18838 RVA: 0x002CA78C File Offset: 0x002C978C
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
		this.ᜂ = A_0.ReadDouble(A_1);
	}

	// Token: 0x06004997 RID: 18839 RVA: 0x002CA7D4 File Offset: 0x002C97D4
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
		this.m_iLength = 8;
		A_0.WriteDouble(A_1, this.ᜂ);
	}

	// Token: 0x04002167 RID: 8551
	public new const double ᜀ = 0.0;

	// Token: 0x04002168 RID: 8552
	private const int ᜁ = 8;

	// Token: 0x04002169 RID: 8553
	[spr\u2429(0, 8, TFieldType.Float)]
	private double ᜂ;
}
