using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000535 RID: 1333
[spr\u2593(TBIFFRecord.Guts)]
[CLSCompliant(false)]
internal class spr\u1922 : BiffRecordRaw
{
	// Token: 0x0600514B RID: 20811 RVA: 0x0032DEA0 File Offset: 0x0032CEA0
	public ushort ᜂ()
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

	// Token: 0x0600514C RID: 20812 RVA: 0x0032DEE4 File Offset: 0x0032CEE4
	public void ᜀ(ushort A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600514D RID: 20813 RVA: 0x0032DF28 File Offset: 0x0032CF28
	public ushort ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x0600514E RID: 20814 RVA: 0x0032DF6C File Offset: 0x0032CF6C
	public new void ᜃ(ushort A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x0600514F RID: 20815 RVA: 0x0032DFB0 File Offset: 0x0032CFB0
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
		return this.ᜂ;
	}

	// Token: 0x06005150 RID: 20816 RVA: 0x0032DFF4 File Offset: 0x0032CFF4
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
		this.ᜂ = A_0;
	}

	// Token: 0x06005151 RID: 20817 RVA: 0x0032E038 File Offset: 0x0032D038
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
		return this.ᜃ;
	}

	// Token: 0x06005152 RID: 20818 RVA: 0x0032E07C File Offset: 0x0032D07C
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
		this.ᜃ = A_0;
	}

	// Token: 0x06005153 RID: 20819 RVA: 0x0032E0C0 File Offset: 0x0032D0C0
	public virtual int ᜁ()
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

	// Token: 0x06005154 RID: 20820 RVA: 0x0032E0FC File Offset: 0x0032D0FC
	public virtual int ᜅ()
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

	// Token: 0x06005155 RID: 20821 RVA: 0x0032E138 File Offset: 0x0032D138
	public spr\u1922()
	{
	}

	// Token: 0x06005156 RID: 20822 RVA: 0x0032E14C File Offset: 0x0032D14C
	public spr\u1922(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005157 RID: 20823 RVA: 0x0032E164 File Offset: 0x0032D164
	public spr\u1922(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005158 RID: 20824 RVA: 0x0032E178 File Offset: 0x0032D178
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
		this.ᜀ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜁ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜂ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜃ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x06005159 RID: 20825 RVA: 0x0032E1F8 File Offset: 0x0032D1F8
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
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜂ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜃ);
	}

	// Token: 0x0400244E RID: 9294
	[spr\u2429(0, 2)]
	private new ushort ᜀ;

	// Token: 0x0400244F RID: 9295
	[spr\u2429(2, 2)]
	private ushort ᜁ;

	// Token: 0x04002450 RID: 9296
	[spr\u2429(4, 2)]
	private ushort ᜂ;

	// Token: 0x04002451 RID: 9297
	[spr\u2429(6, 2)]
	private new ushort ᜃ;
}
