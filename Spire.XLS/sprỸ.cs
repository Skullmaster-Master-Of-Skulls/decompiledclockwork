using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002F2 RID: 754
internal class sprỸ : BiffRecordRaw
{
	// Token: 0x06002EA9 RID: 11945 RVA: 0x001A1BF4 File Offset: 0x001A0BF4
	public sprỸ()
	{
	}

	// Token: 0x06002EAA RID: 11946 RVA: 0x001A1C08 File Offset: 0x001A0C08
	public sprỸ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x001A1C20 File Offset: 0x001A0C20
	public sprỸ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x001A1C34 File Offset: 0x001A0C34
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
		return 16;
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x001A1C74 File Offset: 0x001A0C74
	public virtual int ᜀ()
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
		return 16;
	}

	// Token: 0x06002EAE RID: 11950 RVA: 0x001A1CB4 File Offset: 0x001A0CB4
	public int ᜅ()
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

	// Token: 0x06002EAF RID: 11951 RVA: 0x001A1CF8 File Offset: 0x001A0CF8
	public void ᜁ(int A_0)
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

	// Token: 0x06002EB0 RID: 11952 RVA: 0x001A1D3C File Offset: 0x001A0D3C
	public int ᜂ()
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

	// Token: 0x06002EB1 RID: 11953 RVA: 0x001A1D80 File Offset: 0x001A0D80
	public new void ᜃ(int A_0)
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

	// Token: 0x06002EB2 RID: 11954 RVA: 0x001A1DC4 File Offset: 0x001A0DC4
	public int ᜄ()
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

	// Token: 0x06002EB3 RID: 11955 RVA: 0x001A1E08 File Offset: 0x001A0E08
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
		this.ᜃ = A_0;
	}

	// Token: 0x06002EB4 RID: 11956 RVA: 0x001A1E4C File Offset: 0x001A0E4C
	public new int ᜃ()
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

	// Token: 0x06002EB5 RID: 11957 RVA: 0x001A1E90 File Offset: 0x001A0E90
	public void ᜂ(int A_0)
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

	// Token: 0x06002EB6 RID: 11958 RVA: 0x001A1ED4 File Offset: 0x001A0ED4
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
		return 16;
	}

	// Token: 0x06002EB7 RID: 11959 RVA: 0x001A1F14 File Offset: 0x001A0F14
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
		A_0.WriteInt32(A_1, this.ᜁ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜃ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜂ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜄ);
	}

	// Token: 0x06002EB8 RID: 11960 RVA: 0x001A1FA0 File Offset: 0x001A0FA0
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
		this.ᜃ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜂ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜄ = A_0.ReadInt32(A_1);
	}

	// Token: 0x040014FC RID: 5372
	private new const int ᜀ = 16;

	// Token: 0x040014FD RID: 5373
	[spr\u2429(0, 4, true)]
	private int ᜁ;

	// Token: 0x040014FE RID: 5374
	[spr\u2429(8, 4, true)]
	private int ᜂ;

	// Token: 0x040014FF RID: 5375
	[spr\u2429(4, 4, true)]
	private new int ᜃ;

	// Token: 0x04001500 RID: 5376
	[spr\u2429(12, 4, true)]
	private int ᜄ;
}
