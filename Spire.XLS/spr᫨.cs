using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000393 RID: 915
[spr\u2593(TBIFFRecord.Protect)]
[CLSCompliant(false)]
internal class spr\u1AE8 : BiffRecordRaw
{
	// Token: 0x060037B9 RID: 14265 RVA: 0x001F4670 File Offset: 0x001F3670
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
		return this.ᜀ == 1;
	}

	// Token: 0x060037BA RID: 14266 RVA: 0x001F46B4 File Offset: 0x001F36B4
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
		this.ᜀ = (A_0 ? 1 : 0);
	}

	// Token: 0x060037BB RID: 14267 RVA: 0x001F4704 File Offset: 0x001F3704
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

	// Token: 0x060037BC RID: 14268 RVA: 0x001F4740 File Offset: 0x001F3740
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

	// Token: 0x060037BD RID: 14269 RVA: 0x001F477C File Offset: 0x001F377C
	public spr\u1AE8()
	{
	}

	// Token: 0x060037BE RID: 14270 RVA: 0x001F4790 File Offset: 0x001F3790
	public spr\u1AE8(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060037BF RID: 14271 RVA: 0x001F47A8 File Offset: 0x001F37A8
	public spr\u1AE8(int A_0) : base(A_0)
	{
	}

	// Token: 0x060037C0 RID: 14272 RVA: 0x001F47BC File Offset: 0x001F37BC
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

	// Token: 0x060037C1 RID: 14273 RVA: 0x001F4804 File Offset: 0x001F3804
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

	// Token: 0x0400188B RID: 6283
	[spr\u2429(0, 2)]
	private new ushort ᜀ;
}
