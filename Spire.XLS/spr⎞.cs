using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004E5 RID: 1253
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartSerFmt)]
internal class spr\u239E : BiffRecordRaw
{
	// Token: 0x06004CD4 RID: 19668 RVA: 0x002EE9A0 File Offset: 0x002ED9A0
	public ushort ᜄ()
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

	// Token: 0x06004CD5 RID: 19669 RVA: 0x002EE9E4 File Offset: 0x002ED9E4
	public bool ᜁ()
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

	// Token: 0x06004CD6 RID: 19670 RVA: 0x002EEA28 File Offset: 0x002EDA28
	public void ᜂ(bool A_0)
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

	// Token: 0x06004CD7 RID: 19671 RVA: 0x002EEA6C File Offset: 0x002EDA6C
	public bool ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x06004CD8 RID: 19672 RVA: 0x002EEAB0 File Offset: 0x002EDAB0
	public void ᜀ(bool A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004CD9 RID: 19673 RVA: 0x002EEAF4 File Offset: 0x002EDAF4
	public bool ᜅ()
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
		return this.ᜄ;
	}

	// Token: 0x06004CDA RID: 19674 RVA: 0x002EEB38 File Offset: 0x002EDB38
	public void ᜁ(bool A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06004CDB RID: 19675 RVA: 0x002EEB7C File Offset: 0x002EDB7C
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
		return 2;
	}

	// Token: 0x06004CDC RID: 19676 RVA: 0x002EEBB8 File Offset: 0x002EDBB8
	public virtual int ᜃ()
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

	// Token: 0x06004CDD RID: 19677 RVA: 0x002EEBF4 File Offset: 0x002EDBF4
	public spr\u239E()
	{
	}

	// Token: 0x06004CDE RID: 19678 RVA: 0x002EEC08 File Offset: 0x002EDC08
	public spr\u239E(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004CDF RID: 19679 RVA: 0x002EEC20 File Offset: 0x002EDC20
	public spr\u239E(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004CE0 RID: 19680 RVA: 0x002EEC34 File Offset: 0x002EDC34
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
		this.ᜂ = A_0.ReadBit(A_1, 0);
		this.ᜃ = A_0.ReadBit(A_1, 1);
		this.ᜄ = A_0.ReadBit(A_1, 2);
	}

	// Token: 0x06004CE1 RID: 19681 RVA: 0x002EECA8 File Offset: 0x002EDCA8
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteBit(A_1, this.ᜂ, 0);
		A_0.WriteBit(A_1, this.ᜃ, 1);
		A_0.WriteBit(A_1, this.ᜄ, 2);
		this.m_iLength = 2;
	}

	// Token: 0x06004CE2 RID: 19682 RVA: 0x002EED24 File Offset: 0x002EDD24
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

	// Token: 0x040022FC RID: 8956
	public new const int ᜀ = 2;

	// Token: 0x040022FD RID: 8957
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x040022FE RID: 8958
	[spr\u2429(0, 0, TFieldType.Bit)]
	private bool ᜂ;

	// Token: 0x040022FF RID: 8959
	[spr\u2429(0, 1, TFieldType.Bit)]
	private new bool ᜃ;

	// Token: 0x04002300 RID: 8960
	[spr\u2429(0, 2, TFieldType.Bit)]
	private bool ᜄ;
}
