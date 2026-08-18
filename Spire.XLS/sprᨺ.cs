using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002EE RID: 750
[spr\u2593(TBIFFRecord.ChartSurface)]
[CLSCompliant(false)]
internal class sprᨺ : BiffRecordRaw
{
	// Token: 0x06002E7F RID: 11903 RVA: 0x001A11D8 File Offset: 0x001A01D8
	public ushort ᜂ()
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

	// Token: 0x06002E80 RID: 11904 RVA: 0x001A121C File Offset: 0x001A021C
	public bool ᜄ()
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

	// Token: 0x06002E81 RID: 11905 RVA: 0x001A1260 File Offset: 0x001A0260
	public void ᜀ(bool A_0)
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

	// Token: 0x06002E82 RID: 11906 RVA: 0x001A12A4 File Offset: 0x001A02A4
	public bool ᜀ()
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

	// Token: 0x06002E83 RID: 11907 RVA: 0x001A12E8 File Offset: 0x001A02E8
	public void ᜁ(bool A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06002E84 RID: 11908 RVA: 0x001A132C File Offset: 0x001A032C
	public virtual int ᜃ()
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

	// Token: 0x06002E85 RID: 11909 RVA: 0x001A1368 File Offset: 0x001A0368
	public virtual int ᜁ()
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

	// Token: 0x06002E86 RID: 11910 RVA: 0x001A13A4 File Offset: 0x001A03A4
	public sprᨺ()
	{
	}

	// Token: 0x06002E87 RID: 11911 RVA: 0x001A13B8 File Offset: 0x001A03B8
	public sprᨺ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002E88 RID: 11912 RVA: 0x001A13D0 File Offset: 0x001A03D0
	public sprᨺ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002E89 RID: 11913 RVA: 0x001A13E4 File Offset: 0x001A03E4
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
		this.ᜂ = A_0.ReadBit(A_1, 0);
		this.ᜃ = A_0.ReadBit(A_1, 1);
	}

	// Token: 0x06002E8A RID: 11914 RVA: 0x001A1448 File Offset: 0x001A0448
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
		A_0.WriteBit(A_1, this.ᜂ, 0);
		A_0.WriteBit(A_1, this.ᜃ, 1);
		this.m_iLength = 2;
	}

	// Token: 0x040014F0 RID: 5360
	public new const int ᜀ = 2;

	// Token: 0x040014F1 RID: 5361
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x040014F2 RID: 5362
	[spr\u2429(0, 0, TFieldType.Bit)]
	private bool ᜂ;

	// Token: 0x040014F3 RID: 5363
	[spr\u2429(0, 1, TFieldType.Bit)]
	private new bool ᜃ;
}
