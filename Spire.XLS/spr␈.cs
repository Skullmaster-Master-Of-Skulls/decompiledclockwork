using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000264 RID: 612
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.Pane)]
internal class spr\u2408 : BiffRecordRaw
{
	// Token: 0x060024B2 RID: 9394 RVA: 0x001555F4 File Offset: 0x001545F4
	public new int ᜃ()
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

	// Token: 0x060024B3 RID: 9395 RVA: 0x00155638 File Offset: 0x00154638
	public void ᜀ(int A_0)
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

	// Token: 0x060024B4 RID: 9396 RVA: 0x0015567C File Offset: 0x0015467C
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
		return this.ᜂ;
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x001556C0 File Offset: 0x001546C0
	public void ᜁ(int A_0)
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

	// Token: 0x060024B6 RID: 9398 RVA: 0x00155704 File Offset: 0x00154704
	public int ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x00155748 File Offset: 0x00154748
	public void ᜂ(int A_0)
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

	// Token: 0x060024B8 RID: 9400 RVA: 0x0015578C File Offset: 0x0015478C
	public int ᜅ()
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

	// Token: 0x060024B9 RID: 9401 RVA: 0x001557D0 File Offset: 0x001547D0
	public new void ᜃ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x060024BA RID: 9402 RVA: 0x00155814 File Offset: 0x00154814
	public ushort ᜆ()
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

	// Token: 0x060024BB RID: 9403 RVA: 0x00155858 File Offset: 0x00154858
	public void ᜀ(ushort A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x060024BC RID: 9404 RVA: 0x0015589C File Offset: 0x0015489C
	public virtual int ᜂ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return 10;
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x001558DC File Offset: 0x001548DC
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
		return 10;
	}

	// Token: 0x060024BE RID: 9406 RVA: 0x0015591C File Offset: 0x0015491C
	public spr\u2408()
	{
	}

	// Token: 0x060024BF RID: 9407 RVA: 0x00155930 File Offset: 0x00154930
	public spr\u2408(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x00155948 File Offset: 0x00154948
	public spr\u2408(int A_0) : base(A_0)
	{
	}

	// Token: 0x060024C1 RID: 9409 RVA: 0x0015595C File Offset: 0x0015495C
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
		this.ᜁ = (int)A_0.ReadUInt16(A_1);
		this.ᜂ = (int)A_0.ReadUInt16(A_1 + 2);
		this.ᜃ = (int)A_0.ReadUInt16(A_1 + 4);
		this.ᜄ = (int)A_0.ReadUInt16(A_1 + 6);
		this.ᜅ = A_0.ReadUInt16(A_1 + 8);
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x001559E0 File Offset: 0x001549E0
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
		A_0.WriteUInt16(A_1, (ushort)this.ᜁ);
		A_0.WriteUInt16(A_1 + 2, (ushort)this.ᜂ);
		A_0.WriteUInt16(A_1 + 4, (ushort)this.ᜃ);
		A_0.WriteUInt16(A_1 + 6, (ushort)this.ᜄ);
		A_0.WriteUInt16(A_1 + 8, this.ᜅ);
		this.m_iLength = 10;
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x00155A70 File Offset: 0x00154A70
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
		return 10;
	}

	// Token: 0x0400128C RID: 4748
	private new const int ᜀ = 10;

	// Token: 0x0400128D RID: 4749
	[spr\u2429(0, 2)]
	private int ᜁ;

	// Token: 0x0400128E RID: 4750
	[spr\u2429(2, 2)]
	private int ᜂ;

	// Token: 0x0400128F RID: 4751
	[spr\u2429(4, 2)]
	private new int ᜃ;

	// Token: 0x04001290 RID: 4752
	[spr\u2429(6, 2)]
	private int ᜄ;

	// Token: 0x04001291 RID: 4753
	[spr\u2429(8, 2)]
	private ushort ᜅ;
}
