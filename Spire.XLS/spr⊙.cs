using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200026F RID: 623
[spr\u2593(TBIFFRecord.ChartPieFormat)]
[CLSCompliant(false)]
internal class spr\u2299 : BiffRecordRaw
{
	// Token: 0x060025AC RID: 9644 RVA: 0x0015CB98 File Offset: 0x0015BB98
	public ushort ᜂ()
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

	// Token: 0x060025AD RID: 9645 RVA: 0x0015CBDC File Offset: 0x0015BBDC
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

	// Token: 0x060025AE RID: 9646 RVA: 0x0015CC20 File Offset: 0x0015BC20
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

	// Token: 0x060025AF RID: 9647 RVA: 0x0015CC5C File Offset: 0x0015BC5C
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

	// Token: 0x060025B0 RID: 9648 RVA: 0x0015CC98 File Offset: 0x0015BC98
	public spr\u2299()
	{
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x0015CCAC File Offset: 0x0015BCAC
	public spr\u2299(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x0015CCC4 File Offset: 0x0015BCC4
	public spr\u2299(int A_0) : base(A_0)
	{
	}

	// Token: 0x060025B3 RID: 9651 RVA: 0x0015CCD8 File Offset: 0x0015BCD8
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

	// Token: 0x060025B4 RID: 9652 RVA: 0x0015CD20 File Offset: 0x0015BD20
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
		A_0.WriteUInt16(A_1, this.ᜁ);
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x0015CD78 File Offset: 0x0015BD78
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

	// Token: 0x040012D3 RID: 4819
	public new const int ᜀ = 2;

	// Token: 0x040012D4 RID: 4820
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
