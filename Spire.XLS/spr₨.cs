using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000324 RID: 804
[spr\u2593(TBIFFRecord.PivotError)]
[CLSCompliant(false)]
internal class spr\u20A8 : BiffRecordRaw, spr\u1929
{
	// Token: 0x060031A0 RID: 12704 RVA: 0x001CB8A4 File Offset: 0x001CA8A4
	public spr\u20A8()
	{
	}

	// Token: 0x060031A1 RID: 12705 RVA: 0x001CB8B8 File Offset: 0x001CA8B8
	public spr\u20A8(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060031A2 RID: 12706 RVA: 0x001CB8D0 File Offset: 0x001CA8D0
	public spr\u20A8(int A_0) : base(A_0)
	{
	}

	// Token: 0x060031A3 RID: 12707 RVA: 0x001CB8E4 File Offset: 0x001CA8E4
	public new ushort ᜃ()
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

	// Token: 0x060031A4 RID: 12708 RVA: 0x001CB928 File Offset: 0x001CA928
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

	// Token: 0x060031A5 RID: 12709 RVA: 0x001CB96C File Offset: 0x001CA96C
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

	// Token: 0x060031A6 RID: 12710 RVA: 0x001CB9A8 File Offset: 0x001CA9A8
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

	// Token: 0x060031A7 RID: 12711 RVA: 0x001CB9E4 File Offset: 0x001CA9E4
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

	// Token: 0x060031A8 RID: 12712 RVA: 0x001CBA2C File Offset: 0x001CAA2C
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

	// Token: 0x060031A9 RID: 12713 RVA: 0x001CBA7C File Offset: 0x001CAA7C
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

	// Token: 0x060031AA RID: 12714 RVA: 0x001CBAB8 File Offset: 0x001CAAB8
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

	// Token: 0x060031AB RID: 12715 RVA: 0x001CBB00 File Offset: 0x001CAB00
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
		this.ᜀ((ushort)A_0);
	}

	// Token: 0x040015D3 RID: 5587
	private new const int ᜀ = 2;

	// Token: 0x040015D4 RID: 5588
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
