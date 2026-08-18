using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003A2 RID: 930
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartRadarArea)]
internal class sprᶗ : BiffRecordRaw
{
	// Token: 0x06003892 RID: 14482 RVA: 0x001F990C File Offset: 0x001F890C
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

	// Token: 0x06003893 RID: 14483 RVA: 0x001F9950 File Offset: 0x001F8950
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

	// Token: 0x06003894 RID: 14484 RVA: 0x001F9994 File Offset: 0x001F8994
	public void ᜀ(bool A_0)
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

	// Token: 0x06003895 RID: 14485 RVA: 0x001F99D8 File Offset: 0x001F89D8
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
		return this.ᜃ;
	}

	// Token: 0x06003896 RID: 14486 RVA: 0x001F9A1C File Offset: 0x001F8A1C
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
		this.ᜃ = A_0;
	}

	// Token: 0x06003897 RID: 14487 RVA: 0x001F9A60 File Offset: 0x001F8A60
	public virtual int ᜄ()
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
		return 4;
	}

	// Token: 0x06003898 RID: 14488 RVA: 0x001F9A9C File Offset: 0x001F8A9C
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
		return 4;
	}

	// Token: 0x06003899 RID: 14489 RVA: 0x001F9AD8 File Offset: 0x001F8AD8
	public sprᶗ()
	{
	}

	// Token: 0x0600389A RID: 14490 RVA: 0x001F9AEC File Offset: 0x001F8AEC
	public sprᶗ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600389B RID: 14491 RVA: 0x001F9B04 File Offset: 0x001F8B04
	public sprᶗ(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600389C RID: 14492 RVA: 0x001F9B18 File Offset: 0x001F8B18
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
		this.ᜂ = A_0.ReadBit(A_1, 0);
		this.ᜃ = A_0.ReadUInt16(A_1 + 2);
	}

	// Token: 0x0600389D RID: 14493 RVA: 0x001F9B80 File Offset: 0x001F8B80
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
		A_0.WriteBit(A_1, this.ᜂ, 0);
		A_0.WriteUInt16(A_1 + 2, this.ᜃ);
		this.m_iLength = 4;
	}

	// Token: 0x0600389E RID: 14494 RVA: 0x001F9BEC File Offset: 0x001F8BEC
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

	// Token: 0x040018DB RID: 6363
	public new const int ᜀ = 4;

	// Token: 0x040018DC RID: 6364
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x040018DD RID: 6365
	[spr\u2429(0, 0, TFieldType.Bit)]
	private bool ᜂ;

	// Token: 0x040018DE RID: 6366
	[spr\u2429(2, 2)]
	private new ushort ᜃ;
}
