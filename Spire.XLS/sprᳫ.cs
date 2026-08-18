using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004E7 RID: 1255
[spr\u2593(TBIFFRecord.ChartFrame)]
[CLSCompliant(false)]
internal class sprᳫ : BiffRecordRaw
{
	// Token: 0x06004CF3 RID: 19699 RVA: 0x002EF168 File Offset: 0x002EE168
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

	// Token: 0x06004CF4 RID: 19700 RVA: 0x002EF1AC File Offset: 0x002EE1AC
	public new RectangleStyleType ᜃ()
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
		return (RectangleStyleType)this.ᜁ;
	}

	// Token: 0x06004CF5 RID: 19701 RVA: 0x002EF1F0 File Offset: 0x002EE1F0
	public void ᜀ(RectangleStyleType A_0)
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
		this.ᜁ = (ushort)A_0;
	}

	// Token: 0x06004CF6 RID: 19702 RVA: 0x002EF234 File Offset: 0x002EE234
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

	// Token: 0x06004CF7 RID: 19703 RVA: 0x002EF278 File Offset: 0x002EE278
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004CF8 RID: 19704 RVA: 0x002EF2BC File Offset: 0x002EE2BC
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

	// Token: 0x06004CF9 RID: 19705 RVA: 0x002EF300 File Offset: 0x002EE300
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
		this.ᜄ = A_0;
	}

	// Token: 0x06004CFA RID: 19706 RVA: 0x002EF344 File Offset: 0x002EE344
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
		return 4;
	}

	// Token: 0x06004CFB RID: 19707 RVA: 0x002EF380 File Offset: 0x002EE380
	public virtual int ᜁ()
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

	// Token: 0x06004CFC RID: 19708 RVA: 0x002EF3BC File Offset: 0x002EE3BC
	public sprᳫ()
	{
	}

	// Token: 0x06004CFD RID: 19709 RVA: 0x002EF3E0 File Offset: 0x002EE3E0
	public sprᳫ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004CFE RID: 19710 RVA: 0x002EF404 File Offset: 0x002EE404
	public sprᳫ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004CFF RID: 19711 RVA: 0x002EF428 File Offset: 0x002EE428
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
		A_1 += 2;
		this.ᜂ = A_0.ReadUInt16(A_1);
		this.ᜃ = A_0.ReadBit(A_1, 0);
		this.ᜄ = A_0.ReadBit(A_1, 1);
	}

	// Token: 0x06004D00 RID: 19712 RVA: 0x002EF4A0 File Offset: 0x002EE4A0
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜂ);
		A_0.WriteBit(A_1, this.ᜃ, 0);
		A_0.WriteBit(A_1, this.ᜄ, 1);
	}

	// Token: 0x06004D01 RID: 19713 RVA: 0x002EF524 File Offset: 0x002EE524
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

	// Token: 0x04002306 RID: 8966
	public new const int ᜀ = 4;

	// Token: 0x04002307 RID: 8967
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04002308 RID: 8968
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x04002309 RID: 8969
	[spr\u2429(2, 0, TFieldType.Bit)]
	private new bool ᜃ = true;

	// Token: 0x0400230A RID: 8970
	[spr\u2429(2, 1, TFieldType.Bit)]
	private bool ᜄ = true;
}
