using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004D8 RID: 1240
[spr\u2593(TBIFFRecord.WindowTwo)]
[CLSCompliant(false)]
internal class sprṫ : BiffRecordRaw
{
	// Token: 0x06004C0D RID: 19469 RVA: 0x002E99CC File Offset: 0x002E89CC
	public ushort ᜐ()
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
		return this.ᜃ;
	}

	// Token: 0x06004C0E RID: 19470 RVA: 0x002E9A10 File Offset: 0x002E8A10
	public new void ᜃ(ushort A_0)
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

	// Token: 0x06004C0F RID: 19471 RVA: 0x002E9A54 File Offset: 0x002E8A54
	public ushort ᜌ()
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

	// Token: 0x06004C10 RID: 19472 RVA: 0x002E9A98 File Offset: 0x002E8A98
	public void ᜀ(ushort A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06004C11 RID: 19473 RVA: 0x002E9ADC File Offset: 0x002E8ADC
	public int ᜆ()
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

	// Token: 0x06004C12 RID: 19474 RVA: 0x002E9B20 File Offset: 0x002E8B20
	public void ᜀ(int A_0)
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

	// Token: 0x06004C13 RID: 19475 RVA: 0x002E9B64 File Offset: 0x002E8B64
	public bool ᜑ()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.DisplayFormulas) != 0;
	}

	// Token: 0x06004C14 RID: 19476 RVA: 0x002E9BB0 File Offset: 0x002E8BB0
	public void ᜉ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_43;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜂ |= sprṫ.OptionFlags.DisplayFormulas;
			return;
		}
		IL_43:
		this.ᜂ &= ~sprṫ.OptionFlags.DisplayFormulas;
	}

	// Token: 0x06004C15 RID: 19477 RVA: 0x002E9C14 File Offset: 0x002E8C14
	public bool \u1713()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.DisplayGridlines) != 0;
	}

	// Token: 0x06004C16 RID: 19478 RVA: 0x002E9C60 File Offset: 0x002E8C60
	public void ᜁ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_43;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜂ |= sprṫ.OptionFlags.DisplayGridlines;
			return;
		}
		IL_43:
		this.ᜂ &= ~sprṫ.OptionFlags.DisplayGridlines;
	}

	// Token: 0x06004C17 RID: 19479 RVA: 0x002E9CC4 File Offset: 0x002E8CC4
	public bool ᜏ()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.DisplayRowColHeadings) != 0;
	}

	// Token: 0x06004C18 RID: 19480 RVA: 0x002E9D10 File Offset: 0x002E8D10
	public void ᜀ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_43;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ |= sprṫ.OptionFlags.DisplayRowColHeadings;
			return;
		}
		IL_43:
		this.ᜂ &= ~sprṫ.OptionFlags.DisplayRowColHeadings;
	}

	// Token: 0x06004C19 RID: 19481 RVA: 0x002E9D74 File Offset: 0x002E8D74
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.FreezePanes) != 0;
	}

	// Token: 0x06004C1A RID: 19482 RVA: 0x002E9DC0 File Offset: 0x002E8DC0
	public void ᜊ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_43;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ |= sprṫ.OptionFlags.FreezePanes;
			return;
		}
		IL_43:
		this.ᜂ &= ~sprṫ.OptionFlags.FreezePanes;
	}

	// Token: 0x06004C1B RID: 19483 RVA: 0x002E9E24 File Offset: 0x002E8E24
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.DisplayZeros) != 0;
	}

	// Token: 0x06004C1C RID: 19484 RVA: 0x002E9E70 File Offset: 0x002E8E70
	public void ᜂ(bool A_0)
	{
		if (true)
		{
		}
		if (A_0)
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
				this.ᜂ |= sprṫ.OptionFlags.DisplayZeros;
				return;
			}
		}
		this.ᜂ &= ~sprṫ.OptionFlags.DisplayZeros;
	}

	// Token: 0x06004C1D RID: 19485 RVA: 0x002E9ED4 File Offset: 0x002E8ED4
	public bool ᜅ()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.DefaultHeader) != 0;
	}

	// Token: 0x06004C1E RID: 19486 RVA: 0x002E9F20 File Offset: 0x002E8F20
	public new void ᜃ(bool A_0)
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
			if (!A_0)
			{
				this.ᜂ &= ~sprṫ.OptionFlags.DefaultHeader;
				return;
			}
			break;
		}
		this.ᜂ |= sprṫ.OptionFlags.DefaultHeader;
	}

	// Token: 0x06004C1F RID: 19487 RVA: 0x002E9F84 File Offset: 0x002E8F84
	public bool ᜊ()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.Arabic) != 0;
	}

	// Token: 0x06004C20 RID: 19488 RVA: 0x002E9FD0 File Offset: 0x002E8FD0
	public void ᜋ(bool A_0)
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
			if (!A_0)
			{
				this.ᜂ &= ~sprṫ.OptionFlags.Arabic;
				return;
			}
			break;
		}
		this.ᜂ |= sprṫ.OptionFlags.Arabic;
	}

	// Token: 0x06004C21 RID: 19489 RVA: 0x002EA034 File Offset: 0x002E9034
	public bool ᜉ()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.DisplayGuts) != 0;
	}

	// Token: 0x06004C22 RID: 19490 RVA: 0x002EA084 File Offset: 0x002E9084
	public void ᜄ(bool A_0)
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
			if (!A_0)
			{
				this.ᜂ &= ~sprṫ.OptionFlags.DisplayGuts;
				return;
			}
			break;
		}
		if (true)
		{
		}
		this.ᜂ |= sprṫ.OptionFlags.DisplayGuts;
	}

	// Token: 0x06004C23 RID: 19491 RVA: 0x002EA0EC File Offset: 0x002E90EC
	public bool ᜀ()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.FreezePanesNoSplit) != 0;
	}

	// Token: 0x06004C24 RID: 19492 RVA: 0x002EA13C File Offset: 0x002E913C
	public void ᜈ(bool A_0)
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
			if (!A_0)
			{
				this.ᜂ &= ~sprṫ.OptionFlags.FreezePanesNoSplit;
				return;
			}
			break;
		}
		if (true)
		{
		}
		this.ᜂ |= sprṫ.OptionFlags.FreezePanesNoSplit;
	}

	// Token: 0x06004C25 RID: 19493 RVA: 0x002EA1A4 File Offset: 0x002E91A4
	public bool \u1712()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.Selected) != 0;
	}

	// Token: 0x06004C26 RID: 19494 RVA: 0x002EA1F4 File Offset: 0x002E91F4
	public void ᜇ(bool A_0)
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
			if (!A_0)
			{
				if (true)
				{
				}
				this.ᜂ &= ~sprṫ.OptionFlags.Selected;
				return;
			}
			break;
		}
		this.ᜂ |= sprṫ.OptionFlags.Selected;
	}

	// Token: 0x06004C27 RID: 19495 RVA: 0x002EA25C File Offset: 0x002E925C
	public new bool ᜃ()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.Paged) != 0;
	}

	// Token: 0x06004C28 RID: 19496 RVA: 0x002EA2AC File Offset: 0x002E92AC
	public void ᜆ(bool A_0)
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
			if (!A_0)
			{
				this.ᜂ &= ~sprṫ.OptionFlags.Paged;
				return;
			}
			break;
		}
		if (true)
		{
		}
		this.ᜂ |= sprṫ.OptionFlags.Paged;
	}

	// Token: 0x06004C29 RID: 19497 RVA: 0x002EA314 File Offset: 0x002E9314
	public bool \u170D()
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
		return (ushort)(this.ᜂ & sprṫ.OptionFlags.SavedInPageBreakPreview) != 0;
	}

	// Token: 0x06004C2A RID: 19498 RVA: 0x002EA364 File Offset: 0x002E9364
	public void ᜅ(bool A_0)
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
			if (!A_0)
			{
				this.ᜂ &= ~sprṫ.OptionFlags.SavedInPageBreakPreview;
				return;
			}
			break;
		}
		this.ᜂ |= sprṫ.OptionFlags.SavedInPageBreakPreview;
	}

	// Token: 0x06004C2B RID: 19499 RVA: 0x002EA3CC File Offset: 0x002E93CC
	public ushort ᜋ()
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
		return (ushort)this.ᜂ;
	}

	// Token: 0x06004C2C RID: 19500 RVA: 0x002EA410 File Offset: 0x002E9410
	public virtual int \u1714()
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
		return 10;
	}

	// Token: 0x06004C2D RID: 19501 RVA: 0x002EA450 File Offset: 0x002E9450
	public virtual int ᜂ()
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
		return 18;
	}

	// Token: 0x06004C2E RID: 19502 RVA: 0x002EA490 File Offset: 0x002E9490
	internal int ᜎ()
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
		return this.ᜉ;
	}

	// Token: 0x06004C2F RID: 19503 RVA: 0x002EA4D4 File Offset: 0x002E94D4
	internal void ᜁ(int A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x06004C30 RID: 19504 RVA: 0x002EA518 File Offset: 0x002E9518
	internal ushort ᜇ()
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
		return this.ᜇ;
	}

	// Token: 0x06004C31 RID: 19505 RVA: 0x002EA55C File Offset: 0x002E955C
	internal void ᜁ(ushort A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x06004C32 RID: 19506 RVA: 0x002EA5A0 File Offset: 0x002E95A0
	internal ushort ᜈ()
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
		return this.ᜆ;
	}

	// Token: 0x06004C33 RID: 19507 RVA: 0x002EA5E4 File Offset: 0x002E95E4
	internal void ᜂ(ushort A_0)
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

	// Token: 0x06004C34 RID: 19508 RVA: 0x002EA628 File Offset: 0x002E9628
	public sprṫ()
	{
	}

	// Token: 0x06004C35 RID: 19509 RVA: 0x002EA650 File Offset: 0x002E9650
	public sprṫ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004C36 RID: 19510 RVA: 0x002EA678 File Offset: 0x002E9678
	public sprṫ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004C37 RID: 19511 RVA: 0x002EA6A0 File Offset: 0x002E96A0
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			this.ᜂ = (sprṫ.OptionFlags)A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜃ = A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜄ = A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜅ = A_0.ReadInt32(A_1);
			A_1 += 4;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜆ = A_0.ReadUInt16(A_1);
					A_1 += 2;
					this.ᜇ = A_0.ReadUInt16(A_1);
					A_1 += 2;
					if (true)
					{
					}
					num = 2;
					continue;
				case 1:
					this.ᜈ = A_0.ReadInt32(A_1);
					num = 4;
					continue;
				case 2:
					goto IL_C7;
				case 3:
					if (this.m_iLength > 14)
					{
						num = 1;
						continue;
					}
					goto IL_122;
				case 4:
					goto IL_C5;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					if (this.m_iLength > 10)
					{
						num = 0;
						continue;
					}
					goto IL_C7;
				}
				break;
				IL_C7:
				num = 3;
			}
		}
		IL_C5:
		IL_122:
		this.ᜉ = this.m_iLength;
	}

	// Token: 0x06004C38 RID: 19512 RVA: 0x002EA7DC File Offset: 0x002E97DC
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteUInt16(A_1, (ushort)this.ᜂ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜃ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜄ);
		A_1 += 2;
		A_0.WriteInt32(A_1, this.ᜅ);
		A_1 += 4;
		A_0.WriteUInt16(A_1, this.ᜆ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜇ);
		A_1 += 2;
		A_0.WriteInt32(A_1, this.ᜈ);
	}

	// Token: 0x06004C39 RID: 19513 RVA: 0x002EA8A0 File Offset: 0x002E98A0
	public virtual int ᜀ(ExcelVersion A_0)
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
			if (this.ᜉ > 0)
			{
				return this.ᜉ;
			}
			break;
		}
		return 18;
	}

	// Token: 0x04002299 RID: 8857
	private new const int ᜀ = 18;

	// Token: 0x0400229A RID: 8858
	internal const int ᜁ = 10;

	// Token: 0x0400229B RID: 8859
	[spr\u2429(0, 2)]
	private sprṫ.OptionFlags ᜂ = sprṫ.OptionFlags.DisplayGridlines | sprṫ.OptionFlags.DisplayRowColHeadings | sprṫ.OptionFlags.DisplayZeros | sprṫ.OptionFlags.DefaultHeader | sprṫ.OptionFlags.DisplayGuts;

	// Token: 0x0400229C RID: 8860
	[spr\u2429(2, 2)]
	private new ushort ᜃ;

	// Token: 0x0400229D RID: 8861
	[spr\u2429(4, 2)]
	private ushort ᜄ;

	// Token: 0x0400229E RID: 8862
	[spr\u2429(6, 4, true)]
	private int ᜅ = 64;

	// Token: 0x0400229F RID: 8863
	private ushort ᜆ;

	// Token: 0x040022A0 RID: 8864
	private ushort ᜇ;

	// Token: 0x040022A1 RID: 8865
	private int ᜈ;

	// Token: 0x040022A2 RID: 8866
	private int ᜉ;

	// Token: 0x020004D9 RID: 1241
	[Flags]
	private enum OptionFlags : ushort
	{
		// Token: 0x040022A4 RID: 8868
		DisplayFormulas = 1,
		// Token: 0x040022A5 RID: 8869
		DisplayGridlines = 2,
		// Token: 0x040022A6 RID: 8870
		DisplayRowColHeadings = 4,
		// Token: 0x040022A7 RID: 8871
		FreezePanes = 8,
		// Token: 0x040022A8 RID: 8872
		DisplayZeros = 16,
		// Token: 0x040022A9 RID: 8873
		DefaultHeader = 32,
		// Token: 0x040022AA RID: 8874
		Arabic = 64,
		// Token: 0x040022AB RID: 8875
		DisplayGuts = 128,
		// Token: 0x040022AC RID: 8876
		FreezePanesNoSplit = 256,
		// Token: 0x040022AD RID: 8877
		Selected = 512,
		// Token: 0x040022AE RID: 8878
		Paged = 1024,
		// Token: 0x040022AF RID: 8879
		SavedInPageBreakPreview = 2048
	}
}
