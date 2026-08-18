using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002B2 RID: 690
[spr\u2593(TBIFFRecord.InterfaceHdr)]
[CLSCompliant(false)]
internal class spr\u25B6 : BiffRecordRaw
{
	// Token: 0x060029D0 RID: 10704 RVA: 0x00178A74 File Offset: 0x00177A74
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

	// Token: 0x060029D1 RID: 10705 RVA: 0x00178AB8 File Offset: 0x00177AB8
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

	// Token: 0x060029D2 RID: 10706 RVA: 0x00178AFC File Offset: 0x00177AFC
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

	// Token: 0x060029D3 RID: 10707 RVA: 0x00178B38 File Offset: 0x00177B38
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

	// Token: 0x060029D4 RID: 10708 RVA: 0x00178B74 File Offset: 0x00177B74
	public spr\u25B6()
	{
	}

	// Token: 0x060029D5 RID: 10709 RVA: 0x00178B94 File Offset: 0x00177B94
	public spr\u25B6(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060029D6 RID: 10710 RVA: 0x00178BB4 File Offset: 0x00177BB4
	public spr\u25B6(int A_0) : base(A_0)
	{
	}

	// Token: 0x060029D7 RID: 10711 RVA: 0x00178BD4 File Offset: 0x00177BD4
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

	// Token: 0x060029D8 RID: 10712 RVA: 0x00178C1C File Offset: 0x00177C1C
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

	// Token: 0x060029D9 RID: 10713 RVA: 0x00178C6C File Offset: 0x00177C6C
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

	// Token: 0x040013E9 RID: 5097
	private new const int ᜀ = 2;

	// Token: 0x040013EA RID: 5098
	[spr\u2429(0, 2)]
	private ushort ᜁ = 1200;
}
