using System;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000484 RID: 1156
[spr\u2400(FormulaToken.tAreaN2)]
[spr\u2400(FormulaToken.tAreaN1)]
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tAreaN3)]
internal class spr\u2596 : sprᲔ
{
	// Token: 0x06004719 RID: 18201 RVA: 0x002B2B14 File Offset: 0x002B1B14
	public spr\u2596()
	{
	}

	// Token: 0x0600471A RID: 18202 RVA: 0x002B2B28 File Offset: 0x002B1B28
	public spr\u2596(string A_0, IWorkbook A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x0600471B RID: 18203 RVA: 0x002B2B40 File Offset: 0x002B1B40
	public spr\u2596(int A_0, int A_1, string A_2, string A_3, string A_4, string A_5, bool A_6, IWorkbook A_7)
	{
		base.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x0600471C RID: 18204 RVA: 0x002B2B74 File Offset: 0x002B1B74
	public spr\u2596(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600471D RID: 18205 RVA: 0x002B2B8C File Offset: 0x002B1B8C
	public new short ᜄ()
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
		return (short)((ushort)base.ᜄ());
	}

	// Token: 0x0600471E RID: 18206 RVA: 0x002B2BD0 File Offset: 0x002B1BD0
	public void ᜁ(short A_0)
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
		base.ᜃ((int)((ushort)A_0));
	}

	// Token: 0x0600471F RID: 18207 RVA: 0x002B2C14 File Offset: 0x002B1C14
	public new short ᜂ()
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
		return (short)((ushort)base.ᜂ());
	}

