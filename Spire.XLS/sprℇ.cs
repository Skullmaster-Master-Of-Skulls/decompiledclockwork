using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002ED RID: 749
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartUnits)]
internal class sprℇ : BiffRecordRaw
{
	// Token: 0x06002E77 RID: 11895 RVA: 0x001A1044 File Offset: 0x001A0044
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

	// Token: 0x06002E78 RID: 11896 RVA: 0x001A1088 File Offset: 0x001A0088
	public virtual int ᜂ()
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

	// Token: 0x06002E79 RID: 11897 RVA: 0x001A10C4 File Offset: 0x001A00C4
	public virtual int ᜁ()
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

	// Token: 0x06002E7A RID: 11898 RVA: 0x001A1100 File Offset: 0x001A0100
	public sprℇ()
	{
	}

	// Token: 0x06002E7B RID: 11899 RVA: 0x001A1114 File Offset: 0x001A0114
	public sprℇ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002E7C RID: 11900 RVA: 0x001A112C File Offset: 0x001A012C
	public sprℇ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002E7D RID: 11901 RVA: 0x001A1140 File Offset: 0x001A0140
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

	// Token: 0x06002E7E RID: 11902 RVA: 0x001A1188 File Offset: 0x001A0188
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		this.m_iLength = 2;
	}

	// Token: 0x040014EE RID: 5358
	public new const int ᜀ = 2;

	// Token: 0x040014EF RID: 5359
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
