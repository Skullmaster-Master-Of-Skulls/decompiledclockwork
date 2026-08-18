using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000273 RID: 627
[spr\u2593(TBIFFRecord.Backup)]
[CLSCompliant(false)]
internal class spr᪓ : BiffRecordRaw
{
	// Token: 0x060025EC RID: 9708 RVA: 0x0015E16C File Offset: 0x0015D16C
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
		return this.ᜀ;
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x0015E1B0 File Offset: 0x0015D1B0
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
		this.ᜀ = A_0;
	}

	// Token: 0x060025EE RID: 9710 RVA: 0x0015E1F4 File Offset: 0x0015D1F4
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

	// Token: 0x060025EF RID: 9711 RVA: 0x0015E230 File Offset: 0x0015D230
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

	// Token: 0x060025F0 RID: 9712 RVA: 0x0015E26C File Offset: 0x0015D26C
	public spr᪓()
	{
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x0015E280 File Offset: 0x0015D280
	public spr᪓(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060025F2 RID: 9714 RVA: 0x0015E298 File Offset: 0x0015D298
	public spr᪓(int A_0) : base(A_0)
	{
	}

	// Token: 0x060025F3 RID: 9715 RVA: 0x0015E2AC File Offset: 0x0015D2AC
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
		this.ᜀ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x060025F4 RID: 9716 RVA: 0x0015E2F4 File Offset: 0x0015D2F4
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
		A_0.WriteUInt16(A_1, this.ᜀ);
		this.m_iLength = 2;
	}

	// Token: 0x040012E9 RID: 4841
	[spr\u2429(0, 2)]
	private new ushort ᜀ;
}