	// Token: 0x06004720 RID: 18208 RVA: 0x002B2C58 File Offset: 0x002B1C58
	public void ᜀ(short A_0)
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
		base.ᜅ((int)((ushort)A_0));
	}

	// Token: 0x06004721 RID: 18209 RVA: 0x002B2C9C File Offset: 0x002B1C9C
	public override Ptg ᜀ(IWorkbook A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num5;
			int num6;
			int num7;
			int num8;
			for (;;)
			{
				bool flag = base.ᜀ(A_0);
				bool flag2 = base.ᜁ(A_0);
				int num = 12;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int num9;
					switch (num)
					{
					case 0:
						num2 = A_2 + (int)this.ᜄ();
						goto IL_1F3;
					case 1:
						num3 = (int)this.ᜂ();
						goto IL_17C;
					case 2:
						num = 20;
						continue;
					case 3:
						num = 17;
						continue;
					case 4:
						num = 7;
						continue;
					case 5:
						goto IL_2AF;
					case 6:
						goto IL_2E2;
					case 7:
						if (flag)
						{
							goto IL_2A1;
						}
						num = 0;
						continue;
					case 8:
						num4 = A_1 + base.ᜉ();
						goto IL_1A6;
					case 9:
						if (base.ᜌ())
						{
							num = 3;
							continue;
						}
						goto IL_BB;
					case 10:
						num5 = (int)((byte)num5);
						num6 = (int)((byte)num6);
						num7 = (int)((ushort)num7);
						num8 = (int)((ushort)num8);
						num = 6;
						continue;
					case 11:
						goto IL_EB;
					case 12:
						if (base.ᜈ())
						{
							num = 4;
							continue;
						}
						goto IL_2AF;
					case 13:
						goto IL_BB;
					case 14:
						num9 = A_1 + base.ᜋ();
						goto IL_14C;
					case 15:
						if (!base.ᜏ())
						{
							goto IL_138;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A1;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 24;
							continue;
						}
						break;
					case 16:
						if (base.ᜇ())
						{
							num = 2;
							continue;
						}
						goto IL_EB;
					case 17:
						if (flag)
						{
							num = 13;
							continue;
						}
						num = 22;
						continue;
					case 18:
						goto IL_138;
					case 19:
						num9 = base.ᜋ();
						goto IL_14C;
					case 20:
						if (flag2)
						{
							num = 11;
							continue;
						}
						num = 8;
						continue;
					case 21:
						if (A_0.Version == ExcelVersion.Version97to2003)
						{
							num = 10;
							continue;
						}
						goto IL_2E4;
					case 22:
						num3 = A_2 + (int)this.ᜂ();
						goto IL_17C;
					case 23:
						num4 = base.ᜉ();
						goto IL_1A6;
					case 24:
						num = 25;
						continue;
					case 25:
						if (flag2)
						{
							num = 18;
							continue;
						}
						num = 14;
						continue;
					case 26:
						num2 = (int)this.ᜄ();
						goto IL_1F3;
					}
					break;
					IL_BB:
					num = 1;
					continue;
					IL_EB:
					num = 23;
					continue;
					IL_138:
					num = 19;
					continue;
					IL_14C:
					num7 = num9;
					num = 9;
					continue;
					IL_17C:
					num6 = num3;
					num = 16;
					continue;
					IL_1A6:
					num8 = num4;
					num = 21;
					continue;
					IL_1F3:
					num5 = num2;
					num = 15;
					continue;
					IL_2A1:
					num = 5;
					continue;
					IL_2AF:
					num = 26;
				}
			}
			IL_2E2:
			IL_2E4:
			Ptg ptg = new sprᲔ(num7, num5, num8, num6, base.\u170D(), base.ᜐ());
			int a_ = spr\u2596.ᜀ(this.TokenCode);
			ptg.TokenCode = sprᲔ.ᜀ(a_);
			return ptg;
		}
		}
	}

	// Token: 0x06004722 RID: 18210 RVA: 0x002B2FC4 File Offset: 0x002B1FC4
	public new static int ᜀ(FormulaToken A_0)
	{
		int a_ = 18;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != FormulaToken.tAreaN3)
					{
						num = 3;
						continue;
					}
					return 3;
				case 1:
					if (A_0 != FormulaToken.tAreaN1)
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_67;
					default:
						goto IL_A0;
					}
					break;
				case 2:
					goto IL_58;
				case 3:
					num = 2;
					continue;
				case 4:
					num = 0;
					continue;
				case 5:
					num = 6;
					continue;
				case 6:
					if (A_0 != FormulaToken.tAreaN2)
					{
						goto IL_67;
					}
					goto IL_46;
				}
				break;
				IL_67:
				num = 4;
			}
		}
		IL_46:
		if (true)
		{
		}
		return 2;
		IL_58:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ⑉⡋⭍⡏", a_));
		IL_A0:
		if (false)
		{
		}
		return 1;
	}

	// Token: 0x06004723 RID: 18211 RVA: 0x002B3098 File Offset: 0x002B2098
	public new static FormulaToken ᜀ(int A_0)
	{
		int a_ = 3;
		for (;;)
		{
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch (A_0)
						{
						case 1:
							return FormulaToken.tAreaN1;
						case 2:
							return FormulaToken.tAreaN2;
						case 3:
							return FormulaToken.tAreaN3;
						default:
							num = 1;
							continue;
						}
						break;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_65;
					}
					break;
				}
			}
			IL_65:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_80;
			}
		}
		return FormulaToken.tAreaN2;
		IL_80:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倸唺夼娾㥀", a_));
	}

	// Token: 0x06004724 RID: 18212 RVA: 0x002B3140 File Offset: 0x002B2140
	public override byte[] ᜀ(ExcelVersion A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			int num2;
			int num3;
			int num4;
			byte[] array;
			int num5;
			int num6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (num2 > 255)
					{
						num = 2;
						continue;
					}
					goto IL_10C;
				case 1:
					num = 5;
					continue;
				case 2:
					goto IL_67;
				case 3:
					num = 0;
					continue;
				case 4:
					if (num3 <= 65535)
					{
						num = 1;
						continue;
					}
					goto IL_67;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (num4 > 65535)
						{
							goto IL_67;
						}
						break;
					}
					num = 9;
					continue;
				case 6:
					array = base.ᜀ(A_0);
					num5 = 1;
					num3 = base.ᜋ() - this.ᜀ;
					num4 = base.ᜉ() - this.ᜀ;
					num6 = (int)this.ᜄ() - this.ᜁ;
					num2 = (int)this.ᜂ() - this.ᜁ;
					num = 4;
					continue;
				case 8:
					if (num6 <= 255)
					{
						num = 3;
						continue;
					}
					goto IL_67;
				case 9:
					num = 8;
					continue;
				case 10:
					goto IL_7E;
				}
				if (A_0 == ExcelVersion.Version97to2003)
				{
					num = 6;
					continue;
				}
				goto IL_1D1;
				IL_67:
				FormulaToken formulaToken = this.ᜅ();
				array[0] = (byte)formulaToken;
				num = 10;
			}
			IL_7E:
			IL_10C:
			if (true)
			{
			}
			BitConverter.GetBytes((ushort)num3).CopyTo(array, num5);
			num5 += 2;
			BitConverter.GetBytes((ushort)num4).CopyTo(array, num5);
			num5 += 2;
			array[num5++] = (byte)num6;
			array[num5++] = this.ᜃ;
			array[num5++] = (byte)num2;
			array[num5] = this.ᜅ;
			return array;
			IL_1D1:
			return base.ᜀ(A_0);
		}
		}
	}

	// Token: 0x04002052 RID: 8274
	private new int ᜀ;

	// Token: 0x04002053 RID: 8275
	private new int ᜁ;
}
