using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003D9 RID: 985
[spr\u2593(TBIFFRecord.ChartAttachedLabel)]
[CLSCompliant(false)]
internal class sprή : BiffRecordRaw
{
	// Token: 0x06003BBC RID: 15292 RVA: 0x00216264 File Offset: 0x00215264
	public ushort ᜄ()
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
		return (ushort)this.ᜁ;
	}

	// Token: 0x06003BBD RID: 15293 RVA: 0x002162A8 File Offset: 0x002152A8
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
		return (this.ᜁ & sprή.OptionFlags.ActiveValue) != sprή.OptionFlags.None;
	}

	// Token: 0x06003BBE RID: 15294 RVA: 0x002162F4 File Offset: 0x002152F4
	public new void ᜃ(bool A_0)
	{
		if (!A_0)
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
				this.ᜁ &= ~sprή.OptionFlags.ActiveValue;
				return;
			}
		}
		this.ᜁ |= sprή.OptionFlags.ActiveValue;
	}

	// Token: 0x06003BBF RID: 15295 RVA: 0x00216354 File Offset: 0x00215354
	public bool ᜆ()
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
		return (this.ᜁ & sprή.OptionFlags.PiePercents) != sprή.OptionFlags.None;
	}

	// Token: 0x06003BC0 RID: 15296 RVA: 0x002163A0 File Offset: 0x002153A0
	public void ᜀ(bool A_0)
	{
		if (!A_0)
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
				this.ᜁ &= ~sprή.OptionFlags.PiePercents;
				return;
			}
		}
		this.ᜁ |= sprή.OptionFlags.PiePercents;
	}

	// Token: 0x06003BC1 RID: 15297 RVA: 0x00216400 File Offset: 0x00215400
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
		return (this.ᜁ & sprή.OptionFlags.PieCategoryLabel) != sprή.OptionFlags.None;
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x0021644C File Offset: 0x0021544C
	public void ᜅ(bool A_0)
	{
		if (!A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_05;
			}
			if (false)
			{
			}
			this.ᜁ &= ~sprή.OptionFlags.PieCategoryLabel;
			return;
		}
		IL_05:
		if (true)
		{
		}
		this.ᜁ |= sprή.OptionFlags.PieCategoryLabel;
	}

	// Token: 0x06003BC3 RID: 15299 RVA: 0x002164AC File Offset: 0x002154AC
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
		return (this.ᜁ & sprή.OptionFlags.SmoothLine) != sprή.OptionFlags.None;
	}

	// Token: 0x06003BC4 RID: 15300 RVA: 0x002164F8 File Offset: 0x002154F8
	public void ᜁ(bool A_0)
	{
		if (!A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0D;
			}
			if (false)
			{
			}
			this.ᜁ &= ~sprή.OptionFlags.SmoothLine;
			return;
		}
		if (true)
		{
		}
		IL_0D:
		this.ᜁ |= sprή.OptionFlags.SmoothLine;
	}

	// Token: 0x06003BC5 RID: 15301 RVA: 0x00216558 File Offset: 0x00215558
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
		return (this.ᜁ & sprή.OptionFlags.CategoryLabel) != sprή.OptionFlags.None;
	}

	// Token: 0x06003BC6 RID: 15302 RVA: 0x002165A4 File Offset: 0x002155A4
	public void ᜄ(bool A_0)
	{
		if (true)
		{
		}
		if (!A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0D;
			}
			if (false)
			{
			}
			this.ᜁ &= ~sprή.OptionFlags.CategoryLabel;
			return;
		}
		IL_0D:
		this.ᜁ |= sprή.OptionFlags.CategoryLabel;
	}

	// Token: 0x06003BC7 RID: 15303 RVA: 0x00216604 File Offset: 0x00215604
	public bool ᜅ()
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
		return (this.ᜁ & sprή.OptionFlags.Bubble) != sprή.OptionFlags.None;
	}

	// Token: 0x06003BC8 RID: 15304 RVA: 0x00216650 File Offset: 0x00215650
	public void ᜂ(bool A_0)
	{
		if (!A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0D;
			}
			if (false)
			{
			}
			this.ᜁ &= ~sprή.OptionFlags.Bubble;
			return;
		}
		if (true)
		{
		}
		IL_0D:
		this.ᜁ |= sprή.OptionFlags.Bubble;
	}

	// Token: 0x06003BC9 RID: 15305 RVA: 0x002166B0 File Offset: 0x002156B0
	public sprή()
	{
	}

	// Token: 0x06003BCA RID: 15306 RVA: 0x002166C4 File Offset: 0x002156C4
	public sprή(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003BCB RID: 15307 RVA: 0x002166DC File Offset: 0x002156DC
	public sprή(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003BCC RID: 15308 RVA: 0x002166F0 File Offset: 0x002156F0
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
		this.ᜁ = (sprή.OptionFlags)A_0.ReadUInt16(A_1);
	}

	// Token: 0x06003BCD RID: 15309 RVA: 0x00216738 File Offset: 0x00215738
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
		A_0.WriteUInt16(A_1, (ushort)this.ᜁ);
	}

	// Token: 0x06003BCE RID: 15310 RVA: 0x00216790 File Offset: 0x00215790
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 2;
	}

	// Token: 0x040019EE RID: 6638
	private new const int ᜀ = 2;

	// Token: 0x040019EF RID: 6639
	[spr\u2429(0, 2)]
	private sprή.OptionFlags ᜁ;

	// Token: 0x020003DA RID: 986
	[Flags]
	private enum OptionFlags
	{
		// Token: 0x040019F1 RID: 6641
		None = 0,
		// Token: 0x040019F2 RID: 6642
		ActiveValue = 1,
		// Token: 0x040019F3 RID: 6643
		PiePercents = 2,
		// Token: 0x040019F4 RID: 6644
		PieCategoryLabel = 4,
		// Token: 0x040019F5 RID: 6645
		SmoothLine = 8,
		// Token: 0x040019F6 RID: 6646
		CategoryLabel = 16,
		// Token: 0x040019F7 RID: 6647
		Bubble = 32
	}
}
