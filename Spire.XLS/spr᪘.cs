using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200048C RID: 1164
[spr\u2593(TBIFFRecord.ChartChartFormat)]
[CLSCompliant(false)]
internal class spr᪘ : BiffRecordRaw
{
	// Token: 0x0600478B RID: 18315 RVA: 0x002B5484 File Offset: 0x002B4484
	public int ᜈ()
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

	// Token: 0x0600478C RID: 18316 RVA: 0x002B54C8 File Offset: 0x002B44C8
	public int ᜇ()
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

	// Token: 0x0600478D RID: 18317 RVA: 0x002B550C File Offset: 0x002B450C
	public int ᜁ()
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

	// Token: 0x0600478E RID: 18318 RVA: 0x002B5550 File Offset: 0x002B4550
	public int ᜀ()
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
		return this.ᜄ;
	}

	// Token: 0x0600478F RID: 18319 RVA: 0x002B5594 File Offset: 0x002B4594
	public ushort ᜅ()
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

	// Token: 0x06004790 RID: 18320 RVA: 0x002B55D8 File Offset: 0x002B45D8
	public bool ᜂ()
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
		return this.ᜆ;
	}

	// Token: 0x06004791 RID: 18321 RVA: 0x002B561C File Offset: 0x002B461C
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
		this.ᜆ = A_0;
	}

	// Token: 0x06004792 RID: 18322 RVA: 0x002B5660 File Offset: 0x002B4660
	public ushort ᜆ()
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
		return this.ᜇ;
	}

	// Token: 0x06004793 RID: 18323 RVA: 0x002B56A4 File Offset: 0x002B46A4
	public void ᜀ(ushort A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_4A:
			if (A_0 == this.ᜇ)
			{
				return;
			}
			num = 2;
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				this.ᜇ = A_0;
				num = 0;
				continue;
			}
			break;
		}
		goto IL_4A;
	}

	// Token: 0x06004794 RID: 18324 RVA: 0x002B5720 File Offset: 0x002B4720
	public virtual int ᜃ()
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
		return 20;
	}

	// Token: 0x06004795 RID: 18325 RVA: 0x002B5760 File Offset: 0x002B4760
	public virtual int ᜄ()
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
		return 20;
	}

	// Token: 0x06004796 RID: 18326 RVA: 0x002B57A0 File Offset: 0x002B47A0
	public spr᪘()
	{
	}

	// Token: 0x06004797 RID: 18327 RVA: 0x002B57B4 File Offset: 0x002B47B4
	public spr᪘(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004798 RID: 18328 RVA: 0x002B57CC File Offset: 0x002B47CC
	public spr᪘(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004799 RID: 18329 RVA: 0x002B57E0 File Offset: 0x002B47E0
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
		this.ᜁ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜂ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜃ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜄ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜅ = A_0.ReadUInt16(A_1);
		this.ᜆ = A_0.ReadBit(A_1, 0);
		A_1 += 2;
		this.ᜇ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x0600479A RID: 18330 RVA: 0x002B5890 File Offset: 0x002B4890
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
		this.ᜁ = (this.ᜂ = (this.ᜃ = (this.ᜄ = 0)));
		this.ᜅ &= 1;
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteInt32(A_1, this.ᜁ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜂ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜃ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜄ);
		A_1 += 4;
		A_0.WriteUInt16(A_1, this.ᜅ);
		A_0.WriteBit(A_1, this.ᜆ, 0);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜇ);
	}

	// Token: 0x0600479B RID: 18331 RVA: 0x002B5980 File Offset: 0x002B4980
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
		return 20;
	}

	// Token: 0x0600479C RID: 18332 RVA: 0x002B59C0 File Offset: 0x002B49C0
	internal bool ᜀ(spr᪘ A_0)
	{
		while (this.ᜅ == A_0.ᜅ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜆ == A_0.ᜆ;
			}
		}
		return false;
	}

	// Token: 0x0400206C RID: 8300
	private new const int ᜀ = 20;

	// Token: 0x0400206D RID: 8301
	[spr\u2429(0, 4, true)]
	private int ᜁ;

	// Token: 0x0400206E RID: 8302
	[spr\u2429(4, 4, true)]
	private int ᜂ;

	// Token: 0x0400206F RID: 8303
	[spr\u2429(8, 4, true)]
	private new int ᜃ;

	// Token: 0x04002070 RID: 8304
	[spr\u2429(12, 4, true)]
	private int ᜄ;

	// Token: 0x04002071 RID: 8305
	[spr\u2429(16, 2)]
	private ushort ᜅ;

	// Token: 0x04002072 RID: 8306
	[spr\u2429(16, 0, TFieldType.Bit)]
	private bool ᜆ;

	// Token: 0x04002073 RID: 8307
	[spr\u2429(18, 2)]
	private ushort ᜇ;
}
