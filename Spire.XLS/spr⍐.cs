using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200033E RID: 830
[spr\u2593(TBIFFRecord.ChartDefaultText)]
[CLSCompliant(false)]
internal class spr\u2350 : BiffRecordRaw
{
	// Token: 0x060032B0 RID: 12976 RVA: 0x001D23B0 File Offset: 0x001D13B0
	public spr\u2350.TextDefaults ᜀ()
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
		return (spr\u2350.TextDefaults)this.ᜁ;
	}

	// Token: 0x060032B1 RID: 12977 RVA: 0x001D23F4 File Offset: 0x001D13F4
	public void ᜀ(spr\u2350.TextDefaults A_0)
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

	// Token: 0x060032B2 RID: 12978 RVA: 0x001D2438 File Offset: 0x001D1438
	public spr\u2350()
	{
	}

	// Token: 0x060032B3 RID: 12979 RVA: 0x001D244C File Offset: 0x001D144C
	public spr\u2350(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060032B4 RID: 12980 RVA: 0x001D2464 File Offset: 0x001D1464
	public spr\u2350(int A_0) : base(A_0)
	{
	}

	// Token: 0x060032B5 RID: 12981 RVA: 0x001D2478 File Offset: 0x001D1478
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x060032B6 RID: 12982 RVA: 0x001D24C0 File Offset: 0x001D14C0
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteUInt16(A_1, this.ᜁ);
	}

	// Token: 0x060032B7 RID: 12983 RVA: 0x001D2518 File Offset: 0x001D1518
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
		return 2;
	}

	// Token: 0x04001621 RID: 5665
	private new const int ᜀ = 2;

	// Token: 0x04001622 RID: 5666
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x0200033F RID: 831
	public enum TextDefaults
	{
		// Token: 0x04001624 RID: 5668
		ShowLabels,
		// Token: 0x04001625 RID: 5669
		ValueAndPercents,
		// Token: 0x04001626 RID: 5670
		All
	}
}
