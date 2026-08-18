using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200056D RID: 1389
[spr\u2593(TBIFFRecord.FnGroupCount)]
[CLSCompliant(false)]
internal class sprḼ : BiffRecordRaw
{
	// Token: 0x0600537D RID: 21373 RVA: 0x0034036C File Offset: 0x0033F36C
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

	// Token: 0x0600537E RID: 21374 RVA: 0x003403B0 File Offset: 0x0033F3B0
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

	// Token: 0x0600537F RID: 21375 RVA: 0x003403F4 File Offset: 0x0033F3F4
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

	// Token: 0x06005380 RID: 21376 RVA: 0x00340430 File Offset: 0x0033F430
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

	// Token: 0x06005381 RID: 21377 RVA: 0x0034046C File Offset: 0x0033F46C
	public sprḼ()
	{
	}

	// Token: 0x06005382 RID: 21378 RVA: 0x00340488 File Offset: 0x0033F488
	public sprḼ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005383 RID: 21379 RVA: 0x003404A8 File Offset: 0x0033F4A8
	public sprḼ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005384 RID: 21380 RVA: 0x003404C4 File Offset: 0x0033F4C4
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

	// Token: 0x06005385 RID: 21381 RVA: 0x0034050C File Offset: 0x0033F50C
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

	// Token: 0x06005386 RID: 21382 RVA: 0x0034055C File Offset: 0x0033F55C
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

	// Token: 0x0400270F RID: 9999
	private new const int ᜀ = 2;

	// Token: 0x04002710 RID: 10000
	[spr\u2429(0, 2)]
	private ushort ᜁ = 14;
}
