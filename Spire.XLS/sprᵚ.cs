using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200048D RID: 1165
[spr\u2593(TBIFFRecord.ChartArea)]
[CLSCompliant(false)]
internal class spr\u1D5A : BiffRecordRaw, spr\u1C7F
{
	// Token: 0x0600479D RID: 18333 RVA: 0x002B5A1C File Offset: 0x002B4A1C
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

	// Token: 0x0600479E RID: 18334 RVA: 0x002B5A60 File Offset: 0x002B4A60
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

	// Token: 0x0600479F RID: 18335 RVA: 0x002B5AA4 File Offset: 0x002B4AA4
	public new void ᜃ(bool A_0)
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

	// Token: 0x060047A0 RID: 18336 RVA: 0x002B5AE8 File Offset: 0x002B4AE8
	public bool ᜂ()
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

	// Token: 0x060047A1 RID: 18337 RVA: 0x002B5B2C File Offset: 0x002B4B2C
	public void ᜂ(bool A_0)
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

	// Token: 0x060047A2 RID: 18338 RVA: 0x002B5B70 File Offset: 0x002B4B70
	public new bool ᜃ()
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

	// Token: 0x060047A3 RID: 18339 RVA: 0x002B5BB4 File Offset: 0x002B4BB4
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

	// Token: 0x060047A4 RID: 18340 RVA: 0x002B5BF8 File Offset: 0x002B4BF8
	public spr\u1D5A()
	{
	}

	// Token: 0x060047A5 RID: 18341 RVA: 0x002B5C0C File Offset: 0x002B4C0C
	public spr\u1D5A(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060047A6 RID: 18342 RVA: 0x002B5C24 File Offset: 0x002B4C24
	public spr\u1D5A(int A_0) : base(A_0)
	{
	}

	// Token: 0x060047A7 RID: 18343 RVA: 0x002B5C38 File Offset: 0x002B4C38
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
		this.ᜂ = A_0.ReadBit(A_1, 0);
		this.ᜃ = A_0.ReadBit(A_1, 1);
		this.ᜄ = A_0.ReadBit(A_1, 2);
	}

	// Token: 0x060047A8 RID: 18344 RVA: 0x002B5CAC File Offset: 0x002B4CAC
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
		this.ᜁ &= 7;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteBit(A_1, this.ᜂ, 0);
		A_0.WriteBit(A_1, this.ᜃ, 1);
		A_0.WriteBit(A_1, this.ᜄ, 2);
		this.m_iLength = 2;
	}

	// Token: 0x060047A9 RID: 18345 RVA: 0x002B5D34 File Offset: 0x002B4D34
	public virtual int ᜀ(ExcelVersion A_0)
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

	// Token: 0x060047AA RID: 18346 RVA: 0x002B5D70 File Offset: 0x002B4D70
	bool spr\u1C7F.ᜀ()
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
		return this.ᜂ();
	}

	// Token: 0x060047AB RID: 18347 RVA: 0x002B5DB4 File Offset: 0x002B4DB4
	void spr\u1C7F.ᜄ(bool A_0)
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
		this.ᜂ(A_0);
	}

	// Token: 0x060047AC RID: 18348 RVA: 0x002B5DF8 File Offset: 0x002B4DF8
	bool spr\u1C7F.ᜅ()
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
		return this.ᜁ();
	}

	// Token: 0x060047AD RID: 18349 RVA: 0x002B5E3C File Offset: 0x002B4E3C
	void spr\u1C7F.ᜀ(bool A_0)
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
		this.ᜃ(A_0);
	}

	// Token: 0x04002074 RID: 8308
	private new const int ᜀ = 2;

	// Token: 0x04002075 RID: 8309
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04002076 RID: 8310
	[spr\u2429(0, 0, TFieldType.Bit)]
	private bool ᜂ;

	// Token: 0x04002077 RID: 8311
	[spr\u2429(0, 1, TFieldType.Bit)]
	private new bool ᜃ;

	// Token: 0x04002078 RID: 8312
	[spr\u2429(0, 2, TFieldType.Bit)]
	private bool ᜄ;
}
