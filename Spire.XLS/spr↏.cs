using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002BD RID: 701
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartBoppCustom)]
internal class spr\u218F : BiffRecordRaw
{
	// Token: 0x06002A6C RID: 10860 RVA: 0x0017C370 File Offset: 0x0017B370
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

	// Token: 0x06002A6D RID: 10861 RVA: 0x0017C3B4 File Offset: 0x0017B3B4
	public byte[] ᜂ()
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

	// Token: 0x06002A6E RID: 10862 RVA: 0x0017C3F8 File Offset: 0x0017B3F8
	public void ᜀ(byte[] A_0)
	{
		int a_ = 2;
		while (A_0 == null)
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
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("丷嬹倻䬽┿", a_));
			}
		}
		this.ᜁ = A_0;
		this.ᜀ = (ushort)A_0.Length;
	}

	// Token: 0x06002A6F RID: 10863 RVA: 0x0017C468 File Offset: 0x0017B468
	public spr\u218F()
	{
	}

	// Token: 0x06002A70 RID: 10864 RVA: 0x0017C47C File Offset: 0x0017B47C
	public spr\u218F(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002A71 RID: 10865 RVA: 0x0017C494 File Offset: 0x0017B494
	public spr\u218F(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002A72 RID: 10866 RVA: 0x0017C4A8 File Offset: 0x0017B4A8
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
		this.ᜁ = new byte[(int)this.ᜀ];
		A_0.ReadArray(A_1 + 2, this.ᜁ);
	}

	// Token: 0x06002A73 RID: 10867 RVA: 0x0017C514 File Offset: 0x0017B514
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
		A_0.WriteBytes(A_1 + 2, this.ᜁ, 0, this.ᜁ.Length);
		this.m_iLength = this.ᜁ.Length + 2;
	}

	// Token: 0x06002A74 RID: 10868 RVA: 0x0017C584 File Offset: 0x0017B584
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
		return (int)(2 + this.ᜀ);
	}

	// Token: 0x06002A75 RID: 10869 RVA: 0x0017C5C8 File Offset: 0x0017B5C8
	public virtual object ᜁ()
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
		spr\u218F spr_u218F = (spr\u218F)base.Clone();
		spr_u218F.ᜁ = spr\u1CD3.ᜀ(this.ᜁ);
		return spr_u218F;
	}

	// Token: 0x0400141E RID: 5150
	[spr\u2429(0, 2)]
	private new ushort ᜀ;

	// Token: 0x0400141F RID: 5151
	private byte[] ᜁ;
}
