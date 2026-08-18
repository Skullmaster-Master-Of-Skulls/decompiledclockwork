using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000373 RID: 883
[spr\u2593(TBIFFRecord.ChartAxis)]
[CLSCompliant(false)]
internal class spr\u2426 : BiffRecordRaw
{
	// Token: 0x060035D2 RID: 13778 RVA: 0x001EA44C File Offset: 0x001E944C
	public new spr\u2426.ChartAxisType ᜃ()
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
		return (spr\u2426.ChartAxisType)this.ᜁ;
	}

	// Token: 0x060035D3 RID: 13779 RVA: 0x001EA490 File Offset: 0x001E9490
	public void ᜀ(spr\u2426.ChartAxisType A_0)
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
		this.ᜁ = (ushort)A_0;
	}

	// Token: 0x060035D4 RID: 13780 RVA: 0x001EA4D4 File Offset: 0x001E94D4
	public int ᜀ()
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

	// Token: 0x060035D5 RID: 13781 RVA: 0x001EA518 File Offset: 0x001E9518
	public int ᜄ()
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
		return this.ᜃ;
	}

	// Token: 0x060035D6 RID: 13782 RVA: 0x001EA55C File Offset: 0x001E955C
	public int ᜂ()
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
		return this.ᜄ;
	}

	// Token: 0x060035D7 RID: 13783 RVA: 0x001EA5A0 File Offset: 0x001E95A0
	public int ᜁ()
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
		return this.ᜅ;
	}

	// Token: 0x060035D8 RID: 13784 RVA: 0x001EA5E4 File Offset: 0x001E95E4
	public spr\u2426()
	{
	}

	// Token: 0x060035D9 RID: 13785 RVA: 0x001EA5F8 File Offset: 0x001E95F8
	public spr\u2426(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060035DA RID: 13786 RVA: 0x001EA610 File Offset: 0x001E9610
	public spr\u2426(int A_0) : base(A_0)
	{
	}

	// Token: 0x060035DB RID: 13787 RVA: 0x001EA624 File Offset: 0x001E9624
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
		this.ᜁ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜂ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜃ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜄ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜅ = A_0.ReadInt32(A_1);
	}

	// Token: 0x060035DC RID: 13788 RVA: 0x001EA6B4 File Offset: 0x001E96B4
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
		this.ᜂ = (this.ᜃ = (this.ᜄ = (this.ᜅ = 0)));
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_1 += 2;
		A_0.WriteInt32(A_1, this.ᜂ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜃ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜄ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜅ);
	}

	// Token: 0x060035DD RID: 13789 RVA: 0x001EA774 File Offset: 0x001E9774
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
		return 18;
	}

	// Token: 0x04001777 RID: 6007
	private new const int ᜀ = 18;

	// Token: 0x04001778 RID: 6008
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04001779 RID: 6009
	[spr\u2429(2, 4, true)]
	private int ᜂ;

	// Token: 0x0400177A RID: 6010
	[spr\u2429(6, 4, true)]
	private new int ᜃ;

	// Token: 0x0400177B RID: 6011
	[spr\u2429(10, 4, true)]
	private int ᜄ;

	// Token: 0x0400177C RID: 6012
	[spr\u2429(14, 4, true)]
	private int ᜅ;

	// Token: 0x02000374 RID: 884
	public enum ChartAxisType
	{
		// Token: 0x0400177E RID: 6014
		CategoryAxis,
		// Token: 0x0400177F RID: 6015
		ValueAxis,
		// Token: 0x04001780 RID: 6016
		SeriesAxis
	}
}
