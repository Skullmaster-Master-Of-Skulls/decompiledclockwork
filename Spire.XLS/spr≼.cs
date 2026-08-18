using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002D5 RID: 725
[sprᦱ]
internal class spr\u227C : spr\u25AF
{
	// Token: 0x06002C8C RID: 11404 RVA: 0x00190F90 File Offset: 0x0018FF90
	public override spr\u22EA ᜀ(string A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					if (this.ᜎ)
					{
						num = 15;
						continue;
					}
					goto IL_2B4;
				case 1:
					goto IL_1DA;
				case 2:
					goto IL_267;
				case 3:
					goto IL_156;
				case 4:
				{
					int num2;
					if (num2 > 1)
					{
						num = 16;
						continue;
					}
					goto IL_2B4;
				}
				case 6:
					goto IL_DD;
				case 7:
				{
					int num2;
					if (num3 < num2)
					{
						num = 14;
						continue;
					}
					this.ᜂ = this.ᜊ;
					this.ᜃ = this.ᜋ;
					this.ᜄ = this.ᜌ;
					this.ᜅ = this.\u170D;
					num = 1;
					continue;
				}
				case 8:
				{
					string[] array;
					if (!spr\u25AF.ᜀ(array[num3], out this.ᜂ, out this.ᜃ, out this.ᜄ, out this.ᜅ))
					{
						num = 18;
						continue;
					}
					goto IL_2DA;
				}
				case 9:
					num = 12;
					continue;
				case 10:
				{
					int num2;
					if (num3 < num2)
					{
						num = 9;
						continue;
					}
					goto IL_202;
				}
				case 11:
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
					int num2 = array.Length;
					num = 13;
					continue;
				}
				case 12:
				{
					string[] array;
					if (!spr\u25AF.ᜀ(array[num3], out this.ᜊ, out this.ᜋ, out this.ᜌ, out this.\u170D))
					{
						num = 6;
						continue;
					}
					num3++;
					num = 19;
					continue;
				}
				case 13:
				{
					string[] array;
					if (array[0].ToLower() != RecordTableEnumerator.b("♄⡆㥈㉊", a_))
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
				}
				case 14:
					num = 8;
					continue;
				case 15:
				{
					int num2;
					num2--;
					num = 17;
					continue;
				}
				case 16:
				{
					int num2;
					string[] array;
					this.ᜎ = (array[num2 - 1].ToLower() == RecordTableEnumerator.b("㙄㍆え❊⡌㱎", a_));
					num = 0;
					continue;
				}
				case 17:
					goto IL_2B4;
				case 18:
					goto IL_198;
				case 19:
					goto IL_202;
				case 20:
					num = 11;
					continue;
				}
				if (A_0 != null)
				{
					num = 20;
					continue;
				}
				goto IL_19A;
				IL_202:
				num = 7;
				continue;
				IL_2B4:
				num3 = 1;
				num = 10;
			}
			IL_DD:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_198:
				return null;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				return null;
			}
			IL_156:
			return null;
			IL_19A:
			return null;
			IL_1DA:
			goto IL_2DA;
			IL_267:
			goto IL_19A;
			IL_2DA:
			return (spr\u22EA)this.ᜅ();
		}
		}
	}

	// Token: 0x06002C8D RID: 11405 RVA: 0x00191284 File Offset: 0x00190284
	public override void ᜀ(IWorksheet A_0, Point A_1, ref int A_2, ref int A_3, IList<long> A_4, spr\u2064 A_5)
	{
		switch (0)
		{
		default:
		{
			XlsWorksheet xlsWorksheet;
			IXLSRange a_;
			IXLSRange a_2;
			for (;;)
			{
				int dx = A_2 - A_1.X;
				int dy = A_3 - A_1.Y;
				Point point = base.ᜀ(A_1, A_5.ᜀ());
				Point point2 = spr\u25AF.ᜀ(A_1, this.ᜂ, this.ᜃ, this.ᜄ, this.ᜅ, A_5.ᜀ());
				Point point3 = point;
				point3.Offset(dx, dy);
				xlsWorksheet = (XlsWorksheet)A_0;
				a_ = A_0.AllocatedRange[point3.X, point3.Y];
				a_2 = A_0.AllocatedRange[point.X, point.Y, point2.X, point2.Y];
				if (true)
				{
				}
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
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_136;
						case 1:
							num = 0;
							continue;
						case 2:
							goto IL_11D;
						case 3:
							if (!this.ᜎ)
							{
								num = 1;
								continue;
							}
							num = 2;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
			IL_11D:
			CopyRangeOptions copyRangeOptions = CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges;
			goto IL_13A;
			IL_136:
			copyRangeOptions = CopyRangeOptions.All;
			IL_13A:
			CopyRangeOptions a_3 = copyRangeOptions;
			xlsWorksheet.ᜁ(a_, a_2, a_3);
			return;
		}
		}
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x001913DC File Offset: 0x001903DC
	public override int ᜀ()
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
		return base.ᜀ() + 1;
	}

	// Token: 0x06002C8F RID: 11407 RVA: 0x00191420 File Offset: 0x00190420
	public override bool ᜃ()
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
		return true;
	}

	// Token: 0x04001492 RID: 5266
	private new const string ᜀ = "copy";

	// Token: 0x04001493 RID: 5267
	private new const CopyRangeOptions ᜁ = CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges;

	// Token: 0x04001494 RID: 5268
	private new int ᜂ;

	// Token: 0x04001495 RID: 5269
	private int ᜃ;

	// Token: 0x04001496 RID: 5270
	private new bool ᜄ;

	// Token: 0x04001497 RID: 5271
	private new bool ᜅ;
}
