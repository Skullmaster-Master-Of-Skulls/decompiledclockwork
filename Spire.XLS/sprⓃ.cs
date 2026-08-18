using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000397 RID: 919
[spr\u2593(TBIFFRecord.Password)]
[CLSCompliant(false)]
internal class spr\u24C3 : BiffRecordRaw
{
	// Token: 0x0600381D RID: 14365 RVA: 0x001F64E8 File Offset: 0x001F54E8
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
		return this.ᜀ;
	}

	// Token: 0x0600381E RID: 14366 RVA: 0x001F652C File Offset: 0x001F552C
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600381F RID: 14367 RVA: 0x001F6570 File Offset: 0x001F5570
	public virtual int ᜂ()
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

	// Token: 0x06003820 RID: 14368 RVA: 0x001F65AC File Offset: 0x001F55AC
	public virtual int ᜁ()
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

	// Token: 0x06003821 RID: 14369 RVA: 0x001F65E8 File Offset: 0x001F55E8
	public spr\u24C3()
	{
	}

	// Token: 0x06003822 RID: 14370 RVA: 0x001F65FC File Offset: 0x001F55FC
	public spr\u24C3(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003823 RID: 14371 RVA: 0x001F6614 File Offset: 0x001F5614
	public spr\u24C3(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003824 RID: 14372 RVA: 0x001F6628 File Offset: 0x001F5628
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
		this.ᜀ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x06003825 RID: 14373 RVA: 0x001F6670 File Offset: 0x001F5670
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
		A_0.WriteUInt16(A_1, this.ᜀ);
		this.m_iLength = 2;
	}

	// Token: 0x040018C8 RID: 6344
	[spr\u2429(0, 2)]
	private new ushort ᜀ;
}
