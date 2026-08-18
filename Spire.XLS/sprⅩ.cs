using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000321 RID: 801
[spr\u2593(TBIFFRecord.SaveRecalc)]
[CLSCompliant(false)]
internal class spr\u2169 : BiffRecordRaw
{
	// Token: 0x06003176 RID: 12662 RVA: 0x001CA620 File Offset: 0x001C9620
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

	// Token: 0x06003177 RID: 12663 RVA: 0x001CA664 File Offset: 0x001C9664
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

	// Token: 0x06003178 RID: 12664 RVA: 0x001CA6A8 File Offset: 0x001C96A8
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

	// Token: 0x06003179 RID: 12665 RVA: 0x001CA6E4 File Offset: 0x001C96E4
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

	// Token: 0x0600317A RID: 12666 RVA: 0x001CA720 File Offset: 0x001C9720
	public spr\u2169()
	{
	}

	// Token: 0x0600317B RID: 12667 RVA: 0x001CA73C File Offset: 0x001C973C
	public spr\u2169(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600317C RID: 12668 RVA: 0x001CA758 File Offset: 0x001C9758
	public spr\u2169(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600317D RID: 12669 RVA: 0x001CA774 File Offset: 0x001C9774
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

	// Token: 0x0600317E RID: 12670 RVA: 0x001CA7BC File Offset: 0x001C97BC
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

	// Token: 0x040015C7 RID: 5575
	private new const int ᜀ = 2;

	// Token: 0x040015C8 RID: 5576
	[spr\u2429(0, 2)]
	private ushort ᜁ = 1;
}
