using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200035D RID: 861
[spr\u2593(TBIFFRecord.ProtectionRev4)]
[CLSCompliant(false)]
internal class spr\u237D : BiffRecordRaw
{
	// Token: 0x06003493 RID: 13459 RVA: 0x001E33F8 File Offset: 0x001E23F8
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

	// Token: 0x06003494 RID: 13460 RVA: 0x001E343C File Offset: 0x001E243C
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

	// Token: 0x06003495 RID: 13461 RVA: 0x001E3480 File Offset: 0x001E2480
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

	// Token: 0x06003496 RID: 13462 RVA: 0x001E34BC File Offset: 0x001E24BC
	public virtual int ᜀ()
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

	// Token: 0x06003497 RID: 13463 RVA: 0x001E34F8 File Offset: 0x001E24F8
	public spr\u237D()
	{
	}

	// Token: 0x06003498 RID: 13464 RVA: 0x001E350C File Offset: 0x001E250C
	public spr\u237D(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003499 RID: 13465 RVA: 0x001E3524 File Offset: 0x001E2524
	public spr\u237D(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600349A RID: 13466 RVA: 0x001E3538 File Offset: 0x001E2538
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

	// Token: 0x0600349B RID: 13467 RVA: 0x001E3580 File Offset: 0x001E2580
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
		this.m_iLength = 2;
	}

	// Token: 0x0600349C RID: 13468 RVA: 0x001E35D0 File Offset: 0x001E25D0
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

	// Token: 0x040016F6 RID: 5878
	private new const int ᜀ = 2;

	// Token: 0x040016F7 RID: 5879
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
