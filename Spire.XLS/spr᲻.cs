using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000263 RID: 611
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.PivotNamePair)]
internal class spr\u1CBB : BiffRecordRaw
{
	// Token: 0x060024A0 RID: 9376 RVA: 0x001550F4 File Offset: 0x001540F4
	public spr\u1CBB()
	{
	}

	// Token: 0x060024A1 RID: 9377 RVA: 0x00155108 File Offset: 0x00154108
	public spr\u1CBB(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060024A2 RID: 9378 RVA: 0x00155120 File Offset: 0x00154120
	public spr\u1CBB(int A_0) : base(A_0)
	{
	}

	// Token: 0x060024A3 RID: 9379 RVA: 0x00155134 File Offset: 0x00154134
	public ushort ᜆ()
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

	// Token: 0x060024A4 RID: 9380 RVA: 0x00155178 File Offset: 0x00154178
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

	// Token: 0x060024A5 RID: 9381 RVA: 0x001551BC File Offset: 0x001541BC
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
		return this.ᜂ;
	}

	// Token: 0x060024A6 RID: 9382 RVA: 0x00155200 File Offset: 0x00154200
	public void ᜁ(ushort A_0)
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

	// Token: 0x060024A7 RID: 9383 RVA: 0x00155244 File Offset: 0x00154244
	public ushort ᜅ()
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
		return this.ᜃ;
	}

	// Token: 0x060024A8 RID: 9384 RVA: 0x00155288 File Offset: 0x00154288
	public new ushort ᜃ()
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

	// Token: 0x060024A9 RID: 9385 RVA: 0x001552CC File Offset: 0x001542CC
	public bool ᜄ()
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
		return this.ᜅ;
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x00155310 File Offset: 0x00154310
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
		this.ᜅ = A_0;
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x00155354 File Offset: 0x00154354
	public bool ᜀ()
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
		return this.ᜆ;
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x00155398 File Offset: 0x00154398
	public void ᜁ(bool A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x001553DC File Offset: 0x001543DC
	public bool ᜂ()
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
		return this.ᜇ;
	}

	// Token: 0x060024AE RID: 9390 RVA: 0x00155420 File Offset: 0x00154420
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
		this.ᜇ = A_0;
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x00155464 File Offset: 0x00154464
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
		this.ᜂ = A_0.ReadUInt16(A_1 + 2);
		this.ᜃ = A_0.ReadUInt16(A_1 + 4);
		this.ᜄ = A_0.ReadUInt16(A_1 + 6);
		this.ᜅ = A_0.ReadBit(A_1 + 6, 0);
		this.ᜆ = A_0.ReadBit(A_1 + 6, 3);
		this.ᜇ = A_0.ReadBit(A_1 + 6, 4);
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x0015550C File Offset: 0x0015450C
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
		A_0.WriteUInt16(A_1 + 2, this.ᜂ);
		A_0.WriteUInt16(A_1 + 4, this.ᜃ);
		A_0.WriteUInt16(A_1 + 6, this.ᜄ);
		A_0.WriteBit(A_1 + 6, this.ᜅ, 0);
		A_0.WriteBit(A_1 + 6, this.ᜆ, 3);
		A_0.WriteBit(A_1 + 6, this.ᜇ, 4);
		this.m_iLength = 8;
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x001555B8 File Offset: 0x001545B8
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

	// Token: 0x04001284 RID: 4740
	private new const int ᜀ = 8;

	// Token: 0x04001285 RID: 4741
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04001286 RID: 4742
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x04001287 RID: 4743
	[spr\u2429(4, 2)]
	private new ushort ᜃ;

	// Token: 0x04001288 RID: 4744
	[spr\u2429(6, 2)]
	private ushort ᜄ;

	// Token: 0x04001289 RID: 4745
	[spr\u2429(6, 0, TFieldType.Bit)]
	private bool ᜅ;

	// Token: 0x0400128A RID: 4746
	[spr\u2429(6, 3, TFieldType.Bit)]
	private bool ᜆ;

	// Token: 0x0400128B RID: 4747
	[spr\u2429(6, 4, TFieldType.Bit)]
	private bool ᜇ;
}
