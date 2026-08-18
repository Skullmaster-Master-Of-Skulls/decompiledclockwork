using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002EB RID: 747
[spr\u2593(TBIFFRecord.DxGCol)]
internal class spr\u24FD : BiffRecordRaw
{
	// Token: 0x06002E61 RID: 11873 RVA: 0x001A0A14 File Offset: 0x0019FA14
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

	// Token: 0x06002E62 RID: 11874 RVA: 0x001A0A58 File Offset: 0x0019FA58
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

	// Token: 0x06002E63 RID: 11875 RVA: 0x001A0A9C File Offset: 0x0019FA9C
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

	// Token: 0x06002E64 RID: 11876 RVA: 0x001A0AD8 File Offset: 0x0019FAD8
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

	// Token: 0x06002E65 RID: 11877 RVA: 0x001A0B14 File Offset: 0x0019FB14
	public spr\u24FD()
	{
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x001A0B34 File Offset: 0x0019FB34
	public spr\u24FD(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002E67 RID: 11879 RVA: 0x001A0B54 File Offset: 0x0019FB54
	public spr\u24FD(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002E68 RID: 11880 RVA: 0x001A0B74 File Offset: 0x0019FB74
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

	// Token: 0x06002E69 RID: 11881 RVA: 0x001A0BBC File Offset: 0x0019FBBC
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

	// Token: 0x06002E6A RID: 11882 RVA: 0x001A0C0C File Offset: 0x0019FC0C
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

	// Token: 0x040014E8 RID: 5352
	private new const int ᜀ = 2;

	// Token: 0x040014E9 RID: 5353
	[spr\u2429(0, 2)]
	private ushort ᜁ = 2340;
}
