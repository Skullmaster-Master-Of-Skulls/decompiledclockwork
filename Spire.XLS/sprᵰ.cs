using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002BB RID: 699
[spr\u2593(TBIFFRecord.DCONRef)]
[CLSCompliant(false)]
internal class sprᵰ : BiffRecordRaw
{
	// Token: 0x06002A45 RID: 10821 RVA: 0x0017B844 File Offset: 0x0017A844
	public sprᵰ()
	{
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x0017B858 File Offset: 0x0017A858
	public sprᵰ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002A47 RID: 10823 RVA: 0x0017B870 File Offset: 0x0017A870
	public sprᵰ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002A48 RID: 10824 RVA: 0x0017B884 File Offset: 0x0017A884
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

	// Token: 0x06002A49 RID: 10825 RVA: 0x0017B8C8 File Offset: 0x0017A8C8
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
		this.ᜀ = A_0;
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x0017B90C File Offset: 0x0017A90C
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
		return this.ᜁ;
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x0017B950 File Offset: 0x0017A950
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
		this.ᜁ = A_0;
	}

	// Token: 0x06002A4C RID: 10828 RVA: 0x0017B994 File Offset: 0x0017A994
	public byte ᜁ()
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

	// Token: 0x06002A4D RID: 10829 RVA: 0x0017B9D8 File Offset: 0x0017A9D8
	public void ᜁ(byte A_0)
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

	// Token: 0x06002A4E RID: 10830 RVA: 0x0017BA1C File Offset: 0x0017AA1C
	public byte ᜂ()
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

	// Token: 0x06002A4F RID: 10831 RVA: 0x0017BA60 File Offset: 0x0017AA60
	public void ᜀ(byte A_0)
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

	// Token: 0x06002A50 RID: 10832 RVA: 0x0017BAA4 File Offset: 0x0017AAA4
	public string ᜄ()
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

	// Token: 0x06002A51 RID: 10833 RVA: 0x0017BAE8 File Offset: 0x0017AAE8
	public void ᜀ(string A_0)
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

	// Token: 0x06002A52 RID: 10834 RVA: 0x0017BB2C File Offset: 0x0017AB2C
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
		this.ᜁ = A_0.ReadUInt16(A_1 + 2);
		this.ᜂ = A_0.ReadByte(A_1 + 4);
		this.ᜃ = A_0.ReadByte(A_1 + 5);
		int num;
		this.ᜄ = A_0.ReadString16Bit(A_1 + 6, out num);
	}

	// Token: 0x06002A53 RID: 10835 RVA: 0x0017BBB4 File Offset: 0x0017ABB4
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
		A_0.WriteUInt16(A_1 + 2, this.ᜁ);
		A_0.WriteByte(A_1 + 4, this.ᜂ);
		A_0.WriteByte(A_1 + 5, this.ᜃ);
		A_0.WriteString16Bit(A_1 + 6, this.ᜄ);
		this.m_iLength = this.GetStoreSize(A_2);
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x0017BC48 File Offset: 0x0017AC48
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
		return 9 + this.ᜄ.Length * 2;
	}

	// Token: 0x0400140F RID: 5135
	[spr\u2429(0, 2)]
	private new ushort ᜀ;

	// Token: 0x04001410 RID: 5136
	[spr\u2429(2, 2)]
	private ushort ᜁ;

	// Token: 0x04001411 RID: 5137
	[spr\u2429(4, 1)]
	private byte ᜂ;

	// Token: 0x04001412 RID: 5138
	[spr\u2429(5, 1)]
	private new byte ᜃ;

	// Token: 0x04001413 RID: 5139
	[spr\u2429(6, TFieldType.String16Bit)]
	private string ᜄ;
}
