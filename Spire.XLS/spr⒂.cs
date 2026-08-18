using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004FF RID: 1279
[spr\u2593(TBIFFRecord.RefMode)]
[CLSCompliant(false)]
internal class spr\u2482 : BiffRecordRaw
{
	// Token: 0x06004E0C RID: 19980 RVA: 0x002F8E30 File Offset: 0x002F7E30
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

	// Token: 0x06004E0D RID: 19981 RVA: 0x002F8E74 File Offset: 0x002F7E74
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

	// Token: 0x06004E0E RID: 19982 RVA: 0x002F8EB8 File Offset: 0x002F7EB8
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

	// Token: 0x06004E0F RID: 19983 RVA: 0x002F8EF4 File Offset: 0x002F7EF4
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

	// Token: 0x06004E10 RID: 19984 RVA: 0x002F8F30 File Offset: 0x002F7F30
	public spr\u2482()
	{
	}

	// Token: 0x06004E11 RID: 19985 RVA: 0x002F8F4C File Offset: 0x002F7F4C
	public spr\u2482(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004E12 RID: 19986 RVA: 0x002F8F68 File Offset: 0x002F7F68
	public spr\u2482(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004E13 RID: 19987 RVA: 0x002F8F84 File Offset: 0x002F7F84
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

	// Token: 0x06004E14 RID: 19988 RVA: 0x002F8FCC File Offset: 0x002F7FCC
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

	// Token: 0x0400234B RID: 9035
	private new const int ᜀ = 2;

	// Token: 0x0400234C RID: 9036
	[spr\u2429(0, 2)]
	private ushort ᜁ = 1;
}
