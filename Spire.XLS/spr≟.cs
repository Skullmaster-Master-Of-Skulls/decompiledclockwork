using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000344 RID: 836
[spr\u2593(TBIFFRecord.Array)]
[CLSCompliant(false)]
internal class spr\u225F : BiffRecordRaw, spr\u2614, ICloneable, spr᥌
{
	// Token: 0x060032F3 RID: 13043 RVA: 0x001D34C8 File Offset: 0x001D24C8
	public int ᜉ()
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

	// Token: 0x060032F4 RID: 13044 RVA: 0x001D350C File Offset: 0x001D250C
	public void ᜂ(int A_0)
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

	// Token: 0x060032F5 RID: 13045 RVA: 0x001D3550 File Offset: 0x001D2550
	public int \u170D()
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

	// Token: 0x060032F6 RID: 13046 RVA: 0x001D3594 File Offset: 0x001D2594
	public void ᜀ(int A_0)
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

	// Token: 0x060032F7 RID: 13047 RVA: 0x001D35D8 File Offset: 0x001D25D8
	public int ᜈ()
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

	// Token: 0x060032F8 RID: 13048 RVA: 0x001D361C File Offset: 0x001D261C
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
		this.ᜄ = A_0;
	}

	// Token: 0x060032F9 RID: 13049 RVA: 0x001D3660 File Offset: 0x001D2660
	public int ᜀ()
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

	// Token: 0x060032FA RID: 13050 RVA: 0x001D36A4 File Offset: 0x001D26A4
	public void ᜁ(int A_0)
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

	// Token: 0x060032FB RID: 13051 RVA: 0x001D36E8 File Offset: 0x001D26E8
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
		return this.ᜊ;
	}

	// Token: 0x060032FC RID: 13052 RVA: 0x001D372C File Offset: 0x001D272C
	public byte[] ᜂ()
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
		return this.ᜋ;
	}

	// Token: 0x060032FD RID: 13053 RVA: 0x001D3770 File Offset: 0x001D2770
	public void ᜀ(byte[] A_0)
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
		this.ᜋ = A_0;
		this.ᜊ = ((A_0 != null) ? ((ushort)A_0.Length) : 0);
	}

	// Token: 0x060032FE RID: 13054 RVA: 0x001D37C8 File Offset: 0x001D27C8
	public Ptg[] ᜅ()
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
		return this.ᜌ;
	}

	// Token: 0x060032FF RID: 13055 RVA: 0x001D380C File Offset: 0x001D280C
	public void ᜀ(Ptg[] A_0)
	{
		int a_ = 6;
		if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("稻儽㈿⽁ㅃ⩅⥇", a_));
			}
		}
		int num;
		this.ᜋ = FormulaUtil.ᜀ(A_0, out num, ExcelVersion.Version2007);
		this.ᜊ = (ushort)num;
		this.ᜌ = A_0;
	}

	// Token: 0x06003300 RID: 13056 RVA: 0x001D3888 File Offset: 0x001D2888
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
		return this.ᜉ;
	}

	// Token: 0x06003301 RID: 13057 RVA: 0x001D38CC File Offset: 0x001D28CC
	public virtual int ᜋ()
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
		return 14;
	}

	// Token: 0x06003302 RID: 13058 RVA: 0x001D390C File Offset: 0x001D290C
	public bool ᜇ()
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

	// Token: 0x06003303 RID: 13059 RVA: 0x001D3950 File Offset: 0x001D2950
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
		this.ᜇ = A_0;
	}

	// Token: 0x06003304 RID: 13060 RVA: 0x001D3994 File Offset: 0x001D2994
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
		return this.ᜈ;
	}

	// Token: 0x06003305 RID: 13061 RVA: 0x001D39D8 File Offset: 0x001D29D8
	public void ᜁ(bool A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06003306 RID: 13062 RVA: 0x001D3A1C File Offset: 0x001D2A1C
	public ushort ᜆ()
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

	// Token: 0x06003307 RID: 13063 RVA: 0x001D3A60 File Offset: 0x001D2A60
	public spr\u225F()
	{
	}

	// Token: 0x06003308 RID: 13064 RVA: 0x001D3A74 File Offset: 0x001D2A74
	public spr\u225F(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003309 RID: 13065 RVA: 0x001D3A8C File Offset: 0x001D2A8C
	public spr\u225F(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600330A RID: 13066 RVA: 0x001D3AA0 File Offset: 0x001D2AA0
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
		A_1 = spr\u225F.ᜀ(this, A_0, A_1, A_3);
		this.ᜆ = A_0.ReadUInt16(A_1);
		this.ᜇ = A_0.ReadBit(A_1, 0);
		this.ᜈ = A_0.ReadBit(A_1, 1);
		A_1 += 2;
		this.ᜉ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜊ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		int num;
		this.ᜌ = FormulaUtil.ᜀ(A_0, A_1, (int)this.ᜊ, out num, A_3);
		this.ᜋ = new byte[num - A_1];
		A_0.ReadArray(A_1, this.ᜋ);
	}

	// Token: 0x0600330B RID: 13067 RVA: 0x001D3B6C File Offset: 0x001D2B6C
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		this.ᜉ = 0;
		int num = A_1;
		int num2;
		this.ᜋ = FormulaUtil.ᜀ(this.ᜌ, out num2, A_2);
		this.ᜊ = (ushort)num2;
		A_1 = spr\u225F.ᜁ(this, A_0, A_1, A_2);
		A_0.WriteUInt16(A_1, this.ᜆ);
		A_0.WriteBit(A_1, this.ᜇ, 0);
		A_0.WriteBit(A_1, this.ᜈ, 1);
		A_1 += 2;
		A_0.WriteInt32(A_1, this.ᜉ);
		A_1 += 4;
		A_0.WriteUInt16(A_1, this.ᜊ);
		A_1 += 2;
		this.m_iLength = A_1 - num;
		int num3 = this.ᜋ.Length;
		A_0.WriteBytes(A_1, this.ᜋ, 0, num3);
		this.m_iLength += num3;
	}

	// Token: 0x0600330C RID: 13068 RVA: 0x001D3C58 File Offset: 0x001D2C58
	public virtual int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			num = 14 + sprᡣ.ᜀ(this.ᜌ, A_0, true);
			int num2 = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					num += 10;
					goto IL_6D;
				case 1:
					if (A_0 != ExcelVersion.Version97to2003)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6D;
						}
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					return num;
				case 2:
					return num;
				}
				break;
				IL_6D:
				num2 = 2;
			}
		}
		return num;
	}

	// Token: 0x0600330D RID: 13069 RVA: 0x001D3CE0 File Offset: 0x001D2CE0
	public static int ᜁ(spr\u2614 A_0, DataProvider A_1, int A_2, ExcelVersion A_3)
	{
		int a_ = 0;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A6;
			case 1:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_E0;
					}
				}
				IL_E0:
				if (false)
				{
				}
				A_1.WriteUInt16(A_2, (ushort)A_0.ᜀ());
				A_2 += 2;
				A_1.WriteUInt16(A_2, (ushort)A_0.ᜁ());
				A_2 += 2;
				A_1.WriteByte(A_2, (byte)A_0.ᜂ());
				A_2++;
				A_1.WriteByte(A_2, (byte)A_0.ᜃ());
				A_2++;
				num = 3;
				continue;
			case 2:
				num = 7;
				continue;
			case 3:
				goto IL_13D;
			case 4:
				if (A_3 != ExcelVersion.Version2007)
				{
					num = 2;
					continue;
				}
				goto IL_53;
			case 5:
				goto IL_53;
			case 7:
				if (A_3 == ExcelVersion.Version2010)
				{
					num = 5;
					continue;
				}
				goto IL_13F;
			}
			if (A_3 == ExcelVersion.Version97to2003)
			{
				num = 1;
				continue;
			}
			num = 4;
			continue;
			IL_53:
			A_1.WriteInt32(A_2, A_0.ᜀ());
			A_2 += 4;
			A_1.WriteInt32(A_2, A_0.ᜁ());
			A_2 += 4;
			A_1.WriteInt32(A_2, A_0.ᜂ());
			A_2 += 4;
			A_1.WriteInt32(A_2, A_0.ᜃ());
			A_2 += 4;
			num = 0;
		}
		IL_A6:
		IL_13D:
		return A_2;
		IL_13F:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䀵崷䠹伻圽⼿ⱁ", a_));
	}

	// Token: 0x0600330E RID: 13070 RVA: 0x001D3E6C File Offset: 0x001D2E6C
	public static int ᜀ(spr\u2614 A_0, DataProvider A_1, int A_2, ExcelVersion A_3)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_53;
			case 2:
				if (A_3 == ExcelVersion.Version2010)
				{
					num = 0;
					continue;
				}
				goto IL_13B;
			case 3:
				if (A_3 != ExcelVersion.Version2007)
				{
					num = 4;
					continue;
				}
				goto IL_53;
			case 4:
				num = 2;
				continue;
			case 5:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_E0;
					}
				}
				IL_E0:
				if (false)
				{
				}
				A_0.ᜀ((int)A_1.ReadUInt16(A_2));
				A_2 += 2;
				A_0.ᜁ((int)A_1.ReadUInt16(A_2));
				A_2 += 2;
				A_0.ᜂ((int)A_1.ReadByte(A_2));
				A_2++;
				A_0.ᜃ((int)A_1.ReadByte(A_2));
				A_2++;
				num = 6;
				continue;
			case 6:
				goto IL_139;
			case 7:
				goto IL_A6;
			}
			if (A_3 == ExcelVersion.Version97to2003)
			{
				num = 5;
				continue;
			}
			num = 3;
			continue;
			IL_53:
			A_0.ᜀ(A_1.ReadInt32(A_2));
			A_2 += 4;
			A_0.ᜁ(A_1.ReadInt32(A_2));
			A_2 += 4;
			A_0.ᜂ(A_1.ReadInt32(A_2));
			A_2 += 4;
			A_0.ᜃ(A_1.ReadInt32(A_2));
			A_2 += 4;
			num = 7;
		}
		IL_A6:
		IL_139:
		goto IL_171;
		IL_13B:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤾⑀ㅂ㙄⹆♈╊", a_));
		IL_171:
		if (true)
		{
		}
		return A_2;
	}

	// Token: 0x0600330F RID: 13071 RVA: 0x001D3FF4 File Offset: 0x001D2FF4
	public virtual bool ᜀ(object A_0)
	{
		spr\u225F spr_u225F;
		for (;;)
		{
			IL_4C:
			spr_u225F = (A_0 as spr\u225F);
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
						num = 6;
						continue;
					case 1:
						num = 3;
						continue;
					case 2:
						if (spr_u225F != null)
						{
							goto IL_61;
						}
						goto IL_12C;
					case 3:
						if (true)
						{
						}
						if (spr_u225F.\u170D() == this.\u170D())
						{
							num = 7;
							continue;
						}
						return false;
					case 4:
						num = 5;
						continue;
					case 5:
						if (spr_u225F.ᜈ() == this.ᜈ())
						{
							num = 8;
							continue;
						}
						return false;
					case 6:
						if (spr_u225F.ᜀ() == this.ᜀ())
						{
							num = 1;
							continue;
						}
						return false;
					case 7:
						goto IL_127;
					case 8:
						num = 9;
						continue;
					case 9:
						if (spr_u225F.ᜉ() == this.ᜉ())
						{
							num = 0;
							continue;
						}
						return false;
					}
					goto IL_4C;
				}
				IL_61:
				num = 4;
			}
		}
		return false;
		IL_127:
		return Ptg.CompareArrays(spr_u225F.ᜌ, this.ᜌ);
		IL_12C:
		return base.Equals(A_0);
	}

	// Token: 0x06003310 RID: 13072 RVA: 0x001D4134 File Offset: 0x001D3134
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
		return this.ᜈ().GetHashCode() ^ this.ᜉ().GetHashCode() ^ this.ᜀ().GetHashCode() ^ this.\u170D().GetHashCode();
	}

	// Token: 0x06003311 RID: 13073 RVA: 0x001D41AC File Offset: 0x001D31AC
	public object ᜄ()
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
		spr\u225F spr_u225F = (spr\u225F)base.Clone();
		spr_u225F.ᜋ = spr\u1CD3.ᜀ(this.ᜋ);
		spr_u225F.ᜌ = spr\u1CD3.ᜀ(this.ᜌ);
		return spr_u225F;
	}

	// Token: 0x0400163B RID: 5691
	private new const int ᜀ = 14;

	// Token: 0x0400163C RID: 5692
	private const int ᜁ = 14;

	// Token: 0x0400163D RID: 5693
	[spr\u2429(0, 2)]
	private int ᜂ;

	// Token: 0x0400163E RID: 5694
	[spr\u2429(2, 2)]
	private new int ᜃ;

	// Token: 0x0400163F RID: 5695
	[spr\u2429(4, 1)]
	private int ᜄ;

	// Token: 0x04001640 RID: 5696
	[spr\u2429(5, 1)]
	private int ᜅ;

	// Token: 0x04001641 RID: 5697
	[spr\u2429(6, 2)]
	private ushort ᜆ;

	// Token: 0x04001642 RID: 5698
	[spr\u2429(6, 0, TFieldType.Bit)]
	private bool ᜇ;

	// Token: 0x04001643 RID: 5699
	[spr\u2429(6, 1, TFieldType.Bit)]
	private bool ᜈ;

	// Token: 0x04001644 RID: 5700
	[spr\u2429(8, 4, true)]
	private int ᜉ;

	// Token: 0x04001645 RID: 5701
	[spr\u2429(12, 2)]
	private ushort ᜊ;

	// Token: 0x04001646 RID: 5702
	private byte[] ᜋ;

	// Token: 0x04001647 RID: 5703
	private Ptg[] ᜌ;
}
