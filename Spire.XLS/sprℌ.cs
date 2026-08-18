using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000342 RID: 834
[spr\u2593(TBIFFRecord.CalCount)]
[CLSCompliant(false)]
internal class sprℌ : BiffRecordRaw
{
	// Token: 0x060032E1 RID: 13025 RVA: 0x001D32D8 File Offset: 0x001D22D8
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
		return this.ᜁ;
	}

	// Token: 0x060032E2 RID: 13026 RVA: 0x001D331C File Offset: 0x001D231C
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

	// Token: 0x060032E3 RID: 13027 RVA: 0x001D3360 File Offset: 0x001D2360
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

	// Token: 0x060032E4 RID: 13028 RVA: 0x001D339C File Offset: 0x001D239C
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

	// Token: 0x060032E5 RID: 13029 RVA: 0x001D33D8 File Offset: 0x001D23D8
	public sprℌ()
	{
	}

	// Token: 0x060032E6 RID: 13030 RVA: 0x001D33F4 File Offset: 0x001D23F4
	public sprℌ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060032E7 RID: 13031 RVA: 0x001D3414 File Offset: 0x001D2414
	public sprℌ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060032E8 RID: 13032 RVA: 0x001D3430 File Offset: 0x001D2430
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
	}

	// Token: 0x060032E9 RID: 13033 RVA: 0x001D3478 File Offset: 0x001D2478
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
		this.m_iLength = 2;
		A_0.WriteUInt16(A_1, this.ᜁ);
	}

	// Token: 0x04001639 RID: 5689
	private new const int ᜀ = 2;

	// Token: 0x0400163A RID: 5690
	[spr\u2429(0, 2)]
	private ushort ᜁ = 100;
}
