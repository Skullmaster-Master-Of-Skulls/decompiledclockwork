using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000505 RID: 1285
[spr\u2593(TBIFFRecord.PivotFormula)]
[CLSCompliant(false)]
internal class spr\u241C : BiffRecordRaw
{
	// Token: 0x06004E5B RID: 20059 RVA: 0x002FA514 File Offset: 0x002F9514
	public spr\u241C()
	{
	}

	// Token: 0x06004E5C RID: 20060 RVA: 0x002FA528 File Offset: 0x002F9528
	public spr\u241C(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004E5D RID: 20061 RVA: 0x002FA540 File Offset: 0x002F9540
	public spr\u241C(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004E5E RID: 20062 RVA: 0x002FA554 File Offset: 0x002F9554
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

	// Token: 0x06004E5F RID: 20063 RVA: 0x002FA598 File Offset: 0x002F9598
	public short ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x06004E60 RID: 20064 RVA: 0x002FA5DC File Offset: 0x002F95DC
	public void ᜀ(short A_0)
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

	// Token: 0x06004E61 RID: 20065 RVA: 0x002FA620 File Offset: 0x002F9620
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
		this.ᜂ = A_0.ReadInt16(A_1 + 2);
	}

	// Token: 0x06004E62 RID: 20066 RVA: 0x002FA678 File Offset: 0x002F9678
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
		A_0.WriteInt16(A_1 + 2, this.ᜂ);
		this.m_iLength = 4;
	}

	// Token: 0x06004E63 RID: 20067 RVA: 0x002FA6D8 File Offset: 0x002F96D8
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
		return 4;
	}

	// Token: 0x04002370 RID: 9072
	private new const int ᜀ = 4;

	// Token: 0x04002371 RID: 9073
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04002372 RID: 9074
	[spr\u2429(2, 2, true)]
	private short ᜂ;
}
