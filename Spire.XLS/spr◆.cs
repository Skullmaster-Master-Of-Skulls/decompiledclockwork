using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000452 RID: 1106
[spr\u2593(TBIFFRecord.Chart3DDataFormat)]
[CLSCompliant(false)]
internal class spr\u25C6 : BiffRecordRaw
{
	// Token: 0x060042B0 RID: 17072 RVA: 0x00255B40 File Offset: 0x00254B40
	public BaseFormatType ᜀ()
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
		return (BaseFormatType)this.ᜁ;
	}

	// Token: 0x060042B1 RID: 17073 RVA: 0x00255B84 File Offset: 0x00254B84
	public void ᜀ(BaseFormatType A_0)
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

	// Token: 0x060042B2 RID: 17074 RVA: 0x00255BC8 File Offset: 0x00254BC8
	public TopFormatType ᜁ()
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
		return (TopFormatType)this.ᜂ;
	}

	// Token: 0x060042B3 RID: 17075 RVA: 0x00255C0C File Offset: 0x00254C0C
	public void ᜀ(TopFormatType A_0)
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

	// Token: 0x060042B4 RID: 17076 RVA: 0x00255C50 File Offset: 0x00254C50
	public spr\u25C6()
	{
	}

	// Token: 0x060042B5 RID: 17077 RVA: 0x00255C64 File Offset: 0x00254C64
	public spr\u25C6(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060042B6 RID: 17078 RVA: 0x00255C7C File Offset: 0x00254C7C
	public spr\u25C6(int A_0) : base(A_0)
	{
	}

	// Token: 0x060042B7 RID: 17079 RVA: 0x00255C90 File Offset: 0x00254C90
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
	}

	// Token: 0x060042B8 RID: 17080 RVA: 0x00255CE8 File Offset: 0x00254CE8
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteByte(A_1, this.ᜁ);
		A_0.WriteByte(A_1 + 1, this.ᜂ);
	}

	// Token: 0x060042B9 RID: 17081 RVA: 0x00255D4C File Offset: 0x00254D4C
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

	// Token: 0x04001D90 RID: 7568
	private new const int ᜀ = 2;

	// Token: 0x04001D91 RID: 7569
	[spr\u2429(0, 1)]
	private byte ᜁ;

	// Token: 0x04001D92 RID: 7570
	[spr\u2429(1, 1)]
	private byte ᜂ;
}
