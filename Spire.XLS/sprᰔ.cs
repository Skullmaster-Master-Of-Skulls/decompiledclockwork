using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000327 RID: 807
[spr\u2593(TBIFFRecord.CacheDataEx)]
[CLSCompliant(false)]
internal class sprᰔ : BiffRecordRaw
{
	// Token: 0x060031C9 RID: 12745 RVA: 0x001CC58C File Offset: 0x001CB58C
	public sprᰔ()
	{
	}

	// Token: 0x060031CA RID: 12746 RVA: 0x001CC5A0 File Offset: 0x001CB5A0
	public sprᰔ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060031CB RID: 12747 RVA: 0x001CC5B8 File Offset: 0x001CB5B8
	public sprᰔ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060031CC RID: 12748 RVA: 0x001CC5CC File Offset: 0x001CB5CC
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
		return this.ᜁ;
	}

	// Token: 0x060031CD RID: 12749 RVA: 0x001CC610 File Offset: 0x001CB610
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

	// Token: 0x060031CE RID: 12750 RVA: 0x001CC654 File Offset: 0x001CB654
	public uint ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x060031CF RID: 12751 RVA: 0x001CC698 File Offset: 0x001CB698
	public void ᜀ(uint A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x060031D0 RID: 12752 RVA: 0x001CC6DC File Offset: 0x001CB6DC
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
		this.ᜂ = A_0.ReadUInt32(A_1 + 8);
	}

	// Token: 0x060031D1 RID: 12753 RVA: 0x001CC734 File Offset: 0x001CB734
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
		A_0.WriteUInt32(A_1 + 8, this.ᜂ);
		this.m_iLength = 12;
	}

	// Token: 0x060031D2 RID: 12754 RVA: 0x001CC794 File Offset: 0x001CB794
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
		return 12;
	}

	// Token: 0x040015DD RID: 5597
	private new const int ᜀ = 12;

	// Token: 0x040015DE RID: 5598
	[spr\u2429(0, 8, TFieldType.Float)]
	private double ᜁ;

	// Token: 0x040015DF RID: 5599
	[spr\u2429(8, 4)]
	private uint ᜂ;
}
