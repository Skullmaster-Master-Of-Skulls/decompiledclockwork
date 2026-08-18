using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000488 RID: 1160
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.PrintedChartSize)]
internal class spr\u2605 : BiffRecordRaw
{
	// Token: 0x0600474B RID: 18251 RVA: 0x002B431C File Offset: 0x002B331C
	public PrintedChartSizeType ᜀ()
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
		return (PrintedChartSizeType)this.ᜁ;
	}

	// Token: 0x0600474C RID: 18252 RVA: 0x002B4360 File Offset: 0x002B3360
	public void ᜀ(PrintedChartSizeType A_0)
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

	// Token: 0x0600474D RID: 18253 RVA: 0x002B43A4 File Offset: 0x002B33A4
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

	// Token: 0x0600474E RID: 18254 RVA: 0x002B43E0 File Offset: 0x002B33E0
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

	// Token: 0x0600474F RID: 18255 RVA: 0x002B441C File Offset: 0x002B341C
	public spr\u2605()
	{
	}

	// Token: 0x06004750 RID: 18256 RVA: 0x002B4438 File Offset: 0x002B3438
	public spr\u2605(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004751 RID: 18257 RVA: 0x002B4454 File Offset: 0x002B3454
	public spr\u2605(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004752 RID: 18258 RVA: 0x002B4470 File Offset: 0x002B3470
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

	// Token: 0x06004753 RID: 18259 RVA: 0x002B44B8 File Offset: 0x002B34B8
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

	// Token: 0x06004754 RID: 18260 RVA: 0x002B4508 File Offset: 0x002B3508
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

	// Token: 0x04002055 RID: 8277
	public new const int ᜀ = 2;

	// Token: 0x04002056 RID: 8278
	[spr\u2429(0, 2)]
	private ushort ᜁ = 3;
}
