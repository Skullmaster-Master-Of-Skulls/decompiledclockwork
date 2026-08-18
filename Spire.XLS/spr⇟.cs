using System;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003C3 RID: 963
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.String)]
internal class spr\u21DF : BiffRecordRaw
{
	// Token: 0x06003AA6 RID: 15014 RVA: 0x0020F334 File Offset: 0x0020E334
	public string ᜁ()
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

	// Token: 0x06003AA7 RID: 15015 RVA: 0x0020F378 File Offset: 0x0020E378
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
		this.ᜁ = ((A_0 != null) ? ((ushort)A_0.Length) : 0);
		this.ᜃ = !spr\u251F.ᜀ(A_0);
	}

	// Token: 0x06003AA8 RID: 15016 RVA: 0x0020F3E0 File Offset: 0x0020E3E0
	public virtual int ᜀ()
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

	// Token: 0x06003AA9 RID: 15017 RVA: 0x0020F41C File Offset: 0x0020E41C
	public spr\u21DF()
	{
	}

	// Token: 0x06003AAA RID: 15018 RVA: 0x0020F430 File Offset: 0x0020E430
	public spr\u21DF(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003AAB RID: 15019 RVA: 0x0020F448 File Offset: 0x0020E448
	public spr\u21DF(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003AAC RID: 15020 RVA: 0x0020F45C File Offset: 0x0020E45C
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
		int num;
		this.ᜂ = A_0.ReadString(A_1 + 2, (int)this.ᜁ, out num, false);
		this.ᜃ = (this.ᜂ.Length * 2 > num);
	}

	// Token: 0x06003AAD RID: 15021 RVA: 0x0020F4D4 File Offset: 0x0020E4D4
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		int num = A_1 + 2;
		A_0.WriteStringNoLenUpdateOffset(ref num, this.ᜂ, this.ᜃ);
	}

	// Token: 0x06003AAE RID: 15022 RVA: 0x0020F544 File Offset: 0x0020E544
	public virtual int ᜀ(ExcelVersion A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_4C;
			case 2:
				goto IL_82;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			}
			IL_2A:
			if (true)
			{
			}
			if (!this.ᜃ)
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			goto IL_2A;
		}
		IL_4C:
		int num2 = Encoding.Unicode.GetByteCount(this.ᜂ);
		goto IL_8F;
		IL_82:
		num2 = this.ᜂ.Length;
		IL_8F:
		int num3 = num2;
		return 3 + num3;
	}

	// Token: 0x04001996 RID: 6550
	private new const int ᜀ = 3;

	// Token: 0x04001997 RID: 6551
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04001998 RID: 6552
	private string ᜂ;

	// Token: 0x04001999 RID: 6553
	private new bool ᜃ;
}
