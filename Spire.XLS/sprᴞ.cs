using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200056B RID: 1387
[spr\u2593(TBIFFRecord.Gridset)]
[CLSCompliant(false)]
internal class sprᴞ : BiffRecordRaw
{
	// Token: 0x06005368 RID: 21352 RVA: 0x0033FDD4 File Offset: 0x0033EDD4
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

	// Token: 0x06005369 RID: 21353 RVA: 0x0033FE18 File Offset: 0x0033EE18
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

	// Token: 0x0600536A RID: 21354 RVA: 0x0033FE5C File Offset: 0x0033EE5C
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

	// Token: 0x0600536B RID: 21355 RVA: 0x0033FE98 File Offset: 0x0033EE98
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

	// Token: 0x0600536C RID: 21356 RVA: 0x0033FED4 File Offset: 0x0033EED4
	public sprᴞ()
	{
	}

	// Token: 0x0600536D RID: 21357 RVA: 0x0033FEF0 File Offset: 0x0033EEF0
	public sprᴞ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600536E RID: 21358 RVA: 0x0033FF0C File Offset: 0x0033EF0C
	public sprᴞ(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600536F RID: 21359 RVA: 0x0033FF28 File Offset: 0x0033EF28
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

	// Token: 0x06005370 RID: 21360 RVA: 0x0033FF70 File Offset: 0x0033EF70
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

	// Token: 0x06005371 RID: 21361 RVA: 0x0033FFC0 File Offset: 0x0033EFC0
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

	// Token: 0x0400270A RID: 9994
	private new const int ᜀ = 2;

	// Token: 0x0400270B RID: 9995
	[spr\u2429(0, 2)]
	private ushort ᜁ = 1;
}
