using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000445 RID: 1093
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ScenProtect)]
internal class sprℷ : BiffRecordRaw
{
	// Token: 0x060041DE RID: 16862 RVA: 0x0024FE7C File Offset: 0x0024EE7C
	public bool ᜁ()
	{
		if (this.ᜁ == 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return false;
			}
		}
		if (true)
		{
		}
		return true;
	}

	// Token: 0x060041DF RID: 16863 RVA: 0x0024FEC4 File Offset: 0x0024EEC4
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
		this.ᜁ = (A_0 ? 1 : 0);
	}

	// Token: 0x060041E0 RID: 16864 RVA: 0x0024FF10 File Offset: 0x0024EF10
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

	// Token: 0x060041E1 RID: 16865 RVA: 0x0024FF4C File Offset: 0x0024EF4C
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

	// Token: 0x060041E2 RID: 16866 RVA: 0x0024FF88 File Offset: 0x0024EF88
	public sprℷ()
	{
	}

	// Token: 0x060041E3 RID: 16867 RVA: 0x0024FF9C File Offset: 0x0024EF9C
	public sprℷ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060041E4 RID: 16868 RVA: 0x0024FFB4 File Offset: 0x0024EFB4
	public sprℷ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060041E5 RID: 16869 RVA: 0x0024FFC8 File Offset: 0x0024EFC8
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

	// Token: 0x060041E6 RID: 16870 RVA: 0x00250010 File Offset: 0x0024F010
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

	// Token: 0x060041E7 RID: 16871 RVA: 0x00250060 File Offset: 0x0024F060
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

	// Token: 0x04001D2C RID: 7468
	private new const int ᜀ = 2;

	// Token: 0x04001D2D RID: 7469
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
