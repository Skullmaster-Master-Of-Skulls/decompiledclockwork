using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003FC RID: 1020
[spr\u2593(TBIFFRecord.MulRK)]
[CLSCompliant(false)]
internal class sprᨾ : spr\u22C6, sprᤞ
{
	// Token: 0x06003D5D RID: 15709 RVA: 0x00222E24 File Offset: 0x00221E24
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
		return this.ᜁ;
	}

	// Token: 0x06003D5E RID: 15710 RVA: 0x00222E68 File Offset: 0x00221E68
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

	// Token: 0x06003D5F RID: 15711 RVA: 0x00222EAC File Offset: 0x00221EAC
	public new int ᜁ()
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

	// Token: 0x06003D60 RID: 15712 RVA: 0x00222EF0 File Offset: 0x00221EF0
	public new void ᜁ(int A_0)
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

	// Token: 0x06003D61 RID: 15713 RVA: 0x00222F34 File Offset: 0x00221F34
	public new List<sprᨾ.ᜀ> ᜀ()
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

	// Token: 0x06003D62 RID: 15714 RVA: 0x00222F78 File Offset: 0x00221F78
	public new void ᜀ(List<sprᨾ.ᜀ> A_0)
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

	// Token: 0x06003D63 RID: 15715 RVA: 0x00222FBC File Offset: 0x00221FBC
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

	// Token: 0x06003D65 RID: 15717 RVA: 0x0022300C File Offset: 0x0022200C
	protected override void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		switch (0)
		{
		default:
		{
			int num3;
			for (;;)
			{
				A_1 -= 2;
				int num = base.Length - 6;
				int num2 = 10;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_75;
					case 1:
						goto IL_C8;
					case 2:
						goto IL_FA;
					case 3:
						if (A_2 == ExcelVersion.Version97to2003)
						{
							num2 = 7;
							continue;
						}
						goto IL_178;
					case 4:
					{
						if (base.Length % 6 != 0)
						{
							num2 = 2;
							continue;
						}
						num3 = A_1;
						int num4 = 0;
						num2 = 6;
						continue;
					}
					case 5:
						if (true)
						{
						}
						num -= 6;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 6:
						goto IL_75;
					case 7:
						goto IL_15B;
					case 8:
						num2 = 3;
						continue;
					case 9:
					{
						int num4;
						if (num4 >= num)
						{
							num2 = 8;
							continue;
						}
						sprᨾ.ᜀ item = new sprᨾ.ᜀ(A_0.ReadUInt16(num3), A_0.ReadInt32(num3 + 2));
						this.ᜂ.Add(item);
						num4++;
						num3 += 6;
						num2 = 0;
						continue;
					}
					case 10:
						if (A_2 != ExcelVersion.Version97to2003)
						{
							num2 = 5;
							continue;
						}
						goto IL_C8;
					}
					break;
					IL_75:
					num2 = 9;
					continue;
					IL_C8:
					num /= 6;
					this.ᜂ = new List<sprᨾ.ᜀ>(num);
					num2 = 4;
				}
			}
			IL_FA:
			throw new sprῩ();
			IL_15B:
			this.ᜃ = (int)A_0.ReadUInt16(num3);
			return;
			IL_178:
			this.ᜃ = A_0.ReadInt32(num3);
			return;
		}
		}
	}

	// Token: 0x06003D66 RID: 15718 RVA: 0x002231A0 File Offset: 0x002221A0
	protected override void ᜁ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			this.m_iLength = this.GetStoreSize(A_2);
			A_1 -= 2;
			int num = 0;
			int count = this.ᜂ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_72;
				case 1:
					if (true)
					{
					}
					goto IL_74;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8B;
					default:
						if (false)
						{
						}
						goto IL_74;
					}
					break;
				case 3:
				{
					if (num >= count)
					{
						num2 = 4;
						continue;
					}
					sprᨾ.ᜀ ᜀ = this.ᜂ[num];
					A_0.WriteUInt16(A_1, ᜀ.ᜀ());
					A_0.WriteInt32(A_1 + 2, ᜀ.ᜁ());
					num++;
					A_1 += 6;
					num2 = 2;
					continue;
				}
				case 4:
					goto IL_8B;
				case 5:
					if (A_2 == ExcelVersion.Version97to2003)
					{
						num2 = 0;
						continue;
					}
					goto IL_F7;
				}
				break;
				IL_74:
				num2 = 3;
				continue;
				IL_8B:
				num2 = 5;
			}
		}
		IL_72:
		A_0.WriteUInt16(A_1, (ushort)this.ᜃ);
		return;
		IL_F7:
		A_0.WriteInt32(A_1, this.ᜃ);
	}

	// Token: 0x06003D67 RID: 15719 RVA: 0x002232B4 File Offset: 0x002222B4
	public override int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			IL_14:
			num = this.ᜂ.Count * 6 + 6;
			for (;;)
			{
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num += 6;
						num2 = 2;
						continue;
					case 1:
						if (A_0 != ExcelVersion.Version97to2003)
						{
							num2 = 0;
							continue;
						}
						goto IL_51;
					case 2:
						goto IL_45;
					}
					goto IL_14;
				}
				IL_51:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_67;
				}
				IL_45:
				goto IL_51;
			}
		}
		IL_67:
		if (true)
		{
		}
		if (false)
		{
		}
		return num;
	}

	// Token: 0x06003D68 RID: 15720 RVA: 0x00223338 File Offset: 0x00222338
	public new int ᜁ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			IL_1C:
			num = 14;
			for (;;)
			{
				int num2 = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						if (A_0 != ExcelVersion.Version97to2003)
						{
							num2 = 2;
							continue;
						}
						goto IL_4C;
					case 1:
						goto IL_4A;
					case 2:
						num += 4;
						num2 = 1;
						continue;
					}
					goto IL_1C;
				}
				IL_4C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_62;
				}
				IL_4A:
				goto IL_4C;
			}
		}
		IL_62:
		if (false)
		{
		}
		return num;
	}

	// Token: 0x06003D69 RID: 15721 RVA: 0x002233B0 File Offset: 0x002223B0
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
		return 6;
	}

	// Token: 0x06003D6A RID: 15722 RVA: 0x002233EC File Offset: 0x002223EC
	public new TBIFFRecord ᜃ()
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
		return TBIFFRecord.RK;
	}

	// Token: 0x06003D6B RID: 15723 RVA: 0x0022342C File Offset: 0x0022242C
	public new void ᜁ(spr\u23A5 A_0)
	{
		if (A_0.get_TypeCode() != base.TypeCode)
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
				this.ᜀ(A_0);
				return;
			}
		}
		if (true)
		{
		}
		this.ᜀ((sprᨾ)A_0);
	}

	// Token: 0x06003D6C RID: 15724 RVA: 0x0022348C File Offset: 0x0022248C
	private new void ᜀ(sprᨾ A_0)
	{
		int a_ = 18;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_121;
			case 1:
				goto IL_E1;
			case 2:
				if (A_0.ᜅ() == this.ᜁ() + 1)
				{
					num = 3;
					continue;
				}
				num = 0;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_121;
				default:
					goto IL_A3;
				}
				break;
			case 5:
				goto IL_13C;
			case 6:
				goto IL_47;
			case 7:
				if (A_0.\u1714() != this.ᜀ)
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 7;
			continue;
			IL_121:
			if (A_0.ᜁ() + 1 != this.ᜅ())
			{
				goto IL_160;
			}
			num = 5;
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("╇㽉⁋ᱍ᭏", a_));
		IL_A3:
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜃ = A_0.ᜁ();
		this.ᜂ.AddRange(A_0.ᜂ);
		return;
		IL_E1:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᩇ╉㭋", a_), RecordTableEnumerator.b("ᩇ╉㭋㵍灏⅑㱓㥕ⵗ㙙㡛繝ɟݡ䑣ͥᥧὩ൫ɭ偯ᑱ᭳ѵ塷᡹፻੽ꊁ즃\ud889잋꺍秊ﺙ낝", a_));
		IL_13C:
		this.ᜁ = A_0.ᜁ;
		this.ᜂ.InsertRange(0, A_0.ᜂ);
		return;
		IL_160:
		throw new ArgumentException(RecordTableEnumerator.b("᱇㵉⍋湍ᵏ❑㡓ѕፗ穙⹛㭝͟ൡᙣɥ᭧䩩࡫ŭᕯűᩳ兵౷婹ύᅽ낏몙좟잡횣袥", a_));
	}

	// Token: 0x06003D6D RID: 15725 RVA: 0x0022360C File Offset: 0x0022260C
	public new void ᜀ(spr\u23A5 A_0)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 9;
			int num2;
			ushort a_2;
			for (;;)
			{
				if (true)
				{
				}
				bool flag;
				int num3;
				switch (num)
				{
				case 0:
					if (num2 == this.ᜃ + 1)
					{
						num = 10;
						continue;
					}
					goto IL_32B;
				case 1:
					if (this.ᜃ >= num2)
					{
						num = 17;
						continue;
					}
					goto IL_283;
				case 2:
					if (!flag)
					{
						num = 3;
						continue;
					}
					goto IL_212;
				case 3:
					num = 16;
					continue;
				case 4:
					goto IL_212;
				case 5:
					goto IL_20D;
				case 6:
					num = 1;
					continue;
				case 7:
					if (flag)
					{
						num = 13;
						continue;
					}
					goto IL_2D8;
				case 8:
					if (num2 == this.ᜁ - 1)
					{
						num = 14;
						continue;
					}
					num = 0;
					continue;
				case 10:
					goto IL_27E;
				case 11:
					if (base.\u1714() != num3)
					{
						num = 5;
						continue;
					}
					num = 15;
					continue;
				case 12:
					goto IL_2C2;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EB;
					default:
						if (false)
						{
						}
						this.ᜂ = new List<sprᨾ.ᜀ>();
						num = 12;
						continue;
					}
					break;
				case 14:
					goto IL_2A6;
				case 15:
					if (this.ᜁ <= num2)
					{
						goto IL_EB;
					}
					goto IL_283;
				case 16:
					if (this.ᜂ.Count == 0)
					{
						num = 4;
						continue;
					}
					num = 11;
					continue;
				case 17:
					goto IL_1BA;
				case 18:
					goto IL_96;
				}
				if (A_0.get_TypeCode() != TBIFFRecord.RK)
				{
					num = 18;
					continue;
				}
				num2 = A_0.ᜅ();
				num3 = A_0.ᜄ();
				a_2 = A_0.ᜆ();
				flag = (this.ᜂ == null);
				num = 2;
				continue;
				IL_EB:
				num = 6;
				continue;
				IL_212:
				num = 7;
				continue;
				IL_283:
				num = 8;
			}
			IL_96:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("崽┿⹁⡃桅᱇㍉㱋⭍ፏ㵑こ㍕", a_));
			IL_1BA:
			sprỔ sprỔ = (sprỔ)A_0;
			int index = num2 - this.ᜁ;
			sprᨾ.ᜀ ᜀ = this.ᜂ[index];
			ᜀ.ᜀ(a_2);
			ᜀ.ᜀ(sprỔ.ᜄ());
			return;
			IL_20D:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氽⼿㕁", a_));
			IL_27E:
			sprᨾ.ᜀ item = this.ᜀ((sprỔ)A_0);
			this.ᜂ.Add(item);
			this.ᜃ++;
			return;
			IL_2A6:
			sprᨾ.ᜀ item2 = this.ᜀ((sprỔ)A_0);
			this.ᜂ.Insert(0, item2);
			this.ᜁ--;
			return;
			IL_2C2:
			IL_2D8:
			sprᨾ.ᜀ item3 = this.ᜀ((sprỔ)A_0);
			this.ᜂ.Add(item3);
			this.ᜀ = A_0.ᜄ();
			this.ᜁ = (this.ᜃ = A_0.ᜅ());
			return;
			IL_32B:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("崽┿⹁⡃桅େ╉⁋㭍㵏㱑", a_));
		}
		}
	}

	// Token: 0x06003D6E RID: 15726 RVA: 0x00223958 File Offset: 0x00222958
	private new sprᨾ.ᜀ ᜀ(sprỔ A_0)
	{
		int a_ = 4;
		if (A_0 != null)
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
				return new sprᨾ.ᜀ(A_0.\u1712(), A_0.ᜄ());
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹圻", a_));
	}

	// Token: 0x06003D6F RID: 15727 RVA: 0x002239C8 File Offset: 0x002229C8
	public new spr\u23A5[] ᜃ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0 > this.ᜃ)
					{
						num = 1;
						continue;
					}
					goto IL_9C;
				case 1:
					goto IL_9A;
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
						break;
					}
					break;
				case 3:
					num = 0;
					continue;
				}
				if (A_0 < this.ᜁ)
				{
					break;
				}
				num = 3;
			}
			IL_72:
			return new spr\u23A5[]
			{
				this
			};
			IL_9A:
			goto IL_72;
			IL_9C:
			int num2 = this.ᜁ;
			spr\u23A5 spr_u23A = this.ᜀ(this.ᜁ, A_0 - 1);
			spr\u23A5 spr_u23A2 = this.ᜀ(A_0 + 1, this.ᜃ);
			return new spr\u23A5[]
			{
				spr_u23A,
				spr_u23A2
			};
		}
		}
	}

	// Token: 0x06003D70 RID: 15728 RVA: 0x00223AB4 File Offset: 0x00222AB4
	private new spr\u23A5 ᜀ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				int num = 5;
				sprᨾ sprᨾ;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A8;
					case 1:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 2;
							continue;
						}
						List<sprᨾ.ᜀ> list;
						int num4;
						list[num2] = this.ᜂ[num4];
						num2++;
						num4++;
						num = 6;
						continue;
					}
					case 2:
						goto IL_BE;
					case 3:
						goto IL_73;
					case 4:
					{
						if (A_0 == A_1)
						{
							num = 7;
							continue;
						}
						sprᨾ = (sprᨾ)spr\u175E.ᜀ(TBIFFRecord.MulRK);
						sprᨾ.ᜁ = A_0;
						sprᨾ.ᜃ = A_1;
						sprᨾ.ᜀ = this.ᜀ;
						int num3 = A_1 - A_0 + 1;
						List<sprᨾ.ᜀ> list = new List<sprᨾ.ᜀ>(num3);
						sprᨾ.ᜂ = list;
						int num2 = 0;
						int num4 = A_0 - this.ᜁ;
						num = 0;
						continue;
					}
					case 6:
						goto IL_A8;
					case 7:
						goto IL_DC;
					}
					if (A_0 > A_1)
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
					IL_A8:
					num = 1;
				}
				IL_73:
				break;
				IL_BE:
				if (true)
				{
				}
				return sprᨾ;
				IL_DC:
				return this.ᜀ(A_0);
			}
			}
			return null;
		}
	}

	// Token: 0x06003D71 RID: 15729 RVA: 0x00223C04 File Offset: 0x00222C04
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
		sprỔ sprỔ = (sprỔ)spr\u175E.ᜀ(TBIFFRecord.RK);
		sprᨾ.ᜀ ᜀ = this.ᜂ[A_0 - this.ᜁ];
		sprỔ.ᜁ(ᜀ.ᜀ());
		sprỔ.ᜄ(ᜀ.ᜁ());
		sprỔ.ᜇ(base.\u1714());
		sprỔ.ᜆ(A_0);
		return sprỔ;
	}

	// Token: 0x06003D72 RID: 15730 RVA: 0x00223C90 File Offset: 0x00222C90
	public new BiffRecordRaw[] ᜀ(bool A_0)
	{
		switch (0)
		{
		default:
		{
			BiffRecordRaw[] array;
			for (;;)
			{
				IL_27:
				int num;
				int num2;
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6A:
					goto IL_6C;
				default:
					if (false)
					{
					}
					array = new BiffRecordRaw[this.ᜃ - this.ᜁ + 1];
					num = this.ᜁ;
					num2 = 0;
					num3 = 0;
					break;
				}
				for (;;)
				{
					IL_10:
					switch (num3)
					{
					case 0:
						goto IL_6A;
					case 1:
						goto IL_C0;
					case 2:
						return array;
					case 3:
					{
						if (true)
						{
						}
						if (num > this.ᜃ)
						{
							num3 = 2;
							continue;
						}
						spr\u23A5 spr_u23A = this.ᜀ(num);
						array[num2] = (BiffRecordRaw)spr_u23A;
						num++;
						num2++;
						num3 = 1;
						continue;
					}
					}
					goto IL_27;
				}
				IL_C0:
				IL_6C:
				num3 = 3;
				goto IL_10;
			}
			return array;
		}
		}
	}

	// Token: 0x04001A79 RID: 6777
	public new const int ᜀ = 6;

	// Token: 0x04001A7A RID: 6778
	public new const int ᜁ = 6;

	// Token: 0x04001A7B RID: 6779
	private new List<sprᨾ.ᜀ> ᜂ;

	// Token: 0x04001A7C RID: 6780
	private new int ᜃ;

	// Token: 0x020003FD RID: 1021
	[CLSCompliant(false)]
	internal new class ᜀ
	{
		// Token: 0x06003D73 RID: 15731 RVA: 0x00223D60 File Offset: 0x00222D60
		private ᜀ()
		{
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x00223D74 File Offset: 0x00222D74
		public ᜀ(ushort A_0, int A_1)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x00223D98 File Offset: 0x00222D98
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

		// Token: 0x06003D76 RID: 15734 RVA: 0x00223DDC File Offset: 0x00222DDC
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

		// Token: 0x06003D77 RID: 15735 RVA: 0x00223E20 File Offset: 0x00222E20
		public int ᜁ()
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

		// Token: 0x06003D78 RID: 15736 RVA: 0x00223E64 File Offset: 0x00222E64
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
			this.ᜁ = A_0;
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x00223EA8 File Offset: 0x00222EA8
		public double ᜂ()
		{
			switch (0)
			{
			default:
			{
				double num3;
				double num4;
				for (;;)
				{
					bool flag = (this.ᜁ & 2) == 2;
					bool flag2 = (this.ᜁ & 1) == 1;
					long num = (long)(this.ᜁ >> 2);
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num3 = (double)num;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num2 = 1;
								continue;
							}
							break;
						case 1:
							if (!flag2)
							{
								num2 = 5;
								continue;
							}
							goto IL_8B;
						case 2:
							if (!flag2)
							{
								num2 = 3;
								continue;
							}
							goto IL_EA;
						case 3:
							return num4;
						case 4:
							if (flag)
							{
								num2 = 0;
								continue;
							}
							num4 = spr\u2620.ᜀ(num << 34);
							num2 = 2;
							continue;
						case 5:
							return num3;
						}
						break;
					}
				}
				return num4;
				IL_8B:
				return num3 / 100.0;
				IL_EA:
				return num4 / 100.0;
			}
			}
		}

		// Token: 0x04001A7D RID: 6781
		private ushort ᜀ;

		// Token: 0x04001A7E RID: 6782
		private int ᜁ;
	}
}
