using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200042F RID: 1071
[spr\u2593(TBIFFRecord.ChartPlotGrowth)]
[CLSCompliant(false)]
internal class sprᥦ : BiffRecordRaw
{
	// Token: 0x060040C9 RID: 16585 RVA: 0x0024501C File Offset: 0x0024401C
	public new uint ᜃ()
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

	// Token: 0x060040CA RID: 16586 RVA: 0x00245060 File Offset: 0x00244060
	public void ᜁ(uint A_0)
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

	// Token: 0x060040CB RID: 16587 RVA: 0x002450A4 File Offset: 0x002440A4
	public uint ᜁ()
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

	// Token: 0x060040CC RID: 16588 RVA: 0x002450E8 File Offset: 0x002440E8
	public void ᜀ(uint A_0)
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

	// Token: 0x060040CD RID: 16589 RVA: 0x0024512C File Offset: 0x0024412C
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
		return 8;
	}

	// Token: 0x060040CE RID: 16590 RVA: 0x00245168 File Offset: 0x00244168
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
		return 8;
	}

	// Token: 0x060040CF RID: 16591 RVA: 0x002451A4 File Offset: 0x002441A4
	public sprᥦ()
	{
	}

	// Token: 0x060040D0 RID: 16592 RVA: 0x002451D0 File Offset: 0x002441D0
	public sprᥦ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060040D1 RID: 16593 RVA: 0x002451FC File Offset: 0x002441FC
	public sprᥦ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060040D2 RID: 16594 RVA: 0x00245228 File Offset: 0x00244228
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
		this.ᜁ = A_0.ReadUInt32(A_1);
		A_1 += 4;
		this.ᜂ = A_0.ReadUInt32(A_1);
	}

	// Token: 0x060040D3 RID: 16595 RVA: 0x00245284 File Offset: 0x00244284
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
		A_0.WriteUInt32(A_1, this.ᜁ);
		A_1 += 4;
		A_0.WriteUInt32(A_1, this.ᜂ);
	}

	// Token: 0x060040D4 RID: 16596 RVA: 0x002452EC File Offset: 0x002442EC
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
		return 8;
	}

	// Token: 0x04001CEB RID: 7403
	public new const int ᜀ = 8;

	// Token: 0x04001CEC RID: 7404
	[spr\u2429(0, 4)]
	private uint ᜁ = 65536U;

	// Token: 0x04001CED RID: 7405
	[spr\u2429(4, 4)]
	private uint ᜂ = 65536U;
}
