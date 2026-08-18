using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003A1 RID: 929
[spr\u2593(TBIFFRecord.ChartSiIndex)]
[CLSCompliant(false)]
internal class spr\u220C : BiffRecordRaw
{
	// Token: 0x06003888 RID: 14472 RVA: 0x001F96F0 File Offset: 0x001F86F0
	public ushort ᜁ()
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

	// Token: 0x06003889 RID: 14473 RVA: 0x001F9734 File Offset: 0x001F8734
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

	// Token: 0x0600388A RID: 14474 RVA: 0x001F9778 File Offset: 0x001F8778
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
		return 2;
	}

	// Token: 0x0600388B RID: 14475 RVA: 0x001F97B4 File Offset: 0x001F87B4
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
		return 2;
	}

	// Token: 0x0600388C RID: 14476 RVA: 0x001F97F0 File Offset: 0x001F87F0
	public spr\u220C()
	{
	}

	// Token: 0x0600388D RID: 14477 RVA: 0x001F9804 File Offset: 0x001F8804
	public spr\u220C(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600388E RID: 14478 RVA: 0x001F981C File Offset: 0x001F881C
	public spr\u220C(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600388F RID: 14479 RVA: 0x001F9830 File Offset: 0x001F8830
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
	}

	// Token: 0x06003890 RID: 14480 RVA: 0x001F9878 File Offset: 0x001F8878
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteUInt16(A_1, this.ᜁ);
	}

	// Token: 0x06003891 RID: 14481 RVA: 0x001F98D0 File Offset: 0x001F88D0
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

	// Token: 0x040018D9 RID: 6361
	public new const int ᜀ = 2;

	// Token: 0x040018DA RID: 6362
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
