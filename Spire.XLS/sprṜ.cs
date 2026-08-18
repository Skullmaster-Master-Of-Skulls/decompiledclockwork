using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003F7 RID: 1015
[spr\u2593(TBIFFRecord.DCON)]
[CLSCompliant(false)]
internal class sprṜ : BiffRecordRaw
{
	// Token: 0x06003D20 RID: 15648 RVA: 0x00221E34 File Offset: 0x00220E34
	public sprṜ()
	{
	}

	// Token: 0x06003D21 RID: 15649 RVA: 0x00221E48 File Offset: 0x00220E48
	public sprṜ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003D22 RID: 15650 RVA: 0x00221E60 File Offset: 0x00220E60
	public sprṜ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003D23 RID: 15651 RVA: 0x00221E74 File Offset: 0x00220E74
	public sprṜ.FunctionTypes ᜀ()
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
		return (sprṜ.FunctionTypes)this.ᜁ;
	}

	// Token: 0x06003D24 RID: 15652 RVA: 0x00221EB8 File Offset: 0x00220EB8
	public void ᜀ(sprṜ.FunctionTypes A_0)
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
		this.ᜁ = (ushort)A_0;
	}

	// Token: 0x06003D25 RID: 15653 RVA: 0x00221EFC File Offset: 0x00220EFC
	public new bool ᜃ()
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
		return this.ᜂ == 1;
	}

	// Token: 0x06003D26 RID: 15654 RVA: 0x00221F40 File Offset: 0x00220F40
	public void ᜂ(bool A_0)
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
		this.ᜂ = (A_0 ? 1 : 0);
	}

	// Token: 0x06003D27 RID: 15655 RVA: 0x00221F90 File Offset: 0x00220F90
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
		return this.ᜃ == 1;
	}

	// Token: 0x06003D28 RID: 15656 RVA: 0x00221FD4 File Offset: 0x00220FD4
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
		this.ᜃ = (A_0 ? 1 : 0);
	}

	// Token: 0x06003D29 RID: 15657 RVA: 0x00222024 File Offset: 0x00221024
	public bool ᜂ()
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
		return this.ᜄ == 1;
	}

	// Token: 0x06003D2A RID: 15658 RVA: 0x00222068 File Offset: 0x00221068
	public void ᜀ(bool A_0)
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
		this.ᜄ = (A_0 ? 1 : 0);
	}

	// Token: 0x06003D2B RID: 15659 RVA: 0x002220B8 File Offset: 0x002210B8
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
	}

	// Token: 0x06003D2C RID: 15660 RVA: 0x00222130 File Offset: 0x00221130
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteUInt16(A_1 + 2, this.ᜂ);
		A_0.WriteUInt16(A_1 + 4, this.ᜃ);
		A_0.WriteUInt16(A_1 + 6, this.ᜄ);
		this.m_iLength = 8;
	}

	// Token: 0x06003D2D RID: 15661 RVA: 0x002221AC File Offset: 0x002211AC
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 8;
	}

	// Token: 0x04001A60 RID: 6752
	private new const int ᜀ = 8;

	// Token: 0x04001A61 RID: 6753
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04001A62 RID: 6754
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x04001A63 RID: 6755
	[spr\u2429(4, 2)]
	private new ushort ᜃ;

	// Token: 0x04001A64 RID: 6756
	[spr\u2429(6, 2)]
	private ushort ᜄ;

	// Token: 0x020003F8 RID: 1016
	public enum FunctionTypes
	{
		// Token: 0x04001A66 RID: 6758
		Average,
		// Token: 0x04001A67 RID: 6759
		CountNums,
		// Token: 0x04001A68 RID: 6760
		Count,
		// Token: 0x04001A69 RID: 6761
		Max,
		// Token: 0x04001A6A RID: 6762
		Min,
		// Token: 0x04001A6B RID: 6763
		Product,
		// Token: 0x04001A6C RID: 6764
		StdDev,
		// Token: 0x04001A6D RID: 6765
		StdDevp,
		// Token: 0x04001A6E RID: 6766
		Sum,
		// Token: 0x04001A6F RID: 6767
		Var,
		// Token: 0x04001A70 RID: 6768
		Varp
	}
}
