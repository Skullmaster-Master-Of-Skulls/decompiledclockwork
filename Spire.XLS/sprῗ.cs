using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002AF RID: 687
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.PageItemNameCount)]
internal class sprῗ : BiffRecordRaw
{
	// Token: 0x060029B9 RID: 10681 RVA: 0x00178478 File Offset: 0x00177478
	public sprῗ()
	{
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x0017848C File Offset: 0x0017748C
	public sprῗ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x001784A4 File Offset: 0x001774A4
	public sprῗ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x001784B8 File Offset: 0x001774B8
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

	// Token: 0x060029BD RID: 10685 RVA: 0x001784FC File Offset: 0x001774FC
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

	// Token: 0x060029BE RID: 10686 RVA: 0x00178540 File Offset: 0x00177540
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

	// Token: 0x060029BF RID: 10687 RVA: 0x00178588 File Offset: 0x00177588
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		this.m_iLength = 2;
	}

	// Token: 0x060029C0 RID: 10688 RVA: 0x001785D8 File Offset: 0x001775D8
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

	// Token: 0x040013E4 RID: 5092
	private new const int ᜀ = 2;

	// Token: 0x040013E5 RID: 5093
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
