using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000332 RID: 818
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tRefN1)]
[spr\u2400(FormulaToken.tRefN2)]
[spr\u2400(FormulaToken.tRefN3)]
internal class sprᦈ : sprᦊ
{
	// Token: 0x06003245 RID: 12869 RVA: 0x001CFE9C File Offset: 0x001CEE9C
	public sprᦈ()
	{
	}

	// Token: 0x06003246 RID: 12870 RVA: 0x001CFEB0 File Offset: 0x001CEEB0
	public sprᦈ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003247 RID: 12871 RVA: 0x001CFEC8 File Offset: 0x001CEEC8
	public sprᦈ(int A_0, int A_1, string A_2, string A_3, bool A_4)
	{
		base.ᜀ(A_0, A_1, A_2, A_3, A_4);
		this.ᜃ(this.ᜆ() - A_1);
		this.ᜂ(this.ᜇ() - A_0);
		base.ᜀ(true);
		base.ᜁ(true);
	}

	// Token: 0x06003248 RID: 12872 RVA: 0x001CFF14 File Offset: 0x001CEF14
	public virtual Ptg ᜁ(IWorkbook A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			int num4;
			int num5;
			for (;;)
			{
				int num3;
				int num2;
				int num6;
				int num7;
				int num9;
				int num8;
				switch (num)
				{
				case 0:
					num = 9;
					continue;
				case 1:
					goto IL_138;
				case 2:
					if (A_0.Version != ExcelVersion.Version97to2003)
					{
						num = 4;
						continue;
					}
					goto IL_D8;
				case 3:
					num2 = A_1 + num3;
					goto IL_1A0;
				case 4:
					num = 12;
					continue;
				case 6:
					num4 = (int)((byte)num4);
					num5 = (int)((ushort)num5);
					num = 1;
					continue;
				case 7:
					num6 = this.ᜆ();
					goto IL_151;
				case 8:
					if (A_0.Version == ExcelVersion.Version97to2003)
					{
						num = 6;
						continue;
					}
					goto IL_1FB;
				case 9:
					num6 = base.ᜆ();
					goto IL_151;
				case 10:
					if (!this.ᜃ())
					{
						num = 15;
						continue;
					}
					num = 3;
					continue;
				case 11:
					num = 16;
					continue;
				case 12:
					num7 = base.ᜇ();
					goto IL_100;
				case 13:
					num7 = this.ᜇ();
					goto IL_100;
				case 14:
					if (!this.ᜅ())
					{
						num = 11;
						continue;
					}
					num = 18;
					continue;
				case 15:
					num = 17;
					continue;
				case 16:
					if (true)
					{
					}
					num8 = num9;
					goto IL_1C6;
				case 17:
					num2 = num3;
					goto IL_1A0;
				case 18:
					num8 = A_2 + num9;
					goto IL_1C6;
				}
				if (A_0.Version != ExcelVersion.Version97to2003)
				{
					num = 0;
					continue;
				}
				num = 7;
				continue;
				IL_D8:
				num = 13;
				continue;
				IL_151:
				num9 = num6;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D8;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				IL_100:
				num3 = num7;
				num = 14;
				continue;
				IL_1A0:
				num5 = num2;
				num = 8;
				continue;
				IL_1C6:
				num4 = num8;
				num = 10;
			}
			IL_138:
			IL_1FB:
			Ptg ptg = new sprᦊ(num5, num4, base.ᜊ());
			int a_ = sprᦈ.ᜀ(this.TokenCode);
			ptg.TokenCode = sprᦊ.ᜀ(a_);
			return ptg;
		}
		}
	}

	// Token: 0x06003249 RID: 12873 RVA: 0x001D0148 File Offset: 0x001CF148
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		switch (0)
		{
		default:
		{
			short num2;
			int a_;
			for (;;)
			{
				for (;;)
				{
					short num = (short)this.ᜇ();
					num2 = (short)this.ᜆ();
					int num3 = 6;
					for (;;)
					{
						int num4;
						switch (num3)
						{
						case 0:
							num3 = 3;
							continue;
						case 1:
							if (!this.ᜅ())
							{
								num3 = 4;
								continue;
							}
							num3 = 7;
							continue;
						case 2:
							goto IL_9D;
						case 3:
							num4 = this.ᜇ();
							goto IL_E9;
						case 4:
							num3 = 2;
							continue;
						case 5:
							num4 = Math.Abs(A_1 + (int)num - 1);
							goto IL_E9;
						case 6:
							if (this.ᜃ())
							{
								num3 = 5;
								continue;
							}
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
								num3 = 0;
								continue;
							}
							break;
						case 7:
							goto IL_B1;
						}
						break;
						IL_E9:
						a_ = num4;
						num3 = 1;
					}
				}
			}
			IL_9D:
			int num5 = this.ᜆ();
			goto IL_10C;
			IL_B1:
			num5 = Math.Abs(A_2 + (int)num2 - 1);
			IL_10C:
			int a_2 = num5;
			return sprᦊ.ᜀ(A_1, A_2, a_, a_2, this.ᜃ(), this.ᜅ(), A_3);
		}
		}
	}

	// Token: 0x0600324A RID: 12874 RVA: 0x001D027C File Offset: 0x001CF27C
	public new static int ᜀ(FormulaToken A_0)
	{
		int a_ = 17;
		for (;;)
		{
			for (;;)
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 != FormulaToken.tRefN2)
						{
							num = 2;
							continue;
						}
						return 2;
					case 1:
						if (true)
						{
						}
						if (A_0 != FormulaToken.tRefN3)
						{
							num = 3;
							continue;
						}
						return 3;
					case 2:
						num = 1;
						continue;
					case 3:
						num = 6;
						continue;
					case 4:
						if (A_0 != FormulaToken.tRefN1)
						{
							num = 5;
							continue;
						}
						return 1;
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
							num = 0;
							continue;
						}
						break;
					case 6:
						goto IL_6C;
					}
					break;
				}
			}
		}
		return 2;
		IL_6C:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆❈⽊⡌㝎", a_));
	}

	// Token: 0x0600324B RID: 12875 RVA: 0x001D0354 File Offset: 0x001CF354
	public new static FormulaToken ᜀ(int A_0)
	{
		int a_ = 19;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case 1:
						return FormulaToken.tRefN1;
					case 2:
						return FormulaToken.tRefN2;
					case 3:
						return FormulaToken.tRefN3;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return FormulaToken.tRefN2;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_79;
				}
				break;
			}
		}
		return FormulaToken.tRefN2;
		IL_79:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁈╊⥌⩎⥐", a_));
	}
}
