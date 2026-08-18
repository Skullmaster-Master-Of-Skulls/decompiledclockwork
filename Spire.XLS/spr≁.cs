using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004AD RID: 1197
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartFontx)]
internal class spr\u2241 : BiffRecordRaw
{
	// Token: 0x06004A12 RID: 18962 RVA: 0x002CD350 File Offset: 0x002CC350
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

	// Token: 0x06004A13 RID: 18963 RVA: 0x002CD394 File Offset: 0x002CC394
	public void ᜀ(ushort A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					this.ᜁ = A_0;
					num = 2;
					continue;
				case 2:
					return;
				}
				if (A_0 == this.ᜁ)
				{
					break;
				}
				num = 1;
			}
			break;
		}
		}
	}

	// Token: 0x06004A14 RID: 18964 RVA: 0x002CD410 File Offset: 0x002CC410
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

	// Token: 0x06004A15 RID: 18965 RVA: 0x002CD44C File Offset: 0x002CC44C
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

	// Token: 0x06004A16 RID: 18966 RVA: 0x002CD488 File Offset: 0x002CC488
	public spr\u2241()
	{
	}

	// Token: 0x06004A17 RID: 18967 RVA: 0x002CD49C File Offset: 0x002CC49C
	public spr\u2241(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004A18 RID: 18968 RVA: 0x002CD4B4 File Offset: 0x002CC4B4
	public spr\u2241(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004A19 RID: 18969 RVA: 0x002CD4C8 File Offset: 0x002CC4C8
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

	// Token: 0x06004A1A RID: 18970 RVA: 0x002CD510 File Offset: 0x002CC510
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

	// Token: 0x04002197 RID: 8599
	private new const int ᜀ = 2;

	// Token: 0x04002198 RID: 8600
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
