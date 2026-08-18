using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000598 RID: 1432
[spr\u2593(TBIFFRecord.UnkEnd)]
[CLSCompliant(false)]
internal class spr\u22C3 : BiffRecordRaw
{
	// Token: 0x060056F6 RID: 22262 RVA: 0x0037740C File Offset: 0x0037640C
	public int ᜂ()
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

	// Token: 0x060056F7 RID: 22263 RVA: 0x00377450 File Offset: 0x00376450
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
		this.ᜃ = A_0;
	}

	// Token: 0x060056F8 RID: 22264 RVA: 0x00377494 File Offset: 0x00376494
	public int ᜀ()
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
		return this.ᜄ;
	}

	// Token: 0x060056F9 RID: 22265 RVA: 0x003774D8 File Offset: 0x003764D8
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
		this.ᜄ = A_0;
	}

	// Token: 0x060056FA RID: 22266 RVA: 0x0037751C File Offset: 0x0037651C
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
		return 8;
	}

	// Token: 0x060056FB RID: 22267 RVA: 0x00377558 File Offset: 0x00376558
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
		return 8;
	}

	// Token: 0x060056FC RID: 22268 RVA: 0x00377594 File Offset: 0x00376594
	public spr\u22C3()
	{
	}

	// Token: 0x060056FD RID: 22269 RVA: 0x003775A8 File Offset: 0x003765A8
	public spr\u22C3(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060056FE RID: 22270 RVA: 0x003775C0 File Offset: 0x003765C0
	public spr\u22C3(int A_0) : base(A_0)
	{
	}

	// Token: 0x060056FF RID: 22271 RVA: 0x003775D4 File Offset: 0x003765D4
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
		this.ᜃ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜄ = A_0.ReadInt32(A_1);
	}

	// Token: 0x06005700 RID: 22272 RVA: 0x00377630 File Offset: 0x00376630
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
		this.ᜃ = 449;
		this.ᜄ = 101716;
		this.m_iLength = 8;
		A_0.WriteInt32(A_1, this.ᜃ);
		A_0.WriteInt32(A_1 + 4, this.ᜄ);
	}

	// Token: 0x04002951 RID: 10577
	private new const int ᜀ = 449;

	// Token: 0x04002952 RID: 10578
	private const int ᜁ = 101716;

	// Token: 0x04002953 RID: 10579
	private const int ᜂ = 8;

	// Token: 0x04002954 RID: 10580
	[spr\u2429(0, 4, true)]
	private new int ᜃ;

	// Token: 0x04002955 RID: 10581
	[spr\u2429(4, 4, true)]
	private int ᜄ;
}
