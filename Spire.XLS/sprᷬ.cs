using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000571 RID: 1393
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.DCON)]
internal class spr\u1DEC : BiffRecordRaw
{
	// Token: 0x060053B0 RID: 21424 RVA: 0x00341474 File Offset: 0x00340474
	public spr\u1DEC()
	{
	}

	// Token: 0x060053B1 RID: 21425 RVA: 0x00341488 File Offset: 0x00340488
	public spr\u1DEC(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060053B2 RID: 21426 RVA: 0x003414A0 File Offset: 0x003404A0
	public spr\u1DEC(int A_0) : base(A_0)
	{
	}

	// Token: 0x060053B3 RID: 21427 RVA: 0x003414B4 File Offset: 0x003404B4
	public short ᜄ()
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

	// Token: 0x060053B4 RID: 21428 RVA: 0x003414F8 File Offset: 0x003404F8
	public void ᜀ(short A_0)
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

	// Token: 0x060053B5 RID: 21429 RVA: 0x0034153C File Offset: 0x0034053C
	public bool ᜅ()
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
		return this.ᜂ == 1;
	}

	// Token: 0x060053B6 RID: 21430 RVA: 0x00341580 File Offset: 0x00340580
	public void ᜀ(bool A_0)
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
		this.ᜂ = (A_0 ? 1 : 0);
	}

	// Token: 0x060053B7 RID: 21431 RVA: 0x003415D0 File Offset: 0x003405D0
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
		return this.ᜃ == 1;
	}

	// Token: 0x060053B8 RID: 21432 RVA: 0x00341614 File Offset: 0x00340614
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
		this.ᜃ = (A_0 ? 1 : 0);
	}

	// Token: 0x060053B9 RID: 21433 RVA: 0x00341664 File Offset: 0x00340664
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
		return this.ᜄ == 1;
	}

	// Token: 0x060053BA RID: 21434 RVA: 0x003416A8 File Offset: 0x003406A8
	public void ᜁ(bool A_0)
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
		this.ᜄ = (A_0 ? 1 : 0);
	}

	// Token: 0x060053BB RID: 21435 RVA: 0x003416F8 File Offset: 0x003406F8
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

	// Token: 0x060053BC RID: 21436 RVA: 0x00341734 File Offset: 0x00340734
	public virtual int ᜃ()
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
		return 8;
	}

	// Token: 0x060053BD RID: 21437 RVA: 0x00341770 File Offset: 0x00340770
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadInt16(A_1);
		this.ᜂ = A_0.ReadInt16(A_1 + 2);
		this.ᜃ = A_0.ReadInt16(A_1 + 4);
		this.ᜄ = A_0.ReadInt16(A_1 + 6);
	}

	// Token: 0x060053BE RID: 21438 RVA: 0x003417E8 File Offset: 0x003407E8
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteInt16(A_1, this.ᜁ);
		A_0.WriteInt16(A_1 + 2, this.ᜂ);
		A_0.WriteInt16(A_1 + 4, this.ᜃ);
		A_0.WriteInt16(A_1 + 6, this.ᜄ);
	}

	// Token: 0x04002721 RID: 10017
	private new const int ᜀ = 8;

	// Token: 0x04002722 RID: 10018
	[spr\u2429(0, 2, true)]
	private short ᜁ;

	// Token: 0x04002723 RID: 10019
	[spr\u2429(2, 2, true)]
	private short ᜂ;

	// Token: 0x04002724 RID: 10020
	[spr\u2429(4, 2, true)]
	private new short ᜃ;

	// Token: 0x04002725 RID: 10021
	[spr\u2429(6, 2, true)]
	private short ᜄ;
}
