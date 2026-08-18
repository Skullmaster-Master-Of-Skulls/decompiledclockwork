using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003FE RID: 1022
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.MulBlank)]
internal class sprᲀ : spr\u22C6, sprᤞ
{
	// Token: 0x06003D7A RID: 15738 RVA: 0x00223FAC File Offset: 0x00222FAC
	public int ᜆ()
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

	// Token: 0x06003D7B RID: 15739 RVA: 0x00223FF0 File Offset: 0x00222FF0
	public new void ᜂ(int A_0)
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

	// Token: 0x06003D7C RID: 15740 RVA: 0x00224034 File Offset: 0x00223034
	public List<ushort> ᜄ()
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

	// Token: 0x06003D7D RID: 15741 RVA: 0x00224078 File Offset: 0x00223078
	public new void ᜀ(List<ushort> A_0)
	{
		int a_ = 0;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 != null)
			{
				this.ᜃ = A_0;
				return;
			}
			break;
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䀵夷嘹䤻嬽", a_));
	}

	// Token: 0x06003D7E RID: 15742 RVA: 0x002240DC File Offset: 0x002230DC
	public new int ᜁ()
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

	// Token: 0x06003D7F RID: 15743 RVA: 0x00224120 File Offset: 0x00223120
	public new void ᜁ(int A_0)
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

	// Token: 0x06003D80 RID: 15744 RVA: 0x00224164 File Offset: 0x00223164
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

	// Token: 0x06003D82 RID: 15746 RVA: 0x002241B4 File Offset: 0x002231B4
	protected override void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int a_ = 5;
		for (;;)
		{
			A_1 -= 2;
			int num = 9;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					if (A_2 != ExcelVersion.Version97to2003)
					{
						num = 12;
						continue;
					}
					goto IL_171;
				case 1:
					goto IL_64;
				case 2:
					goto IL_135;
				case 3:
					this.ᜄ = (int)A_0.ReadUInt16(A_1);
					num = 7;
					continue;
				case 4:
					goto IL_171;
				case 5:
					goto IL_135;
				case 6:
					if (A_2 == ExcelVersion.Version97to2003)
					{
						num = 3;
						continue;
					}
					this.ᜄ = A_0.ReadInt32(A_1);
					num = 8;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						goto IL_104;
					}
					break;
				case 8:
					goto IL_16F;
				case 9:
					goto IL_52;
				case 10:
					if (num2 >= num3)
					{
						num = 11;
						continue;
					}
					this.ᜃ.Add(A_0.ReadUInt16(A_1));
					A_1 += 2;
					num2++;
					num = 5;
					continue;
				case 11:
					num = 6;
					continue;
				case 12:
					num3 -= 6;
					num = 4;
					continue;
				}
				break;
				IL_52:
				if (this.m_iLength % 2 != 0)
				{
					num = 1;
					continue;
				}
				num3 = this.m_iLength - 6;
				num = 0;
				continue;
				IL_135:
				num = 10;
				continue;
				IL_171:
				num3 /= 2;
				this.ᜃ = new List<ushort>(num3);
				num2 = 0;
				num = 2;
			}
		}
		IL_64:
		throw new sprῩ(RecordTableEnumerator.b("ጺᴼ猾⑀ⵂ≄㍆ⅈ歊恌潎材獒籔睖籘筚潜罞䁠幢䕤坦", a_));
		IL_104:
		if (false)
		{
		}
		if (true)
		{
		}
		IL_16F:
		this.ᜀ();
	}

	// Token: 0x06003D83 RID: 15747 RVA: 0x00224368 File Offset: 0x00223368
	protected override void ᜁ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			this.m_iLength = this.GetStoreSize(A_2);
			A_1 -= 2;
			int count = this.ᜃ.Count;
			int num = 0;
			if (true)
			{
			}
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_78;
					default:
						if (false)
						{
						}
						goto IL_70;
					}
					break;
				case 1:
					goto IL_78;
				case 2:
					goto IL_70;
				case 3:
					goto IL_87;
				}
				break;
				IL_70:
				num2 = 1;
				continue;
				IL_78:
				if (num >= count)
				{
					num2 = 3;
				}
				else
				{
					A_0.WriteUInt16(A_1, this.ᜃ[num]);
					A_1 += 2;
					num++;
					num2 = 2;
				}
			}
		}
		IL_87:
		A_0.WriteUInt16(A_1, (ushort)this.ᜄ);
	}

	// Token: 0x06003D84 RID: 15748 RVA: 0x00224438 File Offset: 0x00223438
	private new void ᜀ()
	{
		int a_ = 5;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (this.ᜄ - this.ᜁ + 1 == this.ᜃ.Count)
			{
				return;
			}
			break;
		}
		if (true)
		{
		}
		throw new sprῩ(RecordTableEnumerator.b("嘺戼䨾㉀ག⑄㑆㵈ࡊ≌⍎煐繒畔㩖٘⹚⹜ᥞࡠᅢᙤ፦⩨ѪŬ佮婰卲䑴坶塸䙺嵼ቾ\ude80첈歷즎ﺐ튚ﮞ쒠\udba2삤풦螨좬솮횰잲\uddb4", a_));
	}

	// Token: 0x06003D85 RID: 15749 RVA: 0x002244B0 File Offset: 0x002234B0
	public new spr\u171D ᜃ(int A_0)
	{
		int a_ = 5;
		for (;;)
		{
			IL_09:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A5;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					num = 3;
					continue;
				case 3:
					if (A_0 > this.ᜄ)
					{
						num = 0;
						continue;
					}
					goto IL_A7;
				}
				if (A_0 < this.ᜁ)
				{
					goto IL_6A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_09;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}
		IL_6A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("刺縼倾ⵀ㙂⡄⥆H╊⥌⩎⥐", a_), RecordTableEnumerator.b("洺尼匾㑀♂敄⑆⡈╊⍌⁎═獒㝔㉖祘㝚㡜ⱞበ䍢ࡤ㡦ᱨᡪ⭬ٮͰrŴ㑶ᙸ᝺嵼Ṿꖄﮈﮎ떔漢뾞철ﲢ키풦쪪\udeac\udbae\udcb2\ud9b4", a_));
		IL_A5:
		goto IL_6A;
		IL_A7:
		int index = A_0 - this.ᜁ;
		ushort a_2 = this.ᜃ[index];
		spr\u171D spr_u171D = (spr\u171D)spr\u175E.ᜀ(TBIFFRecord.Blank);
		spr_u171D.ᜇ(this.ᜀ);
		spr_u171D.ᜆ(A_0);
		spr_u171D.ᜁ(a_2);
		return spr_u171D;
	}

	// Token: 0x06003D86 RID: 15750 RVA: 0x002245A8 File Offset: 0x002235A8
	public override int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			num = this.ᜃ.Count * 2 + 6;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return num;
				case 1:
					if (true)
					{
					}
					if (A_0 != ExcelVersion.Version97to2003)
					{
						num2 = 2;
						continue;
					}
					return num;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
						if (false)
						{
						}
						num += 6;
						num2 = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x06003D87 RID: 15751 RVA: 0x0022462C File Offset: 0x0022362C
	public new static void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3, int A_4)
	{
		int a_ = 16;
		int num;
		for (;;)
		{
			for (;;)
			{
				num = A_1 + A_2 + 4;
				if (true)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_B4;
					case 1:
						num2 = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							switch (A_3)
							{
							case ExcelVersion.Version97to2003:
								goto IL_90;
							case ExcelVersion.Version2007:
							case ExcelVersion.Version2010:
								goto IL_77;
							default:
								num2 = 1;
								continue;
							}
							break;
						}
						break;
					}
					break;
				}
			}
		}
		IL_77:
		num -= 4;
		int num3 = A_0.ReadInt32(num) + A_4;
		A_0.WriteInt32(num, (int)((short)num3));
		return;
		IL_90:
		num -= 2;
		num3 = (int)A_0.ReadInt16(num) + A_4;
		A_0.WriteInt16(num, (short)num3);
		return;
		IL_B4:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ぅⵇ㡉㽋❍㽏㱑", a_));
	}

	// Token: 0x06003D88 RID: 15752 RVA: 0x00224704 File Offset: 0x00223704
	public new int ᜁ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			num = 10;
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
						if (false)
						{
						}
						num += 4;
						num2 = 1;
						continue;
					}
					break;
				case 1:
					return num;
				case 2:
					if (A_0 != ExcelVersion.Version97to2003)
					{
						num2 = 0;
						continue;
					}
					return num;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x06003D89 RID: 15753 RVA: 0x0022477C File Offset: 0x0022377C
	public int ᜅ()
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
		return 2;
	}

	// Token: 0x06003D8A RID: 15754 RVA: 0x002247B8 File Offset: 0x002237B8
	public new TBIFFRecord ᜃ()
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
		return TBIFFRecord.Blank;
	}

	// Token: 0x06003D8B RID: 15755 RVA: 0x002247F8 File Offset: 0x002237F8
	public new void ᜀ(spr\u23A5 A_0)
	{
		int a_ = 18;
		int num;
		ushort value;
		for (;;)
		{
			for (;;)
			{
				num = A_0.ᜅ();
				int num2 = A_0.ᜄ();
				value = A_0.ᜆ();
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_86;
					case 1:
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
							num3 = 4;
							continue;
						}
						break;
					case 2:
						if (this.ᜁ <= num)
						{
							num3 = 1;
							continue;
						}
						goto IL_59;
					case 3:
						if (base.\u1714() == num2)
						{
							num3 = 5;
							continue;
						}
						goto IL_59;
					case 4:
						if (this.ᜄ < num)
						{
							num3 = 0;
							continue;
						}
						goto IL_D7;
					case 5:
						num3 = 2;
						continue;
					}
					break;
				}
			}
		}
		IL_59:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭇⽉⁋≍繏ᅑ㭓㩕ⵗ㝙㉛", a_));
		IL_86:
		goto IL_59;
		IL_D7:
		this.ᜃ[num - this.ᜁ] = value;
	}

	// Token: 0x06003D8C RID: 15756 RVA: 0x002248F0 File Offset: 0x002238F0
	public spr\u23A5[] ᜄ(int A_0)
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (A_0 > this.ᜄ)
						{
							num = 3;
							continue;
						}
						goto IL_9C;
					case 2:
						num = 1;
						continue;
					case 3:
						goto IL_90;
					}
					if (A_0 < this.ᜁ)
					{
						goto IL_68;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
				break;
			}
			}
		}
		IL_68:
		return new spr\u23A5[]
		{
			this
		};
		IL_90:
		goto IL_68;
		IL_9C:
		int num2 = this.ᜁ;
		spr\u23A5 spr_u23A = this.ᜀ(this.ᜁ, A_0 - 1);
		spr\u23A5 spr_u23A2 = this.ᜀ(A_0 + 1, this.ᜄ);
		return new spr\u23A5[]
		{
			spr_u23A,
			spr_u23A2
		};
	}

	// Token: 0x06003D8D RID: 15757 RVA: 0x002249DC File Offset: 0x002239DC
	private new spr\u23A5 ᜀ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			sprᲀ sprᲀ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_13A;
				case 2:
					goto IL_A5;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13A;
					default:
						goto IL_DC;
					}
					break;
				case 4:
					goto IL_8C;
				case 5:
					goto IL_4D;
				case 6:
				{
					if (A_0 == A_1)
					{
						num = 3;
						continue;
					}
					sprᲀ = (sprᲀ)spr\u175E.ᜀ(TBIFFRecord.MulBlank);
					sprᲀ.ᜁ = A_0;
					sprᲀ.ᜄ = A_1;
					sprᲀ.ᜀ = this.ᜀ;
					int num2 = A_1 - A_0 + 1;
					List<ushort> list = new List<ushort>(num2);
					sprᲀ.ᜃ = list;
					int num3 = 0;
					int num4 = A_0 - this.ᜁ;
					num = 0;
					continue;
				}
				case 7:
				{
					int num2;
					int num3;
					if (num3 >= num2)
					{
						num = 2;
						continue;
					}
					List<ushort> list;
					int num4;
					list[num3] = this.ᜃ[num4];
					num3++;
					num4++;
					num = 4;
					continue;
				}
				}
				if (A_0 > A_1)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
				IL_8C:
				num = 7;
				continue;
				IL_13A:
				goto IL_8C;
			}
			IL_4D:
			return null;
			IL_A5:
			if (true)
			{
			}
			return sprᲀ;
			IL_DC:
			if (false)
			{
			}
			return this.ᜀ(A_0);
		}
		}
	}

	// Token: 0x06003D8E RID: 15758 RVA: 0x00224B34 File Offset: 0x00223B34
	private new spr\u23A5 ᜀ(int A_0)
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
		spr\u171D spr_u171D = (spr\u171D)spr\u175E.ᜀ(TBIFFRecord.Blank);
		spr_u171D.ᜁ(this.ᜃ[A_0 - this.ᜁ]);
		spr_u171D.ᜇ(base.\u1714());
		spr_u171D.ᜆ(A_0);
		return spr_u171D;
	}

	// Token: 0x06003D8F RID: 15759 RVA: 0x00224BAC File Offset: 0x00223BAC
	public new BiffRecordRaw[] ᜀ(bool A_0)
	{
		int num;
		int num2;
		BiffRecordRaw[] array;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_2C:
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					if (num2 > this.ᜄ)
					{
						num = 1;
						continue;
					}
					spr\u23A5 spr_u23A = this.ᜀ(num2);
					array[num3] = (BiffRecordRaw)spr_u23A;
					num2++;
					num3++;
					num = 3;
					continue;
				}
				case 1:
					return array;
				case 2:
					goto IL_76;
				case 3:
					goto IL_76;
				}
				goto IL_43;
				IL_76:
				num = 0;
			}
			return array;
		default:
			if (false)
			{
			}
			num = 0;
			switch (num)
			{
			}
			break;
		}
		IL_43:
		array = new BiffRecordRaw[this.ᜄ - this.ᜁ + 1];
		num2 = this.ᜁ;
		num3 = 0;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x04001A7F RID: 6783
	public new const int ᜀ = 6;

	// Token: 0x04001A80 RID: 6784
	private new const int ᜁ = 6;

	// Token: 0x04001A81 RID: 6785
	public new const int ᜂ = 2;

	// Token: 0x04001A82 RID: 6786
	private new List<ushort> ᜃ;

	// Token: 0x04001A83 RID: 6787
	private int ᜄ;
}
