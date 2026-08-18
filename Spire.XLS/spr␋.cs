using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200035F RID: 863
[spr\u2593(TBIFFRecord.PrintHeaders)]
[CLSCompliant(false)]
internal class spr\u240B : BiffRecordRaw
{
	// Token: 0x060034CD RID: 13517 RVA: 0x001E46E4 File Offset: 0x001E36E4
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

	// Token: 0x060034CE RID: 13518 RVA: 0x001E4728 File Offset: 0x001E3728
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

	// Token: 0x060034CF RID: 13519 RVA: 0x001E476C File Offset: 0x001E376C
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

	// Token: 0x060034D0 RID: 13520 RVA: 0x001E47A8 File Offset: 0x001E37A8
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

	// Token: 0x060034D1 RID: 13521 RVA: 0x001E47E4 File Offset: 0x001E37E4
	public spr\u240B()
	{
	}

	// Token: 0x060034D2 RID: 13522 RVA: 0x001E47F8 File Offset: 0x001E37F8
	public spr\u240B(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060034D3 RID: 13523 RVA: 0x001E4810 File Offset: 0x001E3810
	public spr\u240B(int A_0) : base(A_0)
	{
	}

	// Token: 0x060034D4 RID: 13524 RVA: 0x001E4824 File Offset: 0x001E3824
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

	// Token: 0x060034D5 RID: 13525 RVA: 0x001E486C File Offset: 0x001E386C
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

	// Token: 0x0400170F RID: 5903
	private new const int ᜀ = 2;

	// Token: 0x04001710 RID: 5904
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
