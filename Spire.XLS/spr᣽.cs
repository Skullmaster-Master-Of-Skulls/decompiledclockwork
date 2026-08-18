using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000577 RID: 1399
[spr\u2593(TBIFFRecord.CalcMode)]
[CLSCompliant(false)]
internal class spr\u18FD : BiffRecordRaw
{
	// Token: 0x06005405 RID: 21509 RVA: 0x00343004 File Offset: 0x00342004
	public ExcelCalculationMode ᜀ()
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
		return (ExcelCalculationMode)this.ᜀ;
	}

	// Token: 0x06005406 RID: 21510 RVA: 0x00343048 File Offset: 0x00342048
	public void ᜀ(ExcelCalculationMode A_0)
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
		this.ᜀ = (ushort)A_0;
	}

	// Token: 0x06005407 RID: 21511 RVA: 0x0034308C File Offset: 0x0034208C
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

	// Token: 0x06005408 RID: 21512 RVA: 0x003430C8 File Offset: 0x003420C8
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

	// Token: 0x06005409 RID: 21513 RVA: 0x00343104 File Offset: 0x00342104
	public spr\u18FD()
	{
	}

	// Token: 0x0600540A RID: 21514 RVA: 0x00343120 File Offset: 0x00342120
	public spr\u18FD(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600540B RID: 21515 RVA: 0x0034313C File Offset: 0x0034213C
	public spr\u18FD(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600540C RID: 21516 RVA: 0x00343158 File Offset: 0x00342158
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

	// Token: 0x0600540D RID: 21517 RVA: 0x003431A0 File Offset: 0x003421A0
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

	// Token: 0x0400274B RID: 10059
	[spr\u2429(0, 2)]
	private new ushort ᜀ = 1;
}
