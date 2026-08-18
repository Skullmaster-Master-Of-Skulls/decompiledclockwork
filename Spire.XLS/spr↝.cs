using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200056A RID: 1386
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.Iteration)]
internal class spr\u219D : BiffRecordRaw
{
	// Token: 0x0600535F RID: 21343 RVA: 0x0033FBFC File Offset: 0x0033EBFC
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

	// Token: 0x06005360 RID: 21344 RVA: 0x0033FC40 File Offset: 0x0033EC40
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

	// Token: 0x06005361 RID: 21345 RVA: 0x0033FC84 File Offset: 0x0033EC84
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

	// Token: 0x06005362 RID: 21346 RVA: 0x0033FCC0 File Offset: 0x0033ECC0
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

	// Token: 0x06005363 RID: 21347 RVA: 0x0033FCFC File Offset: 0x0033ECFC
	public spr\u219D()
	{
	}

	// Token: 0x06005364 RID: 21348 RVA: 0x0033FD10 File Offset: 0x0033ED10
	public spr\u219D(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005365 RID: 21349 RVA: 0x0033FD28 File Offset: 0x0033ED28
	public spr\u219D(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005366 RID: 21350 RVA: 0x0033FD3C File Offset: 0x0033ED3C
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

	// Token: 0x06005367 RID: 21351 RVA: 0x0033FD84 File Offset: 0x0033ED84
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
		this.m_iLength = 2;
		A_0.WriteUInt16(A_1, this.ᜁ);
	}

	// Token: 0x04002708 RID: 9992
	private new const int ᜀ = 2;

	// Token: 0x04002709 RID: 9993
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
