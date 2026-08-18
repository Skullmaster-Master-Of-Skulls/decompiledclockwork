using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000372 RID: 882
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartDat)]
internal class spr\u1C8F : BiffRecordRaw
{
	// Token: 0x060035C3 RID: 13763 RVA: 0x001EA054 File Offset: 0x001E9054
	public new ushort ᜃ()
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

	// Token: 0x060035C4 RID: 13764 RVA: 0x001EA098 File Offset: 0x001E9098
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
		return this.ᜂ;
	}

	// Token: 0x060035C5 RID: 13765 RVA: 0x001EA0DC File Offset: 0x001E90DC
	public void ᜂ(bool A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x060035C6 RID: 13766 RVA: 0x001EA120 File Offset: 0x001E9120
	public bool ᜂ()
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
		return this.ᜃ;
	}

	// Token: 0x060035C7 RID: 13767 RVA: 0x001EA164 File Offset: 0x001E9164
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
		this.ᜃ = A_0;
	}

	// Token: 0x060035C8 RID: 13768 RVA: 0x001EA1A8 File Offset: 0x001E91A8
	public bool ᜀ()
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
		return this.ᜄ;
	}

	// Token: 0x060035C9 RID: 13769 RVA: 0x001EA1EC File Offset: 0x001E91EC
	public void ᜁ(bool A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x060035CA RID: 13770 RVA: 0x001EA230 File Offset: 0x001E9230
	public bool ᜄ()
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
		return this.ᜅ;
	}

	// Token: 0x060035CB RID: 13771 RVA: 0x001EA274 File Offset: 0x001E9274
	public new void ᜃ(bool A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x060035CC RID: 13772 RVA: 0x001EA2B8 File Offset: 0x001E92B8
	public spr\u1C8F()
	{
	}

	// Token: 0x060035CD RID: 13773 RVA: 0x001EA2CC File Offset: 0x001E92CC
	public spr\u1C8F(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060035CE RID: 13774 RVA: 0x001EA2E4 File Offset: 0x001E92E4
	public spr\u1C8F(int A_0) : base(A_0)
	{
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x001EA2F8 File Offset: 0x001E92F8
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
		this.ᜁ = A_0.ReadUInt16(A_1);
		this.ᜂ = A_0.ReadBit(A_1, 0);
		this.ᜃ = A_0.ReadBit(A_1, 1);
		this.ᜄ = A_0.ReadBit(A_1, 2);
		this.ᜅ = A_0.ReadBit(A_1, 3);
	}

	// Token: 0x060035D0 RID: 13776 RVA: 0x001EA378 File Offset: 0x001E9378
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜁ &= 15;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteBit(A_1, this.ᜂ, 0);
		A_0.WriteBit(A_1, this.ᜃ, 1);
		A_0.WriteBit(A_1, this.ᜄ, 2);
		A_0.WriteBit(A_1, this.ᜅ, 3);
		this.m_iLength = 2;
	}

	// Token: 0x060035D1 RID: 13777 RVA: 0x001EA410 File Offset: 0x001E9410
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

	// Token: 0x04001771 RID: 6001
	private new const int ᜀ = 2;

	// Token: 0x04001772 RID: 6002
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04001773 RID: 6003
	[spr\u2429(0, 0, TFieldType.Bit)]
	private bool ᜂ;

	// Token: 0x04001774 RID: 6004
	[spr\u2429(0, 1, TFieldType.Bit)]
	private new bool ᜃ;

	// Token: 0x04001775 RID: 6005
	[spr\u2429(0, 2, TFieldType.Bit)]
	private bool ᜄ;

	// Token: 0x04001776 RID: 6006
	[spr\u2429(0, 3, TFieldType.Bit)]
	private bool ᜅ;
}
