using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200039E RID: 926
[spr\u2593(TBIFFRecord.DefaultRowHeight)]
[CLSCompliant(false)]
internal class spr\u2076 : BiffRecordRaw
{
	// Token: 0x0600385B RID: 14427 RVA: 0x001F7A44 File Offset: 0x001F6A44
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
		return this.ᜁ;
	}

	// Token: 0x0600385C RID: 14428 RVA: 0x001F7A88 File Offset: 0x001F6A88
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

	// Token: 0x0600385D RID: 14429 RVA: 0x001F7ACC File Offset: 0x001F6ACC
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

	// Token: 0x0600385E RID: 14430 RVA: 0x001F7B10 File Offset: 0x001F6B10
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

	// Token: 0x0600385F RID: 14431 RVA: 0x001F7B54 File Offset: 0x001F6B54
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

	// Token: 0x06003860 RID: 14432 RVA: 0x001F7B90 File Offset: 0x001F6B90
	public virtual int ᜀ()
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

	// Token: 0x06003861 RID: 14433 RVA: 0x001F7BCC File Offset: 0x001F6BCC
	public spr\u2076()
	{
	}

	// Token: 0x06003862 RID: 14434 RVA: 0x001F7BEC File Offset: 0x001F6BEC
	public spr\u2076(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003863 RID: 14435 RVA: 0x001F7C0C File Offset: 0x001F6C0C
	public spr\u2076(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003864 RID: 14436 RVA: 0x001F7C2C File Offset: 0x001F6C2C
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
		A_1 += 2;
		this.ᜂ = A_0.ReadUInt16(2);
	}

	// Token: 0x06003865 RID: 14437 RVA: 0x001F7C88 File Offset: 0x001F6C88
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
		this.m_iLength = 4;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜂ);
	}

	// Token: 0x040018D4 RID: 6356
	private new const int ᜀ = 4;

	// Token: 0x040018D5 RID: 6357
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x040018D6 RID: 6358
	[spr\u2429(2, 2)]
	private ushort ᜂ = 255;
}
