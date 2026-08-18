using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000370 RID: 880
[spr\u2593(TBIFFRecord.ChartLegendxn)]
[CLSCompliant(false)]
internal class spr\u1A75 : BiffRecordRaw
{
	// Token: 0x060035A6 RID: 13734 RVA: 0x001E9744 File Offset: 0x001E8744
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

	// Token: 0x060035A7 RID: 13735 RVA: 0x001E9788 File Offset: 0x001E8788
	public void ᜀ(ushort A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_4A:
			if (A_0 == this.ᜁ)
			{
				return;
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				this.ᜁ = A_0;
				num = 0;
				continue;
			case 2:
				goto IL_2E;
			}
			goto IL_4A;
		}
		IL_2E:
		if (true)
		{
		}
		goto IL_4A;
	}

	// Token: 0x060035A8 RID: 13736 RVA: 0x001E9804 File Offset: 0x001E8804
	public ushort ᜂ()
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
		return this.ᜂ;
	}

	// Token: 0x060035A9 RID: 13737 RVA: 0x001E9848 File Offset: 0x001E8848
	public bool ᜁ()
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

	// Token: 0x060035AA RID: 13738 RVA: 0x001E988C File Offset: 0x001E888C
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
		this.ᜃ = A_0;
	}

	// Token: 0x060035AB RID: 13739 RVA: 0x001E98D0 File Offset: 0x001E88D0
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

	// Token: 0x060035AC RID: 13740 RVA: 0x001E9914 File Offset: 0x001E8914
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
		this.ᜄ = A_0;
	}

	// Token: 0x060035AD RID: 13741 RVA: 0x001E9958 File Offset: 0x001E8958
	public spr\u1A75()
	{
	}

	// Token: 0x060035AE RID: 13742 RVA: 0x001E9978 File Offset: 0x001E8978
	public spr\u1A75(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060035AF RID: 13743 RVA: 0x001E9998 File Offset: 0x001E8998
	public spr\u1A75(int A_0) : base(A_0)
	{
	}

	// Token: 0x060035B0 RID: 13744 RVA: 0x001E99B8 File Offset: 0x001E89B8
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
		this.ᜂ = A_0.ReadUInt16(A_1 + 2);
		this.ᜃ = A_0.ReadBit(A_1 + 2, 0);
		this.ᜄ = A_0.ReadBit(A_1 + 2, 1);
	}

	// Token: 0x060035B1 RID: 13745 RVA: 0x001E9A30 File Offset: 0x001E8A30
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
		this.ᜂ &= 3;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteUInt16(A_1 + 2, this.ᜂ);
		A_0.WriteBit(A_1 + 2, this.ᜃ, 0);
		A_0.WriteBit(A_1 + 2, this.ᜄ, 1);
		this.m_iLength = 4;
	}

	// Token: 0x060035B2 RID: 13746 RVA: 0x001E9AC0 File Offset: 0x001E8AC0
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 4;
	}

	// Token: 0x04001766 RID: 5990
	private new const int ᜀ = 4;

	// Token: 0x04001767 RID: 5991
	[spr\u2429(0, 2)]
	private ushort ᜁ = ushort.MaxValue;

	// Token: 0x04001768 RID: 5992
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x04001769 RID: 5993
	[spr\u2429(2, 0, TFieldType.Bit)]
	private new bool ᜃ;

	// Token: 0x0400176A RID: 5994
	[spr\u2429(2, 1, TFieldType.Bit)]
	private bool ᜄ;
}
