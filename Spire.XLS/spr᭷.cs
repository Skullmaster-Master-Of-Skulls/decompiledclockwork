using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200048B RID: 1163
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartPie)]
internal class spr\u1B77 : BiffRecordRaw
{
	// Token: 0x0600477A RID: 18298 RVA: 0x002B500C File Offset: 0x002B400C
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

	// Token: 0x0600477B RID: 18299 RVA: 0x002B5050 File Offset: 0x002B4050
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
		this.ᜁ = A_0;
	}

	// Token: 0x0600477C RID: 18300 RVA: 0x002B5094 File Offset: 0x002B4094
	public ushort ᜆ()
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

	// Token: 0x0600477D RID: 18301 RVA: 0x002B50D8 File Offset: 0x002B40D8
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

	// Token: 0x0600477E RID: 18302 RVA: 0x002B511C File Offset: 0x002B411C
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
		return this.ᜃ;
	}

	// Token: 0x0600477F RID: 18303 RVA: 0x002B5160 File Offset: 0x002B4160
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004780 RID: 18304 RVA: 0x002B51A4 File Offset: 0x002B41A4
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
		return this.ᜄ;
	}

	// Token: 0x06004781 RID: 18305 RVA: 0x002B51E8 File Offset: 0x002B41E8
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

	// Token: 0x06004782 RID: 18306 RVA: 0x002B522C File Offset: 0x002B422C
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
		return this.ᜅ;
	}

	// Token: 0x06004783 RID: 18307 RVA: 0x002B5270 File Offset: 0x002B4270
	public void ᜀ(bool A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06004784 RID: 18308 RVA: 0x002B52B4 File Offset: 0x002B42B4
	public virtual int ᜂ()
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
		return 6;
	}

	// Token: 0x06004785 RID: 18309 RVA: 0x002B52F0 File Offset: 0x002B42F0
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
		return 6;
	}

	// Token: 0x06004786 RID: 18310 RVA: 0x002B532C File Offset: 0x002B432C
	public spr\u1B77()
	{
	}

	// Token: 0x06004787 RID: 18311 RVA: 0x002B5340 File Offset: 0x002B4340
	public spr\u1B77(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004788 RID: 18312 RVA: 0x002B5358 File Offset: 0x002B4358
	public spr\u1B77(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004789 RID: 18313 RVA: 0x002B536C File Offset: 0x002B436C
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
		this.ᜄ = A_0.ReadBit(A_1 + 4, 0);
		this.ᜅ = A_0.ReadBit(A_1 + 4, 1);
	}

	// Token: 0x0600478A RID: 18314 RVA: 0x002B53F4 File Offset: 0x002B43F4
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
		A_0.WriteBit(A_1 + 4, this.ᜄ, 0);
		A_0.WriteBit(A_1 + 4, this.ᜅ, 1);
		this.m_iLength = 6;
	}

	// Token: 0x04002066 RID: 8294
	public new const int ᜀ = 6;

	// Token: 0x04002067 RID: 8295
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04002068 RID: 8296
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x04002069 RID: 8297
	[spr\u2429(4, 2)]
	private new ushort ᜃ;

	// Token: 0x0400206A RID: 8298
	[spr\u2429(4, 0, TFieldType.Bit)]
	private bool ᜄ;

	// Token: 0x0400206B RID: 8299
	[spr\u2429(4, 1, TFieldType.Bit)]
	private bool ᜅ;
}
