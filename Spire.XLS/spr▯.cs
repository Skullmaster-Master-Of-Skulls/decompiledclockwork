using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000246 RID: 582
[sprᦱ]
internal class spr\u25AF : spr\u22EA
{
	// Token: 0x06002342 RID: 9026 RVA: 0x00146990 File Offset: 0x00145990
	public spr\u25AF()
	{
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x001469A4 File Offset: 0x001459A4
	public spr\u25AF(int A_0, int A_1, bool A_2, bool A_3)
	{
		this.ᜊ = A_0;
		this.ᜋ = A_1;
		this.ᜌ = A_2;
		this.\u170D = A_3;
	}

	// Token: 0x06002344 RID: 9028 RVA: 0x001469D4 File Offset: 0x001459D4
	public override spr\u22EA ᜀ(string A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					if (num2 > 1)
					{
						num = 1;
						continue;
					}
					goto IL_76;
				case 1:
				{
					if (true)
					{
					}
					string[] array;
					this.ᜎ = (array[num2 - 1].ToLower() == RecordTableEnumerator.b("㌿㙁㵃⩅ⵇ㥉", a_));
					num = 3;
					continue;
				}
				case 2:
					goto IL_143;
				case 3:
					if (this.ᜎ)
					{
						num = 4;
						continue;
					}
					goto IL_76;
				case 4:
					num2--;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_121;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 5:
					goto IL_121;
				case 6:
				{
					if (A_0.Length == 0)
					{
						num = 2;
						continue;
					}
					string[] array = A_0.Split(new char[]
					{
						':'
					});
					num2 = array.Length;
					num = 8;
					continue;
				}
				case 7:
					num = 13;
					continue;
				case 8:
				{
					string[] array;
					if (array[0].ToLower() != RecordTableEnumerator.b("㌿⥁ⵃ㙅", a_))
					{
						num = 14;
						continue;
					}
					num = 0;
					continue;
				}
				case 10:
					num = 6;
					continue;
				case 11:
					goto IL_76;
				case 12:
					if (num3 > 0)
					{
						num = 7;
						continue;
					}
					goto IL_1F7;
				case 13:
				{
					string[] array;
					if (!spr\u25AF.ᜀ(array[num3], out this.ᜊ, out this.ᜋ, out this.ᜌ, out this.\u170D))
					{
						num = 5;
						continue;
					}
					goto IL_1F7;
				}
				case 14:
					goto IL_1D1;
				}
				if (A_0 != null)
				{
					num = 10;
					continue;
				}
				break;
				IL_76:
				num3 = num2 - 1;
				num = 12;
			}
			IL_E2:
			return null;
			IL_121:
			return null;
			IL_143:
			goto IL_E2;
			IL_1D1:
			return null;
			IL_1F7:
			return (spr\u22EA)this.ᜅ();
		}
		}
	}

	// Token: 0x06002345 RID: 9029 RVA: 0x00146BE4 File Offset: 0x00145BE4
	public override void ᜀ(IWorksheet A_0, Point A_1, ref int A_2, ref int A_3, IList<long> A_4, spr\u2064 A_5)
	{
		int a_ = 9;
		switch (0)
		{
		default:
			for (;;)
			{
				int iSourceRow = A_2;
				int iSourceColumn = A_3;
				Point point = this.ᜀ(A_1, A_5.ᜀ());
				A_2 = point.X;
				A_3 = point.Y;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (this.ᜎ)
						{
							num = 4;
							continue;
						}
						return;
					case 2:
						goto IL_C3;
					case 3:
						if (A_2 != 0)
						{
							num = 7;
							continue;
						}
						return;
					case 4:
						num = 6;
						continue;
					case 5:
						num = 3;
						continue;
					case 6:
						if (A_3 != 0)
						{
							goto IL_D6;
						}
						return;
					case 7:
						num = 8;
						continue;
					case 8:
					{
						if (A_0 == null)
						{
							num = 2;
							continue;
						}
						XlsWorksheet xlsWorksheet = (XlsWorksheet)A_0;
						xlsWorksheet.CellRecords.CopyStyle(iSourceRow, iSourceColumn, A_2, A_3);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D6;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					}
					break;
					IL_D6:
					num = 5;
				}
			}
			IL_C3:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀♂⁄㍆", a_));
		}
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x00146D44 File Offset: 0x00145D44
	protected new Point ᜀ(Point A_0, IWorkbook A_1)
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
		return spr\u25AF.ᜀ(A_0, this.ᜊ, this.ᜋ, this.ᜌ, this.\u170D, A_1);
	}

	// Token: 0x06002347 RID: 9031 RVA: 0x00146DA0 File Offset: 0x00145DA0
	private new static bool ᜀ(string A_0, out int A_1)
	{
		int num = 2;
		double num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return false;
			case 1:
				if (Math.Abs(num2) <= 2147483647.0)
				{
					goto IL_B6;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 3:
				if (true)
				{
				}
				goto IL_3A;
			case 4:
				goto IL_38;
			case 5:
				if (double.TryParse(A_0, NumberStyles.Integer, null, out num2))
				{
					num = 3;
					continue;
				}
				return false;
			}
			if (A_0.Length == 0)
			{
				num = 4;
				continue;
			}
			A_1 = 0;
			num = 5;
			continue;
			IL_3A:
			num = 1;
		}
		IL_38:
		A_1 = 0;
		return true;
		IL_B6:
		A_1 = (int)num2;
		return true;
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x00146E68 File Offset: 0x00145E68
	protected new static bool ᜀ(string A_0, out int A_1, out int A_2, out bool A_3, out bool A_4)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				A_1 = (A_2 = 0);
				A_3 = (A_4 = false);
				int num = 6;
				for (;;)
				{
					Match match;
					string value;
					bool flag2;
					switch (num)
					{
					case 0:
						if (match.Success)
						{
							num = 7;
							continue;
						}
						num = 14;
						continue;
					case 1:
						A_3 = (match.Groups[1].Value.Length > 0);
						A_4 = (match.Groups[2].Value.Length > 0);
						value = match.Groups[RecordTableEnumerator.b("㩇╉㭋", a_)].Value;
						num = 11;
						continue;
					case 2:
						if (!spr\u25AF.ᜀ(value, out A_2))
						{
							num = 3;
							continue;
						}
						num = 10;
						continue;
					case 3:
						return false;
					case 4:
						if (A_1 == 0)
						{
							num = 8;
							continue;
						}
						goto IL_ED;
					case 5:
						goto IL_ED;
					case 6:
						if (A_0 != null)
						{
							num = 17;
							continue;
						}
						return false;
					case 7:
						num = 15;
						continue;
					case 8:
						A_3 = true;
						num = 5;
						continue;
					case 9:
						goto IL_14C;
					case 10:
						if (A_2 == 0)
						{
							if (true)
							{
							}
							num = 18;
							continue;
						}
						return flag;
					case 11:
						goto IL_1B3;
					case 12:
						goto IL_28A;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B3;
						default:
							goto IL_1DE;
						}
						break;
					case 14:
						flag2 = false;
						goto IL_28F;
					case 15:
						flag2 = (match.Length == A_0.Length);
						goto IL_28F;
					case 16:
						if (A_0.Length == 0)
						{
							num = 12;
							continue;
						}
						match = spr\u25AF.ᜉ.Match(A_0.ToLower());
						num = 0;
						continue;
					case 17:
						num = 16;
						continue;
					case 18:
						A_4 = true;
						num = 9;
						continue;
					case 19:
						if (flag)
						{
							num = 1;
							continue;
						}
						return flag;
					}
					break;
					IL_ED:
					value = match.Groups[RecordTableEnumerator.b("⭇╉⁋㭍㵏㱑", a_)].Value;
					num = 2;
					continue;
					IL_1B3:
					if (!spr\u25AF.ᜀ(value, out A_1))
					{
						num = 13;
						continue;
					}
					num = 4;
					continue;
					IL_28F:
					flag = flag2;
					num = 19;
				}
			}
			return false;
			IL_14C:
			return flag;
			IL_1DE:
			if (false)
			{
			}
			return false;
			IL_28A:
			return false;
		}
		}
	}

	// Token: 0x06002349 RID: 9033 RVA: 0x0014712C File Offset: 0x0014612C
	protected new static Point ᜀ(Point A_0, int A_1, int A_2, bool A_3, bool A_4, IWorkbook A_5)
	{
		int num = 11;
		int num2;
		int num4;
		for (;;)
		{
			int num3;
			int num5;
			switch (num)
			{
			case 0:
				if (num2 > A_5.MaxColumnCount)
				{
					num = 6;
					continue;
				}
				goto IL_155;
			case 1:
				num = 12;
				continue;
			case 2:
				goto IL_A9;
			case 3:
				num = 0;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E0;
				default:
					if (false)
					{
					}
					num3 = A_0.X + A_1;
					goto IL_138;
				}
				break;
			case 5:
				if (num4 >= 1)
				{
					num = 14;
					continue;
				}
				goto IL_A9;
			case 6:
				goto IL_ED;
			case 7:
				goto IL_155;
			case 8:
				if (!A_3)
				{
					goto IL_E0;
				}
				num = 4;
				continue;
			case 9:
				num5 = A_2;
				goto IL_D1;
			case 10:
				num5 = A_0.Y + A_2;
				goto IL_D1;
			case 12:
				num3 = A_1;
				goto IL_138;
			case 13:
				if (num4 > A_5.MaxRowCount)
				{
					num = 2;
					continue;
				}
				goto IL_19C;
			case 14:
				num = 13;
				continue;
			case 15:
				if (num2 >= 1)
				{
					num = 3;
					continue;
				}
				goto IL_ED;
			case 16:
				goto IL_BE;
			case 17:
				num = 9;
				continue;
			}
			if (!A_4)
			{
				num = 17;
				continue;
			}
			num = 10;
			continue;
			IL_A9:
			if (true)
			{
			}
			num4 = 0;
			num = 16;
			continue;
			IL_D1:
			num2 = num5;
			num = 8;
			continue;
			IL_E0:
			num = 1;
			continue;
			IL_ED:
			num2 = 0;
			num = 7;
			continue;
			IL_138:
			num4 = num3;
			num = 15;
			continue;
			IL_155:
			num = 5;
		}
		IL_BE:
		IL_19C:
		return new Point(num4, num2);
	}

	// Token: 0x0600234A RID: 9034 RVA: 0x001472DC File Offset: 0x001462DC
	public override int ᜀ()
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

	// Token: 0x0600234B RID: 9035 RVA: 0x00147318 File Offset: 0x00146318
	public override bool ᜁ()
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
		return true;
	}

	// Token: 0x0600234C RID: 9036 RVA: 0x00147354 File Offset: 0x00146354
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u25AF()
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u25AF.ᜉ = new Regex(RecordTableEnumerator.b("㭈捊ᅌᑎ硐汒絔桖敘⥚㉜⡞彠㡢㥤䩦㑨呪㙬㍮䅰干䱴⩶ɸ䭺兼䩾ﲀꪂ궄뢆ꆈ몊ꒌ펎첐몒뾖얘삚뒜ꂞ覠鲢馤쒦욨잪\ud8ac슮\udfb0趲钸芼ﳄ髆니﯊ﳎ곐㮝﷔菞볠쫢", a_), RegexOptions.Compiled);
	}

	// Token: 0x04001212 RID: 4626
	protected new const string ᜀ = "row";

	// Token: 0x04001213 RID: 4627
	protected new const string ᜁ = "column";

	// Token: 0x04001214 RID: 4628
	protected const string ᜂ = "Styles";

	// Token: 0x04001215 RID: 4629
	private const string ᜃ = "skip";

	// Token: 0x04001216 RID: 4630
	protected const string ᜄ = "styles";

	// Token: 0x04001217 RID: 4631
	protected new const int ᜅ = 1;

	// Token: 0x04001218 RID: 4632
	protected const int ᜆ = 2;

	// Token: 0x04001219 RID: 4633
	protected const int ᜇ = 2;

	// Token: 0x0400121A RID: 4634
	protected const string ᜈ = "r(\\[)?(?<row>[\\-]?[\\0-9]{0,5})(?(1)\\])c(\\[)?(?<column>[\\-]?[0-9]{0,3})(?(2)\\])";

	// Token: 0x0400121B RID: 4635
	private static readonly Regex ᜉ;

	// Token: 0x0400121C RID: 4636
	protected int ᜊ;

	// Token: 0x0400121D RID: 4637
	protected int ᜋ;

	// Token: 0x0400121E RID: 4638
	protected bool ᜌ;

	// Token: 0x0400121F RID: 4639
	protected bool \u170D;

	// Token: 0x04001220 RID: 4640
	protected bool ᜎ;
}
