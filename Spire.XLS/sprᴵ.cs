using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000407 RID: 1031
[spr\u2593(TBIFFRecord.ChartSeriesText)]
[CLSCompliant(false)]
internal class spr\u1D35 : BiffRecordRaw
{
	// Token: 0x06003E0D RID: 15885 RVA: 0x00228DA4 File Offset: 0x00227DA4
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

	// Token: 0x06003E0E RID: 15886 RVA: 0x00228DE8 File Offset: 0x00227DE8
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

	// Token: 0x06003E0F RID: 15887 RVA: 0x00228E2C File Offset: 0x00227E2C
	public string ᜁ()
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

	// Token: 0x06003E10 RID: 15888 RVA: 0x00228E70 File Offset: 0x00227E70
	public void ᜀ(string A_0)
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

	// Token: 0x06003E11 RID: 15889 RVA: 0x00228EB4 File Offset: 0x00227EB4
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
		return 3;
	}

	// Token: 0x06003E12 RID: 15890 RVA: 0x00228EF0 File Offset: 0x00227EF0
	public spr\u1D35()
	{
	}

	// Token: 0x06003E13 RID: 15891 RVA: 0x00228F10 File Offset: 0x00227F10
	public spr\u1D35(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003E14 RID: 15892 RVA: 0x00228F30 File Offset: 0x00227F30
	public spr\u1D35(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003E15 RID: 15893 RVA: 0x00228F50 File Offset: 0x00227F50
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
		int num;
		this.ᜂ = A_0.ReadString8Bit(A_1 + 2, out num);
	}

	// Token: 0x06003E16 RID: 15894 RVA: 0x00228FAC File Offset: 0x00227FAC
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
		int num = A_1;
		A_1 += 2;
		A_0.WriteString8BitUpdateOffset(ref A_1, this.ᜂ);
		this.m_iLength = A_1 - num;
	}

	// Token: 0x06003E17 RID: 15895 RVA: 0x00229014 File Offset: 0x00228014
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
		return 4 + this.ᜂ.Length * 2;
	}

	// Token: 0x04001AAC RID: 6828
	public new const int ᜀ = 3;

	// Token: 0x04001AAD RID: 6829
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04001AAE RID: 6830
	[spr\u2429(2, 1, TFieldType.String)]
	private string ᜂ = string.Empty;
}
