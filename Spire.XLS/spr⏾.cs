using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003D6 RID: 982
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartSerAuxErrBar)]
internal class spr\u23FE : BiffRecordRaw
{
	// Token: 0x06003BA1 RID: 15265 RVA: 0x00215AF4 File Offset: 0x00214AF4
	public spr\u23FE.TErrorBarValue ᜀ()
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
		return (spr\u23FE.TErrorBarValue)this.ᜁ;
	}

	// Token: 0x06003BA2 RID: 15266 RVA: 0x00215B38 File Offset: 0x00214B38
	public void ᜀ(spr\u23FE.TErrorBarValue A_0)
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
		this.ᜁ = (byte)A_0;
	}

	// Token: 0x06003BA3 RID: 15267 RVA: 0x00215B7C File Offset: 0x00214B7C
	public ErrorBarType ᜄ()
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
		return (ErrorBarType)this.ᜂ;
	}

	// Token: 0x06003BA4 RID: 15268 RVA: 0x00215BC0 File Offset: 0x00214BC0
	public void ᜀ(ErrorBarType A_0)
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
		this.ᜂ = (byte)A_0;
	}

	// Token: 0x06003BA5 RID: 15269 RVA: 0x00215C04 File Offset: 0x00214C04
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
		return this.ᜃ == 1;
	}

	// Token: 0x06003BA6 RID: 15270 RVA: 0x00215C48 File Offset: 0x00214C48
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
		this.ᜃ = (A_0 ? 1 : 0);
	}

	// Token: 0x06003BA7 RID: 15271 RVA: 0x00215C98 File Offset: 0x00214C98
	public byte ᜁ()
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

	// Token: 0x06003BA8 RID: 15272 RVA: 0x00215CDC File Offset: 0x00214CDC
	public double ᜂ()
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

	// Token: 0x06003BA9 RID: 15273 RVA: 0x00215D20 File Offset: 0x00214D20
	public void ᜀ(double A_0)
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

	// Token: 0x06003BAA RID: 15274 RVA: 0x00215D64 File Offset: 0x00214D64
	public ushort ᜅ()
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
		return this.ᜆ;
	}

	// Token: 0x06003BAB RID: 15275 RVA: 0x00215DA8 File Offset: 0x00214DA8
	public void ᜀ(ushort A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06003BAC RID: 15276 RVA: 0x00215DEC File Offset: 0x00214DEC
	public spr\u23FE()
	{
	}

	// Token: 0x06003BAD RID: 15277 RVA: 0x00215E24 File Offset: 0x00214E24
	public spr\u23FE(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003BAE RID: 15278 RVA: 0x00215E60 File Offset: 0x00214E60
	public spr\u23FE(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003BAF RID: 15279 RVA: 0x00215E98 File Offset: 0x00214E98
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
		this.ᜁ = A_0.ReadByte(A_1);
		this.ᜂ = A_0.ReadByte(A_1 + 1);
		this.ᜃ = A_0.ReadByte(A_1 + 2);
		this.ᜄ = A_0.ReadByte(A_1 + 3);
		this.ᜅ = A_0.ReadDouble(A_1 + 4);
		this.ᜆ = A_0.ReadUInt16(A_1 + 12);
	}

	// Token: 0x06003BB0 RID: 15280 RVA: 0x00215F2C File Offset: 0x00214F2C
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
		A_0.WriteByte(A_1, this.ᜁ);
		A_0.WriteByte(A_1 + 1, this.ᜂ);
		A_0.WriteByte(A_1 + 2, this.ᜃ);
		A_0.WriteByte(A_1 + 3, this.ᜄ);
		A_0.WriteDouble(A_1 + 4, this.ᜅ);
		A_0.WriteUInt16(A_1 + 12, this.ᜆ);
		this.m_iLength = 14;
	}

	// Token: 0x06003BB1 RID: 15281 RVA: 0x00215FC8 File Offset: 0x00214FC8
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
		return 14;
	}

	// Token: 0x040019DF RID: 6623
	public new const int ᜀ = 14;

	// Token: 0x040019E0 RID: 6624
	[spr\u2429(0, 1)]
	private byte ᜁ;

	// Token: 0x040019E1 RID: 6625
	[spr\u2429(1, 1)]
	private byte ᜂ = 2;

	// Token: 0x040019E2 RID: 6626
	[spr\u2429(2, 1)]
	private new byte ᜃ = 1;

	// Token: 0x040019E3 RID: 6627
	[spr\u2429(3, 1)]
	private byte ᜄ = 1;

	// Token: 0x040019E4 RID: 6628
	[spr\u2429(4, 8, TFieldType.Float)]
	private double ᜅ = 10.0;

	// Token: 0x040019E5 RID: 6629
	[spr\u2429(12, 2)]
	private ushort ᜆ;

	// Token: 0x020003D7 RID: 983
	public enum TErrorBarValue
	{
		// Token: 0x040019E7 RID: 6631
		XDirectionPlus = 1,
		// Token: 0x040019E8 RID: 6632
		XDirectionMinus,
		// Token: 0x040019E9 RID: 6633
		YDirectionPlus,
		// Token: 0x040019EA RID: 6634
		YDirectionMinus
	}
}
