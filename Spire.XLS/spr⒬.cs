using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002AE RID: 686
[spr\u2593(TBIFFRecord.PivotSourceInfo)]
[CLSCompliant(false)]
internal class spr\u24AC : BiffRecordRaw
{
	// Token: 0x060029AA RID: 10666 RVA: 0x00178090 File Offset: 0x00177090
	public spr\u24AC()
	{
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x001780A4 File Offset: 0x001770A4
	public spr\u24AC(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x001780BC File Offset: 0x001770BC
	public spr\u24AC(int A_0) : base(A_0)
	{
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x001780D0 File Offset: 0x001770D0
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
		return this.ᜂ;
	}

	// Token: 0x060029AE RID: 10670 RVA: 0x00178114 File Offset: 0x00177114
	public void ᜂ(ushort A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x00178158 File Offset: 0x00177158
	public new ushort ᜃ()
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

	// Token: 0x060029B0 RID: 10672 RVA: 0x0017819C File Offset: 0x0017719C
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
		this.ᜃ = A_0;
	}

	// Token: 0x060029B1 RID: 10673 RVA: 0x001781E0 File Offset: 0x001771E0
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
		return this.ᜄ;
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x00178224 File Offset: 0x00177224
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
		return this.ᜅ;
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x00178268 File Offset: 0x00177268
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

	// Token: 0x060029B4 RID: 10676 RVA: 0x001782AC File Offset: 0x001772AC
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
		return BiffRecordRaw.ᜀ(this.ᜄ, 32767);
	}

	// Token: 0x060029B5 RID: 10677 RVA: 0x001782F8 File Offset: 0x001772F8
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
		BiffRecordRaw.ᜀ(ref this.ᜄ, 32767, A_0);
	}

	// Token: 0x060029B6 RID: 10678 RVA: 0x00178344 File Offset: 0x00177344
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
		this.ᜂ = A_0.ReadUInt16(A_1);
		this.ᜃ = A_0.ReadUInt16(A_1 + 2);
		this.ᜄ = A_0.ReadUInt16(A_1 + 4);
		this.ᜅ = A_0.ReadBit(A_1 + 5, 7);
	}

	// Token: 0x060029B7 RID: 10679 RVA: 0x001783BC File Offset: 0x001773BC
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
		A_0.WriteUInt16(A_1, this.ᜂ);
		A_0.WriteUInt16(A_1 + 2, this.ᜃ);
		A_0.WriteUInt16(A_1 + 4, this.ᜄ);
		A_0.WriteBit(A_1 + 5, this.ᜅ, 7);
		this.m_iLength = 4;
	}

	// Token: 0x060029B8 RID: 10680 RVA: 0x0017843C File Offset: 0x0017743C
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

	// Token: 0x040013DE RID: 5086
	private new const ushort ᜀ = 32767;

	// Token: 0x040013DF RID: 5087
	private const int ᜁ = 4;

	// Token: 0x040013E0 RID: 5088
	[spr\u2429(0, 2)]
	private ushort ᜂ;

	// Token: 0x040013E1 RID: 5089
	[spr\u2429(2, 2)]
	private new ushort ᜃ;

	// Token: 0x040013E2 RID: 5090
	[spr\u2429(4, 2)]
	private ushort ᜄ;

	// Token: 0x040013E3 RID: 5091
	[spr\u2429(5, 7, TFieldType.Bit)]
	private bool ᜅ;
}
