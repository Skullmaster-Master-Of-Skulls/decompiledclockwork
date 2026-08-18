using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x020002AB RID: 683
[spr\u2593(TBIFFRecord.Row)]
[CLSCompliant(false)]
internal class spr\u20BA : BiffRecordRaw, spr\u2502
{
	// Token: 0x06002947 RID: 10567 RVA: 0x00175C28 File Offset: 0x00174C28
	public int ᜊ()
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
		return (int)this.ᜈ;
	}

	// Token: 0x06002948 RID: 10568 RVA: 0x00175C6C File Offset: 0x00174C6C
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
		this.ᜈ = (spr\u20BA.OptionFlags)A_0;
	}

	// Token: 0x06002949 RID: 10569 RVA: 0x00175CB0 File Offset: 0x00174CB0
	public ushort ᜇ()
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

	// Token: 0x0600294A RID: 10570 RVA: 0x00175CF4 File Offset: 0x00174CF4
	public void ᜆ(ushort A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x0600294B RID: 10571 RVA: 0x00175D38 File Offset: 0x00174D38
	public ushort ᜌ()
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

	// Token: 0x0600294C RID: 10572 RVA: 0x00175D7C File Offset: 0x00174D7C
	public void ᜅ(ushort A_0)
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

	// Token: 0x0600294D RID: 10573 RVA: 0x00175DC0 File Offset: 0x00174DC0
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
		return this.ᜅ;
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x00175E04 File Offset: 0x00174E04
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
		this.ᜅ = A_0;
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x00175E48 File Offset: 0x00174E48
	public ushort ᜏ()
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

	// Token: 0x06002950 RID: 10576 RVA: 0x00175E8C File Offset: 0x00174E8C
	public void ᜄ(ushort A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x00175ED0 File Offset: 0x00174ED0
	public ushort ᜑ()
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
		return (ushort)((this.ᜈ & (spr\u20BA.OptionFlags)268369920) >> 16);
	}

	// Token: 0x06002952 RID: 10578 RVA: 0x00175F1C File Offset: 0x00174F1C
	public new void ᜃ(ushort A_0)
	{
		int num;
		for (;;)
		{
			IL_3A:
			if (true)
			{
			}
			num = (int)this.ᜈ;
			num &= -268369921;
			num |= ((int)A_0 << 16 & 268369920);
			int num2 = 0;
			for (;;)
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
					switch (num2)
					{
					case 0:
						if (A_0 != 15)
						{
							num2 = 2;
							continue;
						}
						goto IL_89;
					case 1:
						goto IL_87;
					case 2:
						goto IL_73;
					}
					goto IL_3A;
				}
				IL_73:
				this.ᜅ(true);
				num2 = 1;
			}
		}
		IL_87:
		IL_89:
		this.ᜈ = (spr\u20BA.OptionFlags)num;
	}

	// Token: 0x06002953 RID: 10579 RVA: 0x00175FBC File Offset: 0x00174FBC
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
		return (ushort)(this.ᜈ & (spr\u20BA.OptionFlags)7);
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x00176000 File Offset: 0x00175000
	public void ᜂ(ushort A_0)
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
			if (A_0 <= 7)
			{
				int num = (int)this.ᜈ;
				num &= -8;
				num |= (int)(A_0 & 7);
				this.ᜈ = (spr\u20BA.OptionFlags)num;
				return;
			}
			break;
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06002955 RID: 10581 RVA: 0x00176060 File Offset: 0x00175060
	public bool ᜆ()
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
		return (this.ᜈ & spr\u20BA.OptionFlags.Colapsed) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x06002956 RID: 10582 RVA: 0x001760AC File Offset: 0x001750AC
	public void ᜀ(bool A_0)
	{
		if (true)
		{
		}
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
				this.ᜈ &= (spr\u20BA.OptionFlags)(-17);
				return;
			}
			break;
		}
		this.ᜈ |= spr\u20BA.OptionFlags.Colapsed;
	}

	// Token: 0x06002957 RID: 10583 RVA: 0x0017610C File Offset: 0x0017510C
	public bool ᜈ()
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
		return (this.ᜈ & spr\u20BA.OptionFlags.ZeroHeight) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x06002958 RID: 10584 RVA: 0x00176158 File Offset: 0x00175158
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
				this.ᜈ &= (spr\u20BA.OptionFlags)(-33);
				return;
			}
			break;
		}
		this.ᜈ |= spr\u20BA.OptionFlags.ZeroHeight;
	}

	// Token: 0x06002959 RID: 10585 RVA: 0x001761B8 File Offset: 0x001751B8
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
		return (this.ᜈ & spr\u20BA.OptionFlags.BadFontHeight) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x0600295A RID: 10586 RVA: 0x00176204 File Offset: 0x00175204
	public void ᜆ(bool A_0)
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
				this.ᜈ &= (spr\u20BA.OptionFlags)(-65);
				return;
			}
			break;
		}
		this.ᜈ |= spr\u20BA.OptionFlags.BadFontHeight;
	}

	// Token: 0x0600295B RID: 10587 RVA: 0x00176264 File Offset: 0x00175264
	public bool \u1712()
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
		return (this.ᜈ & spr\u20BA.OptionFlags.Formatted) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x0600295C RID: 10588 RVA: 0x001762B4 File Offset: 0x001752B4
	public void ᜅ(bool A_0)
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
				this.ᜈ &= (spr\u20BA.OptionFlags)(-129);
				return;
			}
			break;
		}
		if (true)
		{
		}
		this.ᜈ |= spr\u20BA.OptionFlags.Formatted;
	}

	// Token: 0x0600295D RID: 10589 RVA: 0x0017631C File Offset: 0x0017531C
	public bool ᜋ()
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
		return (this.ᜈ & spr\u20BA.OptionFlags.SpaceAbove) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x0017636C File Offset: 0x0017536C
	public void ᜁ(bool A_0)
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
				this.ᜈ &= (spr\u20BA.OptionFlags)(-268435457);
				return;
			}
			break;
		}
		if (true)
		{
		}
		this.ᜈ |= spr\u20BA.OptionFlags.SpaceAbove;
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x001763D4 File Offset: 0x001753D4
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
		return (this.ᜈ & spr\u20BA.OptionFlags.SpaceBelow) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x00176424 File Offset: 0x00175424
	public void ᜂ(bool A_0)
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
				this.ᜈ &= (spr\u20BA.OptionFlags)(-536870913);
				return;
			}
			break;
		}
		this.ᜈ |= spr\u20BA.OptionFlags.SpaceBelow;
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x0017648C File Offset: 0x0017548C
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
		return (this.ᜈ & spr\u20BA.OptionFlags.ShowOutlineGroups) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x001764DC File Offset: 0x001754DC
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
				this.ᜈ &= (spr\u20BA.OptionFlags)(-257);
				return;
			}
			break;
		}
		if (true)
		{
		}
		this.ᜈ |= spr\u20BA.OptionFlags.ShowOutlineGroups;
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x00176544 File Offset: 0x00175544
	public int ᜎ()
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
		return this.ᜇ;
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x00176588 File Offset: 0x00175588
	public virtual int \u1713()
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

	// Token: 0x06002965 RID: 10597 RVA: 0x001765C8 File Offset: 0x001755C8
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
		return 16;
	}

	// Token: 0x06002966 RID: 10598 RVA: 0x00176608 File Offset: 0x00175608
	public virtual int ᜃ()
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

	// Token: 0x06002967 RID: 10599 RVA: 0x00176648 File Offset: 0x00175648
	ushort spr\u2502.ᜂ()
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
		return this.ᜇ();
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x0017668C File Offset: 0x0017568C
	void spr\u2502.ᜁ(ushort A_0)
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
		this.ᜆ(A_0);
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x001766D0 File Offset: 0x001756D0
	internal XlsWorksheet ᜉ()
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

	// Token: 0x0600296A RID: 10602 RVA: 0x00176714 File Offset: 0x00175714
	internal void ᜀ(XlsWorksheet A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x0600296B RID: 10603 RVA: 0x00176758 File Offset: 0x00175758
	public spr\u20BA()
	{
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x00176778 File Offset: 0x00175778
	public spr\u20BA(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x00176798 File Offset: 0x00175798
	public spr\u20BA(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x001767B8 File Offset: 0x001757B8
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
		this.ᜃ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜄ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜅ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜆ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜇ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜈ = (spr\u20BA.OptionFlags)A_0.ReadInt32(A_1);
	}

	// Token: 0x0600296F RID: 10607 RVA: 0x0017685C File Offset: 0x0017585C
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		if (true)
		{
		}
		for (;;)
		{
			IL_46:
			this.ᜅ(this.ᜑ() != 15);
			A_0.WriteUInt16(A_1, this.ᜃ);
			A_1 += 2;
			A_0.WriteUInt16(A_1, this.ᜄ);
			A_1 += 2;
			A_0.WriteUInt16(A_1, this.ᜅ);
			A_1 += 2;
			int num = 2;
			for (;;)
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
					switch (num)
					{
					case 0:
						goto IL_CD;
					case 1:
						goto IL_AD;
					case 2:
						if (!this.ᜅ())
						{
							num = 1;
							continue;
						}
						num = 3;
						continue;
					case 3:
						goto IL_BA;
					}
					goto IL_46;
				}
				IL_AD:
				num = 0;
			}
		}
		IL_BA:
		ushort num2 = this.ᜆ;
		goto IL_E5;
		IL_CD:
		num2 = (ushort)(this.ᜉ().PageSetup as XlsPageSetup).DefaultRowHeight;
		IL_E5:
		ushort value = num2;
		A_0.WriteUInt16(A_1, value);
		A_1 += 2;
		A_0.WriteInt32(A_1, this.ᜇ);
		A_1 += 4;
		A_0.WriteInt32(A_1, (int)this.ᜈ);
	}

	// Token: 0x040013AB RID: 5035
	public new const ushort ᜀ = 7;

	// Token: 0x040013AC RID: 5036
	public const double ᜁ = 409.5;

	// Token: 0x040013AD RID: 5037
	internal const int ᜂ = 16;

	// Token: 0x040013AE RID: 5038
	[spr\u2429(0, 2)]
	private new ushort ᜃ;

	// Token: 0x040013AF RID: 5039
	[spr\u2429(2, 2)]
	private ushort ᜄ;

	// Token: 0x040013B0 RID: 5040
	[spr\u2429(4, 2)]
	private ushort ᜅ;

	// Token: 0x040013B1 RID: 5041
	[spr\u2429(6, 2)]
	private ushort ᜆ;

	// Token: 0x040013B2 RID: 5042
	[spr\u2429(8, 4, true)]
	private int ᜇ;

	// Token: 0x040013B3 RID: 5043
	[spr\u2429(12, 4, true)]
	private spr\u20BA.OptionFlags ᜈ = spr\u20BA.OptionFlags.ShowOutlineGroups;

	// Token: 0x040013B4 RID: 5044
	private XlsWorksheet ᜉ;

	// Token: 0x020002AC RID: 684
	internal enum OptionFlags
	{
		// Token: 0x040013B6 RID: 5046
		Colapsed = 16,
		// Token: 0x040013B7 RID: 5047
		ZeroHeight = 32,
		// Token: 0x040013B8 RID: 5048
		BadFontHeight = 64,
		// Token: 0x040013B9 RID: 5049
		Formatted = 128,
		// Token: 0x040013BA RID: 5050
		ShowOutlineGroups = 256,
		// Token: 0x040013BB RID: 5051
		SpaceAbove = 268435456,
		// Token: 0x040013BC RID: 5052
		SpaceBelow = 536870912
	}
}
