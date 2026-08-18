using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002E6 RID: 742
[spr\u2593(TBIFFRecord.MMS)]
[CLSCompliant(false)]
internal class spr\u1AC9 : BiffRecordRaw
{
	// Token: 0x06002E2C RID: 11820 RVA: 0x0019F598 File Offset: 0x0019E598
	public new byte ᜃ()
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

	// Token: 0x06002E2D RID: 11821 RVA: 0x0019F5DC File Offset: 0x0019E5DC
	public void ᜁ(byte A_0)
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

	// Token: 0x06002E2E RID: 11822 RVA: 0x0019F620 File Offset: 0x0019E620
	public byte ᜀ()
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

	// Token: 0x06002E2F RID: 11823 RVA: 0x0019F664 File Offset: 0x0019E664
	public void ᜀ(byte A_0)
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

	// Token: 0x06002E30 RID: 11824 RVA: 0x0019F6A8 File Offset: 0x0019E6A8
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

	// Token: 0x06002E31 RID: 11825 RVA: 0x0019F6E4 File Offset: 0x0019E6E4
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

	// Token: 0x06002E32 RID: 11826 RVA: 0x0019F720 File Offset: 0x0019E720
	public spr\u1AC9()
	{
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x0019F734 File Offset: 0x0019E734
	public spr\u1AC9(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x0019F74C File Offset: 0x0019E74C
	public spr\u1AC9(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x0019F760 File Offset: 0x0019E760
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
		this.ᜁ = A_0.ReadByte(A_1);
		this.ᜂ = A_0.ReadByte(A_1 + 1);
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x0019F7B8 File Offset: 0x0019E7B8
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
		A_0.WriteByte(A_1, this.ᜁ);
		A_0.WriteByte(A_1 + 1, this.ᜂ);
		this.m_iLength = 2;
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x0019F818 File Offset: 0x0019E818
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

	// Token: 0x040014DA RID: 5338
	private new const int ᜀ = 2;

	// Token: 0x040014DB RID: 5339
	[spr\u2429(0, 1)]
	private byte ᜁ;

	// Token: 0x040014DC RID: 5340
	[spr\u2429(1, 1)]
	private byte ᜂ;
}
