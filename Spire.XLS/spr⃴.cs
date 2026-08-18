using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200053D RID: 1341
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartObjectLink)]
internal class spr\u20F4 : BiffRecordRaw
{
	// Token: 0x060051A4 RID: 20900 RVA: 0x0032F4C4 File Offset: 0x0032E4C4
	public new ObjectTextLinkType ᜃ()
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
		return (ObjectTextLinkType)this.ᜁ;
	}

	// Token: 0x060051A5 RID: 20901 RVA: 0x0032F508 File Offset: 0x0032E508
	public void ᜀ(ObjectTextLinkType A_0)
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

	// Token: 0x060051A6 RID: 20902 RVA: 0x0032F54C File Offset: 0x0032E54C
	public ushort ᜁ()
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

	// Token: 0x060051A7 RID: 20903 RVA: 0x0032F590 File Offset: 0x0032E590
	public void ᜁ(ushort A_0)
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

	// Token: 0x060051A8 RID: 20904 RVA: 0x0032F5D4 File Offset: 0x0032E5D4
	public ushort ᜄ()
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

	// Token: 0x060051A9 RID: 20905 RVA: 0x0032F618 File Offset: 0x0032E618
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
		this.ᜃ = A_0;
	}

	// Token: 0x060051AA RID: 20906 RVA: 0x0032F65C File Offset: 0x0032E65C
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
		return 6;
	}

	// Token: 0x060051AB RID: 20907 RVA: 0x0032F698 File Offset: 0x0032E698
	public virtual int ᜀ()
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
		return 6;
	}

	// Token: 0x060051AC RID: 20908 RVA: 0x0032F6D4 File Offset: 0x0032E6D4
	public spr\u20F4()
	{
	}

	// Token: 0x060051AD RID: 20909 RVA: 0x0032F6E8 File Offset: 0x0032E6E8
	public spr\u20F4(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060051AE RID: 20910 RVA: 0x0032F700 File Offset: 0x0032E700
	public spr\u20F4(int A_0) : base(A_0)
	{
	}

	// Token: 0x060051AF RID: 20911 RVA: 0x0032F714 File Offset: 0x0032E714
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
		A_1 += 2;
		this.ᜂ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜃ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x060051B0 RID: 20912 RVA: 0x0032F780 File Offset: 0x0032E780
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
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜂ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜃ);
	}

	// Token: 0x060051B1 RID: 20913 RVA: 0x0032F7FC File Offset: 0x0032E7FC
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 6;
	}

	// Token: 0x04002465 RID: 9317
	public new const int ᜀ = 6;

	// Token: 0x04002466 RID: 9318
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04002467 RID: 9319
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x04002468 RID: 9320
	[spr\u2429(4, 2)]
	private new ushort ᜃ;
}
