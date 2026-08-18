using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020005A8 RID: 1448
[spr\u2593(TBIFFRecord.ChartLine)]
[CLSCompliant(false)]
internal class sprᯙ : BiffRecordRaw, spr\u1C7F
{
	// Token: 0x060057D0 RID: 22480 RVA: 0x0037C4C4 File Offset: 0x0037B4C4
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

	// Token: 0x060057D1 RID: 22481 RVA: 0x0037C508 File Offset: 0x0037B508
	public bool ᜀ()
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

	// Token: 0x060057D2 RID: 22482 RVA: 0x0037C54C File Offset: 0x0037B54C
	public void ᜁ(bool A_0)
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

	// Token: 0x060057D3 RID: 22483 RVA: 0x0037C590 File Offset: 0x0037B590
	public bool ᜁ()
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

	// Token: 0x060057D4 RID: 22484 RVA: 0x0037C5D4 File Offset: 0x0037B5D4
	public void ᜂ(bool A_0)
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

	// Token: 0x060057D5 RID: 22485 RVA: 0x0037C618 File Offset: 0x0037B618
	public bool ᜂ()
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

	// Token: 0x060057D6 RID: 22486 RVA: 0x0037C65C File Offset: 0x0037B65C
	public void ᜀ(bool A_0)
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

	// Token: 0x060057D7 RID: 22487 RVA: 0x0037C6A0 File Offset: 0x0037B6A0
	public sprᯙ()
	{
	}

	// Token: 0x060057D8 RID: 22488 RVA: 0x0037C6B4 File Offset: 0x0037B6B4
	public sprᯙ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060057D9 RID: 22489 RVA: 0x0037C6CC File Offset: 0x0037B6CC
	public sprᯙ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060057DA RID: 22490 RVA: 0x0037C6E0 File Offset: 0x0037B6E0
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
		this.ᜄ = A_0.ReadBit(A_1, 2);
	}

	// Token: 0x060057DB RID: 22491 RVA: 0x0037C754 File Offset: 0x0037B754
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
		this.ᜁ &= 7;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteBit(A_1, this.ᜂ, 0);
		A_0.WriteBit(A_1, this.ᜃ, 1);
		A_0.WriteBit(A_1, this.ᜄ, 2);
		this.m_iLength = 2;
	}

	// Token: 0x060057DC RID: 22492 RVA: 0x0037C7DC File Offset: 0x0037B7DC
	public virtual int ᜀ(ExcelVersion A_0)
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

	// Token: 0x040029C6 RID: 10694
	private new const int ᜀ = 2;

	// Token: 0x040029C7 RID: 10695
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x040029C8 RID: 10696
	[spr\u2429(0, 0, TFieldType.Bit)]
	private bool ᜂ;

	// Token: 0x040029C9 RID: 10697
	[spr\u2429(0, 1, TFieldType.Bit)]
	private new bool ᜃ;

	// Token: 0x040029CA RID: 10698
	[spr\u2429(0, 2, TFieldType.Bit)]
	private bool ᜄ;
}
