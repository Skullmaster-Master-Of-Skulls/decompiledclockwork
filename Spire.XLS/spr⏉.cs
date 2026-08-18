using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000547 RID: 1351
[spr\u2593(TBIFFRecord.AutoFilterInfo)]
[CLSCompliant(false)]
internal class spr\u23C9 : BiffRecordRaw
{
	// Token: 0x06005217 RID: 21015 RVA: 0x00332320 File Offset: 0x00331320
	public virtual bool ᜂ()
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
		return true;
	}

	// Token: 0x06005218 RID: 21016 RVA: 0x0033235C File Offset: 0x0033135C
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
		return 2;
	}

	// Token: 0x06005219 RID: 21017 RVA: 0x00332398 File Offset: 0x00331398
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
		return 2;
	}

	// Token: 0x0600521A RID: 21018 RVA: 0x003323D4 File Offset: 0x003313D4
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
		return this.ᜁ;
	}

	// Token: 0x0600521B RID: 21019 RVA: 0x00332418 File Offset: 0x00331418
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

	// Token: 0x0600521C RID: 21020 RVA: 0x0033245C File Offset: 0x0033145C
	public spr\u23C9()
	{
	}

	// Token: 0x0600521D RID: 21021 RVA: 0x00332470 File Offset: 0x00331470
	public spr\u23C9(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600521E RID: 21022 RVA: 0x00332488 File Offset: 0x00331488
	public spr\u23C9(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600521F RID: 21023 RVA: 0x0033249C File Offset: 0x0033149C
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

	// Token: 0x06005220 RID: 21024 RVA: 0x003324E4 File Offset: 0x003314E4
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

	// Token: 0x0400249E RID: 9374
	private new const int ᜀ = 2;

	// Token: 0x0400249F RID: 9375
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
