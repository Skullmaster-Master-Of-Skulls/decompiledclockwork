using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004E6 RID: 1254
[spr\u2593(TBIFFRecord.ChartSbaseref)]
[CLSCompliant(false)]
internal class sprᲣ : BiffRecordRaw
{
	// Token: 0x06004CE3 RID: 19683 RVA: 0x002EED60 File Offset: 0x002EDD60
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

	// Token: 0x06004CE4 RID: 19684 RVA: 0x002EEDA4 File Offset: 0x002EDDA4
	public void ᜂ(ushort A_0)
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

	// Token: 0x06004CE5 RID: 19685 RVA: 0x002EEDE8 File Offset: 0x002EDDE8
	public new ushort ᜃ()
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

	// Token: 0x06004CE6 RID: 19686 RVA: 0x002EEE2C File Offset: 0x002EDE2C
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
		this.ᜂ = A_0;
	}

	// Token: 0x06004CE7 RID: 19687 RVA: 0x002EEE70 File Offset: 0x002EDE70
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
		return this.ᜃ;
	}

	// Token: 0x06004CE8 RID: 19688 RVA: 0x002EEEB4 File Offset: 0x002EDEB4
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

	// Token: 0x06004CE9 RID: 19689 RVA: 0x002EEEF8 File Offset: 0x002EDEF8
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
		return this.ᜄ;
	}

	// Token: 0x06004CEA RID: 19690 RVA: 0x002EEF3C File Offset: 0x002EDF3C
	public void ᜁ(ushort A_0)
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

	// Token: 0x06004CEB RID: 19691 RVA: 0x002EEF80 File Offset: 0x002EDF80
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
		return 8;
	}

	// Token: 0x06004CEC RID: 19692 RVA: 0x002EEFBC File Offset: 0x002EDFBC
	public virtual int ᜄ()
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
		return 8;
	}

	// Token: 0x06004CED RID: 19693 RVA: 0x002EEFF8 File Offset: 0x002EDFF8
	public sprᲣ()
	{
	}

	// Token: 0x06004CEE RID: 19694 RVA: 0x002EF00C File Offset: 0x002EE00C
	public sprᲣ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004CEF RID: 19695 RVA: 0x002EF024 File Offset: 0x002EE024
	public sprᲣ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004CF0 RID: 19696 RVA: 0x002EF038 File Offset: 0x002EE038
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
		this.ᜂ = A_0.ReadUInt16(A_1 + 2);
		this.ᜃ = A_0.ReadUInt16(A_1 + 4);
		this.ᜄ = A_0.ReadUInt16(A_1 + 6);
	}

	// Token: 0x06004CF1 RID: 19697 RVA: 0x002EF0B0 File Offset: 0x002EE0B0
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
		A_0.WriteUInt16(A_1 + 2, this.ᜂ);
		A_0.WriteUInt16(A_1 + 4, this.ᜃ);
		A_0.WriteUInt16(A_1 + 6, this.ᜄ);
		this.m_iLength = 8;
	}

	// Token: 0x06004CF2 RID: 19698 RVA: 0x002EF12C File Offset: 0x002EE12C
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
		return 8;
	}

	// Token: 0x04002301 RID: 8961
	public new const int ᜀ = 8;

	// Token: 0x04002302 RID: 8962
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04002303 RID: 8963
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x04002304 RID: 8964
	[spr\u2429(4, 2)]
	private new ushort ᜃ;

	// Token: 0x04002305 RID: 8965
	[spr\u2429(6, 2)]
	private ushort ᜄ;
}
