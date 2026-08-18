using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000337 RID: 823
[spr\u2593(TBIFFRecord.FileSharing)]
[CLSCompliant(false)]
internal class sprẋ : BiffRecordRaw
{
	// Token: 0x0600326E RID: 12910 RVA: 0x001D1150 File Offset: 0x001D0150
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
		return this.ᜀ;
	}

	// Token: 0x0600326F RID: 12911 RVA: 0x001D1194 File Offset: 0x001D0194
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

	// Token: 0x06003270 RID: 12912 RVA: 0x001D11D8 File Offset: 0x001D01D8
	public ushort ᜁ()
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

	// Token: 0x06003271 RID: 12913 RVA: 0x001D121C File Offset: 0x001D021C
	public void ᜁ(ushort A_0)
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

	// Token: 0x06003272 RID: 12914 RVA: 0x001D1260 File Offset: 0x001D0260
	public string ᜀ()
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

	// Token: 0x06003273 RID: 12915 RVA: 0x001D12A4 File Offset: 0x001D02A4
	public void ᜀ(string A_0)
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

	// Token: 0x06003274 RID: 12916 RVA: 0x001D12E8 File Offset: 0x001D02E8
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
		return 6;
	}

	// Token: 0x06003275 RID: 12917 RVA: 0x001D1324 File Offset: 0x001D0324
	public sprẋ()
	{
	}

	// Token: 0x06003276 RID: 12918 RVA: 0x001D1338 File Offset: 0x001D0338
	public sprẋ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003277 RID: 12919 RVA: 0x001D1350 File Offset: 0x001D0350
	public sprẋ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003278 RID: 12920 RVA: 0x001D1364 File Offset: 0x001D0364
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
		this.ᜀ = A_0.ReadUInt16(A_1);
		this.ᜁ = A_0.ReadUInt16(A_1 + 2);
		int num;
		this.ᜂ = A_0.ReadString16Bit(A_1 + 4, out num);
	}

	// Token: 0x06003279 RID: 12921 RVA: 0x001D13CC File Offset: 0x001D03CC
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
		this.m_iLength = 4;
		this.m_iLength += A_0.WriteString16Bit(A_1 + 4, this.ᜂ);
	}

	// Token: 0x0600327A RID: 12922 RVA: 0x001D1448 File Offset: 0x001D0448
	public virtual int ᜀ(ExcelVersion A_0)
	{
		if (true)
		{
		}
		int num = 2;
		int num3;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				num2 = this.ᜂ.Length;
				goto IL_84;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 3:
				num2 = 0;
				goto IL_84;
			case 4:
				goto IL_8D;
			}
			goto IL_2C;
			IL_3E:
			num = 1;
			continue;
			IL_2C:
			if (this.ᜂ == null)
			{
				goto IL_3E;
			}
			num = 0;
			continue;
			IL_84:
			num3 = num2;
			num = 4;
		}
		IL_8D:
		return 4 + ((num3 > 0) ? (3 + num3 * 2) : 2);
	}

	// Token: 0x04001601 RID: 5633
	[spr\u2429(0, 2)]
	private new ushort ᜀ;

	// Token: 0x04001602 RID: 5634
	[spr\u2429(2, 2)]
	private ushort ᜁ;

	// Token: 0x04001603 RID: 5635
	[spr\u2429(4, 2, TFieldType.String16Bit)]
	private string ᜂ;
}
