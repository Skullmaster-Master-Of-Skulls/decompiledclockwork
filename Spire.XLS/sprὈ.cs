using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200053C RID: 1340
[spr\u2593(TBIFFRecord.ChartSertocrt)]
[CLSCompliant(false)]
internal class sprὈ : BiffRecordRaw
{
	// Token: 0x0600519A RID: 20890 RVA: 0x0032F2A8 File Offset: 0x0032E2A8
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

	// Token: 0x0600519B RID: 20891 RVA: 0x0032F2EC File Offset: 0x0032E2EC
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

	// Token: 0x0600519C RID: 20892 RVA: 0x0032F330 File Offset: 0x0032E330
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

	// Token: 0x0600519D RID: 20893 RVA: 0x0032F36C File Offset: 0x0032E36C
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

	// Token: 0x0600519E RID: 20894 RVA: 0x0032F3A8 File Offset: 0x0032E3A8
	public sprὈ()
	{
	}

	// Token: 0x0600519F RID: 20895 RVA: 0x0032F3BC File Offset: 0x0032E3BC
	public sprὈ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060051A0 RID: 20896 RVA: 0x0032F3D4 File Offset: 0x0032E3D4
	public sprὈ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060051A1 RID: 20897 RVA: 0x0032F3E8 File Offset: 0x0032E3E8
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

	// Token: 0x060051A2 RID: 20898 RVA: 0x0032F430 File Offset: 0x0032E430
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
		A_0.WriteUInt16(A_1, this.ᜁ);
	}

	// Token: 0x060051A3 RID: 20899 RVA: 0x0032F488 File Offset: 0x0032E488
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

	// Token: 0x04002463 RID: 9315
	public new const int ᜀ = 2;

	// Token: 0x04002464 RID: 9316
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
