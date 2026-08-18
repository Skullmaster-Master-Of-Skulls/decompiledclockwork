using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000514 RID: 1300
[spr\u2593(TBIFFRecord.ChartAxesUsed)]
[CLSCompliant(false)]
internal class sprỴ : BiffRecordRaw
{
	// Token: 0x06004EFF RID: 20223 RVA: 0x002FDAB4 File Offset: 0x002FCAB4
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

	// Token: 0x06004F00 RID: 20224 RVA: 0x002FDAF8 File Offset: 0x002FCAF8
	public void ᜀ(ushort A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					this.ᜀ = A_0;
					num = 1;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 == this.ᜀ)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06004F01 RID: 20225 RVA: 0x002FDB74 File Offset: 0x002FCB74
	public sprỴ()
	{
	}

	// Token: 0x06004F02 RID: 20226 RVA: 0x002FDB88 File Offset: 0x002FCB88
	public sprỴ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004F03 RID: 20227 RVA: 0x002FDBA0 File Offset: 0x002FCBA0
	public sprỴ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004F04 RID: 20228 RVA: 0x002FDBB4 File Offset: 0x002FCBB4
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜀ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x06004F05 RID: 20229 RVA: 0x002FDBFC File Offset: 0x002FCBFC
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteUInt16(A_1, this.ᜀ);
		this.m_iLength = 2;
	}

	// Token: 0x06004F06 RID: 20230 RVA: 0x002FDC4C File Offset: 0x002FCC4C
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

	// Token: 0x040023AA RID: 9130
	[spr\u2429(0, 2)]
	private new ushort ᜀ;
}
